using Contabilidad.WebForms.App_Code.Administracion;
using Contabilidad.WebForms.App_Code.CuentasxCobrar;
using Contabilidad.WebForms.App_Code.General;
using Contabilidad.WebForms.App_Code.Presupuesto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.Ppresupuesto
{
    public partial class Parametrizacion : BasePage
    {
        #region Instancias

        Ccaracteristica caracteristicas = new Ccaracteristica();
        Ctransaccion transaccion = new Ctransaccion();
        Ctransacciones transacciones = new Ctransacciones();
        Citems citems = new Citems();
        CtipoTransaccion CtipoTransaccion = new CtipoTransaccion();

        #endregion Instancias

        #region Funciones


        private void Cancelar()
        {

            this.gvLista.Visible = true;
            this.nilbAsociarVariable.Visible = true;
            this.pAsociacion.Visible = false;
            this.ddlMovimientos.SelectedValue = "";

            this.chkResultado.Checked = false;
            this.pFormula.Visible = false;
            this.ddlAnalisisF.SelectedValue = "";
            this.txtConstante.Text = "";
            this.txtFormula.Text = "";
            this.lblExpresion.Text = "";
            this.lblResultadoFormula.Text = "";
            this.hdConteoItems.Value = "0";
            this.ddlItemsRetornaDatos.DataSource = null;
            this.ddlItemsRetornaDatos.DataBind();
            this.btnRegistrar.Visible = false;
            this.btnCancelar.Visible = false;
            cargarProducto();
            niddlFormulacion.ClearSelection();
            niddlTipoTransaccion.ClearSelection();
            niddlPeriodicidad.SelectedIndex = 1;
        }
        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");

            CcontrolesUsuario.InhabilitarControles(Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);
            seguridad.InsertaLog(Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + Page.ToString(),
                "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
        }
        private void CargaItemsRetornaDatos(int orden)
        {


            try
            {
                this.ddlItemsRetornaDatos.DataSource = transaccion.GetItemRetornaDatosLaboratorio(
                    orden, Convert.ToInt16(Session["empresa"]));
                this.ddlItemsRetornaDatos.DataValueField = "codigo";
                this.ddlItemsRetornaDatos.DataTextField = "descripcion";
                this.ddlItemsRetornaDatos.DataBind();
                this.ddlItemsRetornaDatos.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private void VerificaFormula()
        {
            string expresion = "";

            try
            {
                transaccion.VerificaFormulaPresupuesto(this.niddlFormulacion.SelectedValue, this.txtFormula.Text.Trim(), "V", out expresion, Convert.ToInt16(Session["empresa"]),
                    DateTime.Now.Year, DateTime.Now.Month
                    );

                this.lblResultadoFormula.ForeColor = System.Drawing.Color.Navy;
                this.lblResultadoFormula.Text = expresion;
            }
            catch (Exception ex)
            {

                ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private void CargaCombosAsociacion()
        {
            try
            {
                string modulo = ConfigurationManager.AppSettings["modulo"].ToString();

                this.ddlAnalisisF.DataSource = DvProductoMovimiento(
                    this.niddlFormulacion.SelectedValue, modulo);
                this.ddlAnalisisF.DataValueField = "codigo";
                this.ddlAnalisisF.DataTextField = "descripcion";
                this.ddlAnalisisF.DataBind();
                this.ddlAnalisisF.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private DataView DvProductoMovimiento(string producto, string modulo)
        {
            try
            {
                DataView dvVariables = caracteristicas.GetMovimientoProductoContable(
                    producto: niddlFormulacion.SelectedValue.Trim(),
                    periodicidad: niddlPeriodicidad.SelectedValue.Trim(),
                    tipotransaccion: niddlTipoTransaccion.SelectedValue.Trim(),
                    empresa: Convert.ToInt16(Session["empresa"]),
                    modulo: modulo
                    ).Tables[0].DefaultView;

                return dvVariables;
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
                return null;
            }
        }
        private void EntidadKey()
        {

            string modulo = ConfigurationManager.AppSettings["modulo"].ToString();
            try
            {
                object[] objKey = new object[] {
                 Convert.ToInt16(Session["empresa"]),    //// @empresa	int
                   modulo,   //@modulo	varchar
                    ddlMovimientos.SelectedValue.Trim().ToString(), //@movimiento	varchar
                   niddlPeriodicidad.SelectedValue.Trim(),  //@periodicidad	varchar
                   niddlFormulacion.SelectedValue, //@producto	varchar
                   niddlTipoTransaccion.SelectedValue //@tipotransaccion	varchar
            };

                if (CentidadMetodos.EntidadGetKey(
                    "pProductoMovimientoContable",
                    "ppa",
                    objKey).Tables[0].Rows.Count > 0)
                {

                    ManejoError("Movimiento " + this.ddlMovimientos.SelectedItem.Text + " ya se encuentra asociado al producto " + niddlFormulacion.SelectedItem.Text, insertar);

                    Cancelar();

                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al validar la llave primaria correspondiente a: " + ex.Message, "C");
            }
        }

        private void CargarMovimientos()
        {

            try
            {

                CargaCombosAsociacion();

                this.gvLista.Visible = true;
                this.nilbAsociarVariable.Visible = true;
                this.pAsociacion.Visible = false;
                this.ddlMovimientos.SelectedValue = "";

                this.chkResultado.Checked = false;
                this.pFormula.Visible = false;
                this.ddlAnalisisF.SelectedValue = "";
                this.txtConstante.Text = "";
                this.txtFormula.Text = "";
                this.lblExpresion.Text = "";
                this.lblResultadoFormula.Text = "";

                this.nilbAsociarVariable.Visible = true;

                CargarProductoMovimientos();


                CcontrolesUsuario.InhabilitarControles(
                    this.Page.Controls);

                this.gvLista.Visible = true;
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private void CargaMovimientosAsociados()
        {
            try
            {
                DataView dvConcepto = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("iItems", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                dvConcepto.RowFilter = "tipo in ('MP') and empresa=" + Session["empresa"].ToString();
                this.ddlMovimientos.DataSource = dvConcepto;
                this.ddlMovimientos.DataValueField = "codigo";
                this.ddlMovimientos.DataTextField = "descripcion";
                this.ddlMovimientos.DataBind();
                this.ddlMovimientos.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {

                ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private void CargarProductoMovimientos()
        {
            string modulo = ConfigurationManager.AppSettings["modulo"].ToString();

            try
            {
                this.gvLista.DataSource = caracteristicas.GetMovimientoProductoContable(
                   producto: niddlFormulacion.SelectedValue,
                   periodicidad: niddlPeriodicidad.SelectedValue,
                   tipotransaccion: niddlTipoTransaccion.SelectedValue,
                   empresa: Convert.ToInt16(Session["empresa"]),
                   modulo: modulo
                    ).Tables[0].DefaultView;
                this.gvLista.DataBind();

                if (this.gvLista.Rows.Count == 0)
                {
                    ManejoError("Elemento sin características definidas", insertar);
                }

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }
        void cargarProducto()
        {

            try
            {
                DataView dvFormulacion = citems.ItemsPresupuesto(Convert.ToInt16(Session["empresa"]));
                this.niddlFormulacion.DataSource = dvFormulacion;
                this.niddlFormulacion.DataValueField = "codigo";
                this.niddlFormulacion.DataTextField = "descripcion";
                this.niddlFormulacion.DataBind();
                this.niddlFormulacion.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }

            try
            {
                DataView dvFormulacion = CtipoTransaccion.GetTipoTransaccionModuloContable(Convert.ToInt16(Session["empresa"]));
                this.niddlTipoTransaccion.DataSource = dvFormulacion;
                this.niddlTipoTransaccion.DataValueField = "codigo";
                this.niddlTipoTransaccion.DataTextField = "descripcion";
                this.niddlTipoTransaccion.DataBind();
                this.niddlTipoTransaccion.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }

        }

        #endregion Funciones

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
            {
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            }
            else
            {
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(),
                    ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) != 0)
                {

                    if (!IsPostBack)
                    {
                        cargarProducto();
                        btnRegistrar.Visible = false;
                        niimbBuscar.Visible = false;
                        CcontrolesUsuario.HabilitarControles(Page.Controls);
                    }
                    this.niddlFormulacion.Focus();

                }
                else
                {
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
                }


            }
        }
        protected void nilbAsociarCaracteristica_Click(object sender, EventArgs e)
        {
            CargaMovimientosAsociados();
            CargaCombosAsociacion();

            this.pAsociacion.Visible = true;
            this.nilbAsociarVariable.Visible = false;
            this.pAsociacion.Visible = true;
            this.txtOrden.Text = "";
            this.chkResultado.Checked = false;
            this.pFormula.Visible = false;
            this.ddlAnalisisF.SelectedValue = "";
            this.txtConstante.Text = "";
            this.txtFormula.Text = "";
            this.lblExpresion.Text = "";
            this.lblResultadoFormula.Text = "";
            this.btnRegistrar.Visible = true;
            this.btnCancelar.Visible = true;
            this.Session["editar"] = false;

        }

        protected void chkResultadoProduccion_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void imbAddSentencia_Click(object sender, EventArgs e)
        {
            if (this.ddlSentencia.SelectedValue.Trim().Length != 0)
            {
                if (this.ddlSentencia.SelectedValue.StartsWith("dbo"))
                {
                    this.txtFormula.Text = this.txtFormula.Text.Trim() + "|F" + this.ddlSentencia.SelectedValue.Trim();
                    CargaItemsRetornaDatos(1);
                }
                else
                    this.txtFormula.Text = this.txtFormula.Text.Trim() + "|S" + this.ddlSentencia.SelectedValue.Trim() + "|";

                this.lblExpresion.Text = this.lblExpresion.Text + this.ddlSentencia.SelectedValue;
                this.ddlSentencia.SelectedIndex = 0;
            }
        }

        protected void imbAddVariable_Click(object sender, EventArgs e)
        {


            if (this.txtFormula.Text.Trim().StartsWith("|Ldbo.fRetornaDeTabla("))
            {
                if (Convert.ToInt16(this.hdConteoItems.Value) == 4)
                {
                    this.txtFormula.Text = this.txtFormula.Text.Trim() + "|V(" + this.ddlAnalisisF.SelectedValue.Trim() + ")|";
                    this.lblExpresion.Text = this.lblExpresion.Text + this.ddlAnalisisF.SelectedItem;
                    this.ddlAnalisisF.SelectedIndex = 0;
                    this.ddlItemsRetornaDatos.DataSource = null;
                    this.ddlItemsRetornaDatos.DataBind();
                    this.txtFormula.Text = this.txtFormula.Text + "|S)|";
                    this.lblExpresion.Text = this.lblExpresion.Text + ")";
                }


            }
            else if (this.ddlAnalisisF.SelectedValue.Trim().Length != 0)
            {
                this.txtFormula.Text = this.txtFormula.Text.Trim() + "|V(" + this.ddlAnalisisF.SelectedValue.Trim() + ")|";
                this.lblExpresion.Text = this.lblExpresion.Text + this.ddlAnalisisF.SelectedItem;
                this.ddlAnalisisF.SelectedIndex = 0;
            }
        }

        protected void imbAddConstante_Click(object sender, EventArgs e)
        {
            if (this.txtConstante.Text.Trim().Length != 0)
            {
                this.txtFormula.Text = this.txtFormula.Text.Trim() + "|N" + this.txtConstante.Text.Trim() + "|";
                this.lblExpresion.Text = this.lblExpresion.Text + this.txtConstante.Text;
                this.txtConstante.Text = "";
            }
        }

        protected void imbUndo_Click(object sender, EventArgs e)
        {
            if (this.txtFormula.Text.Trim().Length > 0)
            {
                this.txtFormula.Text = "";
                this.lblExpresion.Text = "";
                this.hdConteoItems.Value = "0";
            }
        }

        protected void imbValidarFormula_Click(object sender, EventArgs e)
        {
            VerificaFormula();
        }


        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string operacion = "elimina";
            string modulo = ConfigurationManager.AppSettings["modulo"].ToString();

            try
            {
                object[] objValores = new object[] {
                       Convert.ToInt16(Session["empresa"]),  //@empresa    int
                       modulo, //@modulo varchar
                        Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)),  //@movimiento varchar
                       niddlPeriodicidad.SelectedValue, //@periocidad varchar
                       niddlFormulacion.SelectedValue, //@producto   varchar
                       niddlTipoTransaccion.SelectedValue //@tipotransaccion    varchar
            };

                if (CentidadMetodos.EntidadInsertUpdateDelete(
                    "pProductoMovimientoContable",
                    operacion,
                    "ppa",
                    objValores) == 0)
                {
                    CargarProductoMovimientos();
                    ManejoExito("Datos eliminados satisfactoriamente", "E");
                }
                else
                {
                    ManejoError("Errores al eliminar el registro. Operación no realizada", "E");
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
            }
        }
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session["editar"] = true;
            CargaMovimientosAsociados();
            CargaCombosAsociacion();
            string formula = "", expresion = "";
            try
            {

                string modulo = ConfigurationManager.AppSettings["modulo"].ToString();


                object[] objValores = new object[] {
                       Convert.ToInt16(Session["empresa"]),     //                @empresa    int
                         modulo,   //@modulo varchar
                         this.gvLista.SelectedRow.Cells[2].Text.Trim(),   //@movimiento varchar
                         this.gvLista.SelectedRow.Cells[13].Text.Trim(),    //@periodicidad   varchar
                           niddlFormulacion.SelectedValue.Trim(),  //@producto   varchar
                         niddlTipoTransaccion.SelectedValue   //@tipotransaccion    varchar
                 };

                object[] objVariables = transaccion.DatosProductoMovmineto(
                    objValores);
                formula = Convert.ToString(objVariables.GetValue(3));


                if (Convert.ToBoolean(objVariables.GetValue(6)) == true)
                {
                    ddlMovimientos.SelectedValue = objVariables.GetValue(2).ToString();
                    pAsociacion.Visible = true;
                    chkResultado.Checked = true;
                    this.txtOrden.Text = objVariables.GetValue(5).ToString();
                    chkDecimal.Checked = Convert.ToBoolean(objVariables.GetValue(9));
                    this.chkCalcular.Checked = Convert.ToBoolean(objVariables.GetValue(8));
                    pFormula.Visible = true;
                    this.txvPrioridad.Text = objVariables.GetValue(4).ToString();
                    this.txtFormula.Text = objVariables.GetValue(3).ToString();
                    this.chkMostrarInforme.Checked = Convert.ToBoolean(objVariables.GetValue(10));
                    chkAlmacena.Checked = Convert.ToBoolean(objVariables.GetValue(7));
                    chkActivo.Checked = Convert.ToBoolean(objVariables.GetValue(11));
                    lblExpresion.Text = objVariables.GetValue(13).ToString();
                    chkUtilizarEjecuatado.Checked = Convert.ToBoolean(objVariables.GetValue(17));


                }
                else
                {
                    pAsociacion.Visible = true;
                    ddlMovimientos.SelectedValue = objVariables.GetValue(2).ToString();
                    chkResultado.Checked = false;
                    this.txtOrden.Text = objVariables.GetValue(5).ToString();
                    chkDecimal.Checked = Convert.ToBoolean(objVariables.GetValue(9));
                    this.chkCalcular.Checked = Convert.ToBoolean(objVariables.GetValue(8));
                    this.chkMostrarInforme.Checked = Convert.ToBoolean(objVariables.GetValue(10));
                    chkAlmacena.Checked = Convert.ToBoolean(objVariables.GetValue(7));
                    pFormula.Visible = false;
                    chkActivo.Checked = Convert.ToBoolean(objVariables.GetValue(11));
                    lblExpresion.Text = objVariables.GetValue(13).ToString();
                    chkUtilizarEjecuatado.Checked = Convert.ToBoolean(objVariables.GetValue(17));

                }


                if (formula.Trim().Length != 0)
                    transaccion.VerificaFormulaPresupuesto(niddlFormulacion.SelectedValue, formula, "V", out expresion, Convert.ToInt16(Session["empresa"]), DateTime.Now.Year, DateTime.Now.Month);

                this.btnCancelar.Visible = true;
                this.btnRegistrar.Visible = true;
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }

        protected void imbItems_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(this.hdConteoItems.Value) == 4)
            {
                this.hdConteoItems.Value = "0";
            }

            if (hdRetornaDatos.Value == "dbo.fRetornaDatos(")
            {
                if (this.ddlItemsRetornaDatos.SelectedValue.Trim().Length != 0)
                {
                    this.txtFormula.Text = this.txtFormula.Text.Trim() + "'" + this.ddlItemsRetornaDatos.SelectedValue.Trim() + "'";
                    this.lblExpresion.Text = this.lblExpresion.Text + this.ddlItemsRetornaDatos.SelectedValue;
                    this.hdConteoItems.Value = Convert.ToString(Convert.ToInt16(this.hdConteoItems.Value) + 1);

                    if (Convert.ToInt16(this.hdConteoItems.Value) == 4)
                    {
                        this.ddlItemsRetornaDatos.DataSource = null;
                        this.ddlItemsRetornaDatos.DataBind();
                        this.txtFormula.Text = this.txtFormula.Text + ",'" + Convert.ToString(Session["empresa"]) + "'||S)|";
                        this.lblExpresion.Text = this.lblExpresion.Text + ")";
                    }
                    else
                    {
                        CargaItemsRetornaDatos(Convert.ToInt16(this.hdConteoItems.Value) + 1);

                        this.ddlItemsRetornaDatos.SelectedIndex = 0;
                        this.txtFormula.Text = this.txtFormula.Text + ",";
                        this.lblExpresion.Text = this.lblExpresion.Text + ",";

                        if (Convert.ToInt16(this.hdConteoItems.Value) == 3 && this.txtFormula.Text.Trim().StartsWith("|Ldbo.fRetornaDeTabla("))
                        {
                            this.txtFormula.Text = this.txtFormula.Text + "'" + Convert.ToString(Session["empresa"]) + "',|";
                            this.ddlItemsRetornaDatos.DataSource = null;
                            this.ddlItemsRetornaDatos.DataBind();
                        }
                    }
                }
            }
            else
            {

                if (this.ddlItemsRetornaDatos.SelectedValue.Trim().Length != 0)
                {
                    this.txtFormula.Text = this.txtFormula.Text.Trim() + "'" + this.ddlItemsRetornaDatos.SelectedValue.Trim() + "'";
                    this.lblExpresion.Text = this.lblExpresion.Text + this.ddlItemsRetornaDatos.SelectedItem.Text;
                    this.hdConteoItems.Value = Convert.ToString(Convert.ToInt16(this.hdConteoItems.Value) + 1);

                    if (Convert.ToInt16(this.hdConteoItems.Value) == 4)
                    {
                        this.ddlItemsRetornaDatos.DataSource = null;
                        this.ddlItemsRetornaDatos.DataBind();
                        this.txtFormula.Text = this.txtFormula.Text + ",'AÑO'" + ",'" + Convert.ToString(Session["empresa"]) + "'||S)|";
                        this.lblExpresion.Text = this.lblExpresion.Text + ")";
                    }
                    else
                    {
                        CargaItemsRetornaDatos(Convert.ToInt16(this.hdConteoItems.Value) + 1);

                        this.ddlItemsRetornaDatos.SelectedIndex = 0;
                        this.txtFormula.Text = this.txtFormula.Text + ",";
                        this.lblExpresion.Text = this.lblExpresion.Text + ",";
                    }
                }
            }
        }


        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (niddlFormulacion.SelectedValue.Trim().Length > 0 && niddlTipoTransaccion.SelectedValue.Trim().Length > 0)
                CargarMovimientos();
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {

            string formula = "";
            int prioridad = 0, orden = 0;

            if (this.ddlMovimientos.SelectedValue.Trim().Length == 0)
            {
                ManejoError("Campos vacios. Por favor corrija", insertar);
                return;
            }

            if (chkResultado.Checked == true)
            {
                if (this.txtFormula.Text.Trim().Length == 0)
                {
                    ManejoError("Campos vacios. Por favor corrija", insertar);
                    return;
                }
                else
                {
                    VerificaFormula();

                    if (this.lblResultadoFormula.ForeColor == System.Drawing.Color.Orange)
                    {
                        return;
                    }
                    else
                    {
                        formula = this.txtFormula.Text.Trim();
                    }
                }
            }

            if (this.txtOrden.Text.Trim().Length == 0)
            {
                ManejoError("Campos vacios. Por favor corrija", insertar);
                return;
            }
            else
            {
                orden = Convert.ToInt16(this.txtOrden.Text);
            }

            if (this.txvPrioridad.Text.Trim().Length == 0)
            {
                prioridad = 0;
            }
            else
            {
                prioridad = Convert.ToInt16(this.txvPrioridad.Text);
            }

            string operacion = "inserta";
            string modulo = ConfigurationManager.AppSettings["modulo"].ToString();


            object[] objValores = new object[]{
                       chkActivo.Checked,  //  @activo bit
                        this.chkAlmacena.Checked,  //@almacena   bit
                       "999", //@certificado    varchar
                       Convert.ToUInt16(Session["empresa"]), //@empresa    int
                        lblExpresion.Text, //@expresion  varchar
                       formula, //@formula    varchar
                        chkCalcular.Checked, //@mCalcular  bit
                         this.chkDecimal.Checked,//@mDecimal   bit
                        this.chkMostrarInforme.Checked,//@mInforme   bit
                        modulo, //@modulo varchar
                        this.ddlMovimientos.SelectedValue.Trim(), //@movimiento varchar
                        orden,//@orden  int
                        niddlPeriodicidad.SelectedValue.Trim(), //@perioricidad   varchar
                       prioridad,  //@prioridad  int
                       this.niddlFormulacion.SelectedValue,  //@producto   varchar
                       this.chkResultado.Checked, //@resultado  bit
                        niddlTipoTransaccion.SelectedValue.Trim(), //@tipoTransaccion    varchar
                        chkUtilizarEjecuatado.Checked
        };

            try
            {

                if (Convert.ToBoolean(this.Session["editar"]) == true)
                {
                    operacion = "actualiza";
                }
                switch (CentidadMetodos.EntidadInsertUpdateDelete(
                    "pProductoMovimientoContable",
                    operacion,
                    "ppa",
                    objValores))
                {
                    case 0:

                        CargaCombosAsociacion();
                        CargarProductoMovimientos();

                        ManejoExito("Registro guardado exitosamente !!", "I");
                        this.gvLista.Visible = true;
                        this.ddlMovimientos.SelectedValue = "";
                        this.chkResultado.Checked = false;
                        this.chkAlmacena.Checked = false;
                        this.pFormula.Visible = false;
                        this.ddlAnalisisF.SelectedValue = "";
                        this.txtConstante.Text = "";
                        this.txtFormula.Text = "";
                        this.lblExpresion.Text = "";
                        this.lblResultadoFormula.Text = "";
                        this.btnRegistrar.Visible = false;
                        this.btnCancelar.Visible = false;
                        this.pAsociacion.Visible = false;
                        this.nilbAsociarVariable.Visible = true;
                        break;

                    case 1:

                        ManejoError("Error al guardar el registro. Operación no realizada", "I");
                        break;
                }

            }
            catch (Exception ex)
            {
                ManejoError("Error al insertar el registro. Correspondiente a: " + ex.Message, "I");
            }
        }


        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CargaCombosAsociacion();
            Cancelar();
            CargarProductoMovimientos();
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            CargarMovimientos();
            gvLista.DataBind();
        }
        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            if (niddlFormulacion.SelectedValue.Trim().Length > 0)
                CargarMovimientos();
        }

        protected void ddlVariable_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntidadKey();
            txtOrden.Focus();
        }

        #endregion Eventos



        protected void chkResultado_CheckedChanged(object sender, EventArgs e)
        {
            this.chkResultado.Focus();

            if (this.chkResultado.Checked == true)
            {
                this.pFormula.Visible = true;

            }
            else
            {
                this.pFormula.Visible = false;
            }
        }
    }
}