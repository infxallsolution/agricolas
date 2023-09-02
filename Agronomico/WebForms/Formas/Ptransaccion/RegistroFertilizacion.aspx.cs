using Agronomico.seguridadinfos;
using Agronomico.WebForms.App_Code.Administracion;
using Agronomico.WebForms.App_Code.General;
using Agronomico.WebForms.App_Code.Transaccion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agronomico.WebForms.Formas.Ptransaccion
{
    public partial class RegistroFertilizacion : BasePage
    {

        #region Instancias

        CentidadMetodos CentidadMetodos = new CentidadMetodos();
        Security seguridad = new Security();
        List<Ctercero> listaTerceros = new List<Ctercero>();
        Ccuadrillas cuadrillas = new Ccuadrillas();
        Ctercero terceros;
        CtransaccionFertilizante novedadTransaccion = new CtransaccionFertilizante();
        Cseccion seccion = new Cseccion();
        CIP ip = new CIP();
        Ctransacciones transacciones = new Ctransacciones();
        Cnovedad Cnovedad = new Cnovedad();
        CListaPrecios listaPrecios = new CListaPrecios();
        CtipoTransaccion tipoTransaccion = new CtipoTransaccion();
        Cbascula bascula = new Cbascula();
        Clotes lotes = new Clotes();
        Cempresa empresa = new Cempresa();
        string numerotransaccion = "";
        Cnovedad novedad = new Cnovedad();
        List<Csubtotal> subtotal = new List<Csubtotal>();
        Coperadores operadores = new Coperadores();
        Cperiodos periodo = new Cperiodos();
        CpromedioPeso peso = new CpromedioPeso();
        List<CtransaccionFertilizante> listaTransaccionFertilizante = new List<CtransaccionFertilizante>();
        CtransaccionFertilizante trafer = new CtransaccionFertilizante();
        Clotes lot = null;
        private List<Clotes> listaLotes
        {
            get { object o = ViewState["listaLotes"]; return (o == null) ? null : (List<Clotes>)o; }
            set { ViewState["listaLotes"] = value; }
        }

        private List<CtransaccionFertilizante> listaTransaccion
        {
            get { object o = Session["listaTransaccion"]; return (o == null) ? null : (List<CtransaccionFertilizante>)o; }
            set { Session["listaTransaccion"] = value; }
        }

        private List<CtransaccionFertilizante> listaNovedadesTransaccion
        {
            get { object o = Session["listaNovedadesTransaccion"]; return (o == null) ? null : (List<CtransaccionFertilizante>)o; }
            set { Session["listaNovedadesTransaccion"] = value; }
        }


        #endregion Instancias

        #region Metodos


        private void referencia()
        {
            if (ddlReferencia.Visible)
            {
                try
                {
                    this.ddlReferencia.DataSource = trafer.ejecutaReferencia(Convert.ToInt16(Session["empresa"]), trafer.RetornaDsReferenciaTransaccion(ddlTipoDocumento.SelectedValue.Trim(), Convert.ToInt32(this.Session["empresa"])));
                    this.ddlReferencia.DataValueField = "numero";
                    this.ddlReferencia.DataTextField = "cadena";
                    this.ddlReferencia.DataBind();
                    this.ddlReferencia.Items.Insert(0, new ListItem("", ""));
                }
                catch (Exception ex)
                {
                    ManejoError("Error al cargar referencia. Correspondiente a: " + ex.Message, "C");
                }
            }
        }

        private void cargarNovedades()
        {
            try
            {
                DataView dvNovedad = CentidadMetodos.EntidadGet("anovedad", "ppa").Tables[0].DefaultView;
                dvNovedad.RowFilter = "claseLabor=6 and empresa =" + Session["empresa"].ToString();
                this.ddlNovedad.DataSource = dvNovedad;
                this.ddlNovedad.DataValueField = "codigo";
                this.ddlNovedad.DataTextField = "descripcion";
                this.ddlNovedad.DataBind();
                this.ddlNovedad.Items.Insert(0, new ListItem("Seleccione una opción", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar Novedades. Correspondiente a: " + ex.Message, "C");
            }
        }

        private void CargaCampos()
        {
            try
            {
                DataView dvCampo = transacciones.GetCamposEntidades("vSeleccionaTransaccionCompletaLabores", "");
                dvCampo.RowFilter = "name   in  ('numero','tipo','fecha', 'finca', 'seccion', 'observacion', 'empleado')";
                this.niddlCampo.DataSource = dvCampo;
                this.niddlCampo.DataValueField = "name";
                this.niddlCampo.DataTextField = "name";
                this.niddlCampo.DataBind();
                this.niddlCampo.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos para edición. Correspondiente a: " + ex.Message, "C");
            }
        }
        private void cargarComboxDetalle()
        {
            cargarSesiones();
            cargarLotes();
        }
        private void cargarEncabezado()
        {
            upEncabezado.Visible = true;
            DataView dvEncabezado = transacciones.RetornaEncabezadoTransaccionLabores(ddlTipoDocumento.SelectedValue, txtNumero.Text, Convert.ToInt16(this.Session["empresa"]));
            foreach (DataRowView registro in dvEncabezado)
            {

                txtFecha.Text = Convert.ToDateTime(registro.Row.ItemArray.GetValue(5)).ToShortDateString();
                ddlFinca.SelectedValue = registro.Row.ItemArray.GetValue(7).ToString().Trim();
                txtObservacion.Text = registro.Row.ItemArray.GetValue(15).ToString();
                txtRemision.Text = registro.Row.ItemArray.GetValue(14).ToString();
                if (ddlAño.Visible)
                {
                    ddlAño.SelectedValue = registro.Row.ItemArray.GetValue(23).ToString();
                    ddlMesInicial.SelectedValue = registro.Row.ItemArray.GetValue(24).ToString();
                    ddlMesFinal.SelectedValue = registro.Row.ItemArray.GetValue(25).ToString();
                }

                if (ddlReferencia.Visible)
                {
                    ddlReferencia.SelectedValue = txtRemision.Text = registro.Row.ItemArray.GetValue(6).ToString();
                }

                if (txtFechaF.Visible)
                {
                    txtFechaF.Text = Convert.ToDateTime(registro[5]).ToShortDateString();
                }


            }
        }
        private void cargarDetalle()
        {
            ViewState["listaLotes"] = null;
            List<CtransaccionFertilizante> listaNT = new List<CtransaccionFertilizante>();
            CtransaccionFertilizante novedadTran;
            List<Ctercero> listaTer = null;
            Ctercero ter;
            upEncabezado.Visible = true;
            DataView dvNovedad = transacciones.RetornaEncabezadoTransaccionLaboresDetalle(ddlTipoDocumento.SelectedValue, txtNumero.Text, Convert.ToInt16(this.Session["empresa"]));
            DataView dvTerceros = transacciones.RetornaEncabezadoTransaccionLaboresTercero(ddlTipoDocumento.SelectedValue, txtNumero.Text, Convert.ToInt16(this.Session["empresa"]));
            DataView dvItems = transacciones.RetornaEncabezadoTransaccionLaboresItems(ddlTipoDocumento.SelectedValue, txtNumero.Text, Convert.ToInt16(this.Session["empresa"]));
            int x = 0;
            string iditem = "", item = "", umedida = "", loted = "";
            decimal dosis = 0, pbulto = 0, nbulto = 0;
            int registroI = 0, noPalma = 0, registriR;
            listaLotes = new List<Clotes>();
            List<CtransaccionFertilizante> listaTransaccion = new List<CtransaccionFertilizante>();
            List<CtransaccionFertilizante> listaTransaccionFertilizante = new List<CtransaccionFertilizante>();
            foreach (DataRowView registro in dvNovedad)
            {

                string novedad = "", desnovedad = "", lote = "", deslote = "", secion = "", desseccion = "", racimos = "", uMedida = "", fecha = "";
                decimal cantidad = 0, jornal = 0, pesoRacimo = 0, precioLabor = 0;
                int registroNovedad = 0, registroNT = 0; novedadTran = null;
                listaTer = new List<Ctercero>();

                if (!(registro.Row.ItemArray.GetValue(2) is DBNull))
                {
                    novedad = registro.Row.ItemArray.GetValue(2).ToString();
                    desnovedad = registro.Row.ItemArray.GetValue(3).ToString();
                }

                if (!(registro.Row.ItemArray.GetValue(4) is DBNull))
                {
                    lote = registro.Row.ItemArray.GetValue(4).ToString();
                    deslote = registro.Row.ItemArray.GetValue(5).ToString();
                }

                if (!(registro.Row.ItemArray.GetValue(7) is DBNull))
                {
                    secion = registro.Row.ItemArray.GetValue(7).ToString();
                    desseccion = registro.Row.ItemArray.GetValue(8).ToString();
                }

                if (!(registro.Row.ItemArray.GetValue(10) is DBNull))
                    cantidad = Convert.ToDecimal(registro.Row.ItemArray.GetValue(10));

                if (!(registro.Row.ItemArray.GetValue(6) is DBNull))
                    racimos = registro.Row.ItemArray.GetValue(6).ToString();

                if (!(registro.Row.ItemArray.GetValue(9) is DBNull))
                    uMedida = registro.Row.ItemArray.GetValue(9).ToString();

                if (!(registro.Row.ItemArray.GetValue(12) is DBNull))
                    pesoRacimo = Convert.ToDecimal(registro.Row.ItemArray.GetValue(12).ToString());

                if (!(registro.Row.ItemArray.GetValue(11) is DBNull))
                    fecha = registro.Row.ItemArray.GetValue(11).ToString();

                if (!(registro.Row.ItemArray.GetValue(13) is DBNull))
                    jornal = Convert.ToDecimal(registro.Row.ItemArray.GetValue(13).ToString());

                if (!(registro.Row.ItemArray.GetValue(14) is DBNull))
                    registroNovedad = Convert.ToInt16(registro.Row.ItemArray.GetValue(14).ToString());

                if (!(registro.Row.ItemArray.GetValue(16) is DBNull))
                    precioLabor = Convert.ToDecimal(registro.Row.ItemArray.GetValue(16).ToString());

                foreach (DataRowView registrotercero in dvTerceros)
                {
                    string tlote = "";
                    string tcuadrilla = "";
                    decimal tjornal = 0, tcantidad = 0, precioLaborTercero = 0;

                    if (!(registrotercero.Row.ItemArray.GetValue(3) is DBNull))
                        tlote = registrotercero.Row.ItemArray.GetValue(3).ToString().Trim();

                    if (!(registrotercero.Row.ItemArray.GetValue(4) is DBNull))
                        tcuadrilla = registrotercero.Row.ItemArray.GetValue(4).ToString().Trim();

                    if (!(registrotercero.Row.ItemArray.GetValue(5) is DBNull))
                        tcantidad = Convert.ToDecimal(registrotercero.Row.ItemArray.GetValue(5));

                    if (!(registrotercero.Row.ItemArray.GetValue(6) is DBNull))
                        tjornal = Convert.ToDecimal(registrotercero.Row.ItemArray.GetValue(6));

                    if (!(registrotercero.Row.ItemArray.GetValue(7) is DBNull))
                        registroNT = Convert.ToInt16(registrotercero.Row.ItemArray.GetValue(7));

                    if (!(registrotercero.Row.ItemArray.GetValue(9) is DBNull))
                        precioLaborTercero = Convert.ToDecimal(registrotercero.Row.ItemArray.GetValue(9));

                    ter = new Ctercero(Convert.ToInt16(registrotercero.Row.ItemArray.GetValue(1)), registrotercero.Row.ItemArray.GetValue(2).ToString(), tlote, tcuadrilla, tcantidad, tjornal, precioLaborTercero);

                    if (novedad == registrotercero.Row.ItemArray.GetValue(0).ToString().Trim() && lote == tlote & registroNovedad == registroNT)
                        listaTer.Add(ter);
                }

                novedadTran = new CtransaccionFertilizante(novedad, desnovedad, lote, deslote, secion, desseccion, Convert.ToDecimal(racimos), Convert.ToDecimal(cantidad),
                   listaTer, x, uMedida, Convert.ToDecimal(pesoRacimo), fecha, Convert.ToDecimal(jornal), precioLabor, null);

                listaNT.Add(novedadTran);
                x++;
            }
            listaTransaccion = new List<CtransaccionFertilizante>();
            listaTransaccion = listaNT;
            listaNovedadesTransaccion = listaNT;
            Session["listaTransaccion"] = listaNT;
            dlDetalle.DataSource = listaNT;
            dlDetalle.DataBind();
            dlDetalle.Visible = true;

            foreach (DataListItem d in dlDetalle.Items)
            {
                foreach (CtransaccionFertilizante nt in listaNT)
                {
                    if (((Label)d.FindControl("lblNovedad")).Text.Trim() == nt.Codnovedad.ToString() & ((Label)d.FindControl("lblSeccion")).Text.Trim() == nt.Codseccion.ToString() &
                        ((Label)d.FindControl("lblLote")).Text.Trim() == nt.Codlote.ToString() & ((Label)d.FindControl("lblRegistro")).Text.Trim() == nt.Registro.ToString())
                    {
                        ((GridView)d.FindControl("gvLotes")).DataSource = nt.Terceros;
                        ((GridView)d.FindControl("gvLotes")).DataBind();

                        //((GridView)d.FindControl("gvItems")).DataSource = nt.ListaItems;
                        //((GridView)d.FindControl("gvItems")).DataBind();
                    }
                }
            }

            foreach (DataListItem d in dlDetalle.Items)
            {
                ((DropDownList)d.FindControl("ddlTerceroGrilla")).DataSource = transacciones.SelccionaTercernoNovedad(Convert.ToInt16(this.Session["empresa"]));
                ((DropDownList)d.FindControl("ddlTerceroGrilla")).DataValueField = "id";
                ((DropDownList)d.FindControl("ddlTerceroGrilla")).DataTextField = "cadena";
                ((DropDownList)d.FindControl("ddlTerceroGrilla")).DataBind();

                DataView dvItemDetalle = CentidadMetodos.EntidadGet("iItems", "ppa").Tables[0].DefaultView;
                dvItemDetalle.RowFilter = "empresa=" + Convert.ToInt32(this.Session["empresa"]) + " and tipo='FI'";
                ((DropDownList)d.FindControl("ddlItemDetalle")).DataSource = dvItemDetalle;
                ((DropDownList)d.FindControl("ddlItemDetalle")).DataValueField = "codigo";
                ((DropDownList)d.FindControl("ddlItemDetalle")).DataTextField = "descripcion";
                ((DropDownList)d.FindControl("ddlItemDetalle")).DataBind();
                ((DropDownList)d.FindControl("ddlItemDetalle")).Items.Insert(0, new ListItem("Seleccione una opción", ""));

                /////////////////////
                DataListItem dli = d;

                CtransaccionFertilizante trafer = new CtransaccionFertilizante();
                listaTransaccionFertilizante = new List<CtransaccionFertilizante>();
                foreach (DataRowView drv in dvItems)
                {
                    iditem = drv.Row.ItemArray.GetValue(0).ToString();
                    item = drv.Row.ItemArray.GetValue(1).ToString();
                    umedida = drv.Row.ItemArray.GetValue(2).ToString();
                    dosis = Convert.ToDecimal(drv.Row.ItemArray.GetValue(3).ToString());
                    pbulto = Convert.ToDecimal(drv.Row.ItemArray.GetValue(4).ToString());
                    nbulto = Convert.ToDecimal(drv.Row.ItemArray.GetValue(5).ToString());
                    noPalma = Convert.ToInt32(drv.Row.ItemArray.GetValue(6).ToString());
                    registroI = Convert.ToInt32(drv.Row.ItemArray.GetValue(7).ToString());
                    loted = drv.Row.ItemArray.GetValue(8).ToString();
                    registriR = Convert.ToInt32(drv.Row.ItemArray.GetValue(9).ToString());

                    trafer = new CtransaccionFertilizante(iditem,
                           item,
                           umedida,
                           dosis,
                           nbulto,
                           pbulto,
                           noPalma,
                           registroI, registriR);

                    if (((Label)dli.FindControl("lblLote")).Text == loted & Convert.ToInt16(((Label)d.FindControl("lblRegistro")).Text.Trim()) == registriR)
                    {
                        listaTransaccionFertilizante.Add(trafer);
                    }
                }

                var _nt = listaNT.FirstOrDefault(nt => ((Label)d.FindControl("lblNovedad")).Text.Trim() == nt.Codnovedad.ToString() & ((Label)d.FindControl("lblSeccion")).Text.Trim() == nt.Codseccion.ToString() & ((Label)d.FindControl("lblLote")).Text.Trim() == nt.Codlote.ToString() & ((Label)d.FindControl("lblRegistro")).Text.Trim() == nt.Registro.ToString());
                if (_nt != null)
                {
                    _nt.ListaItems = listaTransaccionFertilizante;
                }

                ((GridView)dli.FindControl("gvItems")).DataSource = _nt.ListaItems;
                ((GridView)dli.FindControl("gvItems")).DataBind();

            }
            /////////////////////


            iditem = ""; item = ""; umedida = ""; loted = ""; dosis = 0; pbulto = 0; nbulto = 0;
            registroI = 0; noPalma = 0;

            foreach (DataRowView drv in dvItems)
            {
                if (!(drv.Row.ItemArray.GetValue(0) is DBNull))
                {
                    iditem = drv.Row.ItemArray.GetValue(0).ToString();
                    item = drv.Row.ItemArray.GetValue(1).ToString();
                    umedida = drv.Row.ItemArray.GetValue(2).ToString();
                    dosis = Convert.ToDecimal(drv.Row.ItemArray.GetValue(3).ToString());
                    pbulto = Convert.ToDecimal(drv.Row.ItemArray.GetValue(4).ToString());
                    nbulto = Convert.ToDecimal(drv.Row.ItemArray.GetValue(5).ToString());
                    noPalma = Convert.ToInt32(drv.Row.ItemArray.GetValue(6).ToString());
                    registroI = Convert.ToInt32(drv.Row.ItemArray.GetValue(7).ToString());
                    loted = drv.Row.ItemArray.GetValue(8).ToString();


                    if (ddlItem.Visible)
                    {


                        if (listaLotes == null)
                        {
                            listaTransaccion = new List<CtransaccionFertilizante>();
                            trafer = new CtransaccionFertilizante(iditem, item, umedida,
                            dosis, nbulto, pbulto, noPalma, registroI, 0);
                            listaTransaccion.Add(trafer);
                            lot = new Clotes(loted, listaTransaccion);
                            listaLotes = new List<Clotes>();
                            listaLotes.Add(lot);

                        }
                        else
                        {
                            bool validar = false;

                            for (x = 0; x < listaLotes.Count; x++)
                            {
                                if (loted == listaLotes[x].Lote)
                                {
                                    listaTransaccion = new List<CtransaccionFertilizante>();
                                    listaTransaccion = listaLotes[x].Items;
                                    trafer = new CtransaccionFertilizante(iditem, item, umedida,
                            dosis, nbulto, pbulto, noPalma, registroI, 0);
                                    listaTransaccion.Add(trafer);
                                    listaLotes[x].Items = listaTransaccion;
                                    validar = true;
                                }
                            }

                            if (validar == false)
                            {
                                listaTransaccion = new List<CtransaccionFertilizante>();
                                trafer = new CtransaccionFertilizante(iditem, item, umedida,
                            dosis, nbulto, pbulto, noPalma, registroI, 0);
                                listaTransaccion.Add(trafer);
                                lot = new Clotes(loted, listaTransaccion);
                                listaLotes.Add(lot);
                            }

                        }
                        ViewState["listaLotes"] = listaLotes;

                        this.dlLotes.DataSource = listaLotes;
                        this.dlLotes.DataBind();


                        foreach (DataListItem gvr in dlLotes.Items)
                        {
                            for (x = 0; x < listaLotes.Count; x++)
                            {
                                if (((Label)gvr.FindControl("lblCodLote")).Text == listaLotes[x].Lote)
                                {
                                    ((GridView)(gvr.FindControl("gvItems"))).DataSource = listaLotes[x].Items;
                                    ((GridView)(gvr.FindControl("gvItems"))).DataBind();
                                    break;
                                }
                            }
                        }
                    }
                }
            }


            if (listaNT.Count > 0)
                this.listaNovedadesTransaccion = listaNT;
            else
                this.listaNovedadesTransaccion = null;

        }

        private void verificaPeriodoCerrado(int año, int mes, int empresa, DateTime fecha)
        {
            if (periodo.RetornaPeriodoCerradoNomina(año, mes, empresa, fecha) == 1)
            {
                ManejoError("Periodo cerrado de nomina. No es posible realizar nuevas transacciones", "I");
            }
            if (periodo.RetornaPeriodoCerradoNomina(año, mes, empresa, fecha) == 2)
            {
                ManejoError("Periodo de nomina no existe. No es posible realizar nuevas transacciones", "I");
            }

        }
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
        private void BusquedaTransaccion()
        {
            try
            {
                if (this.gvParametros.Rows.Count > 0)
                {
                    string where = operadores.FormatoWhere((List<Coperadores>)Session["operadores"]);
                    this.gvTransaccion.DataSource = transacciones.GetTransaccionCompletaLabores(where, Convert.ToInt16(Session["empresa"]));
                    this.gvTransaccion.DataBind();
                    this.nilblRegistros.Text = "Nro. Registros " + Convert.ToString(this.gvTransaccion.Rows.Count);
                    EstadoInicialGrillaTransacciones();
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al procesar la consulta de transacciones. Correspondiente a: " + ex.Message, "C");
            }
        }
        protected void Guardar()
        {
            string operacion = "inserta";
            bool verificaEncabezado = false;
            bool verificaDetalle = false;
            bool verificaBascula = false;

            this.Session["numeroTransaccion"] = null;
            try
            {

                using (TransactionScope ts = new TransactionScope())
                {
                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                    {
                        DateTime fecha = Convert.ToDateTime(txtFecha.Text);
                        DateTime fechaF = Convert.ToDateTime(txtFecha.Text);
                        string referencia = null, remision = null;
                        numerotransaccion = txtNumero.Text;
                        this.Session["numerotransaccion"] = numerotransaccion;

                        if (ddlReferencia.Visible)
                            referencia = ddlReferencia.SelectedValue.Trim();

                        if (txtRemision.Enabled == true)
                            remision = txtRemision.Text;

                        object[] objValoDeleteNovedad = new object[]{
                                 Convert.ToInt16(this.Session["empresa"]) ,  //@empresa	int
                                numerotransaccion,    //@numero	varchar
                                ddlTipoDocumento.SelectedValue,  //@tipo	varchar
                         };
                        switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionItem", "elimina", "ppa", objValoDeleteNovedad))
                        {
                            case 1:
                                ManejoError("Error al eliminar la novedad registrada", "E");
                                break;
                        }

                        object[] objValoDeleteTerceroItems = new object[]{
                                 Convert.ToInt16(this.Session["empresa"]) ,  //@empresa	int
                                numerotransaccion,    //@numero	varchar
                                ddlTipoDocumento.SelectedValue,  //@tipo	varchar
                         };
                        switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionTercero", "elimina", "ppa", objValoDeleteTerceroItems))
                        {
                            case 1:
                                ManejoError("Error al eliminar los terceros de la novedad registrada", "E");
                                break;
                        }

                        object[] objValoDeleteTerceroNovedad = new object[]{
                                 Convert.ToInt16(this.Session["empresa"]) ,  //@empresa	int
                                numerotransaccion,    //@numero	varchar
                                ddlTipoDocumento.SelectedValue,  //@tipo	varchar
                         };
                        switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionNovedad", "elimina", "ppa", objValoDeleteTerceroNovedad))
                        {
                            case 1:
                                ManejoError("Error al eliminar los terceros de la novedad registrada", "E");
                                break;
                        }

                        object[] objValo = new object[]{
                                                         false, // @anulado	bit
                                                      Convert.ToUInt32(fecha.Year), //@año	int
                                                      ddlAño.SelectedValue.Trim(), //añofer int
                                                      0,  //@cantidad	int
                                                     Convert.ToInt16(this.Session["empresa"]),   //@empresa	int
                                                     fecha,   //@fecha	date
                                                     fecha,  //@fechaAnulado	datetime
                                                     fechaF,  //@fechaFinal	date
                                                     DateTime.Now,   //@fechaRegistro	datetime
                                                     ddlFinca.SelectedValue.Trim(),   //@finca	char
                                                     0,   //@jornal	int
                                                     Convert.ToUInt32(fecha.Month),  //@mes	int
                                                     Convert.ToInt32(ddlMesFinal.SelectedValue), //mesFfer int
                                                     Convert.ToInt32(ddlMesInicial.SelectedValue), //mesIfer int
                                                     numerotransaccion,   //@numero	varchar
                                                     txtObservacion.Text,   //@observacion	varchar
                                                     0,   //@precio	money
                                                     0,   //@racimos	int
                                                     referencia,   //@referencia	varchar
                                                     remision,   //@remision	varchar
                                                     ddlTipoDocumento.SelectedValue.Trim(),   //@tipo	varchar
                                                     null,   //@usuarioAnulado	varchar
                                                     this.Session["usuario"].ToString(),   //@usuarioRegistro	varchar
                                                     0   //@valorTotal	money
                              };

                        switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionF", "actualiza", "ppa", objValo))
                        {
                            case 0:

                                foreach (DataListItem dli in dlLotes.Items)
                                {
                                    foreach (GridViewRow gvr in ((GridView)dli.FindControl("gvItems")).Rows)
                                    {
                                        decimal noBultosI = Convert.ToDecimal(gvr.Cells[5].Text.Trim());
                                        decimal peBultosI = Convert.ToDecimal(gvr.Cells[6].Text.Trim());
                                        decimal dosisI = Convert.ToDecimal(gvr.Cells[4].Text.Trim());
                                        decimal cantidadI = noBultosI * peBultosI;
                                        decimal noPalmas = Convert.ToInt32(gvr.Cells[7].Text.Trim());
                                        string lote = ((Label)dli.FindControl("lblCodLote")).Text.Trim();

                                        object[] objValoresItem = new object[]{
                                       Convert.ToDateTime(txtFecha.Text).Year,     //@año	int
                                       cantidadI,     //@cantidad	float
                                       dosisI,             //@dosis float
                                       Convert.ToInt32(this.Session["empresa"]),      //@empresa	int
                                       Convert.ToDateTime(txtFecha.Text),     //@fecha	date
                                       fechaF,     //@fechaFinal	date
                                       gvr.Cells[1].Text.Trim(),    //@item	varchar
                                       lote, //@lote varchar
                                       noBultosI,  //@mBulto	float
                                       Convert.ToDateTime(txtFecha.Text).Month,     //@mes	int
                                       noPalmas,
                                       null,     //@novedad	varchar
                                       txtNumero.Text.Trim(),     //@numero	varchar
                                       peBultosI,    //@pBulto	float
                                       Convert.ToInt32(gvr.Cells[8].Text.Trim()) ,  //@registro	int
                                       gvr.RowIndex,  //@registror int
                                       cantidadI,  //@saldo	float
                                       ddlTipoDocumento.SelectedValue.Trim(),     //@tipo	varchar
                                       gvr.Cells[3].Text.Trim()     //@uMedida	varchar
                             };
                                        switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionItem", operacion, "ppa", objValoresItem))
                                        {
                                            case 1:
                                                ManejoError("Error al insertar el detalle de la transacción", "I");
                                                verificaDetalle = true;
                                                break;
                                        }
                                    }
                                }

                                foreach (DataListItem dl in dlDetalle.Items)
                                {
                                    DateTime fechaDetalle = new DateTime();
                                    decimal cantidad = 0, jornales = 0, racimos = 0, pesoRacimo = 0, precioLabor = 0, precioLaborTercero = 0;
                                    string lote = "", novedad = "", seccion = "", umedida = "";
                                    GridView gvTerceros = new GridView();

                                    if (((Label)dl.FindControl("lblFechaD")) != null)
                                        fechaDetalle = Convert.ToDateTime(((Label)dl.FindControl("lblFechaD")).Text);

                                    if (((TextBox)dl.FindControl("txvCantidadG")) != null)
                                        cantidad = Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text);

                                    if (((TextBox)dl.FindControl("txvJornalesD")) != null)
                                        jornales = Convert.ToDecimal(((TextBox)dl.FindControl("txvJornalesD")).Text);

                                    if (((Label)dl.FindControl("lblLote")) != null)
                                        lote = ((Label)dl.FindControl("lblLote")).Text;

                                    if (((Label)dl.FindControl("lblPesoPromedio")) != null)
                                        pesoRacimo = Convert.ToDecimal(((Label)dl.FindControl("lblPesoPromedio")).Text);

                                    if (((Label)dl.FindControl("lblNovedad")) != null)
                                        novedad = ((Label)dl.FindControl("lblNovedad")).Text;

                                    if (((TextBox)dl.FindControl("txvRacimoG")) != null)
                                        racimos = decimal.Round(Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text), 0);

                                    if (((Label)dl.FindControl("lblSeccion")) != null)
                                        seccion = ((Label)dl.FindControl("lblSeccion")).Text;

                                    if (((Label)dl.FindControl("lblUmedida")) != null)
                                        umedida = ((Label)dl.FindControl("lblUmedida")).Text;

                                    if (((Label)dl.FindControl("lblPrecioLabor")) != null)
                                        precioLabor = Convert.ToDecimal(((Label)dl.FindControl("lblPrecioLabor")).Text);

                                    if (lote.Trim().Length == 0)
                                        lote = null;

                                    GridView gvItemsDl = (GridView)dl.FindControl("gvItems");

                                    foreach (GridViewRow gvr in gvItemsDl.Rows)
                                    {
                                        decimal dosisI = Convert.ToDecimal(gvr.Cells[4].Text.Trim());
                                        decimal noBultosI = Convert.ToDecimal(gvr.Cells[5].Text.Trim());
                                        decimal peBultosI = Convert.ToDecimal(gvr.Cells[6].Text.Trim());

                                        object[] objValoresItem = new object[]{
                                       Convert.ToDateTime(fecha).Year,     //@año	int
                                       cantidad *dosisI,     //@cantidad	float,
                                       dosisI,//@dosis
                                       Convert.ToInt32(this.Session["empresa"]),      //@empresa	int
                                       fechaDetalle,     //@fecha	date
                                       fechaDetalle,     //@fechaFinal	date
                                       gvr.Cells[1].Text.Trim(),    //@item	varchar
                                       lote, //@lote
                                       noBultosI,  //@mBulto	float
                                       Convert.ToDateTime(fecha).Month,     //@mes	int
                                       cantidad , //@noPalmas 
                                       novedad,     //@novedad	varchar
                                       txtNumero.Text.Trim(),     //@numero	varchar
                                       peBultosI,    //@pBulto	float
                                       gvr.RowIndex ,  //@registro	int
                                       dl.ItemIndex,            //@registror int
                                       0,  //@saldo	float
                                       ddlTipoDocumento.SelectedValue.Trim(),     //@tipo	varchar
                                       gvr.Cells[3].Text.Trim()     //@uMedida	varchar
                             };
                                        switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionItem", operacion, "ppa", objValoresItem))
                                        {
                                            case 1:
                                                ManejoError("Error al insertar el detalle de la transacción", "I");
                                                verificaDetalle = true;
                                                break;
                                        }
                                    }


                                    object[] objValores1 = new object[]{
                                    Convert.ToInt16( fechaDetalle.Year.ToString()),   //@año 1
                                    cantidad, //@cantidad 2
                                    false,   //@ejecutado3
                                    Convert.ToInt16(this.Session["empresa"]),     //@empresa4
                                    fechaDetalle,    //@fecha5
                                    jornales,  //@jornales6
                                    lote,    //@lote7
                                    Convert.ToInt16( fechaDetalle.Month.ToString()) ,  //@mes8
                                    novedad,    //@novedad9
                                    txtNumero.Text,    //@numero10
                                    pesoRacimo, //@pesoRacimo 11
                                    precioLabor,//12
                                    racimos,    //@racimos 13
                                    dl.ItemIndex,   //@registro, 14
                                    0,//15
                                    cantidad,    //@saldo//16
                                    seccion,   //@seccion17
                                    ddlTipoDocumento.SelectedValue.Trim(),    //@tipo18
                                    umedida //@uMedida 19
                                 };

                                    switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionNovedad", operacion, "ppa", objValores1))
                                    {
                                        case 0:
                                            decimal cantidadT = 0, jornalT = 0;
                                            string cuadrilla = null;

                                            if (((GridView)dl.FindControl("gvLotes")) != null)
                                            {
                                                foreach (GridViewRow gv in ((GridView)dl.FindControl("gvLotes")).Rows)
                                                {
                                                    if (((TextBox)gv.FindControl("txtCantidad")) != null)
                                                        cantidadT = Convert.ToDecimal(((TextBox)gv.FindControl("txtCantidad")).Text);

                                                    if (((TextBox)gv.FindControl("txtJornal")) != null)
                                                        jornalT = Convert.ToDecimal(((TextBox)gv.FindControl("txtJornal")).Text);

                                                    if (!(gv.Cells[3].Text.Trim().Length == 0) & gv.Cells[3].Text.Trim() != "&nbsp;")
                                                        cuadrilla = gv.Cells[3].Text.Trim();

                                                    if (!(gv.Cells[5].Text.Trim().Length == 0) & gv.Cells[5].Text.Trim() != "&nbsp;")
                                                        precioLaborTercero = Convert.ToDecimal((gv.Cells[5].Text.Trim()));


                                                    // if (cantidadT != 0 & jornalT != 0)
                                                    //{
                                                    object[] objValores2 = new object[]{
                                                                Convert.ToInt16( fechaDetalle.Year.ToString()),  //@año
                                                                cantidadT, //@cantidad
                                                                false, //@ejecutado
                                                                Convert.ToInt16(this.Session["empresa"]), //@empresa
                                                                Convert.ToDateTime(fechaDetalle),//fecha
                                                                ddlFinca.SelectedValue.Trim() , //finca
                                                                jornalT, //@jornales
                                                                lote,//@lote
                                                                Convert.ToInt16( fechaDetalle.Month.ToString()),//@mes
                                                                novedad,//@novedad
                                                                txtNumero.Text, //@numero
                                                                precioLaborTercero, //@precio
                                                                gv.RowIndex,//@registro
                                                                dl.ItemIndex,//@registroNovedad
                                                                cantidadT,//@saldo
                                                                seccion,//@seccion
                                                                gv.Cells[1].Text.Trim(),//@tercero
                                                                ddlTipoDocumento.SelectedValue.Trim(),//@tipo
                                                                cuadrilla//@cuadrilla
                                                                };

                                                    switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionTercero", operacion, "ppa", objValores2))
                                                    {
                                                        case 1:
                                                            ManejoError("Error al insertar el detalle de la transacción", "I");
                                                            verificaDetalle = true;
                                                            break;
                                                    }
                                                }
                                            }
                                            break;
                                        case 1:
                                            ManejoError("Error al insertar el encabezado de la transaccción", "I");
                                            break;
                                    }
                                }
                                break;
                            case 1:
                                ManejoError("Error al insertar el detalle de la transaccción", "I");
                                break;
                        }

                        if (verificaEncabezado == false & verificaDetalle == false & verificaBascula == false)
                        {
                            //    transacciones.ActualizaConsecutivo(ddlTipoDocumento.Text, Convert.ToInt16(this.Session["empresa"]));
                            ts.Complete();
                            ManejoExito("Datos Actualizados satisfactoriamente", "I");
                        }

                    }
                    else
                    {
                        if (ddlAño.Visible)
                        {
                            txtFecha.Text = DateTime.Now.ToShortDateString();
                        }
                        DateTime fecha = Convert.ToDateTime(txtFecha.Text);
                        DateTime fechaF;
                        if (txtFechaF.Visible)
                            fechaF = Convert.ToDateTime(txtFechaF.Text);
                        else
                            fechaF = Convert.ToDateTime(txtFecha.Text);

                        string referencia = null, remision = null;

                        if (txtRemision.Enabled == true)
                            remision = txtRemision.Text;
                        if (ddlReferencia.Visible)
                            referencia = ddlReferencia.SelectedValue.Trim();

                        numerotransaccion = transacciones.RetornaNumeroTransaccion(ddlTipoDocumento.SelectedValue, Convert.ToInt16(this.Session["empresa"]));
                        this.Session["numerotransaccion"] = numerotransaccion;

                        object[] objValo = new object[]{
                                                      false, // @anulado	bit
                                                      Convert.ToUInt32(fecha.Year), //@año	int
                                                      ddlAño.SelectedValue.Trim(), //añofer int
                                                      0,  //@cantidad	int
                                                     Convert.ToInt16(this.Session["empresa"]),   //@empresa	int
                                                     fecha,   //@fecha	date
                                                     fecha,  //@fechaAnulado	datetime
                                                     fechaF,  //@fechaFinal	date
                                                     DateTime.Now,   //@fechaRegistro	datetime
                                                     ddlFinca.SelectedValue.Trim(),   //@finca	char
                                                     0,   //@jornal	int
                                                     Convert.ToUInt32(fecha.Month),  //@mes	int
                                                     Convert.ToInt32(ddlMesFinal.SelectedValue), //mesFfer int
                                                     Convert.ToInt32(ddlMesInicial.SelectedValue), //mesIfer int
                                                     numerotransaccion,   //@numero	varchar
                                                     txtObservacion.Text,   //@observacion	varchar
                                                     0,   //@precio	money
                                                     0,   //@racimos	int
                                                     referencia,   //@referencia	varchar
                                                     remision,   //@remision	varchar
                                                     ddlTipoDocumento.SelectedValue.Trim(),   //@tipo	varchar
                                                     null,   //@usuarioAnulado	varchar
                                                     this.Session["usuario"].ToString(),   //@usuarioRegistro	varchar
                                                     0   //@valorTotal	money
                                          };

                        switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionF", operacion, "ppa", objValo))
                        {
                            case 0:
                                foreach (DataListItem dli in dlLotes.Items)
                                {
                                    foreach (GridViewRow gvr in ((GridView)dli.FindControl("gvItems")).Rows)
                                    {
                                        decimal noBultosI = Convert.ToDecimal(gvr.Cells[5].Text.Trim());
                                        decimal peBultosI = Convert.ToDecimal(gvr.Cells[6].Text.Trim());
                                        decimal dosisI = Convert.ToDecimal(gvr.Cells[4].Text.Trim());
                                        decimal cantidadI = noBultosI * peBultosI;
                                        decimal noPalmas = Convert.ToInt32(gvr.Cells[7].Text.Trim());
                                        string lote = ((Label)dli.FindControl("lblCodLote")).Text.Trim();

                                        object[] objValoresItem = new object[]{
                                       Convert.ToDateTime(txtFecha.Text).Year,     //@año	int
                                       cantidadI,     //@cantidad	float
                                       dosisI,             //@dosis float
                                       Convert.ToInt32(this.Session["empresa"]),      //@empresa	int
                                       Convert.ToDateTime(txtFecha.Text),     //@fecha	date
                                       fechaF,     //@fechaFinal	date
                                       gvr.Cells[1].Text.Trim(),    //@item	varchar
                                       lote, //@lote varchar
                                       noBultosI,  //@mBulto	float
                                       Convert.ToDateTime(txtFecha.Text).Month,     //@mes	int
                                       noPalmas,
                                       null,     //@novedad	varchar
                                       txtNumero.Text.Trim(),     //@numero	varchar
                                       peBultosI,    //@pBulto	float
                                       Convert.ToInt32(gvr.Cells[8].Text.Trim()) ,  //@registro	int
                                       0  ,   //@referenciaI
                                       cantidadI,  //@saldo	float
                                       ddlTipoDocumento.SelectedValue.Trim(),     //@tipo	varchar
                                       gvr.Cells[3].Text.Trim()     //@uMedida	varchar
                             };
                                        switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionItem", operacion, "ppa", objValoresItem))
                                        {
                                            case 1:
                                                ManejoError("Error al insertar el detalle de la transacción", "I");
                                                verificaDetalle = true;
                                                break;
                                        }
                                    }
                                }



                                foreach (DataListItem dl in dlDetalle.Items)
                                {
                                    DateTime fechaDetalle = new DateTime();
                                    decimal cantidad = 0, jornales = 0, racimos = 0, pesoRacimo = 0, precioLabor = 0, precioLaborTercero = 0;
                                    string lote = "", novedad = "", seccion = "", umedida = "";
                                    GridView gvTerceros = new GridView();

                                    if (((Label)dl.FindControl("lblFechaD")) != null)
                                        fechaDetalle = Convert.ToDateTime(((Label)dl.FindControl("lblFechaD")).Text);

                                    if (((TextBox)dl.FindControl("txvCantidadG")) != null)
                                        cantidad = Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text);

                                    if (((TextBox)dl.FindControl("txvJornalesD")) != null)
                                        jornales = Convert.ToDecimal(((TextBox)dl.FindControl("txvJornalesD")).Text);

                                    if (((Label)dl.FindControl("lblLote")) != null)
                                        lote = ((Label)dl.FindControl("lblLote")).Text;

                                    if (((Label)dl.FindControl("lblPesoPromedio")) != null)
                                        pesoRacimo = Convert.ToDecimal(((Label)dl.FindControl("lblPesoPromedio")).Text);

                                    if (((Label)dl.FindControl("lblNovedad")) != null)
                                        novedad = ((Label)dl.FindControl("lblNovedad")).Text;

                                    if (((TextBox)dl.FindControl("txvRacimoG")) != null)
                                        racimos = decimal.Round(Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text), 0);

                                    if (((Label)dl.FindControl("lblSeccion")) != null)
                                        seccion = ((Label)dl.FindControl("lblSeccion")).Text;

                                    if (((Label)dl.FindControl("lblUmedida")) != null)
                                        umedida = ((Label)dl.FindControl("lblUmedida")).Text;

                                    if (((Label)dl.FindControl("lblPrecioLabor")) != null)
                                        precioLabor = Convert.ToDecimal(((Label)dl.FindControl("lblPrecioLabor")).Text);

                                    if (lote.Trim().Length == 0)
                                        lote = null;

                                    GridView gvItemsDl = (GridView)dl.FindControl("gvItems");

                                    foreach (GridViewRow gvr in gvItemsDl.Rows)
                                    {
                                        decimal dosisI = Convert.ToDecimal(gvr.Cells[4].Text.Trim());
                                        decimal noBultosI = Convert.ToDecimal(gvr.Cells[5].Text.Trim());
                                        decimal peBultosI = Convert.ToDecimal(gvr.Cells[6].Text.Trim());

                                        object[] objValoresItem = new object[]{
                                       Convert.ToDateTime(fecha).Year,     //@año	int
                                       cantidad *dosisI,     //@cantidad	float,
                                       dosisI,//@dosis
                                       Convert.ToInt32(this.Session["empresa"]),      //@empresa	int
                                       fechaDetalle,     //@fecha	date
                                       fechaDetalle,     //@fechaFinal	date
                                       gvr.Cells[1].Text.Trim(),    //@item	varchar
                                       lote, //@lote
                                       noBultosI,  //@mBulto	float
                                       Convert.ToDateTime(fecha).Month,     //@mes	int
                                       cantidad , //@noPalmas 
                                       novedad,     //@novedad	varchar
                                       txtNumero.Text.Trim(),     //@numero	varchar
                                       peBultosI,    //@pBulto	float
                                       gvr.RowIndex ,  //@registro	int
                                      dl.ItemIndex,     //@registroR int
                                       0,  //@saldo	float
                                       ddlTipoDocumento.SelectedValue.Trim(),     //@tipo	varchar
                                       gvr.Cells[3].Text.Trim()     //@uMedida	varchar
                                    };
                                        switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionItem", operacion, "ppa", objValoresItem))
                                        {
                                            case 1:
                                                ManejoError("Error al insertar el detalle de la transacción", "I");
                                                verificaDetalle = true;
                                                break;
                                        }
                                    }

                                    object[] objValores1 = new object[]{
                                        Convert.ToInt16( fechaDetalle.Year.ToString()),   //@año 1
                                    cantidad, //@cantidad 2
                                    false,   //@ejecutado3
                                    Convert.ToInt16(this.Session["empresa"]),     //@empresa4
                                    fechaDetalle,    //@fecha5
                                    ddlFinca.SelectedValue.Trim(),//finca 6
                                    jornales,  //@jornales7
                                    lote,    //@lote8
                                    Convert.ToInt16( fechaDetalle.Month.ToString()) ,  //@mes9
                                    novedad,    //@novedad10
                                    txtNumero.Text,    //@numero11
                                    pesoRacimo, //@pesoRacimo 12
                                    precioLabor,//13
                                    racimos,    //@racimos 14
                                    dl.ItemIndex,   //@registro, 15
                                    0,//16
                                    0, //sacos 17
                                    cantidad,    //@saldo//18
                                    seccion,   //@seccion19
                                    ddlTipoDocumento.SelectedValue.Trim(),    //@tipo20
                                    umedida //@uMedida 21
                                         };

                                    switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionNovedad", operacion, "ppa", objValores1))
                                    {
                                        case 0:
                                            decimal cantidadT = 0, jornalT = 0;
                                            string cuadrilla = null;

                                            if (((GridView)dl.FindControl("gvLotes")) != null)
                                            {
                                                foreach (GridViewRow gv in ((GridView)dl.FindControl("gvLotes")).Rows)
                                                {

                                                    if (((TextBox)gv.FindControl("txtCantidad")) != null)
                                                        cantidadT = Convert.ToDecimal(((TextBox)gv.FindControl("txtCantidad")).Text);

                                                    if (((TextBox)gv.FindControl("txtJornal")) != null)
                                                        jornalT = Convert.ToDecimal(((TextBox)gv.FindControl("txtJornal")).Text);

                                                    if (!(gv.Cells[3].Text.Trim().Length == 0) & gv.Cells[3].Text.Trim() != "&nbsp;")
                                                        cuadrilla = gv.Cells[3].Text.Trim();

                                                    if (!(gv.Cells[5].Text.Trim().Length == 0) & gv.Cells[5].Text.Trim() != "&nbsp;")
                                                        precioLaborTercero = Convert.ToDecimal(gv.Cells[5].Text.Trim());

                                                    //  if (cantidadT != 0 & jornalT != 0)
                                                    //{
                                                    object[] objValores2 = new object[]{
                                                         Convert.ToInt16( fechaDetalle.Year.ToString()),  //@año,
                                                                cantidadT, //@cantidad
                                                                false, //@ejecutado
                                                                Convert.ToInt16(this.Session["empresa"]), //@empresa
                                                                Convert.ToDateTime(fechaDetalle),//fecha
                                                                ddlFinca.SelectedValue.Trim() , //finca
                                                                jornalT, //@jornales
                                                                lote,//@lote
                                                                Convert.ToInt16( fechaDetalle.Month.ToString()),//@mes
                                                                novedad,//@novedad
                                                                txtNumero.Text, //@numero
                                                                precioLaborTercero, //@precio
                                                                racimos, //racimos
                                                                gv.RowIndex,//@registro
                                                                dl.ItemIndex,//@registroNovedad
                                                                cantidadT,//@saldo
                                                                seccion,//@seccion
                                                                gv.Cells[1].Text.Trim(),//@tercero
                                                                ddlTipoDocumento.SelectedValue.Trim(),//@tipo
                                                                cuadrilla//@cuadrilla
                                                            };

                                                    switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionTercero", operacion, "ppa", objValores2))
                                                    {
                                                        case 1:
                                                            ManejoError("Error al insertar el detalle de la transacción", "I");
                                                            verificaDetalle = true;
                                                            break;
                                                    }
                                                    //}
                                                }
                                            }
                                            break;
                                        case 1:
                                            ManejoError("Error al insertar el encabezado de la transaccción", "I");
                                            break;
                                    }
                                }
                                break;
                            case 1:
                                ManejoError("Error al insertar el detalle de la transaccción", "I");
                                break;
                        }

                        if (verificaEncabezado == false & verificaDetalle == false & verificaBascula == false)
                        {
                            transacciones.ActualizaConsecutivo(ddlTipoDocumento.SelectedValue, Convert.ToInt16(this.Session["empresa"]));
                            ts.Complete();
                            ManejoExito("Datos registrados satisfactoriamente transaccion N° " + this.Session["numerotransaccion"].ToString(), "I");
                        }
                    }
                }
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
                ManejoError("Error al recuperar posición de configuración de tipo de transacción. Correspondiente a: " + ex.Message, "C");
                return null;
            }
        }
        private object NovedadConfig(int posicion, string novedad)
        {

            object retorno = null;
            string cadena;
            char[] comodin = new char[] { '*' };
            int indice = posicion + 1;
            try
            {
                cadena = Cnovedad.NovedadConfig(novedad, Convert.ToInt16(Session["empresa"])).ToString();
                retorno = cadena.Split(comodin, indice).GetValue(posicion - 1);
                return retorno;
            }
            catch (Exception ex)
            {
                ManejoError("Error al recuperar posición de configuración del lote. Correspondiente a: " + ex.Message, "C");
                return null;
            }
        }
        private decimal ObtenerPesoPromedio(string lote, DateTime fecha)
        {
            //int año, mes;
            //if (fecha.Month == 1)
            //{
            //    año = Convert.ToInt16(fecha.Year) - 1;
            //    mes = 12;
            //}
            //else
            //{
            //    año = Convert.ToInt16(fecha.Year);
            //    mes = Convert.ToInt16(fecha.Month) - 1;
            //}

            try
            {
                decimal retorno = Convert.ToDecimal(peso.valorPesoPeriodo(Convert.ToInt16(Session["empresa"]), fecha, lote, ddlFinca.SelectedValue));
                return retorno;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        private DataView ObtenerLote()
        {
            object[] objKey = new object[] { this.ddlLote.SelectedValue.Trim().ToString(), Convert.ToInt16(Session["empresa"]) };
            try
            {
                return CentidadMetodos.EntidadGetKey("aLotes", "ppa", objKey).Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Error al cargar el lote debida a: " + ex, "warning");
                return null;
            }
        }
        private object LoteConfig(int posicion, string lote)
        {
            object retorno = null;
            string cadena;
            char[] comodin = new char[] { '*' };
            int indice = posicion + 1;
            try
            {
                cadena = lotes.LotesConfig(lote, Convert.ToInt16(Session["empresa"])).ToString();
                retorno = cadena.Split(comodin, indice).GetValue(posicion - 1);
                return retorno;
            }
            catch (Exception ex)
            {
                ManejoError("Error al recuperar posición de configuración del lote. Correspondiente a: " + ex.Message, "C");
                return null;
            }
        }
        private List<CtransaccionFertilizante> reasignarRegistros(List<CtransaccionFertilizante> listaNovedadesTransaccion)
        {
            int z = 0;
            foreach (CtransaccionFertilizante ln in listaNovedadesTransaccion)
            {
                ln.Registro = z;
                z++;
            }
            return listaNovedadesTransaccion;
        }
        private void CargarTipoTransaccion()
        {
            try
            {
                this.ddlTipoDocumento.DataSource = transacciones.GetTipoTransaccionModulo(Convert.ToInt16(this.Session["empresa"]));
                this.ddlTipoDocumento.DataValueField = "codigo";
                this.ddlTipoDocumento.DataTextField = "descripcion";
                this.ddlTipoDocumento.DataBind();
                this.ddlTipoDocumento.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tipos de transacción. Correspondiente a: " + ex.Message, "C");
            }
        }
        protected void cargarDL()
        {
            int posicionNovedad = 0;
            decimal pesoRacimo = 0, cantidad = 0, precioLabor = 0;
            string novedad = null, nombreNovedad = null, uMedidad = null;
            novedad = ddlNovedad.SelectedValue;
            nombreNovedad = ddlNovedad.SelectedItem.ToString();
            uMedidad = ddlUmedida.SelectedValue;
            decimal precioLaborTercero = 0;
            precioLabor = listaPrecios.SeleccionaPrecioNovedadAño(Convert.ToInt16(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaD.Text).Year);
            cantidad = Convert.ToDecimal(txvCantidadD.Text);
            if (Convert.ToBoolean(NovedadConfig(12, ddlNovedad.SelectedValue)) & Convert.ToBoolean(TipoTransaccionConfig(25)))
            {
                pesoRacimo = ObtenerPesoPromedio(ddlLote.SelectedValue, Convert.ToDateTime(txtFechaD.Text));
                cantidad = pesoRacimo * 0;
            }

            foreach (DataListItem dl in dlDetalle.Items)
            {
                ((TextBox)dl.FindControl("txvCantidadG")).Enabled = !Convert.ToBoolean(NovedadConfig(18, ddlNovedad.SelectedValue));
                ((TextBox)dl.FindControl("txvRacimoG")).Enabled = Convert.ToBoolean(NovedadConfig(18, ddlNovedad.SelectedValue));
                ((TextBox)dl.FindControl("txvJornalesD")).Enabled = !Convert.ToBoolean(NovedadConfig(18, ddlNovedad.SelectedValue));
            }


            if (listaNovedadesTransaccion == null)//cuando la lista de novedades es nula
            {
                listaNovedadesTransaccion = new List<CtransaccionFertilizante>();
                for (int x = 0; x < selTerceroCosecha.Items.Count; x++)
                {
                    if (selTerceroCosecha.Items[x].Selected)
                    {
                        precioLaborTercero = listaPrecios.SeleccionaPrecioNovedadAñoTercero(Convert.ToInt16(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaD.Text).Year, Convert.ToInt32(selTerceroCosecha.Items[x].Value), Convert.ToDateTime(txtFechaD.Text));
                        terceros = new Ctercero(Convert.ToInt32(selTerceroCosecha.Items[x].Value), selTerceroCosecha.Items[x].Text, ddlLote.SelectedValue.ToString().Trim(), null, 0, 0, precioLaborTercero);
                        listaTerceros.Add(terceros);
                    }
                }
                novedadTransaccion = new CtransaccionFertilizante(novedad, nombreNovedad, ddlLote.SelectedValue.Trim(), ddlLote.SelectedItem.Text, ddlSeccion.SelectedValue.Trim(), ddlSeccion.SelectedItem.Text,
                    0, cantidad, listaTerceros, dlDetalle.Items.Count, uMedidad, pesoRacimo, txtFechaD.Text, Convert.ToDecimal(txvJornalesD.Text), precioLabor, null);
                listaNovedadesTransaccion
                    .Add(novedadTransaccion);
            }
            // cuando la lista de novedades  NO es nula
            else
            {
                listaTerceros = new List<Ctercero>();
                for (int x = 0; x < selTerceroCosecha.Items.Count; x++)
                {
                    if (selTerceroCosecha.Items[x].Selected)
                    {
                        precioLaborTercero = listaPrecios.SeleccionaPrecioNovedadAñoTercero(Convert.ToInt16(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaD.Text).Year, Convert.ToInt32(selTerceroCosecha.Items[x].Value), Convert.ToDateTime(txtFechaD.Text));
                        terceros = new Ctercero(Convert.ToInt32(selTerceroCosecha.Items[x].Value), selTerceroCosecha.Items[x].Text, ddlLote.SelectedValue.ToString().Trim(), null, 0, 0, precioLaborTercero);
                        listaTerceros.Add(terceros);
                    }
                }

                novedadTransaccion = new CtransaccionFertilizante(novedad, nombreNovedad, ddlLote.SelectedValue.Trim(), ddlLote.SelectedItem.Text, ddlSeccion.SelectedValue.Trim(), ddlSeccion.SelectedItem.Text,
                    0, cantidad, listaTerceros, dlDetalle.Items.Count, uMedidad, pesoRacimo, txtFechaD.Text, Convert.ToDecimal(txvJornalesD.Text), precioLabor, null);
                listaNovedadesTransaccion.Add(novedadTransaccion);
                posicionNovedad++;
            }

            dlDetalle.DataSource = listaNovedadesTransaccion;
            dlDetalle.DataBind();

            foreach (DataListItem d in dlDetalle.Items)
            {
                foreach (CtransaccionFertilizante nt in listaNovedadesTransaccion)
                {
                    if (((Label)d.FindControl("lblNovedad")).Text.Trim() == nt.Codnovedad.ToString() & ((Label)d.FindControl("lblSeccion")).Text.Trim() == nt.Codseccion.ToString() &
                        ((Label)d.FindControl("lblLote")).Text.Trim() == nt.Codlote.ToString() & ((Label)d.FindControl("lblRegistro")).Text.Trim() == nt.Registro.ToString())
                    {
                        ((GridView)d.FindControl("gvLotes")).DataSource = nt.Terceros;
                        ((GridView)d.FindControl("gvLotes")).DataBind();

                        ((GridView)d.FindControl("gvItems")).DataSource = nt.ListaItems;
                        ((GridView)d.FindControl("gvItems")).DataBind();
                    }
                }
            }
        }

        private void ManejoControlDetalle(bool manejo)
        {
            txtFechaD.Enabled = manejo;
            txvCantidadD.Enabled = manejo;
            txvJornalesD.Enabled = manejo;
        }
        protected void cargarLotes()
        {
            try
            {
                this.ddlLote.DataSource = lotes.LotesSeccionFinca(this.ddlSeccion.SelectedValue.ToString().Trim(), Convert.ToInt16(this.Session["empresa"]), ddlFinca.SelectedValue.ToString().Trim());
                this.ddlLote.DataValueField = "codigo";
                this.ddlLote.DataTextField = "descripcion";
                this.ddlLote.DataBind();
                this.ddlLote.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar lotes. Correspondiente a: " + ex.Message, "C");
            }
        }
        protected void cargarCombox()
        {
            cargarNovedades();

            try
            {
                DataView fincas = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("aFinca", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"]));
                fincas.RowFilter = "interna=1 and empresa=" + (Convert.ToInt16(this.Session["empresa"])).ToString();
                this.ddlFinca.DataSource = fincas;
                this.ddlFinca.DataValueField = "codigo";
                this.ddlFinca.DataTextField = "descripcion";
                this.ddlFinca.DataBind();
                this.ddlFinca.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar Fincas. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                DataView dvAño = periodo.GetAnosPeriodos(Convert.ToInt32(this.Session["empresa"]));
                dvAño.RowFilter = "cerrado = 0";
                this.ddlAño.DataSource = dvAño;
                this.ddlAño.DataValueField = "año";
                this.ddlAño.DataTextField = "año";
                this.ddlAño.DataBind();
                this.ddlAño.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar unidades de medida. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                DataView dvUmedida = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("gUnidadMedida", "ppa"), "descripcion", Convert.ToInt16(Session["empresa"]));
                dvUmedida.RowFilter = "empresa=" + (Convert.ToInt16(this.Session["empresa"])).ToString();
                this.ddlUmedida.DataSource = dvUmedida;
                this.ddlUmedida.DataValueField = "codigo";
                this.ddlUmedida.DataTextField = "descripcion";
                this.ddlUmedida.DataBind();
                this.ddlUmedida.Items.Insert(0, new ListItem("Seleccione una opción", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar unidades de medida. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.selTerceroCosecha.DataSource = transacciones.SelccionaTercernoNovedad(Convert.ToInt16(this.Session["empresa"]));
                this.selTerceroCosecha.DataValueField = "id";
                this.selTerceroCosecha.DataTextField = "cadena";
                this.selTerceroCosecha.DataBind();

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar trabajadores. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                DataView dvitem = CentidadMetodos.EntidadGet("iItems", "ppa").Tables[0].DefaultView;
                dvitem.RowFilter = "empresa=" + Convert.ToInt32(this.Session["empresa"]) + " and tipo='FI'";
                this.ddlItem.DataSource = dvitem;
                this.ddlItem.DataValueField = "codigo";
                this.ddlItem.DataTextField = "descripcion";
                this.ddlItem.DataBind();
                this.ddlItem.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar items. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                DataView dvUmedida = CentidadMetodos.EntidadGet("gUnidadMedida", "ppa").Tables[0].DefaultView;
                dvUmedida.RowFilter = "empresa=" + (Convert.ToInt16(this.Session["empresa"])).ToString();
                this.ddlumedidaItem.DataSource = dvUmedida;
                this.ddlumedidaItem.DataValueField = "codigo";
                this.ddlumedidaItem.DataTextField = "descripcion";
                this.ddlumedidaItem.DataBind();
                this.ddlumedidaItem.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar unidades de medida del item. Correspondiente a: " + ex.Message, "C");
            }

        }

        protected void cargarSesiones()
        {

            if (ddlFinca.SelectedValue.Trim().Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar una finca", "warning");
                return;
            }
            try
            {
                ddlSeccion.Enabled = true;
                lblSeccion.Enabled = true;

                this.ddlSeccion.DataSource = seccion.SeleccionaSesionesFinca(Convert.ToInt16(this.Session["empresa"]), ddlFinca.SelectedValue);
                this.ddlSeccion.DataValueField = "codigo";
                this.ddlSeccion.DataTextField = "descripcion";
                this.ddlSeccion.DataBind();
                this.ddlSeccion.Items.Insert(0, new ListItem("Seleccione una opción", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar secciones. Correspondiente a: " + ex.Message, "C");
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
            //this.Session["error"] = error;
            //this.Session["paginaAnterior"] = this.Page.Request.FilePath.ToString();
            //this.Session["editar"] = null;
            CerroresGeneral.ManejoError(this, GetType(), error, "warning");
            seguridad.InsertaLog(
                this.Session["usuario"].ToString(),
                operacion,
                ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "er",
                error, ObtenerIP(), Convert.ToInt16(Session["empresa"]));

        }

        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            CcontrolesUsuario.InhabilitarControles(this.upDetalle.Controls);
            CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upEncabezado.Controls);
            CcontrolesUsuario.LimpiarControles(this.upEncabezado.Controls);
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            upDetalle.Visible = false;
            upEncabezado.Visible = false;
            dlDetalle.DataSource = null;
            dlDetalle.DataBind();
            lblTipoDocumento.Visible = false;
            ddlTipoDocumento.Visible = false;
            lblNumero.Visible = false;
            txtNumero.Visible = false;
            this.nilbNuevo.Visible = true;
            gvSubTotales.DataSource = null;
            gvSubTotales.DataBind();
            nilbNuevo.Visible = true;
            lbCancelar.Visible = false;
            lbRegistrar.Visible = false;
            niimbImprimir.Visible = true;
            this.imbConsulta.Visible = true;
            this.niimbRegistro.Visible = true;
            upConsulta.Visible = false;
            CcontrolesUsuario.LimpiarControles(upConsulta.Controls);
            selTerceroCosecha.Visible = false;
            this.Session["editar"] = null;
            ViewState["listaLotes"] = null;
            Session["listaTransaccion"] = null;
            Session["listaNovedadesTransaccion"] = null;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                mensaje, ObtenerIP(), Convert.ToInt16(Session["empresa"]));
        }

        private void ManejoEncabezado()
        {
            HabilitaEncabezado();
            CargarTipoTransaccion();
        }
        private int CompruebaTransaccionExistente()
        {
            try
            {
                object[] objkey = new object[] { Convert.ToInt16(this.Session["empresa"]), this.txtNumero.Text, Convert.ToString(this.ddlTipoDocumento.SelectedValue) };

                if (CentidadMetodos.EntidadGetKey("aTransaccion", "ppa", objkey).Tables[0].DefaultView.Count > 0)
                    return 1;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                ManejoError("Error al comprobar transacción existente. Correspondiente a: " + ex.Message, "C");
                return 1;
            }
        }
        private void actualizarDatos()
        {

            foreach (DataListItem dli in dlDetalle.Items)
            {

                for (int x = 0; x < listaNovedadesTransaccion.Count; x++)
                {
                    if (((Label)dli.FindControl("lblNovedad")).Text == listaNovedadesTransaccion[x].Codnovedad & ((Label)dli.FindControl("lblSeccion")).Text == listaNovedadesTransaccion[x].Codseccion
                        & ((Label)dli.FindControl("lblLote")).Text == listaNovedadesTransaccion[x].Codlote & ((Label)dli.FindControl("lblRegistro")).Text == listaNovedadesTransaccion[x].Registro.ToString())
                    {
                        listaNovedadesTransaccion[x].Racimos = Convert.ToDecimal(((TextBox)dli.FindControl("txvRacimoG")).Text);
                        listaNovedadesTransaccion[x].Jornal = Convert.ToDecimal(((TextBox)dli.FindControl("txvJornalesD")).Text);
                        listaNovedadesTransaccion[x].Cantidad = Convert.ToDecimal(((TextBox)dli.FindControl("txvCantidadG")).Text);

                        foreach (GridViewRow gvr in ((GridView)dli.FindControl("gvLotes")).Rows)
                        {
                            for (int y = 0; y < listaNovedadesTransaccion[x].Terceros.Count; y++)
                            {
                                if (listaNovedadesTransaccion[x].Terceros[y].Codtercero == Convert.ToInt16(gvr.Cells[1].Text))
                                {
                                    listaNovedadesTransaccion[x].Terceros[y].Cantidad = Convert.ToDecimal(((TextBox)gvr.FindControl("txtCantidad")).Text);
                                    listaNovedadesTransaccion[x].Terceros[y].Jornal = Convert.ToDecimal(((TextBox)gvr.FindControl("txtJornal")).Text);
                                }

                            }
                        }

                    }
                }
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
                        CerroresGeneral.ManejoError(this, GetType(), "Transacción existente. Por favor corrija", "warning");
                        return;
                    }
                }
                this.txtNumero.Enabled = false;
                //CcontrolesUsuario.HabilitarControles(this.upEncabezado.Controls);
                this.niimbImprimir.Visible = false;
                this.nilbNuevo.Visible = false;
                //this.txtFecha.Visible = false;
                this.txtFecha.Focus();
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
            listaTransaccion = null;

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
        private string ConsecutivoTransaccion()
        {
            string numero = "";

            try
            {
                numero = transacciones.RetornaNumeroTransaccion(Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(this.Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al obtener el número de transacción. Correspondiente a: " + ex.Message, "C");
            }
            return numero;
        }
        private void ComportamientoTransaccion()
        {
            upEncabezado.Visible = true;
            upDetalle.Visible = true;

            CcontrolesUsuario.ComportamientoCampoEntidadTransaccionUP(upEncabezado, "aTransaccion", Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(this.Session["empresa"]));
            CcontrolesUsuario.ComportamientoCampoEntidadTransaccionUP(upDetalle, "aTransaccionDetalle", Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(this.Session["empresa"]));
            ////this.imbCargar.Visible = true;
        }
        private void ConfiguracionNovedad(string novedad)
        {
            try
            {
                if (novedad.Trim().Length > 0)
                {
                    ddlLote.Visible = Convert.ToBoolean(NovedadConfig(12, ddlNovedad.SelectedValue));
                    lblLote.Visible = Convert.ToBoolean(NovedadConfig(12, ddlNovedad.SelectedValue));
                    ddlSeccion.Visible = Convert.ToBoolean(NovedadConfig(12, ddlNovedad.SelectedValue));
                    lblSeccion.Visible = Convert.ToBoolean(NovedadConfig(12, ddlNovedad.SelectedValue));
                    lblFechaD.Enabled = true;

                    if (Convert.ToBoolean(NovedadConfig(12, ddlNovedad.SelectedValue)) == false)
                    {
                        ddlLote.DataSource = null;
                        ddlLote.DataBind();
                        ddlLote.SelectedIndex = -1;
                        ddlSeccion.DataSource = null;
                        ddlSeccion.DataBind();
                        ddlSeccion.SelectedIndex = -1;
                    }

                    ddlUmedida.SelectedValue = NovedadConfig(4, ddlNovedad.SelectedValue).ToString().Trim();
                    this.Session["manejalote"] = Convert.ToBoolean(NovedadConfig(12, ddlNovedad.SelectedValue));
                    txvCantidadD.Enabled = !Convert.ToBoolean(NovedadConfig(18, ddlNovedad.SelectedValue));
                    txvCantidadD.Enabled = !Convert.ToBoolean(NovedadConfig(18, ddlNovedad.SelectedValue));
                    txvJornalesD.Enabled = Convert.ToBoolean(NovedadConfig(19, ddlNovedad.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al recuperar el configuración de la novedad. Correspondiente a: " + ex.Message, "C");
            }
        }

        private void LimpiarDDL()
        {
            ddlSeccion.DataSource = null;
            ddlSeccion.DataBind();
            ddlLote.DataSource = null;
            ddlLote.DataBind();
        }
        private void repartirCantidades()
        {
            decimal diferencia = 0, cantidadTotal = 0, cantidadTotalAsignada = 0, cantidadTercero = 0, cantidadJornales = 0, jornalesTotales = 0, jornalesAsignados = 0, diferenciaJornal;
            decimal subCantidad = 0, subRacimos = 0, subjornales = 0;
            Csubtotal subT;
            bool validarN = false;
            int pn = 0, noTerceros = 0;
            int contarN = 0;

            foreach (DataListItem dl in dlDetalle.Items)
            {
                for (int x = 0; x < subtotal.Count; x++)
                {
                    if (((Label)dl.FindControl("lblNovedad")).Text == subtotal[x].novedades)
                    {
                        validarN = true;
                        pn = x;
                        break;
                    }
                }
                if (validarN)
                {
                    subtotal[pn].subCantidad = subtotal[pn].subCantidad + Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text);
                    validarN = false;
                }
                else
                {
                    subT = new Csubtotal(((Label)dl.FindControl("lblNovedad")).Text, ((Label)dl.FindControl("lblDesNovedad")).Text, Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text), 0, 0);
                    subtotal.Add(subT);
                }

            }

            for (int z = 0; z < subtotal.Count; z++)
            {
                foreach (DataListItem dl in dlDetalle.Items)
                {
                    if (((Label)dl.FindControl("lblNovedad")).Text == subtotal[z].novedades)
                        contarN++;
                }

            }
            subtotal = new List<Csubtotal>();
            foreach (DataListItem dl in dlDetalle.Items)
            {
                cantidadTotal = Decimal.Round(Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text), 2);
                cantidadTotalAsignada = 0;
                jornalesAsignados = 0;
                jornalesTotales = Convert.ToDecimal(((TextBox)dl.FindControl("txvJornalesD")).Text);
                noTerceros = 0;
                diferencia = 0;
                cantidadTercero = 0;
                subCantidad += cantidadTotal;
                subjornales += jornalesTotales;
                subRacimos += Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text);

                foreach (GridViewRow r in ((GridView)dl.FindControl("gvLotes")).Rows)
                {
                    noTerceros += 1;
                }

                if (Convert.ToBoolean(NovedadConfig(32, ((Label)dl.FindControl("lblNovedad")).Text)) == true)
                {
                    foreach (GridViewRow r in ((GridView)dl.FindControl("gvLotes")).Rows)
                    {
                        cantidadTercero = Convert.ToDecimal(Decimal.Round((cantidadTotal / noTerceros), 2));
                        cantidadJornales = decimal.Round(Convert.ToDecimal(jornalesTotales / noTerceros), 2);
                        cantidadTotalAsignada += cantidadTercero;
                        jornalesAsignados += cantidadJornales;
                        ((TextBox)r.FindControl("txtCantidad")).Text = Convert.ToString(cantidadTercero);
                        ((TextBox)r.FindControl("txtJornal")).Text = Convert.ToString(cantidadJornales);
                    }
                    diferencia = cantidadTotal - cantidadTotalAsignada;
                    diferenciaJornal = jornalesTotales - jornalesAsignados;

                    int fila = new Random().Next(0, noTerceros);
                    foreach (GridViewRow r in ((GridView)dl.FindControl("gvLotes")).Rows)
                    {
                        if (r.RowIndex == fila)
                        {
                            ((TextBox)r.FindControl("txtCantidad")).Text = Convert.ToString(Decimal.Round((Convert.ToDecimal(((TextBox)r.FindControl("txtCantidad")).Text) + diferencia), 2));
                            ((TextBox)r.FindControl("txtJornal")).Text = Convert.ToString(Convert.ToDecimal(((TextBox)r.FindControl("txtJornal")).Text) + diferenciaJornal);
                        }
                    }
                }
                else
                {
                    foreach (GridViewRow r in ((GridView)dl.FindControl("gvLotes")).Rows)
                    {
                        cantidadTercero = Convert.ToDecimal(Decimal.Round((cantidadTotal / noTerceros), 0));
                        cantidadJornales = decimal.Round(Convert.ToDecimal(jornalesTotales / noTerceros), 2);
                        cantidadTotalAsignada += cantidadTercero;
                        jornalesAsignados += cantidadJornales;
                        ((TextBox)r.FindControl("txtCantidad")).Text = Convert.ToString(cantidadTercero);
                        ((TextBox)r.FindControl("txtJornal")).Text = Convert.ToString(cantidadJornales);
                    }
                    diferencia = cantidadTotal - cantidadTotalAsignada;
                    diferenciaJornal = jornalesTotales - jornalesAsignados;

                    int fila = new Random().Next(0, noTerceros);
                    foreach (GridViewRow r in ((GridView)dl.FindControl("gvLotes")).Rows)
                    {
                        if (r.RowIndex == fila)
                        {
                            ((TextBox)r.FindControl("txtCantidad")).Text = Convert.ToString(Decimal.Round((Convert.ToDecimal(((TextBox)r.FindControl("txtCantidad")).Text) + diferencia), 0));
                            ((TextBox)r.FindControl("txtJornal")).Text = Convert.ToString(Convert.ToDecimal(((TextBox)r.FindControl("txtJornal")).Text) + diferenciaJornal);
                        }
                    }
                }
            }
            actualizarDatos();
            CalcularSubtotal();
        }
        private void CalcularSubtotal()
        {
            gvSubTotales.DataSource = null;
            gvSubTotales.DataBind();
            subtotal = new List<Csubtotal>();
            decimal subCantidad = 0, subRacimos = 0, subjornales = 0;
            Csubtotal sub;
            bool validarNovedad = false;
            int posicionSubtotal = 0;

            if (listaNovedadesTransaccion != null)
            {
                for (int x = 0; x < listaNovedadesTransaccion.Count; x++)
                {
                    subRacimos = 0;
                    subCantidad = 0;
                    subjornales = 0;

                    for (int z = 0; z < subtotal.Count; z++)
                    {
                        if (listaNovedadesTransaccion[x].Codnovedad == subtotal[z].novedades)
                        {
                            validarNovedad = true;
                            posicionSubtotal = z;
                            break;
                        }
                    }

                    if (validarNovedad)
                    {
                        subCantidad += listaNovedadesTransaccion[x].Cantidad;
                        subjornales += listaNovedadesTransaccion[x].Jornal;
                        subRacimos += listaNovedadesTransaccion[x].Racimos;
                        subtotal[posicionSubtotal].subCantidad = subtotal[posicionSubtotal].subCantidad + subCantidad;
                        subtotal[posicionSubtotal].subJornal = subtotal[posicionSubtotal].subJornal + subjornales;
                        subtotal[posicionSubtotal].subRacimo = subtotal[posicionSubtotal].subRacimo + subRacimos;
                        validarNovedad = false;
                    }
                    else
                    {
                        subCantidad += listaNovedadesTransaccion[x].Cantidad;
                        subjornales += listaNovedadesTransaccion[x].Jornal;
                        subRacimos += listaNovedadesTransaccion[x].Racimos;
                        sub = new Csubtotal(listaNovedadesTransaccion[x].Codnovedad, listaNovedadesTransaccion[x].Desnovedad, subCantidad, subRacimos, subjornales);
                        subtotal.Add(sub);
                    }
                }

                gvSubTotales.DataSource = subtotal;
                gvSubTotales.DataBind();
                this.Session["subtotal"] = subtotal;
            }
        }
        private void LiquidaTransaccioin(decimal racimos, decimal cant)
        {
            try
            {
                decimal noRacimos = 0, cantidad = 0, totalKilos = 0, cantidadP = 0;
                List<int> listacantidadesRepartidas = new List<int>();
                List<Decimal> difKilosLote = null;
                difKilosLote = new List<decimal>();
                int difTotalKg = 0;

                foreach (DataListItem dl in dlDetalle.Items)
                {
                    string loteI = null;
                    string novedadI = null;

                    if (((Label)dl.FindControl("lblNovedad")) != null)
                    {
                        novedadI = ((Label)dl.FindControl("lblNovedad")).Text;

                        if (((Label)dl.FindControl("lblLote")) != null)
                            loteI = ((Label)dl.FindControl("lblLote")).Text;

                        if (novedad.ManejaCanalNovedad(novedadI, Convert.ToInt16(this.Session["empresa"])) == true)
                        {
                            decimal cantidadcanal = novedad.SeleccionaMetrajeTipoCanalNovedad(novedadI, loteI, Convert.ToInt16(this.Session["empresa"]));

                            if (Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text) > cantidadcanal)
                            {
                                ((TextBox)dl.FindControl("txvCantidadG")).Text = cantidadcanal.ToString();
                                CerroresGeneral.ManejoError(this, GetType(), "La cantidad para esta novedad no esta permitido debe elegir una cantidad menor", "warning");

                                return;
                            }
                        }
                    }
                }

                if (this.Session["editar"] == null)
                {
                    foreach (DataListItem dl in dlDetalle.Items)
                    {
                        ((TextBox)dl.FindControl("txvCantidadG")).Enabled = !Convert.ToBoolean(NovedadConfig(18, ddlNovedad.SelectedValue));
                        ((TextBox)dl.FindControl("txvRacimoG")).Enabled = Convert.ToBoolean(NovedadConfig(18, ddlNovedad.SelectedValue));
                        ((TextBox)dl.FindControl("txvJornalesD")).Enabled = !Convert.ToBoolean(NovedadConfig(19, ddlNovedad.SelectedValue));
                    }
                }

                foreach (DataListItem dl in dlDetalle.Items)
                {
                    noRacimos += Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text);
                    cantidad += Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text);
                }

                if (dlDetalle.Items.Count > 0)
                {
                    totalKilos = 0;
                    cantidadP = 0;

                    foreach (DataListItem dl in dlDetalle.Items)
                    {
                        if (((Label)dl.FindControl("lblLote")).Text.Trim().Length > 0)
                        {
                            cantidadP = Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text);
                            totalKilos += cantidadP;
                        }
                    }

                    foreach (DataListItem dl in dlDetalle.Items)
                    {
                        if (((Label)dl.FindControl("lblLote")).Text.Trim().Length > 0)
                            ((TextBox)dl.FindControl("txvCantidadG")).Text = Convert.ToString(Decimal.Round((Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text) + difTotalKg), 2));
                    }
                }

                foreach (DataListItem d in dlDetalle.Items)
                {
                    ((DropDownList)d.FindControl("ddlTerceroGrilla")).DataSource = transacciones.SelccionaTercernoNovedad(Convert.ToInt16(this.Session["empresa"]));
                    ((DropDownList)d.FindControl("ddlTerceroGrilla")).DataValueField = "id";
                    ((DropDownList)d.FindControl("ddlTerceroGrilla")).DataTextField = "cadena";
                    ((DropDownList)d.FindControl("ddlTerceroGrilla")).DataBind();
                    DataView dvItemDetalle = CentidadMetodos.EntidadGet("iItems", "ppa").Tables[0].DefaultView;
                    dvItemDetalle.RowFilter = "empresa=" + Convert.ToInt32(this.Session["empresa"]) + " and tipo='FI'";
                    ((DropDownList)d.FindControl("ddlItemDetalle")).DataSource = dvItemDetalle;
                    ((DropDownList)d.FindControl("ddlItemDetalle")).DataValueField = "codigo";
                    ((DropDownList)d.FindControl("ddlItemDetalle")).DataTextField = "descripcion";
                    ((DropDownList)d.FindControl("ddlItemDetalle")).DataBind();
                    ((DropDownList)d.FindControl("ddlItemDetalle")).Items.Insert(0, new ListItem("", ""));

                    DataView dvUmedida = CentidadMetodos.EntidadGet("gUnidadMedida", "ppa").Tables[0].DefaultView;
                    dvUmedida.RowFilter = "empresa=" + this.Session["empresa"].ToString();
                    ((DropDownList)d.FindControl("ddlUmedidaItem")).DataSource = dvUmedida;
                    ((DropDownList)d.FindControl("ddlUmedidaItem")).DataValueField = "codigo";
                    ((DropDownList)d.FindControl("ddlUmedidaItem")).DataTextField = "descripcion";
                    ((DropDownList)d.FindControl("ddlUmedidaItem")).DataBind();
                    ((DropDownList)d.FindControl("ddlUmedidaItem")).Items.Insert(0, new ListItem("", ""));
                }

                try
                {
                    this.selTerceroCosecha.DataSource = transacciones.SelccionaTercernoNovedad(Convert.ToInt16(this.Session["empresa"]));
                    this.selTerceroCosecha.DataValueField = "id";
                    this.selTerceroCosecha.DataTextField = "cadena";
                    this.selTerceroCosecha.DataBind();

                }
                catch (Exception ex)
                {
                    ManejoError("Error al cargar trabajadores. Correspondiente a: " + ex.Message, "C");
                }

                txvJornalesD.Text = "0";
                txvCantidadD.Text = "0";
                repartirCantidades();
                CerroresGeneral.ManejoError(this, GetType(), "Liquidación generada exitosamente", "info");

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
                if (!IsPostBack)
                {
                    //CargaCampos();
                    manejoCancelar();
                    TabRegistro();

                }
            }

        }
        protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                        nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }
            try
            {
                this.Session["editar"] = false;
                listaNovedadesTransaccion = null;
                this.Session["numerotransaccion"] = null;
                this.Session["subtotal"] = null;
                this.Session["numerotransaccion"] = null;
                this.Session["editar"] = null;
                this.Session["lote"] = null;
                LimpiarDDL();
                ddlSeccion.DataSource = null;
                ddlSeccion.DataBind();
                ddlLote.DataSource = null;
                ddlLote.DataBind();
                gvSubTotales.DataSource = null;
                gvSubTotales.DataBind();
                dlDetalle.DataSource = null;
                dlDetalle.DataBind();
                CcontrolesUsuario.LimpiarControles(this.upEncabezado.Controls);
                CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
                cargarCombox();
                this.txtNumero.Text = ConsecutivoTransaccion();
                ComportamientoConsecutivo();
                upEncabezado.Visible = true;
                upDetalle.Visible = true;
                niimbImprimir.Visible = false;
                ComportamientoTransaccion();
                this.hdTransaccionConfig = CcontrolesUsuario.TipoTransaccionConfig(this.ddlTipoDocumento.SelectedValue, Convert.ToInt16(Session["empresa"]));
                selTerceroCosecha.Visible = false;
                string[] split = hdTransaccionConfig.Split('*');
                selTerceroCosecha.Visible = Convert.ToBoolean(split[28]);
                referencia();
                manejoItem();

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tipo de transacción debido a: " + ex.Message, "C");
            }

        }

        private void manejoItem()
        {
            if (ddlItem.Visible == true)
            {
                txvNoBultos.Visible = true;
                txvNoPalma.Visible = true;
                txvPesoBulto.Visible = true;
                txvNoBultos.Enabled = false;
                txvNoPalma.Enabled = false;
                txvPesoBulto.Text = "50";
                lblCantidadD.Text = "Dosis";
            }
            else
            {
                txvNoBultos.Visible = false;
                txvNoPalma.Visible = false;
                txvPesoBulto.Visible = false;
                txvPesoBulto.Enabled = false;
                lblCantidadD.Text = "Cantidad";

            }
        }

        protected void lbNuevo_Click(object sender, EventArgs e)
        {

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "I", Convert.ToInt16(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }
            //CcontrolesUsuario.HabilitarControles(upEncabezado.Controls);
            this.Session["editar"] = false;
            listaNovedadesTransaccion = null;
            this.Session["numerotransaccion"] = null;
            ManejoEncabezado();
            CargarTipoTransaccion();
            LimpiarDDL();
            ddlSeccion.DataSource = null;
            ddlSeccion.DataBind();
            ddlLote.DataSource = null;
            ddlLote.DataBind();
            gvSubTotales.DataSource = null;
            gvSubTotales.DataBind();
            niimbImprimir.Visible = false;
            dlLotes.DataSource = null;
            ViewState["listaLotes"] = null;
            Session["listaTransaccion"] = null;
            Session["listaNovedadesTransaccion"] = null;
            dlLotes.DataBind();

        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            manejoCancelar();
        }

        private void manejoCancelar()
        {
            InHabilitaEncabezado();
            upEncabezado.Visible = false;
            upDetalle.Visible = false;
            CcontrolesUsuario.LimpiarControles(this.upEncabezado.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upEncabezado.Controls);
            CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upDetalle.Controls);
            niimbImprimir.Visible = false;
            listaTransaccion = null;
            this.dlDetalle.DataSource = null;
            this.dlDetalle.DataBind();
            Session["lote"] = null;
            this.lbRegistrar.Visible = false;
            this.lbCancelar.Visible = false;
            this.niimbImprimir.Visible = false;
            this.Session["subtotal"] = null;
            ddlSeccion.DataSource = null;
            ddlSeccion.DataBind();
            ddlLote.DataSource = null;
            ddlLote.DataBind();
            this.Session["numerotransaccion"] = null;
            txtFecha.Enabled = false;
            txtFechaD.Enabled = false;
            gvSubTotales.DataSource = null;
            gvSubTotales.DataBind();
            dlLotes.DataSource = null;
            dlLotes.DataBind();
            selTerceroCosecha.Visible = false;
            this.Session["editar"] = null;
            listaTransaccion = null;
            listaNovedadesTransaccion = null;
            ViewState["listaLotes"] = null;
            Session["listaTransaccion"] = null;
            Session["listaNovedadesTransaccion"] = null;

            if (imbConsulta.Enabled == false)
            {
                upConsulta.Visible = true;
                nilbNuevo.Visible = false;
                manejoConsulta();
            }
            else
            {
                upConsulta.Visible = false;
                nilbNuevo.Visible = true;
                TabRegistro();
            }
        }

        protected void imbCargar_Click(object sender, EventArgs e)
        {
            cargarDL();
        }
        protected void imbCargar_Click1(object sender, EventArgs e)
        {


            if (ddlLote.Visible & ddlNovedad.Visible)
            {
                if (ValidaAñoLote() == true)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "El lote seleccionado esta fuera del rango de siembra de la novedad, por favor corrija ", "warning");
                    imbCargar.Enabled = false;
                    return;
                }
                else
                {
                    imbCargar.Enabled = true;

                }
            }
            if (txvCantidadD.Enabled & txvCantidadD.Visible & ddlNovedad.Visible)
            {
                if (Convert.ToDecimal(txvCantidadD.Text.Trim()) == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "La cantidad debe ser diferente de cero", "warning");
                    return;
                }
            }
            if (txtFechaD.Enabled & txtFechaD.Visible & ddlNovedad.Visible)
            {
                if (txtFechaD.Text.Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "debe ingresar una fecha para continuar", "warning");
                    return;

                }
            }

            decimal subCantidad = 0, subJornal = 0, subRacimos = 0, cantidad = 0;
            int contador = 0;

            foreach (GridViewRow gv in gvSubTotales.Rows)
            {
                if (ddlNovedad.SelectedValue.Trim() == gv.Cells[0].Text.Trim())
                {
                    subCantidad = Convert.ToDecimal(gv.Cells[2].Text);
                    subRacimos = Convert.ToDecimal(gv.Cells[3].Text);
                    subJornal = Convert.ToDecimal(gv.Cells[4].Text);
                }
            }
            cantidad = Convert.ToDecimal(txvCantidadD.Text);

            if (ddlNovedad.Visible)
            {
                if (Convert.ToBoolean(NovedadConfig(12, ddlNovedad.SelectedValue)) == true)
                {
                    if (ddlLote.SelectedValue.Length == 0)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar un lote antes de continuar", "warning");
                        return;
                    }
                }

                if (novedad.ManejaCanalNovedad(ddlNovedad.SelectedValue.Trim(), Convert.ToInt16(this.Session["empresa"])) == true)
                {
                    decimal cantidadcanal = novedad.SeleccionaMetrajeTipoCanalNovedad(ddlNovedad.SelectedValue.Trim(), ddlLote.SelectedValue.Trim(), Convert.ToInt16(this.Session["empresa"]));
                    if (Convert.ToDecimal(txvCantidadD.Text) > cantidadcanal)
                    {
                        txvCantidadD.Text = cantidadcanal.ToString();
                        CerroresGeneral.ManejoError(this, GetType(), "La cantidad para esta novedad no esta permitido debe elegir una cantidad menor", "warning");
                        return;
                    }
                }

                if (novedad.validaManejaPalmas(ddlNovedad.SelectedValue.Trim(), Convert.ToInt16(this.Session["empresa"])) == 1)
                {
                    decimal palmasLote = novedad.SeleccionaPalmasLote(ddlLote.SelectedValue.Trim(), Convert.ToInt16(this.Session["empresa"]));
                    if (Convert.ToDecimal(txvCantidadD.Text) > palmasLote)
                    {
                        txvCantidadD.Text = palmasLote.ToString();
                        CerroresGeneral.ManejoError(this, GetType(), "La cantidad para esta novedad no esta permitido debe elegir una cantidad menor", "warning");
                        return;
                    }
                }


                for (int x = 0; x < selTerceroCosecha.Items.Count; x++)
                {
                    if (selTerceroCosecha.Items[x].Selected)
                        contador++;
                }
                if (contador == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe selecionar un trabajador para continuar", "warning");
                    return;
                }

                if (listaPrecios.SeleccionaPrecioNovedadAño(Convert.ToInt16(Session["empresa"]), ddlNovedad.SelectedValue, Convert.ToDateTime(txtFechaD.Text).Year) == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "La labor seleccionada no tiene precio en el año, por favor registrar precio para continuar.", "warning");
                    return;
                }
            }
            if (selTerceroCosecha.Visible)
            {
                cargarDL();
                LiquidaTransaccioin(0, 0);
                ddlLote.Enabled = true;
                ddlFinca.Enabled = true;
                ddlNovedad.Enabled = true;
                ddlFinca.Enabled = true;
                ddlSeccion.Enabled = true;
                lblFecha.Enabled = true;
                ConfiguracionNovedad(ddlNovedad.SelectedValue.ToString().Trim());
                LiquidaTransaccioin(0, 0);
                List<CtransaccionFertilizante> listaTransaccionGV = null;

                foreach (DataListItem dli in dlDetalle.Items)
                {

                    if (listaTransaccion != null)
                    {
                        int registroR = Convert.ToInt32(((Label)dli.FindControl("lblRegistro")).Text);
                        trafer = null;
                        for (int x = 0; x < listaTransaccion.Count; x++)
                        {
                            listaTransaccionGV = new List<CtransaccionFertilizante>();

                            if (dli.ItemIndex == x)
                            {
                                trafer = new CtransaccionFertilizante(listaTransaccion[x].IdItem,
                                listaTransaccion[x].Item,
                                listaTransaccion[x].UMedidaItem,
                                listaTransaccion[x].CantidadItem,
                                listaTransaccion[x].NoBulto,
                                listaTransaccion[x].PeBulto,
                                0,
                                listaTransaccion[x].RegistroItem,
                                registroR);
                                listaTransaccionGV.Add(trafer);
                            }
                        }


                        ((GridView)dli.FindControl("gvItems")).DataSource = listaTransaccionGV;
                        ((GridView)dli.FindControl("gvItems")).DataBind();

                    }
                }
            }
            else
            {
                if (ddlItem.Visible)
                {
                    bool validaritem = false;

                    if (txvCantidadD.Visible)
                    {
                        if (txvCantidadD.Text.Trim().Length > 0)
                        {
                            if (Convert.ToDecimal(txvCantidadD.Text) == 0)
                            {
                                CerroresGeneral.ManejoError(this, GetType(), "Ingrese una dosis mayor a 0", "warning");
                                return;
                            }
                        }
                    }

                    if (txvPesoBulto.Visible)
                    {
                        if (txvPesoBulto.Text.Trim().Length > 0)
                        {
                            if (Convert.ToDecimal(txvPesoBulto.Text) == 0)
                            {
                                CerroresGeneral.ManejoError(this, GetType(), "Ingrese un peso de bulto mayor a 0", "warning");
                                return;
                            }
                        }
                    }

                    if (txvNoPalma.Visible)
                    {
                        if (txvNoPalma.Text.Trim().Length > 0)
                        {
                            if (Convert.ToDecimal(txvNoPalma.Text) == 0)
                            {
                                CerroresGeneral.ManejoError(this, GetType(), "No puede ingresar no hay palmas en el lote", "warning");
                                return;
                            }
                        }
                    }

                    if (txvNoBultos.Visible)
                    {
                        if (txvNoBultos.Text.Trim().Length > 0)
                        {
                            if (Convert.ToDecimal(txvNoBultos.Text) == 0)
                            {
                                CerroresGeneral.ManejoError(this, GetType(), "No puede ingresar 0 bultos", "warning");
                                return;
                            }
                        }
                    }

                    foreach (DataListItem dli in dlLotes.Items)
                    {
                        GridView gvItemss = (GridView)dli.FindControl("gvItems");
                        foreach (GridViewRow gvri in gvItemss.Rows)
                        {
                            if (gvri.Cells[1].Text.Trim() == ddlItem.SelectedValue.Trim() & ((Label)dli.FindControl("lblCodLote")).Text == ddlLote.SelectedValue.Trim())
                            {
                                validaritem = true;
                            }
                        }
                    }

                    if (validaritem == true)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "El item ya fue ingresado no puede ser ingresado 2 veces", "warning");
                        return;
                    }


                    if (!txvNoBultos.Visible)
                    {
                        txvNoBultos.Text = "0";
                        txvPesoBulto.Text = "0";
                    }


                    if (listaTransaccion == null)
                    {
                        listaTransaccion = new List<CtransaccionFertilizante>();
                        trafer = new CtransaccionFertilizante(ddlItem.SelectedValue.Trim(), ddlItem.SelectedItem.Text.Trim(), ddlumedidaItem.SelectedValue.Trim(),
                        Convert.ToDecimal(txvCantidadD.Text), Convert.ToDecimal(txvNoBultos.Text), Convert.ToDecimal(txvPesoBulto.Text), Convert.ToInt32(txvNoPalma.Text), listaTransaccion.Count + 1, 0);
                        listaTransaccion.Add(trafer);
                        lot = new Clotes(ddlLote.SelectedValue.Trim(), listaTransaccion);
                        listaLotes = new List<Clotes>();
                        listaLotes.Add(lot);

                    }
                    else
                    {
                        bool validar = false;

                        for (int x = 0; x < listaLotes.Count; x++)
                        {
                            if (ddlLote.SelectedValue.Trim() == listaLotes[x].Lote)
                            {
                                listaTransaccion = new List<CtransaccionFertilizante>();
                                listaTransaccion = listaLotes[x].Items;
                                trafer = new CtransaccionFertilizante(ddlItem.SelectedValue.Trim(), ddlItem.SelectedItem.Text.Trim(), ddlumedidaItem.SelectedValue.Trim(),
                                  Convert.ToDecimal(txvCantidadD.Text), Convert.ToDecimal(txvNoBultos.Text), Convert.ToDecimal(txvPesoBulto.Text), Convert.ToInt32(txvNoPalma.Text), listaTransaccion.Count + 1, 0);
                                listaTransaccion.Add(trafer);
                                listaLotes[x].Items = listaTransaccion;
                                validar = true;
                            }
                        }

                        if (validar == false)
                        {
                            listaTransaccion = new List<CtransaccionFertilizante>();
                            trafer = new CtransaccionFertilizante(ddlItem.SelectedValue.Trim(), ddlItem.SelectedItem.Text.Trim(), ddlumedidaItem.SelectedValue.Trim(),
                            Convert.ToDecimal(txvCantidadD.Text), Convert.ToDecimal(txvNoBultos.Text), Convert.ToDecimal(txvPesoBulto.Text), Convert.ToInt32(txvNoPalma.Text), listaTransaccion.Count + 1, 0);
                            listaTransaccion.Add(trafer);
                            lot = new Clotes(ddlLote.SelectedValue.Trim(), listaTransaccion);
                            listaLotes.Add(lot);
                        }

                    }

                    this.dlLotes.DataSource = listaLotes;
                    this.dlLotes.DataBind();


                    foreach (DataListItem gvr in dlLotes.Items)
                    {
                        for (int x = 0; x < listaLotes.Count; x++)
                        {
                            if (((Label)gvr.FindControl("lblCodLote")).Text == listaLotes[x].Lote)
                            {
                                ((GridView)(gvr.FindControl("gvItems"))).DataSource = listaLotes[x].Items;
                                ((GridView)(gvr.FindControl("gvItems"))).DataBind();
                                break;
                            }
                        }
                    }


                }
            }

        }

        protected void ddlNovedad_SelectedIndexChanged(object sender, EventArgs e)
        {
            manejoNovedad();
        }

        private void manejoNovedad()
        {
            CcontrolesUsuario.LimpiarControles(upDetalle.Controls);
            ConfiguracionNovedad(ddlNovedad.SelectedValue.ToString().Trim());
            imbCargar.Enabled = true;
            gvSubTotales.DataSource = null;
            gvSubTotales.DataBind();
            txtIdNovedad.Text = ddlNovedad.SelectedValue;

            CalcularSubtotal();
            ddlNovedad.Focus();
        }
        protected void ddlSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarLotes();
            ddlSeccion.Focus();
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {

                int verificarCRJ = 0;

                if (ddlReferencia.Visible & ddlReferencia.SelectedValue.Trim().Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar una referencia valida para la transacción", "warning");
                    return;
                }
                if (ddlItem.Visible)
                {
                    if (dlLotes.Items.Count == 0)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "El nivel de detalle de la transacción debe tener por lo menos un registro para los items", "warning");
                        return;
                    }
                }

                if (dlDetalle.Items.Count <= 0 & dlLotes.Items.Count <= 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "El nivel de detalle de la transacción debe tener por lo menos un registro", "warning");
                    return;
                }
                bool validar = false;
                if (dlDetalle.Items.Count > 0)
                {
                    foreach (DataListItem dli in dlDetalle.Items)
                    {
                        if (((GridView)dli.FindControl("gvItems")).Rows.Count == 0)
                        {
                            validar = true;
                        }
                    }
                }

                if (validar)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe haber un detalle del item en todos los lotes", "warning");
                    return;
                }

                switch (verificarCRJ)
                {
                    case 1:
                        CerroresGeneral.ManejoError(this, GetType(), "La cantidad no puede ser diferentes en el detalle de la transacción", "warning");
                        return;
                    case 2:
                        CerroresGeneral.ManejoError(this, GetType(), "Los racimos no pueden ser diferentes en el detalle de la transacción", "warning");
                        return;
                    case 3:
                        CerroresGeneral.ManejoError(this, GetType(), "Cantidad de jornales no pueden  ser diferentes en el detalle de la transacción", "warning");
                        return;
                }
                this.Session["subtotal"] = null;
            }

            if (upEncabezado.Visible == true)
            {
                bool validar = false;

                if (txtFecha.Enabled == true)
                {
                    if (txtFecha.Text.Trim().Length == 0)
                        validar = true;
                }

                if (txtRemision.Enabled == true)
                {
                    if (txtRemision.Text.Trim().Length == 0)
                        validar = true;
                }
            }
            if (selTerceroCosecha.Visible)
            {
                LiquidaTransaccioin(0, 0);
            }
            Guardar();
        }
        protected void ddlLote_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                if (ddlNovedad.Visible == true)
                {
                    if (ValidaAñoLote() == true)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "El lote seleccionado esta fuera del rango de siembra de la novedad, por favor corrija ", "warning");
                        imbCargar.Enabled = false;
                        return;
                    }
                    else
                    {
                        imbCargar.Enabled = true;
                    }
                }

                if (txvNoPalma.Visible == true)
                {
                    txvNoPalma.Text = lotes.SeleccionaNoPalmasLote(Convert.ToInt16(this.Session["empresa"]), ddlLote.SelectedValue.Trim()).ToString();
                    txvNoBultos.Text = "0";
                    txvPesoBulto.Text = "50";
                    txvCantidadD.Text = "0";
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al retornar información del lote debido a: " + ex.Message, "C");
            }

            ddlLote.Focus();
        }

        private bool ValidaAñoLote()
        {
            int desde = 0, hasta = 0, añoSiembra = 0, difAño = 0;
            bool valor = false;

            if (Convert.ToBoolean(NovedadConfig(25, ddlNovedad.SelectedValue)) == true)
            {
                desde = Convert.ToInt16(NovedadConfig(26, ddlNovedad.SelectedValue));
                hasta = Convert.ToInt16(NovedadConfig(27, ddlNovedad.SelectedValue));
                añoSiembra = Convert.ToInt16(LoteConfig(5, ddlLote.SelectedValue));
                difAño = Convert.ToInt16(Convert.ToDateTime(txtFecha.Text).Year) - añoSiembra;
                if (difAño >= desde && difAño <= hasta)
                    valor = false;
                else
                    valor = true;
            }

            return valor;

        }
        protected void dlDetalle_DeleteCommand(object source, DataListCommandEventArgs e)
        {
            int posicionNovedad = 0;
            string novedad = ((Label)dlDetalle.Items[e.Item.ItemIndex].FindControl("lblNovedad")).Text.Trim();
            string seccion = ((Label)dlDetalle.Items[e.Item.ItemIndex].FindControl("lblSeccion")).Text.Trim();
            string lote = ((Label)dlDetalle.Items[e.Item.ItemIndex].FindControl("lblLote")).Text.Trim();
            int registro = Convert.ToInt16(((Label)dlDetalle.Items[e.Item.ItemIndex].FindControl("lblRegistro")).Text.Trim());

            foreach (CtransaccionFertilizante nt in listaNovedadesTransaccion)
            {
                if (lote == nt.Codlote & novedad == nt.Codnovedad & nt.Codseccion == seccion & registro == nt.Registro)
                    break;
                posicionNovedad++;
            }
            listaNovedadesTransaccion.RemoveAt(posicionNovedad);
            listaNovedadesTransaccion = reasignarRegistros(listaNovedadesTransaccion);
            dlDetalle.DataSource = listaNovedadesTransaccion;
            dlDetalle.DataBind();


            decimal cantidadTotal = 0, jornalesTotales = 0, subCantidad = 0, subJornales = 0, subRacimos = 0;

            foreach (DataListItem d in dlDetalle.Items)
            {
                cantidadTotal = Convert.ToDecimal(((TextBox)d.FindControl("txvCantidadG")).Text);
                jornalesTotales = Convert.ToDecimal(((TextBox)d.FindControl("txvJornalesD")).Text);
                subCantidad += cantidadTotal;
                subJornales += jornalesTotales;
                subRacimos += Convert.ToDecimal(((TextBox)d.FindControl("txvRacimoG")).Text);

                foreach (CtransaccionFertilizante nt in listaNovedadesTransaccion)
                {
                    if (((Label)d.FindControl("lblNovedad")).Text.Trim() == nt.Codnovedad.ToString() & ((Label)d.FindControl("lblSeccion")).Text.Trim() == nt.Codseccion.ToString() &
                        ((Label)d.FindControl("lblLote")).Text.Trim() == nt.Codlote.ToString() & ((Label)d.FindControl("lblRegistro")).Text.Trim() == nt.Registro.ToString())
                    {
                        ((GridView)d.FindControl("gvLotes")).DataSource = nt.Terceros;
                        ((GridView)d.FindControl("gvLotes")).DataBind();

                        ((GridView)d.FindControl("gvItems")).DataSource = nt.ListaItems;
                        ((GridView)d.FindControl("gvItems")).DataBind();
                    }
                }
            }
            LiquidaTransaccioin(0, 0);
        }

        protected void imbLiquidar_Click(object sender, EventArgs e)
        {
            LiquidaTransaccioin(0, 0);
            ddlLote.Enabled = true;
            ddlFinca.Enabled = true;
            ddlNovedad.Enabled = true;
            ddlFinca.Enabled = true;
            ddlSeccion.Enabled = true;
            ConfiguracionNovedad(ddlNovedad.SelectedValue.ToString().Trim());
        }


        private void validarFechas()
        {
            if (Convert.ToDateTime(txtFechaD.Text) > Convert.ToDateTime(txtFecha.Text))
            {
                CerroresGeneral.ManejoError(this, GetType(), "La fecha de la labor no puede ser mayor a la transacción", "warning");
                txtFechaD.Text = "";
                txtFechaD.Focus();
                return;
            }
        }


        protected void imbBusqueda_Click(object sender, EventArgs e)
        {

            if (gvParametros.Rows.Count == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Seleccione un filtro para realizar la busqueda", "warning");
                return;
            }
            BusquedaTransaccion();

        }
        protected void nitxtValor1_TextChanged(object sender, EventArgs e)
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
        protected void imbConsulta_Click(object sender, EventArgs e)
        {
            manejoCancelar();
            manejoConsulta();
        }

        private void manejoConsulta()
        {
            CcontrolesUsuario.LimpiarControles(upConsulta.Controls);
            CcontrolesUsuario.HabilitarControles(upConsulta.Controls);
            upGeneral.Visible = true;
            this.upDetalle.Visible = false;
            this.upEncabezado.Visible = false;
            this.upConsulta.Visible = true;
            this.niimbRegistro.BorderStyle = BorderStyle.None;
            this.imbConsulta.BorderStyle = BorderStyle.Solid;
            this.imbConsulta.BorderColor = System.Drawing.Color.Silver;
            this.imbConsulta.BorderWidth = Unit.Pixel(1);
            imbBusqueda.Visible = false;
            nitxtValor2.Visible = false;
            this.niimbRegistro.Enabled = true;
            lblTipoDocumento.Visible = false;
            ddlTipoDocumento.Visible = false;
            txtNumero.Visible = false;
            lblNumero.Visible = false;
            lbCancelar.Visible = false;
            lbRegistrar.Visible = false;
            listaTransaccion = null;
            this.gvSubTotales.DataSource = null;
            this.gvSubTotales.DataBind();
            this.lbRegistrar.Enabled = true;
            this.nilbNuevo.Visible = false;
            this.niimbImprimir.Visible = false;
            this.imbConsulta.Enabled = false;
            gvParametros.DataSource = null;
            gvParametros.DataBind();
            CargaCampos();
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
                if (this.gvParametros.Rows.Count == 0)
                    this.imbBusqueda.Visible = false;
                EstadoInicialGrillaTransacciones();
            }
            catch
            {

            }
        }
        protected void gvTransaccion_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            bool anulado = false;
            foreach (Control objControl in gvTransaccion.Rows[e.RowIndex].Cells[7].Controls)
            {
                anulado = ((CheckBox)objControl).Checked;
            }

            if (anulado == true)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Registro anulado no es posible su edición", "warning");
                return;
            }

            if (transacciones.validaEjecutarTransaccion(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text.Trim(), this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text.Trim(), Convert.ToInt16(Session["empresa"])) == 1)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Registro ejecutado no es posible su edición", "warning");
                return;
            }

            try
            {


                DateTime fecha = Convert.ToDateTime(this.gvTransaccion.Rows[e.RowIndex].Cells[4].Text);
                CargarTipoTransaccion();
                HabilitaEncabezado();
                listaTransaccion = null;
                txtNumero.Enabled = false;
                ddlTipoDocumento.Enabled = false;
                lbRegistrar.Visible = false;
                upConsulta.Visible = false;
                CcontrolesUsuario.LimpiarControles(upConsulta.Controls);
                ddlTipoDocumento.SelectedValue = this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text.Trim();
                cargarCombox();
                ComportamientoTransaccion();
                txtFecha.Text = fecha.ToString();
                txtNumero.Text = this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text;
                txtObservacion.Enabled = true;
                referencia();
                cargarEncabezado();
                cargarDetalle();
                CalcularSubtotal();
                manejoFinca();
                manejoItem();
                txtRemision.Focus();
                lbRegistrar.Visible = true;
                imbLiquidar.Visible = true;
                CcontrolesUsuario.InhabilitarUsoControles(upEncabezado.Controls);
                txtObservacion.Enabled = true;
                this.hdTransaccionConfig = CcontrolesUsuario.TipoTransaccionConfig(this.ddlTipoDocumento.SelectedValue, Convert.ToInt16(Session["empresa"]));
                selTerceroCosecha.Visible = false;
                string[] split = hdTransaccionConfig.Split('*');
                selTerceroCosecha.Visible = Convert.ToBoolean(split[28]);
                this.Session["editar"] = true;

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los datos. Correspondiente a: " + ex.Message, "C");
            }
        }
        private string hdTransaccionConfig;

        protected void niddlOperador_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.niddlOperador.SelectedValue.ToString() == "between")
                this.nitxtValor2.Visible = true;
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
                if (Convert.ToString(this.niddlCampo.SelectedValue) == registro.Cells[1].Text && Convert.ToString(this.niddlOperador.SelectedValue) == Server.HtmlDecode(registro.Cells[2].Text) && this.nitxtValor1.Text == registro.Cells[3].Text)
                    return;
            }
            operadores = new Coperadores(Convert.ToString(this.niddlCampo.SelectedValue), Server.HtmlDecode(Convert.ToString(this.niddlOperador.SelectedValue)), this.nitxtValor1.Text, this.nitxtValor2.Text);
            List<Coperadores> listaOperadores = null;
            if (this.Session["operadores"] == null)
            {
                listaOperadores = new List<Coperadores>();
                listaOperadores.Add(operadores);
            }
            else
            {
                listaOperadores = (List<Coperadores>)Session["operadores"];
                listaOperadores.Add(operadores);
            }

            this.Session["operadores"] = listaOperadores;
            this.imbBusqueda.Visible = true;
            this.gvParametros.DataSource = listaOperadores;
            this.gvParametros.DataBind();
            this.gvTransaccion.DataSource = null;
            this.gvTransaccion.DataBind();
            this.nilblRegistros.Text = "Nro. Registros 0";
            EstadoInicialGrillaTransacciones();
            imbBusqueda.Focus();
        }
        protected void niimbRegistro_Click(object sender, EventArgs e)
        {
            manejoCancelar();
            TabRegistro();
        }
        private void TabRegistro()
        {
            this.Session["editar"] = null;
            CcontrolesUsuario.LimpiarControles(upConsulta.Controls);
            gvTransaccion.DataSource = null;
            gvTransaccion.DataBind();
            listaNovedadesTransaccion = null;
            listaTransaccion = null;
            this.gvTransaccion.DataSource = null;
            this.gvTransaccion.DataBind();
            this.imbConsulta.BorderStyle = BorderStyle.None;
            this.niimbRegistro.BorderStyle = BorderStyle.Solid;
            this.niimbRegistro.BorderColor = System.Drawing.Color.Silver;
            this.niimbRegistro.BorderWidth = Unit.Pixel(1);
            this.niimbRegistro.Enabled = false;
            this.imbBusqueda.Enabled = true;
            this.niimbRegistro.Enabled = false;
            this.gvTransaccion.DataSource = null;
            this.gvTransaccion.DataBind();
            this.nilblRegistros.Text = "Nro. Registros 0";
            this.niimbImprimir.Visible = false;
            this.nilbNuevo.Visible = true;
            this.imbConsulta.Visible = true;
            this.upGeneral.Visible = true;
            this.upConsulta.Visible = false;
            this.upDetalle.Visible = false;
            this.upEncabezado.Visible = false;
            this.niimbRegistro.Enabled = false;
            this.imbConsulta.Enabled = true;
        }

        protected void gvTransaccion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    this.Session["numerotransaccion"] = this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text;
                    if (transacciones.VerificaEdicionBorrado(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text, Convert.ToInt16(this.Session["empresa"])) != 0)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "Transacción ejecutada / anulada no es posible su edición", "warning");
                        return;
                    }

                    if (tipoTransaccion.RetornaTipoBorrado(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, Convert.ToInt16(this.Session["empresa"])) == "E")
                    {
                        switch (transacciones.EliminarTransaccionLabores(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text, Convert.ToInt16(this.Session["empresa"])))
                        {
                            case 0:
                                CerroresGeneral.ManejoError(this, GetType(), "Registro Anulado satisfactoriamente", "warning");
                                BusquedaTransaccion();
                                ts.Complete();
                                break;
                            case 1:
                                CerroresGeneral.ManejoError(this, GetType(), "Error al eliminar registros ", "warning");
                                break;
                        }
                    }
                    else
                    {
                        switch (transacciones.AnulaTransaccion(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text, this.Session["usuario"].ToString().Trim(), Convert.ToInt16(this.Session["empresa"])))
                        {
                            case 0:
                                CerroresGeneral.ManejoError(this, GetType(), "Registro Anulado satisfactoriamente", "warning");
                                BusquedaTransaccion();
                                ts.Complete();
                                break;
                            case 1:
                                CerroresGeneral.ManejoError(this, GetType(), "Error al anular la transacción. Operación no realizada", "warning");
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
        protected void chkCalcular_CheckedChanged(object sender, EventArgs e)
        {
            CalcularSubtotal();
        }
        protected void chkSeleccion_CheckedChanged(object sender, EventArgs e)
        {
            LiquidaTransaccioin(0, 0);
        }
        protected void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataListItem dli in dlDetalle.Items)
            {
                if (((CheckBox)dli.FindControl("chkSeleccionar")).Checked == true)
                {
                    cargarSesiones();
                    cargarLotes();
                    string novedad = ((Label)dli.FindControl("lblNovedad")).Text;
                    string seccion = ((Label)dli.FindControl("lblSeccion")).Text;
                    string lote = ((Label)dli.FindControl("lblLote")).Text;
                    string fecha = Convert.ToDateTime(((Label)dli.FindControl("lblFechaD")).Text).ToShortDateString();
                    ddlNovedad.SelectedValue = novedad.Trim();
                    ConfiguracionNovedad(ddlNovedad.SelectedValue.ToString().Trim());
                    ddlSeccion.SelectedValue = seccion.Trim();
                    ddlLote.SelectedValue = lote;
                    txtFechaD.Text = fecha;
                    ddlLote.Enabled = false;
                    ddlFinca.Enabled = false;
                    ddlNovedad.Enabled = false;
                    ddlFinca.Enabled = false;
                    ddlSeccion.Enabled = false;
                    lblFecha.Enabled = false;
                    txvCantidadD.ReadOnly = false;
                    txvJornalesD.ReadOnly = false;
                    ManejoControlDetalle(false);
                    LiquidaTransaccioin(0, 0);
                }

            }
        }
        protected void lbCancelarD_Click(object sender, EventArgs e)
        {
            ConfiguracionNovedad(ddlNovedad.SelectedValue.ToString().Trim());
            cargarComboxDetalle();
        }
        protected void ddlFinca_SelectedIndexChanged2(object sender, EventArgs e)
        {
            manejoFinca();
        }
        private void manejoFinca()
        {
            if (txtFecha.Text.Trim().Length == 0 & txtFecha.Visible)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar una fecha para continuar", "warning");
                ddlFinca.SelectedValue = "";
                return;
            }
            if (txtFechaF.Text.Trim().Length == 0 & txtFechaF.Visible)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar una fecha final para continuar", "warning");
                ddlFinca.SelectedValue = "";
                return;
            }
            cargarComboxDetalle();
            imbCargar.Visible = true;
            ddlFinca.Focus();
        }
        protected void dlDetalle_ItemCommand(object source, DataListCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    int indexDL = e.Item.ItemIndex;
                    DataListItem dli = dlDetalle.Items[e.Item.ItemIndex];
                    GridView gvTerceros = (GridView)dli.FindControl("gvLotes");
                    int registro = Convert.ToInt16(((Label)dli.FindControl("lblRegistro")).Text);
                    List<Ctercero> tercerosNovedad = listaNovedadesTransaccion[registro].Terceros;
                    listaNovedadesTransaccion[registro].Jornal = Convert.ToDecimal(((TextBox)dli.FindControl("txvJornalesD")).Text);
                    listaNovedadesTransaccion[registro].Cantidad = Convert.ToDecimal(((TextBox)dli.FindControl("txvCantidadG")).Text);
                    string lote = ((Label)dli.FindControl("lblLote")).Text;
                    DropDownList ddlTercero = ((DropDownList)dli.FindControl("ddlTerceroGrilla"));
                    string novedad = ((Label)dli.FindControl("lblNovedad")).Text;
                    DateTime fecha = Convert.ToDateTime(((Label)dli.FindControl("lblFechaD")).Text);

                    foreach (GridViewRow gv in gvTerceros.Rows)
                    {
                        if (gv.Cells[1].Text == ddlTercero.SelectedValue)
                        {
                            CerroresGeneral.ManejoError(this, GetType(), "El trabajador seleccionado ya se encuentra en la grilla, por favor corrija", "warning");

                            return;
                        }

                    }

                    decimal precioLabor = Convert.ToDecimal(listaPrecios.SeleccionaPrecioNovedadAñoTercero(Convert.ToInt16(Session["empresa"]), novedad, fecha.Year, Convert.ToInt16(ddlTercero.SelectedValue), fecha));

                    Ctercero ter = new Ctercero(Convert.ToInt16(ddlTercero.SelectedValue), ddlTercero.SelectedItem.Text, lote, null, 0, 0, precioLabor);
                    tercerosNovedad.Add(ter);
                    listaNovedadesTransaccion[registro].Terceros = tercerosNovedad;
                    dlDetalle.DataSource = listaNovedadesTransaccion;
                    dlDetalle.DataBind();

                    foreach (DataListItem d in dlDetalle.Items)
                    {
                        foreach (CtransaccionFertilizante nt in listaNovedadesTransaccion)
                        {
                            if (((Label)d.FindControl("lblNovedad")).Text.Trim() == nt.Codnovedad.ToString() & ((Label)d.FindControl("lblSeccion")).Text.Trim() == nt.Codseccion.ToString() &
                                ((Label)d.FindControl("lblLote")).Text.Trim() == nt.Codlote.ToString() & ((Label)d.FindControl("lblRegistro")).Text.Trim() == nt.Registro.ToString())
                            {
                                ((GridView)d.FindControl("gvLotes")).DataSource = nt.Terceros;
                                ((GridView)d.FindControl("gvLotes")).DataBind();

                                ((GridView)d.FindControl("gvItems")).DataSource = nt.ListaItems;
                                ((GridView)d.FindControl("gvItems")).DataBind();
                            }
                        }
                    }
                    LiquidaTransaccioin(0, 0);
                }

                if (e.CommandName == "Update")
                {
                    DataListItem dli = dlDetalle.Items[e.Item.ItemIndex];

                    GridView gvTerceros = (GridView)dli.FindControl("gvLotes");
                    int registro = Convert.ToInt16(((Label)dli.FindControl("lblRegistro")).Text);
                    List<Ctercero> tercerosNovedad = listaNovedadesTransaccion[registro].Terceros;
                    listaNovedadesTransaccion[registro].Jornal = Convert.ToDecimal(((TextBox)dli.FindControl("txvJornalesD")).Text);
                    listaNovedadesTransaccion[registro].Cantidad = Convert.ToDecimal(((TextBox)dli.FindControl("txvCantidadG")).Text);

                    foreach (GridViewRow gv in gvTerceros.Rows)
                    {
                        if (((CheckBox)gv.FindControl("chkSeleccion")).Checked)
                            tercerosNovedad.RemoveAt(gv.RowIndex);
                    }

                    listaNovedadesTransaccion[registro].Terceros = tercerosNovedad;
                    dlDetalle.DataSource = listaNovedadesTransaccion;
                    dlDetalle.DataBind();

                    foreach (DataListItem d in dlDetalle.Items)
                    {
                        foreach (CtransaccionFertilizante nt in listaNovedadesTransaccion)
                        {
                            if (((Label)d.FindControl("lblNovedad")).Text.Trim() == nt.Codnovedad.ToString() & ((Label)d.FindControl("lblSeccion")).Text.Trim() == nt.Codseccion.ToString() &
                                ((Label)d.FindControl("lblLote")).Text.Trim() == nt.Codlote.ToString() & ((Label)d.FindControl("lblRegistro")).Text.Trim() == nt.Registro.ToString())
                            {
                                ((GridView)d.FindControl("gvLotes")).DataSource = nt.Terceros;
                                ((GridView)d.FindControl("gvLotes")).DataBind();

                                ((GridView)d.FindControl("gvItems")).DataSource = nt.ListaItems;
                                ((GridView)d.FindControl("gvItems")).DataBind();
                            }
                        }
                    }
                    LiquidaTransaccioin(0, 0);
                }
                if (e.CommandName == "AddItem")
                {

                    DataListItem dli = dlDetalle.Items[e.Item.ItemIndex];
                    int registroR = Convert.ToInt32(((Label)dli.FindControl("lblRegistro")).Text);
                    listaTransaccionFertilizante = listaNovedadesTransaccion[registroR].ListaItems;

                    if (((GridView)dli.FindControl("gvItems")).Rows.Count == 1)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "Solo se puede ingresar 1 item", "warning");

                        return;
                    }



                    foreach (GridViewRow gvr in ((GridView)dli.FindControl("gvItems")).Rows)
                    {
                        if (gvr.Cells[1].Text.Trim() == ((DropDownList)dli.FindControl("ddlItemDetalle")).SelectedValue.Trim())
                        {
                            CerroresGeneral.ManejoError(this, GetType(), "El item ya fue ingresado no puede ser ingresado 2 veces", "warning");

                            return;
                        }
                    }

                    if (((TextBox)dli.FindControl("txvCantidadD")).Visible)
                    {
                        if (((TextBox)dli.FindControl("txvCantidadD")).Text.Trim().Length > 0)
                        {
                            if (((TextBox)dli.FindControl("txvCantidadD")).Text == "0")
                            {
                                CerroresGeneral.ManejoError(this, GetType(), "Ingrese una cantidad mayor a 0", "warning");

                                return;
                            }
                        }
                        else
                            CerroresGeneral.ManejoError(this, GetType(), "Ingrese una cantidad valida", "warning");
                    }
                    if (!((TextBox)dli.FindControl("txtNoBultos")).Visible)
                    {
                        ((TextBox)dli.FindControl("txtNoBultos")).Text = "0";
                        ((TextBox)dli.FindControl("txtPesoBulto")).Text = "0";
                    }

                    if (trafer.verificaSaldoItem(ddlReferencia.SelectedValue.Trim(), ((DropDownList)dli.FindControl("ddlItemDetalle")).SelectedValue.Trim(), ((Label)dli.FindControl("lblLote")).Text,
                        Convert.ToInt32(this.Session["empresa"])) == 1)
                    {

                        trafer = new CtransaccionFertilizante(((DropDownList)dli.FindControl("ddlItemDetalle")).SelectedValue.Trim(),
                            ((DropDownList)dli.FindControl("ddlItemDetalle")).SelectedItem.Text.Trim(),
                            ((DropDownList)dli.FindControl("ddlUmedidaItem")).SelectedValue.Trim(),
                            Convert.ToDecimal(((TextBox)dli.FindControl("txvCantidadD")).Text),
                            Convert.ToDecimal(((TextBox)dli.FindControl("txtNoBultos")).Text),
                            Convert.ToDecimal(((TextBox)dli.FindControl("txtPesoBulto")).Text),
                            0,
                            ((GridView)dli.FindControl("gvItems")).Rows.Count + 1,
                            registroR);


                        if (listaTransaccionFertilizante == null)
                        {
                            listaTransaccionFertilizante = new List<CtransaccionFertilizante>();
                        }
                        listaTransaccionFertilizante.Add(trafer);


                        ((GridView)dli.FindControl("gvItems")).DataSource = listaTransaccionFertilizante;
                        ((GridView)dli.FindControl("gvItems")).DataBind();

                        listaNovedadesTransaccion[registroR].ListaItems = listaTransaccionFertilizante;

                    }
                    else
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "No hay saldos de este item para continuar con la transacción", "warning");
                        return;
                    }


                }

                if (e.CommandName == "DelItem")
                {
                    DataListItem dli = dlDetalle.Items[e.Item.ItemIndex];
                    GridView gvItemsDl = (GridView)dli.FindControl("gvItems");
                    int registroR = Convert.ToInt32(((Label)dli.FindControl("lblRegistro")).Text);
                    int y = -1;
                    foreach (GridViewRow gvc in gvItemsDl.Rows)
                    {
                        CheckBox chkSeleccion = (CheckBox)gvc.FindControl("chkSeleccion");

                        if (chkSeleccion.Checked)
                        {
                            for (int x = 0; x < listaTransaccionFertilizante.Count; x++)
                            {
                                if (registroR == listaTransaccionFertilizante[x].Registro)
                                {
                                    y = x;
                                    break;
                                }
                            }

                        }

                        if (y >= 0)
                        {
                            break;
                        }
                    }

                    if (y >= 0)
                    {
                        listaTransaccionFertilizante.RemoveAt(y);
                    }


                    List<CtransaccionFertilizante> listaTransaccionGV = listaTransaccionFertilizante.Where(i => i.Registro == registroR).ToList();

                    if (listaTransaccionGV.Count == 0)
                    {
                        listaTransaccionGV = null;
                    }

                    ((GridView)dli.FindControl("gvItems")).DataSource = listaTransaccionGV;
                    ((GridView)dli.FindControl("gvItems")).DataBind();

                    listaNovedadesTransaccion[registroR].ListaItems = listaTransaccionGV;
                }

                subtotalitems();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar items debido a:    " + ex.ToString(), "C");
            }
        }

        private void subtotalitems()
        {
            string primerlote = "";

            List<CsubtotalItems> listaSubtotalitems = new List<CsubtotalItems>();
            CsubtotalItems subitems = null;
            decimal cantidadPal = 0;

            foreach (DataListItem dli in dlDetalle.Items)
            {
                primerlote = ((Label)dli.FindControl("lblLote")).Text;

                GridView gvItemsd = (GridView)dli.FindControl("gvItems");
                cantidadPal = Convert.ToDecimal(((TextBox)dli.FindControl("txvCantidadG")).Text);

                foreach (GridViewRow gvr in gvItemsd.Rows)
                {


                    for (int x = 0; x < listaSubtotalitems.Count; x++)
                    {
                        if (primerlote == listaSubtotalitems[x].Lote)
                        {
                            listaSubtotalitems[x].Cantidad = listaSubtotalitems[x].Cantidad + Convert.ToDecimal(gvr.Cells[4].Text) * cantidadPal;
                        }
                        else
                        {
                            subitems = new CsubtotalItems(primerlote, Convert.ToDecimal(gvr.Cells[4].Text) * cantidadPal, gvr.Cells[2].Text);
                            listaSubtotalitems.Add(subitems);
                        }
                    }

                    if (listaSubtotalitems.Count == 0)
                    {
                        subitems = new CsubtotalItems(primerlote, Convert.ToDecimal(gvr.Cells[4].Text.Replace(".", ",")) * cantidadPal, gvr.Cells[2].Text);
                        listaSubtotalitems.Add(subitems);
                    }
                }
            }

            gvSubItem.DataSource = listaSubtotalitems;
            gvSubItem.DataBind();
        }
        protected void txtFecha_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFecha.Text);
            }
            catch
            {

                CerroresGeneral.ManejoError(this, GetType(), "formato de fecha no valido..", "warning");
                txtFecha.Text = "";
                txtFecha.Focus();
                return;
            }

            ddlFinca.Enabled = true;
            ddlFinca.Focus();


        }
        protected void txtFechaD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFechaD.Text);
            }
            catch
            {
                CerroresGeneral.ManejoError(this, GetType(), "Formato de fecha no valido por favor correjir", "warning");
                txtFechaD.Text = "";
                txtFechaD.Focus();
                return;

            }
        }
        protected void txtIdNovedad_TextChanged(object sender, EventArgs e)
        {
            cargarNovedades();

            try
            {
                ddlNovedad.SelectedValue = txtIdNovedad.Text;
                ddlUmedida.Enabled = false;
                manejoNovedad();
            }
            catch
            {
                DataView dvNovedad = tipoTransaccion.SeleccionaNovedadTipoDocumentos(ddlTipoDocumento.SelectedValue, Convert.ToInt16(Session["empresa"]));
                dvNovedad.RowFilter = "claseLabor=1 and (codigo like '%" + txtIdNovedad.Text + "%' or descripcion like '%" + txtIdNovedad.Text + "%') and empresa=" + Session["empresa"].ToString();

                if (dvNovedad.Table.Rows.Count > 0)
                {
                    cargarNovedades();
                }
                else
                {
                    dvNovedad = tipoTransaccion.SeleccionaNovedadTipoDocumentos(ddlTipoDocumento.SelectedValue, Convert.ToInt16(Session["empresa"]));
                    dvNovedad.RowFilter = "claseLabor=1 and empresa =" + Session["empresa"].ToString();
                    this.ddlNovedad.DataSource = dvNovedad;
                    this.ddlNovedad.DataValueField = "codigo";
                    this.ddlNovedad.DataTextField = "descripcion";
                    this.ddlNovedad.DataBind();
                    this.ddlNovedad.Items.Insert(0, new ListItem("Seleecione una opción", ""));
                    ddlUmedida.Enabled = false;

                }
                txtIdNovedad.Focus();

            }
        }
        protected void gvTransaccion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTransaccion.PageIndex = e.NewPageIndex;
            BusquedaTransaccion();
            gvTransaccion.DataBind();
        }

        protected void ddlFinca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtFecha.Text.Trim().Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar una fecha para continuar", "warning");
                return;
            }
        }
        protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                umedidaItem();
                txvPesoBulto.Text = "50";
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar la unidad de medida del item debido a: " + ex.Message, "C");
            }
        }

        private void umedidaItem()
        {
            string umedida = trafer.retornaUmedidaCatalogo(ddlItem.SelectedValue.Trim(), Convert.ToInt32(this.Session["empresa"]));
            if (umedida.Length > 0)
            {
                ddlumedidaItem.SelectedValue = umedida.Trim();
            }
        }
        protected void ddlumedidaItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                umedidaBulto();
            }
            catch (Exception ex)
            {
                ManejoError("Error al verificar unidad de medida debido a: " + ex.Message, "C");
            }
        }

        private void umedidaBulto()
        {
            if (trafer.verificaUmedidaFertilizacion(ddlumedidaItem.SelectedValue.Trim(), Convert.ToInt16(this.Session["empresa"])) == 1)
            {
                lblNoBultos.Visible = true;
                lblPesoBulto.Visible = true;
                txvPesoBulto.Visible = true;
                txvNoBultos.Visible = true;
                lblNoBultos.Enabled = true;
                txvPesoBulto.Enabled = true;
                txvNoBultos.Enabled = true;
                txvPesoBulto.ReadOnly = false;
                txvNoBultos.ReadOnly = false;
                txvPesoBulto.Text = "50";
                txvNoBultos.Text = "0";
                txvCantidadD.Enabled = false;
            }
            else
            {
                lblNoBultos.Visible = false;
                lblPesoBulto.Visible = false;
                txvPesoBulto.Visible = false;
                txvNoBultos.Visible = false;
                txvPesoBulto.Enabled = false;
                txvNoBultos.Enabled = false;
                txvPesoBulto.ReadOnly = true;
                txvNoBultos.ReadOnly = true;
                txvPesoBulto.Text = "0";
                txvNoBultos.Text = "0";
                txvCantidadD.Enabled = true;
            }
        }



        protected void txtNoBultos_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txvPesoBulto.Text = (Convert.ToDecimal(txvNoBultos.Text) * Convert.ToDecimal(txvPesoBulto.Text)).ToString();
            }
            catch (Exception)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Solo números", "warning");
                txvNoBultos.Text = "0";
                txvPesoBulto.Text = "0";
                return;
            }
        }
        protected void txtPesoBulto_TextChanged(object sender, EventArgs e)
        {
            manejoBulto();
        }

        private void manejoBulto()
        {
            try
            {
                txvNoBultos.Text = ((Convert.ToDecimal(txvNoPalma.Text) * Convert.ToDecimal(txvCantidadD.Text)) / Convert.ToDecimal(txvPesoBulto.Text)).ToString();
                txvPesoBulto.Focus();
            }
            catch (Exception)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Solo números", "warning");
                txvNoBultos.Text = "0";
                txvPesoBulto.Text = "0";
                return;
            }
        }


        protected void txtFechaF_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFecha.Text);
            }
            catch
            {

                CerroresGeneral.ManejoError(this, GetType(), "formato de fecha no valido..", "warning");
                txtFecha.Text = "";
                txtFecha.Focus();
                return;
            }
        }

        protected void gvItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridView gvIt = ((GridView)(sender));

                DataListItem dlitem = (DataListItem)((GridView)(sender)).Parent;

                listaTransaccionFertilizante = new List<CtransaccionFertilizante>();



                for (int x = 0; x < listaLotes.Count; x++)
                {
                    listaTransaccionFertilizante = listaLotes[x].Items;
                    for (int y = 0; y < listaTransaccionFertilizante.Count; y++)
                    {
                        if (((Label)dlitem.FindControl("lblCodLote")).Text == listaLotes[x].Lote)
                        {
                            if (Convert.ToInt16(gvIt.Rows[e.RowIndex].Cells[8].Text) == listaTransaccionFertilizante[y].RegistroItem)
                            {
                                listaTransaccionFertilizante.RemoveAt(y);

                            }
                        }
                    }

                    listaLotes[x].Items = listaTransaccionFertilizante;
                    if (listaTransaccionFertilizante.Count == 0)
                    {
                        listaLotes.RemoveAt(x);
                        x = 0;
                    }
                }

                for (int x = 0; x < listaLotes.Count; x++)
                {
                    for (int y = 0; y < listaLotes[x].Items.Count; y++)
                    {
                        listaLotes[x].Items[y].RegistroItem = y + 1;
                    }
                }


                dlLotes.DataSource = listaLotes;
                dlLotes.DataBind();


                foreach (DataListItem gvr in dlLotes.Items)
                {
                    for (int x = 0; x < listaLotes.Count; x++)
                    {
                        if (((Label)gvr.FindControl("lblCodLote")).Text.Trim() == listaLotes[x].Lote)
                        {
                            ((GridView)(gvr.FindControl("gvItems"))).DataSource = listaLotes[x].Items;
                            ((GridView)(gvr.FindControl("gvItems"))).DataBind();
                            break;
                        }
                    }
                }


                CerroresGeneral.ManejoError(this, GetType(), "Item borrado satisfactoriamenteo", "warning");
            }
            catch (Exception ex)
            {
                ManejoError("Error al editar el item debido a: " + ex.Message, "C");
            }
        }





        protected void dlDetalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (sender is DropDownList)
            //{
            //    DropDownList item = (DropDownList)(sender);
            //    if (item.ID == "ddlItemsDetalle")
            //        nilblInformacion.Text = item.SelectedValue;
            //}

        }
        protected void ddlumedidaItem_SelectedIndexChanged1(object sender, EventArgs e)
        {
            manejoumedida(sender);
        }

        private void manejoumedida(object sender)
        {
            DataListItem item = (DataListItem)((DropDownList)sender).Parent;

            if (trafer.verificaUmedidaFertilizacion(((DropDownList)sender).SelectedValue.Trim(), Convert.ToInt16(this.Session["empresa"])) == 1)
            {
                ((Label)item.FindControl("lblNoBultos")).Visible = true;
                ((Label)item.FindControl("lblPesoKgBulto")).Visible = true;
                ((TextBox)item.FindControl("txtPesoBulto")).Visible = true;
                ((TextBox)item.FindControl("txtNoBultos")).Visible = true;
                ((TextBox)item.FindControl("txtNoBultos")).Enabled = true;
                ((TextBox)item.FindControl("txtNoBultos")).ReadOnly = true;
                ((TextBox)item.FindControl("txtPesoBulto")).Visible = true;
                ((TextBox)item.FindControl("txtPesoBulto")).Enabled = true;
                ((TextBox)item.FindControl("txtPesoBulto")).ReadOnly = true;
                ((TextBox)item.FindControl("txtPesoBulto")).Text = "50";
                ((TextBox)item.FindControl("txtNoBultos")).Text = "0";
                ((TextBox)item.FindControl("txvCantidadD")).Enabled = false;
            }
            else
            {
                ((Label)item.FindControl("lblNoBultos")).Visible = false;
                ((Label)item.FindControl("lblPesoKgBulto")).Visible = false;
                ((TextBox)item.FindControl("txtPesoBulto")).Visible = false;
                ((TextBox)item.FindControl("txtNoBultos")).Visible = false;
                ((TextBox)item.FindControl("txtNoBultos")).Enabled = false;
                ((TextBox)item.FindControl("txtNoBultos")).ReadOnly = false;
                ((TextBox)item.FindControl("txtPesoBulto")).Visible = false;
                ((TextBox)item.FindControl("txtPesoBulto")).Enabled = false;
                ((TextBox)item.FindControl("txtPesoBulto")).ReadOnly = false;
                ((TextBox)item.FindControl("txtPesoBulto")).Text = "0";
                ((TextBox)item.FindControl("txtNoBultos")).Text = "0";
                ((TextBox)item.FindControl("txvCantidadD")).Enabled = true;
            }
        }
        protected void ddlItemDetalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataListItem item = (DataListItem)((DropDownList)sender).Parent;
            string umedida = trafer.retornaUmedidaCatalogo(((DropDownList)sender).SelectedValue.Trim(), Convert.ToInt32(this.Session["empresa"]));

            if (trafer.verificaUmedidaBulto(((Label)item.FindControl("lblUmedida")).Text, Convert.ToInt16(this.Session["empresa"])) == 1)
            {
                ((DropDownList)item.FindControl("ddlUmedidaItem")).SelectedValue = ((Label)item.FindControl("lblUmedida")).Text;
                ((DropDownList)item.FindControl("ddlUmedidaItem")).Enabled = false;
                ((Label)item.FindControl("lblNoBultos")).Visible = true;
                ((Label)item.FindControl("lblPesoKgBulto")).Visible = true;
                ((TextBox)item.FindControl("txtPesoBulto")).Visible = true;
                ((TextBox)item.FindControl("txtNoBultos")).Visible = true;
                ((TextBox)item.FindControl("txtNoBultos")).Enabled = true;
                ((TextBox)item.FindControl("txtNoBultos")).ReadOnly = false;
                ((TextBox)item.FindControl("txtPesoBulto")).Visible = true;
                ((TextBox)item.FindControl("txtPesoBulto")).Enabled = true;
                ((TextBox)item.FindControl("txtPesoBulto")).ReadOnly = false;
                ((TextBox)item.FindControl("txtPesoBulto")).Text = "50";
                ((TextBox)item.FindControl("txtNoBultos")).Text = "0";
                ((TextBox)item.FindControl("txvCantidadD")).Enabled = false;
                ((TextBox)item.FindControl("txvCantidadD")).Visible = false;
                ((Label)item.FindControl("lblCantidadD")).Visible = false;

            }

            else
            {
                if (umedida.Length > 0)
                {
                    ((DropDownList)item.FindControl("ddlUmedidaItem")).SelectedValue = umedida.Trim();
                    ((DropDownList)item.FindControl("ddlUmedidaItem")).Enabled = false;
                    ((Label)item.FindControl("lblNoBultos")).Visible = false;
                    ((Label)item.FindControl("lblPesoKgBulto")).Visible = false;
                    ((TextBox)item.FindControl("txtPesoBulto")).Visible = false;
                    ((TextBox)item.FindControl("txtNoBultos")).Visible = false;
                    ((TextBox)item.FindControl("txtNoBultos")).Enabled = false;
                    ((TextBox)item.FindControl("txtNoBultos")).ReadOnly = false;
                    ((TextBox)item.FindControl("txtPesoBulto")).Visible = false;
                    ((TextBox)item.FindControl("txtPesoBulto")).Enabled = false;
                    ((TextBox)item.FindControl("txtPesoBulto")).ReadOnly = false;
                    ((TextBox)item.FindControl("txtPesoBulto")).Text = "0";
                    ((TextBox)item.FindControl("txtNoBultos")).Text = "0";
                    ((TextBox)item.FindControl("txvCantidadD")).Visible = true;
                    ((Label)item.FindControl("lblCantidadD")).Visible = true;
                }
            }
        }
        protected void txvCantidadD_TextChanged(object sender, EventArgs e)
        {
            if (txvPesoBulto.Text.Length > 0 & txvPesoBulto.Text != "0")
            {
                manejoBulto();
            }
        }


        #endregion Eventos
    }
}