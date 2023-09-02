using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Pentidades
{
    public partial class AFC : BasePage
    {
        #region Instancias



        cParametrosGenerales parametrosGenerales = new cParametrosGenerales();


        Cafc afc = new Cafc();

        #endregion Instancias

        #region Metodos


        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                                        ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "C", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                this.gvLista.DataSource = afc.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(
                    this.Session["usuario"].ToString(), "C",
                  ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                    this.gvLista.Rows.Count.ToString() + " Registros encontrados", ObtenerIP(), Convert.ToInt16(Session["empresa"]));

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente a: " + ex.Message, "C");
            }
        }

        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");

            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;

            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion,
                ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "ex", mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));

            GetEntidad();
        }
        private void CargarCombos()
        {

            try
            {
                DataView dvterceros = CentidadMetodos.EntidadGet("cTercero", "ppa").Tables[0].DefaultView;
                dvterceros.RowFilter = "empresa = " + Convert.ToInt32(this.Session["empresa"]).ToString() + " and activo=1 and proveedor=1";
                dvterceros.Sort = "descripcion";
                this.ddlTercero.DataSource = dvterceros;
                this.ddlTercero.DataValueField = "id";
                this.ddlTercero.DataTextField = "RazonSocial";
                this.ddlTercero.DataBind();
                this.ddlTercero.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tercero. Correspondiente a: " + ex.Message, "C");
            }


            try
            {

                this.ddlCodigoNacional.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("gEntidadNacional", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"]));
                this.ddlCodigoNacional.DataValueField = "codigo";
                this.ddlCodigoNacional.DataTextField = "descripcion";
                this.ddlCodigoNacional.DataBind();
                this.ddlCodigoNacional.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tercero. Correspondiente a: " + ex.Message, "C");
            }
        }
        private void cargarProveedor()
        {
            try
            {
                DataView proveedor = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cxpProveedor", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"])); ;
                proveedor.RowFilter = "idTercero=" + ddlTercero.SelectedValue.ToString();
                this.ddlProveedor.DataSource = proveedor;
                this.ddlProveedor.DataValueField = "codigo";
                this.ddlProveedor.DataTextField = "descripcion";
                this.ddlProveedor.DataBind();
                this.ddlProveedor.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar proveedor. Correspondiente a: " + ex.Message, "C");
            }
        }

        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text.Trim().ToString(), Convert.ToInt16(Session["empresa"]) };
            try
            {
                if (CentidadMetodos.EntidadGetKey("nEntidadAfc", "ppa", objKey).Tables[0].Rows.Count > 0)
                {

                    MostrarMensaje("Código " + this.txtCodigo.Text + " ya se encuentra registrado");

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
            string operacion = "inserta", codigoNacional = null;


            if (this.txtDescripcion.Text.Length == 0 || this.txtCodigo.Text.Length == 0)
            {
                MostrarMensaje("Campos vacios por favor corrija");
            }
            else
            {
                try
                {

                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                    {
                        operacion = "actualiza";
                    }

                    if (ddlCodigoNacional.SelectedValue.Length == 0)
                    {
                        codigoNacional = null;
                    }
                    else
                    {
                        codigoNacional = ddlCodigoNacional.SelectedValue;
                    }

                    string cuenta = null;

                    if (ddlCuenta.SelectedValue.Length > 0)
                        cuenta = ddlCuenta.SelectedValue;


                    object[] objValores = new object[]{
                    chkActivo.Checked,
                      txtCodigo.Text,
                      codigoNacional,
                      cuenta,
                    this.txtDescripcion.Text,
                   Convert.ToInt16(Session["empresa"]),
                   DateTime.Now,
                   txtObservacion.Text,
                   ddlProveedor.SelectedValue,
                   ddlTercero.SelectedValue,
                   Convert.ToString(Session["usuario"])

                };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete(
                        "nEntidadAfc",
                        operacion,
                        "ppa",
                        objValores))
                    {
                        case 0:

                            ManejoExito("Datos insertados satisfactoriamente", operacion.Substring(0, 1).ToUpper());
                            break;

                        case 1:

                            ManejoError("Errores al insertar el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ManejoError("Error al guardar los datos correspondiente a: " + ex.Message, operacion.Substring(0, 1).ToUpper());
                }
            }
        }

        #endregion Metodos

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
            {
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            }
            else
            {
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(),
                    ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) != 0)
                {


                    this.txtCodigo.Focus();

                }
                else
                {
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
                }
            }
        }



        protected void lbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                                 ConfigurationManager.AppSettings["Modulo"].ToString(),
                                  nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            CcontrolesUsuario.HabilitarControles(
                this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;

            CargarCombos();

            txtCodigo.Focus();

            this.nilblInformacion.Text = "";
        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(
                this.Page.Controls);

            CcontrolesUsuario.LimpiarControles(
                this.Page.Controls);

            this.gvLista.DataSource = null;
            this.gvLista.DataBind();

            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
        }

        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
            txtDescripcion.Focus();
        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                                        ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                object[] objValores = new object[] {
                Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)),
                Convert.ToInt16(Session["empresa"])
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete(
                    "nEntidadAfc",
                    "elimina",
                    "ppa",
                    objValores))
                {
                    case 0:

                        ManejoExito("Registro eliminado satisfactoriamente", "E");
                        break;

                    case 1:

                        ManejoError("Error al eliminar el registro. Operación no realizada", "E");
                        break;
                }
            }
            catch (Exception ex)
            {
                if (ex.HResult.ToString() == "-2146233087")
                {
                    ManejoError("El código ('" + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)) +
                    "' - ' " + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[3].Text)) + "') tiene una asociación, no es posible eliminar el registro.", "E");
                }
                else
                {
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
                }
            }
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {

            if (txtCodigo.Text.Trim().ToString().Length == 0 || txtDescripcion.Text.Length == 0 || ddlTercero.SelectedValue.Length == 0 || ddlProveedor.SelectedValue.Length == 0)
            {
                MostrarMensaje("Campos vacios por favor corrija");
                return;
            }

            Guardar();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (seguridad.VerificaAccesoOperacion(
             this.Session["usuario"].ToString(),
             ConfigurationManager.AppSettings["Modulo"].ToString(),
              nombrePaginaActual(),
             "A", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "A");
                return;
            }


            CcontrolesUsuario.HabilitarControles(
             this.Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;

            this.txtCodigo.Enabled = false;
            this.txtDescripcion.Focus();
            CargarCombos();
            try
            {
                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                {
                    this.txtCodigo.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[2].Text);
                }
                else
                {
                    this.txtCodigo.Text = "";
                }

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                {
                    this.txtDescripcion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[3].Text);
                }
                else
                {
                    this.txtDescripcion.Text = "";
                }

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                {
                    this.ddlTercero.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[4].Text);
                    cargarProveedor();
                }

                if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                {
                    this.ddlProveedor.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[5].Text);
                }

                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                {
                    this.ddlCodigoNacional.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[6].Text);
                }


                if (this.gvLista.SelectedRow.Cells[7].Text != "&nbsp;")
                {
                    this.txtObservacion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[7].Text);
                }
                else
                {
                    this.txtObservacion.Text = "";
                }



                foreach (Control objControl in this.gvLista.SelectedRow.Cells[8].Controls)
                {
                    if (objControl is CheckBox)
                    {
                        this.chkActivo.Checked = ((CheckBox)objControl).Checked;
                    }
                }

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(
                this.Page.Controls);

            this.nilbNuevo.Visible = true;

            GetEntidad();
        }

        protected void gvLista_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }

        protected void ddlTercero_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarProveedor();
        }




        #endregion Eventos

    }

}