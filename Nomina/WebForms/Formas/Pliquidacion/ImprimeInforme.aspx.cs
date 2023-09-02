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

namespace Nomina.WebForms.Formas.Pliquidacion
{
    public partial class ImprimeInforme : BasePage
    {
        #region Atributos

        ReportParameter rpEmpresa;
        ReportParameter rpAño;
        ReportParameter rpPeriodo;
        ReportParameter rpNumero;
        ReportParameter rpEmpleado;
        ReportParameter rpPeriodoInicial;
        ReportParameter rpPeriodoFinal;
        ReportParameter rpRegistro;

        #endregion Atributos

        #region Instancias


        #endregion Instancias

        #region Metodos

        private void SetParametrosReportes()
        {
            rpEmpresa = new ReportParameter("empresa", Convert.ToString(this.Session["empresa"]));


            if (this.Request.QueryString["informe"].ToString() == "liquidacionNomina")
            {
                rpAño = new ReportParameter("año", Request.QueryString["ano"].ToString());
                rpPeriodo = new ReportParameter("periodo", Request.QueryString["periodo"].ToString());
                rpNumero = new ReportParameter("numero", Request.QueryString["numero"].ToString());
            }

            if (this.Request.QueryString["informe"].ToString() == "LiquidacionCesantias")
            {
                rpAño = new ReportParameter("año", Request.QueryString["ano"].ToString());
                rpNumero = new ReportParameter("numero", Request.QueryString["numero"].ToString());
            }


            if (this.Request.QueryString["informe"].ToString() == "liquidacionContrato")
                rpNumero = new ReportParameter("numero", Request.QueryString["numero"].ToString());

            if (this.Request.QueryString["informe"].ToString() == "VacacionesxEmpleado")
            {
                rpEmpleado = new ReportParameter("empleado", Request.QueryString["empleado"].ToString());
                rpPeriodoInicial = new ReportParameter("periodoInicial", Request.QueryString["periodoInicial"].ToString());
                rpPeriodoFinal = new ReportParameter("periodoFinal", Request.QueryString["PeriodoFinal"].ToString());
                rpRegistro = new ReportParameter("registro", Request.QueryString["registro"].ToString());
            }

            if (this.Request.QueryString["informe"].ToString() == "PagoNominaPeriodo")
            {
                rpAño = new ReportParameter("año", Request.QueryString["año"].ToString());
                rpPeriodo = new ReportParameter("periodo", Request.QueryString["periodo"].ToString());
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
                    Cgeneral general = new Cgeneral();
                    string ReportService = general.RetornoParametroGeneral("ReportService", Convert.ToInt16(Session["empresa"]));
                    DataView dvDatosReportes = general.retornaDatosReporteador(ConfigurationManager.AppSettings["modulo"].ToString());

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
                    this.rvImprimir.ServerReport.ReportServerCredentials = new MyReportServerCredentials(empresa: Convert.ToInt32(this.Session["empresa"]));
                    this.rvImprimir.ServerReport.ReportServerUrl = url;

                    SetParametrosReportes();

                    switch (this.Request.QueryString["informe"].ToString())

                    {

                        case "Preliquidacion":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "PreLiquidacionPrimas":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "PreLiquidacionCesantias":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "liquidacionNomina":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa, rpAño, rpPeriodo, rpNumero });
                            break;

                        case "LiquidacionCesantias":
                            this.rvImprimir.ServerReport.ReportPath = ConfigurationManager.AppSettings["UrlInformes"].ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa, rpNumero, rpAño });
                            break;

                        case "liquidacionContrato":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa, rpNumero });
                            break;

                        case "VacacionesxEmpleado":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa, rpEmpleado, rpPeriodoFinal, rpPeriodoInicial, rpRegistro });
                            break;

                        case "PagoNominaPeriodo":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa, rpAño, rpPeriodo, rpNumero });
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