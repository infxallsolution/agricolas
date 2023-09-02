using Contabilidad.seguridadInfos;
using Contabilidad.WebForms.App_Code.Administracion;
using Contabilidad.WebForms.App_Code.Contabilizacion;
using Contabilidad.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.Contabilizacion
{
    public partial class PlanoContabilizacion : BasePage
    {
        #region Instancias

        CentidadMetodos CentidadMetodos = new CentidadMetodos();
        Security seguridad = new Security();
        cClaseParametro clase = new cClaseParametro();
        Cgeneral general = new Cgeneral();
        cParametrosGenerales parametrosgenerales = new cParametrosGenerales();
        Cperiodos periodo = new Cperiodos();
        Ccontabilizacion contabilizacion = new Ccontabilizacion();


        #endregion Instancias

        #region Metodos


        private string cargarPlano()
        {
            string plano = "";
            int contador = 0;
            try
            {
                int ultimoregistro = contabilizacion.GeneraPlanoContabilizacion(Convert.ToInt32(ddlAño.SelectedValue), ddlTipo.SelectedValue.Trim(),
                    Convert.ToInt32(this.Session["empresa"]), txtNoComprobante.Text.Trim(), txtNota.Text.Trim(), Convert.ToInt32(txtRegistroCruce.Text),
                    Convert.ToDateTime(txtFechaIni.Text), ddlNumero.SelectedValue.Trim()).Count;


                foreach (DataRowView r in contabilizacion.GeneraPlanoContabilizacion(Convert.ToInt32(ddlAño.SelectedValue), ddlTipo.SelectedValue.Trim(),
                    Convert.ToInt32(this.Session["empresa"]), txtNoComprobante.Text.Trim(), txtNota.Text.Trim(), Convert.ToInt32(txtRegistroCruce.Text),
                    Convert.ToDateTime(txtFechaIni.Text), ddlNumero.SelectedValue.Trim()))
                {

                    if (r.Row.ItemArray.GetValue(0).ToString().Trim().Length > 0)
                        plano += r.Row.ItemArray.GetValue(0).ToString().Trim() + "\r\n";
                }

                return plano.Trim();
            }
            catch (Exception ex)
            {
                ManejoError("Error al generar el plano debido a: " + ex.Message, "A");
                return "";
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
            //try
            //{
            //    if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "C", Convert.ToInt16(Session["empresa"])) == 0)
            //    {
            //        ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
            //        return;
            //    }
            //    this.gvLista.DataSource = clase.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
            //    this.gvLista.DataBind();
            //    this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";
            //    seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
            //            this.gvLista.Rows.Count.ToString() + " Registros encontrados", HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            //}
            //catch (Exception ex)
            //{
            //    ManejoError("Error al cargar la tabla correspondiente a: " + ex.Message, "C");
            //}
        }

        private void ManejoError(string error, string operacion)
        {
            this.Session["error"] = error;
            this.Session["paginaAnterior"] = this.Page.Request.FilePath.ToString();

            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "er", error, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            this.Response.Redirect("~/Contabilidad/Error.aspx", false);
        }

        private void ManejoExito(string mensaje, string operacion)
        {
            this.nilblInformacion.Text = mensaje;
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
            object[] objKey = new object[] { this.txtNoComprobante.Text.Trim().ToString(), Convert.ToInt16(Session["empresa"]) };
            try
            {
                if (CentidadMetodos.EntidadGetKey("cClaseParametroContaNomi", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    this.nilblInformacion.Text = "Código " + this.txtNoComprobante.Text + " ya se encuentra registrado";
                    CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
                    this.nilbNuevo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al validar la llave primaria correspondiente a: " + ex.Message, "C");
            }
        }

        private void Guardar()
        {

            string plano = "";

            plano = cargarPlano();
            // nilblMensaje.Text = plano;

            this.Session["textoPlano"] = plano;
            string script = "<script language='javascript'>" +
                            "Visualizacion(" + Convert.ToString(this.Session["empresa"]) + "," + Convert.ToString(ddlAño.SelectedValue) + ",'" + Convert.ToString(ddlNumero.SelectedValue) + "')" +
                             "</script>";
            Page.RegisterStartupScript("Visualizacion", script);
        }

        #endregion Metodos

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
            {
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            }
            else
            {
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(),
                    ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) != 0)
                {


                    this.txtNoComprobante.Focus();

                }
                else
                {
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
                }
            }
        }



        protected void lbNuevo_Click(object sender, ImageClickEventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                                 ConfigurationManager.AppSettings["Modulo"].ToString(),
                                  nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            CcontrolesUsuario.HabilitarControles(
                this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);

            CargarCombos();

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            this.txtRegistroCruce.Text = "260";
            this.nilblInformacion.Text = "";
        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(
                this.Page.Controls);

            CcontrolesUsuario.LimpiarControles(
                this.Page.Controls);

            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
            this.txtRegistroCruce.Text = "260";
        }

        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();

        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                                        ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                object[] objValores = new object[] {
                //Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)),
                //Convert.ToInt16(Session["empresa"])
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete(
                    "cClaseParametroContaNomi",
                    "elimina",
                    "ppa",
                    objValores))
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
                    //ManejoError("El código ('" + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)) +
                    //"' - ' " + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[3].Text)) + "') tiene una asociación, no es posible eliminar el registro.", "E");
                }
                else
                {
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
                }
            }
        }


        protected void lbRegistrar_Click(object sender, ImageClickEventArgs e)
        {

            if (ddlAño.SelectedValue.Trim().Length == 0 || txtNoComprobante.Text.Trim().ToString().Length == 0 || txtFechaIni.Text.Length == 0 || ddlTipo.SelectedValue.Trim().Length == 0 || ddlAño.SelectedValue.Trim().Length == 0 || txtNota.Text.Length == 0 || txtRegistroCruce.Text.Length == 0 || txtNoComprobante.Text.Length == 0)
            {
                nilblMensaje.Text = "Campos vacios por favor corrija";
                return;
            }

            Guardar();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "A", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "A");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            this.txtNoComprobante.Enabled = false;

            CargarCombos();
            try
            {
                //if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                //    this.txtNoComprobante.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[2].Text);
                //else
                //    this.txtNoComprobante.Text = "";



                //if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                //    this.ddlTipo.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[4].Text);


                //foreach (Control objControl in this.gvLista.SelectedRow.Cells[8].Controls)
                //{
                //    //if (objControl is CheckBox)
                //        //this.chkActivo.Checked = ((CheckBox)objControl).Checked;
                //}

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }

        protected void niimbBuscar_Click(object sender, ImageClickEventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }



        protected void lbFechaIni_Click(object sender, EventArgs e)
        {
            this.CalendarFechaIni.Visible = true;
            this.txtFechaIni.Visible = false;
            this.CalendarFechaIni.SelectedDate = Convert.ToDateTime(null);
        }

        protected void CalendarFechaIni_SelectionChanged(object sender, EventArgs e)
        {
            this.CalendarFechaIni.Visible = false;
            this.txtFechaIni.Visible = true;
            this.txtFechaIni.Text = this.CalendarFechaIni.SelectedDate.ToShortDateString();
            //this.txtFechaIni.Enabled = true;
        }


        protected void ddlAño_SelectedIndexChanged1(object sender, EventArgs e)
        {
            manejotipo();
        }



        #endregion Eventos


        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAño.SelectedValue.Trim().Length == 0)
                {
                    nilblMensaje.Text = "Seleccione un año valido";
                    return;
                }
                manejotipo();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar documentos contables debido a: " + ex.Message, "C");
            }
        }

        private void manejotipo()
        {

            DataView numero = contabilizacion.SeleccionaDocumentostipo(ddlTipo.SelectedValue.Trim(), Convert.ToInt32(this.Session["empresa"]), Convert.ToInt32(ddlAño.SelectedValue));
            ddlNumero.DataSource = numero;
            ddlNumero.DataValueField = "numero";
            ddlNumero.DataTextField = "cadena";
            ddlNumero.DataBind();
        }



        protected void txtFechaIni_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFechaIni.Text);
            }
            catch
            {

                nilblInformacion.Text = "formato de fecha no valido..";
                txtFechaIni.Text = "";
                txtFechaIni.Focus();
                return;
            }
        }
    }
}