using Microsoft.Reporting.Map.WebForms.BingMaps;
using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
using Nomina.WebForms.App_Code.Programacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Pprogramacion
{
    public partial class Cuadrillas : BasePage
    {

        #region Instancias


        Ccuadrillas cuadrillas = new Ccuadrillas();
        Cdepartamentos departamentos = new Cdepartamentos();

        #endregion Instancias

        #region Metodos


        private void Consecutivo()
        {
            try
            {
                this.txtCodigo.Text = cuadrillas.Consecutivo(Convert.ToString(this.ddlDepartamento.SelectedValue), Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "C", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }
                selFuncionarios.Visible = false;
                this.gvLista.DataSource = cuadrillas.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));

                this.gvLista.DataBind();


                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                            this.gvLista.Rows.Count.ToString() + " Registros encontrados", HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.nibtnNuevo.Visible = true;
            this.ddlDepartamento.Enabled = true;
            this.selFuncionarios.Visible = false;
            this.nibtnNuevo.Visible = true;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                 "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }
        private void CargarCombos()
        {
            try
            {
                this.ddlDepartamento.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nDepartamento", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlDepartamento.DataValueField = "codigo";
                this.ddlDepartamento.DataTextField = "cadena";
                this.ddlDepartamento.DataBind();
                this.ddlDepartamento.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }

            try
            {
                DataView dvFuncionario = cuadrillas.SeleccionaFuncionariosSinCuadrilla(Convert.ToInt16(Session["empresa"]));
                this.selFuncionarios.DataSource = dvFuncionario;
                this.selFuncionarios.DataValueField = "codigo";
                this.selFuncionarios.DataTextField = "descripcion";
                this.selFuncionarios.DataBind();

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text, Convert.ToInt16(Session["empresa"]) };

            try
            {
                if (CentidadMetodos.EntidadGetKey("nCuadrilla", "ppa", objKey).Tables[0].Rows.Count > 0)
                {

                    this.nilblInformacion.Text = "Cuadrilla " + this.txtCodigo.Text + " ya se encuentra registrada";
                    CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
                    this.nibtnNuevo.Visible = true;
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

                using (TransactionScope ts = new TransactionScope())
                {
                    for (int x = 0; x < this.selFuncionarios.Items.Count; x++)
                    {
                        if (this.selFuncionarios.Items[x].Selected)
                            verificacion = true;
                    }
                    if (verificacion == false)
                    {
                        ManejoError("Debe seleccionar al menos un funcionario para realizar la asignación", "error");
                        return;
                    }

                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                        operacion = "actualiza";
                    else
                        Consecutivo();

                    object[] objValores = new object[]{
                chkActivo.Checked,
                this.txtCodigo.Text,
                Convert.ToString(this.ddlDepartamento.SelectedValue),
                this.txtDescripcion.Text,
                Convert.ToInt16(Session["empresa"])
                    };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("nCuadrilla", operacion, "ppa", objValores))
                    {
                        case 0:
                            if (Convert.ToBoolean(this.Session["editar"]) == true)
                            {
                                for (int x = 0; x < this.selFuncionarios.Items.Count; x++)
                                {
                                    if (cuadrillas.VerificaFuncionarioCuadrilla(txtCodigo.Text, Convert.ToString(this.selFuncionarios.Items[x].Value), Convert.ToInt16(Session["empresa"])) == 0)
                                    {
                                        if (this.selFuncionarios.Items[x].Selected == false)
                                        {

                                            object[] objValoresConcepto = new object[]{
                                                        Convert.ToString(txtCodigo.Text),
                                                        Convert.ToInt16(Session["empresa"]),
                                                        Convert.ToString(this.selFuncionarios.Items[x].Value)  };

                                            switch (CentidadMetodos.EntidadInsertUpdateDelete("nCuadrillaFuncionario", "elimina", "ppa", objValoresConcepto))
                                            {
                                                case 1:
                                                    verificacion = true;
                                                    break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (this.selFuncionarios.Items[x].Selected == true)
                                        {

                                            object[] objValoresConcepto = new object[]{
                                                        Convert.ToString(txtCodigo.Text),
                                                        Convert.ToInt16(Session["empresa"]),
                                                        Convert.ToString(this.selFuncionarios.Items[x].Value)  };

                                            switch (CentidadMetodos.EntidadInsertUpdateDelete("nCuadrillaFuncionario", "inserta", "ppa", objValoresConcepto))
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
                                for (int x = 0; x < this.selFuncionarios.Items.Count; x++)
                                {
                                    if (this.selFuncionarios.Items[x].Selected == true)
                                    {
                                        if (cuadrillas.VerificaFuncionarioCuadrilla(txtCodigo.Text, Convert.ToString(this.selFuncionarios.Items[x].Value), Convert.ToInt16(Session["empresa"])) == 1)
                                        {

                                            object[] objValoresConcepto = new object[]{
                                                       Convert.ToString(txtCodigo.Text),
                                                        Convert.ToInt16(Session["empresa"]),
                                                        Convert.ToString(this.selFuncionarios.Items[x].Value)  };

                                            switch (CentidadMetodos.EntidadInsertUpdateDelete("nCuadrillaFuncionario", operacion, "ppa", objValoresConcepto))
                                            {
                                                case 1:
                                                    verificacion = true;
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            nilblInformacion.Text += "El empleado " + selFuncionarios.Items[x].Text + "ya se encuentra en una cuadrilla";
                                            verificacion = true;
                                        }
                                    }
                                }
                            }

                            break;

                        case 1:
                            verificacion = false;
                            break;
                    }

                    if (verificacion == false)
                    {
                        this.nilblInformacion.Text = "Error al insertar el registro. operación no realizada";
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
            for (int x = 0; x < this.selFuncionarios.Items.Count; x++)
            {
                if (cuadrillas.VerificaFuncionarioCuadrilla(txtCodigo.Text, Convert.ToString(selFuncionarios.Items[x].Value), Convert.ToInt16(Session["empresa"])) == 0)
                    selFuncionarios.Items[x].Selected = true;
                else
                    selFuncionarios.Items[x].Selected = false;
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
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) != 0)
                {
                    this.ddlDepartamento.Focus();
                    if (this.txtCodigo.Text.Length > 0)
                        this.txtDescripcion.Focus();
                }
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "A", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "A");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            this.selFuncionarios.Visible = true;
            CargarCombos();
            this.nibtnNuevo.Visible = false;
            this.Session["editar"] = true;
            this.ddlDepartamento.Enabled = false;
            this.txtCodigo.Enabled = false;
            this.txtDescripcion.Focus();

            try
            {
                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                {
                    this.txtCodigo.Text = this.gvLista.SelectedRow.Cells[2].Text.Trim();
                    ValidaRegistro();
                }
                else
                    this.txtCodigo.Text = "";

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    this.txtDescripcion.Text = this.gvLista.SelectedRow.Cells[3].Text;
                else
                    this.txtDescripcion.Text = "";

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                    this.ddlDepartamento.SelectedValue = this.gvLista.SelectedRow.Cells[4].Text;


                foreach (Control objControl in this.gvLista.SelectedRow.Cells[6].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkActivo.Checked = ((CheckBox)objControl).Checked;
                }


            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string operacion = "elimina";

            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                           nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }


                object[] objValores = new object[] {
                Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text),
                Convert.ToInt16(Session["empresa"])
            };

                if (cuadrillas.EliminaFunncionarios(Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text), Convert.ToInt16(Session["empresa"])) == 0)
                {
                    if (CentidadMetodos.EntidadInsertUpdateDelete("nCuadrilla", operacion, "ppa", objValores) == 0)
                    {
                        ManejoExito("Registro eliminado satisfactoriamente", "E");
                    }
                }
                else
                {
                    ManejoError("Errores al eliminar el registro. Operación no realizada", "E");

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
                    ManejoErrorCatch(ex);
            }
        }
        protected void nilblListado_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListadoDestinos.aspx");
        }
        protected void niddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            Consecutivo();
            txtDescripcion.Focus();


        }
        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
        }
        protected void nilbRegresar_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("Programacion.aspx");
        }
        protected void gvAsignacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gvLista.PageIndex = e.NewPageIndex;
            CargarCombos();
            gvLista.DataBind();
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
        protected void nibtnBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nibtnNuevo.Visible = true;
            GetEntidad();
        }
        protected void nibtnNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                              nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);
            CargarCombos();
            this.nibtnNuevo.Visible = false;
            this.Session["editar"] = false;
            txtCodigo.Text = "";
            this.txtCodigo.Enabled = false;
            this.ddlDepartamento.Focus();
            this.nilblInformacion.Text = "";
            this.selFuncionarios.Visible = true;
        }
        protected void nibtnCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.nibtnNuevo.Visible = true;
            this.nilblInformacion.Text = "";
            this.ddlDepartamento.Enabled = true;
            this.selFuncionarios.Visible = false;
        }
        protected void nibtnGuardar_Click(object sender, EventArgs e)
        {
            if (txtDescripcion.Text.Length == 0 || txtCodigo.Text.Length == 0)
            {
                MostrarMensaje("Campos vacios por favor corrija");
                return;
            }
            Guardar();
        }

        #endregion Eventos

    }
}