using Administracion.WebForms.App_Code.General;
using Administracion.WebForms.App_Code.Seguridad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracion.WebForms.Formas.Pcontrol
{
    public partial class Usuarios : BasePage
    {


        #region Instancias

        Cusuarios usuario = new Cusuarios();

        #endregion Instancias

        #region Metodos


        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                    nombrePaginaActual(), consulta, Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", consulta);
                    return;
                }


                this.gvLista.DataSource = usuario.BuscarEntidad(this.nitxtBusqueda.Text);
                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), consulta, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                  "ex", this.gvLista.Rows.Count.ToString() + " Registros encontrados", HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
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


        }

        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text };

            try
            {
                if (CentidadMetodos.EntidadGetKey("sUsuarios", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Usuario " + this.txtCodigo.Text + " ya se encuentra registrado", "warning");
                    CcontrolesUsuario.InhabilitarControles(this.Page.Controls);

                    this.nilbNuevo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }

        private void Guardar()
        {
            try
            {
                if (Convert.ToBoolean(this.Session["editar"]) == true)
                {

                    switch (usuario.ModificaUsuario(this.txtCodigo.Text, this.txtDescripcion.Text, this.chkActivo.Checked, txtCorreo.Text))
                    {
                        case 0:

                            ManejoExito("Usuario modificado satisfactoriamente", "A");
                            break;

                        case 1:

                            ManejoError("Error al modificar el registro. Operación no realizada", "A");
                            break;
                    }
                }
                else
                {
                    switch (usuario.InsertaUsuario(this.txtCodigo.Text, this.txtDescripcion.Text, this.txtContrasena.Text, this.chkActivo.Checked, DateTime.Now, txtCorreo.Text))
                    {
                        case 0:

                            ManejoExito("Datos insertados satisfactoriamente", "I");
                            break;

                        case 1:

                            ManejoError("Usuario existente. Operación no realizada", "I");
                            break;

                        case 2:

                            ManejoError("La longitud de la contraseña debe ser mayor o iual a 4 caracteres. Operación no realizada", "I");
                            break;

                        case 3:

                            ManejoError("Error al insertar el registro. Operación no realizada", "I");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }

        #endregion Metodos

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                        nombrePaginaActual(), Convert.ToInt16(this.Session["empresa"])) != 0)
            {
                this.nitxtBusqueda.Focus();

                if (this.txtCodigo.Text.Length > 0)
                    this.txtDescripcion.Focus();
            }
            else
                ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                                 ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(), editar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", editar);
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            this.txtCodigo.Enabled = false;
            this.txtContrasena.Enabled = false;
            this.txtDescripcion.Focus();

            try
            {
                CargarCombos();

                if (this.gvLista.SelectedRow.Cells[1].Text != "&nbsp;")
                    this.txtCodigo.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[1].Text);
                else
                    this.txtCodigo.Text = "";

                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.txtDescripcion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[2].Text);
                else
                    this.txtDescripcion.Text = "";


                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    this.txtCorreo.Text = this.gvLista.SelectedRow.Cells[3].Text;
                else
                    this.txtCorreo.Text = "";

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[4].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkActivo.Checked = ((CheckBox)objControl).Checked;
                }

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }

        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
        }

        protected void lbCambiarContrasena_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                             nombrePaginaActual(), editar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", editar);
                return;
            }

            this.txtContrasenaAnterior.Visible = true;
            this.txtContrasenaNueva.Visible = true;
            this.lblContrasenaAnterior.Visible = true;
            this.txtContrasenaAnterior.Focus();
            this.lbCambiar.Visible = true;
            lbRestablecer.Visible = false;
        }
        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }
        protected void nilbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                             nombrePaginaActual(), insertar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", insertar);
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            CargarCombos();
            this.txtCodigo.Enabled = true;
            this.txtContrasena.Enabled = true;
            this.txtCodigo.Focus();
            this.nilblInformacion.Text = "";
            this.lbCambiarContrasena.Visible = false;
            this.txtContrasenaAnterior.Visible = false;
            this.txtContrasenaNueva.Visible = false;
            this.lblContrasenaAnterior.Visible = false;
            this.lbCambiar.Visible = false;
            this.lbRestablecer.Visible = false;
            this.lbRestablecerContrasena.Visible = true;
            this.lbCambiarContrasena.Visible = true;
            this.lbCambiarContrasena.Visible = false;
            this.lbRestablecerContrasena.Visible = false;
        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Length == 0 || txtDescripcion.Text.Length == 0 || txtCorreo.Text.Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Campos vacios por favor corrija", "warning");
                return;
            }

            Guardar();
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


        [WebMethod]
        public static string cambiarPass(string usuario, string passAnt, string passNue)
        {

            Cusuarios cusuarios = new Cusuarios();
            string retorno = (cusuarios.ActualizaIdSysUsuario(
                 usuario,
                 passAnt,
                 passNue)).ToString();

            return retorno;

        }

        [WebMethod]
        public static string restablecerpass(string user, string passnue)
        {
            Cusuarios usuario = new Cusuarios();
            string retorno = (usuario.ReestableceUsuario(user, passnue)).ToString();
            return retorno;
        }
        protected void lbRestablecerContrasena_Click(object sender, EventArgs e)
        {
            txtContrasenaNueva.Visible = true;
            lblContrasenaAnterior.Visible = false;
            lblContrasenaAnterior.Visible = false;
            txtContrasenaAnterior.Visible = false;
            lbRestablecerContrasena.Visible = true;
            lbRestablecer.Visible = true;
            lbCambiar.Visible = false;
        }
        protected void lbCancelarCambio_Click(object sender, EventArgs e)
        {
            txtContrasenaNueva.Visible = false;
            lblContrasenaAnterior.Visible = false;
            lbRestablecerContrasena.Visible = true;
            txtContrasenaAnterior.Visible = false;
            lbCambiarContrasena.Visible = true;
            lbRestablecer.Visible = false;
            lbCambiar.Visible = false;
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }



        #endregion Eventos

    }

}