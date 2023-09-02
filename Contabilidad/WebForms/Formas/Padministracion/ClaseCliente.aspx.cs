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

namespace Contabilidad.WebForms.Formas.Padministracion
{
    public partial class ClaseCliente : BasePage
    {

        #region Instancias

        CclaseCliente operaciones = new CclaseCliente();

        #endregion Instancias

        #region Metodos

        private void CargarCombos()
        {
            try
            {
                DataView dvPuc = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cPuc", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"]));
                dvPuc.RowFilter = "activo=1 and auxiliar=1 and empresa=" + Session["empresa"].ToString();
                this.ddlAuxiliarCxP.DataSource = dvPuc;
                this.ddlAuxiliarCxP.DataValueField = "codigo";
                this.ddlAuxiliarCxP.DataTextField = "cadena";
                this.ddlAuxiliarCxP.DataBind();
                this.ddlAuxiliarCxP.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }

            try
            {
                DataView dvPuc = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cPuc", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"]));
                dvPuc.RowFilter = "activo=1 and auxiliar=1 and empresa=" + Session["empresa"].ToString();
                this.ddlAuxiliarAnticipo.DataSource = dvPuc;
                this.ddlAuxiliarAnticipo.DataValueField = "codigo";
                this.ddlAuxiliarAnticipo.DataTextField = "cadena";
                this.ddlAuxiliarAnticipo.DataBind();
                this.ddlAuxiliarAnticipo.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }

            try
            {
                DataView dvPuc = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cPuc", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"]));
                dvPuc.RowFilter = "activo=1 and auxiliar=1 and empresa=" + Session["empresa"].ToString();
                this.ddlAuxiliarPasivo.DataSource = dvPuc;
                this.ddlAuxiliarPasivo.DataValueField = "codigo";
                this.ddlAuxiliarPasivo.DataTextField = "cadena";
                this.ddlAuxiliarPasivo.DataBind();
                this.ddlAuxiliarPasivo.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }

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

                this.gvLista.DataSource = operaciones.BuscarEntidad(this.nitxtBusqueda.Text, Convert.ToInt32(Session["empresa"]));
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

        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text, Convert.ToInt16(Session["empresa"]) };

            try
            {
                if (CentidadMetodos.EntidadGetKey("cxcClaseCliente", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    ManejoError("Operación " + this.txtCodigo.Text + " ya se encuentra registrada", "C");
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
            string operacion = "inserta", auxiliarCxP = null, auxiliarAnticipo = null, auxiliarPasivo = null;

            try
            {
                if (Convert.ToBoolean(this.Session["editar"]) == true)
                    operacion = "actualiza";
                if (ddlAuxiliarCxP.SelectedValue.Length > 0)
                    auxiliarCxP = ddlAuxiliarCxP.SelectedValue;
                if (ddlAuxiliarAnticipo.SelectedValue.Length > 0)
                    auxiliarAnticipo = ddlAuxiliarAnticipo.SelectedValue;
                if (ddlAuxiliarPasivo.SelectedValue.Length > 0)
                    auxiliarPasivo = ddlAuxiliarPasivo.SelectedValue;


                object[] objValores = new object[]{
                  this.chkActivo.Checked,
                auxiliarAnticipo,
                auxiliarCxP,
                auxiliarPasivo,
                this.txtCodigo.Text,
                this.txtDescripcion.Text,
                Convert.ToInt16(Session["empresa"])

            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("cxcClaseCliente", operacion, "ppa", objValores))
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
                {
                    this.nitxtBusqueda.Focus();

                    if (this.txtCodigo.Text.Length > 0)
                        this.txtDescripcion.Focus();
                }
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
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
            CargarCombos();
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            this.txtCodigo.Enabled = true;
            this.txtCodigo.Focus();
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

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
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
            CargarCombos();
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
                    this.ddlAuxiliarCxP.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[4].Text);

                if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                    ddlAuxiliarAnticipo.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[5].Text);

                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                    this.ddlAuxiliarPasivo.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[6].Text);

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[7].Controls)
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

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string operacion = "elimina";

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                  nombrePaginaActual(), eliminar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", eliminar);
                return;
            }

            try
            {
                object[] objValores = new object[] { Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text), Convert.ToInt16(Session["empresa"]) };

                if (CentidadMetodos.EntidadInsertUpdateDelete("cxcClaseCliente", operacion, "ppa", objValores) == 0)
                    ManejoExito("Datos eliminados satisfactoriamente", "E");
                else
                    ManejoError("Errores al eliminar el registro. Operación no realizada", "E");

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Length == 0 || txtDescripcion.Text.Length == 0)
            {
                nilblInformacion.Text = "Campos vacios por favor corrija";
                return;
            }

            Guardar();
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