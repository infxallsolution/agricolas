using Contabilidad.WebForms.App_Code.Administracion;
using Contabilidad.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.Padministracion
{
    public partial class GrupoIR : BasePage
    {
        #region Instancias

        CgrupoIR grupoIR = new CgrupoIR();

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
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(),
                                        ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "C", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                this.selConceptos.DataSource = null;
                this.selConceptos.DataBind();
                selConceptos.Visible = false;

                this.gvLista.DataSource = grupoIR.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();

                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(
                    this.Session["usuario"].ToString(), "C",
                  ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                    this.gvLista.Rows.Count.ToString() + " Registros encontrados", ObtenerIP(), Convert.ToInt16(this.Session["empresa"]));

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
            try
            {


                selConceptos.Visible = true;
                this.selConceptos.DataSource = grupoIR.SeleccionaImpuestos(Convert.ToInt16(Session["empresa"]));
                this.selConceptos.DataValueField = "codigo";
                this.selConceptos.DataTextField = "concadenacion";
                this.selConceptos.DataBind();


            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los IR correspondiente a: " + limpiarMensaje(ex.Message), "C");
            }

        }

        private void ValidaRegistro()
        {

            for (int x = 0; x < selConceptos.Items.Count; x++)
            {

                if (grupoIR.VerificaConceptoyGrupoIR(txtCodigo.Text, selConceptos.Items[x].Value, Convert.ToInt16(Session["empresa"])) == 0)
                    selConceptos.Items[x].Selected = true;
                else
                    selConceptos.Items[x].Selected = false;
            }
        }

        private void EntidadKey()
        {
            object[] objKey = new object[] { this.txtCodigo.Text, Convert.ToInt16(Session["empresa"]) };
            try
            {
                if (CentidadMetodos.EntidadGetKey(
                    "cGrupoIR",
                    "ppa",
                    objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    this.nilblInformacion.Text = "Código " + this.txtCodigo.Text + " ya se encuentra registrado";

                    CcontrolesUsuario.InhabilitarControles(
                        this.Controls);

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
            bool verificacion = false;

            if (this.txtDescripcion.Text.Length == 0 || this.txtCodigo.Text.Length == 0)
            {
                this.nilblInformacion.Text = "Campos vacios por favor corrija";
            }
            else
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope())
                    {

                        if (Convert.ToBoolean(this.Session["editar"]) == true)
                        {
                            operacion = "actualiza";
                        }

                        object[] objValores = new object[]{
                    chkActivo.Checked,
                    this.txtCodigo.Text,
                    this.txtDescripcion.Text,
                    Convert.ToInt16(Session["empresa"]),
                    txtObservacion.Text

                };

                        switch (CentidadMetodos.EntidadInsertUpdateDelete(
                            "cGrupoIR",
                            operacion,
                            "ppa",
                            objValores))
                        {
                            case 0:
                                if (Convert.ToBoolean(this.Session["editar"]) == true)
                                {

                                    for (int x = 0; x < selConceptos.Items.Count; x++)
                                    {

                                        if (grupoIR.VerificaConceptoyGrupoIR(txtCodigo.Text, selConceptos.Items[x].Value, Convert.ToInt16(Session["empresa"])) == 0)
                                        {
                                            if (selConceptos.Items[x].Selected == false)
                                            {

                                                object[] objValoresConcepto = new object[]{
                                                        selConceptos.Items[x].Value,
                                                        Convert.ToInt16(Session["empresa"]),
                                                        this.txtCodigo.Text};

                                                switch (CentidadMetodos.EntidadInsertUpdateDelete(
                                                "cGrupoConceptoIR",
                                                "elimina",
                                                "ppa",
                                                objValoresConcepto))
                                                {
                                                    case 1:
                                                        verificacion = true;
                                                        break;
                                                }

                                            }

                                        }
                                        else
                                        {
                                            if (selConceptos.Items[x].Selected == true)
                                            {

                                                object[] objValoresConcepto = new object[]{
                                                        selConceptos.Items[x].Value,
                                                        Convert.ToInt16(Session["empresa"]),
                                                        this.txtCodigo.Text};

                                                switch (CentidadMetodos.EntidadInsertUpdateDelete(
                                                "cGrupoConceptoIR",
                                                "inserta",
                                                "ppa",
                                                objValoresConcepto))
                                                {
                                                    case 1:
                                                        verificacion = true;
                                                        break;
                                                }

                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    for (int x = 0; x < selConceptos.Items.Count; x++)
                                    {
                                        if (selConceptos.Items[x].Selected == true)
                                        {

                                            object[] objValoresConcepto = new object[]{
                                                        selConceptos.Items[x].Value,
                                                        Convert.ToInt16(Session["empresa"]),
                                                        this.txtCodigo.Text};

                                            switch (CentidadMetodos.EntidadInsertUpdateDelete(
                                            "cGrupoConceptoIR",
                                            operacion,
                                            "ppa",
                                            objValoresConcepto))
                                            {
                                                case 1:
                                                    verificacion = true;
                                                    break;
                                            }
                                        }
                                    }
                                }
                                break;
                            case 1:
                                verificacion = true;
                                break;

                        }

                        if (verificacion == true)
                        {
                            this.nilblInformacion.Text = "Error al insertar el detalle de la transacción. Operación no realizada";
                            return;
                        }

                        ManejoExito("Transacción registrada satisfactoriamente.", "I");
                        ts.Complete();
                    }

                }
                catch (Exception ex)
                {
                    ManejoError("Error al guardar los datos correspondiente a: " + limpiarMensaje(ex.Message), operacion.Substring(0, 1).ToUpper());
                }
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
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(),
                    ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) != 0)
                {
                    //if (Convert.ToBoolean(this.Session["labor"]) == true)
                    //    nilblRegresar.Visible = true;
                    //else
                    //    nilblRegresar.Visible = false;

                    this.txtCodigo.Focus();

                }
                else
                {
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
                }
            }
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

            CcontrolesUsuario.HabilitarControles(
                this.Controls);


            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            CargarCombos();

            this.txtDescripcion.Enabled = true;
            this.txtCodigo.Focus();
            this.nilblInformacion.Text = "";
        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(
                this.Controls);

            CcontrolesUsuario.LimpiarControles(
                this.Controls);

            this.gvLista.DataSource = null;
            this.gvLista.DataBind();

            this.selConceptos.DataSource = null;
            this.selConceptos.DataBind();

            selConceptos.Visible = false;

            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
            this.selConceptos.Visible = false;
            this.selConceptos.DataSource = null;
            this.selConceptos.DataBind();
        }

        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
            chkActivo.Focus();
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

                object[] objValores = new object[] {
                Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text),
                Convert.ToInt16(Session["empresa"])
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete(
                    "cGrupoIR",
                    "elimina",
                    "ppa",
                    objValores))
                {
                    case 0:
                        if (grupoIR.EliminaConceptosdelGrupo(Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text), Convert.ToInt16(Session["empresa"])) == 0)
                            ManejoExito("Registro eliminado satisfactoriamente", "E");

                        break;

                    case 1:

                        ManejoError("Error al eliminar el registro. Operación no realizada", "E");
                        break;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al eliminar el registro. Correspondiente a: " + limpiarMensaje(ex.Message), "E");
            }
        }

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            bool swValida = false;
            for (int x = 0; x < selConceptos.Items.Count; x++)
            {
                if (selConceptos.Items[x].Selected == true)
                {
                    swValida = true;
                }
            }

            if (swValida == false)
            {
                nilblInformacion.Text = "Debe seleccionar por lo menos un Imp/Rete";
                return;
            }

            Guardar();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            CcontrolesUsuario.HabilitarControles(
              this.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;

            selConceptos.Visible = true;
            CargarCombos();
            this.txtCodigo.Enabled = false;
            this.txtDescripcion.Focus();

            try
            {
                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                {
                    this.txtCodigo.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[2].Text);
                    ValidaRegistro();
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
                    this.txtObservacion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[4].Text);
                }
                else
                {
                    this.txtObservacion.Text = "";
                }

                if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                {
                    foreach (Control objControl in this.gvLista.SelectedRow.Cells[5].Controls)
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

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(
                this.Controls);

            this.nilbNuevo.Visible = true;

            GetEntidad();
        }

        protected void nilblRegresar_Click(object sender, EventArgs e)
        {
            // Response.Redirect("Labor.aspx");
        }

        #endregion Eventos



    }
}