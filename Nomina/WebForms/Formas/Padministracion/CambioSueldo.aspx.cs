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

namespace Nomina.WebForms.Formas.Padministracion
{
    public partial class CambioSueldo : BasePage
    {

        #region Instancias



        Coperadores operadores = new Coperadores();
        CtipoTransaccion tipoTransaccion = new CtipoTransaccion();
        CtransaccionNovedad transaccionNovedad = new CtransaccionNovedad();
        Cperiodos periodo = new Cperiodos();
        string numerotransaccion = "";
        Ctransacciones transacciones = new Ctransacciones();
        CcambiarSueldo liquidacion = new CcambiarSueldo();
        Cgeneral general = new Cgeneral();
        Cfuncionarios funcionario = new Cfuncionarios();

        #endregion Instancias

        #region 

        private void Preliquidar()
        {
            try
            {
                switch (liquidacion.CambiarSueldoTercero(Convert.ToInt16(Session["empresa"]), ddlccosto.SelectedValue.Trim(), ddlEmpleado.SelectedValue.Trim(), Convert.ToInt32(ddlOpcionLiquidacion.SelectedValue), Convert.ToInt16(ddlTipo.SelectedValue), Convert.ToDecimal(txvProcentaje.Text), Convert.ToDecimal(txvValor.Text), Convert.ToDecimal(txvSueldoAnterior.Text), Convert.ToDecimal(txvSueldoNuevo.Text)))
                {
                    case 0:
                        CerroresGeneral.ManejoError(this, GetType(), "Datos Guardados satisfactoriamente", "info");
                        break;
                    case 1:
                        CerroresGeneral.ManejoError(this, GetType(), "Error al guardar los datos", "error");
                        break;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar empleados. Correspondiente a: " + ex.Message, "C");
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

            lblPorcentaje.Visible = false;
            txvProcentaje.Visible = false;
            txvValor.Visible = false;
            txvSueldoAnterior.Visible = false;
            txvSueldoNuevo.Visible = false;
            lblValor.Visible = false;
            lblSueldoAnterior.Visible = false;
            lblSueldoNuevo.Visible = false;
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
                        manejoOpcionLiquidacion();
                    }
                }
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
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
                if (ddlccosto.SelectedValue.Trim().Length == 0 & ddlccosto.Visible == true)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar un de centro de costo para seguir", "warning");
                    ddlccosto.Focus();
                    return;
                }


                if (ddlEmpleado.SelectedValue.Trim().Length == 0 & ddlEmpleado.Visible == true)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar un de centro de costp para seguir", "warning");
                    ddlEmpleado.Focus();
                    return;
                }

                Preliquidar();
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }
        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Convert.ToInt16(ddlTipo.SelectedValue))
            {
                case 1:
                    lblPorcentaje.Visible = false;
                    txvProcentaje.Visible = false;
                    txvValor.Visible = true;
                    txvSueldoAnterior.Visible = false;
                    txvSueldoNuevo.Visible = false;
                    lblValor.Visible = true;
                    lblSueldoAnterior.Visible = false;
                    lblSueldoNuevo.Visible = false;
                    break;
                case 2:
                    lblPorcentaje.Visible = true;
                    txvProcentaje.Visible = true;
                    txvValor.Visible = false;
                    txvSueldoAnterior.Visible = false;
                    txvSueldoNuevo.Visible = false;
                    lblValor.Visible = false;
                    lblSueldoAnterior.Visible = false;
                    lblSueldoNuevo.Visible = false;
                    break;
                case 3:
                    lblPorcentaje.Visible = false;
                    txvProcentaje.Visible = false;
                    txvValor.Visible = false;
                    txvSueldoAnterior.Visible = true;
                    txvSueldoNuevo.Visible = true;
                    lblValor.Visible = false;
                    lblSueldoAnterior.Visible = true;
                    lblSueldoNuevo.Visible = true;
                    break;
            }

        }

        #endregion Eventos
    }
}