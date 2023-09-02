using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
using Nomina.WebForms.App_Code.Models.Payroll.DTO.Enum.PayrollElectronic.AL;
using Nomina.WebForms.App_Code.Services.Implementation.ElectronicPayroll;
using Nomina.WebForms.App_Code.Services.Interfaces.ElectronicPayroll;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Padministracion
{
    public partial class ParametrosNE : BasePage
    {
        #region Instancias

        Cdepartamentos departamentos = new Cdepartamentos();
        IPayRollService _equivalenceElectronicPay = new PayRollService();


        #endregion Instancias

        #region Metodos
        private void Consecutivo()
        {
            try
            {
            }
            catch (Exception ex)
            {
                ManejoError("Error al obtener el consecutivo. Correspondiente a: " + ex.Message, "C");
            }
        }

        private void GetEntidad()
        {
            try
            {
                //if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                //                        ConfigurationManager.AppSettings["Modulo"].ToString(),
                //                         nombrePaginaActual(), "C", Convert.ToInt16(Session["empresa"])) == 0)
                //{
                //    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                //    return;
                //}

                this.gvLista.DataSource = departamentos.BuscarEntidad(this.nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));

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
            gvLista.DataSource = null;
            gvLista.DataBind();
            this.nilbNuevo.Visible = true;
            niimbBuscar.Visible = true;
            niddlEntitySearch.Visible = true;
            niddlEntitySearch.ClearSelection();
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                 "ex", mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));
        }

        private void CargarCombos()
        {
            try
            {
                var entities = _equivalenceElectronicPay.GetEntities();
                this.ddlEntities.DataSource = entities;
                this.ddlEntities.DataValueField = "Id";
                this.ddlEntities.DataTextField = "Name";
                this.ddlEntities.DataBind();
                this.ddlEntities.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError(string.Format("Error al cargar {0} Correspondiente a:{1} ", ddlEntities.ID.Substring(3, ddlEntities.ID.Length - 3), ex.Message), "C");
            }
        }

        private void LoadSearch()
        {
            try
            {
                var entities = _equivalenceElectronicPay.GetEntities();
                this.niddlEntitySearch.DataSource = entities;
                this.niddlEntitySearch.DataValueField = "Id";
                this.niddlEntitySearch.DataTextField = "Name";
                this.niddlEntitySearch.DataBind();
                this.niddlEntitySearch.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError(string.Format("Error al cargar {0} Correspondiente a:{1} ", ddlEntities.ID.Substring(3, ddlEntities.ID.Length - 3), ex.Message), "C");
            }
        }

        private void EntidadKey()
        {
            object[] objKey = new object[] { Convert.ToInt16(Session["empresa"]) };

            try
            {
                if (CentidadMetodos.EntidadGetKey("nDepartamento", "ppa", objKey).Tables[0].Rows.Count > 0)
                {


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
            try
            {

                if (ddlEntities.SelectedValue.Trim().Length == 0)
                {
                    MostrarMensaje("Seleccione una entidad válida");
                    return;
                }

                if (ddlEntityValues.SelectedValue.Trim().Length == 0)
                {
                    MostrarMensaje("Seleccione un valor de la entidad válida");
                    return;
                }

                if (ddlEquivalenceAL.SelectedValue.Trim().Length == 0)
                {
                    MostrarMensaje("Seleccione un valor para asignar la equivalencia");
                    return;
                }
                var result = _equivalenceElectronicPay.Save(new App_Code.Models.Payroll.DTO.Input.CreateEquivalenceInput()
                {
                    Company = Convert.ToInt32(this.Session["empresa"]),
                    Entity = (Entities)Enum.Parse(typeof(Entities), ddlEntities.SelectedValue),
                    Id = ddlEntityValues.SelectedValue.Trim(),
                    Value = ddlEquivalenceAL.SelectedValue.Trim()
                });

                if (result)
                {
                    ManejoExito("Entidad asociada con exito", "In");
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar debido a: " + ex.Message, "C");
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
                //if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(),
                // ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) != 0)
                //{
                if (!IsPostBack)
                {
                    LoadSearch();
                    this.nitxtBusqueda.Focus();
                }

                //}
                //else
                //    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }

        protected void lbNuevo_Click(object sender, EventArgs e)
        {
            //if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
            //                     ConfigurationManager.AppSettings["Modulo"].ToString(),
            //                      nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            //{
            //    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
            //    return;
            //}

            nuevo();
        }

        private void nuevo()
        {
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;

            CargarCombos();
            ddlEntities.ClearSelection();
            ddlEntityValues.ClearSelection();
            ddlEquivalenceAL.ClearSelection();
            niddlEntitySearch.Visible = false;
            niimbBuscar.Visible = false;

            this.ddlEntities.Focus();
            this.nilblInformacion.Text = "";
        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            niimbBuscar.Visible = true;
            niddlEntitySearch.ClearSelection();
            niddlEntitySearch.Visible = true;
            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
        }

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
            //nombrePaginaActual(), "A", Convert.ToInt16(Session["empresa"])) == 0)
            //{
            //    ManejoError("Usuario no autorizado para ejecutar esta operación", "A");
            //    return;
            //}

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;


            try
            {
                CargarCombos();


                ddlEntities.SelectedValue = niddlEntitySearch.SelectedValue;
                loadEntities();

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                    this.ddlEntityValues.SelectedValue = this.gvLista.SelectedRow.Cells[2].Text;

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                    this.ddlEquivalenceAL.SelectedValue = this.gvLista.SelectedRow.Cells[4].Text;


            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
            //                              ConfigurationManager.AppSettings["Modulo"].ToString(),
            //                               nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
            //{
            //    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
            //    return;
            //}

            try
            {
                if (niddlEntitySearch.SelectedValue.Trim().Length == 0)
                {

                    MostrarMensaje("por favor seleccione una entidad de busqueda correcta");
                    return;
                }
                var Id = ((HiddenField)gvLista.Rows[e.RowIndex].FindControl("hfId")).Value;

                var output = _equivalenceElectronicPay.DeleteByEntityID(Convert.ToInt32(this.Session["empresa"]), (Entities)Enum.Parse(typeof(Entities), niddlEntitySearch.SelectedValue), Guid.Parse(Id));

                if (output)
                {
                    ManejoExito("Datos eliminados con exito", "I");
                }
                else
                {
                    ManejoError("Por favor verifique los datos al eliminar", "E");
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
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
            }
        }

        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
        }



        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            Buscar();
            gvLista.DataBind();

        }

        protected void ddlEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadEntities();

        }

        private void loadEntities()
        {
            if (ddlEntities.SelectedValue.Trim().Length == 0)
            {
                MostrarMensaje("Seleccione una entidad válida");
                return;
            }

            try
            {
                var entitySelected = (Entities)Enum.Parse(typeof(Entities), ddlEntities.SelectedValue);
                var data = _equivalenceElectronicPay.GetDataFromType(Convert.ToInt32(this.Session["empresa"]), entitySelected);
                ddlEntityValues.DataSource = data;
                ddlEntityValues.DataValueField = "Codigo";
                ddlEntityValues.DataTextField = "Cadena";
                ddlEntityValues.DataBind();
                ddlEntityValues.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError(string.Format("Error al cargar {0} Correspondiente a:{1} ", ddlEntities.ID.Substring(3, ddlEntities.ID.Length - 3), ex.Message), "C");
            }

            try
            {
                var entitySelected = (Entities)Enum.Parse(typeof(Entities), ddlEntities.SelectedValue);
                var data = _equivalenceElectronicPay.GetValuesFromType(Convert.ToInt32(this.Session["empresa"]), entitySelected);
                ddlEquivalenceAL.DataSource = data;
                ddlEquivalenceAL.DataValueField = "Codigo";
                ddlEquivalenceAL.DataTextField = "Cadena";
                ddlEquivalenceAL.DataBind();
                ddlEquivalenceAL.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError(string.Format("Error al cargar {0} Correspondiente a:{1} ", ddlEntities.ID.Substring(3, ddlEntities.ID.Length - 3), ex.Message), "C");
            }
        }

        protected void ddlEntityValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (ddlEntityValues.SelectedValue.Trim().Length == 0)
                    {

                        MostrarMensaje("Por favor seleccione un valor de entidad");
                        return;
                    }


                    var entitySelected = (Entities)Enum.Parse(typeof(Entities), ddlEntities.SelectedValue);
                    var data = _equivalenceElectronicPay.GetDataFromTypeById(Convert.ToInt32(this.Session["empresa"]), entitySelected, ddlEntityValues.SelectedValue);
                    if (data != null)
                        ddlEquivalenceAL.SelectedValue = data;
                    else
                        ddlEquivalenceAL.ClearSelection();
                }
                catch (Exception ex)
                {
                    //   ManejoError(string.Format("Error al cargar {0} Correspondiente a:{1} ", ddlEntities.ID.Substring(3, ddlEntities.ID.Length - 3), ex.Message), "C");
                }
            }
            catch (Exception ex)
            {
                ManejoError(string.Format("Error al cargar {0} Correspondiente a:{1} ", ddlEntities.ID.Substring(3, ddlEntities.ID.Length - 3), ex.Message), "C");
            }

        }
        #endregion Eventos



        protected void niimbBuscar_Click1(object sender, EventArgs e)
        {
            try
            {
                if (niddlEntitySearch.SelectedValue.Trim().Length == 0)
                {
                    MostrarMensaje("Seleccione una entidad para la busqueda");
                    return;
                }

                Buscar();

            }
            catch (Exception ex)
            {
                ManejoError(string.Format("Error al realizar la busqueda : {0} ", ex.Message), "C");
            }
        }

        private void Buscar()
        {
            var data = _equivalenceElectronicPay.SearchByEntity(Convert.ToInt32(this.Session["empresa"]), (Entities)Enum.Parse(typeof(Entities), niddlEntitySearch.SelectedValue));

            if (data != null)
            {
                gvLista.DataSource = data;
                gvLista.DataBind();
            }
            else
            {
                MostrarMensaje("No se encontraron registros");
            }
        }
    }
}