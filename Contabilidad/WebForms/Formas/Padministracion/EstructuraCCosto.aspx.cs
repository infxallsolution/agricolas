using Contabilidad.seguridadInfos;
using Contabilidad.WebForms.App_Code.Administracion;
using Contabilidad.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.Padministracion
{
    public partial class EstructuraCCosto : BasePage
    {
        #region Instancias
        Security seguridad = new Security();
        CestruturaCC estructura = new CestruturaCC();

        #endregion Instancias

        #region Metodos


        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                 nombrePaginaActual(), "C", Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                this.gvLista.DataSource = estructura.BuscarEntidad(this.nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C",
                 ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                this.gvLista.Rows.Count.ToString() + " Registros encontrados", HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(this.Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente a: " + ex.Message, "C");
            }
        }

        private void Consecutivo()
        {
            try
            {
                this.txtCodigo.Text = estructura.Consecutivo(Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al obtener consecutivo. Correspondiente a: " + ex.Message, "C");
            }
        }

        private void ManejoError(string error, string operacion)
        {
            this.Session["error"] = error;
            this.Session["paginaAnterior"] = this.Page.Request.FilePath.ToString();

            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                 "er", error, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(this.Session["empresa"]));
            this.Response.Redirect("~/Contabilidad/Error.aspx", false);
        }

        private void ManejoExito(string mensaje, string operacion)
        {
            this.nilblMensaje.Text = mensaje;
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;

            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(this.Session["empresa"]));
            GetEntidad();
        }

        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text, Convert.ToInt16(this.txtCodigo.Text), Convert.ToInt16(this.Session["empresa"]) };
            try
            {
                if (CentidadMetodos.EntidadGetKey("iDestino", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    this.nilblInformacion.Text = "Destino " + this.txtCodigo.Text + " ya se encuentra registrado";
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
            decimal nivel = 0;
            try
            {

                if (Convert.ToBoolean(this.Session["editar"]) == true)
                {
                    operacion = "actualiza";
                    nivel = Convert.ToDecimal(txtCodigo.Text);
                }
                else
                {
                    Consecutivo();
                    nivel = Convert.ToDecimal(txtCodigo.Text);
                }

                object[] objValores = new object[]{
                this.chkActivo.Checked,
                txtDescripcion.Text,
                Convert.ToInt16(Session["empresa"]),
                Convert.ToDecimal(txvInicio.Text),
               nivel,
               Convert.ToDecimal(txvTamaño.Text),
               Convert.ToDecimal(txvTotal.Text)
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("cEstructuraCCosto", operacion, "ppa", objValores))
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
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(this.Session["empresa"])) != 0)
                {
                    this.nitxtBusqueda.Focus();
                    if (this.txtCodigo.Text.Length > 0)
                        this.txtDescripcion.Focus();
                }
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }
        protected void niimbBuscar_Click(object sender, ImageClickEventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }


        protected void lbNuevo_Click(object sender, ImageClickEventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
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
            txvInicio.Focus();
            this.nilblInformacion.Text = "";
        }
        protected void lbCancelar_Click(object sender, ImageClickEventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
        }

        protected void lbRegistrar_Click1(object sender, ImageClickEventArgs e)
        {
            if (txtCodigo.Text.Length == 0 || txtDescripcion.Text.Length == 0 || txvInicio.Text.Length == 0 || txvTamaño.Text.Length == 0 || txvTotal.Text.Length == 0)
            {
                nilblInformacion.Text = "Campos vacios por favor corrija";
                return;
            }

            if (Convert.ToDecimal(txvInicio.Text) < 0 || Convert.ToDecimal(txvTamaño.Text) < 0 || Convert.ToDecimal(txvTotal.Text) < 0)
            {
                nilblInformacion.Text = "Campos en cero, por favor corrija deben de ser mayor que cero.";
                return;
            }

            Guardar();
        }

        #endregion Eventos
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "A", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "A");
                return;
            }
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            this.nilblMensaje.Text = "";
            this.txtCodigo.Enabled = false;
            this.txtDescripcion.Focus();
            try
            {
                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.txtCodigo.Text = this.gvLista.SelectedRow.Cells[2].Text;
                else
                    this.txtCodigo.Text = "";

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    this.txvInicio.Text = this.gvLista.SelectedRow.Cells[3].Text;
                else
                    this.txvInicio.Text = "0";
                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                    this.txvTamaño.Text = this.gvLista.SelectedRow.Cells[4].Text;
                else
                    this.txvTamaño.Text = "0";
                if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                    this.txvTotal.Text = this.gvLista.SelectedRow.Cells[5].Text;
                else
                    this.txvTotal.Text = "0";

                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                    this.txtDescripcion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[6].Text);
                else
                    this.txtDescripcion.Text = "";

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[7].Controls)
                {
                    this.chkActivo.Checked = ((CheckBox)objControl).Checked;
                }

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }
            string operacion = "elimina";
            try
            {
                object[] objValores = new object[] { Convert.ToInt16(Session["empresa"]), Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)) };

                if (CentidadMetodos.EntidadInsertUpdateDelete("cEstructuraCCosto", operacion, "ppa", objValores) == 0)
                    ManejoExito("Datos eliminados satisfactoriamente", "E");
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
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
            }
        }
    }
}