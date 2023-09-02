using Contabilidad.WebForms.App_Code.Presupuesto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.App_Code.General
{
    public partial class PlanPresupuestal : BasePage
    {
        #region Instancias

        CplanPresupuestal planpresupuestal = new CplanPresupuestal();
        #endregion Instancias

        #region Metodos

        private void CargarCombos()
        {

            DataView planpresupuestalspresupuesto = planpresupuestal.RetornaMayoresPlanPresupuesto(Convert.ToInt16(this.Session["empresa"]));
            this.ddlRaiz.DataSource = planpresupuestalspresupuesto;
            this.ddlRaiz.DataValueField = "codigo";
            this.ddlRaiz.DataTextField = "nombre";
            this.ddlRaiz.DataBind();
            this.ddlRaiz.Items.Insert(0, new ListItem("", ""));
        }


        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(),//pagina
                                consulta, Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", consulta);
                    return;
                }
                this.gvLista.DataSource = planpresupuestal.BuscarEntidad(this.nitxtBusqueda.Text, Convert.ToInt16(this.Session["empresa"]));
                this.gvLista.DataBind();
                this.nilblInformacion.Visible = true;
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                   "ex", this.gvLista.Rows.Count.ToString() + " Registros encontrados", HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(this.Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla. Correspondiente a: " + ex.Message, "C");
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


        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text, Convert.ToInt16(this.Session["empresa"]) };

            if (CentidadMetodos.EntidadGetKey("cPlanPresupuestal", "ppa", objKey).Tables[0].Rows.Count > 0)
            {
                ManejoError("El centro de costo " + this.txtCodigo.Text + " ya se encuentra registrada", "I");
                CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
                this.nilbNuevo.Visible = true;
                this.txtCodigo.Text = "";
            }
        }

        private void Guardar()
        {
            string operacion = "inserta";
            string padre = null;

            try
            {
                if (Convert.ToBoolean(this.Session["editar"]) == true)
                    operacion = "actualiza";

                if (ddlTipo.SelectedValue == "A" & ddlRaiz.SelectedValue.Trim().Length == 0)
                {

                    ManejoError("Debe seleccionar una centro de costo padre para continuar", "I");
                    return;
                }
                else
                {
                    padre = ddlRaiz.SelectedValue.Trim();
                }


                object[] objValores = new object[] {
                       chkActivo.Checked,     //@activo bit
                       txtCodigo.Text.Trim(),     //@codigo varchar
                      Convert.ToInt16(this.Session["empresa"]),      //@empresa    int
                      chkEjecutado.Checked, //@mejecutado
                      chkInforme.Checked,//@minforme
                      txtNivel.Text,      //@nivel  int
                      txtNombre.Text,       //@nombre varchar
                      padre,      //@padre  varchar
                      ddlTipo.SelectedValue.Trim()      //@tipo   varchar

            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete(
                    "cPlanPresupuestal",
                    operacion,
                    "ppa",
                    objValores))
                {
                    case 0:
                        ManejoExito("Registro creado satisfactoriamente", operacion.Substring(0, 1).ToUpper());
                        break;
                    case 1:
                        ManejoError("Error al crear el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                        break;
                }

            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar el registro. Correspondiente a: " + ex.Message, operacion.Substring(0, 1).ToUpper());
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
                if (seguridad.VerificaAccesoPagina(
                       this.Session["usuario"].ToString(),
                       ConfigurationManager.AppSettings["Modulo"].ToString(),
                       nombrePaginaActual(),
                       Convert.ToInt16(this.Session["empresa"])) != 0)
                {
                    this.nitxtBusqueda.Focus();
                }
                else
                {
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
                }
            }
        }



        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(
                this.Page.Controls);

            CcontrolesUsuario.LimpiarControles(
                this.Page.Controls);

            this.gvLista.DataSource = null;
            this.gvLista.DataBind();

            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
        }

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(
                this.Page.Controls);

            this.nilbNuevo.Visible = true;

            GetEntidad();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Length == 0 || txtNombre.Text.Length == 0 || txtNivel.Text.Length == 0)
            {
                ManejoError("Campos vacios por favor corrija", "I");
                return;
            }
            Guardar();
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
            this.txtCodigo.Enabled = false;
            this.txtNivel.Enabled = false;
            this.txtNombre.Focus();

            try
            {
                CargarCombos();

                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.txtCodigo.Text = this.gvLista.SelectedRow.Cells[2].Text;
                else
                    this.txtCodigo.Text = "";

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    this.txtNombre.Text = this.gvLista.SelectedRow.Cells[3].Text;
                else
                    this.txtNombre.Text = "";

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                    this.ddlRaiz.SelectedValue = this.gvLista.SelectedRow.Cells[4].Text;
                else
                    this.ddlRaiz.SelectedValue = "";


                if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                    this.txtNivel.Text = this.gvLista.SelectedRow.Cells[5].Text;
                else
                    this.txtNivel.Text = "";

                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                    this.ddlTipo.Text = this.gvLista.SelectedRow.Cells[6].Text;
                else
                    this.ddlTipo.Text = "";
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[7].Controls)
                {
                    this.chkInforme.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[8].Controls)
                {
                    this.chkEjecutado.Checked = ((CheckBox)objControl).Checked;
                }

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[9].Controls)
                {
                    this.chkActivo.Checked = ((CheckBox)objControl).Checked;
                }

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos. Correspondiente a: " + ex.Message, "A");
            }
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string operacion = "elimina";

            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                 nombrePaginaActual(), eliminar, Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", eliminar);
                    return;
                }

                object[] objValores = new object[] {
            Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text),
            (int)this.Session["empresa"]
            };

                if (CentidadMetodos.EntidadInsertUpdateDelete("cPlanPresupuestal", operacion, "ppa", objValores) == 0)
                    ManejoExito("Registro eliminado satisfactoriamente", "E");
                else
                    ManejoError("Error al eliminar el registro. Operación no realizada", "E");
            }
            catch (Exception ex)
            {
                ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
            }
        }

        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();

            this.txtNombre.Focus();
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ComportamientoTipo(0);
        }

        protected void nilbInforme_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("ListadoPuc.aspx");
        }

        protected void nilbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                 nombrePaginaActual(), insertar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", insertar);
                return;
            }
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            CargarCombos();
            this.txtCodigo.Focus();
        }
        #endregion Eventos

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.DataBind();
            GetEntidad();
        }


    }
}