﻿using Nomina.WebForms.App_Code.Administracion;
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
    public partial class GrupoCC : BasePage
    {
        #region Instancias

        CgrupoCC grupo = new CgrupoCC();

        #endregion Instancias

        #region Metodos

        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                     nombrePaginaActual(), consulta, Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                this.gvLista.DataSource = grupo.BuscarEntidad(this.nitxtBusqueda.Text, Convert.ToInt16(this.Session["empresa"]));
                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                  "ex", this.gvLista.Rows.Count.ToString() + " Registros encontrados", ObtenerIP(), Convert.ToInt16(this.Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente a: " + ex.Message, consulta);
            }
        }



        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");

            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);

            this.nilbNuevo.Visible = true;

            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                    "ex", mensaje, ObtenerIP(), Convert.ToInt16(this.Session["empresa"]));

            GetEntidad();
        }

        private void CargarCombos()
        {
        }

        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text, Convert.ToInt16(this.Session["empresa"]) };

            try
            {
                if (CentidadMetodos.EntidadGetKey("cGrupoCCosto", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    MostrarMensaje("Centro de Costo " + this.txtCodigo.Text + " ya se encuentra registrado");

                    CcontrolesUsuario.InhabilitarControles(
                        this.Page.Controls);

                    this.nilbNuevo.Visible = true;
                    this.txtCodigo.Text = "";
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

                if (Convert.ToBoolean(this.Session["editar"]) == true)
                {
                    operacion = "actualiza";
                }

                object[] objValores = new object[]{
                chkActivo.Checked,
                this.txtCodigo.Text,
                this.txtDescripcion.Text ,
                Convert.ToInt16(this.Session["empresa"])
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete(
                    "cGrupoCCosto",
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

        #endregion Metodos

        #region Eventos

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
                    {
                        this.txtDescripcion.Focus();
                    }
                }
                else
                {
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
                }
            }
        }

        protected void lbNuevo_Click(object sender, EventArgs e)
        {

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
               nombrePaginaActual(), insertar, Convert.ToInt32(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "I");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;

            CargarCombos();

            this.txtCodigo.Enabled = true;
            this.txtCodigo.Focus();
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



        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                   nombrePaginaActual(), editar, Convert.ToInt32(this.Session["empresa"])) == 0)
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

            try
            {
                CargarCombos();

                if (this.gvLista.SelectedRow.Cells[2].Text != "")
                    this.txtCodigo.Text = this.gvLista.SelectedRow.Cells[2].Text;
                else
                    this.txtCodigo.Text = "";

                if (this.gvLista.SelectedRow.Cells[3].Text != "")
                    this.txtDescripcion.Text = this.gvLista.SelectedRow.Cells[3].Text;
                else
                    this.txtDescripcion.Text = "";

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[4].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkActivo.Checked = ((CheckBox)objControl).Checked;
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
                eliminar,
                Convert.ToInt32(this.Session["empresa"].ToString())) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "E");
                return;
            }

            string operacion = "elimina";

            try
            {
                object[] objValores = new object[] {
                Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text),
                Convert.ToInt16(this.Session["empresa"])
     };

                if (CentidadMetodos.EntidadInsertUpdateDelete("cGrupoCCosto", operacion, "ppa", objValores) == 0)
                    ManejoExito("Datos eliminados satisfactoriamente", "E");
                else
                    ManejoError("Errores al eliminar el registro. Operación no realizada", "E");
            }
            catch (Exception ex)
            {
                ManejoError("Error al eliminar los datos correspondiente a: " + ex.Message, "E");
            }
        }

        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Length == 0 || txtDescripcion.Text.Length == 0)
            {
                MostrarMensaje("Campos vacios por favor corrija");
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

        #endregion Eventos

    }
}