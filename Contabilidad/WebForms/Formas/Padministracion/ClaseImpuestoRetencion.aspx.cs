using Contabilidad.WebForms.App_Code.Administracion;
using Contabilidad.WebForms.App_Code.CuentasxCobrar;
using Contabilidad.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.Padministracion
{
    public partial class ClaseImpuestoRetencion : BasePage
    {
        #region Instancias

        CImpRet impret = new CImpRet();

        #endregion Instancias

        #region Metodos
        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                     ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(), consulta, Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                this.gvLista.DataSource = impret.BuscarEntidad(
                    this.nitxtBusqueda.Text, Convert.ToInt16(this.Session["empresa"]));

                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(
                  this.Session["usuario"].ToString(),
                  "C",
                   ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                  "ex", this.gvLista.Rows.Count.ToString() + " Registros encontrados",
                  ObtenerIP(), Convert.ToInt16(this.Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente a: " + limpiarMensaje(ex.Message), consulta);
            }
        }
        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");

            InhabilitarControles(Page.Controls);
            LimpiarControles(Page.Controls);
            nilbNuevo.Visible = true;
            seguridad.InsertaLog(Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + Page.ToString(),
                "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }
        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text, Convert.ToInt16(this.Session["empresa"]) };

            try
            {
                if (CentidadMetodos.EntidadGetKey(
                    "cClaseIR",
                    "ppa",
                    objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    this.nilblInformacion.Text = "Centro de Costo " + this.txtCodigo.Text + " ya se encuentra registrado";

                    CcontrolesUsuario.InhabilitarControles(
                        this.Page.Controls);

                    this.nilbNuevo.Visible = true;
                    this.txtCodigo.Text = "";
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al validar la llave primaria correspondiente a: " + limpiarMensaje(ex.Message), "C");
            }
        }
        private void Guardar()
        {
            string operacion = "inserta";
            bool impuesto = false;
            bool retencion = false;
            bool autoretencion = false;
            string codigo = "";
            string referencia = null;

            try
            {

                if (Convert.ToBoolean(this.Session["editar"]) == true)
                {
                    operacion = "actualiza";
                    codigo = txtCodigo.Text;
                }
                else
                {
                    codigo = Cgeneral.RetornaConsecutivoAutomatico("cClaseIR", "codigo", Convert.ToInt16(this.Session["empresa"]));
                }

                if (ddlTipo.SelectedValue == "R")
                    retencion = true;
                if (ddlTipo.SelectedValue == "I")
                    impuesto = true;
                if (ddlTipo.SelectedValue == "A")
                    autoretencion = true;

                if (ddlRerencia.SelectedValue.Trim().Length > 0)
                    referencia = ddlRerencia.SelectedValue;

                object[] objValores = new object[]{
                      chkActivo.Checked,  //@activo
                      codigo, //@codigo
                      txtDescripcion.Text , //@descripcion
                      Convert.ToInt16(this.Session["empresa"]), //@empresa
                    impuesto,//@impuesto
                      chkmLLave.Checked, //@llaveimpuesto
                      chkCliente.Checked,      //  @mCliente bit
                      chkProveedor.Checked,      //@mProveedor bit
                      referencia, //@referencias
                      retencion, //@retencion
                      txtSigla.Text //@sigla
              
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("cClaseIR", operacion, "ppa", objValores))
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
                ManejoError("Error al guardar los datos correspondiente a: " + limpiarMensaje(ex.Message), operacion.Substring(0, 1).ToUpper());
            }
        }
        private void ManejoReferencia()
        {
            DataView dvReferencia = impret.ReternaReferenciaImpuesto(ddlTipo.SelectedValue, Convert.ToInt16(this.Session["empresa"]));
            dvReferencia.Sort = "codigo";
            ddlRerencia.DataSource = dvReferencia;
            ddlRerencia.DataValueField = "codigo";
            ddlRerencia.DataTextField = "descripcion";
            ddlRerencia.DataBind();
            ddlRerencia.Items.Insert(0, new ListItem("", ""));
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

                    if (this.txtCodigo.Text.Length > 0)
                    {
                        this.txtDescripcion.Focus();
                    }
                }
                else
                {
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
                }
            }
        }
        protected void nilbNuevo_Click(object sender, EventArgs e)
        {

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                   ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(),
                   insertar, Convert.ToInt32(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "I");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            this.txtCodigo.Enabled = false;
            this.txtCodigo.Text = Cgeneral.RetornaConsecutivoAutomatico("cClaseIR", "codigo", Convert.ToInt16(this.Session["empresa"]));
            this.txtDescripcion.Focus();
            this.nilblInformacion.Text = "";
            try
            {
                ManejoReferencia();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la referencia debido a:" + limpiarMensaje(ex.Message), "I");
            }
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
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                   nombrePaginaActual(), editar, Convert.ToInt32(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "A");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            this.txtCodigo.Enabled = false;
            this.txtDescripcion.Focus();

            try
            {

                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                {
                    this.txtCodigo.Text = this.gvLista.SelectedRow.Cells[2].Text;
                    this.txtCodigo.Enabled = false;
                }
                else
                    this.txtCodigo.Text = "";

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    this.txtDescripcion.Text = this.gvLista.SelectedRow.Cells[3].Text;
                else
                    this.txtDescripcion.Text = "";

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                    this.txtSigla.Text = this.gvLista.SelectedRow.Cells[4].Text;
                else
                    this.txtSigla.Text = "";

                foreach (Control c in gvLista.SelectedRow.Cells[5].Controls)
                {
                    if (c is CheckBox)
                    {
                        if (((CheckBox)c).Checked)
                            ddlTipo.SelectedValue = "I";
                    }

                }
                foreach (Control c in gvLista.SelectedRow.Cells[6].Controls)
                {
                    if (c is CheckBox)
                    {
                        if (((CheckBox)c).Checked)
                            ddlTipo.SelectedValue = "R";
                    }

                }
                foreach (Control c in gvLista.SelectedRow.Cells[7].Controls)
                {
                    if (c is CheckBox)
                    {
                        if (((CheckBox)c).Checked)
                            ddlTipo.SelectedValue = "A";
                    }

                }

                ManejoReferencia();

                if (this.gvLista.SelectedRow.Cells[8].Text != "&nbsp;")
                    this.ddlRerencia.SelectedValue = this.gvLista.SelectedRow.Cells[8].Text;
                else
                    this.ddlRerencia.SelectedValue = "";


                foreach (Control c in gvLista.SelectedRow.Cells[9].Controls)
                {
                    if (c is CheckBox)
                        chkmLLave.Checked = ((CheckBox)c).Checked;

                }
                foreach (Control c in gvLista.SelectedRow.Cells[10].Controls)
                {
                    if (c is CheckBox)
                        chkProveedor.Checked = ((CheckBox)c).Checked;

                }
                foreach (Control c in gvLista.SelectedRow.Cells[11].Controls)
                {
                    if (c is CheckBox)
                        chkCliente.Checked = ((CheckBox)c).Checked;

                }

                foreach (Control c in gvLista.SelectedRow.Cells[12].Controls)
                {
                    if (c is CheckBox)
                        chkActivo.Checked = ((CheckBox)c).Checked;

                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + limpiarMensaje(ex.Message), "C");
            }
        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(
                this.Session["usuario"].ToString(),
                ConfigurationManager.AppSettings["modulo"].ToString(),
                nombrePaginaActual(),
                eliminar,
                Convert.ToInt32(this.Session["empresa"].ToString())) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "E");
                return;
            }

            string operacion = "elimina";

            try
            {
                object[] objValores = new object[] {
                Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)),
                Convert.ToInt16(this.Session["empresa"])
     };

                if (CentidadMetodos.EntidadInsertUpdateDelete(
                    "cClaseIR",
                    operacion,
                    "ppa",
                    objValores) == 0)
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
                ManejoError("Error al eliminar los datos correspondiente a: " + limpiarMensaje(ex.Message), "E");
            }
        }
        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(this.Session["editar"]) == false)
                EntidadKey();
        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Length == 0 || txtDescripcion.Text.Length == 0 || txtSigla.Text.Trim().Length == 0
                )
            {

                return;
            }

            Guardar();
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.DataBind();
            GetEntidad();
        }
        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ManejoReferencia();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la referencia debido a:" + limpiarMensaje(ex.Message), "I");
            }
        }

        #endregion Eventos
    }
}