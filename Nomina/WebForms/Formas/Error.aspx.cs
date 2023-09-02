using Nomina.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas
{
    public partial class Error : BasePage
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
            {
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            }
            else
            {
                this.lblError.Text = this.Session["error"].ToString();
            }
        }


        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            this.Response.Redirect(this.Session["paginaAnterior"].ToString());
        }

        #endregion Eventos


    }
}