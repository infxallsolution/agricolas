using Administracion.WebForms.App_Code.General;
using Administracion.WebForms.App_Code.Operacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracion.WebForms.Formas.Poperaciones
{
    public partial class Salidas : BasePage
    {

        #region Instancias

        Cvehiculos vehiculos = new Cvehiculos();

        #endregion Instancias

        #region Metodos

        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));

            GetEntidad();
        }
        private void GetEntidad()
        {
            try
            {
                this.gvLista.DataSource = vehiculos.GetVehiculosEnPlanta("EP", Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();
                this.nilblInformacion.Visible = true;
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";
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
                    GetEntidad();
            }

        }
        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            GetEntidad();
        }
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (vehiculos.ActualizaFechaSalidaEstado(Server.HtmlDecode(Convert.ToString(this.gvLista.SelectedRow.Cells[6].Text)), Convert.ToInt16(Session["empresa"])))
                {
                    case 0:
                        GetEntidad();
                        this.nilblInformacion.Text = "Vehículo registrado satisfactoriamente";
                        break;

                    case 1:

                        this.nilblInformacion.Text = "Errores al dar salida al vehículo";
                        break;
                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }

        #endregion Eventos
    }
}