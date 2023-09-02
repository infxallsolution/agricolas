using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Padministracion
{
    public partial class CentroCosto : BasePage
    {
        #region Instancias

        CcentroCosto centroCosto = new CcentroCosto();

        #endregion Instancias

        #region Metodos


        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                                 ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(),
                                "C", Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }


                this.gvLista.DataSource = centroCosto.BuscarEntidad(this.nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                this.gvLista.Rows.Count.ToString() + " Registros encontrados", ObtenerIP(), Convert.ToInt16(this.Session["empresa"]));
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
                 ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
               mensaje, ObtenerIP(), Convert.ToInt16(this.Session["empresa"]));

            GetEntidad();
        }
        private void CargarCombos()
        {
            try
            {

                this.ddlNivel.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cEstructuraCCosto", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"])); ;
                this.ddlNivel.DataValueField = "nivel";
                this.ddlNivel.DataTextField = "descripcion";
                this.ddlNivel.DataBind();
                this.ddlNivel.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar estructura de centro de costo. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlNivelMayor.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cEstructuraCCosto", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"])); ;
                this.ddlNivelMayor.DataValueField = "nivel";
                this.ddlNivelMayor.DataTextField = "descripcion";
                this.ddlNivelMayor.DataBind();
                this.ddlNivelMayor.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar Niveles Mayor. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlGrupo.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cGrupoCCosto", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"]));
                this.ddlGrupo.DataValueField = "codigo";
                this.ddlGrupo.DataTextField = "descripcion";
                this.ddlGrupo.DataBind();
                this.ddlGrupo.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar grupos de centro de costos. Correspondiente a: " + ex.Message, "C");
            }


        }
        private void EntidadKey()
        {
            object[] objKey = new object[] {
            this.txtCodigo.Text,
            Convert.ToInt16(this.Session["empresa"]),
            Convert.ToInt16(this.ddlNivel.SelectedValue)        };

            try
            {
                if (CentidadMetodos.EntidadGetKey("cCentrosCosto", "ppa", objKey).Tables[0].Rows.Count > 0)
                {

                    MostrarMensaje("Centro de costo " + this.txtCodigo.Text + " ya se encuentra registrado");

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
            string nivelMayor = null;
            string mayor = null;

            try
            {
                if (this.ddlNivelMayor.SelectedValue.Length == 0)
                {
                    nivelMayor = null;
                    mayor = null;
                }
                else
                {
                    nivelMayor = this.ddlNivelMayor.SelectedValue;
                    mayor = this.ddlMayor.SelectedValue;
                }

                if (Convert.ToBoolean(this.Session["editar"]) == true)
                    operacion = "actualiza";

                object[] objValores = new object[]{
                this.chkActivo.Checked,
                chkAuxiliar.Checked,
                this.txtCodigo.Text,
                txtDescripcion.Text,
                Convert.ToInt16(this.Session["empresa"]),
                Convert.ToString(this.ddlGrupo.SelectedValue),
                chkManejaHE.Checked,
                chkManejaLC.Checked,
                mayor,
                ddlNivel.SelectedValue,
                nivelMayor,
                chkProvisiona.Checked,
                Server.HtmlDecode(this.txtResponsable.Text)
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("cCentrosCosto", operacion, "ppa", objValores))
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

        #endregion Metodos

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(),
                        ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(),
                        Convert.ToInt16(this.Session["empresa"])) != 0)
                {
                    this.nitxtBusqueda.Focus();

                    if (this.txtCodigo.Text.Length > 0)
                        this.txtDescripcion.Focus();
                }
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();

            this.nilbNuevo.Visible = true;
            lbRegistrar.Visible = false;
            this.nilblInformacion.Text = "";
        }
        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(
                this.Page.Controls);

            this.nilbNuevo.Visible = true;

            GetEntidad();
        }
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                 nombrePaginaActual(), "A", Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "A");
                return;
            }
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;

            txtCodigo.Enabled = false;
            this.ddlNivel.Enabled = false;
            this.txtDescripcion.Focus();

            try
            {
                var description = (gvLista.SelectedRow.Cells[2].FindControl("hfDescripcion") as HiddenField)?.Value;
                CargarCombos();

                if (this.gvLista.SelectedRow.Cells[2].Text.Trim() != "&nbsp;")
                    this.txtCodigo.Text = this.gvLista.SelectedRow.Cells[2].Text.Trim();
                else
                    this.txtCodigo.Text = "";

                if (this.gvLista.SelectedRow.Cells[3].Text.Trim() != "&nbsp;")
                    this.ddlNivel.SelectedValue = this.gvLista.SelectedRow.Cells[3].Text.Trim();

                if (this.gvLista.SelectedRow.Cells[4].Text.Trim() != "&nbsp;")
                {
                    this.ddlNivelMayor.SelectedValue = this.gvLista.SelectedRow.Cells[4].Text.Trim();
                    cargarMayor();
                }

                if (this.gvLista.SelectedRow.Cells[5].Text.Trim() != "&nbsp;")
                    this.ddlMayor.SelectedValue = this.gvLista.SelectedRow.Cells[5].Text.Trim();
                else
                    this.ddlMayor.SelectedValue = "";

                if (this.gvLista.SelectedRow.Cells[6].Text.Trim() != "&nbsp;")
                    this.txtDescripcion.Text = description;
                else
                    this.txtDescripcion.Text = "";

                if (this.gvLista.SelectedRow.Cells[7].Text.Trim() != "&nbsp;")
                    this.ddlGrupo.SelectedValue = this.gvLista.SelectedRow.Cells[7].Text.Trim();

                if (this.gvLista.SelectedRow.Cells[8].Text.Trim() != "&nbsp;")
                    txtResponsable.Text = this.gvLista.SelectedRow.Cells[8].Text.Trim();

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[9].Controls)
                {
                    this.chkAuxiliar.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[10].Controls)
                {
                    this.chkActivo.Checked = ((CheckBox)objControl).Checked;
                }

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[11].Controls)
                {
                    this.chkManejaHE.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[12].Controls)
                {
                    this.chkManejaLC.Checked = ((CheckBox)objControl).Checked;
                }

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[13].Controls)
                {
                    this.chkProvisiona.Checked = ((CheckBox)objControl).Checked;
                }

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(
                                  this.Session["usuario"].ToString(),
                                   ConfigurationManager.AppSettings["modulo"].ToString(),
                                   nombrePaginaActual(),
                                  "E",
                                 Convert.ToInt16(this.Session["empresa"]))
                                 == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "E");
                return;
            }


            string operacion = "elimina";

            try
            {
                object[] objValores = new object[] {
                Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text),
                Convert.ToInt16(this.Session["empresa"]),
                };

                if (CentidadMetodos.EntidadInsertUpdateDelete(
                    "cCentrosCosto",
                    operacion,
                    "ppa",
                    objValores) == 0)
                {
                    ManejoExito("Datos eliminados satisfactoriamente", "E");
                }
                else
                {
                    ManejoError("Errores al eliminar el registro. Operación no realizada", "E");
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al eliminar los datos correspondiente a: " + ex.Message, "E");
            }
        }
        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();

            this.ddlNivelMayor.Focus();
        }
        protected void nilbNiveles_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoPagina(
                this.Session["usuario"].ToString(),
                ConfigurationManager.AppSettings["Modulo"].ToString(),
                "Nivel.aspx",
                Convert.ToInt16(this.Session["empresa"])) != 0)
            {
                this.Response.Redirect("Nivel.aspx");
            }
            else
            {
                ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }
        protected void ddlNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtCodigo.Focus();

            if (Convert.ToInt16(((DropDownList)sender).SelectedValue) == 0)
            {
                this.ddlNivelMayor.Enabled = false;
                this.ddlMayor.Enabled = false;
            }
            else
            {
                this.ddlNivelMayor.Enabled = true;
                this.ddlMayor.Enabled = true;
            }
        }
        protected void ddlNivelPadre_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarMayor();
        }
        private void cargarMayor()
        {
            if (Convert.ToInt16(this.ddlNivelMayor.SelectedValue) != Convert.ToInt16(this.ddlNivel.SelectedValue))
            {
                try
                {
                    this.ddlMayor.DataSource = centroCosto.CentroCostoNivel(Convert.ToInt16(this.ddlNivelMayor.SelectedValue), Convert.ToInt16(this.Session["empresa"]));
                    this.ddlMayor.DataValueField = "codigo";
                    this.ddlMayor.DataTextField = "Descripcion";
                    this.ddlMayor.DataBind();
                    this.ddlMayor.Items.Insert(0, new ListItem("", ""));
                    this.ddlMayor.Focus();
                }
                catch (Exception ex)
                {
                    ManejoError("Error al cargar destinos por nivel. Correspondiente a: " + ex.Message, "C");
                }
            }
            else
            {
                this.nilblInformacion.Text = "El nivel principal debe ser diferente a el nivel del padre del destino. Por favor corrija";
            }
        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Length == 0 || txtResponsable.Text.Length == 0 || ddlGrupo.SelectedIndex.ToString().Length == 0)
            {
                MostrarMensaje("Campos vacios por favor corrija");
                return;
            }

            Guardar();
        }
        protected void lbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(
                                this.Session["usuario"].ToString(),
                                 ConfigurationManager.AppSettings["modulo"].ToString(),
                                 nombrePaginaActual(),
                                "I",
                               Convert.ToInt16(this.Session["empresa"]))
                               == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "I");
                return;
            }


            CcontrolesUsuario.HabilitarControles(
                this.Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;

            CargarCombos();

            this.ddlNivel.Enabled = true;
            this.ddlNivel.Focus();
            this.nilblInformacion.Text = "";
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
        #endregion Eventos
    }
}