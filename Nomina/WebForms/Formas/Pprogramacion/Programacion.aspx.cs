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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Pprogramacion
{
    public partial class Programacion : BasePage
    {
        #region Instancias



        Cturnos turnos = new Cturnos();
        Ccuadrillas cuadrillas = new Ccuadrillas();
        Cprogramacion programacion = new Cprogramacion();

        #endregion Instancias

        #region Metodos


        private void GetFuncionariosIndividual()
        {
            try
            {
                this.gvLista.DataSource = programacion.GetFuncionariosSinProgramacionCuadrilla(Convert.ToDateTime(txtFecha.Text), Convert.ToString(this.ddlCuadrilla.SelectedValue), Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private void GuardarExtras()
        {
            bool verifica = false;
            this.nilblInformacion.Text = "";

            try
            {
                if (Convert.ToString(this.niddlTurno.SelectedValue).Length == 0 || Convert.ToString(this.ddlCuadrilla.SelectedValue).Length == 0 || this.txtFecha.Text.Length == 0 || this.gvExtras.Rows.Count == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Campos vacios. Por favor corrija", "w");
                    return;
                }

                foreach (GridViewRow registro in this.gvExtras.Rows)
                {
                    if (((TextBox)registro.Cells[3].FindControl("txtExtrasLun")).Text.Length > 0)
                        verifica = true;

                    if (((TextBox)registro.Cells[4].FindControl("txtExtrasMar")).Text.Length > 0)
                        verifica = true;

                    if (((TextBox)registro.Cells[5].FindControl("txtExtrasMie")).Text.Length > 0)
                        verifica = true;

                    if (((TextBox)registro.Cells[6].FindControl("txtExtrasJue")).Text.Length > 0)
                        verifica = true;

                    if (((TextBox)registro.Cells[7].FindControl("txtExtrasVie")).Text.Length > 0)
                        verifica = true;

                    if (((TextBox)registro.Cells[8].FindControl("txtExtrasSab")).Text.Length > 0)
                        verifica = true;

                    if (((TextBox)registro.Cells[9].FindControl("txtExtrasDom")).Text.Length > 0)
                        verifica = true;
                }

                if (verifica == false)
                    CerroresGeneral.ManejoError(this, GetType(), "Debe asignar horas extras a almenos un funcionario en un día de la semana", "w");
                else
                {
                    using (TransactionScope ts = new TransactionScope())
                    {
                        foreach (GridViewRow registro in this.gvExtras.Rows)
                        {
                            if (verifica == true)
                            {
                                if (((TextBox)registro.Cells[3].FindControl("txtExtrasLun")).Text.Length > 0)
                                {
                                    switch (programacion.AutorizaHorasExtras(
                                        Convert.ToDateTime(txtFecha.Text),
                                        Convert.ToString(this.niddlTurno.SelectedValue),
                                        registro.Cells[0].Text,
                                        Convert.ToDecimal(((TextBox)registro.Cells[3].FindControl("txtExtrasLun")).Text),
                                        "lun", this.Session["usuario"].ToString(), Convert.ToInt16(Session["empresa"]), txtObservacionAdicion.Text))
                                    {
                                        case 1:
                                            verifica = false;
                                            break;
                                        case 7:
                                            this.nilblInformacion.ForeColor = Color.Red;
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede autorizar horas extras, porque supera las horas extras autorizadas del mes.", "w");
                                            verifica = false;
                                            break;
                                        case 8:
                                            this.nilblInformacion.ForeColor = Color.Red;
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede autorizar horas extras, porque supera las horas extras autorizadas de la semana.", "w");
                                            verifica = false;
                                            break;
                                    }
                                }
                                if (((TextBox)registro.Cells[4].FindControl("txtExtrasMar")).Text.Length > 0)
                                {
                                    switch (programacion.AutorizaHorasExtras(
                                        Convert.ToDateTime(txtFecha.Text),
                                        Convert.ToString(this.niddlTurno.SelectedValue),
                                        registro.Cells[0].Text,
                                        Convert.ToDecimal(((TextBox)registro.Cells[4].FindControl("txtExtrasMar")).Text),
                                        "mar", this.Session["usuario"].ToString(), Convert.ToInt16(Session["empresa"]), txtObservacionAdicion.Text))
                                    {
                                        case 1:

                                            verifica = false;
                                            break;
                                        case 7:
                                            this.nilblInformacion.ForeColor = Color.Red;
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede autorizar horas extras, porque supera las horas extras autorizadas del mes.", "w");
                                            verifica = false;
                                            break;
                                        case 8:
                                            this.nilblInformacion.ForeColor = Color.Red;
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede autorizar horas extras, porque supera las horas extras autorizadas de la semana.", "w");
                                            verifica = false;
                                            break;
                                    }
                                }
                                if (((TextBox)registro.Cells[5].FindControl("txtExtrasMie")).Text.Length > 0)
                                {
                                    switch (programacion.AutorizaHorasExtras(
                                        Convert.ToDateTime(txtFecha.Text),
                                        Convert.ToString(this.niddlTurno.SelectedValue),
                                        registro.Cells[0].Text,
                                        Convert.ToDecimal(((TextBox)registro.Cells[5].FindControl("txtExtrasMie")).Text),
                                        "mie", this.Session["usuario"].ToString(), Convert.ToInt16(Session["empresa"]), txtObservacionAdicion.Text))
                                    {
                                        case 1:
                                            verifica = false;
                                            break;
                                        case 7:
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede autorizar horas extras, porque supera las horas extras autorizadas del mes.", "w");
                                            verifica = false;
                                            return;
                                        case 8:
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede autorizar horas extras, porque supera las horas extras autorizadas de la semana.", "w");
                                            verifica = false;
                                            return;
                                    }
                                }
                                if (((TextBox)registro.Cells[6].FindControl("txtExtrasJue")).Text.Length > 0)
                                {
                                    switch (programacion.AutorizaHorasExtras(Convert.ToDateTime(txtFecha.Text),
                                        Convert.ToString(this.niddlTurno.SelectedValue), registro.Cells[0].Text, Convert.ToDecimal(((TextBox)registro.Cells[6].FindControl("txtExtrasJue")).Text),
                                        "jue", this.Session["usuario"].ToString(), Convert.ToInt16(Session["empresa"]), txtObservacionAdicion.Text))
                                    {
                                        case 1:
                                            verifica = false;
                                            break;
                                        case 7:
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede autorizar horas extras, porque supera las horas extras autorizadas del mes.", "w");
                                            verifica = false;
                                            return;
                                        case 8:
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede autorizar horas extras, porque supera las horas extras autorizadas de la semana.", "w");
                                            verifica = false;
                                            return;
                                    }
                                }
                                if (((TextBox)registro.Cells[7].FindControl("txtExtrasVie")).Text.Length > 0)
                                {
                                    switch (programacion.AutorizaHorasExtras(
                                        Convert.ToDateTime(txtFecha.Text),
                                        Convert.ToString(this.niddlTurno.SelectedValue),
                                        registro.Cells[0].Text,
                                        Convert.ToDecimal(((TextBox)registro.Cells[7].FindControl("txtExtrasVie")).Text),
                                        "vie", this.Session["usuario"].ToString(), Convert.ToInt16(Session["empresa"]), txtObservacionAdicion.Text))
                                    {
                                        case 1:
                                            verifica = false;
                                            break;
                                        case 7:
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede autorizar horas extras, porque supera las horas extras autorizadas del mes.", "w");
                                            verifica = false;
                                            return;
                                        case 8:
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede autorizar horas extras, porque supera las horas extras autorizadas de la semana.", "w");
                                            verifica = false;
                                            return;
                                    }
                                }

                                if (((TextBox)registro.Cells[8].FindControl("txtExtrasSab")).Text.Length > 0)
                                {
                                    switch (programacion.AutorizaHorasExtras(
                                        Convert.ToDateTime(txtFecha.Text),
                                        Convert.ToString(this.niddlTurno.SelectedValue),
                                        registro.Cells[0].Text,
                                        Convert.ToDecimal(((TextBox)registro.Cells[8].FindControl("txtExtrasSab")).Text),
                                        "sab", this.Session["usuario"].ToString(), Convert.ToInt16(Session["empresa"]), txtObservacionAdicion.Text))
                                    {
                                        case 1:
                                            verifica = false;
                                            break;
                                        case 7:
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede autorizar horas extras, porque supera las horas extras autorizadas del mes.", "w");
                                            verifica = false;
                                            return;
                                        case 8:
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede autorizar horas extras, porque supera las horas extras autorizadas de la semana.", "w");
                                            verifica = false;
                                            return;
                                    }
                                }
                                if (((TextBox)registro.Cells[9].FindControl("txtExtrasDom")).Text.Length > 0)
                                {
                                    switch (programacion.AutorizaHorasExtras(Convert.ToDateTime(txtFecha.Text),
                                        Convert.ToString(this.niddlTurno.SelectedValue), registro.Cells[0].Text,
                                        Convert.ToDecimal(((TextBox)registro.Cells[9].FindControl("txtExtrasDom")).Text),
                                        "dom", this.Session["usuario"].ToString(), Convert.ToInt16(Session["empresa"]), txtObservacionAdicion.Text))
                                    {
                                        case 1:
                                            verifica = false;
                                            break;
                                        case 7:
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede autorizar horas extras, porque supera las horas extras autorizadas del mes.", "w");
                                            verifica = false;
                                            return;
                                        case 8:
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no puede autorizar horas extras, porque supera las horas extras autorizadas de la semana.", "w");
                                            verifica = false;
                                            return;
                                    }
                                }
                            }
                        }

                        if (verifica == true)
                        {
                            this.nilblInformacion.ForeColor = Color.Green;
                            CerroresGeneral.ManejoError(this, GetType(), "Autorización de horas extras registrada satisfactoriamente", "w");
                            this.gvExtras.Visible = false;
                            this.lbRegistrarExtras.Visible = false;
                            ts.Complete();

                        }
                        else
                            CerroresGeneral.ManejoError(this, GetType(), "Error al registrar autorización de horas extras. Operación no realizada", "e");
                    }
                }
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private void GetEntidadExtras()
        {
            int i = 0;
            bool sw = false;
            this.nilblInformacion.Text = "";
            try
            {
                this.gvExtras.DataSource = programacion.GetProgramacionFuncionariosCuadrillaRegistroPorteria(
                    Convert.ToString(this.ddlCuadrilla.SelectedValue), Convert.ToString(this.niddlTurno.SelectedValue),
                    Convert.ToString(this.Session["usuario"]), Convert.ToDateTime(txtFecha.Text), Convert.ToInt16(Session["empresa"]));
                this.gvExtras.DataBind();

                foreach (DataRowView registro in programacion.GetProgramacionFuncionariosCuadrillaRegistroPorteria(
                      Convert.ToString(this.ddlCuadrilla.SelectedValue), Convert.ToString(this.niddlTurno.SelectedValue),
                      Convert.ToString(this.Session["usuario"]), Convert.ToDateTime(txtFecha.Text), Convert.ToInt16(Session["empresa"])))
                {
                    if (Convert.ToInt16(registro.Row.ItemArray.GetValue(3)) == 1)
                    {
                        sw = true;
                        foreach (DataRowView dia in programacion.GetProgramacionFuncionariosDiasHorasExtras(Convert.ToDateTime(txtFecha.Text),
                            Convert.ToString(this.Session["usuario"]), Convert.ToString(this.niddlTurno.SelectedValue),
                            Convert.ToString(this.ddlCuadrilla.SelectedValue), Convert.ToString(registro.Row.ItemArray.GetValue(0)), Convert.ToInt16(Session["empresa"])))
                        {
                            if (Convert.ToInt16(dia.Row.ItemArray.GetValue(1)) >= 0)
                            {
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasLun")).Visible = true;
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasLun")).Text = "0";
                                if (Convert.ToInt16(dia.Row.ItemArray.GetValue(1)) > 0)
                                    ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasLun")).Text = Convert.ToString(dia.Row.ItemArray.GetValue(1));
                            }
                            else
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasLun")).Visible = false;

                            if (Convert.ToInt16(dia.Row.ItemArray.GetValue(2)) >= 0)
                            {
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasMar")).Visible = true;
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasMar")).Text = "0";
                                if (Convert.ToInt16(dia.Row.ItemArray.GetValue(2)) > 0)
                                    ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasMar")).Text = Convert.ToString(dia.Row.ItemArray.GetValue(2));

                            }
                            else
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasMar")).Visible = false;

                            if (Convert.ToInt16(dia.Row.ItemArray.GetValue(3)) >= 0)
                            {
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasMie")).Visible = true;
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasMie")).Text = "0";
                                if (Convert.ToInt16(dia.Row.ItemArray.GetValue(3)) > 0)
                                    ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasMie")).Text = Convert.ToString(dia.Row.ItemArray.GetValue(3));
                            }
                            else
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasMie")).Visible = false;

                            if (Convert.ToInt16(dia.Row.ItemArray.GetValue(4)) >= 0)
                            {
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasJue")).Visible = true;
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasJue")).Text = "0";
                                if (Convert.ToInt16(dia.Row.ItemArray.GetValue(4)) > 0)
                                    ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasJue")).Text = Convert.ToString(dia.Row.ItemArray.GetValue(4));
                            }
                            else
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasJue")).Visible = false;

                            if (Convert.ToInt16(dia.Row.ItemArray.GetValue(5)) >= 0)
                            {
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasVie")).Visible = true;
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasVie")).Text = "0";
                                if (Convert.ToInt16(dia.Row.ItemArray.GetValue(5)) > 0)
                                    ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasVie")).Text = Convert.ToString(dia.Row.ItemArray.GetValue(5));
                            }
                            else
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasVie")).Visible = false;

                            if (Convert.ToInt16(dia.Row.ItemArray.GetValue(6)) >= 0)
                            {
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasSab")).Visible = true;
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasSab")).Text = "0";
                                if (Convert.ToInt16(dia.Row.ItemArray.GetValue(6)) > 0)
                                    ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasSab")).Text = Convert.ToString(dia.Row.ItemArray.GetValue(6));
                            }
                            else
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasSab")).Visible = false;

                            if (Convert.ToInt16(dia.Row.ItemArray.GetValue(7)) >= 0)
                            {
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasDom")).Visible = true;
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasDom")).Text = "0";
                                if (Convert.ToInt16(dia.Row.ItemArray.GetValue(7)) > 0)
                                    ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasDom")).Text = Convert.ToString(dia.Row.ItemArray.GetValue(7));
                            }
                            else
                                ((TextBox)this.gvExtras.Rows[i].FindControl("txtExtrasDom")).Visible = false;
                        }


                    }
                    i++;
                }
                if (sw == false)
                {
                    this.lbRegistrarExtras.Visible = false;
                    this.nilblInformacion.ForeColor = Color.Red;
                    CerroresGeneral.ManejoError(this, GetType(), "Las programaciones no se encuentran ejecutadas para autorizar horas extras", "w");

                }
                else
                    this.lbRegistrarExtras.Visible = true;
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private void Guardar()
        {
            bool verificacion = true;

            try
            {
                string lun = "lun", mar = "mar", mier = "mie", jue = "jue", vie = "vie", sab = "sab", dom = "dom", dia = null, diasemana = null;
                string lunes = "lunes", martes = "martes", miercoles = "miercoles", jueves = "jueves", viernes = "viernes", sabado = "sabado", domingo = "domingo";
                if (Convert.ToString(this.niddlTurno.SelectedValue).Length == 0 || Convert.ToString(this.ddlCuadrilla.SelectedValue).Length == 0 || this.txtFecha.Text.Length == 0 || this.gvLista.Rows.Count == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Campos vacios. Por favor corrija", "w");
                    return;
                }

                using (TransactionScope ts = new TransactionScope())
                {
                    foreach (GridViewRow registro in this.gvLista.Rows)
                    {
                        if (verificacion == true)
                        {
                            for (int x = 1; x <= 7; x++)
                            {
                                var chkDiaSemana = new CheckBox();
                                switch (x)
                                {
                                    case 1:
                                        chkDiaSemana = ((CheckBox)registro.FindControl("chkLun"));
                                        diasemana = lunes;
                                        dia = lun;
                                        break;
                                    case 2:
                                        chkDiaSemana = ((CheckBox)registro.FindControl("chkMar"));
                                        diasemana = martes;
                                        dia = mar;
                                        break;
                                    case 3:
                                        chkDiaSemana = ((CheckBox)registro.FindControl("chkMie"));
                                        diasemana = miercoles;
                                        dia = mier;
                                        break;
                                    case 4:
                                        chkDiaSemana = ((CheckBox)registro.FindControl("chkJue"));
                                        diasemana = jueves;
                                        dia = jue;
                                        break;
                                    case 5:
                                        chkDiaSemana = ((CheckBox)registro.FindControl("chkVie"));
                                        diasemana = viernes;
                                        dia = vie;
                                        break;
                                    case 6:
                                        chkDiaSemana = ((CheckBox)registro.FindControl("chkSab"));
                                        diasemana = sabado;
                                        dia = sab;
                                        break;
                                    case 7:
                                        chkDiaSemana = ((CheckBox)registro.FindControl("chkDom"));
                                        diasemana = domingo;
                                        dia = dom;
                                        break;
                                }

                                if (chkDiaSemana.Enabled)
                                {

                                    switch (programacion.GuardaProgramacion(Convert.ToDateTime(txtFecha.Text), Convert.ToString(this.niddlTurno.SelectedValue), registro.Cells[0].Text,
                                        Convert.ToString(this.ddlCuadrilla.SelectedValue), this.Session["usuario"].ToString(), dia, chkDiaSemana.Checked, Convert.ToInt16(Session["empresa"]), txtObservacionProgramacion.Text))
                                    {
                                        case 1:
                                            this.nilblInformacion.ForeColor = Color.Red;
                                            CerroresGeneral.ManejoError(this, GetType(), "Error al insertar la programación. Operación no realizada", "w");
                                            verificacion = false;
                                            break;

                                        case 2:
                                            this.nilblInformacion.ForeColor = Color.Red;
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " se encuentra programado el mismo día en un turno que coincide con el seleccionado", "w");
                                            verificacion = false;
                                            break;
                                        case 3:
                                            this.nilblInformacion.ForeColor = Color.Red;
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " no se encuentra programado el día " + diasemana + " y este es inferior a la fecha", "w");
                                            verificacion = false;
                                            break;
                                        case 4:
                                            this.nilblInformacion.ForeColor = Color.Red;
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionario " + registro.Cells[1].Text + " No puede eliminar la programación fue ejecutada.", "w");
                                            verificacion = false;
                                            break;
                                        case 5:
                                            this.nilblInformacion.ForeColor = Color.Red;
                                            CerroresGeneral.ManejoError(this, GetType(), "Error al Programar:  hora del turno iniciada", "w");
                                            verificacion = false;
                                            break;
                                        case 6:
                                            this.nilblInformacion.ForeColor = Color.Red;
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionarios " + registro.Cells[1].Text + " no puede ser porgramado el día " + diasemana + ", se encuentra disfrutando compensatorio.", "w");
                                            verificacion = false;
                                            break;
                                        case 7:
                                            this.nilblInformacion.ForeColor = Color.Red;
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionarios " + registro.Cells[1].Text + " no puede ser porgramado el día " + diasemana + ", porque supera las horas extras autorizadas del mes.", "w");
                                            verificacion = false;
                                            break;
                                        case 8:
                                            this.nilblInformacion.ForeColor = Color.Red;
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionarios " + registro.Cells[1].Text + " no puede ser porgramado el día " + diasemana + ", porque supera las horas extras autorizadas de la semana.", "w");
                                            verificacion = false;
                                            break;
                                        case 9:
                                            this.nilblInformacion.ForeColor = Color.Red;
                                            CerroresGeneral.ManejoError(this, GetType(), "El funcionarios " + registro.Cells[1].Text + " no puede ser porgramado el día " + diasemana + ", porque se encuentra de vacaciones.", "w");
                                            verificacion = false;
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    if (!verificacion)
                        return;
                    else
                    {
                        this.nilblInformacion.ForeColor = Color.Green;
                        CerroresGeneral.ManejoError(this, GetType(), "Programación registrada satisfactoriamente", "w");
                        GetProgramacion();
                        ts.Complete();
                    }


                }
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private void ManejarControles()
        {
            this.lbAsignar.Visible = false;
            this.lbRegistrar.Visible = false;

            txtFecha.Enabled = true;
        }
        private void GetProgramacion()
        {
            gvLista.DataSource = null;
            gvLista.DataBind();
            ManejarControles();
            int i = 0;
            this.gvLista.Visible = true;
            txtObservacionProgramacion.Visible = true;
            txtObservacionProgramacion.Text = "";

            DateTime fechahoy = DateTime.Now.Date;
            DateTime fecha = Convert.ToDateTime(txtFecha.Text);
            int diasemana = Convert.ToInt16(fecha.DayOfWeek);
            diasemana = diasemana == 0 ? 7 : diasemana;
            int diaLunes = diasemana - 1;
            int diaDomingo = 7 - diasemana;
            DateTime lunes = fecha.AddDays(-diaLunes);
            DateTime domingo = fecha.AddDays(diaDomingo);
            bool funcionarioNov = false;
            var chkDiaSemanaT = new CheckBox();


            try
            {
                if (Convert.ToString(this.niddlTurno.SelectedValue).Length == 0 || Convert.ToString(Convert.ToDateTime(txtFecha.Text)).Length == 0 ||
                    Convert.ToString(this.ddlCuadrilla.SelectedValue).Length == 0)
                {
                    return;
                }

                this.gvLista.DataSource = programacion.GetProgramacionFuncionariosCuadrilla(Convert.ToString(this.ddlCuadrilla.SelectedValue),
                    Convert.ToString(this.niddlTurno.SelectedValue), Convert.ToDateTime(txtFecha.Text), Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();

                if (Convert.ToInt16(programacion.VerificaProgramacionExiste(Convert.ToDateTime(txtFecha.Text), Convert.ToString(this.niddlTurno.SelectedValue),
                    Convert.ToString(this.ddlCuadrilla.SelectedValue), Convert.ToInt16(Session["empresa"]))) == 0 && this.gvLista.Rows.Count > 0)
                {
                    foreach (GridViewRow gvr in gvLista.Rows)
                    {
                        lunes = fecha.AddDays(-diaLunes);
                        funcionarioNov = false;
                        while (lunes <= domingo)
                        {
                            string idchk = null;
                            string idchkT = null;

                            switch (Convert.ToInt16(lunes.DayOfWeek))
                            {
                                case 1:
                                    idchk = "chkLun";
                                    idchkT = "chkLunT";
                                    break;
                                case 2:
                                    idchk = "chkMar";
                                    idchkT = "chkMarT";
                                    break;
                                case 3:
                                    idchk = "chkMie";
                                    idchkT = "chkMieT";
                                    break;
                                case 4:
                                    idchk = "chkJue";
                                    idchkT = "chkJueT";
                                    break;
                                case 5:
                                    idchk = "chkVie";
                                    idchkT = "chkVieT";
                                    break;
                                case 6:
                                    idchk = "chkSab";
                                    idchkT = "chkSabT";
                                    break;
                                case 0:
                                    idchk = "chkDom";
                                    idchkT = "chkDomT";

                                    break;
                            }
                            chkDiaSemanaT = gvLista.HeaderRow.FindControl(idchkT) as CheckBox;
                            chkDiaSemanaT.Text = idchk.Replace("chk", "").Replace("T", "") + "-" + lunes.Day.ToString();

                            var chkDiasemana = gvr.FindControl(idchk) as CheckBox;
                            var chkSemana = gvr.FindControl("chkAsignacion") as CheckBox;
                            if (lunes < fechahoy)
                            {

                                chkDiaSemanaT.Checked = false;
                                chkSemana.Checked = false;
                                chkDiasemana.Checked = false;
                            }
                            else
                            {
                                chkDiaSemanaT.Checked = false;
                                chkSemana.Checked = false;
                                chkDiasemana.Checked = true;
                            }
                            lunes = lunes.AddDays(1);
                        }
                    }
                }
                else
                {
                    foreach (DataRowView registro in programacion.GetProgramacionFuncionariosCuadrilla(Convert.ToString(this.ddlCuadrilla.SelectedValue),
                        Convert.ToString(this.niddlTurno.SelectedValue), Convert.ToDateTime(txtFecha.Text), Convert.ToInt16(Session["empresa"])))
                    {
                        if (Convert.ToInt16(registro.Row.ItemArray.GetValue(3)) == 1)
                        {
                            ((CheckBox)this.gvLista.Rows[i].FindControl("chkAsignacion")).Checked = true;

                            foreach (DataRowView dia in programacion.GetProgramacionFuncionariosDias(Convert.ToDateTime(txtFecha.Text), Convert.ToString(this.niddlTurno.SelectedValue),
                                Convert.ToString(this.ddlCuadrilla.SelectedValue), Convert.ToString(registro.Row.ItemArray.GetValue(0)), Convert.ToInt16(Session["empresa"])))
                            {
                                if (Convert.ToInt16(dia.Row.ItemArray.GetValue(1)) >= 1)
                                    ((CheckBox)this.gvLista.Rows[i].FindControl("chkLun")).Checked = true;
                                else
                                    ((CheckBox)this.gvLista.Rows[i].FindControl("chkLun")).Checked = false;

                                if (Convert.ToInt16(dia.Row.ItemArray.GetValue(2)) >= 1)
                                    ((CheckBox)this.gvLista.Rows[i].FindControl("chkMar")).Checked = true;
                                else
                                    ((CheckBox)this.gvLista.Rows[i].FindControl("chkMar")).Checked = false;

                                if (Convert.ToInt16(dia.Row.ItemArray.GetValue(3)) >= 1)
                                    ((CheckBox)this.gvLista.Rows[i].FindControl("chkMie")).Checked = true;
                                else
                                    ((CheckBox)this.gvLista.Rows[i].FindControl("chkMie")).Checked = false;

                                if (Convert.ToInt16(dia.Row.ItemArray.GetValue(4)) >= 1)
                                    ((CheckBox)this.gvLista.Rows[i].FindControl("chkJue")).Checked = true;
                                else
                                    ((CheckBox)this.gvLista.Rows[i].FindControl("chkJue")).Checked = false;

                                if (Convert.ToInt16(dia.Row.ItemArray.GetValue(5)) >= 1)
                                    ((CheckBox)this.gvLista.Rows[i].FindControl("chkVie")).Checked = true;
                                else
                                    ((CheckBox)this.gvLista.Rows[i].FindControl("chkVie")).Checked = false;

                                if (Convert.ToInt16(dia.Row.ItemArray.GetValue(6)) >= 1)
                                    ((CheckBox)this.gvLista.Rows[i].FindControl("chkSab")).Checked = true;
                                else
                                    ((CheckBox)this.gvLista.Rows[i].FindControl("chkSab")).Checked = false;

                                if (Convert.ToInt16(dia.Row.ItemArray.GetValue(7)) >= 1)
                                    ((CheckBox)this.gvLista.Rows[i].FindControl("chkDom")).Checked = true;
                                else
                                    ((CheckBox)this.gvLista.Rows[i].FindControl("chkDom")).Checked = false;

                                txtObservacionProgramacion.Text = dia.Row.ItemArray.GetValue(8).ToString();
                            }
                        }
                        else
                            ((CheckBox)this.gvLista.Rows[i].FindControl("chkAsignacion")).Checked = false;

                        i++;
                    }
                }


                DataView dvNovedades = programacion.SeleccionaNovedadesFechaFuncionarios(Convert.ToDateTime(txtFecha.Text), Convert.ToInt16(Session["empresa"]));

                foreach (GridViewRow gvr in gvLista.Rows)
                {
                    lunes = fecha.AddDays(-diaLunes);

                    funcionarioNov = false;

                    while (lunes <= domingo)
                    {
                        string idchk = null;
                        string idchkT = null;

                        switch (Convert.ToInt16(lunes.DayOfWeek))
                        {
                            case 1:
                                idchk = "chkLun";
                                idchkT = "chkLunT";
                                break;
                            case 2:
                                idchk = "chkMar";
                                idchkT = "chkMarT";
                                break;
                            case 3:
                                idchk = "chkMie";
                                idchkT = "chkMieT";
                                break;
                            case 4:
                                idchk = "chkJue";
                                idchkT = "chkJueT";
                                break;
                            case 5:
                                idchk = "chkVie";
                                idchkT = "chkVieT";
                                break;
                            case 6:
                                idchk = "chkSab";
                                idchkT = "chkSabT";
                                break;
                            case 0:
                                idchk = "chkDom";
                                idchkT = "chkDomT";

                                break;
                        }
                        chkDiaSemanaT = gvLista.HeaderRow.FindControl(idchkT) as CheckBox;
                        chkDiaSemanaT.Text = idchk.Replace("chk", "").Replace("T", "") + "-" + lunes.Day.ToString();

                        if (lunes < fechahoy)
                        {
                            var chkDiasemana = gvr.FindControl(idchk) as CheckBox;

                            var chkSemana = gvr.FindControl("chkAsignacion") as CheckBox;
                            chkDiasemana.Enabled = false;
                            chkDiaSemanaT.Checked = false;
                            chkSemana.Checked = false;
                            chkDiasemana.CssClass = "disabled check";

                        }


                        lunes = lunes.AddDays(1);
                    }

                    lunes = fecha.AddDays(-diaLunes);

                    foreach (DataRowView drv in dvNovedades)
                    {
                        if (gvr.Cells[0].Text.Trim() == drv.Row.ItemArray.GetValue(2).ToString().Trim())
                        {
                            funcionarioNov = true;
                            lunes = fecha.AddDays(-diaLunes);
                            while (lunes <= domingo)
                            {
                                var lbldiaSemana = gvr.FindControl("lblNov" + Convert.ToInt16(lunes.DayOfWeek).ToString()) as Label;
                                var divDay = gvr.FindControl("divday" + Convert.ToInt16(lunes.DayOfWeek).ToString()) as HtmlControl;
                                if (lunes >= Convert.ToDateTime(drv.Row.ItemArray.GetValue(3)) & lunes <= Convert.ToDateTime(drv.Row.ItemArray.GetValue(4)))
                                {
                                    lbldiaSemana.Text += "* " + drv.Row.ItemArray.GetValue(1).ToString() + "<br>";
                                    divDay.Attributes["class"] = "tooltipChk";
                                    string idchk = null;
                                    string idchkT = null;

                                    switch (Convert.ToInt16(lunes.DayOfWeek))
                                    {
                                        case 1:
                                            idchk = "chkLun";
                                            idchkT = "chkLunT";
                                            break;
                                        case 2:
                                            idchk = "chkMar";
                                            idchkT = "chkMarT";
                                            break;
                                        case 3:
                                            idchk = "chkMie";
                                            idchkT = "chkMieT";
                                            break;
                                        case 4:
                                            idchk = "chkJue";
                                            idchkT = "chkJueT";
                                            break;
                                        case 5:
                                            idchk = "chkVie";
                                            idchkT = "chkVieT";
                                            break;
                                        case 6:
                                            idchk = "chkSab";
                                            idchkT = "chkSabT";
                                            break;
                                        case 0:
                                            idchk = "chkDom";
                                            idchkT = "chkDomT";
                                            break;
                                    }
                                    var chkDiasemana = gvr.FindControl(idchk) as CheckBox;
                                    chkDiaSemanaT = gvLista.HeaderRow.FindControl(idchkT) as CheckBox;
                                    var chkSemana = gvr.FindControl("chkAsignacion") as CheckBox;
                                    chkDiasemana.Enabled = false;
                                    chkDiasemana.Checked = false;
                                    chkDiasemana.CssClass = "disabled check novedad";
                                    chkDiaSemanaT.Checked = false;
                                    chkSemana.Checked = false;
                                }


                                lunes = lunes.AddDays(1);
                            }
                        }

                    }

                    if (!funcionarioNov)
                    {
                        ((HtmlControl)gvr.FindControl("divday1")).Attributes["class"] = "";
                        ((HtmlControl)gvr.FindControl("divday2")).Attributes["class"] = "";
                        ((HtmlControl)gvr.FindControl("divday3")).Attributes["class"] = "";
                        ((HtmlControl)gvr.FindControl("divday4")).Attributes["class"] = "";
                        ((HtmlControl)gvr.FindControl("divday5")).Attributes["class"] = "";
                        ((HtmlControl)gvr.FindControl("divday6")).Attributes["class"] = "";
                        ((HtmlControl)gvr.FindControl("divday0")).Attributes["class"] = "";
                    }

                }

                this.lbAsignar.Visible = true;
                this.lbRegistrar.Visible = true;
                txtObservacionAdicion.Visible = false;
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }


        private void CargaCombos()
        {
            try
            {
                DataView dvTurnos = CentidadMetodos.EntidadGet("nTurno", "ppa").Tables[0].DefaultView;
                EnumerableRowCollection<DataRow> query = from order in dvTurnos.Table.AsEnumerable()
                                                         where order.Field<int>("empresa") == Convert.ToInt16(this.Session["empresa"])
                                                         orderby order.Field<int>("horaInicio")
                                                         select order;
                var dr = query.AsDataView();
                this.niddlTurno.DataSource = dr;
                this.niddlTurno.DataValueField = "codigo";
                this.niddlTurno.DataTextField = "descripcion";
                this.niddlTurno.DataBind();
                this.niddlTurno.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }

            try
            {
                this.ddlCuadrilla.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nCuadrilla", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"]));
                this.ddlCuadrilla.DataValueField = "codigo";
                this.ddlCuadrilla.DataTextField = "descripcion";
                this.ddlCuadrilla.DataBind();
                this.ddlCuadrilla.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }

        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                 "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
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
                    {
                        CargaCombos();
                        ManejarControles();
                    }

                }
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }

        protected void ddlCuadrilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.nilblInformacion.Text = "";
            this.gvExtras.DataSource = null;
            this.gvExtras.DataBind();
            this.lbRegistrarExtras.Visible = false;
            GetProgramacion();
        }



        protected void CalendarFecha_SelectionChanged(object sender, EventArgs e)
        {
            this.nilblInformacion.Text = "";
            this.ddlCuadrilla.Focus();
            this.txtFecha.Visible = true;
            this.txtFecha.Text = Convert.ToDateTime(txtFecha.Text).ToShortDateString();
            this.gvExtras.Visible = false;
            this.gvExtras.DataSource = null;
            this.gvExtras.DataBind();
            this.lbRegistrarExtras.Visible = false;

            GetProgramacion();
        }




        protected void chkAsignacion_CheckedChanged(object sender, EventArgs e)
        {
            ((CheckBox)this.gvLista.Rows[CcontrolesUsuario.IndiceControlGrilla("chkAsignacion", ((CheckBox)sender).ClientID)].Cells[4].FindControl("chkLun")).Checked = ((CheckBox)sender).Checked;
            ((CheckBox)this.gvLista.Rows[CcontrolesUsuario.IndiceControlGrilla("chkAsignacion", ((CheckBox)sender).ClientID)].Cells[5].FindControl("chkMar")).Checked = ((CheckBox)sender).Checked;
            ((CheckBox)this.gvLista.Rows[CcontrolesUsuario.IndiceControlGrilla("chkAsignacion", ((CheckBox)sender).ClientID)].Cells[6].FindControl("chkMie")).Checked = ((CheckBox)sender).Checked;
            ((CheckBox)this.gvLista.Rows[CcontrolesUsuario.IndiceControlGrilla("chkAsignacion", ((CheckBox)sender).ClientID)].Cells[7].FindControl("chkJue")).Checked = ((CheckBox)sender).Checked;
            ((CheckBox)this.gvLista.Rows[CcontrolesUsuario.IndiceControlGrilla("chkAsignacion", ((CheckBox)sender).ClientID)].Cells[8].FindControl("chkVie")).Checked = ((CheckBox)sender).Checked;
            ((CheckBox)this.gvLista.Rows[CcontrolesUsuario.IndiceControlGrilla("chkAsignacion", ((CheckBox)sender).ClientID)].Cells[9].FindControl("chkSab")).Checked = ((CheckBox)sender).Checked;
            ((CheckBox)this.gvLista.Rows[CcontrolesUsuario.IndiceControlGrilla("chkAsignacion", ((CheckBox)sender).ClientID)].Cells[10].FindControl("chkDom")).Checked = ((CheckBox)sender).Checked;
        }



        protected void niddlTurno_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.nilblInformacion.Text = "";
            this.gvExtras.Visible = false;
            this.gvExtras.DataSource = null;
            this.gvExtras.DataBind();
            this.lbRegistrarExtras.Visible = false;

            GetProgramacion();
        }

        protected void imbExtras_Click(object sender, ImageClickEventArgs e)
        {
            string funcionario = this.gvLista.Rows[CcontrolesUsuario.IndiceControlGrilla("imbExtras", ((ImageButton)sender).ClientID)].Cells[0].Text;
            CerroresGeneral.ManejoError(this, GetType(), funcionario, "w");
        }



        protected void lbAsignar_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void imbPermisos_Click(object sender, ImageClickEventArgs e)
        {
            string funcionario = this.gvLista.Rows[CcontrolesUsuario.IndiceControlGrilla("imbPermisos", ((ImageButton)sender).ClientID)].Cells[0].Text;
            string nombre = this.gvLista.Rows[CcontrolesUsuario.IndiceControlGrilla("imbPermisos", ((ImageButton)sender).ClientID)].Cells[1].Text;
            string script = "<script language='javascript'>" + "AutorizaPermiso('" + funcionario + "','" + nombre + "','" + Convert.ToString(this.niddlTurno.SelectedValue) + "');" + "</script>";
            Page.RegisterStartupScript("AutorizaPermiso", script);
        }

        protected void imbInformeProgramacion_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void imbInformeEntradas_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void imbCuadrilla0_Click(object sender, ImageClickEventArgs e)
        {

        }



        protected void txtFecha_TextChanged(object sender, EventArgs e)
        {
            try
            {
                nilblInformacion.Text = "";
                Convert.ToDateTime(txtFecha.Text);
                GetProgramacion();
            }
            catch
            {
                CcontrolesUsuario.MensajeError("Formato de fecha no valido", nilblInformacion);
                txtFecha.Text = "";
                txtFecha.Focus();
                return;
            }
        }



        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            GetFuncionariosIndividual();
        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            GuardarExtras();
        }



        protected void lbRefresca_Click(object sender, EventArgs e)
        {
            this.nilblInformacion.Text = "";
            this.gvExtras.Visible = false;
            this.gvExtras.DataSource = null;
            this.gvExtras.DataBind();
            this.lbRegistrarExtras.Visible = false;

            GetProgramacion();
        }



        protected void imbCuadrilla_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), "Cuadrillas.aspx", Convert.ToInt16(Session["empresa"])) == 0)
                ManejoError("Usuario no autorizado para acceder a esta página", "C");
            else
                this.Response.Redirect("Cuadrillas.aspx");
        }

        protected void imbTurnos_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), "Turnos.aspx", Convert.ToInt16(Session["empresa"])) == 0)
                ManejoError("Usuario no autorizado para acceder a esta página", "C");
            else
                this.Response.Redirect("Turnos.aspx");
        }

        protected void imbCuadrilla0_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(this.niddlTurno.SelectedValue).Length == 0 || Convert.ToString(Convert.ToDateTime(txtFecha.Text)).Length == 0 ||
                   Convert.ToString(this.ddlCuadrilla.SelectedValue).Length == 0)
            {
                this.nilblInformacion.ForeColor = Color.Red;
                CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar un turno, la fecha y la cuadrilla", "w");
                return;
            }

            this.gvExtras.Visible = true;
            this.lbRegistrarExtras.Visible = true;
            this.gvLista.Visible = false;
            this.lbRegistrar.Visible = false;
            this.lbAsignar.Visible = false;
            txtObservacionAdicion.Visible = true;
            GetEntidadExtras();
        }

        protected void imbInformeProgramacion_Click(object sender, EventArgs e)
        {
            string script = "<script language='javascript'>" + "Visualizacion2('FuncionarioProgramado');" + "</script>";
            Page.RegisterStartupScript("Visualizacion", script);
        }

        protected void imbInformeEntradas_Click(object sender, EventArgs e)
        {
            string script = "<script language='javascript'>" + "Visualizacion2('PersonalEnPlanta');" + "</script>";
            Page.RegisterStartupScript("Registro", script);
        }
        #endregion Eventos



        protected void txtFecha_TextChanged1(object sender, EventArgs e)
        {
            this.nilblInformacion.Text = "";
            this.ddlCuadrilla.Focus();
            this.txtFecha.Visible = true;
            this.txtFecha.Text = Convert.ToDateTime(txtFecha.Text).ToShortDateString();
            this.gvExtras.Visible = false;
            this.gvExtras.DataSource = null;
            this.gvExtras.DataBind();
            this.lbRegistrarExtras.Visible = false;

            GetProgramacion();
        }
    }
}