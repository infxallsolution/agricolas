using Contabilidad.seguridadInfos;
using Contabilidad.WebForms.App_Code.Administracion;
using Contabilidad.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.CuentasxPagar
{
    public partial class BuscarCuentas : BasePage
    {
        #region Instancias
        Security seguridad = new Security();

        Cpuc cuentas = new Cpuc();
        CentidadMetodos centidades = new CentidadMetodos();
        CIP ip = new CIP();

        #endregion Instancias

        #region Metodos

        private void GetEntidad()
        {
            try
            {
                if (this.nitxtBusqueda.Text.Trim().Length > 0)
                {
                    this.nilblMensaje.Text = "";
                    DataView dvEntidad = cuentas.GetPuc(this.nitxtBusqueda.Text, Convert.ToInt16(this.Session["empresa"]));
                    this.gvLista.DataSource = dvEntidad;
                    this.gvLista.DataBind();
                }
                else
                {
                    ManejoError("Debe ingresar un código o descripción válida para la busqueda", "I");
                    return;
                }
            }
            catch (Exception ex)
            {
                this.nilblMensaje.Text = "Error al cargar cuentas. Correspondiente a: " + ex.Message;
            }
        }
        private void ManejoError(string error, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), error, "error");
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion,
                ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "er",
                error, ObtenerIP(), Convert.ToInt16(Session["empresa"]));
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
                    string busqueda = Request.QueryString["cuenta"];
                    nitxtBusqueda.Text = busqueda;
                    if (nitxtBusqueda.Text.Trim().Length > 0)
                        GetEntidad();
                }
                this.nitxtBusqueda.Focus();
            }
        }

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            GetEntidad();
        }

        #endregion Eventos
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
    }
}