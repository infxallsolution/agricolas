using Contabilidad.seguridadInfos;
using Contabilidad.WebForms.App_Code.Administracion;
using Contabilidad.WebForms.App_Code.Contabilizacion;
using Contabilidad.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.Contabilizacion
{
    public partial class ClaseParametro : BasePage
    {
        #region Instancias

        Security seguridad = new Security();
        cClaseParametro clase = new cClaseParametro();
        Cgeneral general = new Cgeneral();
        cParametrosGenerales parametrosgenerales = new cParametrosGenerales();

        #endregion Instancias

        #region Metodos

        private void manejoCentroCosto()
        {
            if (ddlMayorCcostoCruce.SelectedValue.Trim().Length == 0)
            {
                nilblInformacion.Text = "Debe seleccionar un mayor de centro de costo de cuenta contable valido";
                return;
            }
            else
            {
                try
                {

                    DataView dvClase = CentidadMetodos.EntidadGet("cCentrosCostoSigo", "ppa").Tables[0].DefaultView;
                    dvClase.RowFilter = "empresa=" + Convert.ToString(this.Session["empresa"]) + " and activo=True and" + " mayor='" + ddlMayorCcostoCruce.SelectedValue.Trim() + "'";
                    dvClase.Sort = "descripcion";

                    this.ddlCcostoCruce.DataSource = dvClase;
                    this.ddlCcostoCruce.DataValueField = "codigo";
                    this.ddlCcostoCruce.DataTextField = "descripcion";
                    this.ddlCcostoCruce.DataBind();
                    this.ddlCcostoCruce.Items.Insert(0, new ListItem("", ""));
                }
                catch (Exception ex)
                {
                    ManejoError("Error al cargar los centro de costo de nomina para contabilización. Correspondiente a: " + ex.Message, "C");
                }
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
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "C", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }
                this.gvLista.DataSource = clase.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";
                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                        this.gvLista.Rows.Count.ToString() + " Registros encontrados", HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente a: " + ex.Message, "C");
            }
        }

        private void ManejoError(string error, string operacion)
        {
            this.Session["error"] = error;
            this.Session["paginaAnterior"] = this.Page.Request.FilePath.ToString();

            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "er", error, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            this.Response.Redirect("~/Contabilidad/Error.aspx", false);
        }

        private void ManejoExito(string mensaje, string operacion)
        {
            this.nilblInformacion.Text = mensaje;
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                    "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }

        private void CargarCombos()
        {
            try
            {
                DataView dvCuenta = parametrosgenerales.RetornaAuxiliaresPuc(Convert.ToInt16(this.Session["empresa"]));
                this.ddlCuenta.DataSource = dvCuenta;
                this.ddlCuenta.DataValueField = "codigo";
                this.ddlCuenta.DataTextField = "nombre";
                this.ddlCuenta.DataBind();
                this.ddlCuenta.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar cuenta contable. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                DataView dvCuenta = parametrosgenerales.RetornaAuxiliaresPuc(Convert.ToInt16(this.Session["empresa"]));
                this.ddlCuentaCruce.DataSource = dvCuenta;
                this.ddlCuentaCruce.DataValueField = "codigo";
                this.ddlCuentaCruce.DataTextField = "nombre";
                this.ddlCuentaCruce.DataBind();
                this.ddlCuentaCruce.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar cuenta contable. Correspondiente a: " + ex.Message, "C");
            }

            try
            {

                DataView dvClase = CentidadMetodos.EntidadGet("cCentrosCostoSigo", "ppa").Tables[0].DefaultView;
                dvClase.RowFilter = "empresa=" + Convert.ToString(this.Session["empresa"]) + " and activo=True and auxiliar=0";
                dvClase.Sort = "descripcion";

                this.ddlMayorCcostoCruce.DataSource = dvClase;
                this.ddlMayorCcostoCruce.DataValueField = "codigo";
                this.ddlMayorCcostoCruce.DataTextField = "descripcion";
                this.ddlMayorCcostoCruce.DataBind();
                this.ddlMayorCcostoCruce.Items.Insert(0, new ListItem("", ""));

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar centro de costo mayor contable. Correspondiente a: " + ex.Message, "C");
            }
        }

        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text.Trim().ToString(), Convert.ToInt16(Session["empresa"]) };
            try
            {
                if (CentidadMetodos.EntidadGetKey("cClaseParametroContaNomi", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    this.nilblInformacion.Text = "Código " + this.txtCodigo.Text + " ya se encuentra registrado";
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

            if (this.txtDescripcion.Text.Length == 0 || this.txtCodigo.Text.Length == 0)
                this.nilblInformacion.Text = "Campos vacios por favor corrija";
            else
            {
                try
                {
                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                        operacion = "actualiza";


                    object[] objValores = new object[]{

                          chkActivo.Checked,  //@activo	bit
                          ddlCcostoCruce.SelectedValue,  //@ccosto	varchar
                          ddlMayorCcostoCruce.SelectedValue,  //@ccostoMayor	varchar
                          txtCodigo.Text,  //@codigo	varchar
                          txtComprobanteSiigo.Text,  //@comprobante	varchar
                          ddlCuentaCruce.SelectedValue,  //@cuentaCruce	varchar
                          ddlCuenta.SelectedValue,  //@cuentaPuente	varchar
                        txtDescripcion.Text,  //@descripcion	varchar
                           Convert.ToInt16(Session["empresa"]), //@empresa	int
                           chkCentrocosto.Checked, //@porCentroCosto	bit
                           chkCuenta.Checked, //@porCuenta	bit
                           chkTercero.Checked, //@porTercero	bit
                           ddlTipo.SelectedValue, //@tipo	varchar
                           txtDocumentoSiigo.Text //@tipoDocumento	varchar
                    
                };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("cClaseParametroContaNomi", operacion, "ppa", objValores))
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


                    this.txtCodigo.Focus();

                }
                else
                {
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
                }
            }
        }



        protected void lbNuevo_Click(object sender, ImageClickEventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                                 ConfigurationManager.AppSettings["Modulo"].ToString(),
                                  nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            CcontrolesUsuario.HabilitarControles(
                this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;

            CargarCombos();

            try
            {
                txtCodigo.Text = Cgeneral.RetornaConsecutivoAutomatico("cClaseParametroContaNomi", "codigo", Convert.ToInt16(this.Session["empresa"]));
                txtCodigo.Enabled = false;
                txtDescripcion.Focus();
            }
            catch (Exception ex)
            {

                ManejoError("Error al cargar consecutivo debido a:  " + ex.Message, "C");

            }



            this.nilblInformacion.Text = "";
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

        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
            txtDescripcion.Focus();
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
                    "cClaseParametroContaNomi",
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

        protected void lbRegistrar_Click(object sender, ImageClickEventArgs e)
        {

            if (txtCodigo.Text.Trim().ToString().Length == 0 || txtDescripcion.Text.Length == 0 || txtDocumentoSiigo.Text.Length == 0 || txtComprobanteSiigo.Text.Length == 0)
            {
                nilblMensaje.Text = "Campos vacios por favor corrija";
                return;
            }

            if (this.ddlCuenta.SelectedValue.ToString().Length == 0)
            {
                this.nilblInformacion.Text = "Debe seleccionar una cuenta puente para cierre documento";
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
                    this.ddlTipo.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[4].Text);

                if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                    this.txtDocumentoSiigo.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[5].Text);
                else
                    this.txtDocumentoSiigo.Text = "0";

                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                    this.txtComprobanteSiigo.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[6].Text);
                else
                    this.txtComprobanteSiigo.Text = "0";

                if (this.gvLista.SelectedRow.Cells[7].Text != "&nbsp;")
                    this.ddlCuenta.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[7].Text);

                if (this.gvLista.SelectedRow.Cells[8].Text != "&nbsp;")
                    this.ddlCuentaCruce.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[8].Text);

                if (this.gvLista.SelectedRow.Cells[9].Text != "&nbsp;")
                {
                    if (this.gvLista.SelectedRow.Cells[9].Text.Trim().Length > 0)
                    {
                        this.ddlMayorCcostoCruce.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[9].Text);
                        manejoCentroCosto();
                    }
                }

                if (this.gvLista.SelectedRow.Cells[10].Text != "&nbsp;")
                    this.ddlCcostoCruce.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[10].Text);


                foreach (WebControl objControl in this.gvLista.SelectedRow.Cells[11].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkTercero.Checked = ((CheckBox)objControl).Checked;
                }


                foreach (WebControl objControl in this.gvLista.SelectedRow.Cells[12].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkCuenta.Checked = ((CheckBox)objControl).Checked;
                }


                foreach (WebControl objControl in this.gvLista.SelectedRow.Cells[13].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkCentrocosto.Checked = ((CheckBox)objControl).Checked;
                }

                foreach (WebControl objControl in this.gvLista.SelectedRow.Cells[14].Controls)
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

        protected void niimbBuscar_Click(object sender, ImageClickEventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }

        protected void gvLista_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }

        protected void ddlMayorCcostoCruce_SelectedIndexChanged(object sender, EventArgs e)
        {
            manejoCentroCosto();
        }



        #endregion Eventos







    }
}