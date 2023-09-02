using Contabilidad.seguridadInfos;
using Contabilidad.WebForms.App_Code.Administracion;
using Contabilidad.WebForms.App_Code.Contabilizacion;
using Contabilidad.WebForms.App_Code.General;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.Contabilizacion
{
    public partial class Liquidacion : BasePage
    {

        #region Instancias
        Security seguridad = new Security();
        //Coperadores operadores = new Coperadores();
        CtipoTransaccion tipoTransaccion = new CtipoTransaccion();
        //CtransaccionNovedad transaccionNovedad = new CtransaccionNovedad();
        Cperiodos periodo = new Cperiodos();
        string numerotransaccion = "";
        //Ctransacciones transacciones = new Ctransacciones();
        //CliquidacionNomina liquidacion = new CliquidacionNomina();
        Cgeneral general = new Cgeneral();
        //Cfuncionarios funcionario = new Cfuncionarios();
        Ccontabilizacion contabilizacion = new Ccontabilizacion();
        #endregion Instancias

        #region Metodos

        private void manejoTipo()
        {

            if (ddlTipo.SelectedValue.Trim() == "SS")
            {
                try
                {
                    this.ddlPeriodo.DataSource = periodo.PeriodosSeguridadSocial(Convert.ToInt32(ddlAño.SelectedValue.Trim()), Convert.ToInt16(Session["empresa"])).Tables[0].DefaultView;
                    this.ddlPeriodo.DataValueField = "mes";
                    this.ddlPeriodo.DataTextField = "nombreMes";
                    this.ddlPeriodo.DataBind();
                    this.ddlPeriodo.Items.Insert(0, new ListItem("", ""));
                }
                catch (Exception ex)
                {
                    ManejoError("Error al cargar periodo inicial. Correspondiente a: " + ex.Message, "C");
                }
            }
        }

        private void cargarLiquidacion()
        {
            //DataView dvLiquidacion = liquidacion.getLiquidacionNomina(Convert.ToInt32(ddlPeriodo.SelectedValue.Trim()), Convert.ToInt16(Session["empresa"])); ;
            //gvLista.DataSource = dvLiquidacion;
            //gvLista.DataBind();

            //if (gvLista.Rows.Count > 0)
            //{
            //    this.Session["liquidacion"] = dvLiquidacion;
            //}
        }

        //    private void Preliquidar()
        //    {
        //        string script = "", nombreTercero = "";
        //        int retorno = 0;
        //        retorno = contabilizacion.PrecontabilizaNominaTipoPeriodo(Convert.ToInt32(ddlAño.SelectedValue.Trim()), Convert.ToInt32(ddlPeriodo.SelectedValue.Trim()), ddlTipo.SelectedValue.Trim(), Convert.ToInt16(Session["empresa"]), Convert.ToDateTime(txtFecha.Text), this.Session["usuario"].ToString());
        //        switch (retorno)
        //        {
        //            case 1:
        //                script = @"<script type='text/javascript'>
        //                            alerta('Error al generar preliquidacion');
        //                        </script>";
        //                break;


        //            case 0:

        //                script = "<script language='javascript'>Visualizacion('Precontabilizacion', '" + ddlAño.SelectedValue.ToString() + "','" + ddlPeriodo.SelectedValue.Trim() + "','" + ddlTipo.SelectedValue.Trim() + "');</script>";
        //                Page.RegisterStartupScript("Visualizacion", script);

        //                script = "<script language='javascript'>VisualizacionResumen('ResumenPrecontabilizacion', '" + ddlAño.SelectedValue.ToString() + "','" + ddlPeriodo.SelectedValue.Trim() + "','" + ddlTipo.SelectedValue.Trim() + "');</script>";
        //                Page.RegisterStartupScript("VisualizacionResumen", script);

        //                this.nilblMensaje.Text = "Preliquidacion con exito";
        //                break;
        //        }
        //    }
        private void Liquidar()
        {
            string script = "";
            string retorno = "";
            string noTransaccion = "";

            contabilizacion.ContabilizaNominaTipoPeriodo(Convert.ToInt32(ddlAño.SelectedValue.Trim()), Convert.ToInt32(ddlPeriodo.SelectedValue.Trim()), ddlTipo.SelectedValue.Trim(), Convert.ToInt16(Session["empresa"]), Convert.ToDateTime(txtFecha.Text), this.Session["usuario"].ToString(), txtObservacion.Text, ddlEmpleado.SelectedValue.Trim(), out retorno, out noTransaccion);

            switch (retorno)
            {
                case "0":
                    ManejoExito("Contabilizacion registrada satisfactoriamente N° de transaccion : " + noTransaccion, "I");

                    break;

                case "1":
                    script = @"<script type='text/javascript'>
                            alerta('Error al contabilizar');
                        </script>";

                    if (!Page.IsClientScriptBlockRegistered("alerta"))
                        Page.RegisterStartupScript("alerta", script);
                    break;

                case "2":
                    script = @"<script type='text/javascript'>
                            alerta('Debe crear el tipo de transaccion para contabilizar');
                        </script>";

                    if (!Page.IsClientScriptBlockRegistered("alerta"))
                        Page.RegisterStartupScript("", script);
                    break;

                case "3":
                    script = @"<script type='text/javascript'>
                            alerta('Periodo cerrado no puede procesarse');
                        </script>";

                    if (!Page.IsClientScriptBlockRegistered("alerta"))
                        Page.RegisterStartupScript("alerta", script);
                    break;

                case "4":
                    script = @"<script type='text/javascript'>
                            alerta('Existen equivalencias  contables  sin parametros  por favor revisar');
                        </script>";

                    if (!Page.IsClientScriptBlockRegistered("alerta"))
                        Page.RegisterStartupScript("alerta", script);
                    break;

                case "5":
                    script = @"<script type='text/javascript'>
                            alerta('No existe ninguna precontabilizacion del periodo seleccionado');
                        </script>";

                    if (!Page.IsClientScriptBlockRegistered("alerta"))
                        Page.RegisterStartupScript("alerta", script);
                    break;

                case "6":
                    script = @"<script type='text/javascript'>
                            alerta('Hay centros de costos contables sin auxiliar o mayor por favor revisar');
                        </script>";

                    if (!Page.IsClientScriptBlockRegistered("alerta"))
                        Page.RegisterStartupScript("alerta", script);
                    break;

                case "7":
                    script = @"<script type='text/javascript'>
                            alerta('El periodo ya se encuentra contabilizado');
                        </script>";

                    if (!Page.IsClientScriptBlockRegistered("alerta"))
                        Page.RegisterStartupScript("alerta", script);
                    break;

                case "8":
                    script = @"<script type='text/javascript'>
                            alerta('El empleado ya se encuentra contabilizado');
                        </script>";

                    if (!Page.IsClientScriptBlockRegistered("alerta"))
                        Page.RegisterStartupScript("alerta", script);
                    break;

                case "9":
                    script = @"<script type='text/javascript'>
                            alerta('No existe precontabilizacion del empleado');
                        </script>";

                    if (!Page.IsClientScriptBlockRegistered("alerta"))
                        Page.RegisterStartupScript("alerta", script);
                    break;

            }

        }
        private void manejoLiquidacion()
        {
            if (ddlPeriodo.SelectedValue.Trim().Length > 0 & ddlAño.SelectedValue.Trim().Length > 0)
            {
                btnLiquidar.Visible = true;
            }
            else
            {
                btnLiquidar.Visible = false;
            }
        }
        protected void cargarPeriodos()
        {
            try
            {
                this.ddlPeriodo.DataSource = periodo.PeriodosCeradoNominaAño(Convert.ToInt32(ddlAño.SelectedValue.Trim()), Convert.ToInt16(Session["empresa"]), ddlTipo.SelectedValue.Trim());
                this.ddlPeriodo.DataValueField = "noPeriodo";
                this.ddlPeriodo.DataTextField = "descripcion";
                this.ddlPeriodo.DataBind();
                this.ddlPeriodo.Items.Insert(0, new ListItem("", ""));
                manejoTipo();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar periodo inicial. Correspondiente a: " + ex.Message, "C");
            }
        }

        protected string nombrePaginaActual()
        {
            string[] arrResult = HttpContext.Current.Request.RawUrl.Split('/');
            string result = arrResult[arrResult.GetUpperBound(0)];
            arrResult = result.Split('?');
            return arrResult[arrResult.GetLowerBound(0)];
        }
        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "C", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                this.gvLista.DataSource = contabilizacion.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();

                this.nilblInformacion.Visible = true;
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                            this.gvLista.Rows.Count.ToString() + " Registros encontrados", HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente a: " + ex.Message, "C");
            }
        }
        private void ManejoError(string error, string operacion)
        {
            this.Session["error"] = error;
            this.Session["paginaAnterior"] = this.Page.Request.FilePath.ToString();

            seguridad.InsertaLog(
                  this.Session["usuario"].ToString(),
                  operacion,
                  ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                  "er",
                  error, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));

            this.Response.Redirect("~/Contabilidad/Error.aspx", false);
        }
        private void ManejoExito(string mensaje, string operacion)
        {
            this.nilblMensaje.Text = mensaje;
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;

            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
          "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }
        private void CargarCombos()
        {


            try
            {
                this.ddlAño.DataSource = periodo.PeriodoAñoCerradoNomina(Convert.ToInt16(Session["empresa"]));
                this.ddlAño.DataValueField = "año";
                this.ddlAño.DataTextField = "año";
                this.ddlAño.DataBind();
                this.ddlAño.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar año. Correspondiente a: " + ex.Message, "C");
            }
        }
        private void EntidadKey()
        {
            object[] objKey = new object[] { ""
            //ddlAño.SelectedValue, ddlCentroCosto.SelectedValue, Convert.ToInt16(Session["empresa"]), ddlMes.SelectedValue, ddlPeriodo.SelectedValue
        };

            try
            {
                if (CentidadMetodos.EntidadGetKey(
                    "nConceptosFijos",
                    "ppa",
                    objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    this.nilblInformacion.Text = "Ya se encuentra registrada la combinación";

                    CcontrolesUsuario.InhabilitarControles(
                        this.Page.Controls);

                    this.nilbNuevo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al validar la llave primaria correspondiente a: " + ex.Message, "C");
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
                {
                }
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }

        protected void lbNuevo_Click(object sender, ImageClickEventArgs e)
        {

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);

            CargarCombos();
            this.nilbNuevo.Visible = false;
            ddlEmpleado.Visible = false;
            lblEmpleado.Visible = false;
            this.Session["editar"] = null;
            this.nilblInformacion.Text = "";
            this.txtFecha.Focus();
            this.btnLiquidar.Visible = false;
        }
        protected void lbCancelar_Click(object sender, ImageClickEventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
            this.Session["editar"] = null;
        }


        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
            nombrePaginaActual(), "A", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "A");
                return;
            }
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CargarCombos();
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            ddlAño.Enabled = false;
            ddlPeriodo.Enabled = false;

            try
            {
                if (this.gvLista.SelectedRow.Cells[2].Text != "")
                { }

                if (this.gvLista.SelectedRow.Cells[4].Text != "")
                {
                    this.ddlAño.SelectedValue = this.gvLista.SelectedRow.Cells[4].Text;

                    if (ddlAño.SelectedValue.Length != 0)
                    {
                    }
                }
                if (this.gvLista.SelectedRow.Cells[5].Text != "")
                {


                }
                if (this.gvLista.SelectedRow.Cells[6].Text != "")
                {
                }
                if (this.gvLista.SelectedRow.Cells[7].Text != "")
                {

                }
                if (this.gvLista.SelectedRow.Cells[8].Text != "")
                {
                    this.txtObservacion.Text = this.gvLista.SelectedRow.Cells[8].Text;
                }


            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.nilblMensaje.Text = "";

            try
            {
                switch (contabilizacion.AnulaTransaccionContable(Convert.ToInt32(this.Session["empresa"]),
                    gvLista.Rows[e.RowIndex].Cells[1].Text, gvLista.Rows[e.RowIndex].Cells[2].Text, gvLista.Rows[e.RowIndex].Cells[3].Text,
                   Convert.ToInt32(gvLista.Rows[e.RowIndex].Cells[5].Text), Convert.ToInt32(gvLista.Rows[e.RowIndex].Cells[8].Text), this.Session["usuario"].ToString()))
                {
                    case 0:
                        nilblMensaje.Text = "Registro Anulado satisfactoriamente";
                        GetEntidad();
                        break;
                    case 1:
                        nilblMensaje.Text = "Error al anular la transacción. Operación no realizada";
                        break;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
            }

        }



        protected void nilblListado_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListadoDestinos.aspx");
        }


        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
        }

        protected void nilbRegresar_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("Programacion.aspx");
        }

        #endregion Eventos

        #region MetodosFuncionario




        #endregion MetodosFuncionario

        #region EventosFuncionario


        protected void niimbBuscar_Click(object sender, ImageClickEventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }


        protected void CalendarFecha_SelectionChanged(object sender, EventArgs e)
        {
            this.niCalendarFecha.Visible = false;
            this.txtFecha.Visible = true;
            this.txtFecha.Text = this.niCalendarFecha.SelectedDate.ToShortDateString();

        }
        protected void lbFecha_Click(object sender, EventArgs e)
        {
            this.niCalendarFecha.Visible = true;
            this.txtFecha.Visible = false;
            this.niCalendarFecha.SelectedDate = Convert.ToDateTime(null);
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

        }


        protected void ddlAño_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddlAño.SelectedValue.Trim().Length > 0)
            {
                cargarPeriodos();
                manejoLiquidacion();
            }
        }


        protected void ddlPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                manejoLiquidacion();

                if (ddlTipo.SelectedValue.Trim() == "PS")
                {
                    this.ddlEmpleado.Visible = true;
                    this.lblEmpleado.Visible = true;
                    this.ddlEmpleado.DataSource = contabilizacion.RetornaEmpleadosLiquidacionContratos(Convert.ToInt32(this.Session["empresa"]), Convert.ToInt32(ddlAño.SelectedValue.Trim()), Convert.ToInt32(ddlPeriodo.SelectedValue.Trim()));
                    this.ddlEmpleado.DataValueField = "numero";
                    this.ddlEmpleado.DataTextField = "cadena";
                    this.ddlEmpleado.DataBind();
                    this.ddlEmpleado.Items.Insert(0, new ListItem("", ""));

                }
                else
                {
                    this.ddlEmpleado.Visible = false;
                    this.lblEmpleado.Visible = false;
                    this.ddlEmpleado.DataSource = null;
                    this.ddlEmpleado.DataBind();
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar empleado debido a: " + ex.Message, "C");
            }
        }


        protected void lbPreLiquidar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtFecha.Text.Trim().Length == 0)
                {
                    nilblInformacion.Text = "Debe seleccionar una fecha para guardar liquidación";
                    txtFecha.Focus();
                    return;
                }

                if (ddlAño.SelectedValue.Trim().Length == 0)
                {
                    nilblInformacion.Text = "Debe seleccionar un año para guardar liquidación";
                    ddlAño.Focus();
                    return;
                }

                if (ddlAño.SelectedValue.Trim().Length == 0)
                {
                    nilblInformacion.Text = "Debe seleccionar una forma de liquidación para seguir";
                    txtFecha.Focus();
                    return;
                }


                if (ddlEmpleado.SelectedValue.Trim().Length == 0 & ddlEmpleado.Visible == true)
                {
                    nilblInformacion.Text = "Debe seleccionar un de centro de costp para seguir";
                    ddlEmpleado.Focus();
                    return;
                }

                //Preliquidar();
            }
            catch (Exception ex)
            {
                ManejoError("Error al liquidar el documento debido a :" + ex.Message, "I");
            }
        }
        protected void btnLiquidar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtFecha.Text.Trim().Length == 0)
                {
                    nilblInformacion.Text = "Debe seleccionar una fecha para guardar liquidación";
                    txtFecha.Focus();
                    return;
                }

                if (ddlAño.SelectedValue.Trim().Length == 0)
                {
                    nilblInformacion.Text = "Debe seleccionar un año para guardar liquidación";
                    ddlAño.Focus();
                    return;
                }


                if (ddlEmpleado.SelectedValue.Trim().Length == 0 & ddlEmpleado.Visible == true)
                {
                    nilblInformacion.Text = "Debe seleccionar un de centro de costp para seguir";
                    ddlEmpleado.Focus();
                    return;
                }

                Liquidar();

            }
            catch (Exception ex)
            {
                ManejoError("Error al liquidar el documento debido a :" + ex.Message, "I");
            }
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion EventosFuncionario

    }
}