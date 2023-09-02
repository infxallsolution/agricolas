using Almacen.seguridadInfos;
using Almacen.WebForms.App_Code.General;
using Almacen.WebForms.App_Code.Transaccion;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Almacen.WebForms.Formas.Pinformes
{
    public partial class ImprimeTransaccion : BasePage
    {
        #region Atributos
        ReportParameter rpEmpresa;
        ReportParameter rpNumero;
        ReportParameter rpTipo;

        #endregion Atributos

        #region Instancias
        Security seguridad = new Security();

        CIP ip = new CIP();
        Ctransacciones transaccion = new Ctransacciones();

        #endregion Instancias

        #region Metodos
        private void ManejoError(string error, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), error, "error");
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion,
                ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "er",
                error, ip.ObtenerIP(), Convert.ToInt16(Session["empresa"]));
        }

        private void SetParametrosReportes(string empresa, string tipo, string numero)
        {
            rpEmpresa = new ReportParameter("empresa", empresa);
            rpTipo = new ReportParameter("tipo", tipo);
            rpNumero = new ReportParameter("numero", numero);
        }

        #endregion Metodos

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
            {
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    //System.Uri url = new Uri(ConfigurationManager.AppSettings["ReportService"].ToString());

                    //this.rvImprimir.ServerReport.ReportServerCredentials = new MyReportServerCredentials();
                    //this.rvImprimir.ServerReport.ReportServerUrl = url;
                    //string tipo = this.Request.QueryString["tipo"].ToString();
                    //string numero = this.Request.QueryString["numero"].ToString();
                    //string formato = transaccion.SeleccionaFomatoTransaccion(Convert.ToInt32(this.Session["empresa"]), tipo);

                    //if (formato.Trim().Length == 0)
                    //{
                    //    ManejoError("Error al imprimir transaccion el formato de impresión no esta configurado", "I");
                    //    return;
                    //}
                    //else
                    //{
                    //    this.rvImprimir.ServerReport.ReportPath = ConfigurationManager.AppSettings["UrlFormatos"].ToString() + formato;
                    //    this.rvImprimir.ServerReport.Refresh();
                    //    this.rvImprimir.ServerReport.ReportServerUrl = url;
                    //    SetParametrosReportes( Convert.ToString(this.Session["empresa"]) , tipo, numero);
                    //    this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa, rpTipo, rpNumero });
                    //}

                    Cgeneral general = new Cgeneral();
                    string ReportService = general.RetornoParametroGeneral("ReportService", Convert.ToInt16(Session["empresa"]));
                    DataView dvDatosReportes = general.retornaDatosReporteador(ConfigurationManager.AppSettings["modulo"].ToString());
                    string tipoRemision = Request.QueryString["tipo"].ToString();
                    string formato = general.retornaFormatoTipoTransaccion(tipoRemision, Convert.ToInt16(Session["empresa"]));


                    if (ReportService.Trim().Length == 0)
                    {
                        ManejoError("Debe configurar la url del reporteador", "I");
                        return;
                    }

                    if (dvDatosReportes.Table.Rows.Count == 0)
                    {
                        ManejoError("No se han parametrizado ", "I");
                        return;
                    }
                    else
                    {
                        if (dvDatosReportes.Table.Rows[0].ItemArray.GetValue(0) is DBNull)
                        {
                            ManejoError("No se han parametrizado la url de formatos ", "I");
                            return;
                        }

                        if (dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1) is DBNull)
                        {
                            ManejoError("No se han parametrizado la url de reportes ", "I");
                            return;
                        }
                    }


                    System.Uri url = new Uri(ReportService);
                    this.rvImprimir.ServerReport.ReportServerCredentials = new MyReportServerCredentials(Convert.ToInt16(Session["empresa"]));
                    this.rvImprimir.ServerReport.ReportServerUrl = url;
                    this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(0).ToString() + formato;
                    SetParametros();
                    this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa, rpTipo, rpNumero });
                    this.rvImprimir.ServerReport.Refresh();

                }
            }
        }

        private void SetParametros()
        {
            rpTipo = new ReportParameter("tipo", Request.QueryString["tipo"].ToString());
            rpNumero = new ReportParameter("numero", Request.QueryString["numero"].ToString());
            rpEmpresa = new ReportParameter("empresa", Request.QueryString["empresa"].ToString());
        }


        #endregion Eventos

        #region ClaseInterna

        [Serializable]
        public sealed class MyReportServerCredentials :
            IReportServerCredentials
        {
            private int empresa;

            public MyReportServerCredentials(int empresa)
            {
                this.empresa = empresa;
            }
            public WindowsIdentity ImpersonationUser
            {
                get
                {
                    return null;
                }
            }

            public int Empresa
            {
                get
                {
                    return empresa;
                }

                set
                {
                    empresa = value;
                }
            }

            ICredentials IReportServerCredentials.NetworkCredentials
            {
                get
                {
                    Cgeneral cgeneral = new Cgeneral();

                    // User name
                    string userName = cgeneral.RetornoParametroGeneral("usuarioReporte", empresa);
                    string password = cgeneral.RetornoParametroGeneral("claveReporte", empresa);
                    string dominio = cgeneral.RetornoParametroGeneral("DominioReporte", empresa);

                    return new NetworkCredential(userName, password, dominio);
                }
            }



            public bool GetFormsCredentials(out Cookie authCookie,
                        out string userName, out string password,
                        out string authority)
            {
                authCookie = null;
                userName = null;
                password = null;
                authority = null;

                // Not using form credentials
                return false;
            }
        }

        #endregion ClaseInterna
    }
}