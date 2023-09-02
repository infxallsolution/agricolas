using Nomina.WebForms.App_Code.General;
using Nomina.WebForms.App_Code.Programacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Pprogramacion
{
    public partial class AprobarHoras : BasePage
    {
        #region Instancias


        Cturnos turnos = new Cturnos();
        Ccuadrillas cuadrillas = new Ccuadrillas();
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
        //private void GuardarExtras()
        //{
        //    bool verifica = false;

        //    this.nilblInformacion.Text = "";

        //    try
        //    {
        //        //if (Convert.ToString("").Length == 0 || Convert.ToString(this.ddlCuadrilla.SelectedValue).Length == 0 || this.txtFecha.Text.Length == 0 || this.gvExtras.Rows.Count == 0)
        //        //{
        //        //    this.nilblInformacion.Text = "Campos vacios. Por favor corrija";
        //        //    return;
        //        //}

        //        foreach (GridViewRow registro in this.gvExtras.Rows)
        //        {
        //            if (((TextBox)registro.Cells[3].FindControl("txtExtrasLun")).Text.Length > 0)
        //                verifica = true;

        //            if (((TextBox)registro.Cells[4].FindControl("txtExtrasMar")).Text.Length > 0)
        //                verifica = true;

        //            if (((TextBox)registro.Cells[5].FindControl("txtExtrasMie")).Text.Length > 0)
        //                verifica = true;

        //            if (((TextBox)registro.Cells[6].FindControl("txtExtrasJue")).Text.Length > 0)
        //                verifica = true;

        //            if (((TextBox)registro.Cells[7].FindControl("txtExtrasVie")).Text.Length > 0)
        //                verifica = true;

        //            if (((TextBox)registro.Cells[8].FindControl("txtExtrasSab")).Text.Length > 0)
        //                verifica = true;

        //            if (((TextBox)registro.Cells[9].FindControl("txtExtrasDom")).Text.Length > 0)
        //                verifica = true;
        //        }

        //        //if (verifica == false)
        //        //    this.nilblInformacion.Text = "Debe asignar horas extras a almenos un funcionario en un día de la semana";
        //        //else
        //        //{
        //        //    using (TransactionScope ts = new TransactionScope())
        //        //    {
        //        //        foreach (GridViewRow registro in this.gvExtras.Rows)
        //        //        {
        //        //            if (verifica == true)
        //        //            {
        //        //                if (((TextBox)registro.Cells[3].FindControl("txtExtrasLun")).Text.Length > 0)
        //        //                {
        //        //                    switch (programacion.AutorizaHorasExtras(Convert.ToDateTime(txtFecha.Text), Convert.ToString(""),
        //        //                        registro.Cells[0].Text, Convert.ToDecimal(((TextBox)registro.Cells[3].FindControl("txtExtrasLun")).Text),
        //        //                        "lun", this.Session["usuario"].ToString(), Convert.ToInt16(Session["empresa"])))
        //        //                    {
        //        //                        case 1:

        //        //                            verifica = false;
        //        //                            break;
        //        //                    }
        //        //                }

        //        //                if (((TextBox)registro.Cells[4].FindControl("txtExtrasMar")).Text.Length > 0)
        //        //                {
        //        //                    switch (programacion.AutorizaHorasExtras(
        //        //                        Convert.ToDateTime(txtFecha.Text),
        //        //                        Convert.ToString(""),
        //        //                        registro.Cells[0].Text,
        //        //                        Convert.ToDecimal(((TextBox)registro.Cells[4].FindControl("txtExtrasMar")).Text),
        //        //                        "mar", this.Session["usuario"].ToString(), Convert.ToInt16(Session["empresa"])))
        //        //                    {
        //        //                        case 1:

        //        //                            verifica = false;
        //        //                            break;
        //        //                    }
        //        //                }

        //        //                if (((TextBox)registro.Cells[5].FindControl("txtExtrasMie")).Text.Length > 0)
        //        //                {
        //        //                    switch (programacion.AutorizaHorasExtras(
        //        //                        Convert.ToDateTime(txtFecha.Text),
        //        //                        Convert.ToString(""),
        //        //                        registro.Cells[0].Text,
        //        //                        Convert.ToDecimal(((TextBox)registro.Cells[5].FindControl("txtExtrasMie")).Text),
        //        //                        "mie", this.Session["usuario"].ToString(), Convert.ToInt16(Session["empresa"])))
        //        //                    {
        //        //                        case 1:

        //        //                            verifica = false;
        //        //                            break;
        //        //                    }
        //        //                }

        //        //                if (((TextBox)registro.Cells[6].FindControl("txtExtrasJue")).Text.Length > 0)
        //        //                {
        //        //                    switch (programacion.AutorizaHorasExtras(
        //        //                        Convert.ToDateTime(txtFecha.Text),
        //        //                        Convert.ToString(""),
        //        //                        registro.Cells[0].Text,
        //        //                        Convert.ToDecimal(((TextBox)registro.Cells[6].FindControl("txtExtrasJue")).Text),
        //        //                        "jue", this.Session["usuario"].ToString(), Convert.ToInt16(Session["empresa"])))
        //        //                    {
        //        //                        case 1:

        //        //                            verifica = false;
        //        //                            break;
        //        //                    }
        //        //                }

        //        //                if (((TextBox)registro.Cells[7].FindControl("txtExtrasVie")).Text.Length > 0)
        //        //                {
        //        //                    switch (programacion.AutorizaHorasExtras(
        //        //                        Convert.ToDateTime(txtFecha.Text),
        //        //                        Convert.ToString(""),
        //        //                        registro.Cells[0].Text,
        //        //                        Convert.ToDecimal(((TextBox)registro.Cells[7].FindControl("txtExtrasVie")).Text),
        //        //                        "vie", this.Session["usuario"].ToString(), Convert.ToInt16(Session["empresa"])))
        //        //                    {
        //        //                        case 1:

        //        //                            verifica = false;
        //        //                            break;
        //        //                    }
        //        //                }

        //        //                if (((TextBox)registro.Cells[8].FindControl("txtExtrasSab")).Text.Length > 0)
        //        //                {
        //        //                    switch (programacion.AutorizaHorasExtras(
        //        //                        Convert.ToDateTime(txtFecha.Text),
        //        //                        Convert.ToString(""),
        //        //                        registro.Cells[0].Text,
        //        //                        Convert.ToDecimal(((TextBox)registro.Cells[8].FindControl("txtExtrasSab")).Text),
        //        //                        "sab", this.Session["usuario"].ToString(), Convert.ToInt16(Session["empresa"])))
        //        //                    {
        //        //                        case 1:

        //        //                            verifica = false;
        //        //                            break;
        //        //                    }
        //        //                }

        //        //                if (((TextBox)registro.Cells[9].FindControl("txtExtrasDom")).Text.Length > 0)
        //        //                {
        //        //                    switch (programacion.AutorizaHorasExtras(
        //        //                        Convert.ToDateTime(txtFecha.Text),
        //        //                        Convert.ToString(""),
        //        //                        registro.Cells[0].Text,
        //        //                        Convert.ToDecimal(((TextBox)registro.Cells[9].FindControl("txtExtrasDom")).Text),
        //        //                        "dom", this.Session["usuario"].ToString(), Convert.ToInt16(Session["empresa"])))
        //        //                    {
        //        //                        case 1:

        //        //                            verifica = false;
        //        //                            break;
        //        //                    }
        //        //                }
        //        //            }
        //        //        }

        //        //        if (verifica == true)
        //        //        {
        //        //            this.nilblInformacion.ForeColor = Color.Green;
        //        //            this.nilblInformacion.Text = "Autorización de horas extras registrada satisfactoriamente";
        //        //            this.gvExtras.Visible = false;
        //        //            this.lbRegistrarExtras.Visible = false;
        //        //            ts.Complete();

        //        //        }
        //        //        else
        //        //            this.nilblInformacion.Text = "Error al registrar autorización de horas extras. Operación no realizada";
        //        //    }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ManejoError("Error al autorizar horas extras correspondiente a: " + ex.Message, "C");
        //    }
        //}


        private void GetEntidadExtras()
        {
            int i = 0;
            bool sw = false;
            this.nilblInformacion.Text = "";
            try
            {
                gvExtras.Visible = true;


                this.gvExtras.DataSource = programacion.GetProgramacionFuncionariosAprobar(Convert.ToDateTime(txtFecha.Text), ddlCuadrilla.SelectedValue, Convert.ToInt16(Session["empresa"]));
                this.gvExtras.DataBind();

                foreach (DataRowView registro in programacion.GetProgramacionFuncionariosAprobar(Convert.ToDateTime(txtFecha.Text), ddlCuadrilla.SelectedValue, Convert.ToInt16(Session["empresa"])))
                {
                    if (Convert.ToInt16(registro.Row.ItemArray.GetValue(3)) == 1)
                    {
                        sw = true;
                        foreach (DataRowView dia in programacion.GetProgramacionFuncionariosDiasAprobar(Convert.ToDateTime(txtFecha.Text),
                             Convert.ToString(registro.Row.ItemArray.GetValue(0)), ddlCuadrilla.SelectedValue, Convert.ToInt16(Session["empresa"])))
                        {

                            if (Convert.ToInt16(dia.Row.ItemArray.GetValue(1)) > 0)
                            {
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasLun")).Visible = true;
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasLun")).Text = Convert.ToString(dia.Row.ItemArray.GetValue(1));
                                if (Convert.ToInt16(dia.Row.ItemArray.GetValue(8)) == 1)
                                {
                                    ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasLun")).Enabled = false;
                                    ((CheckBox)this.gvExtras.Rows[i].FindControl("chkSab")).Checked = true;
                                }
                                else
                                    ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasLun")).Enabled = true;
                            }
                            else
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasLun")).Visible = false;

                            if (Convert.ToInt16(dia.Row.ItemArray.GetValue(2)) > 0)
                            {
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasMar")).Visible = true;
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasMar")).Text = Convert.ToString(dia.Row.ItemArray.GetValue(2));
                                if (Convert.ToInt16(dia.Row.ItemArray.GetValue(9)) == 1)
                                {
                                    ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasMar")).Enabled = false;
                                    ((CheckBox)this.gvExtras.Rows[i].FindControl("chkSab")).Checked = true;
                                }
                                else
                                    ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasMar")).Enabled = true;
                            }
                            else
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasMar")).Visible = false;

                            if (Convert.ToInt16(dia.Row.ItemArray.GetValue(3)) > 0)
                            {
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasMie")).Visible = true;
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasMie")).Text = Convert.ToString(dia.Row.ItemArray.GetValue(3));
                                if (Convert.ToInt16(dia.Row.ItemArray.GetValue(10)) == 1)
                                {
                                    ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasMie")).Enabled = false;
                                    ((CheckBox)this.gvExtras.Rows[i].FindControl("chkSab")).Checked = true;
                                }
                                else
                                    ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasMie")).Enabled = true;
                            }
                            else
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasMie")).Visible = false;

                            if (Convert.ToInt16(dia.Row.ItemArray.GetValue(4)) > 0)
                            {
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasJue")).Visible = true;
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasJue")).Text = Convert.ToString(dia.Row.ItemArray.GetValue(4));
                                if (Convert.ToInt16(dia.Row.ItemArray.GetValue(11)) == 1)
                                {
                                    ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasJue")).Enabled = false;
                                    ((CheckBox)this.gvExtras.Rows[i].FindControl("chkSab")).Checked = true;
                                }
                                else
                                    ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasJue")).Enabled = true;
                            }
                            else
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasJue")).Visible = false;

                            if (Convert.ToInt16(dia.Row.ItemArray.GetValue(5)) > 0)
                            {
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasVie")).Visible = true;
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasVie")).Text = Convert.ToString(dia.Row.ItemArray.GetValue(5));
                                if (Convert.ToInt16(dia.Row.ItemArray.GetValue(12)) == 1)
                                {
                                    ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasVie")).Enabled = false;
                                    ((CheckBox)this.gvExtras.Rows[i].FindControl("chkSab")).Checked = true;
                                }
                                else
                                    ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasVie")).Enabled = true;
                            }
                            else
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasVie")).Visible = false;

                            if (Convert.ToInt16(dia.Row.ItemArray.GetValue(6)) > 0)
                            {
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasSab")).Visible = true;
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasSab")).Text = Convert.ToString(dia.Row.ItemArray.GetValue(6));
                                if (Convert.ToInt16(dia.Row.ItemArray.GetValue(13)) == 1)
                                {
                                    ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasSab")).Enabled = false;
                                    ((CheckBox)this.gvExtras.Rows[i].FindControl("chkSab")).Checked = true;
                                }
                                else
                                    ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasSab")).Enabled = true;
                            }
                            else
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasSab")).Visible = false;

                            if (Convert.ToInt16(dia.Row.ItemArray.GetValue(7)) > 0)
                            {
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasDom")).Visible = true;
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasDom")).Text = Convert.ToString(dia.Row.ItemArray.GetValue(7));
                                if (Convert.ToInt16(dia.Row.ItemArray.GetValue(14)) == 1)
                                {
                                    ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasDom")).Enabled = false;
                                    ((CheckBox)this.gvExtras.Rows[i].FindControl("chkSab")).Checked = true;
                                }
                                else
                                    ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasDom")).Enabled = true;
                            }
                            else
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasDom")).Visible = false;
                        }


                    }


                    i++;
                }
                if (sw == false)
                {
                    this.nilblInformacion.Visible = false;
                    this.nilblInformacion.ForeColor = Color.Red;
                    this.nilblInformacion.Text = "Las programaciones no se encuentran ejecutadas para autorizar horas extras";

                }
                else
                {
                    this.nilblInformacion.Visible = true;
                    lbRegistrar.Visible = true;
                }

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la programación para autorización de horas extras. Correspondiente a: " + ex.Message, "C");
            }
        }

        private void ManejarControles()
        {
            this.lbRegistrar.Visible = false;
            txtFecha.Enabled = true;
            CargaCombos();
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
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                 "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
        }

        private void CargaCombos()
        {

            try
            {
                var vcuadrilla = CentidadMetodos.EntidadGet("nCuadrilla", "ppa").Tables[0].DefaultView;
                vcuadrilla.RowFilter = vcuadrilla.RowFilter = "activo=1 and empresa=" + this.Session["empresa"].ToString();
                this.ddlCuadrilla.DataSource = vcuadrilla;
                this.ddlCuadrilla.DataValueField = "codigo";
                this.ddlCuadrilla.DataTextField = "descripcion";
                this.ddlCuadrilla.DataBind();
                this.ddlCuadrilla.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar cuadrillas. Correspondiente a: " + ex.Message, "C");
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
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) != 0)
                {
                    if (!IsPostBack)
                        ManejarControles();
                }
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");


            }
        }

        protected void chkSabT_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow registro in this.gvExtras.Rows)
            {
                ((CheckBox)registro.Cells[9].FindControl("chkSab")).Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void txtFecha_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFecha.Text);
            }
            catch
            {
                CcontrolesUsuario.MensajeError("Formato de fecha no valido", nilblInformacion);
                txtFecha.Text = "";
                txtFecha.Focus();
                return;
            }
        }

        private void Guardar()
        {
            bool verificacion = false, verifica = true, vencimiento = true;
            try
            {

                foreach (GridViewRow registro in this.gvExtras.Rows)
                {
                    if (((CheckBox)registro.Cells[3].FindControl("chkSab")).Checked)
                        verificacion = true;
                }

                if (verificacion == false)
                {
                    this.nilblInformacion.ForeColor = Color.Red;
                    this.nilblInformacion.Text = "Debe seleccionar por lo menos un funcionario para aprobar";
                }
                else
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        foreach (GridViewRow registro in this.gvExtras.Rows)
                        {
                            if (verifica == true)
                            {
                                if (((TextBox)registro.Cells[4].FindControl("txtExtrasLun")).Text.Length > 0 && ((TextBox)registro.Cells[4].FindControl("txtExtrasLun")).Enabled == true)
                                {
                                    switch (programacion.GuardaAprobacionAdicionExtras(Convert.ToDateTime(txtFecha.Text), registro.Cells[0].Text,
                                        this.Session["usuario"].ToString(), "lun",
                                        Convert.ToDecimal(((TextBox)registro.Cells[4].FindControl("txtExtrasLun")).Text),
                                        ((CheckBox)registro.Cells[3].FindControl("chkSab")).Checked, Convert.ToInt16(Session["empresa"])))
                                    {
                                        case 1:

                                            verifica = false;
                                            break;
                                        case 2:
                                            this.nilblInformacion.Text = "El tiempo de aprobación del funcionario " + registro.Cells[1].Text + " se vencio, solo son (4) dias ";
                                            return;
                                        case 7:
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede aprobar horas extras, porque supera las horas extras autorizadas del mes.", "w");
                                            verifica = false;
                                            return;
                                        case 8:
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede aprobar horas extras, porque supera las horas extras autorizadas de la semana.", "w");
                                            verifica = false;
                                            return;
                                    }
                                }

                                if (((TextBox)registro.Cells[5].FindControl("txtExtrasMar")).Text.Length > 0 && ((TextBox)registro.Cells[5].FindControl("txtExtrasMar")).Enabled == true)
                                {
                                    switch (programacion.GuardaAprobacionAdicionExtras(Convert.ToDateTime(txtFecha.Text), registro.Cells[0].Text,
                                         this.Session["usuario"].ToString(), "mar",
                                         Convert.ToDecimal(((TextBox)registro.Cells[5].FindControl("txtExtrasMar")).Text),
                                         ((CheckBox)registro.Cells[3].FindControl("chkSab")).Checked, Convert.ToInt16(Session["empresa"])))
                                    {
                                        case 1:

                                            verifica = false;
                                            break;
                                        case 2:
                                            this.nilblInformacion.Text = "El tiempo de aprobación del funcionario " + registro.Cells[1].Text + " se vencio, solo son (4) dias ";
                                            return;
                                        case 7:
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede aprobar horas extras, porque supera las horas extras autorizadas del mes.", "w");
                                            verifica = false;
                                            return;
                                        case 8:
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede aprobar horas extras, porque supera las horas extras autorizadas de la semana.", "w");
                                            verifica = false;
                                            return;
                                    }
                                }

                                if (((TextBox)registro.Cells[6].FindControl("txtExtrasMie")).Text.Length > 0 && ((TextBox)registro.Cells[6].FindControl("txtExtrasMie")).Enabled == true)
                                {
                                    switch (programacion.GuardaAprobacionAdicionExtras(Convert.ToDateTime(txtFecha.Text), registro.Cells[0].Text,
                                        this.Session["usuario"].ToString(), "mie",
                                        Convert.ToDecimal(((TextBox)registro.Cells[6].FindControl("txtExtrasMie")).Text),
                                        ((CheckBox)registro.Cells[3].FindControl("chkSab")).Checked, Convert.ToInt16(Session["empresa"])))
                                    {
                                        case 1:

                                            verifica = false;
                                            break;
                                        case 2:
                                            this.nilblInformacion.Text = "El tiempo de aprobación del funcionario " + registro.Cells[1].Text + " se vencio, solo son (4) dias ";
                                            return;
                                        case 7:
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede aprobar horas extras, porque supera las horas extras autorizadas del mes.", "w");
                                            verifica = false;
                                            return;
                                        case 8:
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede aprobar horas extras, porque supera las horas extras autorizadas de la semana.", "w");
                                            verifica = false;
                                            return;
                                    }
                                }

                                if (((TextBox)registro.Cells[7].FindControl("txtExtrasJue")).Text.Length > 0 && ((TextBox)registro.Cells[7].FindControl("txtExtrasJue")).Enabled == true)
                                {
                                    switch (programacion.GuardaAprobacionAdicionExtras(Convert.ToDateTime(txtFecha.Text), registro.Cells[0].Text,
                                         this.Session["usuario"].ToString(), "jue", Convert.ToDecimal(((TextBox)registro.Cells[7].FindControl("txtExtrasJue")).Text),
                                         ((CheckBox)registro.Cells[3].FindControl("chkSab")).Checked, Convert.ToInt16(Session["empresa"])))
                                    {
                                        case 1:

                                            verifica = false;
                                            break;
                                        case 2:
                                            this.nilblInformacion.Text = "El tiempo de aprobación del funcionario " + registro.Cells[1].Text + " se vencio, solo son (4) dias ";
                                            return;
                                        case 7:
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede aprobar horas extras, porque supera las horas extras autorizadas del mes.", "w");
                                            verifica = false;
                                            return;
                                        case 8:
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede aprobar horas extras, porque supera las horas extras autorizadas de la semana.", "w");
                                            verifica = false;
                                            return;
                                    }
                                }

                                if (((TextBox)registro.Cells[8].FindControl("txtExtrasVie")).Text.Length > 0 && ((TextBox)registro.Cells[8].FindControl("txtExtrasVie")).Enabled == true)
                                {
                                    switch (programacion.GuardaAprobacionAdicionExtras(Convert.ToDateTime(txtFecha.Text), registro.Cells[0].Text,
                                        this.Session["usuario"].ToString(), "vie",
                                        Convert.ToDecimal(((TextBox)registro.Cells[8].FindControl("txtExtrasVie")).Text),
                                        ((CheckBox)registro.Cells[3].FindControl("chkSab")).Checked, Convert.ToInt16(Session["empresa"])))
                                    {
                                        case 1:

                                            verifica = false;
                                            break;
                                        case 2:
                                            this.nilblInformacion.Text = "El tiempo de aprobación del funcionario " + registro.Cells[1].Text + " se vencio, solo son (4) dias ";
                                            return;
                                        case 7:
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede aprobar horas extras, porque supera las horas extras autorizadas del mes.", "w");
                                            verifica = false;
                                            return;
                                        case 8:
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede aprobar horas extras, porque supera las horas extras autorizadas de la semana.", "w");
                                            verifica = false;
                                            return;
                                    }
                                }

                                if (((TextBox)registro.Cells[9].FindControl("txtExtrasSab")).Text.Length > 0 && ((TextBox)registro.Cells[9].FindControl("txtExtrasSab")).Enabled == true)
                                {
                                    switch (programacion.GuardaAprobacionAdicionExtras(Convert.ToDateTime(txtFecha.Text), registro.Cells[0].Text,
                                         this.Session["usuario"].ToString(), "sab",
                                         Convert.ToDecimal(((TextBox)registro.Cells[9].FindControl("txtExtrasSab")).Text),
                                         ((CheckBox)registro.Cells[3].FindControl("chkSab")).Checked, Convert.ToInt16(Session["empresa"])))
                                    {
                                        case 1:

                                            verifica = false;
                                            break;
                                        case 2:
                                            this.nilblInformacion.Text = "El tiempo de aprobación del funcionario " + registro.Cells[1].Text + " se vencio, solo son (4) dias ";
                                            return;
                                        case 7:
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede aprobar horas extras, porque supera las horas extras autorizadas del mes.", "w");
                                            verifica = false;
                                            return;
                                        case 8:
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede aprobar horas extras, porque supera las horas extras autorizadas de la semana.", "w");
                                            verifica = false;
                                            return;
                                    }
                                }

                                if (((TextBox)registro.Cells[10].FindControl("txtExtrasDom")).Text.Length > 0 && ((TextBox)registro.Cells[10].FindControl("txtExtrasDom")).Enabled == true)
                                {
                                    switch (programacion.GuardaAprobacionAdicionExtras(Convert.ToDateTime(txtFecha.Text), registro.Cells[0].Text,
                                        this.Session["usuario"].ToString(), "dom",
                                        Convert.ToDecimal(((TextBox)registro.Cells[10].FindControl("txtExtrasDom")).Text),
                                        ((CheckBox)registro.Cells[3].FindControl("chkSab")).Checked, Convert.ToInt16(Session["empresa"])))
                                    {
                                        case 1:

                                            verifica = false;
                                            break;
                                        case 2:
                                            this.nilblInformacion.Text = "El tiempo de aprobación del funcionario " + registro.Cells[1].Text + " se vencio, solo son (4) dias ";
                                            return;
                                        case 7:
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede aprobar horas extras, porque supera las horas extras autorizadas del mes.", "w");
                                            verifica = false;
                                            return;
                                        case 8:
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede aprobar horas extras, porque supera las horas extras autorizadas de la semana.", "w");
                                            verifica = false;
                                            return;
                                    }
                                }
                            }
                        }

                        if (verifica == true)
                        {
                            this.nilblInformacion.ForeColor = Color.Green;
                            this.nilblInformacion.Text = "Autorización de horas extras registrada satisfactoriamente";
                            this.gvExtras.Visible = false;
                            this.nilblInformacion.Visible = false;
                            ts.Complete();

                        }
                        else
                            this.nilblInformacion.Text = "Error al registrar autorización de horas extras. Operación no realizada";
                    }
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al autorizar horas extras correspondiente a: " + ex.Message, "C");
            }
        }

        #endregion Eventos

        protected void lbRegistrar_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected void ddlCuadrilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.nilblInformacion.Text = "";
            this.txtFecha.Visible = true;
            this.gvExtras.Visible = false;
            this.gvExtras.DataSource = null;
            this.gvExtras.DataBind();
            GetEntidadExtras();
        }

        protected void lbRegistrar_Click1(object sender, EventArgs e)
        {
            Guardar();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.nilblInformacion.Text = "";
            this.txtFecha.Visible = true;
            this.gvExtras.Visible = false;
            this.gvExtras.DataSource = null;
            this.gvExtras.DataBind();

            CargaCombos();
        }

        protected void lbRefresca_Click(object sender, EventArgs e)
        {
            this.nilblInformacion.Text = "";
            this.txtFecha.Visible = true;
            this.gvExtras.Visible = false;
            this.gvExtras.DataSource = null;
            this.gvExtras.DataBind();
            GetEntidadExtras();
        }
    }
}