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
    public partial class Destinos : BasePage
    {
        #region Instancias
        Security seguridad = new Security();

        Cdestinos destinos = new Cdestinos();

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


                this.gvLista.DataSource = destinos.BuscarEntidad(
                    this.nitxtBusqueda.Text, (int)this.Session["empresa"]);

                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(
                this.Session["usuario"].ToString(),
                "C",
                 ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "ex",
                this.gvLista.Rows.Count.ToString() + " Registros encontrados",
                HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(this.Session["empresa"]));
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

            seguridad.InsertaLog(this.Session["usuario"].ToString(),
                 operacion,
                  ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                 "er",
                 error,
                 HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(this.Session["empresa"]));

            this.Response.Redirect("~/Contabilidad/Error.aspx", false);
        }

        private void ManejoExito(string mensaje, string operacion)
        {
            this.nilblMensaje.Text = mensaje;

            CcontrolesUsuario.InhabilitarControles(
                this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(
                this.Page.Controls);

            this.nilbNuevo.Visible = true;

            seguridad.InsertaLog(
                this.Session["usuario"].ToString(),
               operacion,
                 ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "ex",
               mensaje,
                HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(this.Session["empresa"]));

            GetEntidad();
        }

        private void CargarCombos()
        {
            try
            {
                DataView nivel = CcontrolesUsuario.OrdenarEntidad(
                    CentidadMetodos.EntidadGet("iNivelDestino", "ppa"),
                    "descripcion", Convert.ToInt16(this.Session["empresa"]));
                nivel.RowFilter = "empresa=" + Convert.ToInt16(this.Session["empresa"]);
                this.ddlNivel.DataSource = nivel;
                this.ddlNivel.DataValueField = "codigo";
                this.ddlNivel.DataTextField = "descripcion";
                this.ddlNivel.DataBind();
                this.ddlNivel.Items.Insert(0, new ListItem("Seleccione una opción", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar Niveles. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlNivelPadre.DataSource = ((DataView)this.ddlNivel.DataSource);
                this.ddlNivelPadre.DataValueField = "codigo";
                this.ddlNivelPadre.DataTextField = "descripcion";
                this.ddlNivelPadre.DataBind();
                this.ddlNivelPadre.Items.Insert(0, new ListItem("Seleccione una opción", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar Niveles Padre. Correspondiente a: " + ex.Message, "C");
            }

            //try
            //{
            //    this.ddlCtaInversion.DataSource = puc.GetPucDestino("A", Convert.ToInt16(this.Session["empresa"]));
            //    this.ddlCtaInversion.DataValueField = "codigo";
            //    this.ddlCtaInversion.DataTextField = "cadena";
            //    this.ddlCtaInversion.DataBind();
            //    this.ddlCtaInversion.Items.Insert(0, new ListItem("Seleccione una opción", ""));
            //}
            //catch (Exception ex)
            //{
            //    ManejoError("Error al cargar cuentas de inversión. Correspondiente a: " + ex.Message, "C");
            //}

            try
            {
                this.ddlCtaGasto.DataSource = ((DataView)this.ddlCtaInversion.DataSource);
                this.ddlCtaGasto.DataValueField = "codigo";
                this.ddlCtaGasto.DataTextField = "cadena";
                this.ddlCtaGasto.DataBind();
                this.ddlCtaGasto.Items.Insert(0, new ListItem("Seleccione una opción", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar cuentas de gasto. Correspondiente a: " + ex.Message, "C");
            }
        }

        private void EntidadKey()
        {
            object[] objKey = new object[] {
            this.txtCodigo.Text,
            Convert.ToInt16(this.ddlNivel.SelectedValue), Convert.ToInt16(this.Session["empresa"])
        };

            try
            {
                if (CentidadMetodos.EntidadGetKey(
                    "iDestino",
                    "ppa",
                    objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    this.nilblInformacion.Text = "Destino " + this.txtCodigo.Text + " ya se encuentra registrado";

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
            int nivelPadre = 0;
            string padre = "";

            try
            {
                if (Convert.ToInt16(this.ddlNivel.SelectedValue) == 0)
                {
                    nivelPadre = 0;
                    padre = "";
                }
                else
                {
                    nivelPadre = Convert.ToInt16(this.ddlNivelPadre.SelectedValue);
                    padre = Convert.ToString(this.ddlPadre.SelectedValue);
                }

                if (Convert.ToBoolean(this.Session["editar"]) == true)
                {
                    operacion = "actualiza";
                }

                object[] objValores = new object[]{
                this.chkActivo.Checked,
                this.txtCodigo.Text,
                Convert.ToString(this.ddlCtaGasto.SelectedValue),
                Convert.ToString(this.ddlCtaInversion.SelectedValue),
                Server.HtmlDecode(this.txtDescripcion.Text),
                Convert.ToInt16(this.Session["empresa"]),
                Convert.ToInt16(this.ddlNivel.SelectedValue),
                nivelPadre,
                padre
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete(
                    "iDestino",
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

        protected void niimbBuscar_Click(object sender, ImageClickEventArgs e)
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

            this.nilblMensaje.Text = "";
            this.ddlNivel.Enabled = false;
            this.txtDescripcion.Focus();

            try
            {
                CargarCombos();

                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                {
                    this.txtCodigo.Text = this.gvLista.SelectedRow.Cells[2].Text;
                }
                else
                {
                    this.txtCodigo.Text = "";
                }

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                {
                    this.ddlNivel.SelectedValue = this.gvLista.SelectedRow.Cells[3].Text;
                }

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                {
                    this.ddlNivelPadre.SelectedValue = this.gvLista.SelectedRow.Cells[4].Text;
                }

                if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                {
                    this.ddlPadre.SelectedValue = this.gvLista.SelectedRow.Cells[5].Text;
                }

                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                {
                    this.txtDescripcion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[6].Text);
                }
                else
                {
                    this.txtDescripcion.Text = "";
                }

                if (this.gvLista.SelectedRow.Cells[7].Text != "&nbsp;")
                {
                    this.ddlCtaInversion.SelectedValue = this.gvLista.SelectedRow.Cells[7].Text;
                }

                if (this.gvLista.SelectedRow.Cells[8].Text != "&nbsp;")
                {
                    this.ddlCtaGasto.SelectedValue = this.gvLista.SelectedRow.Cells[8].Text;
                }

                if (this.gvLista.SelectedRow.Cells[9].Text != "&nbsp;")
                {
                    foreach (Control objControl in this.gvLista.SelectedRow.Cells[9].Controls)
                    {
                        this.chkActivo.Checked = ((CheckBox)objControl).Checked;
                    }
                }
                else
                {
                    this.chkActivo.Checked = false;
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


            string operacion = "elimina";

            try
            {
                object[] objValores = new object[] {
                Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text),
                Convert.ToInt16(this.Session["empresa"]),
                Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[3].Text)
            };

                if (CentidadMetodos.EntidadInsertUpdateDelete(
                    "iDestino",
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

            this.ddlNivelPadre.Focus();
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
                this.ddlNivelPadre.Enabled = false;
                this.ddlPadre.Enabled = false;
            }
            else
            {
                this.ddlNivelPadre.Enabled = true;
                this.ddlPadre.Enabled = true;
            }
        }

        protected void ddlNivelPadre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt16(this.ddlNivelPadre.SelectedValue) != Convert.ToInt16(this.ddlNivel.SelectedValue))
            {
                try
                {
                    this.ddlPadre.DataSource = destinos.GetDestinoNivel(
                        Convert.ToInt16(this.ddlNivelPadre.SelectedValue), Convert.ToInt16(this.Session["empresa"]));
                    this.ddlPadre.DataValueField = "codigo";
                    this.ddlPadre.DataTextField = "Descripcion";
                    this.ddlPadre.DataBind();
                    this.ddlPadre.Items.Insert(0, new ListItem("Seleccione una opción", ""));
                    this.ddlPadre.Focus();
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

        #endregion Eventos

        protected void lbRegistrar_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCodigo.Text.Length == 0 || txtCodigo.Text.Length == 0)
            {

                nilblMensaje.Text = "Campos vacios por favor corrija";
                return;
            }

            if (ddlCtaGasto.SelectedIndex.ToString().Length == 0 || ddlCtaInversion.SelectedIndex.ToString().Length == 0)
            {

                nilblMensaje.Text = "Seleccione una cuenta de gasto / inversión valida";
                return;
            }


            Guardar();
        }

        protected void lbNuevo_Click(object sender, ImageClickEventArgs e)
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

            this.ddlNivel.Enabled = true;
            this.ddlNivel.Focus();
            this.nilblInformacion.Text = "";
        }

    }
}