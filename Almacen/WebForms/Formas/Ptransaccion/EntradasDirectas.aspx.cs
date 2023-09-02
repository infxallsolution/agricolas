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
    public partial class EntradasDirectas : BasePage
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
        public List<CtransaccionAlmacen> ListaTransaccion
        {
            get { return this.Session["transaccion"] != null ? (List<CtransaccionAlmacen>)this.Session["transaccion"] : null; }
            set { Session["transaccion"] = value; }
        }

        #endregion Instancias

        #region Metodos


        private void ImprimriTransaccion(string empresa, string tipo, string numero)
        {
            string vtn = "window.open('../Pinformes/ImprimeTransaccion.aspx?empresa=" + empresa + "&tipo=" + tipo + "&numero=" + numero + "','Impresión de Formatos','scrollbars=yes,resizable=yes','height=300', 'width=400')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", vtn, true);
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
        private void BusquedaTransaccion()
        {
            try
            {
                if (this.gvParametros.Rows.Count > 0)
                {
                    string where = operadores.FormatoWhere((List<Coperadores>)Session["operadores"]);

                    this.gvTransaccion.Visible = true;
                    this.gvTransaccion.DataSource = transacciones.GetTransaccionEntradasDirectas(where, Convert.ToInt16(Session["empresa"]));
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


            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.lbRegistrar.Visible = false;
            this.lbCancelar.Visible = false;
            upDetalle.Visible = false;
            upCabeza.Visible = false;
            upConsulta.Visible = false;
            this.Session["operadores"] = null;
            Session["editarDetalle"] = false;
            this.Session["impuestos"] = null;
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
                            int i = 0;
                            foreach (var cuerpo in ListaTransaccion)
                            {

                                if (Convert.ToBoolean(TipoTransaccionConfig(14)) == true)
                                    cantidadAprobada = cuerpo.Cantidad;
                                else
                                    cantidadAprobada = 0;

                                object[] objValoresCuerpo = new object[]{

                                    false,//@anulado
                                     Convert.ToDateTime(txtFecha.Text).Year,//@año
                                                      false,//@aprobado
                                                      cuerpo.Bodega,//@bodega
                                                      cuerpo.Cantidad,//@cantidad
                                                      cuerpo.Cantidad ,//@cantidadRequerida
                                                      cuerpo.Ccosto,//@ccosto
                                                      false,//@cerrado
                                                      null,
                                                      cuerpo.Destino,//@destino
                                                      cuerpo.Detalle,//@detalle
                                                      false,//@ejecutado
                                                      Convert.ToInt16(Session["empresa"]),//@empresa
                                                      null,//@fechaAnulado
                                                      null,//@fechaAprobado
                                                      null,//@fechaCierre
                                                      cuerpo.Producto,//@item
                                                      Convert.ToDateTime(txtFecha.Text).Month,//@mes
                                                      numero,//@numero
                                                      null,//@numeroReferencia
                                                      0,//@pDescuento
                                                      periodo,//@periodo
                                                      0,
                                                      i,//@registro
                                                      cuerpo.Cantidad ,//@saldo
                                                      cuerpo.Tercero, //@tercero
                                                      ddlTipoDocumento.SelectedValue,//@tipo
                                                      null,//@tipoReferencia
                                                      cuerpo.Umedida ,//@uMedida
                                                      usuarioAnulado,//@usuarioAnulado
                                                      null,//@usuarioAprobado
                                                      null,//@usuarioCierre
                                                       cuerpo.ValorTotal,//@valorBruto
                                                      0,//@valorDescuento
                                                      0,//valorimpuesto
                                                      cuerpo.ValorTotal,//@valorNeto
                                                      0,//@valorSaldo
                                                      cuerpo.ValorTotal,//@valorTotal
                                                      0, //valortotalveta
                                                      cuerpo.ValorUnitario,//@valorUnitario
                                                      0//valounitarioventa
                                                };

                                switch (CentidadMetodos.EntidadInsertUpdateDelete("iTransaccionDetalle", "inserta", "ppa", objValoresCuerpo))
                                {
                                    case 1:
                                        verificacion = false;
                                        break;
                                }

                                i++;
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
                                            verificacion = false;
                                            break;
                                    }

                                }
                            }

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
            

           

        }
        private void cargarBodega()
        {
            try
            {


                DataView dvBodega = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("iBodega", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                dvBodega.RowFilter = "activo=1 and mexistencias=1 and empresa=" + Session["empresa"].ToString();
                this.ddlBodega.DataSource = dvBodega;
                this.ddlBodega.DataValueField = "codigo";
                this.ddlBodega.DataTextField = "descripcion";
                this.ddlBodega.DataBind();
                this.ddlBodega.Items.Insert(0, new ListItem("", ""));

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar bodegas. Correspondiente a: " + ex.Message, "C");
            }
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
        private void CargarCombos()
        {
            try
            {
                var dvUmedida = CentidadMetodos.EntidadGet("gUnidadMedida", "ppa").Tables[0].DefaultView.Table.AsEnumerable().Where(x => x.Field<int>("empresa") == Convert.ToInt16(Session["empresa"])).Select(
                    x => new { codigo = x.Field<string>("codigo"), descripcion = x.Field<string>("descripcion") }
                    ).OrderBy(x => x.descripcion);

                this.ddlUmedida.DataSource = dvUmedida.ToList();
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
                this.ddlTipoDocumento.DataSource = tipoTransaccion.GetTipoTransaccionModuloForma(Convert.ToInt16(Session["empresa"]), 10);
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
            this.btnRegistrar.Enabled = true;
            CcontrolesUsuario.ComportamientoCampoEntidad(this.upCabeza.Controls, "iTransaccion", Convert.ToString(this.ddlTipoDocumento.SelectedValue), (int)this.Session["empresa"]);
            CcontrolesUsuario.ComportamientoCampoEntidad(this.upDetalle.Controls, "iTransaccionDetalle", Convert.ToString(this.ddlTipoDocumento.SelectedValue), (int)this.Session["empresa"]);
            txtFiltroProveedor.Visible = ddlTercero.Visible;
            txtFiltroProveedor.Enabled = ddlTercero.Enabled;
            txtFiltroProveedor.ReadOnly = false;
            txtFiltroProducto.Visible = ddlProducto.Visible;
            txtFiltroProducto.Enabled = ddlProducto.Enabled;
            txtFiltroProducto.ReadOnly = false;
            if (Convert.ToBoolean(TipoTransaccionConfig(15)) == false)
            {
                this.btnRegistrar.Visible = false;
              
            }
            else
            {
                this.btnRegistrar.Visible = true;
            }
        }
        private void CargaProductos()
        {
            try
            {
                DataView dvProducto = item.RetornaItemsFiltro(Convert.ToInt32(this.Session["empresa"]), txtFiltroProducto.Text);
                //var resultado = dvProducto.Table.AsEnumerable().Where(y => y.Field<int>("empresa") == Convert.ToInt32(this.Session["empresa"])).Select(x => new { codigo = x.Field<string>("codigo"), descripcion = x.Field<string>("descripcion") });
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
                DataView dvCcosto = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cCentrosCostoContable", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
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
        private void cargarImpuesto()
        {
            bool validaImpuesto = false;
            List<Cimpuestos> listaImpuesto = new List<Cimpuestos>(); ;

            foreach (GridViewRow registro in this.gvLista.Rows)
            {
                validaImpuesto = false;
                DataView dvImpuesto = tipoTransaccion.SeleccionaSoloImpuestoItemTercero(Convert.ToInt16(Session["empresa"]), ddlSucursal.SelectedValue, Convert.ToInt16(ddlTercero.SelectedValue), Convert.ToInt16(registro.Cells[2].Text), Convert.ToDecimal(registro.Cells[8].Text));

                foreach (DataRowView impuesto in dvImpuesto)
                {
                    foreach (Cimpuestos nt in listaImpuesto)
                    {
                        if (impuesto.Row.ItemArray.GetValue(0).ToString() == nt.Concepto)
                        {
                            validaImpuesto = true;
                            break;
                        }
                    }


                    if (validaImpuesto == false)
                    {
                        transaccionImpuesto = new Cimpuestos(concepto: impuesto.Row.ItemArray.GetValue(0).ToString(),
                            nombreConcepto: impuesto.Row.ItemArray.GetValue(1).ToString(),
                            baseGravable: Convert.ToDecimal(impuesto.Row.ItemArray.GetValue(2)),
                           baseMinima: Convert.ToDecimal(impuesto.Row.ItemArray.GetValue(3)),
                           clase: impuesto.Row.ItemArray.GetValue(4).ToString(),
                           tasa: Convert.ToDecimal(impuesto.Row.ItemArray.GetValue(5)),
                           valoBruto: Convert.ToDecimal(impuesto.Row.ItemArray.GetValue(6)),
                            valorImpuesto: Convert.ToDecimal(impuesto.Row.ItemArray.GetValue(7)));

                        listaImpuesto.Add(transaccionImpuesto);
                    }
                    else
                    {
                        listaImpuesto.FirstOrDefault(x => x.Concepto == impuesto.Row.ItemArray.GetValue(0).ToString()).ValorBruto += Convert.ToDecimal(impuesto.Row.ItemArray.GetValue(6));
                        listaImpuesto.FirstOrDefault(x => x.Concepto == impuesto.Row.ItemArray.GetValue(0).ToString()).ValorImpuesto += Convert.ToDecimal(impuesto.Row.ItemArray.GetValue(7));
                    }
                }
            }
            gvImpuesto.Visible = true;
            this.gvImpuesto.DataSource = listaImpuesto;
            this.gvImpuesto.DataBind();

            decimal baseimpuesto = 0, totalimpuesto = 0;
            foreach (GridViewRow gvr in gvImpuesto.Rows)
            {
                totalimpuesto += Convert.ToDecimal(gvr.Cells[5].Text);
            }
            nitxtTotalImpuesto.Text = totalimpuesto.ToString("N2");

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
                nitxtTotalImpuesto.Text = "0";
                decimal totalimpuestos = 0;
                foreach (GridViewRow registro in this.gvLista.Rows)
                {
                    this.nitxtTotalValorBruto.Text = ListaTransaccion.Sum(x => x.ValorTotal).ToString("N2");
                }
                nitxtTotalImpuesto.Text = totalimpuestos.ToString("N2");
                decimal valorBruto = 0;
                this.nitxtTotal.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(nitxtTotalImpuesto.Text) + Convert.ToDecimal(nitxtTotalValorBruto.Text) - Convert.ToDecimal(this.nitxtTotalDescuento.Text)).ToString("N2"));
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
                        CargarCombos();
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
                this.Session["impuestos"] = null;
                this.gvLista.DataSource = null;
                this.gvLista.DataBind();
                this.Session["transaccion"] = null;
                this.txtNumero.Text = ConsecutivoTransaccion();
                this.hdTransaccionConfig.Value = CcontrolesUsuario.TipoTransaccionConfig(this.ddlTipoDocumento.SelectedValue, Convert.ToInt16(Session["empresa"]));

                ComportamientoConsecutivo();
                ComportamientoTransaccion();
                CargaDestinos();
                CargarCentroCosto();
                cargarBodega();

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
                    int reg = Convert.ToInt16(((HiddenField)gvLista.Rows[e.RowIndex].FindControl("hfRegistro")).Value);
                    ListaTransaccion.RemoveAll(x => x.Registro == reg);
                    this.gvLista.DataSource = ListaTransaccion;
                    this.gvLista.DataBind();

                    if (ListaTransaccion.Count == 0)
                    {
                        gvImpuesto.DataSource = null;
                        gvImpuesto.DataBind();
                    }

                    CcontrolesUsuario.LimpiarControles(upDetalle.Controls);
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

                this.hdCantidad.Value = this.gvLista.SelectedRow.Cells[8].Text;
                this.hdRegistro.Value = ((HiddenField)(this.gvLista.SelectedRow.FindControl("hfRegistro"))).Value;
                CargarCombos();
                CargaProductos();
                var resultado = ListaTransaccion.First(x => x.Registro == Convert.ToInt16(hdRegistro.Value));

                if (resultado != null)
                {
                    if (resultado.Producto != "&nbsp;")
                    {
                        this.ddlProducto.SelectedValue = resultado.Producto;
                        txtFiltroProducto.Text = resultado.Producto;
                    }
                    else
                        this.ddlProducto.SelectedValue = "";

                    if (resultado.Ccosto != "&nbsp;")
                        this.ddlCcosto.SelectedValue = resultado.Ccosto;
                    else
                        this.ddlCcosto.SelectedValue = "";

                    if (resultado.Destino != "&nbsp;")
                        this.ddlDestino.SelectedValue = resultado.Destino;
                    else
                        this.ddlDestino.SelectedValue = "";

                    if (resultado.Cantidad.ToString() != "&nbsp;")
                    {
                        txvCantidad.Text = resultado.Cantidad.ToString("N2");
                        this.Session["cant"] = resultado.Cantidad;
                    }
                    else
                        txvCantidad.Text = "0";

                    if (resultado.ValorUnitario.ToString() != "&nbsp;")
                    {
                        txvValorUnitario.Text = resultado.ValorUnitario.ToString("N2");
                        this.Session["valor"] = resultado.ValorUnitario;
                    }
                    else
                        txvValorUnitario.Text = "0";

                    if (resultado.ValorTotal.ToString() != "&nbsp;")
                    {
                        txvValorTotal.Text = resultado.ValorTotal.ToString("N2");
                    }
                    else
                        txvValorTotal.Text = "0";

                    if (resultado.Umedida != "&nbsp;")
                        this.ddlUmedida.SelectedValue = Server.HtmlDecode(resultado.Umedida);
                    else
                        this.ddlUmedida.SelectedValue = "";

                    if (resultado.Bodega != "&nbsp;")
                        this.ddlBodega.SelectedValue = resultado.Bodega;
                    else
                        this.ddlBodega.SelectedValue = "";

                    if (resultado.Nota != "&nbsp;")
                        this.txtDetalle.Text = Server.HtmlDecode(resultado.Nota);
                    else
                        this.txtDetalle.Text = "";

                    //txtFiltroProducto.Enabled = false;

                    ListaTransaccion.Remove(resultado);
                    this.gvLista.DataSource = ListaTransaccion;
                    this.gvLista.DataBind();

                    txvValorUnitario.Enabled = true;
                    ddlCcosto.Enabled = true;
                    ddlDestino.Enabled = true;
                    ddlProducto.Enabled = true;
                    ddlUmedida.Enabled = true;
                    TotalizaGrillaReferencia();
                }

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

                if (this.gvParametros.Rows.Count == 0)
                    this.imbBusqueda.Visible = false;

                cargarImpuesto();

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
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                bool oc = false; decimal valorTotal = 0, valorUnitario = 0;

                if (hfocd.Value != "&nbsp;" & hfocd.Value != "")
                {
                    oc = Convert.ToBoolean(hfocd.Value);

                    if (hfCantidadOCR.Value != "&nbsp;" & oc == true)
                    {
                        if (Convert.ToDecimal(hfCantidadOCR.Value) < Convert.ToDecimal(txvCantidad.Text))
                        {
                            MostrarMensaje("La cantidad ingresada no puede ser mayor a la de la orden de compra");
                            return;
                        }
                    }
                }

                if (ListaTransaccion != null)
                    if (ListaTransaccion.Exists(y => y.Producto.Equals(this.ddlProducto.SelectedValue)))
                    {
                        MostrarMensaje("El producto seleccionado ya se encuentra registrado. Por favor corrija");
                        return;
                    }

                if (Convert.ToString(this.ddlTipoDocumento.SelectedValue).Trim().Length == 0 ||
                    this.txtNumero.Text.Trim().Length == 0)
                {
                    MostrarMensaje("Debe ingresar tipo y número de transacción");
                    return;
                }

                if (ddlTercero.SelectedValue.Length == 0 || ddlSucursal.SelectedValue.Length == 0)
                {
                    MostrarMensaje("Debe seleccionar proveedor y sucursal, antes de cargar datos");
                    return;
                }

                if (ddlProducto.SelectedValue.Length == 0)
                {
                    MostrarMensaje("Debe seleccionar un producto...");
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
                int registro = ListaTransaccion == null || ListaTransaccion.Count==0 ? 0 : ListaTransaccion.Max(r => r.Registro) + 1;

                transaccionAlmacen = new CtransaccionAlmacen()
                {
                    Bodega = ddlBodega.SelectedValue.ToString().Trim(),
                    nombreBodega = ddlBodega.SelectedItem.Text,
                    Producto = ddlProducto.SelectedValue.Trim(),
                    NombreProducto = ddlProducto.SelectedItem.Text,
                    Cantidad = Math.Truncate(Convert.ToDecimal(txvCantidad.Text) * 100) / 100,
                    Umedida = Server.HtmlDecode(ddlUmedida.SelectedValue.Trim()),
                    ValorUnitario = valorUnitario,
                    Destino = ddlDestino.SelectedValue.Trim().Length == 0 ? null : ddlDestino.SelectedValue.Trim(),
                    NombreDestino = ddlDestino.SelectedValue.Trim().Length == 0 ? null : ddlDestino.SelectedItem.Text,
                    Ccosto = ddlCcosto.SelectedValue.Trim().Length == 0 ? null : ddlCcosto.SelectedValue.Trim(),
                    nombreCentroCosto = ddlCcosto.SelectedValue.Trim().Length == 0 ? null : ddlCcosto.SelectedItem.Text,
                    ValorTotal = valorTotal,
                    Detalle = ddlProducto.SelectedItem.Text.Trim(),
                    Registro = registro,
                    Anulado = false,
                    Nota = Server.HtmlDecode(this.txtDetalle.Text.Trim())
                };

                if (ListaTransaccion == null)
                {
                    ListaTransaccion = new List<CtransaccionAlmacen>();
                }

                ListaTransaccion.Add(transaccionAlmacen);
                int registror = ListaTransaccion.Max(r => r.Registro) + 1;
                hdRegistro.Value = registror.ToString();

                ListaTransaccion.OrderBy(r => r.Registro);
                this.gvLista.DataSource = ListaTransaccion;
                this.gvLista.DataBind();
                this.ddlProducto.Focus();
                CargaProductos();
                Session["editarDetalle"] = false;
                hfCantidadOCR.Value = "";
                hfocd.Value = "";
                txvValorUnitario.Enabled = true;
                ddlCcosto.Enabled = true;
                ddlDestino.Enabled = true;
                ddlProducto.Enabled = true;
                ddlUmedida.Enabled = true;
                CcontrolesUsuario.LimpiarControles(upDetalle.Controls);
                TotalizaGrillaReferencia();
                hdRegistro.Value = "";
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
        protected void ddlBodega_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var ddlBodega = (DropDownList)sender;
                GridViewRow gvr = (GridViewRow)ddlBodega.Parent.Parent;
                int re = Convert.ToInt16(((HiddenField)gvr.FindControl("hfRegistro")).Value);
                ListaTransaccion.FirstOrDefault(d => d.Registro == re).Bodega = ddlBodega.SelectedValue.Trim();
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