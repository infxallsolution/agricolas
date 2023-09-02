using Contabilidad.WebForms.App_Code.Administracion;
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
    public partial class Puc : BasePage
    {
        #region Instancias

        Cpuc cuentas = new Cpuc();

        #endregion Instancias

        #region Metodos


        private void manejoCampos()
        {
            ddlTipoDisponible.ClearSelection();
            if (chkDisponible.Checked)
                ddlTipoDisponible.Enabled = true;
            else
                ddlTipoDisponible.Enabled = false;

            if (chkCcosto.Checked)
            {
                ddlGrupoCC.Enabled = true;
                cargarGrupoCC();
            }
            else
            {
                ddlGrupoCC.Enabled = false;
                ddlGrupoCC.DataSource = null;
                this.ddlGrupoCC.DataBind();
            }

            if (chkImpuesto.Checked)
            {
                ddlCalseIR.Enabled = true;
                ddlTipoIR.Enabled = true;
                //  txvTasaIR.Enabled = true;
                cargarClaseIR();
                //if (ddlTipoIR.SelectedValue == "RF" || ddlTipoIR.SelectedValue == "RP")
                //    txvBaseIR.Enabled = true;
                //else
                //    txvBaseIR.Enabled = false;
                //txvBaseIR.Text = "0";
                //txvTasaIR.Text = "0";
            }
            else
            {
                ddlCalseIR.Enabled = false;
                ddlTipoIR.Enabled = false;
                //txvBaseIR.Text = "0";
                //txvTasaIR.Text = "0";
                //txvBaseIR.Enabled = false;
                //txvTasaIR.Enabled = false;
                ddlCalseIR.DataSource = null;
                ddlCalseIR.DataBind();
            }

            if (chkTercero.Checked)
            {
                ddlTipoManejaTercero.Enabled = true;
                if (ddlTipoManejaTercero.SelectedValue == "SS")
                    ddlTipoSaldo.Visible = true;
                else
                    ddlTipoSaldo.Visible = false;
            }
            else
            {
                ddlTipoManejaTercero.Enabled = false;
                ddlTipoSaldo.Visible = false;
            }

        }

        private void ComportamientoTipo(int estado)
        {
            try
            {
                if (estado == 0)
                {
                    if (cuentas.VerificaCuentaEnMovimientos(this.txtCodigo.Text) == 1)
                    {
                        ManejoError("Cuenta con movimiento no es posible editar su tipo", "A");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }

            if (chkAuxiliar.Checked)
            {
                this.chkCcosto.Enabled = true;
                this.chkTercero.Enabled = true;
                this.chkTercero.Focus();
            }
            else
            {
                this.chkCcosto.Enabled = false;
                this.chkTercero.Enabled = false;
                this.chkActio.Focus();
            }
        }

        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(),//pagina
                                consulta, Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", consulta);
                    return;
                }
                this.gvLista.DataSource = cuentas.BuscarEntidad(this.nitxtBusqueda.Text, Convert.ToInt16(this.Session["empresa"]));
                this.gvLista.DataBind();
                this.nilblInformacion.Visible = true;
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                   "ex", this.gvLista.Rows.Count.ToString() + " Registros encontrados", HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(this.Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
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

            CcontrolesUsuario.InhabilitarControles(this.formContainer.Controls);
            CcontrolesUsuario.LimpiarControles(this.formContainer.Controls);
            CcontrolesUsuario.InhabilitarControles(this.formImpuesto.Controls);
            CcontrolesUsuario.LimpiarControles(this.formImpuesto.Controls);
            this.formImpuesto.Visible = false;

            this.nilbNuevo.Visible = true;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }


        private void CargarCombos()
        {
            try
            {
                this.ddlPlanCuenta.DataSource = OrdenarEntidad(CentidadMetodos.EntidadGet("cPlanCuenta", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlPlanCuenta.DataValueField = "codigo";
                this.ddlPlanCuenta.DataTextField = "descripcion";
                this.ddlPlanCuenta.DataBind();
                this.ddlPlanCuenta.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
            cargarGrupoCC();
            cargarClaseIR();
        }

        private void cargarGrupoCC()
        {
            try
            {
                this.ddlGrupoCC.DataSource = OrdenarEntidad(CentidadMetodos.EntidadGet("cGrupoCCosto", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlGrupoCC.DataValueField = "codigo";
                this.ddlGrupoCC.DataTextField = "descripcion";
                this.ddlGrupoCC.DataBind();
                this.ddlGrupoCC.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        private void cargarClaseIR()
        {
            try
            {
                this.ddlCalseIR.DataSource = OrdenarEntidad(CentidadMetodos.EntidadGet("cConceptoIR", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlCalseIR.DataValueField = "codigo";
                this.ddlCalseIR.DataTextField = "descripcion";
                this.ddlCalseIR.DataBind();
                this.ddlCalseIR.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text, Convert.ToInt16(this.Session["empresa"]) };

            if (CentidadMetodos.EntidadGetKey("cPuc", "ppa", objKey).Tables[0].Rows.Count > 0)
            {
                this.nilblInformacion.Visible = true;
                CerroresGeneral.ManejoError(this, GetType(), "La cuenta " + this.txtCodigo.Text + " ya se encuentra registrada", "warning");
                InhabilitarControles(this.formContainer.Controls);
                InhabilitarControles(this.formImpuesto.Controls);
                this.nilbNuevo.Visible = true;
                this.txtCodigo.Text = "";
            }
        }

        private void Guardar()
        {
            string operacion = "inserta";

            try
            {
                if (Convert.ToBoolean(this.ViewState["editar"]) == true)
                    operacion = "actualiza";

                switch (cuentas.RegistraPuc(
                        chkActio.Checked,
                        chkAuxiliar.Checked,
                       0,
                        ddlCalseIR.SelectedValue,
                          this.txtCodigo.Text.Trim(),
                        this.txtNombre.Text,
                        chkDisponible.Checked,
                        Convert.ToInt16(this.Session["empresa"]),
                        ddlGrupoCC.SelectedValue,
                        chkImpuesto.Checked,
                        chkCcosto.Checked,
                        chkTercero.Checked,
                        ddlNaturaleza.SelectedValue,
                        Convert.ToDecimal(txtNivel.Text),
                        txtNotas.Text,
                        ddlPlanCuenta.SelectedValue,
                        txtRaiz.Text,
                        ddlTipoSaldo.SelectedValue,
                        0,
                        ddlTipoCuenta.SelectedValue,
                        ddlTipoDisponible.SelectedValue,
                        ddlTipoIR.SelectedValue,
                        ddlTipoManejaTercero.SelectedValue,
                        operacion
                        ))
                {
                    case 0:
                        ManejoExito("Registro creado satisfactoriamente", operacion.Substring(0, 1).ToUpper());
                        break;
                    case 1:
                        ManejoError("Error al crear el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                        break;
                    case 2:
                        ManejoError("La raíz de la cuenta no existe. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                        break;
                    case 3:
                        ManejoError("No se puede crear una cuenta auxiliar de una cuenta auxiliar. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                        break;
                    case 4:
                        ManejoError("Error no especificado. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                        break;
                    case 5:
                        ManejoError("Campos vacios. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                        break;
                }

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        private void manejoAuxiliar()
        {
            if (chkAuxiliar.Checked)
            {
                chkCcosto.Checked = false;
                chkDisponible.Checked = false;
                chkTercero.Checked = false;
                chkImpuesto.Checked = false;
                chkCcosto.Enabled = true;
                chkTercero.Enabled = true;
                chkImpuesto.Enabled = true;
                chkDisponible.Enabled = true;
            }
            else
            {
                chkCcosto.Checked = false;
                chkDisponible.Checked = false;
                chkTercero.Checked = false;
                chkImpuesto.Checked = false;
                chkCcosto.Enabled = false;
                chkTercero.Enabled = false;
                chkImpuesto.Enabled = false;
                chkDisponible.Enabled = false;
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
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(),
                       Convert.ToInt16(this.Session["empresa"])) != 0)
                    this.nitxtBusqueda.Focus();
                else
                    ManejoErrorAcceso("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            InhabilitarControles(this.formContainer.Controls);
            LimpiarControles(this.formContainer.Controls);
            formImpuesto.Visible = false;
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
        }

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            InhabilitarControles(this.formContainer.Controls);
            this.nilbNuevo.Visible = true;
            formImpuesto.Visible = false;
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
            HabilitarControles(this.formContainer.Controls);
            formImpuesto.Visible = true;
            HabilitarControles(formImpuesto.Controls);
            this.nilbNuevo.Visible = false;
            this.ViewState["editar"] = true;
            this.txtCodigo.Enabled = false;
            this.txtRaiz.Enabled = false;
            this.ddlNaturaleza.Enabled = false;
            this.txtNivel.Enabled = false;
            this.txtNombre.Focus();


            try
            {
                CargarCombos();
                DataView dvContrato = cuentas.DatosCuenta(this.gvLista.SelectedRow.Cells[2].Text, Convert.ToInt16(Session["empresa"]));

                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.txtCodigo.Text = this.gvLista.SelectedRow.Cells[2].Text;
                else
                    this.txtCodigo.Text = "";

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    this.txtRaiz.Text = this.gvLista.SelectedRow.Cells[3].Text;
                else
                    this.txtRaiz.Text = "";

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                    this.txtNombre.Text = this.gvLista.SelectedRow.Cells[4].Text;
                else
                    this.txtNombre.Text = "";

                if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                    this.ddlNaturaleza.SelectedValue = this.gvLista.SelectedRow.Cells[5].Text;

                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                    this.txtNivel.Text = this.gvLista.SelectedRow.Cells[6].Text;
                else
                    this.txtNivel.Text = "";
                if (this.gvLista.SelectedRow.Cells[7].Text != "&nbsp;")
                    this.ddlTipoCuenta.SelectedValue = this.gvLista.SelectedRow.Cells[7].Text;

                foreach (DataRowView registro in dvContrato)
                {
                    if (registro.Row.ItemArray.GetValue(2) != null)
                        this.ddlPlanCuenta.SelectedValue = registro.Row.ItemArray.GetValue(2).ToString();

                    if (registro.Row.ItemArray.GetValue(8) != null)
                        chkCcosto.Checked = Convert.ToBoolean(registro.Row.ItemArray.GetValue(8).ToString());

                    if (registro.Row.ItemArray.GetValue(9) != null)
                        this.ddlGrupoCC.SelectedValue = registro.Row.ItemArray.GetValue(9).ToString();

                    if (registro.Row.ItemArray.GetValue(10) != null)
                        chkDisponible.Checked = Convert.ToBoolean(registro.Row.ItemArray.GetValue(10).ToString());

                    if (registro.Row.ItemArray.GetValue(11) != null)
                        this.ddlTipoDisponible.SelectedValue = registro.Row.ItemArray.GetValue(11).ToString();

                    if (registro.Row.ItemArray.GetValue(12) != null)
                        chkTercero.Checked = Convert.ToBoolean(registro.Row.ItemArray.GetValue(12).ToString());

                    if (registro.Row.ItemArray.GetValue(13) != null)
                        this.ddlTipoManejaTercero.SelectedValue = registro.Row.ItemArray.GetValue(13).ToString();

                    if (registro.Row.ItemArray.GetValue(14) != null)
                        this.ddlTipoSaldo.SelectedValue = registro.Row.ItemArray.GetValue(14).ToString();

                    if (registro.Row.ItemArray.GetValue(15) != null)
                        chkImpuesto.Checked = Convert.ToBoolean(registro.Row.ItemArray.GetValue(15).ToString());

                    if (registro.Row.ItemArray.GetValue(16) != null)
                    {
                        this.ddlTipoIR.SelectedValue = registro.Row.ItemArray.GetValue(16).ToString();
                        manejoCampos();
                    }

                    if (registro.Row.ItemArray.GetValue(17) != null)
                        this.ddlCalseIR.SelectedValue = registro.Row.ItemArray.GetValue(17).ToString();

                    if (registro.Row.ItemArray.GetValue(20) != null)
                        txtNotas.Text = registro.Row.ItemArray.GetValue(20).ToString();
                    else
                        txtNotas.Text = "";

                    if (registro.Row.ItemArray.GetValue(21) != null)
                        chkActio.Checked = Convert.ToBoolean(registro.Row.ItemArray.GetValue(21).ToString());

                    if (registro.Row.ItemArray.GetValue(22) != null)
                        chkAuxiliar.Checked = Convert.ToBoolean(registro.Row.ItemArray.GetValue(22).ToString());
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

            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                 nombrePaginaActual(), eliminar, Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", eliminar);
                    return;
                }

                object[] objValores = new object[] { Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text), (int)this.Session["empresa"] };

                if (CentidadMetodos.EntidadInsertUpdateDelete("cPuc", operacion, "ppa", objValores) == 0)
                    ManejoExito("Registro eliminado satisfactoriamente", "E");
                else
                    ManejoError("Error al eliminar el registro. Operación no realizada", "E");
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
            this.txtNivel.Focus();
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            manejoCampos();
        }

        protected void nilbInforme_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("ListadoPuc.aspx");
        }

        protected void nilbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                 nombrePaginaActual(), insertar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", insertar);
                return;
            }
            HabilitarControles(this.formContainer.Controls);
            LimpiarControles(formContainer.Controls);
            formImpuesto.Visible = true;
            HabilitarControles(formImpuesto.Controls);
            LimpiarControles(formImpuesto.Controls);
            this.nilbNuevo.Visible = false;
            this.ViewState["editar"] = false;
            CargarCombos();
            manejoAuxiliar();
            manejoCampos();
            this.txtCodigo.Focus();
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.DataBind();
            GetEntidad();
        }

        protected void chkDisponible_CheckedChanged(object sender, EventArgs e)
        {
            manejoCampos();
        }

        protected void ddlTipoIR_SelectedIndexChanged(object sender, EventArgs e)
        {
            manejoCampos();
        }

        protected void chkImpuesto_CheckedChanged(object sender, EventArgs e)
        {
            manejoCampos();
        }

        protected void chkAuxiliar_CheckedChanged(object sender, EventArgs e)
        {
            manejoAuxiliar();
            manejoCampos();
        }

        protected void chkCcosto_CheckedChanged(object sender, EventArgs e)
        {
            manejoCampos();
        }

        protected void chkTercero_CheckedChanged(object sender, EventArgs e)
        {
            manejoCampos();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Length == 0 || txtNombre.Text.Length == 0 || ddlPlanCuenta.SelectedValue.Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Campos vacios por favor corrija", "warning");
                return;
            }
            if (chkCcosto.Checked)
            {
                if (ddlGrupoCC.SelectedValue.Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "El campo de grupo de centro de costo esta vacio, por favor corrija", "warning");
                    return;
                }
            }

            if (chkImpuesto.Checked)
            {
                if (ddlCalseIR.SelectedValue.Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "El campo de clase impuesto esta vacio, por favor corrija", "warning");
                    return;
                }
            }
            Guardar();
        }

        #endregion Eventos
    }
}