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
    public partial class LiquidacionPrimas : BasePage
    {



        #region Instancias

        public List<LiquidacionPrima> ListadoDetallePrimas
        {
            get
            {
                object o = ViewState["ListadoDetallePrimas"];
                return (o == null) ? null : (List<LiquidacionPrima>)o;
            }
            set
            {
                ViewState["ListadoDetallePrimas"] = value;
            }
        }
        Coperadores operadores = new Coperadores();
        CtipoTransaccion tipoTransaccion = new CtipoTransaccion();
        CtransaccionNovedad transaccionNovedad = new CtransaccionNovedad();
        Cperiodos periodo = new Cperiodos();
        string numerotransaccion = "";
        Ctransacciones transacciones = new Ctransacciones();
        CliquidacionNomina liquidacion = new CliquidacionNomina();
        Cgeneral general = new Cgeneral();
        Cfuncionarios funcionario = new Cfuncionarios();
        CModificacionPrimas modificacionPrima = new CModificacionPrimas();

        #endregion Instancias

        #region Metodos

        private void CargarCabeceraPrima(string tipo, string numero)
        {

            try
            {
                var dr = modificacionPrima.CargarCabeceraPrima(Session["empresa"].ToString(), tipo, numero);
                if (dr == null)
                    return;
                this.txtTipo.Text = dr["tipo"].ToString();
                this.txtNumero.Text = dr["numero"].ToString();
                this.txtPeriodoPago.Text = dr["periodo"].ToString();
                this.txtAñoPago.Text = dr["año"].ToString();
                this.txtFechaDetalle.Text = (!(dr["fecha"] is DateTime) ? "N/A" : ((DateTime)dr["fecha"]).ToString("yyyy/MM/dd"));
                this.txtObservacionEdita.Text = dr["observacion"].ToString();
                this.txtAñoDesde.Text = dr["añoInicial"].ToString();
                this.txtAñoHasta.Text = dr["añoFinal"].ToString();
                this.txtPeriodoDesde.Text = dr["periodoInicial"].ToString();
                this.txtPeriodoHasta.Text = dr["periodoFinal"].ToString();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los detalles de la liquidación." + ex.ToString(), "IN");
            }
        }
        private void Preliquidar()
        {
            string script = "", nombreTercero = "", numeroTransaccion = "";
            int retorno = 0;
            liquidacion.LiquidacionPrimas(Convert.ToInt32(ddlAñoDesde.SelectedValue.Trim()), Convert.ToInt32(ddlAñoHasta.SelectedValue.Trim()), Convert.ToInt32(ddlPeriodoDesde.SelectedValue.Trim()), Convert.ToInt32(ddlPeriodoHasta.SelectedValue.Trim()), Convert.ToInt16(Session["empresa"]),
                ddlccosto.SelectedValue.Trim(), ddlEmpleado.SelectedValue.Trim(), Convert.ToDateTime(txtFecha.Text), Convert.ToInt32(ddlOpcionLiquidacion.SelectedValue), out retorno);
            switch (retorno)
            {
                case 1:
                    MostrarMensaje("Periodo no existe o cerrado");
                    break;

                case 2:
                    MostrarMensaje("NO existen conceptos fijos parametrizado");
                    break;

                case 3:
                    MostrarMensaje("No existen parametros generales de nomina para esta empresa");
                    break;

                case 4:
                    MostrarMensaje("Existen centros de costo no parametrizados para este tipo de liquidación");

                    break;

                case 20:
                    script = "<script language='javascript'>Visualizacion('PreLiquidacionPrimas');</script>";
                    Page.RegisterStartupScript("Visualizacion", script);
                    break;
                case 55:
                    MostrarMensaje("El etrabajado " + nombreTercero + " se le vencio el contrato, por favor ingrese una prorroga para su liquidación");
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
            liquidacion.LiquidacionPrimasDefinitiva(Convert.ToInt32(ddlAñoPago.SelectedValue.Trim()), Convert.ToInt32(ddlPeriodoPago.SelectedValue.Trim()), Convert.ToInt32(ddlAñoDesde.SelectedValue.Trim()), Convert.ToInt32(ddlAñoHasta.SelectedValue.Trim()), Convert.ToInt32(ddlPeriodoDesde.SelectedValue.Trim()), Convert.ToInt32(ddlPeriodoHasta.SelectedValue.Trim()), Convert.ToInt16(Session["empresa"]),
                ddlccosto.SelectedValue.Trim(), ddlEmpleado.SelectedValue.Trim(), Convert.ToDateTime(txtFecha.Text), Convert.ToInt32(ddlOpcionLiquidacion.SelectedValue),
                ConfigurationManager.AppSettings["TipoTransaccionPrima"].ToString(), Session["usuario"].ToString(), txtObservacion.Text, out retorno, out numeroTransaccion);
            switch (retorno)
            {
                case 5:
                    MostrarMensaje("El periodo ya tiene primas liquidadas, por favor corrija");
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
                    ManejoExito("Liquidación de primas realizadas satisfactoriamente.", "I");
                    script = "<script language='javascript'>VisualizacionLiquidacion('InformeLiquidacionPrimas'," + Convert.ToString(ddlAñoPago.SelectedValue.Trim()) + "," + Convert.ToString(ddlPeriodoPago.SelectedValue.Trim()) + ",'" + numeroTransaccion + "');</script>";
                    Page.RegisterStartupScript("VisualizacionLiquidacion", script);
                    break;
                case 55:
                    MostrarMensaje("El etrabajado " + nombreTercero + " se lInformeLiquidacionPrimae vencio el contrato, por favor ingrese una prorroga para su liquidación ");
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

                this.gvLista.DataSource = liquidacion.BuscarEntidadPrima(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();


                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

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
                this.ddlAñoDesde.DataSource = periodo.PeriodoAñoAbiertoNomina(Convert.ToInt16(Session["empresa"]));
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
                this.ddlAñoHasta.DataSource = periodo.PeriodoAñoAbiertoNomina(Convert.ToInt16(Session["empresa"]));
                this.ddlAñoHasta.DataValueField = "año";
                this.ddlAñoHasta.DataTextField = "año";
                this.ddlAñoHasta.DataBind();
                this.ddlAñoHasta.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar año. Correspondiente a: " + ex.Message, "C");
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

                    int mes = liquidacion.RetornaMesPeriodoNomina(Convert.ToInt32(ddlAñoDesde.SelectedValue.Trim()), Convert.ToInt32(ddlPeriodoDesde.SelectedValue.Trim()), Convert.ToInt16(Session["empresa"]));

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
                                         Convert.ToInt32(ddlPeriodoDesde.SelectedValue.Trim()),  //@noPeriodo	int
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
        private void CargarDetallePrima(string tipo, string numero)
        {

            try
            {
                var ds = modificacionPrima.CargarDetallePrima(Convert.ToString(Session["empresa"]), tipo, numero);
                ListadoDetallePrimas = new List<LiquidacionPrima>();
                gvDetalleLiquidacion.DataSource = null;
                gvDetalleLiquidacion.DataBind();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var item = new LiquidacionPrima();
                    item.CodigoTercero = dr["codigoTercero"].ToString();
                    item.IdentificacionTercero = dr["identificacionTercero"].ToString();
                    item.NombreTercero = dr["nombreTercero"].ToString();
                    item.FechaIngreso = (!(dr["fechaIngreso"] is DateTime) ? "N/A" : ((DateTime)dr["fechaIngreso"]).ToString("yyyy/MM/dd"));
                    item.FechaInicial = (!(dr["fechaInicial"] is DateTime) ? "N/A" : ((DateTime)dr["fechaInicial"]).ToString("yyyy/MM/dd"));
                    item.FechaFinal = (!(dr["fechaFinal"] is DateTime) ? "N/A" : ((DateTime)dr["fechaFinal"]).ToString("yyyy/MM/dd"));
                    item.Basico = (!(dr["basico"] is int) ? "0" : ((int)dr["basico"]).ToString("#,#"));
                    item.Transporte = !(dr["transporte"] is int) ? "0" : ((int)dr["transporte"]).ToString("#,#0");
                    item.ValorPromedio = (!(dr["valorPromedio"] is int) ? "0" : ((int)dr["valorPromedio"]).ToString("#,#0"));
                    item.Base = (!(dr["base"] is int) ? "0" : ((int)dr["base"]).ToString("#,#0"));
                    item.DiasPromedio = dr["diasPromedio"].ToString();
                    item.DiasPrimas = dr["diasPrimas"].ToString();
                    item.ValorPrima = (!(dr["valorPrima"] is int) ? "0" : ((int)dr["valorPrima"]).ToString("#,#0"));
                    item.Contrato = !(dr["contrato"] is int) ? 0 : (int)dr["contrato"];
                    ListadoDetallePrimas.Add(item);
                }
                gvDetalleLiquidacion.DataSource = ListadoDetallePrimas;
                gvDetalleLiquidacion.DataBind();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los detalles de la liquidación." + ex.ToString(), "IN");
            }
        }
        private void GuardarDatos(string tipo, string numero)
        {

            bool validar = false;

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    object[] objValores = new object[] { Convert.ToInt16(this.Session["empresa"]), numero, tipo };
                    switch (CentidadMetodos.EntidadInsertUpdateDelete("nliquidacionprimadetalle", "elimina", "ppa", objValores))
                    {
                        case 0:
                            foreach (GridViewRow item in gvDetalleLiquidacion.Rows)
                            {
                                decimal valorPrima = Convert.ToDecimal(((HiddenField)item.FindControl("hfValorPrima")).Value);

                                object[] objValoresDetalle = new object[] {
                                    Convert.ToInt32(txtAñoHasta.Text),//añoFinal
                                    Convert.ToInt32(txtAñoDesde.Text),
                                    Convert.ToDecimal(((HiddenField)item.FindControl("hfBase")).Value),
                                    Convert.ToDecimal(((TextBox)item.FindControl("txvBasico")).Text),
                                    Convert.ToDecimal(item.Cells[14].Text),
                                    Convert.ToDecimal(((TextBox)item.FindControl("txvDiasPrima")).Text),
                                    Convert.ToDecimal(((TextBox)item.FindControl("txvDiasPromedio")).Text),
                                    Convert.ToInt16(this.Session["empresa"]),
                                    Convert.ToDateTime(item.Cells[6].Text).ToString("yyyy/MM/dd"),
                                    Convert.ToDateTime(item.Cells[4].Text).ToString("yyyy/MM/dd"),
                                    Convert.ToDateTime(item.Cells[5].Text).ToString("yyyy/MM/dd"),
                                    numero,
                                    Convert.ToInt32(txtPeriodoDesde.Text),
                                    Convert.ToInt32(txtPeriodoHasta.Text),
                                    Convert.ToInt32(item.Cells[1].Text),
                                    tipo,
                                    valorPrima,
                                    Convert.ToDecimal(((TextBox)item.FindControl("txvValorPromedio")).Text),
                                    Convert.ToDecimal(((TextBox)item.FindControl("txvTransporte")).Text)
                            };

                                switch (CentidadMetodos.EntidadInsertUpdateDelete("nliquidacionprimadetalle", "inserta", "ppa", objValoresDetalle))
                                {
                                    case 1:
                                        validar = true;
                                        break;
                                }
                            }
                            break;
                        case 1:
                            validar = true;
                            break;
                    }
                    if (validar == true)
                    {
                        ManejoError("Errores al insertar el registro. Operación no realizada", "I");
                        return;
                    }

                    ManejoError(this, GetType(), "Cambios guardados exitosamente", "info");
                    ts.Complete();
                }
                CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
                this.nilbNuevo.Visible = true;
                GetEntidad();
            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar los datos correspondiente a: " + ex.Message, "I");
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
        private void CargarTerceros()
        {
            string empresa = (Session["empresa"] ?? "").ToString();
            try
            {
                if (ListadoDetallePrimas.Count == 0)
                    return;
                var ids = ListadoDetallePrimas.Select(item => item.CodigoTercero).Aggregate((s1, s2) => s1 + ", " + s2);
                var ds = modificacionPrima.CargarTerceros(empresa);
                var dv = ds.Tables[0].AsDataView();
                dv.RowFilter = "id not in (" + ids + ")";
                ddlTercero.DataSource = dv;
                ddlTercero.DataValueField = "id";
                ddlTercero.DataTextField = "descripcion";

                ddlTercero.DataBind();
                ddlTercero.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los terceros." + ex.ToString(), "IN");
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
                 ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) == 0)
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
            this.nilblInformacion.Text = "";
            this.txtFecha.Focus();
            ddlAñoPago.Visible = false;
            ddlPeriodoPago.Visible = false;
            lblañoPago.Visible = false;
            lblPeriodoPago.Visible = false;
            manejoOpcionLiquidacion();
        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            tModifica.Visible = false;
            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
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

            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            tModifica.Visible = false;
            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
            this.Session["editar"] = null;
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
            if (ddlAñoDesde.SelectedValue.Trim().Length > 0)
            {
                try
                {
                    this.ddlPeriodoDesde.DataSource = periodo.PeriodoNominaAño(Convert.ToInt32(ddlAñoDesde.SelectedValue.Trim()), Convert.ToInt16(Session["empresa"])).Tables[0].DefaultView;
                    this.ddlPeriodoDesde.DataValueField = "noPeriodo";
                    this.ddlPeriodoDesde.DataTextField = "descripcion";
                    this.ddlPeriodoDesde.DataBind();
                    this.ddlPeriodoDesde.Items.Insert(0, new ListItem("", ""));
                }
                catch (Exception ex)
                {
                    ManejoError("Error al cargar periodo inicial. Correspondiente a: " + ex.Message, "C");
                }

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

                if (chkPagaNomina.Checked == false)
                {
                    MostrarMensaje("Debe chequear un periodo de pago");
                    return;
                }

                if (ddlAñoPago.SelectedValue.Trim().Length == 0)
                {
                    MostrarMensaje("Debe seleccionar un año y periodo de pago");
                    return;
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
            bool anulado = false;

            try
            {
                foreach (Control objControl in gvLista.Rows[e.RowIndex].Cells[8].Controls)
                {
                    anulado = ((CheckBox)objControl).Checked;
                }

                if (anulado == true)
                {
                    this.MostrarMensaje("Transacción ejecutada / anulada no es posible su edición");
                    return;
                }

                switch (transacciones.AnulaLiquidacionPrima(
                        this.gvLista.Rows[e.RowIndex].Cells[2].Text,
                        this.gvLista.Rows[e.RowIndex].Cells[3].Text,
                        this.Session["usuario"].ToString().Trim(),
                        Convert.ToInt16(Session["empresa"])))
                {
                    case 0:
                        MostrarMensaje("Registro Anulado satisfactoriamente");
                        CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
                        this.nilbNuevo.Visible = true;
                        GetEntidad();
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
        protected void ddlAñoHasta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAñoHasta.SelectedValue.Trim().Length > 0)
            {
                try
                {
                    this.ddlPeriodoHasta.DataSource = periodo.PeriodoNominaAño(Convert.ToInt32(ddlAñoHasta.SelectedValue.Trim()), Convert.ToInt16(Session["empresa"])).Tables[0].DefaultView;
                    this.ddlPeriodoHasta.DataValueField = "noPeriodo";
                    this.ddlPeriodoHasta.DataTextField = "descripcion";
                    this.ddlPeriodoHasta.DataBind();
                    this.ddlPeriodoHasta.Items.Insert(0, new ListItem("", ""));
                }
                catch (Exception ex)
                {
                    ManejoError("Error al cargar periodo hasta. Correspondiente a: " + ex.Message, "C");
                }
            }
        }
        protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "A", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "A");
                    return;
                }

                var row = gvLista.Rows[e.NewEditIndex];
                string tipo = row.Cells[2].Text;
                string numero = row.Cells[3].Text;
                tModifica.Visible = true;
                Session["tipo"] = tipo;
                Session["numero"] = numero;
                lbCancelar.Visible = true;
                CargarCabeceraPrima(tipo, numero);
                CargarDetallePrima(tipo, numero);
                CargarTerceros();
                nilbNuevo.Visible = false;

            }
            catch (Exception ex)
            {
                ManejoError("Error al ir a editar el registro. Correspondiente a: " + ex.ToString(), "A");
            }
        }
        protected void btnCargar_Click(object sender, EventArgs e)
        {
            try
            {
                bool valid = true;
                if (ddlTercero.SelectedValue == "")
                {
                    MostrarMensaje("Debe seleccionar un tercero");
                    valid = false;
                }
                if (txtFechaFin.Text.Trim() == "")
                {
                    MostrarMensaje("Debe seleccionar una fecha de fin");
                    valid = false;
                }
                if (txtFechaIngreso.Text.Trim() == "")
                {
                    MostrarMensaje("Debe seleccionar una fecha de ingreso");
                    valid = false;
                }
                if (txtFechaInicio.Text.Trim() == "")
                {
                    MostrarMensaje("Debe seleccionar una fecha de inicio");
                    valid = false;
                }
                if (!valid)
                    return;

                var dr = modificacionPrima.CargarInformacionTercero(Session["empresa"].ToString(), ddlTercero.SelectedValue);
                var item = new LiquidacionPrima()
                {
                    CodigoTercero = !(dr["id"] is int) ? string.Empty : ((int)dr["id"]).ToString(),
                    IdentificacionTercero = !(dr["identificacion"] is string) ? string.Empty : (string)dr["identificacion"],
                    NombreTercero = !(dr["descripcion"] is string) ? string.Empty : (string)dr["descripcion"],
                    Contrato = !(dr["contrato"] is int) ? int.MinValue : (int)dr["contrato"],
                    FechaFinal = Convert.ToDateTime(txtFechaFin.Text).ToString("yyyy/MM/dd"),
                    FechaInicial = Convert.ToDateTime(txtFechaInicio.Text).ToString("yyyy/MM/dd"),
                    FechaIngreso = Convert.ToDateTime(txtFechaIngreso.Text).ToString("yyyy/MM/dd"),
                    Base = "0",
                    Basico = "0",
                    DiasPrimas = "0",
                    DiasPromedio = "0",
                    Transporte = "0",
                    ValorPrima = "0",
                    ValorPromedio = "0"
                };
                ListadoDetallePrimas.Add(item);
                ListadoDetallePrimas = ListadoDetallePrimas.OrderBy(_i => Convert.ToInt32(_i.CodigoTercero)).ToList();

                gvDetalleLiquidacion.DataSource = ListadoDetallePrimas;
                gvDetalleLiquidacion.DataBind();

                CargarTerceros();

                ddlTercero.SelectedValue = "";
                txtFechaInicio.Text = "";
                txtFechaFin.Text = "";
                txtFechaIngreso.Text = "";
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los detalles de la liquidación." + ex.ToString(), "IN");
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarDatos(Convert.ToString(Session["tipo"]), Convert.ToString(Session["numero"]));
        }
        protected void gvDetalleLiquidacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var dr = gvDetalleLiquidacion.Rows[e.RowIndex];
            var item = ListadoDetallePrimas.Where(i => i.CodigoTercero == Server.HtmlDecode(dr.Cells[1].Text).Trim() && i.IdentificacionTercero == Server.HtmlDecode(dr.Cells[2].Text).Trim()).First();
            ListadoDetallePrimas.Remove(item);
            gvDetalleLiquidacion.DataSource = ListadoDetallePrimas;
            gvDetalleLiquidacion.DataBind();
            CargarTerceros();
        }
        protected void txtFechaIngreso_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFechaIngreso.Text);
            }
            catch
            {
                MostrarMensaje("Formato de fecha no valido");
                txtFechaIngreso.Text = "";
                txtFechaIngreso.Focus();
            }
        }
        protected void txtFechaInicio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFechaInicio.Text);
            }
            catch
            {
                MostrarMensaje("Formato de fecha no valido");
                txtFechaInicio.Text = "";
                txtFechaInicio.Focus();
            }
        }
        protected void txtFechaFin_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFechaFin.Text);
            }
            catch
            {
                MostrarMensaje("Formato de fecha no valido");
                txtFechaFin.Text = "";
                txtFechaFin.Focus();
            }
        }

        #endregion Eventos


    }
}