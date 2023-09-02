using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
using Nomina.WebForms.App_Code.Liquidacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Pliquidacion
{
    public partial class Prestamos  : BasePage
    {
        #region Instancias

        Cprestamo prestamo = new Cprestamo();
        Cperiodos periodo = new Cperiodos();
        Cfuncionarios funcionario = new Cfuncionarios();
        Cgeneral general = new Cgeneral();

        #endregion Instancias

        #region Metodos

        private void verificaPeriodoCerrado(int año, int mes, int empresa, DateTime fecha)
        {
            if (periodo.RetornaPeriodoCerradoNominaSinAgro(año, mes, empresa, fecha) == 1)
                ManejoError("Periodo cerrado. No es posible realizar ediciones, elimina y nuevas transacciones", "I");
        }
        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "C", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }
                this.gvLista.DataSource = prestamo.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();

                nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
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
            Session["rangos"] = null;
            this.nilbNuevo.Visible = true;

            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }
        private void CargarCombos()
        {
            try
            {
                DataView dvTerceroCCosto = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nFuncionario", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlTercero.DataSource = dvTerceroCCosto;
                this.ddlTercero.DataValueField = "tercero";
                this.ddlTercero.DataTextField = "descripcion";
                this.ddlTercero.DataBind();
                this.ddlTercero.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar empleados. Correspondiente a: " + ex.Message, "C");
            }


            try
            {
                DataView dvConceptosFijos = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nConcepto", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"]));
                dvConceptosFijos.RowFilter = "ControlaSaldo=True and empresa =" + Convert.ToInt16(this.Session["empresa"]).ToString();
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
                this.ddlFormaPago.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("gFormaPago", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlFormaPago.DataValueField = "codigo";
                this.ddlFormaPago.DataTextField = "descripcion";
                this.ddlFormaPago.DataBind();
                this.ddlFormaPago.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar forma de pago. Correspondiente a: " + ex.Message, "C");
            }
        }
        private void CargarEmpleados()
        {
            try
            {
                DataView dvTerceroCCosto = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nFuncionario", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlTercero.DataSource = dvTerceroCCosto;
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
            string operacion = "inserta";

            try
            {
                if (Convert.ToBoolean(this.Session["editar"]) == true)
                    operacion = "actualiza";
                else
                    Consecutivo();

                object[] objValores = new object[]{
                           Convert.ToDecimal(txvAñoInicial.Text) ,  //                @año	int
                        null,          //@ccosto	varchar
                        txtCodigo.Text,         //@codigo	varchar
                        ddlConcepto.SelectedValue,        //@concepto	varchar
                        Convert.ToInt32(ddlContratos.SelectedValue),
                        Convert.ToDecimal(txvCuotas.Text),       //@cuotas	int
                        Convert.ToDecimal(txvCuotasPendiente.Text),       //@cuotasPendiente	int
                        txtDocRef.Text,       //@docRef	varchar
                        Convert.ToInt32(ddlTercero.SelectedValue.Trim() ),     //@empleado	int                                
                        Convert.ToInt16(Session["empresa"]),      //@empresa	int
                        Convert.ToDateTime(txtFecha.Text ),        //@fecha	date
                        DateTime.Now,       //@fechaRegistro	datetime
                        ddlFormaPago.SelectedValue,     //@formaPago	varchar                                            
                        Convert.ToDecimal(ddlFrecuencia.SelectedValue),    //@frecuencia	int
                        false,       //@liquidado	bit
                        Convert.ToDateTime(txtFecha.Text ).Month,     //@mes	int
                        txtObservacion.Text,      //@observacion	varchar
                        Convert.ToDecimal(txvPeriodoInicial.Text), //@periodoInicial	int
                        Session["usuario"].ToString(),   //@usuarioRegistro	varchar
                        Convert.ToDecimal(txvValor.Text),  //@valor	money
                        Convert.ToDecimal(txvValorCuota.Text),   //@valorCuotas	money
                        Convert.ToDecimal(txvSaldo.Text)      //@valorSaldo	money
                };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("nPrestamo", operacion, "ppa", objValores))
                {
                    case 0:
                        ManejoExito("Datos insertados satisfactoriamente", operacion.Substring(0, 1).ToUpper());
                        break;
                    case 1:
                        ManejoError("Errores al insertar el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                        break;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar los datos correspondiente a: " + ex.Message, operacion.Substring(0, 1).ToUpper());
            }
        }
        private void Consecutivo()
        {
            try
            {
                this.txtCodigo.Text = prestamo.Consecutivo(Convert.ToInt16(Session["empresa"]));
                txtCodigo.Enabled = false;
            }
            catch (Exception ex)
            {
                ManejoError("Error al obtener el consecutivo. Correspondiente a: " + ex.Message, "C");
            }
        }
        private void cargarContrato()
        {
            try
            {
                DataView dvContratos = prestamo.SelecccionaContratosTercero(Convert.ToInt32(ddlTercero.SelectedValue), Convert.ToInt32(this.Session["empresa"]));
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

        #endregion Metodos

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) != 0)
                    this.txtCodigo.Focus();
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }
        protected void lbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                  nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            Session["rangos"] = null;
            Session["rangoFinal"] = null;

            CargarCombos();
            Consecutivo();

            this.nilblInformacion.Text = "";
        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {

            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();

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

                bool liquidado = false;

                foreach (Control objControl in this.gvLista.Rows[e.RowIndex].Cells[2].Controls)
                {
                    if (objControl is CheckBox)
                        liquidado = ((CheckBox)objControl).Checked;
                }

                if (liquidado == false)
                {
                    nilblInformacion.Text = "El registro no puede ser eliminado, ya fue liquidado";
                    return;
                }

                verificaPeriodoCerrado(Convert.ToInt32(Convert.ToInt32(this.gvLista.Rows[e.RowIndex].Cells[5].Text)),
                   Convert.ToInt32(this.gvLista.Rows[e.RowIndex].Cells[6].Text),
                   Convert.ToInt16(Session["empresa"]), Convert.ToDateTime(this.gvLista.Rows[e.RowIndex].Cells[7].Text));

                object[] objValores = new object[] { Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text), Convert.ToInt16(Session["empresa"]) };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("nPrestamo", "elimina", "ppa", objValores))
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
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
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

            if (txtCodigo.Text.Trim().ToString().Length == 0 || ddlContratos.SelectedValue.Length == 0 || ddlConcepto.SelectedValue.Length == 0 || ddlTercero.SelectedValue.Length == 0)
            {
                MostrarMensaje("Campos vacios por favor corrija");
                return;
            }

            if (Convert.ToDecimal(txvCuotas.Text) == 0 || Convert.ToDecimal(txvValor.Text) == 0 || Convert.ToDecimal(txvValorCuota.Text) == 0)
            {
                MostrarMensaje("Campos en cero (0), por favor corrija");
                return;
            }
            Guardar();
        }
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(),
             "A", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "A");
                return;
            }
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            int columna = 0, año = 0, mes = 0;
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            this.txtCodigo.Enabled = false;
            ddlTercero.Enabled = false;
            txtFecha.Enabled = false;
            ddlContratos.Enabled = false;
            ddlConcepto.Enabled = false;
            lbFecha.Enabled = false;
            CargarCombos();
            try
            {
                DataView dvDatos = prestamo.RetornaDatos(this.gvLista.SelectedRow.Cells[5].Text.Trim(), Convert.ToInt32(Session["empresa"]));

                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    txtCodigo.Text = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                columna += 1;
                CargarEmpleados();
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
                {
                    año = Convert.ToInt16(dvDatos.Table.Rows[0].ItemArray.GetValue(columna));
                    txvAñoInicial.Text = Convert.ToInt16(dvDatos.Table.Rows[0].ItemArray.GetValue(columna)).ToString();
                }
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    mes = Convert.ToInt16(dvDatos.Table.Rows[0].ItemArray.GetValue(columna));
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    txtFecha.Text = Convert.ToDateTime(dvDatos.Table.Rows[0].ItemArray.GetValue(columna)).ToShortDateString();

                //  verificaPeriodoCerrado(Convert.ToInt32(año), Convert.ToInt32(mes), Convert.ToInt16(Session["empresa"]), Convert.ToDateTime(txtFecha.Text));
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    ddlConcepto.SelectedValue = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    txvPeriodoInicial.Text = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    txvValor.Text = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    txvCuotas.Text = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    txvValorCuota.Text = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    txvCuotasPendiente.Text = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    txvSaldo.Text = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    ddlFrecuencia.SelectedValue = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    txtObservacion.Text = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    ddlFormaPago.SelectedValue = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                columna += 1;
                if (dvDatos.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    txtDocRef.Text = dvDatos.Table.Rows[0].ItemArray.GetValue(columna).ToString();

                bool liquidado = false;
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[12].Controls)
                {
                    if (objControl is CheckBox)
                        liquidado = ((CheckBox)objControl).Checked;
                }

                if (liquidado == true)
                {
                    txvPeriodoInicial.Enabled = false;
                    txvValor.Enabled = false;
                    txvCuotas.Enabled = false;
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
        protected void txtFecha_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFecha.Text);
            }
            catch
            {
                nilblInformacion.Text = "Formato de fecha no valido";
                txtFecha.Text = "";
                txtFecha.Focus();
                return;
            }

            verificaPeriodoCerrado(Convert.ToDateTime(txtFecha.Text).Year,
                  Convert.ToDateTime(txtFecha.Text).Month, Convert.ToInt16(Session["empresa"]), Convert.ToDateTime(txtFecha.Text));
        }
        protected void ddlEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTercero.SelectedValue.Length > 0)
                cargarContrato();

        }

        #endregion Eventos

    }
}