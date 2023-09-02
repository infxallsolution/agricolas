using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Administracion.WebForms.App_Code.General
{
    public class CcontrolesUsuario
    {
        public CcontrolesUsuario()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        static public void HabilitarControles(ControlCollection controles)
        {
            foreach (Control parenControl in controles)
            {
                foreach (Control objControl in parenControl.Controls)
                {
                    if (objControl is TextBox)
                    {
                        ((TextBox)objControl).ReadOnly = false;
                        ((TextBox)objControl).Visible = true;
                        ((TextBox)objControl).Enabled = true;
                    }

                    if (objControl is DropDownList)
                    {
                        ((DropDownList)objControl).Enabled = true;
                        ((DropDownList)objControl).Visible = true;
                    }

                    if (objControl is CheckBox)
                    {
                        ((CheckBox)objControl).Enabled = true;
                        ((CheckBox)objControl).Visible = true;
                    }

                    if (objControl is RadioButton)
                    {
                        ((RadioButton)objControl).Enabled = true;
                        ((RadioButton)objControl).Visible = true;
                    }

                    if (objControl is Label)
                    {
                        ((Label)objControl).Visible = true;
                    }

                    if (objControl is ImageButton)
                    {
                        ((ImageButton)objControl).Visible = true;
                    }

                    if (objControl is Image)
                    {
                        ((Image)objControl).Visible = true;
                    }


                    if (objControl is Button)
                    {
                        ((Button)objControl).Visible = true;
                    }

                    if (objControl is LinkButton)
                    {
                        ((LinkButton)objControl).Visible = true;
                    }

                    if (objControl is FileUpload)
                    {
                        ((FileUpload)objControl).Visible = true;
                    }

                    if (objControl is RadioButtonList)
                    {
                        ((RadioButtonList)objControl).Enabled = true;
                        ((RadioButtonList)objControl).Visible = true;
                    }
                }
            }
        }

        static public void InhabilitarControles(ControlCollection controles)
        {
            foreach (Control parenControl in controles)
            {
                foreach (Control objControl in parenControl.Controls)
                {
                    if (objControl is TextBox)
                    {
                        if (!((TextBox)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((TextBox)objControl).ReadOnly = true;
                            ((TextBox)objControl).Visible = false;
                        }
                    }

                    if (objControl is DropDownList)
                    {
                        if (!((DropDownList)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((DropDownList)objControl).Enabled = false;
                            ((DropDownList)objControl).Visible = false;
                        }
                    }

                    if (objControl is CheckBox)
                    {
                        if (!((CheckBox)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((CheckBox)objControl).Enabled = false;
                            ((CheckBox)objControl).Visible = false;
                        }
                    }

                    if (objControl is RadioButton)
                    {
                        if (!((RadioButton)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((RadioButton)objControl).Enabled = false;
                            ((RadioButton)objControl).Visible = false;
                        }
                    }

                    if (objControl is RadioButtonList)
                    {
                        if (!((RadioButtonList)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((RadioButtonList)objControl).Enabled = false;
                            ((RadioButtonList)objControl).Visible = false;
                        }
                    }

                    if (objControl is Label)
                    {
                        if (!((Label)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((Label)objControl).Visible = false;
                        }
                    }

                    if (objControl is Button)
                    {
                        if (!((Button)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((Button)objControl).Visible = false;
                        }
                    }

                    if (objControl is ImageButton)
                    {
                        if (!((ImageButton)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((ImageButton)objControl).Visible = false;
                        }
                    }

                    if (objControl is Image)
                    {
                        if (!((Image)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((Image)objControl).Visible = false;
                        }
                    }

                    if (objControl is FileUpload)
                    {
                        if (!((FileUpload)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((FileUpload)objControl).Visible = false;
                        }
                    }

                    if (objControl is LinkButton)
                    {
                        if (!((LinkButton)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((LinkButton)objControl).Visible = false;
                        }
                    }
                }
            }
        }

        static public void LimpiarControles(ControlCollection controles)
        {
            foreach (Control parentControl in controles)
            {
                foreach (Control objControl in parentControl.Controls)
                {
                    if (objControl is TextBox)
                    {
                        ((TextBox)objControl).Text = "";
                    }

                    if (objControl is CheckBox)
                    {
                        ((CheckBox)objControl).Checked = false;
                    }
                }
            }
        }

        static public void OpcionesDefault(ControlCollection controles, int estado)
        {
            switch (estado)
            {
                case 0:
                    foreach (Control parentControl in controles)
                    {
                        foreach (Control objControl in parentControl.Controls)
                        {
                            if (objControl is ImageButton)
                            {
                                if (((ImageButton)objControl).ID == "imbNuevo")
                                {
                                    ((ImageButton)objControl).Enabled = true;
                                    ((ImageButton)objControl).Visible = true;
                                }

                                if (((ImageButton)objControl).ID == "imbGuradar")
                                {
                                    ((ImageButton)objControl).Enabled = false;
                                    ((ImageButton)objControl).Visible = false;
                                }

                                if (((ImageButton)objControl).ID == "imbEliminar")
                                {
                                    ((ImageButton)objControl).Enabled = false;
                                    ((ImageButton)objControl).Visible = false;
                                }

                                if (((ImageButton)objControl).ID == "imbCancelar")
                                {
                                    ((ImageButton)objControl).Enabled = false;
                                    ((ImageButton)objControl).Visible = false;
                                }
                            }

                            if (objControl is Label)
                            {
                                if (((Label)objControl).ID == "lblMensaje")
                                {
                                    ((Label)objControl).Text = "--";
                                }
                            }
                        }
                    }
                    break;

                case 1:
                    foreach (Control parentControl in controles)
                    {
                        foreach (Control objControl in parentControl.Controls)
                        {
                            if (objControl is ImageButton)
                            {
                                if (((ImageButton)objControl).ID == "imbNuevo")
                                {
                                    ((ImageButton)objControl).Enabled = false;
                                    ((ImageButton)objControl).Visible = false;
                                }

                                if (((ImageButton)objControl).ID == "imbGuradar")
                                {
                                    ((ImageButton)objControl).Enabled = true;
                                    ((ImageButton)objControl).Visible = true;
                                }

                                if (((ImageButton)objControl).ID == "imbEliminar")
                                {
                                    ((ImageButton)objControl).Enabled = false;
                                    ((ImageButton)objControl).Visible = false;
                                }

                                if (((ImageButton)objControl).ID == "imbCancelar")
                                {
                                    ((ImageButton)objControl).Enabled = true;
                                    ((ImageButton)objControl).Visible = true;
                                }
                            }
                        }
                    }
                    break;

                case 2:
                    foreach (Control parentControl in controles)
                    {
                        foreach (Control objControl in parentControl.Controls)
                        {
                            if (objControl is ImageButton)
                            {
                                if (((ImageButton)objControl).ID == "imbNuevo")
                                {
                                    ((ImageButton)objControl).Enabled = false;
                                    ((ImageButton)objControl).Visible = false;
                                }

                                if (((ImageButton)objControl).ID == "imbGuradar")
                                {
                                    ((ImageButton)objControl).Enabled = true;
                                    ((ImageButton)objControl).Visible = true;
                                }

                                if (((ImageButton)objControl).ID == "imbEliminar")
                                {
                                    ((ImageButton)objControl).Enabled = true;
                                    ((ImageButton)objControl).Visible = true;
                                }

                                if (((ImageButton)objControl).ID == "imbCancelar")
                                {
                                    ((ImageButton)objControl).Enabled = true;
                                    ((ImageButton)objControl).Visible = true;
                                }
                            }
                        }
                    }
                    break;
            }
        }

        static private TreeNode CreaNodo(string id, string texto)
        {
            TreeNode nodo = new TreeNode();
            nodo.Text = texto;
            nodo.Value = id;
            nodo.PopulateOnDemand = true;
            nodo.ShowCheckBox = true;
            return nodo;
        }

        static private TreeNode CreaNodoHijo(string id, string texto)
        {
            TreeNode nodo = new TreeNode();
            nodo.Text = texto;
            nodo.Value = id;
            nodo.PopulateOnDemand = true;
            nodo.ShowCheckBox = true;

            return nodo;
        }

        static private TreeNode CreaNodoNoDemanda(string id, string texto)
        {
            TreeNode nodo = new TreeNode();
            nodo.Text = texto;
            nodo.Value = id;
            nodo.ShowCheckBox = true;
            return nodo;
        }

        static public void CreaNodoRaiz(DataSet dsDatos, string id, string texto, TreeView arbol)
        {


            foreach (DataRow registro in dsDatos.Tables[0].Rows)
            {
                arbol.Nodes.Add(
                    CreaNodo(
                        registro[id].ToString(),
                        registro[texto].ToString()));
            }
            arbol.DataBind();
        }

        static public void CreaNodoRaizNoDemanda(DataSet dsDatos, string id, string texto, TreeView arbol)
        {
            foreach (DataRow registro in dsDatos.Tables[0].Rows)
            {
                arbol.Nodes.Add(
                    CreaNodoNoDemanda(
                        registro[id].ToString(),
                        registro[texto].ToString()));

                arbol.DataBind();
            }
        }

        static public void CreaNodoHijo(DataSet dsDatos, string id, string texto, TreeView arbol, TreeNode nodoPadre)
        {
            foreach (DataRow registro in dsDatos.Tables[0].Rows)
            {
                nodoPadre.ChildNodes.Add(
                    CreaNodoHijo(registro[id].ToString(), registro[texto].ToString()));
            }
            arbol.DataBind();
        }

        static public void CreaNodoHijoNoDemanda(DataSet dsDatos, string id, string texto, TreeView arbol, TreeNode nodoPadre)
        {
            foreach (DataRow registro in dsDatos.Tables[0].Rows)
            {
                nodoPadre.ChildNodes.Add(
                    CreaNodoNoDemanda(
                        registro[id].ToString(),
                        registro[texto].ToString()));

                arbol.DataBind();
            }
        }

        static public DataView OrdenarEntidad(DataView entidad, string campoOrden)
        {
            entidad.Sort = campoOrden;

            return entidad;
        }

        static public DataView OrdenarEntidad(DataSet entidad, string campoOrden, int empresa)
        {
            DataView dvEntidad = entidad.Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa);
            dvEntidad.Sort = campoOrden;

            return dvEntidad;
        }

        static public DataView OrdenarEntidadSinEmpresa(DataSet entidad, string campoOrden)
        {
            DataView dvEntidad = entidad.Tables[0].DefaultView;
            dvEntidad.Sort = campoOrden;

            return dvEntidad;
        }

        static public DataView OrdenarEntidadSinEmpresayActivo(DataSet entidad, string campoOrden)
        {
            DataView dvEntidad = entidad.Tables[0].DefaultView;
            dvEntidad.RowFilter = "activo=True";
            dvEntidad.Sort = campoOrden;

            return dvEntidad;
        }

        static public DataView OrdenarEntidadyActivos(DataSet entidad, string campoOrden, int empresa)
        {
            DataView dvEntidad = entidad.Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and activo=True";
            dvEntidad.Sort = campoOrden;
            return dvEntidad;
        }

        static public DataView OrdenarEntidadTercero(DataSet entidad, string campoOrden, string filtro, int empresa)
        {
            DataView dvEntidad = entidad.Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and activo=True and " + filtro + "=True";

            dvEntidad.Sort = campoOrden;
            return dvEntidad;
        }

        //static public object[] CampoTransaccion(string tipoTransaccion, string entidad, string campo)
        //{
        //    string[] iParametros = new string[] { "@tipoTransaccion", "@entidad", "@campo" };
        //    object[] objValores = new object[] { tipoTransaccion, entidad, campo };
        //    object[] resultado = new object[7];

        //    foreach (DataRowView registro in Cacceso.DataSetParametros(
        //        "spSeleccionaCampoTipoEntidad",
        //        iParametros,
        //        objValores,
        //        "ppa").Tables[0].DefaultView)
        //    {
        //        resultado.SetValue(registro.Row.ItemArray.GetValue(0), 0);
        //        resultado.SetValue(registro.Row.ItemArray.GetValue(1), 1);
        //        resultado.SetValue(registro.Row.ItemArray.GetValue(2), 2);
        //        resultado.SetValue(registro.Row.ItemArray.GetValue(3), 3);
        //        resultado.SetValue(registro.Row.ItemArray.GetValue(4), 4);
        //        resultado.SetValue(registro.Row.ItemArray.GetValue(5), 5);
        //        resultado.SetValue(registro.Row.ItemArray.GetValue(6), 6);
        //    }

        //    return resultado;
        //}

        //static public string TipoTransaccionConfig(string tipoTransaccion)
        //{
        //    string retorno = "";
        //    object[] objKey = new object[] { tipoTransaccion };

        //    foreach (DataRowView registro in CentidadMetodos.EntidadGetKey(
        //        "gTipoTransaccionConfig",
        //        "ppa",
        //        objKey).Tables[0].DefaultView)
        //    {
        //        for (int i = 1; i < registro.Row.ItemArray.Length; i++)
        //        {
        //            retorno = retorno + registro.Row.ItemArray.GetValue(i).ToString() + "*";
        //        }
        //    }

        //    return retorno;
        //}

        //static public void ComportamientoCampoEntidad(ControlCollection controles, string entidad, string TipoTransaccion, LinkButton fecha)
        //{
        //    InhabilitarControles(
        //        controles);

        //    foreach (Control parentControl in controles)
        //    {
        //        foreach (Control objControl in parentControl.Controls)
        //        {
        //            if (objControl.ID != null)
        //            {
        //                object[] objCampo = CcontrolesUsuario.CampoTransaccion(
        //                    TipoTransaccion,
        //                    entidad,
        //                    objControl.ID.ToString());

        //                if (Convert.ToString(objCampo.GetValue(0)).Trim().Length != 0)
        //                {
        //                    objControl.Visible = true;

        //                    if (objControl is System.Web.UI.WebControls.Calendar)
        //                    {
        //                        fecha.Visible = true;
        //                    }
        //                    else
        //                    {
        //                        if (objControl is CheckBox)
        //                        {
        //                            ((CheckBox)objControl).Checked = false;
        //                            ((CheckBox)objControl).Enabled = true;
        //                        }
        //                        else
        //                        {
        //                            if (objControl is TextBox)
        //                            {
        //                                ((TextBox)objControl).Enabled = true;
        //                                ((TextBox)objControl).ReadOnly = false;

        //                                ((Label)parentControl.FindControl(
        //                                    Convert.ToString(objControl.ID).Trim().Replace(
        //                                        Convert.ToString(objControl.ID).Substring(0, 3),
        //                                        "lbl"))).Visible = true;
        //                            }

        //                            if (objControl is DropDownList)
        //                            {
        //                                ((DropDownList)objControl).Enabled = true;

        //                                ((Label)parentControl.FindControl(
        //                                    Convert.ToString(objControl.ID).Trim().Replace(
        //                                        Convert.ToString(objControl.ID).Substring(0, 3),
        //                                        "lbl"))).Visible = true;
        //                            }
        //                            else
        //                            {
        //                                ((Label)parentControl.FindControl(
        //                                    Convert.ToString(objControl.ID).Trim().Replace(
        //                                        Convert.ToString(objControl.ID).Substring(0, 3),
        //                                        "lbl"))).Visible = true;
        //                            }
        //                        }
        //                    }

        //                    if (Convert.ToInt16(objCampo.GetValue(1)) == 1)
        //                    {
        //                        if (Convert.ToInt16(objCampo.GetValue(2)) == 1)
        //                        {
        //                            ((DropDownList)objControl).DataSource = CcontrolesUsuario.OrdenarEntidad(
        //                                CentidadMetodos.EntidadGet("logCliente", "ppa"),
        //                                "descripcion");
        //                            ((DropDownList)objControl).DataValueField = "codigo";
        //                            ((DropDownList)objControl).DataTextField = "descripcion";
        //                            ((DropDownList)objControl).DataBind();
        //                            ((DropDownList)objControl).Items.Insert(0, new ListItem("Seleccione una opción", ""));

        //                            ((Label)parentControl.FindControl(
        //                                Convert.ToString(objCampo.GetValue(0)).Trim().Replace(
        //                                    Convert.ToString(objCampo.GetValue(6)),
        //                                    "lbl"))).Text = "Cliente";
        //                        }

        //                        if (Convert.ToInt16(objCampo.GetValue(3)) == 1)
        //                        {
        //                            ((DropDownList)objControl).DataSource = CcontrolesUsuario.OrdenarEntidad(
        //                                CentidadMetodos.EntidadGet("cxpProveedor", "ppa"),
        //                                "cadena");
        //                            ((DropDownList)objControl).DataValueField = "codigo";
        //                            ((DropDownList)objControl).DataTextField = "cadena";
        //                            ((DropDownList)objControl).DataBind();
        //                            ((DropDownList)objControl).Items.Insert(0, new ListItem("Seleccione una opción", ""));

        //                            ((Label)parentControl.FindControl(
        //                                Convert.ToString(objCampo.GetValue(0)).Trim().Replace(
        //                                    Convert.ToString(objCampo.GetValue(6)),
        //                                    "lbl"))).Text = "Proveedor";
        //                        }

        //                        if (Convert.ToInt16(objCampo.GetValue(4)) == 1)
        //                        {
        //                            ((DropDownList)objControl).DataSource = CcontrolesUsuario.OrdenarEntidad(
        //                                CentidadMetodos.EntidadGet("cTerceros", "ppa"),
        //                                "descripcion");
        //                            ((DropDownList)objControl).DataValueField = "codigo";
        //                            ((DropDownList)objControl).DataTextField = "descripcion";
        //                            ((DropDownList)objControl).DataBind();
        //                            ((DropDownList)objControl).Items.Insert(0, new ListItem("Seleccione una opción", ""));

        //                            ((Label)parentControl.FindControl(
        //                                Convert.ToString(objCampo.GetValue(0)).Trim().Replace(
        //                                    Convert.ToString(objCampo.GetValue(6)),
        //                                    "lbl"))).Text = "Tercero";
        //                        }

        //                        if (Convert.ToInt16(objCampo.GetValue(5)) == 1)
        //                        {
        //                            ((DropDownList)objControl).Enabled = false;
        //                        }
        //                        else
        //                        {
        //                            ((DropDownList)objControl).Enabled = true;
        //                        }

        //                        ((DropDownList)objControl).SelectedValue = "";
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    }
}