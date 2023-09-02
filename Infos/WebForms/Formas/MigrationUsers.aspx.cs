using Infos.WebForms.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infos.WebForms.Formas
{
    public partial class MigrationUsers : BasePage
    {

        cMenu menu = new cMenu();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/Inicio.aspx");
            else
            {
                try
                {
                    this.lblFullName.Text = menu.RetornaNombreUsuario(this.Session["usuario"].ToString());

                }
                catch (Exception ex)
                {

                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}