using Contabilidad.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas
{
    public partial class MenuInfos : BasePage
    {

        #region Instancias

        cMenu menu = new cMenu();

        #endregion Instancias

        public static string Encriptar(string cadena)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(cadena);
            result = Convert.ToBase64String(encryted);
            return result;
        }
        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                switch (menu.ActualizaIdSysUsuario(this.Session["usuario"].ToString(), this.txtContrasenaAnterior.Text, this.txtContrasenaNueva.Text))
                {
                    case 0:
                        this.Session["clave"] = txtContrasenaNueva.Text;
                        this.Clave.Value = this.Session["clave"].ToString();
                        this.ResolveUrl("~/WebForms/Inicio.aspx?clave=" + this.Session["clave"].ToString() + "&usuario=" + this.Session["usuario"].ToString() + "'");
                        break;
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["empresa"] = this.gvLista.SelectedRow.Cells[1].Text;
                this.lbEmpresa.Text = this.Session["empresa"].ToString();
                this.lbEmpresa.Text = menu.RetornaNombreEmpresa(Convert.ToInt16(this.Session["empresa"].ToString()));
                this.lbFecha.Text = DateTime.Now.ToString();
                seguridad.InsertaLog(Convert.ToString(Session["usuario"]), "IN", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                        "ex", "Cambio de empresa", ObtenerIP(), Convert.ToInt16(this.Session["empresa"].ToString()));
            }
            catch (Exception ex)
            {
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
            {
                if (this.Parent != null)
                    this.Parent.Page.Response.Redirect("~/WebForms/Inicio.aspx");
                else
                    this.Response.Redirect("~/WebForms/Inicio.aspx");
            }
            else
            {
                try
                {
                    this.gvLista.DataSource = menu.SeleccionaEmpresaUsuario(Convert.ToString(Session["usuario"]));
                    this.gvLista.DataBind();

                    this.lbUsuario.Text = this.Session["usuario"].ToString();
                    this.lbNombreUsuario.Text = menu.RetornaNombreUsuario(this.Session["usuario"].ToString());

                    cargarDatosModulo();

                    if (Session["empresa"] == null)
                    {
                        Session["empresa"] = menu.RetornaCodigoEmpresaUsuario(this.Session["usuario"].ToString());
                        this.lbEmpresa.Text = menu.RetornaNombreEmpresa(Convert.ToInt16(this.Session["empresa"].ToString()));
                    }
                    else
                    {
                        this.lbEmpresa.Text = menu.RetornaNombreEmpresa(Convert.ToInt16(this.Session["empresa"]));
                        this.lbFecha.Text = DateTime.Now.ToString();
                    }
                }
                catch (Exception ex)
                {
                    // lblInformacion.Text = "Error al cargar el menu. Correspondiente a: " + limpiarMensaje(ex.Message);
                }
            }


        }
        protected void imbPrincipal_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(menu.SeleccionaMenuPrincipal(this.Session["usuario"].ToString(), this.Session["clave"].ToString(), Convert.ToInt16(this.Session["empresa"])));

        }
        protected void hpMenu_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("~/WebForms/Inicio.aspx");
            this.Session["usuario"] = null;
            this.Session["clave"] = null;
            this.Session["empresa"] = null;
            HttpCookie a = new HttpCookie("this");
            a.Value = null;

        }

        private void cargarDatosModulo()
        {
            DataTable dt = menu.seleccionaIconoNombreModulo(ConfigurationManager.AppSettings["Modulo"].ToString()).ToTable();

            if (dt.Rows.Count > 0)
            {
                string icono = dt.Rows[0][3].ToString() + " fa-4x pt-4";
                string modulo = dt.Rows[0][1].ToString();

                iconoModulo.Attributes["class"] = icono;
                nombreModulo.InnerText = modulo;
            }
        }


    }
}