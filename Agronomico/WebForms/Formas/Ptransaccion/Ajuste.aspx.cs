using Agronomico.WebForms.App_Code.Administracion;
using Agronomico.WebForms.App_Code.General;
using Agronomico.WebForms.App_Code.Transaccion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agronomico.WebForms.Formas.Ptransaccion
{
    public partial class Ajuste : BasePage
    {
        #region Instancias

        Ctransacciones transacciones = new Ctransacciones();
        CtipoTransaccion tipoTransaccion = new CtipoTransaccion();
        Coperadores operadores = new Coperadores();
        Cperiodos periodo = new Cperiodos();
        Citems item = new Citems();
        Cnovedad Cnovedad = new Cnovedad();

        #endregion Instancias

        #region Metodos



        private void cancelarTransaccion()
        {
            InHabilitaEncabezado();
            CcontrolesUsuario.InhabilitarControles(this.upCabeza.Controls);
            CcontrolesUsuario.LimpiarControles(this.upCabeza.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upReferencia.Controls);
            CcontrolesUsuario.LimpiarControles(this.upReferencia.Controls);
            this.Session["transaccion"] = null;
            this.lbRegistrar.Visible = false;
            this.gvReferencia.DataSource = null;
            this.gvReferencia.DataBind();
            this.lbCancelar.Visible = false;
            upCabeza.Visible = false;
            upReferencia.Visible = false;
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
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al editar la transacción. Correspondiente a: " + ex.Message, "A");
            }
        }

        private void GuardaReferencia()
        {

            decimal vigencia = 0;
            bool verificacion = false;
            string numero = "", tercero = "", periodo = Convert.ToString(Convert.ToDateTime(txtFecha.Text).Year) +
                Convert.ToString(Convert.ToDateTime(txtFecha.Text).Month).PadLeft(2, '0').Trim(), bodega = "", terceroSucursal = "";
            bool verificacionCK = false;

            try
            {
                if (this.gvReferencia.Rows.Count == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar un registro para guardar la transacción", "warning");
                    return;
                }

                if (Convert.ToString(this.ddlTipoDocumento.SelectedValue).Trim().Length == 0 || Server.HtmlDecode(this.txtNumero.Text.Trim()).Length == 0 || Server.HtmlDecode(this.txtFecha.Text.Trim()).Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Campos vacios en el encabezado. Por favor corrija", "warning");
                    return;
                }

                if (this.ddlNovedad.Enabled == true)
                {
                    if (Convert.ToString(this.ddlNovedad.SelectedValue).Trim().Length == 0)
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
                        CerroresGeneral.ManejoError(this, GetType(), "Debe ingresar el valor unitario de por lo menos un artículo para continuar", "warning");
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
                        CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar al menos un producto para guardar la transacción", "warning");
                        return;
                    }


                    if (this.ddlNovedad.Enabled == false)
                        tercero = null;
                    else
                        tercero = Convert.ToString(this.ddlNovedad.SelectedValue);

                    if (this.ddlUmedida.Enabled == false)
                        terceroSucursal = null;
                    else
                        terceroSucursal = Convert.ToString(this.ddlUmedida.SelectedValue);

                    bodega = null;

                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                    {
                        this.txtNumero.Enabled = false;
                        numero = this.txtNumero.Text.Trim();
                        object[] objKey = new object[] { Convert.ToInt16(this.Session["empresa"]), numero, ddlTipoDocumento.SelectedValue };
                        CentidadMetodos.EntidadInsertUpdateDelete("iTransaccionDetalle", "elimina", "ppa", objKey);
                        CentidadMetodos.EntidadInsertUpdateDelete("iTransaccionImpuesto", "elimina", "ppa", objKey);
                        CentidadMetodos.EntidadInsertUpdateDelete("iTransaccion", "elimina", "ppa", objKey);
                    }
                    else
                    {
                        if (this.txtNumero.Enabled == false)
                            numero = transacciones.RetornaNumeroTransaccion(Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(this.Session["empresa"]));
                        else
                            numero = this.txtNumero.Text.Trim();
                    }

                    string tipo = ddlTipoDocumento.SelectedValue;



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
                                    null,//@referencia
                                    null,//@signo
                                    null,//@solicitante
                                    terceroSucursal,//@surcursalTercero
                                    tercero,//@tercero
                                    ddlTipoDocumento.SelectedValue,//@tipo
                                    this.Session["usuario"].ToString(),//@usuario
                                    null,//@usuarioAbrobado
                                    null,//@usuarioAnulado
                                    vigencia//@vigencia
                                };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("iTransaccion", "inserta", "ppa", objValores))
                    {
                        case 0:
                            foreach (GridViewRow registro in this.gvReferencia.Rows)
                            {
                                if (((CheckBox)registro.FindControl("chkSeleccion")).Checked == true)
                                {
                                    if (Convert.ToDecimal(registro.Cells[9].Text) != 0 && Convert.ToDecimal(((TextBox)registro.FindControl("txtCantidad")).Text) > 0)
                                    {

                                        object[] objValoresCuerpo = new object[]{
                                                      Convert.ToDateTime(txtFecha.Text).Year,//@año
                                                      false,//@anulado
                                                      false,//@aprobado
                                                      bodega,//@bodega
                                                     Convert.ToDecimal(((TextBox)registro.FindControl("txtCantidad")).Text),//@cantidad
                                                      Convert.ToDecimal(((TextBox)registro.FindControl("txtCantidad")).Text),//@cantidadRequerida
                                                      null,//@ccosto
                                                      false,//@cerrado
                                                      null,//@destino
                                                      registro.Cells[2].Text,//@detalle
                                                      false,//@ejecutado
                                                      Convert.ToInt16(Session["empresa"]),//@empresa
                                                      null,//@fechaAnulado
                                                      null,//@fechaAprobado
                                                      null,//@fechaCierre
                                                      registro.Cells[1].Text,//@item
                                                      Convert.ToDateTime(txtFecha.Text).Month,//@mes
                                                      numero,//@numero
                                                      registro.Cells[11].Text,//@numeroReferencia
                                                      Convert.ToDecimal(((TextBox)registro.FindControl("txtDescuento")).Text),//@pDescuento
                                                      periodo,//@periodo
                                                      registro.RowIndex,//@registro
                                                      Convert.ToDecimal(((TextBox)registro.FindControl("txtCantidad")).Text),//@saldo
                                                      ddlTipoDocumento.SelectedValue,//@tipo
                                                      registro.Cells[10].Text,//@tipoReferencia
                                                      registro.Cells[4].Text,//@uMedida
                                                      null,//@usuarioAnulado
                                                      null,//@usuarioAprobado
                                                      null,//@usuarioCierre
                                                      Convert.ToDecimal(registro.Cells[8].Text),//@valorDescuento
                                                      0,//@valorSaldo
                                                      Convert.ToDecimal(registro.Cells[9].Text),//@valorTotal
                                                      Convert.ToDecimal(((TextBox)registro.FindControl("txtValorUnitario")).Text)//@valorUnitario
                                                };

                                        switch (CentidadMetodos.EntidadInsertUpdateDelete("iTransaccionDetalle", "inserta", "ppa", objValoresCuerpo))
                                        {
                                            case 1:
                                                verificacion = true;
                                                break;
                                        }

                                    }
                                }
                            }
                            break;
                        case 1:
                            verificacion = true;
                            break;
                    }

                    if (verificacion == false)
                    {
                        if (Convert.ToBoolean(this.Session["editar"]) == true)
                            ts.Complete();
                        else
                        {
                            transacciones.ActualizaConsecutivo(ddlTipoDocumento.SelectedValue, Convert.ToInt16(this.Session["empresa"]));
                            ts.Complete();
                        }
                        ManejoExito("Datos registrados satisfactoriamente número " + this.Session["numerotransaccion"].ToString(), "I");
                    }
                    else
                        ManejoError("Error al eliminar la novedad registrada", "E");

                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar la transacción. Correspondiente a: " + ex.Message, "I");
            }
        }

        private void validaFecha()
        {
            this.txtFecha.Visible = true;
            try
            {
                Convert.ToDateTime(txtFecha.Text);
                ddlNovedad.Focus();
            }
            catch
            {
                txtFecha.Text = "";
                txtFecha.Focus();
                CerroresGeneral.ManejoError(this, GetType(), "formato de fecha no valido..", "warning");
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
                    CerroresGeneral.ManejoError(this, GetType(), "Transacción existente. Por favor corrija", "warning");
                    return;
                }



                CcontrolesUsuario.ComportamientoCampoEntidad(this.upCabeza.Controls, "iTransaccionDetalle", Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(this.Session["empresa"]));

                if (Convert.ToBoolean(TipoTransaccionConfig(14)) == true)
                {
                    try
                    {
                        DataView dvProveedor = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cxpProveedor", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                        dvProveedor.RowFilter = "entradaDirecta = 1 and empresa=" + Session["empresa"].ToString();
                        this.ddlNovedad.DataSource = dvProveedor;
                        this.ddlNovedad.DataValueField = "codigo";
                        this.ddlNovedad.DataTextField = "cadena";
                        this.ddlNovedad.DataBind();
                        this.ddlNovedad.Items.Insert(0, new ListItem("", ""));
                    }
                    catch (Exception ex)
                    {
                        ManejoError("Error al cargar proveedores habilitados para orden directa. Correspondiente a: " + ex.Message, "C");
                    }
                }

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
            CcontrolesUsuario.InhabilitarControles(this.upCabeza.Controls);
            CcontrolesUsuario.LimpiarControles(this.upCabeza.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upReferencia.Controls);
            CcontrolesUsuario.LimpiarControles(this.upReferencia.Controls);
            upCabeza.Visible = false;
            InHabilitaEncabezado();
            this.Session["transaccion"] = null;
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
            upReferencia.Visible = false;
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
                this.ddlTipoDocumento.DataSource = transacciones.GetTipoTransaccionModulo(Convert.ToInt32(this.Session["empresa"]));
                this.ddlTipoDocumento.DataValueField = "codigo";
                this.ddlTipoDocumento.DataTextField = "descripcion";
                this.ddlTipoDocumento.DataBind();
                this.ddlTipoDocumento.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
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
            this.Session["transaccion"] = null;
            this.Session["impuesto"] = null;


        }
        private string ConsecutivoTransaccion()
        {
            string numero = "";

            try
            {
                numero = transacciones.RetornaNumeroTransaccion(Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(this.Session["empresa"]));
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
                object[] objkey = new object[] { Convert.ToInt16(this.Session["empresa"]), this.txtNumero.Text, Convert.ToString(this.ddlTipoDocumento.SelectedValue) };

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
                CcontrolesUsuario.LimpiarControles(this.upReferencia.Controls);
                this.gvReferencia.DataSource = null;
                this.gvReferencia.DataBind();
                this.Session["transaccion"] = null;
                this.txtNumero.Text = ConsecutivoTransaccion();


                //if (Convert.ToBoolean(TipoTransaccionConfig(15)) == false)
                //{
                //    CerroresGeneral.ManejoError(this, GetType(), "Transacción no habilitada para registro directo", "warning");
                //    return;
                //}

                ComportamientoConsecutivo();

                upCabeza.Visible = true;
                tbCabeza.Visible = true;
                txtFecha.Visible = true;
                cargarNovedades();
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
        }

        protected void niimbConsulta_Click(object sender, EventArgs e)
        {
            this.upRegistro.Visible = false;
            this.upCabeza.Visible = false;
            this.upReferencia.Visible = false;
            this.Session["transaccion"] = null;
            this.gvReferencia.DataSource = null;
            this.gvReferencia.DataBind();
        }

        #endregion Eventos


        protected void ddlTercero_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cargarUnidadMedida(ddlNovedad.SelectedValue);
                ddlUmedida.Focus();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar documentos referencia. Correspondiente a: " + ex.Message, "C");
            }
        }

        private void cargarNovedades()
        {
            try
            {
                DataView dvNovedad = tipoTransaccion.SeleccionaNovedadTipoDocumentos(ddlTipoDocumento.SelectedValue, Convert.ToInt32(Session["empresa"]));
                dvNovedad.RowFilter = "claseLabor=1 and empresa =" + Session["empresa"].ToString();
                this.ddlNovedad.DataSource = dvNovedad;
                this.ddlNovedad.DataValueField = "codigo";
                this.ddlNovedad.DataTextField = "descripcion";
                this.ddlNovedad.DataBind();
                this.ddlNovedad.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar Novedades. Correspondiente a: " + ex.Message, "C");
            }


            try
            {
                this.ddlFinca.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("aFinca", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlFinca.DataValueField = "codigo";
                this.ddlFinca.DataTextField = "descripcion";
                this.ddlFinca.DataBind();
                this.ddlFinca.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar fincas. Correspondiente a: " + ex.Message, "C");
            }

        }

        private object NovedadConfig(int posicion, string novedad)
        {

            object retorno = null;
            string cadena;
            char[] comodin = new char[] { '*' };
            int indice = posicion + 1;
            try
            {
                cadena = Cnovedad.NovedadConfig(novedad, Convert.ToInt32(Session["empresa"])).ToString();
                retorno = cadena.Split(comodin, indice).GetValue(posicion - 1);
                return retorno;
            }
            catch (Exception ex)
            {
                ManejoError("Error al recuperar posición de configuración del lote. Correspondiente a: " + ex.Message, "C");
                return null;
            }
        }

        private void cargarUnidadMedida(string tercero)
        {
            try
            {
                DataView dvSucursal = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("gUnidadMedida", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlUmedida.DataSource = dvSucursal;
                this.ddlUmedida.DataValueField = "codigo";
                this.ddlUmedida.DataTextField = "descripcion";
                this.ddlUmedida.DataBind();
                this.ddlUmedida.Items.Insert(0, new ListItem("", ""));
                this.ddlUmedida.SelectedValue = "";

                ddlUmedida.SelectedValue = NovedadConfig(4, tercero).ToString().Trim();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar sucursal del tercero. Correspondiente a: " + ex.Message, "C");
            }
        }
        protected void nibtnCargar_Click(object sender, EventArgs e)
        {

            string requi = "";
            bool verifica = false;
            try
            {
                if (verifica == false)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar minimo una requisición", "warning");
                    this.gvReferencia.DataSource = null;
                    this.gvReferencia.DataBind();
                    return;
                }

                if (ddlNovedad.SelectedValue.Length == 0 || ddlUmedida.SelectedValue.Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar proveedor y sucursal, antes de cargar datos", "warning");
                    return;
                }


            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar detalle de Transacción. Correspondiente a: " + ex.Message, "C");
            }
        }

        protected void txtFecha_TextChanged(object sender, EventArgs e)
        {
            validaFecha();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtFechaInicial.Text.Length == 0 || txtFechaFinal.Text.Length == 0 || ddlFinca.SelectedValue.Length == 0)
                {
                    ManejoError("Campos vacios de fecha y finca, por favor corrija", "w");
                    return;
                }
                upReferencia.Visible = true;
                this.gvReferencia.DataSource = transacciones.SeleccionaAjusteTrabajadores(Convert.ToDateTime(txtFechaInicial.Text),
                    Convert.ToDateTime(txtFechaFinal.Text), ddlFinca.SelectedValue, Convert.ToInt32(Session["empresa"]));
                this.gvReferencia.DataBind();
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        protected void txtFechaInicial_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFechaInicial.Text);

            }
            catch
            {
                txtFechaInicial.Text = "";
                txtFechaInicial.Focus();
                CerroresGeneral.ManejoError(this, GetType(), "formato de fecha no valido..", "warning");
                return;
            }
        }

        protected void txtFechaFinal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFechaFinal.Text);

            }
            catch
            {
                txtFechaFinal.Text = "";
                txtFechaFinal.Focus();
                CerroresGeneral.ManejoError(this, GetType(), "formato de fecha no valido..", "warning");
                return;
            }
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (ddlFinca.SelectedValue.Length == 0 || txtFecha.Text.Length == 0 || ddlNovedad.SelectedValue.Length == 0)
            {
                MostrarMensaje("Campos vacios por favor corrija");
                return;
            }

            bool validar = false;
            foreach (GridViewRow gv in gvReferencia.Rows)
            {
                if (((CheckBox)gv.FindControl("chkSeleccion")).Checked)
                    validar = true;
            }

            if (validar == false)
            {
                MostrarMensaje("Debe seleccionar por lo menos un trabajador, por favor corrija");
                return;
            }

            Guardar();
        }

        protected void Guardar()
        {
            string operacion = "inserta";
            bool verifica = false;
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    string numerotransaccion = null;

                    numerotransaccion = transacciones.RetornaNumeroTransaccion(ddlTipoDocumento.SelectedValue, Convert.ToInt16(this.Session["empresa"]));
                    this.Session["numerotransaccion"] = numerotransaccion;

                    DateTime fecha = Convert.ToDateTime(txtFecha.Text), fechaF = Convert.ToDateTime(txtFecha.Text);
                    string referencia = null, remision = null;

                    object[] objValo = new object[]{
                                                     false, // @anulado	bit
                                                     Convert.ToUInt32(Convert.ToDateTime(txtFecha.Text).Year), //@año	int
                                                     Convert.ToInt32(this.Session["empresa"]),   //@empresa	int
                                                     fecha,   //@fecha	date
                                                     DateTime.Now,  //@fechaAnulado	datetime
                                                     DateTime.Now,  //@fechaFinal	date
                                                     DateTime.Now,   //@fechaRegistro	datetime
                                                     Convert.ToUInt32(fecha.Month),  //@mes	int
                                                     numerotransaccion,   //@numero	varchar
                                                     txtObservacion.Text,   //@observacion	varchar
                                                     referencia,   //@referencia	varchar
                                                     remision,   //@remision	varchar
                                                     ddlTipoDocumento.SelectedValue.Trim(),   //@tipo	varchar
                                                     null,   //@usuarioAnulado	varchar
                                                     this.Session["usuario"].ToString()   //@usuarioRegistro	varchar
                                          };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccion", operacion, "ppa", objValo))
                    {
                        case 0:

                            decimal precioLaborTercero = 0, valorTotal = 0, cantidadNovedad = 0;
                            DateTime fechaDetalle;

                            foreach (GridViewRow gv in gvReferencia.Rows)
                            {
                                if (((CheckBox)gv.FindControl("chkSeleccion")).Checked)
                                {
                                    valorTotal += Convert.ToDecimal(gv.Cells[6].Text);
                                    cantidadNovedad += 1;
                                }
                            }

                            object[] objValoresNovedad = new object[]{
                                Convert.ToInt32(Convert.ToDateTime(txtFecha.Text).Year),   //@año
                                                cantidadNovedad, //@cantidad
                                                false,   //@ejecutado
                                                Convert.ToInt16(this.Session["empresa"]),     //@empresa
                                                fecha,    //@fecha
                                                ddlFinca.SelectedValue,//finca
                                                0,  //@jornales
                                                null,    //@lote
                                                Convert.ToInt32(fecha.Month.ToString()) ,  //@mes
                                                ddlNovedad.SelectedValue,    //@novedad
                                                numerotransaccion,    //@numero
                                                0, //@pesoRacimo
                                                valorTotal,
                                                0,    //@racimos
                                                0,   //@registro
                                                0,
                                                0,
                                                1,    //@saldo
                                                null,   //@seccion
                                                ddlTipoDocumento.SelectedValue.Trim(),    //@tipo
                                                ddlUmedida.SelectedValue //@uMedida
                                         };

                            switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionNovedad", "inserta", "ppa", objValoresNovedad))
                            {
                                case 0:
                                    foreach (GridViewRow gv in gvReferencia.Rows)
                                    {
                                        if (((CheckBox)gv.FindControl("chkSeleccion")).Checked)
                                        {

                                            precioLaborTercero = Convert.ToDecimal(gv.Cells[6].Text.Trim());
                                            fechaDetalle = Convert.ToDateTime(gv.Cells[1].Text);

                                            object[] objValoresTercero = new object[]{
                                                                    Convert.ToInt32(Convert.ToDateTime(txtFecha.Text).Year),  //@año
                                                                    1, //@cantidad
                                                                    false, //@ejecutado
                                                                    Convert.ToInt16(this.Session["empresa"]), //@empresa
                                                                    fechaDetalle,
                                                                    ddlFinca.SelectedValue,
                                                                    0, //@jornales
                                                                    null,//@lote
                                                                    Convert.ToInt32( fechaDetalle.Month.ToString()),//@mes
                                                                    ddlNovedad.SelectedValue,//@novedad
                                                                    numerotransaccion, //@numero
                                                                    precioLaborTercero,
                                                                    0,
                                                                    gv.RowIndex,//@registro
                                                                    0,//@registroNovedad
                                                                    1,//@saldo
                                                                    null,//@seccion
                                                                    gv.Cells[2].Text.Trim(),//@tercero
                                                                    ddlTipoDocumento.SelectedValue.Trim(),//@tipo
                                                                    null//@cuadrilla
                                                            };
                                            switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionTercero", "inserta", "ppa", objValoresTercero))
                                            {
                                                case 1:
                                                    ManejoError("Error al eliminar la novedad registrada", "E");
                                                    verifica = true;
                                                    break;
                                            }
                                        }
                                    }
                                    break;
                                case 1:
                                    ManejoError("Error al eliminar la novedad registrada", "E");
                                    verifica = true;
                                    break;
                            }
                            break;
                        case 1:
                            ManejoError("Error al eliminar la novedad registrada", "E");
                            verifica = true;
                            break;
                    }

                    if (verifica == false)
                    {
                        if (Convert.ToBoolean(this.Session["editar"]) == true)
                            ts.Complete();
                        else
                        {
                            transacciones.ActualizaConsecutivo(ddlTipoDocumento.SelectedValue, Convert.ToInt16(this.Session["empresa"]));
                            ts.Complete();
                        }
                        ManejoExito("Datos registrados satisfactoriamente número " + this.Session["numerotransaccion"].ToString(), "I");
                    }
                    else
                        ManejoError("Error al eliminar la novedad registrada", "E");
                }
            }

            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        protected void chbTotal_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow registro in this.gvReferencia.Rows)
            {
                ((CheckBox)registro.Cells[0].FindControl("chkSeleccion")).Checked = ((CheckBox)sender).Checked;
            }
        }
    }
}