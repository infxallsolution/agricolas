using Almacen.WebForms.App_Code.General;
using Almacen.WebForms.App_Code.Modelo;
using Almacen.WebForms.App_Code.Transaccion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Almacen.WebForms.Formas.Ptransaccion
{
    public partial class ConteoFisico : BasePage
    {

        #region Instancias
        CconteoFisico CconteoFisico = new CconteoFisico();
        #endregion Instancias


        #region Metodos


        private object[] validaciones()
        {
            if (txtObservacion.Text.Trim().Length == 0) return new object[] { 1, txtObservacion };
            if (ddlBodega.SelectedValue.Trim().Length == 0) return new object[] { 1, ddlBodega };
            if (txtConteos.Text.Trim().Length == 0) return new object[] { 1, txtConteos };
            if (ddlCriterio.SelectedValue.Trim().Length == 0 & chkCiclico.Checked) return new object[] { 1, txtConteos };
            return null;
        }


        private void GetEntidad()
        {
            try
            {
                switch (CconteoFisico.anularConteo(Convert.ToInt32(this.Session["empresa"]), txtCodigo.Text))
                {
                    case 0:

                        break;

                }

            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }
        private void ManejoExito(string mensaje, string operacion)
        {
            ManejoError(this, GetType(), mensaje, "info");

            InhabilitarControles(Page.Controls);
            LimpiarControles(Page.Controls);
            nilbNuevo.Visible = true;
            seguridad.InsertaLog(Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + Page.ToString(),
                "ex", mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));
            //GetEntidad();
        }
        private void CargarCombos()
        {
            try
            {
                ddlBodega.DataSource = CentidadMetodos.EntidadGet("iBodega", "ppa").Tables[0].AsEnumerable().Where(x => x.Field<bool>("mexistencias") == true
                 && x.Field<int>("empresa") == Convert.ToInt16(this.Session["empresa"])
                ).Select(y => new
                {
                    codigo = y.Field<string>("codigo"),
                    descripcion = y.Field<string>("descripcion"),
                    descorta = y.Field<string>("desCorta"),
                    cadena = string.Format("{0}-{1}", y.Field<string>("codigo"), y.Field<string>("descripcion"))
                }).ToList();

                ddlBodega.DataValueField = "codigo";
                ddlBodega.DataTextField = "cadena";
                ddlBodega.DataBind();
                ddlBodega.Items.Insert(0, new ListItem() { Value = "", Text = "" });
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }

        }
        private void Guardar()
        {
            string operacion = "inserta";

            try
            {
                if (Convert.ToBoolean(this.Session["editar"]) == true)
                    operacion = "actualiza";

                object[] objValores = new object[]{
                   chkFisico.Checked,    // @activo	bit
                   ddlBodega.SelectedValue,
                   txtCodigo.Text,     //@codigo	varchar
                   txtObservacion.Text,     //@descripcion	varchar
                   Convert.ToInt16(this.Session["empresa"]),     //@empresa	int
                   DateTime.Now,     //@fechaRegistro	datetime
                   this.Session["usuario"].ToString()//@usuario	varchar
            };

                switch (CentidadMetodos.EntidadInsertUpdateDelete("iDestino", operacion, "ppa", objValores))
                {
                    case 0:
                        ManejoExito("Datos insertados satisfactoriamente", operacion.Substring(0, 1).ToUpper());
                        break;

                    case 1:

                        ManejoError("Errores al insertar el registro. Operación no realizada", operacion.Substring(0, 1).ToUpper());
                        break;
                }
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);

            }
        }
        private void EntidadKey()
        {
            object[] objKey = new object[] {
            this.txtCodigo.Text,
            Convert.ToInt16(Session["empresa"])
        };

            try
            {
                if (CentidadMetodos.EntidadGetKey("iDestino", "ppa", objKey).Tables[0].Rows.Count > 0)
                {

                    CcontrolesUsuario.InhabilitarControles(this.Page.Controls);

                    this.nilbNuevo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }


        #endregion Metodos

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Session["empresa"] = 2;
            this.Session["usuario"] = "rgomez";
            hfEmpresa.Value = "2";
            hfUsuario.Value = "rgomez";




            //if (!IsPostBack)
            //{
            //    var conteo = CconteoFisico.seleccionaConteosAbiertos(Convert.ToInt32(this.Session["empresa"]));

            //    if (conteo.Count > 0)
            //    {
            //        var conteoEncabezado = conteo.Table.AsEnumerable().Select(x =>
            //      new
            //      {
            //          codigo = x.Field<int>("codigo"),
            //          bodega = x.Field<string>("bodega"),
            //          ciclico = x.Field<bool>("ciclico"),
            //          fisico = x.Field<bool>("fisico"),
            //          descripcion = x.Field<string>("descripcion"),
            //          fecha = x.Field<DateTime>("fecha"),
            //          noConteos = x.Field<int>("noConteos"),
            //          linea = x.Field<string>("linea"),
            //          conteos = x.Field<int>("Conteos")
            //      }
            //        ).ToList();

            //        CcontrolesUsuario.HabilitarControles(Page.Controls);
            //        CcontrolesUsuario.LimpiarControles(Page.Controls);
            //        nuevo();
            //        conteoEncabezado.ForEach(y =>
            //        {
            //            txtCodigo.Text = y.codigo.ToString(); txtCodigo.Enabled = false;
            //            ddlBodega.SelectedValue = y.bodega.ToString(); ddlBodega.Enabled = false;
            //            chkCiclico.Checked = y.ciclico; chkCiclico.Enabled = false;
            //            chkFisico.Checked = y.fisico; chkCiclico.Enabled = false;
            //            txtDescripcion.Text = y.descripcion.ToString(); txtDescripcion.Enabled = false;
            //            hfConteo.Value = (y.conteos).ToString();
            //            txtConteos.Text = y.noConteos.ToString(); txtConteos.Enabled = false;

            //        });
            //        var dvConteo = CconteoFisico.seleccionaConteoFisico(numero: txtCodigo.Text, empresa: Convert.ToInt32(Session["empresa"]));
            //        gvLista.DataSource = dvConteo;
            //        gvLista.DataBind();

            //        lbRegistrar.Visible = false;
            //        lblConteo.Visible = true;
            //        lblItemsContados.Visible = true;
            //        lbCancelar.Visible = false;
            //        lbAnular.Visible = true;

            //    }

            //}

            //if (this.Session["usuario"] == null)
            //if (this.Session["usuario"] == null)
            //{
            //    this.Response.Redirect("~/WebForms/Inicio.aspx");
            //}
            //else
            //{
            //if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(),
            //        ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(),
            //        Convert.ToInt16(this.Session["empresa"])) != 0)
            //{
            //    this.nitxtBusqueda.Focus();

            //}
            //else
            //{
            //    ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
            //}
            //}
        }
        protected void imbCliente_Click(object sender, ImageClickEventArgs e)
        {
            if (seguridad.VerificaAccesoPagina(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), "Nivel.aspx", Convert.ToInt16(this.Session["empresa"])) != 0)
                this.Response.Redirect("Nivel.aspx");
            else
                ManejoError("Usuario no autorizado para ingresar a esta página", "IN");
        }
        protected void ddlNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtCodigo.Focus();
        }
        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            EntidadKey();
            txtObservacion.Focus();
        }
        protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(),
                                editar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", editar);
                return;
            }
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);

            this.nilbNuevo.Visible = false;
            this.Session["editar"] = true;

            txtCodigo.Enabled = false;
            this.txtObservacion.Focus();

            try
            {
                CargarCombos();

                if (this.gvLista.SelectedRow.Cells[2].Text != "&nbsp;")
                    this.txtCodigo.Text = this.gvLista.SelectedRow.Cells[2].Text;
                else
                    this.txtCodigo.Text = "";


                if (this.gvLista.SelectedRow.Cells[3].Text != "&nbsp;")
                    this.txtObservacion.Text = Server.HtmlDecode(this.gvLista.SelectedRow.Cells[3].Text);
                else
                    this.txtObservacion.Text = "";

                if (this.gvLista.SelectedRow.Cells[4].Text != "&nbsp;")
                    this.ddlBodega.SelectedValue = this.gvLista.SelectedRow.Cells[4].Text.Trim();

                foreach (Control objControl in this.gvLista.SelectedRow.Cells[5].Controls)
                {
                    this.chkFisico.Checked = ((CheckBox)objControl).Checked;
                }
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(),
                                  eliminar, Convert.ToInt16(this.Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", eliminar);
                return;
            }
            string operacion = "elimina";
            try
            {
                object[] objValores = new object[] { Convert.ToString(this.gvLista.Rows[e.RowIndex].Cells[2].Text), Convert.ToInt16(this.Session["empresa"]) };

                if (CentidadMetodos.EntidadInsertUpdateDelete("iDestino", operacion, "ppa", objValores) == 0)
                    ManejoExito("Datos eliminados satisfactoriamente", "E");
                else
                    ManejoError("Errores al eliminar el registro. Operación no realizada", "E");
            }
            catch (Exception ex)
            {
                if (ex.HResult.ToString() == "-2146233087")
                {
                    ManejoError("El código ('" + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[2].Text)) +
                    "' - ' " + Convert.ToString(Server.HtmlDecode(this.gvLista.Rows[e.RowIndex].Cells[3].Text)) + "') tiene una asociación, no es posible eliminar el registro.", "E");
                }
                else
                    CerroresGeneral.ManejoErrorCatch(this, GetType(), ex);
            }
        }
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            GetEntidad();
            gvLista.DataBind();
        }
        protected void niimbBuscar_Click(object sender, EventArgs e)
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = true;
            GetEntidad();
        }
        protected void nilbNuevo_Click(object sender, EventArgs e)
        {
            //if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["modulo"].ToString(), nombrePaginaActual(),
            //                    insertar, Convert.ToInt16(this.Session["empresa"])) == 0)
            //{
            //    ManejoError("Usuario no autorizado para ejecutar esta operación", insertar);
            //    return;
            //}
            nuevo();

        }

        private void nuevo()
        {
            CcontrolesUsuario.HabilitarControles(this.Page.Controls);
            this.nilbNuevo.Visible = false;
            this.Session["editar"] = false;
            CargarCombos();
            this.txtCodigo.Enabled = false;
            txtCodigo.Text = CconteoFisico.Consecutivo(Convert.ToInt32(this.Session["empresa"])).ToString();
            txtCodigo.Focus();
            chkFisico.Checked = true;
            txtConteos.Text = "1";
            lblConteo.Visible = false;
            lblItemsContados.Visible = false;
            lbRegistrar.Visible = true;
            lblInventario.Visible = false;
            ddlInventario.Visible = false;
            ddlCriterio.Visible = false;
            chkCriterio.Visible = false;
            ddlCriterio.Visible = false;
            lbEmpezarConteo.Visible = false;
            lbAnular.Visible = false;
            ddlMayor.Visible = false;
            lblMayor.Visible = false;
        }

        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            cancelar();
        }

        private void cancelar()
        {
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.gvLista.DataSource = null;
            this.gvLista.DataBind();
            nilbNuevo.Visible = true;
            btnGuardarItems.Visible = false;
        }

        protected void gvLista_DataBound(object sender, EventArgs e)
        {
            if (this.gvLista.HeaderRow != null)
                this.gvLista.HeaderRow.TableSection = TableRowSection.TableHeader;
        }



        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public static string GuardarConteo(List<MguardarConteo> conteos)
        {
            try
            {
                var retorno = "0";

                foreach (var i in conteos)
                {
                    CconteoFisico cconteoFisico = new CconteoFisico();
                    switch (cconteoFisico.guardaItemConteo(empresa: i.empresa, consecutivo: i.consecutivo, item: i.item, conteo: Convert.ToDecimal(i.conteo), noConteo: Convert.ToInt16(i.noConteo), usuario: i.usuario))
                    {
                        case 1:
                            retorno = "1";
                            break;
                        default:
                            break;
                    }
                }
                return retorno;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        [WebMethod]
        public static string ConsultarConteo(string item, string consecutivo, decimal conteo, int noConteo, int empresa, string usuario)
        {
            try
            {
                CconteoFisico cconteoFisico = new CconteoFisico();

                var dv = cconteoFisico.itemsRegistrados(empresa: empresa, numero: consecutivo, noConteo: noConteo);
                var conteos = dv.Table.AsEnumerable().Select(x => new
                {
                    item = x.Field<int>("item"),
                    conteo = x.Field<Double>("conteo"),
                    itemsInventario = x.Field<int>("itemsInventario")
                }).ToList();

                return JsonConvert.SerializeObject(conteos);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        protected void lbCerrar_Click(object sender, EventArgs e)
        {
            try
            {

                InhabilitarControles(Page.Controls);
                LimpiarControles(Page.Controls);
                lblInventario.Visible = true;
                ddlInventario.Visible = true;

            }
            catch (Exception ex)
            {


            }

        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                var validar = false;
                using (var ts = new TransactionScope())
                {

                    var val = validaciones();

                    if (val != null)
                    {
                        var control = (Control)val[1];
                        MostrarMensaje(string.Format("Debe ingresar el campo {0}", (control).ID.ToString().Substring(3, (control).ID.ToString().Length - 3)));
                        return;
                    }

                    string _codigo = CconteoFisico.Consecutivo(Convert.ToInt32(Session["empresa"])).ToString();
                    string _linea = (ddlCriterio.Visible) ? ddlCriterio.SelectedValue.Trim() : null;
                    txtCodigo.Text = _codigo;

                    switch (CconteoFisico.procesarConteo(empresa: Convert.ToInt32(Session["empresa"]), codigo: _codigo, bodega: ddlBodega.SelectedValue.Trim(),
                        fisico: chkCiclico.Checked, ciclico: chkCiclico.Checked, descripcion: txtObservacion.Text.Trim(), fecha: DateTime.Now,
                        usuario: this.Session["usuario"].ToString(), linea: _linea, noConteos: Convert.ToInt32(txtConteos.Text)
                        ))
                    {
                        case 1:
                            validar = true;
                            break;
                        case 0:
                            ts.Complete();
                            //var dvConteo = CconteoFisico.seleccionaConteoFisico(numero: _codigo, empresa: Convert.ToInt32(Session["empresa"]));
                            //gvLista.DataSource = dvConteo;
                            //gvLista.DataBind();

                            //ddlBodega.Enabled = false;
                            //ddlLinea.Enabled = false;
                            //txtConteos.Enabled = false;
                            //chkCiclico.Enabled = false;
                            //chkFisico.Enabled = false;
                            //hfConteo.Value = "0";
                            //lblConteo.Text = string.Format("Conteo #{0} de {1}", hfConteo.Value, txtConteos.Text);
                            //lblItemsContados.Text = string.Format("Items contados :  {0} de {1}", 0, gvLista.Rows.Count);
                            //lblConteo.Visible = true;
                            //lblItemsContados.Visible = true;
                            //txtDescripcion.Enabled = false;
                            //lbRegistrar.Visible = false;
                            break;
                    }
                }

                if (!validar)
                {
                    ManejoExito(string.Format("Inventario #{0} guardado satisfactoriamente #", txtCodigo.Text), "I");
                }
                else
                {
                    MostrarMensaje("Error al ingresar la información");
                }
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        protected void nilbContar_Click(object sender, EventArgs e)
        {
            try
            {

                cancelar();

                LimpiarControles(Page.Controls);
                lblInventario.Visible = true;
                ddlInventario.Visible = true;
                ddlInventario.Enabled = true;
                chkCriterio.Visible = true;
                ddlCriterio.Visible = true;
                ddlCriterio.Enabled = false;
                lblConteo.Visible = false;
                lblItemsContados.Visible = false;
                chkCriterio.Enabled = true;
                ddlMayor.Enabled = true;
                ddlCriterio.Enabled = false;
                ddlMayor.Visible = true;
                ddlMayor.Enabled = false;
                lblMayor.Visible = true;
                chkCriterio.Checked = false;
                lbCancelar.Visible = true;
                lblNoConteo.Visible = true;
                txtConteos.Visible = true;
                txtConteos.Text = "1";
                lblLeyendaConteo.Visible = true;



                var conteosAbiertos = CconteoFisico.seleccionaConteosAbiertos(Convert.ToInt32(this.Session["empresa"]))
                       .Table.AsEnumerable().Select(x =>
                    new
                    {
                        codigo = x.Field<int>("codigo"),
                        bodega = x.Field<string>("bodega"),
                        ciclico = x.Field<bool>("ciclico"),
                        fisico = x.Field<bool>("fisico"),
                        descripcion = x.Field<string>("descripcion"),
                        noConteos = x.Field<int>("noConteos"),
                        cadena = string.Format("Inventario #{0}, Bodega:{1}, Tipo de inventario: {2} ", x.Field<int>("codigo"), x.Field<string>("nombreBodega"), x.Field<bool>("fisico") ? "Fisico" : "Ciclico")
                    }
                    ).ToList();

                DataView plan = CentidadMetodos.EntidadGet("iPlanItem", "ppa").Tables[0].DefaultView;

                var iplan = plan.Table.AsEnumerable().Where(y => y.Field<int>("empresa") == Convert.ToInt32(this.Session["empresa"]))
                    .Select(y => new { codigo = y.Field<string>("codigo"), descripcion = y.Field<string>("descripcion") }).ToList();

                ddlCriterio.DataSource = iplan;
                ddlCriterio.DataValueField = "codigo";
                ddlCriterio.DataTextField = "descripcion";
                ddlCriterio.DataBind();
                ddlCriterio.Items.Insert(0, new ListItem("", ""));

                ddlInventario.DataSource = conteosAbiertos;
                ddlInventario.DataValueField = "codigo";
                ddlInventario.DataTextField = "cadena";
                ddlInventario.DataBind();
                ddlInventario.Items.Insert(0, new ListItem("", ""));
                lbEmpezarConteo.Visible = true;
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }


        protected void chkCriterio_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                mcriterio();

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }

        }

        private void mcriterio()
        {
            ddlCriterio.ClearSelection();
            ddlMayor.ClearSelection();
            ddlMayor.Enabled = chkCriterio.Checked;
            ddlCriterio.Enabled = chkCriterio.Checked;
            ddlMayor.Enabled = chkCriterio.Checked;
        }

        protected void ddlCriterio_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCriterio.SelectedValue.Trim().Length == 0)
                {
                    MostrarMensaje("Ingrese un criterio válido");
                    return;
                }

                var mayores = CentidadMetodos.EntidadGet("iMayorItem", "ppa").Tables[0].DefaultView.Table.AsEnumerable().Where(x => x.Field<int>("empresa") == Convert.ToInt32(this.Session["empresa"]) && x.Field<string>("planes") == ddlCriterio.SelectedValue.Trim())
                        .Select(x => new
                        {
                            codigo = x.Field<string>("codigo"),
                            descripcion = x.Field<string>("descripcion")
                        });
                ddlMayor.DataSource = mayores;
                ddlMayor.DataTextField = "descripcion";
                ddlMayor.DataValueField = "codigo";
                ddlMayor.DataBind();
                ddlMayor.Items.Insert(0, new ListItem("", ""));

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        protected void lbEmpezarConteo_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlInventario.SelectedValue.Trim().Length == 0)
                {
                    MostrarMensaje("Seleccione un inventario");
                }

                var dvConteo = CconteoFisico.seleccionaConteoFisico(numero: ddlInventario.SelectedValue.Trim(), empresa: Convert.ToInt32(Session["empresa"]), plan: ddlCriterio.SelectedValue.Trim().Length == 0 ? null : ddlCriterio.SelectedValue.Trim(),
                    mayor: ddlMayor.SelectedValue.Trim().Length == 0 ? null : ddlMayor.SelectedValue.Trim()
                    );
                gvLista.DataSource = dvConteo;
                gvLista.DataBind();
                btnGuardarItems.Visible = true;
                lblItemsContados.Visible = true;
                lblConteo.Visible = true;
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }
    }
}