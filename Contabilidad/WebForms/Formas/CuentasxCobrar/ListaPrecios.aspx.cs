using Contabilidad.WebForms.App_Code.Administracion;
using Contabilidad.WebForms.App_Code.CuentasxCobrar;
using Contabilidad.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.CuentasxCobrar
{
    public partial class ListaPrecios : BasePage
    {

        #region Instancias

        CListaPrecios listaPrecios = new CListaPrecios();
        Citems novedad = new Citems();
        Cperiodos periodo = new Cperiodos();

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

                this.gvLista.DataSource = listaPrecios.BuscarEntidad(Convert.ToInt32(this.Session["empresa"]), nitxtBusqueda.Text);
                this.gvLista.DataBind();

                gvNovedades.DataSource = null;
                gvNovedades.DataBind();
                gvLista.Visible = true;

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                    this.gvLista.Rows.Count.ToString() + " Registros encontrados", ObtenerIP(), Convert.ToInt32(Session["empresa"]));

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente a: " + ex.Message, "C");
            }
        }


        private void ManejoExito(string mensaje, string operacion)
        {
            this.nilblInformacion.Text = mensaje;

            CcontrolesUsuario.InhabilitarControles(this.formContainer.Controls);
            CcontrolesUsuario.LimpiarControles(this.formContainer.Controls);

            this.nilbNuevo.Visible = true;

            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "ex", mensaje, ObtenerIP(), Convert.ToInt32(Session["empresa"]));

            GetEntidad();
        }

        private void cargarMes(int año)
        {
            try
            {
                this.ddlMes.DataSource = periodo.GetMesPeriodos(Convert.ToInt32(Session["empresa"]), año);
                this.ddlMes.DataValueField = "mes";
                this.ddlMes.DataTextField = "descripcion";
                this.ddlMes.DataBind();
                this.ddlMes.Items.Insert(0, new ListItem("Meses...", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar mes. Correspondiente a: " + ex.Message, "C");
            }
        }

        private void CargarCombos()
        {
            try
            {
                this.ddlAño.DataSource = periodo.GetAnosPeriodos(Convert.ToInt32(Session["empresa"]));
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
                DataView claseProveedor = OrdenarEntidad(CentidadMetodos.EntidadGet("cxcClaseCliente", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"]));
                claseProveedor.RowFilter = "empresa=" + Convert.ToInt16(this.Session["empresa"]);
                this.ddlClaseCliente.DataSource = claseProveedor;
                this.ddlClaseCliente.DataValueField = "codigo";
                this.ddlClaseCliente.DataTextField = "descripcion";
                this.ddlClaseCliente.DataBind();
                this.ddlClaseCliente.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }


        }

        private void cargarNovedades()
        {
            if (novedad.verificarRegistroPrecio(Convert.ToInt32(ddlAño.SelectedValue.Trim()), Convert.ToInt32(ddlMes.SelectedValue.Trim()), ddlClaseCliente.SelectedValue, (int)this.Session["empresa"]) == 1)
                this.Session["editar"] = true;
            else
                this.Session["editar"] = false;

            if (ddlAño.SelectedValue.Trim().Length > 0 && ddlMes.SelectedValue.Length > 0 && ddlClaseCliente.SelectedValue.Length > 0)
            {
                gvNovedades.DataSource = novedad.SeleccionaNovedadPrecios(Convert.ToInt32(ddlAño.SelectedValue.Trim()), Convert.ToInt32(ddlMes.SelectedValue.Trim()), ddlClaseCliente.SelectedValue, (int)this.Session["empresa"]);
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
        private void Guardar()
        {
            bool validar = false;
            string operacion = "inserta";
            bool modificado = false;

            try
            {

                operacion = "inserta";

                if (Convert.ToBoolean(this.Session["editar"]) == true)
                {
                    modificado = true;
                    operacion = "inserta";
                    object[] objValoresEliminaDetalle = new object[] { Convert.ToDecimal(ddlAño.SelectedValue.Trim()), ddlClaseCliente.SelectedValue, (int)this.Session["empresa"], Convert.ToDecimal(ddlMes.SelectedValue.Trim()), };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("cListaPrecioItem", "elimina", "ppa", objValoresEliminaDetalle))
                    {
                        case 1:
                            validar = true;
                            break;
                    }
                }

                foreach (GridViewRow r in gvNovedades.Rows)
                {

                    decimal precioTerceros = Convert.ToDecimal(((TextBox)r.FindControl("txtPrecioTerceros")).Text);

                    object[] objValores = new object[]{
                                            Convert.ToInt32( ddlAño.SelectedValue.Trim()),  //@año
                                            ddlClaseCliente.SelectedValue,
                                              (int) this.Session["empresa"], //@empresa
                                               Convert.ToInt32(r.Cells[0].Text.Trim()), //@novedad
                                               Convert.ToInt32(ddlMes.SelectedValue),
                                               precioTerceros
                };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("cListaPrecioItem", operacion, "ppa", objValores))
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


            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar los datos correspondiente a: " + ex.Message, operacion.Substring(0, 1).ToUpper());
            }

        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt32(Session["empresa"])) != 0)
                    ddlAño.Focus();
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }

        }
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CcontrolesUsuario.HabilitarControles(this.formContainer.Controls);

                ddlAño.Enabled = false;
                ddlMes.Enabled = false;
                ddlClaseCliente.Enabled = false;
                CargarCombos();
                if (gvLista.Rows[gvLista.SelectedIndex].Cells[2].Text != "&nbsp;")
                {
                    ddlAño.SelectedValue = gvLista.Rows[gvLista.SelectedIndex].Cells[2].Text.Trim();
                    cargarMes(Convert.ToInt32(ddlAño.SelectedValue));
                }
                if (gvLista.Rows[gvLista.SelectedIndex].Cells[3].Text != "&nbsp;")
                    ddlMes.SelectedValue = gvLista.Rows[gvLista.SelectedIndex].Cells[3].Text.Trim();
                if (gvLista.Rows[gvLista.SelectedIndex].Cells[4].Text != "&nbsp;")
                    ddlClaseCliente.SelectedValue = gvLista.Rows[gvLista.SelectedIndex].Cells[4].Text.Trim();

                cargarNovedades();
                nilbNuevo.Visible = false;
                gvLista.Visible = false;
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }


        protected void ddlAño_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAño.SelectedValue.Length > 0)
                cargarMes(Convert.ToInt32(ddlAño.SelectedValue));
            else
            {
                ddlMes.DataSource = null;
                ddlMes.DataBind();
            }
        }

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.formContainer.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }

        protected void lbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                       nombrePaginaActual(), "I", Convert.ToInt32(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            gvNovedades.DataSource = null;
            gvNovedades.DataBind();
            gvNovedades.Visible = false;

            CcontrolesUsuario.HabilitarControles(this.formContainer.Controls);
            CcontrolesUsuario.LimpiarControles(this.formContainer.Controls);

            CargarCombos();

            this.nilbNuevo.Visible = false;
            gvNovedades.DataSource = null;
            gvNovedades.DataSource = null;
            this.Session["editar"] = false;

            this.nilblInformacion.Text = "";
        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.formContainer.Controls);
            CcontrolesUsuario.LimpiarControles(this.formContainer.Controls);

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
                if (((TextBox)gvr.FindControl("txtPrecioTerceros")).Text.Length == 0)
                    validar = true;
            }

            if (validar == true)
            {
                nilblInformacion.Text = "Campos vacios por favor corrija";
                return;
            }

            Guardar();

        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),//sitio
                               nombrePaginaActual(), eliminar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", eliminar);
                return;
            }

            string operacion = "elimina";

            try
            {


                object[] objValores = new object[] {Convert.ToInt32(this.gvLista.Rows[e.RowIndex].Cells[2].Text),Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[4].Text),
                Convert.ToInt16(this.Session["empresa"]),Convert.ToInt32(this.gvLista.Rows[e.RowIndex].Cells[3].Text)};

                if (CentidadMetodos.EntidadInsertUpdateDelete("cListaPrecioItem", operacion, "ppa", objValores) == 0)
                    ManejoExito("Datos eliminados satisfactoriamente", "E");
                else
                    ManejoError("Errores al eliminar el registro. Operación no realizada", "E");
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }




        protected void ddlClaseCliente_SelectedIndexChanged(object sender, EventArgs e)
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
    }
}