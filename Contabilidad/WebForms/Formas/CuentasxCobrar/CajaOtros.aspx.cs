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
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.CuentasxCobrar
{
    public partial class CajaOtros : BasePage
    {
        #region Instancias

        CentidadMetodos CentidadMetodos = new CentidadMetodos();
        Security seguridad = new Security();
        CIP ip = new CIP();
        Cterceros tercero = new Cterceros();
        Ctransacciones transacciones = new Ctransacciones();
        CtipoTransaccion tipoTransaccion = new CtipoTransaccion();
        Coperadores operadores = new Coperadores();
        Cperiodos periodo = new Cperiodos();
        Citems item = new Citems();
        Cdestinos destino = new Cdestinos();
        CmediosPago medioPago = new CmediosPago();
        Cimpuestos transaccionImpuesto = new Cimpuestos();
        CcajaOtros transaccionCaja = new CcajaOtros();
        Cpuc puc = new Cpuc();
        decimal valorTotal = 0;

        #endregion Instancias

        #region Metodos
        private void ValidaMedioPago()
        {
            try
            {
                lblValorDetalle.Visible = true;
                txvValorDetalle.Visible = true;
                lblDetalle.Visible = true;
                txtDetalle.Visible = true;
                switch (medioPago.TipoMedioPago(ddlMediosPago.SelectedValue, Convert.ToInt32(Session["empresa"])))
                {
                    case "":
                        ddlCuentaBancaria.Visible = false;
                        lblCuentaBancaria.Visible = false;
                        ddlCaja.Visible = false;
                        lblCaja.Visible = false;
                        lblCheque.Visible = false;
                        txtNoCheque.Visible = false;
                        txtReferencia.Visible = false;
                        lblReferencia.Visible = false;
                        lblBanco.Visible = false;
                        ddlBanco.Visible = false;
                        break;
                    case "EF":
                        ddlCuentaBancaria.Visible = false;
                        lblCuentaBancaria.Visible = false;
                        ddlCaja.Visible = true;
                        lblCaja.Visible = true;
                        lblCheque.Visible = false;
                        txtNoCheque.Visible = false;
                        txtReferencia.Visible = false;
                        lblReferencia.Visible = false;
                        lblBanco.Visible = false;
                        ddlBanco.Visible = false;
                        break;
                    case "CH":
                        ddlCuentaBancaria.Visible = true;
                        lblCuentaBancaria.Visible = true;
                        ddlCaja.Visible = false;
                        lblCaja.Visible = false;
                        lblCheque.Visible = true;
                        txtNoCheque.Visible = true;
                        txtReferencia.Visible = false;
                        lblReferencia.Visible = false;
                        lblBanco.Visible = true;
                        ddlBanco.Visible = true;
                        break;
                    default:
                        ddlCuentaBancaria.Visible = true;
                        lblCuentaBancaria.Visible = true;
                        ddlCaja.Visible = false;
                        lblCaja.Visible = false;
                        lblCheque.Visible = false;
                        txtNoCheque.Visible = false;
                        txtReferencia.Visible = true;
                        lblReferencia.Visible = true;
                        lblBanco.Visible = false;
                        ddlBanco.Visible = false;
                        break;

                }
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
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
                    this.gvTransaccion.DataSource = transacciones.GetTransaccionCajaOtros(where, Convert.ToInt16(Session["empresa"]));
                    this.gvTransaccion.DataBind();
                    this.nilblRegistros.Text = "Nro. Registros " + Convert.ToString(this.gvTransaccion.Rows.Count);
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al procesar la consulta de transacciones. Correspondiente a: " + ex.Message, "C");
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
                this.upCabeza.Visible = true;

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
            this.Session["cajaCliente"] = null;
            this.niCalendarFecha.Visible = false;
            this.niCalendarFecha.SelectedDate = Convert.ToDateTime(null);
            this.lbRegistrar.Visible = false;
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            lbFecha.Visible = false;
            txtFecha.Visible = false;
            txtFecha.Text = "";
            this.lbCancelar.Visible = false;
            upCabeza.Visible = false;
            upConsulta.Visible = false;
            upDetalle.Visible = false;
        }
        private void Editar()
        {
            bool verificacion = false;


            if (verificacion == false)
            {
                CerroresGeneral.ManejoError(this, GetType(), "La transacción debe contener por lo menos un registro válido para editar", "warning");
                return;
            }

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    DateTime Calendario = Convert.ToDateTime(this.txtFecha.Text);
                    //switch (transacciones.EditaEncabezado(Calendario.Year.ToString() + Calendario.Month.ToString().PadLeft(2, '0'), this.ddlTipoDocumento.SelectedValue.Trim(), this.txtNumero.Text.Trim(), this.niCalendarFecha.SelectedDate, Server.HtmlDecode(this.txtObservacion.Text.Trim())))
                    //{
                    //    case 0:

                    //        foreach (GridViewRow registro in this.gvLista.Rows)
                    //        {
                    //            switch (transacciones.EditaDetalle(
                    //                this.niCalendarFecha.SelectedDate.Year.ToString() + this.niCalendarFecha.SelectedDate.Month.ToString().PadLeft(2, '0'),
                    //                this.ddlTipoDocumento.SelectedValue.Trim(),
                    //                this.txtNumero.Text.Trim(),
                    //                Convert.ToInt16(registro.Cells[18].Text),
                    //                Convert.ToDecimal(registro.Cells[5].Text),
                    //                ((CheckBox)registro.FindControl("chkAnulado")).Checked))
                    //            {
                    //                case 1:

                    //                    verificacion = false;
                    //                    break;
                    //            }
                    //        }

                    //        if (verificacion == false)
                    //        {
                    //            CerroresGeneral.ManejoError(this,GetType(), "Error al editar el registro. Operación no realizada";
                    //        }
                    //        else
                    //        {
                    //            ManejoExito("Transacción editada satisfactoriamente. Transacción número " + this.txtNumero.Text.Trim(), "A");
                    //            ts.Complete();
                    //        }
                    //        break;

                    //    case 1:

                    //        CerroresGeneral.ManejoError(this,GetType(), "Error al editar el encabezado. Operación no realizada";
                    //        break;
                    //}
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al editar la transacción. Correspondiente a: " + ex.Message, "A");
            }
        }
        private void GuardaReferencia()
        {

            bool verificacion = false;
            string numero = "", tercero = "", periodo = Convert.ToString(this.niCalendarFecha.SelectedDate.Year) +
                Convert.ToString(this.niCalendarFecha.SelectedDate.Month).PadLeft(2, '0').Trim(), terceroSucursal = "";
            bool verificacionCK = false;

            try
            {
                if (this.gvLista.Rows.Count == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar un registro para guardar la transacción", "warning");
                    return;
                }

                if (Convert.ToString(this.ddlTipoDocumento.SelectedValue).Trim().Length == 0 || Server.HtmlDecode(this.txtNumero.Text.Trim()).Length == 0 ||
                    Server.HtmlDecode(this.txtFecha.Text.Trim()).Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Campos vacios en el encabezado. Por favor corrija", "warning");
                    return;
                }


                if (Convert.ToString(this.ddlMediosPago.SelectedValue).Trim().Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Campos vacios en medios de pago. Por favor corrija", "warning");
                    return;
                }

                if (Convert.ToDecimal(nitxtTotalDiferencia.Text) > 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "E documento no esta terminado, el total del detalle debe ser igual al valor recaudado. Por favor corrija", "warning");
                    return;
                }

                if (this.ddlTercero.Enabled == true)
                {
                    if (Convert.ToString(this.ddlTercero.SelectedValue).Trim().Length == 0)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar un tercero para guardar la transacción", "warning");
                        return;
                    }
                }


                using (TransactionScope ts = new TransactionScope())
                {


                    if (this.ddlTercero.Enabled == false)
                        tercero = null;
                    else
                        tercero = Convert.ToString(this.ddlTercero.SelectedValue);

                    TotalizaGrillaReferencia();


                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                    {
                        this.txtNumero.Enabled = false;
                        numero = this.txtNumero.Text.Trim();
                        object[] objKey = new object[] { (int)this.Session["empresa"], numero, ddlTipoDocumento.SelectedValue };
                        CentidadMetodos.EntidadInsertUpdateDelete("cTesoreriaDetalle", "elimina", "ppa", objKey);
                        CentidadMetodos.EntidadInsertUpdateDelete("cTesoreria", "elimina", "ppa", objKey);
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
                                    null,//@@clase
                                    false,//@ejecutado
                                    Convert.ToInt16(Session["empresa"]),//@empresa
                                    Convert.ToDateTime(txtFecha.Text),//@fecha
                                    null,//@fechaAnulado
                                    null,//@fechaAprobado
                                    null,//@fechaCierre
                                    DateTime.Now,//@fechaRegistro
                                    Convert.ToDateTime(txtFecha.Text).Month,//@mes
                                    false,
                                    numero,
                                    Server.HtmlDecode(this.txtObservacion.Text.Trim()),//@notas
                                    "",
                                    tercero,//@tercero
                                    ddlTipoDocumento.SelectedValue,//@tipo
                                    this.Session["usuario"].ToString(),//@usuario
                                    null,//@usuarioAbrobado
                                    null,//@usuarioAnulado
                                    null,
                                    Convert.ToDecimal(nitxtTotalPagado.Text),
                                    Convert.ToDecimal(nitxtTotalDiferencia.Text)
                                };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("cTesoreria", "inserta", "ppa", objValores))
                    {
                        case 0:

                            string banco = ddlBanco.Visible == true ? ddlBanco.SelectedValue : null;
                            string cheque = txtNoCheque.Visible == true ? txtNoCheque.Text : null;
                            string referencia = txtReferencia.Visible == true ? txtReferencia.Text : null;
                            string numeroCuenta = ddlCuentaBancaria.Visible == true ? ddlCuentaBancaria.SelectedValue : null;
                            string caja = ddlCaja.Visible == true ? ddlCaja.SelectedValue : null;
                            int i = 0;

                            foreach (GridViewRow registro in this.gvLista.Rows)
                            {
                                object[] objValoresCuerpoCredito = new object[]{
                                                      Convert.ToDateTime(txtFecha.Text).Year,//@año
                                                      banco,//@banco
                                                      caja,//@caja
                                                      null,//@ccosto
                                                      cheque,//@cheque
                                                      Convert.ToDecimal(registro.Cells[4].Text),//@credito
                                                      registro.Cells[2].Text,//@cuenta
                                                      0,//@debito
                                                      Convert.ToInt16(Session["empresa"]),//@empresa
                                                      ddlMediosPago.SelectedValue,//@medioPago
                                                      Convert.ToDateTime(txtFecha.Text).Month,//@mes
                                                      registro.Cells[5].Text,//@nota
                                                      numero,//@numero
                                                      numeroCuenta,//@numeroCuenta
                                                      null,//@numeroReferencia
                                                      referencia,//@referencia
                                                      i,//@registro
                                                      ddlSucursal.SelectedValue,//@sucursal
                                                      tercero,
                                                      ddlTipoDocumento.SelectedValue,//@tipo
                                                      null,//@tipoReferencia
                                                      Convert.ToDecimal(registro.Cells[4].Text),//@valorPagado
                                                      0,//@valorTotal
                                                      Convert.ToDecimal(registro.Cells[4].Text)//@valorTotal
                                                };

                                switch (CentidadMetodos.EntidadInsertUpdateDelete("cTesoreriadetalle", "inserta", "ppa", objValoresCuerpoCredito))
                                {
                                    case 0:
                                        string cuenta = numeroCuenta == null ? medioPago.RetornaAuxiliarCaja(caja, Convert.ToInt16(Session["empresa"])) :
                                            medioPago.RetornaAuxiliarCuentaBanco(numeroCuenta, Convert.ToInt16(Session["empresa"]));
                                        i++;
                                        object[] objValoresCuerpoDebito = new object[]{
                                                       Convert.ToDateTime(txtFecha.Text).Year,//@año
                                                      banco,//@banco
                                                      caja,//@caja
                                                      null,//@ccosto
                                                      cheque,//@cheque
                                                      0,//@credito
                                                      cuenta,//@cuenta
                                                      Convert.ToDecimal(registro.Cells[4].Text),//@debito
                                                      Convert.ToInt16(Session["empresa"]),//@empresa
                                                      ddlMediosPago.SelectedValue,//@medioPago
                                                      Convert.ToDateTime(txtFecha.Text).Month,//@mes
                                                      registro.Cells[5].Text,//@nota
                                                      numero,//@numero
                                                      numeroCuenta,//@numeroCuenta
                                                      null,//@numeroReferencia
                                                      referencia,//@referencia
                                                      i,//@registro
                                                      ddlSucursal.SelectedValue,//@sucursal
                                                      tercero,
                                                      ddlTipoDocumento.SelectedValue,//@tipo
                                                      null,//@tipoReferencia
                                                      Convert.ToDecimal(registro.Cells[4].Text),//@valorPagado
                                                      0,//@valorTotal
                                                      Convert.ToDecimal(registro.Cells[4].Text)//@valorTotal
                                                };

                                        switch (CentidadMetodos.EntidadInsertUpdateDelete("cTesoreriadetalle", "inserta", "ppa", objValoresCuerpoDebito))
                                        {
                                            case 1:
                                                verificacion = true;
                                                break;
                                        }
                                        break;
                                    case 1:
                                        verificacion = true;
                                        break;
                                }
                                i++;

                            }

                            if (gvLista.Rows.Count > 0)
                            {
                                int j = 0;

                                object[] objValoresCabezaCxC = new object[]{
                                                       Convert.ToDateTime(txtFecha.Text).Year,//@año
                                                       false,//@anulado
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
                                        foreach (GridViewRow registro in this.gvLista.Rows)
                                        {
                                            object[] objValoresCuerpoCredito = new object[]{
                                                      Convert.ToDateTime(txtFecha.Text).Year,//@año
                                                      Convert.ToDecimal(registro.Cells[4].Text),//@base
                                                      null,//@ccosto
                                                      Convert.ToDecimal(registro.Cells[4].Text),//@credito
                                                      Convert.ToDecimal(registro.Cells[4].Text),//@creditoAlterno
                                                      registro.Cells[2].Text,//@cuenta
                                                      null,//@cuentaAlterna
                                                      0,//@debito
                                                      0,//@debitoAlterno
                                                      0,//@diasVencimiento
                                                      Convert.ToInt16(Session["empresa"]),//@empresa
                                                      Convert.ToDateTime(txtFecha.Text),
                                                      Convert.ToDateTime(txtFecha.Text).Month,//@mes
                                                      txtDetalle.Text,
                                                      numero,//@numero
                                                      null,//@numeroReferencia
                                                      0,//@porcentaje
                                                      j,//@registro
                                                      ddlSucursal.SelectedValue,//@sucursal
                                                      tercero,
                                                      ddlTipoDocumento.SelectedValue,//@tipo
                                                      null,//@tipo
                                                      
                                                };

                                            switch (CentidadMetodos.EntidadInsertUpdateDelete("cTransaccionDetalle", "inserta", "ppa", objValoresCuerpoCredito))
                                            {
                                                case 0:
                                                    j++;
                                                    string cuenta = numeroCuenta == null ? medioPago.RetornaAuxiliarCaja(caja, Convert.ToInt16(Session["empresa"])) :
                                                            medioPago.RetornaAuxiliarCuentaBanco(numeroCuenta, Convert.ToInt16(Session["empresa"]));

                                                    object[] objValoresCuerpoDebito = new object[]{
                                                      Convert.ToDateTime(txtFecha.Text).Year,//@año
                                                      Convert.ToDecimal(registro.Cells[4].Text),//@base
                                                      null,//@ccosto
                                                      0,//@credito
                                                      0,//@creditoAlterno
                                                      cuenta,//@cuenta
                                                      null,//@cuentaAlterna
                                                      Convert.ToDecimal(registro.Cells[4].Text),//@debito
                                                      Convert.ToDecimal(registro.Cells[4].Text),//@debitoAlterno
                                                      0,//@diasVencimiento
                                                      Convert.ToInt16(Session["empresa"]),//@empresa
                                                      Convert.ToDateTime(txtFecha.Text),
                                                      Convert.ToDateTime(txtFecha.Text).Month,//@mes
                                                      txtDetalle.Text,
                                                      numero,//@numero
                                                      null,//@numeroReferencia
                                                      0,//@porcentaje
                                                      j,//@registro
                                                      ddlSucursal.SelectedValue,
                                                      tercero,
                                                      ddlTipoDocumento.SelectedValue,//@tipo
                                                      null,//@tipo
                                                      
                                                };
                                                    switch (CentidadMetodos.EntidadInsertUpdateDelete("cTransaccionDetalle", "inserta", "ppa", objValoresCuerpoDebito))
                                                    {
                                                        case 1:
                                                            verificacion = true;
                                                            break;
                                                    }
                                                    break;
                                                case 1:
                                                    verificacion = true;
                                                    break;
                                            }
                                            j++;
                                        }
                                        break;
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

                    if (verificacion == true)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "Error al insertar el detalle de la transacción. Operación no realizada", "warning");
                        return;
                    }

                    if (this.txtNumero.Enabled == false)
                    {
                        if (tipoTransaccion.ActualizaConsecutivo(Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"])) != 0)
                        {
                            CerroresGeneral.ManejoError(this, GetType(), "Error al actualizar el consecutivo. Operación no realizada", "warning");
                            return;
                        }
                    }

                    this.Session["numero"] = numero;
                    this.Session["tipo"] = this.ddlTipoDocumento.SelectedValue;

                    ManejoExito("Transacción registrada satisfactoriamente. Transacción número " + numero, "I");
                    ts.Complete();
                    ImprimriTransaccion(Convert.ToString(this.Session["empresa"]), tipo, numero);
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar la transacción. Correspondiente a: " + ex.Message, "I");
            }
        }
        private void CargarCombosDetalle()
        {
            try
            {
                this.ddlCaja.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cCaja", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlCaja.DataValueField = "codigo";
                this.ddlCaja.DataTextField = "descripcion";
                this.ddlCaja.DataBind();
                this.ddlCaja.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }

            try
            {
                this.ddlMediosPago.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cMediosPago", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlMediosPago.DataValueField = "codigo";
                this.ddlMediosPago.DataTextField = "descripcion";
                this.ddlMediosPago.DataBind();
                this.ddlMediosPago.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }

            try
            {
                this.ddlCuentaBancaria.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cCuentaBancaria", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlCuentaBancaria.DataValueField = "codigo";
                this.ddlCuentaBancaria.DataTextField = "descripcion";
                this.ddlCuentaBancaria.DataBind();
                this.ddlCuentaBancaria.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
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
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private void validaFecha()
        {
            this.niCalendarFecha.Visible = false;
            this.txtFecha.Visible = true;
            try
            {
                Convert.ToDateTime(txtFecha.Text);
                ddlTercero.Focus();
            }
            catch
            {
                txtFecha.Text = "";
                txtFecha.Focus();
                CerroresGeneral.ManejoError(this, GetType(), "formato de fecha no valido..", "warning");
                return;
            }

            if (periodo.RetornaPeriodoCerrado(Convert.ToDateTime(txtFecha.Text).Year, Convert.ToDateTime(txtFecha.Text).Month, (int)this.Session["empresa"]) == 1)
            {
                ManejoError("Periodo cerrado. No es posible realizar nuevas transacciones", "I");
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
            this.Session["cajaCliente"] = null;
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "i");
            this.lbRegistrar.Visible = false;
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
            lbFecha.Visible = false;
            txtFecha.Visible = false;
            upDetalle.Visible = false;
        }
        private void CargaCampos()
        {
            try
            {
                this.niddlCampo.DataSource = transacciones.GetCamposEntidades("cTesoreria", "");
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
        private void ManejoEncabezado()
        {
            HabilitaEncabezado();
            CargarTipoTransaccion();
        }
        private void CargarTipoTransaccion()
        {
            try
            {
                this.ddlTipoDocumento.DataSource = tipoTransaccion.GetTipoTransaccionModuloForma(Convert.ToInt16(Session["empresa"]), 27);
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
            this.Session["cajaCliente"] = null;
            this.Session["impuesto"] = null;
            lbFecha.Visible = true;
            txtFecha.Visible = true;
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

                if (CentidadMetodos.EntidadGetKey("cTesoreria", "ppa", objkey).Tables[0].DefaultView.Count > 0)
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
            CcontrolesUsuario.ComportamientoCampoEntidad(this.upCabeza.Controls, "cTesoreria", Convert.ToString(this.ddlTipoDocumento.SelectedValue), (int)this.Session["empresa"]);


        }
        private void TotalizaGrillaReferencia()
        {
            try
            {
                this.nitxtTotalDiferencia.Text = "0";
                nitxtTotalPagado.Text = "0";

                foreach (GridViewRow registro in this.gvLista.Rows)
                {
                    this.nitxtTotalPagado.Text = Convert.ToString(Convert.ToDecimal(registro.Cells[4].Text) + Convert.ToDecimal(this.nitxtTotalPagado.Text));

                }
                this.nitxtTotalDiferencia.Text = Convert.ToString(Convert.ToDecimal(this.txvValor.Text) - Convert.ToDecimal(nitxtTotalPagado.Text));
                nitxtTotalDiferencia.Enabled = false;
                nitxtTotalPagado.Enabled = false;

            }
            catch (Exception ex)
            {
                ManejoError("Error al totalizar la grilla de referencia. Correspondiente a: " + ex.Message, "C");
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
                        CargaCampos();
                        this.Session["cajaCliente"] = null;
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
                upCabeza.Visible = true;
                upDetalle.Visible = true;
                CcontrolesUsuario.HabilitarControles(this.upCabeza.Controls);
                CcontrolesUsuario.LimpiarControles(this.upCabeza.Controls);
                CcontrolesUsuario.HabilitarControles(this.upDetalle.Controls);
                CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
                this.gvLista.DataSource = null;
                this.gvLista.DataBind();
                this.Session["cajaCliente"] = null;

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
                CargarCombosDetalle();

                ValidaMedioPago();

                lbFecha.Visible = true;
                txtFecha.Visible = true;
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
            this.upCabeza.Visible = false;
            this.upDetalle.Visible = false;
            this.upConsulta.Visible = true;

            this.niimbConsulta.BorderStyle = BorderStyle.None;
            this.niimbRegistro.BorderStyle = BorderStyle.Solid;
            this.niimbRegistro.BorderColor = System.Drawing.Color.White;
            this.niimbRegistro.BorderWidth = Unit.Pixel(1);
            this.niimbConsulta.Enabled = false;
            this.niimbRegistro.Enabled = true;
            this.Session["cajaCliente"] = null;
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();


        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(TipoTransaccionConfig(15)) == true)
                GuardaReferencia();
            else
                Editar();
        }
        protected void ddlTercero_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(TipoTransaccionConfig(6)) == true)
                {
                    this.gvLista.DataSource = null;
                    this.gvLista.DataBind();
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar documentos referencia. Correspondiente a: " + ex.Message, "C");
            }

            cargarSucurlsal(ddlTercero.SelectedValue);
        }
        protected void nibtnCargar_Click(object sender, EventArgs e)
        {

            string requi = "";
            bool verifica = false;
            try
            {
                //foreach (GridViewRow registro in this.nigvTrnReferencia.Rows)
                //{
                //    if (((CheckBox)registro.FindControl("chbRequi")).Checked == true)
                //        verifica = true;
                //}

                //if (verifica == false)
                //{
                //    CerroresGeneral.ManejoError(this,GetType(), "Debe seleccionar minimo una requisición", "warning");
                //    this.gvLista.DataSource = null;
                //    this.gvLista.DataBind();
                //    return;
                //}

                //if (ddlTercero.SelectedValue.Length == 0 || ddlSucursal.SelectedValue.Length == 0)
                //{
                //    CerroresGeneral.ManejoError(this,GetType(), "Debe seleccionar proveedor y sucursal, antes de cargar datos", "warning");
                //    return;
                //}
                //foreach (GridViewRow registro in this.nigvTrnReferencia.Rows)
                //{
                //    if (((CheckBox)registro.FindControl("chbRequi")).Checked == true)
                //        requi = registro.Cells[2].Text + "," + requi;
                //}

                //this.gvLista.DataSource = tipoTransaccion.ExecReferenciaDetalle(Convert.ToString(TipoTransaccionConfig(8)), requi.Substring(0, requi.Length - 1), Convert.ToInt16(Session["empresa"]));
                //this.gvLista.DataBind();
                //bool validaImpuesto = false;
                //List<Cimpuestos> listaImpuesto = null;
                //foreach (GridViewRow registro in this.gvLista.Rows)
                //{
                //    validaImpuesto = false;
                //    DataView dvImpuesto = tipoTransaccion.SeleccionaImpuestoItemTercero(Convert.ToInt16(Session["empresa"]), ddlSucursal.SelectedValue, Convert.ToInt16(ddlTercero.SelectedValue), Convert.ToInt16(registro.Cells[1].Text));
                //    foreach (DataRowView impuesto in dvImpuesto)
                //    {

                //        listaImpuesto = (List<Cimpuestos>)this.Session["impuestos"];
                //        if (Session["impuestos"] != null)
                //        {
                //            foreach (Cimpuestos nt in listaImpuesto)
                //            {
                //                if (impuesto.Row.ItemArray.GetValue(0).ToString() == nt.Concepto)
                //                    validaImpuesto = true;
                //            }
                //        }

                //        if (validaImpuesto == false)
                //        {
                //            transaccionImpuesto = new Cimpuestos(impuesto.Row.ItemArray.GetValue(0).ToString(),
                //                impuesto.Row.ItemArray.GetValue(1).ToString(),
                //                impuesto.Row.ItemArray.GetValue(2).ToString(),
                //                Convert.ToDecimal(impuesto.Row.ItemArray.GetValue(3)),
                //                Convert.ToDecimal(impuesto.Row.ItemArray.GetValue(4)),
                //                Convert.ToDecimal(impuesto.Row.ItemArray.GetValue(5)),
                //                0,
                //                0);

                //            if (this.Session["impuestos"] == null)
                //            {
                //                listaImpuesto = new List<Cimpuestos>();
                //                listaImpuesto.Add(transaccionImpuesto);
                //            }
                //            else
                //            {
                //                listaImpuesto = (List<Cimpuestos>)Session["impuestos"];
                //                listaImpuesto.Add(transaccionImpuesto);
                //            }
                //            this.Session["impuestos"] = listaImpuesto;
                //        }
                //    }
                //    gvImpuesto.Visible = true;
                //    this.gvImpuesto.DataSource = listaImpuesto;
                //    this.gvImpuesto.DataBind();
                //}
                //foreach (GridViewRow registro in this.gvLista.Rows)
                //{
                //    ((TextBox)registro.FindControl("txtCantidad")).Enabled = Convert.ToBoolean(TipoTransaccionConfig(9));
                //    ((TextBox)registro.FindControl("txtValorUnitario")).Enabled = Convert.ToBoolean(TipoTransaccionConfig(10));
                //    ((TextBox)registro.FindControl("txtDescuento")).Enabled = Convert.ToBoolean(TipoTransaccionConfig(20));
                //}
                //TotalizaGrillaReferencia();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar detalle de Transacción. Correspondiente a: " + ex.Message, "C");
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
                {
                    this.imbBusqueda.Visible = false;
                }

                EstadoInicialGrillaTransacciones();
            }
            catch
            {
            }
        }
        protected void gvTransaccion_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


            this.Session["editar"] = true;
            this.Session["cajaCliente"] = null;
            bool anulado = false, aprobado = false, cerrado = false;

            foreach (Control objControl in gvTransaccion.Rows[e.RowIndex].Cells[8].Controls)
            {
                anulado = ((CheckBox)objControl).Checked;
            }

            if (anulado == true)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Registro anulado no es posible su edición", "warning");
                return;
            }


            foreach (Control objControl in gvTransaccion.Rows[e.RowIndex].Cells[9].Controls)
            {
                aprobado = ((CheckBox)objControl).Checked;
            }

            if (aprobado == true)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Registro aprobado no es posible su edición", "warning");
                string vtn = "window.open('Detalle.aspx?numero=" + gvTransaccion.Rows[e.RowIndex].Cells[2].Text.Trim() + "&tipo=" + gvTransaccion.Rows[e.RowIndex].Cells[1].Text.Trim() + "','Detalle de transacción','scrollbars=yes,resizable=yes','height=200', 'width=300')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
                return;
            }

            foreach (Control objControl in gvTransaccion.Rows[e.RowIndex].Cells[10].Controls)
            {
                cerrado = ((CheckBox)objControl).Checked;
            }

            if (cerrado == true)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Registro cerrado no es posible su edición", "warning");
                string vtn = "window.open('Detalle.aspx?numero=" + gvTransaccion.Rows[e.RowIndex].Cells[2].Text.Trim() + "&tipo=" + gvTransaccion.Rows[e.RowIndex].Cells[1].Text.Trim() + "','Detalle de transacción','scrollbars=yes,resizable=yes','height=200', 'width=300')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
                return;
            }



            try
            {
                if (periodo.RetornaPeriodoCerrado(Convert.ToInt16(gvTransaccion.Rows[e.RowIndex].Cells[3].Text), Convert.ToInt16(gvTransaccion.Rows[e.RowIndex].Cells[4].Text), Convert.ToInt16(Session["empresa"])) == 1)
                {
                    ManejoError("Periodo cerrado contable. No es posible editar transacciones", "I");
                    string vtn = "window.open('Detalle.aspx?numero=" + gvTransaccion.Rows[e.RowIndex].Cells[2].Text.Trim() + "&tipo=" + gvTransaccion.Rows[e.RowIndex].Cells[1].Text.Trim() + "','Detalle de transacción','scrollbars=yes,resizable=yes','height=200', 'width=300')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
                    return;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al verificar periodo. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.hdTransaccionConfig.Value = CcontrolesUsuario.TipoTransaccionConfig(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, Convert.ToInt16(Session["empresa"]));


                if (transacciones.VerificaEdicionBorrado(this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, Convert.ToInt16(Session["empresa"])) != 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Transacción ejecutada no es posible su edición", "warning");
                    string vtn = "window.open('Detalle.aspx?numero=" + gvTransaccion.Rows[e.RowIndex].Cells[2].Text.Trim() + "&tipo=" + gvTransaccion.Rows[e.RowIndex].Cells[1].Text.Trim() + "','Detalle de transacción','scrollbars=yes,resizable=yes','height=200', 'width=300')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
                    return;
                }

                CargarTipoTransaccion();
                upRegistro.Visible = true;
                CcontrolesUsuario.HabilitarControles(this.upRegistro.Controls);
                this.ddlTipoDocumento.SelectedValue = this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text;
                this.ddlTipoDocumento.Enabled = false;
                this.txtNumero.Text = this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text;
                this.txtNumero.Enabled = false;
                this.nilbNuevo.Visible = false;
                this.hdTransaccionConfig.Value = CcontrolesUsuario.TipoTransaccionConfig(this.ddlTipoDocumento.SelectedValue, Convert.ToInt16(Session["empresa"]));

                CcontrolesUsuario.LimpiarControles(this.upCabeza.Controls);
                ComportamientoTransaccion();
                object[] objCab = new object[] { Convert.ToInt16(Session["empresa"]), this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text };
                //foreach (DataRowView encabezado in CentidadMetodos.EntidadGetKey("cTesoreria", "ppa", objCab).Tables[0].DefaultView)
                //{
                //    this.niCalendarFecha.SelectedDate = Convert.ToDateTime(encabezado.Row.ItemArray.GetValue(8));
                //    this.niCalendarFecha.Visible = false;
                //    this.txtFecha.Text = Convert.ToString(encabezado.Row.ItemArray.GetValue(8));
                //    txtDocref.Text = Convert.ToString(encabezado.Row.ItemArray.GetValue(12));
                //    this.txtObservacion.Text = Convert.ToString(encabezado.Row.ItemArray.GetValue(13));
                //    if (this.ddlTercero.Visible == true)
                //    {
                //        this.ddlTercero.SelectedValue = Convert.ToString(encabezado.Row.ItemArray.GetValue(9));
                //        cargarSucurlsal(ddlTercero.SelectedValue.ToString());
                //        if (this.ddlSucursal.Visible == true)
                //            this.ddlSucursal.SelectedValue = Convert.ToString(encabezado.Row.ItemArray.GetValue(10));
                //    }


                //    nitxtTotalValorBruto.Text = encabezado.Row.ItemArray.GetValue(14).ToString();
                //    nitxtTotalDescuento.Text = encabezado.Row.ItemArray.GetValue(15).ToString();
                //    nitxtTotalImpuesto.Text = encabezado.Row.ItemArray.GetValue(16).ToString();
                //    nitxtTotal.Text = encabezado.Row.ItemArray.GetValue(17).ToString();
                //}

                //this.upDetalle.Visible = true;
                //this.nilblValorTotal.Visible = true;
                //this.nitxtTotalValorBruto.Visible = true;
                //this.nigvTrnReferencia.Visible = true;
                //this.nibtnBuscar.Visible = false;
                //nibtnActualizar.Visible = false;

                //try
                //{
                //    gvLista.DataSource = tipoTransaccion.DatosEditarCotizacion(this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, Convert.ToInt16(Session["empresa"]));
                //    gvLista.DataBind();
                //}
                //catch (Exception ex)
                //{
                //    ManejoError("Error al cargar detalle de la transacción. Correspondiente a: " + ex.Message, "A");
                //}

                //try
                //{
                //    gvImpuesto.DataSource = tipoTransaccion.datosImpuestoEditarCotizacion(this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, Convert.ToInt16(Session["empresa"]));
                //    gvImpuesto.DataBind();
                //}
                //catch (Exception ex)
                //{
                //    ManejoError("Error al cargar los impuestos. Correspondiente a: " + ex.Message, "A");
                //}


            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la transacción. Correspondiente a: " + ex.Message, "A");
            }

            TabRegistro();
        }
        protected void gvTransaccion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    if (transacciones.VerificaEdicionBorrado(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text, Convert.ToInt16(Session["empresa"])) != 0)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "Transacción ejecutada no es posible su edición", "warning");
                        return;
                    }

                    if (tipoTransaccion.RetornaTipoBorrado(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, Convert.ToInt16(Session["empresa"])) == "E")
                    {
                        object[] objValores = new object[] { Convert.ToInt16(Session["empresa"]), Convert.ToString(this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text).Trim(), Convert.ToString(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text).Trim() };

                        switch (CentidadMetodos.EntidadInsertUpdateDelete("cTesoreriaDetalle", "elimina", "ppa", objValores))
                        {
                            case 0:
                                switch (CentidadMetodos.EntidadInsertUpdateDelete("cTesoreria", "elimina", "ppa", objValores))
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
                        //switch (transacciones.AnulaTransaccion(Convert.ToInt16(Session["empresa"]), this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text, this.Session["usuario"].ToString().Trim()))
                        //{
                        //    case 0:
                        //         CerroresGeneral.ManejoError(this,GetType(), "Registro Anulado Satisfactoriamente", "i");
                        //        BusquedaTransaccion();
                        //        ts.Complete();
                        //        break;
                        //    case 1:
                        //         CerroresGeneral.ManejoError(this,GetType(), "Error al anular la transacción. Operación no realizada", "i");
                        //        break;
                        //}
                    }
                }
                catch (Exception ex)
                {
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
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
        private void ImprimriTransaccion(string empresa, string tipo, string numero)
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
                    GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    int RowIndex = row.RowIndex;
                    ImprimriTransaccion(this.Session["empresa"].ToString(), gvTransaccion.Rows[RowIndex].Cells[1].Text.Trim(), gvTransaccion.Rows[RowIndex].Cells[2].Text.Trim());
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
                foreach (Control c in this.gvLista.SelectedRow.Cells[6].Controls)
                {
                    if (c is CheckBox)
                        anulado = ((CheckBox)c).Checked;
                }
                if (anulado == true)
                {
                    Session["editarDetalle"] = false;
                    CerroresGeneral.ManejoError(this, GetType(), "Registro anulado no es posible su edición", "w");
                    return;
                }

                this.hdCantidad.Value = this.gvLista.SelectedRow.Cells[4].Text;
                this.hdRegistro.Value = this.gvLista.SelectedRow.Cells[7].Text;
                CargarCombosDetalle();

                List<CcajaOtros> listaTransaccion = null;
                listaTransaccion = (List<CcajaOtros>)this.Session["cajaCliente"];
                foreach (CcajaOtros datos in listaTransaccion)
                {
                    if (datos.Registro == Convert.ToInt32(gvLista.SelectedRow.Cells[7].Text))
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
            }
            catch (Exception ex)
            {
                Session["editarDetalle"] = false;
                CerroresGeneral.ManejoError(this, GetType(), "Error al cargar los campos del registro en el formulario. Correspondiente a: " + ex.Message, "w");
            }
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
                }
                catch (Exception ex)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Error al eliminar el registro. Correspondiente a: " + ex.Message, "warning");
                }
            }
            else
            {
                foreach (Control c in this.gvLista.Rows[e.RowIndex].Cells[6].Controls)
                {
                    if (c is CheckBox)
                        anulado = ((CheckBox)c).Checked;
                }
                if (anulado == true)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Registro anulado no es posible volver anular", "warning");
                    return;
                }

                foreach (Control c in this.gvLista.Rows[e.RowIndex].Cells[6].Controls)
                {
                    if (c is CheckBox)
                        ((CheckBox)c).Checked = true;
                }
                this.gvLista.Rows[e.RowIndex].BackColor = System.Drawing.Color.Red;
            }
        }
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {

                if (Convert.ToString(this.ddlTipoDocumento.SelectedValue).Trim().Length == 0 || this.txtNumero.Text.Trim().Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe ingresar tipo y número de transacción", "warning");
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

                if (valorTotal + Convert.ToDecimal(txvValorDetalle.Text) > Convert.ToDecimal(txvValor.Text))
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Valor del detalle no puede ser mayor al valor total recaudado. Por favor corrija", "warning");
                    return;
                }


                if (Convert.ToDecimal(txvValorDetalle.Text) <= 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "El valor no puede ser igual o menor que cero. Por favor corrija", "warning");
                    return;
                }

                if (ddlTercero.SelectedValue.Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar proveedor y sucursal, antes de cargar datos", "warning");
                    return;
                }

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
                CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
                CcontrolesUsuario.LimpiarCombos(this.upDetalle.Controls);
                TotalizaGrillaReferencia();
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Error al insertar el registro. Correspondiente a: " + ex.Message, "warning");
            }
        }
        protected void ddlMediosPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidaMedioPago();
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
        protected void txtCuenta_TextChanged(object sender, EventArgs e)
        {
            manejoCuenta();

            int verifica = puc.verificaAuxiliar(txtCuenta.Text.Trim(), Convert.ToInt16(this.Session["empresa"]));
            if (verifica == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "buscarcuenta", "buscarCuenta();", true);
            }
        }

        #endregion Eventos
    }
}