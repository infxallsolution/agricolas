using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Almacen.WebForms.App_Code.General
{
    public class CcontrolesUsuario
    {
        public CcontrolesUsuario()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        static public DataView OrdenarEntidadSinEmpresa(DataSet entidad, string campoOrden)
        {
            DataView dvEntidad = entidad.Tables[0].DefaultView;
            dvEntidad.Sort = campoOrden;

            return dvEntidad;
        }

        static public bool VerificaCamposRequeridos(ControlCollection controles)
        {
            foreach (Control objParentControl in controles)
            {
                foreach (Control objControl in objParentControl.Controls)
                {
                    if (objControl.Visible == true)
                    {
                        if (objControl is TextBox)
                        {
                            if (((TextBox)objControl).Text.Trim().Length == 0 && ((TextBox)objControl).Enabled == true && ((TextBox)objControl).Visible == true)
                            {
                                return false;
                            }
                        }

                        if (objControl is DropDownList)
                        {
                            if (Convert.ToString(((DropDownList)objControl).SelectedValue).Trim().Length == 0 && ((DropDownList)objControl).Enabled == true && ((DropDownList)objControl).Visible == true)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        static public void MensajeError(string mensaje, Label etiqueta)
        {
            etiqueta.Visible = true;
            etiqueta.ForeColor = System.Drawing.Color.Red;
            etiqueta.Text = mensaje;
        }

        static public void LimpiarCombos(ControlCollection controles)
        {
            foreach (Control objParentControl in controles)
            {
                foreach (Control objControl in objParentControl.Controls)
                {
                    if (objControl is DropDownList)
                    {
                        ((DropDownList)objControl).SelectedValue = "";
                    }
                }
            }
        }

        static public int IndiceControlGrilla(string control, string idCliente)
        {
            string indice = idCliente;

            if (idCliente.Contains("ctl"))
            {
                indice = indice.Substring(indice.IndexOf("ctl")).Replace("_" + control, "").Remove(0, 3);

                return (Convert.ToInt32(indice) - 2);
            }
            else
            {
                return 0;
            }
        }

        static public void HabilitarControles(ControlCollection controles)
        {
            foreach (Control parenControl in controles)
            {
                if (parenControl is TextBox)
                {
                    ((TextBox)parenControl).ReadOnly = false;
                    ((TextBox)parenControl).Visible = true;
                    ((TextBox)parenControl).Enabled = true;
                }
                if (parenControl is DropDownList)
                {
                    ((DropDownList)parenControl).Enabled = true;
                    ((DropDownList)parenControl).Visible = true;
                }
                if (parenControl is CheckBox)
                {
                    ((CheckBox)parenControl).Enabled = true;
                    ((CheckBox)parenControl).Visible = true;
                }
                if (parenControl is RadioButton)
                {
                    ((RadioButton)parenControl).Enabled = true;
                    ((RadioButton)parenControl).Visible = true;
                }
                if (parenControl is RadioButtonList)
                {
                    ((RadioButtonList)parenControl).Enabled = true;
                    ((RadioButtonList)parenControl).Visible = true;
                }
                if (parenControl is Label)
                {
                    ((Label)parenControl).Visible = true;
                }
                if (parenControl is ImageButton)
                {
                    ((ImageButton)parenControl).Visible = true;
                }
                if (parenControl is Image)
                {
                    ((Image)parenControl).Visible = true;
                }
                if (parenControl is Button)
                {
                    ((Button)parenControl).Visible = true;
                    ((Button)parenControl).Enabled = true;
                }
                if (parenControl is LinkButton)
                {
                    ((LinkButton)parenControl).Visible = true;
                }
                if (parenControl is FileUpload)
                {
                    ((FileUpload)parenControl).Visible = true;
                }

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
                    if (objControl is RadioButtonList)
                    {
                        ((RadioButtonList)objControl).Enabled = true;
                        ((RadioButtonList)objControl).Visible = true;
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
                        ((Button)objControl).Enabled = true;
                    }
                    if (objControl is LinkButton)
                    {
                        ((LinkButton)objControl).Visible = true;
                    }
                    if (objControl is FileUpload)
                    {
                        ((FileUpload)objControl).Visible = true;
                    }

                    foreach (Control objControlD in objControl.Controls)
                    {
                        if (objControlD is TextBox)
                        {
                            ((TextBox)objControlD).ReadOnly = false;
                            ((TextBox)objControlD).Visible = true;
                            ((TextBox)objControlD).Enabled = true;
                        }
                        if (objControlD is DropDownList)
                        {
                            ((DropDownList)objControlD).Enabled = true;
                            ((DropDownList)objControlD).Visible = true;
                        }
                        if (objControlD is CheckBox)
                        {
                            ((CheckBox)objControlD).Enabled = true;
                            ((CheckBox)objControlD).Visible = true;
                        }
                        if (objControlD is RadioButton)
                        {
                            ((RadioButton)objControlD).Enabled = true;
                            ((RadioButton)objControlD).Visible = true;
                        }
                        if (objControlD is RadioButtonList)
                        {
                            ((RadioButtonList)objControlD).Enabled = true;
                            ((RadioButtonList)objControlD).Visible = true;
                        }
                        if (objControlD is Label)
                            ((Label)objControlD).Visible = true;
                        if (objControlD is ImageButton)
                            ((ImageButton)objControlD).Visible = true;
                        if (objControlD is Image)
                            ((Image)objControlD).Visible = true;
                        if (objControlD is Button)
                            ((Button)objControlD).Visible = true;
                        if (objControlD is LinkButton)
                            ((LinkButton)objControlD).Visible = true;
                        if (objControlD is FileUpload)
                            ((FileUpload)objControlD).Visible = true;
                        if (objControlD is CheckBoxList)
                            ((CheckBoxList)objControlD).Visible = true;
                    }
                }
            }
        }


        static public void InhabilitarUsoControles(ControlCollection controles)
        {
            foreach (Control parenControl in controles)
            {
                foreach (Control objControl in parenControl.Controls)
                {
                    if (objControl is TextBox)
                    {
                        if (!((TextBox)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((TextBox)objControl).Enabled = false;

                        }
                    }

                    if (objControl is DropDownList)
                    {
                        if (!((DropDownList)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((DropDownList)objControl).Enabled = false;

                        }
                    }

                    if (objControl is CheckBox)
                    {
                        if (!((CheckBox)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((CheckBox)objControl).Enabled = false;

                        }
                    }

                    if (objControl is RadioButton)
                    {
                        if (!((RadioButton)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((RadioButton)objControl).Enabled = false;

                        }
                    }

                    if (objControl is RadioButtonList)
                    {
                        if (!((RadioButtonList)objControl).ID.ToString().StartsWith("ni"))
                        {

                            ((RadioButtonList)objControl).Enabled = false;

                        }
                    }


                    if (objControl is Label)
                    {
                        if (!((Label)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((Label)objControl).Enabled = false;
                        }
                    }

                    if (objControl is Button)
                    {
                        if (!((Button)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((Button)objControl).Enabled = false;
                        }
                    }

                    if (objControl is ImageButton)
                    {
                        if (!((ImageButton)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((ImageButton)objControl).Enabled = false;
                        }
                    }

                    if (objControl is Image)
                    {
                        if (!((Image)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((Image)objControl).Enabled = false;
                        }
                    }

                    if (objControl is FileUpload)
                    {
                        if (!((FileUpload)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((FileUpload)objControl).Enabled = false;
                        }
                    }

                    if (objControl is LinkButton)
                    {
                        if (!((LinkButton)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((LinkButton)objControl).Enabled = false;
                        }
                    }
                }
            }
        }
        static public void HabilitarUsoControles(ControlCollection controles)
        {
            foreach (Control parenControl in controles)
            {
                foreach (Control objControl in parenControl.Controls)
                {
                    if (objControl is TextBox)
                    {
                        if (!((TextBox)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((TextBox)objControl).Enabled = true;

                        }
                    }

                    if (objControl is DropDownList)
                    {
                        if (!((DropDownList)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((DropDownList)objControl).Enabled = true;

                        }
                    }

                    if (objControl is CheckBox)
                    {
                        if (!((CheckBox)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((CheckBox)objControl).Enabled = true;

                        }
                    }

                    if (objControl is RadioButton)
                    {
                        if (!((RadioButton)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((RadioButton)objControl).Enabled = true;

                        }
                    }

                    if (objControl is RadioButtonList)
                    {
                        if (!((RadioButtonList)objControl).ID.ToString().StartsWith("ni"))
                        {

                            ((RadioButtonList)objControl).Enabled = true;

                        }
                    }


                    if (objControl is Label)
                    {
                        if (!((Label)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((Label)objControl).Enabled = true;
                        }
                    }

                    if (objControl is Button)
                    {
                        if (!((Button)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((Button)objControl).Enabled = true;
                        }
                    }

                    if (objControl is ImageButton)
                    {
                        if (!((ImageButton)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((ImageButton)objControl).Enabled = true;
                        }
                    }

                    if (objControl is Image)
                    {
                        if (!((Image)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((Image)objControl).Enabled = true;
                        }
                    }

                    if (objControl is FileUpload)
                    {
                        if (!((FileUpload)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((FileUpload)objControl).Enabled = true;
                        }
                    }

                    if (objControl is LinkButton)
                    {
                        if (!((LinkButton)objControl).ID.ToString().StartsWith("ni"))
                        {
                            ((LinkButton)objControl).Enabled = true;
                        }
                    }
                }
            }
        }

        static public void InhabilitarControlesEnable(ControlCollection controles)
        {
            foreach (Control objControl in controles)
            {
                if (objControl is TextBox)
                {
                    if (!((TextBox)objControl).ID.ToString().StartsWith("ni"))
                    {
                        ((TextBox)objControl).Enabled = false;
                    }
                }

                if (objControl is DropDownList)
                {
                    if (!((DropDownList)objControl).ID.ToString().StartsWith("ni"))
                    {
                        ((DropDownList)objControl).Enabled = false;
                    }
                }

                if (objControl is CheckBox)
                {
                    if (!((CheckBox)objControl).ID.ToString().StartsWith("ni"))
                    {
                        ((CheckBox)objControl).Enabled = false;
                    }
                }

                if (objControl is RadioButton)
                {
                    if (!((RadioButton)objControl).ID.ToString().StartsWith("ni"))
                    {
                        ((RadioButton)objControl).Enabled = false;
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

                if (objControl is Button)
                {
                    if (!((Button)objControl).ID.ToString().StartsWith("ni"))
                    {
                        ((Button)objControl).Enabled = false;
                    }
                }

                if (objControl is ImageButton)
                {
                    if (!((ImageButton)objControl).ID.ToString().StartsWith("ni"))
                    {
                        ((ImageButton)objControl).Enabled = false;
                    }
                }

                if (objControl is Image)
                {
                    if (!((Image)objControl).ID.ToString().StartsWith("ni"))
                    {
                        ((Image)objControl).Enabled = false;
                    }
                }

                if (objControl is FileUpload)
                {
                    if (!((FileUpload)objControl).ID.ToString().StartsWith("ni"))
                    {
                        ((FileUpload)objControl).Enabled = false;
                    }
                }

                if (objControl is LinkButton)
                {
                    if (!((LinkButton)objControl).ID.ToString().StartsWith("ni"))
                    {
                        ((LinkButton)objControl).Enabled = false;
                    }
                }
                if (objControl.Controls.Count >= 0)
                    InhabilitarControlesEnable(objControl.Controls);
            }
        }

        static public void InhabilitarControles(ControlCollection controles)
        {
            try
            {
                foreach (Control objControl in controles)
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

                    //if (objControl is LinkButton)
                    //{
                    //    if (!((LinkButton)objControl).ID.ToString().StartsWith("ni"))
                    //    {
                    //        ((LinkButton)objControl).Visible = false;
                    //    }
                    //}

                    if (objControl.Controls.Count >= 0)
                        InhabilitarControles(objControl.Controls);
                }
            }
            catch (Exception ex)
            {
                //CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }

        }

        static public void LimpiarControles(ControlCollection controles)
        {
            foreach (Control parentControl in controles)
            {
                if (parentControl is TextBox)
                {
                    if (((TextBox)parentControl).ID.StartsWith("txv"))
                        ((TextBox)parentControl).Text = "0";
                    else
                        ((TextBox)parentControl).Text = "";
                }

                if (parentControl is DropDownList)
                    ((DropDownList)parentControl).ClearSelection();

                if (parentControl is CheckBox)
                    ((CheckBox)parentControl).Checked = false;

                if (parentControl.Controls.Count >= 0)
                    LimpiarControles(parentControl.Controls);
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

                arbol.DataBind();
            }
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

                arbol.DataBind();
            }
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

        static public DataView OrdenarEntidadFac(DataSet entidad, string campoOrden, int empresa)
        {
            DataView dvEntidad = entidad.Tables[0].DefaultView;
            dvEntidad.RowFilter = "compañia=" + Convert.ToString(empresa);
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

        static public DataView OrdenarEntidadyActivosFac(DataSet entidad, string campoOrden, int empresa)
        {
            DataView dvEntidad = entidad.Tables[0].DefaultView;
            dvEntidad.RowFilter = "compañia=" + Convert.ToString(empresa) + " and activo=True";
            dvEntidad.Sort = campoOrden;
            return dvEntidad;
        }

        static public DataView OrdenarEntidadTercero(DataSet entidad, string campoOrden, string filtro, int empresa)
        {
            DataView dvEntidad = entidad.Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and " + filtro + "=True";

            dvEntidad.Sort = campoOrden;
            return dvEntidad;
        }

        static public object[] CampoTransaccion(string tipoTransaccion, string entidad, string campo, int empresa)
        {


            string[] iParametros = new string[] { "@tipoTransaccion", "@entidad", "@campo", "@empresa" };
            object[] objValores = new object[] { tipoTransaccion, entidad, campo, empresa };
            object[] resultado = new object[7];

            foreach (DataRowView registro in Cacceso.DataSetParametros(
                "spSeleccionaCampoTipoEntidad",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView)
            {
                resultado.SetValue(registro.Row.ItemArray.GetValue(0), 0);
                resultado.SetValue(registro.Row.ItemArray.GetValue(1), 1);
                resultado.SetValue(registro.Row.ItemArray.GetValue(2), 2);
                resultado.SetValue(registro.Row.ItemArray.GetValue(3), 3);
                resultado.SetValue(registro.Row.ItemArray.GetValue(4), 4);
                resultado.SetValue(registro.Row.ItemArray.GetValue(5), 5);
                resultado.SetValue(registro.Row.ItemArray.GetValue(6), 6);
            }

            return resultado;
        }

        static public string TipoTransaccionConfig(string tipoTransaccion, int empresa)
        {


            string retorno = "";
            //object[] objKey = new object[] { empresa, tipoTransaccion };

            //foreach (DataRowView registro in CentidadMetodos.EntidadGetKey("gTipoTransaccionConfig", "ppa",
            //    objKey).Tables[0].DefaultView)
            //{
            //    for (int i = 2; i < registro.Row.ItemArray.Length; i++)
            //    {
            //        retorno = retorno + registro.Row.ItemArray.GetValue(i).ToString() + "*";
            //    }
            //}

            return retorno;
        }

        static public void ComportamientoCampoEntidad(ControlCollection controles, string entidad, string TipoTransaccion, int empresa)
        {

            InhabilitarControles(controles);

            foreach (Control parentControl in controles)
            {
                if (parentControl.Controls.Count > 0)
                {
                    foreach (Control objControl in parentControl.Controls)
                    {
                        if (objControl.ID != null)
                        {
                            object[] objCampo = CcontrolesUsuario.CampoTransaccion(TipoTransaccion, entidad, objControl.ID.ToString(), empresa);

                            if (Convert.ToString(objCampo.GetValue(0)).Trim().Length != 0)
                            {
                                objControl.Visible = true;

                                if (objControl is CheckBox)
                                {
                                    ((CheckBox)objControl).Checked = false;
                                    ((CheckBox)objControl).Enabled = true;
                                    ((CheckBox)objControl).Visible = true;
                                }
                                else if (objControl is TextBox)
                                {
                                    ((TextBox)objControl).Enabled = true;
                                    ((TextBox)objControl).ReadOnly = false;
                                    ((TextBox)objControl).Visible = true;

                                    if (((Label)parentControl.FindControl(Convert.ToString(objControl.ID).Trim().Replace(Convert.ToString(objControl.ID).Substring(0, 3), "lbl")) != null))
                                        ((Label)parentControl.FindControl(Convert.ToString(objControl.ID).Trim().Replace(Convert.ToString(objControl.ID).Substring(0, 3), "lbl"))).Visible = true;
                                    else
                                    {
                                        if (((LinkButton)parentControl.FindControl(Convert.ToString(objControl.ID).Trim().Replace(Convert.ToString(objControl.ID).Substring(0, 3), "lb"))) != null)
                                            ((LinkButton)parentControl.FindControl(Convert.ToString(objControl.ID).Trim().Replace(Convert.ToString(objControl.ID).Substring(0, 3), "lb"))).Visible = true;
                                    }
                                }
                                else if (objControl is DropDownList)
                                {
                                    ((DropDownList)objControl).Enabled = true;
                                    ((DropDownList)objControl).Visible = true;
                                    if (((Label)parentControl.FindControl(Convert.ToString(objControl.ID).Trim().Replace(Convert.ToString(objControl.ID).Substring(0, 3), "lbl"))) != null)
                                        ((Label)parentControl.FindControl(Convert.ToString(objControl.ID).Trim().Replace(Convert.ToString(objControl.ID).Substring(0, 3), "lbl"))).Visible = true;
                                }
                            }


                            if (Convert.ToInt16(objCampo.GetValue(1)) == 1)
                            {
                                DataView dvTercero = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cTercero", "ppa"), "descripcion", empresa);

                                if (Convert.ToInt16(objCampo.GetValue(2)) == 1)
                                {
                                    dvTercero.RowFilter = "empresa=" + empresa.ToString() + " and cliente=1 and activo=1";
                                    ((DropDownList)objControl).DataSource = dvTercero;
                                    ((DropDownList)objControl).DataValueField = "id";
                                    ((DropDownList)objControl).DataTextField = "descripcion";
                                    ((DropDownList)objControl).DataBind();
                                    ((DropDownList)objControl).Items.Insert(0, new ListItem("", ""));
                                    ((Label)parentControl.FindControl(Convert.ToString(objCampo.GetValue(0)).Trim().Replace(Convert.ToString(objCampo.GetValue(6)), "lbl"))).Text = "Cliente";
                                }

                                if (Convert.ToInt16(objCampo.GetValue(3)) == 1)
                                {
                                    dvTercero.RowFilter = "empresa=" + empresa.ToString() + " and proveedor=1 and activo=1";
                                    ((DropDownList)objControl).DataSource = dvTercero;
                                    ((DropDownList)objControl).DataValueField = "id";
                                    ((DropDownList)objControl).DataTextField = "descripcion";
                                    ((DropDownList)objControl).DataBind();
                                    ((DropDownList)objControl).Items.Insert(0, new ListItem("", ""));
                                    ((Label)parentControl.FindControl(Convert.ToString(objCampo.GetValue(0)).Trim().Replace(Convert.ToString(objCampo.GetValue(6)), "lbl"))).Text = "Proveedor";
                                }

                                if (Convert.ToInt16(objCampo.GetValue(4)) == 1)
                                {
                                    dvTercero.RowFilter = "empresa=" + empresa.ToString() + " and funcionario=1 and activo=1";
                                    ((DropDownList)objControl).DataSource = dvTercero;
                                    ((DropDownList)objControl).DataValueField = "id";
                                    ((DropDownList)objControl).DataTextField = "descripcion";
                                    ((DropDownList)objControl).DataBind();
                                    ((DropDownList)objControl).Items.Insert(0, new ListItem("", ""));
                                    ((Label)parentControl.FindControl(Convert.ToString(objCampo.GetValue(0)).Trim().Replace(Convert.ToString(objCampo.GetValue(6)), "lbl"))).Text = "Tercero";
                                }

                                if (Convert.ToInt16(objCampo.GetValue(5)) == 1)
                                    ((DropDownList)objControl).Enabled = false;
                                else
                                    ((DropDownList)objControl).Enabled = true;
                                ((DropDownList)objControl).SelectedValue = "";
                            }
                        }
                    }
                }
                else
                {
                    if (parentControl.ID != null)
                    {
                        object[] objCampo = CcontrolesUsuario.CampoTransaccion(TipoTransaccion, entidad, parentControl.ID.ToString(), empresa);

                        if (Convert.ToString(objCampo.GetValue(0)).Trim().Length != 0)
                        {
                            parentControl.Visible = true;

                            if (parentControl is CheckBox)
                            {
                                ((CheckBox)parentControl).Checked = false;
                                ((CheckBox)parentControl).Enabled = true;
                                ((CheckBox)parentControl).Visible = true;
                            }
                            else if (parentControl is TextBox)
                            {
                                ((TextBox)parentControl).Enabled = true;
                                ((TextBox)parentControl).ReadOnly = false;
                                ((TextBox)parentControl).Visible = true;

                                if (((Label)parentControl.FindControl(Convert.ToString(parentControl.ID).Trim().Replace(Convert.ToString(parentControl.ID).Substring(0, 3), "lbl")) != null))
                                    ((Label)parentControl.FindControl(Convert.ToString(parentControl.ID).Trim().Replace(Convert.ToString(parentControl.ID).Substring(0, 3), "lbl"))).Visible = true;
                                else
                                {
                                    if (((LinkButton)parentControl.FindControl(Convert.ToString(parentControl.ID).Trim().Replace(Convert.ToString(parentControl.ID).Substring(0, 3), "lb"))) != null)
                                        ((LinkButton)parentControl.FindControl(Convert.ToString(parentControl.ID).Trim().Replace(Convert.ToString(parentControl.ID).Substring(0, 3), "lb"))).Visible = true;
                                }
                            }
                            else if (parentControl is DropDownList)
                            {
                                ((DropDownList)parentControl).Enabled = true;
                                ((DropDownList)parentControl).Visible = true;
                                if (((Label)parentControl.FindControl(Convert.ToString(parentControl.ID).Trim().Replace(Convert.ToString(parentControl.ID).Substring(0, 3), "lbl"))) != null)
                                    ((Label)parentControl.FindControl(Convert.ToString(parentControl.ID).Trim().Replace(Convert.ToString(parentControl.ID).Substring(0, 3), "lbl"))).Visible = true;
                            }
                        }


                        if (Convert.ToInt16(objCampo.GetValue(1)) == 1)
                        {
                            DataView dvTercero = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cTercero", "ppa"), "descripcion", empresa);

                            if (Convert.ToInt16(objCampo.GetValue(2)) == 1)
                            {
                                dvTercero.RowFilter = "empresa=" + empresa.ToString() + " and cliente=1 and activo=1";
                                ((DropDownList)parentControl).DataSource = dvTercero;
                                ((DropDownList)parentControl).DataValueField = "id";
                                ((DropDownList)parentControl).DataTextField = "descripcion";
                                ((DropDownList)parentControl).DataBind();
                                ((DropDownList)parentControl).Items.Insert(0, new ListItem("", ""));
                                ((Label)parentControl.FindControl(Convert.ToString(objCampo.GetValue(0)).Trim().Replace(Convert.ToString(objCampo.GetValue(6)), "lbl"))).Text = "Cliente";
                            }

                            if (Convert.ToInt16(objCampo.GetValue(3)) == 1)
                            {
                                dvTercero.RowFilter = "empresa=" + empresa.ToString() + " and proveedor=1 and activo=1";
                                ((DropDownList)parentControl).DataSource = dvTercero;
                                ((DropDownList)parentControl).DataValueField = "id";
                                ((DropDownList)parentControl).DataTextField = "descripcion";
                                ((DropDownList)parentControl).DataBind();
                                ((DropDownList)parentControl).Items.Insert(0, new ListItem("", ""));
                                ((Label)parentControl.FindControl(Convert.ToString(objCampo.GetValue(0)).Trim().Replace(Convert.ToString(objCampo.GetValue(6)), "lbl"))).Text = "Proveedor";
                            }

                            if (Convert.ToInt16(objCampo.GetValue(4)) == 1)
                            {
                                dvTercero.RowFilter = "empresa=" + empresa.ToString() + " and funcionario=1 and activo=1";
                                ((DropDownList)parentControl).DataSource = dvTercero;
                                ((DropDownList)parentControl).DataValueField = "id";
                                ((DropDownList)parentControl).DataTextField = "descripcion";
                                ((DropDownList)parentControl).DataBind();
                                ((DropDownList)parentControl).Items.Insert(0, new ListItem("", ""));
                                ((Label)parentControl.FindControl(Convert.ToString(objCampo.GetValue(0)).Trim().Replace(Convert.ToString(objCampo.GetValue(6)), "lbl"))).Text = "Tercero";
                            }

                            if (Convert.ToInt16(objCampo.GetValue(5)) == 1)
                                ((DropDownList)parentControl).Enabled = false;
                            else
                                ((DropDownList)parentControl).Enabled = true;
                            ((DropDownList)parentControl).SelectedValue = "";
                        }
                    }
                }
            }
        }

    }
}