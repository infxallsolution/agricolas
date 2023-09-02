using Administracion.seguridadinfos;
using Administracion.WebForms.App_Code.Administracion;
using Administracion.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracion.WebForms.Formas.Padministracion
{
    public partial class ReplicarTablas : BasePage
    {

        #region Instancias

        Creplicaciones replicaciones = new Creplicaciones();
        Centidades entidades = new Centidades();
        Security seguridad = new Security();
        CIP ip = new CIP();
        string consulta = "C";
        string insertar = "I";
        string eliminar = "E";
        string imprime = "IM";
        string ingreso = "IN";
        string editar = "A";

        #endregion Instancias

        #region Metodos
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
                if (seguridad.VerificaAccesoOperacion(
                            this.Session["usuario"].ToString(),
                             ConfigurationManager.AppSettings["modulo"].ToString(),
                             nombrePaginaActual(),
                            consulta,
                           Convert.ToInt16(this.Session["empresa"]))
                           == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", consulta);
                    return;
                }

                this.gvLista.DataSource = replicaciones.BuscarEntidadCampo(
                    this.nitxtBusqueda.Text);
                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(
                    this.Session["usuario"].ToString(),
                    consulta,
                     ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                    "ex",
                    this.gvLista.Rows.Count.ToString() + " Registros encontrados",
                   HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente a: " + limpiarMensaje(limpiarMensaje(limpiarMensaje(ex.Message))), "C");
            }
        }

        private void ManejoError(string error, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), error, "error");

            seguridad.InsertaLog(Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + Page.ToString(),
                "er", error, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
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
                this.ddlTabla.DataSource = entidades.BuscarEntidad();
                this.ddlTabla.DataValueField = "name";
                this.ddlTabla.DataTextField = "name";
                this.ddlTabla.DataBind();
                this.ddlTabla.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar entidades. Correspondiente a: " + limpiarMensaje(limpiarMensaje(ex.Message)), "C");
            }

            try
            {
                this.ddlEmpresa.DataSource = CcontrolesUsuario.OrdenarEntidadSinEmpresa(CentidadMetodos.EntidadGet("gEmpresa", "ppa"), "razonSocial");
                this.ddlEmpresa.DataValueField = "id";
                this.ddlEmpresa.DataTextField = "razonSocial";
                this.ddlEmpresa.DataBind();
                this.ddlEmpresa.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar empresa desde. Correspondiente a: " + limpiarMensaje(limpiarMensaje(ex.Message)), "C");
            }

            try
            {
                this.ddlEmpresaDestino.DataSource = CcontrolesUsuario.OrdenarEntidadSinEmpresa(CentidadMetodos.EntidadGet("gEmpresa", "ppa"), "razonSocial");
                this.ddlEmpresaDestino.DataValueField = "id";
                this.ddlEmpresaDestino.DataTextField = "razonSocial";
                this.ddlEmpresaDestino.DataBind();
                this.ddlEmpresaDestino.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar empresa destino. Correspondiente a: " + limpiarMensaje(limpiarMensaje(ex.Message)), "C");
            }
        }

        private void Guardar()
        {
            string operacion = "inserta";

            try
            {
                if (replicaciones.AgregarRegistro(ddlEmpresa.SelectedValue, ddlEmpresaDestino.SelectedValue, ddlTabla.SelectedValue) == 0)
                {

                    object[] objValores = new object[]{
                ddlEmpresa.SelectedValue,
                ddlEmpresaDestino.SelectedValue,
                DateTime.Now,
                txtNoRegistros.Text,
                ddlTabla.SelectedValue,
                Session["usuario"].ToString()
            };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete(
                        "sReplicacion",
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
                else
                {
                    ManejoError("Errores al insertar el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar los datos correspondiente a: " + limpiarMensaje(limpiarMensaje(ex.Message)), operacion.Substring(0, 1).ToUpper());
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

                }
                else
                {
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
                }
            }
        }


        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
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
                ManejoError("Usuario no autorizado para ejecutar esta operación", insertar);
                return;
            }

            CcontrolesUsuario.HabilitarControles(
                this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;

            CargarCombos();
            this.ddlEmpresa.Enabled = true;
            this.ddlEmpresa.Focus();
            this.ddlTabla.Enabled = true;
            this.ddlEmpresaDestino.Enabled = true;
            this.nilblInformacion.Text = "";
            txtNoRegistros.ReadOnly = true;
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


        protected void lbRegistrar_Click(object sender, EventArgs e)
        {

            if (ddlEmpresa.SelectedValue.Length == 0 || ddlEmpresaDestino.SelectedValue.Length == 0 || ddlTabla.SelectedValue.Length == 0 || txtNoRegistros.Text.Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Campos vacios por favor corrija", "warning");
                return;
            }

            Guardar();
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(
                                this.Session["usuario"].ToString(),
                                 ConfigurationManager.AppSettings["modulo"].ToString(),
                                 nombrePaginaActual(),
                                eliminar,
                               Convert.ToInt16(this.Session["empresa"]))
                               == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", eliminar);
                return;
            }

            string operacion = "elimina";

            try
            {
                object[] objValores = new object[] {
                Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[1].Text),

            };

                if (CentidadMetodos.EntidadInsertUpdateDelete(
                    "sReplicacion",
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
                ManejoError("Error al eliminar los datos correspondiente a: " + limpiarMensaje(limpiarMensaje(ex.Message)), "E");
            }
        }

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(
                this.Page.Controls);

            this.nilbNuevo.Visible = true;

            GetEntidad();
        }



        protected void ddlEntidad_SelectedIndexChanged(object sender, EventArgs e)
        {

            CargarNoRegistro();
        }

        private void CargarNoRegistro()
        {
            try
            {
                if (ddlTabla.SelectedValue.Length > 0 && ddlEmpresa.SelectedValue.Length > 0)
                {
                    string[] noRegistro = replicaciones.NoRegistros(ddlEmpresa.SelectedValue, ddlTabla.SelectedValue);

                    txtNoRegistros.Text = Convert.ToString(noRegistro.GetValue(0));
                }

            }
            catch (Exception ex)
            {
                ManejoError("Error al recuperar el nombre del tercero. Correspondiente a: " + limpiarMensaje(limpiarMensaje(ex.Message)), "C");
            }
        }

        #endregion Eventos


        protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarNoRegistro();
        }
    }
}