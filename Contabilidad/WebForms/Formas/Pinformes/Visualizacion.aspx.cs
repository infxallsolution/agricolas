using Contabilidad.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.Pinformes
{
    public partial class Visualizacion : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void tvInformes_SelectedNodeChanged(object sender, EventArgs e)
        {
            string script = "";

            switch (((TreeView)sender).SelectedNode.Value.ToString())
            {
                case "Conta01":
                    script = "<script language='javascript'>Visualizacion('Terceros');</script>";
                    Page.RegisterStartupScript("Visualizacion", script);
                    break;

                case "Conta02":
                    script = "<script language='javascript'>Visualizacion('Puc');</script>";
                    Page.RegisterStartupScript("Visualizacion", script);
                    break;

                case "Conta03":
                    script = "<script language='javascript'>Visualizacion('ParametrizacionContabilizacion');</script>";
                    Page.RegisterStartupScript("Visualizacion", script);
                    break;
            }
        }
    }
}