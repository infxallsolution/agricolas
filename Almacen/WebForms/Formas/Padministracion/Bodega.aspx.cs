using Almacen.WebForms.App_Code.General;
using Almacen.WebForms.App_Code.Parametros;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Almacen.WebForms.Formas.Padministracion
{
    public partial class Bodega : BasePage
    {
        #region Instancias
        cBodega bode = new cBodega();
        #endregion Instancias

        #region Metodos

        private void CargarCombos()
        {
            try
            {
                DataView dvCuentas = CentidadMetodos.EntidadGet("cpuc", "ppa").Tables[0].DefaultView;
                EnumerableRowCollection<DataRow> query = from cuenta in dvCuentas.Table.AsEnumerable()
                                                         where cuenta.Field<bool>("activo") == true && cuenta.Field<bool>("auxiliar") == true && cuenta.Field<int>("empresa") == Convert.ToInt32(this.Session["empresa"])
                                                         select cuenta;
                DataView dvResultado = query.AsDataView();
                this.ddlAuxiliarInventario.DataSource = dvResultado;
                this.ddlAuxiliarInventario.DataValueField = "codigo";
                this.ddlAuxiliarInventario.DataTextField = "cadena";
                this.ddlAuxiliarInventario.DataBind();
                this.ddlAuxiliarInventario.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar las cuentas contables debido a:" + ex.Message, "I");
            }

        }

        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),//sitio
                                 nombrePaginaActual(), "C", Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                DataView dvbodega = bode.BuscarEntidad(this.nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataSource = dvbodega;
                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C",
                 ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                this.gvLista.Rows.Count.ToString() + " Registros encontrados", ObtenerIP(), Convert.ToInt16(this.Session["empresa"]));
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }

        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            Session["rangos"] = null;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                        mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }

        private void EntidadKey()
        {
            object[] objKey = new object[] {
            this.txtCodigo.Text,
            Convert.ToInt16(this.Session["empresa"])      };

            try
            {
                if (CentidadMetodos.EntidadGetKey("iBodega", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "El código " + this.txtCodigo.Text + " ya se encuentra registrado", "w");

                    CcontrolesUsuario.InhabilitarControles(this.Page.Controls);

                    this.nilbNuevo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private void Guardar()
        {
            string operacion = "inserta";

            try
            {

                if (Convert.ToBoolean(this.Session["editar"]) == true)
                    operacion = "actualiza";

                object[] objValores = new object[]{
                                chkActivo.Checked,         //@activo
                                ddlAuxiliarInventario.SelectedValue,
                               txtCodigo.Text,     //@codigo varchar
                               txtDescripcionCorta.Text,     //@descorta   varchar
                               txtDescripcion.Text,     //@descripcion    varchar
                               chkDesechos.Checked,
                                Convert.ToInt16(this.Session["empresa"]) ,   //@empresa    int
                                DateTime.Now,    //@fechaModificacion  varchar
                                DateTime.Now,    //@fechaRegistro  datetime
                                chkManejarExistencia.Checked,    //@mexistencias   bit
                                chkProduccion.Checked, //@@produccion
                                chkProductoTerminado.Checked,
                                chkServicio.Checked,    //@servicio   bit
                                chkSuministro.Checked, //@suministro bit
                                chkTanque.Checked,    //@tanque bit
                                Convert.ToBoolean(this.Session["editar"])?this.Session["usuario"].ToString():null ,   //@usuarioModificacion    varchar
                                 this.Session["usuario"].ToString()    //@usuarioRegistro    varchar
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("iBodega", operacion, "ppa", objValores))
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
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
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
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(),
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
            this.txtDescripcion.Focus();
            CargarCombos();

            try
            {


                if (this.gvLista.SelectedRow.Cells[2].Text.Trim() != "&nbsp;")
                    this.txtCodigo.Text = this.gvLista.SelectedRow.Cells[2].Text.Trim();
                else
                    this.txtCodigo.Text = "";

                if (this.gvLista.SelectedRow.Cells[3].Text.Trim() != "&nbsp;")
                    this.txtDescripcion.Text = this.gvLista.SelectedRow.Cells[3].Text.Trim();

                if (this.gvLista.SelectedRow.Cells[4].Text.Trim() != "&nbsp;")
                {
                    this.txtDescripcionCorta.Text = this.gvLista.SelectedRow.Cells[4].Text.Trim();
                }

               
                foreach (Control objControl in this.gvLista.SelectedRow.Cells[5].Controls)
                {
                    this.chkManejarExistencia.Checked = ((CheckBox)objControl).Checked;
                }

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[6].Controls)
                {
                    this.chkServicio.Checked = ((CheckBox)objControl).Checked;
                }

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[7].Controls)
                {
                    this.chkActivo.Checked = ((CheckBox)objControl).Checked;
                }


            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(), "E", Convert.ToInt16(this.Session["empresa"])) == 0)
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

                if (CentidadMetodos.EntidadInsertUpdateDelete("iBodega", operacion, "ppa", objValores) == 0)
                    ManejoExito("Datos eliminados satisfactoriamente", "E");
                else
                    ManejoError("Errores al eliminar el registro. Operación no realizada", "E");
            }
            catch (Exception ex)
            {
                ManejoError("Error al eliminar los datos correspondiente a: " + ex.Message, "E");
            }
        }
        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();

        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Trim().Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Ingrese un código válido", "warning");
                return;
            }

            if (txtDescripcion.Text.Trim().Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Ingrese una descripción válido", "warning");
                return;
            }

            if (txtDescripcionCorta.Text.Trim().Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Ingrese un descripción corta válida", "warning");
                return;
            }


            Guardar();
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
        protected void nilbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(),
                "I", Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "I");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            CargarCombos();
            this.nilblInformacion.Text = "";
        }
        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }

        #endregion Eventos

    }
}