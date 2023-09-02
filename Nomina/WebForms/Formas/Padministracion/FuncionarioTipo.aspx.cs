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
    public partial class FuncionarioTipo : BasePage
    {
        #region Instancias

        CtipoFuncionario tipoFuncionario = new CtipoFuncionario();

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

                this.gvLista.DataSource = tipoFuncionario.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(
                    this.Session["usuario"].ToString(), "C",
                  ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                    this.gvLista.Rows.Count.ToString() + " Registros encontrados", HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
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
                mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));

            GetEntidad();
        }
        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text, Convert.ToInt16(Session["empresa"]) };
            try
            {
                if (CentidadMetodos.EntidadGetKey("nFuncionariotipo", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    MostrarMensaje("Código " + this.txtCodigo.Text + " ya se encuentra registrado");
                    CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
                    this.nilbNuevo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }
        private void Guardar()
        {
            string operacion = "inserta";

            try
            {
                if (Convert.ToBoolean(this.Session["editar"]) == true)
                    operacion = "actualiza";


                object[] objValores = new object[]{
                    chkActivo.Checked,
                    this.txtCodigo.Text,
                    chkContratista.Checked,
                    this.txtDescripcion.Text,
                    Convert.ToInt16(Session["empresa"]),
                    chkFuncionario.Checked,
                    chkOtros.Checked,
                    chkSocio.Checked,
                    chkVisitante.Checked
                };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("nFuncionarioTipo", operacion, "ppa", objValores))
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
        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
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

                object[] objValores = new object[] {                Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)),
                Convert.ToInt16(Session["empresa"])
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("nFuncionarioTipo", "elimina", "ppa", objValores))
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
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                 nombrePaginaActual(), "A", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "A");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            this.txtCodigo.Enabled = false;
            this.txtDescripcion.Focus();

            try
            {
                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.txtCodigo.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[2].Text);
                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    this.txtDescripcion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[3].Text);

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[4].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkActivo.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[5].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkFuncionario.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[6].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkContratista.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[7].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkSocio.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[8].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkVisitante.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[9].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkOtros.Checked = ((CheckBox)objControl).Checked;
                }

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
        protected void nilbNuevo_Click(object sender, EventArgs e)
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
            txtCodigo.Focus();
            this.nilblInformacion.Text = "";
        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            Guardar();
        }
        protected void chkFuncionario_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFuncionario.Checked)
            {
                chkContratista.Checked = false;
                chkOtros.Checked = false;
                chkSocio.Checked = false;
                chkVisitante.Checked = false;
            }
        }
        protected void chkContratista_CheckedChanged(object sender, EventArgs e)
        {
            if (chkContratista.Checked)
            {
                chkFuncionario.Checked = false;
                chkOtros.Checked = false;
                chkSocio.Checked = false;
                chkVisitante.Checked = false;
            }
        }
        protected void chkSocio_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSocio.Checked)
            {
                chkContratista.Checked = false;
                chkOtros.Checked = false;
                chkFuncionario.Checked = false;
                chkVisitante.Checked = false;
            }
        }
        protected void chkVisitante_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVisitante.Checked)
            {
                chkContratista.Checked = false;
                chkOtros.Checked = false;
                chkSocio.Checked = false;
                chkFuncionario.Checked = false;
            }
        }
        protected void chkOtros_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOtros.Checked)
            {
                chkContratista.Checked = false;
                chkFuncionario.Checked = false;
                chkSocio.Checked = false;
                chkVisitante.Checked = false;
            }
        }
        #endregion Eventos
    }
}