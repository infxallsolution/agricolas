using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
using Nomina.WebForms.App_Code.Liquidacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Pliquidacion
{
    public partial class Incapacidades : BasePage
    {
        #region Instancias

        Cincapacidades incapacidad = new Cincapacidades();
        Cperiodos periodo = new Cperiodos();

        #endregion Instancias

        #region Metodos

        private void totalizar()
        {
            decimal total = 0, totalPagado = 0, dias = 0, diasPagar = 0;

            foreach (GridViewRow gvp in gvDetalle.Rows)
            {
                total += Convert.ToDecimal(((TextBox)gvp.FindControl("txtValorTotal")).Text);
                totalPagado += Convert.ToDecimal(((TextBox)gvp.FindControl("txtValorPagar")).Text);
                dias += 1;
                if (Convert.ToDecimal(((TextBox)gvp.FindControl("txtValorPagar")).Text) > 0)
                    diasPagar += 1;
            }
            txvValorIncapacidad.Text = total.ToString();
            txvValorPagar.Text = totalPagado.ToString();
        }


        private void verificaPeriodoCerrado(int año, int mes, int empresa)
        {
            if (periodo.RetornaPeriodoCerrado(año, mes, empresa) == 1)
            {
                ManejoError("Periodo cerrado. No es posible realizar nuevas transacciones", "I");
            }

        }

        private void cargarAusentismoDetalle()
        {
            try
            {
                gvDetalle.Visible = true;
                gvDetalle.DataSource = incapacidad.seleccionaDetalleAusentismo(ddlTercero.SelectedValue, Convert.ToInt32(ddlContratos.SelectedValue),
                    txtCodigo.Text, Convert.ToInt32(Session["empresa"]));
                gvDetalle.DataBind();
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }
        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                                        ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "C", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }
                this.gvLista.DataSource = incapacidad.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();

                nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C",
                  ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                    this.gvLista.Rows.Count.ToString() + " Registros encontrados", ObtenerIP(), Convert.ToInt16(Session["empresa"]));

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente a: " + ex.Message, "C");
            }
        }
        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            gvDetalle.Visible = false;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));

            GetEntidad();
        }
        private void CargarIncapacidades()
        {

            try
            {
                ddlProrroga.Enabled = true;
                this.ddlProrroga.DataSource = incapacidad.ProrrogaIncapacidadTercero(Convert.ToDateTime(txtFechaInicial.Text), Convert.ToInt16(Session["empresa"]), ddlTercero.SelectedValue);
                this.ddlProrroga.DataValueField = "numero";
                this.ddlProrroga.DataTextField = "cadena";
                this.ddlProrroga.DataBind();
                this.ddlProrroga.Items.Insert(0, new ListItem("", ""));

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar diagnostico. Correspondiente a: " + ex.Message, "C");
            }
        }
        private void CargarCombos()
        {

            try
            {
                this.ddlDiagnostico.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("gDiagnostico", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlDiagnostico.DataValueField = "codigo";
                this.ddlDiagnostico.DataTextField = "descripcion";
                this.ddlDiagnostico.DataBind();
                this.ddlDiagnostico.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar diagnostico. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                DataView dvConceptosFijos = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nConcepto", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"]));
                dvConceptosFijos.RowFilter = "activo =1 and ControlaSaldo=0 and ausentismo=1 and empresa =" + Convert.ToInt16(this.Session["empresa"]).ToString();
                this.ddlConcepto.DataSource = dvConceptosFijos;
                this.ddlConcepto.DataValueField = "codigo";
                this.ddlConcepto.DataTextField = "descripcion";
                this.ddlConcepto.DataBind();
                this.ddlConcepto.Items.Insert(0, new ListItem("", ""));

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar finca. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlTipoIncapacidad.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nTipoIncapacidad", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"]));
                this.ddlTipoIncapacidad.DataValueField = "codigo";
                this.ddlTipoIncapacidad.DataTextField = "descripcion";
                this.ddlTipoIncapacidad.DataBind();
                this.ddlTipoIncapacidad.Items.Insert(0, new ListItem("", ""));

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tipo incapacidad. Correspondiente a: " + ex.Message, "C");
            }

        }
        private void CalcularValorIncapacidad()
        {
            object[] objRetono = new object[2];
            try
            {
                Convert.ToDateTime(txtFechaInicial.Text);
            }
            catch
            {
                nilblInformacion.Text = "Formato de fecha no valido por favor correjir";
                return;
            }

            if (ddlTipoIncapacidad.SelectedValue.ToString().Length != 0 || Convert.ToDecimal(txvNoDias.Text) != 0 || ddlTercero.SelectedValue.ToString().Length != 0 ||
                 ddlContratos.SelectedValue.ToString().Length != 0)
            {
                objRetono = incapacidad.CalculaIncapacidad(Convert.ToInt16(Session["empresa"]), ddlTercero.SelectedValue, Convert.ToDecimal(txvNoDias.Text), ddlTipoIncapacidad.SelectedValue, Convert.ToDateTime(txtFechaInicial.Text), Convert.ToInt16(Convert.ToDecimal(txvDiasPagar.Text)), Convert.ToInt16(ddlDiasInicio.SelectedValue));
                txvValorIncapacidad.Text = objRetono[0].ToString();
                txvValorPagar.Text = objRetono[1].ToString();
                txtFechaFinal.Text = Convert.ToString((Convert.ToDateTime(txtFechaInicial.Text).AddDays(Convert.ToDouble(Convert.ToDecimal(txvNoDias.Text) - 1))).ToShortDateString());

                CargarDetalle();


            }
        }

        private void CargarDetalle()
        {
            try
            {
                gvDetalle.Visible = true;
                gvDetalle.DataSource = incapacidad.CalulaIncapacidadEmpleadoDetalleTemporal(Convert.ToInt32(ddlTercero.SelectedValue), Convert.ToInt32(Session["empresa"]),
                    Convert.ToDateTime(txtFechaInicial.Text), Convert.ToDateTime(txtFechaFinal.Text), Convert.ToDecimal(txvNoDias.Text), Convert.ToDecimal(txvDiasPagar.Text),
                    ddlTipoIncapacidad.SelectedValue, Convert.ToInt32(ddlContratos.SelectedValue));
                gvDetalle.DataBind();
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }
        private void CargarEmpleados()
        {
            try
            {
                DataView dvDatosCCosto = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nFuncionario", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlTercero.DataSource = dvDatosCCosto;
                this.ddlTercero.DataValueField = "tercero";
                this.ddlTercero.DataTextField = "descripcion";
                this.ddlTercero.DataBind();
                this.ddlTercero.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar empleados. Correspondiente a: " + ex.Message, "C");
            }

        }
        private void Guardar()
        {
            string operacion = "inserta", numeroProrroga = null;
            bool validar = false;
            try
            {
                if (Convert.ToBoolean(this.Session["editar"]) == true)
                    operacion = "actualiza";
                else
                    retornaConsecutivo();

                if (chkProrroga.Checked)
                    numeroProrroga = ddlProrroga.SelectedValue;
                string diasnostico = null;

                if (ddlDiagnostico.SelectedValue.Length > 0)
                    diasnostico = ddlDiagnostico.SelectedValue;

                using (TransactionScope ts = new TransactionScope())
                {

                    object[] objValores = new object[]{
                    Convert.ToInt16(Convert.ToDateTime(txtFechaInicial.Text).Year),
                    ddlConcepto.SelectedValue,
                    Convert.ToInt32( ddlContratos.SelectedValue),
                    diasnostico,
                    Convert.ToDecimal(ddlDiasInicio.SelectedValue),
                    Convert.ToDecimal(txvDiasPagar.Text),
                    Convert.ToInt16(Session["empresa"]),      //@empresa	int
                    Convert.ToDateTime(txtFechaFinal.Text ) ,    //                fechaFinal              
                     Convert.ToDateTime(txtFechaInicial.Text ) , 	//@fechaInicial                  
                     DateTime.Now,//@fechaRegistro
                     false,//@liquida
                     Convert.ToInt16(Convert.ToDateTime(txtFechaInicial.Text).Month),
                     Convert.ToDecimal(txvNoDias.Text),
                     txtCodigo.Text,
                     numeroProrroga,
                     txtObservacion.Text,
                     chkProrroga.Checked,
                     txtReferencia.Text,
                     Convert.ToDecimal(txvNoDias.Text),
                     ddlTercero.SelectedValue,
                     ddlTipoIncapacidad.SelectedValue,
                     Session["usuario"].ToString(),
                     Convert.ToDecimal(txvValorIncapacidad.Text),
                     Convert.ToDecimal(txvValorPagar.Text)
                };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("nIncapacidad", operacion, "ppa", objValores))
                    {
                        case 0:

                            switch (incapacidad.EliminaIncapacidadDetalle(ddlTercero.SelectedValue, Convert.ToInt32(ddlContratos.SelectedValue),
                                                txtCodigo.Text, Convert.ToInt32(Session["empresa"])))
                            {
                                case 0:
                                    foreach (GridViewRow r in gvDetalle.Rows)
                                    {
                                        switch (incapacidad.InsertaDetalleIncapacidad(Convert.ToInt16(Session["empresa"]), ddlTercero.SelectedValue, Convert.ToInt32(ddlContratos.SelectedValue),
                                            txtCodigo.Text, Convert.ToDateTime(r.Cells[1].Text),
                                             Convert.ToDecimal(((TextBox)r.FindControl("txtValorTotal")).Text), Convert.ToDecimal(((TextBox)r.FindControl("txtValorPagado")).Text)))
                                        {
                                            case 1:
                                                validar = true;
                                                break;
                                        }
                                    }
                                    break;
                                case 1:
                                    validar = true;
                                    break;
                            }

                            break;

                        case 1:
                            validar = true;
                            break;
                    }

                    if (validar == false)
                    {
                        ManejoExito("Registros insertado satisfactoriamente", "I");
                        ts.Complete();
                    }
                    else
                        ManejoError("Errores al insertar el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar los datos correspondiente a: " + ex.Message, operacion.Substring(0, 1).ToUpper());
            }
        }
        private void manejoNuevo()
        {
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            Session["rangos"] = null;
            Session["rangoFinal"] = null;

            CargarCombos();
            CargarEmpleados();
            retornaConsecutivo();
            ddlProrroga.Enabled = false;
            txtCodigo.Enabled = false;
            txtFechaFinal.Enabled = false;
            this.nilblInformacion.Text = "";
        }
        private void cargarContrato()
        {
            try
            {
                DataView dvContratos = incapacidad.SelecccionaContratosTercero(Convert.ToInt32(ddlTercero.SelectedValue), Convert.ToInt32(this.Session["empresa"]));
                this.ddlContratos.DataSource = dvContratos;
                this.ddlContratos.DataValueField = "noContrato";
                this.ddlContratos.DataTextField = "desContrato";
                this.ddlContratos.DataBind();
                this.ddlContratos.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar contratos de tercero. Correspondiente a: " + ex.Message, "C");
            }
        }
        private void manejoEmpleado()
        {
            ddlTipoIncapacidad.SelectedValue = "";
            ddlProrroga.DataSource = null;
            ddlProrroga.DataBind();
            ddlDiagnostico.SelectedValue = "";
            txvNoDias.Text = "0";
            txtReferencia.Text = "";
            txvValorIncapacidad.Text = "0";
            txtObservacion.Text = "";
            txtFechaInicial.Text = "";
            txtFechaFinal.Text = "";
            lbFechaInicial.Visible = true;
            txtFechaInicial.Visible = true;
            txtFechaFinal.Visible = true;
            chkProrroga.Checked = false;
        }
        private void retornaConsecutivo()
        {
            try
            {
                txtCodigo.Text = incapacidad.Consecutivo(Convert.ToInt16(Session["empresa"]), ddlTercero.SelectedValue);
            }
            catch (Exception ex)
            {
                ManejoError("Error al recuperar los datos del funcionario. Correspondiente a: " + ex.Message, "C");
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
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(),
                    ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) != 0)
                    this.txtCodigo.Focus();
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }
        protected void lbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                                 ConfigurationManager.AppSettings["Modulo"].ToString(),
                                  nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            manejoNuevo();
        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {

            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            gvDetalle.Visible = false;
            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";

        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                bool liquidado = false, anulado = false;
                foreach (Control objControl in this.gvLista.Rows[e.RowIndex].Cells[14].Controls)
                {
                    if (objControl is CheckBox)
                        anulado = ((CheckBox)objControl).Checked;
                }
                if (anulado == true)
                    ManejoError("Registro anulado no es posible la edición de la transacción", "A");

                verificaPeriodoCerrado(Convert.ToInt32(Convert.ToInt32(this.gvLista.Rows[e.RowIndex].Cells[9].Text)),
                    Convert.ToInt32(this.gvLista.Rows[e.RowIndex].Cells[10].Text),
                    Convert.ToInt16(Session["empresa"]), Convert.ToDateTime(this.gvLista.Rows[e.RowIndex].Cells[7].Text));

                foreach (Control objControl in this.gvLista.Rows[e.RowIndex].Cells[12].Controls)
                {
                    if (objControl is CheckBox)
                        liquidado = ((CheckBox)objControl).Checked;
                }

                if (liquidado == true)
                    nilblInformacion.Text = "El registro no puede ser eliminado, ya fue liquidado";



                object[] objValores = new object[] {Convert.ToInt32(this.gvLista.Rows[e.RowIndex].Cells[5].Text),
                    Convert.ToInt16(Session["empresa"]) , //@empresa	int
                                 Convert.ToInt32(this.gvLista.Rows[e.RowIndex].Cells[6].Text) ,               //@numero	int
                               Convert.ToInt32(this.gvLista.Rows[e.RowIndex].Cells[2].Text)  ,           //@tercero	int
                               Session["usuario"].ToString()            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("nIncapacidad", "elimina", "ppa", objValores))
                {
                    case 0:
                        ManejoExito("Registro eliminado satisfactoriamente", "E");
                        break;
                    case 1:
                        ManejoError("Error al eliminar el registro. Operación no realizada", "E");
                        break;
                }

            }
            catch (Exception ex)
            {
                if (ex.HResult.ToString() == "-2146233087")
                {
                    ManejoError("El código ('" + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)) +
                    "' - ' " + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[3].Text)) + "') tiene una asociación, no es posible eliminar el registro.", "E");
                }
                else
                {
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
                }
            }
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {

            if (txtCodigo.Text.Trim().ToString().Length == 0 || ddlTipoIncapacidad.SelectedValue.Length == 0 || ddlContratos.SelectedValue.Length == 0 || ddlConcepto.SelectedValue.Length == 0 || ddlTercero.SelectedValue.Length == 0)
            {
                MostrarMensaje("Campos vacios por favor corrija");
                return;
            }

            if (Convert.ToDecimal(txvNoDias.Text) == 0)
            {
                MostrarMensaje("Campos en cero (0), por favor corrija");
                return;
            }

            if (chkProrroga.Checked)
            {
                if (ddlProrroga.SelectedValue.Length == 0)
                {
                    MostrarMensaje("Debe seleccionar una prorroga por favor corrija");
                    return;
                }
            }

            if (Convert.ToBoolean(this.Session["editar"]) == false)
            {
                if (incapacidad.validaRegistroIncapacidadFecha(Convert.ToInt16(Session["empresa"]), ddlTercero.SelectedValue, Convert.ToDateTime(txtFechaInicial.Text), Convert.ToDateTime(txtFechaFinal.Text)) == 1)
                {
                    MostrarMensaje("Ya existe un ausentismo para el funcionario seleccionado en el rango de fecha, por favor corrija");
                    return;
                }
            }

            if (gvDetalle.Rows.Count == 0)
            {
                MostrarMensaje("Debe tener un detalle la incapacidad, por favor corrija o haga 'click' en liquidar");
                return;
            }
            Guardar();
        }
        private void verificaPeriodoCerrado(int año, int mes, int empresa, DateTime fecha)
        {
            if (periodo.RetornaPeriodoCerradoNominaSinAgro(año, mes, Convert.ToInt16(Session["empresa"]), fecha) == 1)
            {
                ManejoError("Periodo cerrado. No es posible realizar ediciones, anulaciones ni nuevos registros", "I");
                return;
            }
        }
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "A", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "A");
                return;
            }

            bool anulado = false;
            int columna = 0, año = 0, mes = 0;

            foreach (Control objControl in this.gvLista.SelectedRow.Cells[14].Controls)
            {
                if (objControl is CheckBox)
                    anulado = ((CheckBox)objControl).Checked;
            }
            if (anulado == true)
            {
                MostrarMensaje("Registro anulado no es posible la edición de la transacción");
                return;
            }

            if (periodo.RetornaPeriodoCerradoNominaSinAgro(Convert.ToInt32(this.gvLista.SelectedRow.Cells[9].Text.Trim()),
                Convert.ToInt32(this.gvLista.SelectedRow.Cells[10].Text.Trim()), Convert.ToInt16(Session["empresa"]),
                Convert.ToDateTime(this.gvLista.SelectedRow.Cells[7].Text)) == 1)
            {
                MostrarMensaje("Periodo cerrado. No es posible realizar ediciones, anulaciones ni nuevos registros");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            this.txtCodigo.Enabled = false;
            ddlTercero.Enabled = false;
            lbFechaInicial.Enabled = false;
            ddlContratos.Enabled = false;

            CargarCombos();
            CargarEmpleados();

            try
            {

                DataView dvDatos = incapacidad.RetornaDatos(this.gvLista.SelectedRow.Cells[2].Text.Trim(), Convert.ToInt32(gvLista.SelectedRow.Cells[5].Text),
                   Convert.ToInt32(Session["empresa"]), Convert.ToInt32(gvLista.SelectedRow.Cells[6].Text));

                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    ddlTercero.SelectedValue = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                {
                    cargarContrato();
                    ddlContratos.SelectedValue = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();
                }
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    txtCodigo.Text = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    año = Convert.ToInt16(dvDatos.Table.Rows[0].ItemArray.GetValue(columna));
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    mes = Convert.ToInt16(dvDatos.Table.Rows[0].ItemArray.GetValue(columna));
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    txtFechaInicial.Text = Convert.ToDateTime(dvDatos.Table.Rows[0].ItemArray.GetValue(columna)).ToShortDateString();

                verificaPeriodoCerrado(Convert.ToInt32(año), Convert.ToInt32(mes), Convert.ToInt16(Session["empresa"]), Convert.ToDateTime(txtFechaInicial.Text));

                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    txtFechaFinal.Text = Convert.ToDateTime(dvDatos.Table.Rows[0].ItemArray.GetValue(columna)).ToShortDateString();
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    txvNoDias.Text = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    txtReferencia.Text = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    ddlTipoIncapacidad.SelectedValue = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                columna = 12;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    ddlDiagnostico.SelectedValue = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    txtObservacion.Text = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                bool liquidado = false;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[12].Controls)
                {
                    if (objControl is CheckBox)
                        liquidado = ((CheckBox)objControl).Checked;
                }

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[13].Controls)
                {
                    if (objControl is CheckBox)
                        chkProrroga.Checked = ((CheckBox)objControl).Checked;
                }

                columna = 18;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    txvValorIncapacidad.Text = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                columna += 1;
                if (chkProrroga.Checked)
                {
                    if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    {
                        CargarIncapacidades();
                        ddlProrroga.SelectedValue = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                    }
                }
                else
                    ddlProrroga.Enabled = false;

                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    txvDiasPagar.Text = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    ddlDiasInicio.SelectedValue = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    txvValorPagar.Text = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    ddlConcepto.SelectedValue = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                cargarAusentismoDetalle();

                if (liquidado == true)
                {
                    txvNoDias.Enabled = false;
                    txvValorPagar.Enabled = false;
                    txvValorIncapacidad.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }


        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            gvDetalle.Visible = false;
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }
        protected void gvLista_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
        protected void ddlCentroCosto_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEmpleados();
        }
        protected void ddlEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            manejoEmpleado();
            retornaConsecutivo();
            if (ddlTercero.SelectedValue.Length > 0)
                cargarContrato();

        }
        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            CalcularValorIncapacidad();

        }
        protected void chkProrroga_CheckedChanged(object sender, EventArgs e)
        {
            if (txtFechaInicial.Text.Length == 0)
            {
                MostrarMensaje("Debe seleccionar fecha Inicial para cargar prorrogas");
                return;
            }

            if (chkProrroga.Checked == true)
            {
                CargarIncapacidades();
                CalcularValorIncapacidad();
            }
            else
            {

                this.ddlProrroga.DataSource = null;
                this.ddlProrroga.DataBind();

            }
        }
        protected void txvNoDias_TextChanged(object sender, EventArgs e)
        {


            txtFechaFinal.Focus();
        }
        protected void txtFechaInicial_TextChanged(object sender, EventArgs e)
        {
            txvNoDias.Focus();
        }
        protected void ddlDiasInicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(ddlDiasInicio.SelectedValue) > Convert.ToDecimal(txvDiasPagar.Text) || Convert.ToDecimal(ddlDiasInicio.SelectedValue) > Convert.ToDecimal(txvNoDias.Text))
            {
                MostrarMensaje("Debe seleccionar un día inferior al número de días a pagar");
                return;
            }

            if (txvDiasPagar.Text.Trim().Length > 0 & txtFechaInicial.Text.Trim().Length > 0)
            {
                CalcularValorIncapacidad();
            }
            txvDiasPagar.Focus();
        }
        protected void txvDiasPagar_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(ddlDiasInicio.SelectedValue) > Convert.ToDecimal(txvDiasPagar.Text) || Convert.ToDecimal(ddlDiasInicio.SelectedValue) > Convert.ToDecimal(txvNoDias.Text))
            {
                MostrarMensaje("Debe seleccionar un día inferior al número de días a pagar");
                return;
            }

            if (txvDiasPagar.Text.Trim().Length > 0 & txtFechaInicial.Text.Trim().Length > 0)
            {
                CalcularValorIncapacidad();
            }
            txvDiasPagar.Focus();
        }



        protected void gvSaludPension_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string concepto = gvDetalle.Rows[e.RowIndex].Cells[1].Text.Trim();
                DataView dvVacaciones = (DataView)this.Session["vacaciones"];
                foreach (DataRowView dtv in dvVacaciones)
                {
                    if (dtv.Row.ItemArray.GetValue(0).ToString() == concepto)
                        dtv.Row.Delete();
                }
                gvDetalle.DataSource = dvVacaciones;
                gvDetalle.DataBind();
                this.Session["vacaciones"] = dvVacaciones;

                totalizar();
            }

            catch (Exception ex)
            {
                ManejoError("Error al eliminar borrar la fila debido a : " + ex.Message, "E");
            }
        }

        protected void btnLiquidar_Click(object sender, EventArgs e)
        {
            if (txvNoDias.Text.Trim().Length > 0 & txtFechaInicial.Text.Trim().Length > 0)
            {
                CalcularValorIncapacidad();
            }
        }

        #endregion Eventos
    }
}