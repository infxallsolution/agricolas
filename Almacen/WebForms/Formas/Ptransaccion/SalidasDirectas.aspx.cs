using Almacen.WebForms.App_Code.General;
using Almacen.WebForms.App_Code.Parametros;
using Almacen.WebForms.App_Code.Transaccion;
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

namespace Almacen.WebForms.Formas.Ptransaccion
{
    public partial class SalidasDirectas : BasePage
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
        CtransaccionAlmacen transaccionAlmacen = new CtransaccionAlmacen();
        public DataView Tercero
        {
            get { return this.Session["terceros"] != null ? (DataView)this.Session["terceros"] : null; }
            set { Session["terceros"] = value; }
        }

        public DataView Bodega
        {
            get { return this.Session["bodega"] != null ? (DataView)this.Session["bodega"] : null; }
            set { Session["bodega"] = value; }
        }

        public List<Cimpuestos> ListaImpuesto
        {
            get { return this.Session["impuesto"] != null ? (List<Cimpuestos>)this.Session["impuesto"] : null; }
            set { Session["impuesto"] = value; }
        }

        public List<CtransaccionAlmacen> ListaTransaccion
        {
            get { return this.Session["transaccion"] != null ? (List<CtransaccionAlmacen>)this.Session["transaccion"] : null; }
            set { Session["transaccion"] = value; }
        }

        public string registro
        {
            get { return this.Session["registro"] != null ? this.Session["registro"].ToString() : null; }
            set { Session["registro"] = value; }
        }

        #endregion Instancias

        #region Metodos

        private void manejoDetalle()
        {
            CcontrolesUsuario.ComportamientoCampoEntidad(this.upDetalle.Controls, "iTransaccionDetalle", Convert.ToString(this.ddlTipoDocumento.SelectedValue), (int)this.Session["empresa"]);
            CcontrolesUsuario.LimpiarControles(upDetalle.Controls);
            TotalizaGrillaReferencia();
            txtFiltroProducto.Visible = ddlProducto.Visible;
            txtFiltroProducto.Enabled = ddlProducto.Enabled;
            txtFiltroProducto.ReadOnly = false;
            lblDestino.Enabled = true;
            lblDestino.Visible = true;
            ddlDestino.Visible = true;
            ddlDestino.Enabled = true;
            ddlBodega.Visible = true;
            lblBodega.Visible = true;
            ddlBodega.Enabled = true;
            txvValorUnitario.Enabled = false;
            txvValorUnitario.ReadOnly = false;
            cargarSolicitante();
            btnRegistrar.Enabled = true;
            btnRegistrar.Visible = true;
            ddlFuncionario.Visible = true;
            ddlFuncionario.Enabled = true;
            lblFuncionario.Visible = true;
        }

        private void ImprimriTransaccion(string empresa, string tipo, string numero)
        {
            string vtn = "window.open('../Pinformes/ImprimeTransaccion.aspx?empresa=" + empresa + "&tipo=" + tipo + "&numero=" + numero + "','Impresión de Formatos','scrollbars=yes,resizable=yes','height=300', 'width=400')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
        }
        private void cargarSucurlsal(string tercero)
        {
            try
            {
                DataView dvSucursal = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cxcCliente", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                dvSucursal.RowFilter = "empresa = " + Session["empresa"].ToString() + " and activo =1 and idTercero=" + tercero;
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
        static string limpiarMensaje(string strIn)
        {
            // Replace invalid characters with empty strings.
            try
            {
                return Regex.Replace(strIn, @"[^\w\.@-]", " ",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters, 
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }
        private void precioProductoBodega()
        {
            if (ddlProducto.SelectedValue.Trim().Length > 0 & ddlBodega.SelectedValue.Trim().Length > 0)
                txvValorUnitario.Text = transacciones.SeleccionaBodegaSaldoItemCosto(Convert.ToInt16(this.Session["empresa"]),
                    ddlProducto.SelectedValue.Trim(), ddlBodega.SelectedValue.Trim()).ToString("#,##0.00");
        }
        private DataView compruebaSaldo()
        {
            DataView dvSaldo = null;
            try
            {

                if (Convert.ToBoolean(TipoTransaccionConfig(4)) == true)
                {
                    dvSaldo = item.RetornaSaldoItem(Convert.ToInt32(ddlProducto.SelectedValue.Trim()), Convert.ToInt16(Session["empresa"]), ddlBodega.SelectedValue);
                }
                return dvSaldo;
            }
            catch (Exception ex)
            {
                return dvSaldo;
            }

        }
        private void ValidarSaldo()
        {
            if (compruebaSaldo() != null)
            {
                txvSaldo.Visible = true;
                txvSaldo.Enabled = false;
                lblSaldo.Visible = true;
                foreach (DataRowView drv in compruebaSaldo())
                {
                    txvSaldo.Text = drv.Row.ItemArray.GetValue(0).ToString();
                }
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
                var ddlBodega = gvr.FindControl("ddlBodega") as DropDownList;
                DataView dvTipoDocto = bode.BuscarEntidad("", Convert.ToInt16(Session["empresa"]));
                EnumerableRowCollection<DataRow> query = from tipodoc in dvTipoDocto.Table.AsEnumerable()
                                                         where tipodoc.Field<int>("empresa") == Convert.ToInt16(Session["empresa"])
                                                         select tipodoc;

                ddlBodega.DataSource = query.AsDataView();
                ddlBodega.DataValueField = "codigo";
                ddlBodega.DataTextField = "descripcion";
                ddlBodega.DataBind();
                ddlBodega.Items.Insert(0, new ListItem("", ""));


                if (Convert.ToBoolean(hfoc.Value))
                {
                    //gvr.Cells[0].Enabled = false;

                    gvr.Cells[1].Enabled = false;
                    gvr.ForeColor = System.Drawing.Color.Red;
                    gvr.Cells[0].ForeColor = System.Drawing.Color.White;
                    gvr.Cells[1].ForeColor = System.Drawing.Color.White;
                }
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
                    this.gvTransaccion.DataSource = transacciones.GetTransaccionSA(where, Convert.ToInt16(Session["empresa"]));
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
            upDetalle.Visible = false;
            upCabeza.Visible = false;
            upConsulta.Visible = false;
            this.Session["transaccion"] = null;
            this.Session["operadores"] = null;
            Session["editarDetalle"] = false;
            Tercero = null;
            Bodega = null;
            ListaTransaccion = null;
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
            if (ListaTransaccion == null)
            {
                MostrarMensaje("El detalle debe tener por lo menos un regitro");
                return;
            }

            if (ListaTransaccion.Count == 0)
            {
                MostrarMensaje("El detalle debe tener por lo menos un regitro");
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




            if (ddlSolicitante.Visible)
            {
                if (ddlSolicitante.SelectedValue.Trim().Length == 0)
                {
                    ManejoError("Debe seleccionar un solicitante válido", "I");
                    return;
                }
                tercero = ddlSolicitante.SelectedValue.Trim();
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


            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    bool verificaSaldo = false;
                    tipo = ddlTipoDocumento.SelectedValue.Trim();
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
                            numero = transacciones.RetornaNumeroTransaccion(Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt32(this.Session["empresa"]));
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
                                    -1,//@signo
                                    null,//@solicitante
                                    terceroSucursal,//@surcursalTercero
                                    tercero,//@tercero
                                    ddlTipoDocumento.SelectedValue,//@tipo
                                    Convert.ToDecimal(nitxtTotalVenta.Text),//@totalDescuento
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
                                object[] objValoresCuerpo = new object[]{
                                 false,//@anulado
                                 Convert.ToDateTime(txtFecha.Text).Year,//@año
                                                      false,//@aprobado
                                                      y.Bodega,//@bodega
                                                      y.Cantidad,//@cantidad
                                                      y.Cantidad,//@cantidadRequerida
                                                      y.Ccosto,//@ccosto
                                                      false,//@cerrado
                                                      null,
                                                      y.Destino,//@destino
                                                      y.Nota,//@detalle
                                                      false,//@ejecutado
                                                      Convert.ToInt16(Session["empresa"]),//@empresa
                                                      null,//@fechaAnulado
                                                      null,//@fechaAprobado
                                                      null,//@fechaCierre
                                                       y.Producto,//@item
                                                      Convert.ToDateTime(txtFecha.Text).Month,//@mes
                                                      numero,//@numero
                                                      null,//@numeroReferencia
                                                      0,//@pDescuento
                                                      periodo,//@periodo
                                                      0,
                                                      y.Registro,//@registro
                                                      y.Cantidad,//@saldo
                                                      y.Tercero,//tercero
                                                      ddlTipoDocumento.SelectedValue,//@tipo
                                                      null,//@tipoReferencia
                                                      y.Umedida,//@uMedida
                                                      usuarioAnulado,//@usuarioAnulado
                                                      null,//@usuarioAprobado
                                                      null,//@usuarioCierre
                                                      y.ValorTotal,//@valorTotal
                                                      0,//@valorDescuento
                                                      0,
                                                      y.ValorTotal,//@valorTotal
                                                      0,//@valorSaldo
                                                      y.ValorTotal,//@valorTotal
                                                      y.valorTotalVenta,
                                                      y.ValorUnitario, //@valorUnitario
                                                      y.valorUnitarioVenta
                                                };

                                switch (CentidadMetodos.EntidadInsertUpdateDelete("iTransaccionDetalle", "inserta", "ppa", objValoresCuerpo))
                                {
                                    case 1:
                                        verificacion = false;
                                        break;
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
                                ImprimriTransaccion(this.Session["empresa"].ToString(), tipo, numero);
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


                if (Convert.ToBoolean(TipoTransaccionConfig(14)) == true)
                {
                    try
                    {
                        DataView dvProveedor = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cxcCliente", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                        dvProveedor.RowFilter = "activo =1 and empresa=" + Session["empresa"].ToString();
                        this.ddlTercero.DataSource = dvProveedor;
                        this.ddlTercero.DataValueField = "codigo";
                        this.ddlTercero.DataTextField = "cadena";
                        this.ddlTercero.DataBind();
                        this.ddlTercero.Items.Insert(0, new ListItem("", ""));
                    }
                    catch (Exception ex)
                    {
                        ManejoError("Error al cargar proveedores habilitados para orden directa. Correspondiente a: " + ex.Message, "C");
                    }
                }

                CargaDestinos();

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
                Bodega = null;
                if (Convert.ToBoolean(TipoTransaccionConfig(16)) == true)
                {

                    DataView dvBodega = CentidadMetodos.EntidadGet("iBodega", "ppa").Tables[0].DefaultView;
                    Bodega = dvBodega;
                    dvBodega.RowFilter = "tipo = 'V' and empresa=" + Session["empresa"].ToString();
                    this.ddlBodega.DataSource = dvBodega;
                    this.ddlBodega.DataValueField = "codigo";
                    this.ddlBodega.DataTextField = "descripcion";
                    this.ddlBodega.DataBind();
                    this.ddlBodega.Items.Insert(0, new ListItem("", ""));
                }
                else
                {
                    Bodega = transacciones.SeleccionaBodegaSaldoItem(Convert.ToInt16(this.Session["empresa"]), Convert.ToInt16(ddlProducto.SelectedValue.Trim()));
                    this.ddlBodega.DataSource = Bodega;
                    this.ddlBodega.DataValueField = "codigo";
                    this.ddlBodega.DataTextField = "cadena";
                    this.ddlBodega.DataBind();
                    this.ddlBodega.Items.Insert(0, new ListItem("", ""));
                }


            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar bodegas. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
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
            this.gvImpuesto.DataSource = null;
            this.gvImpuesto.DataBind();
            gvImpuesto.Visible = false;
            this.Session["impuesto"] = null;
            this.Session["transaccion"] = null;
            this.lbRegistrar.Visible = false;
            nitxtTotalVenta.Visible = false;
            nitxtTotal.Visible = false;
            nitxtTotalImpuesto.Visible = false;
            nitxtTotalValorBruto.Visible = false;
            nilblValorTotal.Visible = false;
            nilblValorTotal0.Visible = false;
            nilblValorTotalVenta.Visible = false;
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
        private void CargarCombos()
        {

            cargarSolicitante();

        }
        private void cargarSolicitante()
        {
            try
            {
                DataView dvTerceros = CentidadMetodos.EntidadGet("cTercero", "ppa").Tables[0].DefaultView;
                var resultado = dvTerceros.Table.AsEnumerable().Where(y => y.Field<int>("empresa").Equals(Convert.ToInt32(this.Session["empresa"])) && y.Field<bool>("empleado").Equals(true)).
                    Select(z => z);
                Tercero = resultado.AsDataView();
                ddlSolicitante.DataSource = Tercero;
                this.ddlSolicitante.DataValueField = "codigo";
                this.ddlSolicitante.DataTextField = "RazonSocial";
                this.ddlSolicitante.DataBind();
                this.ddlSolicitante.Items.Insert(0, new ListItem("", ""));
                cargarFuncionarios();

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar solicitantes. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
            }
        }
        private void cargarFuncionarios()
        {
            ddlFuncionario.DataSource = Tercero;
            this.ddlFuncionario.DataValueField = "codigo";
            this.ddlFuncionario.DataTextField = "RazonSocial";
            this.ddlFuncionario.DataBind();
            this.ddlFuncionario.Items.Insert(0, new ListItem("", ""));
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
                ManejoError("Error al cargar unidades de medida. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
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
                this.ddlTipoDocumento.DataSource = tipoTransaccion.GetTipoTransaccionModuloForma(Convert.ToInt16(Session["empresa"]), 12);
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
            CcontrolesUsuario.ComportamientoCampoEntidad(this.upDetalle.Controls, "iTransaccionDetalle", Convert.ToString(this.ddlTipoDocumento.SelectedValue), (int)this.Session["empresa"]);
            txtFiltroProducto.Visible = ddlProducto.Visible;
            txtFiltroProducto.Enabled = ddlProducto.Enabled;
            txtFiltroProducto.ReadOnly = false;
            if (Convert.ToBoolean(TipoTransaccionConfig(15)) == false)
                this.btnRegistrar.Visible = false;
            else
                this.btnRegistrar.Visible = true;
        }
        private void CargaProductos()
        {
            //try
            //{
            //    DataView dvProducto = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("iItems", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
            //    dvProducto.RowFilter = "tipo in ('I','T') and empresa=" + Convert.ToString(Session["empresa"]);
            //    this.ddlProducto.DataSource = dvProducto;
            //    this.ddlProducto.DataValueField = "codigo";
            //    this.ddlProducto.DataTextField = "cadena";
            //    this.ddlProducto.DataBind();
            //    this.ddlProducto.Items.Insert(0, new ListItem("", ""));
            //    this.ddlProducto.SelectedValue = "";
            //}
            //catch (Exception ex)
            //{
            //    ManejoError("Error al cargar productos. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
            //}
        }
        private void CargaDestinos()
        {
            try
            {
                DataView dvDestino = CentidadMetodos.EntidadGet("iDestino", "ppa").Tables[0].DefaultView;
                var resultado = dvDestino.Table.AsEnumerable().Where(x => x.Field<int>("empresa") == Convert.ToInt32(this.Session["empresa"]) && x.Field<bool>("activo") == true).Select(x => x);
                this.ddlDestino.DataSource = resultado.AsDataView();
                this.ddlDestino.DataValueField = "codigo";
                this.ddlDestino.DataTextField = "descripcion";
                this.ddlDestino.DataBind();
                this.ddlDestino.Items.Insert(0, new ListItem("", ""));
                this.ddlDestino.SelectedValue = "";
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar destinos. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
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
                ManejoError("Error al recuperar unidad de medida producto. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
            }
        }
        private void FiltroProductos()
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
                    ManejoError("No se ha encontrado ningún producto por favor vuelva a intentarlo", "I");
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
        private void CargarCentroCosto()
        {
            try
            {
                DataView dvCcosto = transacciones.ccostoDestino(ddlDestino.SelectedValue.Trim(), Convert.ToInt32(this.Session["empresa"]));

                if (dvCcosto.Table.Rows.Count == 0)
                {
                    this.ddlCcosto.ClearSelection();
                    this.ddlCcosto.DataSource = null;
                    this.ddlCcosto.DataBind();
                }
                else
                {
                    this.ddlCcosto.DataSource = dvCcosto;
                    this.ddlCcosto.DataValueField = "codigo";
                    this.ddlCcosto.DataTextField = "descripcion";
                    this.ddlCcosto.DataBind();
                    this.ddlCcosto.Items.Insert(0, new ListItem("", ""));
                }

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar centro de costos. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
            }
        }
        private void Cargarimpuesto()
        {
            ListaImpuesto = null;
            if (ListaTransaccion == null)
                return;
            ListaTransaccion.ForEach(y =>
            {
                var dvImpuesto = tipoTransaccion.SeleccionaImpuestoItemTercero(Convert.ToInt16(Session["empresa"]), ddlSucursal.SelectedValue,
                    Convert.ToInt16(ddlTercero.SelectedValue), Convert.ToInt16(y.Producto));

                if (dvImpuesto.Count > 0)
                {
                    if (ListaImpuesto == null)
                    {
                        ListaImpuesto = dvImpuesto.Table.AsEnumerable().Select(x =>
                        new Cimpuestos
                        {
                            Concepto = x.Field<string>("concepto").ToString(),
                            NombreConcepto = x.Field<string>("nombreConcepto").ToString(),
                            Tasa = Convert.ToDecimal(x.Field<double>("tasa").ToString()),
                            BaseGravable = Convert.ToDecimal(x.Field<double>("baseGravable").ToString()),
                            BaseMinima = Convert.ToDecimal(x.Field<double>("baseMinima").ToString()),
                            ValorImpuesto = 0
                        }).ToList();

                        ListaImpuesto.ForEach(imp =>
                        {
                            y.conceptoImpuesto = imp.Concepto;
                            y.pImpuesto = imp.Tasa;
                            y.valorImpuesto = y.ValorBruto * imp.Tasa / 100;
                            y.valorNeto = y.ValorBruto + y.valorImpuesto;

                        });

                    }
                    else
                    {
                        var resultado = dvImpuesto.Table.AsEnumerable().Select(x => new Cimpuestos
                        {
                            Concepto = x.Field<string>("concepto").ToString(),
                            NombreConcepto = x.Field<string>("nombreConcepto").ToString(),
                            Tasa = Convert.ToDecimal(x.Field<double>("tasa").ToString()),
                            BaseGravable = Convert.ToDecimal(x.Field<double>("baseGravable").ToString()),
                            BaseMinima = Convert.ToDecimal(x.Field<double>("baseMinima").ToString()),
                            ValorImpuesto = 0
                        }).ToList();

                        resultado.ForEach(z =>
                        {
                            if (!ListaImpuesto.Exists(w => w.Concepto == z.Concepto))
                            {
                                ListaImpuesto.Add(z);
                                y.conceptoImpuesto = z.Concepto;
                                y.pImpuesto = z.Tasa;
                                y.valorImpuesto = y.ValorBruto * z.Tasa / 100;
                                y.valorNeto = y.ValorBruto + y.valorImpuesto;
                            }
                            else
                            {
                                y.conceptoImpuesto = z.Concepto;
                                y.pImpuesto = z.Tasa;
                                y.valorImpuesto = y.ValorBruto * z.Tasa / 100;
                                y.valorNeto = y.ValorBruto + y.valorImpuesto;
                            }
                        });

                    }
                }


            });
            gvImpuesto.Visible = true;
            this.gvImpuesto.DataSource = ListaImpuesto;
            this.gvImpuesto.DataBind();

        }
        private void TotalizaGrillaReferencia()
        {
            try
            {
                nitxtTotalVenta.Visible = true;
                nitxtTotal.Visible = true;
                nitxtTotalImpuesto.Visible = true;
                nitxtTotalValorBruto.Visible = true;
                nilblValorTotal.Visible = true;
                nilblValorTotal0.Visible = true;
                nilblValorTotalVenta.Visible = true;
                nilblValorTotal2.Visible = true;
                nitxtTotalImpuesto.Text = "0";
                this.nitxtTotalValorBruto.Text = "0";
                nitxtTotalVenta.Text = "0";
                nitxtTotal.Text = "0";
                this.nitxtTotalValorBruto.Text = ListaTransaccion.Sum(z => z.ValorTotal).ToString("N2");
                this.nitxtTotalVenta.Text = ListaTransaccion.Sum(z => z.valorTotalVenta).ToString("N2");
                nitxtTotalImpuesto.Text = "0";
                nitxtTotal.Text = nitxtTotalValorBruto.Text;
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
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
                ManejoError("Error al comprobar saldo. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
                return false;
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
                        Tercero = null;
                        Bodega = null;
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
            Tercero = null;
            Bodega = null;
            ListaTransaccion = null;
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

                if (Convert.ToBoolean(TipoTransaccionConfig(17)) == true)
                {
                    if (tipoTransaccion.RetornavalidacionRegistro(Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"])) == 1)
                    {
                        ManejoError("No se puede realizar este tipo movimiento el día de hoy", "I");
                        return;
                    }

                }
                ddlSolicitante.Visible = true;
                ComportamientoConsecutivo();
                ComportamientoTransaccion();
                CargaProductos();
                CargaDestinos();
                CargarCombos();
                lblDestino.Enabled = true;
                lblDestino.Visible = true;
                ddlDestino.Visible = true;
                ddlDestino.Enabled = true;
                ddlBodega.Visible = true;
                lblBodega.Visible = true;
                ddlBodega.Enabled = true;
                txvValorUnitario.Enabled = false;
                txvValorUnitario.ReadOnly = false;
                cargarSolicitante();
                ddlFuncionario.Visible = true;
                ddlFuncionario.Enabled = true;
                lblFuncionario.Visible = true;

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
            txtFecha.Visible = true;
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
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            Guardar();
        }
        protected void txtProducto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dvProducto = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("iItems", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                dvProducto.RowFilter = "tipo in ('I','T') and codigo like '%" + ddlProducto.SelectedValue.Trim() + "%' or descripcion like '%" + this.ddlProducto.SelectedValue.Trim() + "%'";
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
        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlProducto.Focus();
            UmedidaProducto();
            UmedidaProducto();
            cargarBodega();
            precioProductoBodega();
        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (Convert.ToBoolean(Session["editar"]) == false)
            {
                try
                {
                    ListaTransaccion.RemoveAt(e.RowIndex);
                    this.gvLista.DataSource = ListaTransaccion;
                    this.gvLista.DataBind();
                    Cargarimpuesto();
                    TotalizaGrillaReferencia();
                }
                catch (Exception ex)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Error al eliminar el registro. Correspondiente a: " + ex.Message, "w");
                }
            }
            else
            {

                var hfRegistro = gvLista.Rows[e.RowIndex].FindControl("hfRegistro") as HiddenField;
                var resultado = ListaTransaccion.Find(y => y.Registro.Equals(Convert.ToInt16(hfRegistro.Value)));

                if (resultado.Anulado)
                {
                    MostrarMensaje("Registro anulado, no es posible volver anular");
                    return;
                }
                else
                    resultado.Anulado = true;

                this.gvLista.DataSource = ListaTransaccion;
                this.gvLista.DataBind();

                this.gvLista.Rows[e.RowIndex].BackColor = System.Drawing.Color.Red;


            }

        }
        protected void gvTransaccion_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(Session["editarDetalle"]) == false)
                    Session["editarDetalle"] = true;
                else
                {
                    ManejoError("Debe de agregar el registro seleccionado para continuar", "I");
                    return;
                }

                CargarCombos();
                CargaDestinos();

                var hfRegistro = gvLista.SelectedRow.FindControl("hfRegistro") as HiddenField;
                registro = hfRegistro.Value;
                var resultado = ListaTransaccion.Find(y => y.Registro.Equals(Convert.ToInt32(hfRegistro.Value)));

                if (resultado.Anulado)
                {
                    Session["editarDetalle"] = false;
                    CerroresGeneral.ManejoError(this, GetType(), "Registro anulado no es posible su edición", "w");
                    return;
                }

                txtFiltroProducto.Text = resultado.Producto.ToString();
                FiltroProductos();
                this.ddlProducto.SelectedValue = resultado.Producto;
                CargarUmedida();
                txvCantidad.Text = resultado.Cantidad.ToString("N2");
                txvValorUnitario.Text = resultado.ValorUnitario.ToString("N2");
                txvValorUnitarioVenta.Text = resultado.valorUnitarioVenta.ToString("N2");
                this.ddlDestino.SelectedValue = resultado.Destino;
                CargarCentroCosto();
                cargarBodega();
                this.ddlCcosto.SelectedValue = resultado.Ccosto;
                this.ddlUmedida.SelectedValue = resultado.Umedida;
                this.ddlBodega.SelectedValue = resultado.Bodega;
                this.ddlFuncionario.SelectedValue = resultado.Tercero;
                this.txtDetalle.Text = Server.HtmlDecode(resultado.Nota);
                ListaTransaccion.Remove(resultado);
                this.gvLista.DataSource = ListaTransaccion;
                this.gvLista.DataBind();
                TotalizaGrillaReferencia();

            }
            catch (Exception ex)
            {
                Session["editarDetalle"] = false;
                ManejoError("Error al cargar los campos del registro en el formulario. Correspondiente a: " + limpiarMensaje(ex.Message), "I");
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
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                bool oc = false; decimal cantidadValida = 0; decimal cantidadComparar = 0;
                DataView dvBodega = transacciones.SeleccionaBodegaSaldoItem(Convert.ToInt16(this.Session["empresa"]), Convert.ToInt16(ddlProducto.SelectedValue.Trim()));
                EnumerableRowCollection<DataRow> query = from tipodoc in dvBodega.Table.AsEnumerable()
                                                         where
                                                         tipodoc.Field<string>("codigo") == ddlBodega.SelectedValue.Trim()
                                                         select tipodoc;


                foreach (DataRowView drv in query.AsDataView())
                {
                    cantidadValida = Convert.ToDecimal(drv.Row.ItemArray.GetValue(2));
                }

                if (ListaTransaccion != null)
                {
                    foreach (CtransaccionAlmacen ta in ListaTransaccion)
                    {
                        if (ta.Producto == ddlProducto.SelectedValue.Trim())
                        {
                            cantidadComparar += ta.Cantidad;
                        }
                    }
                }
                cantidadComparar += Convert.ToDecimal(txvCantidad.Text);

                if (cantidadValida < cantidadComparar)
                {
                    ManejoError("La cantidad ingresada no puede ser mayor al saldo que hay en la bodega", "I");
                    return;
                }

                if (Convert.ToString(this.ddlTipoDocumento.SelectedValue).Trim().Length == 0 ||
                    this.txtNumero.Text.Trim().Length == 0)
                {
                    ManejoError("Debe ingresar tipo y número de transacción", "I");
                    return;
                }

                if (Convert.ToString(this.ddlBodega.SelectedValue).Trim().Length == 0 & ddlBodega.Visible)
                {
                    ManejoError("Debe ingresar una bodega", "I");
                    return;
                }



                if (Convert.ToDecimal(txvCantidad.Text) <= 0)
                {
                    ManejoError("La cantidad no puede ser igual o menor que cero. Por favor corrija", "I");
                    return;
                }

                if (ddlCcosto.Visible)
                {
                    if (this.ddlCcosto.SelectedValue.Trim().Length == 0)
                    {
                        this.ManejoError("Debe seleccionar un centro de costo para cargar la transacción", "I");
                        return;
                    }
                }

                if (ddlDestino.Visible)
                {
                    if (this.ddlDestino.SelectedValue.Trim().Length == 0)
                    {
                        this.ManejoError("Debe seleccionar un destino para cargar la transacción", "I");
                        return;
                    }
                }

                if (ListaTransaccion == null)
                    ListaTransaccion = new List<CtransaccionAlmacen>();

                if (!Convert.ToBoolean(Session["editarDetalle"]))
                    registro = ListaTransaccion.Count() == 0 ? "1" : (ListaTransaccion.Max(r => r.Registro) + 1).ToString();

                Session["editarDetalle"] = false;



                ListaTransaccion.Add(new CtransaccionAlmacen
                {
                    Bodega = ddlBodega.SelectedValue.Trim(),
                    Producto = this.ddlProducto.SelectedValue,
                    NombreProducto = ddlProducto.SelectedItem.Text,
                    nombreBodega = Bodega.Table.AsEnumerable().Where(y => y.Field<string>("codigo").Equals(ddlBodega.SelectedValue)).Select(z => z.Field<string>("descripcion")).FirstOrDefault().ToString(),
                    Cantidad = Convert.ToDecimal(txvCantidad.Text),
                    Umedida = Server.HtmlDecode(Convert.ToString(this.ddlUmedida.SelectedValue)),
                    ValorUnitario = Convert.ToDecimal(txvValorUnitario.Text),
                    Destino = Convert.ToString(this.ddlDestino.SelectedValue),
                    Ccosto = Convert.ToString(this.ddlCcosto.SelectedValue),
                    ValorTotal = Convert.ToDecimal(txvValorUnitario.Text) * Convert.ToDecimal(txvCantidad.Text),
                    Nota = txtDetalle.Text,
                    Registro = Convert.ToInt32(registro),
                    Oc = false,
                    Tercero = ddlFuncionario.SelectedValue,
                    NombreTercero = ddlFuncionario.SelectedItem.Text,
                    nombreCentroCosto = ddlCcosto.SelectedItem.Text,
                    NombreDestino = ddlDestino.SelectedItem.Text,
                    valorUnitarioVenta = txvValorUnitarioVenta.Visible ? Convert.ToDecimal(txvValorUnitarioVenta.Text) : 0,
                    valorTotalVenta = txvValorUnitarioVenta.Visible ? Convert.ToDecimal(txvValorUnitarioVenta.Text) * Convert.ToDecimal(txvCantidad.Text) : 0
                });


                this.gvLista.DataSource = ListaTransaccion.OrderBy(x => x.Registro);
                this.gvLista.DataBind();

                this.ddlProducto.Focus();
                Session["editarDetalle"] = false;
                txvValorUnitario.Enabled = true;
                ddlCcosto.Enabled = true;
                ddlDestino.Enabled = true;
                ddlProducto.Enabled = true;
                ddlUmedida.Enabled = true;
                manejoDetalle();

            }
            catch (Exception ex)
            {
                ManejoError("Error al insertar el registro. Correspondiente a: " + limpiarMensaje(ex.Message), "I");
            }
        }
        protected void ddlBodega_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlBodega.SelectedValue.Trim().Length > 0 && ddlProducto.SelectedValue.Trim().Length > 0)
                    ValidarSaldo();

                precioProductoBodega();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los saldos por bodega debido a:    " + limpiarMensaje(ex.Message), "I");
            }


        }
        protected void txtFiltroProducto_TextChanged(object sender, EventArgs e)
        {
            FiltroProductos();
        }
        protected void ddlDestino_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CargarCentroCosto();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar el centro de costo debido a:" + ex.Message, "I");
            }

        }
        protected void ddlTercero_SelectedIndexChanged(object sender, EventArgs e)
        {

            cargarSucurlsal(ddlTercero.SelectedValue);
            ddlSucursal.Focus();
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