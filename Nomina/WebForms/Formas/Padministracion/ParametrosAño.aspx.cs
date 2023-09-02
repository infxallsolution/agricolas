using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Padministracion
{
    public partial class ParametrosAño : BasePage
    {
        #region Instancias

        Cperiodos periodos = new Cperiodos();
        CparametrosAno parametros = new CparametrosAno();

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
                upCabeza.Visible = false;
                gvLista.Visible = true;

                this.gvLista.DataSource = parametros.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                manejoGrilla(false);
                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C",
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

            CcontrolesUsuario.InhabilitarControles(upCabeza.Controls);
            CcontrolesUsuario.LimpiarControles(upCabeza.Controls);

            upCabeza.Visible = false;
            this.nilbNuevo.Visible = true;

            seguridad.InsertaLog(
                this.Session["usuario"].ToString(), operacion,
                ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "ex", mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));

            GetEntidad();
        }
        private void CargarCombos()
        {
            try
            {
                this.ddlAño.DataSource = periodos.PeriodoAñoAbierto(Convert.ToInt16(Session["empresa"]));
                this.ddlAño.DataValueField = "año";
                this.ddlAño.DataTextField = "año";
                this.ddlAño.DataBind();
                this.ddlAño.Items.Insert(0, new ListItem("Año...", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar año. Correspondiente a: " + ex.Message, "C");
            }


        }
        private void EntidadKey()
        {
            object[] objKey = new object[] { this.ddlAño.SelectedValue, Convert.ToInt16(Session["empresa"]) };
            try
            {
                if (CentidadMetodos.EntidadGetKey("nParametrosAno", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    MostrarMensaje("Código " + this.ddlAño.SelectedValue.ToString() + " ya se encuentra registrado");
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
            string operacion = "inserta", jefe = null;



            try
            {

                if (Convert.ToBoolean(this.Session["editar"]) == true)
                {
                    operacion = "actualiza";
                }


                object[] objValores = new object[]{
                    Convert.ToDecimal(ddlAño.SelectedValue),//@ano
                    chkAplicaArt385.Checked,//@cAplicarArt385
                    chkDiasTNL.Checked,//@cDiasTNL
                    chkRestaIncapacidad.Checked,//@cRestaIncapacidad
                    chkSalarioIntegral.Checked,//@cSalarioIntegral
                   Convert.ToInt16(Session["empresa"]),//@empresa
                   DateTime.Now,//@fechaRegistro
                   Convert.ToDecimal( txvCantidadSMLV.Text),//@noSueldoST
                   txtObservacion.Text,//@observacion
                   Convert.ToDecimal( txvpDep.Text),//@pDependientes
                   Convert.ToDecimal( txvPorcentajeRete.Text),//@pExentoRetencion
                   Convert.ToDecimal( txvpExentoSalario1393.Text),//@pExentoSalario1393
                   Convert.ToDecimal( txvPorcentajeMaximoAFC.Text),//@pMaximoaportePension
                   Convert.ToString(Session["usuario"]),//@usuario
                   Convert.ToDecimal( txvAuxilioTransporte.Text),//@vAuxilioTransporte
                   Convert.ToDecimal( txvValorDependientes.Text),//@vDependientes
                   Convert.ToDecimal( txvIngresosBrutos.Text),//@vIngresoBruto
                   Convert.ToDecimal( txvValorMaxAFC.Text),//@vMaxAporteAFC
                   Convert.ToDecimal( txvValorMaxDeducible.Text),//@vMaxDeducibleVivienda
                   Convert.ToDecimal( txvValorMaxExento.Text),//@vMaximoExento
                   Convert.ToDecimal( txvValorMaximoSalud.Text),//@vMaxPagoSalud
                   Convert.ToDecimal( txvValorMinimoIngresos.Text),//@vMinimoingresosDeclarante
                   Convert.ToDecimal( txvPagoMinPeriodo.Text),//@vMinimoPeriodo
                   Convert.ToDecimal( txvPatrimonioBruto.Text),//@vPatrimonioBruto
                   Convert.ToDecimal( txvSalarioMinimo.Text),//@vSalarioMinimo
                   Convert.ToDecimal( txvValorUVT.Text),//@vUVT
                   Convert.ToDecimal( txvUVT1.Text),//@vUVT1
                   Convert.ToDecimal( txvUVT2.Text),//@vUVT2
                   Convert.ToDecimal( txvUVT3.Text),//@vUVT3
                   Convert.ToDecimal( txvUVT4.Text),//@vUVT4
                   Convert.ToDecimal( txvUVT4.Text),//@vUVT5
                   Convert.ToDecimal( txvUVT6.Text)   //@vUVT6

                };

                switch (CentidadMetodos.EntidadInsertUpdateDelete(
                    "nParametrosAno",
                    operacion,
                    "ppa",
                    objValores))
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
        protected void manejoGrilla(bool manejo)
        {

            for (int x = 0; x < gvLista.Columns.Count; x++)
            {
                if (x > 5)
                    gvLista.Columns[x].Visible = manejo;
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


                    this.ddlAño.Focus();

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

            upCabeza.Visible = true;
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);
            CcontrolesUsuario.HabilitarControles(upCabeza.Controls);
            CcontrolesUsuario.LimpiarControles(upCabeza.Controls);



            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;

            CargarCombos();

            ddlAño.Focus();

            this.nilblInformacion.Text = "";
        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            CcontrolesUsuario.InhabilitarControles(upCabeza.Controls);
            CcontrolesUsuario.LimpiarControles(upCabeza.Controls);

            this.gvLista.DataSource = null;
            this.gvLista.DataBind();

            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
            upCabeza.Visible = false;
        }
        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
            txvSalarioMinimo.Focus();
        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
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
                Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)),
                Convert.ToInt16(Session["empresa"])
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete(
                    "nParametrosAno",
                    "elimina",
                    "ppa",
                    objValores))
                {
                    case 0:

                        ManejoExito("Registro eliminado satisfactoriamente", "E");
                        break;

                    case 1:

                        ManejoError("Error al eliminar el registro. Operación no realizada", "E");
                        break;
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
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {

            if (ddlAño.SelectedValue.ToString().Trim().ToString().Length == 0 || txvSalarioMinimo.Text.Length == 0 || txvAuxilioTransporte.Text.Length == 0)
            {
                MostrarMensaje("Campos vacios por favor corrija");
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

            upCabeza.Visible = true;
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.HabilitarControles(this.upCabeza.Controls);

            this.nilbNuevo.Visible = false;
            manejoGrilla(true);
            this.Session["editar"] = true;

            this.ddlAño.Enabled = false;
            this.txvSalarioMinimo.Focus();
            CargarCombos();

            try
            {
                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                {
                    this.ddlAño.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[2].Text);
                }
                else
                {
                    this.ddlAño.SelectedValue = "";
                }

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                {
                    this.txvSalarioMinimo.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[3].Text);
                }
                else
                {
                    this.txvSalarioMinimo.Text = "0";
                }

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                {
                    this.txvAuxilioTransporte.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[4].Text);
                }
                else
                {
                    this.txvAuxilioTransporte.Text = "0";
                }

                if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                {
                    this.txtObservacion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[5].Text);
                }
                else
                {
                    this.txtObservacion.Text = "";
                }

                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                {
                    this.txvValorUVT.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[6].Text);
                }
                else
                {
                    this.txvValorUVT.Text = "0";
                }
                if (this.gvLista.SelectedRow.Cells[7].Text != "&nbsp;")
                {
                    this.txvPatrimonioBruto.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[7].Text);
                }
                else
                {
                    this.txvPatrimonioBruto.Text = "0";
                }
                if (this.gvLista.SelectedRow.Cells[8].Text != "&nbsp;")
                {
                    this.txvIngresosBrutos.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[8].Text);
                }
                else
                {
                    this.txvIngresosBrutos.Text = "0";
                }
                if (this.gvLista.SelectedRow.Cells[9].Text != "&nbsp;")
                {
                    this.txvPorcentajeRete.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[9].Text);
                }
                else
                {
                    this.txvPorcentajeRete.Text = "0";
                }
                if (this.gvLista.SelectedRow.Cells[10].Text != "&nbsp;")
                {
                    this.txvPorcentajeMaximoAFC.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[10].Text);
                }
                else
                {
                    this.txvPorcentajeMaximoAFC.Text = "0";
                }
                if (this.gvLista.SelectedRow.Cells[11].Text != "&nbsp;")
                {
                    this.txvpExentoSalario1393.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[11].Text);
                }
                else
                {
                    this.txvpExentoSalario1393.Text = "0";
                }
                if (this.gvLista.SelectedRow.Cells[12].Text != "&nbsp;")
                {
                    this.txvpDep.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[12].Text);
                }
                else
                {
                    this.txvpDep.Text = "0";
                }
                if (this.gvLista.SelectedRow.Cells[13].Text != "&nbsp;")
                {
                    this.txvValorMaxExento.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[13].Text);
                }
                else
                {
                    this.txvValorMaxExento.Text = "0";
                }
                if (this.gvLista.SelectedRow.Cells[14].Text != "&nbsp;")
                {
                    this.txvValorMaxAFC.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[14].Text);
                }
                else
                {
                    this.txvValorMaxAFC.Text = "0";
                }
                if (this.gvLista.SelectedRow.Cells[15].Text != "&nbsp;")
                {
                    this.txvValorMaxDeducible.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[15].Text);
                }
                else
                {
                    this.txvValorMaxDeducible.Text = "0";
                }
                if (this.gvLista.SelectedRow.Cells[16].Text != "&nbsp;")
                {
                    this.txvValorDependientes.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[16].Text);
                }
                else
                {
                    this.txvValorDependientes.Text = "0";
                }
                if (this.gvLista.SelectedRow.Cells[17].Text != "&nbsp;")
                {
                    this.txvValorMinimoIngresos.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[17].Text);
                }
                else
                {
                    this.txvValorMinimoIngresos.Text = "0";
                }
                if (this.gvLista.SelectedRow.Cells[18].Text != "&nbsp;")
                {
                    this.txvUVT1.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[18].Text);
                }
                else
                {
                    this.txvUVT1.Text = "0";
                }
                if (this.gvLista.SelectedRow.Cells[19].Text != "&nbsp;")
                {
                    this.txvUVT2.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[19].Text);
                }
                else
                {
                    this.txvUVT2.Text = "0";
                }
                if (this.gvLista.SelectedRow.Cells[20].Text != "&nbsp;")
                {
                    this.txvUVT3.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[20].Text);
                }
                else
                {
                    this.txvUVT3.Text = "0";
                }
                if (this.gvLista.SelectedRow.Cells[21].Text != "&nbsp;")
                {
                    this.txvUVT4.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[21].Text);
                }
                else
                {
                    this.txvUVT4.Text = "0";
                }
                if (this.gvLista.SelectedRow.Cells[22].Text != "&nbsp;")
                {
                    this.txvUVT5.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[22].Text);
                }
                else
                {
                    this.txvUVT5.Text = "0";
                }
                if (this.gvLista.SelectedRow.Cells[23].Text != "&nbsp;")
                {
                    this.txvUVT6.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[23].Text);
                }
                else
                {
                    this.txvUVT6.Text = "0";
                }

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[24].Controls)
                {
                    if (objControl is CheckBox)
                    {
                        this.chkRestaIncapacidad.Checked = ((CheckBox)objControl).Checked;
                    }
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[25].Controls)
                {
                    if (objControl is CheckBox)
                    {
                        this.chkDiasTNL.Checked = ((CheckBox)objControl).Checked;
                    }
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[26].Controls)
                {
                    if (objControl is CheckBox)
                    {
                        this.chkAplicaArt385.Checked = ((CheckBox)objControl).Checked;
                    }
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[27].Controls)
                {
                    if (objControl is CheckBox)
                    {
                        this.chkSalarioIntegral.Checked = ((CheckBox)objControl).Checked;
                    }
                }

                if (this.gvLista.SelectedRow.Cells[29].Text != "&nbsp;")
                {
                    this.txvPagoMinPeriodo.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[29].Text);
                }
                else
                {
                    this.txvPagoMinPeriodo.Text = "0";
                }

                if (this.gvLista.SelectedRow.Cells[30].Text != "&nbsp;")
                {
                    this.txvCantidadSMLV.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[30].Text);
                }
                else
                {
                    this.txvCantidadSMLV.Text = "0";
                }
                if (this.gvLista.SelectedRow.Cells[31].Text != "&nbsp;")
                {
                    this.txvValorMaximoSalud.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[31].Text);
                }
                else
                {
                    this.txvValorMaximoSalud.Text = "0";
                }

                gvLista.Visible = false;
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(
                this.Page.Controls);

            this.nilbNuevo.Visible = true;

            GetEntidad();

        }



        protected void gvLista_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }

        protected void ddlAño_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntidadKey();
            txvSalarioMinimo.Focus();
        }

        #endregion Eventos







    }
}