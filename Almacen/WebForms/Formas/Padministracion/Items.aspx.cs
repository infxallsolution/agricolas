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
    public partial class Items : BasePage
    {
        #region Instancias

        Citems items = new Citems();

        #endregion Instancias

        #region Metodos

        private void Consecutivo()
        {
            try
            {
                this.txtCodigo.Text = items.Consecutivo(Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al obtener el consecutivo. Correspondiente a: " + ex.Message, "C");
            }
        }
        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                                        ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "C", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }
                gvPlanes.Visible = false;
                this.gvLista.DataSource = items.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                    this.gvLista.Rows.Count.ToString() + " Registros encontrados", ObtenerIP(), Convert.ToInt16(Session["empresa"]));

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
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }
        private void CargarCriterios()
        {
            foreach (GridViewRow registro in gvPlanes.Rows)
            {
                string producto = registro.Cells[0].Text;



                foreach (DataRowView criterio in items.ConsultaCriteriosItems(Convert.ToInt16(txtCodigo.Text), Convert.ToInt16(Session["empresa"])))
                {
                    if (criterio.Row.ItemArray.GetValue(2).ToString() == producto)
                        ((DropDownList)registro.FindControl("ddlCriterio")).SelectedValue = criterio.Row.ItemArray.GetValue(3).ToString();
                }
            }
        }
        private void CargarCombos()
        {
            try
            {
                gvPlanes.Visible = true;
                DataView planes = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("iPlanItem", "ppa"), "codigo", Convert.ToInt16(Session["empresa"]));
                planes.RowFilter = "empresa= " + Session["empresa"].ToString() + " and activo=1 and produccion=0";
                this.gvPlanes.DataSource = planes;
                gvPlanes.DataBind();

                foreach (GridViewRow registro in gvPlanes.Rows)
                {
                    string producto = registro.Cells[0].Text;

                    if (producto != "&nbsp;")
                    {
                        if (items.ConsultaMayorPlan(producto, Convert.ToInt16(Session["empresa"])) == null)
                            ((DropDownList)registro.FindControl("ddlCriterio")).Visible = false;
                        else
                        {
                            ((DropDownList)registro.FindControl("ddlCriterio")).Visible = true;
                            DataView dvMayor = items.ConsultaMayorPlan(producto, Convert.ToInt16(Session["empresa"]));
                            dvMayor.RowFilter = "produccion=0";
                            ((DropDownList)registro.FindControl("ddlCriterio")).DataSource = dvMayor;
                            ((DropDownList)registro.FindControl("ddlCriterio")).DataValueField = "codigo";
                            ((DropDownList)registro.FindControl("ddlCriterio")).DataTextField = "descripcion";
                            ((DropDownList)registro.FindControl("ddlCriterio")).DataBind();
                            ((DropDownList)registro.FindControl("ddlCriterio")).Items.Insert(0, new ListItem("", ""));
                        }
                    }

                    else
                    {
                        ((DropDownList)registro.FindControl("ddlCriterio")).Visible = false;
                        ((DropDownList)registro.FindControl("ddlCriterio")).DataSource = null;
                        ((DropDownList)registro.FindControl("ddlCriterio")).DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los centros de costo. Correspondiente a: " + ex.Message, "C");
            }

            try
            {

                this.ddlUmedidaCompra.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("gUnidadMedida", "ppa"),
                    "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlUmedidaCompra.DataValueField = "codigo";
                this.ddlUmedidaCompra.DataTextField = "descripcion";
                this.ddlUmedidaCompra.DataBind();
                this.ddlUmedidaCompra.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar unidad de medida compra. Correspondiente a: " + ex.Message, "C");
            }

            try
            {

                this.ddlTipoInventario.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cTipoInventario", "ppa"),
                    "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlTipoInventario.DataValueField = "codigo";
                this.ddlTipoInventario.DataTextField = "descripcion";
                this.ddlTipoInventario.DataBind();
                this.ddlTipoInventario.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar unidad de medida compra. Correspondiente a: " + ex.Message, "C");
            }

        }
        private void Guardar()
        {
            string operacion = "inserta", papeleta = null, grupoImpuesto = null, tipoInventario = null;
            int codigo, reposicion;
            decimal maximo, minimo;
            bool venta = false, compra = false;
            try
            {
                maximo = Convert.ToDecimal(txvMaximo.Text);
                minimo = Convert.ToDecimal(txvMinimo.Text);
                reposicion = Convert.ToInt32(txvTiempoReposicion.Text);
                var ddltipoinventariovalue = ddlTipoInventario.SelectedValue;
                tipoInventario = ddltipoinventariovalue.Trim().Length == 0 ? null : ddltipoinventariovalue;

               if (Convert.ToBoolean(this.Session["editar"]) == true)
                {
                    operacion = "actualiza";
                    codigo = Convert.ToInt16(txtCodigo.Text);
                    papeleta = null;

                    object[] objValores = new object[]{
                                         chkActivo.Checked,     //@activo bit 1
                                        codigo,      //@codigo int 2
                                         compra,       //@compras    bit3
                                       this.txtDescripcion.Text.Trim(),       //@descripcion    varchar4
                                         txtDesCorta.Text.Trim(),      //@descripcionAbreviada   varchar5
                                          Convert.ToInt16(Session["empresa"]),      //@empresa    int6
                                          txtEquivalencia.Text.Trim(),      //@equivalencia   varchar7
                                             DateTime.Now,      //@fechaActualiza datetime8
                                           null,     //@foto   int9
                                            grupoImpuesto,   //@grupoIR    char10
                                          false,     //@manalisis  bit11
                                             true,  //@manejaIR   bit12
                                             maximo,      //@maximo decimal13
                                           false,       //@mCalculo   bit14
                                            minimo ,//@minimo decimal15
                                          txtNotas.Text.Trim(),    //@notas  varchar16
                                           0,    //@orden  int17
                                           papeleta,       //@papeleta   varchar18
                                            false, //@producto terminado19
                                            txtReferencia.Text.Trim(),   //@referencia varchar20
                                           false,      //@sello  bit21
                                             false, //@suministro22
                                           reposicion,     //@tiempoReposicion   int23
                                          ddlTipoItem.SelectedValue,     //@tipo   varchar24
                                         tipoInventario,  //@tipoInventario25
                                          ddlUmedidaCompra.SelectedValue,   //@uMedidaCompra  varchar26
                                           ddlUmedidaCompra.SelectedValue,  //@uMedidaConsumo varchar27
                                             Convert.ToString(Session["usuario"]),    //@usuarioActualiza   varchar28
                                           venta        //@ventas bit29
                                 
                };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("iItems", operacion, "ppa", objValores))
                    {
                        case 0:
                            object[] objValoresCriterio = new object[] { Convert.ToInt16(Session["empresa"]), txtCodigo.Text };

                            switch (CentidadMetodos.EntidadInsertUpdateDelete("iItemsCriterios", "elimina", "ppa", objValoresCriterio))
                            {
                                case 0:
                                    foreach (GridViewRow registro in gvPlanes.Rows)
                                    {
                                        if (((DropDownList)registro.FindControl("ddlCriterio")).SelectedValue.Length != 0)
                                        {

                                            object[] objValoresCriterios = new object[]{
                                    Convert.ToInt16(Session["empresa"]),//@empresa
                                    DateTime.Now,//@fechaRegistro
                                    ((DropDownList)registro.FindControl("ddlCriterio")).SelectedValue,
                                    registro.Cells[0].Text,
                                    txtCodigo.Text
                                };

                                            switch (CentidadMetodos.EntidadInsertUpdateDelete("iItemsCriterios", "inserta", "ppa", objValoresCriterios))
                                            {
                                                case 1:
                                                    ManejoError("Errores al insertar el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                                                    break;
                                            }
                                        }
                                    }
                                    break;
                                case 1:
                                    ManejoError("Errores al insertar el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                                    break;
                            }
                            ManejoExito("Datos insertados satisfactoriamente", operacion.Substring(0, 1).ToUpper());
                            break;

                        case 1:

                            ManejoError("Errores al insertar el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                            break;
                    }
                }
                else
                {
                    Consecutivo();
                    codigo = Convert.ToInt16(txtCodigo.Text);

                    object[] objValores = new object[]{
                    chkActivo.Checked,         // @activo bit
                                codigo,        //@codigo int
                                compra,       //@compras    bit
                                this.txtDescripcion.Text.Trim(),        //@descripcion    varchar
                                txtDesCorta.Text.Trim(),       //@descripcionAbreviada   varchar
                                Convert.ToInt16(Session["empresa"]),       //@empresa    int
                                txtEquivalencia.Text.Trim(),       //@equivalencia   varchar
                                DateTime.Now,        //@fechaActualiza datetime
                                DateTime.Now,       //@fechaRegistro  datetime
                                null,      //@foto   int
                                grupoImpuesto,       //@grupoIR    char
                                false,         //@mAnalisis  bit
                                true,       //@manejaIR   bit
                                maximo,       //@maximo decimal
                                false,        //@mCalculo   bit
                                minimo,        //@minimo decimal
                                txtNotas.Text.Trim(),       //@notas  varchar
                                0,        //@orden  int
                                papeleta,        //@papeleta   varchar
                                false,  //@prodcuto terminado
                                txtReferencia.Text.Trim(),         //@referencia varchar
                                false,        //@sello  bit
                                false, //@SUMIINISTRO
                                 reposicion,      //@tiempoReposicion   int
                                 ddlTipoItem.SelectedValue,       //@tipo   varchar
                                 tipoInventario,
                                 ddlUmedidaCompra.SelectedValue,//@uMedidaCompra        varchar
                                   ddlUmedidaCompra.SelectedValue,//@uMedidaConsumo       varchar
                                 Convert.ToString(Session["usuario"]),      //@usuarioActualiza   varchar
                                  Convert.ToString(Session["usuario"]),       //@usuarioRegistro    varchar
                                 venta       //@ventas bit
                };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("iItems", operacion, "ppa", objValores))
                    {
                        case 0:

                            foreach (GridViewRow registro in gvPlanes.Rows)
                            {
                                if (((DropDownList)registro.FindControl("ddlCriterio")).SelectedValue.Length != 0)
                                {

                                    object[] objValoresCriterios = new object[]{
                                    Convert.ToInt16(Session["empresa"]),//@empresa
                                    DateTime.Now,//@fechaRegistro
                                    ((DropDownList)registro.FindControl("ddlCriterio")).SelectedValue,
                                    registro.Cells[0].Text,
                                    txtCodigo.Text
                                };

                                    switch (CentidadMetodos.EntidadInsertUpdateDelete("iItemsCriterios", operacion, "ppa", objValoresCriterios))
                                    {
                                        case 1:
                                            ManejoError("Errores al insertar el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                                            break;
                                    }
                                }
                            }
                            ManejoExito("Datos insertados satisfactoriamente", operacion.Substring(0, 1).ToUpper());
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

        #endregion Metodos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) != 0)
                    this.nitxtBusqueda.Focus();
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                                        ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                object[] objValoresCriterio = new object[] { Convert.ToInt16(Session["empresa"]), Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)) };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("iItemsCriterios", "elimina", "ppa", objValoresCriterio))
                {
                    case 0:
                        object[] objValores = new object[] { Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)), Convert.ToInt16(Session["empresa"]) };

                        switch (CentidadMetodos.EntidadInsertUpdateDelete("iItems", "elimina", "ppa", objValores))
                        {
                            case 1:
                                ManejoError("Error al eliminar el registro. Operación no realizada", "E");
                                break;
                        }
                        ManejoExito("Registro eliminado satisfactoriamente", "E");
                        break;
                    case 1:
                        ManejoError("Error al eliminar el registro. Operación no realizada", "E");
                        break;
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
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
            }
        }
        protected void gvLista_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(), "A", Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "A");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            CargarCombos();
            this.txtReferencia.Focus();
            DataView dvItems = null;



            try
            {

                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                {
                    dvItems = items.RetornaDatosItem(Convert.ToInt32(this.gvLista.SelectedRow.Cells[2].Text), Convert.ToInt32(this.Session["empresa"]));
                    this.txtCodigo.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[2].Text);
                    this.txtCodigo.Enabled = false;
                    CargarCriterios();
                }
                else
                    this.txtCodigo.Text = "";



                if (dvItems.Table.Rows[0].ItemArray.GetValue(2).ToString() != "&nbsp;"
                    & dvItems.Table.Rows[0].ItemArray.GetValue(2) != null)
                {
                    txtDescripcion.Text = dvItems.Table.Rows[0].ItemArray.GetValue(2).ToString();
                }
                else
                {
                    txtDescripcion.Text = "";
                }

                if (dvItems.Table.Rows[0].ItemArray.GetValue(3).ToString() != "&nbsp;"
                  & dvItems.Table.Rows[0].ItemArray.GetValue(3) != null)
                {
                    txtDesCorta.Text = dvItems.Table.Rows[0].ItemArray.GetValue(3).ToString();
                }
                else
                {
                    txtDesCorta.Text = "";
                }

                if (dvItems.Table.Rows[0].ItemArray.GetValue(3).ToString() != "&nbsp;"
                  & dvItems.Table.Rows[0].ItemArray.GetValue(3) != null)
                {
                    txtDesCorta.Text = dvItems.Table.Rows[0].ItemArray.GetValue(3).ToString();
                }
                else
                {
                    txtDesCorta.Text = "";
                }

                if (dvItems.Table.Rows[0].ItemArray.GetValue(4).ToString() != "&nbsp;"
                  & dvItems.Table.Rows[0].ItemArray.GetValue(4) != null)
                {
                    txtReferencia.Text = dvItems.Table.Rows[0].ItemArray.GetValue(4).ToString();
                }
                else
                {
                    txtReferencia.Text = "";
                }

                var equivalence = dvItems.Table.Rows[0].Field<string>("equivalencia");

                if (equivalence != "&nbsp;"
                 & equivalence != null)
                {

                    txtEquivalencia.Text = equivalence;
                }
                else
                {
                    txtEquivalencia.Text = "";
                }



                if (dvItems.Table.Rows[0].ItemArray.GetValue(7).ToString() != "&nbsp;"
                 & dvItems.Table.Rows[0].ItemArray.GetValue(7) != null)
                {
                    ddlTipoItem.SelectedValue = dvItems.Table.Rows[0].ItemArray.GetValue(7).ToString();
                }
                else
                {
                    ddlTipoItem.SelectedValue = "";
                }

                if (dvItems.Table.Rows[0].ItemArray.GetValue(10).ToString() != "&nbsp;"
              & dvItems.Table.Rows[0].ItemArray.GetValue(10) != null)
                {
                    ddlUmedidaCompra.SelectedValue = dvItems.Table.Rows[0].ItemArray.GetValue(10).ToString();
                }
                else
                {
                    ddlUmedidaCompra.SelectedValue = "";
                }


                if (dvItems.Table.Rows[0].ItemArray.GetValue(13).ToString() != "&nbsp;"
             & dvItems.Table.Rows[0].ItemArray.GetValue(13) != null)
                {
                    txvTiempoReposicion.Text = dvItems.Table.Rows[0].ItemArray.GetValue(13).ToString();
                }
                else
                {
                    txvTiempoReposicion.Text = "0";
                }

                if (dvItems.Table.Rows[0].ItemArray.GetValue(14).ToString() != "&nbsp;"
            & dvItems.Table.Rows[0].ItemArray.GetValue(14) != null)
                {
                    txvMinimo.Text = dvItems.Table.Rows[0].ItemArray.GetValue(14).ToString();
                }
                else
                {
                    txvMinimo.Text = "0";
                }


                if (dvItems.Table.Rows[0].ItemArray.GetValue(15).ToString() != "&nbsp;"
            & dvItems.Table.Rows[0].ItemArray.GetValue(15) != null)
                {
                    txvMaximo.Text = dvItems.Table.Rows[0].ItemArray.GetValue(15).ToString();
                }
                else
                {
                    txvMaximo.Text = "0";
                }

                if (dvItems.Table.Rows[0].ItemArray.GetValue(16).ToString() != "&nbsp;"
           & dvItems.Table.Rows[0].ItemArray.GetValue(16) != null)
                {
                    txtNotas.Text = dvItems.Table.Rows[0].ItemArray.GetValue(16).ToString();
                }
                else
                {
                    txtNotas.Text = "";
                }

                var tipoInventario = dvItems.Table.Rows[0].Field<string>("tipoInventario");

                if (tipoInventario != "&nbsp;" && tipoInventario != null)
                {
                    ddlTipoInventario.SelectedValue = dvItems.Table.Rows[0].ItemArray.GetValue(29).ToString();
                }
                else
                {
                    ddlTipoInventario.SelectedValue = "";
                }

                foreach (Control c in this.gvLista.SelectedRow.Cells[7].Controls)
                {
                    if (c is CheckBox)
                        chkActivo.Checked = ((CheckBox)c).Checked;
                }

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }

        }
        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;

            GetEntidad();
        }
        protected void nilbNuevo_Click(object sender, EventArgs e)
        {

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            Consecutivo();
            CargarCombos();
            txtCodigo.Enabled = false;
            txtDescripcion.Focus();
        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);

            this.gvLista.DataSource = null;
            this.gvLista.DataBind();

            this.gvPlanes.DataSource = null;
            this.gvPlanes.DataBind();

            gvPlanes.Visible = false;

            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (txtDesCorta.Text.Length == 0 || txtDescripcion.Text.Length == 0)
            {
                MostrarMensaje("Campos vacios por favor corrija");
                return;
            }

            if (ddlUmedidaCompra.SelectedValue.Length == 0 )
            {
                MostrarMensaje("Debe seleccionar una unidad de compra válida");
                return;
            }

            if (ddlTipoInventario.SelectedValue.Length == 0)
            {
                MostrarMensaje("Debe seleccionar un tipo de inventario válido");
                return;
            }


            Guardar();
        }
    }
}