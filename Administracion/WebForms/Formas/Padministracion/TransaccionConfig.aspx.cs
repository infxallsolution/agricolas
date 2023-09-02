using Administracion.WebForms.App_Code.Administracion;
using Administracion.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracion.WebForms.Formas.Padministracion
{
    public partial class TransaccionConfig : BasePage
    {

        #region Instancias

        CtiposTransaccion tipoTransaccion = new CtiposTransaccion();

        #endregion Instancias

        #region Metodos

        private void ValidaLiquidacion()
        {
            if (chkTipoLiquidacionNomina.Checked)
                ddlTipoLiquidacionNomina.Enabled = true;
            else
                ddlTipoLiquidacionNomina.Enabled = false;
        }

        private void ValidaVigencia()
        {
            if (chkVigencia.Checked)
                txvVigencia.Enabled = true;
            else
            {
                txvVigencia.Enabled = false;
                txvVigencia.Text = "0";
            }
        }


        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(),
                                 consulta, Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", consulta);
                    return;
                }

                this.gvLista.DataSource = tipoTransaccion.BuscarEntidadConfig(this.nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), consulta, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                  "ex", this.gvLista.Rows.Count.ToString() + " Registros encontrados", HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }



        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");

            CcontrolesUsuario.InhabilitarControles(Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);
            nilbNuevo.Visible = true;
            seguridad.InsertaLog(Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + Page.ToString(),
                "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }

        private void CargarCombos()
        {
            try
            {
                this.ddlTipoTransaccion.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("gTipoTransaccion", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlTipoTransaccion.DataValueField = "codigo";
                this.ddlTipoTransaccion.DataTextField = "descripcion";
                this.ddlTipoTransaccion.DataBind();
                this.ddlTipoTransaccion.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        private void Guardar()
        {
            string operacion = "inserta",
                tipoNomino = null;

            try
            {
                if (Convert.ToBoolean(this.Session["editar"]) == true)
                    operacion = "actualiza";

                if (txvVigencia.Text.Trim().Length == 0)
                    txvVigencia.Text = "0";

                if (chkTipoLiquidacionNomina.Checked)
                    tipoNomino = ddlTipoLiquidacionNomina.SelectedValue;

                object[] objValores = new object[]{
                this.chkAjuste.Checked,                                         //@ajuste
                this.chkCantidadEditable.Checked,                               //@cantidadEditable
                this.chkCompraInv.Checked,                                      //@compraInv
                this.chkCompraServ.Checked,                                     //@CompraServ
                "",                                                             //@conceptoContable
                this.chkConsignacion.Checked,                                   //@consignacion
                this.chkDias.Checked,                                           //@diaSemana
                Convert.ToDecimal(txvVigencia.Text),                            //@diasVigencia
                this.chkDocContable.Checked,                                    //@docContable
                this.txtDsReferenciaDetalle.Text.Trim(),                        //@dsReferenciaDetalle
                Convert.ToInt16(Session["empresa"]),                            //@empresa
                this.chkEntradaDirecta.Checked,                                 //@entradaDirecta
                this.chkEstudioCompra.Checked,                                  //@estudioCompra
                this.chkFecha.Checked,                                          //@fechaActual
                this.txtFormatoImpresion.Text.Trim(),                           //@formatoImpresion
                this.chkLiberaReferencia.Checked,                               //@liberaReferencia
                this.chkManejaBascula.Checked,                                  //@manejaBascula
                this.chkManejaBodega.Checked,                                   //@manejaBodega
                this.chkDocumento.Checked,                                      //@manejaDocumento
                this.chkManejaTalonario.Checked,                                //@manejaTalonario
                this.chkManejaAprobacion.Checked,                               //@mAprobacion
                this.chkManejaTercero.Checked,                                  //@mTercero
                this.chkTipoLiquidacionNomina.Checked,                          //@mTipoLiquidacionNomina
                Convert.ToInt16(this.ddlNivelDestino.SelectedValue),            //@nivelDestino
                this.chkPdesEditable.Checked,                                   //@pDesEditable
                this.chkPivaEditable.Checked,                                   //@pIvaEditable
                this.chkReferenciaTercero.Checked,                              //@referenciaTercero
                this.chkRegistroDirecto.Checked,                                //@registroDirecto
                this.chkRegistroProveedor.Checked,                              //@registroProveedor
                this.chkSalida.Checked,                                         //@salida
                Convert.ToString(this.ddlTipoLiquidacionNomina.SelectedValue),  //@tipoLiquidacionNomina
                Convert.ToString(this.ddlTipoTransaccion.SelectedValue),        //@tipoTransaccion
                this.chkUmedidaEditable.Checked,                                //@UmedidaEditable
                this.chkValidaSaldo.Checked,                                    //@validaSaldo
                this.chkVentaInventario.Checked,                                //@ventaInv
                this.chkVentaServicios.Checked,                                 //@ventaServ
                this.chkVigencia.Checked,                                       //@vigencia
                this.chkVunitarioEditable.Checked                               //@vUnitarioEditable
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("gTipoTransaccionConfig", operacion, "ppa", objValores))
                {
                    case 0:
                        ManejoExito("Datos insertados satisfactoriamente", operacion.Substring(0, 1).ToUpper());
                        break;
                    case 1:
                        ManejoError("Errores al insertar el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                        break;
                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
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
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                        nombrePaginaActual(), Convert.ToInt16(this.Session["empresa"])) != 0)
                    this.nitxtBusqueda.Focus();
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }

        protected void nilbNuevo_Click(object sender, EventArgs e)
        {

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(),
                                    insertar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", insertar);
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            CargarCombos();
            GetEntidad();
            this.ddlTipoTransaccion.Enabled = true;
            this.ddlTipoTransaccion.Focus();
            this.nilblInformacion.Text = "";
            ValidaLiquidacion();
            ValidaVigencia();
            txvVigencia.Text = "0";
        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();

            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (ddlTipoTransaccion.SelectedValue.Length == 0 || ddlNivelDestino.SelectedValue.Length == 0)
            {
                ManejoError("Campos vacios por favor corrija", "C");
                return;
            }

            Guardar();
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(),
                                eliminar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", eliminar);
                return;
            }

            string operacion = "elimina";

            try
            {
                object[] objValores = new object[] { Convert.ToInt16(Session["empresa"]), Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text) };

                if (CentidadMetodos.EntidadInsertUpdateDelete("gTipoTransaccionConfig", operacion, "ppa", objValores) == 0)
                    ManejoExito("Datos eliminados satisfactoriamente", "E");
                else
                    ManejoError("Errores al eliminar el registro. Operación no realizada", "E");
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(),
                                editar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", editar);
                return;
            }
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            this.ddlTipoTransaccion.Enabled = false;
            this.ddlNivelDestino.Focus();

            try
            {
                CargarCombos();

                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.ddlTipoTransaccion.SelectedValue = this.gvLista.SelectedRow.Cells[2].Text;

                int i = 4;
                if (this.gvLista.SelectedRow.Cells[i].Text != "&nbsp;")
                    this.ddlNivelDestino.SelectedValue = this.gvLista.SelectedRow.Cells[i].Text;
                i++;
                if (this.gvLista.SelectedRow.Cells[i].Text != "&nbsp;")
                    this.txtFormatoImpresion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[i].Text);
                i++;
                if (this.gvLista.SelectedRow.Cells[i].Text != "&nbsp;")
                    this.txtDsReferenciaDetalle.Text = this.gvLista.SelectedRow.Cells[i].Text;
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkAjuste.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkSalida.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkValidaSaldo.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkReferenciaTercero.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkCantidadEditable.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkVunitarioEditable.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkPivaEditable.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkManejaTalonario.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkManejaBodega.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkLiberaReferencia.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkEntradaDirecta.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkRegistroDirecto.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkConsignacion.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkDias.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkDocumento.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkVigencia.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkPdesEditable.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkUmedidaEditable.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkEstudioCompra.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkRegistroProveedor.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkFecha.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkManejaBascula.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkTipoLiquidacionNomina.Checked = ((CheckBox)objControl).Checked;
                }

                ValidaLiquidacion();
                i++;
                if (this.gvLista.SelectedRow.Cells[i].Text != "&nbsp;" && chkTipoLiquidacionNomina.Checked == true)
                    ddlTipoLiquidacionNomina.SelectedValue = this.gvLista.SelectedRow.Cells[i].Text;
                ValidaVigencia();
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkManejaAprobacion.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                if (this.gvLista.SelectedRow.Cells[i].Text != "&nbsp;")
                    txvVigencia.Text = this.gvLista.SelectedRow.Cells[i].Text;
                else
                    txvVigencia.Text = "0";

                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkManejaTercero.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkCompraInv.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkVentaInventario.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkCompraServ.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkVentaServicios.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkEntradaPlanta.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkSalidaPlanta.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkmHorarioProduccion.Checked = ((CheckBox)objControl).Checked;
                }
                i++;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkDocContable.Checked = ((CheckBox)objControl).Checked;
                }


            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }

        protected void chkLiquidacion_CheckedChanged(object sender, EventArgs e)
        {
            ValidaLiquidacion();
        }
        protected void chkVigencia_CheckedChanged(object sender, EventArgs e)
        {
            ValidaVigencia();
        }

        #endregion Eventos


    }
}