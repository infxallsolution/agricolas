using Contabilidad.WebForms.App_Code.CuentasxCobrar;
using Contabilidad.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.Ppresupuesto
{
    public partial class Items : BasePage
    {
        #region Instancias

        Citems items = new Citems();

        #endregion Instancias

        #region Metodos

        private void Consecutivo()
        {
            try
            {
                this.txtCodigo.Text = items.Consecutivo(Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al obtener el consecutivo. Correspondiente a: " + ex.Message, "C");
            }
        }
        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "C", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }
                upCabeza.Visible = false;
                DataView dvitems = items.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataSource = dvitems;
                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                    this.gvLista.Rows.Count.ToString() + " Registros encontrados", HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente a: " + ex.Message, "C");
            }
        }
        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upCabeza.Controls);
            CcontrolesUsuario.LimpiarControles(this.upCabeza.Controls);

            this.nilbNuevo.Visible = true;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }

        private void CargarCombos()
        {

            try
            {

                this.ddlUmedidaCompra.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("gUnidadMedida", "ppa"),
                    "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlUmedidaCompra.DataValueField = "codigo";
                this.ddlUmedidaCompra.DataTextField = "descripcion";
                this.ddlUmedidaCompra.DataBind();
                this.ddlUmedidaCompra.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar unidad de medida compra. Correspondiente a: " + ex.Message, "C");
            }


        }


        private void Guardar()
        {
            string operacion = "inserta";
            int codigo;

            try
            {

                if (Convert.ToBoolean(this.Session["editar"]) == true)
                {
                    operacion = "actualiza";
                    codigo = Convert.ToInt16(txtCodigo.Text);

                }
                else
                {
                    Consecutivo();
                    codigo = Convert.ToInt16(txtCodigo.Text);
                }

                object[] objValores = new object[]{
                    chkActivo.Checked, //@activo
                    codigo,//@codigo
                    this.txtDescripcion.Text,//@descripcion
                    txtDesCorta.Text,//@descripcionAbreviada
                    Convert.ToInt16(Session["empresa"]),//@empresa
                    txtEquivalencia.Text,//@equivalencia
                    DateTime.Now,//@fechaActualiza
                    DateTime.Now,
                    chkManejaAnalisis.Checked,
                    chkCalculo.Checked,
                    txtNotas.Text,//@notas
                    Convert.ToDecimal(txvOrden.Text),//@orden
                    txtReferencia.Text,//referencia,
                    chkSello.Checked , //sello
                    ddlTipoItem.SelectedValue,//@tipo
                    ddlUmedidaCompra.SelectedValue,//@uMedidaConsumo
                    Convert.ToString(Session["usuario"]),
                    Convert.ToString(Session["usuario"])
                };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("iItems", operacion, "ppa", objValores))
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
                ManejoError("Error al guardar los datos correspondiente a: " + ex.Message, operacion.Substring(0, 1).ToUpper());
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
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) != 0)
                    this.nitxtBusqueda.Focus();
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                object[] objValoresCriterio = new object[] { Convert.ToInt16(Session["empresa"]), Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)) };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("iItemsCriterios", "elimina", "ppa", objValoresCriterio))
                {
                    case 0:
                        object[] objValores = new object[] { Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)), Convert.ToInt16(Session["empresa"]) };

                        switch (CentidadMetodos.EntidadInsertUpdateDelete("iItems", "elimina", "ppa", objValores))
                        {
                            case 1:
                                ManejoError("Error al eliminar el registro. Operación no realizada", "E");
                                break;
                        }
                        ManejoExito("Registro eliminado satisfactoriamente", "E");
                        break;
                    case 1:
                        ManejoError("Error al eliminar el registro. Operación no realizada", "E");
                        break;
                }
            }
            catch (Exception ex)
            {
                if (ex.HResult.ToString() == "-2146233087")
                {
                    ManejoError("El código ('" + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)) +
                    "' - ' " + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[3].Text)) + "') tiene una asociación, no es posible eliminar el registro.", "E");
                }
                else
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
            }
        }
        protected void gvLista_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(), "A", Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "A");
                return;
            }

            upCabeza.Visible = true;
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.HabilitarControles(this.upCabeza.Controls);
            CargarCombos();
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            this.txtReferencia.Focus();

            try
            {
                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                {
                    this.txtCodigo.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[2].Text);
                    this.txtCodigo.Enabled = false;
                }
                else
                    this.txtCodigo.Text = "";

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    txtDescripcion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[3].Text.Trim());
                else
                    txtDescripcion.Text = "";

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                    txtDesCorta.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[4].Text);
                else
                    txtDesCorta.Text = "";

                if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                    txtReferencia.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[5].Text);
                else
                    txtReferencia.Text = "";

                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                    ddlTipoItem.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[6].Text.Trim());

                if (this.gvLista.SelectedRow.Cells[7].Text != "&nbsp;")
                    ddlUmedidaCompra.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[7].Text);

                if (this.gvLista.SelectedRow.Cells[8].Text != "&nbsp;")
                    txtNotas.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[8].Text);
                else
                    txtNotas.Text = "";

                if (this.gvLista.SelectedRow.Cells[9].Text != "&nbsp;")
                    txvOrden.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[9].Text);
                else
                    txvOrden.Text = "0";

                if (this.gvLista.SelectedRow.Cells[10].Text != "&nbsp;")
                    txtEquivalencia.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[10].Text);
                else
                    txtEquivalencia.Text = "";

                foreach (Control c in this.gvLista.SelectedRow.Cells[11].Controls)
                {
                    if (c is CheckBox)
                        chkActivo.Checked = ((CheckBox)c).Checked;
                }

                foreach (Control c in this.gvLista.SelectedRow.Cells[12].Controls)
                {
                    if (c is CheckBox)
                        chkSello.Checked = ((CheckBox)c).Checked;
                }
                foreach (Control c in this.gvLista.SelectedRow.Cells[13].Controls)
                {
                    if (c is CheckBox)
                        chkCalculo.Checked = ((CheckBox)c).Checked;
                }
                foreach (Control c in this.gvLista.SelectedRow.Cells[14].Controls)
                {
                    if (c is CheckBox)
                        chkManejaAnalisis.Checked = ((CheckBox)c).Checked;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }

        }
        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;

            GetEntidad();
        }
        protected void nilbNuevo_Click(object sender, EventArgs e)
        {

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            upCabeza.Visible = true;
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);
            CcontrolesUsuario.HabilitarControles(upCabeza.Controls);
            CcontrolesUsuario.LimpiarControles(upCabeza.Controls);
            CargarCombos();
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            Consecutivo();
            txtCodigo.Enabled = false;
            txtReferencia.Focus();
        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            CcontrolesUsuario.HabilitarControles(upCabeza.Controls);
            CcontrolesUsuario.LimpiarControles(upCabeza.Controls);
            upCabeza.Visible = false;

            this.gvLista.DataSource = null;
            this.gvLista.DataBind();

            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (txtDesCorta.Text.Length == 0 || txtDescripcion.Text.Length == 0 || ddlUmedidaCompra.SelectedValue.Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Campos vacios por favor corrija", "warning");
                return;
            }
            Guardar();
        }

        #endregion Eventos

    }
}