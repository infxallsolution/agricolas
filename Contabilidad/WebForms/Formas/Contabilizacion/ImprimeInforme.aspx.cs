using Contabilidad.WebForms.App_Code.General;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.Contabilizacion
{
    public partial class ImprimeInforme : BasePage
    {
        #region Atributos

        ReportParameter rpEmpresa;
        ReportParameter rpPeriodo;
        ReportParameter rpAño;
        ReportParameter rpTipo;

        #endregion Atributos

        #region Instancias


        #endregion Instancias

        #region Metodos

        private void SetParametrosReportes()
        {
            rpEmpresa = new ReportParameter("empresa", Convert.ToString(this.Session["empresa"]));
            rpAño = new ReportParameter("año", Convert.ToString(this.Request.QueryString["año"].ToString()));
            rpPeriodo = new ReportParameter("periodo", Convert.ToString(this.Request.QueryString["periodo"].ToString()));
            rpTipo = new ReportParameter("tipo", Convert.ToString(this.Request.QueryString["tipo"].ToString()));
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
                    System.Uri url = new Uri(ConfigurationManager.AppSettings["ReportService"].ToString());

                    this.rvImprimir.ServerReport.ReportServerCredentials = new MyReportServerCredentials();
                    this.rvImprimir.ServerReport.ReportServerUrl = url;

                    SetParametrosReportes();

                    switch (this.Request.QueryString["informe"].ToString())
                    {

                        case "Precontabilizacion":
                            this.rvImprimir.ServerReport.ReportPath = ConfigurationManager.AppSettings["UrlInformes"].ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa, rpTipo, rpAño, rpPeriodo });
                            break;

                        case "ResumenPrecontabilizacion":
                            this.rvImprimir.ServerReport.ReportPath = ConfigurationManager.AppSettings["UrlInformes"].ToString() + this.Request.QueryString["informe"].ToString();
                            this.rvImprimir.ServerReport.Refresh();
                            this.rvImprimir.ServerReport.ReportServerUrl = url;
                            SetParametrosReportes();
                            this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa, rpTipo, rpAño, rpPeriodo });
                            break;


                    }
                }
            }
        }

        #endregion Eventos

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
                    string userName =
                        ConfigurationManager.AppSettings["Usuario"].ToString();

                    if (string.IsNullOrEmpty(userName))
                        throw new Exception(
                            "Missing user name from web.config file");

                    // Password
                    string password =
                        ConfigurationManager.AppSettings["Clave"].ToString();

                    if (string.IsNullOrEmpty(password))
                        throw new Exception(
                            "Missing password from web.config file");

                    // Domain
                    string domain =
                        ConfigurationManager.AppSettings["Dominio"].ToString();

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
}