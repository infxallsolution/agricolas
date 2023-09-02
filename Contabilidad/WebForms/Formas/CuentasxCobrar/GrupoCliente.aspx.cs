using Contabilidad.seguridadInfos;
using Contabilidad.WebForms.App_Code.CuentasxCobrar;
using Contabilidad.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.CuentasxCobrar
{
    public partial class GrupoCliente : BasePage
    {

        #region Instancias
        Security seguridad = new Security();
        CIP ip = new CIP();
        CgrupoCliente grupooProveedor = new CgrupoCliente();

        #endregion Instancias

        #region Metodos

        protected string nombrePaginaActual()
        {
            string[] arrResult = HttpContext.Current.Request.RawUrl.Split('/');
            string result = arrResult[arrResult.GetUpperBound(0)];
            arrResult = result.Split('?');
            return arrResult[arrResult.GetLowerBound(0)];
        }
        static string limpiarMensaje(string strIn)
        {
            try
            {
                return Regex.Replace(strIn, @"[^\w\.@-]", " ",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
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
                this.gvLista.DataSource = grupooProveedor.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));

                this.gvLista.DataBind();

                this.nilblInformacion.Visible = true;
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                            this.gvLista.Rows.Count.ToString() + " Registros encontrados", ObtenerIP(), Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente a: " + limpiarMensaje(ex.Message), "C");
            }
        }

        private void ManejoError(string error, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), error, "error");
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "er", error, ObtenerIP(), Convert.ToInt16(Session["empresa"]));
        }

        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.nibtnNuevo.Visible = true;
            this.selFuncionarios.Visible = false;
            this.nibtnNuevo.Visible = true;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                 "ex", mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }
        private void CargarCombos()
        {
            try
            {
                DataView dvFuncionario = grupooProveedor.SeleccionaProveedoresSinGrupo(Convert.ToInt16(Session["empresa"]));
                this.selFuncionarios.DataSource = dvFuncionario;
                this.selFuncionarios.DataValueField = "id";
                this.selFuncionarios.DataTextField = "descripcion";
                this.selFuncionarios.DataBind();

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar funcionarios. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
            }
        }

        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text, Convert.ToInt16(Session["empresa"]) };

            try
            {
                if (CentidadMetodos.EntidadGetKey("cxcGrupoCliente", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    this.nilblInformacion.Text = "Cuadrilla " + this.txtCodigo.Text + " ya se encuentra registrada";
                    CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
                    this.nibtnNuevo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al validar la llave primaria correspondiente a: " + limpiarMensaje(ex.Message), "C");
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
                        CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar al menos un funcionario para realizar la asignación", "warning");
                        return;
                    }

                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                        operacion = "actualiza";

                    object[] objValores = new object[]{
                this.txtCodigo.Text,
                this.txtDescripcion.Text,
                Convert.ToInt16(Session["empresa"])
            };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("cxcGrupoCliente", operacion, "ppa", objValores))
                    {
                        case 0:
                            if (Convert.ToBoolean(this.Session["editar"]) == true)
                            {
                                for (int x = 0; x < this.selFuncionarios.Items.Count; x++)
                                {
                                    if (grupooProveedor.VerificaProveedorGrupo(txtCodigo.Text, Convert.ToInt32(this.selFuncionarios.Items[x].Value), Convert.ToInt16(Session["empresa"])) == 0)
                                    {
                                        if (this.selFuncionarios.Items[x].Selected == false)
                                        {

                                            object[] objValoresConcepto = new object[] { Convert.ToInt16(this.selFuncionarios.Items[x].Value), Convert.ToInt16(Session["empresa"]), Convert.ToString(txtCodigo.Text) };

                                            switch (CentidadMetodos.EntidadInsertUpdateDelete("cxcGrupoClientedetalle", "elimina", "ppa", objValoresConcepto))
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
                                            object[] objValoresConcepto = new object[] { Convert.ToInt16(this.selFuncionarios.Items[x].Value), Convert.ToInt16(Session["empresa"]), Convert.ToString(txtCodigo.Text) };

                                            switch (CentidadMetodos.EntidadInsertUpdateDelete("cxcGrupoClientedetalle", "inserta", "ppa", objValoresConcepto))
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
                                        if (grupooProveedor.VerificaProveedorGrupo(txtCodigo.Text, Convert.ToInt32(this.selFuncionarios.Items[x].Value), Convert.ToInt16(Session["empresa"])) == 1)
                                        {

                                            object[] objValoresConcepto = new object[] { Convert.ToInt16(this.selFuncionarios.Items[x].Value), Convert.ToInt16(Session["empresa"]), Convert.ToString(txtCodigo.Text) };

                                            switch (CentidadMetodos.EntidadInsertUpdateDelete("cxcGrupoClientedetalle", operacion, "ppa", objValoresConcepto))
                                            {
                                                case 1:
                                                    verificacion = true;
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            CerroresGeneral.ManejoError(this, GetType(), "El proveedor " + selFuncionarios.Items[x].Text + "ya se encuentra en un grupo", "warning");
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
                        CerroresGeneral.ManejoError(this, GetType(), "Error al insertar el registro. operación no realizada", "e");
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
                ManejoError("Error al guardar los datos correspondiente a: " + limpiarMensaje(ex.Message), operacion.Substring(0, 1).ToUpper());
            }
        }

        private void ValidaRegistro()
        {
            for (int x = 0; x < this.selFuncionarios.Items.Count; x++)
            {
                if (grupooProveedor.VerificaProveedorGrupo(txtCodigo.Text, Convert.ToInt32(selFuncionarios.Items[x].Value), Convert.ToInt16(Session["empresa"])) == 0)
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
                    this.txtCodigo.Focus();
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

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + limpiarMensaje(ex.Message), "C");
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


                object[] objValores = new object[] { Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text), Convert.ToInt16(Session["empresa"]) };

                if (grupooProveedor.EliminaProveedor(Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text), Convert.ToInt16(Session["empresa"])) == 0)
                {
                    if (CentidadMetodos.EntidadInsertUpdateDelete("cxcGrupoCliente", operacion, "ppa", objValores) == 0)
                        ManejoExito("Registro eliminado satisfactoriamente", "E");
                }
                else
                    ManejoError("Errores al eliminar el registro. Operación no realizada", "E");

            }
            catch (Exception ex)
            {
                if (ex.HResult.ToString() == "-2146233087")
                {
                    ManejoError("El código ('" + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)) +
                    "' - ' " + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[3].Text)) + "') tiene una asociación, no es posible eliminar el registro.", "E");
                }
                else
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + limpiarMensaje(ex.Message), "E");
            }
        }
        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
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
            this.selFuncionarios.Visible = false;
        }
        protected void nibtnGuardar_Click(object sender, EventArgs e)
        {
            if (txtDescripcion.Text.Length == 0 || txtCodigo.Text.Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Campos vacios por favor corrija", "warning");
                return;
            }
            Guardar();
        }

        #endregion Eventos

    }
}