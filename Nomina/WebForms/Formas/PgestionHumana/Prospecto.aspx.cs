using Nomina.WebForms.App_Code.General;
using Nomina.WebForms.App_Code.GestionHumana;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.PgestionHumana
{
    public partial class Prospecto : BasePage
    {
        #region Instancias



        Cprospecto prospecto = new Cprospecto();


        private byte[] Foto
        {
            get { object o = Session["Foto"]; return (o == null) ? null : (byte[])o; }
            set { Session["Foto"] = value; }
        }

        #endregion Instancias

        #region Metodos


        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "C", Convert.ToInt32(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                this.gvLista.DataSource = prospecto.BuscarEntidad(this.nitxtBusqueda.Text, Convert.ToInt32(Session["empresa"]));
                this.gvLista.DataBind();
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";
                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                             this.gvLista.Rows.Count.ToString() + " Registros encontrados", ObtenerIP(), Convert.ToInt32(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente a: " + ex.Message, "C");
            }
        }



        private void ManejoExito(string mensaje, string operacion)
        {
            nilblInformacion.Text = mensaje;
            nilblInformacion.ForeColor = System.Drawing.Color.Green;
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            this.fuFoto.Visible = false;
            imbFuncionario.ImageUrl = "";
            this.imbFuncionario.Visible = false;
            txtFechaNacimeinto.Visible = false;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "ex", mensaje, ObtenerIP(), Convert.ToInt32(Session["empresa"]));
            GetEntidad();
        }

        private void CargarCombos()
        {
            try
            {
                this.ddlTercero.DataSource = CcontrolesUsuario.OrdenarEntidadTercero(CentidadMetodos.EntidadGet("cTercero", "ppa"), "razonSocial", "empleado", Convert.ToInt32(Session["empresa"]));
                this.ddlTercero.DataValueField = "id";
                this.ddlTercero.DataTextField = "razonSocial";
                this.ddlTercero.DataBind();
                this.ddlTercero.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar empleados. Correspondiente a: " + ex.Message, "C");
            }


            try
            {
                DataView ciudad = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("gCiudad", "ppa"), "nombre", Convert.ToInt32(this.Session["empresa"]));
                this.ddlCiudad.DataSource = ciudad;
                this.ddlCiudad.DataValueField = "codigo";
                this.ddlCiudad.DataTextField = "nombre";
                this.ddlCiudad.DataBind();
                this.ddlCiudad.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar ciudades. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                DataView ciudad = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("gEstadoCivil", "ppa"), "descripcion", Convert.ToInt32(this.Session["empresa"]));
                this.ddlEstadoCivil.DataSource = ciudad;
                this.ddlEstadoCivil.DataValueField = "codigo";
                this.ddlEstadoCivil.DataTextField = "descripcion";
                this.ddlEstadoCivil.DataBind();
                this.ddlEstadoCivil.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar estodo civil. Correspondiente a: " + ex.Message, "C");
            }
            try
            {
                this.ddlCiudadNacimineto.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("gCiudad", "ppa"), "nombre", Convert.ToInt32(Session["empresa"]));
                this.ddlCiudadNacimineto.DataValueField = "codigo";
                this.ddlCiudadNacimineto.DataTextField = "nombre";
                this.ddlCiudadNacimineto.DataBind();
                this.ddlCiudadNacimineto.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar ciudades. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlNivelEducativo.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("gNivelEducativo", "ppa"), "descripcion", Convert.ToInt32(Session["empresa"]));
                this.ddlNivelEducativo.DataValueField = "codigo";
                this.ddlNivelEducativo.DataTextField = "descripcion";
                this.ddlNivelEducativo.DataBind();
                this.ddlNivelEducativo.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar nivel educativo. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlRh.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("gRh", "ppa"), "descripcion", Convert.ToInt32(Session["empresa"]));
                this.ddlRh.DataValueField = "codigo";
                this.ddlRh.DataTextField = "descripcion";
                this.ddlRh.DataBind();
                this.ddlRh.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar grupos sanguineos. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlOcupacion.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nOcupacion", "ppa"), "descripcion", Convert.ToInt32(Session["empresa"]));
                this.ddlOcupacion.DataValueField = "codigo";
                this.ddlOcupacion.DataTextField = "descripcion";
                this.ddlOcupacion.DataBind();
                this.ddlOcupacion.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar ocupación. Correspondiente a: " + ex.Message, "C");
            }


        }

        private void EntidadKey()
        {
            object[] objKey = new object[] { Convert.ToInt32(Session["empresa"]), Convert.ToInt32(this.ddlTercero.SelectedValue) };
            try
            {
                if (CentidadMetodos.EntidadGetKey("nFuncionario", "ppa", objKey).Tables[0].Rows.Count > 0)
                {

                    MostrarMensaje("Funcionario " + Convert.ToString(this.ddlTercero.SelectedValue) + " ya se encuentra registrado");
                    CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
                    this.nilbNuevo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al validar la llave primaria correspondiente a: " + ex.Message, "C");
            }
        }
        private Boolean ValidarExtension(string sExtension)
        {
            Boolean verif = false;
            switch (sExtension)
            {
                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".gif":
                case ".bmp":
                    verif = true;
                    break;
                default:
                    verif = false;
                    break;
            }
            return verif;
        }
        private void Guardar()
        {
            string operacion = "inserta", nivelEducativo = null;
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {

                    if (Foto == null)
                    {
                        if (operacion == "inserta" && chkValidaFoto.Checked == true)
                        {
                            CcontrolesUsuario.MensajeError("Debe seleccionar la foto del funcionario", nilblInformacion);
                            return;
                        }
                    }

                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                        operacion = "actualiza";



                    if (ddlNivelEducativo.SelectedValue.Length == 0)
                        nivelEducativo = null;
                    else
                        nivelEducativo = ddlNivelEducativo.SelectedValue;

                    object[] objValores = new object[]{
                            Convert.ToDecimal(txvAltura.Text),
                            txtBarrio.Text,
                            txtCicatrices.Text,
                            ddlCiudad.SelectedValue,
                            ddlCiudadNacimineto.SelectedValue,
                            ddlContratos.SelectedValue,
                            txtDireccion.Text,
                            Convert.ToDecimal(txvEdad.Text),
                            txtEmail.Text,
                            Convert.ToInt32(Session["empresa"]),
                            ddlEstadoCivil.SelectedValue,
                            Convert.ToDateTime(txtFechaNacimeinto.Text),
                            Foto,
                            txtLimitaciones.Text,
                            ddlNivelEducativo.SelectedValue,
                            txtObservacion.Text,
                            ddlOcupacion.SelectedValue,
                            Convert.ToDecimal(txvPeso.Text),
                            ddlRh.SelectedValue,
                            ddlSexo.SelectedValue,
                            ddlCamisa.SelectedValue,
                            Convert.ToDecimal(txvPantalon.Text),
                            Convert.ToDecimal(txvZapato.Text),
                            txtTelefono.Text,
                            Convert.ToInt32(this.ddlTercero.SelectedValue),
                            };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("nProspecto", operacion, "ppa", objValores))
                    {
                        case 0:
                            ManejoExito("Datos insertados satisfactoriamente", operacion.Substring(0, 1).ToUpper());
                            ts.Complete();
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

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(),
                   ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt32(Session["empresa"])) != 0)
                {
                    this.nitxtBusqueda.Focus();
                    if (Convert.ToString(this.ddlTercero.SelectedValue).Length > 0)
                        this.ddlCiudadNacimineto.Focus();
                }
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }

        protected void lbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                   nombrePaginaActual(), "I", Convert.ToInt32(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            CargarCombos();
            this.ddlTercero.Enabled = true;
            this.ddlTercero.Focus();
            txtDireccion.Text = "";
            this.nilblInformacion.Text = "";
            this.fuFoto.Visible = true;
            imbFuncionario.ImageUrl = "";
            chkValidaFoto.Checked = true;
            Foto = null;
        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            txtFechaNacimeinto.Visible = false;
            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
            this.fuFoto.Visible = false;
            imbFuncionario.ImageUrl = "";
            Foto = null;
            this.imbFuncionario.Visible = false;
        }

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            this.imbFuncionario.Visible = false;
            this.fuFoto.Visible = false;
            Foto = null;
            txtFechaNacimeinto.Visible = false;
            GetEntidad();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {

            Guardar();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
            nombrePaginaActual(), "A", Convert.ToInt32(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "A");
                return;
            }
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            this.nilblInformacion.Text = "";
            this.ddlTercero.Enabled = false;
            this.ddlSexo.Focus();
            this.fuFoto.Visible = true;
            this.ddlTercero.Enabled = false;
            Foto = null;
            imbFuncionario.ImageUrl = "";
            int columna = 0;

            try
            {
                CargarCombos();
                DataView dvTercero = prospecto.RetornaDatosProspecto(this.gvLista.SelectedRow.Cells[2].Text.Trim(), Convert.ToInt32(gvLista.SelectedRow.Cells[5].Text), Convert.ToInt32(Session["empresa"]));

                if (dvTercero.Table.Rows[0].ItemArray.GetValue(1) != null)
                    this.Session["id"] = dvTercero.Table.Rows[0].ItemArray.GetValue(1).ToString();
                else
                    this.Session["id"] = null;

                if (dvTercero.Table.Rows.Count > 0)
                {
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(1) != null)
                    {
                        ddlTercero.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(1).ToString();
                        ddlTercero.Enabled = false;
                    }

                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(2) != null)
                    {
                        cargarContrato();
                        ddlContratos.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(2).ToString();
                        ddlContratos.Enabled = false;
                    }

                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(3) != null)
                        ddlOcupacion.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(3).ToString().Trim();

                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(4) != null)
                        txtFechaNacimeinto.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(4).ToString();
                    else
                        txtFechaNacimeinto.Text = "";

                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(4) != null)
                        txtFechaNacimeinto.Text = Convert.ToDateTime(dvTercero.Table.Rows[0].ItemArray.GetValue(4)).ToShortDateString();
                    else
                        txtFechaNacimeinto.Text = "";
                    columna = 5;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                        txvEdad.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                    else
                        txvEdad.Text = "0";
                    columna += 1;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                        ddlEstadoCivil.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();
                    columna += 1;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                        ddlCiudadNacimineto.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();
                    columna += 1;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                        txvAltura.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                    else
                        txvAltura.Text = "0";
                    columna += 1;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                        txvPeso.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                    else
                        txvPeso.Text = "0";
                    columna += 1;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                        ddlSexo.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();
                    columna += 1;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                        ddlRh.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();
                    columna += 1;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                        txtCicatrices.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                    else
                        txtCicatrices.Text = "";
                    columna += 1;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                        txtLimitaciones.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                    else
                        txtLimitaciones.Text = "";
                    columna += 1;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                        ddlNivelEducativo.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();
                    columna += 1;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                        txtDireccion.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                    else
                        txtDireccion.Text = "";
                    columna += 1;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                        txtBarrio.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                    else
                        txtBarrio.Text = "";
                    columna += 1;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                        ddlCiudad.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();
                    columna += 1;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                        txtTelefono.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                    else
                        txtTelefono.Text = "";
                    columna += 1;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                        txtEmail.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                    else
                        txtEmail.Text = "";
                    columna += 1;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                        ddlCamisa.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString().Trim();
                    columna += 1;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                        txvPantalon.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                    else
                        txvPantalon.Text = "0";
                    columna += 1;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                        txvZapato.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                    else
                        txvZapato.Text = "0";
                    columna += 1;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(columna) != null)
                        txtObservacion.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(columna).ToString();
                    else
                        txtObservacion.Text = "0";

                    if (!string.IsNullOrWhiteSpace(Server.HtmlDecode(dvTercero.Table.Rows[0].ItemArray.GetValue(24).ToString())))
                    {
                        Foto = (dvTercero.Table.Rows[0].ItemArray.GetValue(24) is byte[]) ? (byte[])dvTercero.Table.Rows[0].ItemArray.GetValue(24) : null;
                        cargarFoto();
                    }
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }


        private void cargarFoto()
        {
            try
            {
                this.imbFuncionario.Visible = true;
                string urlFoto = string.Empty;
                if (Foto != null)
                {
                    urlFoto = "data:image/png;base64," + Convert.ToBase64String(Foto, Base64FormattingOptions.None);
                    this.imbFuncionario.ImageUrl = urlFoto;
                }
            }
            catch (Exception ex)
            {
                this.nilblInformacion.Text = "Error al recuperar la foto del funcionario. Correspondiente a: " + ex.Message;
            }
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "E", Convert.ToInt32(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }
            string operacion = "elimina";
            try
            {
                object[] objValores = new object[] { Convert.ToInt16(this.gvLista.Rows[e.RowIndex].Cells[5].Text), Convert.ToInt32(Session["empresa"]), Convert.ToInt16(this.gvLista.Rows[e.RowIndex].Cells[2].Text) };

                if (CentidadMetodos.EntidadInsertUpdateDelete("nProspecto", operacion, "ppa", objValores) == 0)
                    ManejoExito("Datos eliminados satisfactoriamente", "E");
                else
                    ManejoError("Errores al eliminar el registro. Operación no realizada", "E");
            }
            catch (Exception ex)
            {
                ManejoError("Error al eliminar los datos correspondiente a: " + ex.Message, "E");
            }
        }


        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }

        protected void chkValidaFoto_CheckedChanged(object sender, EventArgs e)
        {
            if (chkValidaFoto.Checked)
                fuFoto.Visible = true;
            else
                fuFoto.Visible = false;
        }
        #endregion Eventos

        protected void hiddenCommand_Click(object sender, EventArgs e)
        {
            if (this.fuFoto.HasFile)
            {
                Stream fs = fuFoto.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                Foto = br.ReadBytes((int)fs.Length);
            }
            else
            {
                CcontrolesUsuario.MensajeError("Debe seleccionar la foto del funcionario", nilblInformacion);
                return;
            }
            this.imbFuncionario.Visible = true;
            string urlFoto = string.Empty;
            if (Foto != null)
            {
                urlFoto = "data:image/png;base64," + Convert.ToBase64String(Foto, Base64FormattingOptions.None);
                this.imbFuncionario.ImageUrl = urlFoto;
            }
        }

        protected void txtFechaNacimiento_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaNacimiento;
                fechaNacimiento = Convert.ToDateTime(txtFechaNacimeinto.Text);
            }
            catch (Exception ex)
            {
                CcontrolesUsuario.MensajeError("Formato de fecha incorrecto", nilblInformacion);
            }
        }

        protected void ddlEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTercero.SelectedValue.Length > 0)
                cargarContrato();

        }

        private void cargarContrato()
        {
            try
            {
                DataView dvContratos = prospecto.SelecccionaContratosTercero(Convert.ToInt32(ddlTercero.SelectedValue), Convert.ToInt32(this.Session["empresa"]));
                this.ddlContratos.DataSource = dvContratos;
                this.ddlContratos.DataValueField = "noContrato";
                this.ddlContratos.DataTextField = "desContrato";
                this.ddlContratos.DataBind();
                this.ddlContratos.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar contratos de tercero. Correspondiente a: " + ex.Message, "C");
            }
        }
    }
}