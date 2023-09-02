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
    public partial class EntradaSalida : BasePage
    {
        #region Instancias
        //--------------------------------------------------------------------------

        CentidadMetodos CentidadMetodos = new CentidadMetodos();
        Security seguridad = new Security();
        CIP ip = new CIP();
        Cimpuestos transaccionImpuesto = new Cimpuestos();
        Cterceros tercero = new Cterceros();
        cBodega bode = new cBodega();
        public List<CtransaccionAlmacen> ListaTransaccion
        {
            get { return this.Session["transaccion"] != null ? (List<CtransaccionAlmacen>)this.Session["transaccion"] : null; }
            set { Session["transaccion"] = value; }
        }

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
        private void mfechaReferencia()
        {
            try
            {
                DataView dvOC = transacciones.SeleccionaSAEncabezadoActivas(Convert.ToInt32(this.Session["empresa"]));
                EnumerableRowCollection<DataRow> query = from tipodoc in dvOC.Table.AsEnumerable()
                                                         where tipodoc.Field<string>("numero") == ddlSalida.SelectedValue.Trim()
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
        private void mSolicitante()
        {
            try
            {
                cargarSolicitante();
                ddlSolicitante.SelectedValue = transacciones.RetornaSolicitante(ddlSalida.SelectedValue.Trim(), Convert.ToInt32(this.Session["empresa"]));

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar solicitante debido a: " + limpiarMensaje(ex.Message), "I");
            }
        }
        private void cargarSolicitante()
        {
            try
            {
                DataView dvTerceros = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cTercero", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                dvTerceros.RowFilter = "empleado=1";
                ddlSolicitante.DataSource = dvTerceros;
                this.ddlSolicitante.DataValueField = "id";
                this.ddlSolicitante.DataTextField = "descripcion";
                this.ddlSolicitante.DataBind();
                this.ddlSolicitante.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar solicitantes. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
            }
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
                //var ddlBodega = gvr.FindControl("ddlBodega") as DropDownList;
                //DataView dvTipoDocto = bode.BuscarEntidad("", Convert.ToInt16(Session["empresa"]));
                //EnumerableRowCollection<DataRow> query = from tipodoc in dvTipoDocto.Table.AsEnumerable()
                //                                         where tipodoc.Field<int>("empresa") == Convert.ToInt16(Session["empresa"])
                //                                         select tipodoc;

                //ddlBodega.DataSource = query.AsDataView();
                //ddlBodega.DataValueField = "codigo";
                //ddlBodega.DataTextField = "cadena";
                //ddlBodega.DataBind();
                //ddlBodega.Items.Insert(0, new ListItem("", ""));


                if (Convert.ToBoolean(hfoc.Value))
                {
                    //gvr.Cells[0].Enabled = false;
                    gvr.Cells[1].Enabled = false;
                    gvr.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        private void cargarSADetalle()
        {
            try
            {
                DataView dvDetalle = transacciones.SeleccionaSADetalleActivas(Convert.ToInt16(this.Session["empresa"]), ddlSalida.SelectedValue.Trim());

                if (dvDetalle.Count > 0)
                {
                    List<CtransaccionAlmacen> ltransaccion = dvDetalle.Table.AsEnumerable().Select(drv =>
                    new CtransaccionAlmacen
                    {
                        Producto = drv.ItemArray.GetValue(0).ToString(),
                        NombreProducto = drv.ItemArray.GetValue(1).ToString(),
                        Umedida = Server.HtmlDecode(drv.ItemArray.GetValue(2).ToString()),
                        Bodega = drv.ItemArray.GetValue(3).ToString(),
                        Cantidad = 0,
                        ValorUnitario = Convert.ToDecimal(drv.ItemArray.GetValue(5)),
                        ValorTotal = Convert.ToDecimal(drv.ItemArray.GetValue(6)),
                        Destino = drv.ItemArray.GetValue(7).ToString(),
                        Ccosto = drv.ItemArray.GetValue(8).ToString(),
                        Oc = false,
                        Registro = Convert.ToInt32(drv.ItemArray.GetValue(10).ToString()),
                        Saldo = Convert.ToDecimal(drv.ItemArray.GetValue(11)),
                        Nota = drv.ItemArray.GetValue(12).ToString(),
                        NombreDestino = drv.ItemArray.GetValue(13).ToString(),
                        nombreCentroCosto = drv.ItemArray.GetValue(14).ToString(),
                        nombreBodega = drv.ItemArray.GetValue(15).ToString()
                    }
                    ).ToList();
                    ltransaccion.OrderBy(y => y.Registro);
                    ListaTransaccion = ltransaccion;
                    gvLista.DataSource = ListaTransaccion;
                    gvLista.DataBind();
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
                ManejoError("Erro al cargar detalle del requerimiento debido a: " + limpiarMensaje(ex.Message), "I");
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
                ManejoError("Error al cargar sucursal del tercero. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
            }
        }
        private void cargarSalidas()
        {
            try
            {
                DataView dvOC = transacciones.SeleccionaSAEncabezadoActivas(Convert.ToInt32(this.Session["empresa"]));
                this.ddlSalida.DataSource = dvOC;
                this.ddlSalida.DataValueField = "numero";
                this.ddlSalida.DataTextField = "cadena";
                this.ddlSalida.DataBind();
                this.ddlSalida.Items.Insert(0, new ListItem("", ""));
                this.ddlSalida.SelectedValue = "";
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los requerimientos debido  a: " + limpiarMensaje(ex.Message), "I");
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
                    this.gvTransaccion.DataSource = transacciones.GetTransaccionDVA(where, Convert.ToInt16(Session["empresa"]));
                    this.gvTransaccion.DataBind();
                    this.nilblRegistros.Text = "Nro. Registros " + Convert.ToString(this.gvTransaccion.Rows.Count);
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al procesar la consulta de transacciones. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
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
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.lbRegistrar.Visible = false;
            this.lbCancelar.Visible = false;
            // this.lbImprimir.Visible = false;
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
                usuarioAnulado = null, solicitante = "", docReferencia = "", terceroSucursal = null;
            bool verificacion = true, anulado = false, validarCosto = false;
            decimal cantidadAprobada = 0; bool vCcosto = false;

            bool validarCantidades = false;
            decimal vcantidad = 0;

            foreach (GridViewRow gvr in gvLista.Rows)
            {
                var txtCantidadDetalle = gvr.FindControl("txvCantidadDetalle") as TextBox;
                var lblSaldo = gvr.FindControl("hfSaldo") as HiddenField;
                if (Convert.ToDecimal(txtCantidadDetalle.Text) > Convert.ToDecimal(lblSaldo.Value))
                {
                    ManejoError("Las cantidades no pueden ser mayor al saldo ", "I");
                    return;
                }
            }

            foreach (GridViewRow gvr in gvLista.Rows)
            {
                if (Convert.ToDecimal(gvr.Cells[6].Text) == 0)
                {
                    validarCosto = true;
                    break;
                }
            }

            if (ddlSalida.Visible & ddlSalida.SelectedValue.Trim().Length > 0)
            {
                if (Convert.ToDateTime(txtFecha.Text) < Convert.ToDateTime(txtFechaReferencia.Text))
                {
                    ManejoError("La fecha del documento no puede ser menor a la de referencia", "I");
                    return;
                }
            }

            if (validarCosto)
            {
                ManejoError("Hay costos unitarios en 0 por favor verifique", "I");
                return;
            }

            if (gvLista.Rows.Count == 0 && Convert.ToBoolean(TipoTransaccionConfig(1)) == false && Convert.ToBoolean(TipoTransaccionConfig(12)) == false)
            {
                ManejoError("Detalle de la transacción vacio. No es posible registrar la transacción", "I");
                return;
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
            if (this.ddlSucursal.Enabled == true & ddlSucursal.Visible)
            {
                if (Convert.ToString(this.ddlSucursal.SelectedValue).Trim().Length == 0)
                {
                    ManejoError("Debe seleccionar un sucursal para guardar la transacción", "I");
                    return;
                }
            }

            if (this.ddlTercero.Enabled == false & !ddlTercero.Visible)
                tercero = null;
            else
                tercero = Convert.ToString(this.ddlTercero.SelectedValue);

            if (this.ddlSucursal.Enabled == false & !ddlSucursal.Visible)
                terceroSucursal = null;
            else
                terceroSucursal = Convert.ToString(this.ddlSucursal.SelectedValue);

            if (txtDocref.Visible != false)
            {
                docReferencia = Convert.ToString(txtDocref.Text);
            }
            else if (ddlSalida.Visible == true)
            {
                docReferencia = ddlSalida.SelectedValue.Trim();
            }
            else
            {
                docReferencia = null;
            }


            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
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
                                       false,//@anulado
                                         Convert.ToDateTime(txtFecha.Text).Year,//@año
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
                                    ddlSolicitante.SelectedValue.Trim(),//@solicitante
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

                            ListaTransaccion.ForEach(y =>
                            {
                                if (y.Cantidad > 0)
                                {
                                    object[] objValoresCuerpo = new object[]{
                                                        false,//@anulado
                                                             Convert.ToDateTime(txtFecha.Text).Year,//@añok
                                                      false,//@aprobado
                                                      y.Bodega,//@bodega
                                                      y.Cantidad,//@cantidad
                                                      y.Cantidad,//@cantidadRequerida
                                                      y.Ccosto,//@ccosto
                                                      false,//@cerrado
                                                      null, //concepto impuesto
                                                      y.Destino,//@destino
                                                      y.Nota ,//@detalle
                                                      false,//@ejecutado
                                                      Convert.ToInt16(Session["empresa"]),//@empresa
                                                      null,//@fechaAnulado
                                                      null,//@fechaAprobado
                                                      null,//@fechaCierre
                                                      y.Producto,//@item
                                                      Convert.ToDateTime(txtFecha.Text).Month,//@mes
                                                      numero,//@numero
                                                      docReferencia,//@numeroReferencia
                                                      0,//@pDescuento
                                                      periodo,//@periodo
                                                      0, //%impuesto
                                                      y.Registro ,//@registro
                                                      y.Saldo,//@saldo
                                                      null,
                                                      ddlTipoDocumento.SelectedValue,//@tipo
                                                      null,//@tipoReferencia
                                                      y.Umedida,//@uMedida
                                                      usuarioAnulado,//@usuarioAnulado
                                                      null,//@usuarioAprobado
                                                      null,//@usuarioCierre
                                                      y.ValorTotal,//@valorBruto
                                                      0,//@valorDescuento
                                                      0,//valorimpuesto
                                                      y.ValorTotal,//@valorNeto
                                                      0,//@valorSaldo
                                                      y.ValorTotal,//@valorTotal
                                                      0, //valortotalveta
                                                      y.ValorUnitario,//@valorUnitario
                                                      0//valounitarioventa
                                                };

                                    switch (CentidadMetodos.EntidadInsertUpdateDelete("iTransaccionDetalle", "inserta", "ppa", objValoresCuerpo))
                                    {
                                        case 1:
                                            verificacion = false;
                                            break;
                                    }
                                }
                            });


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
                    ManejoError("Error al registrar la transacción. Correspondiente a: " + limpiarMensaje(ex.Message), "I");
                }
            }
        }
        private void validaFecha()
        {
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

            if (!Convert.ToBoolean(this.Session["editar"]))
            {
                if (CompruebaTransaccionExistente() == 1)
                {
                    ManejoError("Transacción existente. Por favor corrija", "I");
                    return;
                }

                CargarCentroCosto();
                cargarBodega();

                this.txtObservacion.Focus();

            }
        }
        private void cargarBodega()
        {
            //try
            //{
            //        this.ddlBodega.DataSource = transacciones.SeleccionaBodegaSaldoItem( Convert.ToInt16(this.Session["empresa"]),   )
            //        this.ddlBodega.DataValueField = "codigo";
            //        this.ddlBodega.DataTextField = "descripcion";
            //        this.ddlBodega.DataBind();
            //        this.ddlBodega.Items.Insert(0, new ListItem("", ""));
            //}
            //catch (Exception ex)
            //{
            //    ManejoError("Error al cargar bodegas. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
            //}
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
            //this.lbImprimir.Visible = true;
            nitxtTotalDescuento.Visible = false;
            nitxtTotal.Visible = false;
            nitxtTotalImpuesto.Visible = false;
            nitxtTotalValorBruto.Visible = false;
            nilblValorTotal.Visible = false;
            nilblValorTotal0.Visible = false;
            nilblValorTotal1.Visible = false;
            nilblValorTotal2.Visible = false;
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
                this.niddlCampo.DataSource = transacciones.GetCamposSA(Convert.ToInt32(this.Session["empresa"]));
                this.niddlCampo.DataValueField = "codigo";
                this.niddlCampo.DataTextField = "descripcion";
                this.niddlCampo.DataBind();
                this.niddlCampo.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos para edición. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
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
                this.ddlTipoDocumento.DataSource = tipoTransaccion.GetTipoTransaccionModuloForma(Convert.ToInt16(Session["empresa"]), 15);
                this.ddlTipoDocumento.DataValueField = "codigo";
                this.ddlTipoDocumento.DataTextField = "descripcion";
                this.ddlTipoDocumento.DataBind();
                this.ddlTipoDocumento.Items.Insert(0, new ListItem("", ""));
                this.ddlTipoDocumento.SelectedValue = "";
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tipos de transacción. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
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
            this.lbRegistrar.Visible = true;
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.gvTransaccion.DataSource = null;
            this.gvTransaccion.DataBind();
            this.Session["transaccion"] = null;
            //this.lbImprimir.Visible = false;
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
                ManejoError("Error al obtener el número de transacción. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
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
                ManejoError("Error al recuperar posición de configuración de tipo de transacción. Correspondiente a: " + limpiarMensaje(ex.Message), "C");

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
                ManejoError("Error al comprobar transacción existente. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
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
                // ManejoError("Error al cargar terceros debido a: " + limpiarMensaje(ex.Message), "I");
            }
        }
        private void CargarCentroCosto()
        {
            //try
            //{
            //    DataView dvCcosto = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cCentrosCosto", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
            //    dvCcosto.RowFilter = "empresa=" + Session["empresa"] + "and auxiliar=1";
            //    this.ddlCcosto.DataSource = dvCcosto;
            //    this.ddlCcosto.DataValueField = "codigo";
            //    this.ddlCcosto.DataTextField = "cadena";
            //    this.ddlCcosto.DataBind();
            //    this.ddlCcosto.Items.Insert(0, new ListItem("", ""));
            //}
            //catch (Exception ex)
            //{
            //    ManejoError("Error al cargar centro de costos. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
            //}
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
                this.nitxtTotalValorBruto.Text = ListaTransaccion.Sum(y => y.ValorTotal).ToString("N2");
                nitxtTotalImpuesto.Text = "0";
                decimal valorBruto = 0;
                valorBruto = Convert.ToDecimal(nitxtTotalValorBruto.Text) - Convert.ToDecimal(nitxtTotalDescuento.Text);
                nitxtTotalImpuesto.Text = "0";
                nitxtTotal.Text = Convert.ToString(ListaTransaccion.Sum(y => y.ValorTotal));
            }
            catch (Exception ex)
            {
                ManejoError("Error al totalizar la grilla de referencia. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
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
                this.txtNumero.Text = ConsecutivoTransaccion();
                this.hdTransaccionConfig.Value = CcontrolesUsuario.TipoTransaccionConfig(this.ddlTipoDocumento.SelectedValue, Convert.ToInt16(Session["empresa"]));


                ComportamientoConsecutivo();
                ComportamientoTransaccion();
                CargarCentroCosto();
                cargarBodega();
                cargarProveedores();
                cargarSalidas();
                ddlSolicitante.Enabled = false;
                ddlTercero.Visible = false;
                ddlSucursal.Visible = false;
                lblTercero.Visible = false;
                lblSucursal.Visible = false;
                txtFechaReferencia.Enabled = false;
                txtFecha.Focus();

            }
            catch (Exception ex)
            {
                ManejoError("Error al comprobar transacción con referencia. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
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
            //this.lbImprimir.Visible = false;
            gvParametros.DataSource = null;
            gvParametros.DataBind();
            this.Session["operadores"] = null;
            nitxtValor1.Text = "";
            nitxtValor2.Text = "";

        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            Guardar();
        }
        protected void lbImprimir_Click(object sender, EventArgs e)
        {

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
        protected void gvTransaccion_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            string vtn = "window.open('Detalle.aspx?numero=" + gvTransaccion.Rows[e.RowIndex].Cells[2].Text.Trim() + "&tipo=" + gvTransaccion.Rows[e.RowIndex].Cells[1].Text.Trim() + "','Detalle de transacción','scrollbars=yes,resizable=yes','height=200', 'width=300')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
        }
        protected void gvTransaccion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    if (transacciones.VerificaEdicionBorrado(this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, Convert.ToInt16(Session["empresa"])) != 0)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "Transacción ejecutada no es posible su edición", "w");
                        return;
                    }

                    bool anulado = false;

                    foreach (Control objControl in gvTransaccion.Rows[e.RowIndex].Cells[8].Controls)
                    {
                        anulado = ((CheckBox)objControl).Checked;
                    }

                    if (anulado)
                    {
                        MostrarMensaje("El regitro ya se encuentra anulado...."); return;
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
                        switch (transacciones.AnulaTransaccion(Convert.ToInt16(Session["empresa"]), this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, this.Session["usuario"].ToString().Trim()))
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
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
                }
            }
        }
        protected void niimbAdicionar_Click(object sender, EventArgs e)
        {
            if (this.nitxtValor1.Text.Length == 0 && Convert.ToString(this.niddlCampo.SelectedValue).Length == 0)
            {
                MostrarMensaje("Debe seleccionar un campo y un valor antes de filtrar, por favor corrija.");
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
        protected void txvCantidadDetalle_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var txtCantidadDetalle = sender as TextBox;
                var gvr = txtCantidadDetalle.Parent.Parent as GridViewRow;
                var hfRegistro = gvr.FindControl("hfRegistro") as HiddenField;
                var chkSeleccionado = gvr.FindControl("chkSeleccionar") as CheckBox;

                var resultado = ListaTransaccion.Find(x => x.Registro == Convert.ToInt32(hfRegistro.Value));
                resultado.Cantidad = Convert.ToDecimal(txtCantidadDetalle.Text);
                resultado.ValorTotal = resultado.Cantidad * resultado.ValorUnitario;
                gvr.Cells[7].Text = resultado.ValorTotal.ToString("N2");
                TotalizaGrillaReferencia();
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }
        protected void ddlTercero_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarSucurlsal(ddlTercero.SelectedValue);
            ddlSucursal.Focus();
        }
        protected void ddlSalida_SelectedIndexChanged(object sender, EventArgs e)
        {
            mfechaReferencia();
            cargarSADetalle();

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
                CtransaccionAlmacen item = listaTransaccion.FirstOrDefault(d => d.Registro == re);
                item.Bodega = ddlBodega.SelectedValue.Trim();
                item.ValorUnitario = transacciones.SeleccionaBodegaSaldoItemCosto(Convert.ToInt16(this.Session["empresa"]), gvr.Cells[2].Text.Trim(), ddlBodega.SelectedValue.Trim());
                item.ValorTotal = item.ValorUnitario * item.Cantidad;
                Session["transaccion"] = listaTransaccion;
                gvLista.DataSource = listaTransaccion;
                gvLista.DataBind();
                bodegaSeleccionada(listaTransaccion);
                TotalizaGrillaReferencia();

            }
            catch (Exception ex)
            {
                ManejoError("Error al seleccionar la bodega debido a: " + limpiarMensaje(ex.Message), "I");
            }
        }
        protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //try
            //{
            //    if (e.Row.DataItem != null)
            //    {
            //        CtransaccionAlmacen drv = (CtransaccionAlmacen)e.Row.DataItem;
            //        var ddlBodega = e.Row.FindControl("ddlBodega") as DropDownList;
            //        ddlBodega.DataSource = transacciones.SeleccionaBodegaSaldoItem(Convert.ToInt16(this.Session["empresa"]), Convert.ToInt16(drv.Producto));
            //        ddlBodega.DataValueField = "codigo";
            //        ddlBodega.DataTextField = "cadena";
            //        ddlBodega.DataBind();
            //        ddlBodega.Items.Insert(0, new ListItem("", ""));
            //    }

            //}
            //catch (Exception ex)
            //{
            //    ManejoError("Error al cargar los datos de las bodegas debido a: " + limpiarMensaje(ex.Message), "I");
            //}
        }
        protected void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                var chkselecionar = sender as CheckBox;
                var gvrow = chkselecionar.Parent.Parent;
                var cantidad = gvrow.FindControl("txvCantidadDetalle") as TextBox;
                var hfRegistro = gvrow.FindControl("hfRegistro") as HiddenField;
                cantidad.Enabled = true;
                var hfSaldo = gvrow.FindControl("hfSaldo") as HiddenField;
                cantidad.Text = "0";
                cantidad.Text = Convert.ToDecimal(hfSaldo.Value).ToString("N2");
                var resultado = ListaTransaccion.Find(z => z.Registro.Equals(Convert.ToInt32(hfRegistro.Value)));
                resultado.Cantidad = Convert.ToDecimal(cantidad.Text);

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
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
        protected void nitxtValor1_TextChanged(object sender, EventArgs e)
        {
            this.niimbAdicionar.Focus();
        }

        #endregion Eventos
    }
}