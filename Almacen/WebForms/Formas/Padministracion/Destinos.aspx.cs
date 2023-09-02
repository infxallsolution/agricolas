using Almacen.WebForms.App_Code.General;
using Almacen.WebForms.App_Code.Parametros;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Almacen.WebForms.Formas.Padministracion
{
    public partial class Destinos : BasePage
    {

        #region Instancias

        Cdestino destino = new Cdestino();
        Cpuc puc = new Cpuc();

        #endregion Instancias


        #region Metodos

        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(),
                                consulta, Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", consulta);
                    return;
                }

                this.gvLista.DataSource = destino.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), consulta, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                  "ex", this.gvLista.Rows.Count.ToString() + " Registros encontrados", ObtenerIP(), Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private void ManejoExito(string mensaje, string operacion)
        {
            ManejoError(this, GetType(), mensaje, "info");

            InhabilitarControles(Page.Controls);
            LimpiarControles(Page.Controls);
            nilbNuevo.Visible = true;
            seguridad.InsertaLog(Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + Page.ToString(),
                "ex", mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }
        private void CargarCombos()
        {


        }
        private void Guardar()
        {
            string operacion = "inserta";

            try
            {
                if (Convert.ToBoolean(this.Session["editar"]) == true)
                    operacion = "actualiza";

                object[] objValores = new object[]{
                   chkActivo.Checked,    // @activo	bit
                   ddlAfecta.SelectedValue,
                   txtCodigo.Text,     //@codigo	varchar
                   txtDescripcion.Text,     //@descripcion	varchar
                   Convert.ToInt16(this.Session["empresa"]),     //@empresa	int
                   DateTime.Now,     //@fechaRegistro	datetime
                   this.Session["usuario"].ToString()//@usuario	varchar
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("iDestino", operacion, "ppa", objValores))
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
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);

            }
        }
        private void EntidadKey()
        {
            object[] objKey = new object[] {
            this.txtCodigo.Text,
            Convert.ToInt16(Session["empresa"])
        };

            try
            {
                if (CentidadMetodos.EntidadGetKey("iDestino", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    this.nilblInformacion.Text = "Destino " + this.txtCodigo.Text + " ya se encuentra registrado";

                    CcontrolesUsuario.InhabilitarControles(this.Page.Controls);

                    this.nilbNuevo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }


        #endregion Metodos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
            {
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            }
            else
            {
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(),
                        ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(),
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
        protected void imbCliente_Click(object sender, ImageClickEventArgs e)
        {
            if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), "Nivel.aspx", Convert.ToInt16(this.Session["empresa"])) != 0)
                this.Response.Redirect("Nivel.aspx");
            else
                ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
        }
        protected void ddlNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtCodigo.Focus();
        }
        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
            txtDescripcion.Focus();
        }
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(),
                                editar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", editar);
                return;
            }
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;

            txtCodigo.Enabled = false;
            this.txtDescripcion.Focus();

            try
            {
                CargarCombos();

                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.txtCodigo.Text = this.gvLista.SelectedRow.Cells[2].Text;
                else
                    this.txtCodigo.Text = "";


                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    this.txtDescripcion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[3].Text);
                else
                    this.txtDescripcion.Text = "";

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                    this.ddlAfecta.SelectedValue = this.gvLista.SelectedRow.Cells[4].Text.Trim();

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[5].Controls)
                {
                    this.chkActivo.Checked = ((CheckBox)objControl).Checked;
                }
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(),
                                  eliminar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", eliminar);
                return;
            }
            string operacion = "elimina";
            try
            {
                object[] objValores = new object[] { Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text), Convert.ToInt16(this.Session["empresa"]) };

                if (CentidadMetodos.EntidadInsertUpdateDelete("iDestino", operacion, "ppa", objValores) == 0)
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
                    CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }
        protected void nilbNuevo_Click(object sender, EventArgs e)
        {


            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(),
                                insertar, Convert.ToInt16(this.Session["empresa"])) == 0)
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
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (txtDescripcion.Text.Length == 0 || txtCodigo.Text.Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Campos vacios, por favor corrija.", "w");
                return;
            }
            Guardar();
        }
    }
}