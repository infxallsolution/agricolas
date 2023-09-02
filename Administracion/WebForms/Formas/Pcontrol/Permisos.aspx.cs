using Administracion.WebForms.App_Code.General;
using Administracion.WebForms.App_Code.Seguridad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administracion.WebForms.Formas.Pcontrol
{
    public partial class Permisos : BasePage
    {

        Cpermisos permisos = new Cpermisos();
        Cperfiles perfiles = new Cperfiles();
        public string modulo;

        private void cargarArbol()
        {
            try
            {
                var filtro = "nivel=1";
                int nivelhijo = 2;
                DataSet dsMenu = permisos.SeleccionaMenu();
                DataSet dsModulos = permisos.SeleciconaModulosActivos();
                DataView dvMenu = dsMenu.Tables[0].DefaultView;
                DataView dvModulos = dsModulos.Tables[0].DefaultView;
                dvMenu.Sort = "nivel";
                var segundonivel = tvPermisos.Nodes;

                foreach (DataRow registro in dsModulos.Tables[0].Rows)
                {
                    TreeNode nodo = new TreeNode(registro["descripcion"].ToString(), registro["codigo"].ToString());
                    nodo.ShowCheckBox = true;
                    nodo.Target = "Modulo";
                    nodo.Expanded = false;
                    tvPermisos.Nodes.Add(nodo);
                }

                foreach (TreeNode node in tvPermisos.Nodes)
                    CrearTreeHijo(dvMenu, node, 1, null, node.Value);

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }

            try
            {
                gvPerfiles.DataSource = permisos.PerfilesActivos();
                gvPerfiles.DataBind();

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }

            try
            {
                gvUsuarios.DataSource = permisos.UsuariosActivos();
                gvUsuarios.DataBind();

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private void RefrescarPermisos()
        {
            foreach (GridViewRow gvrp in gvPerfiles.Rows)
            {
                var chkgvrp = gvrp.FindControl("chkPerfiles") as CheckBox;
                if (chkgvrp.Checked)
                {
                    cargarPermisos(gvrp);
                    break;
                }
            }
        }

        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            seguridad.InsertaLog(Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + Page.ToString(),
                "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
        }

        private void CrearTreeHijo(DataView dvMenu, TreeNode padre, int nivelhijo, string papa, string modulo)
        {
            DataView dvMenuNivel = dvMenu;
            if (papa != null)
                dvMenuNivel.RowFilter = "nivel=" + nivelhijo.ToString() + " and padre=" + papa + " and modulo='" + modulo + "'";
            else
                dvMenuNivel.RowFilter = "nivel=" + nivelhijo.ToString() + " and modulo='" + modulo + "'"; ;

            foreach (DataRowView drv in dvMenuNivel)
            {
                TreeNode nodo = new TreeNode(drv.Row["nombre"].ToString(), drv.Row["rowid"].ToString());
                nodo.ShowCheckBox = true;

                if (papa != null)
                    dvMenuNivel.RowFilter = "padre=" + papa + " and nivel=" + nivelhijo.ToString() + " and modulo='" + modulo + "'";
                else
                    dvMenuNivel.RowFilter = "nivel=" + (Convert.ToInt16(drv.Row["nivel"]) + 1).ToString() + " and modulo='" + modulo + "'";



                if (dvMenuNivel.Count > 0)
                {
                    if (Convert.ToBoolean(drv.Row["mweb"]))
                    {
                        DataView dvOperacioens = permisos.SeleciconaOperacionesMenu(Convert.ToInt16(nodo.Value));
                        foreach (DataRowView drvo in dvOperacioens)
                        {
                            TreeNode operacion = new TreeNode(drvo.Row["descripcion"].ToString(), drvo.Row["codigo"].ToString());
                            operacion.ShowCheckBox = true;
                            operacion.Target = "O";
                            nodo.ChildNodes.Add(operacion);
                        }
                    }
                    CrearTreeHijo(dvMenuNivel, nodo, Convert.ToInt16(drv.Row["nivel"]) + 1, drv.Row["rowid"].ToString(), modulo);
                }
                else
                {

                }

                padre.ChildNodes.Add(nodo);
            }


        }

        private void CargarDatos()
        {

            try
            {
                this.ddlEmpresa.DataSource = CcontrolesUsuario.OrdenarEntidadSinEmpresa(
                    CentidadMetodos.EntidadGet("gEmpresa", "ppa"), "RazonSocial");
                this.ddlEmpresa.DataValueField = "id";
                this.ddlEmpresa.DataTextField = "RazonSocial";
                this.ddlEmpresa.DataBind();
                this.ddlEmpresa.Items.Insert(0, new ListItem("", ""));
                this.ddlEmpresa.SelectedValue = this.Session["empresa"].ToString();
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }

            cargarArbol();
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
                    tvPermisos.Attributes.Add("onclick", "postBackByObject()");
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

                checkchild(tvPermisos.Nodes, false);

                GridViewRow gvr = ((GridViewRow)(((CheckBox)sender).Parent.Parent));
                ChekOnly(sender, gvr.RowIndex);
                cargarPermisos(gvr);


            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }

        protected void chequearNodo(TreeNodeCollection nodos, string valorbuscado, string operacion)
        {
            foreach (TreeNode nodo in nodos)
            {
                if (nodo.ChildNodes != null)
                {
                    chequearNodo(nodo.ChildNodes, valorbuscado, operacion);
                }

                if (nodo.Parent != null)
                {
                    if (nodo.Parent.Value == valorbuscado)
                    {
                        if (nodo.Value == operacion)
                            nodo.Checked = true;

                        checkParent(nodo, true);
                    }

                }
            }
        }

        private void cargarPermisos(GridViewRow gvr)
        {
            DataView dvPermisos = permisos.SeleccionaPermisosPerfilEmpresa(perfil: HttpUtility.HtmlDecode(gvr.Cells[1].Text), empresa: Convert.ToInt16(ddlEmpresa.SelectedValue));
            DataView dvUsuarios = perfiles.GetUsuariosPerfil(perfil: HttpUtility.HtmlDecode(gvr.Cells[1].Text), empresa: Convert.ToInt16(ddlEmpresa.SelectedValue));
            this.Session["dvPermisos"] = dvPermisos;
            this.Session["dvUsuarios"] = dvUsuarios;
            this.Session["perfil"] = HttpUtility.HtmlDecode(gvr.Cells[1].Text);

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

            foreach (DataRowView drv in dvPermisos)
            {
                chequearNodo(tvPermisos.Nodes, drv.Row.ItemArray.GetValue(2).ToString(), drv.Row.ItemArray.GetValue(3).ToString());
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
                nodoActual.Checked = chequeado;
                checkParent(nodoActual.Parent, chequeado);
            }
            if (nodoActual.ChildNodes != null)
            {
                checkchild(nodoActual.ChildNodes, chequeado);
            }

        }

        protected void checkchild(TreeNodeCollection nodos, bool chequeado)
        {
            foreach (TreeNode tn in nodos)
            {
                tn.Checked = chequeado;
                if (tn.ChildNodes != null)
                {
                    checkchild(tn.ChildNodes, chequeado);
                }
            }
        }

        protected void checkParent(TreeNode nodo, bool chequeado)
        {
            if (nodo.ChildNodes != null)
            {
                bool validarcheck = false;
                foreach (TreeNode trn in nodo.ChildNodes)
                {
                    if (trn.Checked)
                    {
                        validarcheck = true;
                        break;
                    }
                }

                if (validarcheck)
                {
                    nodo.Checked = true;
                }

                if (nodo.Parent != null)
                    checkParent(nodo.Parent, chequeado);
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


            Guardar();



        }

        protected void buscarPadre(TreeNode padre)
        {
            if (padre.Parent != null)
            {
                buscarPadre(padre.Parent);
            }
        }
        private void ListarHijos(TreeNodeCollection tnc, List<Cpermisos> listaHijos)
        {
            try
            {

                foreach (TreeNode tn in tnc)
                {
                    if (tn.Value != modulo && tn.Target == "Modulo")
                    {
                        modulo = tn.Value;
                    }

                    if (tn.ChildNodes != null)
                    {
                        ListarHijos(tn.ChildNodes, listaHijos);
                    }


                    if (tn.Target == "O" && tn.Parent != null)
                    {

                        listaHijos.Add(new Cpermisos() { Modulo = modulo, Operaciones = tn.Value, Rowidmenu = tn.Parent.Value, Chequeado = tn.Checked });
                    }

                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }




        }

        private void Guardar()
        {

            try
            {

                DataView dvPerfiles = this.Session["dvPermisos"] as DataView;
                DataView dvUsuarios = this.Session["dvUsuarios"] as DataView;
                string perfil = this.Session["perfil"].ToString();
                bool vTransaccion = false, permitido = false;
                bool bexiste = false, bexisteusuario = false;
                string modulo = "", sitio = "", operacion = "";
                bool chequeado = false;
                var nodos = tvPermisos.Nodes;
                List<Cpermisos> listaHijos = new List<Cpermisos>();
                ListarHijos(nodos, listaHijos);

                object[] objValoresEliminar = new object[] { ddlEmpresa.SelectedValue, perfil };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("sPerfilPermisos", "elimina", "ppa", objValoresEliminar))
                {
                    case 1:
                        vTransaccion = true;
                        break;
                }

                for (int x = 0; x < listaHijos.Count; x++)
                {
                    if (listaHijos[x].Chequeado)
                    {
                        object[] objValores = new object[]{
                                 listaHijos[x].Chequeado,   // permitir 
                                 ddlEmpresa.SelectedValue,
                                 listaHijos[x].Rowidmenu.ToString(),  //menu 
                                 listaHijos[x].Operaciones.ToString(),  // operacion
                                  perfil , //perfil
                                  listaHijos[x].Modulo //sitio
                                };

                        switch (CentidadMetodos.EntidadInsertUpdateDelete("sPerfilPermisos", "inserta", "ppa", objValores))
                        {
                            case 1:
                                vTransaccion = true;
                                break;
                        }

                    }
                }


                dvUsuarios.RowFilter = "perfil='" + perfil.ToString() + "'";
                foreach (GridViewRow gvr in gvUsuarios.Rows)
                {
                    bexisteusuario = false;
                    var chkchequeado = gvr.FindControl("chkUsuarios") as CheckBox;

                    foreach (DataRowView drv in dvUsuarios)
                    {
                        if (drv.Row.ItemArray.GetValue(1).ToString() == gvr.Cells[1].Text.ToString())
                        {
                            bexisteusuario = true;
                        }
                    }

                    if (bexisteusuario & !chkchequeado.Checked)
                    {
                        object[] objValores = new object[] {
                          ddlEmpresa.SelectedValue.Trim(), // @empresa int
                           perfil,  //@perfil varchar
                           HttpUtility.HtmlDecode(gvr.Cells[1].Text)  //@usuario    varchar
            };

                        switch (CentidadMetodos.EntidadInsertUpdateDelete("sUsuarioPerfiles", "elimina", "ppa", objValores))
                        {
                            case 1:

                                vTransaccion = true;
                                break;
                        }
                    }

                    if (!bexisteusuario & chkchequeado.Checked)
                    {
                        object[] objValores = new object[] {
                                  true,  // activo   bit
                                    ddlEmpresa.SelectedValue,   //@empresa    int
                                   perfil, //@perfil varchar
                              HttpUtility.HtmlDecode(gvr.Cells[1].Text ) //@usuario    varchar
            };

                        switch (CentidadMetodos.EntidadInsertUpdateDelete("sUsuarioPerfiles", "inserta", "ppa", objValores))
                        {
                            case 1:

                                vTransaccion = true;
                                break;
                        }
                    }


                }


                if (vTransaccion == true)
                {
                    ManejoError("Error al momento de guardar los registros", "I");
                }
                else
                {
                    ManejoExito("Permisos asignados satisfactoriamente ", "I");
                    //ts.Complete();
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
                //}

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }

        }

        protected void chkMarcarTPermisos_CheckedChanged(object sender, EventArgs e)
        {

            foreach (TreeNode node1 in tvPermisos.Nodes)
            {
                node1.Checked = true;
                foreach (TreeNode node2 in node1.ChildNodes)
                {
                    node2.Checked = true;
                    foreach (TreeNode node3 in node2.ChildNodes)
                    {
                        node3.Checked = true;
                    }
                }
            }

            chkDesmarcarTPermisos.Checked = !chkMarcarTPermisos.Checked;
        }

        protected void chkDesmarcarTPermisos_CheckedChanged(object sender, EventArgs e)
        {
            checkchild(tvPermisos.Nodes, false);
            chkMarcarTPermisos.Checked = !chkMarcarTPermisos.Checked;
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


        protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session["dvPermisos"] = null;
            this.Session["dvUsuarios"] = null;
            this.Session["perfil"] = null;
            btnGuardar.Visible = true;
            tvPermisos.Nodes.Clear();
            cargarArbol();
            foreach (GridViewRow gvr in gvPerfiles.Rows)
            {
                var chkp = gvr.FindControl("chkPerfiles") as CheckBox;
                chkp.Checked = false;
            }

            foreach (GridViewRow gvr in gvUsuarios.Rows)
            {
                var chkp = gvr.FindControl("chkUsuarios") as CheckBox;
                chkp.Checked = false;
            }
        }

        protected void btnRefrescar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Session["dvPermisos"] = null;
                this.Session["dvUsuarios"] = null;
                this.Session["perfil"] = null;
                btnGuardar.Visible = true;
                tvPermisos.Nodes.Clear();
                cargarArbol();
                RefrescarPermisos();

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }



    }
}