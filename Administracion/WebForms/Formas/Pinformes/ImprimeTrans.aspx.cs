using Administracion.WebForms.App_Code.Administracion;
using Administracion.WebForms.App_Code.General;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracion.WebForms.Formas.Pinformes
{
    public partial class ImprimeTrans : BasePage
    {
        #region Atributos

        ReportParameter rpEmpresa;
        ReportParameter rpNumero;
        ReportParameter rpDespacho;
        ReportParameter rpUsuario;
        Ctransacciones transacciones = new Ctransacciones();

        #endregion Atributos

        #region Instancias
        Cgeneral general = new Cgeneral();

        #endregion Instancias

        #region Metodos

        private void SetParametrosReportes()
        {
            rpEmpresa = new ReportParameter("empresa", Convert.ToString(this.Session["empresa"]));
            rpNumero = new ReportParameter("tiquete", this.txtNumero.Text.Trim());
            rpDespacho = new ReportParameter("despacho", this.txtNumero.Text.Trim());
        }


        private void CargarCombos()
        {
            try
            {
                this.ddlTipoDocumento.DataSource = transacciones.GetTipoTransaccionModulo(Convert.ToInt16(this.Session["empresa"]));
                this.ddlTipoDocumento.DataValueField = "codigo";
                this.ddlTipoDocumento.DataTextField = "descripcion";
                this.ddlTipoDocumento.DataBind();
                this.ddlTipoDocumento.Items.Insert(0, new ListItem("", ""));
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
                if (!IsPostBack)
                    CargarCombos();
            }
        }

        #endregion Eventos

        #region ClaseInterna
        [Serializable]
        public sealed class MyReportServerCredentials : IReportServerCredentials
        {
            private int empresa;
            public int Empresa
            {
                get
                {
                    return empresa;
                }

                set
                {
                    empresa = value;
                }
            }

            public MyReportServerCredentials(int empresa)
            {
                this.empresa = empresa;
            }

            public WindowsIdentity ImpersonationUser
            {
                get
                {
                    return null;
                }
            }

            public ICredentials NetworkCredentials
            {
                get
                {
                    Cgeneral cgeneral = new Cgeneral();

                    // User name
                    string userName = cgeneral.RetornoParametroGeneral("usuarioReporte", empresa);
                    string password = cgeneral.RetornoParametroGeneral("claveReporte", empresa);
                    string dominio = cgeneral.RetornoParametroGeneral("DominioReporte", empresa);

                    return new NetworkCredential(userName, password, dominio);
                }
            }

            public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
            {
                authCookie = null;
                userName = null;
                password = null;
                authority = null;
                return false;
            }
        }


        #endregion ClaseInterna

        protected void imbBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                rvTransaccion.Visible = false;
                this.lblMensaje.Text = "";

                if (this.ddlTipoDocumento.SelectedValue.Trim().Length == 0 || this.txtNumero.Text.Trim().Length == 0)
                {
                    ManejoError("Debe seleccionar un tipo de transacción y un número de transacción para realizar la consulta", "C");
                    return;
                }


                string ReportService = general.RetornoParametroGeneral("ReportService", Convert.ToInt16(Session["empresa"]));
                string modulo = "";

                foreach (DataRowView drv in general.spRetornaModuloTipoTransaccion(Convert.ToInt16(Session["empresa"]), ddlTipoDocumento.SelectedValue))
                {
                    modulo = drv.Row.ItemArray.GetValue(0).ToString();
                }

                string formato = transacciones.retornaFormatoTipoTransaccion(ddlTipoDocumento.SelectedValue.Trim(), Convert.ToInt16(Session["empresa"]));
                DataView dvDatosReportes = general.retornaDatosReporteador(modulo);

                if (modulo.Trim().Length == 0)
                {
                    ManejoError("Debe parametrizar un formato para el tipo de transacción y el módulo seleccionado ", "I");
                    return;
                }

                if (formato.Trim().Length == 0)
                {
                    ManejoError("Debe parametrizar un formato para el tipo de transacción seleccionado ", "I");
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

                SetParametrosReportes();

                Uri url = new Uri(ReportService);

                switch (this.ddlTipoDocumento.SelectedValue)
                {

                    case "OSA":

                        rpDespacho = new ReportParameter("despacho", txtNumero.Text);
                        rpEmpresa = new ReportParameter("empresa", this.Session["empresa"].ToString());
                        rpUsuario = new ReportParameter("usuario", this.Session["usuario"].ToString());
                        this.rvTransaccion.ServerReport.ReportServerCredentials = new MyReportServerCredentials(Convert.ToInt16(Session["empresa"]));
                        this.rvTransaccion.Visible = true;
                        this.rvTransaccion.ServerReport.ReportServerUrl = url;
                        this.rvTransaccion.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(0).ToString() + formato;
                        this.rvTransaccion.ServerReport.SetParameters(new ReportParameter[] { rpDespacho, rpEmpresa, rpUsuario });
                        this.rvTransaccion.ServerReport.Refresh();
                        break;

                    default:

                        this.rvTransaccion.Visible = true;
                        this.rvTransaccion.ServerReport.ReportServerCredentials = new MyReportServerCredentials(Convert.ToInt16(Session["empresa"]));
                        this.rvTransaccion.ServerReport.ReportServerUrl = url;
                        this.rvTransaccion.ServerReport.ReportPath = dvDatosReportes.Table.Rows[0].ItemArray.GetValue(0).ToString() + formato;
                        this.rvTransaccion.ServerReport.SetParameters(new ReportParameter[] { rpNumero, rpEmpresa });
                        this.rvTransaccion.ServerReport.Refresh();
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