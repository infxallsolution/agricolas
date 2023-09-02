using Nomina.WebForms.App_Code.Administracion;
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
    public partial class RegistroInOut : BasePage
    {

        #region Instancias

        Ccuadrillas cuadrillas = new Ccuadrillas();
        Cdepartamentos departamentos = new Cdepartamentos();
        Cprogramacion programacion = new Cprogramacion();

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
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "C", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                if (nitxtFI.Text.Length == 0 || nitxtFF.Text.Length == 0)
                    return;

                this.gvLista.DataSource = programacion.BuscaRegistros(nitxtBusqueda.Text, Convert.ToDateTime(nitxtFI.Text), Convert.ToDateTime(nitxtFF.Text), Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();

                this.nilblInformacion.Visible = true;
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

        private void ManejoError(string error, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), error, "error");
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "er", error, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
        }

        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            this.selFuncionarios.Visible = false;
            //string busqueda = nitxtBusqueda.Text;
            //string fechaI = nitxtFI.Text;
            //string fechaF = nitxtFF.Text;
            nilblInformacion.Text = "";
            gvLista.DataSource = null;
            gvLista.DataBind();
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                 "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            //nitxtBusqueda.Text = busqueda;
            //nitxtFF.Text = fechaF;
            //nitxtFI.Text = fechaI;
            GetEntidad();
        }



        private void CargarCombos()
        {
            try
            {
                DataView dvCuadrilla = CentidadMetodos.EntidadGet("ncuadrilla", "ppa").Tables[0].DefaultView;
                dvCuadrilla.RowFilter = "empresa=" + this.Session["empresa"];
                dvCuadrilla.Sort = "descripcion";
                this.ddlCuadrilla.DataSource = dvCuadrilla;
                this.ddlCuadrilla.DataValueField = "codigo";
                this.ddlCuadrilla.DataTextField = "descripcion";
                this.ddlCuadrilla.DataBind();
                this.ddlCuadrilla.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar cuadrillas. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                DataView dvTurno = CentidadMetodos.EntidadGet("nturno", "ppa").Tables[0].DefaultView;
                dvTurno.RowFilter = "empresa=" + this.Session["empresa"];
                dvTurno.Sort = "descripcion";
                this.ddlTurno.DataSource = dvTurno;
                this.ddlTurno.DataValueField = "codigo";
                this.ddlTurno.DataTextField = "descripcion";
                this.ddlTurno.DataBind();
                this.ddlTurno.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar turnos. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                DataView dvFuncionario = CentidadMetodos.EntidadGet("nFuncionario", "ppa").Tables[0].DefaultView;
                dvFuncionario.RowFilter = "empresa=" + this.Session["empresa"];
                dvFuncionario.Sort = "descripcion";
                this.selFuncionarios.DataSource = dvFuncionario;
                this.selFuncionarios.DataValueField = "codigo";
                this.selFuncionarios.DataTextField = "descripcion";
                this.selFuncionarios.DataBind();

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar funcionarios. Correspondiente a: " + ex.Message, "C");
            }
        }

        private void Guardar()
        {
            string operacion = "inserta";
            bool verificacion = true;
            try
            {

                using (TransactionScope ts = new TransactionScope())
                {
                    string cuadrilla = null;
                    if (rblTipo.SelectedValue == "C")
                        cuadrilla = ddlCuadrilla.SelectedValue;

                    if (this.rblOpcion.SelectedValue == "IN")
                        txtFechaSalida.Text = txtFechaEntrada.Text;

                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                    {
                        switch (programacion.GuardaRegistroManual(cuadrilla,
                                    Convert.ToDateTime(this.txtFecha.Text), Convert.ToDateTime(this.txtFechaEntrada.Text),
                                    Convert.ToDateTime(this.txtFechaSalida.Text), lblCedula.Text,
                                    this.rblOpcion.SelectedValue.ToString(), this.ddlTurno.SelectedValue.ToString(),
                                    this.Session["usuario"].ToString(), Convert.ToInt16(Session["empresa"])))
                        {
                            case 1:
                                verificacion = false;
                                break;
                        }
                    }
                    else
                    {
                        for (int x = 0; x < this.selFuncionarios.Items.Count; x++)
                        {
                            if (this.selFuncionarios.Items[x].Selected)
                                verificacion = true;
                        }

                        if (verificacion == false)
                        {
                            this.nilblInformacion.Text = "Debe seleccionar al menos un funcionario para realizar registro";
                            return;
                        }

                        for (int x = 0; x < this.selFuncionarios.Items.Count; x++)
                        {
                            if (this.selFuncionarios.Items[x].Selected == true)
                            {
                                switch (programacion.GuardaRegistroManual(cuadrilla,
                                   Convert.ToDateTime(this.txtFecha.Text), Convert.ToDateTime(this.txtFechaEntrada.Text),
                                   Convert.ToDateTime(this.txtFechaSalida.Text), this.selFuncionarios.Items[x].Value,
                                   this.rblOpcion.SelectedValue.ToString(), this.ddlTurno.SelectedValue.ToString(),
                                   this.Session["usuario"].ToString(), Convert.ToInt16(Session["empresa"])))
                                {
                                    case 1:
                                        verificacion = false;
                                        break;
                                }
                            }
                        }
                    }

                    if (verificacion == false)
                        this.nilblInformacion.Text = "Error al insertar el registro. operación no realizada";
                    else
                    {
                        //nitxtFI.Text = this.txtFecha.Text;
                        //nitxtFF.Text = this.txtFecha.Text;
                        ts.Complete();
                        ManejoExito("Asignación registrada correctamente", "I");
                    }
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar los datos correspondiente a: " + ex.Message, operacion.Substring(0, 1).ToUpper());
            }
        }

        private void ValidaRegistro()
        {
            //for (int x = 0; x < this.selFuncionarios.Items.Count; x++)
            //{
            //    if (cuadrillas.VerificaFuncionarioCuadrilla(txtCodigo.Text, Convert.ToInt32(selFuncionarios.Items[x].Value), Convert.ToInt16(Session["empresa"])) == 0)
            //        selFuncionarios.Items[x].Selected = true;
            //    else
            //        selFuncionarios.Items[x].Selected = false;
            //}
        }

        #endregion Metodos

        #region Eventos

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

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "A", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "A");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            this.selFuncionarios.Visible = false;
            CargarCombos();
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            this.nilblMensaje.Text = "";
            this.ddlCuadrilla.Enabled = false;
            this.ddlTurno.Enabled = false;
            this.txtFechaEntrada.Focus();
            rblTipo.Enabled = false;

            try
            {

                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.txtFecha.Text = this.gvLista.SelectedRow.Cells[2].Text;
                else
                    this.txtFecha.Text = "";

                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    this.ddlTurno.SelectedValue = this.gvLista.SelectedRow.Cells[3].Text;
                else
                    this.ddlTurno.SelectedValue = "";

                if (this.gvLista.SelectedRow.Cells[5].Text != "&nbsp;")
                {
                    this.lblCedula.Text = this.gvLista.SelectedRow.Cells[5].Text;

                }

                if (this.gvLista.SelectedRow.Cells[6].Text != "&nbsp;")
                    this.lblCodigo.Text = this.gvLista.SelectedRow.Cells[6].Text;

                if (this.gvLista.SelectedRow.Cells[7].Text != "&nbsp;")
                    this.lblNombre.Text = this.gvLista.SelectedRow.Cells[7].Text;

                if (this.gvLista.SelectedRow.Cells[8].Text != "&nbsp;")
                    this.ddlCuadrilla.SelectedValue = this.gvLista.SelectedRow.Cells[8].Text;
                else
                    this.ddlCuadrilla.SelectedValue = "";

                if (this.gvLista.SelectedRow.Cells[11].Text != "&nbsp;")
                    this.txtFechaEntrada.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[11].Text);
                else
                    this.txtFechaEntrada.Text = "";

                if (this.gvLista.SelectedRow.Cells[12].Text != "&nbsp;")
                    this.txtFechaSalida.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[12].Text.Trim());
                else
                    this.txtFechaSalida.Text = "";
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string operacion = "elimina";

            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                object[] objValores = new object[] { Convert.ToInt16(Session["empresa"]), Convert.ToDateTime(this.gvLista.Rows[e.RowIndex].Cells[2].Text) ,
             Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[6].Text), Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[3].Text)};

                if (CentidadMetodos.EntidadInsertUpdateDelete("nProgramacion", operacion, "ppa", objValores) == 0)
                    ManejoExito("Registro eliminado satisfactoriamente", "E");
                else
                {
                    ManejoError("Errores al eliminar el registro. Operación no realizada", "E");
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

        protected void txtFecha_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFecha.Text);
            }
            catch (Exception ex)
            {
                nilblInformacion.Text = "Formato de fecha no valido";
                txtFecha.Focus();
                txtFecha.Text = "";
                return;
            }
        }

        #endregion Eventos

        #region MetodosFuncionario




        #endregion MetodosFuncionario

        #region EventosFuncionario

        protected void gvAsignacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            CargarCombos();
            gvLista.DataBind();
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }

        protected void ddlCuadrilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.ddlCuadrilla.SelectedValue != "")
                {
                    selFuncionarios.Visible = true;
                    try
                    {
                        this.selFuncionarios.DataSource = cuadrillas.GetFuncionariosCuadrilla(this.ddlCuadrilla.SelectedValue.ToString(), Convert.ToInt16(Session["empresa"]));
                        this.selFuncionarios.DataValueField = "codigo";
                        this.selFuncionarios.DataTextField = "descripcion";
                        this.selFuncionarios.DataBind();

                    }
                    catch (Exception ex)
                    {
                        ManejoError("Error al cargar funcionarios. Correspondiente a: " + ex.Message, "C");
                    }
                }
                else
                    this.nilblInformacion.Text = "Seleccione una cuadrilla";
            }
            catch (Exception ex)
            {
                this.nilblInformacion.Text = "Error al cargar funcionarios debido a:" + ex;
            }
        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblTipo.SelectedValue == "C")
            {
                try
                {
                    this.ddlCuadrilla.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nCuadrilla", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"]));
                    this.ddlCuadrilla.DataValueField = "codigo";
                    this.ddlCuadrilla.DataTextField = "descripcion";
                    this.ddlCuadrilla.DataBind();
                    this.ddlCuadrilla.Items.Insert(0, new ListItem("Seleccione una opción", ""));
                }
                catch (Exception ex)
                {
                    ManejoError("Error al cargar cuadrillas. Correspondiente a: " + ex.Message, "C");
                }

                selFuncionarios.Visible = false;
                ddlCuadrilla.Enabled = true;
            }
            else
            {
                selFuncionarios.Visible = true;
                GetFuncionariosIndividual();
                ddlCuadrilla.Enabled = false;
            }
        }

        private void GetFuncionariosIndividual()
        {
            try
            {

                this.selFuncionarios.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nFuncionario", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.selFuncionarios.DataValueField = "codigo";
                this.selFuncionarios.DataTextField = "descripcion";
                this.selFuncionarios.DataBind();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar funcionarios sin programación. Correspondiente a: " + ex.Message, "C");
            }
        }


        protected void rblEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rblOpcion.SelectedValue == "IN")
            {
                this.txtFechaSalida.Enabled = false;
                txtFechaSalida.Text = txtFechaEntrada.Text;
            }
            else
                this.txtFechaSalida.Enabled = true;
        }

        #endregion EventosFuncionario


        protected void nibtnBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;

            if (nitxtFI.Text.Length == 0 || nitxtFF.Text.Length == 0)
            {
                nilblInformacion.Text = "Debe seleccionar fecha inicial y fecha final para la busqueda";
                return;
            }

            GetEntidad();
        }

        protected void nibtnNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);
            CargarCombos();
            lblCedula.Text = "";
            lblNombre.Text = "";
            lblCodigo.Text = "";
            rblOpcion.ClearSelection();
            rblTipo.ClearSelection();

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            this.nilblInformacion.Text = "";
            this.selFuncionarios.Visible = false;
        }

        protected void nibtnGuardar_Click(object sender, EventArgs e)
        {
            if (ddlTurno.SelectedValue.Length == 0)
            {
                nilblInformacion.Text = "Campos vacios por favor corrija";
                return;
            }
            Guardar();
        }

        protected void nibtnCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();


            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
            this.selFuncionarios.Visible = false;
        }

        protected void gvLista_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }


                deshacerIngresoAPlanta(Convert.ToDateTime(this.gvLista.Rows[e.RowIndex].Cells[2].Text), Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[5].Text),
                Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[3].Text));


            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }

        }

        private void deshacerIngresoAPlanta(DateTime fecha, string funcionario, string turno)
        {
            try
            {
                if (programacion.ReniciaProgramacion(fecha, funcionario, turno, Session["usuario"].ToString(), Convert.ToInt32(Session["empresa"])) == 0)
                    ManejoExito("Registro reiniciado satisfactoriamente", "E");
                else
                    ManejoError("Error al reiniciar la programación. Operación no realizada", "E");

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }

        }


    }
}