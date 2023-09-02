using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
using Nomina.WebForms.App_Code.Liquidacion;
using Nomina.WebForms.App_Code.Transaccion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Pliquidacion
{
    public partial class RegistroNovedadesExcel : BasePage
    {

        #region Instancias

        Coperadores operadores = new Coperadores();
        CtipoTransaccion tipoTransaccion = new CtipoTransaccion();
        CtransaccionNovedad transaccionNovedad = new CtransaccionNovedad();
        Cperiodos periodo = new Cperiodos();
        string numerotransaccion = "";
        Ctransacciones transacciones = new Ctransacciones();
        Cconceptos conceptos = new Cconceptos();
        Cfuncionarios funcionario = new Cfuncionarios();
        Cgeneral general = new Cgeneral();
        cNovedadTransaccion novedad = new cNovedadTransaccion();

        #endregion Instancias

        #region Metodos

        protected void ImportarGrilla(string FilePath, string Extension, string isHDR, out DataTable informacion)
        {

            try
            {

                string conStr = "";
                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07
                        conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }
                conStr = String.Format(conStr, FilePath, isHDR);
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                cmdExcel.Connection = connExcel;
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();
                informacion = dt;
            }
            catch (Exception ex)
            {
                informacion = null;
                ManejoError("Error al conectar excel " + ex.Message, "C");

            }



        }
        public static void MostrarMensaje(string message)
        {

            string script = "<script type=text/javascript>alert('" + message + "');</script>";
            Page page = HttpContext.Current.CurrentHandler as Page;
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            {
                page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", script, false);
            }
        }



        private void ManejoExito(string mensaje, string operacion)
        {
            CcontrolesUsuario.InhabilitarControles(this.upCabeza.Controls);
            CcontrolesUsuario.LimpiarControles(this.upCabeza.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upDetalle.Controls);
            CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);

            InHabilitaEncabezado();

            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.Session["transaccion"] = null;
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            this.lbRegistrar.Visible = false;
        }
        private void ManejoEncabezado()
        {
            HabilitaEncabezado();
            CargarTipoTransaccion();
        }
        private void CargarTipoTransaccion()
        {
            try
            {
                this.ddlTipoDocumento.DataSource = tipoTransaccion.GetTipoTransaccionModulo(Convert.ToInt16(Session["empresa"]));
                this.ddlTipoDocumento.DataValueField = "codigo";
                this.ddlTipoDocumento.DataTextField = "descripcion";
                this.ddlTipoDocumento.DataBind();
                this.ddlTipoDocumento.Items.Insert(0, new ListItem("", ""));
                this.ddlTipoDocumento.SelectedValue = "";
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tipos de transacción. Correspondiente a: " + ex.Message, "C");
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
                ManejoError("Error al recuperar posición de configuración de tipo de transacción. Correspondiente a: " + ex.Message, "C");

                return null;
            }
        }
        private string ConsecutivoTransaccion()
        {
            string numero = "";

            try
            {
                numero = transacciones.RetornaNumeroTransaccion(
                    Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al obtener el número de transacción. Correspondiente a: " + ex.Message, "C");
            }

            return numero;
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

            this.Session["transaccion"] = null;
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
        private int CompruebaTransaccionExistente()
        {
            try
            {
                object[] objkey = new object[] { Convert.ToInt16(Session["empresa"]), this.txtNumero.Text, Convert.ToString(this.ddlTipoDocumento.SelectedValue) };

                if (CentidadMetodos.EntidadGetKey("aTransaccion", "ppa", objkey).Tables[0].DefaultView.Count > 0)
                    return 1;
                else
                    return 0;
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
                if (this.txtFecha.Visible == true)
                {
                    if (CompruebaTransaccionExistente() == 1)
                    {
                        MostrarMensaje("Transacción existente. Por favor corrija");

                        return;
                    }
                }

                this.txtNumero.Enabled = false;

                CcontrolesUsuario.HabilitarControles(
                    this.upCabeza.Controls);

                this.nilbNuevo.Visible = false;
                this.txtFecha.Visible = false;
                this.txtFecha.Focus();
            }
        }
        private void verificaPeriodoCerrado(int año, int mes, int empresa, DateTime fecha)
        {
            if (periodo.RetornaPeriodoCerradoNomina(año, mes, Convert.ToInt16(Session["empresa"]), fecha) == 1)
            {
                ManejoError("Periodo cerrado. No es posible realizar nuevas transacciones", "I");
                return;
            }
        }
        private void Editar()
        {

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {

                    DateTime fecha = Convert.ToDateTime(txtFecha.Text);
                    bool verificacion = true;
                    string conceptos = null, empleado = null, ccosto = null;
                    string remision = null;

                    bool anulado = false;


                    switch (transacciones.EditaEncabezado(this.ddlTipoDocumento.SelectedValue.Trim(), this.txtNumero.Text.Trim(), fecha, empleado, conceptos, ccosto, remision,
                        Server.HtmlDecode(this.txtObservacion.Text.Trim()), Convert.ToInt16(Session["empresa"])))
                    {
                        case 0:

                            foreach (GridViewRow gv in this.gvLista.Rows)
                            {
                                foreach (Control c in gv.Cells[15].Controls)
                                {
                                    if (c is CheckBox)
                                    {
                                        anulado = ((CheckBox)c).Checked;
                                    }
                                }

                                switch (transacciones.EditaDetalle(ddlTipoDocumento.SelectedValue.Trim(),//@tipo
                                                     this.txtNumero.Text.Trim(),   //@numero	varchar
                                                     Convert.ToInt16(gv.Cells[14].Text),//@registro
                                                   Convert.ToDecimal(gv.Cells[6].Text), //@cantidad
                                                   Convert.ToDecimal(gv.Cells[7].Text), //@valor
                                                   gv.Cells[2].Text,    //@concepto
                                                   gv.Cells[4].Text,    //@empleado
                                               Server.HtmlDecode(Convert.ToString(gv.Cells[13].Text.ToString().Trim())),    //@detalle
                                                    Convert.ToInt16(gv.Cells[12].Text),  //frecuencia
                                                    Convert.ToInt16(gv.Cells[10].Text),    //@periodoInicial
                                                    Convert.ToInt16(gv.Cells[11].Text), //@periodoFinal
                                                   Convert.ToInt16(gv.Cells[8].Text),  //@añoInicial
                                                           Convert.ToInt16(gv.Cells[9].Text),   //@añoFinal
                                                    Convert.ToInt16(this.Session["empresa"]),   //@empresa	int
                                                   anulado))
                                {
                                    case 1:

                                        verificacion = false;
                                        break;
                                }
                            }

                            if (verificacion == false)
                            {
                                MostrarMensaje("Error al editar el registro. Operación no realizada");

                            }
                            else
                            {
                                ManejoExito("Transacción editada satisfactoriamente. Transacción número " + this.txtNumero.Text.Trim(), "A");
                                ts.Complete();
                            }
                            break;

                        case 1:

                            MostrarMensaje("Error al editar el encabezado. Operación no realizada");

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al editar la transacción. Correspondiente a: " + ex.Message, "A");
            }
        }
        protected void Guardar()
        {
            string operacion = "inserta";
            bool verificaEncabezado = false;
            bool verificaDetalle = false;
            bool verificaBascula = false;

            try
            {
                cargarExcel();
                using (TransactionScope ts = new TransactionScope())
                {

                    if (gvErrores.Rows.Count == 0 & gvLista.Rows.Count > 0)
                    {
                        DateTime fecha = Convert.ToDateTime(txtFecha.Text);

                        string conceptos = null, empleado = null, ccosto = null;
                        string remision = null;


                        numerotransaccion = transacciones.RetornaNumeroTransaccion(ddlTipoDocumento.SelectedValue, Convert.ToInt16(Session["empresa"]));
                        this.Session["numerotransaccion"] = numerotransaccion;



                        object[] objValo = new object[]{       false, // @anulado	bit
                                                       ccosto,//@ccosto
                                                       conceptos,//@concepto varchar
                                                       empleado,//@empleado
                                                       Convert.ToInt16(this.Session["empresa"]),   //@empresa	int
                                                       fecha,   //@fecha	date
                                                       null,    //@fechaAnulado datetime
                                                       DateTime.Now,    //@fechaRegistro    datetime
                                                       numerotransaccion,   //@numero	varchar
                                                       txtObservacion.Text,   //@observacion	varchar
                                                       remision,   //@remision	varchar
                                                       ddlTipoDocumento.SelectedValue.Trim(),   //@tipo	varchar
                                                       null,   //@usuarioAnulado	varchar
                                                       this.Session["usuario"].ToString()   //@usuarioRegistro	varchar
                                                     
                              };
                        switch (CentidadMetodos.EntidadInsertUpdateDelete("nNovedades", operacion, "ppa", objValo))
                        {
                            case 0:

                                switch (novedad.InsertaNovedadDetalle(Convert.ToInt16(this.Session["empresa"]), ddlTipoDocumento.SelectedValue.Trim(), numerotransaccion))
                                {

                                    case 1:
                                        verificaDetalle = true;
                                        ManejoError("Error al insertar el detalle de la transaccción", "I");
                                        break;
                                }
                                break;
                            case 1:
                                verificaDetalle = true;
                                ManejoError("Error al insertar el detalle de la transaccción", "I");
                                break;
                        }

                        if (verificaEncabezado == false & verificaDetalle == false & verificaBascula == false)
                        {
                            transacciones.ActualizaConsecutivo(ddlTipoDocumento.Text, Convert.ToInt16(Session["empresa"]));
                            ts.Complete();
                            ManejoExito("Datos registrados satisfactoriamente. Transacción número " + numerotransaccion, "I");

                        }
                        else
                        {
                            ManejoError("Error al registrar los datos. ", "I");
                        }

                    }
                    else
                    {
                        MostrarMensaje("Ingrese informacion valida para registrar");
                    }
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al guardar los datos correspondiente a: " + ex.Message, operacion.Substring(0, 1).ToUpper());
            }
        }

        #endregion Metodos

        #region Evento

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {

                if (!IsPostBack)
                {
                    Page.Form.Attributes.Add("enctype", "multipart/form-data");

                    if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                            nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
                    {
                        ManejoError("Usuario no autorizado para ejecutar esta operación", "I");
                        return;
                    }
                    Session["FileUpload1"] = null;
                    this.Session["transaccion"] = null;
                    this.Session["operadores"] = null;
                }
            }

        }
        protected void nilbNuevo_Click(object sender, EventArgs e)
        {
            this.Session["editar"] = false;
            Session["FileUpload1"] = null;
            ManejoEncabezado();

        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            InHabilitaEncabezado();

            CcontrolesUsuario.InhabilitarControles(this.upCabeza.Controls);
            CcontrolesUsuario.LimpiarControles(this.upCabeza.Controls);

            CcontrolesUsuario.InhabilitarControles(this.upDetalle.Controls);
            CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);

            this.Session["editar"] = false;

            this.Session["transaccion"] = null;
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.lbRegistrar.Visible = false;
            Session["FileUpload1"] = null;
            this.lbCancelar.Visible = false;
            upCabeza.Visible = false;
            upDetalle.Visible = false;

        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {

            if (gvLista.Rows.Count <= 0)
            {
                MostrarMensaje("El detalle de la transacción debe tener por lo menos un registro");
                return;
            }

            bool validar = false;

            if (upCabeza.Visible == true)
            {
                if (txtFecha.Enabled == true)
                {
                    if (txtFecha.Text.Trim().Length == 0)
                    {
                        MostrarMensaje("Debe seleccionar una fecha valida");
                        return;
                    }
                }

            }

            if (Convert.ToBoolean(Session["editar"]) == false)
            {
                Guardar();
            }
        }
        private void ComportamientoTransaccion()
        {
            upCabeza.Visible = true;
            upDetalle.Visible = true;

            CcontrolesUsuario.ComportamientoCampoEntidad(this.upCabeza.Controls,
                   "nNovedades", Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"]));
            CcontrolesUsuario.ComportamientoCampoEntidad(this.upDetalle.Controls,
                  "nNovedadesDetalle", Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(Session["empresa"]));

        }
        protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                CcontrolesUsuario.InhabilitarControles(this.upCabeza.Controls);
                CcontrolesUsuario.LimpiarControles(this.upCabeza.Controls);

                CcontrolesUsuario.InhabilitarControles(this.upDetalle.Controls);
                CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);

                this.gvLista.DataSource = null;
                this.gvLista.DataBind();

                this.Session["transaccion"] = null;

                this.txtNumero.Text = ConsecutivoTransaccion();
                this.hdTransaccionConfig.Value = CcontrolesUsuario.TipoTransaccionConfig(this.ddlTipoDocumento.SelectedValue, Convert.ToInt16(Session["empresa"]));
                ComportamientoConsecutivo();
                ComportamientoTransaccion();
                this.fuExcel.Visible = true;
                this.lblfu.Visible = true;
                this.btnLiquidar.Visible = true;



            }
            catch (Exception ex)
            {
                ManejoError("Error al comprobar transacción con referencia. Correspondiente a: " + ex.Message, "C");
            }

        }


        protected void txtNumero_TextChanged(object sender, EventArgs e)
        {
            if (this.txtFecha.Visible == true)
            {
                if (CompruebaTransaccionExistente() == 1)
                {
                    MostrarMensaje("Transacción existente. Por favor corrija");

                    return;
                }
            }

            CcontrolesUsuario.HabilitarControles(this.upCabeza.Controls);

            this.nilbNuevo.Visible = false;
            this.txtFecha.Visible = false;
        }
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.gvLista.Rows.Count >= 20)
                {
                    MostrarMensaje("El número de artículos no puede ser mayor a 20");

                    return;
                }



                if (Convert.ToString(this.ddlTipoDocumento.SelectedValue).Trim().Length == 0 ||
                    this.txtNumero.Text.Trim().Length == 0)
                {
                    MostrarMensaje("Debe ingresar tipo y número de transacción");
                    return;
                }
                if (CcontrolesUsuario.VerificaCamposRequeridos(this.upDetalle.Controls) == false)
                {
                    MostrarMensaje("Campos vacios. Por favor corrija");
                    return;
                }

            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al insertar el registro. Correspondiente a: " + ex.Message);
            }

        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            bool anulado = false;
            if (Convert.ToBoolean(Session["editar"]) == false)
            {

                try
                {
                    List<CtransaccionNovedad> listaTransaccion = null;
                    listaTransaccion = (List<CtransaccionNovedad>)Session["transaccion"];
                    listaTransaccion.RemoveAt(e.RowIndex);

                    this.gvLista.DataSource = listaTransaccion;
                    this.gvLista.DataBind();
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al eliminar el registro. Correspondiente a: " + ex.Message);
                }
            }
            else
            {

                foreach (Control c in this.gvLista.Rows[e.RowIndex].Cells[15].Controls)
                {
                    if (c is CheckBox)
                        anulado = ((CheckBox)c).Checked;
                }
                if (anulado == true)
                {
                    MostrarMensaje("Registro anulado no es posible volver anular");

                    return;
                }

                foreach (Control c in this.gvLista.Rows[e.RowIndex].Cells[15].Controls)
                {
                    if (c is CheckBox)
                        ((CheckBox)c).Checked = true;
                }
                this.gvLista.Rows[e.RowIndex].BackColor = System.Drawing.Color.Red;
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
                MostrarMensaje("Formato de fecha no valido");

                txtFecha.Text = "";
                txtFecha.Focus();
                return;
            }

            verificaPeriodoCerrado(Convert.ToDateTime(txtFecha.Text).Year,
                Convert.ToDateTime(txtFecha.Text).Month, Convert.ToInt16(Session["empresa"]), Convert.ToDateTime(txtFecha.Text));
        }

        #endregion Evento
        protected void btnLiquidar_Click(object sender, EventArgs e)
        {
            try
            {
                cargarExcel();
            }
            catch (Exception ex)
            {
                ManejoError("Error al liquidar archivo debido a:" + ex.Message, "I");
            }
        }

        private void cargarExcel()
        {
            decimal cantiadTotal = 0, valorTotal = 0;
            DataTable excel = new DataTable(); bool isValidFile = false;
            string[] validFileTypes = { "xls", "xlsx", "xlsm", "xltx" };
            txtCantidadTotal.Visible = true;
            txtValorTotal.Visible = true;
            txtCantidadTotal.Enabled = false;
            txtValorTotal.Enabled = false;
            lblCantidadTotal.Visible = true;
            lblValorTotal.Visible = true;

            if (Session["FileUpload1"] == null && fuExcel.HasFile)
            {
                Session["FileUpload1"] = fuExcel;
            }

            else if (Session["FileUpload1"] != null && (!fuExcel.HasFile))
            {
                fuExcel = (FileUpload)Session["FileUpload1"];
            }

            else if (fuExcel.HasFile)
            {
                Session["FileUpload1"] = fuExcel;
            }

            if (this.fuExcel.HasFile)
            {
                string ext = System.IO.Path.GetExtension(this.fuExcel.PostedFile.FileName);



                for (int i = 0; i < validFileTypes.Length; i++)

                {

                    if (ext == "." + validFileTypes[i])

                    {

                        isValidFile = true;

                        break;

                    }

                }
            }

            if (!isValidFile)
            {
                MostrarMensaje("Archivo invalido.Por favor ingrese un archivo con extensión" +
                               string.Join(",", validFileTypes));
            }
            else
            {
                if (fuExcel.HasFile)
                {

                    string FileName = Path.GetFileName(fuExcel.PostedFile.FileName + DateTime.Now.ToString());
                    string Extension = Path.GetExtension(fuExcel.PostedFile.FileName);
                    string FolderPath = Path.GetTempPath();
                    string FilePath = Path.Combine(@FolderPath, FileName);


                    if (File.Exists(FolderPath + FileName))
                    {
                        File.Delete(FolderPath + FileName);
                        fuExcel.SaveAs(FilePath);
                        ImportarGrilla(FilePath, Extension, "yes", out excel);
                    }
                    else
                    {
                        fuExcel.SaveAs(FilePath);
                        ImportarGrilla(FilePath, Extension, "yes", out excel);

                    }
                }
                else
                {
                    MostrarMensaje("Seleccione un archivo para continuar");
                }

                gvErrores.DataSource = null;
                gvErrores.DataBind();
                DataTable dtErrores = new DataTable();
                dtErrores.Columns.Add("Error");
                dtErrores.Columns.Add("Linea");
                int x = 1;

                string retorno = null;
                foreach (DataRow dr in excel.Rows)
                {
                    retorno = null;
                    string concepto = dr.ItemArray.GetValue(0).ToString();
                    string empleado = dr.ItemArray.GetValue(1).ToString();
                    string añoInicial = dr.ItemArray.GetValue(4).ToString();
                    string añoFinal = dr.ItemArray.GetValue(6).ToString();
                    string periodoInicial = dr.ItemArray.GetValue(5).ToString();
                    string periodoFinal = dr.ItemArray.GetValue(7).ToString();
                    string cantidad = dr.ItemArray.GetValue(2).ToString();
                    string valor = dr.ItemArray.GetValue(3).ToString();
                    string detalle = dr.ItemArray.GetValue(8).ToString();
                    string frecuencia = dr.ItemArray.GetValue(9).ToString();

                    if (concepto.Trim().Length > 0 & empleado.Trim().Length > 0)
                    {
                        retorno = novedad.VerificaCargaExcelNovedades(Convert.ToInt16(this.Session["empresa"]), concepto, empleado, añoInicial, periodoInicial, añoFinal, periodoFinal);

                        try
                        {
                            Convert.ToDecimal(valor.Replace(".", "").Replace(",", "."));
                        }
                        catch (Exception)
                        {
                            retorno += " La columna (valor) tiene datos invalidos modificar **; ";
                        }

                        try
                        {
                            Convert.ToDecimal(cantidad.Replace(".", "").Replace(",", "."));
                        }
                        catch (Exception)
                        {
                            retorno += " La columna (cantidad) tiene datos invalidos modificar **; ";
                        }

                        try
                        {
                            if (Convert.ToInt16(frecuencia) > 2 & Convert.ToInt16(frecuencia) < 0)
                            {
                                retorno += " La columna (frecuencia) tiene datos diferentes a 0, 1 ,2 **; ";
                            }
                        }
                        catch (Exception)
                        {
                            retorno += " La columna (frecuencia) tiene datos invalidos modificar **; ";
                        }

                        if (retorno.Trim().Length > 0)
                        {
                            DataRow row = dtErrores.NewRow();
                            row["Error"] = retorno;
                            row["Linea"] = x;
                            dtErrores.Rows.Add(row);
                        }
                    }
                    else
                    {
                        DataRow row = dtErrores.NewRow();
                        row["Error"] = "Fila " + x + " No contiene datos";
                        row["Linea"] = x;
                        dtErrores.Rows.Add(row);
                    }
                    x++;
                }

                gvErrores.DataSource = dtErrores;
                gvErrores.DataBind();

                if (gvErrores.Rows.Count == 0)
                {
                    this.txtNumero.Text = ConsecutivoTransaccion();
                    int y = 1;
                    bool validar = false;
                    switch (novedad.EliminaTmpNovedadDetalle(Convert.ToInt16(this.Session["empresa"]), ddlTipoDocumento.SelectedValue.Trim(), txtNumero.Text))
                    {
                        case 1:
                            validar = true;
                            break;
                    }

                    foreach (DataRow dr in excel.Rows)
                    {
                        retorno = null;
                        string concepto = dr.ItemArray.GetValue(0).ToString();
                        string empleado = dr.ItemArray.GetValue(1).ToString();
                        string añoInicial = dr.ItemArray.GetValue(4).ToString();
                        string añoFinal = dr.ItemArray.GetValue(6).ToString();
                        string periodoInicial = dr.ItemArray.GetValue(5).ToString();
                        string periodoFinal = dr.ItemArray.GetValue(7).ToString();
                        string cantidad = dr.ItemArray.GetValue(2).ToString();
                        cantiadTotal += Convert.ToDecimal(dr.ItemArray.GetValue(2).ToString().Replace(',', '.'));
                        string valor = dr.ItemArray.GetValue(3).ToString();
                        valorTotal += Convert.ToDecimal(dr.ItemArray.GetValue(3).ToString().Replace(',', '.'));
                        string detalle = dr.ItemArray.GetValue(8).ToString();
                        string frecuencia = dr.ItemArray.GetValue(9).ToString();
                        switch (novedad.insertaTmpDetalle(Convert.ToInt16(this.Session["empresa"]),
                         ddlTipoDocumento.SelectedValue, txtNumero.Text, y, empleado, concepto, Convert.ToDecimal(cantidad.Replace(',', '.')), Convert.ToDecimal(valor.Replace(',', '.')),
                        añoInicial, periodoInicial, añoFinal, periodoFinal, Convert.ToInt16(frecuencia), detalle))
                        {
                            case 1:
                                validar = true;
                                break;
                        }
                        y++;
                    }

                    gvLista.DataSource = novedad.retornaTmpDetalle(Convert.ToInt16(this.Session["empresa"]), ddlTipoDocumento.SelectedValue.Trim(), txtNumero.Text);
                    gvLista.DataBind();
                    txtCantidadTotal.Text = string.Format("{0:N}", cantiadTotal.ToString());
                    txtValorTotal.Text = string.Format("{0:N}", valorTotal.ToString());

                    if (validar == false)
                    {
                        MostrarMensaje("Documento cargado satisfactoriamente");
                    }
                    else
                    {
                        MostrarMensaje("Erro al cargar documento por favor verifique");
                    }
                }
            }
        }
    }
}