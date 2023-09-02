using Administracion.seguridadinfos;
using Administracion.WebForms.App_Code.General;
using Administracion.WebForms.App_Code.Seguridad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracion.WebForms.Formas.Pcontrol
{
    public partial class ConsultarLog : BasePage
    {
        #region Instancias

        Clog log = new Clog();
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


                this.gvLista.DataSource = log.GetLogFechaParametro(
                   fechaI: Convert.ToDateTime(txtDesde.Text),
                   fechaF: Convert.ToDateTime(txtHasta.Text),
                     parametro: this.nitxtBusqueda.Text, empresa: Convert.ToInt16(Session["empresa"]));

                this.DataBind();

                nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + "Registros encontrados";

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
                ManejoError("Error al cargar los datos. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
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
            seguridad.InsertaLog(Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + Page.ToString(),
                "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }

        public void GetHostNameCallBack(IAsyncResult asyncResult)
        {
            string userHostAddress = (string)asyncResult.AsyncState;

            // tenemos el nombre del equipo cliente en hostEntry.HostName
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

        protected void nilblRegresar_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("~/Seguridad/Pcontrol/Seguridad.aspx");
        }


        #endregion Eventos


        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            if (txtDesde.Text.Trim().Length == 0 || txtDesde.Text.Trim().Length == 0)
            {
                ManejoError("Debe seleccionar un rango de fecha antes de buscar, por favor corrija", "C");
                return;
            }

            this.lbImprimir.Visible = true;
            GetEntidad();
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
    }
}