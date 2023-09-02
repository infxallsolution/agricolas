using Almacen.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Almacen.WebForms.Formas.Pinformes
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
                case "Items01":

                    script = "<script language='javascript'>" +
                        "Visualizacion('Items');" +
                        "</script>";
                    Page.RegisterStartupScript("Visualizacion", script);
                    break;

                case "Items02":

                    script = "<script language='javascript'>" +
                        "Visualizacion('SaldoItemsPeriodo');" +
                        "</script>";
                    Page.RegisterStartupScript("Visualizacion", script);
                    break;

                case "Items03":

                    script = "<script language='javascript'>" +
                        "Visualizacion('MovimientoItemsPeriodo');" +
                        "</script>";
                    Page.RegisterStartupScript("Visualizacion", script);
                    break;

                case "Items04":

                    script = "<script language='javascript'>" +
                        "Visualizacion('RequerimientosPendientes');" +
                        "</script>";
                    Page.RegisterStartupScript("Visualizacion", script);
                    break;
                case "Items05":
                    script = "<script language='javascript'>Visualizacion('Salidasalmacen');</script>";
                    Page.RegisterStartupScript("Visualizacion", script);
                    break;
                case "Items06":
                    script = "<script language='javascript'>Visualizacion('OrdenesCompra');</script>";
                    Page.RegisterStartupScript("Visualizacion", script);
                    break;
                case "Items07":
                    script = "<script language='javascript'>Visualizacion('Entradas');</script>";
                    Page.RegisterStartupScript("Visualizacion", script);
                    break;
                case "Items08":
                    script = "<script language='javascript'>Visualizacion('EstadisticaItem');</script>";
                    Page.RegisterStartupScript("Visualizacion", script);
                    break;



            }
        }
    }
}