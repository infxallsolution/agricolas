using Administracion.WebForms.App_Code.Administracion;
using Administracion.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracion.WebForms.Formas.Padministracion
{
    public partial class TransaccionProducto : BasePage
    {
        #region Instancias

        Ctransacciones transacciones = new Ctransacciones();
        CtiposTransaccion tipoTransaccion = new CtiposTransaccion();
        #endregion Instancias
        #region Metodos

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
                selProductos.Visible = false;
                this.gvLista.DataSource = tipoTransaccion.BuscarEntidadTransaccionProducto(
                    Convert.ToString(nitxtBusqueda.Text), Convert.ToInt16(Session["empresa"]));

                this.gvLista.DataBind();

                this.nilblInformacion.Visible = true;
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(
                            this.Session["usuario"].ToString(), "C",
                          ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                            this.gvLista.Rows.Count.ToString() + " Registros encontrados", HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private void ManejoExito(string mensaje, string operacion)
        {

            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");

            CcontrolesUsuario.InhabilitarControles(Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);
            nilbNuevo.Visible = true;
            seguridad.InsertaLog(Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + Page.ToString(),
                "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }
        private void CargarCombos()
        {

            try
            {
                this.ddlTipoTransaccion.DataSource = transacciones.GetTipoTransaccionModulo(Convert.ToInt16(this.Session["empresa"]));
                this.ddlTipoTransaccion.DataValueField = "codigo";
                this.ddlTipoTransaccion.DataTextField = "descripcion";
                this.ddlTipoTransaccion.DataBind();
                this.ddlTipoTransaccion.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }


            try
            {
                DataView productosProduccion = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("iItems", "ppa"),
                    "descripcion", Convert.ToInt16(Session["empresa"]));
                productosProduccion.RowFilter = "tipo in('P','T','CP') and empresa = " + Session["empresa"].ToString();
                this.selProductos.DataSource = productosProduccion;
                this.selProductos.DataValueField = "codigo";
                this.selProductos.DataTextField = "descripcion";
                this.selProductos.DataBind();

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }

        }
        private void Guardar()
        {
            string operacion = "inserta";
            bool verificacion = false;
            try
            {

                using (TransactionScope ts = new TransactionScope())
                {
                    for (int x = 0; x < this.selProductos.Items.Count; x++)
                    {
                        if (this.selProductos.Items[x].Selected)
                            verificacion = true;
                    }

                    if (verificacion == false)
                    {
                        MostrarMensaje("Debe seleccionar al menos un producto para realizar la registro");
                        return;
                    }



                    object[] objValoresElimina = new object[] { Convert.ToInt16(Session["empresa"]), ddlTipoTransaccion.SelectedValue };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("gTipoTransaccionProducto", "elimina", "ppa", objValoresElimina))
                    {
                        case 1:
                            verificacion = true;
                            break;
                    }

                    for (int x = 0; x < this.selProductos.Items.Count; x++)
                    {
                        if (this.selProductos.Items[x].Selected == true)
                        {

                            object[] objValoresConcepto = new object[]{
                                     chkComercializadora.Checked,
                                    Convert.ToInt16(Session["empresa"]),
                                    chkMostrarModulo.Checked,
                                    chkManejaOrdenEnvio.Checked,
                                    Convert.ToInt16(this.selProductos.Items[x].Value),
                                    chkMostrarRemision.Checked,
                                    Convert.ToString(ddlTipoTransaccion.SelectedValue)};

                            switch (CentidadMetodos.EntidadInsertUpdateDelete("gTipoTransaccionProducto", "inserta", "ppa", objValoresConcepto))
                            {
                                case 1:
                                    verificacion = true;
                                    break;
                            }
                        }
                    }

                    if (verificacion == false)
                        ManejoError("Error al insertar el registro. operación no realizada", "I");

                    else
                    {
                        ManejoExito("Asignación registrada correctamente", "I");
                        ts.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private void ValidaRegistro()
        {
            for (int x = 0; x < this.selProductos.Items.Count; x++)
            {
                if (tipoTransaccion.VerificaProductoTipo(ddlTipoTransaccion.SelectedValue, selProductos.Items[x].Value, Convert.ToInt16(Session["empresa"])) == 0)
                    selProductos.Items[x].Selected = true;
                else
                    selProductos.Items[x].Selected = false;
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
                    this.nitxtBusqueda.Focus();
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

        protected void gvAsignacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            CargarCombos();
            gvLista.DataBind();
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }

        protected void nilbNuevo_Click(object sender, EventArgs e)
        {

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                               nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);
            CargarCombos();
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            this.ddlTipoTransaccion.Focus();
            this.nilblInformacion.Text = "";
            this.selProductos.Visible = true;
        }


        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();

            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
            this.ddlTipoTransaccion.Enabled = true;
            this.selProductos.Visible = false;
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (ddlTipoTransaccion.SelectedValue.Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Campos vacios por favor corrija", "warning");
                return;
            }

            Guardar();
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

            this.selProductos.Visible = true;
            CargarCombos();
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;

            this.ddlTipoTransaccion.Enabled = false;
            this.ddlTipoTransaccion.Focus();

            try
            {
                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                {
                    this.ddlTipoTransaccion.Text = this.gvLista.SelectedRow.Cells[2].Text.Trim();
                    ValidaRegistro();
                }

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[4].Controls)
                {
                    this.chkComercializadora.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[5].Controls)
                {
                    this.chkMostrarRemision.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[6].Controls)
                {
                    this.chkMostrarModulo.Checked = ((CheckBox)objControl).Checked;
                }

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string operacion = "elimina";

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
                Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text),
                Convert.ToInt16(Session["empresa"])
            };

                if (tipoTransaccion.EliminaProductoTipo(Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text), Convert.ToInt16(Session["empresa"])) == 0)
                {
                    if (CentidadMetodos.EntidadInsertUpdateDelete("gTipoTransaccionProducto", operacion, "ppa", objValores) == 0)
                        ManejoExito("Registro eliminado satisfactoriamente", "E");
                }
                else
                    ManejoError("Errores al eliminar el registro. Operación no realizada", "E");


            }
            catch (Exception ex)
            {
                if (ex.HResult.ToString() == "-2146233087")
                {
                    ManejoError("El código ('" + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)) +
                    "' - ' " + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[3].Text)) + "') tiene una asociación, no es posible eliminar el registro.", "E");
                }
                else
                    ManejoErrorCatch(this, GetType(), ex);
            }
        }

        #endregion Eventos





    }
}