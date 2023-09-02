using Infos.seguridadInfos;
using Infos.WebForms.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infos.WebForms.Formas
{
    public partial class MenuInfos : BasePage
    {

        #region Instancias

        cMenu menu = new cMenu();
        Security seguridad = new Security();
        #endregion Instancias

        public static string Encriptar(string cadena)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(cadena);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                this.btnCambiarClave.Attributes.Add("OnClick", "javascript:return validarPasswd();");
                this.hpMenu.Attributes.Add("OnClick", "javascript: document.location.href='../WebForms/Inicio.aspx'");
            }

            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {
                try
                {
                    this.lbUsuario.Text = this.Session["usuario"].ToString();
                    this.lbNombreUsuario.Text = menu.RetornaNombreUsuario(
                        this.Session["usuario"].ToString());

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
                    this.Session["pass"] = Encriptar(Session["clave"].ToString());
                    cargarModulos();

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void cargarModulos()
        {
            this.gvLista.DataSource = menu.SeleccionaEmpresaUsuario(Convert.ToString(Session["usuario"]));
            this.gvLista.DataBind();
            this.dlMenu.DataSource = menu.SeleccionaMenu(Convert.ToString(Session["usuario"]), Convert.ToString(Session["clave"]), Convert.ToInt16(Session["empresa"]));
            this.dlMenu.DataBind();
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
                dlMenu.DataSource = null;
                dlMenu.DataBind();
                Session["empresa"] = this.gvLista.SelectedRow.Cells[1].Text;
                this.lbEmpresa.Text = this.Session["empresa"].ToString();
                this.lbEmpresa.Text = menu.RetornaNombreEmpresa(Convert.ToInt16(this.Session["empresa"].ToString()));
                this.lbFecha.Text = DateTime.Now.ToString();
                cargarModulos();
                seguridad.InsertaLog(Convert.ToString(Session["usuario"]), "IN", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                        "ex", "Cambio de empresa", ObtenerIP(), Convert.ToInt16(this.Session["empresa"].ToString()));
            }
            catch (Exception ex)
            {
            }
        }
        private string ObtenerIP()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                    localIP = ip.ToString();
            }
            return localIP;
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
    }
}