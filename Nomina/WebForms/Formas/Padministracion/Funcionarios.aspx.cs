using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Padministracion
{
    public partial class Funcionarios : BasePage
    {
        #region Instancias

        Cterceros terceros = new Cterceros();
        Cfuncionarios funcionarios = new Cfuncionarios();

        #endregion Instancias

        #region Metodos


        private void GetNombreTercero(string tercero)
        {
            try
            {
                string[] nombreTercero = funcionarios.RetornaNombreTercero(tercero, Convert.ToInt32(Session["empresa"]));
                this.txtDescripcion.Text = Convert.ToString(nombreTercero.GetValue(2));
                this.txtIdentificacion.Text = Convert.ToString(nombreTercero.GetValue(1));
            }
            catch (Exception ex)
            {
                ManejoError("Error al recuperar el nombre del tercero. Correspondiente a: " + ex.Message, "C");
            }
        }

        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "C", Convert.ToInt32(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                this.gvLista.DataSource = funcionarios.BuscarEntidad(this.nitxtBusqueda.Text, Convert.ToInt32(Session["empresa"]));
                this.gvLista.DataBind();
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";
                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                             this.gvLista.Rows.Count.ToString() + " Registros encontrados", ObtenerIP(), Convert.ToInt32(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente a: " + ex.Message, "C");
            }
        }


        private void ManejoExito(string mensaje, string operacion)
        {
            nilblInformacion.Text = mensaje;
            nilblInformacion.ForeColor = System.Drawing.Color.Green;
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            CcontrolesUsuario.InhabilitarControles(this.pnTercero.Controls);
            CcontrolesUsuario.LimpiarControles(this.pnTercero.Controls);
            this.nilbNuevo.Visible = true;
            this.pnTercero.Visible = false;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "ex", mensaje, ObtenerIP(), Convert.ToInt32(Session["empresa"]));
            GetEntidad();
        }

        private void CargarCombos()
        {
            try
            {
                this.ddlTercero.DataSource = CcontrolesUsuario.OrdenarEntidadTercero(CentidadMetodos.EntidadGet("cTercero", "ppa"), "razonSocial", "empleado", Convert.ToInt32(Session["empresa"]));
                this.ddlTercero.DataValueField = "id";
                this.ddlTercero.DataTextField = "razonSocial";
                this.ddlTercero.DataBind();
                this.ddlTercero.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar empleados. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlPais.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("gPais", "ppa"), "descripcion", Convert.ToInt32(this.Session["empresa"]));
                this.ddlPais.DataValueField = "codigo";
                this.ddlPais.DataTextField = "descripcion";
                this.ddlPais.DataBind();
                this.ddlPais.Items.Insert(0, new ListItem("", ""));

                ddlDepartamento.DataSource = null;
                ddlDepartamento.DataBind();

                ddlCiudad.DataSource = null;
                ddlCiudad.DataBind();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar país. Correspondiente a: " + ex.Message, "C");
            }


            try
            {
                DataView tipodocumento = CentidadMetodos.EntidadGet("gTipoDocumento", "ppa").Tables[0].DefaultView;
                tipodocumento.RowFilter = "empresa =" + this.Session["empresa"].ToString();
                this.ddlTipoID.DataSource = tipodocumento;
                this.ddlTipoID.DataValueField = "codigo";
                this.ddlTipoID.DataTextField = "descripcion";
                this.ddlTipoID.DataBind();
                this.ddlTipoID.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tipos de documento. Correspondiente a: " + ex.Message, "C");
            }
            cargarComboxDetalle();
        }

        private void EntidadKey()
        {
            object[] objKey = new object[] { Convert.ToInt32(Session["empresa"]), Convert.ToInt32(this.ddlTercero.SelectedValue) };
            try
            {
                if (CentidadMetodos.EntidadGetKey("nFuncionario", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    MostrarMensaje("Funcionario " + Convert.ToString(this.ddlTercero.SelectedValue) + " ya se encuentra registrado");
                    CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
                    this.nilbNuevo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al validar la llave primaria correspondiente a: " + ex.Message, "C");
            }
        }

        private void Guardar()
        {
            string operacion = "inserta", cliente = null, proveedor = null, nivelEducativo = null;
            string operacionTer = "inserta";
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {


                    if (pnTercero.Visible == true)
                    {
                        int id = 0;
                        object nit = null, dv = null, contacto = null, telefono = null, direccion = null, barrio = null;
                        object fax = null, email = null, razonSocial = null, descripcion = null, identificacion = null;


                        if (Convert.ToBoolean(this.Session["editar"]) == true)
                        {
                            operacionTer = "actualiza";
                            id = Convert.ToInt32(this.Session["id"]);
                        }
                        else
                        {
                            switch (terceros.RetornaCodigoTercero(Convert.ToInt32(Convert.ToDecimal(txtCodigo.Text)).ToString(), Convert.ToInt32(Session["empresa"])))
                            {
                                case 1:
                                    ManejoError("Código de usuario existente", "I");
                                    return;
                            }
                            id = terceros.RetornaConsecutivoIdtercero(Convert.ToInt32(Session["empresa"]));
                        }

                        descripcion = txtApellido1.Text.Trim() + " " + txtApellido2.Text.Trim() + " " + txtNombre1.Text.Trim() + " " + txtNombre2.Text.Trim();
                        razonSocial = txtApellido1.Text.Trim() + " " + txtApellido2.Text.Trim() + " " + txtNombre1.Text.Trim() + " " + txtNombre2.Text.Trim();

                        if (ddlTipoID.SelectedValue.ToString().Trim().Length == 0)
                        {
                            CcontrolesUsuario.MensajeError("Seleccione un tipo de documento valido", nilblInformacion);
                            return;
                        }

                        if (txtDocumento.Text.Trim().Length == 0 || txtApellido1.Text.Trim().Length == 0 || txtNombre1.Text.Trim().Length == 0 || txtDireccion.Text.Trim().Length == 0 || ddlCiudad.SelectedValue.Length == 0)
                        {
                            CcontrolesUsuario.MensajeError("Campos vacios por favor corrija", nilblInformacion);
                            return;
                        }

                        object[] objValores = new object[] {
                                        false,//@accionista
                                        true,//@activo
                                        txtApellido1.Text,//@apellido1
                                        txtApellido2.Text,//@apellido2
                                        "",//@barrio
                                        txtTelefono.Text,//@celular
                                        null,//@ciiu
                                        ddlCiudad.SelectedValue,//@cidudad
                                        false,//@cliente
                                        Convert.ToInt32(Convert.ToDecimal(txtCodigo.Text)).ToString(),//@codigo
                                        false,//@comercializadora
                                        "",//@contacto
                                       false,//@contratista
                                       ddlDepartamento.SelectedValue,
                                        descripcion,//@descripcion
                                        txtDireccion.Text,//@direccion
                                        dv,//@dv
                                        null,//@email
                                        true,//@empleado
                                        Convert.ToInt32(this.Session["empresa"]),//@empresa
                                        false,   //extractora
                                        DateTime.Now,//@fechaRegistro
                                        null,//@foto
                                        id,//@id
                                        txtDocumento.Text,//@nit
                                        txtNombre1.Text,//@nombre1
                                        txtNombre2.Text,//@nombre2
                                        ddlPais.SelectedValue,
                                        false,//@proveedor
                                        razonSocial,//@razonSocial
                                        txtTelefono.Text,//@telefono
                                        "1",//@tipo
                                        ddlTipoID.SelectedValue,//@tipoDocumento
                                        Session["usuario"].ToString()
            };
                        switch (CentidadMetodos.EntidadInsertUpdateDelete("cTercero", operacionTer, "ppa", objValores))
                        {
                            case 0:

                                if (Convert.ToBoolean(this.Session["editar"]) == true)
                                    operacion = "actualiza";

                                if (ddlCliente.SelectedValue.Length == 0)
                                    cliente = null;
                                else
                                    cliente = ddlCliente.SelectedValue;
                                if (ddlProveedor.SelectedValue.Length == 0)
                                    proveedor = null;
                                else
                                    proveedor = ddlProveedor.SelectedValue;



                                if (pnTercero.Visible == true)
                                    identificacion = txtDocumento.Text.Trim();
                                else
                                    identificacion = txtIdentificacion.Text.Trim();

                                object[] objValores1 = new object[]{
                                this.chkActivo.Checked,
                                cliente,
                                identificacion,
                                chkConductor.Checked,
                                chkContratista.Checked,
                                descripcion,
                                Convert.ToInt32(Session["empresa"]),
                                chkOperador.Checked,
                                chkOtros.Checked,
                                proveedor,
                                Convert.ToInt32(id),
                                this.chkValidaTurno.Checked
                                };
                                switch (CentidadMetodos.EntidadInsertUpdateDelete("nFuncionario", operacion, "ppa", objValores1))
                                {
                                    case 0:
                                        ManejoExito("Datos insertados satisfactoriamente", operacion.Substring(0, 1).ToUpper());
                                        ts.Complete();
                                        break;
                                    case 1:
                                        ManejoError("Errores al insertar el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                                        break;
                                }
                                break;
                            case 1:
                                ManejoError("Errores al insertar el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                                break;
                        }
                    }
                    else
                    {
                        if (Convert.ToBoolean(this.Session["editar"]) == true)
                            operacion = "actualiza";

                        if (ddlCliente.SelectedValue.Length == 0)
                            cliente = null;
                        else
                            cliente = ddlCliente.SelectedValue;
                        if (ddlProveedor.SelectedValue.Length == 0)
                            proveedor = null;
                        else
                            proveedor = ddlProveedor.SelectedValue;


                        object[] objValores = new object[]{
                            this.chkActivo.Checked,
                            cliente,
                            txtIdentificacion.Text,
                            chkConductor.Checked,
                            chkContratista.Checked,
                            txtDescripcion.Text,
                            Convert.ToInt32(Session["empresa"]),
                            chkOperador.Checked,
                            chkOtros.Checked,
                            proveedor,
                            Convert.ToInt32(this.ddlTercero.SelectedValue),
                            chkValidaTurno.Checked
                            };

                        switch (CentidadMetodos.EntidadInsertUpdateDelete("nFuncionario", operacion, "ppa", objValores))
                        {
                            case 0:
                                ManejoExito("Datos insertados satisfactoriamente", operacion.Substring(0, 1).ToUpper());
                                ts.Complete();
                                break;
                            case 1:
                                ManejoError("Errores al insertar el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                                break;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar los datos correspondiente a: " + ex.Message, operacion.Substring(0, 1).ToUpper());
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
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(),
                   ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt32(Session["empresa"])) != 0)
                    this.nitxtBusqueda.Focus();
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }

        protected void lbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                   nombrePaginaActual(), "I", Convert.ToInt32(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);
            CcontrolesUsuario.InhabilitarControles(this.pnTercero.Controls);
            CcontrolesUsuario.LimpiarControles(this.pnTercero.Controls);
            foreach (Control c in pnTercero.Controls)
                CcontrolesUsuario.LimpiarControles(c.Controls);
            CcontrolesUsuario.HabilitarControles(this.pnTercero.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            CargarCombos();
            this.ddlTercero.Enabled = true;
            this.ddlTercero.Focus();
            txtDescripcion.Text = "";
            txtIdentificacion.Text = "";
            this.nilblInformacion.Text = "";
            this.txtDescripcion.Enabled = false;
            this.txtIdentificacion.Enabled = false;
            chkManejaTercero.Text = "Crea tercero";
            lbFechaContratistaHasta.Visible = false;
            txtFechaContratista.Visible = false;
        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            foreach (Control c in pnTercero.Controls)
                CcontrolesUsuario.LimpiarControles(c.Controls);
            this.pnTercero.Visible = false;
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
            chkManejaTercero.Text = "Crea tercero";
        }

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (pnTercero.Visible == false)
            {
                if (txtDescripcion.Text.Length == 0 || ddlTercero.SelectedValue.Length == 0)
                {
                    CcontrolesUsuario.MensajeError("Campos vacios por favor corrija", nilblInformacion);
                    return;
                }
            }
            if (chkContratista.Checked == true)
            {
                if (ddlProveedor.SelectedValue.Trim().Length == 0)
                {
                    CcontrolesUsuario.MensajeError("Debe seleccionar un proveedor si es contratista", nilblInformacion);
                    return;
                }
            }
            Guardar();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
            nombrePaginaActual(), "A", Convert.ToInt32(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "A");
                return;
            }
            CcontrolesUsuario.LimpiarControles(pnTercero.Controls);
            pnTercero.Visible = false;
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            this.nilblInformacion.Text = "";
            this.ddlTercero.Enabled = false;
            this.txtDescripcion.Enabled = false;
            this.txtIdentificacion.Enabled = false;
            this.ddlTercero.Enabled = false;

            try
            {
                CargarCombos();
                DataView dvTercero = terceros.RetornaDatosTercero(this.gvLista.SelectedRow.Cells[3].Text.Trim(), Convert.ToInt32(Session["empresa"]));
                chkManejaTercero.Text = "Actualiza tercero";

                if (dvTercero.Table.Rows[0].ItemArray.GetValue(1) != null)
                    this.Session["id"] = dvTercero.Table.Rows[0].ItemArray.GetValue(1).ToString();
                else
                    this.Session["id"] = null;



                if (dvTercero.Table.Rows.Count > 0)
                {
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(1) != null)
                    {
                        txtCodigo.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(2).ToString();
                        txtCodigo.Enabled = false;
                    }
                    else
                        txtCodigo.Text = "";

                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(2) != null)
                        txtDocumento.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(2).ToString();
                    else
                        txtDocumento.Text = "";

                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(3) != null)
                        ddlTipoID.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(3).ToString().Trim();
                    else
                        ddlTipoID.SelectedValue = "";

                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(8) != null)
                        txtApellido1.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(8).ToString();
                    else
                        txtApellido1.Text = "";

                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(9) != null)
                        txtApellido2.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(9).ToString();
                    else
                        txtApellido2.Text = "";

                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(10) != null)
                        txtNombre1.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(10).ToString();
                    else
                        txtNombre1.Text = "";

                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(11) != null)
                        txtNombre2.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(11).ToString();
                    else
                        txtNombre2.Text = "";

                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(25) != null)
                        txtDireccion.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(25).ToString();
                    else
                        txtDireccion.Text = "";

                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(24) != null)
                        txtTelefono.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(24).ToString();
                    else
                        txtTelefono.Text = "";

                    if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    {
                        this.ddlTercero.SelectedValue = this.gvLista.SelectedRow.Cells[3].Text.Trim();
                        GetNombreTercero(this.gvLista.SelectedRow.Cells[3].Text);
                    }

                    if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                        this.ddlProveedor.SelectedValue = this.gvLista.SelectedRow.Cells[5].Text;

                    if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                        this.ddlCliente.SelectedValue = this.gvLista.SelectedRow.Cells[6].Text;

                    foreach (Control objControl in this.gvLista.SelectedRow.Cells[7].Controls)
                    {
                        if (objControl is CheckBox)
                            this.chkValidaTurno.Checked = ((CheckBox)objControl).Checked;
                    }

                    foreach (Control objControl in this.gvLista.SelectedRow.Cells[8].Controls)
                    {
                        if (objControl is CheckBox)
                            this.chkConductor.Checked = ((CheckBox)objControl).Checked;
                    }

                    foreach (Control objControl in this.gvLista.SelectedRow.Cells[9].Controls)
                    {
                        if (objControl is CheckBox)
                            this.chkOperador.Checked = ((CheckBox)objControl).Checked;
                    }

                    foreach (Control objControl in this.gvLista.SelectedRow.Cells[10].Controls)
                    {
                        if (objControl is CheckBox)
                            this.chkContratista.Checked = ((CheckBox)objControl).Checked;
                    }

                    foreach (Control objControl in this.gvLista.SelectedRow.Cells[11].Controls)
                    {
                        if (objControl is CheckBox)
                            this.chkActivo.Checked = ((CheckBox)objControl).Checked;
                    }

                    foreach (Control objControl in this.gvLista.SelectedRow.Cells[12].Controls)
                    {
                        if (objControl is CheckBox)
                            this.chkOtros.Checked = ((CheckBox)objControl).Checked;
                    }
                }

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "E", Convert.ToInt32(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }
            string operacion = "elimina";
            try
            {
                object[] objValores = new object[] { Convert.ToInt32(Session["empresa"]), Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[3].Text) };

                if (CentidadMetodos.EntidadInsertUpdateDelete("nFuncionario", operacion, "ppa", objValores) == 0)
                    ManejoExito("Datos eliminados satisfactoriamente", "E");
                else
                    ManejoError("Errores al eliminar el registro. Operación no realizada", "E");
            }
            catch (Exception ex)
            {
                ManejoError("Error al eliminar los datos correspondiente a: " + ex.Message, "E");
            }
        }

        protected void ddlFuncionario_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntidadKey();
            GetNombreTercero(Convert.ToString(this.ddlTercero.SelectedValue));
        }

        private void cargarComboxDetalle()
        {
            try
            {
                DataView proveedor = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cxpProveedor", "ppa"), "descripcion", Convert.ToInt32(this.Session["empresa"])); ;
                this.ddlProveedor.DataSource = proveedor;
                this.ddlProveedor.DataValueField = "idTercero";
                this.ddlProveedor.DataTextField = "descripcion";
                this.ddlProveedor.DataBind();
                this.ddlProveedor.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar proveedor. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                DataView proveedor = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cxcCliente", "ppa"), "descripcion", Convert.ToInt32(this.Session["empresa"])); ;
                this.ddlCliente.DataSource = proveedor;
                this.ddlCliente.DataValueField = "idTercero";
                this.ddlCliente.DataTextField = "descripcion";
                this.ddlCliente.DataBind();
                this.ddlCliente.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar cliente. Correspondiente a: " + ex.Message, "C");
            }
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }


        protected void chkManejaTercero_CheckedChanged1(object sender, EventArgs e)
        {
            foreach (Control c in pnTercero.Controls)
                CcontrolesUsuario.LimpiarControles(c.Controls);
            manejoPanel();
        }

        private void manejoPanel()
        {
            if (chkManejaTercero.Checked == true)
            {
                pnTercero.Visible = true;
                ddlTercero.Visible = false;
                lblTercero.Visible = false;
                txtIdentificacion.Visible = false;
                txtDescripcion.Visible = false;
                lblIdentifiacion.Visible = false;
                lblDescripcion.Visible = false;
                //ddlTipoID.SelectedValue = "13";
                //ddlTipoID.Enabled = false;
            }
            else
            {
                pnTercero.Visible = false;
                lblTercero.Visible = true;
                txtIdentificacion.Visible = true;
                txtDescripcion.Visible = true;
                lblIdentifiacion.Visible = true;
                lblDescripcion.Visible = true;
                ddlTercero.Visible = true;
            }
        }

        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                switch (terceros.RetornaCodigoTercero(Convert.ToInt32(Convert.ToDecimal(txtCodigo.Text)).ToString(), Convert.ToInt32(Session["empresa"])))
                {
                    case 1:
                        ManejoError("Codigo usuario existente", "C");
                        break;
                    case 0:
                        nilblInformacion.Text = "";
                        txtDocumento.Text = Convert.ToInt32(Convert.ToDecimal(txtCodigo.Text)).ToString();
                        break;
                }
                txtDocumento.Focus();
            }
            catch (Exception ex)
            {
                ManejoError("Error al validar usuario debido a:" + ex.Message, "C");
            }
        }




        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarCiudad(ddlDepartamento.SelectedValue);
        }

        private void cargarCiudad(string departamento)
        {
            try
            {
                DataView ciudad = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("gCiudad", "ppa"), "descripcion", Convert.ToInt32(this.Session["empresa"]));
                ciudad.RowFilter = "empresa =" + this.Session["empresa"].ToString() + " and departamento='" + departamento + "'";
                this.ddlCiudad.DataSource = ciudad;
                this.ddlCiudad.DataValueField = "codigo";
                this.ddlCiudad.DataTextField = "descripcion";
                this.ddlCiudad.DataBind();
                this.ddlCiudad.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar ciudades. Correspondiente a: " + ex.Message, "C");
            }
        }
        protected void chkContratista_CheckedChanged(object sender, EventArgs e)
        {
            if (chkContratista.Checked)
                cargarComboxDetalle();
            else
                validarContratistas();
        }


        private void validarContratistas()
        {
            switch (funcionarios.ValidaFuncionarioContratista(Convert.ToInt16(ddlTercero.SelectedValue), Convert.ToInt16(Session["empresa"])))
            {
                case 0:
                    ddlProveedor.Enabled = true;
                    lbFechaContratistaHasta.Visible = false;
                    txtFechaContratista.Visible = false;
                    break;
                case 1:
                    ddlProveedor.Enabled = false;
                    lbFechaContratistaHasta.Visible = true;
                    txtFechaContratista.Visible = true;
                    break;
                case 2:
                    ddlProveedor.Enabled = true;
                    lbFechaContratistaHasta.Visible = false;
                    txtFechaContratista.Visible = false;
                    break;
            }
        }
        protected void txtFechaContratista_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFechaContratista.Text);
            }
            catch
            {
                nilblInformacion.Text = "Formato de fecha no valido";
                txtFechaContratista.Text = "";
                txtFechaContratista.Focus();
                return;
            }

        }

        private void cargarDepartamento(string pais)
        {
            try
            {
                DataView ciudad = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("gDepartamento", "ppa"), "descripcion", Convert.ToInt32(this.Session["empresa"]));
                ciudad.RowFilter = "empresa =" + this.Session["empresa"].ToString() + " and pais='" + pais + "'";
                this.ddlDepartamento.DataSource = ciudad;
                this.ddlDepartamento.DataValueField = "codigo";
                this.ddlDepartamento.DataTextField = "descripcion";
                this.ddlDepartamento.DataBind();
                this.ddlDepartamento.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar departamentos. Correspondiente a: " + ex.Message, "C");
            }
        }

        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarDepartamento(ddlPais.SelectedValue);
        }

        #endregion Eventos
    }
}