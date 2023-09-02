using Agronomico.seguridadinfos;
using Agronomico.WebForms.App_Code.Administracion;
using Agronomico.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agronomico.WebForms.Formas.Padministracion
{
    public partial class Caracteristicas : BasePage
    {
        #region Instancias

        CentidadMetodos CentidadMetodos = new CentidadMetodos();
        Security seguridad = new Security();
        Ccaracteristicas grupoNovedad = new Ccaracteristicas();
        CgrupoCaracteristica grupoCaracteristica = new CgrupoCaracteristica();
        CIP ip = new CIP();


        #endregion Instancias

        #region Metodos

        private void Consecutivo()
        {
            try
            {
                this.txtCodigo.Text = grupoNovedad.Consecutivo(Convert.ToInt32(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                                        ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "C", Convert.ToInt32(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                this.gvLista.DataSource = grupoNovedad.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt32(Session["empresa"]));
                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(
                    this.Session["usuario"].ToString(), "C",
                  ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                    this.gvLista.Rows.Count.ToString() + " Registros encontrados", ObtenerIP(), Convert.ToInt32(Session["empresa"]));

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
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                    "ex", mensaje, ObtenerIP(), Convert.ToInt32(Session["empresa"]));
            GetEntidad();
        }

        private void CargarCombos()
        {

        }

        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text, Convert.ToInt32(Session["empresa"]) };
            try
            {
                if (CentidadMetodos.EntidadGetKey("aCaracteristicas",
                    "ppa",
                    objKey).Tables[0].Rows.Count > 0)
                {
                    ManejoError("El código " + this.txtCodigo.Text + " ya se encuentra registrada", "C");

                    CcontrolesUsuario.InhabilitarControles(
                        this.Page.Controls);

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


            if (this.txtDescripcion.Text.Length == 0 || this.txtCodigo.Text.Length == 0 || (chkGrupoC.Checked == true & ddlGrupoC.SelectedValue.Trim().Length == 0))
            {
                CerroresGeneral.ManejoError(this, GetType(), "Campos vacios por favor corrija", "warning");
            }
            else
            {
                try
                {
                    string grupoCaracteristica = null;

                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                        operacion = "actualiza";
                    else
                        Consecutivo();

                    if (chkGrupoC.Checked == true)
                        grupoCaracteristica = ddlGrupoC.SelectedValue.Trim();


                    object[] objValores = new object[]{
                               chkActivo.Checked, //@activo
                               Convert.ToInt32(txtCodigo.Text),  //@codigo
                              txtDescripcion.Text.Trim(),  //@descripcion
                              (int) this.Session["empresa"],//@empresa
                              grupoCaracteristica,  //@grupoCaracteristica
                              chkGrupoC.Checked  //@manejaCaractistica
                    
                };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("aCaracteristica", operacion, "ppa", objValores))
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
        }

        #endregion Metodos

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(),
                    ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt32(Session["empresa"])) != 0)
                    this.txtCodigo.Focus();

                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }



        protected void lbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                  nombrePaginaActual(), "I", Convert.ToInt32(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            Consecutivo();
            txtCodigo.Enabled = false;
            txtDescripcion.Focus();
            ddlGrupoC.Enabled = false;
            this.nilblInformacion.Text = "";
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
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "E", Convert.ToInt32(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                object[] objValores = new object[] {
                Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)),
                Convert.ToInt32(Session["empresa"])
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("aCaracteristica", "elimina", "ppa", objValores))
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
                    ManejoErrorCatch(ex);
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
            Guardar();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                nombrePaginaActual(), editar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", editar);
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
                else
                    this.txtCodigo.Text = "";

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    this.txtDescripcion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[3].Text);
                else
                    this.txtDescripcion.Text = "";


                foreach (Control objControl in this.gvLista.SelectedRow.Cells[4].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkGrupoC.Checked = ((CheckBox)objControl).Checked;
                }

                if (chkGrupoC.Checked == true)
                {
                    try
                    {
                        CargarGCaracteristica();
                        ddlGrupoC.SelectedValue = gvLista.SelectedRow.Cells[5].Text.Trim();
                    }
                    catch (Exception ex)
                    {
                        ManejoError("Error al cargar unidad de medida. Correspondiente a: " + ex.Message, "C");
                    }

                }
                else
                {
                    ddlGrupoC.DataSource = null;
                    ddlGrupoC.DataBind();
                    ddlGrupoC.Enabled = false;
                }

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[6].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkActivo.Checked = ((CheckBox)objControl).Checked;
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

        protected void chkImpuesto_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGrupoC.Checked)
            {
                try
                {
                    CargarGCaracteristica();
                }
                catch (Exception ex)
                {
                    ManejoError("Error al cargar unidad de medida. Correspondiente a: " + ex.Message, "C");
                }

            }
            else
            {
                this.ddlGrupoC.DataSource = null;
                this.ddlGrupoC.DataBind();
                this.ddlGrupoC.Enabled = false;

            }
        }

        private void CargarGCaracteristica()
        {
            this.ddlGrupoC.DataSource = grupoCaracteristica.BuscarEntidad("", Convert.ToInt16(this.Session["empresa"]));
            this.ddlGrupoC.DataValueField = "codigo";
            this.ddlGrupoC.DataTextField = "descripcion";
            this.ddlGrupoC.DataBind();
            this.ddlGrupoC.Items.Insert(0, new ListItem("", ""));
            this.ddlGrupoC.Enabled = true;
        }

        #endregion Eventos
    }
}