using Contabilidad.WebForms.App_Code.Administracion;
using Contabilidad.WebForms.App_Code.Presupuesto;
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

namespace Contabilidad.WebForms.App_Code.General
{
    public partial class TransaccionPresupuesto : BasePage
    {

        #region Instancias

        CtipoTransaccion tipoTransaccion = new CtipoTransaccion();
        Cperiodos periodo = new Cperiodos();
        Ctransacciones transacciones = new Ctransacciones();
        Ctransaccion ctransaccion = new Ctransaccion();
        Ccaracteristica caracteristica = new Ccaracteristica();
        Coperadores coperadores = new Coperadores();
        Cpresupuesto cpresupuesto = new Cpresupuesto();
        #endregion Instancias


        #region Metodos


        private void EstadoInicialGrillaTransacciones()
        {
            for (int i = 0; i < this.gvTransaccion.Columns.Count; i++)
            {
                this.gvTransaccion.Columns[i].Visible = true;
            }

            foreach (GridViewRow registro in this.gvTransaccion.Rows)
            {
                this.gvTransaccion.Rows[registro.RowIndex].Visible = true;
            }
        }



        private void TabRegistro()
        {
            this.fsRegistro.Visible = true;
            this.fsConsulta.Visible = false;
            this.tsHora.Visible = false;

            if (Convert.ToBoolean(this.Session["editar"]) == true)
            {
                this.fsCabeza.Visible = true;

            }

            this.niimbRegistro.BorderStyle = BorderStyle.None;
            this.niimbConsulta.BorderStyle = BorderStyle.Solid;
            this.niimbConsulta.BorderColor = System.Drawing.Color.White;
            this.niimbConsulta.BorderWidth = Unit.Pixel(1);
            this.niimbConsulta.Enabled = true;
            this.niimbRegistro.Enabled = false;
            this.gvTransaccion.DataSource = null;
            this.gvTransaccion.DataBind();
            this.nilblRegistros.Text = "Nro. Registros 0";
            this.nilblMensajeEdicion.Text = "";
            this.lblCalcular.Visible = false;
            gvParametros.DataSource = null;
            gvParametros.DataBind();
        }


        private void ManejoExito(string mensaje, string operacion)
        {

            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            CcontrolesUsuario.InhabilitarControles(fsGeneral.Controls);
            CcontrolesUsuario.LimpiarControles(fsGeneral.Controls);
            CcontrolesUsuario.InhabilitarControlesFS(fsCabeza.Controls);
            CcontrolesUsuario.LimpiarControlesFS(fsCabeza.Controls);
            CcontrolesUsuario.InhabilitarControlesFS(fsRegistro.Controls);
            CcontrolesUsuario.LimpiarControlesFS(fsRegistro.Controls);
            CcontrolesUsuario.InhabilitarControlesFS(fsConsulta.Controls);
            CcontrolesUsuario.LimpiarControlesFS(fsConsulta.Controls);
            nilbNuevo.Visible = true;
            seguridad.InsertaLog(Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + Page.ToString(),
                "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
        }


        private void ManejoEncabezado()
        {
            HabilitaEncabezado();
            CargarCombos();
            CargarTipoTransaccion();
            manejoEncabezadoT(true);
        }

        private void CargarTipoTransaccion()
        {
            try
            {
                this.ddlTipoDocumento.DataSource = ctransaccion.SeleccionaTipoTransaccionParametros(Convert.ToInt16(this.Session["empresa"]));
                this.ddlTipoDocumento.DataValueField = "codigo";
                this.ddlTipoDocumento.DataTextField = "descripcion";
                this.ddlTipoDocumento.DataBind();
                this.ddlTipoDocumento.Items.Insert(0, new ListItem("", ""));
                this.ddlTipoDocumento.SelectedValue = "";
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        private void CargaCampos()
        {
            try
            {
                this.niddlCampo.DataSource = transacciones.GetCamposEntidades("lTransaccion", "");
                this.niddlCampo.DataValueField = "name";
                this.niddlCampo.DataTextField = "name";
                this.niddlCampo.DataBind();
                this.niddlCampo.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        private void CargarCombos()
        {
            try
            {
                this.ddlFormulacion.DataSource = ctransaccion.seleccionaItemsTipoTransaccion(empresa: Convert.ToInt16(this.Session["empresa"]),
                    tipotransaccion: ddlTipoDocumento.SelectedValue
                    );
                this.ddlFormulacion.DataValueField = "codigo";
                this.ddlFormulacion.DataTextField = "descripcion";
                this.ddlFormulacion.DataBind();
                this.ddlFormulacion.Items.Insert(0, new ListItem("", ""));
                this.ddlFormulacion.ClearSelection();

                DataView año = cpresupuesto.SeleccionaAñosAbiertos(Convert.ToInt16(this.Session["empresa"]));
                ddlAÑo.DataSource = año;
                ddlAÑo.DataValueField = "año";
                ddlAÑo.DataTextField = "año";
                ddlAÑo.DataBind();
                ddlAÑo.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }

        }

        private object TipoTransaccionConfig(int posicion)
        {
            object retorno = null;
            string cadena;
            char[] comodin = new char[] { '*' };
            int indice = posicion + 1;

            try
            {
                cadena = tipoTransaccion.TipoTransaccionConfig(ddlTipoDocumento.SelectedValue, Convert.ToInt16(Session["empresa"])).ToString();
                retorno = cadena.Split(comodin, indice).GetValue(posicion - 1);

                return retorno;
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);

                return null;
            }
        }

        private string ConsecutivoTransaccion()
        {
            string numero = "";

            try
            {
                numero = transacciones.RetornaNumeroTransaccion(
                    Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(this.Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }

            return numero;
        }

        private void Guardar()
        {
            bool verificacion = false;
            string numerotransaccion = "";
            string operacion = "inserta";


            try
            {
                string hora = "0";
                string minuto = "0";

                if (Convert.ToBoolean(this.Session["editar"]) == true)
                {
                    operacion = "actualiza";
                    numerotransaccion = txtNumero.Text;
                    this.Session["numerotransaccion"] = numerotransaccion;
                }
                else
                {
                    numerotransaccion = transacciones.RetornaNumeroTransaccion(ddlTipoDocumento.SelectedValue, Convert.ToInt16(this.Session["empresa"]));
                    this.Session["numerotransaccion"] = numerotransaccion;
                }

                using (TransactionScope ts = new TransactionScope())
                {


                    object[] objValores = new object[]{
                                          ddlAÑo.SelectedValue.Trim(),  // @año    int
                                          false,   //@anulado    bit
                                          Convert.ToInt32(this.Session["empresa"]),  //@empresa    int
                                           null, //@fechaAnulado   datetime
                                           DateTime.Now,  //@fechaRegistro  datetime
                                           numerotransaccion, //@numero varchar
                                           null,  //@Observacion    varchar
                                           ddlFormulacion.SelectedValue, //@producto   varchar
                                           ddlTipoDocumento.SelectedValue.Trim(),  //@tipo   varchar
                                           this.Session["usuario"].ToString(), //@usuario    varchar
                                           null //@usuarioAnulado varchar
                                                    };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("cPresupuesto", operacion, "ppa", objValores))
                    {
                        case 0:

                            if (Convert.ToBoolean(this.Session["editar"]))
                            {
                                object[] objvaloresdetalles = new object[]
                                             {
                                                 Convert.ToInt16(this.Session["empresa"]) ,  //@empresa    int
                                                 numerotransaccion,   //@numero varchar
                                                 ddlTipoDocumento.SelectedValue.Trim(),   //@tipo   varchar
                                             };
                                switch (CentidadMetodos.EntidadInsertUpdateDelete("cPrespuestoDetalle", "elimina", "ppa", objvaloresdetalles))
                                {
                                    case 1:
                                        verificacion = true;
                                        break;
                                }
                            }

                            foreach (GridViewRow gvr in gvLista.Rows)
                            {

                                if (gvr.RowIndex > 0)
                                {
                                    var dtAnalisis = gvr.FindControl("dtAnalisis") as DataList;
                                    var hfvariable = gvr.FindControl("hfVariable") as HiddenField;
                                    var hfPeriocidad = gvr.FindControl("hfPeriocidad") as HiddenField;

                                    foreach (DataListItem dli in dtAnalisis.Items)
                                    {
                                        var txvValorAnalisis = dli.FindControl("txvValorAnalisis") as TextBox;
                                        var hfMes = dli.FindControl("hfMes") as HiddenField;

                                        object[] objvaloresdetalles = new object[]
                                             {
                                                 ddlAÑo.SelectedValue.Trim(),   // @año   int
                                                 Convert.ToInt16(this.Session["empresa"]) ,  //@empresa    int
                                                 ddlFormulacion.SelectedValue.Trim(), //@formulacion
                                                 Convert.ToInt16(hfMes.Value),   //@mes    int
                                                Convert.ToInt16(hfvariable.Value),   //@movimiento int
                                                 numerotransaccion,   //@numero varchar
                                                hfPeriocidad.Value,  //@periocodad
                                                 ddlTipoDocumento.SelectedValue.Trim(),   //@tipo   varchar
                                                 Convert.ToDecimal(txvValorAnalisis.Text)   //@valor  float
                                             };
                                        switch (CentidadMetodos.EntidadInsertUpdateDelete("cPrespuestoDetalle", "inserta", "ppa", objvaloresdetalles))
                                        {
                                            case 1:
                                                verificacion = true;
                                                break;
                                        }
                                    }
                                }
                            }

                            break;

                        case 1:
                            verificacion = true;
                            break;
                    }


                    if (verificacion == true)
                    {
                        ManejoError("Error al insertar el detalle de la transacción. Operación no realizada", "I");
                        return;
                    }

                    if (Convert.ToBoolean(this.Session["editar"]) != true)
                    {
                        transacciones.ActualizaConsecutivo(ddlTipoDocumento.SelectedValue, (int)this.Session["empresa"]);
                    }
                    ManejoExito("Transacción registrada satisfactoriamente número " + Session["numerotransaccion"].ToString(), "I");
                    ts.Complete();
                    this.gvLista.DataSource = null;
                    this.gvLista.DataBind();
                    this.lbRegistrar.Visible = false;
                    this.lblCalcular.Visible = false;

                }

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }

        }

        private void HabilitaEncabezado()
        {
            this.lbCancelar.Visible = true;
            this.nilbNuevo.Visible = false;
            this.lblTipoDocumento.Visible = true;
            this.ddlTipoDocumento.Visible = true;
            this.ddlTipoDocumento.Enabled = true;
            this.lblNumero.Visible = true;
            this.txtNumero.Visible = true;
            this.txtNumero.Text = "";
            this.ddlTipoDocumento.Focus();
            this.lbRegistrar.Visible = true;
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();

            this.gvTransaccion.DataSource = null;
            this.gvTransaccion.DataBind();
            this.Session["transaccion"] = null;
            this.lblCalcular.Visible = false;


        }

        private void InHabilitaEncabezado()
        {
            this.lbCancelar.Visible = false;
            this.nilbNuevo.Visible = true;
            this.lblTipoDocumento.Visible = false;
            this.ddlTipoDocumento.Visible = false;
            this.lblNumero.Visible = false;
            this.txtNumero.Visible = false;
            this.txtNumero.Text = "";
            this.nilbNuevo.Focus();
        }

        private void BusquedaTransaccion()
        {
            try
            {
                if (this.gvParametros.Rows.Count > 0)
                {
                    string where = coperadores.FormatoWhere(
                        (List<Coperadores>)Session["operadores"]);

                    this.gvTransaccion.DataSource = transacciones.GetTransaccionCompleta(where, Convert.ToInt16(Session["empresa"]));
                    this.gvTransaccion.DataBind();

                    this.nilblRegistros.Text = "Nro. Registros " + Convert.ToString(this.gvTransaccion.Rows.Count);

                    EstadoInicialGrillaTransacciones();
                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        private int CompruebaTransaccionExistente()
        {
            try
            {
                object[] objkey = new object[]{
                (int)this.Session["empresa"],
                this.txtNumero.Text,
                 Convert.ToString(this.ddlTipoDocumento.SelectedValue)
                  };

                if (CentidadMetodos.EntidadGetKey(
                    "lTransaccion",
                    "ppa",
                    objkey).Tables[0].DefaultView.Count > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
                return 1;
            }
        }

        private void ComportamientoConsecutivo()
        {
            if (this.txtNumero.Text.Length == 0)
            {
                this.txtNumero.Enabled = true;
                this.txtNumero.ReadOnly = false;
                this.txtNumero.Focus();
            }
            else
            {


                this.txtNumero.Enabled = false;

                CcontrolesUsuario.HabilitarControles(
                    this.fsCabeza.Controls);

                this.lblCalcular.Visible = false;
                this.nilbNuevo.Visible = false;

            }
        }

        #endregion Metodos

        #region Evento
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
            {
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            }
            else
            {
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(),
                   ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) != 0)
                {

                    if (!IsPostBack)
                    {
                        CargarCombos();
                        CargaCampos();
                        TabRegistro();
                        this.Session["transaccion"] = null;
                        this.Session["operadores"] = null;
                    }
                }
                else
                {
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
                }
            }

        }
        protected void nilbNuevo_Click(object sender, EventArgs e)
        {
            this.Session["editar"] = false;
            ManejoEncabezado();

        }


        #endregion Evento

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            cancelar();

        }

        private void cancelar()
        {
            InHabilitaEncabezado();

            CcontrolesUsuario.InhabilitarControlesFS(
                this.fsCabeza.Controls);
            CcontrolesUsuario.LimpiarControlesFS(
                this.fsCabeza.Controls);

            this.Session["transaccion"] = null;
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.lbRegistrar.Visible = false;
            this.lbCancelar.Visible = false;
            this.lblCalcular.Visible = false;
            fsCabeza.Visible = false;
            manejoEncabezadoT(true);
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            Calcular();
            Guardar();
        }


        protected void niimbBuscar_Click(object sender, EventArgs e)
        {

        }
        protected void niimbRegistro_Click(object sender, EventArgs e)
        {
            manejoConsulta();
            TabRegistro();
        }
        protected void niimbConsulta_Click(object sender, EventArgs e)
        {
            TabRegistro();
            manejoConsulta();

        }

        private void manejoConsulta()
        {
            this.fsRegistro.Visible = false;
            this.fsCabeza.Visible = false;
            this.fsConsulta.Visible = true;

            this.niimbConsulta.BorderStyle = BorderStyle.None;
            this.niimbRegistro.BorderStyle = BorderStyle.Solid;
            this.niimbRegistro.BorderColor = System.Drawing.Color.White;
            this.niimbRegistro.BorderWidth = Unit.Pixel(1);
            this.niimbConsulta.Enabled = false;
            this.niimbRegistro.Enabled = true;

            this.Session["operadores"] = null;
            this.Session["transaccion"] = null;
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.gvParametros.DataSource = null;
            this.gvParametros.DataBind();

            this.lblCalcular.Visible = false;
        }

        private void ComportamientoTransaccion()
        {
            fsCabeza.Visible = true;
            //CcontrolesUsuario.ComportamientoCampoEntidad(this.fsCabeza.Controls, "lTransaccion", Convert.ToString(this.ddlTipoDocumento.SelectedValue), (int)this.Session["empresa"]);
        }


        protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarotroscampos();

        }

        private void cargarotroscampos()
        {
            try
            {
                CcontrolesUsuario.HabilitarControlesFS(fsCabeza.Controls);
                CcontrolesUsuario.LimpiarControlesFS(fsCabeza.Controls);
                this.gvLista.DataSource = null;
                this.gvLista.DataBind();
                this.Session["transaccion"] = null;
                this.txtNumero.Text = ConsecutivoTransaccion();
                this.lblCalcular.Visible = false;
                CargarCombos();
                ComportamientoConsecutivo();
                ComportamientoTransaccion();
                if (ddlAÑo.SelectedValue.Trim().Length > 0)
                {

                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        protected void txtNumero_TextChanged(object sender, EventArgs e)
        {

        }


        protected void gvParametros_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<Coperadores> listaOperadores = null;

            try
            {
                listaOperadores = (List<Coperadores>)Session["operadores"];
                listaOperadores.RemoveAt(e.RowIndex);

                this.gvParametros.DataSource = listaOperadores;
                this.gvParametros.DataBind();
                this.gvTransaccion.DataSource = null;
                this.gvTransaccion.DataBind();
                this.nilblRegistros.Text = "Nro. registros 0";
                this.nilblMensajeEdicion.Text = "";

                if (this.gvParametros.Rows.Count == 0)
                {
                    this.imbBusqueda.Visible = false;
                }

                EstadoInicialGrillaTransacciones();
            }
            catch
            {
            }

        }
        protected void niddlOperador_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.niddlOperador.SelectedValue.ToString() == "between")
            {
                this.nitxtValor2.Visible = true;
            }
            else
            {
                this.nitxtValor2.Visible = false;
                this.nitxtValor1.Text = "";
            }

            this.nitxtValor1.Focus();
        }
        protected void niimbAdicionar_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow registro in this.gvParametros.Rows)
            {
                if (Convert.ToString(this.niddlCampo.SelectedValue) == registro.Cells[1].Text &&
                    Convert.ToString(this.niddlOperador.SelectedValue) == Server.HtmlDecode(registro.Cells[2].Text) &&
                    this.nitxtValor1.Text == registro.Cells[3].Text)
                {
                    return;
                }
            }

            coperadores = new Coperadores(
                Convert.ToString(this.niddlCampo.SelectedValue),
                Server.HtmlDecode(Convert.ToString(this.niddlOperador.SelectedValue)),
                nitxtValor1.Text,
                nitxtValor2.Text);

            List<Coperadores> listaOperadores = null;

            if (this.Session["operadores"] == null)
            {
                listaOperadores = new List<Coperadores>();
                listaOperadores.Add(coperadores);
            }
            else
            {
                listaOperadores = (List<Coperadores>)Session["operadores"];
                listaOperadores.Add(coperadores);
            }

            this.Session["operadores"] = listaOperadores;

            this.imbBusqueda.Visible = true;
            this.gvParametros.DataSource = listaOperadores;
            this.gvParametros.DataBind();
            this.gvTransaccion.DataSource = null;
            this.gvTransaccion.DataBind();
            this.nilblRegistros.Text = "Nro. Registros 0";
            this.nilblMensajeEdicion.Text = "";

            EstadoInicialGrillaTransacciones();
        }

        protected void imbBusqueda_Click(object sender, EventArgs e)
        {
            this.nilblMensajeEdicion.Text = "";

            BusquedaTransaccion();
        }
        protected void gvTransaccion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.nilblMensajeEdicion.Text = "";

            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    bool anulado = false;

                    foreach (Control objControl in gvTransaccion.Rows[e.RowIndex].Cells[8].Controls)
                        anulado = ((CheckBox)objControl).Checked;

                    if (anulado == true)
                    {
                        this.nilblMensajeEdicion.Text = "Registro anulado no es posible su edición";
                        return;
                    }

                    if (tipoTransaccion.RetornaTipoBorrado(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, Convert.ToInt16(Session["empresa"])) == "E")
                    {
                        object[] objValores = new object[]{
                         Convert.ToInt16(Session["empresa"]),
                        Convert.ToString(this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text).Trim(),
                        Convert.ToString(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text).Trim()
                    };

                        switch (CentidadMetodos.EntidadInsertUpdateDelete("lTransaccionDetalle", "elimina", "ppa", objValores))
                        {
                            case 0:

                                switch (CentidadMetodos.EntidadInsertUpdateDelete("lTransaccion", "elimina", "ppa", objValores))
                                {
                                    case 0:
                                        MostrarMensaje("Registro eliminado Satisfactoriamente");
                                        BusquedaTransaccion();
                                        ts.Complete();
                                        break;

                                    case 1:

                                        ManejoError("Error al eliminar la transacción. Operación no realizada", eliminar);
                                        break;
                                }
                                break;

                            case 1:

                                ManejoError("Error al eliminar la transacción. Operación no realizada", eliminar);
                                break;
                        }
                    }
                    else
                    {
                        switch (transacciones.AnulaTransaccion(
                            this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text,
                            this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text,
                            this.Session["usuario"].ToString().Trim(),
                            Convert.ToInt16(Session["empresa"])
                            ))
                        {
                            case 0:

                                MostrarMensaje("Registro Anulado Satisfactoriamente");
                                BusquedaTransaccion();
                                ts.Complete();
                                break;
                            case 1:
                                ManejoError("Error al anular la transacción. Operación no realizada", eliminar);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ManejoErrorCatch(ex);
                }
            }
        }


        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrid();
        }


        private void CargarGrid()
        {
            this.lbRegistrar.Visible = false;
            this.lblCalcular.Visible = true;
            string varObj = "";
            string hora = "";// tsHora.Hour.ToString();
            string minuto = "";// tsHora.Minute.ToString();

            try
            {
                gvLista.DataSource = null;
                gvLista.DataBind();

                //     CultureInfo es = new CultureInfo("es-ES");

                //   DateTime fecha = Convert.ToDateTime(txtFecha.Text, es);

                this.gvLista.DataSource = null;
                //tipoTransaccion.SeleccionaFormulacion(Convert.ToInt16(Session["empresa"]), ddlTipoDocumento.SelectedValue.Trim(), ddlFormulacion.SelectedValue.Trim());
                this.gvLista.DataBind();

                DataView formulacioD = null;
                //tipoTransaccion.SeleccionaFormulacionDetalle(Convert.ToInt16(Session["empresa"]), ddlTipoDocumento.SelectedValue.Trim(), ConfigurationManager.AppSettings["modulo"].ToString(), ddlFormulacion.SelectedValue.Trim());

                foreach (GridViewRow registro in gvLista.Rows)
                {
                    ((DataList)registro.FindControl("dtAnalisis")).DataSource = formulacioD;
                    ((DataList)registro.FindControl("dtAnalisis")).DataBind();
                }

                foreach (GridViewRow registro in gvLista.Rows)
                {
                    foreach (DataListItem dli in ((DataList)registro.FindControl("dtAnalisis")).Items)
                    {
                        foreach (DataRowView drv in formulacioD)
                        {

                            if (registro.RowIndex > 0)
                            {
                                ((Label)dli.FindControl("lblIdAnalisis")).Visible = false;
                                ((Label)dli.FindControl("lblAnalisis")).Visible = false;

                            }
                            else
                            {
                                ((Label)dli.FindControl("lblIdAnalisis")).Visible = false;
                                ((Label)dli.FindControl("lblAnalisis")).Visible = true;
                                ((TextBox)dli.FindControl("txvValorAnalisis")).Visible = false;
                                ((CheckBox)registro.FindControl("chkVacio")).Visible = false;

                                for (int y = 0; y < 6; y++)
                                {
                                    registro.Cells[y].BackColor = Color.White;
                                    registro.Cells[y].BorderColor = Color.White;
                                    registro.Cells[y].BorderStyle = BorderStyle.None;

                                }
                            }
                            if (drv.Row.ItemArray.GetValue(0).ToString().Trim() == ((Label)dli.FindControl("lblIdAnalisis")).Text)
                            {
                                ((TextBox)dli.FindControl("txvValorAnalisis")).Enabled = !Convert.ToBoolean(drv.Row.ItemArray.GetValue(2).ToString().Trim());
                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        private bool Calcular()
        {
            this.lbRegistrar.Visible = false;
            string varObj = "";

            try
            {
                this.gvLista.Visible = true;
                List<cResultado> listaEjecutar = new List<cResultado>();

                cResultado cResultado = null;

                foreach (DataRowView registro in transacciones.GetMovimientoResultadoFormulacion(ddlFormulacion.SelectedValue, Convert.ToInt16(Session["empresa"]), ConfigurationManager.AppSettings["Modulo"].ToString(), ddlTipoDocumento.SelectedValue.Trim()))
                {
                    cResultado = new cResultado()
                    {
                        Variable = registro.Row.ItemArray.GetValue(0).ToString(),
                        Periocidad = registro.Row.ItemArray.GetValue(3).ToString(),
                        Tipotransaccion = ddlTipoDocumento.SelectedValue.Trim(),
                    };
                    listaEjecutar.Add(cResultado);
                }

                for (int w = 1; w <= 12; w++)
                {
                    varObj = "";
                    foreach (GridViewRow gvr in gvLista.Rows)
                    {
                        if (gvr.RowIndex > 0)
                        {
                            var hfvariable = gvr.FindControl("hfVariable") as HiddenField;
                            var dtAnalisis = gvr.FindControl("dtAnalisis") as DataList;
                            var hfPeriocidad = gvr.FindControl("hfPeriocidad") as HiddenField;

                            foreach (DataListItem dli in dtAnalisis.Items)
                            {
                                var hfMes = dli.FindControl("hfMes") as HiddenField;
                                var txvValorAnalisis = dli.FindControl("txvValorAnalisis") as TextBox;

                                if (w == Convert.ToInt16(hfMes.Value))
                                {
                                    varObj = varObj + "|" + hfvariable.Value + "(" + txvValorAnalisis.Text.Replace(",", "") + ")|";
                                }
                            }
                        }
                    }

                    for (int x = 0; x < listaEjecutar.Count; x++)
                    {
                        varObj = "";
                        foreach (GridViewRow gvr in gvLista.Rows)
                        {
                            if (gvr.RowIndex > 0)
                            {
                                var hfvariable = gvr.FindControl("hfVariable") as HiddenField;
                                var dtAnalisis = gvr.FindControl("dtAnalisis") as DataList;
                                var hfPeriocidad = gvr.FindControl("hfPeriocidad") as HiddenField;

                                foreach (DataListItem dli in dtAnalisis.Items)
                                {
                                    var hfMes = dli.FindControl("hfMes") as HiddenField;
                                    var txvValorAnalisis = dli.FindControl("txvValorAnalisis") as TextBox;

                                    if (w == Convert.ToInt16(hfMes.Value))
                                    {
                                        varObj = varObj + "|" + hfvariable.Value + "(" + txvValorAnalisis.Text.Replace(",", "") + ")|";
                                    }
                                }
                            }
                        }

                        foreach (DataRowView resultado in transacciones.EjecutaFormulaPresupuesto(
                            tipotransaccion: listaEjecutar[x].Tipotransaccion,
                            formulacion: ddlFormulacion.SelectedValue,
                            variable: listaEjecutar[x].Variable,
                            varObj: varObj,
                            año: Convert.ToInt32(ddlAÑo.SelectedValue),
                            mes: w,
                            empresa: Convert.ToInt32(this.Session["empresa"]),
                            perioricidad: listaEjecutar[x].Periocidad,
                            modo: "R"))
                        {
                            foreach (GridViewRow gvr in gvLista.Rows)
                            {
                                if (gvr.RowIndex > 0)
                                {
                                    var dtAnalisis = gvr.FindControl("dtAnalisis") as DataList;
                                    var hfvariable = gvr.FindControl("hfVariable") as HiddenField;
                                    foreach (DataListItem dli in dtAnalisis.Items)
                                    {
                                        var txvValorAnalisis = dli.FindControl("txvValorAnalisis") as TextBox;
                                        var hfMes = dli.FindControl("hfMes") as HiddenField;
                                        if (hfvariable.Value == resultado.Row.ItemArray.GetValue(2).ToString()
                                            && Convert.ToInt16(hfMes.Value) == w
                                            )
                                        {
                                            if (Convert.ToInt16(resultado.Row.ItemArray.GetValue(3)) == 1)
                                            {
                                                txvValorAnalisis.Text = String.Format("{0:#,#.00}", Convert.ToDecimal(resultado.Row.ItemArray.GetValue(0).ToString()));
                                            }
                                            else
                                            {
                                                txvValorAnalisis.Text = String.Format("{0:#,0}", Convert.ToDecimal(resultado.Row.ItemArray.GetValue(0).ToString()));
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
                lblCalcular.Visible = true;
                lbRegistrar.Visible = true;

                totalizar();
                return true;
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
                return false;
            }
        }

        private void totalizar()
        {

            foreach (GridViewRow gvr in gvLista.Rows)
            {
                decimal totalColumna = 0;
                if (gvr.RowIndex > 0)
                {

                    var dtAnalisis = gvr.FindControl("dtAnalisis") as DataList;
                    var hfvariable = gvr.FindControl("hfVariable") as HiddenField;
                    foreach (DataListItem dli in dtAnalisis.Items)
                    {
                        var txvValorAnalisis = dli.FindControl("txvValorAnalisis") as TextBox;
                        var hfMes = dli.FindControl("hfMes") as HiddenField;
                        if (Convert.ToDecimal(hfMes.Value) < 13)
                        {
                            totalColumna += Convert.ToDecimal(txvValorAnalisis.Text);
                        }

                        if (hfMes.Value == "13")
                        {
                            txvValorAnalisis.Text = String.Format("{0:#,#.00}", Convert.ToDecimal(totalColumna.ToString()));
                        }
                    }
                }
            }

        }

        protected void lbICalcular_Click(object sender, EventArgs e)
        {

            if (Calcular())
            {
                MostrarMensaje("Datos calculados");
            }
        }

        protected void gvTransaccion_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            this.Session["editar"] = true;
            try
            {
                string numero = this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text;
                string formulacion = this.gvTransaccion.Rows[e.RowIndex].Cells[5].Text;
                string tipotransaccion = this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text;
                string año = this.gvTransaccion.Rows[e.RowIndex].Cells[4].Text;
                ManejoEncabezado();

                ddlTipoDocumento.SelectedValue = tipotransaccion;
                cargarotroscampos();
                txtNumero.Text = numero;
                ddlFormulacion.SelectedValue = formulacion;
                ddlAÑo.SelectedValue = año;
                fsCabeza.Visible = true;
                ddlTipoDocumento.Enabled = false;
                txtNumero.Enabled = false;
                ddlFormulacion.Enabled = false;
                ddlAÑo.Enabled = false;
                cargarvariables();

                DataView dvDetalle = transacciones.RetornaDatosPresupuestoDetalle(
                    tipo: tipotransaccion,
                    numero: numero,
                    empresa: Convert.ToInt32(this.Session["empresa"])
                    );

                foreach (GridViewRow gvr in gvLista.Rows)
                {
                    foreach (DataRowView drv in dvDetalle)
                    {
                        if (gvr.RowIndex > 0)
                        {
                            var dtAnalisis = gvr.FindControl("dtAnalisis") as DataList;
                            var hfvariable = gvr.FindControl("hfVariable") as HiddenField;
                            foreach (DataListItem dli in dtAnalisis.Items)
                            {
                                var txvValorAnalisis = dli.FindControl("txvValorAnalisis") as TextBox;
                                var hfMes = dli.FindControl("hfMes") as HiddenField;
                                if (Convert.ToInt16(hfMes.Value) == Convert.ToInt16(drv.Row.ItemArray.GetValue(5))
                                    && hfvariable.Value == drv.Row.ItemArray.GetValue(6).ToString()
                                    )
                                {

                                    if (Convert.ToDecimal(drv.Row.ItemArray.GetValue(7)) == 0)
                                    {
                                        txvValorAnalisis.Text = "0";
                                    }
                                    else
                                    {
                                        txvValorAnalisis.Text = String.Format("{0:#,#.00}", Convert.ToDecimal(drv.Row.ItemArray.GetValue(7)));
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }

                fsGeneral.Visible = true;
                fsCabeza.Visible = true;
                fsRegistro.Visible = true;

                totalizar();
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }

        }

        private void manejoEncabezadoT(bool manejo)
        {
            this.ddlFormulacion.Enabled = manejo;
            this.ddlTipoDocumento.Enabled = manejo;
            tsHora.Visible = manejo;
        }

        protected void niddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.nitxtValor1.Text.Length > 0 && Convert.ToString(this.niddlCampo.SelectedValue).Length > 0)
            {
                this.niimbAdicionar.Enabled = true;
                this.imbBusqueda.Enabled = true;
            }
            else
            {
                this.niimbAdicionar.Enabled = false;
                this.imbBusqueda.Enabled = false;
            }

            this.niimbAdicionar.Focus();
        }

        protected void ddlFormulacion_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (ddlFormulacion.SelectedValue.Trim().Length > 0)
                    CargarGrid();
                else
                    ManejoError("Debe seleccionar una formulacion valida", insertar);
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }
        protected void chkVacio_CheckedChanged(object sender, EventArgs e)
        {

            manejoVacio();

        }

        private void manejoVacio()
        {
            try
            {


                foreach (GridViewRow gvr in gvLista.Rows)
                {
                    CheckBox vacio = ((CheckBox)gvr.FindControl("chkVacio"));

                    if (vacio.Checked == true)
                    {
                        foreach (DataListItem dli in ((DataList)gvr.FindControl("dtAnalisis")).Items)
                        {
                            ((TextBox)dli.FindControl("txvValorAnalisis")).Enabled = false;
                            ((TextBox)dli.FindControl("txvValorAnalisis")).Text = "0";
                            gvr.Cells[4].Enabled = false;
                        }
                    }
                    else
                    {
                        if (gvr.Cells[4].Enabled == false)
                        {
                            foreach (DataListItem dli in ((DataList)gvr.FindControl("dtAnalisis")).Items)
                            {
                                ((TextBox)dli.FindControl("txvValorAnalisis")).Enabled = true;
                                ((TextBox)dli.FindControl("txvValorAnalisis")).Text = "0";
                                gvr.Cells[4].Enabled = true;

                                DataView formulacioD = null; //  tipoTransaccion.SeleccionaFormulacionDetalle(Convert.ToInt16(Session["empresa"]), ddlTipoDocumento.SelectedValue.Trim(), ConfigurationManager.AppSettings["modulo"].ToString(), ddlFormulacion.SelectedValue.Trim());

                                foreach (DataRowView drv in formulacioD)
                                {
                                    if (drv.Row.ItemArray.GetValue(0).ToString().Trim() == ((Label)dli.FindControl("lblIdAnalisis")).Text)
                                    {
                                        ((TextBox)dli.FindControl("txvValorAnalisis")).Enabled = !Convert.ToBoolean(drv.Row.ItemArray.GetValue(2).ToString().Trim());
                                    }
                                }

                            }
                        }

                    }
                    gvr.Cells[5].Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }


        protected void ddlAÑo_SelectedIndexChanged(object sender, EventArgs e)
        {




            cargarvariables();
        }

        private void cargarvariables()
        {
            try
            {
                if (!Convert.ToBoolean(this.Session["editar"]))
                    if (transacciones.verificaRegistroTransaccionPresupuesto(Convert.ToInt32(this.Session["empresa"]),
                     ddlTipoDocumento.SelectedValue.Trim(),
                     ddlFormulacion.SelectedValue,
                    Convert.ToInt32(ddlAÑo.SelectedValue.Trim())
                     ) == 1)
                    {
                        ManejoError("Transaccion existente no puede continuar", "I");
                        cancelar();
                        return;
                    }

                gvLista.DataSource = ctransaccion.seleccionaVariablesTransaccionItems(tipotransaccion: ddlTipoDocumento.SelectedValue, item: ddlFormulacion.SelectedValue,
                     empresa: Convert.ToInt32(this.Session["empresa"])
                     );

                gvLista.DataBind();
                gvLista.ShowHeader = false;
                int x = 0;
                foreach (GridViewRow gvr in gvLista.Rows)
                {
                    var dtAnalisis = gvr.FindControl("dtAnalisis") as DataList;
                    var hfVariable = gvr.FindControl("hfVariable") as HiddenField;
                    dtAnalisis.DataSource = ctransaccion.seleccionaVariablesTransaccionItemsDetalle(movimiento: hfVariable.Value, tipotransaccion: ddlTipoDocumento.SelectedValue, item: ddlFormulacion.SelectedValue, empresa: Convert.ToInt32(this.Session["empresa"]));
                    dtAnalisis.DataBind();
                    foreach (DataListItem dli in dtAnalisis.Items)
                    {
                        var lblmes = dli.FindControl("lblMes") as Label;
                        var hfmes = dli.FindControl("hfmes") as HiddenField;
                        var txvValorAnalisis = dli.FindControl("txvValorAnalisis") as TextBox;
                        if (hfVariable.Value == "0")
                        {
                            txvValorAnalisis.Visible = false;
                        }
                        if (hfmes.Value == "13" || hfVariable.Value == "9999")
                        {
                            txvValorAnalisis.Enabled = false;
                        }
                    }

                    foreach (DataListItem dli in dtAnalisis.Items)
                    {
                        var lblmes = dli.FindControl("lblMes") as Label;
                        if (hfVariable.Value == "0")
                        {
                            var txvValorAnalisis = dli.FindControl("txvValorAnalisis") as TextBox;
                            txvValorAnalisis.Visible = false;
                        }
                        if (x > 0)
                        {
                            lblmes.Visible = false;
                        }
                        if (lblmes.Text.Trim().Length > 0 & x == 0)
                        {
                            x++;
                            break;
                        }
                    }
                }
                lblCalcular.Visible = true;
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        protected void ddlFormulacion_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (ddlAÑo.SelectedValue.Trim().Length > 0)
            {
                cargarvariables();
            }
        }
    }
}