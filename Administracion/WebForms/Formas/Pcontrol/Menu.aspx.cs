using Administracion.WebForms.App_Code.General;
using Administracion.WebForms.App_Code.Seguridad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracion.WebForms.Formas.Pcontrol
{
    public partial class Menu : BasePage
    {
        #region Instancias

        private Cmenu menu = new Cmenu();

        #endregion Instancias

        #region Metodos

        private void mWebSite()
        {
            txtPagina.Enabled = chkWebSite.Checked;
            selOperacion.Visible = chkWebSite.Checked;
            if (chkWebSite.Checked)
                cargarOperaciones();
            else
            {
                selOperacion.DataSource = null;
                selOperacion.DataBind();
            }

            if (!chkWebSite.Checked)
                txtPagina.Text = "";
        }

        private void cargarOperaciones()
        {
            try
            {
                DataView dvOperaciones = CentidadMetodos.EntidadGet("sOperaciones", "ppa").Tables[0].DefaultView;
                dvOperaciones.RowFilter = "activo=1";
                dvOperaciones.Sort = "descripcion";
                this.selOperacion.DataSource = dvOperaciones;
                this.selOperacion.DataValueField = "codigo";
                this.selOperacion.DataTextField = "descripcion";
                this.selOperacion.DataBind();

            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }

        private void mPadre()
        {
            ddlPadre.Enabled = chkPadre.Checked;
            chkWebSite.Enabled = chkPadre.Checked;
            mWebSite();
            cargarMenu();
            if (!chkPadre.Checked)
                ddlPadre.ClearSelection();
        }

        private bool guardaOperaciones(string rowidnew, bool validaroperacion)
        {
            object[] objValoresConcepto = new object[] { Convert.ToString(rowidnew) };

            switch (CentidadMetodos.EntidadInsertUpdateDelete("sMenusOperaciones", "elimina", "ppa", objValoresConcepto))
            {
                case 1:
                    validaroperacion = false;
                    break;
            }

            for (int x = 0; x < this.selOperacion.Items.Count; x++)
            {

                if (this.selOperacion.Items[x].Selected == true)
                {
                    object[] objValoresConceptoinserta = new object[] { this.selOperacion.Items[x].Value, rowidnew };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("sMenusOperaciones", "inserta", "ppa", objValoresConceptoinserta))
                    {
                        case 1:
                            validaroperacion = true;
                            break;
                    }
                }
            }
            return validaroperacion;
        }


        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                  nombrePaginaActual(), consulta, Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Usuario no autorizado para ejecutar esta operación", "error");
                    return;
                }

                DataView dvMenu = menu.GetMenuSitio(Convert.ToString(this.niddlSitio.SelectedValue));
                dvMenu.Sort = "idMenu";
                this.gvLista.DataSource = dvMenu;
                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), consulta, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                   "ex", this.gvLista.Rows.Count.ToString() + " Registros encontrados", HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(this.Session["empresa"]));
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }

        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");

            selOperacion.Visible = false;
            selOperacion.DataSource = null;
            selOperacion.DataBind();
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            seguridad.InsertaLog(Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + Page.ToString(),
                "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            nilbNuevo.Visible = true;

        }

        private void CargarCombos()
        {
            try
            {
                this.niddlSitio.DataSource = OrdenarEntidadSinEmpresayActivo(CentidadMetodos.EntidadGet("sModulos", "ppa"), "descripcion");
                this.niddlSitio.DataValueField = "codigo";
                this.niddlSitio.DataTextField = "descripcion";
                this.niddlSitio.DataBind();
                this.niddlSitio.Items.Insert(0, new ListItem("", ""));

            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }

        private void cargarMenu()
        {
            try
            {
                DataView dvMenu = CentidadMetodos.EntidadGet("smenus", "ppa").Tables[0].DefaultView;
                dvMenu.RowFilter = "mweb=0 and modulo='" + niddlSitio.SelectedValue + "'";
                dvMenu.Sort = "idMenu";
                this.ddlPadre.DataSource = dvMenu;
                this.ddlPadre.DataValueField = "rowid";
                this.ddlPadre.DataTextField = "cadena";
                this.ddlPadre.DataBind();
                this.ddlPadre.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }

        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtDescripcion.Text, Convert.ToString(this.niddlSitio.SelectedValue) };

            try
            {
                if (CentidadMetodos.EntidadGetKey("sMenu", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Menu " + this.txtDescripcion.Text + " ya se encuentra registrado", "warning");

                    CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
                    this.nilbNuevo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }

        private void Guardar()
        {
            string operacion = "inserta";
            string modulo = "";
            string pagina;
            string padre;
            int retorno;
            string rowidnew;
            bool validaroperacion = false;
            try
            {
                modulo = niddlSitio.SelectedValue;
                pagina = chkWebSite.Checked ? txtPagina.Text : null;
                padre = chkPadre.Checked ? ddlPadre.SelectedValue.Trim() : null;

                using (TransactionScope s = new TransactionScope())
                {
                    if (this.txtDescripcion.Text.Length == 0 || Convert.ToString(this.niddlSitio.SelectedValue).Length == 0 | (this.txtPagina.Text.Length == 0 & chkWebSite.Checked))
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "Campos vacios por favor corrija", "warning");
                        return;
                    }
                    else
                    {
                        try
                        {
                            if (Convert.ToBoolean(this.Session["editar"]) == true)
                            {
                                object[] objValores = new object[]{
                             chkActivo.Checked,       //                      @activo   bit
                              DateTime.Now,      //@fechaModificacion  datetime
                              modulo,      //@modulo varchar
                              chkWebSite.Checked,      //@mWeb   bit
                              txtDescripcion.Text,      //@nombre varchar
                               pagina  ,   //@pagina varchar
                               hfrowid.Value  ,  //@rowid  int
                               Session["usuario"].ToString()     //@usuarioModificacion    varchar
                            };

                                switch (CentidadMetodos.EntidadInsertUpdateDelete("sMenus", "actualiza", "ppa", objValores))
                                {
                                    case 0:
                                        validaroperacion = guardaOperaciones(hfrowid.Value, validaroperacion);
                                        break;

                                    case 1:
                                        validaroperacion = true;
                                        CerroresGeneral.ManejoError(this, GetType(), "Errores al insertar el registro. Operación no realizada", "error");
                                        break;
                                }
                            }
                            else
                            {
                                menu.InsertasMenus(activo: chkActivo.Checked, fecharegistro: DateTime.Now, modulo: modulo, mweb: chkWebSite.Checked, nombre: txtDescripcion.Text, padre: padre, pagina: pagina, usuarioRegistro: Session["usuario"].ToString(),
                                   retorno: out retorno, rowid: out rowidnew);
                                switch (retorno)
                                {
                                    case 0:
                                        validaroperacion = guardaOperaciones(rowidnew, validaroperacion);
                                        break;
                                    case 1:
                                        validaroperacion = true;
                                        CerroresGeneral.ManejoError(this, GetType(), "Errores al insertar el registro. Operación no realizada", "error");
                                        break;
                                }
                            }

                            if (validaroperacion == false)
                            {
                                s.Complete();
                                ManejoExito("Datos ingresados satisfactoriamente", operacion.Substring(0, 1).ToUpper());
                            }
                            else
                                CerroresGeneral.ManejoError(this, GetType(), "Errores al insertar el registro. Operación no realizada", "error");
                        }
                        catch (Exception ex)
                        {
                            CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
                        }
                    }
                }
                if (validaroperacion == false)
                    GetEntidad();
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoError(this, GetType(), ex.Message, "info");
            }
        }

        #endregion Metodos

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

            if (this.Session["usuario"] == null)
            {
                this.Response.Redirect("~/WebForms/Inicio.aspx");
                return;
            }

            if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                    nombrePaginaActual(), Convert.ToInt16(this.Session["empresa"])) != 0)
            {
                if (this.txtDescripcion.Text.Length == 0)
                    this.niddlSitio.Focus();
                else
                    this.txtPagina.Focus();

                if (!IsPostBack)
                {
                    CcontrolesUsuario.LimpiarControles(Page.Controls);
                    CcontrolesUsuario.InhabilitarControles(Page.Controls);
                    chkWebSite.Enabled = false;
                    CargarCombos();

                }
            }
            else
                ManejoErrorAcceso("Usuario no autorizado para ingresar a esta página", "IN");

        }

        protected void nilbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                   nombrePaginaActual(), insertar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Usuario no autorizado para ejecutar esta operación", "error");
                return;
            }


            if (niddlSitio.SelectedValue.Trim().Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar un sitio para continuar", "error");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            ddlPadre.Enabled = false;
            txtPagina.Enabled = false;
            this.txtDescripcion.Enabled = true;
            ddlPadre.ClearSelection();
        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);

            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            selOperacion.DataSource = null;
            selOperacion.DataBind();
            selOperacion.Visible = false;
            this.nilbNuevo.Visible = true;
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                 nombrePaginaActual(), editar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Usuario no autorizado para ejecutar esta operación", "error");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            chkPadre.Checked = false;

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;

            this.txtDescripcion.Focus();

            try
            {

                if (((HiddenField)(this.gvLista.SelectedRow.FindControl("hfrowid"))).Value != "&nbsp;")
                    hfrowid.Value = ((HiddenField)(this.gvLista.SelectedRow.FindControl("hfrowid"))).Value;
                else
                    hfrowid.Value = "";

                var hfpadre = this.gvLista.SelectedRow.FindControl("hfpadre") as HiddenField;
                if (hfpadre.Value != "&nbsp;")
                {
                    chkPadre.Checked = true;
                    mPadre();
                    ddlPadre.SelectedValue = hfpadre.Value;
                }
                else
                {
                    ddlPadre.ClearSelection();
                    chkPadre.Checked = false;
                }

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[7].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkWebSite.Checked = ((CheckBox)objControl).Checked;
                    selOperacion.Visible = chkWebSite.Checked;
                    mWebSite();
                }
                if (chkWebSite.Checked)
                {
                    if (((HiddenField)(this.gvLista.SelectedRow.FindControl("hfrowid"))).Value != "&nbsp;")
                    {
                        for (int x = 0; x < this.selOperacion.Items.Count; x++)
                        {
                            if (menu.validaSitioOperacion(((HiddenField)(this.gvLista.SelectedRow.FindControl("hfrowid"))).Value, selOperacion.Items[x].Value) == 1)
                                selOperacion.Items[x].Selected = true;
                            else
                                selOperacion.Items[x].Selected = false;
                        }
                    }
                    if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                        this.txtPagina.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[6].Text);
                    else
                        this.txtPagina.Text = "";
                }
                else
                {
                    txtPagina.Text = "";
                    selOperacion.Visible = false;
                    selOperacion.DataSource = null;
                    selOperacion.DataBind();
                }

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    this.txtDescripcion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[3].Text);
                else
                    this.txtDescripcion.Text = "";

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[8].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkActivo.Checked = ((CheckBox)objControl).Checked;
                }
                if (ddlPadre.SelectedValue.Trim().Length == 0)
                    chkPadre.Checked = false;

                chkPadre.Enabled = false;
                ddlPadre.Enabled = false;
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                  nombrePaginaActual(), eliminar, Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Usuario no autorizado para ejecutar esta operación", "error");
                    return;
                }


                object[] objValores = new object[] { ((HiddenField)(this.gvLista.Rows[e.RowIndex].FindControl("hfrowid"))).Value };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("sMenus", "elimina", "ppa", objValores))
                {
                    case 0:
                        ManejoExito("Registro eliminado satisfactoriamente", "E");
                        GetEntidad();
                        break;
                    case 1:
                        CerroresGeneral.ManejoError(this, GetType(), "Error al eliminar el registro. Operación no realizada", "error");
                        break;
                }
            }
            catch (Exception ex)
            {
                if (ex.HResult.ToString() == "-2146233087")
                {
                    CerroresGeneral.ManejoError(this, GetType(), "El código ('" + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)) +
                    "' - ' " + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[3].Text)) + "') tiene una asociación, no es posible eliminar el registro.", "error");
                }
                else
                    CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }

        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
            txtDescripcion.Focus();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {

            Guardar();
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }

        protected void nilblRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Seguridad.aspx");
        }

        protected void niddlSitio_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvLista.DataSource = null;
            gvLista.DataBind();
            GetEntidad();
            this.txtDescripcion.Focus();
        }

        protected void chkPadre_CheckedChanged(object sender, EventArgs e)
        {
            mPadre();
        }

        protected void chkWebSite_CheckedChanged(object sender, EventArgs e)
        {
            mWebSite();
        }
        #endregion Eventos
    }
}