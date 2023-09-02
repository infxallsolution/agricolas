using Microsoft.Reporting.WebForms;
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

namespace Nomina.WebForms.Formas.Pinformes
{
    public partial class ImprimeInforme : BasePage
    {
        #region Atributos

        ReportParameter rpEmpresa;

        #endregion Atributos


        #region Metodos

        private void SetParametrosReportes()
        {
            rpEmpresa = new ReportParameter("empresa", Convert.ToString(this.Session["empresa"]));
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

                        case "PreliquidacionResumenDescuentos":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "PreliquidacionSupera":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "LiquidacionCesantias":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "PreliquidacionDescuento":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "PreliquidacionResumen":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;

                        case "PreLiquidacionPrimasConcepto":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        //case "HojaVida":
                        //    this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                        //    this.rvImprimir.ServerReport.Refresh();
                        //    this.rvImprimir.ServerReport.ReportServerUrl = url;
                        //    SetParametrosReportes();
                        //    this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                        //    break;
                        case "NovedadDetalle":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "FormatoIR":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "Ausentismos":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "ContratosDestajos":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "IngresoReteDetalle":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "ConsolidadoVacaciones":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "liquidacionContratoInforme":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "PrestamoSaldos":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "PrestamoSaldosConcepto":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "Prestamos":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "IBCTrabajador":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "liquidacionNominaTrabajador":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "Funcionarios":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "RelacionDescuentosTercero":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "RelacionNovedadesPeriodo":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "VencerContratos":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "Contratos":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "TrabajadoresxCcosto":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "Preliquidacion":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "InformeLiquidacionPrimas":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "Conceptos":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "RegistroNovedades":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "NovedadesPeriodicas":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "NovedadesTrabajadorFecha":
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
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "ResumenPrenomina":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "ResumenLiquidacionMes":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "ResumenLiquidacion":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "DesprendibleNomina":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "PagoNominaPeriodo":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "ResumenDescuentos":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "SeguridadSocialPeriodo":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "AcumuladosTerceroPeriodo":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "AcumuladosTerceroAño":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "LiquidacionHoras":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "LiquidacionHorasTotales":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "AcumuladosPrestaciones":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "Laboresnopagadasfecha":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;

                        case "SeguridadSocialEntidades":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;


                        case "VacacionesxPeriodo":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;

                        case "LiquidacionNominaInforme":
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

                        case "DescuentosLiquidacionNomina":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;


                        case "IBCSeguridadSocialPeriodo":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;

                        case "InformeSeguridadSocialNomina":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;

                        case "Chequebancolombia":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;

                        case "ResumenPagos":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;


                        case "DescuentoEntidadSS":

                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;

                        case "DesprendibleNominaGeneral":

                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;

                        case "DescuentoEntidadSSPeriodo":

                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;

                        case "PreliquidacionSemanal":

                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "liquidacionNominaSemanal":

                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "DesprendibleNominaSemanal":

                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;


                        case "PagoNominaPeriodoDetallado":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;

                        case "ResumenDescuentosBananera":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;

                        default:
                            rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
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