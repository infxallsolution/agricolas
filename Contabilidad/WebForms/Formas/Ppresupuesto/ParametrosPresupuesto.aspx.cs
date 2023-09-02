using Contabilidad.WebForms.App_Code.Administracion;
using Contabilidad.WebForms.App_Code.General;
using Contabilidad.WebForms.App_Code.Presupuesto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Contabilidad.WebForms.Formas.Ppresupuesto
{
    public partial class ParametrosPresupuesto : BasePage

    {
        #region Instancias
        CcentroCosto centroCosto = new CcentroCosto();
        cPucSiesa pucisesa = new cPucSiesa();
        Cusuario usuario = new Cusuario();
        CplanPresupuestal planpresupuestal = new CplanPresupuestal();
        CcostoPresupuesto ccentrocostopresupuesto = new CcostoPresupuesto();

        #endregion Instancias

        #region Metodos

        private void cargarCombos()
        {
            try
            {
                this.ddlNivel1.DataSource = pucisesa.RetornaPucSiesaNivel(Convert.ToInt16(this.Session["empresa"]), 1);
                this.ddlNivel1.DataValueField = "codigo";
                this.ddlNivel1.DataTextField = "descripcion";
                this.ddlNivel1.DataBind();
                this.ddlNivel1.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
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
            seguridad.InsertaLog(Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + Page.ToString(),
                "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
        }

        //private void GetEntidad()
        //{
        //    try
        //    {
        //        if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(), consulta, Convert.ToInt16(this.Session["empresa"])) == 0)
        //        {
        //            ManejoError("Usuario no autorizado para ejecutar esta operación", consulta);
        //            return;
        //        }
        //        this.gvLista.DataSource = centroCosto.BuscarEntidad(this.nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
        //        this.gvLista.DataBind();
        //        this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";
        //        seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
        //        this.gvLista.Rows.Count.ToString() + " Registros encontrados", HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(this.Session["empresa"]));
        //    }
        //    catch (Exception ex)
        //    {
        //        ManejoError("Error al cargar la tabla correspondiente a: " +  limpiarMensaje(ex.Message), "C");
        //    }
        //}


        //private void CargarCombos()
        //{
        //    try
        //    {
        //        this.ddlNivel.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cEstructuraCCosto", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"])); ;
        //        this.ddlNivel.DataValueField = "nivel";
        //        this.ddlNivel.DataTextField = "descripcion";
        //        this.ddlNivel.DataBind();
        //        this.ddlNivel.Items.Insert(0, new ListItem("", ""));
        //    }
        //    catch (Exception ex)
        //    {
        //        ManejoError("Error al cargar estructura de centro de costo. Correspondiente a: " +  limpiarMensaje(ex.Message), "C");
        //    }

        //    try
        //    {
        //        this.ddlNivelMayor.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("cEstructuraCCosto", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"])); ;
        //        this.ddlNivelMayor.DataValueField = "nivel";
        //        this.ddlNivelMayor.DataTextField = "descripcion";
        //        this.ddlNivelMayor.DataBind();
        //        this.ddlNivelMayor.Items.Insert(0, new ListItem("", ""));
        //    }
        //    catch (Exception ex)
        //    {
        //        ManejoError("Error al cargar Niveles Mayor. Correspondiente a: " +  limpiarMensaje(ex.Message), "C");
        //    }

        //}

        //private void EntidadKey()
        //{
        //    object[] objKey = new object[] { this.txtCodigo.Text, Convert.ToInt16(this.Session["empresa"]), ddlMayor.SelectedValue };

        //    try
        //    {
        //        if (CentidadMetodos.EntidadGetKey("cCentrosCostoSigo", "ppa", objKey).Tables[0].Rows.Count > 0)
        //        {
        //            this.nilblInformacion.Visible = true;
        //            this.nilblInformacion.Text = "Centro de costo " + this.txtCodigo.Text + " ya se encuentra registrado";
        //            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ManejoError("Error al validar la llave primaria correspondiente a: " +  limpiarMensaje(ex.Message), "C");
        //    }
        //}

        //private void Guardar()
        //{
        //    string operacion = "inserta";
        //    string nivelMayor = null, mayor = null;
        //    try
        //    {
        //        if (this.ddlNivelMayor.SelectedValue.Length == 0)
        //        {
        //            nivelMayor = null;
        //            mayor = null;
        //        }
        //        else
        //        {
        //            nivelMayor = this.ddlNivelMayor.SelectedValue;
        //            mayor = this.ddlMayor.SelectedValue;
        //        }

        //        if (Convert.ToBoolean(this.Session["editar"]) == true)
        //            operacion = "actualiza";

        //        object[] objValores = new object[]
        //        {   
        //            this.chkActivo.Checked,
        //            chkAuxiliar.Checked,
        //            this.txtCodigo.Text,
        //            txtDescripcion.Text,
        //            Convert.ToInt16(this.Session["empresa"]),
        //            mayor,
        //            ddlNivel.SelectedValue,                
        //            nivelMayor,
        //        };

        //        switch (CentidadMetodos.EntidadInsertUpdateDelete("cCentrosCostoSigo", operacion, "ppa", objValores))
        //        {
        //            case 0:
        //                ManejoExito("Datos insertados satisfactoriamente", operacion.Substring(0, 1).ToUpper());
        //                break;
        //            case 1:
        //                ManejoError("Errores al insertar el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ManejoError("Error al guardar los datos correspondiente a: " +  limpiarMensaje(ex.Message), operacion.Substring(0, 1).ToUpper());
        //    }
        //}

        #endregion Metodos

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {
                if (!IsPostBack)
                {
                    if (seguridad.VerificaAccesoPagina(
                           this.Session["usuario"].ToString(),
                           ConfigurationManager.AppSettings["Modulo"].ToString(),
                           nombrePaginaActual(),
                           Convert.ToInt16(this.Session["empresa"])) != 0)
                    {
                        cargarCombos();
                        BindColumnToGridview();
                    }
                    else
                    {
                        ManejoError("Usuario no autorizado para ingresar a esta página", ingreso);
                    }
                }
            }
        }


        protected void lbCancelar_Click(object sender, ImageClickEventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.rptCuentas.DataSource = null;
            this.rptCuentas.DataBind();
        }

        protected void niimbBuscar_Click(object sender, ImageClickEventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);

        }


        protected void ddlNivel1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlNivel1.SelectedValue.Trim().Length == 0)
                {
                    ManejoError("Seleccione un nivel 1 valido", "C");
                    rptCuentas.DataSource = null;
                    rptCuentas.DataBind();
                    lbRegistrar.Visible = false;
                    return;
                }
                else
                {

                    DataView dvNivel2 = pucisesa.RetornaPucSiesaNivel(Convert.ToInt16(this.Session["empresa"]), 2);
                    dvNivel2.RowFilter = "codigo like '" + ddlNivel1.SelectedValue + "%'";
                    this.ddlNivel2.DataSource = dvNivel2;
                    this.ddlNivel2.DataValueField = "codigo";
                    this.ddlNivel2.DataTextField = "descripcion";
                    this.ddlNivel2.DataBind();
                    this.ddlNivel2.Items.Insert(0, new ListItem("", ""));
                }

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        protected void ddlNivel2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarNivel2();

        }

        private void cargarNivel2()
        {
            try
            {
                if (ddlNivel2.SelectedValue.Trim().Length == 0)
                {
                    ManejoError("Seleccione un nivel 2 valido", "C");
                    rptCuentas.DataSource = null;
                    rptCuentas.DataBind();
                    lbRegistrar.Visible = false;
                    return;
                }
                else
                {
                    try
                    {
                        DataView dvNivel3 = pucisesa.RetornaPucSiesaNivel(Convert.ToInt16(this.Session["empresa"]), 3);
                        dvNivel3.RowFilter = "codigo like '" + ddlNivel2.SelectedValue + "%'";
                        this.ddlNivel3.DataSource = dvNivel3;
                        this.ddlNivel3.DataValueField = "codigo";
                        this.ddlNivel3.DataTextField = "descripcion";
                        this.ddlNivel3.DataBind();
                        this.ddlNivel3.Items.Insert(0, new ListItem("", ""));
                    }
                    catch (Exception ex)
                    {
                        ManejoErrorCatch(ex);
                    }

                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
                rptCuentas.DataSource = null;
                rptCuentas.DataBind();
            }
        }


        protected DataView CargarPerfiles()
        {
            try
            {
                DataTable dtPerfiles = new DataTable();
                dtPerfiles = usuario.retornaPerfil().ToTable();
                DataRow fila = dtPerfiles.NewRow();
                fila["codigo"] = "";
                fila["descripcion"] = "";
                fila["activo"] = true;
                dtPerfiles.Rows.Add(fila);
                DataView perfiles = new DataView(dtPerfiles);
                perfiles.Sort = "descripcion";
                return perfiles;

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
                return null;
            }



        }
        [WebMethod(EnableSession = true)]
        public static CuentaClass[] RetornaPlanPresupuesto(string texto)
        {
            CplanPresupuestal planpresupuestal = new CplanPresupuestal();
            List<CuentaClass> Detail = new List<CuentaClass>();
            DataView dvCuentas = null;
            dvCuentas = planpresupuestal.RetornaPlanPresupuesto(Convert.ToInt16(HttpContext.Current.Session["empresa"]));
            dvCuentas.RowFilter = "codigo like '" + texto + "%' or nombre like '%" + texto + "%'";

            foreach (DataRowView dtRow in dvCuentas)
            {
                CuentaClass DataObj = new CuentaClass();
                DataObj.codigoCuenta = dtRow.Row.ItemArray.GetValue(1).ToString();
                DataObj.NombreCuenta = dtRow.Row.ItemArray.GetValue(2).ToString();
                Detail.Add(DataObj);
            }

            return Detail.ToArray();

        }

        [WebMethod(EnableSession = true)]
        public static CuentaClass[] RetornaCcostoPresupuesto(string texto)
        {
            CcostoPresupuesto ccentrocostopresupuesto = new CcostoPresupuesto();
            List<CuentaClass> Detail = new List<CuentaClass>();
            DataView dvCuentas = null;
            dvCuentas = ccentrocostopresupuesto.retornaCcostoPresupuesto(Convert.ToInt16(HttpContext.Current.Session["empresa"]));
            dvCuentas.RowFilter = "codigo like '" + texto + "%' or nombre like '%" + texto + "%'";

            foreach (DataRowView dtRow in dvCuentas)
            {
                CuentaClass DataObj = new CuentaClass();
                DataObj.codigoCuenta = dtRow.Row.ItemArray.GetValue(1).ToString();
                DataObj.NombreCuenta = dtRow.Row.ItemArray.GetValue(2).ToString();
                Detail.Add(DataObj);
            }

            return Detail.ToArray();

        }

        [WebMethod(EnableSession = true)]
        public static CuentaClass[] RetornaCcostoSiesa(string texto, string cuenta)
        {
            cPucSiesa ccentrocostopresupuesto = new cPucSiesa();
            List<CuentaClass> Detail = new List<CuentaClass>();
            DataView dvCuentas = null;
            dvCuentas = ccentrocostopresupuesto.retornaCcostosSiesa(Convert.ToInt16(HttpContext.Current.Session["empresa"]), cuenta);
            dvCuentas.RowFilter = "codigo like '" + texto + "%' or nombre like '%" + texto + "%'";

            foreach (DataRowView dtRow in dvCuentas)
            {
                CuentaClass DataObj = new CuentaClass();
                DataObj.codigoCuenta = dtRow.Row.ItemArray.GetValue(1).ToString();
                DataObj.NombreCuenta = dtRow.Row.ItemArray.GetValue(2).ToString();
                Detail.Add(DataObj);
            }

            return Detail.ToArray();

        }

        private void BindColumnToGridview()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("codigo");
            dt.Columns.Add("nombre");
            dt.Rows.Add();
            gvCuenta.DataSource = dt;
            gvCuenta.DataBind();
            gvCuenta.Rows[0].Visible = false;

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("codigo");
            dt2.Columns.Add("nombre");
            dt2.Rows.Add();

            gvCcosto.DataSource = dt2;
            gvCcosto.DataBind();
            gvCcosto.Rows[0].Visible = false;
        }


        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            string operacion = "inserta";
            bool validar = false;
            string perfil = null;
            string idCuentaSiesa = null;
            string rowIdCuentaSiesa = null;
            string ccostoPresupuesto = null;
            string ccostoSiesa = null;
            string idCuentaPresupuesto = null;
            bool cuentavalida = true;
            bool ccostovalido = true;
            try
            {

                foreach (RepeaterItem rpti in rptCuentas.Items)
                {
                    idCuentaSiesa = ((Label)rpti.FindControl("lblCodigoCuenta")).Text.Trim();
                    rowIdCuentaSiesa = ((HiddenField)rpti.FindControl("hfRowIdCuenta")).Value;
                    var chkMpresupuesto = rpti.FindControl("chkMpresupuesto") as CheckBox;
                    var txtCuentaPresupuesto = rpti.FindControl("txtCuenta") as TextBox;

                    if (chkMpresupuesto.Checked == true & chkMpresupuesto.Visible == true)
                    {
                        if (txtCuentaPresupuesto.Visible == true)
                        {
                            idCuentaPresupuesto = txtCuentaPresupuesto.Text;
                        }

                        if (planpresupuestal.ValidaCuentaPlanPresupuestal(Convert.ToInt16(this.Session["empresa"]), idCuentaPresupuesto) == 0)
                        {
                            cuentavalida = false;
                        }

                    }
                }


                if (cuentavalida == false)
                {
                    ManejoError("Hay cuentas que no son validas verificar", "I");
                    return;
                }
                if (ccostovalido == false)
                {
                    ManejoError("Hay centros de costos que no son validos verificar", "I");
                    return;
                }


                foreach (RepeaterItem rpti in rptCuentas.Items)
                {
                    idCuentaSiesa = ((Label)rpti.FindControl("lblCodigoCuenta")).Text.Trim();
                    rowIdCuentaSiesa = ((HiddenField)rpti.FindControl("hfRowIdCuenta")).Value;

                    object[] objValoresE = new object[] {
                    Convert.ToInt16(this.Session["empresa"]),
                    idCuentaSiesa,
                    rowIdCuentaSiesa
                     };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete(
                        "cParametrosPresupuesto",
                        "elimina",
                        "ppa",
                        objValoresE))
                    {

                        case 1:
                            validar = true;
                            break;
                    }

                }



                foreach (RepeaterItem rpti in rptCuentas.Items)
                {
                    idCuentaSiesa = ((Label)rpti.FindControl("lblCodigoCuenta")).Text.Trim();
                    rowIdCuentaSiesa = ((HiddenField)rpti.FindControl("hfRowIdCuenta")).Value;
                    var chkMpresupuesto = rpti.FindControl("chkMpresupuesto") as CheckBox;
                    var txtCuentaPresupuesto = rpti.FindControl("txtCuenta") as TextBox;
                    var chkMCcosto = rpti.FindControl("chkMccosto") as CheckBox;
                    var ddlPerfil = rpti.FindControl("ddlPerfil") as DropDownList;
                    string tipoCcosto = null;

                    if (ddlPerfil.SelectedValue.Trim().Length != 0)
                    {
                        perfil = ddlPerfil.SelectedValue.Trim();
                    }


                    if (chkMpresupuesto.Checked == true & chkMpresupuesto.Visible == true)
                    {
                        object[] objValores = new object[] {
                                    Convert.ToInt16(this.Session["empresa"]),            //@empresa    int
                                      DateTime.Now,          //@fechaRegistro  datetime
                                      txtCuentaPresupuesto.Text.Trim(),          //@idCuentaPresupuesto    varchar
                                      idCuentaSiesa,    //@idCuentaSiesa  varchar
                                      chkMCcosto.Checked,          //@mCcosto    bit
                                      chkMpresupuesto.Checked,          //@mPresupuesto   bit
                                      perfil,          //@perfil varchar
                                      rowIdCuentaSiesa,          //@rowIdCuentaSiesa   int
                                      tipoCcosto,          //@tipoCcosto varchar
                                      this.Session["usuario"].ToString()          //@usuarioRegistro    varchar
                };

                        switch (CentidadMetodos.EntidadInsertUpdateDelete(
                            "cParametrosPresupuesto",
                            "inserta",
                            "ppa",
                            objValores))
                        {

                            case 1:
                                validar = true;
                                break;
                        }

                    }
                }


                if (validar == false)
                {
                    ManejoExito("Datos insertados satisfactoriamente", insertar);
                }
                else
                {
                    ManejoError("Errores al insertar el registro. Operación no realizada", insertar);

                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        protected void rptCuentas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                //Look for my values here:
                DataRowView drv = (DataRowView)e.Item.DataItem;


                if (drv.Row.ItemArray.GetValue(12).ToString().Trim().Length != 0)
                {
                    ((DropDownList)e.Item.FindControl("ddlPerfil")).SelectedValue = drv.Row.ItemArray.GetValue(12).ToString().Trim();
                }

            }
        }

        protected void ddlNivel3_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                if (ddlNivel3.SelectedValue.Trim().Length == 0)
                {
                    ManejoError("Seleccione un nivel 3 valido", "C");
                    rptCuentas.DataSource = null;
                    rptCuentas.DataBind();
                    lbRegistrar.Visible = false;
                    return;
                }
                else
                {
                    DataView dvCuentas = pucisesa.RetornaPucSiesa(Convert.ToInt16(this.Session["empresa"]), ddlNivel1.SelectedValue.Trim(), ddlNivel2.SelectedValue.Trim(), ddlNivel3.SelectedValue.Trim(), "");

                    if (dvCuentas.Count > 150)
                    {
                        try
                        {
                            if (ddlNivel3.SelectedValue.Trim().Length == 0)
                            {
                                ManejoError("Seleccione un nivel 2 valido", "C");
                                rptCuentas.DataSource = null;
                                rptCuentas.DataBind();
                                lbRegistrar.Visible = false;
                                return;
                            }
                            else
                            {
                                try
                                {
                                    DataView dvNivel4 = pucisesa.RetornaPucSiesaNivel(Convert.ToInt16(this.Session["empresa"]), 4);
                                    dvNivel4.RowFilter = "codigo like '" + ddlNivel3.SelectedValue + "%'";
                                    this.ddlNivel4.DataSource = dvNivel4;
                                    this.ddlNivel4.DataValueField = "codigo";
                                    this.ddlNivel4.DataTextField = "descripcion";
                                    this.ddlNivel4.DataBind();
                                    this.ddlNivel4.Items.Insert(0, new ListItem("", ""));
                                    this.lblNivel4.Visible = true;
                                    this.ddlNivel4.Visible = true;
                                }
                                catch (Exception ex)
                                {
                                    ManejoErrorCatch(ex);
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            ManejoErrorCatch(ex);
                            rptCuentas.DataSource = null;
                            rptCuentas.DataBind();
                        }
                    }
                    else
                    {
                        rptCuentas.DataSource = dvCuentas;
                        rptCuentas.DataBind();
                        lbRegistrar.Visible = true;
                        this.lblNivel4.Visible = false;
                        this.ddlNivel4.Visible = false;

                    }



                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
                rptCuentas.DataSource = null;
                rptCuentas.DataBind();
            }
        }


        protected void rptCuentas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            this.Session["index"] = e.Item.ItemIndex;
            var texbox = e.Item.FindControl("txtCuenta") as TextBox;
            var texbox2 = e.Item.FindControl("txtCentroCosto") as TextBox;
            var rblCCosto = e.Item.FindControl("rblCcosto") as RadioButtonList;
            var linkbuton = new LinkButton();
            var checkbox = new CheckBox();
            var gridview = e.Item.FindControl("gvCCostoPresupuesto") as GridView;

            StringBuilder cstext2 = new StringBuilder();

            if (e.CommandSource is LinkButton)
            {
                linkbuton = e.CommandSource as LinkButton;
            }

            if (e.CommandSource is CheckBox)
            {
                checkbox = e.CommandSource as CheckBox;
            }


            if (checkbox != null)
            {
                if (checkbox.ID == "chkMccosto")
                {
                }

            }



            if (linkbuton != null)
            {

                if (linkbuton.ID == "imCuenta")
                {

                    cstext2.Append(@"
            function modalCuenta() {
                $('#modalCuenta').modal();
                $('#modalCuenta').data('target-texbox','" + texbox.ClientID + @"');
                $('#nitxtBusquedaCuenta').val('" + texbox.Text + @"');
            }
            modalCuenta();
        ");
                }

                if (linkbuton.ID == "imCCosto")
                {
                    cstext2.Append(@"
            function modalCcosto() {
                $('#modalCcosto').modal();
                $('#modalCcosto').data('target-texbox','" + texbox2.ClientID + @"')
                $('#nitxtBusquedaCcosto').val('" + texbox2.Text + @"');
            }
            modalCcosto();
        ");
                }

                if (linkbuton.ID == "imCCostoSiesa")
                {
                    cstext2.Append(@"
            function modalCcosto() {
                $('#modalCcosto').modal();
                $('#modalCcosto').data('target-texbox','" + texbox2.ClientID + @"')
                  $('#modalCcosto').data('cuentasiesa','" + texbox.Text + @"')
                $('#nitxtBusquedaCcosto').val('" + texbox2.Text + @"');
            }
            modalCcosto();
        ");
                }

                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), cstext2.ToString(), true);
            }
        }

        protected void gvCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(this.Session["index"]);
                RepeaterItem rpi = rptCuentas.Items[index];
                ((TextBox)rpi.FindControl("txtCuenta")).Text = gvCuenta.SelectedRow.Cells[1].Text;
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        protected void gvCCostoPresupuesto_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                StringBuilder cstext2 = new StringBuilder();

                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                var textbox = gvr.FindControl("txtCentroCosto") as TextBox;

                cstext2.Append(@"
            function modalCcosto() {
                $('#modalCcosto').modal();
                $('#modalCcosto').data('target-texbox','" + textbox.ClientID.ToString() + @"')
                $('#nitxtBusquedaCcosto').val('" + textbox.Text + @"');
            }
            modalCcosto();
        ");
                ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), cstext2.ToString(), true);
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }


        protected void ddlNivel4_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                DataView dvCuentas = pucisesa.RetornaPucSiesa(Convert.ToInt16(this.Session["empresa"]), ddlNivel1.SelectedValue.Trim(), ddlNivel2.SelectedValue.Trim(), ddlNivel3.SelectedValue.Trim(), ddlNivel4.SelectedValue.Trim());

                if (ddlNivel4.SelectedValue.Trim().Length == 0)
                {
                    ManejoError("Seleccione un nivel 4 valido", "C");
                    rptCuentas.DataSource = null;
                    rptCuentas.DataBind();
                    lbRegistrar.Visible = false;
                    return;
                }
                else
                {
                    try
                    {
                        rptCuentas.DataSource = dvCuentas;
                        rptCuentas.DataBind();
                        lbRegistrar.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        ManejoErrorCatch(ex);
                    }

                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
                rptCuentas.DataSource = null;
                rptCuentas.DataBind();
            }

        }
        protected void rblCcosto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var rblist = sender as RadioButtonList;
                var padre = rblist.Parent.Parent;
                var gridview = padre.FindControl("gvCCostoPresupuesto") as GridView;
                var labelcuenta = padre.FindControl("lblCodigoCuenta") as Label;
                var ddlccosto = padre.FindControl("ddlCcosto") as DropDownList;
            }
            catch (Exception ex)
            {

            }
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            var chek = sender as CheckBox;
            var padre = chek.Parent.Parent;
            var gridview = padre.FindControl("gvCCostoPresupuesto") as GridView;
            var labelcuenta = padre.FindControl("lblCodigoCuenta") as Label;
            var textbox = padre.FindControl("txtCentroCosto") as TextBox;

            if (chek.Checked == false)
            {
                textbox.Text = "";
                textbox.Enabled = false;
            }
            else
            {
                textbox.Text = "";
                textbox.Enabled = true;
            }

        }
    }

    #endregion
}