﻿using Nomina.WebForms.App_Code.General;
using Nomina.WebForms.App_Code.GestionHumana;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.PgestionHumana
{
    public partial class FormacionAcademica : BasePage
    {
        #region Instancias

        CinformacionAcademica expeiencia = new CinformacionAcademica();


        #endregion Instancias

        #region Metodos


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

                this.gvLista.DataSource = expeiencia.BuscarEntidad(this.nitxtBusqueda.Text, Convert.ToInt32(Session["empresa"]));
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
            this.nilbNuevo.Visible = true;
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
                DataView ciudad = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("gCiudad", "ppa"), "nombre", Convert.ToInt32(this.Session["empresa"]));
                this.ddlCiudad.DataSource = ciudad;
                this.ddlCiudad.DataValueField = "codigo";
                this.ddlCiudad.DataTextField = "nombre";
                this.ddlCiudad.DataBind();
                this.ddlCiudad.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar ciudades. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlEntidadEducativa.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("nEntidadEducativa", "ppa"), "descripcion", Convert.ToInt32(Session["empresa"]));
                this.ddlEntidadEducativa.DataValueField = "codigo";
                this.ddlEntidadEducativa.DataTextField = "descripcion";
                this.ddlEntidadEducativa.DataBind();
                this.ddlEntidadEducativa.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar entidad Educativa. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlNivelEducativo.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("gNivelEducativo", "ppa"), "descripcion", Convert.ToInt32(Session["empresa"]));
                this.ddlNivelEducativo.DataValueField = "codigo";
                this.ddlNivelEducativo.DataTextField = "descripcion";
                this.ddlNivelEducativo.DataBind();
                this.ddlNivelEducativo.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar nivel educativo. Correspondiente a: " + ex.Message, "C");
            }


        }
        private void Guardar()
        {
            string operacion = "inserta";
            int registro = 0;
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                    {
                        operacion = "actualiza";
                        registro = Convert.ToInt16(Session["registro"]);
                    }

                    object[] objValores = new object[]{
                            ddlCiudad.SelectedValue,
                            Convert.ToInt16(ddlContratos.SelectedValue),
                            txtDescripcion.Text,
                            Convert.ToInt32(Session["empresa"]),
                            ddlEntidadEducativa.SelectedValue,
                            ddlEstado.SelectedValue,
                            Convert.ToDateTime(txtFechaFinal.Text),
                            Convert.ToDateTime(txtFechaInicio.Text),
                            ddlNivelEducativo.SelectedValue,
                            txtObservacion.Text,
                            registro,
                            Convert.ToInt16(ddlTercero.SelectedValue),
                            ddlTipo.SelectedValue
                            };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("nFormacionAcadamica", operacion, "ppa", objValores))
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
        private void cargarContrato()
        {
            try
            {
                DataView dvContratos = expeiencia.SelecccionaContratosTercero(Convert.ToInt32(ddlTercero.SelectedValue), Convert.ToInt32(this.Session["empresa"]));
                this.ddlContratos.DataSource = dvContratos;
                this.ddlContratos.DataValueField = "noContrato";
                this.ddlContratos.DataTextField = "desContrato";
                this.ddlContratos.DataBind();
                this.ddlContratos.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar contratos de tercero. Correspondiente a: " + ex.Message, "C");
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
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            CargarCombos();
            this.ddlTercero.Enabled = true;
            this.ddlTercero.Focus();
            this.nilblInformacion.Text = "";
        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
        }
        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (txtDescripcion.Text.Length == 0 || ddlEntidadEducativa.SelectedValue.Length == 0 || ddlTercero.SelectedValue.Length == 0 || ddlContratos.SelectedValue.Length == 0)
            {
                MostrarMensaje("Campos vacios por favor corrija");
                return;
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
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            this.nilblInformacion.Text = "";
            this.ddlTercero.Enabled = false;
            ddlContratos.Enabled = false;
            int columna = 0;

            try
            {
                CargarCombos();
                DataView dvTercero = expeiencia.RetornaDatosProspecto(this.gvLista.SelectedRow.Cells[2].Text.Trim(), Convert.ToInt32(gvLista.SelectedRow.Cells[5].Text),
                    Convert.ToInt32(Session["empresa"]), Convert.ToInt32(gvLista.SelectedRow.Cells[6].Text));

                columna += 1;
                if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    ddlTercero.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();
                columna += 1;
                if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                {
                    cargarContrato();
                    ddlContratos.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();
                }
                columna += 1;
                if (dvTercero.Table.Rows[0].ItemArray.GetValue(3) != null)
                    this.Session["registro"] = dvTercero.Table.Rows[0].ItemArray.GetValue(3).ToString();
                else
                    this.Session["registro"] = null;

                columna += 1;
                if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    ddlTipo.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();
                columna += 1;
                if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    ddlEntidadEducativa.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();
                columna += 1;
                if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    ddlNivelEducativo.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();
                columna += 1;
                if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    txtDescripcion.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                else
                    txtDescripcion.Text = "";
                columna += 1;
                if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    txtFechaInicio.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();
                else
                    txtFechaInicio.Text = "";
                columna += 1;
                if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    txtFechaFinal.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                else
                    txtFechaFinal.Text = "";
                columna += 1;
                if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    txtObservacion.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                else
                    txtObservacion.Text = "";
                columna += 1;
                if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    ddlEstado.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();
                columna += 1;
                if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                    ddlCiudad.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();

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
                object[] objValores = new object[] { Convert.ToInt16(this.gvLista.Rows[e.RowIndex].Cells[5].Text), Convert.ToInt32(Session["empresa"]),
                Convert.ToInt16(this.gvLista.Rows[e.RowIndex].Cells[6].Text),Convert.ToInt16(this.gvLista.Rows[e.RowIndex].Cells[2].Text) };

                if (CentidadMetodos.EntidadInsertUpdateDelete("nFormacionAcadamica", operacion, "ppa", objValores) == 0)
                    ManejoExito("Datos eliminados satisfactoriamente", "E");
                else
                    ManejoError("Errores al eliminar el registro. Operación no realizada", "E");
            }
            catch (Exception ex)
            {
                ManejoError("Error al eliminar los datos correspondiente a: " + ex.Message, "E");
            }
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
        protected void ddlEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTercero.SelectedValue.Length > 0)
                cargarContrato();

        }

        #endregion Eventos
        protected void txtFechaInicio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaInicio;
                fechaInicio = Convert.ToDateTime(txtFechaInicio.Text);
            }
            catch (Exception ex)
            {
                CcontrolesUsuario.MensajeError("Formato de fecha incorrecto", nilblInformacion);
            }
        }
        protected void txtFechaFinal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaFinal;
                fechaFinal = Convert.ToDateTime(txtFechaFinal.Text);
            }
            catch (Exception ex)
            {
                CcontrolesUsuario.MensajeError("Formato de fecha incorrecto", nilblInformacion);
            }
        }
    }
}