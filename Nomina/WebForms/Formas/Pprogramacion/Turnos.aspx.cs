using Microsoft.Reporting.Map.WebForms.BingMaps;
using Nomina.WebForms.App_Code.General;
using Nomina.WebForms.App_Code.Programacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Pprogramacion
{
    public partial class Turnos : BasePage
    {
        #region Instancias

        Cturnos turnos = new Cturnos();

        #endregion Instancias

        #region Metodos

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

                this.gvLista.DataSource = turnos.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(
                    this.Session["usuario"].ToString(), "C",
                  ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                    this.gvLista.Rows.Count.ToString() + " Registros encontrados", HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }


        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            nilbNuevo.Visible = true;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                 "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }

        private void CargarCombos()
        {


        }
        private void Consecutivo()
        {
            try
            {
                this.txtCodigo.Text = turnos.Consecutivo(Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }
        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text.Trim().ToString(), Convert.ToInt16(Session["empresa"]) };
            try
            {
                if (CentidadMetodos.EntidadGetKey("nTurno", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Código " + this.txtCodigo.Text + " ya se encuentra registrado", "warning");
                    CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
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
            string operacion = "inserta", descripcion = null;


            if (this.txtCodigo.Text.Length == 0)
                CerroresGeneral.ManejoError(this, GetType(), "Campos vacios por favor corrija", "warning");
            else
            {
                try
                {
                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                        operacion = "actualiza";
                    else
                        Consecutivo();

                    descripcion = "Turno inicia a las " + this.txvHoraInicio.Text + " con " + txvMinutoInicio.Text + " duracion " + this.txvHorasTurno.Text + " horas";

                    object[] objValores = new object[]{
                    chkActivo.Checked,
                      txtCodigo.Text,
                    descripcion,
                   Convert.ToInt16(Session["empresa"]),
                    Convert.ToInt16(this.txvHoraInicio.Text.Trim().PadLeft(2,'0') + this.txvMinutoInicio.Text.Trim().PadLeft(2,'0')),
                   Convert.ToDecimal(txvHorasTurno.Text )
                };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("nTurno", operacion, "ppa", objValores))
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
                    ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) != 0)
                    this.txtCodigo.Focus();
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }



        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(
                this.Page.Controls);

            CcontrolesUsuario.LimpiarControles(
                this.Page.Controls);

            this.gvLista.DataSource = null;
            this.gvLista.DataBind();

            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
        }

        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
            txtDescripcion.Focus();
        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                object[] objValores = new object[] { Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[3].Text)), Convert.ToInt16(Session["empresa"]) };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("nTurno", "elimina", "ppa", objValores))
                {
                    case 0:

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
                {
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
                }
            }
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }



        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
              nombrePaginaActual(), "A", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "A");
                return;
            }
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;

            this.txtCodigo.Enabled = false;
            this.txtDescripcion.Focus();
            CargarCombos();
            try
            {
                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.txtCodigo.Text = this.gvLista.SelectedRow.Cells[2].Text;

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    this.txtDescripcion.Text = this.gvLista.SelectedRow.Cells[3].Text;

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                {
                    if (this.gvLista.SelectedRow.Cells[4].Text.Length == 3)
                    {
                        this.txvHoraInicio.Text = this.gvLista.SelectedRow.Cells[4].Text.Substring(0, 1);
                        this.txvMinutoInicio.Text = this.gvLista.SelectedRow.Cells[4].Text.Substring(1, 2);
                    }
                    else
                    {
                        if (this.gvLista.SelectedRow.Cells[4].Text.Length == 4)
                        {
                            this.txvHoraInicio.Text = this.gvLista.SelectedRow.Cells[4].Text.Substring(0, 2);
                            this.txvMinutoInicio.Text = this.gvLista.SelectedRow.Cells[4].Text.Substring(2, 2);
                        }
                    }
                }
                else
                {
                    this.txvHoraInicio.Text = "";
                    this.txvMinutoInicio.Text = "";
                }

                if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                    this.txvHorasTurno.Text = this.gvLista.SelectedRow.Cells[5].Text;

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[6].Controls)
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


        protected void gvLista_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }

        protected void gvLista_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int i = 0;
            this.Session["indice"] = e.RowIndex;

            if (((GridView)this.gvLista.Rows[e.RowIndex].FindControl("gvDepartamento")).Visible == false)
            {
                ((GridView)this.gvLista.Rows[e.RowIndex].FindControl("gvDepartamento")).Visible = true;
                ((GridView)this.gvLista.Rows[e.RowIndex].FindControl("gvDepartamento")).DataSource = turnos.GetDepartamentosTurno(gvLista.Rows[e.RowIndex].Cells[3].Text, Convert.ToInt16(Session["empresa"]));
                ((GridView)this.gvLista.Rows[e.RowIndex].FindControl("gvDepartamento")).DataBind();
                ((ImageButton)this.gvLista.Rows[e.RowIndex].FindControl("imbDepartamentos")).ImageUrl = "~/Imagen/TabsIcon/Atras.png";
                ((ImageButton)this.gvLista.Rows[e.RowIndex].FindControl("imbDepartamentos")).ToolTip = "Regresar";
                ((ImageButton)this.gvLista.Rows[e.RowIndex].FindControl("imbGuardar")).Visible = true;

                foreach (DataRowView registro in turnos.GetDepartamentosTurno(gvLista.Rows[e.RowIndex].Cells[3].Text, Convert.ToInt16(Session["empresa"])))
                {
                    foreach (Control objControl in ((GridView)this.gvLista.Rows[e.RowIndex].FindControl("gvDepartamento")).Rows[i].Cells[2].Controls)
                    {
                        if (objControl is CheckBox)
                        {
                            ((CheckBox)objControl).Checked = Convert.ToBoolean(registro.Row.ItemArray.GetValue(2));
                        }
                    }

                    i++;
                }

                foreach (GridViewRow fila in this.gvLista.Rows)
                {
                    if (fila.RowIndex != e.RowIndex)
                    {
                        fila.Visible = false;
                    }
                }
            }
            else
            {
                ((GridView)this.gvLista.Rows[e.RowIndex].FindControl("gvDepartamento")).Visible = false;
                ((GridView)this.gvLista.Rows[e.RowIndex].FindControl("gvDepartamento")).DataSource = null;
                ((GridView)this.gvLista.Rows[e.RowIndex].FindControl("gvDepartamento")).DataBind();
                ((ImageButton)this.gvLista.Rows[e.RowIndex].FindControl("imbDepartamentos")).ImageUrl = "~/Imagen/TabsIcon/edit_add.png";
                ((ImageButton)this.gvLista.Rows[e.RowIndex].FindControl("imbDepartamentos")).ToolTip = "Clic aquí para asignar departamentos al perfil";
                ((ImageButton)this.gvLista.Rows[e.RowIndex].FindControl("imbGuardar")).Visible = false;

                foreach (GridViewRow fila in this.gvLista.Rows)
                {
                    fila.Visible = true;
                }
            }
        }

        protected void imbGuardar_Click(object sender, ImageClickEventArgs e)
        {
            bool verificacion = true;
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    object[] objKey = new object[] { Convert.ToInt16(Session["empresa"]), this.gvLista.Rows[Convert.ToInt16(this.Session["indice"])].Cells[3].Text };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("nTurnoDepartamento", "elimina", "ppa", objKey))
                    {
                        case 0:

                            foreach (Control objControl in this.gvLista.Rows[Convert.ToInt16(this.Session["indice"])].Cells[2].Controls)
                            {
                                if (objControl is GridView)
                                {
                                    foreach (GridViewRow registro in ((GridView)objControl).Rows)
                                    {
                                        if (((CheckBox)registro.FindControl("chkAsignado")).Checked == true)
                                        {
                                            object[] objValores = new object[] { ((CheckBox)registro.FindControl("chkAsignado")).Checked, registro.Cells[0].Text, Convert.ToInt16(Session["empresa"]),
                                            this.gvLista.Rows[Convert.ToInt16(this.Session["indice"])].Cells[3].Text };

                                            switch (CentidadMetodos.EntidadInsertUpdateDelete("nTurnoDepartamento", "inserta", "ppa", objValores))
                                            {
                                                case 1:
                                                    verificacion = false;
                                                    break;
                                            }
                                        }
                                    }

                                    if (verificacion == false)
                                        ManejoError("Error al insertar la asignación de departamentos. Operación no realizada", "I");
                                    else
                                    {
                                        ManejoExito("Asignación registrada satisfactoriamente", "I");
                                        ts.Complete();
                                    }
                                }
                            }
                            break;
                        case 1:
                            ManejoError("Error al actualizar la asignación de departamentos. Operación no realizada", "I");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar la asignación de departamentos. Correspondiente a: " + ex.Message, "I");
            }
        }

        protected void nilblRegresar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Programacion.aspx");
        }



        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }

        protected void nilbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                                ConfigurationManager.AppSettings["Modulo"].ToString(),
                                 nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;

            CargarCombos();
            Consecutivo();
            txtCodigo.Enabled = false;
            txtDescripcion.Enabled = false;
            txtDescripcion.Focus();

            this.nilblInformacion.Text = "";
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Trim().ToString().Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Campos vacios por favor corrija", "warning");
                return;
            }
            Guardar();
        }

        #endregion Eventos
    }
}