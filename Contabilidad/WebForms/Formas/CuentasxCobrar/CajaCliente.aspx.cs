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
    public partial class CajaCliente : BasePage
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
        CCajaCliente transaccionCaja = new CCajaCliente();
        decimal valorTotal = 0;

        #endregion Instancias

        #region Metodos
        private void totalizaGrilla()
        {
            decimal totalSaldo = 0, totalPagado = 0;

            foreach (GridViewRow registro in this.gvReferencia.Rows)
            {
                registro.Cells[10].Text = Convert.ToDecimal(Convert.ToDecimal(registro.Cells[8].Text) - Convert.ToDecimal(((TextBox)(registro.Cells[9].FindControl("txtValorUnitario"))).Text)).ToString("N2");
                totalSaldo += Convert.ToDecimal(registro.Cells[10].Text);
                totalPagado += Convert.ToDecimal(((TextBox)(registro.Cells[9].FindControl("txtValorUnitario"))).Text);
            }
            nitxtTotalValor.Text = totalPagado.ToString("N2");
            nitxtTotalSaldo.Text = totalSaldo.ToString("N2");
            nitxtTotalSaldo.Enabled = false;
            nitxtTotalValor.Enabled = false;

        }
        private void ValidaMedioPago()
        {
            try
            {
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
        private void validaFiltroFecha()
        {
            if (chkFechaFiltro.Checked)
            {
                niCalendarFechaFinalFiltro.Visible = false;
                txtFechaInicialFiltro.Enabled = true;
                txtFechaFinalFiltro.Enabled = true;
                txtFechaFinalFiltro.Text = "";
                txtFechaInicialFiltro.Text = "";
                niCalendarFechaInicioFiltro.Visible = false;
            }
            else
            {
                niCalendarFechaFinalFiltro.Visible = false;
                txtFechaInicialFiltro.Enabled = false;
                txtFechaFinalFiltro.Enabled = false;
                txtFechaFinalFiltro.Text = "";
                txtFechaInicialFiltro.Text = "";
                niCalendarFechaInicioFiltro.Visible = false;
            }
        }
        private void validaFiltroNumero()
        {
            if (chkNumeroFiltro.Checked)
            {
                txvInicialFiltro.Enabled = true;
                txvFinalFiltro.Enabled = true;
                txvInicialFiltro.Text = "0";
                txvFinalFiltro.Text = "0";
            }
            else
            {
                txvInicialFiltro.Enabled = false;
                txvFinalFiltro.Enabled = false;
                txvInicialFiltro.Text = "0";
                txvFinalFiltro.Text = "0";
            }
        }
        private void cargarSucurlsal(string tercero)
        {
            try
            {
                DataView dvSucursal = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cxcCliente", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                dvSucursal.RowFilter = "empresa = " + Session["empresa"].ToString() + " and idTercero=" + tercero;
                this.ddlSucursalFiltro.DataSource = dvSucursal;
                this.ddlSucursalFiltro.DataValueField = "codigo";
                this.ddlSucursalFiltro.DataTextField = "descripcion";
                this.ddlSucursalFiltro.DataBind();
                this.ddlSucursalFiltro.Items.Insert(0, new ListItem("", ""));
                this.ddlSucursalFiltro.SelectedValue = "";
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
            this.gvReferencia.DataSource = null;
            this.gvReferencia.DataBind();
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
                if (this.gvReferencia.Rows.Count == 0)
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


                if (Convert.ToString(this.ddlMediosPago.SelectedValue).Trim().Length == 0 || Server.HtmlDecode(this.txtDetalle.Text.Trim()).Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Campos vacios en medios de pago. Por favor corrija", "warning");
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

                if (Convert.ToBoolean(TipoTransaccionConfig(3)) != true)
                {
                    foreach (GridViewRow registro in this.gvReferencia.Rows)
                    {
                        if (Convert.ToDecimal(((TextBox)registro.FindControl("txtValorUnitario")).Text) == 0)
                            verificacion = true;
                    }

                    if (verificacion == true)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "Debe ingresar el valor por lo menos una CxC para continuar", "warning");
                        return;
                    }
                }
                else
                    verificacion = true;

                using (TransactionScope ts = new TransactionScope())
                {
                    foreach (GridViewRow registro in this.gvReferencia.Rows)
                    {
                        if (((CheckBox)registro.FindControl("chkSeleccion")).Checked == true)
                            verificacionCK = true;
                    }

                    if (verificacionCK == false)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar al menosuna CxC para guardar la transacción", "warning");
                        return;
                    }

                    if (this.ddlTercero.Enabled == false)
                        tercero = null;
                    else
                        tercero = Convert.ToString(this.ddlTercero.SelectedValue);

                    totalizaGrilla();


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
                                    Convert.ToDecimal(nitxtTotalValor.Text),
                                    Convert.ToDecimal(nitxtTotalSaldo.Text)
                                };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("cTesoreria", "inserta", "ppa", objValores))
                    {
                        case 0:

                            string banco = null, cheque = null, referencia = null;
                            banco = ddlBanco.Visible == true ? ddlBanco.SelectedValue : null;
                            cheque = txtNoCheque.Visible == true ? txtNoCheque.Text : null;
                            referencia = txtReferencia.Visible == true ? txtReferencia.Text : null;
                            string numeroCuenta = ddlCuentaBancaria.Visible == true ? ddlCuentaBancaria.SelectedValue : null;
                            string caja = ddlCaja.Visible == true ? ddlCaja.SelectedValue : null;
                            int i = 0;

                            foreach (GridViewRow registro in this.gvReferencia.Rows)
                            {
                                if (((CheckBox)registro.FindControl("chkSeleccion")).Checked == true)
                                {
                                    if (Convert.ToDecimal(((TextBox)registro.FindControl("txtValorUnitario")).Text) > 0)
                                    {

                                        object[] objValoresCuerpoCredito = new object[]{
                                                      Convert.ToDateTime(txtFecha.Text).Year,//@año
                                                      banco,
                                                      caja,
                                                      null,
                                                      cheque,
                                                      Convert.ToDecimal(((TextBox)registro.FindControl("txtValorUnitario")).Text),
                                                      registro.Cells[5].Text,//@cuenta
                                                      0,
                                                      Convert.ToInt16(Session["empresa"]),//@empresa
                                                      ddlMediosPago.SelectedValue,
                                                      Convert.ToDateTime(txtFecha.Text).Month,//@mes
                                                      txtDetalle.Text,
                                                      numero,//@numero
                                                      numeroCuenta,
                                                      registro.Cells[2].Text,//@numeroReferencia
                                                      referencia,
                                                      i,//@registro
                                                       registro.Cells[3].Text,
                                                      tercero,
                                                      ddlTipoDocumento.SelectedValue,//@tipo
                                                      registro.Cells[1].Text,//@tipoReferencia
                                                      Convert.ToDecimal(((TextBox)registro.FindControl("txtValorUnitario")).Text),
                                                      Convert.ToDecimal(registro.Cells[10].Text),//@valorTotal
                                                      Convert.ToDecimal(registro.Cells[8].Text)//@valorTotal
                                                };

                                        switch (CentidadMetodos.EntidadInsertUpdateDelete("cTesoreriadetalle", "inserta", "ppa", objValoresCuerpoCredito))
                                        {
                                            case 0:
                                                string cuenta = numeroCuenta == null ? medioPago.RetornaAuxiliarCaja(caja, Convert.ToInt16(Session["empresa"])) :
                                                    medioPago.RetornaAuxiliarCuentaBanco(numeroCuenta, Convert.ToInt16(Session["empresa"]));
                                                i++;
                                                object[] objValoresCuerpoDebito = new object[]{
                                                      Convert.ToDateTime(txtFecha.Text).Year,//@año
                                                      banco,
                                                      caja,
                                                      null,
                                                      cheque,
                                                      0,
                                                      cuenta,
                                                      Convert.ToDecimal(((TextBox)registro.FindControl("txtValorUnitario")).Text),
                                                      Convert.ToInt16(Session["empresa"]),//@empresa
                                                      ddlMediosPago.SelectedValue,
                                                      Convert.ToDateTime(txtFecha.Text).Month,//@mes
                                                      txtDetalle.Text,
                                                      numero,//@numero
                                                      numeroCuenta,
                                                      registro.Cells[2].Text,//@numeroReferencia
                                                      referencia,
                                                      i,//@registro
                                                      registro.Cells[3].Text,
                                                      tercero,
                                                      ddlTipoDocumento.SelectedValue,//@tipo
                                                      registro.Cells[1].Text,//@tipoReferencia
                                                      Convert.ToDecimal(((TextBox)registro.FindControl("txtValorUnitario")).Text),
                                                      Convert.ToDecimal(registro.Cells[10].Text),//@valorTotal
                                                      Convert.ToDecimal(registro.Cells[8].Text)//@valorTotal
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
                                }
                            }

                            if (gvReferencia.Rows.Count > 0)
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
                                        foreach (GridViewRow registro in this.gvReferencia.Rows)
                                        {

                                            if (((CheckBox)registro.FindControl("chkSeleccion")).Checked == true)
                                            {
                                                if (Convert.ToDecimal(((TextBox)registro.FindControl("txtValorUnitario")).Text) > 0)
                                                {

                                                    object[] objValoresCuerpoCredito = new object[]{
                                                      Convert.ToDateTime(txtFecha.Text).Year,//@año
                                                      Convert.ToDecimal(((TextBox)registro.FindControl("txtValorUnitario")).Text),//@base
                                                      null,//@ccosto
                                                      Convert.ToDecimal(((TextBox)registro.FindControl("txtValorUnitario")).Text),//@credito
                                                      Convert.ToDecimal(((TextBox)registro.FindControl("txtValorUnitario")).Text),//@creditoAlterno
                                                      registro.Cells[5].Text,//@cuenta
                                                      null,//@cuentaAlterna
                                                      0,//@debito
                                                      0,//@debitoAlterno
                                                      Convert.ToDecimal(registro.Cells[7].Text),//@diasVencimiento
                                                      Convert.ToInt16(Session["empresa"]),//@empresa
                                                      Convert.ToDateTime(txtFecha.Text),
                                                      Convert.ToDateTime(txtFecha.Text).Month,//@mes
                                                      txtDetalle.Text,
                                                      numero,//@numero
                                                      registro.Cells[2].Text,//@numeroReferencia
                                                      0,//@porcentaje
                                                      j,//@registro
                                                      registro.Cells[3].Text,
                                                      tercero,
                                                      ddlTipoDocumento.SelectedValue,//@tipo
                                                      registro.Cells[1].Text,//@tipo
                                                      
                                                };

                                                    switch (CentidadMetodos.EntidadInsertUpdateDelete("cTransaccionDetalle", "inserta", "ppa", objValoresCuerpoCredito))
                                                    {
                                                        case 0:
                                                            j++;
                                                            string cuenta = numeroCuenta == null ? medioPago.RetornaAuxiliarCaja(caja, Convert.ToInt16(Session["empresa"])) :
                                                     medioPago.RetornaAuxiliarCuentaBanco(numeroCuenta, Convert.ToInt16(Session["empresa"]));

                                                            object[] objValoresCuerpoDebito = new object[]{
                                                      Convert.ToDateTime(txtFecha.Text).Year,//@año
                                                      Convert.ToDecimal(((TextBox)registro.FindControl("txtValorUnitario")).Text),//@base
                                                      null,//@ccosto
                                                      0,//@credito
                                                      0,//@creditoAlterno
                                                      cuenta,//@cuenta
                                                      null,//@cuentaAlterna
                                                      Convert.ToDecimal(((TextBox)registro.FindControl("txtValorUnitario")).Text),//@debito
                                                      Convert.ToDecimal(((TextBox)registro.FindControl("txtValorUnitario")).Text),//@debitoAlterno
                                                      Convert.ToDecimal(registro.Cells[7].Text),//@diasVencimiento
                                                      Convert.ToInt16(Session["empresa"]),//@empresa
                                                      Convert.ToDateTime(txtFecha.Text),
                                                      Convert.ToDateTime(txtFecha.Text).Month,//@mes
                                                      txtDetalle.Text,
                                                      numero,//@numero
                                                      registro.Cells[2].Text,//@numeroReferencia
                                                      0,//@porcentaje
                                                      j,//@registro
                                                      registro.Cells[3].Text,
                                                      tercero,
                                                      ddlTipoDocumento.SelectedValue,//@tipo
                                                      registro.Cells[1].Text,//@tipo
                                                      
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
                                                }
                                                j++;
                                            }
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
            lbFecha.Visible = false;
            txtFecha.Visible = false;
            this.txtNumero.Text = "";
            this.nilbNuevo.Focus();
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

        private void CargarCaomboFiltro()
        {
            try
            {
                this.ddlTipoDocumentoFiltro.DataSource = tipoTransaccion.GetTipoTransaccionModuloForma(Convert.ToInt16(Session["empresa"]), 28);
                this.ddlTipoDocumentoFiltro.DataValueField = "codigo";
                this.ddlTipoDocumentoFiltro.DataTextField = "descripcion";
                this.ddlTipoDocumentoFiltro.DataBind();
                this.ddlTipoDocumentoFiltro.Items.Insert(0, new ListItem("", ""));
                this.ddlTipoDocumentoFiltro.SelectedValue = "";
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }


            try
            {
                DataView dvPuc = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cPuc", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                dvPuc.RowFilter = "empresa=" + Session["empresa"].ToString() + " and auxiliar=1 and activo=1";
                ddlAuxiliarFiltro.DataSource = dvPuc;
                this.ddlAuxiliarFiltro.DataValueField = "codigo";
                this.ddlAuxiliarFiltro.DataTextField = "cadena";
                this.ddlAuxiliarFiltro.DataBind();
                this.ddlAuxiliarFiltro.Items.Insert(0, new ListItem("", ""));
                this.ddlAuxiliarFiltro.SelectedValue = "";
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
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
            this.gvReferencia.DataSource = null;
            this.gvReferencia.DataBind();
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
                this.gvReferencia.DataSource = null;
                this.gvReferencia.DataBind();
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

                CargarCaomboFiltro();
                validaFiltroFecha();
                validaFiltroNumero();

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
            this.gvReferencia.DataSource = null;
            this.gvReferencia.DataBind();


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
                    //this.nigvTrnReferencia.DataSource = tipoTransaccion.GetReferencia(Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"]));
                    //this.nigvTrnReferencia.DataBind();

                    this.gvReferencia.DataSource = null;
                    this.gvReferencia.DataBind();

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
                //    this.gvReferencia.DataSource = null;
                //    this.gvReferencia.DataBind();
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

                //this.gvReferencia.DataSource = tipoTransaccion.ExecReferenciaDetalle(Convert.ToString(TipoTransaccionConfig(8)), requi.Substring(0, requi.Length - 1), Convert.ToInt16(Session["empresa"]));
                //this.gvReferencia.DataBind();
                //bool validaImpuesto = false;
                //List<Cimpuestos> listaImpuesto = null;
                //foreach (GridViewRow registro in this.gvReferencia.Rows)
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
                //foreach (GridViewRow registro in this.gvReferencia.Rows)
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
                        switch (transacciones.AnulaIngresos(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text, this.Session["usuario"].ToString().Trim(), Convert.ToInt16(Session["empresa"])))
                        {
                            case 0:
                                CerroresGeneral.ManejoError(this, GetType(), "Registro Anulado Satisfactoriamente", "i");
                                BusquedaTransaccion();
                                ts.Complete();
                                break;
                            case 1:
                                CerroresGeneral.ManejoError(this, GetType(), "Error al anular la transacción. Operación no realizada", "i");
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
        protected void txtFechaInicialFiltro_TextChanged(object sender, EventArgs e)
        {
            this.niCalendarFechaInicioFiltro.Visible = false;
            this.txtFechaInicialFiltro.Visible = true;
            try
            {
                Convert.ToDateTime(txtFechaInicialFiltro.Text);
            }
            catch
            {
                txtFechaInicialFiltro.Text = "";
                txtFechaInicialFiltro.Focus();
                CerroresGeneral.ManejoError(this, GetType(), "Formato de fecha no valido..", "warning");
                return;
            }
        }
        protected void txtFechaFinalFiltro_TextChanged(object sender, EventArgs e)
        {
            this.niCalendarFechaFinalFiltro.Visible = false;
            this.txtFechaFinalFiltro.Visible = true;
            try
            {
                Convert.ToDateTime(txtFechaFinalFiltro.Text);
            }
            catch
            {
                txtFechaFinalFiltro.Text = "";
                txtFechaFinalFiltro.Focus();
                CerroresGeneral.ManejoError(this, GetType(), "Formato de fecha no valido..", "warning");
                return;
            }
        }
        protected void niCalendarFechaInicioFiltro_SelectionChanged(object sender, EventArgs e)
        {
            this.txtFechaInicialFiltro.Visible = true;
            this.txtFechaInicialFiltro.Text = this.niCalendarFechaInicioFiltro.SelectedDate.ToShortDateString();
            niCalendarFechaInicioFiltro.Visible = false;
        }
        protected void niCalendarFechaFinalFiltro_SelectionChanged(object sender, EventArgs e)
        {
            this.txtFechaFinalFiltro.Visible = true;
            this.txtFechaFinalFiltro.Text = this.niCalendarFechaFinalFiltro.SelectedDate.ToShortDateString();
            niCalendarFechaFinalFiltro.Visible = false;
        }
        protected void chkFechaFiltro_CheckedChanged(object sender, EventArgs e)
        {
            validaFiltroFecha();
        }
        protected void chkFichoNumero_CheckedChanged(object sender, EventArgs e)
        {
            validaFiltroNumero();
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
        protected void nibtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaInicial = DateTime.Now;
                DateTime fechaFinal = DateTime.Now;

                if (chkFechaFiltro.Checked)
                {
                    if (txtFechaFinalFiltro.Text.Length == 0 || txtFechaInicialFiltro.Text.Length == 0)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "Debe ingresar una fecha inicial y final para la busqueda", "warning");
                        return;
                    }
                    else
                    {
                        fechaInicial = Convert.ToDateTime(txtFechaInicialFiltro.Text);
                        fechaInicial = Convert.ToDateTime(txtFechaFinalFiltro.Text);
                    }
                }

                if (chkNumeroFiltro.Checked)
                {
                    if (txvInicialFiltro.Text.Length == 0 || txvFinalFiltro.Text.Length == 0)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "Debe ingresar un número inicial y final para la busqueda", "warning");
                        return;
                    }
                }

                gvReferencia.DataSource = transaccionCaja.SeleccionaDetalleCxC(Convert.ToInt32(Session["empresa"]), ddlAuxiliarFiltro.SelectedValue, ddlTipoDocumentoFiltro.SelectedValue,
                    ddlTercero.SelectedValue, ddlSucursalFiltro.SelectedValue, fechaInicial, fechaFinal,
                    chkFechaFiltro.Checked, chkNumeroFiltro.Checked, Convert.ToDecimal(txvInicialFiltro.Text), Convert.ToDecimal(txvFinalFiltro.Text));
                gvReferencia.DataBind();

                totalizaGrilla();
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Error al cargar CxC. Correspondiente a: " + ex.Message, "warning");
            }
        }

        protected void lbFechaInicialFiltro_Click(object sender, EventArgs e)
        {
            this.niCalendarFechaInicioFiltro.Visible = true;
            this.txtFechaInicialFiltro.Visible = false;
            this.niCalendarFechaInicioFiltro.SelectedDate = Convert.ToDateTime(null);
        }
        protected void lbFechaFinalFiltro_Click(object sender, EventArgs e)
        {
            this.niCalendarFechaFinalFiltro.Visible = true;
            this.txtFechaFinalFiltro.Visible = false;
            this.niCalendarFechaFinalFiltro.SelectedDate = Convert.ToDateTime(null);
        }
        protected void ddlMediosPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidaMedioPago();
        }

        #endregion Eventos
    }
}