using Contabilidad.seguridadInfos;
using Contabilidad.WebForms.App_Code.Administracion;
using Contabilidad.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.Padministracion
{
    public partial class UsuarioPerfil : BasePage
    {

        Cpermisos permisos = new Cpermisos();

        Security seguridad = new Security();
        Cperfiles perfiles = new Cperfiles();
        string consulta = "C";
        string insertar = "I";
        string eliminar = "E";
        string imprime = "IM";
        string ingreso = "IN";
        string editar = "A";
        static string limpiarMensaje(string strIn)
        {
            // Replace invalid characters with empty strings.
            try
            {
                return Regex.Replace(strIn, @"[^\w\.@-]", " ",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters, 
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }
        private void ManejoError(string error, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), error, "error");

            seguridad.InsertaLog(Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + Page.ToString(),
                "er", error, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
        }
        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            seguridad.InsertaLog(Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + Page.ToString(),
                "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
        }
        private void CargarDatos()
        {

            try
            {
                DataView dvempresa = CentidadMetodos.EntidadGet("gEmpresa", "ppa").Tables[0].DefaultView;
                dvempresa.Sort = "RazonSocial";
                this.ddlEmpresa.DataSource = dvempresa;
                this.ddlEmpresa.DataValueField = "id";
                this.ddlEmpresa.DataTextField = "RazonSocial";
                this.ddlEmpresa.DataBind();
                this.ddlEmpresa.Items.Insert(0, new ListItem("", ""));
                this.ddlEmpresa.SelectedValue = this.Session["empresa"].ToString();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar las empresas  correspondiente a: " + limpiarMensaje(limpiarMensaje(ex.Message)), "C");
            }


            try
            {
                gvPerfiles.DataSource = permisos.PerfilesActivos();
                gvPerfiles.DataBind();

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los perfiles correspondiente a: " + limpiarMensaje(limpiarMensaje(ex.Message)), "C");
            }

            try
            {
                gvUsuarios.DataSource = permisos.UsuariosActivos();
                gvUsuarios.DataBind();

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los usuarios correspondiente a: " + limpiarMensaje(limpiarMensaje(ex.Message)), "C");
            }
        }

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
                    CargarDatos();
                    this.Session["dvPermisos"] = null;
                    this.Session["dvUsuarios"] = null;
                    this.Session["perfil"] = null;
                    btnGuardar.Visible = true;
                }
            }

        }
        protected void chkPerfiles_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow gvru in gvUsuarios.Rows)
                {
                    ((CheckBox)gvru.Cells[0].FindControl("chkUsuarios")).Checked = false;
                }
                GridViewRow gvr = ((GridViewRow)(((CheckBox)sender).Parent.Parent));
                ChekOnly(sender, gvr.RowIndex);
                cargarPermisos(gvr);


            }
            catch (Exception ex)
            {
                ManejoError("Error al validar usuarios, perfiles y permisos debido a:" + limpiarMensaje(ex.Message), "C");
            }
        }

        private void cargarPermisos(GridViewRow gvr)
        {
            DataView dvUsuarios = perfiles.GetUsuariosPerfil(perfil: HttpUtility.HtmlDecode(gvr.Cells[1].Text), empresa: Convert.ToInt16(this.Session["empresa"]));
            this.Session["dvUsuarios"] = dvUsuarios;
            this.Session["perfil"] = HttpUtility.HtmlDecode(gvr.Cells[1].Text);
            bool bandera = false, validarPerfiles = false, permitido = false;

            foreach (GridViewRow gvru in gvUsuarios.Rows)
            {
                foreach (DataRowView drv in dvUsuarios)
                {
                    if (HttpUtility.HtmlDecode(gvru.Cells[1].Text.Trim()).Equals(drv.Row.ItemArray.GetValue(1).ToString().Trim()))
                    {
                        ((CheckBox)gvru.Cells[2].FindControl("chkUsuarios")).Checked = true;
                    }
                }
            }

            foreach (GridViewRow gvrp in gvPerfiles.Rows)
            {
                validarPerfiles = ((CheckBox)gvrp.Cells[0].FindControl("chkPerfiles")).Checked;
                if (validarPerfiles == true) break;

            }

        }

        private void ChekOnly(object sender, int gvrIndex)
        {


            foreach (GridViewRow gvrc in gvPerfiles.Rows)
            {
                var d = gvrc.Cells[0].FindControl("chkPerfiles");
                if (d is CheckBox)
                {
                    if (gvrIndex == gvrc.RowIndex)
                    {
                        if (((CheckBox)d).Checked != true)
                            ((CheckBox)d).Checked = false;
                    }
                    else
                        ((CheckBox)d).Checked = false;
                }
            }
        }

        protected void tvPermisos_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {
            TreeNode nodoActual = (TreeNode)e.Node;
            bool chequeado = e.Node.Checked;
            bool nodonocheck = false;
            int nivel = 1;
            bool bandera = false;
            bool validarPerfiles = false;

            foreach (GridViewRow gvr in gvPerfiles.Rows)
            {
                validarPerfiles = ((CheckBox)gvr.Cells[2].FindControl("chkPerfiles")).Checked;
                if (validarPerfiles == true) break;

            }

            if (validarPerfiles == false)
            {
                ManejoError("Seleccione un perfil valido para continuar", "C");
                nodoActual.Checked = false;
                return;
            }

            if (nodoActual.Parent != null)
            {
                if (nodoActual.ChildNodes.Count != 0)
                {
                    foreach (TreeNode hijos in nodoActual.ChildNodes)
                    {
                        hijos.Checked = chequeado;

                    }

                    foreach (TreeNode hermanos in nodoActual.Parent.ChildNodes)
                    {
                        bandera = hermanos.Checked;
                        if (bandera == true)
                            break;
                    }

                    nodoActual.Parent.Checked = bandera;

                }
                else
                {
                    nodoActual.Checked = chequeado;

                    foreach (TreeNode hijos in nodoActual.Parent.ChildNodes)
                    {
                        bandera = hijos.Checked;
                        if (bandera == true)
                            break;
                    }

                    nodoActual.Parent.Checked = bandera;

                    bandera = false;

                    foreach (TreeNode hijos in nodoActual.Parent.Parent.ChildNodes)
                    {
                        bandera = hijos.Checked;
                        if (bandera == true)
                            break;
                    }
                    nodoActual.Parent.Parent.Checked = bandera;
                }

            }
            else
            {
                foreach (TreeNode hijos in nodoActual.ChildNodes)
                {
                    hijos.Checked = chequeado;

                    foreach (TreeNode nietos in hijos.ChildNodes)
                    {
                        nietos.Checked = chequeado;
                    }
                }

            }
        }


        protected void lbRegistrar_Click(object sender, EventArgs e)
        {

            bool validarPerfiles = false;

            foreach (GridViewRow gvr in gvPerfiles.Rows)
            {
                validarPerfiles = ((CheckBox)gvr.Cells[2].FindControl("chkPerfiles")).Checked;
                if (validarPerfiles == true) break;

            }

            if (validarPerfiles == false)
            {
                ManejoError("Seleccione un perfil valido para continuar", "C");
                return;
            }

            try
            {
                Guardar();

            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar los datos de permisos debido " + limpiarMensaje(ex.Message), "I");
            }

        }

        private void Guardar()
        {
            DataView dvUsuarios = this.Session["dvUsuarios"] as DataView;
            string perfil = this.Session["perfil"].ToString();
            bool vTransaccion = false, permitido = false; ;

            int usuariobandera = 0;

            foreach (GridViewRow gvru in gvUsuarios.Rows)
            {
                foreach (DataRowView drvu in dvUsuarios)
                {
                    if (HttpUtility.HtmlDecode(gvru.Cells[1].Text) == drvu.Row.ItemArray.GetValue(1).ToString())
                    {
                        if (((CheckBox)gvru.Cells[1].FindControl("chkUsuarios")).Checked == true)
                        {
                            usuariobandera = 1;
                            break;
                        }


                        if (((CheckBox)gvru.Cells[1].FindControl("chkUsuarios")).Checked == false)
                        {
                            usuariobandera = 2;
                            break;
                        }

                    }
                }

                if (((CheckBox)gvru.Cells[1].FindControl("chkUsuarios")).Checked == true & usuariobandera == 0)
                {
                    usuariobandera = 3;
                }


                if (usuariobandera == 1)
                {
                    object[] objValores = new object[] {
                usuariobandera,  // activo ,
                Convert.ToInt16(  ddlEmpresa.SelectedValue),
                Convert.ToString(perfil),
                Convert.ToString(HttpUtility.HtmlDecode(gvru.Cells[1].Text))
            };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete(
                        "cUsuarioPerfiles",
                        "actualiza",
                        "ppa",
                        objValores))
                    {
                        case 1:

                            vTransaccion = true;
                            break;
                    }
                }

                if (usuariobandera == 2)
                {
                    object[] objValores = new object[] {
                Convert.ToInt16(  ddlEmpresa.SelectedValue),
                perfil,
                Convert.ToString(HttpUtility.HtmlDecode(gvru.Cells[1].Text))
            };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete(
                        "cUsuarioPerfiles",
                        "elimina",
                        "ppa",
                        objValores))
                    {
                        case 1:

                            vTransaccion = true;
                            break;
                    }
                }

                if (usuariobandera == 3)
                {
                    object[] objValores = new object[] {
                usuariobandera,  // activo ,
                Convert.ToInt16(  ddlEmpresa.SelectedValue),
                Convert.ToString(perfil),
                Convert.ToString(HttpUtility.HtmlDecode(gvru.Cells[1].Text))
            };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete(
                        "cUsuarioPerfiles",
                        "inserta",
                        "ppa",
                        objValores))
                    {

                        case 1:

                            vTransaccion = true;
                            break;
                    }
                }
                usuariobandera = 0;
            }




            if (vTransaccion == true)
            {
                ManejoError("Error al momento de guardar los registros", "I");
            }
            else
            {
                ManejoExito("Permisos asignados satisfactoriamente ", "I");
                this.Session["dvPermisos"] = null;
                this.Session["dvUsuarios"] = null;

                foreach (GridViewRow gvr in gvPerfiles.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkPerfiles")).Checked == true)
                    {
                        cargarPermisos(gvr);
                        break;
                    }

                }

            }
        }


        protected void chkMarcarUsuario_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow gvru in gvUsuarios.Rows)
            {
                ((CheckBox)gvru.Cells[0].FindControl("chkUsuarios")).Checked = true;
            }

            chkDesmarcarUsuario.Checked = !chkMarcarUsuario.Checked;
        }

        protected void chkDesmarcarUsuario_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow gvru in gvUsuarios.Rows)
            {
                ((CheckBox)gvru.Cells[0].FindControl("chkUsuarios")).Checked = false;
            }
            chkMarcarUsuario.Checked = !chkDesmarcarUsuario.Checked;
        }
    }
}