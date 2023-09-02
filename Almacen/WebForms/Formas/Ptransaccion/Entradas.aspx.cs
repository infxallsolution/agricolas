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
    public partial class Entradas : BasePage
    {
        #region Instancias
        //--------------------------------------------------------------------------

        CentidadMetodos CentidadMetodos = new CentidadMetodos();
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
        Cregistros transaccionAlmacen = new Cregistros();
        public List<Cregistros> listaRegistro
        {
            get { return this.Session["entrada"] != null ? (List<Cregistros>)this.Session["entrada"] : null; }
            set { Session["entrada"] = value; }
        }
        string cadena
        {
            get { return this.Session["cadena"] != null ? this.Session["cadena"].ToString() : null; }
            set { Session["cadena"] = value; }
        }


        #endregion Instancias

        #region Metodos

        private void ImprimriTransaccion(string empresa, string tipo, string numero)
        {
            string vtn = "window.open('../Pinformes/ImprimeTransaccion.aspx?empresa=" + empresa + "&tipo=" + tipo + "&numero=" + numero + "','Impresión de Formatos','scrollbars=yes,resizable=yes','height=300', 'width=400')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
        }
        private void bodegaSeleccionada(List<Cregistros> listaRegistro)
        {
            foreach (GridViewRow gvr in gvReferencia.Rows)
            {
                var ddlBodega = gvr.FindControl("ddlBodega") as DropDownList;
                var hfRegistro = gvr.FindControl("hfRegistro") as HiddenField;
                string bodega = listaRegistro.FirstOrDefault(x => x.registro == Convert.ToInt16(hfRegistro.Value)).idBodega;
                if (bodega.Trim().Length > 0)
                    ddlBodega.SelectedValue = bodega;
            }
        }
        private void inhabilitarItemsOC()
        {
            try
            {
                DataView dvTipoDocto = bode.SeleccionaBodegaTipoTransaccion(ddlTipoDocumento.SelectedValue, Convert.ToInt16(Session["empresa"]));
                EnumerableRowCollection<DataRow> query = from tipodoc in dvTipoDocto.Table.AsEnumerable()
                                                         where tipodoc.Field<int>("empresa") == Convert.ToInt16(Session["empresa"]) &
                                                         tipodoc.Field<bool>("produccion") == false
                                                         & tipodoc.Field<bool>("productoTerminado") == false
                                                         & tipodoc.Field<bool>("servicio") == Convert.ToBoolean(TipoTransaccionConfig(34))
                                                         & tipodoc.Field<bool>("mExistencias") == Convert.ToBoolean(TipoTransaccionConfig(4))
                                                         select tipodoc;


                foreach (GridViewRow gvr in gvReferencia.Rows)
                {
                    var hfoc = gvr.FindControl("hfoc") as HiddenField;
                    var ddlBodega = gvr.FindControl("ddlBodega") as DropDownList;

                    ddlBodega.DataSource = query.AsDataView();
                    ddlBodega.DataValueField = "codigo";
                    ddlBodega.DataTextField = "cadena";
                    ddlBodega.DataBind();
                    ddlBodega.Items.Insert(0, new ListItem("", ""));

                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
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

                    listaRegistro = dvDetalle.Table.AsEnumerable().Select(drv =>
                    new Cregistros
                    {
                        idItem = drv.ItemArray.GetValue(0).ToString(),
                        nombreItem = drv.ItemArray.GetValue(1).ToString(),
                        idUmedida = Server.HtmlDecode(drv.ItemArray.GetValue(2).ToString()),
                        idBodega = drv.ItemArray.GetValue(3).ToString(),
                        valorUnitario = Convert.ToDecimal(drv.ItemArray.GetValue(5)),
                        valorTotal = Convert.ToDecimal(drv.ItemArray.GetValue(4)) * Convert.ToDecimal(drv.ItemArray.GetValue(5)),
                        idDestino = drv.ItemArray.GetValue(7).ToString(),
                        idCcosto = drv.ItemArray.GetValue(8).ToString(),
                        oc = true,
                        registro = Convert.ToInt32(drv.ItemArray.GetValue(10).ToString()),
                        saldo = Convert.ToDecimal(drv.ItemArray.GetValue(4)),
                        cantidad = Convert.ToDecimal(drv.ItemArray.GetValue(14)),
                        check = true,
                        tipoReferencia = drv.ItemArray.GetValue(13).ToString(),
                        numeroReferencia = drv.ItemArray.GetValue(11).ToString()
                    }
                    ).ToList();

                    gvReferencia.DataSource = listaRegistro;
                    gvReferencia.DataBind();
                    inhabilitarItemsOC();
                    TotalizaGrillaReferencia();
                }


                foreach (GridViewRow gvr in gvReferencia.Rows)
                {
                    ((TextBox)gvr.FindControl("txtCantidad")).Enabled = Convert.ToBoolean(TipoTransaccionConfig(9));
                    ((TextBox)gvr.FindControl("txtValorUnitario")).Enabled = Convert.ToBoolean(TipoTransaccionConfig(10));
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
                                                             tipodoc.Field<string>("idTercero") == tercero
                                                             select tipodoc;

                    this.ddlSucursal.DataSource = query.AsDataView();
                    this.ddlSucursal.DataValueField = "codigo";
                    this.ddlSucursal.DataTextField = "cadena";
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
        }
        private void cancelarTransaccion()
        {
            InHabilitaEncabezado();
            CcontrolesUsuario.InhabilitarControles(this.upCabeza.Controls);
            CcontrolesUsuario.LimpiarControles(this.upCabeza.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upDetalle.Controls);
            CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);


            this.gvReferencia.DataSource = null;
            this.gvReferencia.DataBind();
            this.lbRegistrar.Visible = false;
            this.lbCancelar.Visible = false;
            upDetalle.Visible = false;
            upCabeza.Visible = false;
            upConsulta.Visible = false;
            this.Session["operadores"] = null;
            Session["editarDetalle"] = false;
            this.Session["impuestos"] = null;
            listaRegistro = null;
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

            if (gvReferencia.Rows.Count == 0 && Convert.ToBoolean(TipoTransaccionConfig(1)) == false && Convert.ToBoolean(TipoTransaccionConfig(12)) == false)
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
            if (this.txtFecha.Text.Trim().Length == 0)
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
                docReferencia = ddlOC.SelectedValue.Trim();
            else
                docReferencia = null;

            foreach (GridViewRow cuerpo in this.gvReferencia.Rows)
            {
                if (((DropDownList)(cuerpo.Cells[5].FindControl("ddlBodega"))).SelectedValue.Trim().Length == 0 && ((CheckBox)(cuerpo.FindControl("chkSeleccion"))).Checked)
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
                     false,//@anuladok
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
                                    null,//@solicitante
                                    terceroSucursal,//@surcursalTercero
                                    tercero,//@tercero
                                    ddlTipoDocumento.SelectedValue,//@tipo
                                    0,//@totalDescuento
                                    0,//@totalImpuesto
                                    Convert.ToDecimal(nitxtTotal.Text),//@totalNeto
                                    Convert.ToDecimal(nitxtTotal.Text),//@totalNeto
                                    this.Session["usuario"].ToString(),//@usuario
                                    null,//@usuarioAbrobado
                                    null,//@usuarioAnulado
                                    0//@vigencia
                                };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("iTransaccion", "inserta", "ppa", objValores))
                    {
                        case 0:
                            var seleccion = listaRegistro.Where(h => h.check).ToList();

                            int i = 0;
                            seleccion.ForEach(cuerpo =>
                            {

                                object[] objValoresCuerpo = new object[]{

                                    false,//@anulado
                                     Convert.ToDateTime(txtFecha.Text).Year,//@año
                                                      false,//@aprobado
                                                      cuerpo.idBodega,//@bodega
                                                      cuerpo.saldo,//@cantidad
                                                      cuerpo.saldo ,//@cantidadRequerida
                                                      cuerpo.idCcosto,//@ccosto
                                                      false,//@cerrado
                                                      null,
                                                      cuerpo.idDestino,//@destino
                                                      cuerpo.detalle,//@detalle
                                                      false,//@ejecutado
                                                      Convert.ToInt16(Session["empresa"]),//@empresa
                                                      null,//@fechaAnulado
                                                      null,//@fechaAprobado
                                                      null,//@fechaCierre
                                                      cuerpo.idItem,//@item
                                                      Convert.ToDateTime(txtFecha.Text).Month,//@mes
                                                      numero,//@numero
                                                      cuerpo.numeroReferencia,//@numeroReferencia
                                                      0,//@pDescuento
                                                      periodo,//@periodo
                                                      0,
                                                     i,//@registro
                                                      cuerpo.cantidad ,//@saldo
                                                      cuerpo.idTercero, //@tercero
                                                      ddlTipoDocumento.SelectedValue,//@tipo
                                                      cuerpo.tipoReferencia,//@tipoReferencia
                                                      cuerpo.idUmedida ,//@uMedida
                                                      usuarioAnulado,//@usuarioAnulado
                                                      null,//@usuarioAprobado
                                                      null,//@usuarioCierre
                                                      cuerpo.valorTotal,
                                                      0,//@valorDescuento
                                                      0,
                                                      cuerpo.valorTotal,
                                                      0,//@valorSaldo
                                                      cuerpo.valorTotal,//@valorTotal
                                                      0,
                                                      cuerpo.valorUnitario,//@valorUnitario
                                                      0
                                                };

                                switch (CentidadMetodos.EntidadInsertUpdateDelete("iTransaccionDetalle", "inserta", "ppa", objValoresCuerpo))
                                {
                                    case 0:
                                        i++;
                                        break;
                                    case 1:
                                        verificacion = false;
                                        break;
                                }
                            });


                            switch (transacciones.contabilizarEntradas(tipo: ddlTipoDocumento.SelectedValue, numero: txtNumero.Text, empresa: Convert.ToInt16(Session["empresa"])))
                            {
                                case 2:
                                    verificacion = false;
                                    ManejoError("Debe parametrizar la cuenta para cada tipo de inventario", "I");
                                    return;

                                case 3:
                                    verificacion = false;
                                    ManejoError("Debe parametrizar la cuenta para cada clase de proveedor", "I");
                                    return;

                                case 1:
                                    verificacion = false;
                                    ManejoError("Error al contabilizar la entrada", "I");
                                    return;

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

                this.txtObservacion.Focus();
            }
        }
        protected string nombrePaginaActual()
        {
            string[] arrResult = HttpContext.Current.Request.RawUrl.Split('/');
            string result = arrResult[arrResult.GetUpperBound(0)];
            arrResult = result.Split('?');
            return arrResult[arrResult.GetLowerBound(0)];
        }
        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            CcontrolesUsuario.InhabilitarControles(this.upCabeza.Controls);
            CcontrolesUsuario.LimpiarControles(this.upCabeza.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upDetalle.Controls);
            CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
            InHabilitaEncabezado();
            this.gvReferencia.DataSource = null;
            this.gvReferencia.DataBind();
            this.Session["impuesto"] = null;
            this.Session["transaccion"] = null;
            this.lbRegistrar.Visible = false;
            nitxtTotal.Visible = false;
            nilblValorTotal2.Visible = false;
            Session["rangos"] = null;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                        mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));
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
            this.lbRegistrar.Visible = true;
            this.gvReferencia.DataSource = null;
            this.gvReferencia.DataBind();
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

            char[] comodin = new char[] { '*' };
            int indice = posicion + 1;

            try
            {

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
            CcontrolesUsuario.ComportamientoCampoEntidad(upCabeza.Controls, "iTransaccion", Convert.ToString(this.ddlTipoDocumento.SelectedValue), (int)this.Session["empresa"]);
            CcontrolesUsuario.ComportamientoCampoEntidad(upDetalle.Controls, "iTransaccionDetalle", Convert.ToString(this.ddlTipoDocumento.SelectedValue), (int)this.Session["empresa"]);
            txtFiltroProveedor.Visible = ddlTercero.Visible;
            txtFiltroProveedor.Enabled = ddlTercero.Enabled;
            txtFiltroProveedor.ReadOnly = false;
        }
        private void CargaProductos()
        {
            //try
            //{
            //    DataView dvProducto = CentidadMetodos.EntidadGet("iItems", "ppa").Tables[0].DefaultView;
            //    var resultado = dvProducto.Table.AsEnumerable().Where(y => y.Field<int>("empresa") == Convert.ToInt32(this.Session["empresa"])).Select(x => new { codigo = x.Field<string>("codigo"), descripcion = x.Field<string>("descripcion") });
            //    this.ddlProducto.DataSource = resultado;
            //    this.ddlProducto.DataValueField = "codigo";
            //    this.ddlProducto.DataTextField = "descripcion";
            //    this.ddlProducto.DataBind();
            //    this.ddlProducto.Items.Insert(0, new ListItem("", ""));
            //    this.ddlProducto.SelectedValue = "";
            //}
            //catch (Exception ex)
            //{
            //    ManejoError("Error al cargar productos. Correspondiente a: " + ex.Message, "C");
            //}
        }

        private void CargarCentroCosto()
        {
            //try
            //{
            //    DataView dvCcosto = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cCentrosCosto", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
            //    dvCcosto.RowFilter = "empresa=" + Session["empresa"] + "and auxiliar=1";
            //    this.ddlCcosto.DataSource = dvCcosto;
            //    this.ddlCcosto.DataValueField = "codigo";
            //    this.ddlCcosto.DataTextField = "descripcion";
            //    this.ddlCcosto.DataBind();
            //    this.ddlCcosto.Items.Insert(0, new ListItem("", ""));
            //}
            //catch (Exception ex)
            //{
            //    ManejoError("Error al cargar centro de costos. Correspondiente a: " + ex.Message, "C");
            //}
        }
        private void TotalizaGrillaReferencia()
        {
            nitxtTotal.Visible = true;

            try
            {
                nitxtTotal.Text = "0";
                if (listaRegistro != null || listaRegistro.Count() > 0)
                    this.nitxtTotal.Text = (listaRegistro.Where(y => y.check == true).Sum(y => y.valorTotal)).ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                ManejoError("Error al totalizar la grilla de referencia. Correspondiente a: " + ex.Message, "C");
            }

        }
        private void mfechaReferencia()
        {
            try
            {
                txtFechaReferencia.Text = "";

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
                        this.Session["impuestos"] = null;
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
            this.Session["impuestos"] = null;
            listaRegistro = null;
        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            cancelarTransaccion();
        }
        protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlTipoDocumento.SelectedValue.Length == 0)
                {
                    txtNumero.Text = "";
                    return;
                }


                this.Session["impuestos"] = null;
                this.gvReferencia.DataSource = null;
                this.gvReferencia.DataBind();
                this.Session["transaccion"] = null;
                this.txtNumero.Text = ConsecutivoTransaccion();
                cadena = tipoTransaccion.TipoTransaccionConfig(ddlTipoDocumento.SelectedValue, Convert.ToInt16(Session["empresa"])).ToString();

                if (Convert.ToBoolean(TipoTransaccionConfig(17)) == true)
                {
                    if (tipoTransaccion.RetornavalidacionRegistro(Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"])) == 1)
                    {
                        ManejoError("No se puede realizar este tipo movimiento el día de hoy", "I");
                        return;
                    }

                }

                ComportamientoConsecutivo();
                ComportamientoTransaccion();
                CargarCentroCosto();
                txtFechaReferencia.Enabled = false;


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
            }

            CcontrolesUsuario.HabilitarControles(this.upCabeza.Controls);

            this.nilbNuevo.Visible = false;
            this.txtFecha.Visible = false;
            this.txtFecha.Focus();
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
            this.gvReferencia.DataSource = null;
            this.gvReferencia.DataBind();
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

            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    if (transacciones.VerificaEdicionBorrado(this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, Convert.ToInt16(Session["empresa"])) != 0)
                    {
                        MostrarMensaje("Transacción ejecutada no es posible su edición");
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
                                        CerroresGeneral.ManejoError(this, GetType(), "Registro Eliminado Satisfactoriamente", "info");
                                        BusquedaTransaccion();
                                        ts.Complete();
                                        break;
                                    case 1:
                                        MostrarMensaje("Error al eliminar el registro. Operación no realizada");
                                        break;
                                }
                                break;
                            case 1:
                                MostrarMensaje("Error al eliminar el registro. Operación no realizada");
                                break;
                        }
                    }
                    else
                    {
                        switch (transacciones.AnulaTransaccion(Convert.ToInt16(Session["empresa"]), this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, this.Session["usuario"].ToString().Trim()))
                        {
                            case 0:
                                CerroresGeneral.ManejoError(this, GetType(), "Registro Anulado Satisfactoriamente", "info");
                                BusquedaTransaccion();
                                ts.Complete();
                                break;
                            case 1:
                                MostrarMensaje("Error al anular la transacción. Operación no realizada");
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
            if (ddlOC.SelectedValue.Length == 0)
            {
                listaRegistro = null;
                gvReferencia.DataSource = listaRegistro;
                gvReferencia.DataBind();
                nitxtTotal.Text = "0";
                txtFechaReferencia.Text = "";
                return;
            }

            mfechaReferencia();
            cargarOCDetalle();
        }
        protected void ddlBodega_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var ddlBodega = (DropDownList)sender;
                GridViewRow gvr = (GridViewRow)ddlBodega.Parent.Parent;
                int re = Convert.ToInt16(((HiddenField)gvr.FindControl("hfRegistro")).Value);
                listaRegistro.FirstOrDefault(d => d.registro == re).idBodega = ddlBodega.SelectedValue.Trim();
            }
            catch (Exception ex)
            {
                ManejoError("Error al seleccionar la bodega debido a: " + ex.Message, "I");
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
                DataView dvproveedores = tercero.SeleccionaProveedoresFiltro(Convert.ToInt32(this.Session["empresa"]), txtFiltroProveedor.Text);
                if (dvproveedores.Count == 0)
                {
                    ManejoError("No se ha encontrado ningún proveedor por favor vuelva a intentarlo", "I");
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
        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            var txtCantidad = sender as TextBox;
            if (txtCantidad.Text.Trim().Length > 0)
            {
                var gvr = txtCantidad.Parent.Parent as GridViewRow;

                var hfRegistro = gvr.FindControl("hfRegistro") as HiddenField;
                var txtValorUnitario = gvr.FindControl("txtValorUnitario") as TextBox;
                var chkSelect = gvr.FindControl("chkSeleccion") as CheckBox;
                var resultado = listaRegistro.Find(z => z.registro == Convert.ToInt16(hfRegistro.Value));
                resultado.saldo = Convert.ToDecimal(txtCantidad.Text);
                resultado.valorUnitario = Convert.ToDecimal(txtValorUnitario.Text);
                resultado.valorTotal = resultado.valorUnitario * resultado.saldo;
                gvr.Cells[9].Text = (resultado.valorUnitario * resultado.saldo).ToString("N2");
                resultado.valorDescuento = 0;
                resultado.pDescuento = 0;
                resultado.valorBruto = resultado.valorUnitario * resultado.saldo;
                resultado.valorTotal = resultado.valorUnitario * resultado.saldo;
                resultado.check = chkSelect.Checked;
            }
            TotalizaGrillaReferencia();




        }
        protected void txtValorUnitario_TextChanged(object sender, EventArgs e)
        {
            var txtValorUnitario = sender as TextBox;
            if (txtValorUnitario.Text.Trim().Length > 0)
            {
                var gvr = txtValorUnitario.Parent.Parent as GridViewRow;

                var hfRegistro = gvr.FindControl("hfRegistro") as HiddenField;
                var txtCantidad = gvr.FindControl("txtCantidad") as TextBox;
                var chkSelect = gvr.FindControl("chkSeleccion") as CheckBox;
                var resultado = listaRegistro.Find(z => z.registro == Convert.ToInt16(hfRegistro.Value));
                resultado.cantidad = Convert.ToDecimal(txtCantidad.Text);
                resultado.valorUnitario = Convert.ToDecimal(txtValorUnitario.Text);
                resultado.valorTotal = resultado.valorUnitario * resultado.cantidad;
                gvr.Cells[9].Text = (resultado.valorUnitario * resultado.cantidad).ToString("N2");
                resultado.valorDescuento = 0;
                resultado.pDescuento = 0;
                resultado.valorBruto = resultado.valorUnitario * resultado.cantidad;
                resultado.valorTotal = resultado.valorUnitario * resultado.cantidad;
                resultado.check = chkSelect.Checked;
            }
            TotalizaGrillaReferencia();

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
        protected void chkSeleccion_CheckedChanged(object sender, EventArgs e)
        {
            var chkSelect = sender as CheckBox;
            var gvr = chkSelect.Parent.Parent as GridViewRow;

            var hfRegistro = gvr.FindControl("hfRegistro") as HiddenField;
            var txtCantidad = gvr.FindControl("txtCantidad") as TextBox;
            var txtValorUnitario = gvr.FindControl("txtValorUnitario") as TextBox;
            var resultado = listaRegistro.Find(z => z.registro == Convert.ToInt16(hfRegistro.Value));
            resultado.cantidad = Convert.ToDecimal(txtCantidad.Text);
            resultado.valorUnitario = Convert.ToDecimal(txtValorUnitario.Text);
            resultado.valorTotal = resultado.valorUnitario * resultado.cantidad;
            gvr.Cells[9].Text = (resultado.valorUnitario * resultado.cantidad).ToString("N2");
            resultado.valorDescuento = 0;
            resultado.pDescuento = 0;
            resultado.valorBruto = resultado.valorUnitario * resultado.cantidad;
            resultado.valorTotal = resultado.valorUnitario * resultado.cantidad;
            resultado.check = chkSelect.Checked;

            TotalizaGrillaReferencia();
        }

        #endregion Eventos
    }
}