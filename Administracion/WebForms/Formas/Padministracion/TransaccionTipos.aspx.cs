using Administracion.WebForms.App_Code.Administracion;
using Administracion.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracion.WebForms.Formas.Padministracion
{
    public partial class TransaccionTipos : BasePage
    {
        #region Instancias

        CtiposTransaccion tipoTransaccion = new CtiposTransaccion();

        #endregion Instancias

        #region Metodos


        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                  nombrePaginaActual(), consulta, Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", consulta);
                    return;
                }
                this.gvLista.DataSource = tipoTransaccion.BuscarEntidad(this.nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), consulta, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                  "ex", this.gvLista.Rows.Count.ToString() + " Registros encontrados", HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
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

        private void CargarCombos()
        {
            try
            {
                DataView dvmodulos = CentidadMetodos.EntidadGet("sModulos", "ppa").Tables[0].DefaultView;
                ddlModulo.DataSource = dvmodulos;
                ddlModulo.DataValueField = "codigo";
                ddlModulo.DataTextField = "descripcion";
                ddlModulo.DataBind();
                ddlModulo.Items.Insert(0, new ListItem("", ""));

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }

        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text, Convert.ToInt16(Session["empresa"]) };

            try
            {
                if (CentidadMetodos.EntidadGetKey("gTipoTransaccion", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    ManejoError("Tipo de transacción " + this.txtCodigo.Text + " ya se encuentra registrada", "C");
                    CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
                    this.nilbNuevo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }

        private void Guardar()
        {
            string operacion = "inserta";

            try
            {
                if (Convert.ToBoolean(this.Session["editar"]) == true)
                    operacion = "actualiza";

                object[] objValores = new object[]{
                chkActivo.Checked,
                Convert.ToDecimal(this.txtActual.Text),
                this.txtCodigo.Text,
                this.txtDescripcion.Text,
                Convert.ToInt16(Session["empresa"]),
                DateTime.Now,
                txtFormatoImpresion.Text,
                Convert.ToDecimal(this.txvLongitud.Text),
                Convert.ToString(this.ddlModoAnulacion.SelectedValue),
                Convert.ToString(this.ddlModulo.SelectedValue),
                Convert.ToString(this.ddlNaturaleza.SelectedValue),
                this.chkNumeracion.Checked,
                this.txtPrefijo.Text,
                this.chkReferencia.Checked,
                this.txtDataSet.Text
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("gTipoTransaccion", operacion, "ppa", objValores))
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
                ManejoErrorCatch(this, GetType(), ex);
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
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                        nombrePaginaActual(), Convert.ToInt16(this.Session["empresa"])) != 0)
                {
                    this.nitxtBusqueda.Focus();

                    if (this.txtCodigo.Text.Length > 0)
                        this.txtDescripcion.Focus();
                }
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
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
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            CargarCombos();
            this.txtCodigo.Enabled = true;
            this.txtCodigo.Focus();
            this.nilblInformacion.Text = "";
            this.txtDataSet.Enabled = false;
            this.txtDataSet.Text = "";
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
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }


        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Length == 0 || txtDescripcion.Text.Length == 0 || txtActual.Text.Length == 0 || txvLongitud.Text.Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Campos vacios por favor corrija", "warning");
                return;
            }

            if (chkReferencia.Checked)
            {
                if (txtDataSet.Text.Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Campos vacios por favor corrija", "warning");
                    return;
                }
            }
            Guardar();
        }


        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                 nombrePaginaActual(), eliminar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", eliminar);
                return;
            }

            string operacion = "elimina";

            try
            {
                object[] objValores = new object[] { Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)), Convert.ToInt16(Session["empresa"]) };

                if (CentidadMetodos.EntidadInsertUpdateDelete("gTipoTransaccion", operacion, "ppa", objValores) == 0)
                    ManejoExito("Datos eliminados satisfactoriamente", "E");
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
                    ManejoErrorCatch(this, GetType(), ex);
            }
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();


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

            this.nilblInformacion.Text = "";
            this.txtCodigo.Enabled = false;
            this.txtDescripcion.Focus();
            this.txtDataSet.Enabled = false;
            this.txtDataSet.Text = "";
            CargarCombos();

            try
            {


                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.txtCodigo.Text = this.gvLista.SelectedRow.Cells[2].Text;
                else
                    this.txtCodigo.Text = "";

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    this.txtDescripcion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[3].Text);
                else
                    this.txtDescripcion.Text = "";

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[4].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkNumeracion.Checked = ((CheckBox)objControl).Checked;
                }

                if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                    this.txtActual.Text = this.gvLista.SelectedRow.Cells[5].Text;
                else
                    this.txtActual.Text = "";

                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                    this.txtPrefijo.Text = this.gvLista.SelectedRow.Cells[6].Text;
                else
                    this.txtPrefijo.Text = "";

                if (this.gvLista.SelectedRow.Cells[7].Text != "&nbsp;")
                    this.txvLongitud.Text = this.gvLista.SelectedRow.Cells[7].Text;
                else
                    this.txvLongitud.Text = "";

                if (this.gvLista.SelectedRow.Cells[8].Text != "&nbsp;")
                    this.ddlNaturaleza.SelectedValue = this.gvLista.SelectedRow.Cells[8].Text;

                if (this.gvLista.SelectedRow.Cells[9].Text != "&nbsp;")
                    this.ddlModulo.SelectedValue = this.gvLista.SelectedRow.Cells[9].Text;

                if (this.gvLista.SelectedRow.Cells[10].Text != "&nbsp;")
                    this.ddlModoAnulacion.SelectedValue = this.gvLista.SelectedRow.Cells[10].Text;

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[11].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkReferencia.Checked = ((CheckBox)objControl).Checked;
                }

                if (this.gvLista.SelectedRow.Cells[12].Text != "&nbsp;")
                    this.txtDataSet.Text = this.gvLista.SelectedRow.Cells[12].Text;
                else
                    this.txtDataSet.Text = "";

                if (this.chkReferencia.Checked == true)
                    this.txtDataSet.Enabled = true;

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[11].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkReferencia.Checked = ((CheckBox)objControl).Checked;
                }

                if (this.gvLista.SelectedRow.Cells[13].Text != "&nbsp;")
                    this.txtFormatoImpresion.Text = this.gvLista.SelectedRow.Cells[13].Text;
                else
                    this.txtFormatoImpresion.Text = "";

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[14].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkActivo.Checked = ((CheckBox)objControl).Checked;
                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }

        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
        }

        protected void chkReferencia_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                this.txtDataSet.Enabled = true;
                this.txtDataSet.Focus();
            }
            else
            {
                this.txtDataSet.Enabled = false;
                ((CheckBox)sender).Focus();
            }
        }

        #endregion Eventos
    }
}