using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Padministracion
{
    public partial class PlanoBancos : BasePage
    {
        #region Instancias



        CplanoBanco transaccionPlano = new CplanoBanco();


        CbancoPlano planoBancos = new CbancoPlano();

        #endregion Instancias

        #region Metodos



        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "C", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }
                this.gvLista.DataSource = planoBancos.BuscarEntidad(nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";
                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                    this.gvLista.Rows.Count.ToString() + " Registros encontrados", ObtenerIP(), Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la tabla correspondiente a: " + ex.Message, "C");
            }
        }


        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            this.gvListaDetalle.DataSource = null;
            this.gvListaDetalle.DataBind();
            gvListaDetalle.Visible = false;
            Session["transaccion"] = null;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion,
                ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex", mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }

        private void CargarCombos()
        {
            try
            {
                this.ddlBanco.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("gBanco", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"])); ;
                this.ddlBanco.DataValueField = "codigo";
                this.ddlBanco.DataTextField = "descripcion";
                this.ddlBanco.DataBind();
                this.ddlBanco.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar bancos. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlCampos.DataSource = planoBancos.SeleccionaCamposPlanoBanco();
                this.ddlCampos.DataValueField = "codigo";
                this.ddlCampos.DataTextField = "descripcion";
                this.ddlCampos.DataBind();
                this.ddlCampos.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar campos. Correspondiente a: " + ex.Message, "C");
            }
        }

        private void EntidadKey()
        {
            object[] objKey = new object[] { this.ddlBanco.SelectedValue, Convert.ToInt16(Session["empresa"]) };
            try
            {
                if (CentidadMetodos.EntidadGetKey("nPlanoBanco", "ppa", objKey).Tables[0].Rows.Count > 0)
                {

                    MostrarMensaje("El banco " + this.ddlBanco.SelectedItem.ToString() + " ya se encuentra registrado");
                    CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
                    this.nilbNuevo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al validar la llave primaria correspondiente a: " + ex.Message, "C");
            }
        }

        private void Guardar()
        {
            string operacion = "inserta", nombreCampo = "", valor = "";
            bool valida = false;

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                    {
                        object[] objValoDeleteNovedad = new object[] { ddlBanco.SelectedValue, Convert.ToInt16(Session["empresa"]) };
                        switch (CentidadMetodos.EntidadInsertUpdateDelete("nPlanoBancoDetalle", "elimina", "ppa", objValoDeleteNovedad))
                        {
                            case 1:
                                valida = true;
                                break;
                        }

                        foreach (GridViewRow dl in gvListaDetalle.Rows)
                        {
                            bool mValor = false;
                            foreach (Control c in dl.Cells[7].Controls)
                            {
                                if (c is CheckBox)
                                    mValor = ((CheckBox)c).Checked;
                            }
                            if (dl.Cells[8].Text == "&nbsp;" || dl.Cells[8].Text.Length == 0)
                                valor = null;
                            else
                                valor = dl.Cells[8].Text;
                            if (dl.Cells[3].Text == "&nbsp;" || dl.Cells[3].Text.Length == 0)
                                nombreCampo = null;
                            else
                                nombreCampo = dl.Cells[3].Text;

                            object[] objValoresDetalle = new object[]{
                                    ddlBanco.SelectedValue,//@banco
                                    null,// @campoF
                                    Convert.ToInt16(Session["empresa"]),
                                    Convert.ToDecimal(dl.Cells[5].Text),//@inicio
                                    Convert.ToDecimal(dl.Cells[6].Text),//@longitud
                                    false,// @mcampof
                                    mValor,//@mValorFijo
                                    nombreCampo, //@nombreCampo
                                    dl.RowIndex,
                                    Convert.ToDecimal(dl.Cells[4].Text),//@tipoCampo
                                    Convert.ToInt16(dl.Cells[9].Text),//@tipoRegistro
                                    valor
                                 };

                            switch (CentidadMetodos.EntidadInsertUpdateDelete("nPlanoBancoDetalle", "inserta", "ppa", objValoresDetalle))
                            {
                                case 1:
                                    valida = true;
                                    break;
                            }
                        }
                        if (valida == false)
                        {
                            ts.Complete();
                            ManejoExito("Datos registrados satisfactoriamente", "I");
                        }
                    }
                    else
                    {
                        object[] objValores = new object[]{
                                        ddlBanco.SelectedValue,
                                        Convert.ToInt16(Session["empresa"]),
                                        DateTime.Now,
                                          Session["usuario"].ToString()     };

                        switch (CentidadMetodos.EntidadInsertUpdateDelete("nPlanoBanco", operacion, "ppa", objValores))
                        {
                            case 0:
                                foreach (GridViewRow dl in gvListaDetalle.Rows)
                                {
                                    bool mValor = false;
                                    foreach (Control c in dl.Cells[7].Controls)
                                    {
                                        if (c is CheckBox)
                                            mValor = ((CheckBox)c).Checked;
                                    }

                                    if (dl.Cells[8].Text == "&nbsp;" || dl.Cells[8].Text.Length == 0)
                                        valor = null;
                                    else
                                        valor = dl.Cells[8].Text;
                                    if (dl.Cells[3].Text == "&nbsp;" || dl.Cells[3].Text.Length == 0)
                                        nombreCampo = null;
                                    else
                                        nombreCampo = dl.Cells[3].Text;

                                    object[] objValoresDetalle = new object[]{
                                    ddlBanco.SelectedValue,
                                      null,// @campoF
                                    Convert.ToInt16(Session["empresa"]),
                                    Convert.ToDecimal(dl.Cells[5].Text),
                                    Convert.ToDecimal(dl.Cells[6].Text),
                                    false,// @mcampof
                                    mValor,
                                    nombreCampo,
                                     dl.RowIndex,
                                    Convert.ToDecimal(dl.Cells[4].Text),
                                    Convert.ToInt16(dl.Cells[9].Text),//@tipoRegistro
                                    valor
                                 };

                                    switch (CentidadMetodos.EntidadInsertUpdateDelete("nPlanoBancoDetalle", operacion, "ppa", objValoresDetalle))
                                    {
                                        case 1:
                                            valida = true;
                                            break;
                                    }
                                }
                                break;
                            case 1:
                                valida = true;
                                break;
                        }

                    }

                    if (valida == false)
                    {
                        ts.Complete();
                        ManejoExito("Datos registrados satisfactoriamente", "I");
                    }
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar los datos correspondiente a: " + ex.Message, operacion.Substring(0, 1).ToUpper());
            }
        }

        #endregion Metodos


        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) != 0)
                    this.ddlBanco.Focus();
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
        }
        protected void lbNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                  nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            Session["editarDetalle"] = false;
            txtValorFijo.Enabled = false;
            CargarCombos();
            this.nilblInformacion.Text = "";
            txvInicio.Enabled = false;
            LimpiarDatos();
        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.gvListaDetalle.DataSource = null;
            this.gvListaDetalle.DataBind();
            Session["transaccion"] = null;
            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Text = "";
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                object[] objValores = new object[] { Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text), Convert.ToInt16(Session["empresa"]) };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("nPlanoBancoDetalle", "elimina", "ppa", objValores))
                {
                    case 0:
                        if (CentidadMetodos.EntidadInsertUpdateDelete("nPlanoBanco", "elimina", "ppa", objValores) == 0)
                            ManejoExito("Registro eliminado satisfactoriamente", "E");
                        break;
                    case 1:
                        ManejoError("Errores al eliminar el registro. Operación no realizada", "E");
                        break;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
            }

        }

        private List<CplanoBanco> reasignarRegistros(List<CplanoBanco> listaNovedadesTransaccion)
        {
            int z = 0;
            foreach (CplanoBanco ln in listaNovedadesTransaccion)
            {
                ln.Registro = z;
                z++;
            }
            return listaNovedadesTransaccion;
        }


        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (ddlBanco.SelectedValue.Length == 0)
            {
                MostrarMensaje("Campos vacios por favor corrija");
                return;
            }

            if (gvListaDetalle.Rows.Count <= 0)
            {
                CcontrolesUsuario.MensajeError("El detalle de la transacción debe tener por lo menos un registro", nilblInformacion);
                return;
            }
            Guardar();
        }

        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
              nombrePaginaActual(), "A", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "A");
                return;
            }
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;
            this.ddlBanco.Enabled = false;
            this.ddlCampos.Focus();
            CargarCombos();
            try
            {
                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.ddlBanco.SelectedValue = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[2].Text);

                CargarDetallePlano();

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }

        private void CargarDetallePlano()
        {
            List<CplanoBanco> listaDetalle = new List<CplanoBanco>();
            CplanoBanco novedadTran;
            DataView dvNovedad = planoBancos.SeleccionaPlanoDetalleBanco(Convert.ToInt16(Session["empresa"]), ddlBanco.SelectedValue.ToString());

            foreach (DataRowView registro in dvNovedad)
            {
                string campo = "", valor = "";
                decimal inicio = 0, longitud = 0;
                bool mValor = false;
                int numero = 0, tipo = 0, tipoRegistro = 0;

                if (!(registro.Row.ItemArray.GetValue(2) is DBNull))
                    numero = Convert.ToInt16(registro.Row.ItemArray.GetValue(2));

                if (!(registro.Row.ItemArray.GetValue(3) is DBNull))
                    tipoRegistro = Convert.ToInt16(registro.Row.ItemArray.GetValue(3));


                if (!(registro.Row.ItemArray.GetValue(4) is DBNull) && registro.Row.ItemArray.GetValue(4).ToString() != "&nbsp;")
                    campo = registro.Row.ItemArray.GetValue(4).ToString();
                else
                    campo = "";

                if (!(registro.Row.ItemArray.GetValue(5) is DBNull))
                    inicio = Convert.ToInt16(registro.Row.ItemArray.GetValue(5));

                if (!(registro.Row.ItemArray.GetValue(6) is DBNull))
                    longitud = Convert.ToInt16(registro.Row.ItemArray.GetValue(6));

                mValor = Convert.ToBoolean(registro.Row.ItemArray.GetValue(7));

                if (!(registro.Row.ItemArray.GetValue(8) is DBNull) && registro.Row.ItemArray.GetValue(8).ToString() != "&nbsp;")
                    valor = registro.Row.ItemArray.GetValue(8).ToString();
                else
                    valor = "";

                if (!(registro.Row.ItemArray.GetValue(9) is DBNull))
                    tipo = Convert.ToInt16(registro.Row.ItemArray.GetValue(9));

                novedadTran = new CplanoBanco(numero, campo, tipo, inicio, longitud, mValor, valor, tipoRegistro);
                listaDetalle.Add(novedadTran);
            }
            gvListaDetalle.DataSource = listaDetalle;
            gvListaDetalle.DataBind();
            gvListaDetalle.Visible = true;
            this.Session["transaccion"] = listaDetalle;
            LimpiarDatos();
        }

        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }


        protected void gvLista_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
        protected void ddlBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntidadKey();
        }
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {

            if (gvListaDetalle.Rows.Count == 0)
            {
                txvInicio.Text = "1";
            }
            else
            {
                int fila = gvListaDetalle.Rows.Count - 1;
                txvInicio.Text = Convert.ToString(Convert.ToInt16(gvListaDetalle.Rows[fila].Cells[5].Text) + Convert.ToInt16(gvListaDetalle.Rows[fila].Cells[6].Text));
            }

            if (Convert.ToDecimal(this.txvLongitud.Text) <= 0)
            {
                nilblInformacion.Text = "El valores igual o menor que cero. Por favor corrija";
                return;
            }

            if (chkValorFijo.Checked)
            {
                if (txtValorFijo.Text.Length == 0)
                {
                    nilblInformacion.Text = "Valores en blancos por favor corrija";
                    return;
                }
            }

            if (Convert.ToBoolean(Session["editarDetalle"]) == false)
                hdRegistro.Value = Convert.ToString(gvListaDetalle.Rows.Count);

            transaccionPlano = new CplanoBanco(Convert.ToInt16(this.hdRegistro.Value), ddlCampos.SelectedValue.Trim(), Convert.ToInt16(ddlTipoDatos.SelectedValue),
                Convert.ToDecimal(txvInicio.Text), Convert.ToDecimal(txvLongitud.Text), chkValorFijo.Checked, txtValorFijo.Text.Trim(), Convert.ToInt32(ddlTipoRegistro.SelectedValue));
            List<CplanoBanco> listaTransaccion = null;
            if (this.Session["transaccion"] == null)
            {
                listaTransaccion = new List<CplanoBanco>();
                listaTransaccion.Add(transaccionPlano);
            }
            else
            {
                listaTransaccion = (List<CplanoBanco>)Session["transaccion"];
                listaTransaccion.Add(transaccionPlano);
            }

            listaTransaccion = listaTransaccion.OrderBy(p => p.Registro).ToList();
            this.Session["transaccion"] = listaTransaccion;
            gvListaDetalle.Visible = true;
            Session["editarDetalle"] = false;
            this.gvListaDetalle.DataSource = listaTransaccion;
            this.gvListaDetalle.DataBind();
            LimpiarDatos();
        }

        private void LimpiarDatos()
        {
            if (gvListaDetalle.Rows.Count == 0)
                txvInicio.Text = "1";
            else
            {
                int fila = gvListaDetalle.Rows.Count - 1;
                txvInicio.Text = Convert.ToString(Convert.ToInt16(gvListaDetalle.Rows[fila].Cells[5].Text) + Convert.ToInt16(gvListaDetalle.Rows[fila].Cells[6].Text));
            }

            txvInicio.Enabled = false;
            txvLongitud.Text = "0";
            ddlCampos.SelectedValue = "";
            chkValorFijo.Checked = false;
            txtValorFijo.Text = "";
            ManejoValorFijo();
        }

        protected void chkValorFijo_CheckedChanged(object sender, EventArgs e)
        {
            ManejoValorFijo();
        }

        private void ManejoValorFijo()
        {
            if (chkValorFijo.Checked)
            {
                ddlCampos.SelectedValue = "";
                ddlCampos.Enabled = false;
                txtValorFijo.Enabled = true;
            }
            else
            {
                txtValorFijo.Enabled = false;
                ddlCampos.Enabled = true;
                txtValorFijo.Text = "";
            }
        }

        protected void gvListaDetalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["editarDetalle"] = true;

                this.nilblInformacion.Text = "";
                this.hdRegistro.Value = this.gvListaDetalle.SelectedRow.Cells[2].Text;

                if (this.gvListaDetalle.SelectedRow.Cells[3].Text != "&nbsp;")
                    this.ddlCampos.SelectedValue = this.gvListaDetalle.SelectedRow.Cells[3].Text;

                if (this.gvListaDetalle.SelectedRow.Cells[4].Text != "&nbsp;")
                    this.ddlTipoDatos.SelectedValue = this.gvListaDetalle.SelectedRow.Cells[4].Text;

                if (this.gvListaDetalle.SelectedRow.Cells[5].Text != "&nbsp;")
                    txvInicio.Text = this.gvListaDetalle.SelectedRow.Cells[5].Text;
                else
                    txvInicio.Text = "0";

                if (this.gvListaDetalle.SelectedRow.Cells[6].Text != "&nbsp;")
                    txvLongitud.Text = this.gvListaDetalle.SelectedRow.Cells[6].Text;
                else
                    txvLongitud.Text = "0";

                foreach (Control c in this.gvListaDetalle.SelectedRow.Cells[7].Controls)
                {
                    if (c is CheckBox)
                        chkValorFijo.Checked = ((CheckBox)c).Checked;
                }

                if (this.gvListaDetalle.SelectedRow.Cells[8].Text != "&nbsp;")
                    txtValorFijo.Text = this.gvListaDetalle.SelectedRow.Cells[8].Text;
                else
                    txtValorFijo.Text = "";
                List<CplanoBanco> listaTransaccion = null;
                listaTransaccion = (List<CplanoBanco>)this.Session["transaccion"];
                listaTransaccion.RemoveAt(this.gvListaDetalle.SelectedRow.RowIndex);
                this.gvListaDetalle.DataSource = listaTransaccion;
                this.gvListaDetalle.DataBind();

            }
            catch (Exception ex)
            {
                CcontrolesUsuario.MensajeError("Error al cargar los campos del registro en el formulario. Correspondiente a: " + ex.Message, this.nilblInformacion);
            }
        }
        protected void gvListaDetalle_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.nilblInformacion.Text = "";
            try
            {
                List<CplanoBanco> listaTransaccion = null;
                listaTransaccion = (List<CplanoBanco>)Session["transaccion"];
                listaTransaccion.RemoveAt(e.RowIndex);

                listaTransaccion = reasignarRegistros(listaTransaccion);

                this.gvListaDetalle.DataSource = listaTransaccion;
                this.gvListaDetalle.DataBind();
            }
            catch (Exception ex)
            {
                CcontrolesUsuario.MensajeError("Error al eliminar el registro. Correspondiente a: " + ex.Message, nilblInformacion);
            }
        }


    }
}