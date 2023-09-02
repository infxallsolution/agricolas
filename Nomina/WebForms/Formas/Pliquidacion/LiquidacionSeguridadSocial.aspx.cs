using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
using Nomina.WebForms.App_Code.Liquidacion;
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
    public partial class LiquidacionSeguridadSocial : BasePage
    {

        #region Instancias

        Ccontratos contratos = new Ccontratos();
        Cfuncionarios funcionarios = new Cfuncionarios();


        Coperadores operadores = new Coperadores();
        Cperiodos periodo = new Cperiodos();
        CseguridadSocial seguridadSocial = new CseguridadSocial();

        #endregion Instancias

        #region Metodos

        private void cargarCombo()
        {
            try
            {
                DataView dvTerceroCCosto = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("nFuncionario", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
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

            try
            {
                this.ddlTipoId.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("gTipoDocumento", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlTipoId.DataValueField = "codigo";
                this.ddlTipoId.DataTextField = "descripcion";
                this.ddlTipoId.DataBind();
                this.ddlTipoId.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tipo documento. Correspondiente a: " + ex.Message, "C");
            }
            try
            {
                this.ddlTipoCotizante.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("nTipoCotizante", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlTipoCotizante.DataValueField = "codigo";
                this.ddlTipoCotizante.DataTextField = "descripcion";
                this.ddlTipoCotizante.DataBind();
                this.ddlTipoCotizante.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tipo cotizante. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlDepartamento.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("gDepartamento", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlDepartamento.DataValueField = "codigo";
                this.ddlDepartamento.DataTextField = "descripcion";
                this.ddlDepartamento.DataBind();
                this.ddlDepartamento.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar departamento. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlSubTipoCotizante.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nSubTipoCotizante", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"]));
                this.ddlSubTipoCotizante.DataValueField = "codigo";
                this.ddlSubTipoCotizante.DataTextField = "descripcion";
                this.ddlSubTipoCotizante.DataBind();
                this.ddlSubTipoCotizante.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar subtipo cotizante. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlARL.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("nEntidadArp", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlARL.DataValueField = "tercero";
                this.ddlARL.DataTextField = "descripcion";
                this.ddlARL.DataBind();
                this.ddlARL.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar  ARL. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlPension.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("nEntidadFondoPension", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlPension.DataValueField = "tercero";
                this.ddlPension.DataTextField = "descripcion";
                this.ddlPension.DataBind();
                this.ddlPension.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar  pensión. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlSalud.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("nEntidadEps", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlSalud.DataValueField = "tercero";
                this.ddlSalud.DataTextField = "descripcion";
                this.ddlSalud.DataBind();
                this.ddlSalud.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar  salud. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlCaja.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("nEntidadCaja", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlCaja.DataValueField = "tercero";
                this.ddlCaja.DataTextField = "descripcion";
                this.ddlCaja.DataBind();
                this.ddlCaja.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar  Caja. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlSena.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("nEntidadSena", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlSena.DataValueField = "tercero";
                this.ddlSena.DataTextField = "descripcion";
                this.ddlSena.DataBind();
                this.ddlSena.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar Sena. Correspondiente a: " + ex.Message, "C");
            }


            try
            {
                this.ddlICBF.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("nEntidadIcbf", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlICBF.DataValueField = "tercero";
                this.ddlICBF.DataTextField = "descripcion";
                this.ddlICBF.DataBind();
                this.ddlICBF.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar ICBF. Correspondiente a: " + ex.Message, "C");
            }

        }

        private void verificaPeriodoCerrado(int año, int mes, int empresa, DateTime fecha)
        {
            if (periodo.RetornaPeriodoCerradoNomina(año, mes, Convert.ToInt16(Session["empresa"]), fecha) == 1)
            {
                ManejoError("Periodo cerrado. No es posible realizar nuevas transacciones", "I");
                return;
            }
        }

        private void LiquidarSeguridadSocial()
        {
            try
            {
                this.gvLista.DataSource = seguridadSocial.LiquidaSeguridadSocial(Convert.ToInt16(Session["empresa"]), Convert.ToInt32(nitxvAño.Text), Convert.ToInt16(niddlMes.SelectedValue));
                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";
                nilblInformacion.Text = "Liquidación generada satisfactoriamente";

            }
            catch (Exception ex)
            {
                ManejoError("Error al liquidar seguridad social. Correspondiente a: " + ex.Message, "C");
            }
        }
        private void verificaPeriodoCerradoContable(int año, int mes, int empresa)
        {
            if (periodo.RetornaPeriodoCerrado(año, mes, empresa) == 1)
            {
                ManejoError("Periodo cerrado. No es posible realizar nuevas transacciones", "I");
                return;
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
                pnDatos.Visible = false;
                if (nitxvAño.Text.Length == 0)
                {
                    nilblInformacion.Text = "Debe digitar un año antes de buscar";
                    return;
                }
                this.gvLista.DataSource = seguridadSocial.BuscarEntidad(Convert.ToInt16(Session["empresa"]), Convert.ToInt32(nitxvAño.Text), Convert.ToInt16(niddlMes.SelectedValue), nitxtFiltro.Text);
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
            CcontrolesUsuario.InhabilitarControles(this.pnDatos.Controls);
            CcontrolesUsuario.LimpiarControles(this.pnDatos.Controls);
            this.nilbNuevo.Visible = true;
            this.nilbRegistrar.Visible = false;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "ex", mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }
        private void Guardar()
        {
            string operacion = "inserta";
            try
            {
                int año = 0;
                int registro = 0;
                string departamento = null;
                string ciudad = null;
                string RecidenteExterior = "";
                DateTime fechaRadExterior = Convert.ToDateTime("1990-01-01");
                string ING = "";
                string RET = "";
                string TDE = "";
                string TAE = "";
                string TDP = "";
                string TAP = "";
                string VSP = "";
                string VST = "";
                string SLN = "";
                string IGE = "";
                DateTime fiIGE = Convert.ToDateTime("1990-01-01");
                DateTime ffIGE = Convert.ToDateTime("1990-01-01");
                DateTime fechaIngreso = Convert.ToDateTime("1990-01-01");
                DateTime fechaRetiro = Convert.ToDateTime("1990-01-01");
                DateTime fiSLN = Convert.ToDateTime("1990-01-01");
                DateTime ffSLN = Convert.ToDateTime("1990-01-01");
                string LMA = "";
                DateTime fiLMA = Convert.ToDateTime("1990-01-01");
                DateTime ffLMA = Convert.ToDateTime("1990-01-01");
                string VAC = "";
                DateTime fiVAC = Convert.ToDateTime("1990-01-01");
                DateTime ffVAC = Convert.ToDateTime("1990-01-01");
                string AVP = "";
                string VCT = "";
                DateTime fiVCT = Convert.ToDateTime("1990-01-01");
                DateTime ffVCT = Convert.ToDateTime("1990-01-01");
                DateTime fiIRL = Convert.ToDateTime("1990-01-01");
                DateTime ffIRL = Convert.ToDateTime("1990-01-01");
                string salarioIntegral = "";
                int dPension = 0;
                decimal IBCpension = 0;
                string AFPdestino = "";
                int dSalud = 0;
                decimal IBCsalud = 0;
                string extranjero = "";
                int dArl = 0;
                decimal IBCarl = 0;
                decimal pArl = 0;
                int claseARL = 0;
                string centroTrabajo = "";
                int dCaja = 0;
                decimal IBCcaja = 0;
                string ExS = "";
                decimal IRL = 0;
                DateTime fiVSP = Convert.ToDateTime("1990-01-01");
                string correccion = "", terceroPension = null, terceroPensionAdicional = null, terceroARL = null, terceroCaja = null, terceroSalud = null, terceroICBF = null, terceroSaludAdicional = null, terceroSena = null;


                if (chkING.Checked)
                    ING = ddlIngreso.SelectedValue;
                if (chkRET.Checked)
                    RET = ddlRetiro.SelectedValue;
                if (chkTDE.Checked)
                    TDE = "X";
                if (chkTAE.Checked)
                    TAE = "X";
                if (chkTDP.Checked)
                    TDP = "X";
                if (chkTAP.Checked)
                    TAP = "X";
                if (chkVST.Checked)
                    VST = "X";
                if (chkSLN.Checked)
                    SLN = "X";
                if (chkIGE.Checked)
                    IGE = "X";
                if (chkLMA.Checked)
                    LMA = "X";
                if (chkVAC.Checked)
                    VAC = ddlVacaciones.SelectedValue;
                if (chkAVP.Checked)
                    AVP = "X";
                if (chkVCT.Checked)
                    VCT = "X";
                if (chkExoneraSalud.Checked)
                    ExS = "S";
                else
                    ExS = "N";
                if (chkExtrajero.Checked)
                    extranjero = "X";
                if (chkRecidenteExtranjero.Checked)
                    RecidenteExterior = "X";
                if (chkIRL.Checked)
                    IRL = Convert.ToDecimal(txvIRP.Text);
                if (chkSalarioIntegral.Checked)
                    salarioIntegral = "X";

                if (ddlPension.SelectedValue.Length > 0)
                    terceroPension = ddlPension.SelectedValue;
                if (ddlSalud.SelectedValue.Length > 0)
                    terceroSalud = ddlSalud.SelectedValue;
                if (ddlARL.SelectedValue.Length > 0)
                    terceroARL = ddlARL.SelectedValue;
                if (ddlICBF.SelectedValue.Length > 0)
                    terceroICBF = ddlICBF.SelectedValue;
                if (ddlPensionDestino.SelectedValue.Length > 0)
                    terceroPensionAdicional = ddlPensionDestino.SelectedValue;
                if (ddlCaja.SelectedValue.Length > 0)
                    terceroCaja = ddlCaja.SelectedValue;
                if (ddlSaludDestino.SelectedValue.Length > 0)
                    terceroSaludAdicional = ddlSaludDestino.SelectedValue;
                if (ddlSena.SelectedValue.Length > 0)
                    terceroSena = ddlSena.SelectedValue;

                if (Convert.ToBoolean(this.Session["editar"]) == true)
                {
                    operacion = "actualiza";
                    registro = Convert.ToInt16(lbRegistro.Text);
                }


                if (ddlPensionDestino.SelectedValue.Trim().Length > 0)
                    AFPdestino = ddlPensionDestino.SelectedValue;

                if (txvAño.Text.Trim().Length > 0)
                    año = Convert.ToInt16(txvAño.Text);

                if (ddlCentroTrabajo.SelectedValue.Trim().Length > 0)
                    centroTrabajo = ddlCentroTrabajo.SelectedValue.Trim();

                if (ddlCiudad.SelectedValue.Trim().Length > 0)
                    ciudad = ddlCiudad.SelectedValue.Trim();

                if (ddlClaseARL.SelectedValue.Trim().Length > 0)
                    claseARL = Convert.ToInt16(ddlClaseARL.SelectedValue.Trim());

                if (txvDiasARP.Text.Trim().Length > 0)
                    dArl = Convert.ToInt16(txvDiasARP.Text);

                if (txvDiasCaja.Text.Trim().Length > 0)
                    dCaja = Convert.ToInt16(txvDiasCaja.Text);

                if (ddlDepartamento.SelectedValue.Trim().Length > 0)
                    departamento = ddlDepartamento.SelectedValue.Trim();

                if (txvDiasPension.Text.Trim().Length > 0)
                    dPension = Convert.ToInt16(txvDiasPension.Text);

                if (txvDiasSalud.Text.Trim().Length > 0)
                    dSalud = Convert.ToInt16(txvDiasSalud.Text);
                if (txtFechaIngreso.Text.Trim().Length > 0)
                    fechaIngreso = Convert.ToDateTime(txtFechaIngreso.Text);
                if (txtFechaRadExtrajero.Text.Trim().Length > 0)
                    fechaRadExterior = Convert.ToDateTime(txtFechaRadExtrajero.Text);
                if (txtFechaRadExtrajero.Text.Trim().Length > 0)
                    fechaRadExterior = Convert.ToDateTime(txtFechaRadExtrajero.Text);
                if (txtFechaRetiro.Text.Trim().Length > 0)
                    fechaRetiro = Convert.ToDateTime(txtFechaRetiro.Text);
                if (txtFechaVSP.Text.Trim().Length > 0)
                    fiVSP = Convert.ToDateTime(txtFechaVSP.Text);
                if (txtFechaFinalIGE.Text.Trim().Length > 0)
                    ffIGE = Convert.ToDateTime(txtFechaFinalIGE.Text);
                if (txtFechaFinalIRL.Text.Trim().Length > 0)
                    ffIRL = Convert.ToDateTime(txtFechaFinalIRL.Text);
                if (txtFechaFinalLMA.Text.Trim().Length > 0)
                    ffLMA = Convert.ToDateTime(txtFechaFinalLMA.Text);
                if (txtFechaFinalLMA.Text.Trim().Length > 0)
                    ffLMA = Convert.ToDateTime(txtFechaFinalLMA.Text);
                if (txtFechaFinalSLN.Text.Trim().Length > 0)
                    ffSLN = Convert.ToDateTime(txtFechaFinalSLN.Text);
                if (txtFechaFinalVacaciones.Text.Trim().Length > 0)
                    ffVAC = Convert.ToDateTime(txtFechaFinalVacaciones.Text);
                if (txtFechaFinalVCT.Text.Trim().Length > 0)
                    ffVCT = Convert.ToDateTime(txtFechaFinalVCT.Text);
                if (txtFechaInicialIGE.Text.Trim().Length > 0)
                    fiIGE = Convert.ToDateTime(txtFechaInicialIGE.Text);
                if (txtFechaInicialIRL.Text.Trim().Length > 0)
                    fiIRL = Convert.ToDateTime(txtFechaInicialIRL.Text);
                if (txtFechaInicialLMA.Text.Trim().Length > 0)
                    fiLMA = Convert.ToDateTime(txtFechaInicialLMA.Text);
                if (txtFechaInicialSLN.Text.Trim().Length > 0)
                    fiSLN = Convert.ToDateTime(txtFechaInicialSLN.Text);
                if (txtFechaInicialVacaciones.Text.Trim().Length > 0)
                    fiVAC = Convert.ToDateTime(txtFechaInicialVacaciones.Text);
                if (txtFechaInicialVCT.Text.Trim().Length > 0)
                    fiVCT = Convert.ToDateTime(txtFechaInicialVCT.Text);

                if (txvIBCArp.Text.Trim().Length > 0)
                    IBCarl = Convert.ToDecimal(txvIBCArp.Text);

                if (txvIBCCaja.Text.Trim().Length > 0)
                    IBCcaja = Convert.ToDecimal(txvIBCCaja.Text);

                if (txvIBCCaja.Text.Trim().Length > 0)
                    IBCcaja = Convert.ToDecimal(txvIBCCaja.Text);

                if (txvIBCPension.Text.Trim().Length > 0)
                    IBCpension = Convert.ToDecimal(txvIBCPension.Text);
                if (txvIBCSalud.Text.Trim().Length > 0)
                    IBCsalud = Convert.ToDecimal(txvIBCSalud.Text);

                if (txvpARP.Text.Trim().Length > 0)
                    pArl = Convert.ToDecimal(txvpARP.Text);

                if (chkCorreccion.Checked)
                    correccion = "X";

                object[] objValoEditar = new object[]{
                                    terceroSaludAdicional, // @AFPdestino	string
                                    año  , //@año	int
                                    txtApellido1.Text.Trim(), //@apellido1	string
                                    txtApellido2.Text.Trim(), //@apellido2	string
                                    AVP,  //@AVP	string
                                    centroTrabajo, //@centroTrabajo	string
                                    ciudad  , //@ciudad	string
                                    claseARL,  //@claseARL	int
                                    txtIdentificacion.Text, //@codigoTercero	string
                                    Convert.ToInt16(ddlContratos.SelectedValue),//contrato
                                    correccion,//@correciones	string
                                    Convert.ToDecimal(txvValorVoluntarioAfiliado.Text), //@cotizacionVoluntariaAfiliado	float   10
                                    Convert.ToDecimal(txvValorVoluntarioEmpleador.Text),//@cotizacionVoluntariaEmpleador	float
                                    dArl ,//@dArl	int
                                    dCaja, //@dCaja	int
                                    departamento, //@departamento	string
                                    dPension,  //@dPension	int
                                    dSalud,//@dSalud	int
                                    Convert.ToInt16(this.Session["empresa"]),  //@empresa	int
                                    ExS,  //@exoneraSalud	string
                                    extranjero, //@extranjero	string
                                    fechaIngreso, //@fechaIngreso	date   ----20
                                    fechaRadExterior, //@fechaRadExterior	date
                                    fechaRetiro, //@fechaRetiro	date
                                    fiVSP,    //@fechaVSP	date 23
                                    ffIGE,  //@ffIGE	date
                                    ffIRL,   //@ffIRL	date
                                    ffLMA,  //@ffLMA	date
                                    ffSLN,   //@ffSLN	date
                                    ffVAC, //@ffVAC	date
                                    ffVCT,  //@ffVCT	date
                                    fiIGE,  //@fiIGE	date ----30
                                    fiIRL,  //@fiIRL	date
                                    fiLMA, //@fiLMA	date
                                    fiSLN, //@fiSLN	date
                                    fiVAC, //@fiVAC	date
                                    fiVCT, //@fiVCT	date
                                    Convert.ToDecimal(txvNoHoras.Text), //@horasLaboradas	int
                                   IBCarl ,  //@IBCarl	int
                                   IBCcaja, //@IBCcaja	int
                                   Convert.ToDecimal(txvValorOtrosParafiscales.Text),  //@IBCCajaOtros	float
                                   IBCpension,  //@IBCpension	int  ------40
                                   IBCsalud,  //@IBCsalud	
                                   Convert.ToInt16(ddlEmpleado.SelectedValue), //@idTercero	int
                                   IGE,  //@IGE	string
                                   ddlAltoRiesgo.SelectedValue,  //@indicadorAltoRiesgo	int
                                   ING, //@ING	string
                                   IRL, //@IRL	int
                                   LMA, //@LMA	string
                                   ddlMes.SelectedValue, //@mes	int
                                   txtNoAutorizacionEG.Text, //@noAutorizacionEG	string ---------------50
                                   txtNoAutorizacionLAM.Text, //@noAutorizacionLMA	string
                                   txtIdentificacionUPC.Text, //@noIDcotizanteUPC	string
                                   txtNombre1.Text,  //@nombre1	string
                                   txtNombre2.Text,  //@nombre2	string
                                   pArl,  //@pArl	float -----------------55
                                   Convert.ToDecimal(txvpCaja.Text), //@pCaja	float
                                   Convert.ToDecimal(txvpESAP.Text), //@pESAP	float
                                   Convert.ToDecimal(txvpFondo.Text),  //@pFondo	float
                                   Convert.ToDecimal(txvpICBF.Text),  //@pICBF	float
                                   Convert.ToDecimal(txvpMEN.Text),  //@pMEN	float
                                   Convert.ToDecimal(txvpPension.Text),  //@pPension	float
                                   Convert.ToDecimal(txvpSalud.Text), //@pSalud	float
                                   Convert.ToDecimal(txvpSena.Text), //@pSena	float
                                   RecidenteExterior,   //@RecidenteExterior	string
                                   registro, //@registro	int
                                   RET, //@RET	string
                                   Convert.ToDecimal(txvSalario.Text), //@salario	int
                                   salarioIntegral,  //@salarioIntegral	string
                                   ddlSaludDestino.SelectedValue.Trim(),  //@saludDestino	nchar
                                   SLN, //@SLN	string
                                   ddlSubTipoCotizante.SelectedValue.Trim(), //@subTipoCotizante	string
                                   TAE, //@TAE	string
                                   TAP, //@TAP	string
                                   TDE, //@TDE	string
                                   TDP, //@TDP	string
                                   terceroARL, //@terceroArl	int
                                   terceroCaja, //@terceroCaja	int
                                   terceroICBF, //@terceroIcbf	int
                                   terceroPension,  //@terceroPension	int
                                   terceroSalud, //@terceroSalud	int
                                   terceroSena, //@terceroSena	int
                                   ddlTipoCotizante.SelectedValue, //@tipoCotizante	string
                                   ddlTipoId.SelectedValue, //@tipoIDcotizanteUPC	string
                                   ddlTipoIdUPC.SelectedValue, //@tipoIDcotizanteUPC	string
                                   Convert.ToDecimal( txvValorTotalPension.Text), //@totalPension	float
                                   VAC, //@VAC	string
                                  Convert.ToDecimal( txvValorARP.Text), //@valorArl	float
                                   Convert.ToDecimal( txvValorCaja.Text),  //@valorCaja	int
                                   Convert.ToDecimal( txvValorESAP.Text), //@valorESAP	float
                                  Convert.ToDecimal( txvValorFondo.Text),  //@valorFondo	float
                                  Convert.ToDecimal( txvValorFondoSub.Text),   //@valorFondoSub	float
                                   Convert.ToDecimal( txvValorICBF.Text),   //@valorICBF	int
                                   Convert.ToDecimal(txvValorEG.Text),  //@valorIncapacidad	float
                                   Convert.ToDecimal(txvValorLAM.Text), //@valorLMA	float
                                   Convert.ToDecimal(txvValorMEN.Text), //@valorMEN	float
                                    Convert.ToDecimal(txvValorPension.Text), //@valorPension	int
                                    Convert.ToDecimal(txvValorRetenido.Text), //@valorRetenido	float
                                    Convert.ToDecimal(txvValorSalud.Text), //@valorSalud	float
                                   Convert.ToDecimal(txvValorSena.Text), //@valorSena	int
                                    Convert.ToDecimal(txvValorUPC.Text), //@valorUPC	float
                                   VCT,  //@VCT	string
                                   VSP ,  //@VSP	string
                                   VST //@VST	string                        
                };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("nSeguridadSocialPila", operacion, "ppa", objValoEditar))
                {
                    case 0:
                        ManejoExito("Datos insertados satisfactoriamente", "I");
                        break;
                    case 1:
                        ManejoError("Error al insertar el detalle de la transaccción", "I");
                        break;
                }

            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar los datos correspondiente a: " + ex.Message, operacion.Substring(0, 1).ToUpper());
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
                    // nitxvAño.Text = DateTime.Now.Year.ToString();
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


            // CcontrolesUsuario.HabilitarControles(this.pnDatos.Controls);
            //  CcontrolesUsuario.LimpiarControles(pnDatos.Controls);
            pnDatos.Visible = true;

            CcontrolesUsuario.HabilitarControles(pnDatos.Controls);
            CcontrolesUsuario.LimpiarControles(pnDatos.Controls);

            cargarCombo();
            this.nilbNuevo.Visible = false;
            this.nibtnLiquidar.Visible = false;
            this.nilbRegistrar.Visible = true;
            lbCancelar.Visible = true;
            nibtnGenerarPlano.Visible = false;
            niimbBuscar.Visible = false;
            this.Session["editar"] = null;
            this.nilblInformacion.Text = "";
            this.txvAño.Text = DateTime.Now.Year.ToString();
        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.pnDatos.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.nilbNuevo.Visible = true;
            this.nilbRegistrar.Visible = false;
            pnDatos.Visible = false;
            this.nilblInformacion.Text = "";
            this.nibtnLiquidar.Visible = true;
            this.Session["editar"] = null;
            niimbBuscar.Visible = true;
            nibtnGenerarPlano.Visible = true;
            lbCancelar.Visible = false;
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                object[] objValores = new object[] {
                Convert.ToInt16(gvLista.Rows[e.RowIndex].Cells[2].Text),
                Convert.ToInt16(Session["empresa"]),
                Convert.ToInt16(gvLista.Rows[e.RowIndex].Cells[3].Text),
                Convert.ToInt16(this.gvLista.Rows[e.RowIndex].Cells[4].Text)
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("nSeguridadSocialPila", "elimina", "ppa", objValores))
                {
                    case 0:
                        ManejoExito("Registro eliminado satisfactoriamente", "E");
                        break;
                    case 1:
                        ManejoError("Error al eliminar el registro. Operación no realizada", "E");
                        break;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
            }
        }
        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.pnDatos.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
        protected void btnLiquidar_Click(object sender, EventArgs e)
        {
            if (nitxvAño.Text.Length == 0 || niddlMes.SelectedValue.Length == 0)
            {
                nilblInformacion.Text = "Seleccione un año y mes a liquidar";
                return;
            }

            if (periodo.RetornaPeriodoCerrado(Convert.ToInt32(nitxvAño.Text), Convert.ToInt32(niddlMes.SelectedValue), Convert.ToInt16(Session["empresa"])) == 1)
            {
                ManejoError("Periodo cerrado. No es posible realizar nuevas transacciones", "I");
                return;
            }


            LiquidarSeguridadSocial();
        }
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnDatos.Visible = true;
            CcontrolesUsuario.HabilitarControles(this.pnDatos.Controls);
            CcontrolesUsuario.HabilitarControles(pnDatos.Controls);
            CcontrolesUsuario.LimpiarControles(this.pnDatos.Controls);
            CcontrolesUsuario.LimpiarControles(pnDatos.Controls);

            this.nilbNuevo.Visible = false;
            this.nilbRegistrar.Visible = true;
            this.Session["editar"] = true;
            int año = Convert.ToInt16(gvLista.SelectedRow.Cells[2].Text);
            int mes = Convert.ToInt16(gvLista.SelectedRow.Cells[3].Text);
            int registro = Convert.ToInt16(gvLista.SelectedRow.Cells[4].Text);

            ddlMes.Enabled = false;
            txvAño.Enabled = false;
            cargarCombo();
            DataView dvContrato = seguridadSocial.LiquidaSeguridadSocialRegistro(Convert.ToInt16(Session["empresa"]), año, mes, registro);

            try
            {
                txvAño.Text = gvLista.SelectedRow.Cells[2].Text;
                ddlMes.SelectedValue = gvLista.SelectedRow.Cells[3].Text;

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                    this.lbRegistro.Text = Server.HtmlDecode(gvLista.SelectedRow.Cells[4].Text);
                else
                    this.lbRegistro.Text = "0";

                if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                {
                    ddlEmpleado.SelectedValue = this.gvLista.SelectedRow.Cells[5].Text;
                    txtCodigoTercero.Text = this.gvLista.SelectedRow.Cells[5].Text;
                }
                ddlEmpleado.Enabled = false;
                ddlContratos.Enabled = false;

                foreach (DataRowView registros in dvContrato)
                {
                    int columna = 0;
                    columna = 5;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txtIdentificacion.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 6;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txtApellido1.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 7;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txtApellido2.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 8;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txtNombre1.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 9;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txtNombre2.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 10;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        ddlDepartamento.SelectedValue = registros.Row.ItemArray.GetValue(columna).ToString();
                    cargarCiudad(ddlDepartamento.SelectedValue);
                    columna = 11;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        ddlCiudad.SelectedValue = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 12;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        ddlTipoCotizante.SelectedValue = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 13;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        ddlSubTipoCotizante.SelectedValue = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 14;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvNoHoras.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 15;
                    if (registros.Row.ItemArray.GetValue(columna).ToString() != "")
                        chkExtrajero.Checked = true;
                    columna = 16;
                    if (registros.Row.ItemArray.GetValue(columna).ToString() != "")
                        chkRecidenteExtranjero.Checked = true;
                    columna = 17;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txtFechaRadExtrajero.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    else
                        txtFechaRadExtrajero.Text = "";
                    columna = 18;
                    if (registros.Row.ItemArray.GetValue(columna).ToString() != "")
                    {
                        chkING.Checked = true;
                        ddlIngreso.SelectedValue = registros.Row.ItemArray.GetValue(columna).ToString();
                    }
                    columna = 19;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txtFechaIngreso.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    else
                        txtFechaIngreso.Text = "";
                    columna = 20;
                    if (registros.Row.ItemArray.GetValue(columna).ToString() != "")
                    {
                        chkRET.Checked = true;
                        ddlRetiro.SelectedValue = registros.Row.ItemArray.GetValue(columna).ToString();
                    }
                    columna = 21;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txtFechaRetiro.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    else
                        txtFechaRetiro.Text = "";
                    columna = 22;
                    if (registros.Row.ItemArray.GetValue(columna).ToString() != "")
                        chkTDE.Checked = true;
                    columna = 23;
                    if (registros.Row.ItemArray.GetValue(columna).ToString() != "")
                        chkTAP.Checked = true;
                    columna = 24;
                    if (registros.Row.ItemArray.GetValue(columna).ToString() != "")
                        chkTDP.Checked = true;
                    columna = 25;
                    if (registros.Row.ItemArray.GetValue(columna).ToString() != "")
                        chkTAP.Checked = true;
                    columna = 26;
                    if (registros.Row.ItemArray.GetValue(columna).ToString() != "")
                        chkVSP.Checked = true;
                    columna = 27;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txtFechaVSP.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    else
                        txtFechaVSP.Text = "";
                    columna = 28;
                    if (registros.Row.ItemArray.GetValue(columna).ToString() != "")
                        chkVST.Checked = true;
                    columna = 29;
                    if (registros.Row.ItemArray.GetValue(columna).ToString() != "")
                        chkSLN.Checked = true;
                    columna = 30;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txtFechaInicialSLN.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    else
                        txtFechaInicialSLN.Text = "";
                    columna = 31;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txtFechaFinalSLN.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    else
                        txtFechaFinalSLN.Text = "";
                    columna = 32;
                    if (registros.Row.ItemArray.GetValue(columna).ToString() != "")
                        chkIGE.Checked = true;
                    columna = 33;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txtFechaInicialIGE.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    else
                        txtFechaInicialIGE.Text = "";
                    columna = 34;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txtFechaFinalIGE.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    else
                        txtFechaFinalIGE.Text = "";
                    columna = 35;
                    if (registros.Row.ItemArray.GetValue(columna).ToString() != "")
                        chkLMA.Checked = true;
                    columna = 36;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txtFechaInicialLMA.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    else
                        txtFechaInicialLMA.Text = "";
                    columna = 37;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txtFechaFinalLMA.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    else
                        txtFechaFinalLMA.Text = "";
                    columna = 38;
                    if (registros.Row.ItemArray.GetValue(columna).ToString() != "")
                    {
                        chkVAC.Checked = true;
                        ddlVacaciones.SelectedValue = registros.Row.ItemArray.GetValue(columna).ToString();
                    }
                    columna = 39;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txtFechaInicialVacaciones.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    else
                        txtFechaInicialVacaciones.Text = "";
                    columna = 40;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txtFechaFinalVacaciones.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    else
                        txtFechaFinalVacaciones.Text = "";
                    columna = 41;
                    if (registros.Row.ItemArray.GetValue(columna).ToString() != "")
                        chkAVP.Checked = true;
                    columna = 42;
                    if (registros.Row.ItemArray.GetValue(columna).ToString() != "")
                        chkVCT.Checked = true;
                    columna = 43;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txtFechaInicialVCT.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    else
                        txtFechaInicialVCT.Text = "";
                    columna = 44;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txtFechaFinalVCT.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    else
                        txtFechaFinalVCT.Text = "";
                    columna = 45;
                    txvIRP.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 46;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txtFechaInicialIRL.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    else
                        txtFechaInicialIRL.Text = "";
                    columna = 47;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txtFechaFinalIRL.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    else
                        txtFechaFinalIRL.Text = "";
                    columna = 48;
                    if (registros.Row.ItemArray.GetValue(columna).ToString() != "")
                        chkCorreccion.Checked = true;
                    else
                        chkCorreccion.Checked = false;
                    columna = 49;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvSalario.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    else
                        txvSalario.Text = "0";
                    columna = 50;
                    if (registros.Row.ItemArray.GetValue(columna).ToString() != "")
                        chkSalarioIntegral.Checked = true;
                    else
                        chkSalarioIntegral.Checked = false;
                    columna = 51;
                    if (registros.Row.ItemArray.GetValue(columna) != null || Convert.ToInt16(registros.Row.ItemArray.GetValue(columna).ToString()) > 0)
                    {
                        try
                        {
                            ddlPension.SelectedValue = registros.Row.ItemArray.GetValue(columna).ToString();
                        }
                        catch (Exception)
                        {
                        }
                    }
                    columna = 52;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvDiasPension.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 53;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvIBCPension.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 54;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvpPension.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 55;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvValorPension.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 56;
                    if (registros.Row.ItemArray.GetValue(columna) != null || registros.Row.ItemArray.GetValue(columna).ToString() != "")
                    {
                        try
                        {
                            ddlAltoRiesgo.SelectedValue = registros.Row.ItemArray.GetValue(columna).ToString();
                        }
                        catch (Exception)
                        {
                        }
                    }
                    columna = 57;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvValorVoluntarioAfiliado.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 58;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvValorVoluntarioEmpleador.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 59;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvValorFondo.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 60;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvValorFondoSub.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 61;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvpFondo.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 62;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvValorRetenido.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 63;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvValorTotalPension.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 64;
                    if (registros.Row.ItemArray.GetValue(columna) != null || Convert.ToInt16(registros.Row.ItemArray.GetValue(columna).ToString()) > 0)
                    {
                        try
                        {
                            ddlPensionDestino.SelectedValue = registros.Row.ItemArray.GetValue(columna).ToString();
                        }
                        catch (Exception)
                        {
                        }
                    }

                    columna = 65;
                    if (registros.Row.ItemArray.GetValue(columna) != null || Convert.ToInt16(registros.Row.ItemArray.GetValue(columna).ToString()) > 0)
                    {
                        try
                        {
                            ddlSalud.SelectedValue = registros.Row.ItemArray.GetValue(columna).ToString();
                        }
                        catch (Exception)
                        {
                        }
                    }

                    columna = 66;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvDiasSalud.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 67;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvIBCSalud.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 68;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvpSalud.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 69;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvValorSalud.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 70;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvValorUPC.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 71;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txtNoAutorizacionEG.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 72;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvValorEG.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 73;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txtNoAutorizacionLAM.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 74;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvValorLAM.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 75;
                    if (registros.Row.ItemArray.GetValue(columna) != null || Convert.ToInt16(registros.Row.ItemArray.GetValue(columna).ToString()) > 0)
                    {
                        try
                        {
                            ddlSaludDestino.SelectedValue = registros.Row.ItemArray.GetValue(columna).ToString();
                        }
                        catch (Exception)
                        {
                        }
                    }

                    columna = 76;
                    if (registros.Row.ItemArray.GetValue(columna) != null || Convert.ToInt16(registros.Row.ItemArray.GetValue(columna).ToString()) > 0)
                    {
                        try
                        {
                            ddlARL.SelectedValue = registros.Row.ItemArray.GetValue(columna).ToString();
                        }
                        catch (Exception)
                        {
                        }
                    }

                    columna = 77;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvDiasARP.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 78;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvIBCArp.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 79;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvpARP.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 80;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        ddlClaseARL.SelectedValue = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 81;
                    if (registros.Row.ItemArray.GetValue(columna) != null || registros.Row.ItemArray.GetValue(columna).ToString() != "")
                        ddlCentroTrabajo.SelectedValue = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 82;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvValorARP.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 83;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvDiasCaja.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 84;
                    if (registros.Row.ItemArray.GetValue(columna) != null || Convert.ToInt16(registros.Row.ItemArray.GetValue(columna).ToString()) > 0)
                    {
                        try
                        {
                            ddlCaja.SelectedValue = registros.Row.ItemArray.GetValue(columna).ToString();
                        }
                        catch (Exception)
                        {
                        }
                    }

                    columna = 85;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvIBCCaja.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 86;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvpCaja.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 87;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvValorCaja.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 88;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvValorOtrosParafiscales.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 89;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvpSena.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 90;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvValorSena.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 91;
                    if (registros.Row.ItemArray.GetValue(columna) != null || Convert.ToInt16(registros.Row.ItemArray.GetValue(columna).ToString()) > 0)
                    {
                        try
                        {
                            ddlSena.SelectedValue = registros.Row.ItemArray.GetValue(columna).ToString();
                        }
                        catch (Exception)
                        {
                        }
                    }

                    columna = 92;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvpICBF.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 93;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvValorICBF.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 94;
                    if (registros.Row.ItemArray.GetValue(columna) != null || Convert.ToInt16(registros.Row.ItemArray.GetValue(columna).ToString()) > 0)
                    {
                        try
                        {
                            ddlICBF.SelectedValue = registros.Row.ItemArray.GetValue(columna).ToString();
                        }
                        catch (Exception)
                        {
                        }
                    }

                    columna = 95;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvpESAP.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 96;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvValorESAP.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 97;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvpMEN.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 98;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txvValorMEN.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 99;
                    if (registros.Row.ItemArray.GetValue(columna).ToString() != "N")
                        chkExoneraSalud.Checked = true;
                    else
                        chkExoneraSalud.Checked = false;
                    columna = 100;
                    if (registros.Row.ItemArray.GetValue(columna) != null || registros.Row.ItemArray.GetValue(columna).ToString() != "")
                        ddlTipoIdUPC.SelectedValue = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 101;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        txtIdentificacionUPC.Text = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 102;
                    cargarContrato();
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        ddlContratos.SelectedValue = registros.Row.ItemArray.GetValue(columna).ToString();
                    columna = 103;
                    if (registros.Row.ItemArray.GetValue(columna) != null)
                        ddlTipoId.SelectedValue = registros.Row.ItemArray.GetValue(columna).ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }
        protected void nibtnGenerarPlano_Click(object sender, EventArgs e)
        {
            if (nitxvAño.Text.Length == 0)
            {
                nilblInformacion.Text = "Debe digitar el año";
                return;
            }
            string script = "<script language='javascript'>" +
                             "VisualizacionPlano(" + Convert.ToString(this.Session["empresa"]) + "," + Convert.ToString(nitxvAño.Text) + "," +
                             Convert.ToString(niddlMes.SelectedValue) + ")</script>";
            Page.RegisterStartupScript("VisualizacionPlano", script);

        }
        #endregion Eventos



        protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            validaPeriodoAbierto();
        }

        private void validaPeriodoAbierto()
        {
            if (nitxvAño.Text.Length == 0 || niddlMes.SelectedValue.Length == 0)
            {
                return;
            }
            verificaPeriodoCerradoContable(Convert.ToInt32(nitxvAño.Text), Convert.ToInt32(niddlMes.SelectedValue), Convert.ToInt16(Session["empresa"]));

        }

        protected void txvAño_TextChanged(object sender, EventArgs e)
        {
            validaPeriodoAbierto();
        }
        protected void ddlEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarContrato();
            retornaDatosContrato();

        }

        private void cargarContrato()
        {
            try
            {
                DataView dvContratos = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("nContratos", "ppa"), "id", Convert.ToInt16(Session["empresa"]));
                dvContratos.RowFilter = "tercero=" + ddlEmpleado.SelectedValue.ToString() + " and empresa = " + Session["empresa"].ToString();
                this.ddlContratos.DataSource = dvContratos;
                this.ddlContratos.DataValueField = "id";
                this.ddlContratos.DataTextField = "id";
                this.ddlContratos.DataBind();
                this.ddlContratos.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar contratos. Correspondiente a: " + ex.Message, "C");
            }
        }

        private void retornaDatosContrato()
        {
            try
            {
                if (ddlEmpleado.SelectedValue.Length > 0 && ddlContratos.SelectedValue.Length > 0)
                {
                    DataView dvContrato = contratos.RetornaDatosContratoSS(Convert.ToInt16(Session["empresa"]), ddlEmpleado.SelectedValue, Convert.ToInt16(ddlContratos.SelectedValue));
                    foreach (DataRowView registro in dvContrato)
                    {
                        ddlTipoId.SelectedValue = HttpUtility.HtmlDecode(registro.Row.ItemArray.GetValue(0).ToString().Trim());
                        txtCodigoTercero.Text = HttpUtility.HtmlDecode(registro.Row.ItemArray.GetValue(1).ToString());
                        txtIdentificacion.Text = HttpUtility.HtmlDecode(registro.Row.ItemArray.GetValue(2).ToString());
                        txtApellido1.Text = HttpUtility.HtmlDecode(registro.Row.ItemArray.GetValue(3).ToString());
                        txtApellido2.Text = HttpUtility.HtmlDecode(registro.Row.ItemArray.GetValue(4).ToString());
                        txtNombre1.Text = HttpUtility.HtmlDecode(registro.Row.ItemArray.GetValue(5).ToString());
                        txtNombre2.Text = HttpUtility.HtmlDecode(registro.Row.ItemArray.GetValue(6).ToString());
                        ddlDepartamento.SelectedValue = HttpUtility.HtmlDecode(registro.Row.ItemArray.GetValue(7).ToString());
                        cargarCiudad(ddlDepartamento.SelectedValue);
                        ddlCiudad.SelectedValue = HttpUtility.HtmlDecode(registro.Row.ItemArray.GetValue(8).ToString());
                        ddlTipoCotizante.SelectedValue = HttpUtility.HtmlDecode(registro.Row.ItemArray.GetValue(9).ToString());
                        ddlSubTipoCotizante.SelectedValue = HttpUtility.HtmlDecode(registro.Row.ItemArray.GetValue(10).ToString());
                        txvSalario.Text = decimal.Round(Convert.ToDecimal(HttpUtility.HtmlDecode(registro.Row.ItemArray.GetValue(11).ToString())), 2).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar contratos. Correspondiente a: " + ex.Message, "C");
            }
        }
        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarCiudad(ddlDepartamento.SelectedValue);
        }

        private void cargarCiudad(string departamento)
        {
            try
            {
                if (ddlDepartamento.SelectedValue.Length > 0)
                {
                    DataView ciudad = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("gCiudad", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"]));
                    ciudad.RowFilter = "empresa =" + this.Session["empresa"].ToString() + " and departamento='" + departamento + "'";
                    this.ddlCiudad.DataSource = ciudad;
                    this.ddlCiudad.DataValueField = "codigo";
                    this.ddlCiudad.DataTextField = "descripcion";
                    this.ddlCiudad.DataBind();
                    this.ddlCiudad.Items.Insert(0, new ListItem("", ""));
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar ciudades. Correspondiente a: " + ex.Message, "C");
            }
        }


        protected void ddlContratos_SelectedIndexChanged(object sender, EventArgs e)
        {
            retornaDatosContrato();
        }

        private void validaFormatoFecha(string fecha)
        {
            try
            {
                Convert.ToDateTime(fecha);
            }
            catch
            {
                nilblInformacion.Text = "Formato de fecha no valido";
                return;
            }
        }
        protected void txtFechaRadExtrajero_TextChanged(object sender, EventArgs e)
        {
            validaFormatoFecha(txtFechaRadExtrajero.Text);
        }
        protected void txtFechaIngreso_TextChanged(object sender, EventArgs e)
        {
            validaFormatoFecha(txtFechaIngreso.Text);
        }
        protected void txtFechaRetiro_TextChanged(object sender, EventArgs e)
        {
            validaFormatoFecha(txtFechaIngreso.Text);
        }
        protected void txtFechaInicialAVP_TextChanged(object sender, EventArgs e)
        {
            validaFormatoFecha(txtFechaVSP.Text);
        }
        protected void txtFechaInicialIGE_TextChanged(object sender, EventArgs e)
        {
            validaFormatoFecha(txtFechaInicialIGE.Text);
        }
        protected void txtFechaInicialVacaciones_TextChanged(object sender, EventArgs e)
        {
            validaFormatoFecha(txtFechaInicialVacaciones.Text);
        }
        protected void txtFechaInicialIRL_TextChanged(object sender, EventArgs e)
        {
            validaFormatoFecha(txtFechaInicialIRL.Text);
        }
        protected void txtFechaFinalIGE_TextChanged(object sender, EventArgs e)
        {
            validaFormatoFecha(txtFechaFinalIGE.Text);
        }
        protected void txtFechaFinalVacaciones_TextChanged(object sender, EventArgs e)
        {
            validaFormatoFecha(txtFechaFinalVacaciones.Text);
        }
        protected void txtFechaFinalIRL_TextChanged(object sender, EventArgs e)
        {
            validaFormatoFecha(txtFechaFinalIRL.Text);
        }
        protected void txtFechaInicialSLN_TextChanged(object sender, EventArgs e)
        {
            validaFormatoFecha(txtFechaInicialSLN.Text);
        }
        protected void txtFechaInicialLMA_TextChanged(object sender, EventArgs e)
        {
            validaFormatoFecha(txtFechaInicialLMA.Text);
        }
        protected void txtFechaInicialVCT_TextChanged(object sender, EventArgs e)
        {
            validaFormatoFecha(txtFechaInicialVCT.Text);
        }
        protected void txtFechaFinalSLN_TextChanged(object sender, EventArgs e)
        {
            validaFormatoFecha(txtFechaFinalSLN.Text);
        }
        protected void txtFechaFinalLMA_TextChanged(object sender, EventArgs e)
        {
            validaFormatoFecha(txtFechaFinalLMA.Text);
        }
        protected void txtFechaFinalVCT_TextChanged(object sender, EventArgs e)
        {
            validaFormatoFecha(txtFechaFinalVCT.Text);
        }
        protected void txtFechaRadExtrajero_TextChanged1(object sender, EventArgs e)
        {
            validaFormatoFecha(txtFechaRadExtrajero.Text);
        }


    }
}