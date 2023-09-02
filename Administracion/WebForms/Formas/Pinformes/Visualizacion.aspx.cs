using Administracion.WebForms.App_Code.Administracion;
using Administracion.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracion.WebForms.Formas.Pinformes
{
    public partial class Visualizacion : BasePage
    {
        CtiposTransaccion transacciones = new CtiposTransaccion();

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void tvInformes_SelectedNodeChanged(object sender, EventArgs e)
        {
            string script = "";

            switch (((TreeView)sender).SelectedNode.Value.ToString())
            {
                case "Bascula01":

                    script = "<script language='javascript'>" +
                        "Visualizacion('EstadoRemision');" +
                        "</script>";
                    Page.RegisterStartupScript("Visualizacion", script);
                    break;
            }
        }
        private void RefrescaSiesa()
        {
            transacciones.PasarInformacionSiesa();
            transacciones.SicronizaCasino();
        }


    }
}