using Almacen.WebForms.App_Code.General;
using Almacen.WebForms.App_Code.Transaccion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Almacen.WebForms.Formas.Ptransaccion
{
    public partial class Detalle : BasePage
    {
        #region Instancias


        Caprobar solicitud = new Caprobar();
        Ctransacciones transacciones = new Ctransacciones();
        CtipoTransaccion tipoTransaccion = new CtipoTransaccion();

        #endregion Instancias


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
                    this.nitxtTotalValorBruto.Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(registro.Cells[5].Text) + Convert.ToDecimal(this.nitxtTotalValorBruto.Text)).ToString("N2"));

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
                ManejoErrorCatch(ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {
                if (!IsPostBack)
                {
                    try
                    {
                        if (Request.QueryString["numero"] != null)
                        {
                            string tipo = Request.QueryString["tipo"].ToString();
                            string numero = Request.QueryString["numero"].ToString();
                            foreach (DataRowView drv in transacciones.GetTransaccionesInventarioEncabezado(tipo, numero, Convert.ToInt32(this.Session["empresa"])))
                            {
                                lblTipo.Text = drv.Row.ItemArray.GetValue(0).ToString();
                                lblNumero.Text = drv.Row.ItemArray.GetValue(1).ToString();
                                lblFecha.Text = drv.Row.ItemArray.GetValue(2).ToString();
                                if (drv.Row.ItemArray.GetValue(4).ToString().Trim().Length != 0)
                                {
                                    lblNitProveedor.Text = drv.Row.ItemArray.GetValue(3).ToString();
                                    lblNombreProveedor.Text = drv.Row.ItemArray.GetValue(4).ToString();
                                    lblCodSucursal.Text = drv.Row.ItemArray.GetValue(5).ToString();
                                    lblNombreSucursal.Text = drv.Row.ItemArray.GetValue(6).ToString();
                                    lblProveedor.Visible = true;
                                    lblNitProveedor.Visible = true;
                                    lblNombreProveedor.Visible = true;
                                    lblCodSucursal.Visible = true;
                                    lblNombreSucursal.Visible = true;
                                }
                                else
                                {
                                    lblProveedor.Visible = false;
                                    lblNitProveedor.Visible = false;
                                    lblNombreProveedor.Visible = false;
                                    lblCodSucursal.Visible = false;
                                    lblNombreSucursal.Visible = false;
                                }

                                chkAnulado.Checked = Convert.ToBoolean(drv.Row.ItemArray.GetValue(9).ToString());
                                chkCerrado.Checked = Convert.ToBoolean(drv.Row.ItemArray.GetValue(10).ToString());
                                chkAprobado.Checked = Convert.ToBoolean(drv.Row.ItemArray.GetValue(11).ToString());
                            }

                            gvDetalle.DataSource = transacciones.GetTransaccionesInventarioDetalle(tipo, numero, Convert.ToInt32(this.Session["empresa"]));
                            gvDetalle.DataBind();

                            gvImpuesto.DataSource = transacciones.GetTransaccionesInventarioImpuesto(tipo, numero, Convert.ToInt32(this.Session["empresa"]));
                            gvImpuesto.DataBind();
                            TotalizaGrillaReferencia();
                        }
                    }
                    catch (Exception ex)
                    {
                        ManejoErrorCatch(ex);
                    }

                }
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


                using (TransactionScope ts = new TransactionScope())
                {
                    foreach (GridViewRow registro in this.gvDetalle.Rows)
                    {
                        if (((TextBox)registro.Cells[0].FindControl("txtCantidad")).Text.Trim().Length != 0 &&
                            Convert.ToDecimal(((TextBox)registro.Cells[0].FindControl("txtCantidad")).Text.Trim()) != 0)
                        {
                            switch (solicitud.ApruebaOrden(lblTipo.Text.Trim(), lblNumero.Text, registro.Cells[1].Text,
                                Convert.ToDecimal(((TextBox)registro.FindControl("txtCantidad")).Text), this.Session["usuario"].ToString(), Convert.ToInt16(registro.Cells[9].Text), Convert.ToInt16(Session["empresa"])))
                            {
                                case 1:
                                    verificacion = false;
                                    break;
                            }
                        }
                    }

                    if (verificacion == false)
                    {
                        ManejoError("Error al actualizar el registro. Operación no realizada", "A");
                        return;
                    }

                    //transacciones.EnviaCorreo(this.lblTransaccion.Text.Trim());
                    ManejoExito("Orden de compra aprobada satisfactoriamente", "A");
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }

        }
        protected void btnEstudio_Click(object sender, EventArgs e)
        {
            string script = "";
            //       script = "<script language='javascript'>Visualizacion('" + this.lblNombreSolicitante.Text.Trim() + "');</script>";
            Page.RegisterStartupScript("Visualizacion", script);

        }



    }
}