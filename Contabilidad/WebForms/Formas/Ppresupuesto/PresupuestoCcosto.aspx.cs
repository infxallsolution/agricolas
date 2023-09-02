using Contabilidad.WebForms.App_Code.Presupuesto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.App_Code.General
{
    public partial class PresupuestoCcosto : BasePage
    {
        #region Instancias

        CplanPresupuestal plan = new CplanPresupuestal();
        cParametrosPresupuesto parametros = new cParametrosPresupuesto();
        CcostoPresupuesto ccostopre = new CcostoPresupuesto();

        #endregion Instancias

        #region Metodos

        private void EntidadKey()
        {
            object[] objKey = new object[] {
              ddlCcostoPresupuesto.SelectedValue.Trim(),      //            @ccostoPresupuesto varchar
              ddlCcostoSiesa.SelectedValue.Trim(),      //@ccostoSiesa varchar
              Convert.ToInt16(this.Session["empresa"]) ,     //@empresa    int
              lblCuentaPresupuesto.Text.Trim(),      //@idCuentaPresupuesto    varchar
              ddlCuentaSiesa.SelectedValue      //@idCuentasiesa  varchar
         };

            try
            {
                if (CentidadMetodos.EntidadGetKey("cParametrosPresupuestoDetalle", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    ManejoError(" la combinación  ya se encuentra registrado", "I");
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al validar la llave primaria correspondiente a: " + ex.Message, "C");
            }
        }
        private void CargarCombos()
        {

            DataView ccostospresupuesto = plan.RetornaPlanPresupuestoManejanCcosto(Convert.ToInt16(this.Session["empresa"]));
            this.ddlCuentaSiesa.DataSource = ccostospresupuesto;
            this.ddlCuentaSiesa.DataValueField = "codigo";
            this.ddlCuentaSiesa.DataTextField = "nombre";
            this.ddlCuentaSiesa.DataBind();
            this.ddlCuentaSiesa.Items.Insert(0, new ListItem("", ""));
        }

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
                //if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(),//pagina
                //                consulta, Convert.ToInt16(this.Session["empresa"])) == 0)
                //{
                //    ManejoError("Usuario no autorizado para ejecutar esta operación", consulta);
                //    return;
                //}
                this.gvLista.DataSource = parametros.BuscarEntidad(this.nitxtBusqueda.Text, Convert.ToInt16(this.Session["empresa"]));
                this.gvLista.DataBind();
                this.nilblInformacion.Visible = true;
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";

                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                   "ex", this.gvLista.Rows.Count.ToString() + " Registros encontrados", HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(this.Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla. Correspondiente a: " + ex.Message, "C");
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


        private void Guardar()
        {
            string operacion = "inserta";
            string padre = null;

            try
            {
                if (Convert.ToBoolean(this.Session["editar"]) == true)
                    operacion = "actualiza";

                if (ddlCcostoSiesa.SelectedValue.Trim().Length == 0)
                {
                    ManejoError("Debe seleccionar una centro de costo de siesa valido", "I");
                    return;
                }

                if (ddlCuentaSiesa.SelectedValue.Trim().Length == 0)
                {
                    ManejoError("Debe seleccionar una cuenta de siesa valida", "I");
                    return;
                }


                object[] objValores = new object[] {
                   ddlCcostoPresupuesto.SelectedValue.Trim(),             //@ccostoPresupuesto  varchar
                   ddlCcostoSiesa.SelectedValue.Trim(),              //@ccostoSiesa    varchar
                   Convert.ToInt16(this.Session["empresa"]),             //@empresa    int
                   DateTime.Now ,            //@fechaRegistro  datetime
                   lblCuentaPresupuesto.Text.Trim(),            //@idCuentaPresupuesto    varchar
                   ddlCuentaSiesa.SelectedValue.Trim(),             //@idCuentasiesa  varchar
                   ddlTipo.SelectedValue.Trim(), //@tipo varchar
                   this.Session["usuario"].ToString()             //@usuario    varchar
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete(
                    "cParametrosPresupuestoDetalle",
                    operacion,
                    "ppa",
                    objValores))
                {
                    case 0:
                        ManejoExito("Registro creado satisfactoriamente", operacion.Substring(0, 1).ToUpper());
                        break;
                    case 1:
                        ManejoError("Error al crear el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                        break;
                }

            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar el registro. Correspondiente a: " + limpiarMensaje(ex.Message), operacion.Substring(0, 1).ToUpper());
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
                //if (seguridad.VerificaAccesoPagina(
                //       this.Session["usuario"].ToString(),
                //       ConfigurationManager.AppSettings["Modulo"].ToString(),
                //       nombrePaginaActual(),
                //       Convert.ToInt16(this.Session["empresa"])) != 0)
                //{
                //    this.nitxtBusqueda.Focus();


                //}
                //else
                //{
                //    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
                //}
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

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(
                this.Page.Controls);

            this.nilbNuevo.Visible = true;

            GetEntidad();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            //if (txtCodigo.Text.Length == 0 || txtNombre.Text.Length == 0 || txtNivel.Text.Length == 0)
            //{
            //    ManejoError("Campos vacios por favor corrija", "I");
            //    return;
            //}
            object[] objKey = new object[] {
              ddlCcostoPresupuesto.SelectedValue.Trim(),      //            @ccostoPresupuesto varchar
              ddlCcostoSiesa.SelectedValue.Trim(),      //@ccostoSiesa varchar
              Convert.ToInt16(this.Session["empresa"]) ,     //@empresa    int
              lblCuentaPresupuesto.Text.Trim(),      //@idCuentaPresupuesto    varchar
              ddlCuentaSiesa.SelectedValue      //@idCuentasiesa  varchar
         };

            try
            {
                if (CentidadMetodos.EntidadGetKey("cParametrosPresupuestoDetalle", "ppa", objKey).Tables[0].Rows.Count > 0)
                {
                    this.nilblInformacion.Visible = true;
                    ManejoError(" la combinación  ya se encuentra registrada", "I");
                    return;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al validar la llave primaria correspondiente a: " + ex.Message, "C");
            }


            Guardar();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
            //                     nombrePaginaActual(), editar, Convert.ToInt16(this.Session["empresa"])) == 0)
            //{
            //    ManejoError("Usuario no autorizado para ejecutar esta operación", editar);
            //    return;
            //}
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            //this.txtCodigo.Enabled = false;
            //this.txtNivel.Enabled = false;
            //this.txtNombre.Focus();

            try
            {
                CargarCombos();

                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.ddlCuentaSiesa.Text = this.gvLista.SelectedRow.Cells[2].Text;
                else
                    this.ddlCuentaSiesa.Text = "";




                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                {
                    this.lblCuentaPresupuesto.Text = this.gvLista.SelectedRow.Cells[4].Text;
                    this.lblCuentaNombrePresupuesto.Text = this.gvLista.SelectedRow.Cells[5].Text;
                }
                else
                {
                    this.lblCuentaPresupuesto.Text = "";
                    this.lblCuentaNombrePresupuesto.Text = "";
                }

                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                    this.ddlTipo.SelectedValue = this.gvLista.SelectedRow.Cells[6].Text;
                else
                    this.ddlTipo.SelectedValue = "";

                CargarCcostosSiesa();
                CargarCcostosPrespuesto();

                if (this.gvLista.SelectedRow.Cells[7].Text != "&nbsp;")
                    this.ddlCcostoSiesa.Text = this.gvLista.SelectedRow.Cells[7].Text;
                else
                    this.ddlCcostoSiesa.Text = "";

                if (this.gvLista.SelectedRow.Cells[9].Text != "&nbsp;")
                    this.ddlCcostoPresupuesto.Text = this.gvLista.SelectedRow.Cells[9].Text;
                else
                    this.ddlCcostoPresupuesto.Text = "";

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos. Correspondiente a: " + ex.Message, "A");
            }
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string operacion = "elimina";

            try
            {
                //if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
                //                 nombrePaginaActual(), eliminar, Convert.ToInt16(this.Session["empresa"])) == 0)
                //{
                //    ManejoError("Usuario no autorizado para ejecutar esta operación", eliminar);
                //    return;
                //}

                object[] objValores = new object[] {
                    Convert.ToInt16(this.Session["empresa"] ),    // @empresa   int
                     Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[8].Text)   ,     //@idCcostoPresupuesto    varchar     
                          Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[6].Text)   , //@idccostoSiesa  varchar
                              Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[3].Text)   ,   //@idcuentaPresupuesto    varchar
                                 Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[1].Text)//@idCuentasiesa  varchar
            };

                if (CentidadMetodos.EntidadInsertUpdateDelete("cParametrosPresupuestoDetalle", operacion, "ppa", objValores) == 0)
                    ManejoExito("Registro eliminado satisfactoriamente", "E");
                else
                    ManejoError("Error al eliminar el registro. Operación no realizada", "E");
            }
            catch (Exception ex)
            {
                ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
            }
        }

        protected void txtConcepto_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();

            //this.txtNombre.Focus();
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCcostosPrespuesto();
            CargarCcostosSiesa();
        }

        protected void nilbInforme_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("ListadoPuc.aspx");
        }

        protected void nilbNuevo_Click(object sender, EventArgs e)
        {
            //if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(),
            //                     nombrePaginaActual(), insertar, Convert.ToInt16(this.Session["empresa"])) == 0)
            //{
            //    ManejoError("Usuario no autorizado para ejecutar esta operación", insertar);
            //    return;
            //}
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            this.ddlCcostoPresupuesto.DataSource = null;
            this.ddlCcostoPresupuesto.DataBind();
            this.ddlCcostoSiesa.DataSource = null;
            this.ddlCcostoSiesa.DataBind();
            this.ddlCcostoPresupuesto.DataSource = null;
            this.ddlCcostoPresupuesto.DataBind();
            CargarCombos();
            //this.txtCodigo.Focus();
        }
        #endregion Eventos

        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvLista.DataBind();
            GetEntidad();
        }

        protected void ddlCuentaSiesa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRowView drv in plan.retornaCuentaPresupuesto(Convert.ToInt16(this.Session["empresa"]), ddlCuentaSiesa.SelectedValue.Trim()))
                {
                    lblCuentaPresupuesto.Text = drv.Row.ItemArray.GetValue(0).ToString();
                    lblCuentaNombrePresupuesto.Text = drv.Row.ItemArray.GetValue(1).ToString();
                }
                CargarCcostosSiesa();
                CargarCcostosPrespuesto();

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la cuenta presupuesto debido a :" + ex.Message, "C");

            }

        }

        private void CargarCcostosPrespuesto()
        {
            DataView ccostospresupuesto = ccostopre.retornaCcostoPresupuesto(Convert.ToInt16(this.Session["empresa"]));
            this.ddlCcostoPresupuesto.DataSource = ccostospresupuesto;
            this.ddlCcostoPresupuesto.DataValueField = "codigo";
            this.ddlCcostoPresupuesto.DataTextField = "nombre";
            this.ddlCcostoPresupuesto.DataBind();
            this.ddlCcostoPresupuesto.Items.Insert(0, new ListItem("", ""));
        }

        private void CargarCcostosSiesa()
        {
            if (ddlTipo.SelectedValue == "A")
            {

                DataView ccostospresupuesto = ccostopre.RetornaCcostoSiesaCuenta(Convert.ToInt16(this.Session["empresa"]), ddlCuentaSiesa.SelectedValue.Trim());
                this.ddlCcostoSiesa.DataSource = ccostospresupuesto;
                this.ddlCcostoSiesa.DataValueField = "codigo";
                this.ddlCcostoSiesa.DataTextField = "nombre";
                this.ddlCcostoSiesa.DataBind();
                this.ddlCcostoSiesa.Items.Insert(0, new ListItem("", ""));
            }
            else
            {
                DataView ccostospresupuesto = ccostopre.RetornaMCcostoSiesaCuenta(Convert.ToInt16(this.Session["empresa"]), ddlCuentaSiesa.SelectedValue.Trim());
                this.ddlCcostoSiesa.DataSource = ccostospresupuesto;
                this.ddlCcostoSiesa.DataValueField = "codigo";
                this.ddlCcostoSiesa.DataTextField = "nombre";
                this.ddlCcostoSiesa.DataBind();
                this.ddlCcostoSiesa.Items.Insert(0, new ListItem("", ""));

            }
        }
    }
}