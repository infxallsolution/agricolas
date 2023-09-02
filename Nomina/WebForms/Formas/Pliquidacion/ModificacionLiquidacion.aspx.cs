using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
using Nomina.WebForms.App_Code.Liquidacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Pliquidacion
{
    public partial class ModificacionLiquidacion : BasePage
    {

        public List<LiquidacionDetalle> ListadoDetalleLiquidacion
        {
            get
            {
                object o = ViewState["ListadoDetalleLiquidacion"];
                return (o == null) ? null : (List<LiquidacionDetalle>)o;
            }
            set
            {
                ViewState["ListadoDetalleLiquidacion"] = value;
            }
        }


        #region Instancias



        Cperiodos periodo = new Cperiodos();
        CModificacionNomina modificacionNomina = new CModificacionNomina();
        #endregion Instancias

        #region Metodos

        private void cargarConcepto()
        {
            try
            {
                this.ddlConcepto.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nConcepto", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlConcepto.DataValueField = "codigo";
                this.ddlConcepto.DataTextField = "descripcion";

                this.ddlConcepto.DataBind();
                this.ddlConcepto.Items.Insert(0, new ListItem("", ""));

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar conceptos. Correspondiente a: " + ex.Message, "C");
            }
        }

        private void cargarPeriodos()
        {
            try
            {
                this.ddlPeriodo.DataSource = periodo.PeriodosAbiertoNominaAño(Convert.ToInt32(ddlAño.SelectedValue.Trim()), Convert.ToInt16(Session["empresa"])).Tables[0].DefaultView;
                this.ddlPeriodo.DataValueField = "noPeriodo";
                this.ddlPeriodo.DataTextField = "descripcion";

                this.ddlPeriodo.DataBind();
                if (this.ddlPeriodo.Items.Count == 0)
                {
                    limpiarPeriodos();
                    return;
                }
                this.ddlPeriodo.Items.Insert(0, new ListItem("", ""));
                this.ddlPeriodo.Enabled = true;
                limpiarTipoDeDocumento();
                if (ddlPeriodo.Items.Count == 2)
                {
                    ddlPeriodo.SelectedIndex = 1;
                    cargarTipoDeDocumento();
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar periodo inicial. Correspondiente a: " + ex.Message, "C");
            }
        }


        private void CargarCombos()
        {
            try
            {
                this.ddlAño.DataSource = periodo.PeriodoAñoAbiertoNomina(Convert.ToInt16(Session["empresa"]));
                this.ddlAño.DataValueField = "año";
                this.ddlAño.DataTextField = "año";

                this.ddlAño.DataBind();
                this.ddlAño.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar año. Correspondiente a: " + ex.Message, "C");
            }
        }

        #endregion Metodos

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
                    return;
                }
                if (!IsPostBack)
                {
                    CargarCombos();
                    cargarConcepto();
                    InitView();
                }
                else
                {
                    ListadoDetalleLiquidacion = new List<LiquidacionDetalle>();
                    foreach (GridViewRow dr in gvDetalleLiquidacion.Rows)
                    {
                        var item = new LiquidacionDetalle();
                        item.CodConcepto = Server.HtmlDecode(dr.Cells[2].Text.ToString());
                        item.DescripcionConcepto = Server.HtmlDecode(dr.Cells[3].Text.ToString());
                        item.Cantidad = ((HiddenField)dr.FindControl("cantidad")).Value.ToString();
                        item.ValorUnitario = ((HiddenField)dr.FindControl("valorUnitario")).Value.ToString();
                        item.ValorTotal = ((HiddenField)dr.FindControl("valorTotal")).Value.ToString();
                        item.BaseSeguridadSocial = ((HiddenField)dr.FindControl("BaseSeguridadSocial")).Value.ToString() == true.ToString();
                        item.ValidaPorcentaje = ((HiddenField)dr.FindControl("ValidaPorcentaje")).Value.ToString() == true.ToString();
                        item.Deduccion = ((HiddenField)dr.FindControl("Deduccion")).Value.ToString() == true.ToString();
                        item.HabilitaValorTotal = ((HiddenField)dr.FindControl("HabilitaValorTotal")).Value.ToString() == true.ToString();
                        item.Porcentaje = ((HiddenField)dr.FindControl("Porcentaje")).Value.ToString();
                        item.ValorUnitario = Math.Round(Convert.ToDouble(item.ValorUnitario)).ToString();
                        item.ValorTotal = Math.Round(Convert.ToDouble(item.ValorTotal)).ToString();
                        item.Cantidad = Math.Round(Convert.ToDouble(item.Cantidad)).ToString();
                        item.saldo = ((HiddenField)dr.FindControl("saldo")).Value.ToString();
                        ListadoDetalleLiquidacion.Add(item);
                    }
                    var i = 1;
                    ListadoDetalleLiquidacion = ListadoDetalleLiquidacion.OrderBy(_i => _i.Deduccion ? 1 : 0).ToList();
                    ListadoDetalleLiquidacion.ForEach(_i => _i.RegistroDetalleNomina = (i++).ToString());

                }
            }
        }


        private void InitView()
        {
            detailLoadedPanel.Visible = false;
            detalleLiqidacion.Visible = false;
            //hasMadeChangesPanel.Visible = true;
            limpiarContrato();
            limpiarDetalle();
            limpiarDocumentos();
            limpiarEmpleados();
            limpiarPeriodos();
            limpiarTipoDeDocumento();
        }

        #endregion Eventos

        #region EventosFuncionario

        protected void ddlPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPeriodo.SelectedValue.Trim().Length > 0)
                cargarTipoDeDocumento();
            else
                limpiarTipoDeDocumento();
        }

        private void limpiarTipoDeDocumento()
        {
            ddlTipoDocumento.DataSource = null;

            ddlTipoDocumento.SelectedValue = null;
            ddlTipoDocumento.DataBind();
            ddlTipoDocumento.Enabled = false;
            limpiarDocumentos();
        }

        private void cargarTipoDeDocumento()
        {
            try
            {
                this.ddlTipoDocumento.DataSource = modificacionNomina.CargarTipoDeDocumento(Convert.ToInt32(ddlAño.SelectedValue), Convert.ToInt32(ddlPeriodo.SelectedValue), Convert.ToInt32(Session["empresa"]));
                this.ddlTipoDocumento.DataValueField = "codigo";
                this.ddlTipoDocumento.DataTextField = "tipo";
                this.ddlTipoDocumento.DataBind();
                if (this.ddlTipoDocumento.Items.Count == 0)
                {
                    limpiarTipoDeDocumento();
                    return;
                }
                this.ddlTipoDocumento.Items.Insert(0, new ListItem("", ""));
                this.ddlTipoDocumento.Enabled = true;
                limpiarDocumentos();
                if (ddlTipoDocumento.Items.Count == 2)
                {
                    ddlTipoDocumento.SelectedIndex = 1;
                    cargarDocumentos();
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar año. Correspondiente a: " + ex.Message, "C");
            }
        }

        #endregion EventosFuncionario

        protected void ddlAño_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAño.SelectedValue.Trim().Length > 0)
                cargarPeriodos();
            else
                limpiarPeriodos();
        }

        private void limpiarPeriodos()
        {

            ddlPeriodo.DataSource = null;
            ddlPeriodo.SelectedValue = null;
            ddlPeriodo.DataBind();
            ddlPeriodo.Enabled = false;
            limpiarTipoDeDocumento();
        }

        protected void ddlEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmpleado.SelectedValue.Trim().Length > 0)
                cargarContrato();
            else
                limpiarContrato();
        }

        private void limpiarContrato()
        {
            ddlContratos.DataSource = null;
            ddlContratos.SelectedValue = null;
            ddlContratos.DataBind();
            ddlContratos.Enabled = false;
            limpiarDetalle();
        }

        private void cargarContrato()
        {
            try
            {
                this.ddlContratos.DataSource = modificacionNomina.CargarContratos(
                    Convert.ToInt32(ddlAño.SelectedValue),
                    Convert.ToInt32(ddlPeriodo.SelectedValue),
                    ddlTipoDocumento.SelectedValue,
                    Convert.ToInt32(Session["empresa"]),
                    ddlNumeroDocumento.SelectedValue,
                    Convert.ToInt32(ddlEmpleado.SelectedValue));
                this.ddlContratos.DataValueField = "codContrato";
                this.ddlContratos.DataTextField = "descContrato";
                this.ddlContratos.DataBind();

                if (ddlContratos.Items.Count == 0)
                {
                    limpiarContrato();
                    return;
                }

                this.ddlContratos.Items.Insert(0, new ListItem("", ""));
                ddlContratos.Enabled = true;
                limpiarDetalle();
                if (ddlContratos.Items.Count == 2)
                {
                    ddlContratos.SelectedIndex = 1;
                    CargarDetalle();
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar empleados. Correspondiente a: " + ex.Message, "C");
            }
        }


        protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoDocumento.SelectedValue.Trim().Length > 0)
                cargarDocumentos();
            else
                limpiarDocumentos();
        }

        private void limpiarDocumentos()
        {
            ddlNumeroDocumento.DataSource = null;
            ddlNumeroDocumento.SelectedValue = null;
            ddlNumeroDocumento.DataBind();
            ddlNumeroDocumento.Enabled = false;
            limpiarEmpleados();
        }

        private void cargarDocumentos()
        {
            try
            {
                this.ddlNumeroDocumento.DataSource = modificacionNomina.CargarNumeroDeDocumento(Convert.ToInt32(ddlAño.SelectedValue), Convert.ToInt32(ddlPeriodo.SelectedValue), ddlTipoDocumento.SelectedValue, Convert.ToInt32(Session["empresa"]));
                this.ddlNumeroDocumento.DataValueField = "numero";
                this.ddlNumeroDocumento.DataTextField = "numero";
                this.ddlNumeroDocumento.DataBind();
                if (this.ddlNumeroDocumento.Items.Count == 0)
                {
                    limpiarDocumentos();
                    return;
                }
                this.ddlNumeroDocumento.Items.Insert(0, new ListItem("", ""));
                this.ddlNumeroDocumento.Enabled = true;
                limpiarEmpleados();
                if (ddlNumeroDocumento.Items.Count == 2)
                {
                    ddlNumeroDocumento.SelectedIndex = 1;
                    cargarEmpleados();
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar año. Correspondiente a: " + ex.Message, "C");
            }
        }

        protected void ddlNumeroDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNumeroDocumento.SelectedValue.Trim().Length > 0)
                cargarEmpleados();
            else
                limpiarEmpleados();
        }

        private void limpiarEmpleados()
        {
            ddlEmpleado.DataSource = null;
            ddlEmpleado.SelectedValue = null;
            ddlEmpleado.DataBind();
            ddlEmpleado.Enabled = false;
            limpiarContrato();
        }

        private void cargarEmpleados()
        {
            try
            {
                this.ddlEmpleado.DataSource = modificacionNomina.CargarEmpleado(
                    Convert.ToInt32(ddlAño.SelectedValue),
                    Convert.ToInt32(ddlPeriodo.SelectedValue),
                    ddlTipoDocumento.SelectedValue,
                    Convert.ToInt32(Session["empresa"]),
                    ddlNumeroDocumento.SelectedValue);
                this.ddlEmpleado.DataValueField = "codTercero";
                this.ddlEmpleado.DataTextField = "descripcion";
                this.ddlEmpleado.DataBind();
                if (ddlEmpleado.Items.Count == 0)
                {
                    limpiarEmpleados();
                    return;
                }

                this.ddlEmpleado.Items.Insert(0, new ListItem("", ""));
                this.ddlEmpleado.Enabled = true;
                limpiarContrato();
                if (ddlEmpleado.Items.Count == 2)
                {
                    ddlEmpleado.SelectedIndex = 1;
                    cargarContrato();
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar año. Correspondiente a: " + ex.Message, "C");
            }
        }

        protected void ddlContratos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlContratos.SelectedValue.Trim().Length > 0)
                CargarDetalle();
            else
                limpiarDetalle();
        }

        private void limpiarDetalle()
        {
            gvDetalleLiquidacion.DataSource = null;
            gvDetalleLiquidacion.DataBind();
            gvDetalleLiquidacion.Visible = false;
            detailLoadedPanel.Visible = false;
            detalleLiqidacion.Visible = false;
        }

        private void CargarDetalle()
        {
            try
            {
                ListadoDetalleLiquidacion = new List<LiquidacionDetalle>();
                var result = modificacionNomina.CargarDetalleLiquidación(
                    Convert.ToInt32(ddlAño.SelectedValue),
                    Convert.ToInt32(ddlPeriodo.SelectedValue),
                    ddlTipoDocumento.SelectedValue,
                    Convert.ToInt32(Session["empresa"]),
                    ddlNumeroDocumento.SelectedValue,
                    Convert.ToInt32(ddlEmpleado.SelectedValue),
                    Convert.ToInt32(ddlContratos.SelectedValue));

                foreach (DataRow dr in result.Tables[0].AsEnumerable())
                {
                    var item = new LiquidacionDetalle();
                    item.CodConcepto = dr["codConcepto"].ToString();
                    item.DescripcionConcepto = dr["descripcionConcepto"].ToString();
                    item.Cantidad = dr["cantidad"].ToString();
                    item.ValorUnitario = dr["valorUnitario"].ToString();
                    item.ValorTotal = dr["valorTotal"].ToString();
                    item.Porcentaje = dr["porcentaje"].ToString();
                    item.RegistroDetalleNomina = dr["registroDetalleNomina"].ToString();
                    item.BaseSeguridadSocial = !(dr["baseSeguridadSocial"] is bool) ? false : (bool)dr["baseSeguridadSocial"];
                    item.ValidaPorcentaje = !(dr["validaPorcentaje"] is bool) ? false : (bool)dr["validaPorcentaje"];
                    item.Deduccion = !(dr["signo"] is int) ? false : ((int)dr["signo"] == 2);
                    item.HabilitaValorTotal = !(dr["habilitaValorTotal"] is bool) ? false : (bool)dr["habilitaValorTotal"];
                    item.ValorUnitario = Math.Round(Convert.ToDouble(item.ValorUnitario)).ToString();
                    item.ValorTotal = Math.Round(Convert.ToDouble(item.ValorTotal)).ToString();
                    item.Cantidad = Math.Round(Convert.ToDouble(item.Cantidad)).ToString();
                    item.saldo = dr["saldo"].ToString();
                    ListadoDetalleLiquidacion.Add(item);
                }
                var i = 1;
                ListadoDetalleLiquidacion = ListadoDetalleLiquidacion.OrderBy(_i => _i.Deduccion ? 1 : 0).ToList();
                ListadoDetalleLiquidacion.ForEach(_i => _i.RegistroDetalleNomina = (i++).ToString());

                this.gvDetalleLiquidacion.DataSource = ListadoDetalleLiquidacion;
                this.gvDetalleLiquidacion.DataBind();
                gvDetalleLiquidacion.Visible = true;
                detailLoadedPanel.Visible = true;
                detalleLiqidacion.Visible = true;
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar año. Correspondiente a: " + ex.ToString(), "C");
            }
        }

        protected void gvDetalleLiquidacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var dr = ListadoDetalleLiquidacion[e.RowIndex];
            ListadoDetalleLiquidacion.Remove(dr);
            var i = 1;
            ListadoDetalleLiquidacion = ListadoDetalleLiquidacion.OrderBy(_i => _i.Deduccion ? 1 : 0).ToList();
            ListadoDetalleLiquidacion.ForEach(_i => _i.RegistroDetalleNomina = (i++).ToString());

            gvDetalleLiquidacion.DataSource = ListadoDetalleLiquidacion;
            gvDetalleLiquidacion.DataBind();
        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlConcepto.SelectedValue.Trim()))
            {
                MostrarMensaje("Debe seleccionar un concepto");
                return;
            }
            try
            {
                var dr = modificacionNomina.CargarInformacionContepto(Convert.ToInt16(Session["empresa"]), ddlConcepto.SelectedValue.Trim()).Tables[0].Rows[0];

                var dt = ListadoDetalleLiquidacion;
                var item = new LiquidacionDetalle();
                item.RegistroDetalleNomina = (ListadoDetalleLiquidacion.Count + 1).ToString();
                item.CodConcepto = ddlConcepto.SelectedValue;
                item.DescripcionConcepto = ddlConcepto.SelectedItem.Text;
                item.Cantidad = "0";
                item.ValorUnitario = "0";
                item.ValorTotal = "0";
                item.BaseSeguridadSocial = !(dr["baseSeguridadSocial"] is bool) ? false : (bool)dr["baseSeguridadSocial"];
                item.ValidaPorcentaje = !(dr["calculaSobrePorcentaje"] is bool) ? false : (bool)dr["calculaSobrePorcentaje"];
                item.Deduccion = !(dr["signo"] is int) ? false : ((int)dr["signo"] == 2);
                item.HabilitaValorTotal = !(dr["habilitaValorTotal"] is bool) ? false : (bool)dr["habilitaValorTotal"];
                item.Porcentaje = dr["porcentaje"].ToString();
                item.saldo = "0";
                dt.Add(item);
                var i = 1;
                ListadoDetalleLiquidacion = ListadoDetalleLiquidacion.OrderBy(_i => _i.Deduccion ? 1 : 0).ToList();
                ListadoDetalleLiquidacion.ForEach(_i => _i.RegistroDetalleNomina = (i++).ToString());
                gvDetalleLiquidacion.DataSource = ListadoDetalleLiquidacion;
                gvDetalleLiquidacion.DataBind();

                ddlConcepto.SelectedValue = "";
                //hasMadeChangesPanel.Visible = true;
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la información de la categoría." + ex.ToString(), "C");
            }
        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CargarDetalle();
            ddlConcepto.SelectedValue = "";
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool valid = true;
                foreach (var detalle in ListadoDetalleLiquidacion)
                {
                    valid &= Convert.ToDouble(detalle.ValorTotal) > 0;
                }
                using (TransactionScope ts = new TransactionScope())
                {
                    modificacionNomina.ElimnarDetalleLiquidación(
                        Convert.ToInt32(ddlAño.SelectedValue),
                        Convert.ToInt32(ddlPeriodo.SelectedValue),
                        ddlTipoDocumento.SelectedValue,
                        Convert.ToInt32(Session["empresa"]),
                        ddlNumeroDocumento.SelectedValue,
                        Convert.ToInt32(ddlEmpleado.SelectedValue),
                        Convert.ToInt32(ddlContratos.SelectedValue));

                    modificacionNomina.GuardarDetalleLiqidación(
                        Convert.ToInt32(ddlAño.SelectedValue),
                        Convert.ToInt32(ddlPeriodo.SelectedValue),
                        ddlTipoDocumento.SelectedValue,
                        Convert.ToInt32(Session["empresa"]),
                        ddlNumeroDocumento.SelectedValue,
                        Convert.ToInt32(ddlEmpleado.SelectedValue),
                        Convert.ToInt32(ddlContratos.SelectedValue),
                        ListadoDetalleLiquidacion);
                    ts.Complete();
                }
                ManejoError(this, GetType(), "Cambios realizados exitosamente", "info");
            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar los datos. Correspondiente a: " + ex.ToString(), "C");
            }
        }
    }
}