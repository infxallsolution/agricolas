using Microsoft.Reporting.WebForms;
using Ninject.Activation;
using Nomina.WebForms.App_Code.General;
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

namespace Nomina.WebForms.Formas.Pprogramacion
{
    public partial class ImprimeInforme : BasePage
    {
        #region Atributos

        ReportParameter rpEmpresa;
        ReportParameter rpNumero;
        ReportParameter rpEmpleado;
        Cgeneral general = new Cgeneral();

        #endregion Atributos

        #region Metodos

        private void SetParametrosReportes()
        {
            rpEmpresa = new ReportParameter("empresa", Convert.ToString(this.Session["empresa"]));

            if (this.Request.QueryString["informe"].ToString() == "CompensarorioFuncionario")
            {
                rpEmpleado = new ReportParameter("funcionario", Request.QueryString["funcionario"].ToString());
                rpNumero = new ReportParameter("numero", Request.QueryString["numero"].ToString());
            }

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
                    string urlFormato = null, urlInformes = null;
                    string ReportService = general.RetornoParametroGeneral("ReportService", Convert.ToInt16(Session["empresa"]));
                    string tipoRemision = general.RetornoParametroGeneral("remisionBascula", Convert.ToInt16(Session["empresa"]));
                    DataView dvDatosReportes = general.retornaDatosReporteador(ConfigurationManager.AppSettings["modulo"].ToString());
                    string formato = this.Request.QueryString["informe"].ToString();

                    if (formato.Trim().Length == 0)
                    {
                        ManejoError("Debe parametrizar un formato para el tipo de transacción seleccionado ", "I");
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
                        else
                            urlFormato = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(0).ToString();

                        if (dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1) is DBNull)
                        {
                            ManejoError("No se han parametrizado la url de reportes ", "I");
                            return;
                        }
                        else
                            urlInformes = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString();
                    }


                    SetParametrosReportes();
                    Uri url = new Uri(ReportService);

                    switch (formato)
                    {
                        case "FuncionarioProgramado":
                            this.rvImprimir.ServerReport.ReportPath = urlInformes + formato;
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;

                        case "PersonalEnPlanta":
                            this.rvImprimir.ServerReport.ReportPath = urlInformes + formato;
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;

                        case "CompensarorioFuncionario":
                            this.rvImprimir.ServerReport.ReportPath = urlInformes + formato;
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa, rpEmpleado, rpNumero });
                            break;
                    }
                }
            }
        }

        #endregion Eventos

        #region ClaseInterna

        [Serializable]
        public sealed class MyReportServerCredentials : IReportServerCredentials
        {
            private int empresa;
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

            public ICredentials NetworkCredentials
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

            public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
            {
                authCookie = null;
                userName = null;
                password = null;
                authority = null;
                return false;
            }
        }

        #endregion ClaseInterna
    }
}