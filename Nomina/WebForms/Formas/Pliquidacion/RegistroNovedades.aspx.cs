using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
using Nomina.WebForms.App_Code.Liquidacion;
using Nomina.WebForms.App_Code.Transaccion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Pliquidacion
{
    public partial class RegistroNovedades : BasePage
    {

        #region Instancias




        Coperadores operadores = new Coperadores();
        CtipoTransaccion tipoTransaccion = new CtipoTransaccion();
        CtransaccionNovedad transaccionNovedad = new CtransaccionNovedad();
        Cperiodos periodo = new Cperiodos();
        string numerotransaccion = "";
        Ctransacciones transacciones = new Ctransacciones();
        Cconceptos conceptos = new Cconceptos();
        Cfuncionarios funcionario = new Cfuncionarios();
        Cgeneral general = new Cgeneral();

        #endregion Instancias

        #region Metodos



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

        private void ManejoExito(string mensaje, string operacion)
        {
            CcontrolesUsuario.InhabilitarControles(this.upCabeza.Controls);
            CcontrolesUsuario.LimpiarControles(this.upCabeza.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upDetalle.Controls);
            CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);

            InHabilitaEncabezado();

            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.Session["transaccion"] = null;
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            this.lbRegistrar.Visible = false;
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
                this.ddlTipoDocumento.DataSource = tipoTransaccion.GetTipoTransaccionModulo(Convert.ToInt16(Session["empresa"]));
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
        private void CargaCampos()
        {
            try
            {
                this.niddlCampo.DataSource = transacciones.GetCamposEntidades("nNovedades", "");
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
        private void CargarEmpleados()
        {
            try
            {
                DataView dvTerceroCCosto = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nFuncionario", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlEmpleado.DataSource = dvTerceroCCosto;
                this.ddlEmpleado.DataValueField = "tercero";
                this.ddlEmpleado.DataTextField = "descripcion";
                this.ddlEmpleado.DataBind();
                this.ddlEmpleado.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar empleados. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                DataView dvTerceroCCosto = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nFuncionario", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                //if (Convert.ToBoolean(Session["editar"]) != true)
                //    dvTerceroCCosto.RowFilter = "empresa = " + Convert.ToInt16(Session["empresa"]).ToString() + " and ccosto = '" + ddlCentroCosto.SelectedValue.ToString() + "'";

                this.ddlEmpleadoDetalle.DataSource = dvTerceroCCosto;
                this.ddlEmpleadoDetalle.DataValueField = "tercero";
                this.ddlEmpleadoDetalle.DataTextField = "descripcion";
                this.ddlEmpleadoDetalle.DataBind();
                this.ddlEmpleadoDetalle.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar empleados. Correspondiente a: " + ex.Message, "C");
            }
        }
        protected void cargarCombosDetalle()
        {
            try
            {
                DataView dvConceptosNoFijos = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nConcepto", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                dvConceptosNoFijos.RowFilter = "empresa = " + Convert.ToInt16(Session["empresa"]).ToString() + " and fijo=0 and ausentismo=0";
                this.ddlConceptoDetalle.DataSource = dvConceptosNoFijos;
                this.ddlConceptoDetalle.DataValueField = "codigo";
                this.ddlConceptoDetalle.DataTextField = "descripcion";
                this.ddlConceptoDetalle.DataBind();
                this.ddlConceptoDetalle.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar conceptos. Correspondiente a: " + ex.Message, "C");
            }

        }
        private void CargarCombos()
        {

            //try
            //{
            //    DataView ccosto = general.CentroCosto(true, Convert.ToInt16(Session["empresa"]));
            //    this.ddlCentroCosto.DataSource = ccosto;
            //    this.ddlCentroCosto.DataValueField = "codigo";
            //    this.ddlCentroCosto.DataTextField = "descripcion";
            //    this.ddlCentroCosto.DataBind();
            //    this.ddlCentroCosto.Items.Insert(0, new ListItem("", ""));
            //}
            //catch (Exception ex)
            //{
            //    ManejoError("Error al cargar los centros de costo. Correspondiente a: " + ex.Message, "C");
            //}

            try
            {
                DataView dvConceptosNoFijos = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nConcepto", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                dvConceptosNoFijos.RowFilter = "empresa = " + Convert.ToInt16(Session["empresa"]).ToString() + " and fijo = 0 and ausentismo=0";
                this.ddlConcepto.DataSource = dvConceptosNoFijos;
                this.ddlConcepto.DataValueField = "codigo";
                this.ddlConcepto.DataTextField = "descripcion";
                this.ddlConcepto.DataBind();
                this.ddlConcepto.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar conceptos. Correspondiente a: " + ex.Message, "C");
            }


            try
            {

                this.ddlEmpleado.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nFuncionario", "ppa"),
                    "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlEmpleado.DataValueField = "tercero";
                this.ddlEmpleado.DataTextField = "descripcion";
                this.ddlEmpleado.DataBind();
                this.ddlEmpleado.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar empleados. Correspondiente a: " + ex.Message, "C");
            }




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
        private string ConsecutivoTransaccion()
        {
            string numero = "";

            try
            {
                numero = transacciones.RetornaNumeroTransaccion(
                    Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al obtener el número de transacción. Correspondiente a: " + ex.Message, "C");
            }

            return numero;
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
        private void BusquedaTransaccion()
        {
            try
            {
                if (this.gvParametros.Rows.Count > 0)
                {
                    string where = operadores.FormatoWhere(
                        (List<Coperadores>)Session["operadores"]);

                    this.gvTransaccion.DataSource = transacciones.GetTransaccionCompleta(where, Convert.ToInt16(Session["empresa"]));
                    this.gvTransaccion.DataBind();

                    this.nilblRegistros.Text = "Nro. Registros " + Convert.ToString(this.gvTransaccion.Rows.Count);

                    EstadoInicialGrillaTransacciones();
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al procesar la consulta de transacciones. Correspondiente a: " + ex.Message, "C");
            }
        }
        private int CompruebaTransaccionExistente()
        {
            try
            {
                object[] objkey = new object[] { Convert.ToInt16(Session["empresa"]), this.txtNumero.Text, Convert.ToString(this.ddlTipoDocumento.SelectedValue) };

                if (CentidadMetodos.EntidadGetKey("aTransaccion", "ppa", objkey).Tables[0].DefaultView.Count > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
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
                        MostrarMensaje("Transacción existente. Por favor corrija");

                        return;
                    }
                }

                this.txtNumero.Enabled = false;

                CcontrolesUsuario.HabilitarControles(
                    this.upCabeza.Controls);

                this.nilbNuevo.Visible = false;
                this.txtFecha.Visible = false;
                this.txtFecha.Focus();
            }
        }
        private void verificaPeriodoCerrado(int año, int mes, int empresa, DateTime fecha)
        {
            if (periodo.RetornaPeriodoCerradoNomina(año, mes, Convert.ToInt16(Session["empresa"]), fecha) == 1)
            {
                ManejoError("Periodo cerrado. No es posible realizar nuevas transacciones", "I");
                return;
            }
        }
        private void Editar()
        {

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {

                    DateTime fecha = Convert.ToDateTime(txtFecha.Text);
                    bool verificacion = true;
                    string conceptos = null, empleado = null, ccosto = null;
                    string remision = null;


                    if (txtRemision.Visible == true)
                        remision = txtRemision.Text;
                    if (ddlConcepto.Visible == true)
                        conceptos = ddlConcepto.SelectedValue;
                    if (ddlEmpleado.Visible == true)
                        empleado = ddlEmpleado.SelectedValue;

                    //if (ddlCentroCosto.Visible == true)
                    //    ccosto = ddlCentroCosto.SelectedValue;

                    bool anulado = false;


                    switch (transacciones.EditaEncabezado(this.ddlTipoDocumento.SelectedValue.Trim(), this.txtNumero.Text.Trim(), fecha, empleado, conceptos, ccosto, remision,
                        Server.HtmlDecode(this.txtObservacion.Text.Trim()), Convert.ToInt16(Session["empresa"])))
                    {
                        case 0:

                            object[] objValoDeleteTerceroNovedad = new object[]{
                                 Convert.ToInt16(Session["empresa"]) ,  //@empresa	int
                                 this.txtNumero.Text.Trim(),    //@numero	varchar
                                ddlTipoDocumento.SelectedValue.Trim(),  //@tipo	varchar
                                     };
                            switch (CentidadMetodos.EntidadInsertUpdateDelete("nNovedadesDetalle", "elimina", "ppa", objValoDeleteTerceroNovedad))
                            {
                                case 1:
                                    verificacion = false;
                                    break;
                            }



                            foreach (GridViewRow gv in this.gvLista.Rows)
                            {
                                foreach (Control c in gv.Cells[15].Controls)
                                {
                                    if (c is CheckBox)
                                    {
                                        anulado = ((CheckBox)c).Checked;
                                    }
                                }

                                object[] objValoresDetalle = new object[]{
                                                 anulado,        //  @anulado	bit
                                                  Convert.ToInt16(gv.Cells[9].Text),        //@añoFinal	int
                                                     Convert.ToInt16(gv.Cells[8].Text),       //@añoInicial	int
                                                     Convert.ToDecimal(gv.Cells[6].Text),    //@cantidad	int
                                                      gv.Cells[2].Text,    //@concepto	varchar
                                                      Server.HtmlDecode(gv.Cells[13].Text.ToString().Trim()),   //@detalle	varchar
                                                        gv.Cells[4].Text,    //@empleado	varchar
                                                       Convert.ToInt16(this.Session["empresa"]),  //@empresa	int
                                                         Convert.ToInt16(gv.Cells[12].Text), //@frecuencia	int
                                                       false,    //@liquidada	bit
                                                         this.txtNumero.Text.Trim(),  //@numero	varchar
                                                        Convert.ToInt16(gv.Cells[11].Text), //@periodoFinal	int
                                                       Convert.ToInt16(gv.Cells[10].Text),  //@periodoInicial	int
                                                      gv.RowIndex,  //@registro	int
                                                      ddlTipoDocumento.SelectedValue.Trim(),  //@tipo	varchar
                                                       null,   //@ultimoPeriodoFrecuencia	int
                                                          null,  //@ultimoPeriodoLiquidado	int
                                                      Convert.ToDecimal(gv.Cells[7].Text)   //@valor	money

                                         };

                                switch (CentidadMetodos.EntidadInsertUpdateDelete("nNovedadesDetalle", "inserta", "ppa", objValoresDetalle))
                                {

                                    case 1:
                                        // ManejoError("Error al insertar el detalle de la transacción", "I");
                                        verificacion = false;
                                        break;
                                }
                            }

                            if (verificacion == false)
                                MostrarMensaje("Error al editar el registro. Operación no realizada");
                            else
                            {
                                ManejoExito("Transacción editada satisfactoriamente. Transacción número " + this.txtNumero.Text.Trim(), "A");
                                ts.Complete();
                            }
                            break;

                        case 1:

                            MostrarMensaje("Error al editar el encabezado. Operación no realizada");

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al editar la transacción. Correspondiente a: " + ex.Message, "A");
            }
        }
        protected void Guardar()
        {
            string operacion = "inserta";
            bool verificaEncabezado = false;
            bool verificaDetalle = false;
            bool verificaBascula = false;
            try
            {

                using (TransactionScope ts = new TransactionScope())
                {
                    DateTime fecha = Convert.ToDateTime(txtFecha.Text);

                    string conceptos = null, empleado = null, ccosto = null;
                    string remision = null;


                    if (txtRemision.Visible == true)
                        remision = txtRemision.Text;
                    if (ddlConcepto.Visible == true)
                        conceptos = ddlConcepto.SelectedValue;
                    if (ddlEmpleado.Visible == true)
                        empleado = ddlEmpleado.SelectedValue;

                    //if (ddlCentroCosto.Visible == true)
                    //    ccosto = ddlCentroCosto.SelectedValue;

                    numerotransaccion = transacciones.RetornaNumeroTransaccion(ddlTipoDocumento.SelectedValue, Convert.ToInt16(Session["empresa"]));
                    this.Session["numerotransaccion"] = numerotransaccion;



                    object[] objValo = new object[]{       false, // @anulado	bit
                                                       ccosto,//@ccosto
                                                       conceptos,//@concepto varchar
                                                       empleado,//@empleado
                                                       Convert.ToInt16(this.Session["empresa"]),   //@empresa	int
                                                       fecha,   //@fecha	date
                                                       null,    //@fechaAnulado datetime
                                                       DateTime.Now,    //@fechaRegistro    datetime
                                                       numerotransaccion,   //@numero	varchar
                                                       txtObservacion.Text,   //@observacion	varchar
                                                       remision,   //@remision	varchar
                                                       ddlTipoDocumento.SelectedValue.Trim(),   //@tipo	varchar
                                                       null,   //@usuarioAnulado	varchar
                                                       this.Session["usuario"].ToString()   //@usuarioRegistro	varchar
                                                     
                              };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("nNovedades", operacion, "ppa", objValo))
                    {
                        case 0:

                            bool anulado = false;

                            foreach (GridViewRow gv in gvLista.Rows)
                            {
                                foreach (Control c in gv.Cells[15].Controls)
                                {
                                    if (c is CheckBox)
                                        anulado = ((CheckBox)c).Checked;
                                }


                                object[] objValoresDetalle = new object[]{
                                                 anulado,        //  @anulado	bit
                                                  Convert.ToInt16(gv.Cells[9].Text),        //@añoFinal	int
                                                     Convert.ToInt16(gv.Cells[8].Text),       //@añoInicial	int
                                                     Convert.ToDecimal(gv.Cells[6].Text),    //@cantidad	int
                                                      gv.Cells[2].Text,    //@concepto	varchar
                                                      Server.HtmlDecode(gv.Cells[13].Text.ToString().Trim()),   //@detalle	varchar
                                                        gv.Cells[4].Text,    //@empleado	varchar
                                                       Convert.ToInt16(this.Session["empresa"]),  //@empresa	int
                                                         Convert.ToInt16(gv.Cells[12].Text), //@frecuencia	int
                                                       false,    //@liquidada	bit
                                                        numerotransaccion,  //@numero	varchar
                                                        Convert.ToInt16(gv.Cells[11].Text), //@periodoFinal	int
                                                       Convert.ToInt16(gv.Cells[10].Text),  //@periodoInicial	int
                                                      gv.RowIndex,  //@registro	int
                                                      ddlTipoDocumento.SelectedValue.Trim(),  //@tipo	varchar
                                                       null,   //@ultimoPeriodoFrecuencia	int
                                                          null,  //@ultimoPeriodoLiquidado	int
                                                      Convert.ToDecimal(gv.Cells[7].Text)   //@valor	money

                                         };

                                switch (CentidadMetodos.EntidadInsertUpdateDelete("nNovedadesDetalle", operacion, "ppa", objValoresDetalle))
                                {

                                    case 1:
                                        ManejoError("Error al insertar el detalle de la transacción", "I");
                                        verificaDetalle = true;
                                        break;
                                }

                            }

                            break;

                        case 1:
                            ManejoError("Error al insertar el detalle de la transaccción", "I");
                            break;
                    }

                    if (verificaEncabezado == false & verificaDetalle == false & verificaBascula == false)
                    {
                        transacciones.ActualizaConsecutivo(ddlTipoDocumento.Text, Convert.ToInt16(Session["empresa"]));
                        ts.Complete();
                        ManejoExito("Datos registrados satisfactoriamente. Transacción número " + numerotransaccion, "I");

                    }

                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar los datos correspondiente a: " + ex.Message, operacion.Substring(0, 1).ToUpper());
            }
        }

        #endregion Metodos

        #region Evento

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {

                if (!IsPostBack)
                {
                    if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                            nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
                    {
                        ManejoError("Usuario no autorizado para ejecutar esta operación", "I");
                        return;
                    }

                    CargarCombos();
                    CargaCampos();

                    this.Session["transaccion"] = null;
                    this.Session["operadores"] = null;
                }
            }

        }
        protected void nilbNuevo_Click(object sender, EventArgs e)
        {
            this.Session["editar"] = false;
            ManejoEncabezado();
        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            InHabilitaEncabezado();

            CcontrolesUsuario.InhabilitarControles(this.upCabeza.Controls);
            CcontrolesUsuario.LimpiarControles(this.upCabeza.Controls);

            CcontrolesUsuario.InhabilitarControles(this.upDetalle.Controls);
            CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);

            this.Session["editar"] = false;

            this.Session["transaccion"] = null;
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.lbRegistrar.Visible = false;

            this.lbCancelar.Visible = false;
            upCabeza.Visible = false;
            upDetalle.Visible = false;

        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {

            if (gvLista.Rows.Count <= 0)
            {
                MostrarMensaje("El detalle de la transacción debe tener por lo menos un registro");
                return;
            }

            bool validar = false;

            if (upCabeza.Visible == true)
            {
                if (txtFecha.Enabled == true)
                {
                    if (txtFecha.Text.Trim().Length == 0)
                    {
                        MostrarMensaje("Debe seleccionar una fecha valida");
                        return;
                    }
                }

                if (ddlEmpleado.Visible == true)
                {
                    if (ddlEmpleado.SelectedValue.Trim().Length == 0)
                    {
                        MostrarMensaje("Debe seleccionar una empleado valido");

                        return;
                    }
                }
                if (ddlConcepto.Visible == true)
                {
                    if (ddlConcepto.SelectedValue.Trim().Length == 0)
                    {
                        MostrarMensaje("Debe seleccionar un concepto valido");

                        return;
                    }
                }
            }



            if (Convert.ToBoolean(Session["editar"]) == false)
            {
                Guardar();
            }
            else
            {
                Editar();
            }

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

            this.niimbAdicionar.Enabled = true;
            this.imbBusqueda.Enabled = true;

            this.Session["transaccion"] = null;
            this.Session["operadores"] = null;
            gvParametros.DataSource = null;
            gvParametros.DataBind();
            nitxtValor1.Text = "";
            nitxtValor2.Text = "";

            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            CargaCampos();


        }
        private void ComportamientoTransaccion()
        {
            upCabeza.Visible = true;
            upDetalle.Visible = true;

            CcontrolesUsuario.ComportamientoCampoEntidad(this.upCabeza.Controls, "nNovedades", Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"]));
            CcontrolesUsuario.ComportamientoCampoEntidad(this.upDetalle.Controls, "nNovedadesDetalle", Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"]));
            this.btnRegistrar.Visible = true;

        }
        protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {

                CcontrolesUsuario.InhabilitarControles(this.upCabeza.Controls);
                CcontrolesUsuario.LimpiarControles(this.upCabeza.Controls);

                CcontrolesUsuario.InhabilitarControles(this.upDetalle.Controls);
                CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);

                this.gvLista.DataSource = null;
                this.gvLista.DataBind();

                this.Session["transaccion"] = null;

                this.txtNumero.Text = ConsecutivoTransaccion();
                this.hdTransaccionConfig.Value = CcontrolesUsuario.TipoTransaccionConfig(this.ddlTipoDocumento.SelectedValue, Convert.ToInt16(Session["empresa"]));
                ComportamientoConsecutivo();
                ComportamientoTransaccion();
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
                    MostrarMensaje("Transacción existente. Por favor corrija");

                    return;
                }
            }

            CcontrolesUsuario.HabilitarControles(this.upCabeza.Controls);

            this.nilbNuevo.Visible = false;
            this.txtFecha.Visible = false;
            this.lblFecha.Focus();
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {


            try
            {
                if (this.gvLista.Rows.Count >= 100)
                {
                    MostrarMensaje("El número de artículos no puede ser mayor a 100");

                    return;
                }


                if (Convert.ToDecimal(txtAñoFinal.Text) < Convert.ToDecimal(txtAñoInicial.Text))
                {
                    MostrarMensaje("El año final no puede ser menor al inicial");

                    return;
                }
                else
                {

                    if (Convert.ToDecimal(txtAñoFinal.Text) == Convert.ToDecimal(txtAñoInicial.Text))
                    {
                        if (Convert.ToDecimal(txtPeriodoInicial.Text) > Convert.ToDecimal(txtPeriodoFinal.Text))
                        {
                            MostrarMensaje("El periodo inicial no debe ser mayor al final");

                            return;
                        }
                    }
                }

                if (Convert.ToString(this.ddlTipoDocumento.SelectedValue).Trim().Length == 0 ||
                    this.txtNumero.Text.Trim().Length == 0)
                {
                    MostrarMensaje("Debe ingresar tipo y número de transacción");
                    return;
                }
                if (CcontrolesUsuario.VerificaCamposRequeridos(this.upDetalle.Controls) == false)
                {
                    MostrarMensaje("Campos vacios. Por favor corrija");
                    return;
                }
                if (txvCantidad.Visible == true && txvCantidad.Enabled == true)
                {
                    if (Convert.ToDecimal(this.txvCantidad.Text) <= 0)
                    {
                        MostrarMensaje("La cantidad no puede ser igual o menor que cero. Por favor corrija");
                        return;
                    }
                }

                if (txvValor.Visible == true && txvValor.Enabled == true)
                {
                    if (Convert.ToDecimal(this.txvValor.Text) <= 0)
                    {
                        MostrarMensaje("El valor no puede ser igual o menor que cero. Por favor corrija");
                        return;
                    }
                }

                string concepto, empleado, nombreConcepto, nombreEmpleado;

                if (ddlEmpleado.Visible == true)
                {
                    if (ddlEmpleado.SelectedValue.Length == 0)
                    {
                        MostrarMensaje("Debe seleccionar un empleado antes de registrar");
                        return;
                    }
                    empleado = ddlEmpleado.SelectedValue;
                    nombreEmpleado = ddlEmpleado.SelectedItem.ToString();
                }
                else
                {
                    if (ddlEmpleadoDetalle.SelectedValue.Length == 0)
                    {
                        MostrarMensaje("Debe seleccionar un empleado antes de registrar");
                        return;
                    }
                    empleado = ddlEmpleadoDetalle.SelectedValue;
                    nombreEmpleado = ddlEmpleadoDetalle.SelectedItem.ToString();
                }

                if (ddlConcepto.Visible == true)
                {
                    if (ddlConcepto.SelectedValue.Length == 0)
                    {
                        MostrarMensaje("Debe seleccionar un concepto antes de registrar");
                        return;
                    }
                    concepto = ddlConcepto.SelectedValue;
                    nombreConcepto = ddlConcepto.SelectedItem.ToString();
                }
                else
                {
                    if (ddlConceptoDetalle.SelectedValue.Length == 0)
                    {
                        MostrarMensaje("Debe seleccionar un concepto antes de registrar");
                        return;
                    }
                    concepto = ddlConceptoDetalle.SelectedValue;
                    nombreConcepto = ddlConceptoDetalle.SelectedItem.ToString();
                }


                transaccionNovedad = new CtransaccionNovedad(empleado, concepto, nombreConcepto, nombreEmpleado,
                    Convert.ToDecimal(txvCantidad.Text),
                    Convert.ToInt32(Convert.ToDecimal(txtAñoInicial.Text)),
                    Convert.ToInt32(Convert.ToDecimal(txtAñoFinal.Text)),
                    Convert.ToInt32(Convert.ToDecimal(txtPeriodoInicial.Text)),
                    Convert.ToInt32(Convert.ToDecimal(txtPeriodoFinal.Text)),
                    Convert.ToInt16(Convert.ToDecimal(txvFrecuencia.Text)),
                    Convert.ToDecimal(txvValor.Text),
                    txtDetalle.Text.ToString().Trim(), Convert.ToInt16(this.hdRegistro.Value), false);

                List<CtransaccionNovedad> listaTransaccion = null;

                if (this.Session["transaccion"] == null)
                {
                    listaTransaccion = new List<CtransaccionNovedad>();
                    listaTransaccion.Add(transaccionNovedad);
                }
                else
                {
                    listaTransaccion = (List<CtransaccionNovedad>)Session["transaccion"];
                    listaTransaccion.Add(transaccionNovedad);
                }

                listaTransaccion = listaTransaccion.OrderBy(p => p.Registro).ToList();
                this.Session["transaccion"] = listaTransaccion;
                this.gvLista.DataSource = listaTransaccion;
                this.gvLista.DataBind();
                Session["editarDetalle"] = false;
                CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
                CcontrolesUsuario.LimpiarCombos(this.upDetalle.Controls);
                this.Session["editarRegistro"] = null;
                ddlConcepto.Enabled = false;
                ddlEmpleado.Enabled = false;

            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al insertar el registro. Correspondiente a: " + ex.Message);
            }

        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            bool anulado = false;
            if (Convert.ToBoolean(Session["editar"]) == false)
            {

                try
                {
                    List<CtransaccionNovedad> listaTransaccion = null;
                    listaTransaccion = (List<CtransaccionNovedad>)Session["transaccion"];
                    listaTransaccion.RemoveAt(e.RowIndex);

                    this.gvLista.DataSource = listaTransaccion;
                    this.gvLista.DataBind();

                    if (gvLista.Rows.Count == 0)
                    {
                        ddlEmpleado.Enabled = true;
                        //ddlCentroCosto.Enabled = true;
                        ddlConcepto.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al eliminar el registro. Correspondiente a: " + ex.Message);
                }
            }
            else
            {

                foreach (Control c in this.gvLista.Rows[e.RowIndex].Cells[15].Controls)
                {
                    if (c is CheckBox)
                        anulado = ((CheckBox)c).Checked;
                }
                if (anulado == true)
                {
                    MostrarMensaje("Registro anulado no es posible volver anular");

                    return;
                }

                foreach (Control c in this.gvLista.Rows[e.RowIndex].Cells[15].Controls)
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
                    MostrarMensaje("Debe de agregar el registro seleccionado para continuar");

                    return;
                }
                foreach (Control c in this.gvLista.SelectedRow.Cells[15].Controls)
                {
                    if (c is CheckBox)
                        anulado = ((CheckBox)c).Checked;
                }
                if (anulado == true)
                {
                    MostrarMensaje("Registro anulado no es posible su edición");

                    return;
                }
                this.hdCantidad.Value = this.gvLista.SelectedRow.Cells[6].Text;
                this.hdRegistro.Value = this.gvLista.SelectedRow.Cells[14].Text;
                CargarEmpleados();
                cargarCombosDetalle();

                if (this.ddlConceptoDetalle.Visible == true)
                {
                    if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                        this.ddlConceptoDetalle.SelectedValue = this.gvLista.SelectedRow.Cells[2].Text;
                }
                else
                {
                    if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                        this.ddlConcepto.SelectedValue = this.gvLista.SelectedRow.Cells[2].Text;
                }

                VerificaValoryCantidad(this.gvLista.SelectedRow.Cells[2].Text);


                if (this.ddlEmpleadoDetalle.Visible == true)
                {
                    if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                        this.ddlEmpleadoDetalle.SelectedValue = this.gvLista.SelectedRow.Cells[4].Text;
                }
                else
                {
                    if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                        this.ddlEmpleado.SelectedValue = this.gvLista.SelectedRow.Cells[4].Text;
                    ddlEmpleado.Enabled = false;
                }

                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                {
                    txvCantidad.Text = this.gvLista.SelectedRow.Cells[6].Text;
                    this.Session["cant"] = Convert.ToDecimal(this.gvLista.SelectedRow.Cells[6].Text);
                }
                else
                    txvCantidad.Text = "0";

                if (this.gvLista.SelectedRow.Cells[7].Text != "&nbsp;")
                {
                    txvValor.Text = this.gvLista.SelectedRow.Cells[7].Text;
                    this.Session["valor"] = Convert.ToDecimal(this.gvLista.SelectedRow.Cells[7].Text);
                }
                else
                    txvValor.Text = "0";

                if (this.txtAñoInicial.Visible == true)
                {
                    if (this.gvLista.SelectedRow.Cells[8].Text != "&nbsp;")
                        this.txtAñoInicial.Text = Convert.ToString(this.gvLista.SelectedRow.Cells[8].Text);
                    else
                        this.txtAñoInicial.Text = "";
                }

                if (this.txtAñoFinal.Visible == true)
                {
                    if (this.gvLista.SelectedRow.Cells[9].Text != "&nbsp;")
                        this.txtAñoFinal.Text = Convert.ToString(this.gvLista.SelectedRow.Cells[9].Text);
                    else
                        this.txtAñoFinal.Text = "";
                }


                if (this.txtPeriodoInicial.Visible == true)
                {
                    if (this.gvLista.SelectedRow.Cells[10].Text != "&nbsp;")
                        this.txtPeriodoInicial.Text = Convert.ToString(this.gvLista.SelectedRow.Cells[10].Text);
                    else
                        this.txtPeriodoInicial.Text = "";
                }

                if (this.txtPeriodoFinal.Visible == true)
                {
                    if (this.gvLista.SelectedRow.Cells[11].Text != "&nbsp;")
                        this.txtPeriodoFinal.Text = Convert.ToString(this.gvLista.SelectedRow.Cells[11].Text);
                    else
                        this.txtPeriodoFinal.Text = "";
                }

                if (this.gvLista.SelectedRow.Cells[12].Text != "&nbsp;")
                    this.txvFrecuencia.Text = this.gvLista.SelectedRow.Cells[12].Text;
                else
                    this.txvFrecuencia.Text = "0";

                if (this.gvLista.SelectedRow.Cells[13].Text != "&nbsp;")
                    this.txtDetalle.Text = Server.HtmlDecode(Convert.ToString(this.gvLista.SelectedRow.Cells[13].Text));
                else
                    this.txtDetalle.Text = "";

                List<CtransaccionNovedad> listaTransaccion = null;

                listaTransaccion = (List<CtransaccionNovedad>)this.Session["transaccion"];
                listaTransaccion.RemoveAt(this.gvLista.SelectedRow.RowIndex);

                this.gvLista.DataSource = listaTransaccion;
                this.gvLista.DataBind();

            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar los campos del registro en el formulario. Correspondiente a: " + ex.Message);
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
        protected void niddlOperador_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.niddlOperador.SelectedValue.ToString() == "between")
            {
                this.nitxtValor2.Visible = true;
            }
            else
            {
                this.nitxtValor2.Visible = false;
                this.nitxtValor1.Text = "";
            }

            this.nitxtValor1.Focus();
        }
        protected void niimbAdicionar_Click(object sender, EventArgs e)
        {
            if (niddlCampo.SelectedValue.Length == 0 || nitxtValor1.Text.Length == 0)
            {
                MostrarMensaje("Campos vacios por favor corrija");
                return;
            }

            foreach (GridViewRow registro in this.gvParametros.Rows)
            {
                if (Convert.ToString(this.niddlCampo.SelectedValue) == registro.Cells[1].Text && Convert.ToString(this.niddlOperador.SelectedValue) == Server.HtmlDecode(registro.Cells[2].Text) && this.nitxtValor1.Text == registro.Cells[3].Text)
                    return;
            }


            operadores = new Coperadores(Convert.ToString(this.niddlCampo.SelectedValue), Server.HtmlDecode(Convert.ToString(this.niddlOperador.SelectedValue)),
                this.nitxtValor1.Text, this.nitxtValor2.Text);
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
        protected void nitxtValor1_TextChanged(object sender, EventArgs e)
        {
            if (this.nitxtValor1.Text.Length > 0 && Convert.ToString(this.niddlCampo.SelectedValue).Length > 0)
            {
                this.niimbAdicionar.Enabled = true;
                this.imbBusqueda.Enabled = true;
            }
            else
            {
                this.niimbAdicionar.Enabled = false;
                this.imbBusqueda.Enabled = false;
            }

            this.niimbAdicionar.Focus();
        }
        protected void imbBusqueda_Click(object sender, EventArgs e)
        {
            BusquedaTransaccion();
        }
        protected void gvTransaccion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    bool anulado = false;

                    foreach (Control objControl in gvTransaccion.Rows[e.RowIndex].Cells[7].Controls)
                    {
                        anulado = ((CheckBox)objControl).Checked;
                    }

                    if (anulado == true)
                    {
                        MostrarMensaje("Registro anulado no es posible su edición");
                        return;
                    }

                    if (tipoTransaccion.RetornaTipoBorrado(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, Convert.ToInt16(Session["empresa"])) == "E")
                    {
                        object[] objValores = new object[]{
                         Convert.ToInt16(Session["empresa"]),
                        Convert.ToString(this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text).Trim(),
                        Convert.ToString(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text).Trim()
                    };

                        switch (CentidadMetodos.EntidadInsertUpdateDelete("nNovedadesDetalle", "elimina", "ppa", objValores))
                        {
                            case 0:
                                switch (CentidadMetodos.EntidadInsertUpdateDelete("nNovedades", "elimina", "ppa", objValores))
                                {
                                    case 0:
                                        MostrarMensaje("Registro Eliminado Satisfactoriamente");
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
                        switch (transacciones.AnulaTransaccion(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text,
                            this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text, this.Session["usuario"].ToString().Trim(), Convert.ToInt16(Session["empresa"])))
                        {
                            case 0:

                                MostrarMensaje("Registro Anulado Satisfactoriamente");
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
        protected void gvTransaccion_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            decimal cantidad = 0;
            string empleado, concepto, nombreEmpleado, nombreConcepto;
            this.Session["editar"] = true;
            this.Session["periodo"] = this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text;
            this.Session["transaccion"] = null;
            Session["editarDetalle"] = false;
            bool anulado = false;

            foreach (Control objControl in gvTransaccion.Rows[e.RowIndex].Cells[7].Controls)
            {
                anulado = ((CheckBox)objControl).Checked;
            }

            if (anulado == true)
            {
                MostrarMensaje("Registro anulado no es posible su edición");
                return;
            }

            DateTime fecha = Convert.ToDateTime(this.gvTransaccion.Rows[e.RowIndex].Cells[4].Text);

            if (periodo.RetornaPeriodoCerrado(fecha.Year, fecha.Month, Convert.ToInt16(Session["empresa"])) == 1)
            {
                ManejoError("Periodo cerrado contable. No es posible editar transacciones", "I");
                return;
            }

            try
            {
                this.hdTransaccionConfig.Value = CcontrolesUsuario.TipoTransaccionConfig(this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text, Convert.ToInt16(Session["empresa"]));
                if (transacciones.VerificaEdicionBorrado(this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[4].Text, Convert.ToInt16(Session["empresa"])) != 0)
                {
                    MostrarMensaje("Transacción ejecutada no es posible su edición");
                    return;
                }

                CargarTipoTransaccion();
                cargarCombosDetalle();
                CargarCombos();
                upCabeza.Visible = true;
                CcontrolesUsuario.HabilitarControles(this.upCabeza.Controls);
                CcontrolesUsuario.HabilitarControles(this.upRegistro.Controls);
                this.ddlTipoDocumento.SelectedValue = this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text;
                this.ddlTipoDocumento.Enabled = false;
                this.txtNumero.Text = this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text;
                this.txtNumero.Enabled = false;
                this.nilbNuevo.Visible = false;
                this.hdTransaccionConfig.Value = CcontrolesUsuario.TipoTransaccionConfig(this.ddlTipoDocumento.SelectedValue, Convert.ToInt16(Session["empresa"]));
                CcontrolesUsuario.LimpiarControles(this.upCabeza.Controls);
                CcontrolesUsuario.ComportamientoCampoEntidad(this.upCabeza.Controls, "nNovedades", Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"]));
                CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
                CcontrolesUsuario.ComportamientoCampoEntidad(this.upDetalle.Controls, "nNovedadesDetalle", Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"]));

                object[] objCab = new object[] { Convert.ToInt16(Session["empresa"]), this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text };

                foreach (DataRowView encabezado in CentidadMetodos.EntidadGetKey("nNovedades", "ppa", objCab).Tables[0].DefaultView)
                {
                    this.txtFecha.Visible = true;
                    this.txtFecha.Text = Convert.ToDateTime(encabezado.Row.ItemArray.GetValue(3)).ToShortDateString();
                    //ddlCentroCosto.SelectedValue = Convert.ToString(encabezado.Row.ItemArray.GetValue(5));
                    //ddlCentroCosto.Enabled = false;
                    lblFecha.Enabled = false;
                    txtFecha.Enabled = false;
                    this.txtObservacion.Text = Convert.ToString(encabezado.Row.ItemArray.GetValue(8));
                    this.ddlEmpleado.SelectedValue = Convert.ToString(encabezado.Row.ItemArray.GetValue(6));
                    ddlEmpleado.Enabled = false;
                    this.ddlConcepto.SelectedValue = Convert.ToString(encabezado.Row.ItemArray.GetValue(7));
                    ddlConcepto.Enabled = false;
                }

                foreach (DataRowView detalle in transacciones.SeleccionanNovedadesDetalle(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text, Convert.ToInt16(Session["empresa"])))
                {
                    cantidad = Convert.ToDecimal(detalle.Row.ItemArray.GetValue(6));

                    if (detalle.Row.ItemArray.GetValue(5) == null)
                    {
                        empleado = null;
                        nombreEmpleado = null;
                    }
                    else
                    {
                        empleado = Convert.ToString(detalle.Row.ItemArray.GetValue(5));
                        nombreEmpleado = Convert.ToString(detalle.Row.ItemArray.GetValue(20));
                    }

                    if (detalle.Row.ItemArray.GetValue(4) == null)
                    {
                        concepto = null;
                        nombreConcepto = null;
                    }
                    else
                    {
                        concepto = Convert.ToString(detalle.Row.ItemArray.GetValue(4));
                        nombreConcepto = Convert.ToString(detalle.Row.ItemArray.GetValue(21));
                    }
                    int añoFinal = 0;

                    if (detalle.Row.ItemArray.GetValue(17).ToString().Trim().Length > 0)
                        añoFinal = Convert.ToInt16(detalle.Row.ItemArray.GetValue(17));

                    if (detalle.Row.ItemArray.GetValue(17).ToString().Trim().Length > 0)
                        añoFinal = Convert.ToInt16(detalle.Row.ItemArray.GetValue(17));

                    transaccionNovedad = new CtransaccionNovedad(empleado, concepto, nombreConcepto, nombreEmpleado, cantidad, Convert.ToInt16(detalle.Row.ItemArray.GetValue(8)), añoFinal,
                        Convert.ToInt16(detalle.Row.ItemArray.GetValue(9)), Convert.ToInt16(detalle.Row.ItemArray.GetValue(10)), Convert.ToInt16(detalle.Row.ItemArray.GetValue(11)),
                        Convert.ToDecimal(detalle.Row.ItemArray.GetValue(7)), WebUtility.HtmlDecode(detalle.Row.ItemArray.GetValue(12).ToString()), Convert.ToInt16(detalle.Row.ItemArray.GetValue(3)), Convert.ToBoolean(detalle.Row.ItemArray.GetValue(16)));

                    List<CtransaccionNovedad> listaTransaccion = null;

                    if (this.Session["transaccion"] == null)
                    {
                        listaTransaccion = new List<CtransaccionNovedad>();
                        listaTransaccion.Add(transaccionNovedad);
                    }
                    else
                    {
                        listaTransaccion = (List<CtransaccionNovedad>)Session["transaccion"];
                        listaTransaccion.Add(transaccionNovedad);
                    }

                    this.Session["transaccion"] = listaTransaccion;
                    this.gvLista.DataSource = listaTransaccion;
                    this.gvLista.DataBind();

                }
                this.btnRegistrar.Visible = true;
                TabRegistro();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la transacción. Correspondiente a: " + ex.Message, "A");
            }

        }

        protected void ddlCentroCosto_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEmpleados();
            cargarCombosDetalle();
        }





        protected void ddlConceptoDetalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            VerificaValoryCantidad(ddlConceptoDetalle.SelectedValue.ToString());
        }

        private void VerificaValoryCantidad(string concepto)
        {
            txvCantidad.Enabled = tipoTransaccion.ValidaCantidadValor(Convert.ToInt16(Session["empresa"]), concepto);
            txvValor.Enabled = !tipoTransaccion.ValidaCantidadValor(Convert.ToInt16(Session["empresa"]), concepto);
        }
        protected void txtFecha_TextChanged(object sender, EventArgs e)
        {

            try
            {
                Convert.ToDateTime(txtFecha.Text);
            }
            catch
            {
                MostrarMensaje("Formato de fecha no valido");

                txtFecha.Text = "";
                txtFecha.Focus();
                return;
            }

            verificaPeriodoCerrado(Convert.ToInt32(Convert.ToDateTime(txtFecha.Text).Year),
                Convert.ToInt32(Convert.ToDateTime(txtFecha.Text).Month), Convert.ToInt16(Session["empresa"]), Convert.ToDateTime(txtFecha.Text));
        }
        protected void lbImprimir_Click(object sender, EventArgs e)
        {

        }
        protected void ddlConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            VerificaValoryCantidad(ddlConcepto.SelectedValue.ToString());
            CargarEmpleados();
        }

        protected void gvLista_Sorting(object sender, GridViewSortEventArgs e)
        {

            gvLista.DataBind();
        }


        protected void ddlEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarCombosDetalle();
        }
        protected void nitxtValor1_TextChanged1(object sender, EventArgs e)
        {
            niimbAdicionar.Focus();
        }
        #endregion Evento
    }
}