using Contabilidad.WebForms.App_Code.Administracion;
using Contabilidad.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.Padministracion
{
    public partial class ConfigIR : BasePage
    {
        #region Instancias

        CConfigClaseIR configimpret = new CConfigClaseIR();


        #endregion Instancias

        #region Metodos

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
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                this.gvLista.DataSource = configimpret.BuscarEntidad(
                    this.nitxtBusqueda.Text, Convert.ToInt16(this.Session["empresa"]));

                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.ToString(),
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

            CcontrolesUsuario.InhabilitarControles(this.Controls);
            CcontrolesUsuario.LimpiarControles(this.Controls);
            nilbNuevo.Visible = true;
            seguridad.InsertaLog(Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.ToString(),
                "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }
        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text, Convert.ToInt16(this.Session["empresa"]) };

            try
            {
                if (CentidadMetodos.EntidadGetKey(
                    "cClaseIR",
                    "ppa",
                    objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    this.nilblInformacion.Text = "Centro de Costo " + this.txtCodigo.Text + " ya se encuentra registrado";

                    CcontrolesUsuario.InhabilitarControles(
                        this.Controls);

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
            bool impuesto = false;
            bool retencion = false;
            bool libreimpuesto = false;
            bool autorretenedor = false;
            bool regimensimplificado = false;
            bool responsableImpuesto = false;
            string codigo = "";
            try
            {

                if (Convert.ToBoolean(this.Session["editar"]) == true)
                {
                    operacion = "actualiza";
                    codigo = txtCodigo.Text;
                }
                else
                {
                    codigo = Cgeneral.RetornaConsecutivoAutomatico("cConfigClaseIR", "codigo", Convert.ToInt16(this.Session["empresa"]));
                }

                switch (rbCaracteristica.SelectedValue)
                {
                    case "AU":
                        autorretenedor = true;
                        break;
                    case "LI":
                        libreimpuesto = true;
                        break;
                    case "RS":
                        regimensimplificado = true;
                        break;

                    case "RI":
                        responsableImpuesto = true;
                        break;
                }

                retencion = chkRetencion.Checked;
                impuesto = chkImpuesto.Checked;

                object[] objValores = new object[]{
                    chkActivo.Checked,    //@activo
                    autorretenedor,   // @auretenedor	bit
                    codigo,   //@codigo	int
                    txtDescripcion.Text,    //@descripcion	varchar
                    Convert.ToInt32(this.Session["empresa"]),    //@empresa	int
                    impuesto,    //@impuesto	bit
                    libreimpuesto,   //@libre	bit
                    chkCargaLLave.Checked,
                    regimensimplificado,   //@regimensimplificado	bit
                    responsableImpuesto, //@responsableimpuesto
                    retencion   //@retencion	bit
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("cConfigClaseIR", operacion, "ppa", objValores))
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
                   this.Session["usuario"].ToString(),
                   ConfigurationManager.AppSettings["modulo"].ToString(),
                   nombrePaginaActual(),
                   insertar,
                   Convert.ToInt32(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "I");
                return;
            }


            CcontrolesUsuario.HabilitarControles(this.Controls);
            CcontrolesUsuario.LimpiarControles(this.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            this.txtCodigo.Enabled = false;
            this.txtCodigo.Text = Cgeneral.RetornaConsecutivoAutomatico("cConfigClaseIR", "codigo", Convert.ToInt16(this.Session["empresa"]));
            chkImpuesto.Visible = true;
            this.txtDescripcion.Focus();
            this.nilblInformacion.Text = "";
        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Controls);
            CcontrolesUsuario.LimpiarControles(this.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.nilbNuevo.Visible = true;
            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
        }
        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Controls);
            this.nilblInformacion.Text = "";
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

            CcontrolesUsuario.HabilitarControles(this.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;

            this.txtCodigo.Enabled = false;
            this.txtDescripcion.Focus();

            try
            {

                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                {
                    this.txtCodigo.Text = this.gvLista.SelectedRow.Cells[2].Text;
                    this.txtCodigo.Enabled = false;
                }
                else
                {
                    this.txtCodigo.Text = "";
                }

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                {
                    this.txtDescripcion.Text = this.gvLista.SelectedRow.Cells[3].Text;
                }
                else
                {
                    this.txtDescripcion.Text = "";
                }


                foreach (Control c in gvLista.SelectedRow.Cells[4].Controls)
                {
                    if (c is CheckBox)
                    {
                        if (((CheckBox)c).Checked)
                        {
                            chkRetencion.Checked = ((CheckBox)c).Checked;

                        }
                    }

                }

                foreach (Control c in gvLista.SelectedRow.Cells[5].Controls)
                {
                    if (c is CheckBox)
                    {
                        if (((CheckBox)c).Checked)
                        {
                            chkRetencion.Checked = ((CheckBox)c).Checked;

                        }
                    }

                }


                foreach (Control c in gvLista.SelectedRow.Cells[6].Controls)
                {
                    if (c is CheckBox)
                    {
                        if (((CheckBox)c).Checked)
                        {
                            rbCaracteristica.SelectedValue = "AU";
                        }

                    }

                }

                foreach (Control c in gvLista.SelectedRow.Cells[7].Controls)
                {
                    if (c is CheckBox)
                    {
                        if (((CheckBox)c).Checked)
                        {
                            rbCaracteristica.SelectedValue = "LI";
                        }

                    }

                }


                foreach (Control c in gvLista.SelectedRow.Cells[8].Controls)
                {
                    if (c is CheckBox)
                    {
                        if (((CheckBox)c).Checked)
                        {
                            rbCaracteristica.SelectedValue = "RS";
                        }

                    }

                }


                foreach (Control c in gvLista.SelectedRow.Cells[9].Controls)
                {
                    if (c is CheckBox)
                    {
                        if (((CheckBox)c).Checked)
                        {
                            rbCaracteristica.SelectedValue = "RI";
                        }

                    }

                }

                foreach (Control c in gvLista.SelectedRow.Cells[10].Controls)
                {
                    if (c is CheckBox)
                    {
                        chkCargaLLave.Checked = ((CheckBox)c).Checked;
                    }

                }

                foreach (Control c in gvLista.SelectedRow.Cells[11].Controls)
                {
                    if (c is CheckBox)
                    {
                        chkActivo.Checked = ((CheckBox)c).Checked;
                    }

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
                Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)),
                Convert.ToInt16(this.Session["empresa"])
     };

                if (CentidadMetodos.EntidadInsertUpdateDelete(
                    "cConfigClaseIR",
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
        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(this.Session["editar"]) == false)
                EntidadKey();
        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Length == 0 || txtDescripcion.Text.Length == 0
                )
            {
                ManejoError("Campos vacios por favor corrija", "I");
                return;
            }

            Guardar();
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.DataBind();
            GetEntidad();
        }

        #endregion Eventos
    }
}