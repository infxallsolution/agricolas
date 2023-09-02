using Contabilidad.WebForms.App_Code.Administracion;
using Contabilidad.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.Padministracion
{
    public partial class Perfiles : BasePage
    {

        #region Instancias

        Cperfiles perfiles = new Cperfiles();



        #endregion Instancias

        #region Metodos

        static string limpiarMensaje(string strIn)
        {
            // Replace invalid characters with empty strings.
            try
            {
                return Regex.Replace(strIn, @"[^\w\.@-]", " ",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters, 
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
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
                if (seguridad.VerificaAccesoOperacion(
                                this.Session["usuario"].ToString(),//usuario
                                 ConfigurationManager.AppSettings["modulo"].ToString(),//sitio
                                 nombrePaginaActual(),//pagina
                                consulta,//operacion
                               Convert.ToInt16(this.Session["empresa"]))//empresa
                               == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", consulta);
                    return;
                }

                this.gvLista.DataSource = perfiles.BuscarEntidad(
                    this.nitxtBusqueda.Text);

                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(
                  this.Session["usuario"].ToString(),
                  consulta,
                   ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                  "ex",
                  this.gvLista.Rows.Count.ToString() + " Registros encontrados",
                 HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente a: " + limpiarMensaje(ex.Message), "C");
            }
        }

        private void ManejoError(string error, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), error, "error");

            seguridad.InsertaLog(Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + Page.ToString(),
                "er", error, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
        }

        private void ManejoExito(string mensaje, string operacion)
        {

            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");

            CcontrolesUsuario.InhabilitarControles(Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);
            nilbNuevo.Visible = true;
            seguridad.InsertaLog(Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + Page.ToString(),
                "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }


        private void CargarCombos()
        {
        }

        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text, Convert.ToInt16(Session["empresa"]) };

            try
            {
                if (CentidadMetodos.EntidadGetKey(
                    "cperfiles",
                    "ppa",
                    objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    this.nilblInformacion.Text = "Perfil " + this.txtCodigo.Text + " ya se encuentra registrada";

                    CcontrolesUsuario.InhabilitarControles(
                        this.Page.Controls);

                    this.nilbNuevo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al validar la llave primaria correspondiente a: " + limpiarMensaje(ex.Message), "C");
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
                this.chkActivo.Checked,
                this.txtCodigo.Text,
                this.txtDescripcion.Text,
                Convert.ToInt16(Session["empresa"])
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete(
                    "cperfiles",
                    operacion,
                    "ppa",
                    objValores))
                {
                    case 0:

                        ManejoExito("Datos insertados satisfactoriamente. ", operacion.Substring(0, 1).ToUpper());
                        break;

                    case 1:

                        ManejoError("Errores al insertar el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                        break;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar los datos correspondiente a: " + limpiarMensaje(ex.Message), operacion.Substring(0, 1).ToUpper());
            }
        }

        #endregion Metodos

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
            {
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            }
            else
            {
                if (seguridad.VerificaAccesoPagina(
                       this.Session["usuario"].ToString(),
                       ConfigurationManager.AppSettings["Modulo"].ToString(),
                       nombrePaginaActual(),
                       Convert.ToInt16(this.Session["empresa"])) != 0)
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

        protected void nilbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(
                                 this.Session["usuario"].ToString(),//usuario
                                  ConfigurationManager.AppSettings["modulo"].ToString(),//sitio
                                  nombrePaginaActual(),//pagina
                                 insertar,//operacion
                                Convert.ToInt16(this.Session["empresa"]))//empresa
                                == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", insertar);
                return;
            }
            CcontrolesUsuario.HabilitarControles(
                this.Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;

            CargarCombos();

            this.txtCodigo.Enabled = true;
            this.txtCodigo.Focus();
            this.nilblInformacion.Text = "";
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

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(
                this.Page.Controls);

            this.nilbNuevo.Visible = true;

            GetEntidad();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(
                                    this.Session["usuario"].ToString(),//usuario
                                     ConfigurationManager.AppSettings["modulo"].ToString(),//sitio
                                     nombrePaginaActual(),//pagina
                                    editar,//operacion
                                   Convert.ToInt16(this.Session["empresa"]))//empresa
                                   == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", editar);
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

                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                {
                    this.txtCodigo.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[2].Text);
                }
                else
                {
                    this.txtCodigo.Text = "";
                }

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                {
                    this.txtDescripcion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[3].Text);
                }
                else
                {
                    this.txtDescripcion.Text = "";
                }

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                {
                    foreach (Control objControl in this.gvLista.SelectedRow.Cells[4].Controls)
                    {
                        if (objControl is CheckBox)
                        {
                            this.chkActivo.Checked = ((CheckBox)objControl).Checked;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + limpiarMensaje(ex.Message), "C");
            }
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string operacion = "elimina";

            if (seguridad.VerificaAccesoOperacion(
                                 this.Session["usuario"].ToString(),//usuario
                                  ConfigurationManager.AppSettings["modulo"].ToString(),//sitio
                                  nombrePaginaActual(),//pagina
                                 eliminar,//operacion
                                Convert.ToInt16(this.Session["empresa"]))//empresa
                                == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", eliminar);
                return;
            }

            try
            {
                object[] objValores = new object[] {
                Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text),Convert.ToInt16(Session["empresa"])
            };

                if (CentidadMetodos.EntidadInsertUpdateDelete(
                    "cperfiles",
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
                ManejoError("Error al eliminar los datos correspondiente a: " + limpiarMensaje(ex.Message), "E");
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
                nilblInformacion.Text = "Campos vacios por favor corrija";
                return;
            }


            Guardar();
        }

        //protected void gvLista_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    int i = 0;
        //    this.Session["indice"] = e.RowIndex;

        //    if (((GridView)this.gvLista.Rows[e.RowIndex].FindControl("gvDepartamento")).Visible == false)
        //    {
        //        ((GridView)this.gvLista.Rows[e.RowIndex].FindControl("gvDepartamento")).Visible = true;
        //        ((GridView)this.gvLista.Rows[e.RowIndex].FindControl("gvDepartamento")).DataSource = perfiles.GetDepartamentosPerfil(
        //            this.gvLista.Rows[e.RowIndex].Cells[3].Text);
        //        ((GridView)this.gvLista.Rows[e.RowIndex].FindControl("gvDepartamento")).DataBind();
        //        ((ImageButton)this.gvLista.Rows[e.RowIndex].FindControl("imbDepartamentos")).ImageUrl = "~/Imagenes/botones/Atras.png";
        //        ((ImageButton)this.gvLista.Rows[e.RowIndex].FindControl("imbDepartamentos")).ToolTip = "Regresar";
        //        ((ImageButton)this.gvLista.Rows[e.RowIndex].FindControl("imbGuardar")).Visible = true;

        //        foreach (DataRowView registro in perfiles.GetDepartamentosPerfil(
        //            this.gvLista.Rows[e.RowIndex].Cells[3].Text))
        //        {
        //            foreach (Control objControl in ((GridView)this.gvLista.Rows[e.RowIndex].FindControl("gvDepartamento")).Rows[i].Cells[2].Controls)
        //            {
        //                if (objControl is CheckBox)
        //                {
        //                    ((CheckBox)objControl).Checked = Convert.ToBoolean(registro.Row.ItemArray.GetValue(2));
        //                }
        //            }

        //            i++;
        //        }

        //        foreach (GridViewRow fila in this.gvLista.Rows)
        //        {
        //            if (fila.RowIndex != e.RowIndex)
        //            {
        //                fila.Visible = false;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        ((GridView)this.gvLista.Rows[e.RowIndex].FindControl("gvDepartamento")).Visible = false;
        //        ((GridView)this.gvLista.Rows[e.RowIndex].FindControl("gvDepartamento")).DataSource = null;
        //        ((GridView)this.gvLista.Rows[e.RowIndex].FindControl("gvDepartamento")).DataBind();
        //        ((ImageButton)this.gvLista.Rows[e.RowIndex].FindControl("imbDepartamentos")).ImageUrl = "~/Imagenes/botones/inicio.png";
        //        ((ImageButton)this.gvLista.Rows[e.RowIndex].FindControl("imbDepartamentos")).ToolTip = "Clic aquí para asignar departamentos al perfil";
        //        ((ImageButton)this.gvLista.Rows[e.RowIndex].FindControl("imbGuardar")).Visible = false;

        //        foreach (GridViewRow fila in this.gvLista.Rows)
        //        {
        //            fila.Visible = true;
        //        }
        //    }
        //}

        //protected void imbGuardar_Click(object sender, EventArgs e)
        //{
        //    bool verificacion = true;

        //    try
        //    {
        //        using (TransactionScope ts = new TransactionScope())
        //        {
        //            object[] objKey = new object[]{
        //                this.gvLista.Rows[Convert.ToInt16(this.Session["indice"])].Cells[3].Text
        //            };

        //            switch (CentidadMetodos.EntidadInsertUpdateDelete(
        //                "perfilDepartamentos",
        //                "elimina",
        //                "ppa",
        //                objKey))
        //            {
        //                case 0:

        //                    foreach (Control objControl in this.gvLista.Rows[Convert.ToInt16(this.Session["indice"])].Cells[2].Controls)
        //                    {
        //                        if (objControl is GridView)
        //                        {
        //                            foreach (GridViewRow registro in ((GridView)objControl).Rows)
        //                            {
        //                                if (((CheckBox)registro.FindControl("chkAsignado")).Checked == true)
        //                                {
        //                                    object[] objValores = new object[]{
        //                                        ((CheckBox)registro.FindControl("chkAsignado")).Checked,
        //                                        registro.Cells[0].Text,
        //                                        this.gvLista.Rows[Convert.ToInt16(this.Session["indice"])].Cells[3].Text
        //                                    };

        //                                    switch (CentidadMetodos.EntidadInsertUpdateDelete(
        //                                        "perfilDepartamentos",
        //                                        "inserta",
        //                                        "ppa",
        //                                        objValores))
        //                                    {
        //                                        case 1:

        //                                            verificacion = false;
        //                                            break;
        //                                    }
        //                                }
        //                            }

        //                            if (verificacion == false)
        //                            {
        //                                ManejoError("Error al insertar la asignación de departamentos. Operación no realizada", "I");
        //                            }
        //                            else
        //                            {
        //                                ManejoExito("Asignación registrada satisfactoriamente", "I");
        //                                ts.Complete();
        //                            }
        //                        }
        //                    }

        //                    break;

        //                case 1:

        //                    ManejoError("Error al actualizar la asignación de departamentos. Operación no realizada", "I");
        //                    break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ManejoError("Error al guardar la signación de departamentos. Correspondiente a: " + limpiarMensaje(ex.Message), "I");
        //    }
        //}

        #endregion Eventos



        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }


    }
}