using Agronomico.WebForms.App_Code.Administracion;
using Agronomico.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agronomico.WebForms.Formas.Padministracion
{
    public partial class ListaPrecios : BasePage
    {

        #region Instancias

        Cseccion sesion = new Cseccion();
        CListaPrecios listaPrecios = new CListaPrecios();
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

                this.gvLista.DataSource = listaPrecios.BuscarEntidad(Convert.ToInt32(this.Session["empresa"]));
                this.gvLista.DataBind();

                gvNovedades.DataSource = null;
                gvNovedades.DataBind();
                gvLista.Visible = true;

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(
                    this.Session["usuario"].ToString(), "C",
                  ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
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

            //seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
            //    "ex", mensaje, ObtenerIP(), Convert.ToInt32(Session["empresa"]));

            GetEntidad();
        }

        private void CargarCombos()
        {
            try
            {
                this.ddlAño.DataSource = lotes.PeriodoAñoAbiertoNomina(Convert.ToInt32(Session["empresa"]));
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
                this.ddlAñoAnterior.DataSource = lotes.PeriodoAñoAbiertoNomina(Convert.ToInt32(Session["empresa"]));
                this.ddlAñoAnterior.DataValueField = "año";
                this.ddlAñoAnterior.DataTextField = "año";
                this.ddlAñoAnterior.DataBind();
                this.ddlAñoAnterior.Items.Insert(0, new ListItem("Año...", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }

            try
            {
                this.ddlAñoActual.DataSource = lotes.PeriodoAñoAbiertoNomina(Convert.ToInt32(Session["empresa"]));
                this.ddlAñoActual.DataValueField = "año";
                this.ddlAñoActual.DataTextField = "año";
                this.ddlAñoActual.DataBind();
                this.ddlAñoActual.Items.Insert(0, new ListItem("Año...", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }



        }

        private void cargarNovedades()
        {
            if (novedad.verificarRegistroPrecio(Convert.ToInt32(ddlAño.SelectedValue.Trim()), Convert.ToInt16(this.Session["empresa"])) == 1)
            {
                this.Session["editar"] = true;
            }
            else
            {
                this.Session["editar"] = false;
            }

            if (ddlAño.SelectedValue.Trim().Length > 0)
            {
                gvNovedades.DataSource = novedad.SeleccionaNovedadPrecios(Convert.ToInt16(this.Session["empresa"]), Convert.ToInt32(ddlAño.SelectedValue.Trim()));
                gvNovedades.DataBind();
                gvNovedades.Visible = true;
            }
            else
            {
                gvNovedades.DataSource = null;
                gvNovedades.DataBind();
                gvNovedades.Visible = false;
            }

        }


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
            {
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            }
            else
            {
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(),
                    ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt32(Session["empresa"])) != 0)
                {
                    ddlAño.Focus();

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
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }
        protected void lbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                                       ConfigurationManager.AppSettings["Modulo"].ToString(),
                                       nombrePaginaActual(), "I", Convert.ToInt32(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            gvNovedades.DataSource = null;
            gvNovedades.DataBind();
            gvNovedades.Visible = false;

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);

            CargarCombos();

            this.nilbNuevo.Visible = false;
            gvNovedades.DataSource = null;
            gvNovedades.DataSource = null;
            this.Session["editar"] = false;

            this.nilblInformacion.Text = "";
        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();

            this.gvNovedades.DataSource = null;
            this.gvNovedades.DataBind();

            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
            this.Session["editar"] = false;
            gvNovedades.DataSource = null;
            gvNovedades.DataBind();
            gvNovedades.Visible = false;
        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            bool validar = false;
            foreach (GridViewRow gvr in gvNovedades.Rows)
            {
                if (((TextBox)gvr.FindControl("txtPrecioTerceros")).Text.Length == 0 || ((TextBox)gvr.FindControl("txtPrecioContratistas")).Text.Length == 0
                    || ((TextBox)gvr.FindControl("txtPrecioOtros")).Text.Length == 0)
                {
                    validar = true;
                }

            }

            if (validar == true)
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
                using (var ts = new TransactionScope())
                {
                    operacion = "inserta";

                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                    {
                        modificado = true;
                        operacion = "inserta";
                        object[] objValoresEliminaDetalle = new object[] { Convert.ToDecimal(ddlAño.SelectedValue.Trim()), Convert.ToInt16(this.Session["empresa"]) };

                        switch (CentidadMetodos.EntidadInsertUpdateDelete("anovedadloteprecio", "elimina", "ppa", objValoresEliminaDetalle))
                        {
                            case 1:
                                validar = true;
                                break;
                        }
                    }

                    foreach (GridViewRow r in gvNovedades.Rows)
                    {

                        decimal precioTerceros = Convert.ToDecimal(((TextBox)r.FindControl("txtPrecioTerceros")).Text);
                        decimal precioContratistas = Convert.ToDecimal(((TextBox)r.FindControl("txtPrecioContratistas")).Text);
                        decimal precioOtros = Convert.ToDecimal(((TextBox)r.FindControl("txtPrecioOtros")).Text);
                        decimal porcentaje = Convert.ToDecimal(((TextBox)r.FindControl("txtPorcentaje")).Text);
                        bool baseSueldo = Convert.ToBoolean(((CheckBox)r.FindControl("chkBaseSueldo")).Checked);

                        object[] objValores = new object[]{
                                            Convert.ToDecimal( ddlAño.SelectedValue.Trim()),  //@año
                                            baseSueldo, //@baseSueldo
                                              (int) this.Session["empresa"], //@empresa
                                               DateTime.Now, //@fechaRegistro
                                               modificado, //@modificado
                                               r.Cells[0].Text.Trim(), //@novedad
                                               porcentaje, //@porcentaje
                                               precioContratistas, //@precioContratistas
                                               precioTerceros, //@precioDestajo
                                               precioOtros, //@precioOtros
                                               r.RowIndex, //@registro
                                               this.Session["usuario"].ToString() //@usuario           
                };

                        switch (CentidadMetodos.EntidadInsertUpdateDelete("anovedadloteprecio", operacion, "ppa", objValores))
                        {
                            case 1:
                                validar = true;
                                break;
                        }

                    }

                    if (!validar)
                    {
                        ManejoExito("Datos insertados satisfactoriamente", operacion.Substring(0, 1).ToUpper());
                        ts.Complete();
                    }
                    else
                    {
                        ManejoError("Errores al insertar el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());

                    }

                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }

        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvNovedades.PageIndex = e.NewPageIndex;
            DataView Lote = lotes.SeleccionaLoteDetalle(Convert.ToInt32(this.Session["empresa"]), ddlAño.SelectedValue.Trim());
            gvNovedades.DataSource = Lote;
            gvNovedades.DataBind();

        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CcontrolesUsuario.HabilitarControles(Page.Controls);
                CargarCombos();
                if (gvLista.Rows[gvLista.SelectedIndex].Cells[1].Text != "&nbsp;")
                    ddlAño.SelectedValue = gvLista.Rows[gvLista.SelectedIndex].Cells[1].Text.Trim();
                else
                    ddlAño.SelectedValue = "";
                ddlAño.Enabled = false;
                cargarNovedades();
                nilbNuevo.Visible = false;
                gvLista.Visible = false;

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        protected void ddlAño_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cargarNovedades();

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar novedades debido a : " + ex.Message, "C");

            }
        }
        protected void chkBaseSueldo_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow gv in gvNovedades.Rows)
            {
                if ((((CheckBox)gv.FindControl("chkBaseSueldo")).Checked == true))
                {
                    ((TextBox)gv.FindControl("txtPrecioTerceros")).Enabled = false;
                    ((TextBox)gv.FindControl("txtPrecioContratistas")).Enabled = false; ;
                    ((TextBox)gv.FindControl("txtPrecioOtros")).Enabled = false;
                    ((TextBox)gv.FindControl("txtPorcentaje")).Enabled = false;
                }
                else
                {
                    ((TextBox)gv.FindControl("txtPrecioTerceros")).Enabled = true;
                    ((TextBox)gv.FindControl("txtPrecioContratistas")).Enabled = true;
                    ((TextBox)gv.FindControl("txtPrecioOtros")).Enabled = true;
                    ((TextBox)gv.FindControl("txtPorcentaje")).Enabled = true;

                }
            }
        }

        protected void btnEjecutar_Click(object sender, EventArgs e)
        {
            try
            {

                switch (listaPrecios.EjecutaReplicacionPrecioLabores(Convert.ToInt32(Session["empresa"]), Convert.ToInt32(ddlAñoAnterior.SelectedValue),
                    Convert.ToInt32(ddlAñoActual.SelectedValue), Session["usuario"].ToString()))
                {
                    case 0:
                        ManejoExito("Datos insertados satisfactoriamente", "I");
                        break;
                    case 1:
                        ManejoError("Errores al insertar el registro. Operación no realizada", "I");
                        break;
                    case 2:
                        ManejoError("Errores al insertar el registro, el año acterior de la tabla no existe. Operación no realizada", "I");
                        break;
                    case 3:
                        ManejoError("Errores al insertar el registro, la tabla ya esta replicada para el año actual. Operación no realizada", "I");
                        break;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al ejecutar novedades de replicacion debido a : " + ex.Message, "C");
            }
        }


    }
}