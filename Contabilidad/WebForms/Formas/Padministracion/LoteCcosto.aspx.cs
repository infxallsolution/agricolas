using Contabilidad.seguridadInfos;
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
    public partial class LoteCcosto : BasePage
    {
        #region Instancias
        Security seguridad = new Security();
        CloteCcostoSiigo loteccosto = new CloteCcostoSiigo();
        string consulta = "C";
        string insertar = "I";
        string eliminar = "E";
        string imprime = "IM";
        string ingreso = "IN";
        string editar = "A";

        #endregion Instancias

        #region Metodos

        private void manejoCcostoMayorCuenta()
        {

            if (dddlMayorCCostoSiigo.SelectedValue.Trim().Length == 0)
            {
                nilblInformacion.Text = "Debe seleccionar un mayor de centro de costo de cuenta contable valido";
                return;
            }
            else
            {
                try
                {
                    DataView dvClase = CentidadMetodos.EntidadGet("cCentrosCostoSigo", "ppa").Tables[0].DefaultView;
                    dvClase.RowFilter = "empresa=" + Convert.ToString(this.Session["empresa"]) + " and activo=True and" + " mayor='" + dddlMayorCCostoSiigo.SelectedValue.Trim() + "'";
                    dvClase.Sort = "descripcion";

                    this.ddlCcostoSiigo.DataSource = dvClase;
                    this.ddlCcostoSiigo.DataValueField = "codigo";
                    this.ddlCcostoSiigo.DataTextField = "descripcion";
                    this.ddlCcostoSiigo.DataBind();
                    this.ddlCcostoSiigo.Items.Insert(0, new ListItem("", ""));
                }
                catch (Exception ex)
                {
                    ManejoError("Error al cargar los centro de costo de nomina para contabilización. Correspondiente a: " + ex.Message, "C");
                }
            }
        }

        protected string nombrePaginaActual()
        {
            string[] arrResult = HttpContext.Current.Request.RawUrl.Split('/');
            string result = arrResult[arrResult.GetUpperBound(0)];
            arrResult = result.Split('?');
            return arrResult[arrResult.GetLowerBound(0)];
        }

        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(), consulta, Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", consulta);
                    return;
                }
                this.gvLista.DataSource = loteccosto.BuscarEntidad(this.nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";
                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                this.gvLista.Rows.Count.ToString() + " Registros encontrados", HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(this.Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente a: " + ex.Message, "C");
            }
        }

        private void ManejoError(string error, string operacion)
        {
            this.Session["error"] = error;
            this.Session["paginaAnterior"] = this.Page.Request.FilePath.ToString();

            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                 "er", error, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(this.Session["empresa"]));
            this.Response.Redirect("~/Contabilidad/Error.aspx", false);
        }

        private void ManejoExito(string mensaje, string operacion)
        {
            this.nilblMensaje.Text = mensaje;

            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(this.Session["empresa"]));
            GetEntidad();
        }

        private void CargarCombos()
        {
            try
            {
                this.ddlLote.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("aLotes", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"])); ;
                this.ddlLote.DataValueField = "codigo";
                this.ddlLote.DataTextField = "descripcion";
                this.ddlLote.DataBind();
                this.ddlLote.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar estructura de centro de costo. Correspondiente a: " + ex.Message, "C");
            }

            try
            {

                try
                {
                    DataView dvClase = CentidadMetodos.EntidadGet("cCentrosCostoSigo", "ppa").Tables[0].DefaultView;
                    dvClase.RowFilter = "empresa=" + Convert.ToString(this.Session["empresa"]) + " and activo=True and auxiliar=0";
                    dvClase.Sort = "descripcion";
                    this.dddlMayorCCostoSiigo.DataSource = dvClase;
                    this.dddlMayorCCostoSiigo.DataValueField = "codigo";
                    this.dddlMayorCCostoSiigo.DataTextField = "descripcion";
                    this.dddlMayorCCostoSiigo.DataBind();
                    this.dddlMayorCCostoSiigo.Items.Insert(0, new ListItem("", ""));
                }
                catch (Exception ex)
                {
                    ManejoError("Error al cargar clase de parametro para contabilización. Correspondiente a: " + ex.Message, "C");
                }

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar Niveles Mayor. Correspondiente a: " + ex.Message, "C");
            }

        }

        private void EntidadKey()
        {
            object[] objKey = new object[] { Convert.ToInt16(this.Session["empresa"]), ddlLote.SelectedValue };

            try
            {
                if (CentidadMetodos.EntidadGetKey("aLoteCcostoSigo", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    this.nilblInformacion.Text = "Centro de costo " + this.ddlLote.Text + " ya se encuentra registrado";
                    CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
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
            string nivelMayor = null, mayor = null;
            try
            {

                if (Convert.ToBoolean(this.Session["editar"]) == true)
                    operacion = "actualiza";

                object[] objValores = new object[]
                {
                      ddlCcostoSiigo.SelectedValue.Trim(),  //	@aCcostoSigo	
                         Convert.ToInt16(this.Session["empresa"]), //	@empresa	
                        ddlLote.SelectedValue.Trim(),   //@lote	varchar	50	0
                        dddlMayorCCostoSiigo.SelectedValue.Trim() //@mCcostoSigo	varchar	50	0

                };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("aLoteCcostoSigo", operacion, "ppa", objValores))
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
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(this.Session["empresa"])) != 0)
                {
                    this.nitxtBusqueda.Focus();

                }
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }

        protected void lbCancelar_Click(object sender, ImageClickEventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            nilbGuardar.Visible = false;
            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
        }

        protected void niimbBuscar_Click(object sender, ImageClickEventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
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
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            this.nilblMensaje.Text = "";
            ddlCcostoSiigo.Enabled = false;
            dddlMayorCCostoSiigo.Enabled = false;
            this.ddlLote.Enabled = false;

            try
            {
                CargarCombos();

                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.ddlLote.SelectedValue = this.gvLista.SelectedRow.Cells[2].Text;

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                {
                    this.dddlMayorCCostoSiigo.SelectedValue = this.gvLista.SelectedRow.Cells[3].Text;
                    manejoCcostoMayorCuenta();
                }

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                    this.ddlCcostoSiigo.SelectedValue = this.gvLista.SelectedRow.Cells[4].Text;

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
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
                object[] objValores = new object[] { Convert.ToInt16(this.Session["empresa"]), Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text) };

                if (CentidadMetodos.EntidadInsertUpdateDelete("aLoteCcostoSigo", operacion, "ppa", objValores) == 0)
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
            this.dddlMayorCCostoSiigo.Focus();
        }

        protected void nilbNiveles_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(this.Session["empresa"])) != 0)
                this.Response.Redirect("Nivel.aspx");
            else
                ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
        }

        protected void ddlNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntidadKey();
        }

        protected void ddlNivelPadre_SelectedIndexChanged(object sender, EventArgs e)
        {
            manejoCcostoMayorCuenta();
        }

        protected void lbRegistrar_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlLote.SelectedValue.Trim().Length == 0 && ddlCcostoSiigo.SelectedValue.Trim().Length == 0 && dddlMayorCCostoSiigo.SelectedValue.Trim().Length == 0)
            {
                nilblMensaje.Text = "Campos vacios por favor corrija";
                return;
            }
            Guardar();
        }

        protected void lbNuevo_Click(object sender, ImageClickEventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                                 nombrePaginaActual(), insertar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", insertar);
                return;
            }
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            CargarCombos();
            this.ddlLote.Enabled = true;
            this.ddlLote.Focus();
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