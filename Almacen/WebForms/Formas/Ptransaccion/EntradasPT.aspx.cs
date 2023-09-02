using Almacen.seguridadInfos;
using Almacen.WebForms.App_Code.General;
using Almacen.WebForms.App_Code.Parametros;
using Almacen.WebForms.App_Code.Transaccion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Almacen.WebForms.Formas.Ptransaccion
{
    public partial class EntradasPT : BasePage
    {
        #region Instancias
        //--------------------------------------------------------------------------

        CentidadMetodos CentidadMetodos = new CentidadMetodos();
        Security seguridad = new Security();
        CIP ip = new CIP();
        Cimpuestos transaccionImpuesto = new Cimpuestos();
        Cterceros tercero = new Cterceros();
        cBodega bode = new cBodega();

        //--------------------------------------------------------------------------

        Ctransacciones transacciones = new Ctransacciones();
        CtipoTransaccion tipoTransaccion = new CtipoTransaccion();
        Coperadores operadores = new Coperadores();
        Cperiodos periodo = new Cperiodos();
        Citems item = new Citems();
        Cdestinos destino = new Cdestinos();
        CtransaccionAlmacen transaccionAlmacen = new CtransaccionAlmacen();

        #endregion Instancias

        #region Metodos


        private void ImprimriTransaccion(string empresa, string tipo, string numero)
        {
            string vtn = "window.open('../Pinformes/ImprimeTransaccion.aspx?empresa=" + empresa + "&tipo=" + tipo + "&numero=" + numero + "','Impresión de Formatos','scrollbars=yes,resizable=yes','height=300', 'width=400')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
        }


        private void bodegaSeleccionada(List<CtransaccionAlmacen> listaTransaccion)
        {
            foreach (GridViewRow gvr in gvLista.Rows)
            {
                var ddlBodega = gvr.FindControl("ddlBodega") as DropDownList;
                string bodega = listaTransaccion.FirstOrDefault(x => x.Registro == Convert.ToInt32(gvr.Cells[12].Text)).Bodega;
                if (bodega.Trim().Length > 0)
                    ddlBodega.SelectedValue = bodega;
            }
        }


        private void inhabilitarItemsOC()
        {
            foreach (GridViewRow gvr in gvLista.Rows)
            {
                var hfoc = gvr.FindControl("hfoc") as HiddenField;
                var ddlBodega = gvr.FindControl("ddlBodega") as DropDownList;
                DataView dvTipoDocto = bode.BuscarEntidad("", Convert.ToInt16(Session["empresa"]));
                EnumerableRowCollection<DataRow> query = from tipodoc in dvTipoDocto.Table.AsEnumerable()
                                                         where tipodoc.Field<int>("empresa") == Convert.ToInt16(Session["empresa"]) & tipodoc.Field<bool>("produccion") == false
                                                         select tipodoc;

                ddlBodega.DataSource = query.AsDataView();
                ddlBodega.DataValueField = "codigo";
                ddlBodega.DataTextField = "cadena";
                ddlBodega.DataBind();
                ddlBodega.Items.Insert(0, new ListItem("", ""));


                if (Convert.ToBoolean(hfoc.Value))
                {
                    //gvr.Cells[0].Enabled = false;
                    var eliminar = null as LinkButton;
                    gvr.Cells[1].Enabled = false;
                    foreach (Control c in gvr.Cells[1].Controls)
                    {
                        if (c is LinkButton)
                        {
                            eliminar = (LinkButton)c;
                        }
                    }

                    eliminar.Enabled = false;
                    eliminar.OnClientClick = "";
                    gvr.ForeColor = System.Drawing.Color.Red;
                    gvr.Cells[0].ForeColor = System.Drawing.Color.White;
                    gvr.Cells[1].ForeColor = System.Drawing.Color.White;
                }
            }
        }

        private void cargarOCDetalle()
        {
            try
            {
                DataView dvDetalle = transacciones.SeleccionaOCDetalleActivas(Convert.ToInt16(this.Session["empresa"]), ddlOC.SelectedValue.Trim());

                if (dvDetalle.Count > 0)
                {
                    EnumerableRowCollection<DataRow> query = from tipodoc in dvDetalle.Table.AsEnumerable()
                                                             where
                                                             tipodoc.Field<string>("numero") == ddlOC.SelectedValue.Trim()
                                                             & tipodoc.Field<int>("empresa") == Convert.ToInt32(this.Session["empresa"])
                                                             select tipodoc;

                    dvDetalle = query.AsDataView();

                    List<CtransaccionAlmacen> ltransaccion = dvDetalle.Table.AsEnumerable().Select(drv =>
                    new CtransaccionAlmacen
                    {
                        Producto = drv.ItemArray.GetValue(0).ToString(),
                        Detalle = drv.ItemArray.GetValue(1).ToString(),
                        Umedida = Server.HtmlDecode(drv.ItemArray.GetValue(2).ToString()),
                        Bodega = drv.ItemArray.GetValue(3).ToString(),
                        Cantidad = Convert.ToDecimal(drv.ItemArray.GetValue(4)),
                        ValorUnitario = Convert.ToDecimal(drv.ItemArray.GetValue(5)),
                        ValorTotal = Convert.ToDecimal(drv.ItemArray.GetValue(6)),
                        Destino = drv.ItemArray.GetValue(7).ToString(),
                        Ccosto = drv.ItemArray.GetValue(8).ToString(),
                        Oc = true,
                        Registro = Convert.ToInt32(drv.ItemArray.GetValue(10).ToString()),
                        Saldo = Convert.ToDecimal(drv.ItemArray.GetValue(4))
                    }
                    ).ToList();

                    int registro = ltransaccion.Max(x => x.Registro) + 1;
                    hdRegistro.Value = registro.ToString();
                    this.Session["transaccion"] = ltransaccion;
                    gvLista.DataSource = ltransaccion;
                    gvLista.DataBind();
                    inhabilitarItemsOC();
                    cargarImpuesto();
                    TotalizaGrillaReferencia();
                }
                else
                {
                    this.Session["transaccion"] = null;
                    this.Session["impuesto"] = null;
                    gvLista.DataSource = null;
                    gvLista.DataBind();
                    gvImpuesto.DataSource = null;
                    gvImpuesto.DataBind();
                    TotalizaGrillaReferencia();
                }

            }
            catch (Exception ex)
            {
                ManejoError("Erro al cargar detalle de orden de compra debido a: " + ex.Message, "I");
            }
        }


        private void cargarSucurlsal(string tercero)
        {
            try
            {
                if (tercero.Trim().Length > 0)
                {

                    DataView dvSucursal = CentidadMetodos.EntidadGet("cxpProveedor", "ppa").Tables[0].DefaultView;
                    EnumerableRowCollection<DataRow> query = from tipodoc in dvSucursal.Table.AsEnumerable()
                                                             where tipodoc.Field<int>("empresa") == Convert.ToInt16(Session["empresa"]) &
                                                             tipodoc.Field<int>("idTercero") == Convert.ToInt16(tercero)
                                                             select tipodoc;

                    this.ddlSucursal.DataSource = query.AsDataView();
                    this.ddlSucursal.DataValueField = "codigo";
                    this.ddlSucursal.DataTextField = "descripcion";
                    this.ddlSucursal.DataBind();
                    this.ddlSucursal.Items.Insert(0, new ListItem("", ""));
                    this.ddlSucursal.SelectedValue = "";

                }
                else
                {
                    ddlSucursal.DataSource = null;
                    ddlSucursal.DataBind();
                }

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar sucursal del tercero. Correspondiente a: " + ex.Message, "C");
            }
        }


        private void cargarOcActivas()
        {
            try
            {
                DataView dvOC = transacciones.SeleccionaOCEncabezadoActivas(Convert.ToInt32(this.Session["empresa"]), ddlSucursal.SelectedValue.Trim(), Convert.ToInt32(ddlTercero.SelectedValue.Trim()), ddlTipoDocumento.SelectedValue.Trim());
                this.ddlOC.DataSource = dvOC;
                this.ddlOC.DataValueField = "numero";
                this.ddlOC.DataTextField = "cadena";
                this.ddlOC.DataBind();
                this.ddlOC.Items.Insert(0, new ListItem("", ""));
                this.ddlOC.SelectedValue = "";
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar las ordenes de compra debido  a: " + ex.Message, "I");
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
                    this.gvTransaccion.DataSource = transacciones.GetTransaccionEntradas(where, Convert.ToInt16(Session["empresa"]));
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
            this.nilblMensajeEdicion.Text = "";
        }
        private void cancelarTransaccion()
        {
            InHabilitaEncabezado();
            CcontrolesUsuario.InhabilitarControles(this.upCabeza.Controls);
            CcontrolesUsuario.LimpiarControles(this.upCabeza.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upDetalle.Controls);
            CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.niCalendarFecha.Visible = false;
            this.niCalendarFecha.SelectedDate = Convert.ToDateTime(null);
            this.lbRegistrar.Visible = false;
            this.lbCancelar.Visible = false;
            upDetalle.Visible = false;
            upCabeza.Visible = false;
            upConsulta.Visible = false;
            this.Session["transaccion"] = null;
            this.Session["operadores"] = null;
            Session["editarDetalle"] = false;
        }

        private void Guardar()
        {
            try
            {
                Convert.ToDateTime(txtFecha.Text);
            }
            catch (Exception ex)
            {
                ManejoError("Formato de fecha no válido por favor corrija", "I");
                return;
            }

            string numero = "", periodo = Convert.ToString(Convert.ToDateTime(txtFecha.Text).Year) + Convert.ToString(Convert.ToDateTime(txtFecha.Text).Month).PadLeft(2, '0').Trim(), tercero = "",
                usuarioAnulado = null, solicitante = "", docReferencia = "", terceroSucursal = null, tipo = "";
            bool verificacion = true, anulado = false;
            decimal cantidadAprobada = 0; bool vCcosto = false;

            if (gvLista.Rows.Count == 0 && Convert.ToBoolean(TipoTransaccionConfig(1)) == false && Convert.ToBoolean(TipoTransaccionConfig(12)) == false)
            {
                ManejoError("Detalle de la transacción vacio. No es posible registrar la transacción", "I");
                return;
            }

            if (ddlOC.Visible & ddlOC.SelectedValue.Trim().Length > 0)
            {
                if (Convert.ToDateTime(txtFecha.Text) < Convert.ToDateTime(txtFechaReferencia.Text))
                {
                    ManejoError("La fecha del documento no puede ser menor a la de referencia", "I");
                    return;
                }
            }

            if (Convert.ToString(this.ddlTipoDocumento.SelectedValue).Trim().Length == 0)
            {
                ManejoError("Por favor seleccione un tipo de movimiento. No es posible registrar la transacción", "I");
                return;
            }
            if (this.txtNumero.Text.Trim().Length == 0)
            {
                ManejoError("El número de transacción no puede estar vacio. No es posible registrar la transacción", "I");
                return;
            }
            if (Convert.ToString(this.niCalendarFecha.SelectedDate).Trim().Length == 0)
            {
                ManejoError("Debe seleccionar una fecha. No es posible registrar la transacción", "I");
                return;
            }
            if (this.ddlSucursal.Enabled == true)
            {
                if (Convert.ToString(this.ddlSucursal.SelectedValue).Trim().Length == 0)
                {
                    ManejoError("Debe seleccionar un sucursal para guardar la transacción", "I");
                    return;
                }
            }

            if (this.ddlTercero.Enabled == false)
                tercero = null;
            else
                tercero = Convert.ToString(this.ddlTercero.SelectedValue);

            if (this.ddlSucursal.Enabled == false)
                terceroSucursal = null;
            else
                terceroSucursal = Convert.ToString(this.ddlSucursal.SelectedValue);

            if (txtDocref.Visible != false)
            {
                docReferencia = Convert.ToString(txtDocref.Text);
            }
            else if (ddlOC.Visible == true)
            {
                docReferencia = ddlOC.SelectedValue.Trim();
            }
            else
            {
                docReferencia = null;
            }

            foreach (GridViewRow cuerpo in this.gvLista.Rows)
            {
                if (((DropDownList)(cuerpo.Cells[5].FindControl("ddlBodega"))).SelectedValue.Trim().Length == 0)
                {
                    vCcosto = true;
                    break;
                }

            }

            if (vCcosto)
            {
                ManejoError("Debe seleccionar todas las bodegas para guardar la transacción", "I");
                return;
            }


            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    tipo = ddlTipoDocumento.SelectedValue;
                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                    {
                        this.txtNumero.Enabled = false;
                        numero = this.txtNumero.Text.Trim();
                        object[] objKey = new object[] { (int)this.Session["empresa"], numero, ddlTipoDocumento.SelectedValue };
                        CentidadMetodos.EntidadInsertUpdateDelete("iTransaccionDetalle", "elimina", "ppa", objKey);
                        CentidadMetodos.EntidadInsertUpdateDelete("iTransaccion", "elimina", "ppa", objKey);

                    }
                    else
                    {
                        if (this.txtNumero.Enabled == false)
                            numero = transacciones.RetornaNumeroTransaccion(Convert.ToString(this.ddlTipoDocumento.SelectedValue), (int)this.Session["empresa"]);
                        else
                            numero = this.txtNumero.Text.Trim();
                    }



                    object[] objValores = new object[]{
                                    Convert.ToDateTime(txtFecha.Text).Year,//@año
                                    false,//@anulado
                                    false,//@aprobado
                                    false,//@cerrado
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
                                    docReferencia,//@referencia
                                    null,//@signo
                                    null,//@solicitante
                                    terceroSucursal,//@surcursalTercero
                                    tercero,//@tercero
                                    ddlTipoDocumento.SelectedValue,//@tipo
                                    Convert.ToDecimal(nitxtTotalDescuento.Text),//@totalDescuento
                                    Convert.ToDecimal(nitxtTotalImpuesto.Text),//@totalImpuesto
                                    Convert.ToDecimal(nitxtTotal.Text),//@totalNeto
                                    Convert.ToDecimal(nitxtTotalValorBruto.Text),//@totalValorBruto
                                    this.Session["usuario"].ToString(),//@usuario
                                    null,//@usuarioAbrobado
                                    null,//@usuarioAnulado
                                    0//@vigencia
                                };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("iTransaccion", "inserta", "ppa", objValores))
                    {
                        case 0:

                            foreach (GridViewRow cuerpo in this.gvLista.Rows)
                            {
                                string bodega = null, ccosto = null, destino = null;

                                if (Convert.ToBoolean(TipoTransaccionConfig(14)) == true)
                                    cantidadAprobada = Convert.ToDecimal(cuerpo.Cells[6].Text);
                                else
                                    cantidadAprobada = 0;

                                if (((DropDownList)(cuerpo.Cells[5].FindControl("ddlBodega"))).SelectedValue.Trim().Length > 0)
                                    bodega = ((DropDownList)(cuerpo.Cells[5].FindControl("ddlBodega"))).SelectedValue.Trim();

                                if (Server.HtmlDecode(cuerpo.Cells[10].Text).Trim().Length > 0)
                                    ccosto = Server.HtmlDecode(cuerpo.Cells[10].Text);
                                if (Server.HtmlDecode(cuerpo.Cells[9].Text).Trim().Length > 0)
                                    destino = Server.HtmlDecode(cuerpo.Cells[9].Text);


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
                                    false,//@anulado
                                                      false,//@aprobado
                                                      bodega,//@bodega
                                                      Convert.ToDecimal(cuerpo.Cells[6].Text),//@cantidad
                                                      Convert.ToDecimal(cuerpo.Cells[6].Text),//@cantidadRequerida
                                                      ccosto,//@ccosto
                                                      false,//@cerrado
                                                      null,
                                                      destino,//@destino
                                                      Server.HtmlDecode(cuerpo.Cells[3].Text),//@detalle
                                                      false,//@ejecutado
                                                      Convert.ToInt16(Session["empresa"]),//@empresa
                                                      null,//@fechaAnulado
                                                      null,//@fechaAprobado
                                                      null,//@fechaCierre
                                                      Convert.ToInt16(cuerpo.Cells[2].Text),//@item
                                                      Convert.ToDateTime(txtFecha.Text).Month,//@mes
                                                      numero,//@numero
                                                      docReferencia,//@numeroReferencia
                                                      0,//@pDescuento
                                                      periodo,//@periodo
                                                      0,
                                                      Convert.ToInt16(cuerpo.Cells[12].Text),//@registro
                                                      Convert.ToDecimal(cuerpo.Cells[6].Text),//@saldo
                                                      ddlTipoDocumento.SelectedValue,//@tipo
                                                      null,//@tipoReferencia
                                                      Server.HtmlDecode(cuerpo.Cells[4].Text),//@uMedida
                                                      usuarioAnulado,//@usuarioAnulado
                                                      null,//@usuarioAprobado
                                                      null,//@usuarioCierre
                                                      Convert.ToDecimal(cuerpo.Cells[8].Text),
                                                      0,//@valorDescuento
                                                      0,
                                                      Convert.ToDecimal(cuerpo.Cells[8].Text),
                                                      0,//@valorSaldo
                                                      Convert.ToDecimal(cuerpo.Cells[8].Text),//@valorTotal
                                                      0,
                                                      Convert.ToDecimal(cuerpo.Cells[7].Text),//@valorUnitario
                                                      0
                                                };

                                switch (CentidadMetodos.EntidadInsertUpdateDelete("iTransaccionDetalle", "inserta", "ppa", objValoresCuerpo))
                                {
                                    case 1:
                                        verificacion = false;
                                        break;
                                }
                            }

                            if (gvImpuesto.Rows.Count > 0)
                            {
                                decimal valorBruto = 0;
                                valorBruto = Convert.ToDecimal(nitxtTotalValorBruto.Text) - Convert.ToDecimal(nitxtTotalDescuento.Text);
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

                                    switch (CentidadMetodos.EntidadInsertUpdateDelete("iTransaccionImpuesto", "inserta", "ppa", objValoresCuerpo))
                                    {
                                        case 1:
                                            verificacion = true;
                                            break;
                                    }

                                }
                            }

                            if (verificacion == true)
                            {
                                if (this.txtNumero.Enabled == false && Convert.ToBoolean(this.Session["editar"]) == false)
                                {
                                    if (tipoTransaccion.ActualizaConsecutivo(Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"])) != 0)
                                    {
                                        ManejoError("Error al actualizar el consecutivo. Operación no realizada", "I");
                                        return;
                                    }
                                }

                                ManejoExito("Transacción registrada satisfactoriamente. Transacción número " + numero, "A");


                                ts.Complete();
                                ImprimriTransaccion(Convert.ToString(this.Session["empresa"]), tipo, numero);
                            }
                            else
                            {
                                ManejoError("Error al insertar detalle de transacción. Operación no realizada", "I");
                            }
                            break;

                        case 1:

                            ManejoError("Error al insertar la transacción. Operación no realizada", "I");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ManejoError("Error al registrar la transacción. Correspondiente a: " + ex.Message, "I");
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
                ManejoError("formato de fecha no valido..", "I");
                return;
            }

            if (Convert.ToBoolean(this.Session["editar"]) == true)
            {
                //if (Convert.ToString(Convert.ToDateTime(txtFecha.Text).Year) + Convert.ToString(Convert.ToDateTime(txtFecha.Text).Month).PadLeft(2, '0') != this.Session["periodo"].ToString())
                //    ManejoError("La fecha debe corresponder al periodo actual de la transacción", "A");
            }
            else
            {
                if (CompruebaTransaccionExistente() == 1)
                {
                    ManejoError("Transacción existente. Por favor corrija", "I");
                    return;
                }


                //CcontrolesUsuario.ComportamientoCampoEntidad(this.upCabeza.Controls, "iTransaccionDetalle", Convert.ToString(this.ddlTipoDocumento.SelectedValue), (int)this.Session["empresa"]);
                CargarCentroCosto();
                CargaDestinos();
                cargarBodega();

                this.txtObservacion.Focus();
                this.btnRegistrar.Visible = true;
                if (Convert.ToBoolean(TipoTransaccionConfig(1)) == true && Convert.ToBoolean(TipoTransaccionConfig(12)) == false)
                    this.btnRegistrar.Visible = false;
            }
            //}
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
            CerroresGeneral.ManejoError(this, GetType(), error, "error");
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion,
                ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "er",
                error, ip.ObtenerIP(), Convert.ToInt16(Session["empresa"]));
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
            this.gvImpuesto.DataSource = null;
            this.gvImpuesto.DataBind();
            gvImpuesto.Visible = false;
            this.Session["impuesto"] = null;
            this.Session["transaccion"] = null;
            this.lbRegistrar.Visible = false;
            nitxtTotalDescuento.Visible = false;
            nitxtTotal.Visible = false;
            nitxtTotalImpuesto.Visible = false;
            nitxtTotalValorBruto.Visible = false;
            nilblValorTotal.Visible = false;
            nilblValorTotal0.Visible = false;
            nilblValorTotal1.Visible = false;
            nilblValorTotal2.Visible = false;
            lbFecha.Visible = false;
            Session["rangos"] = null;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                        mensaje, ip.ObtenerIP(), Convert.ToInt16(Session["empresa"]));
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
        private void CargaCampos()
        {
            try
            {
                this.niddlCampo.DataSource = transacciones.GetCamposEA(Convert.ToInt32(this.Session["empresa"]));
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
        private void CargarCombos()
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
                this.ddlTipoDocumento.DataSource = tipoTransaccion.GetTipoTransaccionModuloForma(Convert.ToInt16(Session["empresa"]), 11);
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

                if (CentidadMetodos.EntidadGetKey("iTransaccion", "ppa", objkey).Tables[0].DefaultView.Count > 0)
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
                        ManejoError("Transacción existente. Por favor corrija", "I");
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
            CcontrolesUsuario.ComportamientoCampoEntidad(this.upCabeza.Controls, "iTransaccion", Convert.ToString(this.ddlTipoDocumento.SelectedValue), (int)this.Session["empresa"]);
            CcontrolesUsuario.ComportamientoCampoEntidad(this.upDetalle.Controls, "iTransaccionDetalle", Convert.ToString(this.ddlTipoDocumento.SelectedValue), (int)this.Session["empresa"]);

            if (Convert.ToBoolean(TipoTransaccionConfig(15)) == false)
                this.btnRegistrar.Visible = false;
            else
                this.btnRegistrar.Visible = true;
        }

        private void CargaProductos()
        {
            try
            {
                DataView dvProducto = CentidadMetodos.EntidadGet("iItems", "ppa").Tables[0].DefaultView;

                if (Convert.ToBoolean(TipoTransaccionConfig(14)) == false)
                {

                    dvProducto.RowFilter = " empresa=" + Session["empresa"].ToString() + " and tipo in ('S') and empresa=" + Convert.ToString(Session["empresa"]);
                    dvProducto.Sort = "descripcion";
                    this.ddlProducto.DataSource = dvProducto;
                    this.ddlProducto.DataValueField = "codigo";
                    this.ddlProducto.DataTextField = "cadena";
                    this.ddlProducto.DataBind();
                    this.ddlProducto.Items.Insert(0, new ListItem("", ""));
                    this.ddlProducto.SelectedValue = "";

                }
                else
                {
                    dvProducto.RowFilter = " empresa=" + Session["empresa"].ToString() + " and tipo in ('I') and empresa=" + Convert.ToString(Session["empresa"]);
                    dvProducto.Sort = "descripcion";
                    this.ddlProducto.DataSource = dvProducto;
                    this.ddlProducto.DataValueField = "codigo";
                    this.ddlProducto.DataTextField = "cadena";
                    this.ddlProducto.DataBind();
                    this.ddlProducto.Items.Insert(0, new ListItem("", ""));
                    this.ddlProducto.SelectedValue = "";
                }


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
                        Session["editarDetalle"] = false;
                        cancelarTransaccion();
                        TabRegistro();
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
            this.Session["transaccion"] = null;
            this.Session["operadores"] = null;
            Session["editarDetalle"] = false;
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
                        ManejoError("No se puede realizar este tipo movimiento el día de hoy", "I");
                        this.niCalendarFecha.Visible = false;
                        return;
                    }

                }

                ComportamientoConsecutivo();
                ComportamientoTransaccion();
                CargaProductos();
                CargaDestinos();
                CargarCentroCosto();
                cargarBodega();
                cargarProveedores();
                txtFechaReferencia.Enabled = false;

            }
            catch (Exception ex)
            {
                ManejoError("Error al comprobar transacción con referencia. Correspondiente a: " + ex.Message, "C");
            }
        }

        private void cargarProveedores()
        {
            try
            {

                DataView dvTercero = tercero.getProveedores();
                EnumerableRowCollection<DataRow> query = from ter in dvTercero.Table.AsEnumerable()
                                                         where ter.Field<int>("empresa") == Convert.ToInt16(this.Session["empresa"])
                                                         && ter.Field<bool>("proveedor") == true
                                                         select ter;
                DataView dvT = query.AsDataView();

                ddlTercero.DataSource = dvT;
                this.ddlTercero.DataValueField = "id";
                this.ddlTercero.DataTextField = "cadena";
                this.ddlTercero.DataBind();
                this.ddlTercero.Items.Insert(0, new ListItem("", ""));

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar terceros debido a: " + ex.Message, "I");
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
                    ManejoError("Saldo inferior a la cantidad solicitada. Por favor corrija", "I");
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


        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            Guardar();
        }


        protected void txtProducto_TextChanged(object sender, EventArgs e)
        {
            try
            {

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
                    List<CtransaccionAlmacen> listaTransaccion = null;
                    listaTransaccion = (List<CtransaccionAlmacen>)Session["transaccion"];
                    int reg = Convert.ToInt16(gvLista.Rows[e.RowIndex].Cells[12].Text);
                    listaTransaccion.RemoveAll(x => x.Registro == reg);
                    this.gvLista.DataSource = listaTransaccion;
                    this.gvLista.DataBind();
                    Session["transaccion"] = listaTransaccion;
                    if (listaTransaccion.Count == 0)
                    {
                        gvImpuesto.DataSource = null;
                        gvImpuesto.DataBind();
                    }
                    else
                    {
                        hdRegistro.Value = Convert.ToString(listaTransaccion.Max(x => x.Registro) + 1);
                        bodegaSeleccionada(listaTransaccion);
                    }
                    TotalizaGrillaReferencia();



                }
                catch (Exception ex)
                {
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "I");
                }
            }
            else
            {
                foreach (Control c in this.gvLista.Rows[e.RowIndex].Cells[11].Controls)
                {
                    if (c is CheckBox)
                        anulado = ((CheckBox)c).Checked;
                }
                if (anulado == true)
                {
                    ManejoError("Registro anulado no es posible volver anular", "I");
                    return;
                }

                foreach (Control c in this.gvLista.Rows[e.RowIndex].Cells[11].Controls)
                {
                    if (c is CheckBox)
                        ((CheckBox)c).Checked = true;
                }
                this.gvLista.Rows[e.RowIndex].BackColor = System.Drawing.Color.Red;
            }
            inhabilitarItemsOC();



        }
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool anulado = false;
                var hfOc = gvLista.SelectedRow.FindControl("hfOc") as HiddenField;
                var hfCantidadOC = gvLista.SelectedRow.FindControl("hfCantidadOC") as HiddenField;

                if (Convert.ToBoolean(Session["editarDetalle"]) == false)
                    Session["editarDetalle"] = true;
                else
                {
                    ManejoError("Debe de agregar el registro seleccionado para continuar", "I");
                    return;
                }
                foreach (Control c in this.gvLista.SelectedRow.Cells[11].Controls)
                {
                    if (c is CheckBox)
                        anulado = ((CheckBox)c).Checked;
                }
                if (anulado == true)
                {
                    Session["editarDetalle"] = false;
                    ManejoError("Registro anulado no es posible su edición", "I");
                    return;
                }
                this.hdCantidad.Value = this.gvLista.SelectedRow.Cells[6].Text;
                this.hdRegistro.Value = this.gvLista.SelectedRow.Cells[12].Text;
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

                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                {
                    txvCantidad.Text = this.gvLista.SelectedRow.Cells[6].Text;
                    this.Session["cant"] = Convert.ToDecimal(this.gvLista.SelectedRow.Cells[6].Text);
                }
                else
                    txvCantidad.Text = "0";

                if (this.gvLista.SelectedRow.Cells[7].Text != "&nbsp;")
                {
                    txvValorUnitario.Text = this.gvLista.SelectedRow.Cells[7].Text;
                    this.Session["valor"] = Convert.ToDecimal(this.gvLista.SelectedRow.Cells[7].Text);
                }
                else
                    txvValorUnitario.Text = "0";

                if (this.gvLista.SelectedRow.Cells[8].Text != "&nbsp;")
                {
                    txvValorTotal.Text = this.gvLista.SelectedRow.Cells[8].Text;
                }
                else
                    txvValorTotal.Text = "0";


                if (this.gvLista.SelectedRow.Cells[10].Text != "&nbsp;")
                    this.ddlCcosto.SelectedValue = Convert.ToString(this.gvLista.SelectedRow.Cells[10].Text);
                else
                    this.ddlCcosto.SelectedValue = "";

                if (this.gvLista.SelectedRow.Cells[9].Text != "&nbsp;")
                    this.ddlDestino.SelectedValue = Convert.ToString(this.gvLista.SelectedRow.Cells[9].Text);
                else
                    this.ddlDestino.SelectedValue = "";

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                    this.ddlUmedida.SelectedValue = Convert.ToString(this.gvLista.SelectedRow.Cells[4].Text);
                else
                    this.ddlUmedida.SelectedValue = "";

                if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                    this.ddlBodega.SelectedValue = Convert.ToString(this.gvLista.SelectedRow.Cells[5].Text);
                else
                    this.ddlBodega.SelectedValue = "";

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    this.txtDetalle.Text = Server.HtmlDecode(Convert.ToString(this.gvLista.SelectedRow.Cells[3].Text));
                else
                    this.txtDetalle.Text = "";

                hfCantidadOCR.Value = hfCantidadOC.Value;

                List<CtransaccionAlmacen> listaTransaccion = null;
                listaTransaccion = (List<CtransaccionAlmacen>)this.Session["transaccion"];
                listaTransaccion.RemoveAt(this.gvLista.SelectedRow.RowIndex);
                this.gvLista.DataSource = listaTransaccion;
                this.gvLista.DataBind();
                bodegaSeleccionada(listaTransaccion);

                if (Convert.ToBoolean(hfOc.Value))
                {
                    txvValorUnitario.Enabled = false;
                    ddlCcosto.Enabled = false;
                    ddlDestino.Enabled = false;
                    ddlProducto.Enabled = false;
                    ddlUmedida.Enabled = false;
                    hfocd.Value = hfOc.Value;
                }
                else
                {
                    txvValorUnitario.Enabled = true;
                    ddlCcosto.Enabled = true;
                    ddlDestino.Enabled = true;
                    ddlProducto.Enabled = true;
                    ddlUmedida.Enabled = true;

                }
                inhabilitarItemsOC();

            }
            catch (Exception ex)
            {
                Session["editarDetalle"] = false;
                ManejoError("Error al cargar los campos del registro en el formulario. Correspondiente a: " + ex.Message, "I");
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
            foreach (GridViewRow registro in this.gvLista.Rows)
            {
                validaImpuesto = false;
                DataView dvImpuesto = tipoTransaccion.SeleccionaImpuestoItemTercero(Convert.ToInt16(Session["empresa"]), ddlSucursal.SelectedValue, Convert.ToInt16(ddlTercero.SelectedValue), Convert.ToInt16(registro.Cells[2].Text));
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
                            0);

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

        }

        private void TotalizaGrillaReferencia()
        {
            try
            {
                nitxtTotalDescuento.Visible = true;
                nitxtTotal.Visible = true;
                nitxtTotalImpuesto.Visible = true;
                nitxtTotalValorBruto.Visible = true;
                nilblValorTotal.Visible = true;
                nilblValorTotal0.Visible = true;
                nilblValorTotal1.Visible = true;
                nilblValorTotal2.Visible = true;

                this.nitxtTotalValorBruto.Text = "0";
                nitxtTotalDescuento.Text = "0";
                nitxtTotal.Text = "0";

                foreach (GridViewRow registro in this.gvLista.Rows)
                {
                    this.nitxtTotalValorBruto.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(registro.Cells[8].Text) + Convert.ToDecimal(this.nitxtTotalValorBruto.Text)).ToString("N2"));
                    //  this.nitxtTotalDescuento.Text = Convert.ToString(Convert.ToDecimal(registro.Cells[8].Text) + Convert.ToDecimal(this.nitxtTotalDescuento.Text));

                }
                nitxtTotalImpuesto.Text = "0";
                decimal valorBruto = 0;
                valorBruto = Convert.ToDecimal(nitxtTotalValorBruto.Text) - Convert.ToDecimal(nitxtTotalDescuento.Text);
                foreach (GridViewRow registro in this.gvImpuesto.Rows)
                {
                    registro.Cells[5].Text = Convert.ToString(Convert.ToDecimal((valorBruto * Convert.ToDecimal(registro.Cells[3].Text) / 100) * (Convert.ToDecimal(registro.Cells[2].Text) / 100)).ToString("N2"));
                    this.nitxtTotalImpuesto.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(registro.Cells[5].Text) + Convert.ToDecimal(this.nitxtTotalImpuesto.Text)).ToString("N2"));
                }

                this.nitxtTotal.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(nitxtTotalImpuesto.Text) + Convert.ToDecimal(nitxtTotalValorBruto.Text) - Convert.ToDecimal(this.nitxtTotalDescuento.Text)).ToString("N2"));
            }
            catch (Exception ex)
            {
                ManejoError("Error al totalizar la grilla de referencia. Correspondiente a: " + ex.Message, "C");
            }
        }

        protected void gvTransaccion_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            string vtn = "window.open('Detalle.aspx?numero=" + gvTransaccion.Rows[e.RowIndex].Cells[2].Text.Trim() + "&tipo=" + gvTransaccion.Rows[e.RowIndex].Cells[1].Text.Trim() + "','Detalle de transacción','scrollbars=yes,resizable=yes','height=200', 'width=300')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
        }
        protected void gvTransaccion_OnRowCommand(object sender, GridViewCommandEventArgs e)
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

        protected void gvTransaccion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.nilblMensajeEdicion.Text = "";

            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    if (transacciones.VerificaEdicionBorrado(this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, Convert.ToInt16(Session["empresa"])) != 0)
                    {
                        this.nilblMensajeEdicion.Text = "Transacción ejecutada no es posible su edición";
                        return;
                    }

                    if (tipoTransaccion.RetornaTipoBorrado(this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text, Convert.ToInt16(Session["empresa"])) == "E")
                    {
                        object[] objValores = new object[] { Convert.ToInt16(Session["empresa"]), Convert.ToString(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text).Trim(), Convert.ToString(this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text).Trim() };

                        switch (CentidadMetodos.EntidadInsertUpdateDelete("iTransaccionDetalle", "elimina", "ppa", objValores))
                        {
                            case 0:
                                switch (CentidadMetodos.EntidadInsertUpdateDelete("iTransaccion", "elimina", "ppa", objValores))
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
                        switch (transacciones.AnulaTransaccion(Convert.ToInt16(Session["empresa"]), this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, this.Session["usuario"].ToString().Trim()))
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
        protected void niimbAdicionar_Click(object sender, EventArgs e)
        {
            if (this.nitxtValor1.Text.Length == 0 && Convert.ToString(this.niddlCampo.SelectedValue).Length == 0)
            {
                nilblMensajeEdicion.Text = "Debe seleccionar un campo y un valor antes de filtrar, por favor corrija.";
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
            this.nilblMensajeEdicion.Text = "";
            EstadoInicialGrillaTransacciones();
            imbBusqueda.Focus();
        }
        protected void imbBusqueda_Click(object sender, EventArgs e)
        {
            this.nilblMensajeEdicion.Text = "";

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
                bool oc = false; decimal valorTotal = 0, valorUnitario = 0;


                if (Convert.ToBoolean(TipoTransaccionConfig(14)) == false)
                {
                    if (ddlOC.SelectedValue.Trim().Length == 0)
                    {
                        ManejoError("No se pueden realizar entrada directa el tipo de transacción no esta configurado", "I");
                        return;
                    }
                }

                if (hfocd.Value != "&nbsp;" & hfocd.Value != "")
                {
                    oc = Convert.ToBoolean(hfocd.Value);

                    if (hfCantidadOCR.Value != "&nbsp;" & oc == true)
                    {
                        if (Convert.ToDecimal(hfCantidadOCR.Value) < Convert.ToDecimal(txvCantidad.Text))
                        {
                            ManejoError("La cantidad ingresada no puede ser mayor a la de la orden de compra", "I");
                            return;
                        }
                    }
                }




                foreach (GridViewRow registro in this.gvLista.Rows)
                {
                    if (Convert.ToString(this.ddlProducto.SelectedValue) == registro.Cells[3].Text.Trim())
                    {
                        ManejoError("El producto seleccionado ya se encuentra registrado. Por favor corrija", "I");
                        return;
                    }
                }

                if (Convert.ToString(this.ddlTipoDocumento.SelectedValue).Trim().Length == 0 ||
                    this.txtNumero.Text.Trim().Length == 0)
                {
                    ManejoError("Debe ingresar tipo y número de transacción", "I");
                    return;
                }

                //if (CcontrolesUsuario.VerificaCamposRequeridos(this.upDetalle.Controls) == false)
                //{
                //    ManejoError("Campos vacios. Por favor corrija", "I");
                //    return;
                //}

                if (Convert.ToDecimal(txvCantidad.Text) <= 0)
                {
                    ManejoError("La cantidad no puede ser igual o menor que cero. Por favor corrija", "I");
                    return;
                }


                if (ddlTercero.SelectedValue.Length == 0 || ddlSucursal.SelectedValue.Length == 0)
                {
                    ManejoError("Debe seleccionar proveedor y sucursal, antes de cargar datos", "I");
                    return;
                }

                if (txvValorTotal.Visible)
                {
                    valorTotal = Math.Truncate(Convert.ToDecimal(txvValorTotal.Text) * 100) / 100;
                    valorUnitario = Math.Truncate(Convert.ToDecimal(txvValorTotal.Text) / Convert.ToDecimal(txvCantidad.Text) * 100) / 100; ;
                }
                else
                {
                    valorTotal = Math.Truncate(Convert.ToDecimal(txvValorUnitario.Text) * Convert.ToDecimal(txvCantidad.Text) * 100) / 100;
                    valorUnitario = Math.Truncate(Convert.ToDecimal(txvValorUnitario.Text) * 100) / 100;
                }

                transaccionAlmacen = new CtransaccionAlmacen(
                    Convert.ToString(this.ddlBodega.SelectedValue),
                    Convert.ToString(this.ddlProducto.SelectedValue),
                    Math.Truncate(Convert.ToDecimal(txvCantidad.Text) * 100) / 100,
                    Convert.ToString(this.ddlUmedida.SelectedValue),
                    valorUnitario,
                    Convert.ToString(this.ddlDestino.SelectedValue),
                    Convert.ToString(this.ddlCcosto.SelectedValue),
                    valorTotal,
                    Server.HtmlDecode(this.txtDetalle.Text.Trim()),
                    Convert.ToInt16(this.hdRegistro.Value),
                    Convert.ToInt16(Session["empresa"]),
                    false,
                    oc,
                    txtDetalle.Text
                    );

                List<CtransaccionAlmacen> listaTransaccion = null;

                if (this.Session["transaccion"] == null)
                {
                    listaTransaccion = new List<CtransaccionAlmacen>();
                    listaTransaccion.Add(transaccionAlmacen);
                }
                else
                {
                    listaTransaccion = (List<CtransaccionAlmacen>)Session["transaccion"];
                    listaTransaccion.Add(transaccionAlmacen);
                }

                if (hfocd.Value != "&nbsp;" & hfocd.Value != "")
                    listaTransaccion.First(r => r.Registro == Convert.ToInt16(this.hdRegistro.Value)).Saldo = Convert.ToDecimal(hfCantidadOCR.Value);

                int registror = listaTransaccion.Max(r => r.Registro) + 1;
                hdRegistro.Value = registror.ToString();
                listaTransaccion = listaTransaccion.OrderBy(r => r.Registro).ToList();


                this.Session["transaccion"] = listaTransaccion;
                this.gvLista.DataSource = listaTransaccion;
                this.gvLista.DataBind();
                cargarImpuesto();
                this.ddlProducto.Focus();
                CargaProductos();
                TotalizaGrillaReferencia();
                inhabilitarItemsOC();
                Session["editarDetalle"] = false;
                hfCantidadOCR.Value = "";
                hfocd.Value = "";
                inhabilitarItemsOC();
                bodegaSeleccionada(listaTransaccion);
                txvValorUnitario.Enabled = true;
                ddlCcosto.Enabled = true;
                ddlDestino.Enabled = true;
                ddlProducto.Enabled = true;
                ddlUmedida.Enabled = true;
            }
            catch (Exception ex)
            {

                ManejoError("Error al insertar el registro. Correspondiente a: " + ex.Message, "I");
            }
        }

        protected void ddlTercero_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarSucurlsal(ddlTercero.SelectedValue);
            ddlSucursal.Focus();
        }

        protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {


            cargarOcActivas();

        }


        protected void ddlOC_SelectedIndexChanged(object sender, EventArgs e)
        {
            mfechaReferencia();
            cargarOCDetalle();
        }

        private void mfechaReferencia()
        {
            try
            {
                DataView dvOC = transacciones.SeleccionaOCEncabezadoActivas(Convert.ToInt32(this.Session["empresa"]), ddlSucursal.SelectedValue.Trim(), Convert.ToInt32(ddlTercero.SelectedValue.Trim()), ddlTipoDocumento.SelectedValue.Trim());
                EnumerableRowCollection<DataRow> query = from tipodoc in dvOC.Table.AsEnumerable()
                                                         where tipodoc.Field<string>("numero") == ddlOC.SelectedValue.Trim()
                                                         select tipodoc;
                foreach (DataRowView drv in query.AsDataView())
                {
                    txtFechaReferencia.Text = Convert.ToDateTime(drv.Row.ItemArray.GetValue(2)).ToShortDateString();
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al validar fechas", "I");
            }
        }

        protected void ddlBodega_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var ddlBodega = (DropDownList)sender;
                GridViewRow gvr = (GridViewRow)ddlBodega.Parent.Parent;
                List<CtransaccionAlmacen> listaTransaccion = null;
                listaTransaccion = (List<CtransaccionAlmacen>)Session["transaccion"];
                int re = Convert.ToInt32(gvr.Cells[12].Text);
                listaTransaccion.FirstOrDefault(d => d.Registro == re).Bodega = ddlBodega.SelectedValue.Trim();
                Session["transaccion"] = listaTransaccion;
            }
            catch (Exception ex)
            {
                ManejoError("Error al seleccionar la bodega debido a: " + ex.Message, "I");
            }
        }
        #endregion Eventos



    }
}