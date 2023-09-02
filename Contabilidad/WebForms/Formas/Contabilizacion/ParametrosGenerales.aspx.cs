using Contabilidad.seguridadInfos;
using Contabilidad.WebForms.App_Code.Administracion;
using Contabilidad.WebForms.App_Code.Contabilizacion;
using Contabilidad.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.Contabilizacion
{
    public partial class ParametrosGenerales : BasePage
    {
        #region Instancias

        Security seguridad = new Security();
        cClaseParametro clase = new cClaseParametro();
        Cgeneral general = new Cgeneral();
        CtipoTransaccion tipoTransaccion = new CtipoTransaccion();
        cParametrosGenerales parametrosGenerales = new cParametrosGenerales();

        #endregion Instancias

        #region Metodos



        private void conceptosContratistas()
        {
            try
            {
                this.ddlConcepto.DataSource = parametrosGenerales.RetornaConceptosLaboresxCentroCosto(Convert.ToInt32(this.Session["empresa"]), "C  ");
                this.ddlConcepto.DataValueField = "codigo";
                this.ddlConcepto.DataTextField = "descripcion";
                this.ddlConcepto.DataBind();
                this.ddlConcepto.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los centro de costo de nomina para contabilización. Correspondiente a: " + ex.Message, "C");
            }
        }

        private void verificaClaseConcepto()
        {
            try
            {
                this.ddlConcepto.DataSource = parametrosGenerales.RetornaConceptosLaboresxCentroCosto(Convert.ToInt32(this.Session["empresa"]), "C  ");
                this.ddlConcepto.DataValueField = "codigo";
                this.ddlConcepto.DataTextField = "descripcion";
                this.ddlConcepto.DataBind();
                this.ddlConcepto.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los centro de costo de nomina para contabilización. Correspondiente a: " + ex.Message, "C");
            }
        }

        private void cargarTercerosCredito()
        {

            try
            {
                DataView dvClase = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cTercero", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"]));
                this.ddlTerceroCredito.DataSource = dvClase;
                this.ddlTerceroCredito.DataValueField = "codigo";
                this.ddlTerceroCredito.DataTextField = "descripcion";
                this.ddlTerceroCredito.DataBind();
                this.ddlTerceroCredito.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar clase de parametro para contabilización. Correspondiente a: " + ex.Message, "C");
            }
        }


        private void cargarTerceros()
        {
            try
            {
                DataView dvClase = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cTercero", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"]));
                this.ddlTercero.DataSource = dvClase;
                this.ddlTercero.DataValueField = "codigo";
                this.ddlTercero.DataTextField = "descripcion";
                this.ddlTercero.DataBind();
                this.ddlTercero.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar clase de parametro para contabilización. Correspondiente a: " + ex.Message, "C");
            }
        }
        private void manejoDepartamento()
        {
            if (chkManejaDepartamento.Checked == true)
            {

                if (ddlCentroCostoNomi.SelectedValue.Trim().Length > 0)
                {

                    DataView dvClase = CentidadMetodos.EntidadGet("nDepartamento", "ppa").Tables[0].DefaultView;
                    dvClase.RowFilter = "empresa=" + Convert.ToString(this.Session["empresa"]) + " and " + " ccosto='" + ddlCentroCostoNomi.SelectedValue.Trim() + "'";
                    dvClase.Sort = "descripcion";
                    this.ddlDepartamento.DataSource = dvClase;
                    this.ddlDepartamento.DataValueField = "codigo";
                    this.ddlDepartamento.DataTextField = "descripcion";
                    this.ddlDepartamento.DataBind();
                    this.ddlDepartamento.Items.Insert(0, new ListItem("", ""));
                    ddlDepartamento.Enabled = true;
                }
                else
                {
                    nilblInformacion.Text = "Debe seleccionar un centro de costo valido";
                    ddlDepartamento.Enabled = false;

                }
            }
            else
            {
                ddlDepartamento.SelectedValue = "";
                ddlDepartamento.Enabled = false;
            }
        }
        private void manejoContratista()
        {
            switch (ddlTipo.SelectedValue)
            {
                case "PE":
                    ddlCuentaContratista.Visible = false;
                    lblCuentaContratista.Visible = false;
                    ddlCuentaContratista.DataSource = null;
                    ddlCuentaContratista.DataBind();

                    break;
                case "PC":
                    ddlCuentaContratista.Visible = true;
                    lblCuentaContratista.Visible = true;

                    break;
            }
        }

        private void manejoTipoCantidad()
        {

            if (rblTipoCantidad.SelectedValue == "1")
            {
                txvValor.Enabled = false;
                txvValor.Text = "0";
            }
            else
            {
                txvValor.Enabled = true;
                txvValor.Text = "0";
                txvValor.Focus();
            }
        }


        private void manejoEntidad()
        {
            int validaSeguridadSocial = 0;

            try
            {
                validaSeguridadSocial = parametrosGenerales.VerificaClaseSeguridadSocial(ddlClase.SelectedValue, Convert.ToInt32(this.Session["empresa"]));
            }
            catch (Exception ex)
            {

                ManejoError("Error al validar clase seguridad social debido a:  " + ex.Message, "C");

            }


            if (chkManejaEntidad.Checked == true & validaSeguridadSocial == 0)
            {
                ddlEntidad.Enabled = true;
                ddlCuentaContratista.Enabled = false;
                ddlCuentaContable.Enabled = false;
                ddlCuentaGasto.Enabled = false;
                ddlCentroCosto.Enabled = false;
                ddlCuentaCredito.Enabled = false;
                ddlCentroCostoCredito.Enabled = false;
                ddlCentroCostoMayor.Enabled = false;
                ddlCcostoMayorCredito.Enabled = false;
                ddlCentroCosto.Enabled = false;
                ddlCuentaContratista.SelectedValue = "";
                ddlCuentaGasto.SelectedValue = "";
                ddlCuentaContable.SelectedValue = "";
                ddlCuentaCredito.SelectedValue = "";
                ddlCentroCostoCredito.SelectedValue = "";
                ddlCentroCostoMayor.SelectedValue = "";
                ddlCcostoMayorCredito.SelectedValue = "";
                ddlCuentaGasto.SelectedValue = "";
                ddlCentroCosto.SelectedValue = "";

            }
            else if (chkManejaEntidad.Checked == true & validaSeguridadSocial == 1)
            {
                ddlCuentaContratista.Enabled = true;
                ddlCuentaGasto.Enabled = true;
                ddlCuentaContable.Enabled = true;
                ddlCuentaCredito.Enabled = true;
                ddlCentroCostoCredito.Enabled = true;
                ddlCentroCostoMayor.Enabled = true;
                ddlCcostoMayorCredito.Enabled = true;
                ddlEntidad.Enabled = true;
                ddlCuentaGasto.Enabled = true;
                ddlCentroCosto.Enabled = true;

            }
            else
            {
                ddlCuentaContratista.Enabled = true;
                ddlCuentaGasto.Enabled = true;
                ddlCuentaContable.Enabled = true;
                ddlCuentaCredito.Enabled = true;
                ddlCentroCostoCredito.Enabled = true;
                ddlCentroCostoMayor.Enabled = true;
                ddlCcostoMayorCredito.Enabled = true;
                ddlEntidad.Enabled = false;
                ddlCuentaGasto.Enabled = true;
                ddlCentroCosto.Enabled = true;

            }
        }



        private void manejoMayorCCostoCredito()
        {

            if (ddlCcostoMayorCredito.SelectedValue.Trim().Length == 0)
            {
                nilblInformacion.Text = "Debe seleccionar un mayor de centro de costo credito valido";
                return;
            }
            else
            {
                try
                {

                    DataView dvClase = CentidadMetodos.EntidadGet("cCentrosCostoSigo", "ppa").Tables[0].DefaultView;
                    dvClase.RowFilter = "empresa=" + Convert.ToString(this.Session["empresa"]) + "  and" + " mayor='" + ddlCcostoMayorCredito.SelectedValue.Trim() + "'";
                    dvClase.Sort = "descripcion";

                    this.ddlCentroCostoCredito.DataSource = dvClase;
                    this.ddlCentroCostoCredito.DataValueField = "codigo";
                    this.ddlCentroCostoCredito.DataTextField = "descripcion";
                    this.ddlCentroCostoCredito.DataBind();
                    this.ddlCentroCostoCredito.Items.Insert(0, new ListItem("", ""));
                }
                catch (Exception ex)
                {
                    ManejoError("Error al cargar los centro de costo de nomina para contabilización. Correspondiente a: " + ex.Message, "C");
                }
            }
        }

        private void manejoCcostoMayorCuenta()
        {

            if (ddlCentroCostoMayor.SelectedValue.Trim().Length == 0)
            {
                nilblInformacion.Text = "Debe seleccionar un mayor de centro de costo de cuenta contable valido";
                return;
            }
            else
            {
                try
                {

                    DataView dvClase = CentidadMetodos.EntidadGet("cCentrosCostoSigo", "ppa").Tables[0].DefaultView;
                    dvClase.RowFilter = "empresa=" + Convert.ToString(this.Session["empresa"]) + " and " + " mayor='" + ddlCentroCostoMayor.SelectedValue.Trim() + "'";
                    dvClase.Sort = "descripcion";

                    this.ddlCentroCosto.DataSource = dvClase;
                    this.ddlCentroCosto.DataValueField = "codigo";
                    this.ddlCentroCosto.DataTextField = "descripcion";
                    this.ddlCentroCosto.DataBind();
                    this.ddlCentroCosto.Items.Insert(0, new ListItem("", ""));
                }
                catch (Exception ex)
                {
                    ManejoError("Error al cargar los centro de costo de nomina para contabilización. Correspondiente a: " + ex.Message, "C");
                }
            }
        }
        private void manejoCcostoNominaConcepto()
        {
            if (ddlCentroCostoNomi.SelectedValue.Trim().Length == 0)
            {
                nilblInformacion.Text = "Seleccione un mayor de  centro de costo  nomina valido ";
                return;
            }

            cargarConceptos();
        }

        private void cargarConceptos()
        {
            try
            {
                this.ddlConcepto.DataSource = parametrosGenerales.RetornaConceptosLaboresxCentroCosto(Convert.ToInt32(this.Session["empresa"]), ddlCentroCostoNomi.SelectedValue.Trim());
                this.ddlConcepto.DataValueField = "codigo";
                this.ddlConcepto.DataTextField = "descripcion";
                this.ddlConcepto.DataBind();
                this.ddlConcepto.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los centro de costo de nomina para contabilización. Correspondiente a: " + ex.Message, "C");
            }
        }


        protected void manejoNuevo()
        {
            rblTipoCantidad.Visible = true;
            ddlEntidad.Enabled = false;
            txvValor.Enabled = false;
            ddlDepartamento.Enabled = false;
            txvValor.Text = "0";
            ddlTercero.DataSource = null;
            ddlTercero.DataBind();
            ddlTercero.Enabled = false;
            ddlTerceroCredito.Enabled = false;
            fsCNomina.Visible = true;
            fsContabilidad.Visible = true;
            fsCTransaccion.Visible = true;


        }

        private void menejoCcostoMayorNomina()
        {

            if (ddlCcostoMayorNomi.SelectedValue.Trim().Length == 0)
            {
                nilblInformacion.Text = "Debe seleccionar un mayor de centro de costo de nomina valido";
                return;
            }
            else
            {
                try
                {

                    DataView dvClase = CentidadMetodos.EntidadGet("cCentrosCosto", "ppa").Tables[0].DefaultView;
                    dvClase.RowFilter = "empresa=" + Convert.ToString(this.Session["empresa"]) + " and " + " mayor='" + ddlCcostoMayorNomi.SelectedValue.Trim() + "'";
                    dvClase.Sort = "descripcion";

                    this.ddlCentroCostoNomi.DataSource = dvClase;
                    this.ddlCentroCostoNomi.DataValueField = "codigo";
                    this.ddlCentroCostoNomi.DataTextField = "descripcion";
                    this.ddlCentroCostoNomi.DataBind();
                    this.ddlCentroCostoNomi.Items.Insert(0, new ListItem("", ""));
                }
                catch (Exception ex)
                {
                    ManejoError("Error al cargar los centro de costo de nomina para contabilización. Correspondiente a: " + ex.Message, "C");
                }
            }
        }
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
                this.gvLista.DataSource = parametrosGenerales.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";
                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                        this.gvLista.Rows.Count.ToString() + " Registros encontrados", HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente a: " + ex.Message, "C");
            }
        }

        private void ManejoError(string error, string operacion)
        {
            this.Session["error"] = error;
            this.Session["paginaAnterior"] = this.Page.Request.FilePath.ToString();

            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "er", error, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            this.Response.Redirect("~/Contabilidad/Error.aspx", false);
        }

        private void ManejoExito(string mensaje, string operacion)
        {

            manejoCancelar();

            this.nilbMensaje.Text = mensaje;
            CcontrolesUsuario.InHabilitarControlesFS(this.Page.Controls);
            CcontrolesUsuario.LimpiarControlesFS(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                    "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }

        private void CargarCombos()
        {
            try
            {
                DataView dvClase = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cClaseParametroContaNomi", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"]));
                this.ddlClase.DataSource = dvClase;
                this.ddlClase.DataValueField = "codigo";
                this.ddlClase.DataTextField = "descripcion";
                this.ddlClase.DataBind();
                this.ddlClase.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar clase de parametro para contabilización. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlTipoTraNomina.DataSource = tipoTransaccion.GetTipoTransaccionModulo(Convert.ToInt16(Session["empresa"]));
                this.ddlTipoTraNomina.DataValueField = "codigo";
                this.ddlTipoTraNomina.DataTextField = "descripcion";
                this.ddlTipoTraNomina.DataBind();
                this.ddlTipoTraNomina.Items.Insert(0, new ListItem("", ""));
                this.ddlTipoTraNomina.SelectedValue = "";
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tipos de transacción nomina. Correspondiente a: " + ex.Message, "C");
            }


            try
            {
                DataView dvClase = CentidadMetodos.EntidadGet("cCentrosCosto", "ppa").Tables[0].DefaultView;
                dvClase.RowFilter = "empresa=" + Convert.ToString(this.Session["empresa"]) + " and auxiliar=0";
                dvClase.Sort = "descripcion";
                this.ddlCcostoMayorNomi.DataSource = dvClase;
                this.ddlCcostoMayorNomi.DataValueField = "codigo";
                this.ddlCcostoMayorNomi.DataTextField = "descripcion";
                this.ddlCcostoMayorNomi.DataBind();
                this.ddlCcostoMayorNomi.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar clase de parametro para contabilización. Correspondiente a: " + ex.Message, "C");
            }


            try
            {

                DataView dvClase = CentidadMetodos.EntidadGet("cCentrosCostoSigo", "ppa").Tables[0].DefaultView;
                dvClase.RowFilter = "empresa=" + Convert.ToString(this.Session["empresa"]) + " and auxiliar=0";
                dvClase.Sort = "descripcion";
                this.ddlCentroCostoMayor.DataSource = dvClase;
                this.ddlCentroCostoMayor.DataValueField = "codigo";
                this.ddlCentroCostoMayor.DataTextField = "descripcion";
                this.ddlCentroCostoMayor.DataBind();
                this.ddlCentroCostoMayor.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar clase de parametro para contabilización. Correspondiente a: " + ex.Message, "C");
            }


            try
            {

                DataView dvClase = CentidadMetodos.EntidadGet("cCentrosCostoSigo", "ppa").Tables[0].DefaultView;
                dvClase.RowFilter = "empresa=" + Convert.ToString(this.Session["empresa"]) + "  and auxiliar=0";
                dvClase.Sort = "descripcion";

                this.ddlCcostoMayorCredito.DataSource = dvClase;
                this.ddlCcostoMayorCredito.DataValueField = "codigo";
                this.ddlCcostoMayorCredito.DataTextField = "descripcion";
                this.ddlCcostoMayorCredito.DataBind();
                this.ddlCcostoMayorCredito.Items.Insert(0, new ListItem("", ""));

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar clase de parametro para contabilización. Correspondiente a: " + ex.Message, "C");
            }


            try
            {
                DataView dvCuenta = parametrosGenerales.RetornaAuxiliaresPuc(Convert.ToInt32(this.Session["empresa"]));
                this.ddlCuentaContable.DataSource = dvCuenta;
                this.ddlCuentaContable.DataValueField = "codigo";
                this.ddlCuentaContable.DataTextField = "nombre";
                this.ddlCuentaContable.DataBind();
                this.ddlCuentaContable.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar cuenta contable. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                DataView dvCuenta = parametrosGenerales.RetornaAuxiliaresPuc(Convert.ToInt32(this.Session["empresa"]));
                this.ddlCuentaGasto.DataSource = dvCuenta;
                this.ddlCuentaGasto.DataValueField = "codigo";
                this.ddlCuentaGasto.DataTextField = "nombre";
                this.ddlCuentaGasto.DataBind();
                this.ddlCuentaGasto.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar cuenta contable de gasto. Correspondiente a: " + ex.Message, "C");
            }


            try
            {
                DataView dvCuenta = parametrosGenerales.RetornaAuxiliaresPuc(Convert.ToInt32(this.Session["empresa"]));
                this.ddlCuentaContratista.DataSource = dvCuenta;
                this.ddlCuentaContratista.DataValueField = "codigo";
                this.ddlCuentaContratista.DataTextField = "nombre";
                this.ddlCuentaContratista.DataBind();
                this.ddlCuentaContratista.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar cuenta contable de contratista. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                DataView dvCuenta = parametrosGenerales.RetornaAuxiliaresPuc(Convert.ToInt32(this.Session["empresa"]));
                this.ddlCuentaCredito.DataSource = dvCuenta;
                this.ddlCuentaCredito.DataValueField = "codigo";
                this.ddlCuentaCredito.DataTextField = "nombre";
                this.ddlCuentaCredito.DataBind();
                this.ddlCuentaCredito.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar cuenta contable de contratista. Correspondiente a: " + ex.Message, "C");
            }

        }

        private void EntidadKey()
        {
            object[] objKey = new object[] { this.lblCodigo.Text.Trim().ToString(), Convert.ToInt16(Session["empresa"]) };
            try
            {
                if (CentidadMetodos.EntidadGetKey("cClaseParametroContaNomi", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    this.nilblInformacion.Text = "Código " + this.lblCodigo.Text + " ya se encuentra registrado";
                    CcontrolesUsuario.InHabilitarControlesFS(this.Page.Controls);
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
            string operacion = "inserta", entidad = ""; int id = 0;

            if (this.lblCodigo.Text.Length == 0)
                this.nilblInformacion.Text = "Campos vacios por favor corrija";
            else
            {
                try
                {
                    if (chkManejaEntidad.Checked == true)
                    {
                        entidad = ddlEntidad.SelectedValue;
                    }
                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                    {
                        operacion = "actualiza";
                        id = Convert.ToInt32(lblCodigo.Text);

                    }
                    else
                    {
                        id = Convert.ToInt32(Cgeneral.RetornaConsecutivoAutomatico("cParametroContaNomi", "id", Convert.ToInt32(this.Session["empresa"])));
                    }

                    object[] objValores = new object[]{

                                                 ddlCentroCostoNomi.SelectedValue.Trim(),    //                    @cCosto	varchar
                                                 ddlCentroCostoCredito.SelectedValue.Trim(), //@cCostoCredito	varchar
                                                   ddlCcostoMayorNomi.SelectedValue.Trim(), //@cCostoMayor	varchar
                                                   ddlCcostoMayorCredito.SelectedValue.Trim(),  //@cCostoMayorCredito	varchar
                                                      ddlCentroCostoMayor.SelectedValue,  //@cCostoMayorSigo	varchar
                                                        ddlCentroCosto.SelectedValue, //@cCostoSigo	varchar
                                                         ddlClase.SelectedValue.Trim(),   //@clase	varchar
                                                            ddlConcepto.SelectedValue.Trim(),               //@concepto	varchar
                                                             ddlCuentaContable.SelectedValue.Trim(),            //@cuentaActivo	varchar
                                                              ddlCuentaContratista.SelectedValue.Trim(),          //@cuentaContratista	varchar
                                                              ddlCuentaCredito.SelectedValue.Trim(),          //@cuentaCredito	varchar
                                                              ddlCuentaGasto.SelectedValue.Trim(),          //@cuentaGasto	varchar
                                                              ddlDepartamento.SelectedValue.Trim(),
                                                               Convert.ToInt32( this.Session["empresa"]),           //@empresa	int
                                                              entidad,       //@entidad
                                                             id ,          //@id	int
                                                                    chkManejaEntidad.Checked,       //@manejaEntidad	bit
                                                                      ddlTercero.SelectedValue,//@tercero
                                                                   ddlTerceroCredito.SelectedValue ,//@terceroCredito
                                                                      ddlTipo.SelectedValue, //@tipo	varchar
                                                                          rblTipoCantidad.SelectedValue, //@tipoDato	int
                                           ddlTipoTraNomina.SelectedValue, //@tipoTransaccion	varchar
                                       Convert.ToDecimal(txvValor.Text) //@valorTipoDato	decimal                                       
                    
                };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("cParametroContaNomi", operacion, "ppa", objValores))
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
        }

        #endregion Metodos

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


                    this.lblCodigo.Focus();

                }
                else
                {
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
                }
            }
        }



        protected void lbNuevo_Click(object sender, ImageClickEventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                                 ConfigurationManager.AppSettings["Modulo"].ToString(),
                                  nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            nilbMensaje.Text = "";

            CcontrolesUsuario.HabilitarControlesFS(
                this.fsCNomina.Controls);
            CcontrolesUsuario.LimpiarControlesFS(fsCNomina.Controls);
            CcontrolesUsuario.HabilitarControlesFS(
                this.fsContabilidad.Controls);
            CcontrolesUsuario.LimpiarControlesFS(fsContabilidad.Controls);
            CcontrolesUsuario.HabilitarControlesFS(
                this.fsCTransaccion.Controls);
            CcontrolesUsuario.LimpiarControlesFS(fsCTransaccion.Controls);

            lbCancelar.Visible = true;
            lbRegistrar.Visible = true;
            CargarCombos();
            manejoNuevo();
            manejoContratista();

            limpiarDDL();

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;

            try
            {
                lblCodigo.Text = Cgeneral.RetornaConsecutivoAutomatico("cParametroContaNomi", "id", Convert.ToInt32(this.Session["empresa"]));
                lblCodigo.Enabled = false;
            }
            catch (Exception ex)
            {

                ManejoError("Error al cargar consecutivo debido a:  " + ex.Message, "C");

            }


            this.nilblInformacion.Text = "";
        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            manejoCancelar();
        }

        private void manejoCancelar()
        {
            CcontrolesUsuario.InHabilitarControlesFS(
                this.fsCNomina.Controls);

            CcontrolesUsuario.LimpiarControlesFS(
                this.fsCNomina.Controls);

            CcontrolesUsuario.InHabilitarControlesFS(
               this.fsContabilidad.Controls);

            CcontrolesUsuario.LimpiarControlesFS(
                this.fsContabilidad.Controls);

            CcontrolesUsuario.InHabilitarControlesFS(
               this.fsCTransaccion.Controls);

            CcontrolesUsuario.LimpiarControlesFS(
                this.fsCTransaccion.Controls);


            fsCNomina.Visible = false;
            fsContabilidad.Visible = false;
            fsCTransaccion.Visible = false;
            nilbMensaje.Text = "";

            this.gvLista.DataSource = null;
            this.gvLista.DataBind();

            limpiarDDL();
            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
        }

        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                                        ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                object[] objValores = new object[] {
                Convert.ToInt16(Session["empresa"]),
                Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text))

            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete(
                    "cParametroContaNomi",
                    "elimina",
                    "ppa",
                    objValores))
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

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }

        protected void lbRegistrar_Click(object sender, ImageClickEventArgs e)
        {

            int validaConcepto = 0;
            try
            {
                validaConcepto = parametrosGenerales.VerificaClaseConceptosContabilizacion(ddlClase.SelectedValue, Convert.ToInt32(this.Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Erro al validar concepto debido a : " + ex.Message, "C");
            }

            if (ddlConcepto.SelectedValue.Trim().Length == 0 & validaConcepto == 0)
            {
                this.nilblInformacion.Text = "Debe seleccionar una concepto valido";
                return;
            }

            if (ddlCuentaContable.SelectedValue.Trim().Length == 0 && ddlCuentaCredito.SelectedValue.Trim().Length == 0 && ddlCuentaGasto.SelectedValue.Trim().Length == 0 & chkManejaEntidad.Checked == false)
            {
                this.nilblInformacion.Text = "Debe seleccionar una cuenta valida valido";
                return;
            }

            if (ddlCcostoMayorCredito.SelectedValue.Trim().Length > 0 && ddlCentroCostoCredito.SelectedValue.Trim().Length == 0 & chkManejaEntidad.Checked == false)
            {
                this.nilblInformacion.Text = "Debe seleccionar una centro de costo  mayor credito y subcentro valido";
                return;
            }

            if (ddlCentroCostoMayor.SelectedValue.Trim().Length > 0 && ddlCentroCosto.SelectedValue.Trim().Length == 0 & chkManejaEntidad.Checked == false)
            {
                this.nilblInformacion.Text = "Debe seleccionar una centro de costo  y subcentro valido";
                return;
            }

            Guardar();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {

            CcontrolesUsuario.HabilitarControlesFS(this.fsCNomina.Controls);
            CcontrolesUsuario.HabilitarControlesFS(this.fsCNomina.Controls);
            CcontrolesUsuario.HabilitarControlesFS(this.fsContabilidad.Controls);
            CcontrolesUsuario.HabilitarControlesFS(this.fsContabilidad.Controls);
            CcontrolesUsuario.HabilitarControlesFS(this.fsCTransaccion.Controls);
            CcontrolesUsuario.HabilitarControlesFS(this.fsCTransaccion.Controls);

            limpiarDDL();
            fsCNomina.Visible = true;
            fsContabilidad.Visible = true;
            fsCTransaccion.Visible = true;
            this.lbRegistrar.Visible = true;

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "A", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "A");
                return;
            }


            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            CargarCombos();
            try
            {
                DataView dvParametrosGeneral;

                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                {
                    this.lblCodigo.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[2].Text);
                    dvParametrosGeneral = parametrosGenerales.RetornaDatosParametroGenerales(Convert.ToInt32(this.Session["empresa"]), Convert.ToInt32(lblCodigo.Text));
                }
                else
                {
                    this.lblCodigo.Text = "";
                    dvParametrosGeneral = null;
                }

                foreach (DataRowView drv in dvParametrosGeneral)
                {
                    if (!(drv.Row.ItemArray.GetValue(2) is DBNull))
                    {
                        if (drv.Row.ItemArray.GetValue(2).ToString() != "&nbsp;")
                        {
                            ddlTipo.SelectedValue = drv.Row.ItemArray.GetValue(2).ToString();
                            manejoContratista();
                        }
                        else
                            ddlTipo.SelectedValue = "";
                    }

                    if (!(drv.Row.ItemArray.GetValue(3) is DBNull))
                    {
                        ddlClase.SelectedValue = drv.Row.ItemArray.GetValue(3).ToString();
                    }

                    if (!(drv.Row.ItemArray.GetValue(4) is DBNull))
                    {
                        ddlTipoTraNomina.SelectedValue = drv.Row.ItemArray.GetValue(4).ToString();
                    }

                    if (!(drv.Row.ItemArray.GetValue(5) is DBNull))
                    {
                        if (drv.Row.ItemArray.GetValue(5).ToString() != "&nbsp;")
                        {
                            ddlCcostoMayorNomi.SelectedValue = drv.Row.ItemArray.GetValue(5).ToString();
                            if (ddlCcostoMayorNomi.SelectedValue.Trim().Length > 0)
                                menejoCcostoMayorNomina();
                        }
                        else
                        {
                            ddlCcostoMayorNomi.SelectedValue = "";
                        }
                    }

                    if (!(drv.Row.ItemArray.GetValue(6) is DBNull))
                    {
                        ddlCentroCostoNomi.SelectedValue = drv.Row.ItemArray.GetValue(6).ToString();
                        cargarConceptos();
                    }

                    if (!(drv.Row.ItemArray.GetValue(7) is DBNull))
                    {
                        if (drv.Row.ItemArray.GetValue(7).ToString().Trim().Length > 0)
                        {
                            ddlDepartamento.SelectedValue = drv.Row.ItemArray.GetValue(7).ToString();
                            chkManejaDepartamento.Checked = true;
                        }
                    }

                    if (!(drv.Row.ItemArray.GetValue(8) is DBNull))
                    {
                        ddlConcepto.SelectedValue = drv.Row.ItemArray.GetValue(8).ToString();
                    }


                    if (!(drv.Row.ItemArray.GetValue(9) is DBNull))
                    {
                        chkManejaEntidad.Checked = Convert.ToBoolean(drv.Row.ItemArray.GetValue(9));

                    }

                    if (!(drv.Row.ItemArray.GetValue(10) is DBNull))
                    {
                        ddlCuentaContable.SelectedValue = drv.Row.ItemArray.GetValue(10).ToString();
                    }

                    if (!(drv.Row.ItemArray.GetValue(11) is DBNull))
                    {
                        ddlCuentaGasto.SelectedValue = drv.Row.ItemArray.GetValue(11).ToString();
                    }

                    if (!(drv.Row.ItemArray.GetValue(12) is DBNull))
                    {
                        ddlCuentaContratista.SelectedValue = drv.Row.ItemArray.GetValue(12).ToString();
                    }


                    if (!(drv.Row.ItemArray.GetValue(13) is DBNull))
                    {
                        if (drv.Row.ItemArray.GetValue(13).ToString() != "&nbsp;")
                        {
                            ddlCentroCostoMayor.SelectedValue = drv.Row.ItemArray.GetValue(13).ToString();
                            if (ddlCentroCostoMayor.SelectedValue.Trim().Length > 0)
                                manejoCcostoMayorCuenta();
                        }
                        else
                        {
                            ddlCentroCostoMayor.SelectedValue = "";
                        }
                    }

                    if (!(drv.Row.ItemArray.GetValue(14) is DBNull))
                    {
                        if (ddlCentroCostoMayor.SelectedValue.Trim().Length > 0)
                            ddlCentroCosto.SelectedValue = drv.Row.ItemArray.GetValue(14).ToString();
                    }

                    if (!(drv.Row.ItemArray.GetValue(15) is DBNull))
                    {
                        ddlCuentaCredito.SelectedValue = drv.Row.ItemArray.GetValue(15).ToString();
                    }


                    if (!(drv.Row.ItemArray.GetValue(16) is DBNull))
                    {
                        if (drv.Row.ItemArray.GetValue(16).ToString() != "&nbsp;")
                        {
                            ddlCcostoMayorCredito.SelectedValue = drv.Row.ItemArray.GetValue(16).ToString().Trim();
                            if (ddlCcostoMayorCredito.SelectedValue.Trim().Length > 0)
                                manejoMayorCCostoCredito();

                        }
                        else
                        {
                            ddlCcostoMayorCredito.SelectedValue = "";
                        }
                    }

                    if (!(drv.Row.ItemArray.GetValue(17) is DBNull))
                    {
                        ddlCentroCostoCredito.SelectedValue = drv.Row.ItemArray.GetValue(17).ToString().Trim();
                    }

                    if (!(drv.Row.ItemArray.GetValue(18) is DBNull))
                    {
                        rblTipoCantidad.SelectedValue = drv.Row.ItemArray.GetValue(18).ToString();

                        manejoTipoCantidad();
                    }


                    if (!(drv.Row.ItemArray.GetValue(19) is DBNull))
                    {
                        txvValor.Text = drv.Row.ItemArray.GetValue(19).ToString();

                    }


                    if (!(drv.Row.ItemArray.GetValue(20) is DBNull))
                    {
                        if (drv.Row.ItemArray.GetValue(20).ToString().Length > 0)
                            ddlEntidad.SelectedValue = drv.Row.ItemArray.GetValue(20).ToString();
                    }

                    if (!(drv.Row.ItemArray.GetValue(22) is DBNull))
                    {
                        if (drv.Row.ItemArray.GetValue(22).ToString().Length > 0)
                        {
                            cargarTerceros();
                            ddlTercero.SelectedValue = drv.Row.ItemArray.GetValue(22).ToString();
                            chkTerceroActivo.Checked = ddlTercero.SelectedValue.Trim().Length == 0 ? false : true;
                        }
                    }

                    if (!(drv.Row.ItemArray.GetValue(23) is DBNull))
                    {
                        if (drv.Row.ItemArray.GetValue(23).ToString().Length > 0)
                        {
                            cargarTercerosCredito();
                            ddlTerceroCredito.SelectedValue = drv.Row.ItemArray.GetValue(23).ToString();
                            chkTerceroCredito.Checked = ddlTerceroCredito.SelectedValue.Trim().Length == 0 ? false : true;
                        }
                    }

                }

                manejoEntidad();

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }

        private void limpiarDDL()
        {
            ddlCcostoMayorCredito.DataSource = null;
            ddlCcostoMayorCredito.DataBind();
            ddlCcostoMayorNomi.DataSource = null;
            ddlCcostoMayorNomi.DataBind();
            ddlCentroCosto.DataSource = null;
            ddlCentroCosto.DataBind();
            ddlCentroCostoCredito.DataSource = null;
            ddlCentroCostoCredito.DataBind();
            ddlCentroCostoMayor.DataSource = null;
            ddlCentroCostoMayor.DataBind();
            ddlCentroCostoNomi.DataSource = null;
            ddlCentroCostoNomi.DataBind();
            ddlCuentaGasto.DataSource = null;
            ddlCuentaGasto.DataBind();
            ddlDepartamento.DataSource = null;
            ddlDepartamento.DataBind();
            ddlTipoTraNomina.DataSource = null;
            ddlTipoTraNomina.DataBind();
            ddlCcostoMayorNomi.DataSource = null;
            ddlCcostoMayorNomi.DataBind();
            ddlConcepto.DataSource = null;
            ddlConcepto.DataBind();
            ddlCentroCosto.DataSource = null;
            ddlCentroCosto.DataBind();
            ddlCentroCostoNomi.DataSource = null;
            ddlCentroCostoNomi.DataBind();
            ddlCentroCosto.DataSource = null;
            ddlCentroCosto.DataBind();
        }

        protected void niimbBuscar_Click(object sender, ImageClickEventArgs e)
        {
            CcontrolesUsuario.InHabilitarControlesFS(this.fsCNomina.Controls);
            CcontrolesUsuario.InHabilitarControlesFS(this.fsCNomina.Controls);
            CcontrolesUsuario.InHabilitarControlesFS(this.fsContabilidad.Controls);
            CcontrolesUsuario.InHabilitarControlesFS(this.fsContabilidad.Controls);
            CcontrolesUsuario.InHabilitarControlesFS(this.fsCTransaccion.Controls);
            CcontrolesUsuario.InHabilitarControlesFS(this.fsCTransaccion.Controls);
            nilbMensaje.Text = "";
            fsCNomina.Visible = false;
            fsContabilidad.Visible = false;
            fsCTransaccion.Visible = false;
            this.nilbNuevo.Visible = true;
            limpiarDDL();
            GetEntidad();
            lbCancelar.Visible = true;
        }

        protected void gvLista_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }

        protected void ddlCcostoMayorNomi_SelectedIndexChanged(object sender, EventArgs e)
        {
            menejoCcostoMayorNomina();
        }



        protected void ddlCentroCostoNomi_SelectedIndexChanged(object sender, EventArgs e)
        {

            manejoCcostoNominaConcepto();
        }

        protected void chkManejaEntidad_CheckedChanged(object sender, EventArgs e)
        {
            manejoEntidad();
        }



        protected void rblTipoCantidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            manejoTipoCantidad();
        }



        protected void ddlCentroCostoMayor_SelectedIndexChanged(object sender, EventArgs e)
        {
            manejoCcostoMayorCuenta();
        }



        protected void ddlCcostoMayorCredito_SelectedIndexChanged(object sender, EventArgs e)
        {
            manejoMayorCCostoCredito();

        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            manejoContratista();

            if (ddlTipo.SelectedValue == "PC")
            {

                conceptosContratistas();
            }
        }




        protected void chkManejaDepartamento_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                manejoDepartamento();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar centro costo debido a: " + ex.Message, "C");
            }
        }



        protected void chkTerceroActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTerceroActivo.Checked == true)
            {
                cargarTerceros();
                ddlTercero.Enabled = true;
            }
            else
            {
                ddlTercero.DataSource = null;
                ddlTercero.DataBind();
                ddlTercero.Enabled = false;
            }
        }

        protected void chkTerceroCredito_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTerceroCredito.Checked == true)
            {
                cargarTercerosCredito();
                ddlTerceroCredito.Enabled = true;
            }
            else
            {
                ddlTerceroCredito.DataSource = null;
                ddlTerceroCredito.DataBind();
                ddlTercero.Enabled = false;
            }
        }




        #endregion Eventos





    }
}