using Administracion.WebForms.App_Code.General;
using Administracion.WebForms.App_Code.Seguridad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracion.WebForms.Formas.Pcontrol
{
    public partial class Modulos : BasePage

    {
        #region Instancias

        Cmodulos modulos = new Cmodulos();

        #endregion Instancias

        #region Metodos
        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                  nombrePaginaActual(), consulta, Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Usuario no autorizado para ejecutar esta operación", "error");
                    return;
                }


                this.gvLista.DataSource = modulos.BuscarEntidad(
                    this.nitxtBusqueda.Text);

                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(
                    this.Session["usuario"].ToString(),
                   consulta,
                    ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                   "ex",
                   this.gvLista.Rows.Count.ToString() + " Registros encontrados",
                  HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(this.Session["empresa"]));
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private void ManejoExito(string mensaje, string operacion)
        {

            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");

            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);

            this.nilbNuevo.Visible = true;

            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                  "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(this.Session["empresa"]));

            GetEntidad();
        }
        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text };

            try
            {
                if (CentidadMetodos.EntidadGetKey("sModulos", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Codigo" + this.txtCodigo.Text + " ya se encuentra registrado", "warning");
                    CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
                    this.nilbNuevo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private void Guardar()
        {
            string operacion = "inserta";

            try
            {

                if (Convert.ToBoolean(this.ViewState["editar"]) == true)
                    operacion = "actualiza";


                object[] objValores = new object[]{
                chkActivo.Checked,
                    this.txtCodigo.Text,
                    this.txtDescripcion.Text,
                    this.txtUrl.Text,
                    chkFormula.Checked,
                    this.txtImagen.Text,
                    this.txtOrden.Text,
                    txtUrlFormatos.Text,
                    txtUrlReportes.Text
                           };


                switch (CentidadMetodos.EntidadInsertUpdateDelete("sModulos", operacion, "ppa", objValores))
                {
                    case 0:
                        ManejoExito("Datos insertados satisfactoriamente", operacion.Substring(0, 1).ToUpper());
                        break;
                    case 1:
                        CerroresGeneral.ManejoError(this, GetType(), "Errores al insertar el registro. Operación no realizada", "error");
                        break;
                }


            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }

        #endregion Metodos

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
            {
                this.Response.Redirect("~/WebForms/Inicio.aspx");
                return;
            }

            if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                  nombrePaginaActual(), Convert.ToInt16(this.Session["empresa"])) != 0)
            {
                this.nitxtBusqueda.Focus();
                if (this.txtCodigo.Text.Length > 0)
                    this.txtUrl.Focus();

            }
            else
                ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            nilblInformacion.Text = "";
        }




        protected void nilbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(
                                 this.Session["usuario"].ToString(),
                                 ConfigurationManager.AppSettings["modulo"].ToString(),
                                 nombrePaginaActual(),
                                insertar,
                               Convert.ToInt16(this.Session["empresa"]))
                               == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Usuario no autorizado para ejecutar esta operación", "error");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);

            this.nilbNuevo.Visible = false;
            this.ViewState["editar"] = false;


            this.txtCodigo.Enabled = true;
            this.txtCodigo.Focus();
        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);

            this.gvLista.DataSource = null;
            this.gvLista.DataBind();

            this.nilbNuevo.Visible = true;

        }

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                 nombrePaginaActual(), editar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Usuario no autorizado para ejecutar esta operación", "error");
                return;
            }
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);

            this.nilbNuevo.Visible = false;
            this.ViewState["editar"] = true;
            this.txtCodigo.Enabled = false;
            this.txtUrl.Focus();

            try
            {


                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.txtCodigo.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[2].Text);

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    txtDescripcion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[3].Text);
                else
                    this.txtDescripcion.Text = "";

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                    this.txtUrl.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[4].Text);
                else
                    this.txtUrl.Text = "";


                if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                    this.txtOrden.Text = gvLista.SelectedRow.Cells[5].Text;
                else
                    this.txtOrden.Text = "";

                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                    this.txtImagen.Text = gvLista.SelectedRow.Cells[6].Text;
                else
                    this.txtOrden.Text = "";
                if (this.gvLista.SelectedRow.Cells[7].Text != "&nbsp;")
                    this.txtUrlFormatos.Text = gvLista.SelectedRow.Cells[7].Text;
                else
                    this.txtUrlFormatos.Text = "";
                if (this.gvLista.SelectedRow.Cells[8].Text != "&nbsp;")
                    this.txtUrlReportes.Text = gvLista.SelectedRow.Cells[8].Text;
                else
                    this.txtUrlReportes.Text = "";

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[9].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkActivo.Checked = ((CheckBox)objControl).Checked;
                }

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[10].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkFormula.Checked = ((CheckBox)objControl).Checked;
                }
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }



        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string operacion = "elimina";

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                             nombrePaginaActual(), eliminar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Usuario no autorizado para ejecutar esta operación", "error");
                return;
            }
            try
            {
                object[] objValores = new object[] { Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text) };
                if (CentidadMetodos.EntidadInsertUpdateDelete("sModulos", operacion, "ppa", objValores) == 0)
                    ManejoExito("Datos eliminados satisfactoriamente", "E");
                else
                    CerroresGeneral.ManejoError(this, GetType(), "Errores al eliminar el registro. Operación no realizada", "error");

            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }
        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
        }

        private void WriteToFile(string strPath, ref byte[] Buffer)
        {
            FileStream newFile = new FileStream(strPath, FileMode.Create);
            newFile.Write(Buffer, 0, Buffer.Length);
            newFile.Close();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Length == 0 || txtDescripcion.Text.Length == 0 || txtUrl.Text.Length == 0 || txtUrl.Text.Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Campos vacios, por favor corrija.", "warning");
                return;
            }
            Guardar();
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