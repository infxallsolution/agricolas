using Contabilidad.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms
{
    public partial class Inicio : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Session["empresa"] = null;
                string usuario = "", clave = "";
                int empresa;

                if (Request.QueryString["u"] != null && Request.QueryString["p"] != null)
                {
                    usuario = Convert.ToString(Request.QueryString["u"]);
                    clave = DesEncriptar(Convert.ToString(Request.QueryString["p"]));
                    empresa = Convert.ToInt16(Request.QueryString["e"]);
                    try
                    {
                        switch (seguridad.ValidarUsuario(usuario, clave, ConfigurationManager.AppSettings["Modulo"].ToString()))
                        {
                            case 0:
                                seguridad.InsertaLog(usuario, "IN", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                                "ex", "Entro al Sistema Satisfactoriamente", ObtenerIP(), empresa);
                                this.Session["usuario"] = usuario;
                                this.Session["clave"] = clave;
                                this.Session["empresa"] = empresa;
                                Response.Redirect("~/Webforms/Formas/MenuInfos.aspx", false);
                                break;
                            case 1:
                                seguridad.InsertaLog(usuario, "IN", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                                 "er", "Error de usuario o contraseña", ObtenerIP(), empresa);
                                if (usuario == "" && clave == "" && empresa == 0)
                                    break;
                                else
                                {
                                    ManejoError("Usuario / Contraseña errada", ingreso);
                                    break;
                                }
                        }
                    }
                    catch (Exception ex)
                    {
                        seguridad.InsertaLog(usuario, "IN", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                                 "ex", "Entro al Sistema Satisfactoriamente", ObtenerIP(), empresa);
                        ManejoErrorCatch(this, GetType(), ex);
                    }

                }
                else
                    this.txtUsuario.Focus();
            }

        }
        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsuario.Text.Length == 0 || txtClave.Text.Length == 0)
                {
                    MostrarMensaje("Debe digitar un usuario y contraseña");
                    return;
                }

                switch (seguridad.ValidarUsuario(this.txtUsuario.Text, this.txtClave.Text, ConfigurationManager.AppSettings["Modulo"].ToString()))
                {
                    case 0:
                        seguridad.InsertaLog(this.txtUsuario.Text, "IN", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                            "ex", "Entro al Sistema Satisfactoriamente", ObtenerIP(), 0);
                        this.Session["usuario"] = this.txtUsuario.Text;
                        this.Session["clave"] = this.txtClave.Text;
                        Response.Redirect("~/WebForms/Formas/MenuInfos.aspx", false);
                        break;

                    case 1:

                        MostrarMensaje("Error de usuario o contraseña");
                        break;
                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }
    }
}