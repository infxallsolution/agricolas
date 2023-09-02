using Contabilidad.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.Padministracion
{
    public partial class Auxiliares : BasePage
    {
        #region Instancias

        Centidades entidades = new Centidades();


        #endregion Instancias

        #region Metodos

        private void GetEntidad()
        {
            try
            {
                //if (CvalidacionOperacion.VerificaOperacion(
                //    this.Session["usuario"].ToString(),
                //    "Auxiliares.aspx",
                //    "C") == false)
                //{
                //    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                //    return;
                //}

                if (Convert.ToString(this.niddlEntidad.SelectedValue).Length == 0)
                {
                    this.nilblMensaje.Text = "Debe seleccionar una entidad para realizar la busqueda";
                    this.gvLista.DataSource = null;
                    this.gvLista.DataBind();

                    this.nilblInformacion.Visible = true;
                    this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";
                }
                else
                {
                    //this.gvLista.DataSource = entidades.BuscarEntidad(
                    //    this.nitxtBusqueda.Text,
                    //    Convert.ToString(this.niddlEntidad.SelectedValue)
                    //);

                    this.gvLista.DataBind();

                    this.nilblInformacion.Visible = true;
                    this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente a: " + ex.Message, "C");
            }
        }

        private void ManejoError(string error, string operacion)
        {
            this.Session["error"] = error;
            this.Session["paginaAnterior"] = this.Page.Request.FilePath.ToString();

            //Cseguridad.InsertaLog(
            //    this.Session["usuario"].ToString(),
            //    operacion,
            //    System.Configuration.ConfigurationSettings.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
            //    "er",
            //    error);

            this.Response.Redirect("~/Seguridad/Error.aspx", false);
        }

        private void ManejoExito(string mensaje, string operacion)
        {
            this.nilblMensaje.Text = mensaje;

            CcontrolesUsuario.InhabilitarControles(
                this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(
                this.Page.Controls);

            this.nilbNuevo.Visible = true;

            //Cseguridad.InsertaLog(
            //    this.Session["usuario"].ToString(),
            //    operacion,
            //    System.Configuration.ConfigurationSettings.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
            //    "ex",
            //    mensaje);

            GetEntidad();
        }

        private void CargarCombos()
        {
            try
            {
                //this.niddlEntidad.DataSource = entidades.GetEntidadesAuxiliares().Tables[0];
                //this.niddlEntidad.DataValueField = "name";
                //this.niddlEntidad.DataTextField = "name";
                //this.niddlEntidad.DataBind();
                //this.niddlEntidad.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione una opción", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar entidades correspondiente a: " + ex.Message, "C");
            }
        }

        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text };

            try
            {
                //if (CentidadMetodos.EntidadGetKey(
                //    Convert.ToString(this.niddlEntidad.SelectedValue),
                //    "ppa",
                //    objKey).Tables[0].Rows.Count > 0)
                //{
                //    this.nilblInformacion.Visible = true;
                //    this.nilblInformacion.Text = Convert.ToString(this.niddlEntidad.SelectedValue) + " " + this.txtCodigo.Text + 
                //        " ya se encuentra registrado";

                //    CcontrolesUsuario.InhabilitarControles(
                //        this.Page.Controls);

                //    this.nilbNuevo.Visible = true;
                //}
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

                object[] objValores = new object[] {
                this.txtCodigo.Text,
                this.txtDescripcion.Text
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete(
                    Convert.ToString(this.niddlEntidad.SelectedValue),
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
                //if (Cseguridad.VerificaAccesoPagina(
                //    this.Session["usuario"].ToString(),
                //    System.Configuration.ConfigurationSettings.AppSettings["Modulo"].ToString(),
                //    "Estados.aspx") != 0)
                //{
                //    this.nitxtBusqueda.Focus();

                //    if (this.txtCodigo.Text.Length > 0)
                //    {
                //        this.txtDescripcion.Focus();
                //    }

                if (!IsPostBack)
                {
                    CargarCombos();
                }
                //}
                //else
                //{
                //    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
                //}
            }
        }

        protected void lbNuevo_Click(object sender, EventArgs e)
        {

        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {

        }

        protected void niimbBuscar_Click(object sender, ImageClickEventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(
                this.Page.Controls);

            this.nilbNuevo.Visible = true;

            GetEntidad();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {

        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (CvalidacionOperacion.VerificaOperacion(
            //    this.Session["usuario"].ToString(),
            //    "Auxiliares.aspx",
            //    "A") == false)
            //{
            //    ManejoError("Usuario no autorizado para ejecutar esta operación", "A");
            //    return;
            //}

            CcontrolesUsuario.HabilitarControles(
                this.Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;

            this.txtCodigo.Enabled = false;
            this.txtDescripcion.Focus();

            try
            {
                if (this.gvLista.SelectedRow.Cells[2].Text != "")
                {
                    this.txtCodigo.Text = this.gvLista.SelectedRow.Cells[2].Text;
                }
                else
                {
                    this.txtCodigo.Text = "";
                }

                if (this.gvLista.SelectedRow.Cells[3].Text != "")
                {
                    this.txtDescripcion.Text = this.gvLista.SelectedRow.Cells[3].Text;
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

            try
            {
                //if (CvalidacionOperacion.VerificaOperacion(
                //    this.Session["usuario"].ToString(),
                //    "Auxiliares.aspx",
                //    "E") == false)
                //{
                //    ManejoError("Usuario no autorizado para ejecutar esta operación", "E");
                //    return;
                //}

                object[] objValores = new object[] {
                Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text)
            };

                if (CentidadMetodos.EntidadInsertUpdateDelete(
                    Convert.ToString(this.niddlEntidad.SelectedValue),
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

        protected void ddlEntidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(
                this.Page.Controls);

            CcontrolesUsuario.LimpiarControles(
                this.Page.Controls);

            CcontrolesUsuario.OpcionesDefault(
                this.Page.Controls,
                0);

            this.nilblMensaje.Text = "";
            this.nilbNuevo.Visible = true;

            GetEntidad();
        }

        #endregion Eventos
        protected void lbNuevo_Click(object sender, ImageClickEventArgs e)
        {
            //if (CvalidacionOperacion.VerificaOperacion(
            //    this.Session["usuario"].ToString(),
            //    "Auxiliares.aspx",
            //    "I") == false)
            //{
            //    ManejoError("Usuario no autorizado para ejecutar esta operación", "I");
            //    return;
            //}

            CcontrolesUsuario.HabilitarControles(
                this.Page.Controls);

            this.nilbNuevo.Visible = false;
            this.lbRegistrar.Visible = false;

            this.Session["editar"] = false;

            this.txtCodigo.Enabled = true;
            this.txtCodigo.Focus();
            this.nilblInformacion.Text = "";
        }
        protected void lbCancelar_Click(object sender, ImageClickEventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(
               this.Page.Controls);

            this.gvLista.DataSource = null;
            this.gvLista.DataBind();

            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
        }
        protected void nilbGuardar_Click(object sender, ImageClickEventArgs e)
        {
            Guardar();
        }
    }
}