using Agronomico.WebForms.App_Code.General;
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

namespace Agronomico.WebForms.Formas.Pinformes
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

                        case "CiclosLabor":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "RendimientoTrabajadorCosecha":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "ProduccionFrutaLoteAnualNuevo":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "RevisionLaboresFinca":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;

                        case "ProduccionCorteFinca":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "ReviLaboresFecha":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;

                        case "ResumenContratistaTerceroNovedad":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "ResumenContratistaTercero":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "RevisaFrutaCuadrilla":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "PreliquidacionDescuentoTerceroBase":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "RevisionLabores":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "ReviLabores":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "RelacionControlCorteGrupo":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "CargadoresFincaSeccionTercero":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "CargadoresFincaTercero":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "LaboresFechaTerceroNovedad":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "ProduccionFrutaFinca":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "DiferenciaLoteCorte":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "LotesFinca":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "LotesFincaSeccionSiembra":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "LotesFincaSeccionVariedad":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "HVLotesNovedad":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "LotesLineaPalma":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "CiclosCorte":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "IndicadorAgronomico":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "TiquetesFrutaFechaPendiente":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "InformeGeneralLabores":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "TiquetesAgronomico":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;

                        case "LotesFincaSeccion":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "LaboresFechaTercero":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "LaboresFechaCentroCosto":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "LaboresDetalles":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "PesoPromedioLote":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "Labores":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "LaboresFechaLote":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "ListaPrecioLote":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "ProduccionFrutaLote":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "ProduccionFrutaLoteAnual":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "TiquetesFrutaFecha":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "LiquidacionContratista":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "LiquidacionContratistaLote":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "ResumenContratista":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;

                        case "LaboresFechaLoteMes":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "FincalLoteCanal":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;

                        case "GrupoCaracteristica":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "SanidadDetallado":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;

                        case "PlanFertilizacionSaldo":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;

                        case "LaboresFechaLoteFertilizacion":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;


                        case "LaboresFertilizacionDetalle":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;

                        case "PlanFertilizacionSaldoTransaccion":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;

                        case "TransaccionesOtros":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "RendimientoTrabajador":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;
                        case "IndicadorAgronomicoAño":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                            break;


                        case "LaboresLoteFecha":
                            this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
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