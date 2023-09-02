using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Padministracion
{
    public partial class DiasHabiles : BasePage
    {
        #region Instancias


        CdiasHabiles diasHabiles = new CdiasHabiles();

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

                this.gvLista.DataSource = diasHabiles.BuscarEntidad(
                    this.nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));

                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(
                            this.Session["usuario"].ToString(), "C",
                          ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
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

            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion,
                 ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                 "ex", mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));

            GetEntidad();
        }

        private void CargarCombos()
        {
            try
            {
                DataView ccosto =
                   CentidadMetodos.EntidadGet("cCentrosCosto", "ppa").Tables[0].DefaultView;
                ccosto.RowFilter = "auxiliar=True and empresa = " + this.Session["empresa"].ToString();
                this.ddlCcosto.DataSource = ccosto;
                this.ddlCcosto.DataValueField = "codigo";
                this.ddlCcosto.DataTextField = "descripcion";
                this.ddlCcosto.DataBind();
                this.ddlCcosto.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los centros de costo. Correspondiente a: " + ex.Message, "C");
            }
        }

        private void EntidadKey()
        {
            object[] objKey = new object[] { ddlCcosto.SelectedValue, Convert.ToInt16(this.Session["empresa"]) };

            try
            {
                if (CentidadMetodos.EntidadGetKey("nDepartamento", "ppa", objKey).Tables[0].Rows.Count > 0)
                {

                    MostrarMensaje("Ccosto " + ddlCcosto.SelectedValue + " - " + ddlCcosto.SelectedItem.Text + " ya se encuentra registrado");

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

        private void Guardar()
        {
            string operacion = "inserta";

            try
            {
                if (Convert.ToBoolean(this.Session["editar"]) == true)
                {
                    operacion = "actualiza";
                }

                object[] objValores = new object[]{
                     chkActivo.Checked,   // @activo	bit
                      Convert.ToString(this.ddlCcosto.SelectedValue),   //@ccosto	varchar
                    Convert.ToInt16( ddlDe.SelectedValue) , //@de	int
                     Convert.ToInt16(Session["empresa"]),    //@empresa	int
                     Convert.ToInt16( ddlHasta.SelectedValue)    //@hasta	int               
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("nDiasHabilesCc", operacion, "ppa", objValores))
                {
                    case 0:

                        ManejoExito("Datos insertados satisfactoriamente", operacion.Substring(0, 1).ToUpper());
                        break;

                    case 1:

                        ManejoError("Errores al insertar el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                        break;
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
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(),
                 ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) != 0)
                {


                    this.nitxtBusqueda.Focus();

                }
                else
                {
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
                }
            }
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

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;

            CargarCombos();

            this.ddlCcosto.Focus();
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
            if (ddlCcosto.Text.Trim().Length == 0)
            {
                MostrarMensaje("Centro de costo no valido");
                return;
            }

            if (Convert.ToInt16(ddlDe.SelectedValue) >= Convert.ToInt16(ddlHasta.SelectedValue))
            {
                MostrarMensaje("El dia inicial no puede ser mayor o igual al final");
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
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;

            this.ddlCcosto.Enabled = false;


            try
            {
                CargarCombos();

                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                {
                    this.ddlCcosto.SelectedValue = this.gvLista.SelectedRow.Cells[2].Text;
                }

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                {
                    this.ddlDe.SelectedValue = this.gvLista.SelectedRow.Cells[4].Text;
                }

                if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                {
                    this.ddlHasta.SelectedValue = this.gvLista.SelectedRow.Cells[5].Text;
                }


                foreach (Control objControl in this.gvLista.SelectedRow.Cells[6].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkActivo.Checked = ((CheckBox)objControl).Checked;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                                          ConfigurationManager.AppSettings["Modulo"].ToString(),
                                           nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            string operacion = "elimina";

            try
            {
                object[] objValores = new object[] {
                Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)),
                Convert.ToInt16(Session["empresa"])
            };

                if (CentidadMetodos.EntidadInsertUpdateDelete("nDiasHabilesCc", operacion, "ppa", objValores) == 0)
                {
                    ManejoExito("Datos eliminados satisfactoriamente", "E");
                }
                else
                {
                    ManejoError("Errores al eliminar el registro. Operación no realizada", "E");
                }
            }
            catch (Exception ex)
            {
                if (ex.HResult.ToString() == "-2146233087")
                {
                    ManejoError("El código ('" + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)) +
                    "' - ' " + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[3].Text)) + "') tiene una asociación, no es posible eliminar el registro.", "E");
                }
                else
                {
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
                }
            }
        }

        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
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