using Administracion.WebForms.App_Code.Administracion;
using Administracion.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracion.WebForms.Formas.Padministracion
{
    public partial class ParametrosGeneralConfig : BasePage
    {
        #region Instancias

        CparametrosGeneral parametrosgeneral = new CparametrosGeneral();
        Centidades entidad = new Centidades();

        #endregion Instancias

        #region Metodos

        private void manejoTipoDato()
        {
            switch (rblTipoDato.SelectedIndex)
            {
                case 0:
                    txtValor.Visible = true;
                    txtValorF.Visible = false;
                    txtValorN.Visible = false;
                    break;
                case 1:
                    txtValor.Visible = false;
                    txtValorF.Visible = true;
                    txtValorN.Visible = false;
                    break;

                case 2:
                    txtValor.Visible = false;
                    txtValorF.Visible = false;
                    txtValorN.Visible = true;
                    break;

            }
        }

        private void cargarcampos()
        {
            DataView dvCampos = parametrosgeneral.SeleccionaCamposEntidades(ddlTabla.SelectedValue, "");
            this.ddlCampoValor.DataSource = dvCampos;
            this.ddlCampoValor.DataValueField = "name";
            this.ddlCampoValor.DataTextField = "name";
            this.ddlCampoValor.DataBind();
            this.ddlCampoValor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", ""));

            this.ddlCampoEtiqueta.DataSource = dvCampos;
            this.ddlCampoEtiqueta.DataValueField = "name";
            this.ddlCampoEtiqueta.DataTextField = "name";
            this.ddlCampoEtiqueta.DataBind();
            this.ddlCampoEtiqueta.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", ""));
        }


        private void retornarValores()
        {
            DataView dvCampos = entidad.BuscarEntidad2("", ddlTabla.SelectedValue.Trim(), Convert.ToInt16(this.Session["empresa"]));
            dvCampos.RowFilter = "empresa=" + this.Session["empresa"].ToString();
            this.ddlValor.DataSource = dvCampos;
            this.ddlValor.DataValueField = ddlCampoValor.SelectedValue.Trim();
            this.ddlValor.DataTextField = ddlCampoEtiqueta.SelectedValue.Trim();
            this.ddlValor.DataBind();
            this.ddlValor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", ""));
        }
        private void ManejoDs(bool variable)
        {
            this.lblEtiqueta.Visible = variable;
            this.ddlCampoEtiqueta.Visible = variable;
            this.lblCampoValor.Visible = variable;
            this.ddlCampoValor.Visible = variable;
            this.lblTabla.Visible = variable;
            this.ddlTabla.Visible = variable;
            this.ddlValor.Visible = variable;
            this.txtValor.Visible = !variable;
            this.txtValorF.Visible = false;
            this.txtValorN.Visible = false;
            this.rblTipoDato.Visible = !variable;
            this.lblTipoDato.Visible = !variable;

        }

        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                 nombrePaginaActual(), consulta, Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                this.gvLista.DataSource = parametrosgeneral.BuscarEntidad(this.nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();
                this.nilblInformacion.Visible = true;
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";
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

        private void EntidadKey()
        {
            object[] objKey = new object[] { Convert.ToInt16(Session["empresa"]), this.txtNombre.Text };

            try
            {
                if (CentidadMetodos.EntidadGetKey("gConfigParametrosGenerales", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    ManejoError("El parámetro general " + " " + this.txtNombre.Text + " ya se encuentra registrado", "C");

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
            string operacion = "inserta";

            try
            {
                if (Convert.ToBoolean(this.Session["editar"]) == true)
                {
                    operacion = "actualiza";
                }
                string tipodato = null, valor = null, tabla = null, campovalor = null, campoetiqueta = null;
                if (!chkManejaDs.Checked)
                {
                    tipodato = rblTipoDato.SelectedValue;
                    switch (rblTipoDato.SelectedIndex)
                    {
                        case 0:
                            valor = txtValor.Text;
                            break;
                        case 1:
                            valor = txtValorF.Text;
                            break;
                        case 2:
                            valor = txtValorN.Text;
                            break;
                    }

                }
                else
                {
                    valor = ddlValor.SelectedValue.Trim();
                    tabla = ddlTabla.SelectedValue.Trim();
                    campovalor = ddlCampoValor.SelectedValue.Trim();
                    campoetiqueta = ddlCampoEtiqueta.SelectedValue.Trim();
                }

                object[] objValores = new object[] {
                     campoetiqueta ,  //@cEtiqueta  varchar
                     campovalor,   //@cValor varchar
                     tabla,   //@ds varchar
                     Convert.ToInt16(this.Session["empresa"]),   //@empresa    int
                     chkManejaDs.Checked,   //@manejaDS   bit
                     txtNombre.Text,   //@nombre varchar
                     tipodato,   //@tipoDato   varchar
                     valor   //@valor varchar
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("gConfigParametrosGenerales", operacion, "ppa", objValores))
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
                ManejoErrorCatch(this, GetType(), ex);
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


                }
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }

        protected void nilbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(),
                             insertar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", insertar);
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            this.txtNombre.Enabled = true;
            this.txtNombre.Focus();
            this.nilblInformacion.Text = "";
            ManejoDs(false);
            manejoTipoDato();

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
            this.ddlCampoEtiqueta.Visible = false;
            this.ddlCampoValor.Visible = false;
            this.ddlTabla.Visible = false;

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

            this.txtNombre.Enabled = false;

            try
            {
                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.txtNombre.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[2].Text);
                else
                    this.txtNombre.Text = "";

                foreach (Control c in this.gvLista.SelectedRow.Cells[3].Controls)
                {
                    if (c is CheckBox)
                        chkManejaDs.Checked = ((CheckBox)c).Checked;
                }
                ManejoDs(chkManejaDs.Checked);

                if (chkManejaDs.Checked)
                {
                    CargarTablas();

                    if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                    {
                        ddlTabla.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[5].Text);
                        cargarcampos();
                    }
                    else
                        ddlTabla.SelectedValue = "";
                    if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                        this.ddlCampoValor.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[6].Text);
                    else
                        ddlCampoValor.SelectedValue = "";

                    if (this.gvLista.SelectedRow.Cells[7].Text != "&nbsp;")
                    {
                        this.ddlCampoEtiqueta.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[7].Text);
                        retornarValores();
                    }
                    else ddlCampoEtiqueta.SelectedValue = "";

                    if (this.gvLista.SelectedRow.Cells[8].Text != "&nbsp;")
                        this.ddlValor.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[8].Text);
                    else
                        ddlValor.SelectedValue = "";
                }
                else
                {
                    if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                    {
                        this.rblTipoDato.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[4].Text);
                        manejoTipoDato();
                    }
                    else
                    {
                        this.rblTipoDato.SelectedValue = "varchar(50)";
                        manejoTipoDato();
                    }
                    if (this.gvLista.SelectedRow.Cells[8].Text != "&nbsp;")
                    {
                        switch (rblTipoDato.SelectedIndex)
                        {
                            case 0:
                                this.txtValor.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[8].Text);
                                break;
                            case 1:
                                this.txtValorF.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[8].Text);
                                break;
                            case 2:
                                this.txtValorN.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[8].Text);
                                break;

                        }

                    }
                    else
                        this.txtValor.Text = "";
                }

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string operacion = "elimina";

            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                nombrePaginaActual(), eliminar, Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                object[] objValores = new object[] { Convert.ToInt32(Session["empresa"]), Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text) };

                if (CentidadMetodos.EntidadInsertUpdateDelete("gConfigParametrosGenerales", operacion, "ppa", objValores) == 0)
                    ManejoExito("Datos eliminados satisfactoriamente", "E");
                else
                    ManejoError("Errores al eliminar el registro. Operación no realizada", "E");
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

        protected void ddlEntidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            CcontrolesUsuario.OpcionesDefault(this.Page.Controls, 0);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {

            if (txtNombre.Text.Length == 0)
            {
                ManejoError("Ingrese un nombre de parámetro valido", "I");
                return;
            }
            if ((txtValor.Visible & txtValor.Text.Trim().Length == 0) || (txtValorF.Visible & txtValorF.Text.Trim().Length == 0) || (txtValorN.Visible & txtValorN.Text.Trim().Length == 0) || (ddlValor.Visible & ddlValor.SelectedValue.Trim().Length == 0))
            {
                ManejoError("Campos vacios por favor corrija", "I");
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

        protected void chkManejaDs_CheckedChanged(object sender, EventArgs e)
        {
            ManejoDs(chkManejaDs.Checked);
            if (chkManejaDs.Checked)
                CargarTablas();
        }

        private void CargarTablas()
        {
            try
            {
                this.ddlTabla.DataSource = parametrosgeneral.RetornaTablasInfos();
                this.ddlTabla.DataValueField = "nombretabla";
                this.ddlTabla.DataTextField = "nombretabla";
                this.ddlTabla.DataBind();
                this.ddlTabla.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }

        protected void ddlTabla_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlTabla.SelectedValue.Trim().Length > 0)
                    cargarcampos();
                else
                    ManejoError("Seleecione una tabla para poder escojer los campos", "C");

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }


        protected void ddlCampoValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCampoEtiqueta.SelectedValue.Trim().Length > 0 & ddlCampoValor.SelectedValue.Trim().Length > 0)
                {
                    retornarValores();
                }
                else
                {
                    ddlValor.DataSource = null;
                    ddlValor.DataBind();
                }

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }

        }

        protected void ddlCampoEtiqueta_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCampoEtiqueta.SelectedValue.Trim().Length > 0 & ddlCampoValor.SelectedValue.Trim().Length > 0)
                    retornarValores();
                else
                    ManejoError("Debe seleccionar un campo de valor y etiqueta válidos", "C");

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }
        protected void txtNombre_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
            txtNombre.Focus();
        }

        protected void rblTipoDato_SelectedIndexChanged(object sender, EventArgs e)
        {
            manejoTipoDato();
        }

        #endregion Eventos

    }

}