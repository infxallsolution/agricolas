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
    public partial class TipoServicios : BasePage
    {
        #region Instancias

        cTipoInventario motivos = new cTipoInventario();

        #endregion Instancias

        #region Metodos

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
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(),//pagina
                    consulta, Convert.ToInt16(this.Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                this.gvLista.DataSource = motivos.BuscarEntidad(this.nitxtBusqueda.Text, Convert.ToInt16(this.Session["empresa"]));
                this.gvLista.DataBind();
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                  "ex", this.gvLista.Rows.Count.ToString() + " Registros encontrados",
                  ObtenerIP(), Convert.ToInt16(this.Session["empresa"]));
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
            Session["rangos"] = null;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                        mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }
        private void CargarCombos()
        {
            try
            {
                DataView dvCuentas = CentidadMetodos.EntidadGet("cpuc", "ppa").Tables[0].DefaultView;
                EnumerableRowCollection<DataRow> query = from cuenta in dvCuentas.Table.AsEnumerable()
                                                         where cuenta.Field<bool>("activo") == true && cuenta.Field<bool>("auxiliar") == true && cuenta.Field<int>("empresa") == Convert.ToInt32(this.Session["empresa"])
                                                         select cuenta;
                DataView dvResultado = query.AsDataView();
                this.ddlAuxiliarCompra.DataSource = dvResultado;
                this.ddlAuxiliarCompra.DataValueField = "codigo";
                this.ddlAuxiliarCompra.DataTextField = "cadena";
                this.ddlAuxiliarCompra.DataBind();
                this.ddlAuxiliarCompra.Items.Insert(0, new ListItem("", ""));


                this.ddlAuxiliarVenta.DataSource = dvResultado;
                this.ddlAuxiliarVenta.DataValueField = "codigo";
                this.ddlAuxiliarVenta.DataTextField = "cadena";
                this.ddlAuxiliarVenta.DataBind();
                this.ddlAuxiliarVenta.Items.Insert(0, new ListItem("", ""));

                this.ddlAuxiliarGasto.DataSource = dvResultado;
                this.ddlAuxiliarGasto.DataValueField = "codigo";
                this.ddlAuxiliarGasto.DataTextField = "cadena";
                this.ddlAuxiliarGasto.DataBind();
                this.ddlAuxiliarGasto.Items.Insert(0, new ListItem("", ""));

                this.ddlAuxiliarCosto.DataSource = dvResultado;
                this.ddlAuxiliarCosto.DataValueField = "codigo";
                this.ddlAuxiliarCosto.DataTextField = "cadena";
                this.ddlAuxiliarCosto.DataBind();
                this.ddlAuxiliarCosto.Items.Insert(0, new ListItem("", ""));

                this.ddlAuxiliarInversion.DataSource = dvResultado;
                this.ddlAuxiliarInversion.DataValueField = "codigo";
                this.ddlAuxiliarInversion.DataTextField = "cadena";
                this.ddlAuxiliarInversion.DataBind();
                this.ddlAuxiliarInversion.Items.Insert(0, new ListItem("", ""));

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar las cuentas contables debido a:" + ex.Message, "I");
            }

        }
        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text, Convert.ToInt16(this.Session["empresa"]) };

            try
            {
                if (CentidadMetodos.EntidadGetKey("ctipoinventario", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "El código " + this.txtCodigo.Text + " ya se encuentra registrado", "warning");
                    CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
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
            string operacion = "inserta", auxiliarVenta = null, auxiliarCompra = null;

            try
            {

                if (Convert.ToBoolean(this.Session["editar"]) == true)
                    operacion = "actualiza";

                auxiliarCompra = ddlAuxiliarCompra.SelectedValue.Length > 0 ? ddlAuxiliarCompra.SelectedValue : null;
                auxiliarVenta = ddlAuxiliarVenta.SelectedValue.Length > 0 ? ddlAuxiliarVenta.SelectedValue : null;
                string auxiliarGasto = ddlAuxiliarGasto.SelectedValue.Length > 0 ? ddlAuxiliarGasto.SelectedValue : null;
                string auxiliarCosto = ddlAuxiliarCosto.SelectedValue.Length > 0 ? ddlAuxiliarCosto.SelectedValue : null;
                string auxiliarInversion = ddlAuxiliarInversion.SelectedValue.Length > 0 ? ddlAuxiliarInversion.SelectedValue : null;


                object[] objValores = new object[]{
                    chkActivo.Checked  ,      //              @activo   bit
                    auxiliarCompra,
                    auxiliarCosto,
                    auxiliarGasto,
                    auxiliarInversion,
                    auxiliarVenta,
                        txtCodigo.Text.Trim(),    //@codigo varchar
                        txtDescripcion.Text.Trim(),    //@descripcion    varchar
                       Convert.ToInt16(this.Session["empresa"]),     //@empresa    int
                          DateTime.Now,  //@fechaInactivacion  datetime
                          DateTime.Now,  //@fechaModificacion  datetime
                          DateTime.Now,  //@fechaRegistro  datetime
                          chkInventario.Checked,  //@inventario bit
                          chkServicio.Checked,  //@servicio   bit
                          this.Session["usuario"],  //@usuarioInactivacion    varchar
                           this.Session["usuario"],  //@usuarioModificacion    varchar
                           this.Session["usuario"] //@usuarioRegistro    varchar
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("cTipoInventario", operacion, "ppa", objValores))
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

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
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

                }
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }
        protected void nilbNuevo_Click(object sender, EventArgs e)
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

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            this.txtCodigo.Enabled = false;

            try
            {
                CargarCombos();

                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.txtCodigo.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[2].Text);

                else
                    this.txtCodigo.Text = "";

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    this.txtDescripcion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[3].Text);
                else
                    this.txtDescripcion.Text = "";

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                    ddlAuxiliarCompra.SelectedValue = this.gvLista.SelectedRow.Cells[4].Text.Trim();

                if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                    ddlAuxiliarVenta.SelectedValue = this.gvLista.SelectedRow.Cells[5].Text.Trim();

                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                    ddlAuxiliarGasto.SelectedValue = this.gvLista.SelectedRow.Cells[6].Text.Trim();

                if (this.gvLista.SelectedRow.Cells[7].Text != "&nbsp;")
                    ddlAuxiliarCosto.SelectedValue = this.gvLista.SelectedRow.Cells[7].Text.Trim();

                if (this.gvLista.SelectedRow.Cells[8].Text != "&nbsp;")
                    ddlAuxiliarInversion.SelectedValue = this.gvLista.SelectedRow.Cells[8].Text.Trim();

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[9].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkInventario.Checked = ((CheckBox)objControl).Checked;
                }

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[10].Controls)
                {
                    if (objControl is CheckBox)
                        this.chkServicio.Checked = ((CheckBox)objControl).Checked;
                }

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[11].Controls)
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
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(),
                eliminar, Convert.ToInt32(this.Session["empresa"].ToString())) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "E");
                return;
            }

            string operacion = "elimina";

            try
            {
                object[] objValores = new object[] { Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text), Convert.ToInt16(this.Session["empresa"]) };

                if (CentidadMetodos.EntidadInsertUpdateDelete("cTipoInventario", operacion, "ppa", objValores) == 0)
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
            txtDescripcion.Focus();
        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Trim().Length == 0 || txtDescripcion.Text.Trim().Length == 0)
            {
                ManejoError("Campos vacios por favor corrija", "I");
                return;
            }

            Guardar();
        }



        #endregion Eventos


    }
}