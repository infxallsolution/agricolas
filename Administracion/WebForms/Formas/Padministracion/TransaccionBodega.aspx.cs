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
    public partial class TransaccionBodega : BasePage
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
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "C", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                string[] campos = { "tipo", "descripcion" };
                selBodega.Visible = false;
                this.gvLista.DataSource = tipoTransaccion.BuscarEntidadTransaccionBodega(Convert.ToString(nitxtBusqueda.Text), Convert.ToInt16(Session["empresa"]));

                this.gvLista.DataBind();

                this.nilblInformacion.Visible = true;
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C",
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
                DataView productosProduccion = CentidadMetodos.EntidadGet("iBodega", "ppa").Tables[0].DefaultView;
                productosProduccion.RowFilter = "empresa=" + Convert.ToInt16(this.Session["empresa"]);
                productosProduccion.Sort = "descripcion";
                this.selBodega.DataSource = productosProduccion;
                this.selBodega.DataValueField = "codigo";
                this.selBodega.DataTextField = "descripcion";
                this.selBodega.DataBind();

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
                    for (int x = 0; x < this.selBodega.Items.Count; x++)
                    {
                        if (this.selBodega.Items[x].Selected)
                            verificacion = true;
                    }

                    if (verificacion == false)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar al menos un producto para realizar la registro", "warning");
                        return;
                    }

                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                        operacion = "actualiza";

                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                    {
                        for (int x = 0; x < this.selBodega.Items.Count; x++)
                        {
                            if (tipoTransaccion.VerificaBodegaTipo(ddlTipoTransaccion.SelectedValue, this.selBodega.Items[x].Value, Convert.ToInt16(Session["empresa"])) == 0)
                            {
                                if (this.selBodega.Items[x].Selected == false)
                                {

                                    object[] objValoresConcepto = new object[]{
                                                        this.selBodega.Items[x].Value,
                                                        Convert.ToInt16(Session["empresa"]),
                                 Convert.ToString(ddlTipoTransaccion.SelectedValue)};

                                    switch (CentidadMetodos.EntidadInsertUpdateDelete("iBodegaTipoTransaccion", "elimina", "ppa", objValoresConcepto))
                                    {
                                        case 1:
                                            verificacion = true;
                                            break;
                                    }

                                }

                            }
                            else
                            {
                                if (this.selBodega.Items[x].Selected == true)
                                {

                                    object[] objValoresConcepto = new object[]{
                                                       this.selBodega.Items[x].Value,
                                                        Convert.ToInt16(Session["empresa"]),
                                 Convert.ToString(ddlTipoTransaccion.SelectedValue)};

                                    switch (CentidadMetodos.EntidadInsertUpdateDelete("iBodegaTipoTransaccion", "inserta", "ppa", objValoresConcepto))
                                    {
                                        case 1:
                                            verificacion = true;
                                            break;
                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        for (int x = 0; x < this.selBodega.Items.Count; x++)
                        {
                            if (this.selBodega.Items[x].Selected == true)
                            {

                                object[] objValoresConcepto = new object[]{

                                                      this.selBodega.Items[x].Value,
                                                        Convert.ToInt16(Session["empresa"]),
                                 Convert.ToString(ddlTipoTransaccion.SelectedValue)};

                                switch (CentidadMetodos.EntidadInsertUpdateDelete("iBodegaTipoTransaccion", operacion, "ppa", objValoresConcepto))
                                {
                                    case 1:
                                        verificacion = true;
                                        break;
                                }
                            }
                        }
                    }

                    if (verificacion == false)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "Error al insertar el registro. operación no realizada", "error");
                    }
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
            for (int x = 0; x < this.selBodega.Items.Count; x++)
            {
                if (tipoTransaccion.VerificaBodegaTipo(ddlTipoTransaccion.SelectedValue, selBodega.Items[x].Value, Convert.ToInt16(Session["empresa"])) == 0)
                    selBodega.Items[x].Selected = true;
                else
                    selBodega.Items[x].Selected = false;
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
            this.selBodega.Visible = true;
        }


        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();

            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
            this.ddlTipoTransaccion.Enabled = true;
            this.selBodega.Visible = false;
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

            this.selBodega.Visible = true;
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
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                           nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }


                object[] objValores = new object[] { Convert.ToInt16(Session["empresa"]), Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text) };

                if (CentidadMetodos.EntidadInsertUpdateDelete("iBodegaTipoTransaccion", operacion, "ppa", objValores) == 0)
                    ManejoExito("Registro eliminado satisfactoriamente", "E");
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

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }
        #endregion Eventos




    }
}