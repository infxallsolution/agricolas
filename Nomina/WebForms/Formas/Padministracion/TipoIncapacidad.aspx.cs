using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Padministracion
{
    public partial class TipoIncapacidad : BasePage
    {
        #region Instancias



        CtipoIncapacidad tipoIncapacidad = new CtipoIncapacidad();



        #endregion Instancias

        #region Metodos


        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                                        ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "C", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                this.gvLista.DataSource = tipoIncapacidad.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(
                    this.Session["usuario"].ToString(), "C",
                  ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                    this.gvLista.Rows.Count.ToString() + " Registros encontrados", ObtenerIP(), Convert.ToInt16(Session["empresa"]));

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
            this.nilbNuevo.Visible = true;

            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion,
                ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));

            GetEntidad();
        }
        private void CargarCombos()
        {

        }
        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text, Convert.ToInt16(Session["empresa"]) };
            try
            {
                if (CentidadMetodos.EntidadGetKey("nTipoIncapacidad", "ppa", objKey).Tables[0].Rows.Count > 0)
                {

                    MostrarMensaje("Código " + this.txtCodigo.Text + " ya se encuentra registrado");

                    CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
                    this.nilbNuevo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al validar la llave primaria correspondiente a: " + ex.Message, "C");
            }
        }
        private void Guardar()
        {
            string operacion = "inserta";
            decimal porcentajeNuevo = 0;
            try
            {
                if (Convert.ToBoolean(this.Session["editar"]) == true)
                    operacion = "actualiza";

                if (chkAdicionar.Checked)
                    porcentajeNuevo = Convert.ToDecimal(txvPorcentajeNuevo.Text);
                else
                    porcentajeNuevo = Convert.ToDecimal(txvPorcentaje.Text);

                object[] objValores = new object[]{
                    chkActivo.Checked,
                    chkAdicionar.Checked,
                    chkAfectaSeguridadSocialARL.Checked,
                    ddlAfectaNovedad.SelectedValue,
                    chkAfectaSeguridadSocial.Checked,
                    chkCalculaIBC.Checked,
                    txtCodigo.Text,
                    this.txtDescripcion.Text,
                    Convert.ToDecimal(txvDiasDespues.Text),
                   Convert.ToInt16(Session["empresa"]),
                   Convert.ToDecimal(txvPorcentaje.Text),
                   porcentajeNuevo
                };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("nTipoIncapacidad", operacion, "ppa", objValores))
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
            {
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            }
            else
            {
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(),
                    ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) != 0)
                {

                    this.txtCodigo.Focus();

                }
                else
                {
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
                }
            }
        }
        protected void lbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                                 ConfigurationManager.AppSettings["Modulo"].ToString(),
                                  nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            lblDespues.Visible = false;
            lblPorcentajeNuevo.Visible = false;
            txvPorcentajeNuevo.Visible = false;
            txvDiasDespues.Visible = false;

            txtCodigo.Focus();
            this.nilblInformacion.Text = "";
        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(
                this.Page.Controls);

            CcontrolesUsuario.LimpiarControles(
                this.Page.Controls);

            this.gvLista.DataSource = null;
            this.gvLista.DataBind();

            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
        }
        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
            txtDescripcion.Focus();
        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                                        ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                object[] objValores = new object[] {
                Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)),
                Convert.ToInt16(Session["empresa"])
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete(
                    "nTipoIncapacidad",
                    "elimina",
                    "ppa",
                    objValores))
                {
                    case 0:

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
                {
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
                }
            }
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (this.txtDescripcion.Text.Length == 0 || this.txtCodigo.Text.Length == 0)
            {
                MostrarMensaje("Campos vacios por favor corrija");
                return;
            }
            if (Convert.ToDecimal(txvPorcentaje.Text) > 100)
            {
                this.nilblInformacion.Text = "Valores porcentaje no puede ser mayor a (100)";
                return;
            }

            if (chkAdicionar.Checked)
            {
                if (Convert.ToDecimal(txvPorcentajeNuevo.Text) == 0 || Convert.ToDecimal(txvDiasDespues.Text) == 0)
                {
                    this.nilblInformacion.Text = "Valores debe ser mayor a cero(0)";
                    return;
                }

                if (Convert.ToDecimal(txvPorcentajeNuevo.Text) > 100)
                {
                    this.nilblInformacion.Text = "Valores porcentaje no puede ser mayor a (100)";
                    return;
                }
            }

            Guardar();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;

            this.txtCodigo.Enabled = false;
            this.txtDescripcion.Focus();

            try
            {
                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.txtCodigo.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[2].Text);
                else
                    this.txtCodigo.Text = "";

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    this.txtDescripcion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[3].Text);
                else
                    this.txtDescripcion.Text = "";


                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                    this.txvPorcentaje.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[4].Text);
                else
                    this.txvPorcentaje.Text = "0";

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[5].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkActivo.Checked = ((CheckBox)objControl).Checked;
                }

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[6].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkAfectaSeguridadSocial.Checked = ((CheckBox)objControl).Checked;
                }

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[7].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkAdicionar.Checked = ((CheckBox)objControl).Checked;
                }

                validarAdicionar();

                if (this.gvLista.SelectedRow.Cells[8].Text != "&nbsp;")
                    this.txvDiasDespues.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[8].Text);
                else
                    this.txvDiasDespues.Text = "0";

                if (this.gvLista.SelectedRow.Cells[9].Text != "&nbsp;")
                    this.txvPorcentajeNuevo.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[9].Text);

                else
                    this.txvPorcentajeNuevo.Text = "0";

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[10].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkAfectaSeguridadSocialARL.Checked = ((CheckBox)objControl).Checked;
                }

                if (this.gvLista.SelectedRow.Cells[11].Text != "&nbsp;")
                    this.ddlAfectaNovedad.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[11].Text);

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[12].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkCalculaIBC.Checked = ((CheckBox)objControl).Checked;
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

        protected void chkAdicionar_CheckedChanged(object sender, EventArgs e)
        {
            validarAdicionar();
        }

        private void validarAdicionar()
        {
            if (chkAdicionar.Checked)
            {
                lblDespues.Visible = true;
                lblPorcentajeNuevo.Visible = true;
                txvPorcentajeNuevo.Visible = true;
                txvDiasDespues.Visible = true;
            }
            else
            {
                lblDespues.Visible = false;
                lblPorcentajeNuevo.Visible = false;
                txvPorcentajeNuevo.Visible = false;
                txvDiasDespues.Visible = false;
            }
        }

        #endregion Eventos

    }
}