using Microsoft.Reporting.Map.WebForms.BingMaps;
using Nomina.WebForms.App_Code.General;
using Nomina.WebForms.App_Code.Liquidacion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Pliquidacion
{
    public partial class GenerarPlano : BasePage
    {
        CpagosNomina pagosNomina = new CpagosNomina();





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



                    if (this.Session["textoPlano"] == null)
                        generarPlano(cargarPlano(año, periodo), "PlanoSeguridadSocial" + año.ToString() + periodo.ToString());
                    else
                    {
                        string texto = this.Session["textoPlano"].ToString();
                        string nombre = pagosNomina.RetornaNombreArchivoPlano(empresa, año, periodo);
                        generarPlano(texto, nombre);

                    }


                    string script = "<script language='javascript'>" +
                                "window.close();" +
                                "</script>";
                    Page.RegisterStartupScript("Visualizacion", script);
                }

            }


        }

        private string cargarPlano(int año, int mes)
        {
            string plano = "";
            try
            {
                foreach (DataRowView r in pagosNomina.RetornaDatosSeguridadSocial(Convert.ToInt16(Session["empresa"]), año, mes))
                {
                    plano += Server.HtmlDecode(r.Row.ItemArray.GetValue(0).ToString()) + "\r\n";
                }

                return plano;
            }
            catch (Exception ex)
            {
                ManejoError("Error al generar el plano debido a: " + ex.Message, "A");
                return "";
            }
        }

        protected void generarPlano(string texto, string nombre)
        {
            StringBuilder sb = new StringBuilder();
            string output = texto;
            sb.Append(output);
            sb.Append("\r\n");

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