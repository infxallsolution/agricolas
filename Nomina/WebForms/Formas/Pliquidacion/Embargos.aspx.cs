using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
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
    public partial class Embargos : BasePage
    {
        #region Instancias


        Ccontratos contratos = new Ccontratos();
        cProrroga prorroga = new cProrroga();
        Cperiodos periodos = new Cperiodos();
        Cembargos embargos = new Cembargos();
        Cperiodos periodo = new Cperiodos();

        #endregion Instancias

        #region Metodos

        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "C", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                this.gvLista.DataSource = embargos.BuscarEntidad(this.nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C",
                          ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
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



        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text.Trim().ToString(), Convert.ToInt16(Session["empresa"]) };

            try
            {
                if (CentidadMetodos.EntidadGetKey("nDepartamento", "ppa", objKey).Tables[0].Rows.Count > 0)
                {

                    MostrarMensaje("Departamento " + this.txtCodigo.Text + " ya se encuentra registrado");
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
            int codigo;

            try
            {
                if (Convert.ToBoolean(this.Session["editar"]) == true)
                {
                    operacion = "actualiza";
                    codigo = Convert.ToInt32(txtCodigo.Text);
                }
                else
                    codigo = Convert.ToInt32(embargos.Consecutivo(Convert.ToInt32(Session["empresa"]), ddlTercero.SelectedValue.Trim()));

                decimal valorCobro = 0, valorSaldo = 0;
                decimal valorCobroPosterior = 0;
                decimal pCobro = 0;
                decimal pCobroPosterior = 0;
                string periodoFinal;

                int mes = periodo.RetornaMesPeriodoNomina(Convert.ToDateTime(txtFecha.Text).Year, Convert.ToInt32(txtAñoInicial.Text), Convert.ToInt32(Session["empresa"]));
                string Noperiodo = txtPeriodoInicial.Text;
                periodoFinal = null;
                if (Convert.ToInt32(txvCuotas.Text) == 0 & Convert.ToInt32(txvCuotasPosteriores.Text) == 0)
                    periodoFinal = null;

                valorCobro = Convert.ToDecimal(txvValorEmbargo.Text);
                pCobro = Convert.ToDecimal(txvValorPorcentaje.Text);
                valorCobroPosterior = Convert.ToDecimal(txvValorEmbargoPosterior.Text);
                pCobroPosterior = Convert.ToDecimal(txvValorPorPosterior.Text);

                if (chkManejaSaldo.Checked)
                    valorSaldo = Convert.ToDecimal(txvSaldo.Text);
                else
                    valorSaldo = 0;

                object[] objValores = new object[]{
                              chkActivo.Checked,  // @activo	bit
                                Convert.ToDateTime(txtFecha.Text).Year ,//año int
                               Convert.ToInt32(txtAñoInicial.Text), //@añiInicial int
                              ddlBanco.SelectedValue.Trim(),  //@banco	varchar
                              chkCobroPosterior.Checked,  //@cobroPosterior	bit
                              codigo,  //@codigo	int
                              Convert.ToInt32(ddlContratos.SelectedValue),
                              txtNumeroCuenta.Text,  //@cuentaBancaria	varchar
                              Convert.ToInt32(txvCuotas.Text), //@cuotas
                           Convert.ToInt32(txvCuotasPosteriores.Text) , //@cuotasCobroPosterior	int
                             ddlTerceroEmbargo.SelectedValue.Trim(),   //@embargante	int
                              ddlTercero.SelectedValue.Trim(),  //@empleado	int
                              Convert.ToInt32(Session["empresa"]),  //@empresa	int
                              ddlEmpresaEmbarga.SelectedValue.Trim(),  //@empresaEmbarga	int
                              Convert.ToDateTime(txtFecha.Text),  //@fecha	datetime
                              DateTime.Now,  //@fechaRegistro	datetime
                              txtFiscal.Text,//@fiscal varchar
                              txtNumeroMandato.Text,  //@mandamiento	varchar
                              chkCuotas.Checked,  //@manejaCuota	bit
                              chkCuotasPosteriores.Checked,  //@manejaCuotaPosterior	bit
                              chkManejaSaldo.Checked,  //@manejaSaldo	bit
                                mes         ,//mes
                              ddlFormaPago.SelectedValue.Trim(),  //@modoPago	varchar
                               pCobroPosterior ,//@pCobroPosterior	decimal
                               periodoFinal, //@peridoFinal	varchar
                                Noperiodo , //@periodoInicial	varchar
                               pCobro, //@porcentaje	money
                               chkSalarioMinimo.Checked,// @chkSalarioMinimo bit
                               valorSaldo, //@saldo	money
                               0, //@saldoCuotas	int
                               ddlTipoEmbargo.SelectedValue, //@tipo	varchar
                               ddlTipoCuenta.SelectedValue.Trim(), //@tipoCuenta	varchar
                               this.Session["usuario"].ToString(), //@usuario	varchar
                               Convert.ToDecimal(txvValorBase.Text), //@valorBase	money
                               valorCobroPosterior, //@valorCobroPosterior	money
                               valorCobro,   //@valorCuotas	money
                                Convert.ToDecimal( txvValorFinalEmbargo.Text), //@valorFinal	money
                                Convert.ToDecimal(txvValorFinalPos.Text)//@valorFinalPosterior	money
                                        };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("nEmbargos", operacion, "ppa", objValores))
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

        private void CargarCombos()
        {


            try
            {
                DataView tercero = CcontrolesUsuario.OrdenarEntidadTercero(CentidadMetodos.EntidadGet("cTercero", "ppa"), "descripcion", "activo", Convert.ToInt16(Session["empresa"]));
                ddlEmpresaEmbarga.DataSource = tercero;
                this.ddlEmpresaEmbarga.DataValueField = "id";
                this.ddlEmpresaEmbarga.DataTextField = "descripcion";
                this.ddlEmpresaEmbarga.DataBind();
                this.ddlEmpresaEmbarga.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tercero embargante. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                DataView tercero = CcontrolesUsuario.OrdenarEntidadTercero(CentidadMetodos.EntidadGet("cTercero", "ppa"), "descripcion", "activo", Convert.ToInt16(Session["empresa"]));
                ddlTerceroEmbargo.DataSource = tercero;
                this.ddlTerceroEmbargo.DataValueField = "id";
                this.ddlTerceroEmbargo.DataTextField = "descripcion";
                this.ddlTerceroEmbargo.DataBind();
                this.ddlTerceroEmbargo.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tercero embargante. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlTercero.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nFuncionario", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlTercero.DataValueField = "tercero";
                this.ddlTercero.DataTextField = "descripcion";
                this.ddlTercero.DataBind();
                this.ddlTercero.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar empleados. Correspondiente a: " + ex.Message, "C");
            }



            try
            {
                this.ddlTipoEmbargo.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("gTipoEmbargo", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlTipoEmbargo.DataValueField = "codigo";
                this.ddlTipoEmbargo.DataTextField = "descripcion";
                this.ddlTipoEmbargo.DataBind();
                this.ddlTipoEmbargo.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar año. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlBanco.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("gBanco", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlBanco.DataValueField = "codigo";
                this.ddlBanco.DataTextField = "descripcion";
                this.ddlBanco.DataBind();
                this.ddlBanco.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar bancos. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlTipoCuenta.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("gTipoCuenta", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlTipoCuenta.DataValueField = "codigo";
                this.ddlTipoCuenta.DataTextField = "descripcion";
                this.ddlTipoCuenta.DataBind();
                this.ddlTipoCuenta.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar bancos. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlFormaPago.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("gFormaPago", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlFormaPago.DataValueField = "codigo";
                this.ddlFormaPago.DataTextField = "descripcion";
                this.ddlFormaPago.DataBind();
                this.ddlFormaPago.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar forma de pago. Correspondiente a: " + ex.Message, "C");
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
                    this.nitxtBusqueda.Focus();

                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }

        protected void lbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                  nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);
            CargarCombos();
            manejoNuevo();
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            this.txtCodigo.Enabled = false;
            this.nilblInformacion.Text = "";
        }

        private void manejoNuevo()
        {
            txvValorEmbargoPosterior.Enabled = false;
            txvCuotas.Enabled = false;
            chkCuotasPosteriores.Enabled = false;
            txvCuotasPosteriores.Enabled = false;
            //  txvSaldo.Enabled = false;
            txvValorFinalPos.Enabled = false;
            txvValorPorPosterior.Enabled = false;
            nilblInformacion.Text = "";
        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            ddlTercero.DataSource = null;
            ddlTercero.DataBind();
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

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (ddlTercero.SelectedValue.Trim().Length == 0)
            {
                MostrarMensaje("Seleccione un empleado valido por favor corrija");
                ddlTercero.Focus();
                return;
            }

            if (txtFecha.Text.Trim().Length == 0)
            {
                MostrarMensaje("Ingrese una fecha valida por favor corrija");
                txtFecha.Focus();
                return;
            }

            if (txtNumeroMandato.Text.Trim().Length == 0)
            {
                MostrarMensaje("Ingrese un numero de mandato valido por favor corrija");
                txtNumeroMandato.Focus();
                return;
            }

            if (txvValorEmbargo.Text.Trim().Length == 0)
            {
                MostrarMensaje("Ingrese un valor de embargo valido por favor corrija");
                txvValorEmbargo.Focus();
                return;
            }

            if (ddlTipoEmbargo.SelectedValue.Trim().Length == 0)
            {
                MostrarMensaje("Seleccione un tipo de embargo valido por favor corrija");
                ddlTipoEmbargo.Focus();
                return;
            }

            if (txtAñoInicial.Text.Trim().Length == 0)
            {
                MostrarMensaje("Ingrese un año por favor corrija");
                txtAñoInicial.Focus();
                return;
            }

            if (txtPeriodoInicial.Text.Trim().Length == 0)
            {
                MostrarMensaje("Ingrese un periodo por favor corrija");
                txtPeriodoInicial.Focus();
                return;
            }

            Guardar();
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
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);

            manejoNuevo();

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            this.txtCodigo.Enabled = false;
            this.ddlTercero.Enabled = false;
            this.ddlTipoEmbargo.Enabled = false;
            ddlContratos.Enabled = false;
            txtAñoInicial.Enabled = false;
            txtPeriodoInicial.Enabled = false;
            txtFecha.Enabled = false;

            try
            {
                CargarCombos();

                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    ddlTercero.SelectedValue = this.gvLista.SelectedRow.Cells[3].Text.Trim();

                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                    this.txtCodigo.Text = this.gvLista.SelectedRow.Cells[6].Text;


                if (this.gvLista.SelectedRow.Cells[7].Text != "&nbsp;")
                    this.ddlTipoEmbargo.SelectedValue = this.gvLista.SelectedRow.Cells[7].Text;


                foreach (Control objControl in this.gvLista.SelectedRow.Cells[9].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkActivo.Checked = ((CheckBox)objControl).Checked;
                }

                DataView dvEmbargo = embargos.RetornaDatosEmbargos(Convert.ToInt32(Session["empresa"]), ddlTercero.SelectedValue.Trim(), ddlTipoEmbargo.SelectedValue.Trim(), txtCodigo.Text.Trim());

                foreach (DataRowView registro in dvEmbargo)
                {
                    if (registro.Row.ItemArray.GetValue(6) != null)
                        txtNumeroMandato.Text = registro.Row.ItemArray.GetValue(6).ToString().Trim();

                    if (registro.Row.ItemArray.GetValue(7) != null)
                        txtFecha.Text = Convert.ToDateTime(registro.Row.ItemArray.GetValue(7).ToString().Trim()).ToShortDateString();

                    if (registro.Row.ItemArray.GetValue(8) != null)
                        txtPeriodoInicial.Text = registro.Row.ItemArray.GetValue(8).ToString().Trim();

                    if (registro.Row.ItemArray.GetValue(11) != null)
                    {
                        if (Convert.ToDecimal(registro.Row.ItemArray.GetValue(11).ToString()) > 0)
                            txvValorEmbargo.Text = registro.Row.ItemArray.GetValue(11).ToString();
                    }

                    if (registro.Row.ItemArray.GetValue(12) != null)
                    {
                        if (Convert.ToDecimal(registro.Row.ItemArray.GetValue(12).ToString()) > 0)
                            txvValorPorcentaje.Text = registro.Row.ItemArray.GetValue(12).ToString();
                    }

                    if (registro.Row.ItemArray.GetValue(13) != null)
                    {
                        if (registro.Row.ItemArray.GetValue(13).ToString() != "0")
                            ddlEmpresaEmbarga.SelectedValue = registro.Row.ItemArray.GetValue(13).ToString().Trim();
                    }

                    if (registro.Row.ItemArray.GetValue(14) != null)
                    {
                        if (registro.Row.ItemArray.GetValue(14).ToString() != "0")
                            ddlTerceroEmbargo.SelectedValue = registro.Row.ItemArray.GetValue(14).ToString().Trim();
                    }

                    if (registro.Row.ItemArray.GetValue(15) != null)
                    {
                        txvValorFinalEmbargo.Text = registro.Row.ItemArray.GetValue(15).ToString().Trim();
                    }

                    if (registro.Row.ItemArray.GetValue(16) != null)
                    {
                        if (Convert.ToDecimal(registro.Row.ItemArray.GetValue(16).ToString()) > 0)
                            txvValorPorPosterior.Text = registro.Row.ItemArray.GetValue(16).ToString();
                    }

                    if (registro.Row.ItemArray.GetValue(17) != null)
                        txvCuotasPosteriores.Text = decimal.Round(Convert.ToDecimal(registro.Row.ItemArray.GetValue(17).ToString().Trim()), 0).ToString();

                    if (registro.Row.ItemArray.GetValue(18) != null)
                    {
                        if (Convert.ToDecimal(registro.Row.ItemArray.GetValue(18).ToString()) > 0)
                            txvValorEmbargoPosterior.Text = registro.Row.ItemArray.GetValue(18).ToString();
                        else
                            txvValorEmbargoPosterior.Text = "0";
                    }

                    if (registro.Row.ItemArray.GetValue(19) != null)
                        txvValorBase.Text = registro.Row.ItemArray.GetValue(19).ToString().Trim();

                    if (registro.Row.ItemArray.GetValue(20) != null)
                        txvSaldo.Text = registro.Row.ItemArray.GetValue(20).ToString().Trim();

                    if (registro.Row.ItemArray.GetValue(23) != null)
                        ddlTipoCuenta.SelectedValue = registro.Row.ItemArray.GetValue(23).ToString().Trim();

                    if (registro.Row.ItemArray.GetValue(24) != null)
                        ddlBanco.SelectedValue = registro.Row.ItemArray.GetValue(24).ToString().Trim();

                    if (registro.Row.ItemArray.GetValue(25) != null)
                        txtNumeroCuenta.Text = registro.Row.ItemArray.GetValue(25).ToString().Trim();

                    if (registro.Row.ItemArray.GetValue(26) != null)
                        ddlFormaPago.SelectedValue = registro.Row.ItemArray.GetValue(26).ToString().Trim();

                    if (registro.Row.ItemArray.GetValue(27) != null)
                        chkActivo.Checked = Convert.ToBoolean(registro.Row.ItemArray.GetValue(27).ToString().Trim());

                    if (registro.Row.ItemArray.GetValue(28) != null)
                    {
                        chkCobroPosterior.Checked = Convert.ToBoolean(registro.Row.ItemArray.GetValue(28).ToString().Trim());
                        manejoCobroPos();
                    }

                    if (registro.Row.ItemArray.GetValue(29) != null)
                        chkCuotas.Checked = Convert.ToBoolean(registro.Row.ItemArray.GetValue(29).ToString().Trim());

                    if (registro.Row.ItemArray.GetValue(30) != null)
                    {
                        chkCuotasPosteriores.Checked = Convert.ToBoolean(registro.Row.ItemArray.GetValue(30).ToString().Trim());
                        manejoCuotasPos();
                    }

                    if (registro.Row.ItemArray.GetValue(31) != null)
                        chkManejaSaldo.Checked = Convert.ToBoolean(registro.Row.ItemArray.GetValue(31).ToString().Trim());

                    if (registro.Row.ItemArray.GetValue(32) != null)
                        txvValorFinalPos.Text = registro.Row.ItemArray.GetValue(32).ToString().Trim();

                    if (registro.Row.ItemArray.GetValue(33) != null)
                    {
                        txvCuotas.Text = decimal.Round(Convert.ToDecimal(registro.Row.ItemArray.GetValue(33).ToString().Trim()), 0).ToString();
                        manejoCuotas();
                    }

                    if (registro.Row.ItemArray.GetValue(34) != null)
                        txtFiscal.Text = registro.Row.ItemArray.GetValue(34).ToString().Trim();

                    if (registro.Row.ItemArray.GetValue(35) != null)
                        txtAñoInicial.Text = registro.Row.ItemArray.GetValue(35).ToString().Trim();

                    if (registro.Row.ItemArray.GetValue(36) != null)
                        chkSalarioMinimo.Checked = Convert.ToBoolean(registro.Row.ItemArray.GetValue(36).ToString().Trim());


                    if (registro.Row.ItemArray.GetValue(41) != null)
                    {
                        cargarContrato();
                        ddlContratos.SelectedValue = registro.Row.ItemArray.GetValue(41).ToString().Trim();
                    }

                    manejoCobroPos();
                    manejoCuotas();
                    manejoCuotasPos();
                    manejoSaldo();



                }


            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                                          ConfigurationManager.AppSettings["Modulo"].ToString(),
                                           nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            string operacion = "elimina";

            try
            {
                object[] objValores = new object[] {
                                    Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[6].Text)),        // @codigo
                                      Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[3].Text)),            //@empleado
                                       Convert.ToInt32(Session["empresa"]),     //@empresa
                                          Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[7].Text))  //@tipo
               
            };

                if (CentidadMetodos.EntidadInsertUpdateDelete(
                    "nEmbargos",
                    operacion,
                    "ppa",
                    objValores) == 0)
                {
                    ManejoExito("Datos eliminados satisfactoriamente", "E");
                }
                else
                {
                    ManejoError("Errores al eliminar el registro. Operación no realizada", "E");
                }
            }
            catch (Exception ex)
            {
                if (ex.HResult.ToString() == "-2146233087")
                {
                    ManejoError("El código ('" + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)) +
                    "' - ' " + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[3].Text)) + "') tiene una asociación, no es posible eliminar el registro.", "E");
                }
                else
                {
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
                }
            }
        }

        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
        }



        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();

        }
        #endregion Eventos

        protected void ddlEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlTercero.SelectedValue.Trim().Length > 0)
                    txtCodigo.Text = embargos.Consecutivo(Convert.ToInt32(Session["empresa"]), ddlTercero.SelectedValue.Trim());
                else
                {
                    nilblInformacion.Text = "Por favor seleccione un empleado valido";
                    return;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar codigo embargos debido a : " + ex.Message, "C");

            }

            if (ddlTercero.SelectedValue.Length > 0)
                cargarContrato();

        }

        private void cargarContrato()
        {
            try
            {
                DataView dvContratos = embargos.SelecccionaContratosTercero(Convert.ToInt32(ddlTercero.SelectedValue), Convert.ToInt32(this.Session["empresa"]));
                this.ddlContratos.DataSource = dvContratos;
                this.ddlContratos.DataValueField = "noContrato";
                this.ddlContratos.DataTextField = "desContrato";
                this.ddlContratos.DataBind();
                this.ddlContratos.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar contratos de tercero. Correspondiente a: " + ex.Message, "C");
            }
        }
        private void ManejoFecha()
        {

        }
        protected void ddlMes0_SelectedIndexChanged(object sender, EventArgs e)
        {
            ManejoFecha();
        }
        protected void chkCuotas_CheckedChanged(object sender, EventArgs e)
        {
            manejoCuotas();
        }

        private void manejoCuotas()
        {
            if (chkCuotas.Checked == true)
            {
                txvCuotas.Enabled = true;
            }
            else
            {
                txvCuotas.Enabled = false;
            }
        }
        protected void chkCobroPosterior_CheckedChanged(object sender, EventArgs e)
        {
            manejoCobroPos();
        }

        private void manejoCobroPos()
        {
            if (chkCobroPosterior.Checked == true)
            {
                txvValorEmbargoPosterior.Enabled = true;
                txvValorPorPosterior.Enabled = true;
                txvValorEmbargoPosterior.Enabled = true;
                chkCuotasPosteriores.Enabled = true;
                txvValorFinalPos.Enabled = true;

            }
            else
            {

                txvValorEmbargoPosterior.Enabled = false;
                txvValorPorPosterior.Enabled = false;
                txvValorEmbargoPosterior.Enabled = false;
                chkCuotasPosteriores.Enabled = false;
                txvValorFinalPos.Enabled = false;
            }
        }
        protected void chkCuotasPosteriores_CheckedChanged(object sender, EventArgs e)
        {
            manejoCuotasPos();
        }

        private void manejoCuotasPos()
        {
            if (chkCuotasPosteriores.Checked == true)
            {
                txvCuotasPosteriores.Enabled = true;
            }
            else
            {
                txvCuotasPosteriores.Enabled = false;

            }
        }
        protected void chkManejaSaldo_CheckedChanged(object sender, EventArgs e)
        {
            manejoSaldo();
        }

        private void manejoSaldo()
        {
            if (chkManejaSaldo.Checked == true)
            {
                if (Convert.ToDecimal(txvValorFinalEmbargo.Text) != 0)
                {
                    txvSaldo.Enabled = true;
                    if (Convert.ToBoolean(this.Session["editar"]) == false)
                        txvSaldo.Text = txvValorFinalEmbargo.Text;
                }
                else
                {
                    nilblInformacion.Text = "Debe ingresar un valor final del embargo para continuar";
                    return;
                }
            }
            else
            {
                txvSaldo.Enabled = false;
                txvSaldo.Text = "0";
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
                nilblInformacion.Text = "Seleccione una fecha valida";
                txtFecha.Text = "";
                txtFecha.Focus();
                return;
            }


        }
        protected void txtPeriodoCobro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPeriodoInicial.Text.Length > 0 & txtAñoInicial.Text.Length > 0)
                {
                    Convert.ToInt32(txtAñoInicial.Text);
                    Convert.ToInt32(txtPeriodoInicial.Text);
                }
            }
            catch (Exception ex)
            {
                nilblInformacion.Text = "Formato de año/periodo no valido";
                txtAñoInicial.Text = "";
                txtPeriodoInicial.Text = "";
                return;
            }
        }
    }
}