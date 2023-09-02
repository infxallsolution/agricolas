using Contabilidad.WebForms.App_Code.Administracion;
using Contabilidad.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.Padministracion
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
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(), consulta, Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", consulta);
                    return;
                }
                this.gvLista.DataSource = centroCosto.BuscarEntidadContable(this.nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";
                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                this.gvLista.Rows.Count.ToString() + " Registros encontrados", ObtenerIP(), Convert.ToInt16(this.Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            InhabilitarControles(this.Controls);
            LimpiarControles(this.Controls);
            this.nilbNuevo.Visible = true;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex", mensaje, ObtenerIP(), Convert.ToInt16(this.Session["empresa"]));
            GetEntidad();
        }

        private void CargarCombos()
        {
            try
            {
                this.ddlNivel.DataSource = OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cEstructuraCCosto", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"])); ;
                this.ddlNivel.DataValueField = "nivel";
                this.ddlNivel.DataTextField = "descripcion";
                this.ddlNivel.DataBind();
                this.ddlNivel.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }

            try
            {
                this.ddlNivelMayor.DataSource = OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cEstructuraCCosto", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"])); ;
                this.ddlNivelMayor.DataValueField = "nivel";
                this.ddlNivelMayor.DataTextField = "descripcion";
                this.ddlNivelMayor.DataBind();
                this.ddlNivelMayor.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }


        }

        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text, Convert.ToInt16(this.Session["empresa"]), ddlMayor.SelectedValue };

            try
            {
                if (CentidadMetodos.EntidadGetKey("cCentrosCostoContable", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    MostrarMensaje("Centro de costo " + this.txtCodigo.Text + " ya se encuentra registrado");
                    InhabilitarControles(this.Controls);
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
            string nivelMayor = null, mayor = null;
            try
            {


                if (this.ddlNivelMayor.SelectedValue.Length == 0)
                {

                    nivelMayor = ddlNivel.SelectedValue == "1" ? "1": null;
                    mayor = ddlNivel.SelectedValue == "1" ? "" : null; ;
                }
                else
                {
                    nivelMayor = this.ddlNivelMayor.SelectedValue;
                    mayor = this.ddlMayor.SelectedValue;
                }

                if (Convert.ToBoolean(this.Session["editar"]) == true)
                    operacion = "actualiza";

                object[] objValores = new object[]
                {
                this.chkActivo.Checked,
                chkAuxiliar.Checked,
                this.txtCodigo.Text,
                txtDescripcion.Text,
                Convert.ToInt16(this.Session["empresa"]),
                mayor,
                ddlNivel.SelectedValue,
                nivelMayor
                };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("cCentrosCostoContable", operacion, "ppa", objValores))
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

        private void cargarMayor()
        {
            if (Convert.ToInt16(this.ddlNivelMayor.SelectedValue) != Convert.ToInt16(this.ddlNivel.SelectedValue))
            {
                try
                {
                    var dvMayor = CentidadMetodos.EntidadGet("cCentrosCostoContable", "ppa").Tables[0].AsEnumerable()
                        .Where(x => x.Field<int>("empresa") == Convert.ToInt16(this.Session["empresa"]) & !x.Field<bool>("auxiliar"))
                        .Select(x => new { codigo = x.Field<string>("codigo"), descripcion = x.Field<string>("codigo") + "-" + x.Field<string>("descripcion") });

                    this.ddlMayor.DataSource = dvMayor;
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

        #endregion Metodos

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {
                this.nilblInformacion.Text = "";
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(this.Session["empresa"])) != 0)
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
            InhabilitarControles(this.Controls);
            LimpiarControles(this.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            lbRegistrar.Visible = false;
            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
        }

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            InhabilitarControles(this.Controls);
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
            HabilitarControles(this.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            txtCodigo.Enabled = false;
            ddlMayor.Enabled = false;
            ddlNivelMayor.Enabled = false;
            this.ddlNivel.Enabled = false;
            this.txtDescripcion.Focus();
            try
            {
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
                    this.txtDescripcion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[6].Text.Trim());
                else
                    this.txtDescripcion.Text = "";


                foreach (Control objControl in this.gvLista.SelectedRow.Cells[7].Controls)
                {
                    this.chkAuxiliar.Checked = ((CheckBox)objControl).Checked;
                }
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[8].Controls)
                {
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
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                   nombrePaginaActual(), eliminar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", eliminar);
                return;
            }
            string operacion = "elimina";
            try
            {
                object[] objValores = new object[] { Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text), Convert.ToInt16(this.Session["empresa"]), Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[3].Text) };

                if (CentidadMetodos.EntidadInsertUpdateDelete("cCentrosCostoContable", operacion, "ppa", objValores) == 0)
                    ManejoExito("Datos eliminados satisfactoriamente", "E");
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
            this.ddlNivelMayor.Focus();
        }

        protected void nilbNiveles_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(this.Session["empresa"])) != 0)
                this.Response.Redirect("Nivel.aspx");
            else
                ManejoErrorAcceso("Usuario no autorizado para ingresar a esta página", "IN");
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
            CargarMayor();
        }

        private void CargarMayor()
        {
            if (this.ddlNivelMayor.SelectedValue != this.ddlNivel.SelectedValue)
            {
                try
                {
                    this.ddlMayor.DataSource = centroCosto.CentroCostoNivel(ddlNivelMayor.SelectedValue, Convert.ToInt16(this.Session["empresa"]));
                    this.ddlMayor.DataValueField = "codigo";
                    this.ddlMayor.DataTextField = "Descripcion";
                    this.ddlMayor.DataBind();
                    this.ddlMayor.Items.Insert(0, new ListItem("", ""));
                    this.ddlMayor.Focus();
                }
                catch (Exception ex)
                {
                    ManejoErrorCatch(ex);
                }
            }

        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Campos vacios por favor corrija", "warning");
                return;
            }
            Guardar();
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }

        protected void nilbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                nombrePaginaActual(), insertar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", insertar);
                return;
            }
            HabilitarControles(this.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            CargarCombos();
            this.ddlNivel.Enabled = true;
            this.ddlNivel.Focus();
            this.nilblInformacion.Text = "";
        }

        #endregion Eventos
    }
}