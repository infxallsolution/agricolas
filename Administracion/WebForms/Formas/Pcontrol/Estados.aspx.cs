using Administracion.WebForms.App_Code.General;
using Administracion.WebForms.App_Code.Seguridad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracion.WebForms.Formas.Pcontrol
{
    public partial class Estados : BasePage
    {

        #region Instancias

        Cestados estados = new Cestados();

        #endregion Instancias

        #region Metodos

        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(
                                this.Session["usuario"].ToString(),//usuario
                                 ConfigurationManager.AppSettings["modulo"].ToString(),//sitio
                                 nombrePaginaActual(),//pagina
                                consulta,//operacion
                               Convert.ToInt16(this.Session["empresa"]))//empresa
                               == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", consulta);
                    return;
                }

                this.gvLista.DataSource = estados.BuscarEntidad(
                    this.nitxtBusqueda.Text);

                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(
                  this.Session["usuario"].ToString(),
                  consulta,
                   ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                  "ex",
                  this.gvLista.Rows.Count.ToString() + " Registros encontrados",
                  ObtenerIP(), Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente a: " + ex.Message, "C");
            }
        }



        private void ManejoExito(string mensaje, string operacion)
        {
            this.nilblMensaje.Text = mensaje;

            CcontrolesUsuario.InhabilitarControles(
                this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(
                this.Page.Controls);

            this.nilbNuevo.Visible = true;

            seguridad.InsertaLog(
                    this.Session["usuario"].ToString(),
                    operacion,
                     ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                    "ex",
                    mensaje,
                    ObtenerIP(), Convert.ToInt16(Session["empresa"]));

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
                if (CentidadMetodos.EntidadGetKey(
                    "sEstados",
                    "ppa",
                    objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    this.nilblInformacion.Text = "Operación " + this.txtCodigo.Text + " ya se encuentra registrada";

                    CcontrolesUsuario.InhabilitarControles(
                        this.Page.Controls);

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

            try
            {
                if (Convert.ToBoolean(this.Session["editar"]) == true)
                {
                    operacion = "actualiza";
                }

                object[] objValores = new object[]{
                this.txtDescripcion.Text,
                this.txtCodigo.Text
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete(
                    "sEstados",
                    operacion,
                    "ppa",
                    objValores))
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
                if (seguridad.VerificaAccesoPagina(
                        this.Session["usuario"].ToString(),
                        ConfigurationManager.AppSettings["Modulo"].ToString(),
                        nombrePaginaActual(),
                        Convert.ToInt16(this.Session["empresa"])) != 0)
                {
                    this.nitxtBusqueda.Focus();

                    if (this.txtCodigo.Text.Length > 0)
                    {
                        this.txtDescripcion.Focus();
                    }
                }
                else
                {
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
                }
            }
        }

        protected void lbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(
                                this.Session["usuario"].ToString(),//usuario
                                 ConfigurationManager.AppSettings["modulo"].ToString(),//sitio
                                 nombrePaginaActual(),//pagina
                                insertar,//operacion
                               Convert.ToInt16(this.Session["empresa"]))//empresa
                               == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", insertar);
                return;
            }


            CcontrolesUsuario.HabilitarControles(
                this.Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;

            CargarCombos();

            this.txtCodigo.Enabled = true;
            this.txtCodigo.Focus();
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

        protected void niimbBuscar_Click(object sender, ImageClickEventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(
                this.Page.Controls);

            this.nilbNuevo.Visible = true;

            GetEntidad();
        }



        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(
                                 this.Session["usuario"].ToString(),//usuario
                                  ConfigurationManager.AppSettings["modulo"].ToString(),//sitio
                                  nombrePaginaActual(),//pagina
                                 editar,//operacion
                                Convert.ToInt16(this.Session["empresa"]))//empresa
                                == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", editar);
                return;
            }


            CcontrolesUsuario.HabilitarControles(
                this.Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;

            this.nilblMensaje.Text = "";
            this.txtCodigo.Enabled = false;
            this.txtDescripcion.Focus();

            try
            {
                CargarCombos();

                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                {
                    this.txtCodigo.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[2].Text);
                }
                else
                {
                    this.txtCodigo.Text = "";
                }

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                {
                    this.txtDescripcion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[3].Text);
                }
                else
                {
                    this.txtDescripcion.Text = "";
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string operacion = "elimina";

            if (seguridad.VerificaAccesoOperacion(
                                this.Session["usuario"].ToString(),//usuario
                                 ConfigurationManager.AppSettings["modulo"].ToString(),//sitio
                                 nombrePaginaActual(),//pagina
                                eliminar,//operacion
                               Convert.ToInt16(this.Session["empresa"]))//empresa
                               == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", eliminar);
                return;
            }

            try
            {
                object[] objValores = new object[] {
                Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text)
            };

                if (CentidadMetodos.EntidadInsertUpdateDelete(
                    "sEstados",
                    operacion,
                    "ppa",
                    objValores) == 0)
                {
                    ManejoExito("Datos eliminados satisfactoriamente", "E");
                }
                else
                {
                    ManejoError("Errores al eliminar el registro. Operación no realizada", "E");
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al eliminar los datos correspondiente a: " + ex.Message, "E");
            }
        }

        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
        }

        protected void lbRegistrar_Click1(object sender, ImageClickEventArgs e)
        {
            if (txtCodigo.Text.Length == 0 || txtDescripcion.Text.Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Campos vacios por favor corrija", "warning");
                return;
            }

            Guardar();
        }
        #endregion Eventos


        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
    }
}