using Administracion.WebForms.App_Code.Administracion;
using Administracion.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracion.WebForms.Formas.Padministracion
{
    public partial class RegistrarHuella : BasePage
    {
        Cgeneral general = new Cgeneral();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {
                if (IsPostBack)
                {

                    try
                    {
                        if (this.Session["funcionario"] != null)
                        {
                            ddlFuncionario.SelectedValue = this.Session["funcionario"].ToString();
                        }


                        if (hfIntentos.Value == "0" & hfHuella.Value.Trim().Length > 0 & ddlFuncionario.SelectedValue.Trim().Length > 0)
                        {
                            byte[] huella = System.Convert.FromBase64String(hfHuella.Value);

                            switch (cHuella.InsertaHuella(Convert.ToInt16(this.Session["empresa"]), ddlFuncionario.SelectedValue, huella, ddlDedo.SelectedValue))
                            {
                                case 0:
                                    txtMensaje.Text = "";
                                    hfIntentos.Value = "";
                                    hfHuella.Value = "";
                                    ddlFuncionario.ClearSelection();
                                    ManejoExito("Huella registrada satisfactoriamente");
                                    this.Session["funcionario"] = null;
                                    //cargarCombox();
                                    break;
                                case 1:
                                    ManejoError("Hubo un error ", insertar);
                                    txtMensaje.Text = "";
                                    break;

                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        ManejoErrorCatch(ex);
                    }

                }
                else
                {
                    //  if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(),
                    //ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) != 0)
                    //  {
                    HabilitarControles(Page.Controls);
                    pbFoto.Visible = true;
                    cargarCombox();
                    hfempresa.Value = this.Session["empresa"].ToString();
                    //}
                    //else
                    //{
                    //    InhabilitarControles(this.Page.Controls);
                    //    ManejoError("Usuario no autorizado para ingresar a esta página", ingreso);
                    //}

                    this.Session["funcionario"] = null;
                }



            }
        }

        void cargarCombox()
        {

            try
            {
                DataView dvFUncionario = OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nFuncionarios", "ppa"), "descripcion", Convert.ToInt32(Session["empresa"]));
                this.ddlFuncionario.DataSource = dvFUncionario;
                this.ddlFuncionario.DataValueField = "codigo";
                this.ddlFuncionario.DataTextField = "descripcion";
                this.ddlFuncionario.DataBind();
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private void ManejoExito(string mensaje)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            seguridad.InsertaLog(Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + Page.ToString(),
                "ex", mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));
        }
        private void imagenaceptacancela(bool si)
        {
            if (si)
                lbMensaje.CssClass = "fa fa-check fa-4x text-danger text-center";
            else
                lbMensaje.CssClass = "fa fa-times fa-4x text-danger  text-center";
        }
        private void funcionarioeimagen()
        {

            string Rutaurl = ConfigurationManager.AppSettings["urlFoto"].ToString();
            try
            {
                pbFoto.Visible = true;

            }
            catch
            {
                this.pbFoto.ImageUrl = ConfigurationManager.AppSettings["imgDefault"].ToString();
            }
        }
        protected void nitxtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string CCfuncionario = "";
            this.txtMensaje.Text = "";
            pbFoto.Visible = true;
            this.pbFoto.ImageUrl = ConfigurationManager.AppSettings["imgDefault"].ToString();
            this.txtMensaje.ForeColor = Color.Green;


        }
        protected void imbServicio_Click(object sender, EventArgs e)
        {
            try
            {
                hfIntentos.Value = "";
                hfHuella.Value = "";

                var phuella = Process.GetProcessesByName("wfHuella.exe");

                if (phuella != null)
                {
                    foreach (Process pro in phuella)
                    {
                        pro.Close();
                    }
                }
                Process proc = Process.Start(@"C:\huella\wfHuella.exe");

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }
        protected void ddlFuncionario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Session["funcionario"] != null)
            {
                if (this.Session["funcionario"].ToString() != ddlFuncionario.SelectedValue)
                {
                    Session["funcionario"] = ((DropDownList)(sender)).SelectedValue;
                }
            }
            else
            {
                Session["funcionario"] = ((DropDownList)(sender)).SelectedValue;
            }
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                switch (cHuella.eliminaHuella(Convert.ToInt16(this.Session["empresa"]), ddlFuncionario.SelectedValue, ddlDedo.SelectedValue))
                {
                    case 0:
                        txtMensaje.Text = "";
                        hfIntentos.Value = "";
                        hfHuella.Value = "";
                        ddlFuncionario.ClearSelection();
                        ManejoExito("Huella Eliminada satisfactoriamente");
                        this.Session["funcionario"] = null;
                        break;
                    case 1:
                        ManejoError("Hubo un error ", insertar);
                        txtMensaje.Text = "";
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