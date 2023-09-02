using Agronomico.WebForms.App_Code.General;
using Agronomico.WebForms.App_Code.Transaccion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agronomico.WebForms.Formas.Padministracion
{
    public partial class LiquidaJornales : BasePage
    {

        #region Instancias


        CtipoTransaccion tipoTransaccion = new CtipoTransaccion();
        Cperiodos periodo = new Cperiodos();
        string numerotransaccion = "";
        Cgeneral general = new Cgeneral();
        Ctransacciones tran = new Ctransacciones();

        #endregion Instancias

        #region Metodos


        private void Preliquidar()
        {
            string script = "", nombreTercero = "";
            int retorno = 0;
            retorno = tran.ReliquidaJornales(Convert.ToInt32(this.Session["empresa"]), Convert.ToDateTime(txtFechaInicio.Text), Convert.ToDateTime(txtFechaFinal.Text), this.Session["usuario"].ToString());
            switch (retorno)
            {
                case 1:
                    script = @"<script type='text/javascript'>
                            alerta('Error al generar Liquidación');
                        </script>";
                    Page.RegisterStartupScript("Visualizacion", script);
                    break;

                case 0:
                    script = @"<script type='text/javascript'>
                            alerta('Reliquidación exitosamente');
                        </script>";
                    Page.RegisterStartupScript("Visualizacion", script);
                    break;

            }
        }

        private void ManejoExito(string mensaje, string operacion)
        {
            this.nilblMensaje.Text = mensaje;

            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);

            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                  "ex", mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));

        }

        #endregion Metodos

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(),
                 ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) != 0)
                {

                    if (!IsPostBack)
                    {
                        CcontrolesUsuario.HabilitarControles(this.Page.Controls);
                        CcontrolesUsuario.LimpiarControles(Page.Controls);
                        this.Session["editar"] = null;
                        this.nilblMensaje.Text = "";
                    }
                }
                else
                {
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
                }
            }
        }

        protected void btnLiquidar_Click(object sender, EventArgs e)
        {

            if (txtFechaFinal.Text.Trim().Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Seleccione una fecha inicial válida", "warning");
                return;
            }

            if (txtFechaInicio.Text.Trim().Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Seleccione una fecha final  válida", "warning");
            }

            try
            {
                Convert.ToDateTime(txtFechaFinal.Text);
                Convert.ToDateTime(txtFechaInicio.Text);
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Formato de fechas invalida", "warning");
                return;
            }

            Preliquidar();
        }

        #endregion Eventos




    }
}