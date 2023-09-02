using Contabilidad.WebForms.App_Code.Presupuesto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.App_Code.General
{
    public partial class Presupuesto : BasePage
    {
        #region Instancias

        CcostoPresupuesto ccosto = new CcostoPresupuesto();
        Cpresupuesto presupuesto = new Cpresupuesto();

        #endregion Instancias

        #region Metodos


        private void cargarPresupuesto()
        {

            if (niddlAño.SelectedValue.Trim().Length == 0)
            {
                ManejoError("Seleccione un año valido para el presupuesto", "C");
                return;
            }
            gvPresupuesto.DataSource = presupuesto.GetPresupuesto(Convert.ToInt16(this.Session["empresa"]), this.Session["usuario"].ToString(), Convert.ToInt16(niddlAño.SelectedValue.Trim()), nitxtBusqueda.Text);
            gvPresupuesto.DataBind();

            if (gvPresupuesto.Rows.Count == 0)
            {
                btnGuardar.Visible = false;
            }
            else
            {
                btnGuardar.Visible = true;
            }
        }
        private void CargarCombos()
        {

            DataView año = presupuesto.SeleccionaAñosAbiertos(Convert.ToInt16(this.Session["empresa"]));
            niddlAño.DataSource = año;
            niddlAño.DataValueField = "año";
            niddlAño.DataTextField = "año";
            niddlAño.DataBind();
            niddlAño.Items.Insert(0, new ListItem("", ""));
        }

        static string limpiarMensaje(string strIn)
        {
            // Replace invalid characters with empty strings.
            try
            {
                return Regex.Replace(strIn, @"[^\w\.@-]", " ",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters, 
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }
        protected string nombrePaginaActual()
        {
            string[] arrResult = HttpContext.Current.Request.RawUrl.Split('/');
            string result = arrResult[arrResult.GetUpperBound(0)];
            arrResult = result.Split('?');
            return arrResult[arrResult.GetLowerBound(0)];
        }

        private void GetEntidad()
        {
            try
            {
                //if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(),//pagina
                //                consulta, Convert.ToInt16(this.Session["empresa"])) == 0)
                //{
                //    ManejoError("Usuario no autorizado para ejecutar esta operación", consulta);
                //    return;
                //}
                this.gvPresupuesto.DataSource = ccosto.BuscarEntidad("", Convert.ToInt16(this.Session["empresa"]));
                this.gvPresupuesto.DataBind();
                this.nilblInformacion.Visible = true;
                this.nilblInformacion.Text = this.gvPresupuesto.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                   "ex", this.gvPresupuesto.Rows.Count.ToString() + " Registros encontrados", HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(this.Session["empresa"]));
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
            seguridad.InsertaLog(Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + Page.ToString(),
                "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            gvPresupuesto.DataSource = null;
            gvPresupuesto.DataBind();
            CcontrolesUsuario.HabilitarControles(Page.Controls);
            cargarPresupuesto();
        }


        private void EntidadKey()
        {
            //object[] objKey = new object[] { this.txtCodigo.Text, Convert.ToInt16(this.Session["empresa"]) };

            //if (CentidadMetodos.EntidadGetKey("cCcostoPresupuesto", "ppa", objKey).Tables[0].Rows.Count > 0)
            //{
            //    ManejoError("El centro de costo " + this.txtCodigo.Text + " ya se encuentra registrada", "I");
            //    CcontrolesUsuario.InhabilitarControles(this.Page.Controls);

            //}
        }

        private void Guardar()
        {
            string operacion = "inserta";
            string padre = null;
            string cuentaPrespuesto = "";
            string ccosto = null;
            bool validar = false;

            try
            {

                cuentaPrespuesto = lblidCuentaContable.Text.Trim();

                switch (presupuesto.DeletecPresupuestoAnual(cuenta: cuentaPrespuesto, empresa: Convert.ToInt16(this.Session["empresa"])))
                {
                    case 1:
                        validar = true;
                        break;
                }

                foreach (DataListItem dli in dlCcosto.Items)
                {
                    if (((Label)dli.FindControl("lblidCcosto")).Text.Trim() != "" & ((Label)dli.FindControl("lblidCcosto")).Text.Trim().Length > 0)
                    {
                        ccosto = ((Label)dli.FindControl("lblidCcosto")).Text;
                    }

                    List<decimal> meses = new List<decimal>();
                    meses.Add(Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtEnero")).Value));
                    meses.Add(Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtFebrero")).Value));
                    meses.Add(Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtMarzo")).Value));
                    meses.Add(Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtAbril")).Value));
                    meses.Add(Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtMayo")).Value));
                    meses.Add(Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtJunio")).Value));
                    meses.Add(Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtJulio")).Value));
                    meses.Add(Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtAgosto")).Value));
                    meses.Add(Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtSeptiembre")).Value));
                    meses.Add(Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtOctubre")).Value));
                    meses.Add(Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtNoviembre")).Value));
                    meses.Add(Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtDiciembre")).Value));


                    for (int x = 1; x < 13; x++)
                    {
                        switch (presupuesto.InsertacPresupuestoAnual(empresa: Convert.ToInt16(this.Session["empresa"]),
                            cuentapresupuesto: lblidCuentaContable.Text.Trim(), ccostopresupuesto: ccosto,
                            año: Convert.ToInt16(niddlAño.SelectedValue.Trim()),
                            mes: x,
                            valor: meses[x - 1],
                            usuario: this.Session["usuario"].ToString()))
                        {
                            case 1:
                                validar = true;
                                break;
                        }
                    }

                }

                if (validar == false)
                {
                    ManejoExito("Registro creado satisfactoriamente", operacion.Substring(0, 1).ToUpper());
                }
                else
                {
                    ManejoError("Error al crear el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar el registro. Correspondiente a: " + limpiarMensaje(ex.Message), operacion.Substring(0, 1).ToUpper());
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
                    if (!IsPostBack)
                    {
                        CargarCombos();
                    }
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

            this.gvPresupuesto.DataSource = null;
            this.gvPresupuesto.DataBind();

            //this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
        }

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(
                this.Page.Controls);
            gvPresupuesto.DataSource = null;
            gvPresupuesto.DataBind();

            cargarPresupuesto();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            //if (txtCodigo.Text.Length == 0 || txtNombre.Text.Length == 0 || txtNivel.Text.Length == 0)
            //{
            //    ManejoError("Campos vacios por favor corrija", "I");
            //    return;
            //}
            Guardar();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
            //                     nombrePaginaActual(), editar, Convert.ToInt16(this.Session["empresa"])) == 0)
            //{
            //    ManejoError("Usuario no autorizado para ejecutar esta operación", editar);
            //    return;
            //}
            //CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            //this.nilbNuevo.Visible = false;
            //this.Session["editar"] = true;
            //this.txtCodigo.Enabled = false;
            //this.txtNivel.Enabled = false;
            //this.txtNombre.Focus();

            //try
            //{
            //    CargarCombos();

            //    if (this.gvLista.SelectedRow.Cells[2].Text != "")
            //        this.txtCodigo.Text = this.gvLista.SelectedRow.Cells[2].Text;
            //    else
            //        this.txtCodigo.Text = "";

            //    if (this.gvLista.SelectedRow.Cells[3].Text != "")
            //        this.txtNombre.Text = this.gvLista.SelectedRow.Cells[3].Text;
            //    else
            //        this.txtNombre.Text = "";

            //    if (this.gvLista.SelectedRow.Cells[4].Text != "")
            //        this.ddlRaiz.SelectedValue = this.gvLista.SelectedRow.Cells[4].Text;
            //    else
            //        this.ddlRaiz.SelectedValue = "";


            //    if (this.gvLista.SelectedRow.Cells[5].Text != "")
            //        this.txtNivel.Text = this.gvLista.SelectedRow.Cells[5].Text;
            //    else
            //        this.txtNivel.Text = "";

            //    if (this.gvLista.SelectedRow.Cells[6].Text != "")
            //        this.ddlTipo.Text = this.gvLista.SelectedRow.Cells[6].Text;
            //    else
            //        this.ddlTipo.Text = "";

            //    foreach (Control objControl in this.gvLista.SelectedRow.Cells[7].Controls)
            //    {
            //        this.chkActivo.Checked = ((CheckBox)objControl).Checked;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    ManejoError("Error al cargar los campos. Correspondiente a: " + ex.Message, "A");
            //}
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
            Convert.ToString(this.gvPresupuesto.Rows[e.RowIndex].Cells[2].Text),
            (int)this.Session["empresa"]
            };

                if (CentidadMetodos.EntidadInsertUpdateDelete("cCcostoPresupuesto", operacion, "ppa", objValores) == 0)
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
            //if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
            //                     nombrePaginaActual(), insertar, Convert.ToInt16(this.Session["empresa"])) == 0)
            //{
            //    ManejoError("Usuario no autorizado para ejecutar esta operación", insertar);
            //    return;
            //}
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);
            //this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            CargarCombos();
            //this.txtCodigo.Focus();
        }
        #endregion Eventos

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPresupuesto.PageIndex = e.NewPageIndex;
            gvPresupuesto.DataBind();
            GetEntidad();
        }





        protected void gvPresupuesto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                StringBuilder cstext2 = new StringBuilder();

                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;
                string cuentacontable = gvPresupuesto.Rows[rowIndex].Cells[2].Text.Trim();
                string descuentacontable = gvPresupuesto.Rows[rowIndex].Cells[3].Text.Trim();
                if (e.CommandName == "Limpiar")
                {
                    switch (presupuesto.DeletecPresupuestoAnual(cuenta: cuentacontable, empresa: Convert.ToInt16(this.Session["empresa"])))
                    {
                        case 0:
                            ManejoExito("Registro borrado exitosamente", "I");
                            break;

                        case 1:
                            ManejoError("Error al eliminar el registro", "E");
                            break;

                    }
                }

                if (e.CommandName == "addPresupuesto")
                {
                    cstext2.Append(@"
                    function modalCcosto()
                    { 
                        $('#modalPresupuesto').modal();
                        $('#modalPresupuesto').data('cuentacontable','" + cuentacontable + @"')
                    }
                    modalCcosto();
                    ");

                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), cstext2.ToString(), true);
                    lblidCuentaContable.Text = cuentacontable;
                    lblCuentaContable.Text = descuentacontable;
                    dlCcosto.DataSource = presupuesto.GetPresupuestoDetalle(empresa: Convert.ToInt32(this.Session["empresa"]), usuario: this.Session["usuario"].ToString(), año: Convert.ToInt32(niddlAño.SelectedValue), cuenta: cuentacontable);
                    dlCcosto.DataBind();

                    double sumatotal = 0;

                    foreach (DataListItem dli in dlCcosto.Items)
                    {
                        ((HtmlInputText)dli.FindControl("txtValorTotal")).Value = (Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtEnero")).Value) +
                         Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtFebrero")).Value) +
                         Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtMarzo")).Value) +
                         Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtAbril")).Value) +
                         Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtMayo")).Value) +
                         Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtJunio")).Value) +
                         Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtJulio")).Value) +
                         Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtAgosto")).Value) +
                         Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtSeptiembre")).Value) +
                         Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtOctubre")).Value) +
                         Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtNoviembre")).Value) +
                         Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtDiciembre")).Value)).ToString();
                        sumatotal = sumatotal + Convert.ToDouble(((HtmlInputText)dli.FindControl("txtValorTotal")).Value);

                        ((HtmlInputText)dli.FindControl("txtValorTotal")).Value = Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtValorTotal")).Value).ToString("N0");
                        ((HtmlInputText)dli.FindControl("txtEnero")).Value = Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtEnero")).Value).ToString("N0");
                        ((HtmlInputText)dli.FindControl("txtFebrero")).Value = Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtFebrero")).Value).ToString("N0");
                        ((HtmlInputText)dli.FindControl("txtMarzo")).Value = Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtMarzo")).Value).ToString("N0");
                        ((HtmlInputText)dli.FindControl("txtAbril")).Value = Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtAbril")).Value).ToString("N0");
                        ((HtmlInputText)dli.FindControl("txtMayo")).Value = Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtMayo")).Value).ToString("N0");
                        ((HtmlInputText)dli.FindControl("txtJunio")).Value = Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtJunio")).Value).ToString("N0");
                        ((HtmlInputText)dli.FindControl("txtJulio")).Value = Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtJulio")).Value).ToString("N0");
                        ((HtmlInputText)dli.FindControl("txtAgosto")).Value = Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtAgosto")).Value).ToString("N0");
                        ((HtmlInputText)dli.FindControl("txtSeptiembre")).Value = Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtSeptiembre")).Value).ToString("N0");
                        ((HtmlInputText)dli.FindControl("txtOctubre")).Value = Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtOctubre")).Value).ToString("N0");
                        ((HtmlInputText)dli.FindControl("txtNoviembre")).Value = Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtNoviembre")).Value).ToString("N0");
                        ((HtmlInputText)dli.FindControl("txtDiciembre")).Value = Convert.ToDecimal(((HtmlInputText)dli.FindControl("txtDiciembre")).Value).ToString("N0");

                    }
                    txtTotalCuenta.Value = Convert.ToDecimal(sumatotal).ToString("N0");
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error general debido a: " + ex.Message, "C");

            }
        }




    }
}