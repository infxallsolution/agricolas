using Nomina.WebForms.App_Code.Administracion;
using Nomina.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nomina.WebForms.Formas.Padministracion
{
    public partial class Funcionario : BasePage
    {
        #region Instancias

        Cfuncionario funcionarios = new Cfuncionario();
        Cgeneral general = new Cgeneral();
        CtipoFuncionario tipoFuncionario = new CtipoFuncionario();
        private byte[] Foto
        {
            get { object o = Session["Foto"]; return (o == null) ? null : (byte[])o; }
            set { Session["Foto"] = value; }
        }
        private string descripcionFuncionario = null;

        #endregion Instancias

        #region Metodos

        private void GetEntidad()
        {
            try
            {
                if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                         nombrePaginaActual(), "C", Convert.ToInt16(Session["empresa"])) == 0)
                {
                    ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                    return;
                }

                this.gvLista.DataSource = funcionarios.BuscarEntidad(this.nitxtBusqueda.Text, Convert.ToInt16(Session["empresa"]));
                this.gvLista.DataBind();
                this.nilblInformacion.Text = this.gvLista.Rows.Count.ToString() + " Registros encontrados";
                seguridad.InsertaLog(this.Session["usuario"].ToString(), "C", ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                             this.gvLista.Rows.Count.ToString() + " Registros encontrados", HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.nibtnNuevo.Visible = true;
            this.fuFoto.Visible = false;
            multiEmpresa.Visible = false;
            tdCampos.Visible = false;
            this.imgFuncionario.Visible = false;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "ex", mensaje, HttpContext.Current.Request["REMOTE_HOST"], Convert.ToInt16(Session["empresa"]));
            GetEntidad();
        }
        private void CargarEmpresa()
        {

            try
            {
                DataView dvEmpresa = OrdenarEntidadSinEmpresa(CentidadMetodos.EntidadGet("gEmpresa", "ppa"), "razonSocial");
                dvEmpresa.RowFilter = "mcontrol=1 and activo=1";
                this.selEmpresas.DataSource = dvEmpresa;
                this.selEmpresas.DataValueField = "id";
                this.selEmpresas.DataTextField = "razonSocial";
                this.selEmpresas.DataBind();
                this.selEmpresas.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private void CargarCombos()
        {

            try
            {
                this.ddlTipoFuncionario.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("nFuncionarioTipo", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlTipoFuncionario.DataValueField = "codigo";
                this.ddlTipoFuncionario.DataTextField = "descripcion";
                this.ddlTipoFuncionario.DataBind();
                this.ddlTipoFuncionario.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }


            try
            {
                this.ddlProveedor.DataSource = funcionarios.ProveedoreesContratista(Convert.ToInt16(this.Session["empresa"]));
                this.ddlProveedor.DataValueField = "codigo";
                this.ddlProveedor.DataTextField = "descripcion";
                this.ddlProveedor.DataBind();
                this.ddlProveedor.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }

            try
            {
                this.ddlCentroCosto.DataSource = funcionarios.SeleccionaCentroCosto(Convert.ToInt16(this.Session["empresa"]));
                this.ddlCentroCosto.DataValueField = "descripcion";
                this.ddlCentroCosto.DataTextField = "descripcion";
                this.ddlCentroCosto.DataBind();
                this.ddlCentroCosto.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }

            try
            {
                this.ddlCargo.DataSource = funcionarios.SeleccionaCargos(Convert.ToInt16(this.Session["empresa"]));
                this.ddlCargo.DataValueField = "descripcion";
                this.ddlCargo.DataTextField = "descripcion";
                this.ddlCargo.DataBind();
                this.ddlCargo.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }


            try
            {
                this.ddlEPS.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("nEntidadEps", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlEPS.DataValueField = "codigo";
                this.ddlEPS.DataTextField = "descripcion";
                this.ddlEPS.DataBind();
                this.ddlEPS.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }

            try
            {
                this.ddlARL.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("nEntidadArp", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlARL.DataValueField = "codigo";
                this.ddlARL.DataTextField = "descripcion";
                this.ddlARL.DataBind();
                this.ddlARL.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }



            try
            {
                this.ddlRh.DataSource = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("gRh", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                this.ddlRh.DataValueField = "codigo";
                this.ddlRh.DataTextField = "descripcion";
                this.ddlRh.DataBind();
                this.ddlRh.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }

            try
            {
                DataView tipodocumento = CentidadMetodos.EntidadGet("gTipoDocumento", "ppa").Tables[0].DefaultView;
                tipodocumento.RowFilter = "empresa =" + this.Session["empresa"].ToString();
                this.ddlTipoID.DataSource = tipodocumento;
                this.ddlTipoID.DataValueField = "codigo";
                this.ddlTipoID.DataTextField = "descripcion";
                this.ddlTipoID.DataBind();
                this.ddlTipoID.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private void EntidadKey()
        {
            if (ddlTipoID.SelectedValue.Length > 0 & txtIdentificacion.Text.Length > 0)
            {
                object[] objKey = new object[] { txtIdentificacion.Text, Convert.ToInt16(Session["empresa"]), ddlTipoFuncionario.SelectedValue };
                try
                {
                    if (CentidadMetodos.EntidadGetKey("nFuncionario", "ppa", objKey).Tables[0].Rows.Count > 0)
                    {
                        MostrarMensaje("Funcionario " + Convert.ToString(this.txtIdentificacion.Text) + " ya se encuentra registrado");
                        CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
                        this.nibtnNuevo.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    ManejoErrorCatch(this, GetType(), ex);
                }
            }
        }
        private void Guardar()
        {
            string operacion = "inserta", proveedor = null, eps = null, arl = null, RH = null,
                funcionario = null, apellido1 = null, apellido2 = null, nombres = null,
                tipoID = null, cargo = null, ccosto = null, identificacion = null, descripcion = null, tipoFuncionario = null;
            DateTime fechaNacmiento, fechaRetiro;
            decimal salario = 0, valorCasino = 0;
            bool validaroperacion = false;

            try
            {

                if (chkBDasociada.Checked)
                {
                    DataView dvTercero = funcionarios.RetornaDatosTercero(ddlTercero.SelectedValue, Convert.ToInt32(Session["empresa"]));

                    if (dvTercero.Table.Rows.Count > 0)
                    {
                        tipoID = dvTercero.Table.Rows[0].ItemArray.GetValue(15).ToString();
                        apellido1 = dvTercero.Table.Rows[0].ItemArray.GetValue(3).ToString();
                        apellido2 = dvTercero.Table.Rows[0].ItemArray.GetValue(4).ToString();
                        nombres = dvTercero.Table.Rows[0].ItemArray.GetValue(5).ToString();
                        descripcion = dvTercero.Table.Rows[0].ItemArray.GetValue(6).ToString();
                    }
                    identificacion = ddlTercero.SelectedValue;
                    ccosto = ddlCentroCosto.SelectedItem.ToString();
                }
                else
                {
                    tipoID = ddlTipoID.SelectedValue;
                    apellido1 = txtApellido1.Text;
                    apellido2 = txtApellido2.Text;
                    nombres = txtNombres.Text;
                    ccosto = txtCentroCosto.Text;
                    identificacion = txtIdentificacion.Text;
                    descripcion = apellido1 + ' ' + apellido2 + ' ' + nombres;
                }

                proveedor = (ddlProveedor.SelectedValue.Length > 0) ? ddlProveedor.SelectedValue : null;
                arl = (ddlARL.SelectedValue.Length > 0) ? ddlARL.SelectedValue : null;
                eps = (ddlEPS.SelectedValue.Length > 0) ? ddlEPS.SelectedValue : null;
                RH = (ddlRh.SelectedValue.Length > 0) ? ddlRh.SelectedValue : null;
                Foto = (!chkValidaFoto.Checked) ? null : Foto;
                if (ddlCargo.Visible)
                    cargo = ddlCargo.SelectedItem.ToString();
                else
                    cargo = txtCargo.Text;


                fechaNacmiento = (txtFechaNacimiento.Text.Length > 0) ? Convert.ToDateTime(txtFechaNacimiento.Text) : DateTime.Now;
                fechaRetiro = (txtFechaRetiro.Text.Length > 0) ? Convert.ToDateTime(txtFechaRetiro.Text) : DateTime.Now;
                salario = (txvSalario.Text.Length > 0) ? Convert.ToDecimal(txvSalario.Text) : 0;


                using (TransactionScope ts = new TransactionScope())
                {
                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                        operacion = "actualiza";



                    object[] objValores1 = new object[]{
                                this.chkActivo.Checked,//@activo
                                apellido1,//@apellido1
                                apellido2,//@apellido2
                                arl,//@arl
                                cargo,//@cargo
                                ccosto,//@centroCosto
                                identificacion,//@codigo
                                chkConductor.Checked,//@conductor
                                descripcion,//@descripcion
                                Convert.ToInt16(Session["empresa"]),//@empresa
                                eps,//@eps
                                fechaNacmiento,//@fechaNacimiento
                                DateTime.Now,//@fechaRegistro
                                fechaRetiro,//@fechaRetiro
                                Foto,//@foto
                                chkBDasociada.Checked,//@manejaBD
                                chkFechaRetiro.Checked,//@manejaFechaRetiro
                                chkValidaFoto.Checked,//@manejaFoto
                                chkManejaLabores.Checked,//@manejaLabores
                                false,//@mCasino
                                false,//@multiEmpresa
                                nombres,//@nombres
                                chkOperadorLogistico.Checked,//@operadorLogistico
                                proveedor,//@proveedor
                                RH,//@RH
                                salario,//@salario
                                ddlSexo.SelectedValue,//@sexo
                                ddlTipoFuncionario.SelectedValue,//@tipo
                                tipoID,//@tipoIdentificacion
                                Session["usuario"].ToString(),//@usuario
                                chkValidaTurno.Checked,//@validaTurno
                                valorCasino//@valorCasino
                                };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("nFuncionarios", operacion, "ppa", objValores1))
                    {
                        case 0:
                            validaroperacion = guardaMultiEmpresa(identificacion, ddlTipoFuncionario.SelectedValue);
                            break;
                        case 1:
                            validaroperacion = true;
                            break;
                    }

                    if (validaroperacion)
                    {
                        ManejoError("Errores al insertar el registro. Operación no realizada", "I");
                        return;
                    }
                    ts.Complete();
                }
                ManejoExito("Datos ingresados satisfactoriamente", operacion.Substring(0, 1).ToUpper());
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private bool guardaMultiEmpresa(string funcionario, string tipo)
        {
            bool validaroperacion = false;
            object[] objValoresConcepto = new object[] { Convert.ToInt32(Session["empresa"]), funcionario, tipo };

            switch (CentidadMetodos.EntidadInsertUpdateDelete("nFuncionarioEmpresa", "elimina", "ppa", objValoresConcepto))
            {
                case 1:
                    validaroperacion = true;
                    break;
            }

            for (int x = 0; x < this.selEmpresas.Items.Count; x++)
            {

                if (this.selEmpresas.Items[x].Selected == true)
                {
                    object[] objValoresConceptoinserta = new object[] { Convert.ToInt32(Session["empresa"]), selEmpresas.Items[x].Value, funcionario, tipo };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("nFuncionarioEmpresa", "inserta", "ppa", objValoresConceptoinserta))
                    {
                        case 1:
                            validaroperacion = true;
                            break;
                    }
                }
            }
            return validaroperacion;
        }
        private void CargarTerceros()
        {
            try
            {
                this.ddlTercero.DataSource = funcionarios.SeleccionaTerceros(Convert.ToInt32(Session["empresa"]));
                this.ddlTercero.DataValueField = "codigo";
                this.ddlTercero.DataTextField = "cadena";
                this.ddlTercero.DataBind();
                this.ddlTercero.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private void BDinterna(bool valor)
        {
            lblTipoIdentificaicon.Visible = valor;
            lblIdentificacion.Visible = valor;
            lblPrimerNombre.Visible = valor;
            lblPrimerApellido.Visible = valor;
            lblSegundoApellido.Visible = valor;
            txtApellido1.Visible = valor;
            txtApellido2.Visible = valor;
            txtNombres.Visible = valor;
            ddlTipoID.Visible = valor;
            txtIdentificacion.Visible = valor;
            lblTercero.Visible = !valor;
            ddlTercero.Visible = !valor;
            txtIdentificacion.Enabled = valor;
            ddlCargo.Visible = !valor;
            txtCargo.Visible = valor;

        }
        private void contratista(bool valor)
        {
            ddlARL.Visible = valor;
            lblARP.Visible = valor;
            ddlEPS.Visible = valor;
            lblEPS.Visible = valor;
            lblProveedor.Visible = valor;
            ddlProveedor.Visible = valor;
            empleado(!valor);
            txtCentroCosto.Visible = false;
            ddlCentroCosto.Visible = false;
            txtCargo.Visible = false;
            ddlCargo.Visible = false;
            ddlTipoID.Enabled = valor;
            ddlSexo.Visible = valor;
            chkValidaFoto.Visible = valor;

        }
        private void empleado(bool valor)
        {
            ddlCentroCosto.Visible = chkBDasociada.Checked;
            txtCentroCosto.Visible = !chkBDasociada.Checked;
            txvSalario.Visible = valor;
            lblSalario.Visible = valor;
            lblCcosto.Visible = valor;
            lblCargo.Visible = valor;
            chkValidaTurno.Visible = valor;
            ddlTipoID.Enabled = valor;
            ddlRh.Visible = valor;
            ddlSexo.Visible = valor;
            chkValidaFoto.Visible = valor;
            lblRH.Visible = valor;
            txtFechaNacimiento.Visible = valor;
            lblFechaNacimiento.Visible = valor;
            txtFechaRetiro.Visible = valor;
            chkFechaRetiro.Visible = valor;

            if (chkBDasociada.Checked)
            {
                CargarTerceros();
                BDinterna(false);
            }
            else
                BDinterna(true);

        }
        private void otros(bool valor)
        {
            chkFechaRetiro.Visible = valor;
            txtFechaRetiro.Visible = valor;
            lblRH.Visible = valor;
            ddlRh.Visible = valor;
            chkConductor.Visible = valor;
            chkValidaTurno.Visible = valor;
            chkOperadorLogistico.Visible = valor;
            txtCentroCosto.Visible = valor;
            ddlCentroCosto.Visible = valor;
            txtFechaNacimiento.Visible = valor;
            lblFechaNacimiento.Visible = valor;
            chkManejaLabores.Visible = valor;
            txtCargo.Visible = valor;
            ddlTipoID.Enabled = valor;
            ddlRh.Visible = valor;
            ddlSexo.Visible = valor;
            chkValidaFoto.Visible = valor;
        }
        private void ManejoTipoFuncionario()
        {
            tdCampos.Visible = true;
            multiEmpresa.Visible = false;
            CcontrolesUsuario.HabilitarControles(this.tdCampos.Controls);
            CcontrolesUsuario.LimpiarControles(this.tdCampos.Controls);
            imgFuncionario.Visible = false;
            txtFechaRetiro.Enabled = false;
            fuFoto.Visible = false;
            txtIdentificacion.Enabled = false;


            if (chkBDasociada.Checked)
            {
                CargarTerceros();
                BDinterna(false);
            }
            else
                BDinterna(true);

            if (Convert.ToBoolean(TipoFuncionarioConfig(3)))
                contratista(true);

            if (Convert.ToBoolean(TipoFuncionarioConfig(4)))
            {
                contratista(false);
                empleado(true);
            }

            if (Convert.ToBoolean(TipoFuncionarioConfig(5)))
            {
                contratista(false);
                empleado(false);
                otros(false);
                ddlTipoID.Enabled = true;
                ddlSexo.Visible = true;
                chkValidaFoto.Visible = true;
            }

            if (Convert.ToBoolean(TipoFuncionarioConfig(6)))
            {
                contratista(false);
                empleado(false);
                otros(false);
                ddlTipoID.Enabled = true;
                ddlSexo.Visible = true;
                chkValidaFoto.Visible = true;
            }
            if (Convert.ToBoolean(TipoFuncionarioConfig(7)))
            {
                contratista(false);
                empleado(false);
                otros(false);
                ddlTipoID.Enabled = true;
                ddlSexo.Visible = true;
                chkValidaFoto.Visible = true;
            }
            ddlTercero.Enabled = true;
        }
        private object TipoFuncionarioConfig(int posicion)
        {
            object retorno = null;
            string cadena;
            char[] comodin = new char[] { '*' };
            int indice = posicion + 1;

            try
            {
                cadena = tipoFuncionario.TipoFuncionarioConfig(ddlTipoFuncionario.SelectedValue, Convert.ToInt16(Session["empresa"])).ToString();
                retorno = cadena.Split(comodin, indice).GetValue(posicion - 1);

                return retorno;
            }
            catch (Exception ex)
            {
                ManejoError("Error al recuperar posición de configuración de tipo de transacción. Correspondiente a: " + ex.Message, "C");

                return null;
            }
        }
        private void cargarFoto()
        {
            try
            {
                this.imgFuncionario.Visible = true;
                string urlFoto = string.Empty;
                if (Foto != null)
                {
                    urlFoto = "data:image/png;base64," + Convert.ToBase64String(Foto, Base64FormattingOptions.None);
                    this.imgFuncionario.ImageUrl = urlFoto;
                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }
        private void cargarDatosFuncionario(string tercero)
        {
            try
            {
                //DataView dvTerceros = funcionarios.SeleccionaTerceros(Convert.ToInt32(Session["empresa"]));
                //dvTerceros.RowFilter = "activo=1 and codigo='" + tercero + "'";
                foreach (DataRowView registro in funcionarios.RetornaDatosTercero(tercero, Convert.ToInt32(Session["empresa"])))
                {
                    txtIdentificacion.Text = registro["codigo"].ToString();
                    ddlTipoID.SelectedValue = registro["tipoId"].ToString();
                    txtApellido1.Text = registro["apellido1"].ToString();
                    txtApellido2.Text = registro["apellido2"].ToString();
                    txtNombres.Text = registro["nombres"].ToString();
                    descripcionFuncionario = registro["descripcion"].ToString();
                    ddlCargo.SelectedValue = registro["cargo"].ToString();
                    ddlCentroCosto.SelectedValue = registro["ccosto"].ToString();
                    txvSalario.Text = registro["salario"].ToString();
                    ddlSexo.SelectedValue = registro["sexo"].ToString();
                    txtFechaRetiro.Text = Convert.ToDateTime(registro["fecharetiro"].ToString()).ToShortDateString();
                    txtFechaNacimiento.Text = Convert.ToDateTime(registro["fecnacimiento"].ToString()).ToShortDateString();
                    txtFechaRetiro.Enabled = true;

                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
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
                if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(),
                   ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), Convert.ToInt16(Session["empresa"])) != 0)
                    this.nitxtBusqueda.Focus();
                else
                    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            }
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
            this.nibtnNuevo.Visible = false;
            this.Session["editar"] = true;
            ddlTipoID.Enabled = false;
            this.txtIdentificacion.Enabled = false;
            this.fuFoto.Visible = true;
            try
            {
                CargarCombos();
                CargarEmpresa();
                CargarTerceros();
                Foto = null;
                DataView dvTercero = funcionarios.RetornaDatosProspecto(this.gvLista.SelectedRow.Cells[2].Text.Trim(), gvLista.SelectedRow.Cells[4].Text, Convert.ToInt32(Session["empresa"]));

                if (dvTercero.Table.Rows.Count > 0)
                {

                    chkBDasociada.Checked = Convert.ToBoolean(dvTercero.Table.Rows[0].ItemArray.GetValue(27));
                    ddlTipoFuncionario.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(2).ToString();
                    chkBDasociada.Enabled = false;
                    ddlTipoFuncionario.Enabled = false;
                    ManejoTipoFuncionario();

                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(1) != null)
                    {
                        if (chkBDasociada.Checked)
                            ddlTercero.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(1).ToString();
                        txtIdentificacion.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(1).ToString();
                        ddlTercero.Enabled = false;
                        txtIdentificacion.Enabled = false;
                    }
                    int i = 3;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(i) != null)
                    {
                        ddlTipoID.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(i).ToString();
                        ddlTipoID.Enabled = false;
                    }
                    i++;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(i) != null)
                        ddlProveedor.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(i).ToString();
                    i++;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(i) != null)
                        txtApellido1.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(i).ToString();
                    i++;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(i) != null)
                        txtApellido2.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(i).ToString();
                    i++;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(i) != null)
                        txtNombres.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(i).ToString();
                    i += 2;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(i) != null)
                        ddlRh.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(i).ToString();
                    i++;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(i) != null)
                        ddlARL.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(i).ToString();
                    i++;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(i) != null)
                        ddlEPS.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(i).ToString();
                    i++;
                    string fec = dvTercero.Table.Rows[0].ItemArray.GetValue(i).ToString();
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(i) != null & dvTercero.Table.Rows[0].ItemArray.GetValue(i).ToString() != "")
                        txtFechaNacimiento.Text = Convert.ToDateTime(dvTercero.Table.Rows[0].ItemArray.GetValue(i).ToString()).ToShortDateString();
                    i++;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(i) != null)
                        chkActivo.Checked = Convert.ToBoolean(dvTercero.Table.Rows[0].ItemArray.GetValue(i));
                    i++;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(i) != null)
                        chkValidaTurno.Checked = Convert.ToBoolean(dvTercero.Table.Rows[0].ItemArray.GetValue(i));
                    i++;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(i) != null)
                        chkConductor.Checked = Convert.ToBoolean(dvTercero.Table.Rows[0].ItemArray.GetValue(i));
                    i++;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(i) != null)
                        chkOperadorLogistico.Checked = Convert.ToBoolean(dvTercero.Table.Rows[0].ItemArray.GetValue(i));
                    i++;
                    i++;
                    i++;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(i) != null)
                        txtCargo.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(i).ToString();

                    if (chkBDasociada.Checked)
                    {
                        if (dvTercero.Table.Rows[0].ItemArray.GetValue(19) != null)
                            ddlCargo.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(19).ToString();
                    }

                    i++;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(i) != null & dvTercero.Table.Rows[0].ItemArray.GetValue(i).ToString() != "")
                        chkFechaRetiro.Checked = Convert.ToBoolean(dvTercero.Table.Rows[0].ItemArray.GetValue(i));
                    i++;

                    string fechaRetiro = dvTercero.Table.Rows[0].ItemArray.GetValue(i).ToString();
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(i) != null & fechaRetiro.Length > 0)
                        txtFechaRetiro.Text = Convert.ToDateTime(dvTercero.Table.Rows[0].ItemArray.GetValue(i).ToString()).ToShortDateString();
                    txtFechaRetiro.Enabled = chkFechaRetiro.Checked;
                    txtFechaRetiro.Text = (chkFechaRetiro.Checked) ? Convert.ToDateTime(txtFechaRetiro.Text).ToShortDateString() : "";
                    i++;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(i) != null)
                        txvSalario.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(i).ToString();

                    i++;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(i) != null)
                        txtCentroCosto.Text = dvTercero.Table.Rows[0].ItemArray.GetValue(i).ToString();
                    if (chkBDasociada.Checked)
                    {
                        try
                        {
                            if (dvTercero.Table.Rows[0].ItemArray.GetValue(23) != null)
                                ddlCentroCosto.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(23).ToString();
                        }
                        catch (Exception ex)
                        { }
                    }

                    i++;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(i) != null & dvTercero.Table.Rows[0].ItemArray.GetValue(i).ToString() != "")
                        ddlSexo.SelectedValue = dvTercero.Table.Rows[0].ItemArray.GetValue(i).ToString();
                    i++;
                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(i) != null)
                        chkManejaLabores.Checked = Convert.ToBoolean(dvTercero.Table.Rows[0].ItemArray.GetValue(i));
                    i++;


                    if (dvTercero.Table.Rows[0].ItemArray.GetValue(28) != null & dvTercero.Table.Rows[0].ItemArray.GetValue(28).ToString() != "")
                        chkValidaFoto.Checked = Convert.ToBoolean(dvTercero.Table.Rows[0].ItemArray.GetValue(28));
                    fuFoto.Visible = chkValidaFoto.Checked;

                    if (!string.IsNullOrWhiteSpace(Server.HtmlDecode(dvTercero.Table.Rows[0].ItemArray.GetValue(29).ToString())))
                    {
                        Foto = (dvTercero.Table.Rows[0].ItemArray.GetValue(29) is byte[]) ? (byte[])dvTercero.Table.Rows[0].ItemArray.GetValue(29) : null;
                        cargarFoto();
                    }
                }

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "E", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }
            string operacion = "elimina";
            try
            {
                object[] objValores = new object[] {
                            Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text)   ,  //                @codigo varchar
                             Convert.ToInt16(Session["empresa"]),  //@empresa    int
                                   Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[4].Text)   //@tipo   varchar
            };

                if (CentidadMetodos.EntidadInsertUpdateDelete("nFuncionarios", operacion, "ppa", objValores) == 0)
                    ManejoExito("Datos eliminados satisfactoriamente", "E");
                else
                    ManejoError("Errores al eliminar el registro. Operación no realizada", "E");
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(this, GetType(), ex);
            }
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
        protected void chkValidaFoto_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkValidaFoto.Checked)
            {
                this.imgFuncionario.ImageUrl = null;
                imgFuncionario.Visible = false;
            }
            fuFoto.Visible = chkValidaFoto.Checked;
        }
        protected void nibtnBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nibtnNuevo.Visible = true;
            this.imgFuncionario.Visible = false;
            this.fuFoto.Visible = false;
            multiEmpresa.Visible = false;
            GetEntidad();
        }
        protected void nibtnNuevo_Click(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                   nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(Page.Controls);

            CcontrolesUsuario.LimpiarControles(tbDatosDetalle.Controls);
            this.nibtnNuevo.Visible = false;
            this.Session["editar"] = false;
            CargarCombos();
            txtIdentificacion.Text = "";
            chkValidaFoto.Checked = true;
            chkBDasociada.Checked = true;
            chkBDasociada.Enabled = false;
        }
        protected void nibtnCancelar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            this.nibtnNuevo.Visible = true;
            tdCampos.Visible = false;
            this.fuFoto.Visible = false;
            this.imgFuncionario.Visible = false;
        }
        protected void nibtnGuardar_Click(object sender, EventArgs e)
        {
            string funcionario = null;
            if (ddlProveedor.SelectedValue.Trim().Length == 0 & ddlProveedor.Visible == true)
            {
                MostrarMensaje("Debe seleccionar un proveedor si es contratista");
                return;
            }
            if (ddlRh.SelectedValue.Trim().Length == 0 & ddlRh.Visible == true)
            {
                MostrarMensaje("Debe seleccionar un tipo de sangre...");
                return;
            }
            if (ddlCentroCosto.SelectedValue.Length == 0 & ddlCentroCosto.Visible == true)
            {
                MostrarMensaje("Debe seleccionar un centro de costo...");
                return;
            }

            if (ddlCargo.SelectedValue.Length == 0 & ddlCargo.Visible == true)
            {
                MostrarMensaje("Debe seleccionar un cargo...");
                return;
            }
            if (ddlSexo.SelectedValue.Trim().Length == 0 & ddlSexo.Visible == true)
            {
                MostrarMensaje("Debe seleccionar el sexo...");
                return;
            }
            if (ddlTipoID.SelectedValue.Trim().Length == 0 & ddlTipoID.Visible == true)
            {
                MostrarMensaje("Debe seleccionar un tipo de identificación...");
                return;
            }
            if (ddlEPS.SelectedValue.Trim().Length == 0 & ddlEPS.Visible == true)
            {
                MostrarMensaje("Debe seleccionar una EPS si es contratista...");
                return;
            }
            if (ddlARL.SelectedValue.Trim().Length == 0 & ddlARL.Visible == true)
            {
                MostrarMensaje("Debe seleccionar una ARP si es contratista...");
                return;
            }
            if (chkFechaRetiro.Checked & chkFechaRetiro.Visible)
            {
                if (txtFechaRetiro.Text.Length == 0)
                {
                    MostrarMensaje("Si maneja fecha retiro, debe selecionar una fecha...");
                    return;
                }
            }
            if (!chkBDasociada.Checked)
            {
                if (txtApellido1.Text.Length == 0 || txtApellido2.Text.Length == 0 || txtNombres.Text.Length == 0 || txtIdentificacion.Text.Length == 0)
                {
                    MostrarMensaje("Los apellidos, nombre o identificación no pueden ser vacios");
                    return;
                }
            }
            
            if (Foto == null & chkValidaFoto.Checked)
            {
                MostrarMensaje("Debe seleccionar la foto del funcionario");
                return;
            }

            funcionario = chkBDasociada.Checked ? ddlTercero.SelectedValue.Trim() : txtIdentificacion.Text;

            if (funcionarios.VerificaFuncionarioExistente(empresa: Convert.ToInt16(Session["empresa"]), funcionario: funcionario, tipo: ddlTipoFuncionario.SelectedValue) == 1)
            {
                MostrarMensaje("El funcionario ya se encuetra registrado con otro tipo");
                return;
            }

            Guardar();
        }
        protected void ddlTipoFuncionario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoFuncionario.SelectedValue.Length > 0)
                ManejoTipoFuncionario();
            else
                tdCampos.Visible = false;
        }
        protected void chkBDasociada_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlTipoFuncionario.SelectedValue.Length > 0)
                ManejoTipoFuncionario();
        }
        protected void chkFechaRetiro_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkFechaRetiro.Checked)
                txtFechaRetiro.Text = "";
            txtFechaRetiro.Enabled = chkFechaRetiro.Checked;
        }
        
        protected void hiddenCommand_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.fuFoto.HasFile)
                {
                    Stream fs = fuFoto.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Foto = br.ReadBytes((int)fs.Length);
                }
                else
                {
                    MostrarMensaje("Debe seleccionar la foto del funcionario");
                    return;
                }
                this.imgFuncionario.Visible = true;
                string urlFoto = string.Empty;
                if (Foto != null)
                {
                    urlFoto = "data:image/png;base64," + Convert.ToBase64String(Foto, Base64FormattingOptions.None);
                    this.imgFuncionario.ImageUrl = urlFoto;
                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }
        protected void ddlTipoID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoID.SelectedValue.Length > 0)
            {
                txtIdentificacion.Enabled = true;
                txtIdentificacion.Focus();
            }
            else
                txtIdentificacion.Enabled = false;
            EntidadKey();
        }
        protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
        {
            string funcionario = chkBDasociada.Checked ? ddlTercero.SelectedValue.Trim() : txtIdentificacion.Text;

            if (funcionarios.VerificaFuncionarioExistente(empresa: Convert.ToInt16(Session["empresa"]), funcionario: funcionario, tipo: ddlTipoFuncionario.SelectedValue) == 1)
            {
                MostrarMensaje("El funcionario ya se encuetra registrado en la base de datos");
                txtIdentificacion.Text = "";
                txtIdentificacion.Focus();
                return;
            }
        }
        
        protected void ddlTercero_SelectedIndexChanged(object sender, EventArgs e)
        {
            string funcionario = chkBDasociada.Checked ? ddlTercero.SelectedValue.Trim() : txtIdentificacion.Text;

            if (funcionarios.VerificaFuncionarioExistente(empresa: Convert.ToInt16(Session["empresa"]), funcionario: funcionario, tipo: ddlTipoFuncionario.SelectedValue) == 1)
            {
                MostrarMensaje("El funcionario ya se encuetra registrado en la base de datos");
                ddlTercero.ClearSelection();
                return;
            }
            else
                cargarDatosFuncionario(ddlTercero.SelectedValue);
        }



        #endregion Eventos
    }
}