using Contabilidad.WebForms.App_Code.Administracion;
using Contabilidad.WebForms.App_Code.CuentasxCobrar;
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

namespace Contabilidad.WebForms.Formas.Padministracion
{
    public partial class Clientes : BasePage
    {
        #region Instancias;

        Ccliente proveedor = new Ccliente();
        Cterceros tercerosinfos = new Cterceros();
        CConfigClaseIR cconfigclase = new CConfigClaseIR();
        CConceptoIR llave = new CConceptoIR();

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
                this.gvLista.DataSource = proveedor.BuscarEntidad(this.nitxtBusqueda.Text, Convert.ToInt32(this.Session["empresa"]));
                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.formContainer.ToString(),
                "ex", this.gvLista.Rows.Count.ToString() + " Registros encontrados", HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(this.Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }
        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            InhabilitarControles(this.formContainer.Controls);
            LimpiarControles(this.formContainer.Controls);
            this.nilbNuevo.Visible = true;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.formContainer.ToString(),
                "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(this.Session["empresa"]));
            GetEntidad();
        }
        private void CargarCombos()
        {
            try
            {
                DataView terceros = tercerosinfos.SeleccionaTercerosClientes(Convert.ToInt16(this.Session["empresa"]));
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
                DataView claseProveedor = OrdenarEntidad(CentidadMetodos.EntidadGet("cxcClaseCliente", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"]));
                claseProveedor.RowFilter = "empresa=" + Convert.ToInt16(this.Session["empresa"]);
                this.ddlClaseCliente.DataSource = claseProveedor;
                this.ddlClaseCliente.DataValueField = "codigo";
                this.ddlClaseCliente.DataTextField = "descripcion";
                this.ddlClaseCliente.DataBind();
                this.ddlClaseCliente.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }

            try
            {

                DataView ciudad = OrdenarEntidad(CentidadMetodos.EntidadGet("gPais", "ppa"), "descripcion");
                this.ddlPais.DataSource = ciudad;
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
                CerroresGeneral.ManejoError(this, GetType(), "debe escojer un tercero primero", "info");
                return;
            }
            object[] objKey = new object[] {
            this.txtCodigo.Text,
           Convert.ToInt16(this.Session["empresa"]),
           Convert.ToInt16(ddlTercero.SelectedValue.ToString())
        };

            try
            {
                if (CentidadMetodos.EntidadGetKey("cxcCliente", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    CerroresGeneral.ManejoError(this, GetType(), "El código de cliente " + this.txtCodigo.Text + " ya se encuentra registrado", "warning");
                    gvClaseIR.DataSource = null;
                    gvClaseIR.DataBind();
                    InhabilitarControles(this.formContainer.Controls);
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
                    ddlClaseCliente.SelectedValue.ToString().Trim(),
                    this.txtCodigo.Text,
                    txtContacto.Text,
                    ddlMunicipio.SelectedValue,
                    txtDescripcion.Text,
                    txtDireccion.Text,
                    txtEmail.Text,
                    Convert.ToInt16(this.Session["empresa"]),
                    DateTime.Now,
                    ddlTercero.SelectedValue,
                    ddlPais.SelectedValue,
                    txtTelefono.Text
                };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("cxcCliente", operacion, "ppa", objValores))
                    {
                        case 0:
                            if (Convert.ToBoolean(this.ViewState["editar"]) == true)
                            {
                                foreach (GridViewRow registro in this.gvClaseIR.Rows)
                                {
                                    if (proveedor.VerificaClaseIR(txtCodigo.Text, Convert.ToInt16(ddlTercero.SelectedValue), Convert.ToInt32(registro.Cells[1].Text), Convert.ToInt16(Session["empresa"])) == 0)
                                    {
                                        if (((CheckBox)registro.FindControl("chkSelect")).Checked != true)
                                        {
                                            object[] objValoresElimina = new object[]{
                                            Convert.ToInt16(registro.Cells[1].Text),
                                            txtCodigo.Text,
                                            Convert.ToInt16(Session["empresa"]),
                                            Convert.ToInt16(ddlTercero.SelectedValue)
                                        };

                                            switch (CentidadMetodos.EntidadInsertUpdateDelete("cxcClienteClaseIR", "elimina", "ppa", objValoresElimina))
                                            {
                                                case 1:
                                                    verificacion = true;
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            string concepto;
                                            string llave;

                                            if (((DropDownList)registro.FindControl("ddlConcepto")).SelectedValue == "")
                                                concepto = null;
                                            else
                                                concepto = ((DropDownList)registro.FindControl("ddlConcepto")).SelectedValue;

                                            if (((DropDownList)registro.FindControl("ddlLlave")).SelectedValue == "")
                                                llave = null;
                                            else
                                                llave = ((DropDownList)registro.FindControl("ddlLlave")).SelectedValue;

                                            object[] objValoresClase = new object[]{
                                                       Convert.ToInt16(registro.Cells[1].Text),          //  @clase	int
                                                       txtCodigo.Text,        //@proveedor	varchar
                                                       Convert.ToInt16(Session["empresa"]),          //@empresa	int
                                                        llave,        //@llaveIR	varchar
                                                        Convert.ToInt16(ddlTercero.SelectedValue)  ,       //@tercero	int
                                                        concepto    //@valor	varchar
                                               };

                                            switch (CentidadMetodos.EntidadInsertUpdateDelete("cxcClienteClaseIR", "actualiza", "ppa", objValoresClase))
                                            {
                                                case 1:
                                                    verificacion = true;
                                                    break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (((CheckBox)registro.FindControl("chkSelect")).Checked == true)
                                        {
                                            string concepto;
                                            string llave;

                                            if (((DropDownList)registro.FindControl("ddlConcepto")).SelectedValue == "")
                                                concepto = null;
                                            else
                                                concepto = ((DropDownList)registro.FindControl("ddlConcepto")).SelectedValue;

                                            if (((DropDownList)registro.FindControl("ddlLlave")).SelectedValue == "")
                                                llave = null;
                                            else
                                                llave = ((DropDownList)registro.FindControl("ddlLlave")).SelectedValue;

                                            object[] objValoresClase = new object[]{
                                                       Convert.ToInt16(registro.Cells[1].Text),          //  @clase	int
                                                         txtCodigo.Text,        //@proveedor	varchar
                                                       Convert.ToInt16(Session["empresa"]),          //@empresa	int
                                                        llave,        //@llaveIR	varchar
                                                        Convert.ToInt16(ddlTercero.SelectedValue)  ,       //@tercero	int
                                                        concepto    //@valor	varchar
                                               };

                                            switch (CentidadMetodos.EntidadInsertUpdateDelete("cxcClienteClaseIR", "inserta", "ppa", objValoresClase))
                                            {
                                                case 1:
                                                    verificacion = true;
                                                    break;
                                            }

                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach (GridViewRow registro in this.gvClaseIR.Rows)
                                {
                                    if (((CheckBox)registro.FindControl("chkSelect")).Checked == true)
                                    {
                                        string concepto;
                                        string llave;

                                        if (((DropDownList)registro.FindControl("ddlConcepto")).SelectedValue == "")
                                            concepto = null;
                                        else
                                            concepto = ((DropDownList)registro.FindControl("ddlConcepto")).SelectedValue;

                                        if (((DropDownList)registro.FindControl("ddlLlave")).SelectedValue == "")
                                            llave = null;
                                        else
                                            llave = ((DropDownList)registro.FindControl("ddlLlave")).SelectedValue;

                                        object[] objValoresClase = new object[]{
                                        Convert.ToInt16(registro.Cells[1].Text),
                                        txtCodigo.Text,
                                        Convert.ToInt16(Session["empresa"]),
                                        llave,
                                        Convert.ToInt16(ddlTercero.SelectedValue),
                                        concepto
                                    };

                                        switch (CentidadMetodos.EntidadInsertUpdateDelete("cxcClienteClaseIR", "inserta", "ppa", objValoresClase))
                                        {
                                            case 1:
                                                verificacion = true;
                                                break;
                                        }

                                    }
                                }
                            }
                            break;
                        case 1:
                            verificacion = true;
                            break;
                    }

                    if (verificacion == true)
                        this.nilblInformacion.Text = "Error al insertar el registro. operación no realizada";
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
                ciudad.RowFilter = "pais =" + ddlPais.SelectedValue;
                this.ddlMunicipio.DataSource = ciudad;
                this.ddlMunicipio.DataValueField = "codigo";
                this.ddlMunicipio.DataTextField = "descripcion";
                this.ddlMunicipio.DataBind();
                this.ddlMunicipio.Items.Insert(0, new ListItem("", ""));
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
                ciudad.RowFilter = "departamento ='" + ddlMunicipio.SelectedValue + "'";
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
            InhabilitarControles(this.formContainer.Controls);
            LimpiarControles(this.formContainer.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            gvClaseIR.Visible = false;
            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";

        }
        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            InhabilitarControles(this.formContainer.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),//sitio
                                 nombrePaginaActual(), editar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", editar);
                return;
            }
            HabilitarControles(this.formContainer.Controls);
            this.nilbNuevo.Visible = false;
            this.ViewState["editar"] = true;
            this.ddlTercero.Enabled = false;
            this.txtDescripcion.Focus();
            txtCodigo.Enabled = false;

            try
            {
                CargarCombos();

                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.ddlTercero.SelectedValue = this.gvLista.SelectedRow.Cells[2].Text;
                else
                    this.ddlTercero.SelectedValue = "";



                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                {
                    txtCodigo.Text = this.gvLista.SelectedRow.Cells[3].Text;
                    ValidaRegistro();
                }
                else
                    txtCodigo.Text = "";

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                    txtDescripcion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[4].Text);
                else
                    txtDescripcion.Text = "";

                if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                    ddlClaseCliente.SelectedValue = this.gvLista.SelectedRow.Cells[5].Text;
                else
                    ddlClaseCliente.SelectedValue = "";
                int i = 7;
                if (this.gvLista.SelectedRow.Cells[i].Text != "&nbsp;")
                    txtContacto.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[i].Text);
                else
                    txtContacto.Text = "";

                if (this.gvLista.SelectedRow.Cells[13].Text != "&nbsp;")
                {
                    ddlPais.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[13].Text);
                    cargarMunicipio();
                }
                if (this.gvLista.SelectedRow.Cells[14].Text != "&nbsp;")
                {
                    ddlMunicipio.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[14].Text);
                    cargarciudad();
                }

                i++;
                if (this.gvLista.SelectedRow.Cells[i].Text.Trim() != "")
                    ddlCiudad.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[i].Text.Trim());
                else
                    ddlCiudad.SelectedValue = "";
                i = 10;
                if (this.gvLista.SelectedRow.Cells[i].Text != "&nbsp;")
                    txtDireccion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[i].Text);
                else
                    txtDireccion.Text = "";
                i++;
                if (this.gvLista.SelectedRow.Cells[i].Text != "&nbsp;")
                    txtTelefono.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[i].Text);
                else
                    txtTelefono.Text = "";
                i++;
                if (this.gvLista.SelectedRow.Cells[i].Text != "&nbsp;")
                    txtEmail.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[i].Text);
                i = 15;
                foreach (Control c in gvLista.SelectedRow.Cells[i].Controls)
                {
                    if (c is CheckBox)
                        chkActivo.Checked = ((CheckBox)c).Checked;
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

                if (proveedor.EliminaClaseCliente(Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[3].Text), Convert.ToInt16(this.gvLista.Rows[e.RowIndex].Cells[2].Text), Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    object[] objValores = new object[] {Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[3].Text),
                Convert.ToInt16(this.Session["empresa"]),Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text)};

                    if (CentidadMetodos.EntidadInsertUpdateDelete("cxcCliente", operacion, "ppa", objValores) == 0)
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
        private void ValidaRegistro()
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


                            if (cconfigclase.ManejaLlaveClase(Convert.ToInt16(registro.Cells[1].Text.Trim()), Convert.ToInt16(this.Session["empresa"]), ddlTercero.SelectedValue.Trim()) == 1)
                            {
                                ((DropDownList)registro.FindControl("ddlLLave")).Enabled = true;
                                DataView dvLlave = llave.BuscarEntidad("", Convert.ToInt16(this.Session["empresa"]));
                                dvLlave.RowFilter = "clase=" + registro.Cells[1].Text.Trim() + "and empresa=" + this.Session["empresa"].ToString();
                                ((DropDownList)registro.FindControl("ddlLLave")).DataSource = dvLlave;
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

                    }

                }
            }
        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Length == 0 || txtDescripcion.Text.Length == 0 || txtContacto.Text.Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Campos vacios por favor corrija", "warning");
                return;
            }

            if (ddlClaseCliente.SelectedIndex.ToString().Trim().Length == 0 || ddlCiudad.SelectedIndex.ToString().Trim().Length == 0 || ddlTercero.SelectedIndex.ToString().Trim().Length == 0)
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
            HabilitarControles(this.formContainer.Controls);
            LimpiarControles(this.formContainer.Controls);
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
                    ((DropDownList)this.gvClaseIR.Rows[e.RowIndex].FindControl("ddlConcepto")).DataSource = cconfigclase.ValoresClasesConfig(Convert.ToInt16(gvClaseIR.Rows[e.RowIndex].Cells[0].Text), Convert.ToInt16(Session["empresa"]));
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
                    ((DropDownList)gvr.FindControl("ddlLLave")).DataSource = llave.BuscarEntidad("", Convert.ToInt16(this.Session["empresa"]));
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
        protected void ddlMunicipio_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarciudad();

        }
        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarMunicipio();
        }

        #endregion Eventos



    }
}