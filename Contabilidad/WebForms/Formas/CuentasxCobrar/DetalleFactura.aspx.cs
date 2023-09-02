using Contabilidad.seguridadInfos;
using Contabilidad.WebForms.App_Code.Administracion;
using Contabilidad.WebForms.App_Code.General;
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

namespace Contabilidad.WebForms.Formas.CuentasxCobrar
{
    public partial class DetalleFactura : BasePage
    {
        #region Instancias


        Security seguridad = new Security();
        Ctransacciones transacciones = new Ctransacciones();
        CtipoTransaccion tipoTransaccion = new CtipoTransaccion();
        CIP ip = new CIP();

        #endregion Instancias

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

        private void ManejoError(string error, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), error, "error");

            seguridad.InsertaLog(Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + Page.ToString(),
                "er", error, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
        }
        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");

            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            seguridad.InsertaLog(Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + Page.ToString(),
                "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
        }


        private void TotalizaGrillaReferencia()
        {
            try
            {
                nitxtTotalDescuento.Visible = true;
                nitxtTotal.Visible = true;
                nitxtTotalImpuesto.Visible = true;
                nitxtTotalValorBruto.Visible = true;
                nilblValorTotal.Visible = true;
                nilblValorTotal0.Visible = true;
                nilblValorTotal1.Visible = true;
                nilblValorTotal2.Visible = true;
                this.nitxtTotalDescuento.Text = "0";
                this.nitxtTotalImpuesto.Text = "0";
                this.nitxtTotalValorBruto.Text = "0";
                this.nitxtTotalValorBruto.Text = "0";
                nitxtTotalDescuento.Text = "0";
                nitxtTotal.Text = "0";

                foreach (GridViewRow registro in this.gvDetalle.Rows)
                {
                    this.nitxtTotalValorBruto.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(registro.Cells[4].Text) + Convert.ToDecimal(this.nitxtTotalValorBruto.Text)).ToString("N2"));

                }
                nitxtTotalImpuesto.Text = "0";
                decimal valorBruto = 0;
                valorBruto = Convert.ToDecimal(nitxtTotalValorBruto.Text) - Convert.ToDecimal(nitxtTotalDescuento.Text);
                foreach (GridViewRow registro in this.gvImpuesto.Rows)
                {
                    registro.Cells[5].Text = Convert.ToString(Convert.ToDecimal((valorBruto * Convert.ToDecimal(registro.Cells[3].Text) / 100) * (Convert.ToDecimal(registro.Cells[2].Text) / 100)).ToString("N2"));
                    this.nitxtTotalImpuesto.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(registro.Cells[5].Text) + Convert.ToDecimal(this.nitxtTotalImpuesto.Text)).ToString("N2"));
                }

                this.nitxtTotal.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(nitxtTotalImpuesto.Text) + Convert.ToDecimal(nitxtTotalValorBruto.Text) - Convert.ToDecimal(this.nitxtTotalDescuento.Text)).ToString("N2"));
            }
            catch (Exception ex)
            {
                ManejoError("Error al totalizar la grilla de referencia. Correspondiente a: " + limpiarMensaje(ex.Message), "C");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {
                //if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) != 0)
                //{
                if (!IsPostBack)
                {
                    try
                    {
                        if (Request.QueryString["numero"] != null)
                        {
                            string tipo = Request.QueryString["tipo"].ToString();
                            string numero = Request.QueryString["numero"].ToString();
                            foreach (DataRowView drv in transacciones.GetFacturaContableEncabezado(tipo, numero, Convert.ToInt32(this.Session["empresa"])))
                            {
                                lblTipo.Text = drv.Row.ItemArray.GetValue(0).ToString();
                                lblNumero.Text = drv.Row.ItemArray.GetValue(1).ToString();
                                lblFecha.Text = Convert.ToDateTime(drv.Row.ItemArray.GetValue(2)).ToShortDateString();
                                if (drv.Row.ItemArray.GetValue(4).ToString().Trim().Length != 0)
                                {
                                    lblNitProveedor.Text = drv.Row.ItemArray.GetValue(3).ToString();
                                    lblIdTercero.Text = "(" + drv.Row.ItemArray.GetValue(12).ToString() + ")";
                                    lblNombreProveedor.Text = drv.Row.ItemArray.GetValue(4).ToString();
                                    lblCodSucursal.Text = drv.Row.ItemArray.GetValue(10).ToString();
                                    lblNombreSucursal.Text = drv.Row.ItemArray.GetValue(11).ToString();
                                    lblProveedor.Visible = true;
                                    lblNitProveedor.Visible = true;
                                    lblNombreProveedor.Visible = true;
                                    lblCodSucursal.Visible = true;
                                    lblNombreSucursal.Visible = true;
                                    lblUsuario.Text = drv.Row.ItemArray.GetValue(8).ToString();
                                    lblFechaRegistro.Text = drv.Row.ItemArray.GetValue(13).ToString();
                                    lblRegistroAnulado.Text = drv.Row.ItemArray.GetValue(14).ToString();
                                }
                                else
                                {
                                    lblProveedor.Visible = false;
                                    lblNitProveedor.Visible = false;
                                    lblNombreProveedor.Visible = false;
                                    lblCodSucursal.Visible = false;
                                    lblNombreSucursal.Visible = false;
                                }

                                chkAnulado.Checked = Convert.ToBoolean(drv.Row.ItemArray.GetValue(5).ToString());
                                chkCerrado.Checked = Convert.ToBoolean(drv.Row.ItemArray.GetValue(7).ToString());
                                chkAprobado.Checked = Convert.ToBoolean(drv.Row.ItemArray.GetValue(6).ToString());
                            }

                            gvDetalle.DataSource = transacciones.GetFacturaContableDetalle(tipo, numero, Convert.ToInt32(this.Session["empresa"]));
                            gvDetalle.DataBind();

                            gvImpuesto.DataSource = transacciones.GetFacturaContableImpuesto(tipo, numero, Convert.ToInt32(this.Session["empresa"]));
                            gvImpuesto.DataBind();
                            TotalizaGrillaReferencia();
                        }
                    }
                    catch (Exception ex)
                    {
                        ManejoError("Error al cargar transaccion debido a: " + limpiarMensaje(ex.Message), "C");
                    }

                }
                //}
                //else
                //    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }
        protected void imbCancelar_Click(object sender, EventArgs e)
        {

        }
        protected void imbGuardar_Click(object sender, EventArgs e)
        {
            bool verificacion = false;
            try
            {
                foreach (GridViewRow registro in this.gvDetalle.Rows)
                {
                    if (((TextBox)registro.Cells[0].FindControl("txtCantidad")).Text.Trim().Length != 0 && Convert.ToDecimal(((TextBox)registro.Cells[0].FindControl("txtCantidad")).Text.Trim()) != 0)
                        verificacion = true;
                }


                //using (TransactionScope ts = new TransactionScope())
                //{
                //    foreach (GridViewRow registro in this.gvDetalle.Rows)
                //    {
                //        if (((TextBox)registro.Cells[0].FindControl("txtCantidad")).Text.Trim().Length != 0 &&
                //            Convert.ToDecimal(((TextBox)registro.Cells[0].FindControl("txtCantidad")).Text.Trim()) != 0)
                //        {
                //            switch (solicitud.ApruebaOrden(lblTipo.Text.Trim(), lblNumero.Text, registro.Cells[1].Text,
                //                Convert.ToDecimal(((TextBox)registro.FindControl("txtCantidad")).Text), this.Session["usuario"].ToString(), Convert.ToInt16(registro.Cells[9].Text), Convert.ToInt16(Session["empresa"])))
                //            {
                //                case 1:
                //                    verificacion = false;
                //                    break;
                //            }
                //        }
                //    }

                //    if (verificacion == false)
                //    {
                //        ManejoError("Error al actualizar el registro. Operación no realizada", "A");
                //        return;
                //    }

                //    //transacciones.EnviaCorreo(this.lblTransaccion.Text.Trim());
                //    ManejoExito("Orden de compra aprobada satisfactoriamente", "A");
                //    ts.Complete();
                //}
            }
            catch (Exception ex)
            {
                ManejoError("Error al aprobar la orden de compra. Correspondiente a: " + limpiarMensaje(ex.Message), "A");
            }

        }
        protected void btnEstudio_Click(object sender, EventArgs e)
        {
            //string script = "";
            //script = "<script language='javascript'>Visualizacion('" + this.lblNombreSolicitante.Text.Trim() + "');</script>";
            //Page.RegisterStartupScript("Visualizacion", script);

        }
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void imbActualizar_Click(object sender, EventArgs e)
        {

        }


    }
}