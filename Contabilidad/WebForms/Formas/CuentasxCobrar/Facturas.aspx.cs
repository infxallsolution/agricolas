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
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.CuentasxCobrar
{
    public partial class Facturas : BasePage
    {
        #region Instancias
        CentidadMetodos CentidadMetodos = new CentidadMetodos();
        Security seguridad = new Security();
        CIP ip = new CIP();
        Cimpuestos transaccionImpuesto = new Cimpuestos();
        CcxcFactura transaccionCxC = new CcxcFactura();
        Ctransacciones transacciones = new Ctransacciones();
        CtipoTransaccion tipoTransaccion = new CtipoTransaccion();
        Coperadores operadores = new Coperadores();
        Cperiodos periodo = new Cperiodos();
        Citems item = new Citems();
        Cdestinos destino = new Cdestinos();
        Cfacturas transaccionAlmacen = new Cfacturas();
        Cterceros tercero = new Cterceros();

        #endregion Instancias

        #region Metodos


        public string limpiarMensaje(string strIn)
        {
            try
            {
                return Regex.Replace(strIn, @"[^\w\.@-]", " ",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }

        private void BusquedaTransaccion()
        {
            try
            {
                if (this.gvParametros.Rows.Count > 0)
                {
                    string where = operadores.FormatoWhere((List<Coperadores>)Session["operadores"]);

                    this.gvTransaccion.Visible = true;
                    this.gvTransaccion.DataSource = transacciones.GetTransaccionFacturas(where, Convert.ToInt16(Session["empresa"]));
                    this.gvTransaccion.DataBind();
                    this.nilblRegistros.Text = "Nro. Registros " + Convert.ToString(this.gvTransaccion.Rows.Count);
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al procesar la consulta de transacciones. Correspondiente a: " + ex.Message, "C");
            }
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

        private void TabRegistro()
        {
            this.upRegistro.Visible = true;
            this.upConsulta.Visible = false;

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

        }
        private void cancelarTransaccion()
        {
            InHabilitaEncabezado();
            CcontrolesUsuario.InhabilitarControles(this.upCabeza.Controls);
            CcontrolesUsuario.LimpiarControles(this.upCabeza.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upDetalle.Controls);
            CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
            this.Session["transaccion"] = null;
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.niCalendarFecha.Visible = false;
            this.niCalendarFecha.SelectedDate = Convert.ToDateTime(null);
            ddlTercero.ClearSelection();
            ddlSucursal.ClearSelection();
            this.lbRegistrar.Visible = false;
            this.lbCancelar.Visible = false;
            upDetalle.Visible = false;
            upCabeza.Visible = false;
            upConsulta.Visible = false;
        }

        private void Guardar()
        {
            string numero = "", periodo = Convert.ToString(Convert.ToDateTime(txtFecha.Text).Year) + Convert.ToString(Convert.ToDateTime(txtFecha.Text).Month).PadLeft(2, '0').Trim(), tercero = "",
                usuarioAnulado = null, solicitante = "", condicionPago = "", terceroSucursal = null;
            bool verificacion = true, anulado = false;
            decimal cantidadAprobada = 0;

            if (gvLista.Rows.Count == 0 && Convert.ToBoolean(TipoTransaccionConfig(1)) == false && Convert.ToBoolean(TipoTransaccionConfig(12)) == false)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Detalle de la transacción vacio. No es posible registrar la transacción", "warning");
                return;
            }

            if (Convert.ToString(this.ddlTipoDocumento.SelectedValue).Trim().Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Por favor seleccione un tipo de movimiento. No es posible registrar la transacción", "warning");
                return;
            }
            if (this.txtNumero.Text.Trim().Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "El número de transacción no puede estar vacio. No es posible registrar la transacción", "warning");
                return;
            }
            if (Convert.ToString(this.niCalendarFecha.SelectedDate).Trim().Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar una fecha. No es posible registrar la transacción", "warning");
                return;
            }
            if (this.ddlSucursal.Enabled == true)
            {
                if (Convert.ToString(this.ddlSucursal.SelectedValue).Trim().Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar un sucursal para guardar la transacción", "warning");
                    return;
                }
            }

            if (gvCxC.Rows.Count == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Detalle de la CxC vacio. No es posible registrar la transacción", "warning");
                return;
            }

            if (this.ddlTercero.Enabled == false)
                tercero = null;
            else
                tercero = Convert.ToString(this.ddlTercero.SelectedValue);

            if (this.ddlSucursal.Enabled == false)
                terceroSucursal = null;
            else
                terceroSucursal = Convert.ToString(this.ddlSucursal.SelectedValue);

            if (ddlCondicionPago.Visible == false)
                condicionPago = null;
            else
                condicionPago = Convert.ToString(ddlCondicionPago.SelectedValue);

            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                    {
                        this.txtNumero.Enabled = false;
                        numero = this.txtNumero.Text.Trim();
                        object[] objKey = new object[] { (int)this.Session["empresa"], numero, ddlTipoDocumento.SelectedValue };
                        CentidadMetodos.EntidadInsertUpdateDelete("cFacturaDetalle", "elimina", "ppa", objKey);
                        CentidadMetodos.EntidadInsertUpdateDelete("cFactura", "elimina", "ppa", objKey);

                    }
                    else
                    {
                        if (this.txtNumero.Enabled == false)
                            numero = transacciones.RetornaNumeroTransaccion(Convert.ToString(this.ddlTipoDocumento.SelectedValue), (int)this.Session["empresa"]);
                        else
                            numero = this.txtNumero.Text.Trim();
                    }

                    string tipo = ddlTipoDocumento.SelectedValue;

                    object[] objValores = new object[]{
                                    Convert.ToDateTime(txtFecha.Text).Year,//@año
                                     false,//@anulado
                                    false,//@aprobado
                                    false,//@cerrado
                                    ddlCondicionPago.SelectedValue,
                                    Convert.ToInt16(Session["empresa"]),//@empresa
                                    Convert.ToDateTime(txtFecha.Text),//@fecha
                                    null,//@fechaAnulado
                                    null,//@fechaAprobado
                                    null,//@fechaCierre
                                    DateTime.Now,//@fechaRegistro
                                    Convert.ToDateTime(txtFecha.Text).Month,//@mes
                                    Server.HtmlDecode(this.txtObservacion.Text.Trim()),//@notas
                                    numero,//@numero
                                    periodo,//@periodo
                                     null,//@referencia
                                    0,//@signo
                                    terceroSucursal,//@surcursalTercero
                                    tercero,//@tercero
                                    ddlTipoDocumento.SelectedValue,//@tipo
                                    null,
                                    Convert.ToDecimal(nitxtTotalRetencion.Text),//@totalDescuento
                                    Convert.ToDecimal(nitxtTotalImpuesto.Text),//@totalImpuesto
                                    Convert.ToDecimal(nitxtTotal.Text),//@totalNeto
                                    Convert.ToDecimal(nitxtTotalValorBruto.Text),//@totalValorBruto
                                    this.Session["usuario"].ToString(),//@usuario
                                    null,//@usuarioAbrobado
                                    null,//@usuarioAnulado
                                    0//@vigencia
                                };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("cFactura", "inserta", "ppa", objValores))
                    {
                        case 0:

                            foreach (GridViewRow cuerpo in this.gvLista.Rows)
                            {
                                string bodega = null, ccosto = null, destino = null;

                                if (Convert.ToBoolean(TipoTransaccionConfig(14)) == true)
                                    cantidadAprobada = Convert.ToDecimal(cuerpo.Cells[5].Text);
                                else
                                    cantidadAprobada = 0;

                                if (Server.HtmlDecode(cuerpo.Cells[9].Text).Trim().Length > 0)
                                    ccosto = Server.HtmlDecode(cuerpo.Cells[9].Text);
                                if (Server.HtmlDecode(cuerpo.Cells[8].Text).Trim().Length > 0)
                                    destino = Server.HtmlDecode(cuerpo.Cells[8].Text);


                                if (((CheckBox)cuerpo.FindControl("chkAnulado")).Checked)
                                {
                                    anulado = ((CheckBox)cuerpo.FindControl("chkAnulado")).Checked;
                                    usuarioAnulado = Session["usuario"].ToString();
                                }
                                else
                                {
                                    anulado = false;
                                    usuarioAnulado = null;
                                }

                                object[] objValoresCuerpo = new object[]{
                                                      Convert.ToDateTime(txtFecha.Text).Year,//@año
                                                        anulado,//@anulado
                                                      false,//@aprobado
                                                      bodega,//@bodega
                                                      Convert.ToDecimal(cuerpo.Cells[5].Text),//@cantidadRequerida
                                                      Convert.ToDecimal(cuerpo.Cells[5].Text),//@cantidadRequerida
                                                      ccosto,//@ccosto
                                                      false,//@cerrado
                                                      destino,//@destino
                                                      null,
                                                      Server.HtmlDecode(cuerpo.Cells[3].Text),//@detalle
                                                      false,//@ejecutado
                                                      Convert.ToInt16(Session["empresa"]),//@empresa
                                                      null,//@fechaAnulado
                                                      null,//@fechaAprobado
                                                      null,//@fechaCierre
                                                      null,
                                                      Convert.ToInt16(cuerpo.Cells[2].Text),//@item
                                                      Convert.ToDateTime(txtFecha.Text).Month,//@mes
                                                      false,
                                                      numero,//@numero
                                                      null,//@numeroReferencia
                                                      0,//@pDescuento
                                                      periodo,//@periodo
                                                      false,
                                                      cuerpo.RowIndex,//@registro
                                                      Convert.ToDecimal(cuerpo.Cells[5].Text),//@saldo
                                                      false,
                                                      ddlTipoDocumento.SelectedValue,//@tipo
                                                      null,
                                                      null,//@tipoReferencia
                                                      Server.HtmlDecode(cuerpo.Cells[4].Text),//@uMedida
                                                      usuarioAnulado,//@usuarioAnulado
                                                      null,//@usuarioAprobado
                                                      null,//@usuarioCierre
                                                      null,
                                                      0,//@valorDescuento
                                                      0,//@valorSaldo
                                                      Convert.ToDecimal(cuerpo.Cells[7].Text),//@valorTotal
                                                      Convert.ToDecimal(cuerpo.Cells[6].Text)//@valorUnitario
                                                };

                                switch (CentidadMetodos.EntidadInsertUpdateDelete("cFacturaDetalle", "inserta", "ppa", objValoresCuerpo))
                                {
                                    case 1:
                                        verificacion = false;
                                        break;
                                }
                            }

                            if (gvImpuesto.Rows.Count > 0)
                            {
                                decimal valorBruto = 0;
                                valorBruto = Convert.ToDecimal(nitxtTotalValorBruto.Text) - Convert.ToDecimal(nitxtTotalRetencion.Text);
                                foreach (GridViewRow registro in this.gvImpuesto.Rows)
                                {

                                    object[] objValoresCuerpo = new object[]{
                                                      Convert.ToDateTime(txtFecha.Text).Year,//@año
                                                      Convert.ToDecimal(registro.Cells[3].Text),//@baseGravable
                                                      Convert.ToDecimal(registro.Cells[4].Text),//@@baseMinima
                                                      registro.Cells[0].Text,//@concepto
                                                      Convert.ToInt16(Session["empresa"]),//@empresa
                                                      Convert.ToDateTime(txtFecha.Text).Month,//@mes
                                                      numero,//@numero
                                                      periodo,//@periodo
                                                      registro.RowIndex,//@registro
                                                      Convert.ToDecimal(registro.Cells[2].Text),//tasa
                                                      ddlTipoDocumento.SelectedValue,//@tipo
                                                      valorBruto,
                                                      Convert.ToDecimal(registro.Cells[5].Text)//valorImpuesto
                                                };

                                    switch (CentidadMetodos.EntidadInsertUpdateDelete("cFacturaImpuesto", "inserta", "ppa", objValoresCuerpo))
                                    {
                                        case 1:
                                            verificacion = true;
                                            break;
                                    }

                                }
                            }

                            if (gvCxC.Rows.Count > 0)
                            {
                                object[] objValoresCabezaCxC = new object[]{
                                                       Convert.ToDateTime(txtFecha.Text).Year,//@año
                                                       anulado,//@anulado
                                                       null,
                                                       false,
                                                      Convert.ToInt16(Session["empresa"]),//@empresa
                                                      Convert.ToDateTime(txtFecha.Text),
                                                      null,
                                                      null,
                                                      null,
                                                      DateTime.Now,
                                                      Convert.ToDateTime(txtFecha.Text).Month,//@mes
                                                      false,
                                                      numero,//@numero
                                                      txtObservacion.Text,
                                                      numero,
                                                      tercero,
                                                      ddlTipoDocumento.SelectedValue,
                                                      Session["usuario"].ToString(),
                                                      null,
                                                      null,
                                                      null
                                                };

                                switch (CentidadMetodos.EntidadInsertUpdateDelete("cTransaccion", "inserta", "ppa", objValoresCabezaCxC))
                                {
                                    case 0:
                                        decimal valorBruto = 0;
                                        valorBruto = Convert.ToDecimal(nitxtTotalValorBruto.Text) - Convert.ToDecimal(nitxtTotalRetencion.Text);
                                        foreach (GridViewRow registro in this.gvCxC.Rows)
                                        {

                                            object[] objValoresCuerpo = new object[]{
                                                      Convert.ToDateTime(txtFecha.Text).Year,//@año
                                                      Convert.ToDecimal(nitxtTotalValorBruto.Text),//@baseGravable
                                                      null,
                                                      Convert.ToDecimal(registro.Cells[2].Text),//@@baseMinima
                                                      Convert.ToDecimal(registro.Cells[2].Text),//@@baseMinima
                                                      ((DropDownList)(registro.Cells[0].FindControl("ddlAuxiliarCxC"))).SelectedValue.Trim(),
                                                      null,
                                                      Convert.ToDecimal(registro.Cells[1].Text),//@@baseMinima
                                                      Convert.ToDecimal(registro.Cells[1].Text),//@@baseMinima
                                                      0,
                                                        Convert.ToInt16(Session["empresa"]),//@empresa
                                                      Convert.ToDateTime(txtFecha.Text),
                                                      Convert.ToDateTime(txtFecha.Text).Month,//@mes
                                                      txtObservacion.Text,
                                                      numero,//@numero
                                                      numero,
                                                      0,
                                                      registro.RowIndex,//@registro
                                                      terceroSucursal,
                                                      tercero,
                                                      ddlTipoDocumento.SelectedValue,//@tipo
                                                      ddlTipoDocumento.SelectedValue,//@tipo
                                                      
                                                };

                                            switch (CentidadMetodos.EntidadInsertUpdateDelete("cTransaccionDetalle", "inserta", "ppa", objValoresCuerpo))
                                            {
                                                case 1:
                                                    verificacion = true;
                                                    break;
                                            }
                                        }
                                        break;
                                    case 1:
                                        verificacion = true;
                                        break;
                                }
                            }

                            if (verificacion == true)
                            {
                                if (this.txtNumero.Enabled == false && Convert.ToBoolean(this.Session["editar"]) == false)
                                {
                                    if (tipoTransaccion.ActualizaConsecutivo(Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"])) != 0)
                                    {
                                        CerroresGeneral.ManejoError(this, GetType(), "Error al actualizar el consecutivo. Operación no realizada", "warning");
                                        return;
                                    }
                                }

                                ManejoExito("Transacción registrada satisfactoriamente. Transacción número " + numero, "A");
                                ts.Complete();

                                ImprimrcFactura(Convert.ToString(this.Session["empresa"]), tipo, numero);
                            }
                            else
                                CerroresGeneral.ManejoError(this, GetType(), "Error al insertar detalle de transacción. Operación no realizada", "warning");
                            break;
                        case 1:
                            CerroresGeneral.ManejoError(this, GetType(), "Error al insertar la transacción. Operación no realizada", "warning");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
                }
            }
        }

        private void validaFecha()
        {
            this.niCalendarFecha.Visible = false;
            this.txtFecha.Visible = true;
            try
            {
                Convert.ToDateTime(txtFecha.Text);
            }
            catch
            {
                txtFecha.Text = "";
                txtFecha.Focus();
                CerroresGeneral.ManejoError(this, GetType(), "formato de fecha no valido..", "w");
                return;
            }

            if (periodo.RetornaPeriodoCerrado(Convert.ToDateTime(txtFecha.Text).Year, Convert.ToDateTime(txtFecha.Text).Month, (int)this.Session["empresa"]) == 1)
                ManejoError("Periodo cerrado. No es posible realizar nuevas transacciones", "I");

            else
            {
                if (Convert.ToBoolean(this.Session["editar"]) != true)
                {
                    if (CompruebaTransaccionExistente() == 1)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "Transacción existente. Por favor corrija", "warning");
                        return;
                    }


                    CcontrolesUsuario.ComportamientoCampoEntidad(this.upDetalle.Controls, "cFacturaDetalle", Convert.ToString(this.ddlTipoDocumento.SelectedValue), (int)this.Session["empresa"]);
                    CargarCentroCosto();
                    CargaDestinos();
                    cargarBodega();

                    if (ddlProducto.Visible == true)
                    {
                        txtFiltroProducto.Visible = true;
                        txtFiltroProducto.Enabled = true;
                        txtFiltroProducto.ReadOnly = false;
                    }

                    this.txtFiltroProveedor.Focus();
                    this.btnRegistrar.Visible = true;
                    btnRegistrar.Enabled = true;
                    if (Convert.ToBoolean(TipoTransaccionConfig(1)) == true && Convert.ToBoolean(TipoTransaccionConfig(12)) == false)
                        this.btnRegistrar.Visible = false;
                }
            }
        }
        private void cargarBodega()
        {
            try
            {

                if (Convert.ToBoolean(TipoTransaccionConfig(16)) == true)
                {
                    DataView dvBodega = CentidadMetodos.EntidadGet("iBodega", "ppa").Tables[0].DefaultView;
                    dvBodega.RowFilter = "tipo = 'V' and empresa=" + Session["empresa"].ToString();
                    this.ddlBodega.DataSource = dvBodega;
                    this.ddlBodega.DataValueField = "codigo";
                    this.ddlBodega.DataTextField = "descripcion";
                    this.ddlBodega.DataBind();
                    this.ddlBodega.Items.Insert(0, new ListItem("", ""));
                }
                else
                {
                    this.ddlBodega.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("iBodega", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                    this.ddlBodega.DataValueField = "codigo";
                    this.ddlBodega.DataTextField = "descripcion";
                    this.ddlBodega.DataBind();
                    this.ddlBodega.Items.Insert(0, new ListItem("", ""));
                }


            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar bodegas. Correspondiente a: " + ex.Message, "C");
            }
        }
        protected string nombrePaginaActual()
        {
            string[] arrResult = HttpContext.Current.Request.RawUrl.Split('/');
            string result = arrResult[arrResult.GetUpperBound(0)];
            arrResult = result.Split('?');
            return arrResult[arrResult.GetLowerBound(0)];
        }
        private void ManejoError(string error, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), error, "e");
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "er", error, ObtenerIP(), Convert.ToInt16(Session["empresa"]));
        }
        private void ManejoExito(string mensaje, string operacion)
        {
            CcontrolesUsuario.InhabilitarControles(this.upCabeza.Controls);
            CcontrolesUsuario.LimpiarControles(this.upCabeza.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upDetalle.Controls);
            CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
            InHabilitaEncabezado();
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.gvImpuesto.DataSource = null;
            this.gvImpuesto.DataBind();
            this.ddlTercero.DataSource = null;
            this.ddlTercero.DataBind();
            this.gvCxC.DataSource = null;
            this.gvCxC.DataBind();
            gvImpuesto.Visible = false;
            this.Session["impuesto"] = null;
            this.Session["transaccion"] = null;
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "i");
            this.lbRegistrar.Visible = false;
            nitxtTotalRetencion.Visible = false;
            nitxtTotal.Visible = false;
            nitxtTotalImpuesto.Visible = false;
            nitxtTotalValorBruto.Visible = false;
            nilblValorTotal.Visible = false;
            nilblValorTotal0.Visible = false;
            nilblValorTotal1.Visible = false;
            nilblValorTotal2.Visible = false;
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
            upCabeza.Visible = false;
            upDetalle.Visible = false;
        }
        private void CargaCampos()
        {
            try
            {
                this.niddlCampo.DataSource = transacciones.GetCamposEntidades("cFactura", "");
                this.niddlCampo.DataValueField = "name";
                this.niddlCampo.DataTextField = "name";
                this.niddlCampo.DataBind();
                this.niddlCampo.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos para edición. Correspondiente a: " + ex.Message, "C");
            }
        }
        private void CargarCombos()
        {
            try
            {
                this.ddlCondicionPago.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cCondicionPago", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlCondicionPago.DataValueField = "codigo";
                this.ddlCondicionPago.DataTextField = "descripcion";
                this.ddlCondicionPago.DataBind();
                this.ddlCondicionPago.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar condicción de pago. Correspondiente a: " + ex.Message, "C");
            }
        }

        private void CargarUmedida()
        {
            try
            {
                this.ddlUmedida.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("gUnidadMedida", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlUmedida.DataValueField = "codigo";
                this.ddlUmedida.DataTextField = "descripcion";
                this.ddlUmedida.DataBind();
                this.ddlUmedida.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar unidades de medida. Correspondiente a: " + ex.Message, "C");
            }
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
                this.ddlTipoDocumento.DataSource = tipoTransaccion.GetTipoTransaccionModuloForma(Convert.ToInt16(Session["empresa"]), 28);
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

        }
        private string ConsecutivoTransaccion()
        {
            string numero = "";

            try
            {
                numero = transacciones.RetornaNumeroTransaccion(Convert.ToString(this.ddlTipoDocumento.SelectedValue), (int)this.Session["empresa"]);
            }
            catch (Exception ex)
            {
                ManejoError("Error al obtener el número de transacción. Correspondiente a: " + ex.Message, "C");
            }

            return numero;
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
        private int CompruebaTransaccionExistente()
        {
            try
            {
                object[] objkey = new object[] { (int)this.Session["empresa"], this.txtNumero.Text, Convert.ToString(this.ddlTipoDocumento.SelectedValue) };

                if (CentidadMetodos.EntidadGetKey("cFactura", "ppa", objkey).Tables[0].DefaultView.Count > 0)
                    return 1;
                else
                    return 0;
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
                        CerroresGeneral.ManejoError(this, GetType(), "Transacción existente. Por favor corrija", "warning");
                        return;
                    }

                }
                this.txtNumero.Enabled = false;
                this.nilbNuevo.Visible = false;
                this.txtFecha.Visible = false;
                this.txtFecha.Focus();
            }
        }

        private void ComportamientoTransaccion()
        {
            upCabeza.Visible = true;
            upDetalle.Visible = true;
            CcontrolesUsuario.ComportamientoCampoEntidad(this.upCabeza.Controls, "cFactura", Convert.ToString(this.ddlTipoDocumento.SelectedValue), (int)this.Session["empresa"]);
            CcontrolesUsuario.ComportamientoCampoEntidad(this.upDetalle.Controls, "cFacturaDetalle", Convert.ToString(this.ddlTipoDocumento.SelectedValue), (int)this.Session["empresa"]);

            if (Convert.ToBoolean(TipoTransaccionConfig(15)) == false)
                this.btnRegistrar.Visible = false;
            else
                this.btnRegistrar.Visible = true;
        }

        private void CargaProductos()
        {
            try
            {
                DataView dvProducto = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("iItems", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                if (Convert.ToBoolean(TipoTransaccionConfig(33)) == true)
                    dvProducto.RowFilter = "tipo in ('S') and   empresa=" + Convert.ToString(Session["empresa"]);
                else
                    dvProducto.RowFilter = "tipo in ('I') and   empresa=" + Convert.ToString(Session["empresa"]);

                this.ddlProducto.DataSource = dvProducto;
                this.ddlProducto.DataValueField = "codigo";
                this.ddlProducto.DataTextField = "cadena";
                this.ddlProducto.DataBind();
                this.ddlProducto.Items.Insert(0, new ListItem("", ""));
                this.ddlProducto.SelectedValue = "";
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar productos. Correspondiente a: " + ex.Message, "C");
            }
        }
        private void CargaDestinos()
        {
            try
            {
                DataView dvDestino = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("iDestino", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                dvDestino.RowFilter = "empresa = " + Session["empresa"].ToString() + " and activo=1 ";
                this.ddlDestino.DataSource = dvDestino;
                this.ddlDestino.DataValueField = "codigo";
                this.ddlDestino.DataTextField = "descripcion";
                this.ddlDestino.DataBind();
                this.ddlDestino.Items.Insert(0, new ListItem("", ""));
                this.ddlDestino.SelectedValue = "";
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar destinos. Correspondiente a: " + ex.Message, "C");
            }
        }
        private void UmedidaProducto()
        {
            try
            {
                CargarUmedida();
                this.ddlUmedida.SelectedValue = item.RetornaUmedida(Convert.ToString(this.ddlProducto.SelectedValue), Convert.ToInt16(Session["empresa"]));

                txvValorUnitario.Text = item.RetornaValorUnitario(Convert.ToString(this.ddlProducto.SelectedValue),
                             ddlTercero.SelectedValue, ddlSucursal.SelectedValue, Convert.ToDateTime(txtFecha.Text).Year, Convert.ToDateTime(txtFecha.Text).Month,
                             Convert.ToInt16(Session["empresa"])).ToString("N2");

                if (Convert.ToBoolean(TipoTransaccionConfig(21)) == true)
                    this.ddlUmedida.Enabled = false;
                else
                    this.ddlUmedida.Enabled = true;

            }
            catch (Exception ex)
            {
                ManejoError("Error al recuperar unidad de medida producto. Correspondiente a: " + ex.Message, "C");
            }
        }

        private void CargarCentroCosto()
        {
            try
            {
                DataView dvCcosto = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cCentrosCosto", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                dvCcosto.RowFilter = "empresa=" + Session["empresa"] + "and auxiliar=1";
                this.ddlCcosto.DataSource = dvCcosto;
                this.ddlCcosto.DataValueField = "codigo";
                this.ddlCcosto.DataTextField = "descripcion";
                this.ddlCcosto.DataBind();
                this.ddlCcosto.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar centro de costos. Correspondiente a: " + ex.Message, "C");
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
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(this.Session["empresa"])) != 0)
                {
                    if (!IsPostBack)
                    {
                        CargarCombos();
                        CargaCampos();
                        this.Session["transaccion"] = null;
                        this.Session["operadores"] = null;
                        cancelarTransaccion();
                    }
                }
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }
        protected void nilbNuevo_Click(object sender, EventArgs e)
        {
            this.Session["editar"] = false;
            ManejoEncabezado();
        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            cancelarTransaccion();
        }
        protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.gvLista.DataSource = null;
                this.gvLista.DataBind();
                this.Session["transaccion"] = null;
                this.niCalendarFecha.SelectedDate = Convert.ToDateTime(null);
                this.txtNumero.Text = ConsecutivoTransaccion();
                this.hdTransaccionConfig.Value = CcontrolesUsuario.TipoTransaccionConfig(this.ddlTipoDocumento.SelectedValue, Convert.ToInt16(Session["empresa"]));


                if (Convert.ToBoolean(TipoTransaccionConfig(17)) == true)
                {
                    if (tipoTransaccion.RetornavalidacionRegistro(Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"])) == 1)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "No se puede realizar este tipo movimiento el día de hoy", "warning");
                        this.niCalendarFecha.Visible = false;
                        return;
                    }

                }


                if (Convert.ToBoolean(TipoTransaccionConfig(15)) == false)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Transacción no habilitada para registro directo", "warning");
                    return;
                }

                ComportamientoConsecutivo();
                ComportamientoTransaccion();
                CargaDestinos();
                CargarCentroCosto();
                cargarBodega();
                CargarCombos();

                if (ddlTercero.Visible == true)
                {
                    txtFiltroProveedor.Visible = true;
                    txtFiltroProveedor.Enabled = true;
                    txtFiltroProveedor.ReadOnly = false;
                }

                txtFiltroProducto.Visible = ddlProducto.Visible;
                txtFiltroProducto.Enabled = ddlProducto.Visible;
                txtFiltroProducto.ReadOnly = !ddlProducto.Visible;


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
                    CerroresGeneral.ManejoError(this, GetType(), "Transacción existente. Por favor corrija", "warning");

                    return;
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
        protected void CalendarFecha_SelectionChanged(object sender, EventArgs e)
        {
            this.txtFecha.Text = this.niCalendarFecha.SelectedDate.ToShortDateString();
            validaFecha();
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
            this.Session["transaccion"] = null;
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            gvParametros.DataSource = null;
            gvParametros.DataBind();
            this.Session["operadores"] = null;
            nitxtValor1.Text = "";
            nitxtValor2.Text = "";

        }
        private bool CompruebaSaldo()
        {
            decimal saldo = 0;

            try
            {
                DataView dvSaldo = transacciones.GetSaldoTotalProducto(Convert.ToString(this.ddlProducto.SelectedValue), Convert.ToInt16(Session["empresa"]));

                foreach (DataRowView registro in dvSaldo)
                {
                    saldo = Convert.ToDecimal(registro.Row.ItemArray.GetValue(0)) - Convert.ToDecimal(registro.Row.ItemArray.GetValue(1));
                }

                if (Convert.ToBoolean(this.Session["editar"]) == true)
                    saldo = saldo + Convert.ToDecimal(this.Session["cant"]);

                if (Convert.ToDecimal(txvCantidad.Text) > saldo)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Saldo inferior a la cantidad solicitada. Por favor corrija", "warning");
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                ManejoError("Error al comprobar saldo. Correspondiente a: " + ex.Message, "C");
                return false;
            }
        }

        #endregion Eventos

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            Guardar();
        }


        protected void txtProducto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dvProducto = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("iItems", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                if (Convert.ToBoolean(TipoTransaccionConfig(33)) == true)
                    dvProducto.RowFilter = "tipo in ('I') and codigo like '%" + ddlProducto.SelectedValue.Trim() + "%' or descripcion like '%" + this.ddlProducto.SelectedValue.Trim() + "%'";
                else
                    dvProducto.RowFilter = "tipo in ('S') and codigo like '%" + ddlProducto.SelectedValue.Trim() + "%' or descripcion like '%" + this.ddlProducto.SelectedValue.Trim() + "%'";

                this.ddlProducto.DataSource = dvProducto;
                this.ddlProducto.DataBind();
                this.ddlProducto.Focus();

                UmedidaProducto();
            }
            catch
            {
                this.ddlProducto.SelectedValue = "";
                this.ddlProducto.Focus();
            }
        }
        protected void lbImprimir_Click(object sender, EventArgs e)
        {

        }
        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlProducto.Focus();
            UmedidaProducto();
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            bool anulado = false;
            if (Convert.ToBoolean(Session["editar"]) == false)
            {
                try
                {
                    List<Cfacturas> listaTransaccion = null;
                    listaTransaccion = (List<Cfacturas>)Session["transaccion"];
                    listaTransaccion.RemoveAt(e.RowIndex);
                    this.gvLista.DataSource = listaTransaccion;
                    this.gvLista.DataBind();
                }
                catch (Exception ex)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Error al eliminar el registro. Correspondiente a: " + ex.Message, "warning");
                }
            }
            else
            {
                foreach (WebControl c in this.gvLista.Rows[e.RowIndex].Cells[11].Controls)
                {
                    if (c is CheckBox)
                        anulado = ((CheckBox)c).Checked;
                }
                if (anulado == true)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Registro anulado no es posible volver anular", "warning");
                    return;
                }

                foreach (WebControl c in this.gvLista.Rows[e.RowIndex].Cells[11].Controls)
                {
                    if (c is CheckBox)
                        ((CheckBox)c).Checked = true;
                }
                this.gvLista.Rows[e.RowIndex].BackColor = System.Drawing.Color.Red;
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
                    CerroresGeneral.ManejoError(this, GetType(), "Debe de agregar el registro seleccionado para continuar", "warning");
                    return;
                }
                foreach (WebControl c in this.gvLista.SelectedRow.Cells[8].Controls)
                {
                    if (c is CheckBox)
                        anulado = ((CheckBox)c).Checked;
                }
                if (anulado == true)
                {
                    Session["editarDetalle"] = false;
                    CerroresGeneral.ManejoError(this, GetType(), "Registro anulado no es posible su edición", "warning");
                    return;
                }

                this.hdCantidad.Value = this.gvLista.SelectedRow.Cells[5].Text;
                this.hdRegistro.Value = this.gvLista.SelectedRow.Cells[9].Text;
                CargarCombos();
                CargaDestinos();
                CargaProductos();
                CargarCentroCosto();

                if (this.ddlProducto.Visible == true)
                {
                    if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                        this.ddlProducto.SelectedValue = this.gvLista.SelectedRow.Cells[2].Text;
                }
                else
                    this.ddlProducto.SelectedValue = this.gvLista.SelectedRow.Cells[2].Text;

                if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                {
                    txvCantidad.Text = this.gvLista.SelectedRow.Cells[5].Text;
                    this.Session["cant"] = Convert.ToDecimal(this.gvLista.SelectedRow.Cells[5].Text);
                }
                else
                    txvCantidad.Text = "0";

                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                {
                    txvValorUnitario.Text = this.gvLista.SelectedRow.Cells[6].Text;
                    this.Session["valor"] = Convert.ToDecimal(this.gvLista.SelectedRow.Cells[6].Text);
                }
                else
                    txvValorUnitario.Text = "0";



                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                    this.ddlUmedida.SelectedValue = Convert.ToString(this.gvLista.SelectedRow.Cells[4].Text);
                else
                    this.ddlUmedida.SelectedValue = "";

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    this.txtDetalle.Text = Server.HtmlDecode(Convert.ToString(this.gvLista.SelectedRow.Cells[3].Text));
                else
                    this.txtDetalle.Text = "";

                List<Cfacturas> listaTransaccion = null;
                listaTransaccion = (List<Cfacturas>)this.Session["transaccion"];
                listaTransaccion.RemoveAt(this.gvLista.SelectedRow.RowIndex);
                this.gvLista.DataSource = listaTransaccion;
                this.gvLista.DataBind();
            }
            catch (Exception ex)
            {
                Session["editarDetalle"] = false;
                CerroresGeneral.ManejoError(this, GetType(), "Error al cargar los campos del registro en el formulario. Correspondiente a: " + ex.Message, "warning");
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


                if (this.gvParametros.Rows.Count == 0)
                    this.imbBusqueda.Visible = false;

                EstadoInicialGrillaTransacciones();
            }
            catch
            {
            }
        }

        private void cargarImpuesto()
        {
            bool validaImpuesto = false;
            List<Cimpuestos> listaImpuesto = null;
            Session["impuestos"] = null;
            foreach (GridViewRow registro in this.gvLista.Rows)
            {
                validaImpuesto = false;
                DataView dvImpuesto = tipoTransaccion.SeleccionaImpuestoItemClinte(Convert.ToInt16(Session["empresa"]), ddlSucursal.SelectedValue, Convert.ToInt16(ddlTercero.SelectedValue), Convert.ToInt16(registro.Cells[2].Text));
                foreach (DataRowView impuesto in dvImpuesto)
                {

                    listaImpuesto = (List<Cimpuestos>)this.Session["impuestos"];
                    if (Session["impuestos"] != null)
                    {
                        foreach (Cimpuestos nt in listaImpuesto)
                        {
                            if (impuesto.Row.ItemArray.GetValue(0).ToString() == nt.Concepto)
                                validaImpuesto = true;
                        }
                    }

                    if (validaImpuesto == false)
                    {
                        transaccionImpuesto = new Cimpuestos(impuesto.Row.ItemArray.GetValue(0).ToString(),
                            impuesto.Row.ItemArray.GetValue(1).ToString(),
                            impuesto.Row.ItemArray.GetValue(2).ToString(),
                            Convert.ToDecimal(impuesto.Row.ItemArray.GetValue(3)),
                            Convert.ToDecimal(impuesto.Row.ItemArray.GetValue(4)),
                            Convert.ToDecimal(impuesto.Row.ItemArray.GetValue(5)),
                            0,
                            0,
                             Convert.ToBoolean(impuesto.Row.ItemArray.GetValue(6)),
                              Convert.ToBoolean(impuesto.Row.ItemArray.GetValue(7)),
                                 Convert.ToBoolean(impuesto.Row.ItemArray.GetValue(8))
                            );

                        if (this.Session["impuestos"] == null)
                        {
                            listaImpuesto = new List<Cimpuestos>();
                            listaImpuesto.Add(transaccionImpuesto);
                        }
                        else
                        {
                            listaImpuesto = (List<Cimpuestos>)Session["impuestos"];
                            listaImpuesto.Add(transaccionImpuesto);
                        }
                        this.Session["impuestos"] = listaImpuesto;
                    }
                }
                gvImpuesto.Visible = true;
                this.gvImpuesto.DataSource = listaImpuesto;
                this.gvImpuesto.DataBind();
            }

            gvImpuesto.Visible = true;
            this.gvImpuesto.DataSource = listaImpuesto;
            this.gvImpuesto.DataBind();

            decimal baseimpuesto = 0, totalimpuesto = 0, baseRetencion = 0, totalRetencion = 0;
            bool TipoImpuesto = false, TipoRetecion = false;
            string claseReferencia = null;
            foreach (GridViewRow gvr in gvImpuesto.Rows)
            {
                foreach (GridViewRow gvrl in this.gvLista.Rows)
                {
                    DataView dvImpuesto = tipoTransaccion.SeleccionaImpuestoItemClinte(Convert.ToInt16(Session["empresa"]), ddlSucursal.SelectedValue, Convert.ToInt16(ddlTercero.SelectedValue), Convert.ToInt16(gvrl.Cells[2].Text));
                    foreach (DataRowView drv in dvImpuesto)
                    {
                        if (drv.Row.ItemArray.GetValue(0).ToString() == gvr.Cells[0].Text)
                        {
                            TipoRetecion = Convert.ToBoolean(drv.Row.ItemArray.GetValue(7));
                            TipoImpuesto = Convert.ToBoolean(drv.Row.ItemArray.GetValue(6));
                            claseReferencia = drv.Row.ItemArray.GetValue(8) != null ? drv.Row.ItemArray.GetValue(8).ToString() : "";
                            break;
                        }
                    }

                    if (TipoImpuesto)
                    {
                        baseimpuesto += Convert.ToDecimal(gvrl.Cells[7].Text);
                        TipoImpuesto = false;
                    }

                    if (TipoRetecion)
                    {
                        baseRetencion += Convert.ToDecimal(gvrl.Cells[7].Text);
                        TipoRetecion = false;
                    }
                }

                if (Convert.ToDecimal(gvr.Cells[4].Text) <= baseimpuesto)
                {
                    gvr.Cells[5].Text = (Convert.ToDecimal(gvr.Cells[2].Text) / 100 * Convert.ToDecimal(gvr.Cells[3].Text) / 100 * baseimpuesto).ToString("N2");
                    totalimpuesto += Convert.ToDecimal(gvr.Cells[5].Text);
                }

                if (Convert.ToDecimal(gvr.Cells[4].Text) <= baseRetencion)
                {
                    gvr.Cells[5].Text = (Convert.ToDecimal(gvr.Cells[2].Text) / 100 * Convert.ToDecimal(gvr.Cells[3].Text) / 100 * baseimpuesto).ToString("N2");
                    totalRetencion += Convert.ToDecimal(gvr.Cells[5].Text);
                }

            }
            nitxtTotalImpuesto.Text = totalimpuesto.ToString("N2");
            nitxtTotalRetencion.Text = totalimpuesto.ToString("N2");

        }

        private void TotalizaGrillaReferencia()
        {
            try
            {
                nitxtTotalRetencion.Visible = true;
                nitxtTotal.Visible = true;
                nitxtTotalImpuesto.Visible = true;
                nitxtTotalValorBruto.Visible = true;
                nilblValorTotal.Visible = true;
                nilblValorTotal0.Visible = true;
                nilblValorTotal1.Visible = true;
                nilblValorTotal2.Visible = true;

                this.nitxtTotalValorBruto.Text = "0";
                nitxtTotalRetencion.Text = "0";
                nitxtTotal.Text = "0";

                foreach (GridViewRow registro in this.gvLista.Rows)
                {
                    this.nitxtTotalValorBruto.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(registro.Cells[7].Text) + Convert.ToDecimal(this.nitxtTotalValorBruto.Text)).ToString("N2"));
                }
                nitxtTotalImpuesto.Text = "0";
                decimal valorBruto = 0;
                valorBruto = Convert.ToDecimal(nitxtTotalValorBruto.Text) - Convert.ToDecimal(nitxtTotalRetencion.Text);
                foreach (GridViewRow registro in this.gvImpuesto.Rows)
                {
                    var impuesto = registro.FindControl("hfImpuesto") as HiddenField;
                    var retencion = registro.FindControl("hfRetencion") as HiddenField;


                    registro.Cells[5].Text = Convert.ToString(Convert.ToDecimal((valorBruto * Convert.ToDecimal(registro.Cells[3].Text) / 100) * (Convert.ToDecimal(registro.Cells[2].Text) / 100)).ToString("N2"));
                    if (Convert.ToBoolean(impuesto.Value))
                        this.nitxtTotalImpuesto.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(registro.Cells[5].Text) + Convert.ToDecimal(this.nitxtTotalImpuesto.Text)).ToString("N2"));
                    if (Convert.ToBoolean(retencion.Value))
                        this.nitxtTotalRetencion.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(registro.Cells[5].Text) + Convert.ToDecimal(this.nitxtTotalRetencion.Text)).ToString("N2"));
                }

                this.nitxtTotal.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(nitxtTotalImpuesto.Text) + Convert.ToDecimal(nitxtTotalValorBruto.Text) - Convert.ToDecimal(this.nitxtTotalRetencion.Text)).ToString("N2"));
            }
            catch (Exception ex)
            {
                ManejoError("Error al totalizar la grilla de referencia. Correspondiente a: " + ex.Message, "C");
            }
        }

        protected void gvTransaccion_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            decimal cantidad = 0;


            this.Session["editar"] = true;
            this.Session["transaccion"] = null;
            bool anulado = false, aprobado = false, cerrado = false;

            foreach (WebControl objControl in gvTransaccion.Rows[e.RowIndex].Cells[8].Controls)
            {
                anulado = ((CheckBox)objControl).Checked;
            }

            if (anulado == true)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Registro anulado no es posible su edición", "warning");
                string vtn = "window.open('DetalleFactura.aspx?numero=" + gvTransaccion.Rows[e.RowIndex].Cells[2].Text.Trim() + "&tipo=" + gvTransaccion.Rows[e.RowIndex].Cells[1].Text.Trim() + "','Detalle de transacción','scrollbars=yes,resizable=yes','height=200', 'width=300')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
                return;
            }


            foreach (WebControl objControl in gvTransaccion.Rows[e.RowIndex].Cells[9].Controls)
            {
                aprobado = ((CheckBox)objControl).Checked;
            }

            if (aprobado == true)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Registro aprobado no es posible su edición", "warning");
                string vtn = "window.open('DetalleFactura.aspx?numero=" + gvTransaccion.Rows[e.RowIndex].Cells[2].Text.Trim() + "&tipo=" + gvTransaccion.Rows[e.RowIndex].Cells[1].Text.Trim() + "','Detalle de transacción','scrollbars=yes,resizable=yes','height=200', 'width=300')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
                return;
            }

            foreach (WebControl objControl in gvTransaccion.Rows[e.RowIndex].Cells[10].Controls)
            {
                cerrado = ((CheckBox)objControl).Checked;
            }

            if (cerrado == true)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Registro cerrado no es posible su edición", "warning");
                string vtn = "window.open('DetalleFactura.aspx?numero=" + gvTransaccion.Rows[e.RowIndex].Cells[2].Text.Trim() + "&tipo=" + gvTransaccion.Rows[e.RowIndex].Cells[1].Text.Trim() + "','Detalle de transacción','scrollbars=yes,resizable=yes','height=200', 'width=300')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
                return;
            }

            try
            {
                if (periodo.RetornaPeriodoCerrado(Convert.ToInt16(gvTransaccion.Rows[e.RowIndex].Cells[3].Text), Convert.ToInt16(gvTransaccion.Rows[e.RowIndex].Cells[4].Text), Convert.ToInt16(Session["empresa"])) == 1)
                {
                    ManejoError("Periodo cerrado contable. No es posible editar transacciones", "I");
                    string vtn = "window.open('DetalleFactura.aspx?numero=" + gvTransaccion.Rows[e.RowIndex].Cells[2].Text.Trim() + "&tipo=" + gvTransaccion.Rows[e.RowIndex].Cells[1].Text.Trim() + "','Detalle de transacción','scrollbars=yes,resizable=yes','height=200', 'width=300')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
                    return;
                }

                string vtnn = "window.open('DetalleFactura.aspx?numero=" + gvTransaccion.Rows[e.RowIndex].Cells[2].Text.Trim() + "&tipo=" + gvTransaccion.Rows[e.RowIndex].Cells[1].Text.Trim() + "','Detalle de transacción','scrollbars=yes,resizable=yes','height=200', 'width=300')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtnn, true);

            }
            catch (Exception ex)
            {
                ManejoError("Error al verificar periodo. Correspondiente a: " + ex.Message, "C");
            }

            //try
            //{
            //    this.hdTransaccionConfig.Value = CcontrolesUsuario.TipoTransaccionConfig(this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text, Convert.ToInt16(Session["empresa"]));

            //    if (transacciones.VerificaEdicionBorrado(this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, Convert.ToInt16(Session["empresa"])) != 0)
            //    {
            //        CerroresGeneral.ManejoError(this, GetType(), "Transacción ejecutada no es posible su edición", "warning");
            //        return;
            //    }

            //    CargarTipoTransaccion();

            //    CargarCombos();
            //    upRegistro.Visible = true;
            //    CcontrolesUsuario.HabilitarControles(this.upRegistro.Controls);
            //    this.ddlTipoDocumento.SelectedValue = this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text;
            //    this.ddlTipoDocumento.Enabled = false;
            //    this.txtNumero.Text = this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text;
            //    this.txtNumero.Enabled = false;
            //    this.nilbNuevo.Visible = false;
            //    this.hdTransaccionConfig.Value = CcontrolesUsuario.TipoTransaccionConfig(this.ddlTipoDocumento.SelectedValue, Convert.ToInt16(Session["empresa"]));
            //    CargaDestinos();
            //    CargaProductos();

            //    CcontrolesUsuario.LimpiarControles(this.upCabeza.Controls);
            //    CcontrolesUsuario.ComportamientoCampoEntidad(this.upCabeza.Controls, "cFactura", Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"]));
            //    CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
            //    CcontrolesUsuario.ComportamientoCampoEntidad(this.upDetalle.Controls, "cFacturaDetalle", Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"]));

            //    object[] objCab = new object[] { Convert.ToInt16(Session["empresa"]), this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text };
            //    foreach (DataRowView encabezado in CentidadMetodos.EntidadGetKey("cFactura", "ppa", objCab).Tables[0].DefaultView)
            //    {
            //        this.niCalendarFecha.SelectedDate = Convert.ToDateTime(encabezado.Row.ItemArray.GetValue(8));
            //        this.niCalendarFecha.Visible = false;
            //        this.txtFecha.Visible = true;
            //        this.txtFecha.Text = Convert.ToString(encabezado.Row.ItemArray.GetValue(8));
            //        ddlCondicionPago.SelectedValue = Convert.ToString(encabezado.Row.ItemArray.GetValue(11));
            //        this.txtObservacion.Text = Convert.ToString(encabezado.Row.ItemArray.GetValue(13));
            //        this.ddlTercero.SelectedValue = Convert.ToString(encabezado.Row.ItemArray.GetValue(9));
            //        cargarSucurlsal(ddlTercero.SelectedValue.ToString());
            //        if (this.ddlSucursal.Visible == true)
            //            this.ddlSucursal.SelectedValue = Convert.ToString(encabezado.Row.ItemArray.GetValue(10));
            //    }

            //    foreach (DataRowView detalle in CentidadMetodos.EntidadGetKey("cFacturaDetalle", "ppa", objCab).Tables[0].DefaultView)
            //    {
            //        if (Convert.ToBoolean(TipoTransaccionConfig(15)) == true)
            //            cantidad = Convert.ToDecimal(detalle.Row.ItemArray.GetValue(8));
            //        else
            //            cantidad = Convert.ToDecimal(detalle.Row.ItemArray.GetValue(9));

            //        transaccionAlmacen = new Cfacturas
            //        {
            //            Producto = Convert.ToString(detalle.Row.ItemArray.GetValue(15)),
            //            Cantidad = cantidad,
            //            Umedida = Convert.ToString(detalle.Row.ItemArray.GetValue(13)),
            //            Registro = Convert.ToInt32(detalle.Row.ItemArray.GetValue(6)),
            //            Detalle = Convert.ToString(detalle.Row.ItemArray.GetValue(18)),
            //            ValorTotal = Convert.ToDecimal(detalle.Row.ItemArray.GetValue(13)),
            //            ValorUnitario = Convert.ToDecimal(detalle.Row.ItemArray.GetValue(10))
            //        };

            //        List<Cfacturas> listaTransaccion = null;

            //        if (this.Session["transaccion"] == null)
            //        {
            //            listaTransaccion = new List<Cfacturas>();
            //            listaTransaccion.Add(transaccionAlmacen);
            //        }
            //        else
            //        {
            //            listaTransaccion = (List<Cfacturas>)Session["transaccion"];
            //            listaTransaccion.Add(transaccionAlmacen);
            //        }

            //        this.Session["transaccion"] = listaTransaccion;
            //        this.gvLista.DataSource = listaTransaccion;
            //        this.gvLista.DataBind();
            //        this.btnRegistrar.Visible = true;
            //        this.niimbConsulta.Enabled = false;
            //        this.niimbRegistro.Enabled = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ManejoError("Error al cargar la transacción. Correspondiente a: " + ex.Message, "A");
            //}

            //TabRegistro();

            //if (Convert.ToBoolean(TipoTransaccionConfig(15)) == false)
            //    this.ddlProducto.Enabled = false;
        }
        protected void gvTransaccion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    if (transacciones.VerificaEdicionBorrado(this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, Convert.ToInt16(Session["empresa"])) != 0)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "Transacción ejecutada no es posible su edición", "warning");
                        return;
                    }

                    if (tipoTransaccion.RetornaTipoBorrado(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, Convert.ToInt16(Session["empresa"])) == "E")
                    {
                        object[] objValores = new object[] { Convert.ToInt16(Session["empresa"]), Convert.ToString(this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text).Trim(), Convert.ToString(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text).Trim() };

                        switch (CentidadMetodos.EntidadInsertUpdateDelete("cFacturaDetalle", "elimina", "ppa", objValores))
                        {
                            case 0:
                                switch (CentidadMetodos.EntidadInsertUpdateDelete("cFactura", "elimina", "ppa", objValores))
                                {
                                    case 0:
                                        CerroresGeneral.ManejoError(this, GetType(), "Registro Eliminado Satisfactoriamente", "i");
                                        BusquedaTransaccion();
                                        ts.Complete();
                                        break;
                                    case 1:
                                        CerroresGeneral.ManejoError(this, GetType(), "Error al eliminar el registro. Operación no realizada", "e");
                                        break;
                                }
                                break;
                            case 1:
                                CerroresGeneral.ManejoError(this, GetType(), "Error al eliminar el registro. Operación no realizada", "e");
                                break;
                        }
                    }
                    else
                    {
                        switch (transacciones.AnulaFacturaContable(this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text,
                            this.Session["usuario"].ToString().Trim(), Convert.ToInt16(Session["empresa"])))
                        {
                            case 0:
                                CerroresGeneral.ManejoError(this, GetType(), "Registro Anulado Satisfactoriamente", "i");
                                BusquedaTransaccion();
                                ts.Complete();
                                break;
                            case 1:
                                CerroresGeneral.ManejoError(this, GetType(), "Error al anular la transacción. Operación no realizada", "e");
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + limpiarMensaje(ex.Message), "E");
                }
            }
        }
        protected void niimbAdicionar_Click(object sender, EventArgs e)
        {
            if (this.nitxtValor1.Text.Length == 0 && Convert.ToString(this.niddlCampo.SelectedValue).Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar un campo y un valor antes de filtrar, por favor corrija.", "warning");
                return;
            }

            foreach (GridViewRow registro in this.gvParametros.Rows)
            {
                if (Convert.ToString(this.niddlCampo.SelectedValue) == registro.Cells[1].Text && Convert.ToString(this.niddlOperador.SelectedValue) == Server.HtmlDecode(registro.Cells[2].Text) && this.nitxtValor1.Text == registro.Cells[3].Text)
                    return;
            }
            operadores = new Coperadores(Convert.ToString(this.niddlCampo.SelectedValue), Server.HtmlDecode(Convert.ToString(this.niddlOperador.SelectedValue)), this.nitxtValor1.Text, this.nitxtValor2.Text);
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

            EstadoInicialGrillaTransacciones();
            imbBusqueda.Focus();
        }
        protected void imbBusqueda_Click(object sender, EventArgs e)
        {
            BusquedaTransaccion();
        }

        protected void txtFecha_TextChanged(object sender, EventArgs e)
        {
            validaFecha();
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(TipoTransaccionConfig(4)) == true)
                {
                    if (CompruebaSaldo() == false)
                        return;
                }

                foreach (GridViewRow registro in this.gvLista.Rows)
                {
                    if (Convert.ToString(this.ddlProducto.SelectedValue) == registro.Cells[3].Text.Trim())
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "El producto seleccionado ya se encuentra registrado. Por favor corrija", "warning");
                        return;
                    }
                }

                if (Convert.ToString(this.ddlTipoDocumento.SelectedValue).Trim().Length == 0 ||
                    this.txtNumero.Text.Trim().Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe ingresar tipo y número de transacción", "warning");
                    return;
                }

                if (CcontrolesUsuario.VerificaCamposRequeridos(this.upDetalle.Controls) == false)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Campos vacios. Por favor corrija", "warning");
                    return;
                }

                if (Convert.ToDecimal(txvCantidad.Text) <= 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "La cantidad no puede ser igual o menor que cero. Por favor corrija", "warning");
                    return;
                }

                if (Convert.ToBoolean(TipoTransaccionConfig(15)) == false)
                {
                    if (Convert.ToDecimal(txvCantidad.Text) > Convert.ToDecimal(this.hdCantidad.Value))
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "La cantidad no puede ser mayor a la registrada inicialmente. Por favor Corrija", "warning");
                        txvCantidad.Text = this.hdCantidad.Value;
                    }
                }

                if (ddlTercero.SelectedValue.Length == 0 || ddlSucursal.SelectedValue.Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar proveedor y sucursal, antes de cargar datos", "warning");
                    return;
                }

                Session["editarDetalle"] = false;

                transaccionAlmacen = new Cfacturas
                {
                    Producto = Convert.ToString(this.ddlProducto.SelectedValue),
                    Cantidad = Convert.ToDecimal(txvCantidad.Text),
                    Umedida = Convert.ToString(this.ddlUmedida.SelectedValue),
                    Registro = Convert.ToInt16(this.hdRegistro.Value),
                    Detalle = Convert.ToString(this.ddlProducto.SelectedItem.ToString()),
                    ValorTotal = Convert.ToDecimal(txvValorUnitario.Text) * Convert.ToDecimal(txvCantidad.Text),
                    ValorUnitario = Convert.ToDecimal(txvValorUnitario.Text)
                };

                List<Cfacturas> listaTransaccion = null;

                if (this.Session["transaccion"] == null)
                {
                    listaTransaccion = new List<Cfacturas>();
                    listaTransaccion.Add(transaccionAlmacen);
                }
                else
                {
                    listaTransaccion = (List<Cfacturas>)Session["transaccion"];
                    listaTransaccion.Add(transaccionAlmacen);
                }

                int registror = listaTransaccion.Max(r => r.Registro) + 1;
                hdRegistro.Value = registror.ToString();
                listaTransaccion = listaTransaccion.OrderBy(r => r.Registro).ToList();


                this.Session["transaccion"] = listaTransaccion;
                this.gvLista.DataSource = listaTransaccion;
                this.gvLista.DataBind();
                cargarImpuesto();
                this.ddlProducto.Focus();
                CargaProductos();
                CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
                CcontrolesUsuario.LimpiarCombos(this.upDetalle.Controls);
                TotalizaGrillaReferencia();

                cargarCxC();
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Error al insertar el registro. Correspondiente a: " + ex.Message, "warning");
            }
        }
        private void cargarCxC()
        {
            string producto = null, tercero = null, suscursal = null;
            decimal debito = 0, credito = 0;

            tercero = ddlTercero.SelectedValue;
            suscursal = ddlSucursal.SelectedValue;
            bool validaCuenta = false;
            try
            {
                List<CcxcFactura> listaCxC = null;
                Session["CxC"] = null;
                foreach (GridViewRow cuerpo in this.gvLista.Rows)
                {
                    producto = Server.HtmlDecode(cuerpo.Cells[2].Text);
                    debito = 0;
                    credito = 0;
                    DataView dvCxC = tipoTransaccion.SeleccionaCxCItemTercero(Convert.ToInt32(Session["empresa"]), suscursal, tercero, producto, Convert.ToDecimal(cuerpo.Cells[7].Text));

                    foreach (DataRowView CxC in dvCxC)
                    {
                        listaCxC = (List<CcxcFactura>)this.Session["CxC"];
                        if (Session["CxC"] != null)
                        {
                            foreach (CcxcFactura nt in listaCxC)
                            {
                                if (CxC.Row.ItemArray.GetValue(0).ToString() == nt.Cuenta)
                                {
                                    validaCuenta = true;
                                    nt.Debito += Convert.ToDecimal(CxC.Row.ItemArray.GetValue(2));
                                    nt.Credito += Convert.ToDecimal(CxC.Row.ItemArray.GetValue(3));
                                }
                            }
                        }

                        if (validaCuenta == false)
                        {
                            transaccionCxC = new CcxcFactura(CxC.Row.ItemArray.GetValue(0).ToString(), Convert.ToDecimal(CxC.Row.ItemArray.GetValue(2)), Convert.ToDecimal(CxC.Row.ItemArray.GetValue(3)));

                            if (this.Session["CxC"] == null)
                            {
                                listaCxC = new List<CcxcFactura>();
                                listaCxC.Add(transaccionCxC);
                            }
                            else
                            {
                                listaCxC = (List<CcxcFactura>)Session["CxC"];
                                listaCxC.Add(transaccionCxC);
                            }
                            this.Session["CxC"] = listaCxC;
                        }
                    }
                    validaCuenta = false;

                }

                gvCxC.Visible = true;
                this.gvCxC.DataSource = listaCxC;
                this.gvCxC.DataBind();

                totalizarFooter();
                this.btnRefrezcar.Visible = true;
                this.btnRefrezcar.Enabled = true;
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }




        private void totalizarFooter()
        {
            decimal debito = 0, credito = 0;
            foreach (GridViewRow cuerpo in this.gvCxC.Rows)
            {
                debito += Convert.ToDecimal(cuerpo.Cells[1].Text);
                credito += Convert.ToDecimal(cuerpo.Cells[2].Text);

            }
            gvCxC.Columns[1].FooterText = debito.ToString("N2");
            gvCxC.Columns[2].FooterText = credito.ToString("N2");
        }

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

        private void cargarSucurlsal(string tercero)
        {
            try
            {
                DataView dvSucursal = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cxcCliente", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                dvSucursal.RowFilter = "empresa = " + Session["empresa"].ToString() + " and idTercero=" + tercero;
                this.ddlSucursal.DataSource = dvSucursal;
                this.ddlSucursal.DataValueField = "codigo";
                this.ddlSucursal.DataTextField = "descripcion";
                this.ddlSucursal.DataBind();
                this.ddlSucursal.Items.Insert(0, new ListItem("", ""));
                this.ddlSucursal.SelectedValue = "";
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar sucursal del tercero. Correspondiente a: " + ex.Message, "C");
            }
        }




        protected void ddlTercero_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarSucurlsal(ddlTercero.SelectedValue);
            ddlSucursal.Focus();
        }


        private void ImprimrcFactura(string empresa, string tipo, string numero)
        {
            string vtn = "window.open('../Pinformes/ImprimeTransaccion.aspx?empresa=" + empresa + "&tipo=" + tipo + "&numero=" + numero + "','Impresión de Formatos','scrollbars=yes,resizable=yes','height=300', 'width=400')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
        }

        protected void gvTransaccion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "imprimir")
                {
                    GridViewRow row = (GridViewRow)(((WebControl)e.CommandSource).NamingContainer);
                    int RowIndex = row.RowIndex;
                    ImprimrcFactura(this.Session["empresa"].ToString(), gvTransaccion.Rows[RowIndex].Cells[1].Text.Trim(), gvTransaccion.Rows[RowIndex].Cells[2].Text.Trim());
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al imprimir transacción debido a:" + ex.Message, "I");
            }
        }

        protected void txtFiltroProveedor_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtFiltroProveedor.Text.Trim().Length == 0)
                {
                    ManejoError("Ingrese un filtro válido", "I");
                    txtFiltroProveedor.Focus();
                    return;
                }
                DataView dvproveedores = tercero.SeleccionaClienteFiltro(Convert.ToInt32(this.Session["empresa"]), txtFiltroProveedor.Text);
                if (dvproveedores.Count == 0)
                {
                    ManejoError("No se ha encontrado ningun cliente por favor vuelva a intentarlo", "I");
                    txtFiltroProveedor.Focus();
                    return;
                }
                ddlTercero.DataSource = dvproveedores;
                ddlTercero.DataValueField = "id";
                ddlTercero.DataTextField = "cadena";
                ddlTercero.DataBind();
                ddlTercero.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar proveedores debido a:  " + ex.Message, "I");
            }
        }

        protected void txtFiltroProducto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtFiltroProducto.Text.Trim().Length == 0)
                {
                    ManejoError("Ingrese un filtro válido", "I");
                    txtFiltroProducto.Focus();
                    return;
                }
                DataView dvitems = item.RetornaItemsFiltro(Convert.ToInt32(this.Session["empresa"]), txtFiltroProducto.Text);
                if (dvitems.Count == 0)
                {
                    ManejoError("No se ha encontrado ningun proveedor por favor vuelva a intentarlo", "I");
                    txtFiltroProducto.Focus();
                    return;
                }
                ddlProducto.DataSource = dvitems;
                ddlProducto.DataValueField = "codigo";
                ddlProducto.DataTextField = "cadena";
                ddlProducto.DataBind();
                ddlProducto.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar proveedores debido a:  " + ex.Message, "I");
            }
        }

        decimal debito = 0, credito = 0;

        protected void gvCxC_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                debito = debito + Convert.ToDecimal(e.Row.Cells[1].Text);
                credito = credito + Convert.ToDecimal(e.Row.Cells[2].Text);

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = debito.ToString("N2");
                e.Row.Cells[2].Text = credito.ToString("N2");
            }
        }

        protected void btnRefrezcar_Click(object sender, EventArgs e)
        {
            cargarCxC();
        }
    }
}