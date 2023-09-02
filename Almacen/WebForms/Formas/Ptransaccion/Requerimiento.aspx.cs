using Almacen.seguridadInfos;
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
    public partial class Requerimiento : BasePage
    {
        #region Instancias
        //--------------------------------------------------------------------------


        CentidadMetodos CentidadMetodos = new CentidadMetodos();
        Security seguridad = new Security();

        //--------------------------------------------------------------------------

        Ctransacciones transacciones = new Ctransacciones();
        CtipoTransaccion tipoTransaccion = new CtipoTransaccion();
        Coperadores operadores = new Coperadores();
        Cperiodos periodo = new Cperiodos();
        Citems item = new Citems();
        Cdestinos destino = new Cdestinos();
        CtransaccionAlmacen transaccionAlmacen = new CtransaccionAlmacen();


        public List<Cimpuestos> ListaImpuesto
        {
            get { return this.Session["impuesto"] != null ? (List<Cimpuestos>)this.Session["impuesto"] : null; }
            set { Session["impuesto"] = value; }
        }
        DataView dvProductoActual
        {
            get { return this.Session["productoActual"] != null ? (DataView)this.Session["productoActual"] : null; }
            set { this.Session["productoActual"] = value; }
        }
        public string cadena
        {
            get { return this.Session["cadena"] != null ? this.Session["cadena"].ToString() : null; }
            set { Session["cadena"] = value; }
        }
        public string registro
        {
            get { return this.Session["no"] != null ? this.Session["no"].ToString() : null; }
            set { Session["no"] = value; }
        }
        public List<Cregistros> listaRegistros
        {
            get { return this.Session["reque"] != null ? (List<Cregistros>)this.Session["reque"] : null; }
            set { Session["reque"] = value; }
        }


        #endregion Instancias

        #region Metodos

        private void ImprimriTransaccion(string empresa, string tipo, string numero)
        {
            string vtn = "window.open('../Pinformes/ImprimeTransaccion.aspx?empresa=" + empresa + "&tipo=" + tipo + "&numero=" + numero + "','Impresión de Formatos','scrollbars=yes,resizable=yes','height=300', 'width=400')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
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
        private void ValidarSaldo()
        {
            if (compruebaSaldo() != null)
            {
                txvSaldo.Enabled = false;
                foreach (DataRowView drv in compruebaSaldo())
                {
                    txvSaldo.Text = drv.Row.ItemArray.GetValue(0).ToString();
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
                    this.gvTransaccion.DataSource = transacciones.GetTransaccionReq(where, Convert.ToInt16(Session["empresa"]));
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
            this.Session["transaccion"] = null;
            Session["editarDetalle"] = false;
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.lbRegistrar.Visible = false;
            this.lbCancelar.Visible = false;
            upDetalle.Visible = false;
            upCabeza.Visible = false;
            upConsulta.Visible = false;
        }
        private void Guardar()
        {
            string numero = "", periodo = Convert.ToString(Convert.ToDateTime(txtFecha.Text).Year) + Convert.ToString(Convert.ToDateTime(txtFecha.Text).Month).PadLeft(2, '0').Trim(), tercero = "",
                usuarioAnulado = null, solicitante = "", docReferencia = "", bodega = null, destino = null, ccosto = null, tipo = "";
            bool verificacion = true, anulado = false;
            decimal cantidadAprobada = 0;

            if (gvLista.Rows.Count == 0 && Convert.ToBoolean(TipoTransaccionConfig(1)) == false && Convert.ToBoolean(TipoTransaccionConfig(12)) == false)
            {
                this.ManejoError("Detalle de la transacción vacio. No es posible registrar la transacción", "I");
                return;
            }

            if (Convert.ToString(this.ddlTipoDocumento.SelectedValue).Trim().Length == 0)
            {
                this.ManejoError("Por favor seleccione un tipo de movimiento. No es posible registrar la transacción", "I");
                return;
            }

            if (Convert.ToString(this.ddlSolicitante.SelectedValue).Trim().Length == 0)
            {
                this.ManejoError("Por favor seleccione un solicitante. No es posible registrar la transacción", "I");
                return;
            }
            if (this.txtNumero.Text.Trim().Length == 0)
            {
                this.ManejoError("El número de transacción no puede estar vacio. No es posible registrar la transacción", "I");
                return;
            }
            if (Convert.ToString(txtFecha.Text).Trim().Length == 0)
            {
                this.ManejoError("Debe seleccionar una fecha. No es posible registrar la transacción", "I");
                return;
            }



            if (this.ddlSolicitante.Enabled == true)
            {
                if (this.ddlSolicitante.SelectedValue.Trim().Length == 0)
                {
                    this.ManejoError("Debe seleccionar el solicitante para continuar", "I");
                    return;
                }
            }

            if (this.ddlSolicitante.Enabled == false)
                solicitante = null;
            else
                solicitante = Convert.ToString(this.ddlSolicitante.SelectedValue);

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
                    false,//@anulado 1
                     Convert.ToDateTime(txtFecha.Text).Year,//@año 2
                                    false,//@aprobado 3
                                    false,//@cerrado 4
                                    Convert.ToInt16(Session["empresa"]),//@empresa 5
                                    Convert.ToDateTime(txtFecha.Text),//@fecha 6
                                    null,//@fechaAnulado 7
                                    null,//@fechaAprobado 8
                                    null,//@fechaCierre 9
                                    DateTime.Now,//@fechaRegistro 10
                                    Convert.ToDateTime(txtFecha.Text).Month,//@mes 11
                                    Server.HtmlDecode(this.txtObservacion.Text.Trim()),//@notas 12
                                    numero,//@numero 13 
                                    periodo,//@periodo 14
                                    docReferencia,//@referencia 15
                                    null,//@signo 16
                                    solicitante,//@solicitante 17
                                    null,//@surcursalTercero 18
                                    null,//@tercero 19
                                    ddlTipoDocumento.SelectedValue,//@tipo 20
                                    0,//21
                                    0,//22
                                    0,//23
                                    0,//24
                                    this.Session["usuario"].ToString(),//@usuario 25
                                    null,//@usuarioAbrobado 26
                                    null,//@usuarioAnulado 27
                                    0//@vigencia 28
                                };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("iTransaccion", "inserta", "ppa", objValores))
                    {
                        case 0:

                            int i = 0;
                            listaRegistros.ForEach(z =>
                            {
                                object[] objValoresCuerpo = new object[]{
                                anulado,//@anulado
                                                      Convert.ToDateTime(txtFecha.Text).Year,//@año
                                                      false,//@aprobado
                                                      z.idBodega,//@bodega
                                                      z.cantidad,//@cantidad
                                                      z.cantidad,//@cantidadRequerida
                                                      z.idCcosto,//@ccosto
                                                      false,//@cerrado
                                                      null,
                                                      z.idDestino,//@destino
                                                      z.nota,//@detalle
                                                      false,//@ejecutado
                                                      Convert.ToInt16(Session["empresa"]),//@empresa
                                                      DateTime.Now,//@fechaAnulado
                                                      null,//@fechaAprobado
                                                      null,//@fechaCierre
                                                      z.idItem ,//@item
                                                      Convert.ToDateTime(txtFecha.Text).Month,//@mes
                                                      numero,//@numero
                                                      z.numeroReferencia,//@numeroReferencia
                                                      0,//@pDescuento
                                                      periodo,//@periodo
                                                      0,
                                                      i ,//@registro
                                                      0,//@saldo
                                                      tercero,
                                                      ddlTipoDocumento.SelectedValue,//@tipo
                                                      z.tipoReferencia,//@tipoReferencia
                                                      z.idUmedida ,//@uMedida
                                                      usuarioAnulado,//@usuarioAnulado
                                                      null,//@usuarioAprobado
                                                      null,//@usuarioCierre
                                                      0,
                                                      0,//@valorDescuento
                                                      0,
                                                      0,
                                                      0,//@valorSaldo
                                                      0,//@valorTotal
                                                      0,
                                                      0,//@valorUnitario
                                                      0
                                                };

                                switch (CentidadMetodos.EntidadInsertUpdateDelete("iTransaccionDetalle", "inserta", "ppa", objValoresCuerpo))
                                {
                                    case 1:
                                        verificacion = false;
                                        break;
                                }
                                i++;
                            });

                            if (verificacion == true)
                            {
                                if (this.txtNumero.Enabled == false && Convert.ToBoolean(this.Session["editar"]) == false)
                                {
                                    if (tipoTransaccion.ActualizaConsecutivo(Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"])) != 0)
                                    {
                                        this.ManejoError("Error al actualizar el consecutivo. Operación no realizada", "I");
                                        return;
                                    }
                                }

                                ManejoExito("Transacción registrada satisfactoriamente. Transacción número " + numero, "A");
                                ImprimriTransaccion(Convert.ToString(this.Session["empresa"]), tipo, numero);
                                ts.Complete();

                            }
                            else
                            {
                                this.ManejoError("Error al insertar detalle de transacción. Operación no realizada", "I");
                            }
                            break;

                        case 1:

                            this.ManejoError("Error al insertar la transacción. Operación no realizada", "I");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    this.ManejoError("Error al registrar la transacción. Correspondiente a: " + limpiarMensaje(ex.Message), "I");
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

            if (periodo.RetornaPeriodoCerrado(Convert.ToDateTime(txtFecha.Text).Year, Convert.ToDateTime(txtFecha.Text).Month, (int)this.Session["empresa"]) == 1)
                ManejoError("Periodo cerrado. No es posible realizar nuevas transacciones", "I");

            else
            {
                if (Convert.ToBoolean(this.Session["editar"]) == true)
                {
                    //if (Convert.ToString(Convert.ToDateTime(txtFecha.Text).Year) + Convert.ToString(Convert.ToDateTime(txtFecha.Text).Month).PadLeft(2, '0') != this.Session["periodo"].ToString())
                    //    ManejoError("La fecha debe corresponder al periodo actual de la transacción", "A");
                }
                else
                {
                    if (CompruebaTransaccionExistente() == 1)
                    {
                        this.ManejoError("Transacción existente. Por favor corrija", "I");
                        return;
                    }


                    CargaDestinos();
                    cargarBodega();

                    this.txtObservacion.Focus();
                    this.btnRegistrar.Visible = true;
                    if (Convert.ToBoolean(TipoTransaccionConfig(1)) == true && Convert.ToBoolean(TipoTransaccionConfig(12)) == false)
                        this.btnRegistrar.Visible = true;
                }
            }
        }
        private void cargarBodega()
        {
            try
            {

                DataView dvBodega = CentidadMetodos.EntidadGet("iBodega", "ppa").Tables[0].DefaultView;
                this.ddlBodega.DataSource = dvBodega.Table.AsEnumerable().Where(x => x.Field<int>("empresa").Equals(Convert.ToInt32(this.Session["empresa"])) && x.Field<bool>("mExistencias") == true).Select(x => x).AsDataView();
                this.ddlBodega.DataValueField = "codigo";
                this.ddlBodega.DataTextField = "descripcion";
                this.ddlBodega.DataBind();
                this.ddlBodega.Items.Insert(0, new ListItem("", ""));
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
            this.Session["impuesto"] = null;
            this.Session["transaccion"] = null;
            this.lbRegistrar.Visible = false;
            lbFecha.Visible = false;
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
                this.niddlCampo.DataSource = transacciones.GetCamposREQ(Convert.ToInt16(this.Session["empresa"]));
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

            try
            {
                var resultado = CentidadMetodos.EntidadGet("iSolicitante", "ppa").Tables[0].DefaultView.Table.AsEnumerable().Where(y => y.Field<int>("empresa").Equals(Convert.ToInt32(this.Session["empresa"]))).OrderBy(w => w.Field<string>("nombreTercero")).Select(x => x);
                this.ddlSolicitante.DataSource = resultado.AsDataView();
                this.ddlSolicitante.DataValueField = "codigo";
                this.ddlSolicitante.DataTextField = "nombreTercero";
                this.ddlSolicitante.DataBind();
                this.ddlSolicitante.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar solicitantes. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
            }

        }
        private void CargarUmedida()
        {
            try
            {
                DataView dvUmedida = CentidadMetodos.EntidadGet("gUnidadMedida", "ppa").Tables[0].DefaultView;
                var resultado = dvUmedida.Table.AsEnumerable().Where(x => x.Field<int>("empresa").Equals(Convert.ToInt32(this.Session["empresa"]))).OrderBy(x => x.Field<string>("descripcion"));
                this.ddlUmedida.DataSource = resultado.AsDataView();
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
                this.ddlTipoDocumento.DataSource = tipoTransaccion.GetTipoTransaccionModuloForma(Convert.ToInt16(Session["empresa"]), 14);
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
            listaRegistros = null;
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
                        this.ManejoError("Transacción existente. Por favor corrija", "I");
                        return;
                    }
                }
                this.txtNumero.Enabled = false;
                this.nilbNuevo.Visible = false;
                this.txtFecha.Focus();
                habilitarTransaccion();
            }
        }
        private void ComportamientoTransaccion()
        {
            upCabeza.Visible = true;
            upDetalle.Visible = true;
            txvSaldo.Enabled = false;
            txtFiltroProducto.Visible = ddlProducto.Visible;
            txtFiltroProducto.Enabled = ddlProducto.Enabled;
            txtFiltroProducto.ReadOnly = false;
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
                this.ddlDestino.DataSource = OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("iDestino", "ppa"), "descripcion", Convert.ToInt32(this.Session["empresa"]));
                this.ddlDestino.DataValueField = "codigo";
                this.ddlDestino.DataTextField = "descripcion";
                this.ddlDestino.DataBind();
                this.ddlDestino.Items.Insert(0, new ListItem("", ""));
                this.ddlDestino.SelectedValue = "";
            }
            catch (Exception ex)
            {
                //  ManejoError("Error al cargar destinos. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
            }
        }
        private void UmedidaProducto()
        {
            try
            {
                CargarUmedida();
                this.ddlUmedida.SelectedValue = item.RetornaUmedida(Convert.ToString(this.ddlProducto.SelectedValue), Convert.ToInt16(Session["empresa"]));

            }
            catch (Exception ex)
            {
                ManejoError("Error al recuperar unidad de medida producto. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
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
                    this.ddlCcosto.DataTextField = "cadena";
                    this.ddlCcosto.DataBind();
                    this.ddlCcosto.Items.Insert(0, new ListItem("", ""));
                }

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar centro de costos. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
            }
        }
        private void habilitarTransaccion()
        {
            if (this.txtFecha.Visible == true)
            {
                if (CompruebaTransaccionExistente() == 1)
                {
                    this.ManejoError("Transacción existente. Por favor corrija", "I");

                    return;
                }
            }

            CcontrolesUsuario.HabilitarControles(this.upCabeza.Controls);
            CcontrolesUsuario.HabilitarControles(this.upDetalle.Controls);
            CargarCombos();
            cargarBodega();
            CargaDestinos();
            this.nilbNuevo.Visible = false;
            this.txtFecha.Focus();
            this.btnRegistrar.Visible = true;
            txvSaldo.Enabled = false;
            txvSaldo.ReadOnly = true;

            if (!Convert.ToBoolean(TipoTransaccionConfig(5)))
            {
                ddlTipoReferencia.Visible = false;
                ddlNumeroReferencia.Visible = false;
                lblReferencia.Visible = false;
            }
            else
                CargarTipoReferencia();

        }
        private DataView compruebaSaldo()
        {
            DataView dvSaldo = null;
            try
            {


                dvSaldo = item.RetornaSaldoItem(Convert.ToInt32(ddlProducto.SelectedValue.Trim()), Convert.ToInt16(Session["empresa"]), ddlBodega.SelectedValue.Trim());
                return dvSaldo;
            }
            catch (Exception ex)
            {
                return dvSaldo;
            }



        }
        private void mFiltroProducto()
        {
            dvProductoActual = null;
            if (txtFiltroProducto.Text.Trim().Length == 0)
            {
                MostrarMensaje("Ingrese un filtro válido");
                txtFiltroProducto.Focus();
                return;
            }
            DataView dvitems = item.RetornaItemsFiltro(Convert.ToInt32(this.Session["empresa"]), txtFiltroProducto.Text);
            if (dvitems.Count == 0)
            {
                MostrarMensaje("No se ha encontrado ningún producto por favor vuelva a intentarlo");
                txtFiltroProducto.Focus();
                return;
            }
            ddlProducto.DataSource = dvitems;
            dvProductoActual = dvitems;
            ddlProducto.DataValueField = "codigo";
            ddlProducto.DataTextField = "cadena";

            ddlProducto.DataBind();
            ddlProducto.Items.Insert(0, new ListItem("", ""));
        }
        private void CargarFuncionarios()
        {
            try
            {
                DataView dvFuncionario = CentidadMetodos.EntidadGet("cTercero", "ppa").Tables[0].DefaultView;
                var resultado = dvFuncionario.Table.AsEnumerable().Where(x => x.Field<int>("empresa") == Convert.ToInt32(this.Session["empresa"])
                && x.Field<bool>("activo") == true && (x.Field<bool>("empleado") == true || x.Field<bool>("cliente") == true)).Select(x => x);
                this.ddlTercero.DataSource = resultado.AsDataView();
                this.ddlTercero.DataValueField = "codigo";
                this.ddlTercero.DataTextField = "descripcion";
                this.ddlTercero.DataBind();
                this.ddlTercero.DataBind();
                this.ddlTercero.Items.Insert(0, new ListItem("", ""));

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }
        private void CargarTipoReferencia()
        {
            try
            {
                this.ddlTipoReferencia.DataSource = tipoTransaccion.GetTipoTransaccionModuloMantenimiento(Convert.ToInt16(Session["empresa"]), 33);
                this.ddlTipoReferencia.DataValueField = "codigo";
                this.ddlTipoReferencia.DataTextField = "descripcion";
                this.ddlTipoReferencia.DataBind();
                this.ddlTipoReferencia.Items.Insert(0, new ListItem("", ""));
                this.ddlTipoReferencia.SelectedValue = "";
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        private void cargarNumeroReferencia()
        {
            DataView dvSol = CtipoTransaccion.cargarNumeroTipoOrdenTrabajo(Convert.ToInt16(this.Session["empresa"]), ddlTipoReferencia.SelectedValue.Trim());
            ddlNumeroReferencia.DataSource = dvSol;
            ddlNumeroReferencia.DataValueField = "numero";
            ddlNumeroReferencia.DataTextField = "cadena";
            ddlNumeroReferencia.DataBind();
            ddlNumeroReferencia.Items.Insert(0, new ListItem("", ""));
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
                        this.Session["transaccion"] = null;
                        this.Session["operadores"] = null;
                        Session["editarDetalle"] = false;
                        TabRegistro();
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
            Session["editarDetalle"] = false;

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
                this.txtNumero.Text = ConsecutivoTransaccion();
                ComportamientoTransaccion();
                ComportamientoConsecutivo();
                this.btnRegistrar.Visible = true;
                ddlTercero.Visible = chkTercero.Checked;

            }
            catch (Exception ex)
            {
                ManejoError("Error al comprobar transacción con referencia. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
            }
        }
        protected void txtNumero_TextChanged(object sender, EventArgs e)
        {
            habilitarTransaccion();
        }
        protected void CalendarFecha_SelectionChanged(object sender, EventArgs e)
        {
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
            CargaCampos();
            nitxtValor2.Visible = false;

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
        protected void lbImprimir_Click(object sender, EventArgs e)
        {

        }
        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.ddlProducto.Focus();
                var ldescripcion = dvProductoActual.Table.AsEnumerable().Where(y => y.Field<string>("codigo") == ddlProducto.SelectedValue.Trim()).Select(z => new { nombrePro = z.Field<string>("descripcion") }).ToList();

                ldescripcion.ForEach(w =>
                {
                    ddlProducto.Attributes["data"] = w.nombrePro;
                }
                );

                UmedidaProducto();

                if (ddlUmedida.Items.Count > 0)
                    ddlUmedida.Enabled = false;
                else
                    ddlUmedida.Enabled = false;

                if (ddlBodega.SelectedValue.Trim().Length > 0 && ddlProducto.SelectedValue.Trim().Length > 0)
                    ValidarSaldo();

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }

        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            bool anulado = false;

            if (Convert.ToBoolean(Session["editarDetalle"]) == false)
            {
                try
                {
                    var dato = gvLista.Rows[e.RowIndex].FindControl("Hfregistro") as HiddenField;

                    listaRegistros.RemoveAll(x => x.registro == Convert.ToInt16(dato.Value));
                    this.gvLista.DataSource = listaRegistros.OrderBy(x => x.registro).ToList();
                    this.gvLista.DataBind();
                }
                catch (Exception ex)
                {
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + limpiarMensaje(ex.Message), "I");
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

        }
        public void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {

                bool anulado = false;

                if (Convert.ToBoolean(Session["editarDetalle"]) == false)
                    Session["editarDetalle"] = true;
                else
                {
                    ManejoError("Debe de agregar el registro seleccionado para continuar", "I");
                    return;
                }
                if (anulado == true)
                {
                    Session["editarDetalle"] = false;
                    ManejoError("Registro anulado no es posible su edición", "I");
                    return;
                }

                CargaDestinos();
                CargarCentroCosto();
                CargarUmedida();
                cargarBodega();

                var hfRegistro = gvLista.SelectedRow.FindControl("hfRegistro") as HiddenField;
                registro = hfRegistro.Value;
                var resultado = listaRegistros.Find(y => y.registro == Convert.ToInt32(registro));

                txtFiltroProducto.Text = resultado.idItem ?? "";
                mFiltroProducto();
                this.ddlProducto.SelectedValue = resultado.idItem;

                this.ddlBodega.SelectedValue = resultado.idBodega ?? "";
                if (ddlBodega.SelectedValue.Trim().Length > 0 && ddlProducto.SelectedValue.Trim().Length > 0)
                    ValidarSaldo();

                this.ddlDestino.SelectedValue = resultado.idDestino ?? "";
                this.ddlCcosto.SelectedValue = resultado.idCcosto ?? "";
                this.ddlUmedida.SelectedValue = resultado.idUmedida ?? "";
                txtDetalle.Text = resultado.nota ?? "";
                ddlTipoReferencia.SelectedValue = resultado.tipoReferencia ?? "";
                cargarNumeroReferencia();
                ddlNumeroReferencia.SelectedValue = resultado.numeroReferencia ?? "";
                ddlTercero.SelectedValue = resultado.idTercero ?? "";
                txtDetalle.Text = resultado.nota ?? "";
                txvCantidad.Text = resultado.cantidad.ToString("N2");

                listaRegistros.Remove(resultado);
                this.gvLista.DataSource = listaRegistros;
                this.gvLista.DataBind();

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

            }
            catch
            {
            }
        }
        protected void gvTransaccion_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Session["editarDetalle"] = false;
            this.Session["editar"] = true;
            this.Session["transaccion"] = null;
            bool anulado = false, aprobado = false, cerrado = false;

            foreach (Control objControl in gvTransaccion.Rows[e.RowIndex].Cells[8].Controls)
            {
                anulado = ((CheckBox)objControl).Checked;
            }

            if (anulado == true)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Registro anulado no es posible su edición", "w");
                string vtn = "window.open('Detalle.aspx?numero=" + gvTransaccion.Rows[e.RowIndex].Cells[2].Text.Trim() + "&tipo=" + gvTransaccion.Rows[e.RowIndex].Cells[1].Text.Trim() + "','Detalle de transacción','scrollbars=yes,resizable=yes','height=200', 'width=300')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
                return;
            }

            foreach (Control objControl in gvTransaccion.Rows[e.RowIndex].Cells[9].Controls)
            {
                aprobado = ((CheckBox)objControl).Checked;
            }

            if (aprobado == true)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Registro aprobado no es posible su edición", "w");
                return;
            }

            foreach (Control objControl in gvTransaccion.Rows[e.RowIndex].Cells[10].Controls)
            {
                cerrado = ((CheckBox)objControl).Checked;
            }

            if (cerrado == true)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Registro cerrado no es posible su edición", "w");
                string vtn = "window.open('Detalle.aspx?numero=" + gvTransaccion.Rows[e.RowIndex].Cells[2].Text.Trim() + "&tipo=" + gvTransaccion.Rows[e.RowIndex].Cells[1].Text.Trim() + "','Detalle de transacción','scrollbars=yes,resizable=yes','height=200', 'width=300')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
                return;
            }

            try
            {
                if (periodo.RetornaPeriodoCerrado(Convert.ToInt16(gvTransaccion.Rows[e.RowIndex].Cells[3].Text), Convert.ToInt16(gvTransaccion.Rows[e.RowIndex].Cells[4].Text), Convert.ToInt16(Session["empresa"])) == 1)
                {
                    MostrarMensaje("Periodo cerrado contable. No es posible editar transacciones");
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

                if (tipoTransaccion.RetornaReferenciaTipoTransaccion(this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text, Convert.ToInt16(Session["empresa"])) == 1)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Transacción con referencia no es posible su edición", "w");
                    string vtn = "window.open('Detalle.aspx?numero=" + gvTransaccion.Rows[e.RowIndex].Cells[2].Text.Trim() + "&tipo=" + gvTransaccion.Rows[e.RowIndex].Cells[1].Text.Trim() + "','Detalle de transacción','scrollbars=yes,resizable=yes','height=200', 'width=300')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
                    return;
                }

                if (transacciones.VerificaEdicionBorrado(this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, Convert.ToInt16(Session["empresa"])) != 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Transacción ejecutada no es posible su edición", "w");
                    string vtn = "window.open('Detalle.aspx?numero=" + gvTransaccion.Rows[e.RowIndex].Cells[2].Text.Trim() + "&tipo=" + gvTransaccion.Rows[e.RowIndex].Cells[1].Text.Trim() + "','Detalle de transacción','scrollbars=yes,resizable=yes','height=200', 'width=300')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
                    return;
                }

                CargarTipoTransaccion();

                CargarCombos();
                upRegistro.Visible = true;
                upCabeza.Visible = true;
                CcontrolesUsuario.HabilitarControles(this.upRegistro.Controls);
                CcontrolesUsuario.HabilitarControles(this.upCabeza.Controls);
                this.ddlTipoDocumento.SelectedValue = this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text;
                this.ddlTipoDocumento.Enabled = false;
                this.txtNumero.Text = this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text;
                this.txtNumero.Enabled = false;
                this.nilbNuevo.Visible = false;
                CargaDestinos();
                CargaProductos();

                cadena = tipoTransaccion.TipoTransaccionConfig(ddlTipoDocumento.SelectedValue, Convert.ToInt16(Session["empresa"])).ToString();

                CcontrolesUsuario.LimpiarControles(this.upCabeza.Controls);
                CcontrolesUsuario.ComportamientoCampoEntidad(this.upCabeza.Controls, "iTransaccion", Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"]));
                CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
                CcontrolesUsuario.ComportamientoCampoEntidad(this.upDetalle.Controls, "iTransaccionDetalle", Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"]));
                CcontrolesUsuario.HabilitarControles(this.upCabeza.Controls);
                CcontrolesUsuario.HabilitarControles(this.upDetalle.Controls);


                object[] objCab = new object[] { Convert.ToInt16(Session["empresa"]), this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[1].Text };
                foreach (DataRowView encabezado in CentidadMetodos.EntidadGetKey("iTransaccion", "ppa", objCab).Tables[0].DefaultView)
                {
                    this.txtFecha.Visible = true;
                    ddlSolicitante.SelectedValue = encabezado.Row.ItemArray.GetValue(11).ToString();
                    this.txtFecha.Text = Convert.ToDateTime(encabezado.Row.ItemArray.GetValue(8)).ToShortDateString();
                    this.txtObservacion.Text = Convert.ToString(encabezado.Row.ItemArray.GetValue(13));
                }

                if (!Convert.ToBoolean(TipoTransaccionConfig(5)))
                {
                    ddlTipoReferencia.Visible = false;
                    ddlNumeroReferencia.Visible = false;
                    lblReferencia.Visible = false;
                }
                else
                    CargarTipoReferencia();

                listaRegistros = null;

                listaRegistros = CentidadMetodos.EntidadGetKey("iTransaccionDetalle", "ppa", objCab).Tables[0].DefaultView.Table.AsEnumerable()
                    .Select(y => new Cregistros
                    {
                        idItem = y.Field<string>("producto").ToString(),
                        nombreItem = y.Field<string>("nombreProducto").ToString(),
                        cantidad = Convert.ToDecimal(y.Field<double>("cantidadRequerida").ToString()),
                        idUmedida = Server.HtmlDecode(y.Field<string>("uMedida")),
                        idDestino = y.Field<string>("destino"),
                        idCcosto = y.Field<string>("ccosto"),
                        registro = y.Field<int>("registro"),
                        detalle = y.Field<string>("detalle"),
                        idBodega = y.Field<string>("bodega"),
                        nombreDestino = y.Field<string>("nombreDestino"),
                        nombreCcosto = y.Field<string>("nombreCcosto"),
                        nombreUmedida = y.Field<string>("nombreUmedia"),
                        nombreBodega = y.Field<string>("nombreBodega"),
                        nombreTercero = y.Field<string>("nombreTercero"),
                        nota = y.Field<string>("detalle"),
                        idTercero = y.Field<string>("tercero"),
                        tipoReferencia = y.Field<string>("tipoReferencia"),
                        numeroReferencia = y.Field<string>("numeroReferencia")

                    }).ToList();

                this.gvLista.DataSource = listaRegistros;
                this.gvLista.DataBind();

                this.btnRegistrar.Visible = true;
                this.niimbConsulta.Enabled = false;
                this.niimbRegistro.Enabled = true;
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la transacción. Correspondiente a: " + ex.Message, "A");
            }

            TabRegistro();
            txtFiltroProducto.Visible = ddlProducto.Visible;
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
        protected void txtFiltroProducto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                mFiltroProducto();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar productos debido a:  " + ex.Message, "I");
            }
        }
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            decimal saldo = txvSaldo.Text.Trim().Length > 0 ? Convert.ToDecimal(txvSaldo.Text) : 0;
            try
            {

                if (ddlCcosto.SelectedValue.Trim().Length == 0)
                {
                    this.ManejoError("Debe seleccionar un centro de costo válido. No es posible registrar la transacción", "I");
                    return;
                }

                if (txtFecha.Text.Trim().Length == 0)
                {
                    ManejoError("Seleccione una fecha para fecha para continuar", "I");
                    return;
                }

                if (Convert.ToDecimal(txvCantidad.Text) == 0)
                {
                    ManejoError("Ingrese una cantidad válida", "I");
                    return;
                }


                if (ddlBodega.Visible & ddlBodega.SelectedValue.Trim().Length == 0)
                {
                    this.ManejoError("Seleccione una bodega válida", "I");
                    return;
                }


                if (ddlDestino.Visible & ddlDestino.SelectedValue.Trim().Length == 0)
                {
                    this.ManejoError("Seleccione una destino válido", "I");
                    return;
                }

                if (ddlProducto.Visible & ddlProducto.SelectedValue.Trim().Length == 0)
                {
                    this.ManejoError("Seleccione un producto válido", "I");
                    return;
                }

                if (ddlUmedida.Visible & ddlUmedida.SelectedValue.Trim().Length == 0)
                {
                    this.ManejoError("Seleccione una unidad de medida válida", "I");
                    return;
                }

                if (Convert.ToBoolean(TipoTransaccionConfig(4)) == true)
                {

                    foreach (DataRowView drv in compruebaSaldo())
                    {
                        saldo = Convert.ToDecimal(drv.Row.ItemArray.GetValue(0).ToString());
                    }

                    if (saldo == 0)
                    {
                        this.ManejoError("No hay saldo del producto por favor verifique", "I");
                        return;
                    }

                    if (saldo < Convert.ToDecimal(txvCantidad.Text))
                    {
                        this.ManejoError("No se puede requerir mas de lo que hay en saldo", "I");
                        return;
                    }

                }


                if (Convert.ToString(this.ddlTipoDocumento.SelectedValue).Trim().Length == 0 || this.txtNumero.Text.Trim().Length == 0)
                {
                    ManejoError("Debe ingresar tipo y número de transacción", "I");
                    return;
                }
                if (Convert.ToDecimal(txvCantidad.Text) <= 0)
                {
                    this.ManejoError("La cantidad no puede ser igual o menor que cero. Por favor corrija", "I");
                    return;
                }

                if (ddlNumeroReferencia.Visible)
                {
                    if (ddlNumeroReferencia.SelectedValue.Length == 0)
                    {
                        MostrarMensaje("Debe seleccionar un numero de orden de trabajo");
                        return;
                    }
                }

                if (chkTercero.Checked)
                {
                    if (ddlTercero.SelectedValue.Length == 0)
                    {
                        MostrarMensaje("Debe seleccionar un tercero");
                        return;
                    }
                }

                string tercero = ddlTercero.SelectedValue.Length > 0 ? ddlTercero.SelectedValue : null;
                string nombreTercero = ddlTercero.SelectedValue.Length > 0 ? ddlTercero.SelectedItem.ToString() : null;

                if (listaRegistros == null)
                    listaRegistros = new List<Cregistros>();


                if (!Convert.ToBoolean(Session["editarDetalle"]))
                    registro = listaRegistros.Count() == 0 ? "1" : (listaRegistros.Max(r => r.registro) + 1).ToString();

                Session["editarDetalle"] = false;


                listaRegistros.Add(new Cregistros
                {
                    idItem = Convert.ToString(this.ddlProducto.SelectedValue),
                    cantidad = Convert.ToDecimal(txvCantidad.Text),
                    idUmedida = Server.HtmlDecode(Convert.ToString(this.ddlUmedida.SelectedValue)),
                    idDestino = Convert.ToString(this.ddlDestino.SelectedValue),
                    idCcosto = Convert.ToString(this.ddlCcosto.SelectedValue),
                    registro = Convert.ToInt32(registro),
                    detalle = Convert.ToString(this.ddlProducto.SelectedItem.ToString()),
                    nombreCcosto = Convert.ToString(this.ddlCcosto.SelectedItem),
                    nombreDestino = Convert.ToString(this.ddlDestino.SelectedItem),
                    nombreUmedida = Convert.ToString(this.ddlUmedida.SelectedItem),
                    nombreBodega = Convert.ToString(this.ddlBodega.SelectedItem.Text),
                    idBodega = Convert.ToString(this.ddlBodega.SelectedValue),
                    nombreItem = Convert.ToString(this.ddlProducto.SelectedItem),
                    idTercero = tercero,
                    nombreTercero = nombreTercero,
                    nota = txtDetalle.Text.Length > 0 ? txtDetalle.Text : null,
                    check = true,
                    tipoReferencia = ddlTipoReferencia.Visible ? ddlTipoReferencia.SelectedValue : null,
                    numeroReferencia = ddlNumeroReferencia.Visible ? ddlNumeroReferencia.SelectedValue : null

                });

                this.gvLista.DataSource = listaRegistros;
                this.gvLista.DataBind();

                this.ddlProducto.Focus();
                CargaProductos();
                CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
                CcontrolesUsuario.LimpiarCombos(this.upDetalle.Controls);
                Session["editarDetalle"] = false;

                ddlTercero.Visible = chkTercero.Checked;
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
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los saldos por bodega debido a:    " + limpiarMensaje(ex.Message), "I");
            }
        }
        protected void chkTercero_CheckedChanged(object sender, EventArgs e)
        {
            ddlTercero.Visible = chkTercero.Checked;
            CargarFuncionarios();
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
        protected void ddlTipoReferencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlTipoReferencia.SelectedValue.Trim().Length == 0)
                {
                    ddlNumeroReferencia.Items.Clear();
                    return;
                }
                cargarNumeroReferencia();
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        #endregion Eventos
    }
}