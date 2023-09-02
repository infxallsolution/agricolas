using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
using Nomina.WebForms.App_Code.Liquidacion;
using Nomina.WebForms.App_Code.Transaccion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Pliquidacion
{
    public partial class Preliquidar : BasePage
    {

        #region Instancias

        Coperadores operadores = new Coperadores();
        CtipoTransaccion tipoTransaccion = new CtipoTransaccion();
        CtransaccionNovedad transaccionNovedad = new CtransaccionNovedad();
        Cperiodos periodo = new Cperiodos();
        string numerotransaccion = "";
        Ctransacciones transacciones = new Ctransacciones();
        CliquidacionNomina liquidacion = new CliquidacionNomina();
        Cgeneral general = new Cgeneral();
        Cfuncionarios funcionario = new Cfuncionarios();

        #endregion Instancias

        #region Metodos

        private void Preliquidacion()
        {
            string script = "", nombreTercero = "";
            int retorno = 0;
            liquidacion.LiquidacionNomina(Convert.ToInt32(ddlAño.SelectedValue.Trim()), Convert.ToInt32(ddlPeriodo.SelectedValue.Trim()), Convert.ToInt16(Session["empresa"]), ddlccosto.SelectedValue.Trim(), ddlEmpleado.SelectedValue.Trim(), Convert.ToInt32(Convert.ToDateTime(txtFecha.Text).Month), Convert.ToDateTime(txtFecha.Text), Convert.ToInt32(ddlOpcionLiquidacion.SelectedValue), out retorno, out nombreTercero);
            switch (retorno)
            {
                case 1:
                    MostrarMensaje("Periodo no existe o cerrado");
                    break;

                case 2:
                    MostrarMensaje("No existen conceptos fijos parametrizados");
                    break;
                case 3:
                    MostrarMensaje("No existen parametros generales de nomina para esta empresa");
                    break;

                case 4:
                    MostrarMensaje("Existen centros de costo no parametrizados para este tipo de liquidación ");

                    break;

                case 20:
                    script = "<script language='javascript'>Visualizacion('Preliquidacion');</script>";
                    Page.RegisterStartupScript("Visualizacion", script);
                    break;
                case 55:
                    MostrarMensaje("El etrabajado " + nombreTercero + " se le vencio el contrato, por favor ingrese una prorroga para su liquidación ");
                    break;
            }

            if (retorno >= 1 & retorno <= 4)
            {
                switch (liquidacion.DeleteTmpLiquidacion(Convert.ToInt16(Session["empresa"])))
                {
                    case 1:
                        MostrarMensaje("Error al eliminar datos temporales de la liquidacion");
                        break;
                }
            }
        }
        private void CargarEmpleados()
        {
            try
            {
                DataView dvTerceroCCosto = funcionario.RetornaFuncionarioCcosto(ddlccosto.SelectedValue.ToString(), Convert.ToInt16(Session["empresa"]));
                this.ddlEmpleado.DataSource = dvTerceroCCosto;
                this.ddlEmpleado.DataValueField = "tercero";
                this.ddlEmpleado.DataTextField = "descripcion";
                this.ddlEmpleado.DataBind();
                this.ddlEmpleado.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar empleados. Correspondiente a: " + ex.Message, "C");
            }
        }
        private void manejoLiquidacion()
        {
            if (ddlPeriodo.SelectedValue.Trim().Length > 0 & ddlAño.SelectedValue.Trim().Length > 0)
            {
                lbPreLiquidar.Visible = true;
            }
            else
            {
                lbPreLiquidar.Visible = false;
            }
        }
        protected void cargarPeriodos()
        {
            try
            {
                this.ddlPeriodo.DataSource = periodo.PeriodosAbiertoNominaAño(Convert.ToInt32(ddlAño.SelectedValue.Trim()), Convert.ToInt16(Session["empresa"])).Tables[0].DefaultView;
                this.ddlPeriodo.DataValueField = "noPeriodo";
                this.ddlPeriodo.DataTextField = "descripcion";
                this.ddlPeriodo.DataBind();
                this.ddlPeriodo.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar periodo inicial. Correspondiente a: " + ex.Message, "C");
            }
        }
        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");

            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);

            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                  "ex", mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));

        }
        private void CargarCombos()
        {

            try
            {
                this.ddlAño.DataSource = periodo.PeriodoAñoAbiertoNomina(Convert.ToInt16(Session["empresa"]));
                this.ddlAño.DataValueField = "año";
                this.ddlAño.DataTextField = "año";
                this.ddlAño.DataBind();
                this.ddlAño.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar año. Correspondiente a: " + ex.Message, "C");
            }
        }
        private void manejoOpcionLiquidacion()
        {
            switch (Convert.ToInt16(ddlOpcionLiquidacion.SelectedValue))
            {
                case 1:
                    lblCcosto.Visible = false;
                    lblEmpleado.Visible = false;
                    ddlccosto.Visible = false;
                    ddlEmpleado.Visible = false;
                    break;

                case 2:
                    cargarCentroCosto(true);
                    lblCcosto.Text = "Centro costo";
                    ddlccosto.Visible = true;
                    ddlccosto.Enabled = true;
                    ddlEmpleado.Visible = false;
                    lblCcosto.Visible = true;
                    lblEmpleado.Visible = false;
                    ddlccosto.SelectedValue = "";
                    break;
                case 3:
                    cargarCentroCosto(true);
                    lblCcosto.Text = "Centro costo";
                    ddlccosto.Visible = true;
                    ddlEmpleado.Visible = true;
                    ddlccosto.SelectedValue = "";
                    ddlEmpleado.SelectedValue = "";
                    ddlccosto.Enabled = true;
                    ddlEmpleado.Enabled = true;
                    lblCcosto.Visible = true;
                    lblEmpleado.Visible = true;
                    break;
                case 4:
                    cargarCentroCosto(false);
                    lblCcosto.Text = "Mayor centro costo";
                    ddlccosto.Visible = true;
                    ddlEmpleado.Visible = false;
                    ddlccosto.SelectedValue = "";
                    ddlEmpleado.SelectedValue = "";
                    ddlccosto.Enabled = true;
                    ddlEmpleado.Enabled = false;
                    lblCcosto.Visible = true;
                    lblEmpleado.Visible = false;
                    break;
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
                {

                    if (!IsPostBack)
                    {
                        CcontrolesUsuario.HabilitarControles(this.Page.Controls);
                        CcontrolesUsuario.LimpiarControles(Page.Controls);
                        CargarCombos();
                        this.Session["editar"] = null;
                        this.txtFecha.Focus();
                        manejoOpcionLiquidacion();
                    }
                }
                else
                {
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
                }
            }
        }
        private void cargarCentroCosto(bool auxiliar)
        {
            try
            {

                this.ddlccosto.DataSource = general.CentroCosto(auxiliar, Convert.ToInt16(Session["empresa"]));
                this.ddlccosto.DataValueField = "codigo";
                this.ddlccosto.DataTextField = "descripcion";
                this.ddlccosto.DataBind();
                this.ddlccosto.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los centros de costo. Correspondiente a: " + ex.Message, "C");
            }
        }
        protected void txtFecha_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFecha.Text);
            }
            catch
            {
                MostrarMensaje("Formato de fecha no valido");
                txtFecha.Text = "";
                txtFecha.Focus();
                return;
            }

            if (periodo.RetornaPeriodoCerradoNomina(Convert.ToDateTime(txtFecha.Text).Year,
                 Convert.ToDateTime(txtFecha.Text).Month, Convert.ToInt16(Session["empresa"]), Convert.ToDateTime(txtFecha.Text)) == 1)
            {
                ManejoError("Periodo cerrado de nomina. No es posible realizar s", "I");
                return;
            }
        }
        protected void ddlAño_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddlAño.SelectedValue.Trim().Length > 0)
            {
                cargarPeriodos();
                manejoLiquidacion();
            }
        }
        protected void ddlPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            manejoLiquidacion();
        }
        protected void ddlOpcionLiquidacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            manejoOpcionLiquidacion();
        }
        protected void ddlccosto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt16(ddlOpcionLiquidacion.SelectedValue.Trim()) == 3)
                CargarEmpleados();
            else
            {
                ddlEmpleado.DataSource = null;
                ddlEmpleado.DataBind();
                ddlEmpleado.Visible = false;
            }
        }
        protected void lbPreLiquidar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFecha.Text.Trim().Length == 0)
                {
                    MostrarMensaje("Debe seleccionar una fecha para guardar liquidación");
                    txtFecha.Focus();
                    return;
                }

                if (ddlAño.SelectedValue.Trim().Length == 0)
                {
                    MostrarMensaje("Debe seleccionar un año para guardar liquidación");
                    ddlAño.Focus();
                    return;
                }

                if (ddlAño.SelectedValue.Trim().Length == 0)
                {
                    MostrarMensaje("Debe seleccionar una forma de liquidación para seguir");
                    txtFecha.Focus();
                    return;
                }

                if (ddlccosto.SelectedValue.Trim().Length == 0 & ddlccosto.Visible == true)
                {
                    MostrarMensaje("Debe seleccionar un de centro de costp para seguir");
                    ddlccosto.Focus();
                    return;
                }


                if (ddlEmpleado.SelectedValue.Trim().Length == 0 & ddlEmpleado.Visible == true)
                {
                    MostrarMensaje("Debe seleccionar un de centro de costp para seguir");
                    ddlEmpleado.Focus();
                    return;
                }

                Preliquidacion();
            }
            catch (Exception ex)
            {
                ManejoError("Error al liquidar el documento debido a :" + ex.Message, "I");
            }
        }

        #endregion Eventos


    }
}