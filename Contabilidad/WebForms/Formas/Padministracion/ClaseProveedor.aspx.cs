using Contabilidad.WebForms.App_Code.CuentasxPagar;
using Contabilidad.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.Padministracion
{
    public partial class ClaseProveedor : BasePage
    {
        #region Instancias

        CclaseProveedor claseProveedor = new CclaseProveedor();


        #endregion Instancias

        #region Metodos

        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(
                                this.Session["usuario"].ToString(),//usuario
                                 ConfigurationManager.AppSettings["modulo"].ToString(),//sitio
                                 nombrePaginaActual(),//pagina
                                consulta,//operacion
                               Convert.ToInt16(this.Session["empresa"]))//empresa
                               == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                this.gvLista.DataSource = claseProveedor.BuscarEntidad(
                    this.nitxtBusqueda.Text, Convert.ToInt16(this.Session["empresa"]));

                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(
                  this.Session["usuario"].ToString(),
                  "C",
                   ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                  "ex",
                  this.gvLista.Rows.Count.ToString() + " Registros encontrados",
                  HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(this.Session["empresa"]));
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


            seguridad.InsertaLog(
                    this.Session["usuario"].ToString(),
                    operacion,
                    ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                    "er",
                    error,
                    HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(this.Session["empresa"]));

            this.Response.Redirect("~/Contabilidad/Error.aspx", false);
        }

        private void ManejoExito(string mensaje, string operacion)
        {
            this.nilblMensaje.Text = mensaje;

            CcontrolesUsuario.InhabilitarControles(
                this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(
                this.Page.Controls);


            seguridad.InsertaLog(
                    this.Session["usuario"].ToString(),
                    operacion,
                    ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                    "ex",
                    mensaje,
                    HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(this.Session["empresa"]));

            GetEntidad();
        }



        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text, Convert.ToInt16(this.Session["empresa"]) };

            try
            {
                if (CentidadMetodos.EntidadGetKey(
                    "cxpClaseProveedor",
                    "ppa",
                    objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    this.nilblInformacion.Text = "Centro de Costo " + this.txtCodigo.Text + " ya se encuentra registrado";

                    CcontrolesUsuario.InhabilitarControles(
                        this.Page.Controls);


                    this.txtCodigo.Text = "";
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

            try
            {
                if (Convert.ToBoolean(this.Session["editar"]) == true)
                {
                    operacion = "actualiza";
                }

                object[] objValores = new object[]{
                this.txtCodigo.Text,
                this.txtDescripcion.Text ,
                Convert.ToInt16(this.Session["empresa"])
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete(
                    "cxpClaseProveedor",
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

        protected void lbNuevo_Click(object sender, EventArgs e)
        {

            if (seguridad.VerificaAccesoOperacion(
                                this.Session["usuario"].ToString(),//usuario
                                 ConfigurationManager.AppSettings["modulo"].ToString(),//sitio
                                 nombrePaginaActual(),//pagina
                                insertar,//operacion
                               Convert.ToInt16(this.Session["empresa"]))//empresa
                               == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }


            CcontrolesUsuario.HabilitarControles(
                this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.Session["editar"] = false;
            this.txtCodigo.Enabled = true;
            this.txtCodigo.Focus();
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


            this.nilblInformacion.Text = "";
        }

        protected void niimbBuscar_Click(object sender, ImageClickEventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(
                this.Page.Controls);



            GetEntidad();
        }



        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(
                                this.Session["usuario"].ToString(),//usuario
                                 ConfigurationManager.AppSettings["modulo"].ToString(),//sitio
                                 nombrePaginaActual(),//pagina
                                consulta,//operacion
                               Convert.ToInt16(this.Session["empresa"]))//empresa
                               == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            CcontrolesUsuario.HabilitarControles(
                this.Page.Controls);


            this.Session["editar"] = true;

            this.nilblMensaje.Text = "";
            this.txtCodigo.Enabled = false;
            this.txtDescripcion.Focus();

            try
            {


                if (this.gvLista.SelectedRow.Cells[2].Text != "")
                {
                    this.txtCodigo.Text = this.gvLista.SelectedRow.Cells[2].Text;
                }
                else
                {
                    this.txtCodigo.Text = "";
                }

                if (this.gvLista.SelectedRow.Cells[3].Text != "")
                {
                    this.txtDescripcion.Text = this.gvLista.SelectedRow.Cells[3].Text;
                }
                else
                {
                    this.txtDescripcion.Text = "";
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(
                                this.Session["usuario"].ToString(),//usuario
                                 ConfigurationManager.AppSettings["modulo"].ToString(),//sitio
                                 nombrePaginaActual(),//pagina
                                eliminar,//operacion
                               Convert.ToInt16(this.Session["empresa"]))//empresa
                               == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            string operacion = "elimina";

            try
            {
                object[] objValores = new object[] {
                Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text),
                Convert.ToInt16(this.Session["empresa"])
     };

                if (CentidadMetodos.EntidadInsertUpdateDelete(
                    "cxpClaseProveedor",
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
                ManejoError("Error al eliminar los datos correspondiente a: " + ex.Message, "E");
            }
        }

        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
            txtDescripcion.Focus();
        }

        #endregion Eventos

        protected void nilbGuardar_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCodigo.Text.Length == 0 || txtDescripcion.Text.Length == 0)
            {
                nilblMensaje.Text = "Campos vacios por favor corrija";
                return;
            }

            Guardar();
        }


        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            this.Response.Redirect("proveedor.aspx");
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.DataBind();
            GetEntidad();
        }
        protected void nilbTerceros_Click(object sender, ImageClickEventArgs e)
        {
            this.Response.Redirect("proveedor.aspx");
        }

        protected string nombrePaginaActual()
        {
            string[] arrResult = HttpContext.Current.Request.RawUrl.Split('/');
            string result = arrResult[arrResult.GetUpperBound(0)];
            arrResult = result.Split('?');
            return arrResult[arrResult.GetLowerBound(0)];
        }
    }
}