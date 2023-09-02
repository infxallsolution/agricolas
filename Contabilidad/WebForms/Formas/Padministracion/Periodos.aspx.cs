using Contabilidad.WebForms.App_Code.Administracion;
using Contabilidad.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.Padministracion
{
    public partial class Periodos : BasePage
    {
        #region Instancias
        Cperiodos periodos = new Cperiodos();
        #endregion Instancias

        #region Metodos

        private void OpcionesDefecto()
        {
            this.nilblOperacion.Text = "";
            this.nilblOperacion.Visible = false;
            this.niddlAno.Visible = false;
            this.nilbEjecutar.Visible = false;
            this.nilblCancelar.Visible = false;
            this.nitxtAno.Visible = false;
            this.nichkCerrarAño.Visible = false;
        }

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
                this.gvLista.DataSource = periodos.BuscarEntidad(this.nitxtBusqueda.Text, Convert.ToInt16(this.Session["empresa"]));
                this.gvLista.DataBind();
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";
                seguridad.InsertaLog(this.Session["usuario"].ToString(), consulta, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
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
            nibtnAbrir.Visible = true;
            nibtnEliminar.Visible = true;
            nibtnGenerar.Visible = true;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                   "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(this.Session["empresa"]));
            GetEntidad();
        }

        private void CargarCombos()
        {
            try
            {
                this.niddlAno.DataSource = periodos.GetAnosPeriodos(Convert.ToInt16(Session["empresa"]));
                this.niddlAno.DataValueField = "año";
                this.niddlAno.DataTextField = "año";
                this.niddlAno.DataBind();
                this.niddlAno.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        private void EntidadKey()
        {
            if (txvAño.Text.Length == 0 || ddlMes.Text.Length == 0)
                return;

            object[] objKey = new object[] { Convert.ToDecimal(this.txvAño.Text), Convert.ToInt16(this.Session["empresa"]), ddlMes.Text };
            try
            {
                if (CentidadMetodos.EntidadGetKey("cPeriodo", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    CerroresGeneral.ManejoError(this, GetType(), "Periodo " + this.txvAño.Text + " ya se encuentra registrado", "warning");
                    InhabilitarControles(this.formContainer.Controls);
                    this.nilbNuevo.Visible = true;
                    this.txvAño.Text = "";
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
            DateTime fechaInicial = new DateTime(), fechaFinal = new DateTime();

            try
            {
                Convert.ToDecimal(this.txvAño.Text);
            }
            catch
            {
                CerroresGeneral.ManejoError(this, GetType(), "Año debe ser un numero", "warning");
                return;
            }
            try
            {
                fechaFinal = Convert.ToDateTime(txtFechaFinal.Text);
                fechaInicial = Convert.ToDateTime(txtFechaIni.Text);

            }
            catch
            {
                CerroresGeneral.ManejoError(this, GetType(), "Formato de fecha no validas por favor corrija", "warning");
                return;

            }

            if (txtFechaIni.Text.Length == 0 || txtFechaFinal.Text.Length == 0)

                if (fechaInicial.Year != Convert.ToInt16(txvAño.Text))
                {
                    CerroresGeneral.ManejoError(this, GetType(), "La fecha inicial debe pertenecer al mismo año del seleccionado", "warning");
                    return;
                }

            if (fechaInicial > fechaFinal)
            {
                CerroresGeneral.ManejoError(this, GetType(), "La fecha inicial debe ser menor a la fecha final", "warning");
                return;
            }

            try
            {
                if (Convert.ToBoolean(this.Session["editar"]) == true)
                    operacion = "actualiza";

                object[] objValores = new object[]{
                Convert.ToDecimal(this.txvAño.Text),
                this.chkCerrado.Checked,
                Convert.ToInt16(this.Session["empresa"]),
                Convert.ToDateTime(txtFechaFinal.Text),
                Convert.ToDateTime(txtFechaIni.Text),
                Convert.ToInt16(this.ddlMes.Text )
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("cPeriodo", operacion, "ppa", objValores))
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
                ManejoErrorCatch(ex);
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
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(this.Session["empresa"])) != 0)
                {
                    this.nitxtBusqueda.Focus();
                    this.nitxtBusqueda.Focus();
                    if (this.txvAño.Text.Length > 0)
                        this.chkCerrado.Focus();
                }
                else
                    ManejoErrorAcceso("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }

        protected void lbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                  nombrePaginaActual(), insertar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", insertar);
                return;
            }
            HabilitarControles(this.formContainer.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            CargarCombos();
            this.txvAño.Enabled = true;
            this.txvAño.Focus();
            this.nilblInformacion.Text = "";
            this.niddlAno.Visible = false;
            this.nilbEjecutar.Visible = false;
            this.nilblOperacion.Visible = false;
            this.nilblCancelar.Visible = false;
            this.nitxtAno.Visible = false;
            this.nichkCerrarAño.Visible = false;
            nibtnAbrir.Visible = false;
            nibtnEliminar.Visible = false;
            nibtnGenerar.Visible = false;

        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            InhabilitarControles(this.formContainer.Controls);
            LimpiarControles(this.formContainer.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
            this.nitxtAno.Visible = false;
            this.niddlAno.Visible = false;
            nibtnAbrir.Visible = true;
            nibtnEliminar.Visible = true;
            nibtnGenerar.Visible = true;
        }

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            InhabilitarControles(this.formContainer.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                 nombrePaginaActual(), editar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", editar);
                return;
            }
            HabilitarControles(this.formContainer.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            this.nilblOperacion.Visible = false;
            this.niddlAno.Visible = false;
            this.nitxtAno.Visible = false;
            this.nichkCerrarAño.Visible = false;
            this.nilblCancelar.Visible = false;
            this.nilbEjecutar.Visible = false;
            this.txvAño.Enabled = false;
            ddlMes.Enabled = false;
            this.chkCerrado.Focus();

            try
            {
                CargarCombos();

                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.txvAño.Text = this.gvLista.SelectedRow.Cells[2].Text;
                else
                    this.txvAño.Text = "";

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    this.ddlMes.SelectedValue = this.gvLista.SelectedRow.Cells[3].Text;

                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                    txtFechaIni.Text = Convert.ToDateTime(Server.HtmlDecode(this.gvLista.SelectedRow.Cells[6].Text)).ToShortDateString();

                if (this.gvLista.SelectedRow.Cells[7].Text != "&nbsp;")
                    txtFechaFinal.Text = Convert.ToDateTime(Server.HtmlDecode(this.gvLista.SelectedRow.Cells[7].Text)).ToShortDateString();

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[8].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkCerrado.Checked = ((CheckBox)objControl).Checked;
                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                  nombrePaginaActual(), eliminar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", eliminar);
                return;
            }

            string operacion = "elimina";
            try
            {
                object[] objValores = new object[] { Convert.ToInt32(this.gvLista.Rows[e.RowIndex].Cells[2].Text), Convert.ToInt32(this.Session["empresa"]), Convert.ToInt32(this.gvLista.Rows[e.RowIndex].Cells[3].Text) };

                if (CentidadMetodos.EntidadInsertUpdateDelete("cPeriodo", operacion, "ppa", objValores) == 0)
                    ManejoExito("Datos eliminados satisfactoriamente", "E");
                else
                    ManejoError("Errores al eliminar el registro. Operación no realizada", "E");
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
        }

        protected void nilbAbrirAnos_Click(object sender, EventArgs e)
        {
            this.nilblOperacion.Text = "Seleccione el año que desea abrir";
            this.nilblOperacion.Visible = true;
            this.niddlAno.Visible = true;
            this.divNiddlAno.Visible = true;
            this.nichkCerrarAño.Visible = true;
            this.divChkCerrarAño.Visible = true;

            this.nilbEjecutar.Visible = true;
            this.nilblCancelar.Visible = true;

            this.nitxtAno.Visible = false;
            this.divNitxtAno.Visible = false;

            InhabilitarControles(formContainer.Controls);
            this.ViewState["opcion"] = "abrir";
            CargarCombos();
        }


        protected void nilbEjecutar_Click(object sender, EventArgs e)
        {
            int conteo = 0;
            try
            {
                switch (this.ViewState["opcion"].ToString())
                {
                    case "abrir":
                        if (periodos.AbrirCerrarPeriodosAno(Convert.ToInt16(this.niddlAno.SelectedValue), Convert.ToInt16(this.Session["empresa"]), out conteo, nichkCerrarAño.Checked) == 0)
                        {
                            if (!nichkCerrarAño.Checked)
                                ManejoExito("Peridos abiertos satisfactoriamente. " + conteo.ToString() + " registros afectados", "A");
                            else
                                ManejoExito("Peridos cerrados satisfactoriamente. " + conteo.ToString() + " registros afectados", "A");
                        }
                        else
                            ManejoError("Error al abrir/cerrar los periodos. Operación no realizada", "A");
                        break;
                    case "eliminar":
                        if (periodos.EliminarPeriodosAno(Convert.ToInt16(this.nitxtAno.Text), Convert.ToInt16(this.Session["empresa"])) == 0)
                            ManejoExito("Peridos eliminados satisfactoriamente.", "E");
                        else
                            ManejoError("Error al eliminar los periodos. Operación no realizada", "E");
                        break;
                    case "generar":
                        if (periodos.GenerarPeriodosAno(Convert.ToInt16(this.nitxtAno.Text), Convert.ToInt16(this.Session["empresa"])) == 0)
                            ManejoExito("Peridos generados satisfactoriamente.", "E");
                        else
                            ManejoError("Error al eliminar los periodos. Operación no realizada", "E");
                        break;
                }
                OpcionesDefecto();
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        protected void nilbCerrarPeriodosAno_Click(object sender, EventArgs e)
        {
            this.nilblOperacion.Text = "Seleccione el año que desea cerrar";
            this.nilblOperacion.Visible = true;

            this.divNiddlAno.Visible = true;
            this.niddlAno.Visible = true;
            this.divChkCerrarAño.Visible = true;
            this.nichkCerrarAño.Visible = true;

            this.nilbEjecutar.Visible = true;
            this.nilblCancelar.Visible = true;

            this.divNitxtAno.Visible = false;
            this.nitxtAno.Visible = false;

            this.ViewState["opcion"] = "cerrar";
            CargarCombos();
        }

        protected void lbEliminarPeriodos_Click(object sender, EventArgs e)
        {
            this.nilblOperacion.Text = "Digite el año que desea eliminar";

            this.nilblOperacion.Visible = true;

            this.divNiddlAno.Visible = false;
            this.niddlAno.Visible = false;
            this.nichkCerrarAño.Visible = false;
            this.divChkCerrarAño.Visible = false;

            this.nilbEjecutar.Visible = true;
            this.nilblCancelar.Visible = true;

            this.divNitxtAno.Visible = true;
            this.nitxtAno.Visible = true;

            this.ViewState["opcion"] = "eliminar";
            CargarCombos();
        }

        protected void nilblCancelar_Click(object sender, EventArgs e)
        {
            OpcionesDefecto();
        }

        #endregion Eventos

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }


        protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntidadKey();
        }

        protected void btnAbrir_Click(object sender, EventArgs e)
        {
            this.nilblOperacion.Text = "Seleccione el año que desea abrir";
            this.nilblOperacion.Visible = true;
            this.niddlAno.Visible = true;
            this.divNiddlAno.Visible = true;
            this.nichkCerrarAño.Visible = true;
            this.divChkCerrarAño.Visible = true;

            this.nilbEjecutar.Visible = true;
            this.nilblCancelar.Visible = true;

            this.nitxtAno.Visible = false;
            this.divNitxtAno.Visible = false;

            InhabilitarControles(formContainer.Controls);
            this.ViewState["opcion"] = "abrir";
            CargarCombos();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            this.nilblOperacion.Text = "Digite el año que desea eliminar";

            this.nilblOperacion.Visible = true;

            this.divNiddlAno.Visible = false;
            this.niddlAno.Visible = false;
            this.nichkCerrarAño.Visible = false;
            this.divChkCerrarAño.Visible = false;

            this.nilbEjecutar.Visible = true;
            this.nilblCancelar.Visible = true;

            this.divNitxtAno.Visible = true;
            this.nitxtAno.Visible = true;

            this.ViewState["opcion"] = "eliminar";
            CargarCombos();
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            this.nilblOperacion.Text = "Digite el año que desea generar";

            this.nilblOperacion.Visible = true;

            this.divNiddlAno.Visible = false;
            this.niddlAno.Visible = false;
            this.nichkCerrarAño.Visible = false;
            this.divChkCerrarAño.Visible = false;

            this.nilbEjecutar.Visible = true;
            this.nilblCancelar.Visible = true;

            this.divNitxtAno.Visible = true;
            this.nitxtAno.Visible = true;

            this.ViewState["opcion"] = "generar";
            CargarCombos();
        }
    }
}