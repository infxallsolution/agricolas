using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Padministracion
{
    public partial class Conceptos : BasePage
    {
        #region Instancias
        Cconceptos conceptos = new Cconceptos();
        List<Crangos> listaRango = new List<Crangos>();
        Crangos rangos;

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
                upCabeza.Visible = false;
                this.gvLista.DataSource = conceptos.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";
                //manejoGrilla(false);

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

            CcontrolesUsuario.InhabilitarControles(this.upCabeza.Controls);
            CcontrolesUsuario.LimpiarControles(this.upCabeza.Controls);

            CcontrolesUsuario.InhabilitarControles(this.upRango.Controls);
            CcontrolesUsuario.LimpiarControles(this.upRango.Controls);
            Session["rangos"] = null;

            this.nilbNuevo.Visible = true;

            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion,
                ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));

            GetEntidad();
        }
        private void CargarCombos()
        {
            try
            {
                DataView dvConceptosFijos = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet(
                    "nConcepto", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"]));
                dvConceptosFijos.RowFilter = "fijo=True";
                this.ddlConceptoBase.DataSource = dvConceptosFijos;
                this.ddlConceptoBase.DataValueField = "codigo";
                this.ddlConceptoBase.DataTextField = "descripcion";
                this.ddlConceptoBase.DataBind();
                this.ddlConceptoBase.Items.Insert(0, new ListItem("", ""));

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar finca. Correspondiente a: " + ex.Message, "C");
            }

        }
        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text.Trim().ToString(), Convert.ToInt16(Session["empresa"]) };
            try
            {
                if (CentidadMetodos.EntidadGetKey("nConcepto", "ppa", objKey).Tables[0].Rows.Count > 0)
                {

                    MostrarMensaje("Código " + this.txtCodigo.Text + " ya se encuentra registrado");

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
            bool validar = false;
            string conceptoBase = null;
            try
            {

                using (TransactionScope ts = new TransactionScope())
                {
                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                        operacion = "actualiza";

                    if (ddlConceptoBase.SelectedValue.Length == 0)
                        conceptoBase = null;
                    else
                        conceptoBase = ddlConceptoBase.SelectedValue;


                    object[] objValores = new object[]{
                    txtAbreviatura.Text,
                    chkActivo.Checked,
                    chkConceptoAusentismo.Checked,
                    conceptoBase,
                    chkBaseCaja.Checked,
                    chkBaseCensancias.Checked,
                    chkBaseEmbargo.Checked,
                    chkBaseIntereses.Checked,
                    chkBasePrimas.Checked,
                    chkBaseSeguridad.Checked,
                    chkBaseVaciones.Checked,
                    null,
                    txtCodigo.Text,
                    chkControlaSaldo.Checked,
                    ddlControlConcepto.SelectedValue,
                    txtDescripcion.Text,
                    chkDescuentaDomingo.Checked,
                    chkDescuentaTranporte.Checked,
                    Convert.ToInt16(Session["empresa"]),
                    DateTime.Now,
                    chkConceptoFijo.Checked,
                    chkHabilitaValorTotalEnModificaLiquidacion.Checked,
                    chkIngresoGravado.Checked,
                    chkManejaRango.Checked,
                    chkMostrarCantidad.Checked,
                    chkMostrarDetalle.Checked,
                    chkMostrarFecha.Checked,
                    Convert.ToDecimal(txvNoMes.Text),
                    chkNoMostraDesprendible.Checked,
                    Convert.ToDecimal( txvPorcentaje.Text),
                    0,
                    chkPrestacionSocial.Checked,
                    Convert.ToDecimal( txvPrioridad.Text),
                    0,
                    ddlSigno.SelectedValue,
                    chkSumaPrestacionSocial.Checked,
                    ddlTipoLiquidacion.SelectedValue,
                    Session["usuario"].ToString(),
                    chkValidaPorcentaje.Checked,
                    Convert.ToDecimal(txvValor.Text),
                    Convert.ToDecimal(txvValorMinimo.Text)

                };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("nConcepto", operacion, "ppa", objValores))
                    {
                        case 0:
                            if (Convert.ToBoolean(this.Session["editar"]))
                            {
                                if ((conceptos.EliminaRangosConcepto(Convert.ToInt16(Session["empresa"]), txtCodigo.Text)) == 1)
                                {
                                    validar = true;
                                    break;
                                }
                            }

                            foreach (GridViewRow r in gvRangosConcepto.Rows)
                            {

                                object[] objValoresDetalle = new object[]{
                                txtCodigo.Text,
                                     Convert.ToInt16( this.Session["empresa"]),
                                     Convert.ToDecimal(r.Cells[3].Text),
                                     Convert.ToDecimal(r.Cells[2].Text),
                                     Convert.ToBoolean(((CheckBox)r.FindControl("chkPorRango")).Checked) ,
                                     Convert.ToDecimal(((TextBox)r.FindControl("txvPorRango")).Text) ,
                                     Convert.ToInt16(r.Cells[1].Text),
                                     Convert.ToDecimal(((TextBox)r.FindControl("txvValorRango")).Text)
                                     };

                                switch (CentidadMetodos.EntidadInsertUpdateDelete("nConceptoRango", "inserta", "ppa", objValoresDetalle))
                                {
                                    case 1:
                                        validar = true;
                                        break;
                                }

                            }

                            break;

                        case 1:

                            ManejoError("Errores al insertar el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                            break;
                    }

                    if (validar == true)
                    {
                        ManejoError("Error al insertar datos", operacion.Substring(0, 1).ToUpper());
                        return;
                    }


                    ManejoExito("Datos insertados satisfactoriamente", operacion.Substring(0, 1).ToUpper());
                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar los datos correspondiente a: " + ex.Message, operacion.Substring(0, 1).ToUpper());
            }
        }
        protected void cargarRangos()
        {

            if (conceptos.SeleccionaRangoConcepto(Convert.ToInt32(this.Session["empresa"]), txtCodigo.Text).Table.Rows.Count > 0)
            {
                upRango.Visible = true;
                List<Crangos> listaCanales = null;
                this.Session["canales"] = null;

                foreach (DataRowView dv in conceptos.SeleccionaRangoConcepto(Convert.ToInt32(this.Session["empresa"]), txtCodigo.Text))
                {

                    rangos = new Crangos(Convert.ToInt16(dv.Row.ItemArray.GetValue(2).ToString()), Convert.ToDecimal(dv.Row.ItemArray.GetValue(3)), Convert.ToDecimal(dv.Row.ItemArray.GetValue(4)),
                        Convert.ToDecimal(dv.Row.ItemArray.GetValue(5)), Convert.ToDecimal(dv.Row.ItemArray.GetValue(6)), Convert.ToBoolean(dv.Row.ItemArray.GetValue(7)));

                    if (this.Session["rangos"] == null)
                    {
                        listaCanales = new List<Crangos>();
                        listaCanales.Add(rangos);
                        this.Session["rangos"] = listaCanales;
                    }
                    else
                    {
                        listaCanales = (List<Crangos>)Session["rangos"];
                        listaCanales.Add(rangos);
                    }

                }

                this.Session["rangos"] = listaCanales;

                gvRangosConcepto.DataSource = listaCanales;
                gvRangosConcepto.DataBind();
                gvRangosConcepto.Visible = true;

                if (gvRangosConcepto.Rows.Count <= 0 && chkManejaRango.Checked)
                    MostrarMensaje("No hay registros realizados");
                else
                    this.lbRegistrar.Visible = true;
                verificarGrillaRango();
            }

        }

        private void ManejaBase()
        {
            if (chkManejaBase.Checked)
                ddlConceptoBase.Enabled = true;
            else
            {
                ddlConceptoBase.SelectedValue = "";
                ddlConceptoBase.Enabled = false;
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
                    this.txtCodigo.Focus();
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
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
            upRango.Visible = false;
            upCabeza.Visible = true;
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);

            foreach (var i in upCabeza.Controls)
            {
                CcontrolesUsuario.HabilitarControles(((Control)i).Controls);
                CcontrolesUsuario.LimpiarControles(((Control)i).Controls);
            }


            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            Session["rangos"] = null;
            Session["rangoFinal"] = null;

            CargarCombos();
            ValidaProcentaje();
            txtCodigo.Focus();


            this.nilblInformacion.Text = "";
        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {

            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            CcontrolesUsuario.HabilitarControles(upCabeza.Controls);
            CcontrolesUsuario.LimpiarControles(upCabeza.Controls);
            upCabeza.Visible = false;
            Session["rangos"] = null;
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();

            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";

        }
        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
            txtDescripcion.Focus();
        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                object[] objValores = new object[] { Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)), Convert.ToInt16(Session["empresa"]) };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("nConcepto", "elimina", "ppa", objValores))
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

            if (txtCodigo.Text.Trim().ToString().Length == 0 || txtDescripcion.Text.Length == 0 || txtAbreviatura.Text.Length == 0)
            {
                MostrarMensaje("Campos vacios por favor corrija");
                return;
            }

            if (gvRangosConcepto.Rows.Count == 0 && chkManejaRango.Checked)
            {
                MostrarMensaje("Debe ingresar un canal para continuar");
                return;
            }

            if (txvNoMes.Text.Length == 0)
            {
                MostrarMensaje("El No. de veces en mes debes ser minimo 0.");
                return;
            }

            if (chkValidaPorcentaje.Checked)
            {
                if (Convert.ToDecimal(txvPorcentaje.Text) <= 0)
                {
                    MostrarMensaje("Campos en cero (0) por favor corrija");
                    return;
                }

            }

            Guardar();
        }
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
             ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(),
             "A", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "A");
                return;
            }
            Session["rangos"] = null;

            upCabeza.Visible = true;
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.HabilitarControles(this.upCabeza.Controls);
            //manejoGrilla(true);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            this.txtCodigo.Enabled = false;
            this.txtDescripcion.Focus();
            CargarCombos();

            try
            {
                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.txtCodigo.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[2].Text);
                else
                    this.txtCodigo.Text = "";

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    this.txtDescripcion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[3].Text);
                else
                    this.txtDescripcion.Text = "";

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                    this.txtAbreviatura.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[4].Text);
                else
                    this.txtAbreviatura.Text = "";

                if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                    this.ddlSigno.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[5].Text);
                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                    this.ddlTipoLiquidacion.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[6].Text);

                if (this.gvLista.SelectedRow.Cells[7].Text != "&nbsp;")
                {
                    this.ddlConceptoBase.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[7].Text);
                    chkManejaBase.Checked = true;
                    ManejaBase();
                }
                else
                {
                    chkManejaBase.Checked = false;
                    ManejaBase();
                }

                if (this.gvLista.SelectedRow.Cells[8].Text != "&nbsp;")
                    this.txvValor.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[8].Text);
                else
                    this.txvValor.Text = "0";
                if (this.gvLista.SelectedRow.Cells[9].Text != "&nbsp;")
                    this.txvValorMinimo.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[9].Text);
                else
                    this.txvValorMinimo.Text = "0";

                if (this.gvLista.SelectedRow.Cells[19].Text != "&nbsp;")
                    this.ddlControlConcepto.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[19].Text);

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[10].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkBasePrimas.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[11].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkBaseCaja.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[12].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkBaseCensancias.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[13].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkBaseVaciones.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[14].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkBaseIntereses.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[15].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkBaseSeguridad.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[16].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkControlaSaldo.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[17].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkManejaRango.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[18].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkIngresoGravado.Checked = ((CheckBox)objControl).Checked;
                }

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[20].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkActivo.Checked = ((CheckBox)objControl).Checked;
                }
                if (this.gvLista.SelectedRow.Cells[21].Text != "&nbsp;")
                    this.txvPorcentaje.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[21].Text);
                else
                    this.txvPorcentaje.Text = "0";

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[22].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkValidaPorcentaje.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[23].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkConceptoFijo.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[24].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkBaseEmbargo.Checked = ((CheckBox)objControl).Checked;
                }
                if (this.gvLista.SelectedRow.Cells[25].Text != "&nbsp;")
                {
                    this.txvPrioridad.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[25].Text);
                }
                else
                {
                    this.txvPorcentaje.Text = "0";
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[26].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkDescuentaDomingo.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[27].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkDescuentaTranporte.Checked = ((CheckBox)objControl).Checked;
                }

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[28].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkMostrarFecha.Checked = ((CheckBox)objControl).Checked;
                }

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[29].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkNoMostraDesprendible.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[30].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkMostrarDetalle.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[31].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkConceptoAusentismo.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[32].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkPrestacionSocial.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[33].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkSumaPrestacionSocial.Checked = ((CheckBox)objControl).Checked;
                }

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[34].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkMostrarCantidad.Checked = ((CheckBox)objControl).Checked;
                }
                if (this.gvLista.SelectedRow.Cells[35].Text != "&nbsp;")
                    this.txvNoMes.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[35].Text);
                else
                    this.txvNoMes.Text = "0";

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[36].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkHabilitaValorTotalEnModificaLiquidacion.Checked = ((CheckBox)objControl).Checked;
                }



                ValidaProcentaje();
                manejaRangos();
                cargarRangos();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }
        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.InhabilitarControles(upCabeza.Controls);


            this.nilbNuevo.Visible = true;

            GetEntidad();
        }
        protected void nilblRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Novedad.aspx");
        }
        protected void gvLista_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
        protected void btnCargar_Click(object sender, EventArgs e)
        {
            if (txvRangoFinal.Text.Length == 0 || txvValorRango.Text.Length == 0)
            {
                MostrarMensaje("Campos vacios, por favor corrija");
                return;
            }


            if (gvRangosConcepto.Rows.Count == 0)
                Session["rangoFinal"] = 0;

            decimal rInicio = Convert.ToDecimal(Session["rangoFinal"]), rFinal = Convert.ToDecimal(txvRangoFinal.Text), porcetaje = 0, valor = 0;
            listaRango = null;
            if (chkPorcentajeRango.Checked)
            {
                porcetaje = Convert.ToDecimal(txvValorRango.Text);
                valor = 0;
            }
            else
            {
                valor = Convert.ToDecimal(txvValorRango.Text);
                porcetaje = 0;
            }

            if (Convert.ToDecimal(Session["rangoFinal"]) > Convert.ToDecimal(txvRangoFinal.Text))
            {
                MostrarMensaje("El siguiente rango debe ser mayor al ultimo digitado");
                return;
            }

            if (this.Session["rangos"] == null)
            {
                listaRango = new List<Crangos>();
                rangos = new Crangos(gvRangosConcepto.Rows.Count + 1, 0, rFinal, porcetaje, valor, chkPorcentajeRango.Checked);
                listaRango.Add(rangos);
                this.Session["rangos"] = listaRango;
                Session["rangoFinal"] = rFinal + 1;
            }
            else
            {

                listaRango = (List<Crangos>)this.Session["rangos"];


                rangos = new Crangos(gvRangosConcepto.Rows.Count + 1, rInicio, rFinal, porcetaje, valor, chkPorcentajeRango.Checked);
                listaRango.Add(rangos);
                this.Session["canales"] = listaRango;
                Session["rangoFinal"] = rFinal + 1;

            }

            gvRangosConcepto.Visible = true;
            gvRangosConcepto.DataSource = listaRango;





            gvRangosConcepto.DataBind();

            if (gvRangosConcepto.Rows.Count <= 0 && chkManejaRango.Checked)
            {
                this.lbRegistrar.Visible = false;
                MostrarMensaje("No hay registros realizados");
            }
            else
                this.lbRegistrar.Visible = true;

            verificarGrillaRango();
        }

        private void verificarGrillaRango()
        {
            foreach (GridViewRow r in gvRangosConcepto.Rows)
            {
                if (Convert.ToBoolean(((CheckBox)r.FindControl("chkPorRango")).Checked) == true)
                {
                    ((TextBox)r.FindControl("txvPorRango")).Enabled = true;
                    ((TextBox)r.FindControl("txvValorRango")).Enabled = false;
                }
                else
                {
                    ((TextBox)r.FindControl("txvValorRango")).Enabled = true;
                    ((TextBox)r.FindControl("txvPorRango")).Enabled = false;
                }
            }
        }
        protected void chkManejaRango_CheckedChanged(object sender, EventArgs e)
        {
            manejaRangos();

        }

        private void manejaRangos()
        {
            if (chkManejaRango.Checked)
            {
                upRango.Visible = true;
                CcontrolesUsuario.HabilitarControles(upRango.Controls);
                CcontrolesUsuario.LimpiarControles(upRango.Controls);
                gvRangosConcepto.DataSource = null;
                gvRangosConcepto.DataBind();
                txvRangoFinal.Focus();
            }
            else
            {
                upRango.Visible = false;
                CcontrolesUsuario.HabilitarControles(upRango.Controls);
                CcontrolesUsuario.LimpiarControles(upRango.Controls);
                gvRangosConcepto.DataSource = null;
                gvRangosConcepto.DataBind();
                Session["rangos"] = null;
            }
        }
        protected void gvCanal_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string tipo = Convert.ToString(this.gvRangosConcepto.Rows[e.RowIndex].Cells[1].Text.Trim());
            int numero = 1;

            listaRango = null;
            listaRango = (List<Crangos>)this.Session["rangos"];

            listaRango.RemoveAt(e.RowIndex);

            foreach (Crangos ca in listaRango)
            {
                ca.Numero = numero;
                numero++;
            }

            gvRangosConcepto.DataSource = listaRango;
            gvRangosConcepto.DataBind();

            Session["rangos"] = listaRango;
        }

        protected void chkValidaPorcentaje_CheckedChanged(object sender, EventArgs e)
        {
            ValidaProcentaje();
        }

        private void ValidaProcentaje()
        {
            if (chkValidaPorcentaje.Checked)
            {
                txvPorcentaje.Enabled = true;
                txvValor.Enabled = false;
                txvValorMinimo.Enabled = false;
                txvValor.Text = "0";
                txvValorMinimo.Text = "0";
            }
            else
            {
                txvPorcentaje.Enabled = false;
                txvValor.Enabled = true;
                txvValorMinimo.Enabled = true;
                txvPorcentaje.Text = "0";
            }
        }

        protected void chkManejaBase_CheckedChanged(object sender, EventArgs e)
        {
            ManejaBase();
        }




        #endregion Eventos
    }
}