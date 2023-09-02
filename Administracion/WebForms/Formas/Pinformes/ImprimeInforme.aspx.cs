using Administracion.WebForms.App_Code.General;
using Microsoft.Reporting.WebForms;
using Service.DataAcces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracion.WebForms.Formas.Pinformes
{
    public partial class ImprimeInforme : BasePage
    {
        #region Atributos

        ReportParameter rpEmpresa;

        #endregion Atributos

        #region Instancias


        #endregion Instancias

        #region Metodos

        private void SetParametrosReportes()
        {
            rpEmpresa = new ReportParameter("empresa", Convert.ToString(this.Session["empresa"]));
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
                    try
                    {
                        Cgeneral general = new Cgeneral();
                        string ReportService = general.RetornoParametroGeneral("ReportService", Convert.ToInt16(Session["empresa"]));
                        DataView dvDatosReportes = general.retornaDatosReporteador(ConfigurationManager.AppSettings["modulo"].ToString());

                        if (ReportService.Trim().Length == 0)
                        {
                            ManejoError("Debe configurar la url del reporteador", "I");
                            return;
                        }

                        if (dvDatosReportes.Table.Rows.Count == 0)
                        {
                            ManejoError("No se han parametrizado ", "I");
                            return;
                        }
                        else
                        {
                            if (dvDatosReportes.Table.Rows[0].ItemArray.GetValue(0) is DBNull)
                            {
                                ManejoError("No se han parametrizado la url de formatos ", "I");
                                return;
                            }

                            if (dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1) is DBNull)
                            {
                                ManejoError("No se han parametrizado la url de reportes ", "I");
                                return;
                            }
                        }

                        System.Uri url = new Uri(ReportService);

                        this.rvImprimir.ServerReport.ReportServerCredentials = new MyReportServerCredentials();
                        this.rvImprimir.ServerReport.ReportServerUrl = url;

                        SetParametrosReportes();

                        switch (this.Request.QueryString["informe"].ToString())
                        {
                            case "EstadoRemision":
                                this.rvImprimir.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(1).ToString() + this.Request.QueryString["informe"].ToString();
                                this.rvImprimir.ServerReport.Refresh();
                                this.rvImprimir.ServerReport.ReportServerUrl = url;
                                SetParametrosReportes();
                                this.rvImprimir.ServerReport.SetParameters(new ReportParameter[] { rpEmpresa });
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        ManejoErrorCatch(this, GetType(), ex);
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
                    cParametrosReportes parametrosreport = new cParametrosReportes();
                    // Read the user information from the Web.config file.  
                    // By reading the information on demand instead of 
                    // storing it, the credentials will not be stored in 
                    // session, reducing the vulnerable surface area to the
                    // Web.config file, which can be secured with an ACL.

                    // User name
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
}