using Contabilidad.seguridadInfos;
using Contabilidad.WebForms.App_Code.Administracion;
using Contabilidad.WebForms.App_Code.CuentasxPagar;
using Contabilidad.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.CuentasxPagar
{
    public partial class CuentasBanco : BasePage
    {
        #region Instancias


        Security seguridad = new Security();
        CcuentasBanco cuentasBanco = new CcuentasBanco();
        CIP ip = new CIP();
        Cpuc puc = new Cpuc();

        CbancoPlano planoBancos = new CbancoPlano();

        #endregion Instancias

        #region Metodos

        protected string nombrePaginaActual()
        {
            string[] arrResult = HttpContext.Current.Request.RawUrl.Split('/');
            string result = arrResult[arrResult.GetUpperBound(0)];
            arrResult = result.Split('?');
            return arrResult[arrResult.GetLowerBound(0)];
        }

        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "C", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }
                this.gvLista.DataSource = cuentasBanco.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";
                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                    this.gvLista.Rows.Count.ToString() + " Registros encontrados", ObtenerIP(), Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private void ManejoError(string error, string operacion)
        {

            CerroresGeneral.ManejoError(this, GetType(), error, "error");
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "er", error, ObtenerIP(), Convert.ToInt16(Session["empresa"]));

        }
        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            Session["transaccion"] = null;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion,
                ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex", mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }
        private void CargarCombos()
        {
            try
            {
                this.ddlBanco.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("gBanco", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"]));
                this.ddlBanco.DataValueField = "codigo";
                this.ddlBanco.DataTextField = "descripcion";
                this.ddlBanco.DataBind();
                this.ddlBanco.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }

            try
            {
                DataView dvPuc = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cPuc", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"]));
                dvPuc.RowFilter = "activo=1 and auxiliar=1 and empresa=" + Session["empresa"].ToString();
                this.ddlCuenta.DataSource = dvPuc;
                this.ddlCuenta.DataValueField = "codigo";
                this.ddlCuenta.DataTextField = "cadena";
                this.ddlCuenta.DataBind();
                this.ddlCuenta.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private void EntidadKey()
        {
            object[] objKey = new object[] { txtCodigo.Text, Convert.ToInt16(Session["empresa"]) };
            try
            {
                if (CentidadMetodos.EntidadGetKey("cCuentaBancaria", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "El código " + txtCodigo.Text + " ya se encuentra registrado", "warning");
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
            string operacion = "inserta", tercero = null, formato = null;

            try
            {
                if (Convert.ToBoolean(this.Session["editar"]) == true)
                    operacion = "actualiza";

                if (ddlTercero.SelectedValue.Length > 0)
                    tercero = ddlTercero.SelectedValue;
                if (ddlFormato.SelectedValue.Length > 0)
                    formato = ddlFormato.SelectedValue;

                object[] objValores = new object[]{
                                        chkActivo.Checked,
                                        ddlCuenta.SelectedValue,
                                        ddlBanco.SelectedValue,
                                        txtCodigo.Text,
                                        chkControlaChequera.Checked,
                                        chkControlaConsecutivo.Checked,
                                        txtNocuenta.Text,
                                        txtDescripcion.Text,
                                        Convert.ToInt16(Session["empresa"]),
                                        Convert.ToDecimal(txvFinalCheque.Text),
                                        formato,
                                        Convert.ToDecimal(txvInicioCheque.Text),
                                        Convert.ToDecimal(txvNumeroCheque.Text),
                                        chkPagoElectronico.Checked,
                                        tercero
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("cCuentaBancaria", operacion, "ppa", objValores))
                {
                    case 0:
                        ManejoExito("Datos registrados satisfactoriamente", "I");
                        break;
                    case 1:
                        ManejoError("Errores al insertar el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                        break;
                }
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
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
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) != 0)
                    this.ddlBanco.Focus();
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                object[] objValores = new object[] { Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text), Convert.ToInt16(Session["empresa"]) };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("cCuentaBancaria", "elimina", "ppa", objValores))
                {
                    case 0:
                        ManejoExito("Registro eliminado satisfactoriamente", "E");
                        break;
                    case 1:
                        ManejoError("Errores al eliminar el registro. Operación no realizada", "E");
                        break;
                }
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
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
            txtCodigo.Enabled = false;
            CargarCombos();
            try
            {
                int i = 2;
                if (this.gvLista.SelectedRow.Cells[i].Text != "&nbsp;")
                    txtCodigo.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[i].Text);
                i++;
                if (this.gvLista.SelectedRow.Cells[i].Text != "&nbsp;")
                    txtDescripcion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[i].Text);
                i++;
                if (this.gvLista.SelectedRow.Cells[i].Text != "&nbsp;")
                    ddlBanco.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[i].Text);
                i = 6;
                if (this.gvLista.SelectedRow.Cells[i].Text != "&nbsp;")
                    txtNocuenta.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[i].Text);
                i++;
                if (this.gvLista.SelectedRow.Cells[i].Text != "&nbsp;")
                    ddlCuenta.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[i].Text);
                i++;
                foreach (WebControl objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        chkControlaChequera.Checked = ((CheckBox)objControl).Checked;
                }
                controlaChequera();
                i++;
                if (this.gvLista.SelectedRow.Cells[i].Text != "&nbsp;")
                    txvInicioCheque.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[i].Text);
                i++;
                if (this.gvLista.SelectedRow.Cells[i].Text != "&nbsp;")
                    txvFinalCheque.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[i].Text);

                i++;
                foreach (WebControl objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        chkControlaConsecutivo.Checked = ((CheckBox)objControl).Checked;
                }
                controlaConsecutivoCheque();
                i++;
                if (this.gvLista.SelectedRow.Cells[i].Text != "&nbsp;")
                    txvNumeroCheque.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[i].Text);
                i++;
                foreach (WebControl objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        chkPagoElectronico.Checked = ((CheckBox)objControl).Checked;
                }
                controlaPagoElectronico();
                i++;
                if (this.gvLista.SelectedRow.Cells[i].Text != "&nbsp;")
                    ddlTercero.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[i].Text);
                i = 16;
                if (this.gvLista.SelectedRow.Cells[i].Text != "&nbsp;")
                    ddlTercero.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[i].Text);

                i++;
                foreach (WebControl objControl in this.gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (objControl is CheckBox)
                        chkActivo.Checked = ((CheckBox)objControl).Checked;
                }
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }

        protected void ddlBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntidadKey();
        }
        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }
        protected void nilbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            Session["editarDetalle"] = false;
            CargarCombos();
            this.nilblInformacion.Text = "";
            txvInicioCheque.Enabled = false;
            controlaChequera();
            controlaPagoElectronico();
            controlaConsecutivoCheque();
        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            Session["transaccion"] = null;
            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlBanco.SelectedValue.Length == 0 || txtCodigo.Text.Length == 0 || txtDescripcion.Text.Length == 0 || txtNocuenta.Text.Length == 0 || ddlCuenta.SelectedValue.Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Campos vacios por favor corrija", "warning");
                    return;
                }

                if (chkControlaChequera.Checked)
                {
                    if (Convert.ToDecimal(txvInicioCheque.Text) > Convert.ToDecimal(txvFinalCheque.Text))
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "El inico debe ser menor al final", "warning");
                        return;
                    }
                }

                if (chkPagoElectronico.Checked)
                {
                    if (ddlTercero.SelectedValue.Length == 0)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar un tercero", "warning");
                        return;
                    }
                }
                Guardar();
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }

        }

        protected void chkControlaChequera_CheckedChanged(object sender, EventArgs e)
        {
            controlaChequera();
        }
        protected void chkControlaConsecutivo_CheckedChanged(object sender, EventArgs e)
        {
            controlaConsecutivoCheque();
        }
        protected void chkPagoElectronico_CheckedChanged(object sender, EventArgs e)
        {
            controlaPagoElectronico();
        }

        private void controlaChequera()
        {
            if (chkControlaChequera.Checked)
            {
                txvInicioCheque.Enabled = true;
                txvFinalCheque.Enabled = true;
                chkControlaConsecutivo.Enabled = true;
            }
            else
            {
                chkControlaConsecutivo.Enabled = false;
                txvInicioCheque.Enabled = false;
                txvFinalCheque.Enabled = false;
                chkControlaConsecutivo.Checked = false;
            }
        }

        private void controlaConsecutivoCheque()
        {
            if (chkControlaConsecutivo.Checked)
            {
                txvNumeroCheque.Enabled = true;
                txvNumeroCheque.Text = "0";
            }
            else
            {
                txvNumeroCheque.Enabled = false;
                txvNumeroCheque.Text = "0";
            }
        }
        private void cargarPagosElectronico()
        {
            try
            {
                this.ddlTercero.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cTercero", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"])); ;
                this.ddlTercero.DataValueField = "id";
                this.ddlTercero.DataTextField = "descripcion";
                this.ddlTercero.DataBind();
                this.ddlTercero.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }

            try
            {
                this.ddlFormato.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("gBanco", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"])); ;
                this.ddlFormato.DataValueField = "codigo";
                this.ddlFormato.DataTextField = "descripcion";
                this.ddlFormato.DataBind();
                this.ddlFormato.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }

        private void controlaPagoElectronico()
        {
            if (chkPagoElectronico.Checked)
            {
                ddlTercero.Enabled = true;
                ddlFormato.Enabled = true;
                cargarPagosElectronico();
            }
            else
            {
                ddlTercero.ClearSelection();
                ddlFormato.ClearSelection();

                ddlTercero.Enabled = false;
                ddlFormato.Enabled = false;

            }
        }

        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        #endregion Eventos


    }
}