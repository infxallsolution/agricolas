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
    public partial class Terceros : BasePage
    {

        #region Instancias

        Centidades entidades = new Centidades();
        Cterceros terceros = new Cterceros();

        #endregion Instancias

        #region Metodos

        private void GetEntidad()
        {
            try
            {

                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(), consulta,//operacion
                       Convert.ToInt32(this.Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", consulta);
                    return;
                }
                CcontrolesUsuario.InhabilitarControles(Page.Controls);
                this.gvLista.Visible = true;
                this.nilblInformacion.Visible = true;
                this.nilbNuevo.Visible = true;
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";
                this.nilblMensaje.Text = "";

                this.gvLista.DataSource = terceros.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt32(this.Session["empresa"]));
                this.gvLista.DataBind();
                manejoGrilla(false);
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente a: " + ex.Message, "C");
            }
        }
        protected void manejoGrilla(bool manejo)
        {

            for (int x = 0; x < gvLista.Columns.Count; x++)
            {
                if (x >= 13)
                    gvLista.Columns[x].Visible = manejo;
            }





        }
        void ManejoErrorCatch(Exception exepcion)
        {
            CerroresGeneral.ManejoErrorCatch(Page, Page.GetType(), exepcion);

        }
        void ManejoErrorAcceso(string error, string operacion)
        {
            this.Session["error"] = error;
            this.Session["paginaAnterior"] = this.Page.Request.FilePath.ToString();

            if (this.User.Identity.IsAuthenticated)
            {
                seguridad.InsertaLog(
                      this.User.Identity.Name.ToString(),
                      operacion,
                       ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                      "er",
                       error,
                      HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
                this.Response.Redirect("~/Formas/Error.aspx", false);
            }
        }

        void MostrarMensaje(string message)
        {
            CerroresGeneral.ManejoError(this, GetType(), message, "warning");
        }
        private void ManejoError(string error, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), error, "error");
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "er", error, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
        }

        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");

            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);

            this.nilbNuevo.Visible = true;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }

        protected string nombrePaginaActual()
        {
            string[] arrResult = HttpContext.Current.Request.RawUrl.Split('/');
            string result = arrResult[arrResult.GetUpperBound(0)];
            arrResult = result.Split('?');
            return arrResult[arrResult.GetLowerBound(0)];
        }
        private void CargarCombos()
        {
            try
            {
                this.ddlTipoID.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("gTipoDocumento", "ppa"), "descripcion", Convert.ToInt32(this.Session["empresa"]));
                this.ddlTipoID.DataValueField = "codigo";
                this.ddlTipoID.DataTextField = "descripcion";
                this.ddlTipoID.DataBind();
                this.ddlTipoID.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tipos de documento. Correspondiente a: " + ex.Message, "C");
            }


            try
            {
                this.ddlPais.DataSource = CentidadMetodos.EntidadGet("gPais", "ppa").Tables[0].DefaultView;
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
        }
        private void EntidadKey()
        {
            object[] objKey = new object[] { Convert.ToDecimal(txtCodigo.Text).ToString(), (int)this.Session["empresa"] };

            try
            {
                if (CentidadMetodos.EntidadGetKey(
                    "cTercero",
                    "ppa",
                    objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    this.nilblInformacion.Text = "El codigo: " + " " + Convert.ToDecimal(txtCodigo.Text).ToString() +
                        " ya se encuentra registrado";

                    CcontrolesUsuario.InhabilitarControles(
                        this.Page.Controls);

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
            string operacion = "inserta";

            try
            {
                int id = 0;
                object dv = null;
                object ciudad = null, departamento = null, pais = null;
                object foto = null;
                object contacto = null;
                object telefono = null;
                object direccion = null;
                object barrio = null;
                object fax = null;
                object email = null;
                object razonSocial = null;

                id = terceros.RetornaConsecutivoIdtercero(Convert.ToInt32(Session["empresa"]));

                if (Convert.ToBoolean(this.Session["editar"]) == true)
                {
                    operacion = "actualiza";
                    id = Convert.ToInt32(this.Session["id"]);
                }
                else
                    EntidadKey();


                if (txtDv.Text.Trim().Length > 0)
                    dv = txtDv.Text;

                if (ddlPais.SelectedValue.Trim().Length > 0)
                    pais = ddlPais.SelectedValue.ToString();

                if (ddlDepartamento.SelectedValue.Trim().Length > 0)
                    departamento = ddlDepartamento.SelectedValue.ToString();
                if (ddlCiudad.SelectedValue.Trim().Length > 0)
                    ciudad = ddlCiudad.SelectedValue.ToString();

                if (txtContacto.Text.Trim().Length > 0)
                    contacto = txtContacto.Text;

                if (txtTelefono.Text.Trim().Length > 0)
                    telefono = txtTelefono.Text;

                if (txtDireccion.Text.Trim().Length > 0)
                    direccion = txtDireccion.Text;

                if (txtBarrio.Text.Trim().Length > 0)
                    barrio = txtBarrio.Text;

                if (txtCelular.Text.Trim().Length > 0)
                    fax = txtCelular.Text;

                if (txtEmail.Text.Trim().Length > 0)
                    email = txtEmail.Text;

                if (!txtRazonSocial.Visible)
                    razonSocial = txtDescripcion.Text;
                else
                    razonSocial = txtRazonSocial.Text;

                using (TransactionScope ts = new TransactionScope())
                {

                    object[] objValores = new object[] {
                                        false,//@accionista
                                        chkActivo.Checked,//@activo
                                        txtApellido1.Text,//@apellido1
                                        txtApellido2.Text,//@apellido2
                                        txtBarrio.Text,//@barrio
                                        txtCelular.Text,
                                        ddlCiiu.SelectedValue,
                                        ciudad,//@cidudad
                                        false,//@cliente
                                        txtCodigo.Text,//@codigo
                                       false,
                                        txtContacto.Text,//@contacto
                                        false,//@contratista
                                        departamento,
                                        txtDescripcion.Text,//@descripcion
                                        txtDireccion.Text,//@direccion
                                        dv,//@dv
                                        txtEmail.Text,//@email
                                        true,//@empleado
                                        Convert.ToInt32(this.Session["empresa"]),//@empresa
                                        false,   //extractora
                                        DateTime.Now,//@fechaRegistro
                                        foto,//@foto
                                        id,//@id
                                        txtDocumento.Text,//@nit
                                        txtNombre1.Text,//@nombre1
                                        txtNombre2.Text,//@nombre2
                                        pais,
                                        false,//@proveedor
                                        razonSocial,//@razonSocial
                                        txtTelefono.Text,//@telefono
                                        "1",//@tipo
                                        ddlTipoID.SelectedValue,//@tipoDocumento
                                        Session["usuario"].ToString()
            };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("cTercero", operacion, "ppa", objValores))
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
            catch (Exception ex)
            {
                ManejoError("Error al guardar los datos correspondiente a: " + ex.Message, operacion.Substring(0, 1).ToUpper());
            }
        }
        private void ComportamientoInicialNombres()
        {
            this.txtNombre1.Visible = false;
            this.txtNombre2.Visible = false;
            this.txtApellido1.Visible = false;
            this.txtApellido2.Visible = false;
            this.txtRazonSocial.Visible = false;
            this.lblPrimerNombre.Visible = false;
            this.lblSegundoNombre.Visible = false;
            this.lblPrimerApellido.Visible = false;
            this.lblSegundoApellido.Visible = false;
            this.lblRazonSocial.Visible = false;
        }
        protected void cargarNuevo()
        {
            ComportamientoInicialNombres();
            CargarCombos();
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;

            this.txtCodigo.Enabled = true;
            this.txtCodigo.Focus();
            this.nilblInformacion.Text = "";
            comportamientoCampos();
            this.gvLista.Visible = false;
            this.nilblMensaje.Text = "";
            txtCodigo.Focus();
            this.Session["gvConsulta"] = null;
            comportamientoCampos();
        }
        private void cargarCiudad(string departamento)
        {
            try
            {
                DataView ciudad = CentidadMetodos.EntidadGet("gCiudad", "ppa").Tables[0].DefaultView;
                ciudad.RowFilter = "departamento='" + departamento + "'";
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

        private void cargarDepartamento(string pais)
        {
            try
            {
                DataView ciudad = CentidadMetodos.EntidadGet("gDepartamento", "ppa").Tables[0].DefaultView;
                ciudad.RowFilter = "pais='" + pais + "'";
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
        protected void comportamientoCampos()
        {
            txtDescripcion.Text = "";
            txtNombre1.Text = "";
            txtNombre2.Text = "";
            txtApellido2.Text = "";
            txtApellido1.Text = "";
            txtRazonSocial.Text = "";


            this.txtNombre1.Visible = true;
            this.txtNombre2.Visible = true;
            this.txtApellido1.Visible = true;
            this.txtApellido2.Visible = true;
            this.txtRazonSocial.Visible = true;
            this.lblPrimerNombre.Visible = true;
            this.lblSegundoNombre.Visible = true;
            this.lblPrimerApellido.Visible = true;
            this.lblSegundoApellido.Visible = true;
            this.lblRazonSocial.Visible = false;
            this.txtRazonSocial.Visible = false;
            this.txtRazonSocial.Enabled = false;
            this.txtDv.Visible = true;
            this.txtDv.Enabled = false;
            this.txtDocumento.Focus();

        }

        #endregion Metodos

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt32(this.Session["empresa"])) != 0)
                {
                    this.nitxtBusqueda.Focus();


                    if (!IsPostBack)
                    {
                        CargarCombos();
                        CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
                    }

                    if (this.txtCodigo.Text.Length > 0)
                        this.txtDescripcion.Focus();
                }
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", ingreso);
            }
        }
        protected void ddlEntidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            CcontrolesUsuario.OpcionesDefault(this.Page.Controls, 0);
            this.nilblMensaje.Text = "";
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                 nombrePaginaActual(), editar, Convert.ToInt32(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", editar);
                return;
            }

            CcontrolesUsuario.LimpiarControles(this.Controls);
            cargarNuevo();
            this.Session["editar"] = true;
            try
            {
                CargarCombos();
                DataView dvTercero = terceros.RetornaDatosTercero(this.gvLista.SelectedRow.Cells[2].Text, (int)this.Session["empresa"]);
                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                {
                    this.txtCodigo.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[3].Text);
                    //this.txtCodigo.Enabled = false;
                }

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                    ddlTipoID.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[4].Text.Trim());

                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                    txtDocumento.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[6].Text);

                if (this.gvLista.SelectedRow.Cells[7].Text != "&nbsp;")
                    txtDv.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[7].Text);

                if (dvTercero.Table.Rows[0].ItemArray.GetValue(7) != null)
                    txtRazonSocial.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(7).ToString();

                if (dvTercero.Table.Rows[0].ItemArray.GetValue(8) != null)
                    txtApellido1.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(8).ToString();

                if (dvTercero.Table.Rows[0].ItemArray.GetValue(9) != null)
                    txtApellido2.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(9).ToString();

                if (dvTercero.Table.Rows[0].ItemArray.GetValue(10) != null)
                    txtNombre1.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(10).ToString();

                if (dvTercero.Table.Rows[0].ItemArray.GetValue(11) != null)
                    txtNombre2.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(11).ToString();


                if (dvTercero.Table.Rows[0].ItemArray.GetValue(12) != null)
                {
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(12).ToString().Trim().Length > 0)
                        txtDescripcion.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(12).ToString();
                }

                int fila = 13;
                if (dvTercero.Table.Rows[0].ItemArray.GetValue(fila) != null)
                {
                    try
                    {
                        ddlPais.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(fila).ToString();
                        cargarDepartamento(ddlPais.SelectedValue);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                fila += 1;
                if (dvTercero.Table.Rows[0].ItemArray.GetValue(fila) != null)
                {
                    try
                    {
                        ddlDepartamento.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(fila).ToString();
                        cargarCiudad(ddlDepartamento.SelectedValue);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                fila += 1;
                if (dvTercero.Table.Rows[0].ItemArray.GetValue(fila) != null)
                {
                    try
                    {
                        ddlCiudad.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(fila).ToString();
                    }
                    catch (Exception ex)
                    {
                    }
                }

                fila += 1;
                chkActivo.Checked = Convert.ToBoolean(dvTercero.Table.Rows[0].ItemArray.GetValue(fila));
                fila += 1;
                fila += 1;
                fila += 1;
                fila += 1;
                fila += 1;
                fila += 1;
                fila += 1;
                fila += 2;
                if (dvTercero.Table.Rows[0].ItemArray.GetValue(fila) != null)
                    txtContacto.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(fila).ToString();
                fila += 1;
                if (dvTercero.Table.Rows[0].ItemArray.GetValue(fila) != null)
                    txtTelefono.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(fila).ToString();
                fila += 1;
                if (dvTercero.Table.Rows[0].ItemArray.GetValue(fila) != null)
                    txtCelular.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(fila).ToString();
                fila += 1;
                if (dvTercero.Table.Rows[0].ItemArray.GetValue(fila) != null)
                    txtDireccion.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(fila).ToString();
                fila += 1;
                if (dvTercero.Table.Rows[0].ItemArray.GetValue(fila) != null)
                    txtBarrio.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(fila).ToString();
                fila += 1;
                if (dvTercero.Table.Rows[0].ItemArray.GetValue(fila) != null)
                    txtEmail.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(fila).ToString();
                fila += 1;
                if (dvTercero.Table.Rows[0].ItemArray.GetValue(fila) != null)
                    ddlCiiu.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(fila).ToString();

                if (dvTercero.Table.Rows[0].ItemArray.GetValue(1) != null)
                    this.Session["id"] = dvTercero.Table.Rows[0].ItemArray.GetValue(1).ToString();

                this.txtNombre1.Visible = true;
                this.txtNombre2.Visible = true;
                this.txtApellido1.Visible = true;
                this.txtApellido2.Visible = true;
                this.txtRazonSocial.Visible = false;
                lblRazonSocial.Visible = false;
                this.lblPrimerNombre.Visible = true;
                this.lblSegundoNombre.Visible = true;
                this.lblPrimerApellido.Visible = true;
                this.lblSegundoApellido.Visible = true;
                this.txtDv.Visible = true;
                this.txtDv.Enabled = false;
                this.txtDocumento.Focus();

                gvLista.Visible = false;
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }

        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string operacion = "elimina";

            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(),
                                eliminar, Convert.ToInt32(this.Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", eliminar);
                    return;
                }


                object[] objValores = new object[] { Convert.ToInt32(this.Session["empresa"]), Convert.ToInt32(this.gvLista.Rows[e.RowIndex].Cells[2].Text.Trim()) };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("cTercero", operacion, "ppa", objValores))
                {
                    case 0:
                        ManejoExito("Datos eliminados satisfactoriamente", "E");
                        break;
                    case 1:
                        ManejoError("Errores al eliminar el registro. Operación no realizada", "E");
                        break;
                    case 2:
                        ManejoError("El tercero tiene movimientos contables no es posible su eliminación", operacion.Substring(0, 1).ToUpper());
                        break;
                    case 3:
                        ManejoError("El tercero se encuentra asociado como proveedor, no se puede eliminar", operacion.Substring(0, 1).ToUpper());
                        break;
                    case 4:
                        ManejoError("El tercero se encuentra asociado como cliente, no se  puede eliminar", operacion.Substring(0, 1).ToUpper());
                        break;
                    case 5:
                        ManejoError("El tercero tiene movimientos de despacho no se puede eliminar", operacion.Substring(0, 1).ToUpper());
                        break;
                    case 6:
                        ManejoError("El tercero tiene movimientos en la nómina no se puede eliminar", operacion.Substring(0, 1).ToUpper());
                        break;

                }
            }
            catch (Exception ex)
            {
                if (ex.HResult.ToString() == "-2146233087")
                {
                    ManejoError("El código ('" + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)) +
                    "' - ' " + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[3].Text)) + "') tiene una asociación, no es posible eliminar el registro.  debido a: " + ex.Message, "E");
                }
                else
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.HResult.ToString() + ex.Message, "E");
            }
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
        protected void rbTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            comportamientoCampos();
        }
        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            switch (terceros.RetornaCodigoTercero(txtCodigo.Text, (int)this.Session["empresa"]))
            {
                case 1:
                    ManejoError("Codigo de Tercero existente por favor corrija", "I");
                    break;

                case 0:
                    nilblMensaje.Text = "";
                    txtDocumento.Text = Convert.ToDecimal(txtCodigo.Text).ToString();
                    break;
            }
            ddlTipoID.Focus();
        }
        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarCiudad(ddlDepartamento.SelectedValue);
        }
        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            GetEntidad();
        }
        protected void nilbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(), insertar, Convert.ToInt32(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", insertar);
                return;
            }
            cargarNuevo();
        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
            this.nilblMensaje.Text = "";
            this.Session["gvConsulta"] = null;

        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDouble(txtCodigo.Text);
            }
            catch (Exception ex)
            {
                this.nilblMensaje.Text = "Solo numeros campo codigo";
                return;
            }


            if (Convert.ToBoolean(this.Session["editar"]) == false)
            {
                switch (terceros.RetornaCodigoTercero(Convert.ToDecimal(txtCodigo.Text).ToString(), Convert.ToInt32(this.Session["empresa"])))
                {
                    case 1:
                        ManejoError("Código de usuario existente", "I");
                        return;
                }
            }
            if (ddlTipoID.SelectedValue.ToString().Trim().Length == 0)
            {
                this.nilblMensaje.Text = "Seleccione un tipo de documento valido";
                return;
            }

            if (txtDocumento.Text.Trim().Length == 0 || txtApellido1.Text.Trim().Length == 0
                || txtNombre1.Text.Trim().Length == 0

                || txtDescripcion.Text.Trim().Length == 0)
            {
                this.nilblMensaje.Text = "Ingrese el documento, el nombre, y la descripción";
                return;
            }



            Guardar();
        }

        #endregion Eventos


        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarDepartamento(ddlPais.SelectedValue);
        }

        protected void ddlTipoID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (terceros.RetornaTipoDocumentoNit(ddlTipoID.SelectedValue, Convert.ToInt32(this.Session["empresa"])) == 1)

                    if (terceros.RetornaTipoDocumentoNit(ddlTipoID.SelectedValue, Convert.ToInt32(this.Session["empresa"])) == 1)

                    {
                        txtDv.Enabled = true;
                        txtDv.Text = "";
                    }
                    else
                    {
                        txtDv.Enabled = false;
                        txtDv.Text = "";
                    }
            }
            catch (Exception ex)

            {
                ManejoError("Error al cargar si maneja nit", "I");
            }
        }
    }
}