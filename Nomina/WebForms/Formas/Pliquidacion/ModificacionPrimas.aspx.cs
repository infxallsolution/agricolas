using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
using Nomina.WebForms.App_Code.Liquidacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Pliquidacion
{
    public partial class ModificacionPrimas : BasePage
    {

        public List<LiquidacionPrima> ListadoDetallePrimas
        {
            get
            {
                object o = ViewState["ListadoDetallePrimas"];
                return (o == null) ? null : (List<LiquidacionPrima>)o;
            }
            set
            {
                ViewState["ListadoDetallePrimas"] = value;
            }
        }

        public string Tipo
        {
            get
            {
                object o = ViewState["Tipo"];
                return (o == null) ? null : (string)o;
            }
            set
            {
                ViewState["Tipo"] = value;
            }
        }
        public string Numero
        {
            get
            {
                object o = ViewState["Numero"];
                return (o == null) ? null : (string)o;
            }
            set
            {
                ViewState["Numero"] = value;
            }
        }


        #region Instancias



        Cperiodos periodo = new Cperiodos();
        CModificacionPrimas modificacionPrima = new CModificacionPrimas();
        #endregion Instancias

        #region Metodos



        private string nombrePaginaActual()
        {
            string[] arrResult = HttpContext.Current.Request.RawUrl.Split('/');
            string result = arrResult[arrResult.GetUpperBound(0)];
            arrResult = result.Split('?');
            return arrResult[arrResult.GetLowerBound(0)];
        }

        private void InitView()
        {
            GuardarParametros();
            CargarCabeceraPrima();
            CargarDetallePrima();
            CargarTerceros();
        }

        private void CargarTerceros()
        {
            string empresa = (Session["empresa"] ?? "").ToString();
            try
            {
                if (ListadoDetallePrimas.Count == 0)
                    return;
                var ids = ListadoDetallePrimas.Select(item => item.CodigoTercero).Aggregate((s1, s2) => s1 + ", " + s2);
                var ds = modificacionPrima.CargarTerceros(empresa);
                var dv = ds.Tables[0].AsDataView();
                dv.RowFilter = "id not in (" + ids + ")";
                ddlTercero.DataSource = dv;
                ddlTercero.DataValueField = "id";
                ddlTercero.DataTextField = "descripcion";

                ddlTercero.DataBind();
                ddlTercero.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los terceros." + ex.ToString(), "IN");
            }
        }

        private void GuardarParametros()
        {
            Tipo = (Request["tipo"] ?? "").ToString();
            Numero = (Request["numero"] ?? "").ToString();
        }

        private void CargarCabeceraPrima()
        {
            string empresa = (Session["empresa"] ?? "").ToString();
            string tipo = (Tipo ?? "").ToString();
            string numero = (Numero ?? "").ToString();

            try
            {
                var dr = modificacionPrima.CargarCabeceraPrima(empresa, tipo, numero);
                if (dr == null)
                    return;
                this.txtTipo.Text = dr["tipo"].ToString();
                this.txtNumero.Text = dr["numero"].ToString();
                this.txtPeriodoPago.Text = dr["periodo"].ToString();
                this.txtAñoPago.Text = dr["año"].ToString();
                this.txtFecha.Text = (!(dr["fecha"] is DateTime) ? "N/A" : ((DateTime)dr["fecha"]).ToString("yyyy/MM/dd"));
                this.txtObservacion.Text = dr["observacion"].ToString();
                this.txtAñoDesde.Text = dr["añoInicial"].ToString();
                this.txtAñoHasta.Text = dr["añoFinal"].ToString();
                this.txtPeriodoDesde.Text = dr["periodoInicial"].ToString();
                this.txtPeriodoHasta.Text = dr["periodoFinal"].ToString();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los detalles de la liquidación." + ex.ToString(), "IN");
            }
        }

        private void CargarDetallePrima()
        {
            string empresa = (Session["empresa"] ?? "").ToString();
            string tipo = (Tipo ?? "").ToString();
            string numero = (Numero ?? "").ToString();

            try
            {
                var ds = modificacionPrima.CargarDetallePrima(empresa, tipo, numero);
                ListadoDetallePrimas = new List<LiquidacionPrima>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var item = new LiquidacionPrima();
                    item.CodigoTercero = dr["codigoTercero"].ToString();
                    item.IdentificacionTercero = dr["identificacionTercero"].ToString();
                    item.NombreTercero = dr["nombreTercero"].ToString();
                    item.FechaIngreso = (!(dr["fechaIngreso"] is DateTime) ? "N/A" : ((DateTime)dr["fechaIngreso"]).ToString("yyyy/MM/dd"));
                    item.FechaInicial = (!(dr["fechaInicial"] is DateTime) ? "N/A" : ((DateTime)dr["fechaInicial"]).ToString("yyyy/MM/dd"));
                    item.FechaFinal = (!(dr["fechaFinal"] is DateTime) ? "N/A" : ((DateTime)dr["fechaFinal"]).ToString("yyyy/MM/dd"));
                    item.Basico = (!(dr["basico"] is int) ? "0" : ((int)dr["basico"]).ToString("#,#"));
                    item.Transporte = !(dr["transporte"] is int) ? "0" : ((int)dr["transporte"]).ToString("#,#0");
                    item.ValorPromedio = (!(dr["valorPromedio"] is int) ? "0" : ((int)dr["valorPromedio"]).ToString("#,#0"));
                    item.Base = (!(dr["base"] is int) ? "0" : ((int)dr["base"]).ToString("#,#0"));
                    item.DiasPromedio = dr["diasPromedio"].ToString();
                    item.DiasPrimas = dr["diasPrimas"].ToString();
                    item.ValorPrima = (!(dr["valorPrima"] is int) ? "0" : ((int)dr["valorPrima"]).ToString("#,#0"));
                    item.Contrato = !(dr["contrato"] is int) ? 0 : (int)dr["contrato"];
                    ListadoDetallePrimas.Add(item);
                }
                gvDetalleLiquidacion.DataSource = ListadoDetallePrimas;
                gvDetalleLiquidacion.DataBind();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los detalles de la liquidación." + ex.ToString(), "IN");
            }
        }

        private void GuardarDatos()
        {
            string empresa = (Session["empresa"] ?? "").ToString();
            string tipo = (Tipo ?? "").ToString();
            string numero = (Numero ?? "").ToString();

            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("empresa");
                table.Columns.Add("tipo");
                table.Columns.Add("numero");
                table.Columns.Add("tercero");
                table.Columns.Add("añoInicial");
                table.Columns.Add("añoFinal");
                table.Columns.Add("periodoInical");
                table.Columns.Add("periodoFinal");
                table.Columns.Add("fechaInicial");
                table.Columns.Add("fechaFinal");
                table.Columns.Add("fechaIngreso");
                table.Columns.Add("basico");
                table.Columns.Add("valorTransporte");
                table.Columns.Add("valorPromedio");
                table.Columns.Add("base");
                table.Columns.Add("diasPromedio");
                table.Columns.Add("diasPrimas");
                table.Columns.Add("valorPrima");
                table.Columns.Add("contrato");
                foreach (GridViewRow item in gvDetalleLiquidacion.Rows)
                {
                    var dr = table.NewRow();
                    dr["empresa"] = Convert.ToInt32(empresa);
                    dr["tipo"] = tipo;
                    dr["numero"] = numero;
                    dr["tercero"] = Convert.ToInt32(item.Cells[1].Text);
                    dr["añoInicial"] = Convert.ToInt32(txtAñoDesde.Text.Trim());
                    dr["añoFinal"] = Convert.ToInt32(txtAñoHasta.Text.Trim());
                    dr["periodoInical"] = Convert.ToInt32(txtPeriodoDesde.Text.Trim());
                    dr["periodoFinal"] = Convert.ToInt32(txtPeriodoHasta.Text.Trim());
                    dr["fechaInicial"] = Convert.ToDateTime(item.Cells[5].Text).ToString("yyyy/MM/dd");
                    dr["fechaFinal"] = Convert.ToDateTime(item.Cells[6].Text).ToString("yyyy/MM/dd");
                    dr["fechaIngreso"] = Convert.ToDateTime(item.Cells[4].Text).ToString("yyyy/MM/dd");
                    dr["basico"] = Convert.ToDecimal(((TextBox)item.FindControl("txvBasico")).Text);
                    dr["valorTransporte"] = Convert.ToDecimal(((TextBox)item.FindControl("txvTransporte")).Text);
                    dr["valorPromedio"] = Convert.ToDecimal(((TextBox)item.FindControl("txvValorPromedio")).Text);
                    dr["base"] = Convert.ToDecimal(((TextBox)item.FindControl("txvBase")).Text);
                    dr["diasPromedio"] = Convert.ToDecimal(((TextBox)item.FindControl("txvDiasPromedio")).Text);
                    dr["diasPrimas"] = Convert.ToDecimal(((TextBox)item.FindControl("txvDiasPrima")).Text);
                    dr["valorPrima"] = Convert.ToDecimal(((TextBox)item.FindControl("txvValorPrima")).Text);
                    dr["contrato"] = Convert.ToDecimal(item.Cells[14].Text);
                    table.Rows.Add(dr);
                };
                modificacionPrima.GuardarCambios(table);
                CargarDetallePrima();
                MostrarMensaje("Cambios guardados exitosamente");
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los detalles de la liquidación." + ex.ToString(), "IN");
            }
        }

        private void GatheringAndValidation()
        {
            bool valid = true;
            Regex nonNumericRegex = new Regex(@"\D");
            foreach (GridViewRow dr in gvDetalleLiquidacion.Rows)
            {
                var item = ListadoDetallePrimas.Where(i => i.CodigoTercero == Server.HtmlDecode(dr.Cells[1].Text).Trim() && i.IdentificacionTercero == Server.HtmlDecode(dr.Cells[2].Text).Trim()).First();
                var txvBasico = ((TextBox)dr.FindControl("txvBasico"));
                var value = txvBasico.Text.Replace(",", "");
                txvBasico.BackColor = new Color();
                txvBasico.ForeColor = new Color();
                txvBasico.BorderColor = new Color();
                if (nonNumericRegex.IsMatch(value))
                {
                    txvBasico.BackColor = Color.FromArgb(255, 218, 218);
                    txvBasico.ForeColor = Color.DarkRed;
                    txvBasico.BorderColor = Color.DarkRed;
                    valid = false;
                }
                else
                {
                    item.Basico = value;
                }

                var txvTransporte = ((TextBox)dr.FindControl("txvTransporte"));
                value = txvTransporte.Text.Replace(",", "");
                txvTransporte.BackColor = new Color();
                txvTransporte.ForeColor = new Color();
                txvTransporte.BorderColor = new Color();
                if (nonNumericRegex.IsMatch(value))
                {
                    txvTransporte.BackColor = Color.FromArgb(255, 218, 218);
                    txvTransporte.ForeColor = Color.DarkRed;
                    txvTransporte.BorderColor = Color.DarkRed;
                    valid = false;
                }
                else
                {
                    item.Transporte = value;
                }

                var txvValorPromedio = (TextBox)dr.FindControl("txvValorPromedio");
                value = txvValorPromedio.Text.Replace(",", "");
                txvValorPromedio.BackColor = new Color();
                txvValorPromedio.ForeColor = new Color();
                txvValorPromedio.BorderColor = new Color();
                if (nonNumericRegex.IsMatch(value))
                {
                    txvValorPromedio.BackColor = Color.FromArgb(255, 218, 218);
                    txvValorPromedio.ForeColor = Color.DarkRed;
                    txvValorPromedio.BorderColor = Color.DarkRed;
                    valid = false;
                }
                else
                {
                    item.ValorPromedio = value;
                }

                var txvDiasPromedio = (TextBox)dr.FindControl("txvDiasPromedio");
                value = txvDiasPromedio.Text.Replace(",", "");
                txvDiasPromedio.BackColor = new Color();
                txvDiasPromedio.ForeColor = new Color();
                txvDiasPromedio.BorderColor = new Color();
                if (nonNumericRegex.IsMatch(value))
                {
                    txvDiasPromedio.BackColor = Color.FromArgb(255, 218, 218);
                    txvDiasPromedio.ForeColor = Color.DarkRed;
                    txvDiasPromedio.BorderColor = Color.DarkRed;
                    valid = false;
                }
                else
                {
                    item.DiasPromedio = value;
                }

                // Base calculado ValorPromedio*DiasPrimedio*30
                item.Base = (Convert.ToInt32(item.DiasPromedio) != 0 ? Convert.ToInt32(item.ValorPromedio) / Convert.ToInt32(item.DiasPromedio) * 30 : 0).ToString();

                var txvDiasPrima = (TextBox)dr.FindControl("txvDiasPrima");
                value = txvDiasPrima.Text.Replace(",", "");
                txvDiasPrima.BackColor = new Color();
                txvDiasPrima.ForeColor = new Color();
                txvDiasPrima.BorderColor = new Color();
                if (nonNumericRegex.IsMatch(value))
                {
                    txvDiasPrima.BackColor = Color.FromArgb(255, 218, 218);
                    txvDiasPrima.ForeColor = Color.DarkRed;
                    txvDiasPrima.BorderColor = Color.DarkRed;
                    valid = false;
                }
                else
                {
                    item.DiasPrimas = value;
                }

                // Calculado Base * DiasPrima / 360 
                item.ValorPrima = Math.Round(Convert.ToDouble(item.Base) * Convert.ToDouble(item.DiasPrimas) / 360).ToString();
            }

            if (!valid)
            {
                MostrarMensaje("Todos los campos de entrada deben ser números entreros positivos. Por favor, corrija.");
                return;
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
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
                    return;
                }
                if (!IsPostBack)
                {
                    InitView();
                }
                else
                    GatheringAndValidation();
            }
        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("~/Formas/Pliquidacion/LiquidacionPrimas.aspx", false);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarDatos();
        }

        protected void gvDetalleLiquidacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var dr = gvDetalleLiquidacion.Rows[e.RowIndex];
            var item = ListadoDetallePrimas.Where(i => i.CodigoTercero == Server.HtmlDecode(dr.Cells[1].Text).Trim() && i.IdentificacionTercero == Server.HtmlDecode(dr.Cells[2].Text).Trim()).First();
            ListadoDetallePrimas.Remove(item);
            gvDetalleLiquidacion.DataSource = ListadoDetallePrimas;
            gvDetalleLiquidacion.DataBind();
            CargarTerceros();
        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            try
            {
                bool valid = true;
                if (ddlTercero.SelectedValue == "")
                {
                    MostrarMensaje("Debe seleccionar un tercero");
                    valid = false;
                }
                if (txtFechaFin.Text.Trim() == "")
                {
                    MostrarMensaje("Debe seleccionar una fecha de fin");
                    valid = false;
                }
                if (txtFechaIngreso.Text.Trim() == "")
                {
                    MostrarMensaje("Debe seleccionar una fecha de ingreso");
                    valid = false;
                }
                if (txtFechaInicio.Text.Trim() == "")
                {
                    MostrarMensaje("Debe seleccionar una fecha de inicio");
                    valid = false;
                }
                if (!valid)
                    return;

                var dr = modificacionPrima.CargarInformacionTercero(Session["empresa"].ToString(), ddlTercero.SelectedValue);
                var item = new LiquidacionPrima()
                {
                    CodigoTercero = !(dr["id"] is int) ? string.Empty : ((int)dr["id"]).ToString(),
                    IdentificacionTercero = !(dr["identificacion"] is string) ? string.Empty : (string)dr["identificacion"],
                    NombreTercero = !(dr["descripcion"] is string) ? string.Empty : (string)dr["descripcion"],
                    Contrato = !(dr["contrato"] is int) ? int.MinValue : (int)dr["contrato"],
                    FechaFinal = Convert.ToDateTime(txtFechaFin.Text).ToString("yyyy/MM/dd"),
                    FechaInicial = Convert.ToDateTime(txtFechaInicio.Text).ToString("yyyy/MM/dd"),
                    FechaIngreso = Convert.ToDateTime(txtFechaIngreso.Text).ToString("yyyy/MM/dd"),
                    Base = "0",
                    Basico = "0",
                    DiasPrimas = "0",
                    DiasPromedio = "0",
                    Transporte = "0",
                    ValorPrima = "0",
                    ValorPromedio = "0"
                };
                ListadoDetallePrimas.Add(item);
                ListadoDetallePrimas = ListadoDetallePrimas.OrderBy(_i => Convert.ToInt32(_i.CodigoTercero)).ToList();

                gvDetalleLiquidacion.DataSource = ListadoDetallePrimas;
                gvDetalleLiquidacion.DataBind();

                CargarTerceros();

                ddlTercero.SelectedValue = "";
                txtFechaInicio.Text = "";
                txtFechaFin.Text = "";
                txtFechaIngreso.Text = "";
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los detalles de la liquidación." + ex.ToString(), "IN");
            }
        }

        protected void txtFechaIngreso_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFechaIngreso.Text);
            }
            catch
            {
                MostrarMensaje("Formato de fecha no valido");
                txtFechaIngreso.Text = "";
                txtFechaIngreso.Focus();
            }
        }


        protected void txtFechaInicio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFechaInicio.Text);
            }
            catch
            {
                MostrarMensaje("Formato de fecha no valido");
                txtFechaInicio.Text = "";
                txtFechaInicio.Focus();
            }
        }





        protected void txtFechaFin_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFechaFin.Text);
            }
            catch
            {
                MostrarMensaje("Formato de fecha no valido");
                txtFechaFin.Text = "";
                txtFechaFin.Focus();
            }
        }

        #endregion Eventos

    }
}