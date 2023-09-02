using Contabilidad.WebForms.App_Code.Administracion;
using Contabilidad.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.Contabilizacion
{
    public partial class GenerarPlano : BasePage
    {
        Cperiodos periodos = new Cperiodos();

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

                    int periodo = Convert.ToInt32(this.Request.QueryString["periodo"].ToString());
                    int año = Convert.ToInt32(this.Request.QueryString["año"].ToString());
                    int empresa = Convert.ToInt32(this.Request.QueryString["empresa"].ToString());
                    string texto = this.Session["textoPlano"].ToString();


                    string nombre = periodos.RetornaNombreArchivoPlano(empresa, año, periodo);
                    generarPlano(texto, nombre);

                    string script = "<script language='javascript'>" +
                                "window.close();" +
                                "</script>";
                    Page.RegisterStartupScript("Visualizacion", script);
                }

            }


        }

        protected void generarPlano(string texto, string nombre)
        {
            StringBuilder sb = new StringBuilder();
            string output = texto;
            sb.Append(output);
            string text = sb.ToString();

            Response.Clear();
            Response.ClearHeaders();

            Response.AddHeader("Content-Length", text.Length.ToString());
            Response.ContentType = "text/plain";
            Response.AppendHeader("content-disposition", "attachment;filename=\"Plano" + nombre + DateTime.Now.ToShortDateString() + ".txt\"");

            Response.Write(text);
            Response.End();

        }
    }
}