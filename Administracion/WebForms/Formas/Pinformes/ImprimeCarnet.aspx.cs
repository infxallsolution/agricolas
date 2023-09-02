using Administracion.WebForms.App_Code.General;
using Microsoft.Reporting.WebForms;
using Service.DataAcces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracion.WebForms.Formas.Pinformes
{
    public partial class ImprimeCarnet : BasePage
    {
        #region Instancias
        ReportParameter rpFuncionario;
        ReportParameter rpPlantilla;
        ReportParameter rpColor;
        ReportParameter rpEmpresa;
        #endregion Instancias

        #region Metodos

        static string limpiarMensaje(string strIn)
        {     // Replace invalid characters with empty strings.
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
        private void SetParametrosReportes()
        {
            rpEmpresa = new ReportParameter("empresa", Convert.ToString(this.Session["empresa"]));
        }

        private void CargarCombos()
        {
            try
            {

                this.ddlEmpleado.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nFuncionario", "ppa"),
                    "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlEmpleado.DataValueField = "codigo";
                this.ddlEmpleado.DataTextField = "descripcion";
                this.ddlEmpleado.DataBind();
                this.ddlEmpleado.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar empleados. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
            }

        }

        protected string nombrePaginaActual()
        {
            string[] arrResult = HttpContext.Current.Request.RawUrl.Split('/');
            string result = arrResult[arrResult.GetUpperBound(0)];
            arrResult = result.Split('?');
            return arrResult[arrResult.GetLowerBound(0)];
        }


        private void ManejoExito(string mensaje, string operacion)
        {
            this.nilblMensaje.Text = mensaje;

            CcontrolesUsuario.InhabilitarControles(
                this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(
                this.Page.Controls);
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

                if (!IsPostBack)
                {
                    CargarCombos();
                }

            }
        }

        #endregion Eventos

        protected void imbImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                string color = "Black";
                Cgeneral general = new Cgeneral();
                string ReportService = general.RetornoParametroGeneral("ReportService", Convert.ToInt16(Session["empresa"]));
                switch (this.ddlPlantilla.SelectedValue)
                {
                    case "palmaceite":
                        color = "DarkOrange";
                        break;
                    case "aceites":
                        color = "DarkGreen";
                        break;
                }

                rpFuncionario = new ReportParameter("funcionario", ddlEmpleado.SelectedValue);
                rpPlantilla = new ReportParameter("plantilla", ddlPlantilla.SelectedValue);
                rpColor = new ReportParameter("color", color);
                SetParametrosReportes();
                this.rptCarnet.Visible = true;
                rptCarnet1.Visible = true;
                this.rptCarnet.ServerReport.ReportServerCredentials = new MyReportServerCredentials();
                Uri url = new Uri(ReportService);
                this.rptCarnet.Visible = true;
                this.rptCarnet.ServerReport.ReportServerUrl = url;
                this.rptCarnet.ServerReport.ReportPath = ConfigurationManager.AppSettings["UrlFormatosCarnet"].ToString() + "CarnetEmpleado";
                this.rptCarnet.ServerReport.SetParameters(new ReportParameter[] { rpFuncionario, rpPlantilla, rpColor, rpEmpresa });
                this.rptCarnet1.ServerReport.ReportServerCredentials = new MyReportServerCredentials();
                Uri url2 = new Uri(ReportService);
                this.rptCarnet1.Visible = true;
                this.rptCarnet1.ServerReport.ReportServerUrl = url;
                this.rptCarnet1.ServerReport.ReportPath = ConfigurationManager.AppSettings["UrlFormatosCarnet"].ToString() + "CarnetEmpleado2";
                this.rptCarnet1.ServerReport.SetParameters(new ReportParameter[] { rpFuncionario, rpPlantilla });

                this.rptCarnet.ServerReport.Refresh();
                this.rptCarnet1.ServerReport.Refresh();
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }
    }


    #region ClaseInterna

    [Serializable]
    public sealed class MyReportServerCredentials :
        IReportServerCredentials
    {
        public WindowsIdentity ImpersonationUser
        {
            get
            {
                // Use the default Windows user.  Credentials will be
                // provided by the NetworkCredentials property.
                return null;
            }
        }

        public ICredentials NetworkCredentials
        {
            get
            {
                // Read the user information from the Web.config file.  
                // By reading the information on demand instead of 
                // storing it, the credentials will not be stored in 
                // session, reducing the vulnerable surface area to the
                // Web.config file, which can be secured with an ACL.

                // User name
                cParametrosReportes parametrosreport = new cParametrosReportes();
                string userName = parametrosreport.Usuario;

                if (string.IsNullOrEmpty(userName))
                    throw new Exception(
                        "Missing user name from web.config file");

                // Password
                string password = parametrosreport.Contraseña;

                if (string.IsNullOrEmpty(password))
                    throw new Exception(
                        "Missing password from web.config file");

                // Domain
                string domain = parametrosreport.Dominio;

                if (string.IsNullOrEmpty(domain))
                    throw new Exception(
                        "Missing domain from web.config file");

                return new NetworkCredential(userName, password, domain);
            }
        }

        public bool GetFormsCredentials(out Cookie authCookie,
                    out string userName, out string password,
                    out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;

            // Not using form credentials
            return false;
        }
    }

    #endregion ClaseInterna

}