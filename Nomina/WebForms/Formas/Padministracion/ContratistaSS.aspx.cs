using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Padministracion
{
    public partial class ContratistaSS : BasePage
    {

        #region Instancias




        CfuncionariosContratista funcionarios = new CfuncionariosContratista();
        CcontratistaSS proveedor = new CcontratistaSS();

        #endregion Instancias

        #region Metodos


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
                this.gvLista.DataSource = proveedor.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();

                this.nilblMensaje.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                            this.gvLista.Rows.Count.ToString() + " Registros encontrados", HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente a: " + limpiarMensaje(ex.Message), "C");
            }
        }
        static string limpiarMensaje(string strIn)
        {
            try
            {
                return Regex.Replace(strIn, @"[^\w\.@-]", " ",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }
        private void ManejoError(string error, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), error, "error");
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                  "er", error, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
        }
        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.nibtnNuevo.Visible = true;
            this.ddlProveedor.Enabled = true;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion,
                  ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }
        private void CargarCombos()
        {
            try
            {
                this.ddlProveedor.DataSource = funcionarios.ProveedoreesContratista(Convert.ToInt16(this.Session["empresa"]));
                this.ddlProveedor.DataValueField = "codigo";
                this.ddlProveedor.DataTextField = "descripcion";
                this.ddlProveedor.DataBind();
                this.ddlProveedor.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar proveedor. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
            }
        }
        private void EntidadKey()
        {
            object[] objKey = new object[] { Convert.ToInt16(Session["empresa"]), this.ddlProveedor.Text };

            try
            {
                if (CentidadMetodos.EntidadGetKey("nContratistaSeguridadSocial", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "El proveedor " + this.ddlProveedor.SelectedItem.Text + " ya se encuentra registrada", "warning");
                    CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
                    this.nibtnNuevo.Visible = true;
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

                object[] objValores = new object[] { Convert.ToInt16(Session["empresa"]), Convert.ToDateTime(txtFechaFinal.Text), Convert.ToDateTime(txtFechaInicial.Text), txtObservacion.Text, Convert.ToString(this.ddlProveedor.SelectedValue) };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("nContratistaSeguridadSocial", operacion, "ppa", objValores))
                {
                    case 0:
                        ManejoExito("Registro guardado correctamente", "I");
                        break;
                    case 1:
                        ManejoError("Error al guardar el registro ", "I");
                        break;
                }

            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar los datos correspondiente a: " + limpiarMensaje(ex.Message), operacion.Substring(0, 1).ToUpper());
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
                 ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) != 0)
                    this.ddlProveedor.Focus();
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
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
            CargarCombos();
            this.nibtnNuevo.Visible = false;
            this.Session["editar"] = true;
            this.ddlProveedor.Enabled = false;
            txtFechaInicial.Enabled = false;
            txtFechaFinal.Enabled = false;
            try
            {
                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.ddlProveedor.SelectedValue = this.gvLista.SelectedRow.Cells[2].Text.Trim();

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                    this.txtFechaInicial.Text = this.gvLista.SelectedRow.Cells[4].Text.Trim();
                else
                    this.txtFechaInicial.Text = "";

                if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                    txtFechaFinal.Text = this.gvLista.SelectedRow.Cells[5].Text.Trim();
                else
                    this.txtFechaFinal.Text = "";

                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                    txtObservacion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[6].Text.Trim());
                else
                    txtObservacion.Text = "";
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + limpiarMensaje(ex.Message), "C");
            }
        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string operacion = "elimina";

            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                           nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }


                object[] objValores = new object[] { Convert.ToInt16(Session["empresa"]),Convert.ToDateTime(this.gvLista.Rows[e.RowIndex].Cells[5].Text),
                Convert.ToDateTime(this.gvLista.Rows[e.RowIndex].Cells[4].Text), Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text) };

                if (CentidadMetodos.EntidadInsertUpdateDelete("nContratistaSeguridadSocial", operacion, "ppa", objValores) == 0)
                    ManejoExito("Registro eliminado satisfactoriamente", "E");
                else
                    ManejoError("Errores al eliminar el registro. Operación no realizada", "E");

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
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + limpiarMensaje(ex.Message), "E");
                }
            }
        }
        protected void gvAsignacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            CargarCombos();
            gvLista.DataBind();
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
        protected void txtFechaIni_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFechaInicial.Text);
            }
            catch
            {
                CerroresGeneral.ManejoError(this, GetType(), "Formato de fecha no valido", "warning");
                txtFechaInicial.Text = "";
                txtFechaInicial.Focus();
                return;

            }
        }
        protected void txtFechaFinal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFechaFinal.Text);
            }
            catch
            {
                CerroresGeneral.ManejoError(this, GetType(), "Formato de fecha no valido", "warning");
                txtFechaFinal.Text = "";
                txtFechaFinal.Focus();
                return;

            }
        }
        protected void nibtnBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nibtnNuevo.Visible = true;
            GetEntidad();
        }
        protected void nibtnNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                              nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);
            CargarCombos();
            this.nibtnNuevo.Visible = false;
            this.Session["editar"] = false;
            this.ddlProveedor.Focus();
        }
        protected void nibtnCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.nibtnNuevo.Visible = true;
            this.ddlProveedor.Enabled = true;
        }
        protected void nibtnGuardar_Click(object sender, EventArgs e)
        {
            if (ddlProveedor.SelectedValue.Length == 0 || txtFechaInicial.Text.Length == 0 || txtFechaFinal.Text.Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Campos vacios por favor corrija", "warning");
                return;
            }

            Guardar();
        }

        #endregion Eventos
    }
}