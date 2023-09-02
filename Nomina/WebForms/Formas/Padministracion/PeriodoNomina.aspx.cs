using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Padministracion
{
    public partial class PeriodoNomina : BasePage
    {
        #region Instancias

        Cperiodos periodos = new Cperiodos();
        List<CperiodoDetalle> listaPeriodoDetalle = new List<CperiodoDetalle>();
        CperiodoDetalle periodoDetalle = new CperiodoDetalle();


        #endregion Instancias

        #region Metodos


        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                     nombrePaginaActual(), "C", Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }


                this.gvLista.DataSource = periodos.BuscarEntidad(this.nitxtBusqueda.Text, Convert.ToInt16(this.Session["empresa"]));
                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C",
                   ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                  "ex", this.gvLista.Rows.Count.ToString() + " Registros encontrados", ObtenerIP(), Convert.ToInt16(this.Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente a: " + ex.Message, "C");
            }
        }

        private void ManejoExito(string mensaje, string operacion)
        {
            nilblInformacion.Text = "";
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");

            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);

            //CcontrolesUsuario.InhabilitarControles(         this.upDetalle.Controls);
            //CcontrolesUsuario.LimpiarControles(            this.upDetalle.Controls);

            //upDetalle.Visible = false;

            seguridad.InsertaLog(
                this.Session["usuario"].ToString(),
                operacion,
                ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "ex",
                mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));

            GetEntidad();
        }
        private void CargarCombos()
        {
            nilblInformacion.Text = "";
            try
            {
                this.ddlAño.DataSource = periodos.PeriodoAñoAbierto(Convert.ToInt16(Session["empresa"]));
                this.ddlAño.DataValueField = "año";
                this.ddlAño.DataTextField = "año";
                this.ddlAño.DataBind();
                this.ddlAño.Items.Insert(0, new ListItem("Año...", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar año. Correspondiente a: " + ex.Message, "C");
            }

            try
            {

                this.ddlTipoNomina.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nTipoNomina", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"])); ;
                this.ddlTipoNomina.DataValueField = "codigo";
                this.ddlTipoNomina.DataTextField = "descripcion";
                this.ddlTipoNomina.DataBind();
                this.ddlTipoNomina.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar cargos. Correspondiente a: " + ex.Message, "C");
            }
        }
        private void EntidadKey()
        {
            object[] objKey = new object[] { this.ddlAño.SelectedValue, Convert.ToInt16(this.Session["empresa"]) };

            try
            {
                if (CentidadMetodos.EntidadGetKey("nPeriodo", "ppa", objKey).Tables[0].Rows.Count > 0)
                {

                    MostrarMensaje("El año " + this.ddlAño.SelectedValue + " ya se encuentra registrado");

                    CcontrolesUsuario.InhabilitarControles(this.Page.Controls);


                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al validar la llave primaria correspondiente a: " + ex.Message, "C");
            }
        }



        private void GuardarDetalle()
        {

            string operacion = "inserta";
            int noPeriodo = 0;



            try
            {
                using (TransactionScope ts = new TransactionScope())
                {

                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                    {
                        operacion = "actualiza";
                        noPeriodo = Convert.ToInt16(txvPeriodoDetalle.Text);
                    }
                    else
                        noPeriodo = periodos.consecutivoPeriodoAño(Convert.ToInt16(Session["empresa"]), Convert.ToInt16(ddlAño.SelectedValue));



                    object[] objValores = new object[]{
                                chkAgronomico.Checked,//@agronomico
                                ddlAño.SelectedValue,        //                @año	int
                                chkCerrado.Checked,     //@cerrado	bit
                                Convert.ToInt32(txvDiasNomina.Text), //@diasNomina
                                Convert.ToInt16(this.Session["empresa"]),    //@empresa	int
                                Convert.ToDateTime( txtFechaCorte.Text ),  //@fechaCorte	date
                                Convert.ToDateTime(txtFechaFinal.Text),     //@fechaFinal	date
                                Convert.ToDateTime(txtFechaIni.Text),        //@fechaInicial	date
                                Convert.ToDateTime(txtFechaPago.Text),        //@fechaPago	datetime
                                DateTime.Now,    //@fechaRegistro	datetime
                                Convert.ToInt16( ddlMes.SelectedValue ) ,  //@mes	int
                                noPeriodo,    //@noPeriodo	int
                                ddlTipoNomina.SelectedValue.Trim(),   //@tipoNomina	varchar
                                (string)this.Session["usuario"]    //@usuario	varchar
            };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("nPeriodoDetalle", operacion, "ppa", objValores))
                    {

                        case 0:
                            ManejoExito("Datos insertados satisfactoriamente", operacion.Substring(0, 1).ToUpper());
                            ts.Complete();
                            break;

                        case 1:

                            ManejoError("Errores al insertar el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                            break;
                    }


                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar los datos correspondiente a: " + ex.Message, operacion.Substring(0, 1).ToUpper());
            }

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

                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                   nombrePaginaActual(), Convert.ToInt16(this.Session["empresa"])) != 0)
                {
                    this.nitxtBusqueda.Focus();

                }
                else
                {
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
                }
            }
        }

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.Session["editar"] = null;
            // upDetalle.Visible = false;
            nilbNuevo.Visible = true;
            GetEntidad();
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
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);

            this.Session["editar"] = false;

            CargarCombos();

            nilbNuevo.Visible = false;
            lbCancelar.Visible = true;
            lbRegistrar.Visible = true;

            this.ddlAño.Focus();
            this.nilblInformacion.Text = "";

        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            nilbNuevo.Visible = true;
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
            this.Session["editar"] = true;
            nilbNuevo.Visible = false;
            this.ddlAño.Enabled = false;
            this.ddlTipoNomina.Enabled = false;
            this.txvPeriodoDetalle.Enabled = false;
            nilblInformacion.Text = "";
            CargarCombos();


            try
            {
                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.ddlAño.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[2].Text);

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    ddlMes.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[3].Text.Trim());

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                    txvPeriodoDetalle.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[4].Text.Trim());

                if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                    txtFechaIni.Text = Convert.ToDateTime(Server.HtmlDecode(this.gvLista.SelectedRow.Cells[5].Text)).ToShortDateString();

                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                    txtFechaFinal.Text = Convert.ToDateTime(Server.HtmlDecode(this.gvLista.SelectedRow.Cells[6].Text)).ToShortDateString();

                if (this.gvLista.SelectedRow.Cells[7].Text != "&nbsp;")
                    txtFechaCorte.Text = Convert.ToDateTime(Server.HtmlDecode(this.gvLista.SelectedRow.Cells[7].Text)).ToShortDateString();

                if (this.gvLista.SelectedRow.Cells[8].Text != "&nbsp;")
                    txtFechaPago.Text = Convert.ToDateTime(Server.HtmlDecode(this.gvLista.SelectedRow.Cells[8].Text)).ToShortDateString();

                if (this.gvLista.SelectedRow.Cells[9].Text != "&nbsp;")
                    ddlTipoNomina.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[9].Text.Trim());

                if (this.gvLista.SelectedRow.Cells[10].Text != "&nbsp;")
                    txvDiasNomina.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[10].Text.Trim());

                foreach (Control con in this.gvLista.SelectedRow.Cells[11].Controls)
                {
                    if (con is CheckBox)
                        chkCerrado.Checked = ((CheckBox)con).Checked;
                }

                foreach (Control con in this.gvLista.SelectedRow.Cells[12].Controls)
                {
                    if (con is CheckBox)
                        chkAgronomico.Checked = ((CheckBox)con).Checked;
                }

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }
        protected void gvista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void gvLista_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }

        protected void txvAño_TextChanged(object sender, EventArgs e)
        {


        }

        protected void ddlAño_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAño.SelectedValue.Trim().Length > 0)
                {
                    txvPeriodoDetalle.Text = periodos.consecutivoPeriodoAño(Convert.ToInt16(Session["empresa"]), Convert.ToInt16(ddlAño.SelectedValue)).ToString();
                    //upDetalle.Visible = true;
                    //CcontrolesUsuario.HabilitarControles(upDetalle.Controls);
                    txvPeriodoDetalle.Enabled = false;
                }
                else
                {
                    MostrarMensaje("Seleccione un año valido para continuar");
                    // upDetalle.Visible = false;
                    //CcontrolesUsuario.InhabilitarControles(upDetalle.Controls);
                    return;

                }
            }
            catch (Exception ex)
            {

                ManejoError("Error al cargar consecutivo de periodo debido a:   " + ex.Message, "I");
            }
        }



        protected void btnGuardarDetalle_Click(object sender, EventArgs e)
        {
            DateTime fechaInicial = new DateTime();
            DateTime fechaFinal = new DateTime();
            DateTime fechaPago = new DateTime();
            DateTime fechaCorte = new DateTime();

            try
            {


                fechaCorte = Convert.ToDateTime(txtFechaCorte.Text);
                fechaFinal = Convert.ToDateTime(txtFechaFinal.Text);
                fechaPago = Convert.ToDateTime(txtFechaPago.Text);
                fechaInicial = Convert.ToDateTime(txtFechaIni.Text);

            }
            catch
            {
                nilblInformacion.Text = "Formato de fecha no validas por favor corrija";
                return;

            }

            if (ddlTipoNomina.SelectedValue.Trim().Length == 0 || ddlAño.SelectedValue.Trim().Length == 0)
            {
                nilblInformacion.Text = "Campos de selección vacios";
                return;
            }

            int messeleccionado = Convert.ToInt32(ddlMes.SelectedValue);
            int añoseleccionado = Convert.ToInt32(ddlAño.SelectedValue);

            if (fechaInicial > fechaFinal)
            {
                nilblInformacion.Text = "La fecha inicial debe ser menor  fecha final";
                return;
            }

            //if (fechaInicial.Year != añoseleccionado)
            //{
            //    nilblInformacion.Text = "La fecha inicial debe pertenecer al mismo año del seleccionado";
            //    return;
            //}

            //else if (fechaInicial.Month != messeleccionado)
            //{
            //    nilblInformacion.Text = "La fecha inicial debe pertenecer al mismo mes del seleccionado";
            //    return;
            //}

            //if (fechaCorte.Year != añoseleccionado || fechaPago.Year != añoseleccionado)
            //{

            //    nilblInformacion.Text = "La fecha de corte o de pago debe tener el mismo año seleccionado";
            //}

            GuardarDetalle();
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

                         Convert.ToInt32(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)),//    @año	int
                                Convert.ToInt16(Session["empresa"]),    //@empresa	int
                               Convert.ToInt32(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[3].Text)) ,   //@mes	int
                               Convert.ToInt32(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[4].Text))      //@noPeriodo	int
              ,
                Convert.ToInt16(Session["empresa"])
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete(
                    "nPeriodoDetalle",
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
                    ManejoError("El código ('" + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)) +
                    "' - ' " + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[3].Text)) + "') tiene una asociación, no es posible eliminar el registro, debido a: " + ex.Message, "E");
                }
                else
                {
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
                }
            }
        }
        protected void btnCancelarDetalle_Click(object sender, EventArgs e)
        {
            //CcontrolesUsuario.InhabilitarControles(         this.upDetalle.Controls);

            //CcontrolesUsuario.LimpiarControles(            this.upDetalle.Controls);
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);

            nilbNuevo.Visible = true;
            lbRegistrar.Visible = false;
            lbCancelar.Visible = false;
            this.Session["editar"] = null;
            // upDetalle.Visible = false;
        }
        protected void btnNuevoDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                this.Session["editar"] = null;
                CcontrolesUsuario.HabilitarControles(Page.Controls);
                CargarCombos();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los combos debido a :  " + ex.Message, "I");

            }
        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            DateTime fechaInicial = new DateTime();
            DateTime fechaFinal = new DateTime();
            DateTime fechaPago = new DateTime();
            DateTime fechaCorte = new DateTime();

            try
            {


                fechaCorte = Convert.ToDateTime(txtFechaCorte.Text);
                fechaFinal = Convert.ToDateTime(txtFechaFinal.Text);
                fechaPago = Convert.ToDateTime(txtFechaPago.Text);
                fechaInicial = Convert.ToDateTime(txtFechaIni.Text);

            }
            catch
            {
                nilblInformacion.Text = "Formato de fecha no validas por favor corrija";
                return;

            }

            if (ddlTipoNomina.SelectedValue.Trim().Length == 0 || ddlAño.SelectedValue.Trim().Length == 0)
            {
                nilblInformacion.Text = "Campos de selección vacios";
                return;
            }

            int messeleccionado = Convert.ToInt32(ddlMes.SelectedValue);
            int añoseleccionado = Convert.ToInt32(ddlAño.SelectedValue);

            if (fechaInicial > fechaFinal)
            {
                nilblInformacion.Text = "La fecha inicial debe ser menor  fecha final";
                return;
            }


            if (txvDiasNomina.Text.Trim().Length == 0)
            {
                nilblInformacion.Text = "Ingrese dias de nomina por favor";
                return;
            }


            GuardarDetalle();
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }

        #endregion Eventos
    }
}