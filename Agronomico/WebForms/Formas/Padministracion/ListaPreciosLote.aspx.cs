using Agronomico.WebForms.App_Code.Administracion;
using Agronomico.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agronomico.WebForms.Formas.Padministracion
{
    public partial class ListaPreciosLote : BasePage
    {

        #region Instancias

        Cseccion sesion = new Cseccion();
        CListaPreciosLote listaPrecios = new CListaPreciosLote();
        Clineas lineas;
        Cnovedad novedad = new Cnovedad();
        Clotes lotes = new Clotes();
        Cseccion sesionesFinca = new Cseccion();
        Cfinca finca = new Cfinca();


        #endregion Instancias

        #region Metodos



        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                                        ConfigurationManager.AppSettings["Modulo"].ToString(),
                                        nombrePaginaActual(), "C", Convert.ToInt32(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                this.gvLista.DataSource = listaPrecios.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt32(this.Session["empresa"]));
                this.gvLista.DataBind();


                gvLista.Visible = true;

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                    this.gvLista.Rows.Count.ToString() + " Registros encontrados", ObtenerIP(), Convert.ToInt32(Session["empresa"]));

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }




        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");

            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);

            this.nilbNuevo.Visible = true;

            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "ex", mensaje, ObtenerIP(), Convert.ToInt32(Session["empresa"]));
            GetEntidad();
        }

        private void CargarCombos()
        {
            try
            {
                this.ddlAño.DataSource = lotes.PeriodoAñoAbierto(Convert.ToInt32(Session["empresa"]));
                this.ddlAño.DataValueField = "año";
                this.ddlAño.DataTextField = "año";
                this.ddlAño.DataBind();
                this.ddlAño.Items.Insert(0, new ListItem("Año...", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }


            try
            {
                this.ddlFinca.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("aFinca", "ppa"), "descripcion", Convert.ToInt32(this.Session["empresa"]));
                this.ddlFinca.DataValueField = "codigo";
                this.ddlFinca.DataTextField = "descripcion";
                this.ddlFinca.DataBind();
                this.ddlFinca.Items.Insert(0, new ListItem("", ""));

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }

            try
            {
                this.ddlNovedad.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("aNovedad", "ppa"), "descripcion", Convert.ToInt32(this.Session["empresa"]));
                this.ddlNovedad.DataValueField = "codigo";
                this.ddlNovedad.DataTextField = "descripcion";
                this.ddlNovedad.DataBind();
                this.ddlNovedad.Items.Insert(0, new ListItem("", ""));

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }


        }

        protected void cargarSesiones()
        {
            if (chkSoloSeccion.Checked == true)
            {

                if (ddlFinca.SelectedValue.Trim().Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar una finca", "warning");
                    chkSoloSeccion.Checked = false;
                    return;

                }

                try
                {
                    ddlSeccion.Enabled = true;

                    this.ddlSeccion.DataSource = sesion.SeleccionaSesionesFinca(Convert.ToInt32(this.Session["empresa"]), ddlFinca.SelectedValue);
                    this.ddlSeccion.DataValueField = "codigo";
                    this.ddlSeccion.DataTextField = "descripcion";
                    this.ddlSeccion.DataBind();
                    this.ddlSeccion.Items.Insert(0, new ListItem("Selecciona una opción", ""));
                }
                catch (Exception ex)
                {
                    ManejoErrorCatch(ex);
                }

            }
            else
            {
                this.ddlSeccion.DataSource = null;
                this.ddlSeccion.DataBind();
                ddlSeccion.Enabled = false;
            }

        }

        protected void cargarLotes()
        {
            if (chkSinLote.Checked == true)
            {

                if (ddlFinca.SelectedValue.Trim().Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar una finca", "warning");
                    chkSoloSeccion.Checked = false;
                    return;

                }

                ddlLote.Enabled = true;
                try
                {
                    this.ddlLote.DataSource = lotes.LotesSeccionFinca(this.ddlSeccion.SelectedValue.ToString().Trim(), Convert.ToInt32(this.Session["empresa"]), ddlFinca.SelectedValue.ToString().Trim());
                    this.ddlLote.DataValueField = "codigo";
                    this.ddlLote.DataTextField = "descripcion";
                    this.ddlLote.DataBind();
                    this.ddlLote.Items.Insert(0, new ListItem("Selecciona una opción", ""));
                }
                catch (Exception ex)
                {
                    ManejoErrorCatch(ex);
                }
            }
            else
            {
                this.ddlLote.DataSource = null;
                this.ddlLote.DataBind();
                ddlLote.Enabled = false;
            }

        }


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(),
                    ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt32(Session["empresa"])) != 0)
                    ddlAño.Focus();
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }

        }
        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }
        protected void lbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "I", Convert.ToInt32(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);
            CargarCombos();
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;

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
            this.Session["editar"] = false;

        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (ddlAño.SelectedValue.Length == 0 || ddlNovedad.SelectedValue.Length == 0 || ddlFinca.SelectedValue.Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Campos vacios por favor corrija", "warning");
                return;
            }

            Guardar();


        }


        private void Guardar()
        {
            bool validar = false;
            string operacion = "inserta";
            bool modificado = false;

            try
            {
                operacion = "inserta";
                if (Convert.ToBoolean(this.Session["editar"]) == true)
                    operacion = "actualiza";

                decimal precioTerceros = Convert.ToDecimal(txvPrecioTerceros.Text);
                decimal precioContratistas = Convert.ToDecimal(txvPrecioContratistas.Text);
                decimal precioOtros = Convert.ToDecimal(txvPrecioOtros.Text);
                decimal porcentaje = Convert.ToDecimal(txvPorcentaje.Text);
                bool baseSueldo = Convert.ToBoolean(chkBaseSueldo.Checked);

                object[] objValores = new object[]{
                                            Convert.ToDecimal( ddlAño.SelectedValue.Trim()),  //@año
                                            baseSueldo, //@baseSueldo
                                              Convert.ToInt16( this.Session["empresa"]), //@empresa
                                               DateTime.Now, //@fechaRegistro
                                               ddlFinca.SelectedValue,
                                               ddlLote.SelectedValue,
                                               modificado, //@modificado
                                               ddlNovedad.SelectedValue, //@novedad
                                               porcentaje, //@porcentaje
                                               precioContratistas, //@precioContratistas
                                               precioTerceros, //@precioDestajo
                                               precioOtros, //@precioOtros
                                               ddlSeccion.SelectedValue, //@registro
                                               this.Session["usuario"].ToString() //@usuario           
                };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("aNovedadPrecio", operacion, "ppa", objValores))
                {
                    case 1:
                        validar = true;
                        break;
                }

                if (!validar)
                    ManejoExito("Datos insertados satisfactoriamente", operacion.Substring(0, 1).ToUpper());
                else
                    ManejoError("Errores al insertar el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                              nombrePaginaActual(), editar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", editar);
                return;
            }


            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            CargarCombos();
            this.ddlNovedad.Enabled = false;
            this.ddlLote.Enabled = false;
            this.ddlFinca.Enabled = false;
            this.ddlAño.Enabled = false;
            this.txvPrecioTerceros.Focus();
            nilblInformacion.Text = "";


            try
            {
                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.ddlAño.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[2].Text);

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    this.ddlFinca.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[3].Text);


                if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                {
                    chkSoloSeccion.Checked = true;
                    cargarSesiones();
                    ddlSeccion.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[5].Text.Trim());
                }


                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                {
                    chkSinLote.Checked = true;
                    cargarLotes();
                    ddlLote.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[6].Text);
                }
                if (this.gvLista.SelectedRow.Cells[7].Text != "&nbsp;")
                    ddlNovedad.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[7].Text.Trim());

                if (this.gvLista.SelectedRow.Cells[9].Text != "&nbsp;")
                    this.txvPrecioTerceros.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[9].Text);
                else
                    this.txvPrecioTerceros.Text = "0";

                if (this.gvLista.SelectedRow.Cells[10].Text != "&nbsp;")
                    this.txvPrecioContratistas.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[10].Text);
                else
                    this.txvPrecioContratistas.Text = "0";

                if (this.gvLista.SelectedRow.Cells[11].Text != "&nbsp;")

                    this.txvPrecioOtros.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[11].Text);
                else
                    this.txvPrecioOtros.Text = "0";

                if (this.gvLista.SelectedRow.Cells[12].Text != "&nbsp;")

                    this.txvPorcentaje.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[12].Text);
                else
                    this.txvPorcentaje.Text = "0";

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[13].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkBaseSueldo.Checked = ((CheckBox)objControl).Checked;
                }


            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }

        }

        protected void chkBaseSueldo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBaseSueldo.Checked == true)
            {
                txvPrecioTerceros.Enabled = false;
                txvPrecioContratistas.Enabled = false; ;
                txvPrecioOtros.Enabled = false;
                txvPorcentaje.Enabled = false;
            }
            else
            {
                txvPrecioTerceros.Enabled = true;
                txvPrecioContratistas.Enabled = true;
                txvPrecioOtros.Enabled = true;
                txvPorcentaje.Enabled = true;

            }
        }
        protected void chkSoloSeccion_CheckedChanged(object sender, EventArgs e)
        {
            cargarSesiones();
        }
        protected void chkSinLote_CheckedChanged(object sender, EventArgs e)
        {

            cargarLotes();
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "E", Convert.ToInt32(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                object[] objValores = new object[] { Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)), Convert.ToInt32(Session["empresa"]),
            Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[3].Text)),Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[6].Text)),
            Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[7].Text)),Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[5].Text))};


                switch (CentidadMetodos.EntidadInsertUpdateDelete("aNovedadPrecio", "elimina", "ppa", objValores))
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
                    "' - ' " + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[3].Text)) + "') tiene una asociación, no es posible eliminar el registro. " + ex.Message, "E");
                }
                else
                    ManejoErrorCatch(ex);
            }
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
    }
}