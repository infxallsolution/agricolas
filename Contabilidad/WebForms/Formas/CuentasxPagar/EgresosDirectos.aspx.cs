using Contabilidad.seguridadInfos;
using Contabilidad.WebForms.App_Code.Administracion;
using Contabilidad.WebForms.App_Code.CuentasxCobrar;
using Contabilidad.WebForms.App_Code.General;
using Contabilidad.WebForms.App_Code.Presupuesto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.CuentasxPagar
{
    public partial class EgresosDirectos : BasePage
    {

        #region Instancias
        CcajaOtros transaccionCaja;
        CcondicionPago condicionpago = new CcondicionPago();
        Security seguridad = new Security();
        CIP ip = new CIP();
        CcentroCosto cccosto = new CcentroCosto();
        CtipoTransaccion tipoTransaccion = new CtipoTransaccion();
        Cperiodos periodo = new Cperiodos();
        Ctransacciones transacciones = new Ctransacciones();
        Cgeneral general = new Cgeneral();
        Cpuc puc = new Cpuc();
        Cterceros terceros = new Cterceros();
        Coperadores operadores = new Coperadores();
        bool proveedor = false;
        bool cliente = false;


        #endregion Instancias

        #region Metodos




        public DataView dvPuc()
        {
            try
            {
                DataView dvPuc = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cPuc", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                dvPuc.RowFilter = "empresa=" + Session["empresa"].ToString() + " and auxiliar=1 and activo=1";
                dvPuc.Sort = "descripcion";
                return dvPuc;
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
                return null;
            }
        }

        private void cargarSucursalProveedor()
        {
            DataView dvProveedor = terceros.SeleccionaSucursalProveedor(Convert.ToInt16(Session["empresa"]), txtIdTerceroEncabezado.Text.Trim());
            this.ddlSucursal.DataSource = dvProveedor;
            this.ddlSucursal.DataValueField = "codigo";
            this.ddlSucursal.DataTextField = "cadena";
            this.ddlSucursal.DataBind();
            this.ddlSucursal.Items.Insert(0, new ListItem("", ""));
        }

        //private int guardarEncabezadoEdicion()
        //{
        //    try
        //    {
        //        DateTime fecha = Convert.ToDateTime(txtFecha.Text);

        //        object[] oCabeza = new object[]
        //        { fecha.Year,   //@año    int
        //             false,   //            @anulado   bit
        //             null,   //@clase  varchar
        //            false,   //@ejecutado  bit
        //            Convert.ToInt16(this.Session["empresa"]),    //@empresa    int
        //              fecha,  //@fecha  date
        //              null,  //@fechaAnulado   datetime
        //              null,  //@fechaEjecutado datetime
        //              DateTime.Now,  //@fechaModificado    datetime
        //             null,   //@fechaRegistro  datetime
        //             fecha.Month,   //@mes    int
        //             true,   //@modificado bit
        //             txtNumero.Text,   //@numero varchar
        //             txtNota.Text,    //@observacion    varchar
        //             null,   //@referencia varchar
        //             txtIdTerceroEncabezado.Text ,//@tercero
        //             ddlTipoDocumento.SelectedValue.Trim(),   //@tipo   varchar
        //              null,   //@usuario    varchar
        //             null,   //@usuarioAnulado varchar
        //             null,   //@usuarioEjecutado   varchar
        //             this.Session["usuario"].ToString()   //@usuarioModificado  varchar
        //        };

        //        switch (CentidadMetodos.EntidadInsertUpdateDelete("cTransaccion", "actualiza", "ppa", oCabeza))
        //        {
        //            case 0:
        //                return 0;
        //            case 1:
        //                return 1;
        //            default:
        //                return 1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ManejoError("Error al insertar el registro  debido a: " + ex.Message, "I");
        //        return -1;
        //    }
        //}

        //private int guardarEncabezado()
        //{
        //    try
        //    {
        //        DateTime fecha = Convert.ToDateTime(txtFecha.Text);

        //        object[] oCabeza = new object[]
        //        {   fecha.Year,   //@año    int
        //             false,   //            @anulado   bit
        //             null,   //@clase  varchar
        //            false,   //@ejecutado  bit
        //            Convert.ToInt16(this.Session["empresa"]),    //@empresa    int
        //              fecha,  //@fecha  date
        //              null,  //@fechaAnulado   datetime
        //              null,  //@fechaEjecutado datetime
        //              null,  //@fechaModificado    datetime
        //             DateTime.Now,   //@fechaRegistro  datetime
        //             fecha.Month,   //@mes    int
        //             false,   //@modificado bit
        //             txtNumero.Text,   //@numero varchar
        //             txtNota.Text,    //@observacion    varchar
        //             null,   //@referencia varchar
        //             txtIdTerceroEncabezado.Text ,//@tercero
        //             ddlTipoDocumento.SelectedValue.Trim(),   //@tipo   varchar
        //             this.Session["usuario"].ToString(),   //@usuario    varchar
        //             null,   //@usuarioAnulado varchar
        //             null,   //@usuarioEjecutado   varchar
        //             null   //@usuarioModificado  varchar
        //        };

        //        switch (CentidadMetodos.EntidadInsertUpdateDelete("cTransaccion", "inserta", "ppa", oCabeza))
        //        {
        //            case 0:
        //                return 0;
        //            case 1:
        //                return 1;
        //            default:
        //                return 1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ManejoError("Error al insertar el registro  debido a: " + ex.Message, "I");
        //        return -1;
        //    }
        //}


        private int guardarDetalle()
        {
            //    try
            //    {
            //        //int verificaTransaccion = 0;
            //        //DateTime fecha = Convert.ToDateTime(txtFecha.Text);
            //        //string diasVencimiento = txtDiasVencimiento.Visible & txtDiasVencimiento.Text.Trim().Length > 0 ? txtDiasVencimiento.Text : null;
            //        //DataView dvTransaccion = transacciones.GetTransaccionContable(ddlTipoDocumento.SelectedValue.Trim(), txtNumero.Text, Convert.ToInt32(this.Session["empresa"]));
            //        //int registro = dvTransaccion.Count > 0 ? dvTransaccion.Table.AsEnumerable().Max(x => Convert.ToInt32(x["registro"])) + 1 : 1;

            //        //decimal baseGravable = txvBase.Visible & txvBase.Text.Trim().Length > 0 ? Convert.ToDecimal(txvBase.Text) : 0;
            //        //decimal credito = ddlNaturaleza.SelectedValue == "C" ? Convert.ToDecimal(txvValor.Text) : 0;
            //        //decimal debito = ddlNaturaleza.SelectedValue == "D" ? Convert.ToDecimal(txvValor.Text) : 0;
            //        //string ccosto = ddlCcosto.Visible & ddlCcosto.SelectedValue.Trim().Length > 0 ? ddlCcosto.SelectedValue.Trim() : null;
            //        //string tipoReferencia = txtTipoReferecia.Visible ? txtTipoReferecia.Text : null;
            //        //string numeroreferencia = txtNumeroReerencia.Visible ? txtNumeroReerencia.Text : null;
            //        //decimal porcentaje = txvPorcentaje.Visible & txvPorcentaje.Text.Trim().Length > 0 ? Convert.ToDecimal(txvPorcentaje.Text) : 0;
            //        //string tercero = txtTercero.Visible ? txtTercero.Text : null;
            //        //string sucursal = ddlSucursal.Visible & ddlSucursal.SelectedValue.Trim().Length > 0 ? ddlSucursal.SelectedValue : null;



            //        //switch (transacciones.guardarDetalle(año: fecha.Year, baseG: baseGravable, ccosto: ccosto, credito: credito,
            //        //    creditoAlterno: 0, cuenta: txtCuenta.Text, cuentaAlterna: "", debito: debito, debitoAlterno: 0, empresa: Convert.ToInt32(this.Session["empresa"]),
            //        //    mes: fecha.Month, nota: txtDetalle.Text, numero: txtNumero.Text, numeroReferencia: numeroreferencia,
            //        //    porcentaje: porcentaje, registro: registro, sucursal: sucursal, tercero: tercero, tipo: ddlTipoDocumento.SelectedValue,
            //        //    tipoReferencia: tipoReferencia, diasvencimiento: diasVencimiento))
            //        //{
            //        //    case 0:
            //        //        return 0;
            //        //    case 1:
            //        //        return 1;
            //        //    default:
            //        //        return 1;
            //        //}

            //    }
            //    catch (Exception ex)
            //    {
            //        ManejoError("Error al ingresar detalle debido a: " + ex.Message, "I");
            //        return -1;
            //    }
            return 0;
        }

        static string LimpiarCadena(string strIn)
        {
            // Replace invalid characters with empty strings.
            try
            {
                if ((strIn != null))
                {
                    if (strIn.Trim().Length > 0)
                    {
                        string textoNormalizado = strIn.Normalize(NormalizationForm.FormD);
                        return Regex.Replace(textoNormalizado, @"[^a-zA-z0-9 ]", "",
                                             RegexOptions.None, TimeSpan.FromSeconds(1.5)).ToUpper();
                    }
                    else
                        return "";
                }
                else
                    return "";
            }
            // If we timeout when replacing invalid characters, 
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return null;
            }
        }

        private void totalizar()
        {
            //gvLista.FooterRow.Cells[5].Text = "Total";
            //gvLista.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;

            decimal totalPagar = 0;
            foreach (GridViewRow gvr in gvLista.Rows)
            {
                totalPagar += Convert.ToDecimal(gvr.Cells[4].Text);
            }

            nitxtTotalPagado.Text = totalPagar.ToString("N2");
            nitxtTotalDiferencia.Text = (Convert.ToDecimal(txtValorPago.Text) - totalPagar).ToString("N2");
        }

        private void manejoCuenta()
        {
            try
            {
                string config = puc.spSeleccionaConfigCuenta(Convert.ToInt16(this.Session["empresa"]), txtCuenta.Text.Trim());

                foreach (DataRowView drv in puc.GetPuc(txtCuenta.Text.Trim(), Convert.ToInt16(this.Session["empresa"])))
                {
                    lblNombreCuenta.Text = drv.Row.ItemArray.GetValue(4).ToString();
                    lblNombreCuenta.Visible = true;
                }
                txtCuenta.Focus();
            }
            catch (Exception ex)
            {
                ManejoError("Error en la configuración de la cuenta debido a: " + ex.Message, "C");
            }
        }
        protected string nombrePaginaActual()
        {
            string[] arrResult = HttpContext.Current.Request.RawUrl.Split('/');
            string result = arrResult[arrResult.GetUpperBound(0)];
            arrResult = result.Split('?');
            return arrResult[arrResult.GetLowerBound(0)];
        }
        private void EstadoInicialGrillaTransacciones()
        {
            for (int i = 0; i < this.gvTransaccion.Columns.Count; i++)
            {
                this.gvTransaccion.Columns[i].Visible = true;
            }

            foreach (GridViewRow registro in this.gvTransaccion.Rows)
            {
                this.gvTransaccion.Rows[registro.RowIndex].Visible = true;
            }
        }
        private void ManejoError(string error, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), error, "error");
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion,
                ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "er",
                error, ObtenerIP(), Convert.ToInt16(Session["empresa"]));
        }
        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            CcontrolesUsuario.InhabilitarControles(this.upCabeza.Controls);
            CcontrolesUsuario.LimpiarControles(this.upCabeza.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upDetalle.Controls);
            CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
            InHabilitaEncabezado();
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.Session["impuesto"] = null;
            this.Session["transaccion"] = null;
            this.lbRegistrar.Visible = false;
            lbFecha.Visible = false;
            gvLista.DataSource = null;
            gvLista.DataBind();
            upDetalle.Visible = false;
            Session["rangos"] = null;
            //imbBuscarTercero.Visible = false;
            //imbBuscarCuenta.Visible = false;
            btnTerceroEncabezado.Visible = false;
            btnBeneficiario.Visible = false;
            lbAprobar.Visible = false;
            this.Session["cajaCliente"] = null;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                        mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));
        }
        private void ManejoEncabezado()
        {
            HabilitaEncabezado();
            CargarTipoTransaccion();
        }
        private void CargarTipoTransaccion()
        {
            try
            {
                this.ddlTipoDocumento.DataSource = tipoTransaccion.GetTipoTransaccionContable(Convert.ToInt16(Session["empresa"]));
                this.ddlTipoDocumento.DataValueField = "codigo";
                this.ddlTipoDocumento.DataTextField = "descripcion";
                this.ddlTipoDocumento.DataBind();
                this.ddlTipoDocumento.Items.Insert(0, new ListItem("", ""));
                this.ddlTipoDocumento.SelectedValue = "";
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tipos de transacción. Correspondiente a: " + ex.Message, "C");
            }
        }
        private void CargaCampos()
        {
            try
            {
                this.niddlCampo.DataSource = transacciones.SeleccionaCamposContabilidadDoc(Convert.ToInt32(this.Session["empresa"]));
                this.niddlCampo.DataValueField = "codigo";
                this.niddlCampo.DataTextField = "descripcion";
                this.niddlCampo.DataBind();
                this.niddlCampo.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos para edición. Correspondiente a: " + ex.Message, "C");
            }

        }
        private void CargarEmpleados()
        {
            //try
            //{
            //    DataView dvTerceroCCosto = funcionario.RetornaFuncionarioCcosto(ddlCentroCosto.SelectedValue.ToString(), Convert.ToInt16(Session["empresa"]));
            //    //            DataView dvTerceroCCosto = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nFuncionario", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
            //    //          dvTerceroCCosto.RowFilter = "empresa = " + Convert.ToInt16(Session["empresa"]).ToString() + " and ccosto = '" + ddlCentroCosto.SelectedValue.ToString() + "'";
            //    this.ddlEmpleado.DataSource = dvTerceroCCosto;
            //    this.ddlEmpleado.DataValueField = "tercero";
            //    this.ddlEmpleado.DataTextField = "descripcion";
            //    this.ddlEmpleado.DataBind();
            //    this.ddlEmpleado.Items.Insert(0, new ListItem("", ""));
            //}
            //catch (Exception ex)
            //{
            //    ManejoError("Error al cargar empleados. Correspondiente a: " + ex.Message, "C");
            //}

            //try
            //{
            //    DataView dvTerceroCCosto = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nFuncionario", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
            //    if (Convert.ToBoolean(Session["editar"]) != true)
            //        dvTerceroCCosto.RowFilter = "empresa = " + Convert.ToInt16(Session["empresa"]).ToString() + " and ccosto = '" + ddlCentroCosto.SelectedValue.ToString() + "'";

            //    this.ddlEmpleadoDetalle.DataSource = dvTerceroCCosto;
            //    this.ddlEmpleadoDetalle.DataValueField = "tercero";
            //    this.ddlEmpleadoDetalle.DataTextField = "descripcion";
            //    this.ddlEmpleadoDetalle.DataBind();
            //    this.ddlEmpleadoDetalle.Items.Insert(0, new ListItem("", ""));
            //}
            //catch (Exception ex)
            //{
            //    ManejoError("Error al cargar empleados. Correspondiente a: " + ex.Message, "C");
            //}
        }
        protected void cargarCombosDetalle()
        {
            //try
            //{
            //    DataView dvConceptosNoFijos = CcontrolesUsuario.OrdenarEntidadyActivos(conceptos.SeleccionaConceptoxCcosto(Convert.ToInt16(Session["empresa"]), ddlCentroCosto.SelectedValue), "descripcion", Convert.ToInt16(Session["empresa"]));
            //    dvConceptosNoFijos.RowFilter = "empresa = " + Convert.ToInt16(Session["empresa"]).ToString() + " and fijo=0 and ausentismo=0";
            //    this.ddlConceptoDetalle.DataSource = dvConceptosNoFijos;
            //    this.ddlConceptoDetalle.DataValueField = "codigo";
            //    this.ddlConceptoDetalle.DataTextField = "descripcion";
            //    this.ddlConceptoDetalle.DataBind();
            //    this.ddlConceptoDetalle.Items.Insert(0, new ListItem("", ""));
            //}
            //catch (Exception ex)
            //{
            //    ManejoError("Error al cargar conceptos. Correspondiente a: " + ex.Message, "C");
            //}

        }
        private void CargarCombos()
        {

            try
            {
                DataView dvTipoDocto = condicionpago.BuscarEntidad("", Convert.ToInt16(HttpContext.Current.Session["empresa"]));
                EnumerableRowCollection<DataRow> query = from tipodoc in dvTipoDocto.Table.AsEnumerable()
                                                         where tipodoc.Field<int>("empresa") == Convert.ToInt16(HttpContext.Current.Session["empresa"])
                                                         select tipodoc;

                DataView ccosto = query.AsDataView();
                this.ddlCondicionPago.DataSource = ccosto;
                this.ddlCondicionPago.DataValueField = "codigo";
                this.ddlCondicionPago.DataTextField = "cadena";
                this.ddlCondicionPago.DataBind();
                this.ddlCondicionPago.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar condiciones de pago. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                DataView dvFormaPago = CentidadMetodos.EntidadGet("gFormaPago", "ppa").Tables[0].DefaultView;
                EnumerableRowCollection<DataRow> query = from tipodoc in dvFormaPago.Table.AsEnumerable()
                                                         where tipodoc.Field<int>("empresa") == Convert.ToInt16(HttpContext.Current.Session["empresa"])
                                                         select tipodoc;

                this.ddlFormaPago.DataSource = query.AsDataView();
                this.ddlFormaPago.DataValueField = "codigo";
                this.ddlFormaPago.DataTextField = "cadena";
                this.ddlFormaPago.DataBind();
                this.ddlFormaPago.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar conceptos. Correspondiente a: " + ex.Message, "C");
            }



            try
            {
                DataView dvCuenta = CentidadMetodos.EntidadGet("cCuentaBancaria", "ppa").Tables[0].DefaultView;
                EnumerableRowCollection<DataRow> query = from tipodoc in dvCuenta.Table.AsEnumerable()
                                                         where tipodoc.Field<int>("empresa") == Convert.ToInt16(HttpContext.Current.Session["empresa"])
                                                         select tipodoc;

                this.ddlCuentaCorriente.DataSource = query.AsDataView();
                this.ddlCuentaCorriente.DataValueField = "codigo";
                this.ddlCuentaCorriente.DataTextField = "cadena";
                this.ddlCuentaCorriente.DataBind();
                this.ddlCuentaCorriente.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar conceptos. Correspondiente a: " + ex.Message, "C");
            }


            //try
            //{
            //    DataView dvTercero = terceros.SeleccionaTerceroActivos(Convert.ToInt16(this.Session["empresa"]));
            //    this.txtTercero.DataSource = dvTercero;
            //    this.txtTercero.DataValueField = "codigo";
            //    this.txtTercero.DataTextField = "cadena";
            //    this.txtTercero.DataBind();
            //    this.txtTercero.Items.Insert(0, new ListItem("", ""));
            //}
            //catch (Exception ex)
            //{
            //    ManejoError("Error al cargar empleados. Correspondiente a: " + ex.Message, "C");
            //}




        }
        private object TipoTransaccionConfig(int posicion)
        {
            object retorno = null;
            string cadena;
            char[] comodin = new char[] { '*' };
            int indice = posicion + 1;

            try
            {
                cadena = tipoTransaccion.TipoTransaccionConfig(ddlTipoDocumento.SelectedValue, Convert.ToInt16(Session["empresa"])).ToString();
                retorno = cadena.Split(comodin, indice).GetValue(posicion - 1);

                return retorno;
            }
            catch (Exception ex)
            {
                ManejoError("Error al recuperar posición de configuración de tipo de transacción. Correspondiente a: " + ex.Message, "C");

                return null;
            }
        }
        private string ConsecutivoTransaccion()
        {
            string numero = "";

            try
            {
                numero = transacciones.RetornaNumeroTransaccion(
                    Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al obtener el número de transacción. Correspondiente a: " + ex.Message, "C");
            }

            return numero;
        }
        private void HabilitaEncabezado()
        {
            this.lbCancelar.Visible = true;
            this.nilbNuevo.Visible = false;
            this.lblTipoDocumento.Visible = true;
            this.ddlTipoDocumento.Visible = true;
            this.ddlTipoDocumento.Enabled = true;
            this.lblNumero.Visible = true;
            this.txtNumero.Visible = true;
            this.txtNumero.Text = "";
            this.ddlTipoDocumento.Focus();
            this.niCalendarFecha.Visible = false;
            this.lbRegistrar.Visible = true;
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.gvTransaccion.DataSource = null;
            this.gvTransaccion.DataBind();
            this.Session["transaccion"] = null;
            this.lbCancelar.Visible = true;
            upDetalle.Visible = false;


        }
        private void InHabilitaEncabezado()
        {

            this.lbCancelar.Visible = false;
            this.nilbNuevo.Visible = true;
            this.lblTipoDocumento.Visible = false;
            this.ddlTipoDocumento.Visible = false;
            this.lblNumero.Visible = false;
            this.txtNumero.Visible = false;
            this.txtNumero.Text = "";
            this.nilbNuevo.Focus();
        }
        private void BusquedaTransaccion()
        {
            try
            {
                if (this.gvParametros.Rows.Count > 0)
                {
                    string where = operadores.FormatoWhere(
                        (List<Coperadores>)Session["operadores"]);
                    DataView dvBusqueda = transacciones.GetTransaccionCompletaEgresos(where, Convert.ToInt16(Session["empresa"]));
                    gvTransaccion.DataSource = dvBusqueda;
                    gvTransaccion.DataBind();
                    gvTransaccion.Visible = true;

                    this.nilblRegistros.Text = "Nro. Registros " + Convert.ToString(this.gvTransaccion.Rows.Count);

                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al procesar la consulta de transacciones. Correspondiente a: " + ex.Message, "C");
            }
        }
        private int CompruebaTransaccionExistente()
        {
            try
            {
                object[] objkey = new object[]{                 Convert.ToInt16(Session["empresa"]),                this.txtNumero.Text,                 Convert.ToString(this.ddlTipoDocumento.SelectedValue)
                  };

                if (CentidadMetodos.EntidadGetKey("cCompra", "ppa", objkey).Tables[0].DefaultView.Count > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al comprobar transacción existente. Correspondiente a: " + ex.Message, "C");

                return 1;
            }
        }
        private void ComportamientoConsecutivo()
        {
            if (this.txtNumero.Text.Length == 0)
            {
                this.txtNumero.Enabled = true;
                this.txtNumero.ReadOnly = false;
                this.txtNumero.Focus();
            }
            else
            {
                if (this.txtFecha.Visible == true)
                {
                    if (CompruebaTransaccionExistente() == 1)
                    {
                        ManejoError("Transacción existente. Por favor corrija", "I");

                        return;
                    }
                    else
                    {

                    }
                }

                this.txtNumero.Enabled = false;

                CcontrolesUsuario.HabilitarControles(
                    this.upCabeza.Controls);

                this.nilbNuevo.Visible = false;
                this.txtFecha.Visible = false;
                this.txtFecha.Focus();
            }
        }
        private void verificaPeriodoCerrado(int empresa, DateTime fecha)
        {
            if (periodo.verificaPeriodoCerrado(Convert.ToInt16(Session["empresa"]), fecha) == 1)
            {
                ManejoError("Periodo cerrado. No es posible realizar nuevas transacciones", "I");
                mcancelar();

                return;
            }

        }

        private void verificaPeriodoCreado(int empresa, DateTime fecha)
        {
            if (periodo.verificaPeriodoCreado(Convert.ToInt16(Session["empresa"]), fecha) == 0)
            {
                ManejoError("No existe el periodo. por favor creelo", "I");
                mcancelar();
                return;
            }
        }
        protected void Guardar()
        {
            string operacion = "inserta";
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    bool validartransacicon = false;

                    DateTime fecha = Convert.ToDateTime(txtFecha.Text);

                    this.txtNumero.Text = ConsecutivoTransaccion();
                    bool validarcuenta = false;


                    object[] objValores = new object[] {
                                    Convert.ToDateTime(txtFecha.Text).Year,    //  @año int
                                     false,   //@anulado    bit
                                     txtidBeneficiario.Text, //@beneficiario
                                       0, //@clase  int
                                       "",//@codTercero
                                       null, //@cuentaBanco    varchar
                                       null, //@cuentaBancoAlt varchar
                                       ddlCuentaCorriente.SelectedValue,  //@cuentaCorriente    varchar
                                       null, //@cuentaCorrienteAlt varchar
                                       false, //@ejecutado  bit
                                       Convert.ToInt16(this.Session["empresa"]),  //@empresa    int
                                       null, //@fechaAnulado   datetime
                                       null, //@fechaEjecutado datetime
                                       null, //@fechaModificado    datetime
                                       Convert.ToDateTime(txtFecha.Text), //@fechaPago  datetime
                                       DateTime.Now, //@fechaRegistro  datetime
                                       ddlFormaPago.SelectedValue,  //@formaPago  varchar
                                       Convert.ToDateTime(txtFecha.Text).Month, //@mes    int
                                       false, //@modificado bit
                                       txtNumeroInicial.Text, //@nocomprobante  varchar
                                       txtNota.Text, //@nota   varchar
                                       txtNumero.Text, //@numero varchar
                                       ddlSucursal.SelectedValue.Trim(), //@sucursal
                                       txtidTercero.Text, //@tercero
                                       ddlTipoDocumento.SelectedValue.Trim(), //@tipo   varchar
                                       null, //@usuarioAnulado varchar
                                       null,  //@usuarioEjecutado   varchar
                                       null,  //@usuarioModificado  varchar
                                       this.Session["usuario"].ToString(), //@usuarioRegistro    varchar
                                       Convert.ToDecimal( txtValorPago.Text) //Convert.ToDecimal(hfTotalPagar.Value) //@valorPago  float
                };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete(entidad: "ccxp", operacion: operacion, dBase: "ppa", valores: objValores))
                    {
                        case 0:

                            foreach (GridViewRow gvr in gvLista.Rows)
                            {
                                object[] objValoresDetalle = new object[] {
                                     Convert.ToDateTime(txtFecha.Text).Year,   //@año int
                                       gvr.Cells[2].Text, //@cuentacxp  varchar
                                       null, //@cuentacxpAlt   varchar
                                       0 ,//@diasVencimiento
                                       Convert.ToInt16(this.Session["empresa"]), //@empresa    int
                                       Convert.ToDateTime(txtFecha.Text),  //@fechaFactura
                                       Convert.ToDateTime(txtFecha.Text).Month,  //@mes    int
                                       txtNumero.Text, //@numero varchar
                                       null, //@numeroReferencia   varchar
                                       true,  //@pagado bit
                                       gvr.RowIndex, //@registro   varchar
                                      0,  //@Saldo  float
                                      ddlTipoDocumento.SelectedValue,  //@tipo   varchar
                                        null , //@tipoReferencia varchar
                                      Convert.ToDecimal(gvr.Cells[4].Text), //@valorFactura
                                      Convert.ToDecimal(gvr.Cells[4].Text)  //@valorPago  float
                              };
                                switch (CentidadMetodos.EntidadInsertUpdateDelete(entidad: "ccxpDetalle", operacion: operacion, dBase: "ppa", valores: objValoresDetalle))
                                {
                                    case 1:
                                        validartransacicon = true;
                                        break;
                                }
                            }
                            break;
                        case 1:
                            validartransacicon = true;
                            break;


                    }

                    if (!validartransacicon)
                    {
                        switch (transacciones.contabilizarEgresos(ddlTipoDocumento.Text, txtNumero.Text, Convert.ToInt16(Session["empresa"])))
                        {
                            case 1:
                                validartransacicon = true;
                                break;
                        }

                        switch (transacciones.ActualizaConsecutivo(ddlTipoDocumento.Text, Convert.ToInt16(Session["empresa"])))
                        {
                            case 1:
                                validartransacicon = true;
                                break;
                        }
                    }

                    if (!validartransacicon)
                    {
                        ManejoExito("Registro guardado satisfactiamente", "I");
                        ts.Complete();

                    }
                    else
                    {
                        ManejoError("Error al insertar transacción", "I");
                    }
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar los datos correspondiente a: " + LimpiarCadena(ex.Message), "I");
            }
        }

        private void TabRegistro()
        {
            this.nilbNuevo.Visible = true;
            this.upRegistro.Visible = true;
            this.upConsulta.Visible = false;
            CcontrolesUsuario.InhabilitarControles(upRegistro.Controls);
            if (Convert.ToBoolean(this.Session["editar"]) == true)
            {
                this.upDetalle.Visible = true;
                this.upCabeza.Visible = true;
            }
            this.niimbConsulta.Enabled = true;
            this.niimbRegistro.Enabled = false;
            this.niimbRegistro.BorderStyle = BorderStyle.None;
            this.niimbConsulta.BorderStyle = BorderStyle.Solid;
            this.niimbConsulta.BorderColor = System.Drawing.Color.White;
            this.niimbConsulta.BorderWidth = Unit.Pixel(1);
            this.gvTransaccion.DataSource = null;
            this.gvTransaccion.DataBind();
            this.nilblRegistros.Text = "Nro. Registros 0";
            this.nilblMensajeEdicion.Text = "";
            this.lbCancelar.Visible = false;
            this.lbRegistrar.Visible = false;
            this.niimbConsulta.Enabled = true;
            this.niimbRegistro.Enabled = false;
        }
        private void mcancelar()
        {
            InHabilitaEncabezado();
            CcontrolesUsuario.InhabilitarControles(this.upCabeza.Controls);
            CcontrolesUsuario.LimpiarControles(this.upCabeza.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upDetalle.Controls);
            CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
            this.Session["editar"] = false;
            this.Session["transaccion"] = null;
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.niCalendarFecha.Visible = false;
            this.niCalendarFecha.SelectedDate = Convert.ToDateTime(null);
            this.lbRegistrar.Visible = false;
            this.lbCancelar.Visible = false;
            upCabeza.Visible = false;
            upDetalle.Visible = false;
            //this.ddlNaturaleza.Visible = false;
            CargarTipoTransaccion();
            lbAprobar.Visible = false;
            this.Session["cajaCliente"] = null;
        }
        #endregion Metodos

        #region Evento

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {

                if (Convert.ToString(this.ddlTipoDocumento.SelectedValue).Trim().Length == 0 || this.txtNumero.Text.Trim().Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe ingresar tipo y número de transacción", "warning");
                    return;
                }

                if (this.txtCuenta.Text.Trim().Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe ingresar una cuenta contable", "warning");
                    return;
                }

                if (this.txtDetalle.Text.Trim().Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe ingresar una nota en el detalle", "warning");
                    return;
                }

                if (txtFecha.Text.Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar fecha del documento. Por favor corrija", "warning");
                    return;
                }

                decimal valorTotal = 0;
                foreach (GridViewRow registro in this.gvLista.Rows)
                {
                    valorTotal += Convert.ToDecimal(registro.Cells[4].Text);

                }

                //if (valorTotal + Convert.ToDecimal(txvValorDetalle.Text) > Convert.ToDecimal(txvValor.Text))
                //{
                //    CerroresGeneral.ManejoError(this, GetType(), "Valor del detalle no puede ser mayor al valor total recaudado. Por favor corrija", "warning");
                //    return;
                //}


                if (Convert.ToDecimal(txvValorDetalle.Text) <= 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "El valor no puede ser igual o menor que cero. Por favor corrija", "warning");
                    return;
                }

                //if (ddlTercero.SelectedValue.Length == 0)
                //{
                //    CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar proveedor y sucursal, antes de cargar datos", "warning");
                //    return;
                //}

                Session["editarDetalle"] = false;

                transaccionCaja = new CcajaOtros
                {
                    Notas = txtDetalle.Text,
                    Auxiliar = txtCuenta.Text,
                    NombreCuenta = lblNombreCuenta.Text,
                    Registro = Convert.ToInt16(this.hdRegistro.Value),
                    Valor = Convert.ToDecimal(txvValorDetalle.Text)

                };

                List<CcajaOtros> listaTransaccion = null;

                if (this.Session["cajaCliente"] == null)
                {
                    listaTransaccion = new List<CcajaOtros>();
                    listaTransaccion.Add(transaccionCaja);
                }
                else
                {
                    listaTransaccion = (List<CcajaOtros>)Session["cajaCliente"];
                    listaTransaccion.Add(transaccionCaja);
                }

                int registror = listaTransaccion.Max(r => r.Registro) + 1;
                hdRegistro.Value = registror.ToString();
                listaTransaccion = listaTransaccion.OrderBy(r => r.Registro).ToList();


                this.Session["cajaCliente"] = listaTransaccion;
                this.gvLista.DataSource = listaTransaccion;
                this.gvLista.DataBind();
                nitxtTotalDiferencia.Enabled = false;
                nitxtTotalPagado.Enabled = false;
                CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
                CcontrolesUsuario.LimpiarCombos(this.upDetalle.Controls);
                lblNombreCuenta.Text = "";
                totalizar();
                //TotalizaGrillaReferencia();
                Session["editar"] = false;
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Error al insertar el registro. Correspondiente a: " + ex.Message, "warning");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
            {
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            }
            else
            {

                if (!IsPostBack)
                {
                    if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                           ConfigurationManager.AppSettings["Modulo"].ToString(),
                            nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
                    {
                        ManejoError("Usuario no autorizado para ejecutar esta operación", "I");
                        return;
                    }
                    Session["operadores"] = null;
                    Session["editar"] = false;
                    Session["editarDetalle"] = false;
                    Session["transaccion"] = null;
                    this.Session["cajaCliente"] = null;
                    TabRegistro();
                }
                else
                {
                    txtidBeneficiario.Text = hfBeneficiario.Value.Trim().Length == 0 ? "" : hfBeneficiario.Value;
                }
            }

        }
        protected void nilbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                          ConfigurationManager.AppSettings["Modulo"].ToString(),
                           nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "I");
                return;
            }
            else
            {
                this.Session["editar"] = false;
                ManejoEncabezado();
            }
        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            mcancelar();

        }


        protected void lbRegistrar_Click(object sender, EventArgs e)
        {

            if (gvLista.Rows.Count <= 0)
            {
                ManejoError("El detalle de la transacción debe tener por lo menos un registro", "I");
                return;
            }

            if (ddlSucursal.SelectedValue.Trim().Length == 0)
            {
                ManejoError("Debe seleccionar una sucursal válida", "I");
                return;
            }

            //bool validar = false;

            if (upCabeza.Visible == true)
            {
                if (txtFecha.Enabled == true)
                {
                    if (txtFecha.Text.Trim().Length == 0)
                    {
                        ManejoError("Debe seleccionar una fecha válida", "I");
                        return;
                    }
                }
            }

            if (nitxtTotalDiferencia.Text.Trim().Length == 0)
            {
                ManejoError("La diferencia es no válida", "I");
                return;

            }

            if (Convert.ToDecimal(nitxtTotalDiferencia.Text) > 0)
            {
                ManejoError("La diferencia entre el pago y el detalle no debe ser mayor a 0", "I");
                return;

            }

            if (txtBeneficiario.Text.Trim().Length == 0 && txtBeneficiario.Visible)
            {
                ManejoError("Ingrese un beneficiaro válido", "I");
                return;

            }

            if (txtIdTerceroEncabezado.Visible & txtIdTerceroEncabezado.Text.Trim().Length == 0)
            {
                ManejoError("Ingrese un tercero válido", "I");
                return;
            }

            if (ddlSucursal.Visible & ddlSucursal.SelectedValue.Trim().Length == 0)
            {
                ManejoError("Ingrese una sucursal válida", "I");
                return;
            }

            if (txtValorPago.Visible & txtValorPago.Text.Trim().Length == 0)
            {
                ManejoError("Ingrese una sucursal válida", "I");
                return;
            }

            if (Convert.ToDecimal(txtValorPago.Text) == 0)
            {
                ManejoError("Ingrese una valor de pago mayor a 0", "I");
                return;
            }

            if (Convert.ToBoolean(Session["editar"]) == false)
            {
                Guardar();
            }

        }
        protected void niimbRegistro_Click(object sender, EventArgs e)
        {
            TabRegistro();
        }
        protected void niimbConsulta_Click(object sender, EventArgs e)
        {
            this.upRegistro.Visible = false;
            this.upDetalle.Visible = false;
            this.upCabeza.Visible = false;

            this.upConsulta.Visible = true;

            this.niimbConsulta.BorderStyle = BorderStyle.None;
            this.niimbRegistro.BorderStyle = BorderStyle.Solid;
            this.niimbRegistro.BorderColor = System.Drawing.Color.White;
            this.niimbRegistro.BorderWidth = Unit.Pixel(1);
            this.niimbConsulta.Enabled = false;
            this.niimbRegistro.Enabled = true;

            this.niimbAdicionar.Enabled = true;
            this.imbBusqueda.Enabled = true;

            this.Session["transaccion"] = null;
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            CargaCampos();
            this.niimbConsulta.Enabled = false;
            this.niimbRegistro.Enabled = true;

        }
        private void ComportamientoTransaccion()
        {
            upCabeza.Visible = true;
            CcontrolesUsuario.ComportamientoCampoEntidad(this.upCabeza.Controls,
                   "ccxp", Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"]));
            //CcontrolesUsuario.ComportamientoCampoEntidad(this.upDetalle.Controls,
            //      "ccxpdetalle", Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"]));
            //this.btnRegistrar.Visible = true;
            //chkAprobar.Enabled = false;
            //btnRegistrar.Enabled = true;

        }
        protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                CcontrolesUsuario.HabilitarControles(this.upCabeza.Controls);
                CcontrolesUsuario.LimpiarControles(this.upCabeza.Controls);
                CcontrolesUsuario.HabilitarControles(this.upDetalle.Controls);
                CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
                this.gvLista.DataSource = null;
                this.gvLista.DataBind();
                this.Session["transaccion"] = null;
                this.niCalendarFecha.SelectedDate = Convert.ToDateTime(null);
                this.txtNumero.Text = ConsecutivoTransaccion();
                this.hdTransaccionConfig.Value = CcontrolesUsuario.TipoTransaccionConfig(this.ddlTipoDocumento.SelectedValue, Convert.ToInt16(Session["empresa"]));
                ComportamientoConsecutivo();
                ComportamientoTransaccion();
                CargarCombos();
                txtNombreTerceroEncabezado.Enabled = false;
                btnTerceroEncabezado.Visible = txtIdTerceroEncabezado.Visible;
                ddlSucursal.ClearSelection();
                txtidTercero.Visible = txtIdTerceroEncabezado.Visible;
                txtidTercero.Enabled = false;
                txtidBeneficiario.Visible = txtBeneficiario.Visible;
                hfidTercero.EnableViewState = true;
                txtidBeneficiario.Enabled = false;
                hfBeneficiario.EnableViewState = true;
                txtNombreBeneficiario.Enabled = false;
                upDetalle.Visible = true;

            }
            catch (Exception ex)
            {
                ManejoError("Error al comprobar transacción con referencia. Correspondiente a: " + ex.Message, "C");
            }

        }


        protected void txtNumero_TextChanged(object sender, EventArgs e)
        {
            if (this.txtFecha.Visible == true)
            {
                if (CompruebaTransaccionExistente() == 1)
                {
                    ManejoError("Transacción existente. Por favor corrija", "I");

                    return;
                }
                else
                {

                }
            }

            CcontrolesUsuario.HabilitarControles(this.upCabeza.Controls);

            this.nilbNuevo.Visible = false;
            this.txtFecha.Visible = false;
            this.lbFecha.Focus();
        }
        protected void lbFecha_Click(object sender, EventArgs e)
        {
            this.niCalendarFecha.Visible = true;
            this.txtFecha.Visible = false;
            this.niCalendarFecha.SelectedDate = Convert.ToDateTime(null);
        }

        protected void lbFechaProveedor_Click(object sender, EventArgs e)
        {
        }
        protected void CalendarFecha_SelectionChanged(object sender, EventArgs e)
        {
            this.niCalendarFecha.Visible = false;
            this.txtFecha.Visible = true;
            this.txtFecha.Text = this.niCalendarFecha.SelectedDate.ToString();
            //this.txtFecha.Enabled = false;

            verificaPeriodoCerrado(Convert.ToInt16(Session["empresa"]), Convert.ToDateTime(txtFecha.Text));
            verificaPeriodoCreado(Convert.ToInt16(Session["empresa"]), Convert.ToDateTime(txtFecha.Text));

        }



        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            bool anulado = false;
            if (Convert.ToBoolean(Session["editar"]) == false)
            {
                try
                {
                    List<CcajaOtros> listaTransaccion = null;
                    listaTransaccion = (List<CcajaOtros>)Session["cajaCliente"];
                    listaTransaccion.RemoveAt(e.RowIndex);
                    this.gvLista.DataSource = listaTransaccion;
                    this.gvLista.DataBind();
                    totalizar();
                }
                catch (Exception ex)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Error al eliminar el registro. Correspondiente a: " + ex.Message, "warning");
                }
            }

        }
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool anulado = false;

                if (Convert.ToBoolean(Session["editarDetalle"]) == false)
                    Session["editarDetalle"] = true;
                else
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe de agregar el registro seleccionado para continuar", "w");
                    return;
                }

                this.hdCantidad.Value = this.gvLista.SelectedRow.Cells[4].Text;
                this.hdRegistro.Value = this.gvLista.SelectedRow.Cells[6].Text;

                List<CcajaOtros> listaTransaccion = null;
                listaTransaccion = (List<CcajaOtros>)this.Session["cajaCliente"];
                foreach (CcajaOtros datos in listaTransaccion)
                {
                    if (datos.Registro == Convert.ToInt32(hdRegistro.Value))
                    {
                        if (datos.Auxiliar != null)
                            txtCuenta.Text = datos.Auxiliar.ToString();
                        txvValorDetalle.Text = datos.Valor.ToString();
                        this.Session["cant"] = Convert.ToDecimal(datos.Valor);

                        if (datos.NombreCuenta != null)
                            lblNombreCuenta.Text = datos.NombreCuenta.ToString();

                        if (datos.Notas != null)
                            txtDetalle.Text = datos.Notas.ToString();
                        break;
                    }
                }
                listaTransaccion.RemoveAt(this.gvLista.SelectedRow.RowIndex);
                this.gvLista.DataSource = listaTransaccion;
                this.gvLista.DataBind();
                totalizar();
            }
            catch (Exception ex)
            {
                Session["editarDetalle"] = false;
                CerroresGeneral.ManejoError(this, GetType(), "Error al cargar los campos del registro en el formulario. Correspondiente a: " + ex.Message, "w");
            }
        }
        protected void gvParametros_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<Coperadores> listaOperadores = null;

            try
            {
                listaOperadores = (List<Coperadores>)Session["operadores"];
                listaOperadores.RemoveAt(e.RowIndex);

                this.gvParametros.DataSource = listaOperadores;
                this.gvParametros.DataBind();
                this.gvTransaccion.DataSource = null;
                this.gvTransaccion.DataBind();
                this.nilblRegistros.Text = "Nro. registros 0";
                this.nilblMensajeEdicion.Text = "";

                if (this.gvParametros.Rows.Count == 0)
                {
                    this.imbBusqueda.Visible = false;
                }

                EstadoInicialGrillaTransacciones();
            }
            catch
            {
            }
        }
        protected void niddlOperador_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.niddlOperador.SelectedValue.ToString() == "between")
            {
                this.nitxtValor2.Visible = true;
            }
            else
            {
                this.nitxtValor2.Visible = false;
                this.nitxtValor1.Text = "";
            }

            this.nitxtValor1.Focus();
        }
        protected void niimbAdicionar_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow registro in this.gvParametros.Rows)
            {
                if (Convert.ToString(this.niddlCampo.SelectedValue) == registro.Cells[1].Text && Convert.ToString(this.niddlOperador.SelectedValue) == Server.HtmlDecode(registro.Cells[2].Text) && this.nitxtValor1.Text == registro.Cells[3].Text)
                    return;
            }

            operadores = new Coperadores(Convert.ToString(this.niddlCampo.SelectedValue), Server.HtmlDecode(Convert.ToString(this.niddlOperador.SelectedValue)),
                this.nitxtValor1.Text, this.nitxtValor2.Text);
            List<Coperadores> listaOperadores = null;

            if (this.Session["operadores"] == null)
            {
                listaOperadores = new List<Coperadores>();
                listaOperadores.Add(operadores);
            }
            else
            {
                listaOperadores = (List<Coperadores>)Session["operadores"];
                listaOperadores.Add(operadores);
            }

            this.Session["operadores"] = listaOperadores;

            this.imbBusqueda.Visible = true;
            this.gvParametros.DataSource = listaOperadores;
            this.gvParametros.DataBind();
            this.gvTransaccion.DataSource = null;
            this.gvTransaccion.DataBind();
            this.nilblRegistros.Text = "Nro. Registros 0";
            this.nilblMensajeEdicion.Text = "";
            EstadoInicialGrillaTransacciones();
            imbBusqueda.Focus();
        }
        protected void nitxtValor1_TextChanged(object sender, EventArgs e)
        {
            if (this.nitxtValor1.Text.Length > 0 && Convert.ToString(this.niddlCampo.SelectedValue).Length > 0)
            {
                this.niimbAdicionar.Enabled = true;
                this.imbBusqueda.Enabled = true;
            }
            else
            {
                this.niimbAdicionar.Enabled = false;
                this.imbBusqueda.Enabled = false;
            }

            this.niimbAdicionar.Focus();
        }
        protected void imbBusqueda_Click(object sender, EventArgs e)
        {
            this.nilblMensajeEdicion.Text = "";

            BusquedaTransaccion();
        }
        protected void gvTransaccion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.nilblMensajeEdicion.Text = "";

            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    bool anulado = false;
                    bool ejecutado = false;

                    if (periodo.verificaPeriodoCerrado(Convert.ToInt32(this.Session["empresa"]), Convert.ToDateTime(gvTransaccion.Rows[e.RowIndex].Cells[5].Text)) == 1)
                    {
                        ManejoError("Periodo cerrado no es posible ninguna actividad", "I");
                        return;
                    }

                    foreach (Control objControl in gvTransaccion.Rows[e.RowIndex].Cells[8].Controls)
                    {
                        anulado = ((CheckBox)objControl).Checked;
                    }

                    if (anulado == true)
                    {
                        ManejoError("Registro anulado no es posible su edición", "A");
                        return;
                    }

                    if (tipoTransaccion.RetornaTipoBorrado(this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text, Convert.ToInt16(Session["empresa"])) == "E")
                    {
                        object[] objValores = new object[]{
                         Convert.ToInt16(Session["empresa"]),
                        Convert.ToString(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text).Trim(),
                        Convert.ToString(this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text).Trim()
                    };

                        switch (CentidadMetodos.EntidadInsertUpdateDelete("cTransaccionDetalle", "elimina", "ppa", objValores))
                        {
                            case 0:
                                switch (CentidadMetodos.EntidadInsertUpdateDelete("cTransaccion", "elimina", "ppa", objValores))
                                {
                                    case 0:
                                        this.nilblMensajeEdicion.Text = "Registro Eliminado Satisfactoriamente";
                                        BusquedaTransaccion();
                                        ts.Complete();
                                        break;

                                    case 1:

                                        this.nilblMensajeEdicion.Text = "Error al eliminar el registro. Operación no realizada";
                                        break;
                                }
                                break;

                            case 1:

                                this.nilblMensajeEdicion.Text = "Error al eliminar el registro. Operación no realizada";
                                break;
                        }
                    }
                    else
                    {
                        switch (transacciones.AnulaEgresos(this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text,
                            this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, this.Session["usuario"].ToString().Trim(), Convert.ToInt16(Session["empresa"])))
                        {
                            case 0:
                                this.nilblMensajeEdicion.Text = "Registro Anulado Satisfactoriamente";
                                BusquedaTransaccion();
                                ts.Complete();
                                break;
                            case 1:
                                this.nilblMensajeEdicion.Text = "Error al anular la transacción. Operación no realizada";
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
                }
            }
        }
        protected void gvTransaccion_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            this.nilblMensajeEdicion.Text = "";

            this.Session["editar"] = true;
            this.Session["periodo"] = this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text;
            this.Session["transaccion"] = null;
            Session["editarDetalle"] = false;
            bool anulado = false;
            bool ejecutado = false;

            foreach (Control objControl in gvTransaccion.Rows[e.RowIndex].Cells[6].Controls)
            {
                anulado = ((CheckBox)objControl).Checked;
            }

            if (anulado == true)
            {
                this.nilblMensajeEdicion.Text = "Registro anulado no es posible su edición";
                return;
            }

            foreach (Control objControl in gvTransaccion.Rows[e.RowIndex].Cells[7].Controls)
            {
                ejecutado = ((CheckBox)objControl).Checked;
            }

            if (ejecutado == true)
            {
                this.nilblMensajeEdicion.Text = "Registro aprobado no es posible su edición";
                return;
            }

            DateTime fecha = Convert.ToDateTime(this.gvTransaccion.Rows[e.RowIndex].Cells[4].Text);

            //if (periodo.RetornaPeriodoCerrado(fecha.Year, fecha.Month, Convert.ToInt16(Session["empresa"])) == 1)
            //{
            //    ManejoError("Periodo cerrado contable. No es posible editar transacciones", "I");
            //    return;
            //}

            try
            {

                CargarTipoTransaccion();
                cargarCombosDetalle();
                CargarCombos();
                upCabeza.Visible = true;
                CcontrolesUsuario.HabilitarControles(this.upCabeza.Controls);
                CcontrolesUsuario.HabilitarControles(this.upRegistro.Controls);
                this.ddlTipoDocumento.SelectedValue = this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text;
                this.ddlTipoDocumento.Enabled = false;
                this.txtNumero.Text = this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text;
                this.txtNumero.Enabled = false;
                this.nilbNuevo.Visible = false;
                this.hdTransaccionConfig.Value = CcontrolesUsuario.TipoTransaccionConfig(this.ddlTipoDocumento.SelectedValue, Convert.ToInt16(Session["empresa"]));
                CcontrolesUsuario.LimpiarControles(this.upCabeza.Controls);
                CcontrolesUsuario.ComportamientoCampoEntidad(this.upCabeza.Controls, "cTransaccion", Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"]));
                CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
                CcontrolesUsuario.ComportamientoCampoEntidad(this.upDetalle.Controls, "cTransaccionDetalle", Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"]));

                object[] objCab = new object[] { Convert.ToInt16(Session["empresa"]), this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text };

                foreach (DataRowView encabezado in CentidadMetodos.EntidadGetKey("cTransaccion", "ppa", objCab).Tables[0].DefaultView)
                {
                    this.niCalendarFecha.SelectedDate = Convert.ToDateTime(encabezado.Row.ItemArray.GetValue(8));
                    this.niCalendarFecha.Visible = false;
                    this.txtFecha.Visible = true;
                    this.txtFecha.Text = Convert.ToString(encabezado.Row.ItemArray.GetValue(8));
                    lbFecha.Enabled = false;
                    txtFecha.Enabled = false;
                    this.txtNota.Text = Convert.ToString(encabezado.Row.ItemArray.GetValue(10));
                }
                string tercero = null, sucursal = null, ccosto = null, referencia = null, detalle = null, cuenta = null;
                decimal valorBase = 0, debito = 0, credito = 0; int registro = 0;
                List<Ctransacciones> listaTransaccion = null;

                foreach (DataRowView detalleTransaccion in CentidadMetodos.EntidadGetKey("cTransaccionDetalle", "ppa", objCab).Tables[0].DefaultView)
                {
                    if (detalleTransaccion.Row.ItemArray.GetValue(6).ToString() != null)
                    {
                        registro = Convert.ToInt16(detalleTransaccion.Row.ItemArray.GetValue(6).ToString().Trim());
                    }

                    if (detalleTransaccion.Row.ItemArray.GetValue(7).ToString() != null)
                    {
                        cuenta = detalleTransaccion.Row.ItemArray.GetValue(7).ToString().Trim();
                    }

                    if (detalleTransaccion.Row.ItemArray.GetValue(8).ToString() != null)
                    {
                        tercero = detalleTransaccion.Row.ItemArray.GetValue(8).ToString().Trim();
                    }

                    if (detalleTransaccion.Row.ItemArray.GetValue(9).ToString() != null)
                    {
                        sucursal = detalleTransaccion.Row.ItemArray.GetValue(9).ToString().Trim();
                    }

                    if (detalleTransaccion.Row.ItemArray.GetValue(10).ToString() != null)
                    {
                        ccosto = detalleTransaccion.Row.ItemArray.GetValue(10).ToString().Trim();
                    }

                    if (detalleTransaccion.Row.ItemArray.GetValue(11).ToString() != null)
                    {
                        referencia = detalleTransaccion.Row.ItemArray.GetValue(11).ToString().Trim();
                    }

                    if (detalleTransaccion.Row.ItemArray.GetValue(12).ToString() != null)
                    {
                        valorBase = Convert.ToDecimal(detalleTransaccion.Row.ItemArray.GetValue(12).ToString().Trim());
                    }

                    if (detalleTransaccion.Row.ItemArray.GetValue(13).ToString() != null)
                    {
                        debito = Convert.ToDecimal(detalleTransaccion.Row.ItemArray.GetValue(13).ToString().Trim());
                    }

                    if (detalleTransaccion.Row.ItemArray.GetValue(14).ToString() != null)
                    {
                        credito = Convert.ToDecimal(detalleTransaccion.Row.ItemArray.GetValue(14).ToString().Trim());
                    }
                    if (detalleTransaccion.Row.ItemArray.GetValue(15).ToString() != null)
                    {
                        detalle = detalleTransaccion.Row.ItemArray.GetValue(15).ToString().Trim();
                    }

                    transacciones = new Ctransacciones(cuenta, tercero, sucursal, ccosto, valorBase, referencia, debito, credito, detalle, registro, anulado);

                    hdRegistro.Value = registro.ToString();

                    if (this.Session["transaccion"] == null)
                    {
                        listaTransaccion = new List<Ctransacciones>();
                        listaTransaccion.Add(transacciones);
                    }
                    else
                    {
                        listaTransaccion = (List<Ctransacciones>)Session["transaccion"];
                        listaTransaccion.Add(transacciones);
                    }
                    this.Session["transaccion"] = listaTransaccion;
                }

                this.gvLista.DataSource = listaTransaccion;
                this.gvLista.DataBind();
                //this.btnRegistrar.Visible = true;
                TabRegistro();
                totalizar();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la transacción. Correspondiente a: " + ex.Message, "A");
            }

        }

        protected void ddlCentroCosto_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEmpleados();
            cargarCombosDetalle();
        }


        //protected void ddlConceptoDetalle_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    VerificaValoryCantidad(txtTercero.SelectedValue.ToString());
        //}

        private void VerificaValoryCantidad(string concepto)
        {
            //txtReferenciaD.Enabled = tipoTransaccion.ValidaCantidadValor(Convert.ToInt16(Session["empresa"]), concepto);
            //txvValor.Enabled = !tipoTransaccion.ValidaCantidadValor(Convert.ToInt16(Session["empresa"]), concepto);
        }
        protected void txtFecha_TextChanged(object sender, EventArgs e)
        {

            try
            {
                Convert.ToDateTime(txtFecha.Text);
            }
            catch
            {
                ManejoError("Formato de fecha no valido", "I");

                txtFecha.Text = "";
                txtFecha.Focus();
                return;
            }
            verificaPeriodoCerrado(Convert.ToInt16(Session["empresa"]), Convert.ToDateTime(txtFecha.Text));

            verificaPeriodoCreado(Convert.ToInt16(Session["empresa"]), Convert.ToDateTime(txtFecha.Text));
        }


        protected void lbImprimir_Click(object sender, EventArgs e)
        {

        }
        protected void ddlConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            //VerificaValoryCantidad(ddlConcepto.SelectedValue.ToString());
        }

        protected void gvLista_Sorting(object sender, GridViewSortEventArgs e)
        {

            gvLista.DataBind();
        }
        protected void ddlCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                manejoCuenta();

                UpdatePanel cupdetalle = ((UpdatePanel)((DropDownList)(sender)).Parent.Parent);

                //if (txtTercero.Visible)
                //{
                //    ScriptManager1.SetFocus(((DropDownList)cupdetalle.FindControl(txtTercero.ClientID)));
                //    txtTercero.SelectedValue = "";
                //}
                //else
                //{
                //    if (ddlCcosto.Visible)
                //    {
                //        ScriptManager1.SetFocus(ddlCcosto);
                //    }
                //}

                // ScriptManager sm = (ScriptManager)Page.FindControl("ScriptManager1");
                //sm.SetFocus(ddlTercero.ID);


            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la configuracion de la cuenta debido a: " + ex.Message, "C");
            }
        }



        protected void ddlTercero_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                manejoTercero();

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar sucursales. Correspondiente a: " + ex.Message, "C");
            }
        }



        private void cargarRegistro()
        {
            gvLista.DataSource = transacciones.GetTransaccionContable(ddlTipoDocumento.SelectedValue.Trim(), txtNumero.Text, Convert.ToInt32(this.Session["empresa"]));
            gvLista.DataBind();

        }


        protected void txtTercero_TextChanged(object sender, EventArgs e)
        {
            try
            {
                manejoTercero();
            }
            catch (Exception ex)
            {
                ManejoError("Error al retornar nombre tercero debido a:" + ex.Message, "I");
            }
        }

        private void manejoTercero()
        {
            //lblNombreTercero.Visible = false;
            //lblNombreTercero.Text = "";
            //foreach (DataRowView drv in terceros.RetornaDatosTerceroCodigo(txtTercero.Text, Convert.ToInt16(this.Session["empresa"])))
            //{
            //    lblNombreTercero.Text = drv.Row.ItemArray.GetValue(7).ToString();
            //    lblNombreTercero.Visible = true;
            //}

            //if (Convert.ToBoolean(hdSA.Value))
            //{
            //    if (Convert.ToBoolean(hdProveedor.Value))
            //    {
            //        cargarSucursalProveedor();
            //    }
            //    else
            //        if (Convert.ToBoolean(hdCliente.Value))
            //    {
            //        DataView dvcliente = terceros.SeleccionaSucursalCliente(Convert.ToInt16(Session["empresa"]), txtTercero.Text.Trim());
            //        this.ddlSucursal.DataSource = dvcliente;
            //        this.ddlSucursal.DataValueField = "codigo";
            //        this.ddlSucursal.DataTextField = "cadena";
            //        this.ddlSucursal.DataBind();
            //        this.ddlSucursal.Items.Insert(0, new ListItem("", ""));
            //    }
            //}
        }


        protected void txtIdTerceroEncabezado_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (gvLista.Rows.Count == 0)
                {
                    txtidTercero.Text = hfidTercero.Value;
                    txtBeneficiario.Text = txtIdTerceroEncabezado.Text;
                    txtidBeneficiario.Text = txtidTercero.Text;
                    hfBeneficiario.Value = hfidTercero.Value;
                    if (terceros.verificaCodigoTercero(txtIdTerceroEncabezado.Text.Trim(), Convert.ToInt16(Session["empresa"])) == 1 && gvLista.Rows.Count == 0)
                    {
                        cargarSucursalProveedor();

                        //upDetalle.Visible = true;
                        //CcontrolesUsuario.LimpiarControles(upDetalle.Controls);
                        nombreTerceroEncabezado();
                        //txtTotalDebito.Visible = true;
                        //lblTotalDebito.Visible = true;
                        //txtTotalCredito.Visible = true;
                        //lblTotalCredito.Visible = true;
                        //txtTotalDebito.Enabled = false;
                        //txtTotalCredito.Enabled = false;
                        //txtTotalCredito.Text = "0.00";
                        //txtTotalDebito.Text = "0.00";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "terceroencabezado();", true);
                        ClientScript.RegisterStartupScript(this.GetType(), "myScript", "<script>javascript:terceroencabezado();</script>");

                        upDetalle.Visible = false;
                        CcontrolesUsuario.LimpiarControles(upDetalle.Controls);
                    }
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al validar tercero debido a: " + ex.Message, "I");
            }
        }

        private void nombreTerceroEncabezado()
        {
            foreach (DataRowView drv in terceros.RetornaDatosTerceroCodigo(txtidTercero.Text, Convert.ToInt16(this.Session["empresa"])))
            {
                txtNombreTerceroEncabezado.Text = drv.Row.ItemArray.GetValue(7).ToString();
                txtNombreBeneficiario.Text = drv.Row.ItemArray.GetValue(7).ToString();
            }
        }

        protected void gvTransaccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    bool anulado = false;

            //    if (periodo.verificaPeriodoCerrado(Convert.ToInt32(this.Session["empresa"]), Convert.ToDateTime(gvTransaccion.SelectedRow.Cells[6].Text)) == 1)
            //    {
            //        ManejoError("Periodo cerrado no es posible ninguna actividad", "I");
            //        return;
            //    }

            //    foreach (Control objControl in gvTransaccion.SelectedRow.Cells[9].Controls)
            //    {
            //        anulado = ((CheckBox)objControl).Checked;
            //    }

            //    if (anulado == true)
            //    {
            //        ManejoError("Registro anulado no es posible su edición", "A");
            //        return;
            //    }

            //    DataView dvCabeza = transacciones.GetTransaccionContableEncabezado(tipo: gvTransaccion.SelectedRow.Cells[2].Text, numero: gvTransaccion.SelectedRow.Cells[3].Text, empresa: Convert.ToInt32(this.Session["empresa"]));
            //    TabRegistro();
            //    CcontrolesUsuario.HabilitarControles(upRegistro.Controls);

            //    upGeneral.Visible = true;
            //    upConsulta.Visible = false;
            //    upCabeza.Visible = true;
            //    CargarCombos();
            //    cargarCombosDetalle();
            //    ManejoEncabezado();

            //    foreach (DataRowView drv in dvCabeza)
            //    {
            //        ddlTipoDocumento.SelectedValue = drv.Row.ItemArray.GetValue(1).ToString();
            //        txtNumero.Text = drv.Row.ItemArray.GetValue(2).ToString();
            //        ddlTipoDocumento.Enabled = false;
            //        txtNumero.Enabled = false;
            //        txtFecha.Text = drv.Row.ItemArray.GetValue(6).ToString();
            //        if (!(drv.Row.ItemArray.GetValue(7) is DBNull))
            //        {
            //            ddlReferencia.SelectedValue = drv.Row.ItemArray.GetValue(7).ToString();
            //        }
            //        if (!(drv.Row.ItemArray.GetValue(8) is DBNull))
            //        {
            //            txtIdTerceroEncabezado.Text = drv.Row.ItemArray.GetValue(8).ToString();
            //            nombreTerceroEncabezado();
            //        }
            //        if (!(drv.Row.ItemArray.GetValue(9) is DBNull))
            //        {
            //            txtNota.Text = drv.Row.ItemArray.GetValue(9).ToString();
            //        }
            //        chkAprobar.Checked = Convert.ToBoolean(drv.Row.ItemArray.GetValue(18));

            //    }
            //    ComportamientoTransaccion();
            //    cargarRegistro();
            //    upDetalle.Visible = true;

            //    CcontrolesUsuario.LimpiarControles(upDetalle.Controls);
            //    CcontrolesUsuario.HabilitarControles(upDetalle.Controls);
            //    manejoCuenta();
            //    if (chkAprobar.Checked)
            //    {
            //        gvLista.Enabled = false;
            //        btnRegistrar.Visible = false;
            //        lbCancelar.Visible = true;
            //        lbAprobar.Visible = false;
            //        btnRegistrar.Visible = false;
            //    }
            //    else
            //    {
            //        btnRegistrar.Visible = true;
            //        lbCancelar.Visible = true;
            //        lbAprobar.Visible = true;
            //        btnRegistrar.Visible = true;
            //    }
            //    lblIdTerceroEncabezado.Visible = true;
            //    btnTerceroEncabezado.Visible = true;
            //    txtNombreTerceroEncabezado.Enabled = false;
            //    totalizar();
            //    txtTotalCredito.Enabled = false;
            //    txtTotalDebito.Enabled = false;
            //}
            //catch (Exception ex)
            //{
            //    ManejoError("Error al cargar la transacción debido a:   " + ex.Message, "I");
            //}
        }

        protected void lbAprobar_Click(object sender, EventArgs e)
        {
            try
            {
                switch (transacciones.aprobarTransaccionContabilidad(tipo: ddlTipoDocumento.SelectedValue, numero: txtNumero.Text, empresa: Convert.ToInt32(this.Session["empresa"]), usuario: this.Session["usuario"].ToString()))
                {
                    case 0:
                        ManejoExito("Transacción aprobada satisfactoriamente", "I");
                        break;
                    case 1:
                        ManejoError("Error al aprobar transacción", "I");
                        break;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al aprobar la transacción debido a: " + ex.Message, "I");
            }
        }


        protected void chkSel_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string ea = "";
                string separador = ";";

                foreach (GridViewRow gvr in gvLista.Rows)
                {
                    var chkSel = gvr.Cells[2].FindControl("chkSel") as CheckBox;
                    if (chkSel.Checked)
                        ea += gvr.Cells[2].Text + ";";
                }

            }
            catch (Exception ex)
            {
                ManejoError("Error al seleccionar entrada debido a: " + ex.Message, "I");
            }

        }

        protected void txtCuenta_TextChanged(object sender, EventArgs e)
        {
            manejoCuenta();

            int verifica = puc.verificaAuxiliar(txtCuenta.Text.Trim(), Convert.ToInt16(this.Session["empresa"]));
            if (verifica == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "buscarcuenta", "buscarCuenta();", true);
            }
        }


        protected void chkMultiplesPagos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos debido a: " + ex.Message, "I");
            }
        }


        protected void txtNota_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlCondicionPago_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion Evento








    }
}