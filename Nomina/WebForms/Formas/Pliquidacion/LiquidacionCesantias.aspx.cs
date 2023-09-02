using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
using Nomina.WebForms.App_Code.Liquidacion;
using Nomina.WebForms.App_Code.Transaccion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Pliquidacion
{
    public partial class LiquidacionCesantias : BasePage
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
        private void Preliquidar()
        {
            string script = "", nombreTercero = "";
            int retorno = 0;
            liquidacion.LiquidacionCesantias(Convert.ToInt32(ddlAñoDesde.SelectedValue.Trim()), Convert.ToInt16(Session["empresa"]),
                ddlccosto.SelectedValue.Trim(), ddlEmpleado.SelectedValue.Trim(), Convert.ToDateTime(txtFecha.Text), Convert.ToInt32(ddlOpcionLiquidacion.SelectedValue), chkSueldoActual.Checked, out retorno);
            switch (retorno)
            {
                case 1:
                    MostrarMensaje("Periodo no existe o cerrado");
                    break;

                case 2:
                    MostrarMensaje("NO existen conceptos fijos parametrizados");
                    break;

                case 3:
                    MostrarMensaje("No existen parametros generales de nomina para esta empresa");
                    break;

                case 4:
                    MostrarMensaje("Existen centros de costo no parametrizados para este tipo de liquidación ");
                    break;
                case 20:
                    script = "<script language='javascript'>Visualizacion('PreLiquidacionCesantias');</script>";
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

                    case 0:
                        MostrarMensaje("Datos temporales de la liquidacion borrados");

                        break;

                    case 1:
                        MostrarMensaje("Error al eliminar datos temporales de la liquidacion");
                        break;
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
        private void Liquidar()
        {
            string script = "", nombreTercero = "", numeroTransaccion = "";
            int retorno = 0;
            liquidacion.LiquidacionCesantiasDefinitiva(Convert.ToInt32(ddlAñoPago.SelectedValue.Trim()), Convert.ToInt32(ddlPeriodoPago.SelectedValue.Trim()), Convert.ToInt32(ddlAñoDesde.SelectedValue.Trim()), Convert.ToInt16(Session["empresa"]),
                ddlccosto.SelectedValue.Trim(), ddlEmpleado.SelectedValue.Trim(), Convert.ToDateTime(txtFecha.Text), Convert.ToInt32(ddlOpcionLiquidacion.SelectedValue),
                ConfigurationManager.AppSettings["TipoTransaccionCesa"].ToString(), Session["usuario"].ToString(), txtObservacion.Text, chkSueldoActual.Checked, chkPagaNomina.Checked, out retorno, out numeroTransaccion);
            switch (retorno)
            {
                case 1:
                    MostrarMensaje("Periodo no existe o cerrado");
                    break;

                case 2:
                    MostrarMensaje("NO existen conceptos fijos parametrizados");
                    break;

                case 3:
                    MostrarMensaje("No existen parametros generales de nomina para esta empresa");
                    break;

                case 4:
                    MostrarMensaje("Existen centros de costo no parametrizados para este tipo de liquidación ");
                    break;
                case 5:
                    MostrarMensaje("El periodo ya tiene primas liquidadas, por favor corrija");
                    break;

                case 20:
                    ManejoExito("Liquidación de primas realizadas satisfactoriamente.", "I");
                    script = "<script language='javascript'>VisualizacionLiquidacion('LiquidacionCesantias','" + Convert.ToString(ddlAñoDesde.SelectedValue.Trim()) + "','" + ddlPeriodoPago.SelectedValue + "','" + numeroTransaccion + "');</script>";
                    Page.RegisterStartupScript("VisualizacionLiquidacion", script);
                    break;
                case 55:
                    MostrarMensaje("El etrabajado " + nombreTercero + " se le vencio el contrato, por favor ingrese una prorroga para su liquidación ");
                    break;
            }

            if (retorno >= 1 & retorno <= 4)
            {
                switch (liquidacion.DeleteTmpLiquidacion(Convert.ToInt16(Session["empresa"])))
                {

                    case 0:
                        MostrarMensaje("Datos temporales de la liquidacion borrados");

                        break;

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
                DataView dvTerceroCCosto = funcionario.RetornaFuncionarioCcosto(ddlccosto.SelectedValue, Convert.ToInt16(Session["empresa"]));
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
        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "C", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                this.gvLista.DataSource = liquidacion.BuscarEntidadCesantias(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();


                this.MostrarMensaje(this.gvLista.Rows.Count.ToString() + " Registros encontrados");

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                            this.gvLista.Rows.Count.ToString() + " Registros encontrados", ObtenerIP(), Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente a: " + ex.Message, "C");
            }
        }
        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;

            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
          "ex", mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }
        private void CargarCombos()
        {
            try
            {
                this.ddlAñoDesde.DataSource = periodo.PeriodoAñoCerradoNomina(Convert.ToInt16(Session["empresa"]));
                this.ddlAñoDesde.DataValueField = "año";
                this.ddlAñoDesde.DataTextField = "año";
                this.ddlAñoDesde.DataBind();
                this.ddlAñoDesde.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar año. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlAñoPago.DataSource = periodo.PeriodoAñoAbiertoNomina(Convert.ToInt16(Session["empresa"]));
                this.ddlAñoPago.DataValueField = "año";
                this.ddlAñoPago.DataTextField = "año";
                this.ddlAñoPago.DataBind();
                this.ddlAñoPago.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar año de pago. Correspondiente a: " + ex.Message, "C");
            }

        }
        private void Guardar()
        {
            string operacion = "inserta";
            bool verificaEncabezado = false;
            bool verificaDetalle = false;
            bool verificaBascula = false;

            try
            {

                using (TransactionScope ts = new TransactionScope())
                {
                    DateTime fecha = Convert.ToDateTime(txtFecha.Text);

                    string conceptos = null, empleado = null, ccosto = null;
                    string remision = null;

                    int mes = liquidacion.RetornaMesPeriodoNomina(Convert.ToInt32(ddlAñoDesde.SelectedValue.Trim()), Convert.ToInt32(ddlPeriodoPago.SelectedValue.Trim()), Convert.ToInt16(Session["empresa"]));

                    if (mes == 14)
                    {
                        MostrarMensaje("Error de parametro en el periodo por favor verificar periodos de nomina");
                        return;
                    }

                    numerotransaccion = ConsecutivoTransaccion();

                    this.Session["numerotransaccion"] = numerotransaccion;



                    object[] objValo = new object[]{
                                        false,    //@anulado	bit
                                       Convert.ToInt32(ddlAñoDesde.SelectedValue),     //@año	int
                                        false,    //@cerrado	bit
                                        Convert.ToInt16(Session["empresa"]),    //@empresa	int
                                        0,    //@estado	int
                                        Convert.ToDateTime(txtFecha.Text),  //@fecha	datetime
                                          DateTime.Now,  //@fechaRegistro	datetime
                                          mes,  //@mes	int
                                         numerotransaccion,   //@numero	varchar
                                         txtObservacion.Text,
                                            "",   //@tipo	varchar
                                         this.Session["usuario"].ToString(),   //@usuario	varchar
                                         null   //@usuarioAnulado	bit                                                    
                              };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("nLiquidacionNomina", operacion, "ppa", objValo))
                    {
                        case 0:

                            break;

                        case 1:
                            ManejoError("Error al insertar el detalle de la transaccción", "I");
                            break;
                    }

                    if (verificaEncabezado == false & verificaDetalle == false & verificaBascula == false)
                    {
                        transacciones.ActualizaConsecutivo(ConfigurationManager.AppSettings["TipoTransaccionNomina"].ToString(), Convert.ToInt16(Session["empresa"]));
                        ts.Complete();
                        ManejoExito("Datos registrados satisfactoriamente. Transacción número " + numerotransaccion, "I");
                    }

                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar los datos correspondiente a: " + ex.Message, operacion.Substring(0, 1).ToUpper());
            }
        }
        private string ConsecutivoTransaccion()
        {
            string numero = "";

            try
            {
                numero = transacciones.RetornaNumeroTransaccion(ConfigurationManager.AppSettings["TipoTransaccionNomina"].ToString(), Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al obtener el número de transacción. Correspondiente a: " + ex.Message, "C");
            }

            return numero;
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
                }
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }
        protected void lbNuevo_Click(object sender, EventArgs e)
        {

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);

            CargarCombos();
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = null;
            this.txtFecha.Focus();
            manejoOpcionLiquidacion();
        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            this.Session["editar"] = null;
        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (liquidacion.VerificaTmpLiquidacion(Convert.ToInt16(Session["empresa"])) == 1)
            {

                if (Convert.ToBoolean(Session["editar"]) == false)
                    Guardar();
            }
            else
            {
                MostrarMensaje("Operación no realizada no existen registros de prenomina para registrar");
                return;
            }


        }
        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
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

                if (ddlAñoDesde.SelectedValue.Trim().Length == 0)
                {
                    MostrarMensaje("Debe seleccionar un año para guardar liquidación");
                    ddlAñoDesde.Focus();
                    return;
                }

                if (ddlAñoDesde.SelectedValue.Trim().Length == 0)
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

                Preliquidar();
            }
            catch (Exception ex)
            {
                ManejoError("Error al liquidar el documento debido a :" + ex.Message, "I");
            }
        }
        protected void btnLiquidar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFecha.Text.Trim().Length == 0)
                {
                    MostrarMensaje("Debe seleccionar una fecha para guardar liquidación");
                    txtFecha.Focus();
                    return;
                }

                if (ddlAñoDesde.SelectedValue.Trim().Length == 0)
                {
                    MostrarMensaje("Debe seleccionar un año para guardar liquidación");
                    ddlAñoDesde.Focus();
                    return;
                }

                if (ddlAñoDesde.SelectedValue.Trim().Length == 0)
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


                if (ddlAñoPago.SelectedValue.Trim().Length == 0)
                {
                    MostrarMensaje("Debe seleccionar un año para el pago de cesantias");
                }

                if (ddlPeriodoPago.SelectedValue.Trim().Length == 0)
                {
                    MostrarMensaje("Debe seleccionar un periodo para el pago de cesantias");
                }

                Liquidar();

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
        protected void chkPagaNomina_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPagaNomina.Checked)
            {
                ddlAñoPago.Visible = true;
                ddlPeriodoPago.Visible = true;
                lblañoPago.Visible = true;
                lblPeriodoPago.Visible = true;

                try
                {
                    this.ddlAñoPago.DataSource = periodo.PeriodoAñoAbiertoNomina(Convert.ToInt16(Session["empresa"]));
                    this.ddlAñoPago.DataValueField = "año";
                    this.ddlAñoPago.DataTextField = "año";
                    this.ddlAñoPago.DataBind();
                    this.ddlAñoPago.Items.Insert(0, new ListItem("", ""));
                }
                catch (Exception ex)
                {
                    ManejoError("Error al cargar año de pago. Correspondiente a: " + ex.Message, "C");
                }


            }
            else
            {
                ddlAñoPago.Visible = false;
                ddlPeriodoPago.Visible = false;
                lblañoPago.Visible = false;
                lblPeriodoPago.Visible = false;
            }
        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    if (transacciones.VerificaEdicionBorrado(this.gvLista.Rows[e.RowIndex].Cells[1].Text, this.gvLista.Rows[e.RowIndex].Cells[2].Text, Convert.ToInt16(Session["empresa"])) != 0)
                    {
                        this.MostrarMensaje("Transacción ejecutada / anulada no es posible su edición");
                        return;
                    }

                    switch (transacciones.AnulaLiquidacionCesantias(this.gvLista.Rows[e.RowIndex].Cells[1].Text, this.gvLista.Rows[e.RowIndex].Cells[2].Text, this.Session["usuario"].ToString().Trim(), Convert.ToInt16(Session["empresa"]),
                        Convert.ToInt16(gvLista.Rows[e.RowIndex].Cells[4].Text), Convert.ToInt16(gvLista.Rows[e.RowIndex].Cells[5].Text)))
                    {
                        case 0:

                            GetEntidad();

                            ts.Complete();
                            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
                            this.nilbNuevo.Visible = true;

                            break;
                        case 1:
                            MostrarMensaje("Error al anular la transacción. Operación no realizada");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
                }
            }
        }
        protected void ddlAñoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAñoPago.SelectedValue.Trim().Length > 0)
            {
                try
                {
                    this.ddlPeriodoPago.DataSource = periodo.PeriodosAbiertoNominaAño(Convert.ToInt32(ddlAñoPago.SelectedValue.Trim()), Convert.ToInt16(Session["empresa"])).Tables[0].DefaultView;
                    this.ddlPeriodoPago.DataValueField = "noPeriodo";
                    this.ddlPeriodoPago.DataTextField = "descripcion";
                    this.ddlPeriodoPago.DataBind();
                    this.ddlPeriodoPago.Items.Insert(0, new ListItem("", ""));
                }
                catch (Exception ex)
                {
                    ManejoError("Error al cargar periodo pago. Correspondiente a: " + ex.Message, "C");
                }
            }
        }


        #endregion Eventos
    }
}