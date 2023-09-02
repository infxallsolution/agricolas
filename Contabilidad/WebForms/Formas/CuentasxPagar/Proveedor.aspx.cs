using Contabilidad.WebForms.App_Code.Administracion;
using Contabilidad.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.CuentasxPagar
{
    public partial class Proveedor : BasePage
    {
        #region Instancias;
        Cproveedor proveedor = new Cproveedor();
        Cterceros tercerosinfos = new Cterceros();
        CConfigClaseIR cconfigclase = new CConfigClaseIR();
        CConceptoIR llave = new CConceptoIR();

        public DataView dvConfig
        {
            get { return (DataView)(this.Session["proveedor"] ?? null); }
            set { this.Session["proveedor"] = value; }

        }

        #endregion Instancias

        #region Metodos

        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),//sitio
                                  nombrePaginaActual(), consulta, Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", consulta);
                    return;
                }

                gvClaseIR.Visible = false;

                dvConfig = proveedor.BuscarEntidad(this.nitxtBusqueda.Text, Convert.ToInt32(this.Session["empresa"]));
                this.gvLista.DataSource = dvConfig;
                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "ex", this.gvLista.Rows.Count.ToString() + " Registros encontrados", ObtenerIP(), Convert.ToInt16(this.Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }
        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");

            InhabilitarControles(this.Page.Controls);
            LimpiarControles(this.Page.Controls);

            this.nilbNuevo.Visible = true;

            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "ex", mensaje, ObtenerIP(), Convert.ToInt16(this.Session["empresa"]));
            GetEntidad();
        }
        private void CargarCombos()
        {
            try
            {
                DataView terceros = tercerosinfos.SeleccionaTercerosProveedor(Convert.ToInt16(this.Session["empresa"]));
                this.ddlTercero.DataSource = terceros;
                this.ddlTercero.DataValueField = "id";
                this.ddlTercero.DataTextField = "cadena";
                this.ddlTercero.DataBind();
                this.ddlTercero.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }

            try
            {

                DataView claseProveedor = OrdenarEntidad(CentidadMetodos.EntidadGet("gPais", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"]));
                claseProveedor.RowFilter = "empresa=" + Convert.ToInt16(this.Session["empresa"]);
                this.ddlPais.DataSource = claseProveedor;
                this.ddlPais.DataValueField = "codigo";
                this.ddlPais.DataTextField = "descripcion";
                this.ddlPais.DataBind();
                this.ddlPais.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }


            try
            {

                DataView claseProveedor = OrdenarEntidad(CentidadMetodos.EntidadGet("cxpClaseProveedor", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"]));
                claseProveedor.RowFilter = "empresa=" + Convert.ToInt16(this.Session["empresa"]);
                this.ddlClaseProveedor.DataSource = claseProveedor;
                this.ddlClaseProveedor.DataValueField = "codigo";
                this.ddlClaseProveedor.DataTextField = "descripcion";
                this.ddlClaseProveedor.DataBind();
                this.ddlClaseProveedor.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }


            try
            {
                gvClaseIR.Visible = true;
                this.gvClaseIR.DataSource = OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cClaseIR", "ppa"), "codigo", Convert.ToInt16(Session["empresa"]));
                gvClaseIR.DataBind();

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }

        }
        private void EntidadKey()
        {
            if (ddlTercero.SelectedValue.ToString().Trim().Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "debe escojer un tercero primero", "warning");
                return;
            }
            object[] objKey = new object[] { this.txtCodigo.Text, Convert.ToInt16(this.Session["empresa"]), Convert.ToInt16(ddlTercero.SelectedValue.ToString()) };

            try
            {
                if (CentidadMetodos.EntidadGetKey("cxpProveedor", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    CerroresGeneral.ManejoError(this, GetType(), "El código " + this.txtCodigo.Text + " ya se encuentra registrado", "warning");
                    gvClaseIR.DataSource = null;
                    gvClaseIR.DataBind();
                    InhabilitarControles(this.Page.Controls);
                    this.nilbNuevo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }
        private void Guardar()
        {
            string operacion = "inserta";
            bool verificacion = false;

            try
            {

                if (Convert.ToBoolean(this.ViewState["editar"]) == true)
                    operacion = "actualiza";

                using (TransactionScope ts = new TransactionScope())
                {

                    object[] objValores = new object[]{
                    chkActivo.Checked,
                    ddlCiudad.SelectedValue.ToString().Trim(),
                    ddlClaseProveedor.SelectedValue.ToString().Trim(),
                    this.txtCodigo.Text,
                    txtContacto.Text,
                    ddlDepartamento.SelectedValue,
                    txtDescripcion.Text,
                    txtDireccion.Text,
                    txtEmail.Text,
                    Convert.ToInt16(this.Session["empresa"]),
                      chkEntradaDirecta.Checked,
                    DateTime.Now,
                    ddlTercero.SelectedValue,
                    ddlPais.SelectedValue.Trim(),
                    txtTelefono.Text
                };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("cxpProveedor", operacion, "ppa", objValores))
                    {
                        case 0:

                            object[] objValoresElimina = new object[]{
                                            Convert.ToInt16(Session["empresa"]),
                                            txtCodigo.Text,
                                            Convert.ToInt16(ddlTercero.SelectedValue)
                                        };

                            switch (CentidadMetodos.EntidadInsertUpdateDelete("cxpProveedorCalseIR", "elimina", "ppa", objValoresElimina))
                            {
                                case 1:
                                    verificacion = true;
                                    break;
                            }


                            foreach (GridViewRow registro in this.gvClaseIR.Rows)
                            {
                                if (((CheckBox)registro.FindControl("chkSelect")).Checked == true)
                                {
                                    string concepto;
                                    string llave;

                                    if (((DropDownList)registro.FindControl("ddlConcepto")).SelectedValue == "")
                                    {
                                        concepto = null;
                                    }
                                    else
                                    {
                                        concepto = ((DropDownList)registro.FindControl("ddlConcepto")).SelectedValue;
                                    }

                                    if (((DropDownList)registro.FindControl("ddlLlave")).SelectedValue == "")
                                    {
                                        llave = null;
                                    }
                                    else
                                    {
                                        llave = ((DropDownList)registro.FindControl("ddlLlave")).SelectedValue;
                                    }

                                    object[] objValoresClase = new object[]{
                                        Convert.ToInt16(registro.Cells[1].Text),
                                        Convert.ToInt16(Session["empresa"]),
                                        llave,
                                        txtCodigo.Text,
                                        Convert.ToInt16(ddlTercero.SelectedValue),
                                        concepto
                                    };

                                    switch (CentidadMetodos.EntidadInsertUpdateDelete("cxpProveedorCalseIR", "inserta", "ppa", objValoresClase))
                                    {
                                        case 1:
                                            verificacion = true;
                                            break;
                                    }

                                }
                            }

                            break;
                        case 1:
                            verificacion = true;
                            break;
                    }

                    if (verificacion == true)
                    {
                        MostrarMensaje("Error al insertar el registro. operación no realizada");
                    }
                    else
                    {
                        ManejoExito("Asignación registrada correctamente", "I");
                        ts.Complete();
                    }
                }
            }

            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }
        private void ValidaRegistro()
        {
            try
            {
                foreach (GridViewRow registro in this.gvClaseIR.Rows)
                {
                    if (proveedor.VerificaClaseIR(txtCodigo.Text, Convert.ToInt16(ddlTercero.SelectedValue), Convert.ToInt32(registro.Cells[1].Text), Convert.ToInt16(Session["empresa"])) == 0)
                    {
                        ((CheckBox)registro.FindControl("chkSelect")).Checked = true;


                        foreach (DataRowView clases in proveedor.TerceroClase(Convert.ToInt16(registro.Cells[1].Text), txtCodigo.Text, Convert.ToInt16(ddlTercero.SelectedValue), Convert.ToInt16(Session["empresa"])))
                        {
                            if (clases.Row.ItemArray.GetValue(3).ToString() == registro.Cells[1].Text)
                            {
                                ((DropDownList)registro.FindControl("ddlConcepto")).DataSource = cconfigclase.ValoresClasesConfig(Convert.ToInt32(registro.Cells[1].Text), Convert.ToInt16(Session["empresa"]));
                                ((DropDownList)registro.FindControl("ddlConcepto")).DataValueField = "codigo";
                                ((DropDownList)registro.FindControl("ddlConcepto")).DataTextField = "descripcion";
                                ((DropDownList)registro.FindControl("ddlConcepto")).DataBind();
                                ((DropDownList)registro.FindControl("ddlConcepto")).Items.Insert(0, new ListItem("", ""));
                                ((DropDownList)registro.FindControl("ddlConcepto")).Enabled = true;

                                if (clases.Row.ItemArray.GetValue(4).ToString() == "")
                                    ((DropDownList)registro.FindControl("ddlConcepto")).SelectedValue = "";
                                else
                                    ((DropDownList)registro.FindControl("ddlConcepto")).SelectedValue = clases.Row.ItemArray.GetValue(4).ToString();


                                if (cconfigclase.ManejaLlaveClase(Convert.ToInt16(registro.Cells[1].Text.Trim()), Convert.ToInt16(this.Session["empresa"]), ((DropDownList)registro.FindControl("ddlConcepto")).SelectedValue) == 1)
                                {
                                    ((DropDownList)registro.FindControl("ddlLLave")).Enabled = true;
                                    ((DropDownList)registro.FindControl("ddlLLave")).DataSource = llave.BuscarEntidad("", Convert.ToInt16(this.Session["empresa"]));
                                    ((DropDownList)registro.FindControl("ddlLLave")).DataValueField = "codigo";
                                    ((DropDownList)registro.FindControl("ddlLLave")).DataTextField = "descripcion";
                                    ((DropDownList)registro.FindControl("ddlLLave")).DataBind();
                                    ((DropDownList)registro.FindControl("ddlLLave")).Items.Insert(0, new ListItem("", ""));
                                    ((DropDownList)registro.FindControl("ddlLLave")).SelectedValue = "";

                                    if (clases.Row.ItemArray.GetValue(5).ToString() == "")
                                        ((DropDownList)registro.FindControl("ddlLLave")).SelectedValue = "";
                                    else
                                        ((DropDownList)registro.FindControl("ddlLLave")).SelectedValue = clases.Row.ItemArray.GetValue(5).ToString();

                                }
                                else
                                {
                                    ((DropDownList)registro.FindControl("ddlLLave")).DataSource = null;
                                    ((DropDownList)registro.FindControl("ddlLLave")).DataBind();
                                    ((DropDownList)registro.FindControl("ddlLLave")).Enabled = false;
                                }

                            }
                            else
                            {
                                ((DropDownList)registro.FindControl("ddlConcepto")).Enabled = false;
                                ((DropDownList)registro.FindControl("ddlConcepto")).SelectedValue = "";
                            }

                        }

                    }
                    else
                    {
                        ((CheckBox)registro.FindControl("chkSelect")).Checked = false;
                        ((DropDownList)registro.FindControl("ddlConcepto")).DataSource = null;
                        ((DropDownList)registro.FindControl("ddlConcepto")).DataBind();

                    }
                }

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }
        private void cargarMunicipio()
        {
            try
            {
                if (ddlPais.SelectedValue.Trim().Length == 0)
                {
                    ManejoError("Ingrese un país válido", "I");
                    return;
                }
                DataView ciudad = OrdenarEntidad(CentidadMetodos.EntidadGet("gDepartamento", "ppa"), "descripcion");
                ciudad.RowFilter = "empresa= " + Session["empresa"] + " and pais =" + ddlPais.SelectedValue;
                this.ddlDepartamento.DataSource = ciudad;
                this.ddlDepartamento.DataValueField = "codigo";
                this.ddlDepartamento.DataTextField = "descripcion";
                this.ddlDepartamento.DataBind();
                this.ddlDepartamento.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar municipios debido a:" + ex.Message, "I");
            }
        }
        private void cargarciudad()
        {
            try
            {
                if (ddlPais.SelectedValue.Trim().Length == 0)
                {
                    ManejoError("Ingrese un país válido", "I");
                    return;
                }
                DataView ciudad = OrdenarEntidad(CentidadMetodos.EntidadGet("gCiudad", "ppa"), "descripcion");
                ciudad.RowFilter = "empresa= " + Session["empresa"].ToString() + " and departamento ='" + ddlDepartamento.SelectedValue + "'";
                this.ddlCiudad.DataSource = ciudad;
                this.ddlCiudad.DataValueField = "codigo";
                this.ddlCiudad.DataTextField = "descripcion";
                this.ddlCiudad.DataBind();
                this.ddlCiudad.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar municipios debido a:" + ex.Message, "I");
            }
        }

        #endregion Metodos

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                        nombrePaginaActual(), Convert.ToInt16(this.Session["empresa"])) != 0)
                {
                    this.nitxtBusqueda.Focus();

                    if (this.txtCodigo.Text.Length > 0)
                        this.txtDescripcion.Focus();
                }
                else
                    ManejoErrorAcceso("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            InhabilitarControles(this.Page.Controls);
            LimpiarControles(this.Page.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            gvClaseIR.Visible = false;
            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";

        }
        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                 nombrePaginaActual(), editar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", editar);
                return;
            }
            HabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = false;
            this.ViewState["editar"] = true;
            this.ddlTercero.Enabled = false;
            this.txtDescripcion.Focus();
            txtCodigo.Enabled = false;

            try
            {
                CargarCombos();


                foreach (DataRowView info in dvConfig.Table.AsEnumerable().Where(x => x.Field<int>("empresa") == Convert.ToInt32(this.Session["empresa"])
                & x.Field<string>("idTercero") == gvLista.SelectedRow.Cells[2].Text.Trim()
            & x.Field<string>("codigo") == gvLista.SelectedRow.Cells[3].Text.Trim()).Select(x => x).AsDataView())
                {
                    ddlTercero.SelectedValue = info.Row.Field<string>("idTercero") ?? "";
                    txtCodigo.Text = info.Row.Field<string>("codigo") ?? "";
                    ValidaRegistro();
                    txtDescripcion.Text = info.Row.Field<string>("descripcion") ?? "";
                    txtDireccion.Text = info.Row.Field<string>("direccion") ?? "";
                    txtTelefono.Text = info.Row.Field<string>("telefono") ?? "";
                    txtEmail.Text = info.Row.Field<string>("email") ?? "";
                    txtContacto.Text = info.Row.Field<string>("contacto") ?? "";
                    ddlClaseProveedor.SelectedValue = info.Row.Field<string>("clase") ?? "";
                    chkActivo.Checked = info.Row.Field<bool>("activo");
                    chkEntradaDirecta.Checked = info.Row.Field<bool>("entradaDirecta");

                    try
                    {
                        ddlPais.SelectedValue = info.Row.Field<string>("pais") ?? "";
                        cargarMunicipio();
                        ddlDepartamento.SelectedValue = info.Row.Field<string>("departamento") ?? "";
                        cargarciudad();
                        ddlCiudad.SelectedValue = info.Row.Field<string>("ciudad") ?? "";
                    }
                    catch (Exception ex)
                    {
                    }

                }


            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),//sitio
                                 nombrePaginaActual(), eliminar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", eliminar);
                return;
            }
            string operacion = "elimina";
            try
            {
                if (proveedor.EliminaClaseProveedor(Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[3].Text), Convert.ToInt16(this.gvLista.Rows[e.RowIndex].Cells[2].Text), Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    object[] objValores = new object[] {
                Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[3].Text),
                Convert.ToInt16(this.Session["empresa"]),
                Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text)            };

                    if (CentidadMetodos.EntidadInsertUpdateDelete("cxpProveedor", operacion, "ppa", objValores) == 0)
                        ManejoExito("Datos eliminados satisfactoriamente", "E");
                    else
                        ManejoError("Errores al eliminar el registro. Operación no realizada", "E");
                }
                else
                    ManejoError("Errores al eliminar el registro. Operación no realizada", "E");
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }
        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
            txtDescripcion.Focus();
        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Length == 0 || txtDescripcion.Text.Length == 0 ||
                txtContacto.Text.Length == 0)
            {

                CerroresGeneral.ManejoError(this, GetType(), "Campos vacios por favor corrija", "warning");
                return;
            }

            if (ddlClaseProveedor.SelectedIndex.ToString().Trim().Length == 0 || ddlCiudad.SelectedIndex.ToString().Trim().Length == 0 ||
                ddlTercero.SelectedIndex.ToString().Trim().Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Seleccione un tercero /ciudad / clase proveedor", "warning");
                return;
            }


            Guardar();
        }
        protected void lbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),//sitio
                                  nombrePaginaActual(), insertar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", insertar);
                return;
            }

            HabilitarControles(this.Page.Controls);
            LimpiarControles(this.Page.Controls);
            this.nilbNuevo.Visible = false;
            this.ViewState["editar"] = false;
            CargarCombos();
            this.ddlTercero.Enabled = true;
            this.ddlTercero.Focus();
            this.nilblInformacion.Text = "";
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.DataBind();
            GetEntidad();
        }
        protected void gvClaseIR_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                if (((CheckBox)this.gvClaseIR.Rows[e.RowIndex].FindControl("chkSelect")).Checked)
                {
                    ((DropDownList)this.gvClaseIR.Rows[e.RowIndex].FindControl("ddlConcepto")).Enabled = true;
                    ((DropDownList)this.gvClaseIR.Rows[e.RowIndex].FindControl("ddlConcepto")).DataSource = cconfigclase.ValoresClasesConfig(Convert.ToInt16(gvClaseIR.Rows[e.RowIndex].Cells[1].Text), Convert.ToInt16(Session["empresa"]));
                    ((DropDownList)this.gvClaseIR.Rows[e.RowIndex].FindControl("ddlConcepto")).DataValueField = "codigo";
                    ((DropDownList)this.gvClaseIR.Rows[e.RowIndex].FindControl("ddlConcepto")).DataTextField = "descripcion";
                    ((DropDownList)this.gvClaseIR.Rows[e.RowIndex].FindControl("ddlConcepto")).DataBind();
                    ((DropDownList)this.gvClaseIR.Rows[e.RowIndex].FindControl("ddlConcepto")).Items.Insert(0, new ListItem("", ""));

                }
                else
                {
                    ((DropDownList)this.gvClaseIR.Rows[e.RowIndex].FindControl("ddlConcepto")).DataSource = null;
                    ((DropDownList)this.gvClaseIR.Rows[e.RowIndex].FindControl("ddlConcepto")).DataBind();
                    ((DropDownList)this.gvClaseIR.Rows[e.RowIndex].FindControl("ddlConcepto")).Items.Insert(0, new ListItem("", ""));
                    ((DropDownList)this.gvClaseIR.Rows[e.RowIndex].FindControl("ddlConcepto")).SelectedValue = "";
                    ((DropDownList)this.gvClaseIR.Rows[e.RowIndex].FindControl("ddlConcepto")).Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }

        }
        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox drop = (CheckBox)sender;
                GridViewRow row = (GridViewRow)((CheckBox)sender).Parent.Parent;

                if (((CheckBox)row.FindControl("chkSelect")).Checked)
                {
                    ((DropDownList)row.FindControl("ddlConcepto")).Enabled = true;
                    ((DropDownList)row.FindControl("ddlConcepto")).DataSource = cconfigclase.ValoresClasesConfig(Convert.ToInt16(row.Cells[1].Text), Convert.ToInt16(Session["empresa"]));
                    ((DropDownList)row.FindControl("ddlConcepto")).DataValueField = "codigo";
                    ((DropDownList)row.FindControl("ddlConcepto")).DataTextField = "descripcion";
                    ((DropDownList)row.FindControl("ddlConcepto")).DataBind();
                    ((DropDownList)row.FindControl("ddlConcepto")).Items.Insert(0, new ListItem("", ""));

                }
                else
                {
                    ((DropDownList)row.FindControl("ddlConcepto")).DataSource = null;
                    ((DropDownList)row.FindControl("ddlConcepto")).DataBind();
                    ((DropDownList)row.FindControl("ddlConcepto")).Items.Insert(0, new ListItem("", ""));
                    ((DropDownList)row.FindControl("ddlConcepto")).SelectedValue = "";
                    ((DropDownList)row.FindControl("ddlConcepto")).Enabled = false;
                    ((DropDownList)row.FindControl("ddlLLave")).DataSource = null;
                    ((DropDownList)row.FindControl("ddlLLave")).DataBind();
                    ((DropDownList)row.FindControl("ddlLLave")).Items.Insert(0, new ListItem("", ""));
                    ((DropDownList)row.FindControl("ddlLLave")).SelectedValue = "";
                    ((DropDownList)row.FindControl("ddlLLave")).Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }

        }
        protected void ddlConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvr = (GridViewRow)((DropDownList)sender).Parent.Parent;

                if (cconfigclase.ManejaLlaveClase(Convert.ToInt16(gvr.Cells[1].Text.Trim()), Convert.ToInt16(this.Session["empresa"]), ((DropDownList)sender).SelectedValue.Trim()) == 1)
                {
                    ((DropDownList)gvr.FindControl("ddlLLave")).Enabled = true;
                    ((DropDownList)gvr.FindControl("ddlLLave")).DataSource = llave.seleccionaValoresClase(Convert.ToInt16(gvr.Cells[1].Text.Trim()), Convert.ToInt16(this.Session["empresa"]));
                    ((DropDownList)gvr.FindControl("ddlLLave")).DataValueField = "codigo";
                    ((DropDownList)gvr.FindControl("ddlLLave")).DataTextField = "descripcion";
                    ((DropDownList)gvr.FindControl("ddlLLave")).DataBind();
                    ((DropDownList)gvr.FindControl("ddlLLave")).Items.Insert(0, new ListItem("", ""));
                    ((DropDownList)gvr.FindControl("ddlLLave")).SelectedValue = "";

                }
                else
                {
                    ((DropDownList)gvr.FindControl("ddlLLave")).DataSource = null;
                    ((DropDownList)gvr.FindControl("ddlLLave")).DataBind();
                    ((DropDownList)gvr.FindControl("ddlLLave")).Items.Insert(0, new ListItem("", ""));
                    ((DropDownList)gvr.FindControl("ddlLLave")).SelectedValue = "";
                    ((DropDownList)gvr.FindControl("ddlLLave")).Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }
        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarMunicipio();
        }
        protected void ddlMunicipio_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarciudad();

        }
        protected void ddlTercero_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlTercero.SelectedValue.Trim().Length > 0)
                {
                    if (ddlTercero.SelectedItem.Text.IndexOf('-') > 0)
                        txtDescripcion.Text = ddlTercero.SelectedItem.Text.Split('-')[1].Trim();
                }

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tercero debido a: " + ex.Message, "I");
            }
        }

        #endregion Eventos


    }
}