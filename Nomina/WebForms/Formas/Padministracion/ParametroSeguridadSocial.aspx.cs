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
    public partial class ParametroSeguridadSocial : BasePage
    {

        #region Instancias




        CparametroSeguridadSocial seguridadSocial = new CparametroSeguridadSocial();
        #endregion Instancias

        #region Metodos


        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "C", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }
                selConceptos.Visible = false;
                this.gvLista.DataSource = seguridadSocial.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();


                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

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
            this.nilbNuevo.Visible = true;
            this.ddlTipo.Enabled = true;
            this.selConceptos.Visible = false;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
          "ex", mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }
        private void CargarCombos()
        {

            try
            {
                this.selConceptos.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nConcepto", "ppa"),
                    "descripcion", Convert.ToInt16(Session["empresa"]));
                this.selConceptos.DataValueField = "codigo";
                this.selConceptos.DataTextField = "descripcion";
                this.selConceptos.DataBind();

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar funcionarios. Correspondiente a: " + ex.Message, "C");
            }
        }
        private void EntidadKey()
        {
            object[] objKey = new object[] { this.ddlTipo.SelectedValue, Convert.ToInt16(Session["empresa"]) };

            try
            {
                if (CentidadMetodos.EntidadGetKey("nParametroSeguridadSocial", "ppa", objKey).Tables[0].Rows.Count > 0)
                {

                    this.nilblInformacion.Text = "El tipo de parametro  " + ddlTipo.SelectedItem.ToString() + " ya se encuentra registrada";
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
            string operacion = "inserta";
            bool verificacion = false;
            try
            {

                using (TransactionScope ts = new TransactionScope())
                {
                    for (int x = 0; x < this.selConceptos.Items.Count; x++)
                    {
                        if (this.selConceptos.Items[x].Selected)
                            verificacion = true;
                    }

                    if (verificacion == false)
                    {
                        this.nilblInformacion.Text = "Debe seleccionar al menos un concepto para realizar la asignación";
                        return;
                    }

                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                        operacion = "actualiza";

                    object[] objValores = new object[] { ddlTipo.SelectedValue, Convert.ToString(this.ddlTipo.SelectedItem.ToString()), Convert.ToInt16(Session["empresa"]) };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("nParametroSeguridadSocial", operacion, "ppa", objValores))
                    {
                        case 0:
                            if (Convert.ToBoolean(this.Session["editar"]) == true)
                            {
                                for (int x = 0; x < this.selConceptos.Items.Count; x++)
                                {
                                    if (seguridadSocial.VerificaConceptosTipo(ddlTipo.SelectedValue, Convert.ToString(this.selConceptos.Items[x].Value), Convert.ToInt16(Session["empresa"])) == 0)
                                    {
                                        if (this.selConceptos.Items[x].Selected == false)
                                        {
                                            object[] objValoresConcepto = new object[]{
                                                        Convert.ToString(ddlTipo.SelectedValue),
                                                        Convert.ToInt16(this.selConceptos.Items[x].Value),
                                        Convert.ToInt16(Session["empresa"]),};

                                            switch (CentidadMetodos.EntidadInsertUpdateDelete("nParametroSeguridadSocialDetalle", "elimina", "ppa", objValoresConcepto))
                                            {
                                                case 1:
                                                    verificacion = true;
                                                    break;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        if (this.selConceptos.Items[x].Selected == true)
                                        {

                                            object[] objValoresConcepto = new object[]{
                                                        Convert.ToString(ddlTipo.SelectedValue),
                                                        Convert.ToInt16(this.selConceptos.Items[x].Value) ,
                                        Convert.ToInt16(Session["empresa"]),};

                                            switch (CentidadMetodos.EntidadInsertUpdateDelete("nParametroSeguridadSocialDetalle", "inserta", "ppa", objValoresConcepto))
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
                                for (int x = 0; x < this.selConceptos.Items.Count; x++)
                                {
                                    if (this.selConceptos.Items[x].Selected == true)
                                    {

                                        object[] objValoresConcepto = new object[]{
                                                                Convert.ToString(ddlTipo.SelectedValue),

                                                        Convert.ToInt16(this.selConceptos.Items[x].Value),
                                    Convert.ToInt16(Session["empresa"])};

                                        switch (CentidadMetodos.EntidadInsertUpdateDelete("nParametroSeguridadSocialDetalle", operacion, "ppa", objValoresConcepto))
                                        {
                                            case 1:
                                                verificacion = true;
                                                break;
                                        }
                                    }
                                }
                            }

                            break;

                        case 1:
                            verificacion = false;
                            break;
                    }

                    if (verificacion == false)
                    {
                        this.nilblInformacion.Text = "Error al insertar el registro. operación no realizada";
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
                ManejoError("Error al guardar los datos correspondiente a: " + ex.Message, operacion.Substring(0, 1).ToUpper());
            }
        }

        private void ValidaRegistro()
        {
            for (int x = 0; x < this.selConceptos.Items.Count; x++)
            {
                if (seguridadSocial.VerificaConceptosTipo(ddlTipo.SelectedValue, Convert.ToString(selConceptos.Items[x].Value), Convert.ToInt16(Session["empresa"])) == 0)
                    selConceptos.Items[x].Selected = true;
                else
                    selConceptos.Items[x].Selected = false;
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
                    this.ddlTipo.Focus();

                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }

        protected void lbNuevo_Click(object sender, EventArgs e)
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
            this.Session["editar"] = false;
            this.ddlTipo.Focus();
            this.nilblInformacion.Text = "";
            this.selConceptos.Visible = true;
        }


        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            gvLista.DataSource = null;
            gvLista.DataBind();
            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
            this.ddlTipo.Enabled = true;
            this.selConceptos.Visible = false;
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (ddlTipo.SelectedValue.Length == 0)
            {
                MostrarMensaje("Campos vacios por favor corrija");
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
            this.selConceptos.Visible = true;
            CargarCombos();
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            this.ddlTipo.Enabled = false;

            try
            {
                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                {
                    ddlTipo.SelectedValue = this.gvLista.SelectedRow.Cells[2].Text.Trim();
                    ValidaRegistro();
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                object[] objValores = new object[] { Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text), Convert.ToInt16(Session["empresa"]) };

                if (seguridadSocial.EliminaParametros(Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text), Convert.ToInt16(Session["empresa"])) == 0)
                    ManejoExito("Registro eliminado satisfactoriamente", "E");
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
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
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
        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntidadKey();
        }
        #endregion Eventos

    }
}