using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
using Nomina.WebForms.App_Code.Models.Payroll.DTO.Enum.PayrollElectronic.AL;
using Nomina.WebForms.App_Code.Models.Payroll.DTO.Input;
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
    public partial class GrupoConceptosNE : BasePage
    {
        #region Instancias

        Cconceptos conceptos = new Cconceptos();
        CgrupoConcepto grupoconcepto = new CgrupoConcepto();
        IPayRollService _equivalenceElectronicPay = new PayRollService();
        IServicePayrollConceptsEquivalence _servicePayrollConceptsEquivalence = new ServicePayrollConceptsEquivalence();

        #endregion Instancias

        #region Metodos


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

                this.selConceptos.DataSource = null;
                this.selConceptos.DataBind();
                selConceptos.Visible = false;

                var data = _servicePayrollConceptsEquivalence.Get(Convert.ToInt32(this.Session["empresa"]));
                this.gvLista.DataSource = data;
                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(
                    this.Session["usuario"].ToString(), "C",
                  ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                    this.gvLista.Rows.Count.ToString() + " Registros encontrados", ObtenerIP(), Convert.ToInt16(this.Session["empresa"]));

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
                "ex", mensaje, ObtenerIP(), Convert.ToInt16(this.Session["empresa"]));
            GetEntidad();
        }

        private void CargarCombos()
        {
            try
            {
                selConceptos.Visible = true;
                this.selConceptos.DataSource = _servicePayrollConceptsEquivalence.GetConcept(Convert.ToInt32(this.Session["empresa"]));
                this.selConceptos.DataValueField = "codigo";
                this.selConceptos.DataTextField = "Cadena";
                this.selConceptos.DataBind();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los IR correspondiente a: " + ex.Message, "C");
            }

            try
            {
                var entities = _equivalenceElectronicPay.GetTypeEquivalences();
                this.ddlTipoEquivalencia.DataSource = entities;
                this.ddlTipoEquivalencia.DataValueField = "Id";
                this.ddlTipoEquivalencia.DataTextField = "Name";
                this.ddlTipoEquivalencia.DataBind();
                this.ddlTipoEquivalencia.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError(string.Format("Error al cargar {0} Correspondiente a:{1} ", ddlTipoEquivalencia.ID.Substring(3, ddlTipoEquivalencia.ID.Length - 3), ex.Message), "C");
            }

        }

        private void ValidaRegistro()
        {
            for (int x = 0; x < selConceptos.Items.Count; x++)
            {

                //if (grupoconcepto.VerificaGrupoConcepto(txtCodigo.Text, selConceptos.Items[x].Value, Convert.ToInt16(Session["empresa"])) == 0)
                //    selConceptos.Items[x].Selected = true;
                //else
                //    selConceptos.Items[x].Selected = false;
            }
        }

        private void EntidadKey()
        {
            //object[] objKey = new object[] { this.txtCodigo.Text, Convert.ToInt16(Session["empresa"]) };
            //try
            //{
            //    if (CentidadMetodos.EntidadGetKey("nGrupoConcepto", "ppa", objKey).Tables[0].Rows.Count > 0)
            //    {
            //        MostrarMensaje("Código " + this.txtCodigo.Text + " ya se encuentra registrado");
            //        CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            //        this.nilbNuevo.Visible = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ManejoError("Error al validar la llave primaria correspondiente a: " + ex.Message, "C");
            //}
        }

        private void Guardar()
        {
            string operacion = "inserta";
            bool verificacion = false;

            //if (this.txtDescripcion.Text.Length == 0 || this.txtCodigo.Text.Length == 0)
            //    MostrarMensaje("Campos vacios por favor corrija");
            //else
            //{
            //    try
            //    {
            //        using (TransactionScope ts = new TransactionScope())
            //        {
            //            if (Convert.ToBoolean(this.Session["editar"]) == true)
            //                operacion = "actualiza";

            //            object[] objValores = new object[]{
            //            chkActivo.Checked,
            //            this.txtCodigo.Text,
            //            this.txtDescripcion.Text,
            //            Convert.ToInt16(Session["empresa"]),
            //            txtObservacion.Text

            //        };

            //            switch (CentidadMetodos.EntidadInsertUpdateDelete("nGrupoConcepto", operacion, "ppa", objValores))
            //            {
            //                case 0:
            //                    if (Convert.ToBoolean(this.Session["editar"]) == true)
            //                    {
            //                        for (int x = 0; x < selConceptos.Items.Count; x++)
            //                        {
            //                            if (grupoconcepto.VerificaGrupoConcepto(txtCodigo.Text, selConceptos.Items[x].Value, Convert.ToInt16(Session["empresa"])) == 0)
            //                            {
            //                                if (selConceptos.Items[x].Selected == false)
            //                                {
            //                                    object[] objValoresConcepto = new object[]{
            //                                                selConceptos.Items[x].Value,
            //                                                Convert.ToInt16(Session["empresa"]),
            //                                                this.txtCodigo.Text};

            //                                    switch (CentidadMetodos.EntidadInsertUpdateDelete("nGrupoConceptoDetalle", "elimina", "ppa", objValoresConcepto))
            //                                    {
            //                                        case 1:
            //                                            verificacion = true;
            //                                            break;
            //                                    }
            //                                }
            //                            }
            //                            else
            //                            {
            //                                if (selConceptos.Items[x].Selected == true)
            //                                {
            //                                    object[] objValoresConcepto = new object[]{
            //                                                selConceptos.Items[x].Value,
            //                                                Convert.ToInt16(Session["empresa"]),
            //                                                this.txtCodigo.Text};

            //                                    switch (CentidadMetodos.EntidadInsertUpdateDelete("nGrupoConceptoDetalle", "inserta", "ppa", objValoresConcepto))
            //                                    {
            //                                        case 1:
            //                                            verificacion = true;
            //                                            break;
            //                                    }

            //                                }
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        for (int x = 0; x < selConceptos.Items.Count; x++)
            //                        {
            //                            if (selConceptos.Items[x].Selected == true)
            //                            {

            //                                object[] objValoresConcepto = new object[]{
            //                                                selConceptos.Items[x].Value,
            //                                                Convert.ToInt16(Session["empresa"]),
            //                                                this.txtCodigo.Text};

            //                                switch (CentidadMetodos.EntidadInsertUpdateDelete("nGrupoConceptoDetalle", operacion, "ppa", objValoresConcepto))
            //                                {
            //                                    case 1:
            //                                        verificacion = true;
            //                                        break;
            //                                }
            //                            }
            //                        }
            //                    }
            //                    break;
            //                case 1:
            //                    verificacion = true;
            //                    break;
            //            }

            //            if (verificacion == true)
            //            {
            //                this.nilblInformacion.Text = "Error al insertar el detalle de la transacción. Operación no realizada";
            //                return;
            //            }
            //            ManejoExito("Transacción registrada satisfactoriamente.", "I");
            //            ts.Complete();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        ManejoError("Error al guardar los datos correspondiente a: " + ex.Message, operacion.Substring(0, 1).ToUpper());
            //    }
            //}
        }

        #endregion Metodos

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {
                //if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) != 0) { }
                ////this.txtCodigo.Focus();
                //else
                //    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }

        protected void lbNuevo_Click(object sender, EventArgs e)
        {
            //if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
            //                            ConfigurationManager.AppSettings["Modulo"].ToString(),
            //                             nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            //{
            //    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
            //    return;
            //}

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);


            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            CargarCombos();

            //this.txtDescripcion.Enabled = true;
            //this.txtCodigo.Focus();
            this.nilblInformacion.Text = "";
        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();

            this.selConceptos.DataSource = null;
            this.selConceptos.DataBind();

            selConceptos.Visible = false;

            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
            this.selConceptos.Visible = false;
            this.selConceptos.DataSource = null;
            this.selConceptos.DataBind();
        }

        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
            //chkActivo.Focus();
        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                //if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                //                        ConfigurationManager.AppSettings["Modulo"].ToString(),
                //                         nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
                //{
                //    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                //    return;
                //}
                var id = ((HiddenField)gvLista.Rows[e.RowIndex].FindControl("hfId")).Value;
                var result = _servicePayrollConceptsEquivalence.Delete(Guid.Parse(id));
                if (result)
                {
                    ManejoExito("Datos eliminados satisfactoriamente", "I");
                }
                else
                {
                    ManejoError("Error al eliminar los datos", "I");
                }

            }
            catch (Exception ex)
            {
                ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
            }
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            var selectedany = false;
            if (ddlEquivalenciasConceptos.SelectedValue.Trim().Length == 0)
            {
                MostrarMensaje("Seleccione un concepto válido");
                return;
            }

            if (ddlTipoEquivalencia.SelectedValue.Trim().Length == 0)
            {
                MostrarMensaje("Seleccione un tipo de equivalencia válido");
                return;
            }

            foreach (ListItem i in selConceptos.Items)
            {
                if (i.Selected)
                {
                    selectedany = true;
                }
            }

            if (!selectedany)
            {
                MostrarMensaje("Seleccione un tipo de equivalencia válido");
                return;
            }

            var input = new CreateEquivalenceConceptInput()
            {
                Company = Convert.ToInt32(this.Session["empresa"]),
                EquivalenceConcept = (EnumConfigurationConcepts)Enum.Parse(typeof(EnumConfigurationConcepts), ddlEquivalenciasConceptos.SelectedValue)
            };

            var inputDetail = new List<CreateEquivalenceDetailInput>();

            foreach (ListItem i in selConceptos.Items)
            {
                if (i.Selected)
                {
                    inputDetail.Add(new CreateEquivalenceDetailInput()
                    {
                        ConceptId = i.Value
                    });
                }
            }

            input.GroupConceptsDetail = inputDetail;

            if (_servicePayrollConceptsEquivalence.Save(input))
            {
                ManejoExito("Transacción guardada exitosamente", "I");
            }
            else
            {
                ManejoError("Error al guardar la transacción", "I");
            }

        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            CcontrolesUsuario.HabilitarControles(
              this.Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;

            selConceptos.Visible = true;
            CargarCombos();
            try
            {
                ddlEquivalenciasConceptos.Enabled = false;
                ddlTipoEquivalencia.Enabled = false;
                var Id = ((HiddenField)gvLista.SelectedRow.FindControl("hfId")).Value;
                var data = _servicePayrollConceptsEquivalence.GetById(Guid.Parse(Id));
                ddlTipoEquivalencia.SelectedValue = ((int)data.TypeEquivalence).ToString();
                ChargeEquivalenceType();
                ddlEquivalenciasConceptos.SelectedValue = ((int)data.EquivalenceConcept).ToString();

                foreach (ListItem li in selConceptos.Items)
                {
                    li.Selected = data.GroupConceptsDetail.Any(y => y.ConceptId == li.Value);
                }

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);

            this.nilbNuevo.Visible = true;

            GetEntidad();
        }



        #endregion Eventos




        protected void ddlTipoEquivalencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChargeEquivalenceType();
        }

        private void ChargeEquivalenceType()
        {
            try
            {
                if (ddlTipoEquivalencia.SelectedValue.Trim().Length == 0)
                {

                    MostrarMensaje("Por favor seleccione un valor de entidad");
                    return;
                }


                var entitySelected = (EnumTypeEquivalences)Enum.Parse(typeof(EnumTypeEquivalences), ddlTipoEquivalencia.SelectedValue);

                var entities = _equivalenceElectronicPay.GetDataByEquivalenceType(entitySelected);
                this.ddlEquivalenciasConceptos.DataSource = entities;
                this.ddlEquivalenciasConceptos.DataValueField = "Id";
                this.ddlEquivalenciasConceptos.DataTextField = "Name";
                this.ddlEquivalenciasConceptos.DataBind();
                this.ddlEquivalenciasConceptos.Items.Insert(0, new ListItem("", ""));


            }
            catch (Exception ex)
            {
                ManejoError(string.Format("Error al cargar {0} Correspondiente a:{1} ", ddlTipoEquivalencia.ID.Substring(3, ddlTipoEquivalencia.ID.Length - 3), ex.Message), "C");
            }
        }
    }
}