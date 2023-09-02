using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using Almacen.seguridadInfos;

namespace Almacen.WebForms.App_Code.General
{
    public class BasePage : System.Web.UI.Page
    {
        public string consulta = "C";
        public string insertar = "I";
        public string eliminar = "E";
        public string imprime = "IM";
        public string ingreso = "IN";
        public string editar = "A";
        public Security seguridad = new Security();


        public string ObtenerIP()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }
        public static string DesEncriptar(string cadena)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(cadena);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

        public void ManejoErrorAcceso(string error, string operacion)
        {
            this.Session["error"] = error;
            this.Session["paginaAnterior"] = this.Page.Request.FilePath.ToString();

            if (this.User.Identity.IsAuthenticated)
            {
                seguridad.InsertaLog(this.User.Identity.Name.ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                      "er", error, ObtenerIP(), Convert.ToInt16(Session["empresa"]));
            }
        }

        public void ManejoError(string error, string operacion)
        {
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "er", error, ObtenerIP(), Convert.ToInt16(Session["empresa"]));

            ManejoError(this, GetType(), error, "error");
        }

        public void MostrarMensaje(string message)
        {
            ManejoError(this, GetType(), message, "warning");
        }

        public string nombrePaginaActual()
        {
            string[] arrResult = HttpContext.Current.Request.RawUrl.Split('/');
            string result = arrResult[arrResult.GetUpperBound(0)];
            arrResult = result.Split('?');
            return arrResult[arrResult.GetLowerBound(0)];
        }


        public void HabilitarControles(ControlCollection controles)
        {
            foreach (Control objControl in controles)
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

                if (objControl is CheckBoxList)
                {
                    ((CheckBoxList)objControl).Enabled = true;
                    ((CheckBoxList)objControl).Visible = true;
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

                if (objControl is LinkButton)
                {
                    ((LinkButton)objControl).Visible = true;
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


                if (objControl is FileUpload)
                {
                    ((FileUpload)objControl).Visible = true;
                }
                if (objControl.Controls.Count > 0)
                    HabilitarControles(objControl.Controls);
            }
        }

        public void InhabilitarControles(ControlCollection controles)
        {
            foreach (Control objControl in controles)
            {
                if (objControl is TextBox)
                {
                    if (!string.IsNullOrEmpty(((TextBox)objControl).ID) && !((TextBox)objControl).ID.ToString().StartsWith("ni"))
                    {
                        ((TextBox)objControl).ReadOnly = true;
                        ((TextBox)objControl).Visible = false;
                    }
                }

                if (objControl is DropDownList)
                {
                    if (!string.IsNullOrEmpty(((DropDownList)objControl).ID) && !((DropDownList)objControl).ID.ToString().StartsWith("ni"))
                    {
                        ((DropDownList)objControl).Enabled = false;
                        ((DropDownList)objControl).Visible = false;
                    }
                }

                if (objControl is CheckBox)
                {
                    if (!string.IsNullOrEmpty(((CheckBox)objControl).ID) && !((CheckBox)objControl).ID.ToString().StartsWith("ni"))
                    {
                        ((CheckBox)objControl).Enabled = false;
                        ((CheckBox)objControl).Visible = false;
                    }
                }

                if (objControl is CheckBoxList)
                {
                    if (!string.IsNullOrEmpty(((CheckBoxList)objControl).ID) && !((CheckBoxList)objControl).ID.ToString().StartsWith("ni"))
                    {
                        ((CheckBoxList)objControl).Enabled = false;
                        ((CheckBoxList)objControl).Visible = false;
                    }
                }

                if (objControl is RadioButton)
                {
                    if (!string.IsNullOrEmpty(((RadioButton)objControl).ID) && !((RadioButton)objControl).ID.ToString().StartsWith("ni"))
                    {
                        ((RadioButton)objControl).Enabled = false;
                        ((RadioButton)objControl).Visible = false;
                    }
                }

                if (objControl is RadioButtonList)
                {
                    if (!string.IsNullOrEmpty(((RadioButtonList)objControl).ID) && !((RadioButtonList)objControl).ID.ToString().StartsWith("ni"))
                    {
                        ((RadioButtonList)objControl).Enabled = false;
                        ((RadioButtonList)objControl).Visible = false;
                    }
                }

                if (objControl is Label)
                {
                    if (!string.IsNullOrEmpty(((Label)objControl).ID) && !((Label)objControl).ID.ToString().StartsWith("ni"))
                    {
                        ((Label)objControl).Visible = false;
                    }
                }

                if (objControl is Button)
                {
                    if (!string.IsNullOrEmpty(((Button)objControl).ID) && !((Button)objControl).ID.ToString().StartsWith("ni"))
                    {
                        ((Button)objControl).Visible = false;
                    }
                }

                if (objControl is ImageButton)
                {
                    if (!string.IsNullOrEmpty(((ImageButton)objControl).ID) && !((ImageButton)objControl).ID.ToString().StartsWith("ni"))
                    {
                        ((ImageButton)objControl).Visible = false;
                    }
                }

                if (objControl is Image)
                {
                    if (!string.IsNullOrEmpty(((Image)objControl).ID) && !((Image)objControl).ID.ToString().StartsWith("ni"))
                    {
                        ((Image)objControl).Visible = false;
                    }
                }

                if (objControl is FileUpload)
                {
                    if (!string.IsNullOrEmpty(((FileUpload)objControl).ID) && !((FileUpload)objControl).ID.ToString().StartsWith("ni"))
                    {
                        ((FileUpload)objControl).Visible = false;
                    }
                }

                if (objControl is LinkButton)
                {
                    if (!string.IsNullOrEmpty(((LinkButton)objControl).ID) && !((LinkButton)objControl).ID.ToString().StartsWith("ni"))
                    {
                        ((LinkButton)objControl).Visible = false;
                    }
                }

                if (objControl.Controls.Count > 0)
                    InhabilitarControles(objControl.Controls);
            }
        }

        public void LimpiarControles(ControlCollection controles)
        {

            foreach (Control objControl in controles)
            {
                if (objControl is TextBox)
                {
                    ((TextBox)objControl).Text = "";
                }

                if (objControl is CheckBox)
                {
                    ((CheckBox)objControl).Checked = false;
                }

                if (objControl is CheckBoxList)
                {
                    ((CheckBoxList)objControl).ClearSelection();
                }

                if (objControl is DropDownList)
                {
                    ((DropDownList)objControl).ClearSelection();
                }
                if (objControl.Controls.Count > 0)
                    LimpiarControles(objControl.Controls);
            }

        }

        public void OpcionesDefault(ControlCollection controles, int estado)
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

        public void CreaNodoRaiz(DataSet dsDatos, string id, string texto, TreeView arbol)
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

        public void CreaNodoRaizNoDemanda(DataSet dsDatos, string id, string texto, TreeView arbol)
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

        public void CreaNodoHijo(DataSet dsDatos, string id, string texto, TreeView arbol, TreeNode nodoPadre)
        {
            foreach (DataRow registro in dsDatos.Tables[0].Rows)
            {
                nodoPadre.ChildNodes.Add(
                    CreaNodoHijo(registro[id].ToString(), registro[texto].ToString()));

                arbol.DataBind();
            }
        }

        public void CreaNodoHijoNoDemanda(DataSet dsDatos, string id, string texto, TreeView arbol, TreeNode nodoPadre)
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

        public DataView OrdenarEntidad(DataView entidad, string campoOrden)
        {
            entidad.Sort = campoOrden;

            return entidad;
        }

        public DataView OrdenarEntidad(DataSet entidad, string campoOrden)
        {
            DataView dvEntidad = entidad.Tables[0].DefaultView;
            dvEntidad.Sort = campoOrden;

            return dvEntidad;
        }

        public DataView OrdenarEntidad(DataSet entidad, string campoOrden, int empresa)
        {
            DataView dvEntidad = entidad.Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa);
            dvEntidad.Sort = campoOrden;

            return dvEntidad;
        }

        public DataView OrdenarEntidadSinEmpresa(DataSet entidad, string campoOrden)
        {
            DataView dvEntidad = entidad.Tables[0].DefaultView;
            dvEntidad.Sort = campoOrden;

            return dvEntidad;
        }

        public DataView OrdenarEntidadSinEmpresayActivo(DataSet entidad, string campoOrden)
        {
            DataView dvEntidad = entidad.Tables[0].DefaultView;
            dvEntidad.RowFilter = "activo=True";
            dvEntidad.Sort = campoOrden;

            return dvEntidad;
        }

        public DataView OrdenarEntidadyActivos(DataSet entidad, string campoOrden, int empresa)
        {
            DataView dvEntidad = entidad.Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and activo=1";
            dvEntidad.Sort = campoOrden;
            return dvEntidad;
        }

        public DataView OrdenarEntidadTercero(DataSet entidad, string campoOrden, string filtro, int empresa)
        {
            DataView dvEntidad = entidad.Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and activo=True and " + filtro + "=True";

            dvEntidad.Sort = campoOrden;
            return dvEntidad;
        }


        public string RetornaConsecutivoAutomatico(string tabla, string nombreCampo, int empresa)
        {
            DataView dvEntidad = new DataView();
            string[] iParametros = new string[] { "@tabla", "@campo", "@empresa" };
            object[] objValores = new object[] { tabla, nombreCampo, empresa };

            dvEntidad = Cacceso.DataSetParametros("spRetornaConsecutivoAutomatico", iParametros, objValores, "ppa").Tables[0].DefaultView;
            return dvEntidad.Table.Rows[0].ItemArray[0].ToString();
        }

        public string RetornaConsecutivoAutomaticoSinEmpresa(string tabla, string nombreCampo)
        {
            DataView dvEntidad = new DataView();
            string[] iParametros = new string[] { "@tabla", "@campo" };
            object[] objValores = new object[] { tabla, nombreCampo };

            dvEntidad = Cacceso.DataSetParametros("spRetornaConsecutivoAutomaticoSinEmpresa", iParametros, objValores, "ppa").Tables[0].DefaultView;

            return dvEntidad.Table.Rows[0].ItemArray[0].ToString();

        }


        public dynamic ObtenerErroresGeneral(string errores, string[] args = null)
        {
            var nivel = "error";
            var mensaje = "Se ha presentado un error general en el sistema. Por favor, contacte con el administrador.";
            var dataSet = CentidadMetodos.EntidadGet(
                    "sErrores",
                    "ppa");
            var dataTable = dataSet.Tables[0];
            foreach (DataRow row in dataTable.Rows)
            {
                var errorTemplate = new Regex((string)row["plantillaError"]);
                var traduccionTemplate = (string)row["plantillaTraduccion"];
                if (errorTemplate.IsMatch(errores))
                {
                    Match m = errorTemplate.Match(errores);
                    if (args != null)
                        mensaje = string.Format(errorTemplate.Replace(m.Value, traduccionTemplate), args);
                    else
                        mensaje = errorTemplate.Replace(m.Value, traduccionTemplate);
                    nivel = (string)row["severidad"];
                }
            }
            return new
            {
                mensaje = mensaje,
                nivel = nivel
            };
        }

        public void ManejoErrorCatch(Exception exepcion)
        {
            ManejoErrorCatch(Page, Page.GetType(), exepcion);
        }

        public void ManejoErrorCatch(Control control, Type type, Exception exepcion, string[] args = null)
        {
            string errorOriginal = limpiarMensaje(exepcion.ToString());
            var nivel = "error";
            var mensaje = "Se ha presentado un error general en el sistema. Por favor, contacte con el administrador.";
            var errores = new
            {
                mensaje = mensaje,
                nivel = nivel
            };
            StringBuilder cstext2 = new StringBuilder();
            cstext2.Append(@"
            function clientScriptError(title, text, type) {
                $(document).ready(function () {
                    swal({
                        title: title,
                        html: text,
                        type: type,
                        confirmButtonText: ""Aceptar"",
                        showCancelButton: true,
			            cancelButtonText: 'Mostrar detalle <i class=""fa fa-angle-right""></i>'
                    }).then(
			            function () {}, 
			            function (dismiss) {
				            if (dismiss === 'cancel') {
					            swal({
						            title: ""<small>Detalle de la excepción </small>"",
                                    html: ""<textarea disabled style='width:100%; height:300px;max-height:299px;resize:none;font-size:initial;font-family:monospace;'>" + errorOriginal.Replace(Environment.NewLine, "\\r\\n").Replace("\n", "\\n").Replace("\"", "\\\"") + @"</textarea>"",
						            confirmButtonText: ""Aceptar"",
						            animation: false,
                                });
				            }
			            });
                });
            }
            clientScriptError(""Error"",""" + errores.mensaje + @""",""" + errores.nivel + @""");
        ");

            ScriptManager.RegisterStartupScript(control, type, Guid.NewGuid().ToString(), cstext2.ToString(), true);
        }

        public string limpiarMensaje(string strIn)
        {
            try
            {
                return Regex.Replace(strIn, @"[^\w\.@-]", " ", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }

        static string limpiaMensaje(string strIn)
        {
            try
            {
                return Regex.Replace(strIn, @"[^\w\.@-]", " ", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }

        public static void ManejoError(Control upPanel, Type type, string mensaje, string nivel, string[] args = null)
        {
            string titulo = "";

            if (nivel == "e" || nivel == "error")
            {
                titulo = "Error";
                nivel = "error";
            }

            if (nivel == "i" || nivel == "info")
            {
                titulo = "Información";
                nivel = "info";
            }

            if (nivel == "e" || nivel == "warning")
            {
                titulo = "Advertencia";
                nivel = "warning";
            }
            mensaje = limpiaMensaje(mensaje);


            var errores = new
            {
                mensaje = mensaje,
                nivel = nivel
            };

            StringBuilder cstext2 = new StringBuilder();
            cstext2.Append(@"
            function clientScriptError(title, text, type) {
                $(document).ready(function () {
                    swal({
                        title: title,
                        html: text,
                        type: type,
                        confirmButtonText: ""Aceptar"",
                        showCancelButton: true,
			            cancelButtonText: 'Mostrar detalle <i class=""fa fa-angle-right""></i>'
                    }).then(
			            function () {}, 
			            function (dismiss) {
				            if (dismiss === 'cancel') {
					            swal({
						            title: ""<small>Detalle de la excepción </small>"",
                                    html: ""<textarea disabled style='width:100%; height:300px;max-height:299px;resize:none;font-size:initial;font-family:monospace;'>" + mensaje.Replace("\n", "\\n").Replace(Environment.NewLine, "\\r\\n").Replace("\"", "\\\"") + @"</textarea>"",
						            confirmButtonText: ""Aceptar"",
						            animation: false,
                                });
				            }
			            });
                });
            }
            clientScriptError(""" + titulo + @""",""" + errores.mensaje + @""",""" + errores.nivel + @""");        ");

            ScriptManager.RegisterStartupScript(upPanel, type, Guid.NewGuid().ToString(), cstext2.ToString(), true);
        }


        public void ComportamientoCampoEntidad(Control root, ControlCollection controles, string entidad, string TipoTransaccion, int empresa)
        {
            foreach (Control objControl in controles)
            {
                if (objControl.ID != null)
                {
                    object[] objCampo = CampoTransaccion(TipoTransaccion, entidad, objControl.ID.ToString(), empresa);

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

                            var labelAsociado = root.FindControl(Convert.ToString(objControl.ID).Trim().Replace(Convert.ToString(objControl.ID).Substring(0, 3), "lbl")) as Label;
                            if (labelAsociado != null)
                                labelAsociado.Visible = true;


                        }
                        else if (objControl is DropDownList)
                        {
                            ((DropDownList)objControl).Enabled = true;
                            ((DropDownList)objControl).Visible = true;

                            var labelAsociado = root.FindControl(Convert.ToString(objControl.ID).Trim().Replace(Convert.ToString(objControl.ID).Substring(0, 3), "lbl")) as Label;
                            if (labelAsociado != null)
                                labelAsociado.Visible = true;
                        }

                    }


                    if (objControl is DropDownList)
                        if (Convert.ToInt16(objCampo.GetValue(1)) == 1)
                        {
                            DataView dvTercero = OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cTercero", "ppa"), "descripcion", empresa);

                            if (Convert.ToInt16(objCampo.GetValue(2)) == 1)
                            {
                                dvTercero.RowFilter = "empresa=" + empresa.ToString() + " and cliente=1 and activo=1";
                                ((DropDownList)objControl).DataSource = dvTercero;
                                ((DropDownList)objControl).DataValueField = "id";
                                ((DropDownList)objControl).DataTextField = "descripcion";
                                ((DropDownList)objControl).DataBind();
                                ((DropDownList)objControl).Items.Insert(0, new ListItem("", ""));
                                var labelAsociado = root.FindControl(Convert.ToString(objControl.ID).Trim().Replace(Convert.ToString(objControl.ID).Substring(0, 3), "lbl")) as Label;
                                if (labelAsociado != null)
                                    labelAsociado.Text = "Cliente";
                            }

                            if (Convert.ToInt16(objCampo.GetValue(3)) == 1)
                            {
                                ((DropDownList)objControl).ClearSelection();
                                dvTercero.RowFilter = "empresa=" + empresa.ToString() + " and proveedor=1 and activo=1";
                                ((DropDownList)objControl).DataSource = dvTercero;
                                ((DropDownList)objControl).DataValueField = "id";
                                ((DropDownList)objControl).DataTextField = "descripcion";
                                ((DropDownList)objControl).DataBind();
                                ((DropDownList)objControl).Items.Insert(0, new ListItem("", ""));
                                var labelAsociado = root.FindControl(Convert.ToString(objControl.ID).Trim().Replace(Convert.ToString(objControl.ID).Substring(0, 3), "lbl")) as Label;
                                if (labelAsociado != null)
                                    labelAsociado.Text = "Proveedor";
                            }

                            if (Convert.ToInt16(objCampo.GetValue(4)) == 1)
                            {
                                dvTercero.RowFilter = "empresa=" + empresa.ToString() + " and funcionario=1 and activo=1";
                                ((DropDownList)objControl).DataSource = dvTercero;
                                ((DropDownList)objControl).DataValueField = "id";
                                ((DropDownList)objControl).DataTextField = "descripcion";
                                ((DropDownList)objControl).DataBind();
                                ((DropDownList)objControl).Items.Insert(0, new ListItem("", ""));
                                var labelAsociado = root.FindControl(Convert.ToString(objControl.ID).Trim().Replace(Convert.ToString(objControl.ID).Substring(0, 3), "lbl")) as Label;
                                if (labelAsociado != null)
                                    labelAsociado.Text = "Tercero";

                            }

                            if (Convert.ToInt16(objCampo.GetValue(5)) == 1)
                                ((DropDownList)objControl).Enabled = false;
                            else
                                ((DropDownList)objControl).Enabled = true;
                            ((DropDownList)objControl).SelectedValue = "";
                        }
                }

                ComportamientoCampoEntidad(root, objControl.Controls, entidad, TipoTransaccion, empresa);
            }
        }

        public string TipoTransaccionConfig(string tipoTransaccion, int empresa)
        {
            string retorno = "";
            object[] objKey = new object[] { empresa, tipoTransaccion };

            foreach (DataRowView registro in CentidadMetodos.EntidadGetKey("gTipoTransaccionConfig", "ppa", objKey).Tables[0].DefaultView)
            {
                for (int i = 1; i < registro.Row.ItemArray.Length; i++)
                {
                    retorno = retorno + registro.Row.ItemArray.GetValue(i).ToString() + "*";
                }
            }

            return retorno;
        }

        public object[] CampoTransaccion(string tipoTransaccion, string entidad, string campo, int empresa)
        {

            string[] iParametros = new string[] { "@tipoTransaccion", "@entidad", "@campo", "@empresa" };
            object[] objValores = new object[] { tipoTransaccion, entidad, campo, empresa };
            object[] resultado = new object[7];

            foreach (DataRowView registro in Cacceso.DataSetParametros("spSeleccionaCampoTipoEntidad", iParametros, objValores, "ppa").Tables[0].DefaultView)
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

        public void MensajeError(string mensaje, Label etiqueta)
        {
            etiqueta.ForeColor = System.Drawing.Color.Red;
            etiqueta.Text = mensaje;
        }

        public bool VerificaCamposRequeridos(ControlCollection controles)
        {
            foreach (Control objControl in controles)
            {
                if (objControl.Visible == true)
                {
                    if (objControl is TextBox)
                    {
                        if (((TextBox)objControl).Text.Trim().Length == 0 && ((TextBox)objControl).Enabled == true)
                        {
                            return false;
                        }
                    }

                    if (objControl is DropDownList)
                    {
                        if (Convert.ToString(((DropDownList)objControl).SelectedValue).Trim().Length == 0 && ((DropDownList)objControl).Enabled == true)
                        {
                            return false;
                        }
                    }
                }

                if (!VerificaCamposRequeridos(objControl.Controls))
                    return false;
            }
            return true;
        }
    }
}