using Nomina.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Padministracion
{
    public partial class ModoCampos : BasePage
    {

        #region Instancias

        //  CtiposTransaccion tipoTransaccion = new CtiposTransaccion();
        //    Centidades entidades = new Centidades();



        string consulta = "C";
        string insertar = "I";
        string eliminar = "E";
        string imprime = "IM";
        string ingreso = "IN";
        string editar = "A";

        #endregion Instancias

        #region Metodos


        private void GetEntidad()
        {
            //try
            //{
            //    if (seguridad.VerificaAccesoOperacion(
            //                this.Session["usuario"].ToString(),
            //                 ConfigurationManager.AppSettings["modulo"].ToString(),
            //                 nombrePaginaActual(),
            //                consulta,
            //               Convert.ToInt16(this.Session["empresa"]))
            //               == 0)
            //    {
            //        ManejoError("Usuario no autorizado para ejecutar esta operación", consulta);
            //        return;
            //    }

            //    this.gvLista.DataSource = tipoTransaccion.BuscarEntidadCampo(
            //        this.nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
            //    this.gvLista.DataBind();

            //    this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

            //    seguridad.InsertaLog(
            //        this.Session["usuario"].ToString(),
            //        consulta,
            //         ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
            //        "ex",
            //        this.gvLista.Rows.Count.ToString() + " Registros encontrados",
            //        ObtenerIP(), Convert.ToInt16(Session["empresa"]));
            //}
            //catch (Exception ex)
            //{
            //    ManejoError("Error al cargar la tabla correspondiente a: " + ex.Message, "C");
            //}
        }



        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");

            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);

            this.nilbNuevo.Visible = true;

            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion,
                    ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                   "ex", mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }

        private void CargarCombos()
        {
            //try
            //{
            //    this.ddlEntidad.DataSource = entidades.BuscarEntidad();
            //    this.ddlEntidad.DataValueField = "name";
            //    this.ddlEntidad.DataTextField = "name";
            //    this.ddlEntidad.DataBind();
            //    this.ddlEntidad.Items.Insert(0, new ListItem("", ""));
            //}
            //catch (Exception ex)
            //{
            //    ManejoError("Error al cargar entidades. Correspondiente a: " + ex.Message, "C");
            //}

            //try
            //{
            //    this.ddlTipoTransaccion.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("gTipoTransaccion", "ppa"),
            //        "descripcion", Convert.ToInt16(Session["empresa"]));
            //    this.ddlTipoTransaccion.DataValueField = "codigo";
            //    this.ddlTipoTransaccion.DataTextField = "descripcion";
            //    this.ddlTipoTransaccion.DataBind();
            //    this.ddlTipoTransaccion.Items.Insert(0, new ListItem("", ""));
            //}
            //catch (Exception ex)
            //{
            //    ManejoError("Error al cargar tipo de transacción. Correspondiente a: " + ex.Message, "C");
            //}
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

                //object[] objValores = new object[]{                
                //    this.chkAplicaCliente.Checked,
                //    this.chkAplicaProveedor.Checked,
                //    this.chkAplicaTercero.Checked,
                //    Convert.ToString(this.ddlCampo.SelectedValue),
                //    Convert.ToInt16(Session["empresa"]),
                //    Convert.ToString(ddlEntidad.SelectedValue),
                //    this.chkTercero.Checked,
                //    this.chkTerceroDefecto.Checked,
                //    Convert.ToString(this.ddlTipoCampo.SelectedValue),
                //    Convert.ToString(this.ddlTipoTransaccion.SelectedValue)
                //};

                //switch (CentidadMetodos.EntidadInsertUpdateDelete(
                //    "gTipoTransaccionCampo",
                //    operacion,
                //    "ppa",
                //    objValores))
                //{
                //    case 0:

                //        ManejoExito("Datos insertados satisfactoriamente", operacion.Substring(0, 1).ToUpper());
                //        break;

                //    case 1:

                //        ManejoError("Errores al insertar el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                //        break;
                //}
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

                }
                else
                {
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
                }
            }
        }


        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }


        protected void lbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(
                               this.Session["usuario"].ToString(),
                                ConfigurationManager.AppSettings["modulo"].ToString(),
                                nombrePaginaActual(),
                               insertar,
                              Convert.ToInt16(this.Session["empresa"]))
                              == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", insertar);
                return;
            }

            CcontrolesUsuario.HabilitarControles(
                this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;

            CargarCombos();
            GetEntidad();

            this.ddlTipoTransaccion.Enabled = true;
            this.ddlTipoTransaccion.Focus();
            this.ddlEntidad.Enabled = true;
            this.ddlCampo.Enabled = true;
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

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(
                                this.Session["usuario"].ToString(),
                                 ConfigurationManager.AppSettings["modulo"].ToString(),
                                 nombrePaginaActual(),
                                eliminar,
                               Convert.ToInt16(this.Session["empresa"]))
                               == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", eliminar);
                return;
            }

            string operacion = "elimina";

            try
            {
                object[] objValores = new object[] {
                Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[4].Text),
                Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[3].Text),
                Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text)
            };

                if (CentidadMetodos.EntidadInsertUpdateDelete(
                    "gTipoTransaccionCampo",
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

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;

            GetEntidad();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(
                                this.Session["usuario"].ToString(),
                                 ConfigurationManager.AppSettings["modulo"].ToString(),
                                 nombrePaginaActual(),
                                editar,
                               Convert.ToInt16(this.Session["empresa"]))
                               == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", editar);
                return;
            }

            CcontrolesUsuario.HabilitarControles(
                this.Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;

            this.ddlTipoTransaccion.Enabled = false;
            this.ddlEntidad.Enabled = false;
            this.ddlCampo.Enabled = false;
            this.ddlTipoCampo.Focus();

            try
            {
                CargarCombos();


            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }

        protected void ddlEntidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.ddlCampo.Focus();

            //try
            //{
            //    this.ddlCampo.DataSource = entidades.GetEntidadCampoEntidad(
            //        Convert.ToString(((DropDownList)sender).SelectedValue));
            //    this.ddlCampo.DataValueField = "campo";
            //    this.ddlCampo.DataTextField = "campo";
            //    this.ddlCampo.DataBind();
            //    this.ddlCampo.Items.Insert(0, new ListItem("", ""));
            //}
            //catch (Exception ex)
            //{
            //    ManejoError("Error al cargar campos de la entidad. Correspondiente a: " + ex.Message, "C");
            //}
        }

        #endregion Eventos

    }
}