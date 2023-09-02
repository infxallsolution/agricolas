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
    public partial class TransaccionesLabores : BasePage
    {

        #region Instancias

        CentidadMetodos CentidadMetodos = new CentidadMetodos();
        Security seguridad = new Security();
        List<Ctercero> listaTerceros = new List<Ctercero>();
        Ccuadrillas cuadrillas = new Ccuadrillas();
        Ctercero terceros;
        cNovedadTransaccion novedadTransaccion = new cNovedadTransaccion();
        List<cNovedadTransaccion> listaNovedadesTransaccion = new List<cNovedadTransaccion>();
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

        #endregion Instancias

        #region Metodos


        private void cargarNovedades()
        {
            try
            {
                DataView dvNovedad = tipoTransaccion.SeleccionaNovedadTipoDocumentos(ddlTipoDocumento.SelectedValue, Convert.ToInt32(Session["empresa"]));
                dvNovedad.RowFilter = "empresa =" + Session["empresa"].ToString();
                this.ddlNovedad.DataSource = dvNovedad;
                this.ddlNovedad.DataValueField = "codigo";
                this.ddlNovedad.DataTextField = "descripcion";
                this.ddlNovedad.DataBind();
                this.ddlNovedad.Items.Insert(0, new ListItem("", ""));
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
            ddlLote.Enabled = true;
            ddlNovedad.Enabled = true;
        }
        private void cargarEncabezado()
        {
            upEncabezado.Visible = true;
            DataView dvEncabezado = transacciones.RetornaEncabezadoTransaccionLabores(ddlTipoDocumento.SelectedValue, txtNumero.Text, Convert.ToInt16(this.Session["empresa"]));
            foreach (DataRowView registro in dvEncabezado)
            {
                txtFecha.Text = Convert.ToDateTime(registro[5]).ToShortDateString();
                // ddlFinca.SelectedValue = registro[7].ToString().Trim();
                txtObservacion.Text = registro.Row.ItemArray.GetValue(9).ToString();
                txtRemision.Text = registro.Row.ItemArray.GetValue(8).ToString();
            }
            txtFecha.Enabled = false;

        }
        private void cargarDetalle()
        {
            List<cNovedadTransaccion> listaNT = new List<cNovedadTransaccion>();
            cNovedadTransaccion novedadTran;
            List<Ctercero> listaTer = null;
            Ctercero ter;
            upEncabezado.Visible = true;
            DataView dvNovedad = transacciones.RetornaEncabezadoTransaccionLaboresDetalle(ddlTipoDocumento.SelectedValue, txtNumero.Text, Convert.ToInt16(this.Session["empresa"]));
            DataView dvTerceros = transacciones.RetornaEncabezadoTransaccionLaboresTercero(ddlTipoDocumento.SelectedValue, txtNumero.Text, Convert.ToInt16(this.Session["empresa"]));
            int x = 0;
            foreach (DataRowView registro in dvNovedad)
            {
                string novedad = "", desnovedad = "", lote = "", deslote = "", secion = "", desseccion = "", finca = "", nombreFinca = "", racimos = "", uMedida = "", fecha = "";
                decimal cantidad = 0, jornal = 0, pesoRacimo = 0, precioLabor = 0, sacos = 0;
                int registroNovedad = 0, registroNT = 0;
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
                    fecha = Convert.ToDateTime(registro.Row.ItemArray.GetValue(11)).ToShortDateString();

                if (!(registro.Row.ItemArray.GetValue(13) is DBNull))
                    jornal = Convert.ToDecimal(registro.Row.ItemArray.GetValue(13).ToString());

                if (!(registro.Row.ItemArray.GetValue(14) is DBNull))
                    registroNovedad = Convert.ToInt32(registro.Row.ItemArray.GetValue(14).ToString());

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
                        registroNT = Convert.ToInt32(registrotercero.Row.ItemArray.GetValue(7));

                    if (!(registrotercero.Row.ItemArray.GetValue(9) is DBNull))
                        precioLaborTercero = Convert.ToDecimal(registrotercero.Row.ItemArray.GetValue(9));

                    ter = new Ctercero(Convert.ToInt32(registrotercero.Row.ItemArray.GetValue(1)), registrotercero.Row.ItemArray.GetValue(2).ToString(), tlote, tcuadrilla, tcantidad, tjornal, precioLaborTercero);

                    if (!(registrotercero.Row.ItemArray.GetValue(10) is DBNull))
                        finca = Convert.ToString(registrotercero.Row.ItemArray.GetValue(10).ToString());

                    if (!(registrotercero.Row.ItemArray.GetValue(11) is DBNull))
                        nombreFinca = Convert.ToString(registrotercero.Row.ItemArray.GetValue(11).ToString());

                    if (!(registrotercero.Row.ItemArray.GetValue(12) is DBNull))
                        sacos = Convert.ToDecimal(registrotercero.Row.ItemArray.GetValue(12).ToString());

                    if (novedad == registrotercero.Row.ItemArray.GetValue(0).ToString().Trim() && lote == tlote & registroNovedad == registroNT)
                        listaTer.Add(ter);
                }

                novedadTran = new cNovedadTransaccion(finca, nombreFinca, novedad, desnovedad, lote, deslote, secion, desseccion, Convert.ToDecimal(racimos), Convert.ToDecimal(cantidad),
             listaTer, x, uMedida, Convert.ToDecimal(pesoRacimo), fecha, Convert.ToDecimal(jornal), precioLabor, sacos);

                listaNT.Add(novedadTran);
                x++;
            }
            dlDetalle.DataSource = listaNT;
            dlDetalle.DataBind();
            dlDetalle.Visible = true;

            foreach (DataListItem d in dlDetalle.Items)
            {
                foreach (cNovedadTransaccion nt in listaNT)
                {
                    if (((Label)d.FindControl("lblNovedad")).Text.Trim() == nt.Codnovedad.ToString() & ((Label)d.FindControl("lblSeccion")).Text.Trim() == nt.Codseccion.ToString() &
                        ((Label)d.FindControl("lblLote")).Text.Trim() == nt.Codlote.ToString() & ((Label)d.FindControl("lblRegistro")).Text.Trim() == nt.Registro.ToString())
                    {
                        ((GridView)d.FindControl("gvLotes")).DataSource = nt.Terceros;
                        ((GridView)d.FindControl("gvLotes")).DataBind();
                    }
                }
            }

            foreach (DataListItem d in dlDetalle.Items)
            {
                ((DropDownList)d.FindControl("ddlTerceroGrilla")).DataSource = transacciones.SelccionaTercernoNovedad(Convert.ToInt16(this.Session["empresa"]));
                ((DropDownList)d.FindControl("ddlTerceroGrilla")).DataValueField = "id";
                ((DropDownList)d.FindControl("ddlTerceroGrilla")).DataTextField = "cadena";
                ((DropDownList)d.FindControl("ddlTerceroGrilla")).DataBind();
            }


            this.Session["novedadLoteSesion"] = listaNT;
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
                    this.gvTransaccion.DataSource = transacciones.GetTransaccionCompletaLabores(where, Convert.ToInt32(Session["empresa"]));
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
            bool verifica = false;
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                    {
                        numerotransaccion = txtNumero.Text;
                        this.Session["numerotransaccion"] = numerotransaccion;
                        operacion = "actualiza";

                        object[] objValoDeleteNovedad = new object[]{Convert.ToInt16(this.Session["empresa"]),
                                numerotransaccion,ddlTipoDocumento.SelectedValue};

                        switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionNovedad", "elimina", "ppa", objValoDeleteNovedad))
                        {
                            case 0:
                                switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionTercero", "elimina", "ppa", objValoDeleteNovedad))
                                {
                                    case 1:
                                        ManejoError("Error al eliminar la novedad registrada", "E");
                                        verifica = true;
                                        break;
                                }
                                break;
                            case 1:
                                ManejoError("Error al eliminar la novedad registrada", "E");
                                verifica = true;
                                break;
                        }
                    }
                    else
                    {
                        numerotransaccion = transacciones.RetornaNumeroTransaccion(ddlTipoDocumento.SelectedValue, Convert.ToInt16(this.Session["empresa"]));
                        this.Session["numerotransaccion"] = numerotransaccion;
                    }

                    DateTime fecha = Convert.ToDateTime(txtFecha.Text), fechaF = Convert.ToDateTime(txtFecha.Text);
                    string referencia = null, remision = null;
                    if (txtRemision.Enabled == true)
                        remision = txtRemision.Text;
                    object[] objValo = new object[]{
                                                     false, // @anulado	bit
                                                     Convert.ToUInt32(Convert.ToDateTime(txtFecha.Text).Year), //@año	int
                                                     Convert.ToInt32(this.Session["empresa"]),   //@empresa	int
                                                     fecha,   //@fecha	date
                                                     DateTime.Now,  //@fechaAnulado	datetime
                                                     DateTime.Now,  //@fechaFinal	date
                                                     DateTime.Now,   //@fechaRegistro	datetime
                                                     Convert.ToUInt32(fecha.Month),  //@mes	int
                                                     numerotransaccion,   //@numero	varchar
                                                     txtObservacion.Text,   //@observacion	varchar
                                                     referencia,   //@referencia	varchar
                                                     remision,   //@remision	varchar
                                                     ddlTipoDocumento.SelectedValue.Trim(),   //@tipo	varchar
                                                     null,   //@usuarioAnulado	varchar
                                                     this.Session["usuario"].ToString()   //@usuarioRegistro	varchar
                                          };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccion", operacion, "ppa", objValo))
                    {
                        case 0:
                            foreach (DataListItem dl in dlDetalle.Items)
                            {
                                DateTime fechaDetalle = new DateTime();
                                decimal cantidad = 0, jornales = 0, racimos = 0, pesoRacimo = 0, precioLabor = 0, precioLaborTercero = 0, sacos = 0;
                                string lote = "", novedad = "", seccion = "", umedida = "", finca = "";
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

                                if (((Label)dl.FindControl("lblFinca")) != null)
                                    finca = ((Label)dl.FindControl("lblFinca")).Text;

                                if (((Label)dl.FindControl("lblSeccion")) != null)
                                    seccion = ((Label)dl.FindControl("lblSeccion")).Text;

                                if (((Label)dl.FindControl("lblUmedida")) != null)
                                    umedida = ((Label)dl.FindControl("lblUmedida")).Text;

                                if (((Label)dl.FindControl("lblPrecioLabor")) != null)
                                    precioLabor = Convert.ToDecimal(((Label)dl.FindControl("lblPrecioLabor")).Text);

                                if (lote.Trim().Length == 0)
                                    lote = null;

                                object[] objValoresNovedad = new object[]{
                                                Convert.ToInt32(Convert.ToDateTime(txtFecha.Text).Year),   //@año
                                                cantidad, //@cantidad
                                                false,   //@ejecutado
                                                Convert.ToInt16(this.Session["empresa"]),     //@empresa
                                                fechaDetalle,    //@fecha
                                                finca,//finca
                                                jornales,  //@jornales
                                                lote,    //@lote
                                                Convert.ToInt32( fechaDetalle.Month.ToString()) ,  //@mes
                                                novedad,    //@novedad
                                                numerotransaccion,    //@numero
                                                pesoRacimo, //@pesoRacimo
                                                precioLabor,
                                                racimos,    //@racimos
                                                dl.ItemIndex,   //@registro
                                                0,
                                                sacos,
                                                cantidad,    //@saldo
                                                seccion,   //@seccion
                                                ddlTipoDocumento.SelectedValue.Trim(),    //@tipo
                                                umedida //@uMedida
                                         };

                                switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionNovedad", "inserta", "ppa", objValoresNovedad))
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

                                                object[] objValoresTercero = new object[]{
                                                                    Convert.ToInt32(Convert.ToDateTime(txtFecha.Text).Year),  //@año
                                                                    cantidadT, //@cantidad
                                                                    false, //@ejecutado
                                                                    Convert.ToInt16(this.Session["empresa"]), //@empresa
                                                                    fechaDetalle,
                                                                    finca,
                                                                    jornalT, //@jornales
                                                                    lote,//@lote
                                                                    Convert.ToInt32( fechaDetalle.Month.ToString()),//@mes
                                                                    novedad,//@novedad
                                                                    numerotransaccion, //@numero
                                                                    precioLaborTercero,
                                                                    racimos,
                                                                    gv.RowIndex,//@registro
                                                                    dl.ItemIndex,//@registroNovedad
                                                                    cantidadT,//@saldo
                                                                    seccion,//@seccion
                                                                    gv.Cells[1].Text.Trim(),//@tercero
                                                                    ddlTipoDocumento.SelectedValue.Trim(),//@tipo
                                                                    cuadrilla//@cuadrilla
                                                            };

                                                switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionTercero", "inserta", "ppa", objValoresTercero))
                                                {
                                                    case 1:
                                                        ManejoError("Error al eliminar la novedad registrada", "E");
                                                        verifica = true;
                                                        break;
                                                }
                                            }
                                        }
                                        break;
                                    case 1:
                                        ManejoError("Error al eliminar la novedad registrada", "E");
                                        verifica = true;
                                        break;
                                }
                            }
                            break;
                        case 1:
                            ManejoError("Error al eliminar la novedad registrada", "E");
                            verifica = true;
                            break;
                    }
                    if (verifica == false)
                    {
                        if (Convert.ToBoolean(this.Session["editar"]) == true)
                            ts.Complete();
                        else
                        {
                            transacciones.ActualizaConsecutivo(ddlTipoDocumento.SelectedValue, Convert.ToInt16(this.Session["empresa"]));
                            ts.Complete();
                        }
                        ManejoExito("Datos registrados satisfactoriamente número " + this.Session["numerotransaccion"].ToString(), "I");
                    }
                    else
                        ManejoError("Error al eliminar la novedad registrada", "E");
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
                cadena = tipoTransaccion.TipoTransaccionConfig(ddlTipoDocumento.SelectedValue, Convert.ToInt32(Session["empresa"])).ToString();
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
                cadena = Cnovedad.NovedadConfig(novedad, Convert.ToInt32(Session["empresa"])).ToString();
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

            try
            {
                decimal retorno = Convert.ToDecimal(peso.valorPesoPeriodo(Convert.ToInt32(Session["empresa"]), fecha, lote, ddlFinca.SelectedValue));
                return retorno;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        private DataView ObtenerLote()
        {
            object[] objKey = new object[] { this.ddlLote.SelectedValue.Trim().ToString(), Convert.ToInt32(Session["empresa"]) };
            try
            {
                return CentidadMetodos.EntidadGetKey("aLotes", "ppa", objKey).Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
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
                cadena = lotes.LotesConfig(lote, Convert.ToInt32(Session["empresa"])).ToString();
                retorno = cadena.Split(comodin, indice).GetValue(posicion - 1);
                return retorno;
            }
            catch (Exception ex)
            {
                ManejoError("Error al recuperar posición de configuración del lote. Correspondiente a: " + ex.Message, "C");
                return null;
            }
        }
        private List<cNovedadTransaccion> reasignarRegistros(List<cNovedadTransaccion> listaNovedadesTransaccion)
        {
            int z = 0;
            foreach (cNovedadTransaccion ln in listaNovedadesTransaccion)
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
                this.ddlTipoDocumento.DataSource = transacciones.GetTipoTransaccionModulo(Convert.ToInt32(this.Session["empresa"]));
                this.ddlTipoDocumento.DataValueField = "codigo";
                this.ddlTipoDocumento.DataTextField = "descripcion";
                this.ddlTipoDocumento.DataBind();
                this.ddlTipoDocumento.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
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
            precioLabor = listaPrecios.SeleccionaPrecioNovedadAño(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaD.Text).Year);
            cantidad = Convert.ToDecimal(txvCantidadD.Text);
            if (Convert.ToBoolean(NovedadConfig(12, ddlNovedad.SelectedValue)) & Convert.ToBoolean(TipoTransaccionConfig(25)))
            {
                pesoRacimo = ObtenerPesoPromedio(ddlLote.SelectedValue, Convert.ToDateTime(txtFechaD.Text));
                cantidad = pesoRacimo * 0;
            }

            foreach (DataListItem dl in dlDetalle.Items)
            {
                ((TextBox)dl.FindControl("txvCantidadG")).Enabled = !Convert.ToBoolean(NovedadConfig(18, ddlNovedad.SelectedValue));
                ((TextBox)dl.FindControl("txvJornalesD")).Enabled = !Convert.ToBoolean(NovedadConfig(18, ddlNovedad.SelectedValue));
            }


            if (this.Session["novedadLoteSesion"] == null)//cuando la lista de novedades es nula
            {
                for (int x = 0; x < selTerceroCosecha.Items.Count; x++)
                {
                    if (selTerceroCosecha.Items[x].Selected)
                    {
                        precioLaborTercero = listaPrecios.SeleccionaPrecioNovedadAñoTerceroFincaLote(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaD.Text).Year,
                           Convert.ToInt32(selTerceroCosecha.Items[x].Value), Convert.ToDateTime(txtFechaD.Text), ddlFinca.SelectedValue, ddlLote.SelectedValue, ddlSeccion.SelectedValue);

                        if (precioLaborTercero == 0)
                            precioLaborTercero = listaPrecios.SeleccionaPrecioNovedadAñoTercero(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaD.Text).Year, Convert.ToInt32(selTerceroCosecha.Items[x].Value), Convert.ToDateTime(txtFechaD.Text));

                        if (precioLaborTercero <= 0)
                        {
                            CerroresGeneral.ManejoError(this, GetType(), "Error al cargar el detalle de la transacción debido a: la labor " + ddlNovedad.SelectedItem.ToString() + " no tiene precio para el año "
                                + Convert.ToDateTime(txtFechaD.Text).Year.ToString() + " para el tercero " + selTerceroCosecha.Items[x].Text + ", por favor revisar listado de precio por lote o por año, sino revisar parametros del trabajador, tienes que estar activo, ocacional o como contratista", "warning");
                            return;
                        }
                        terceros = new Ctercero(Convert.ToInt32(selTerceroCosecha.Items[x].Value), selTerceroCosecha.Items[x].Text, ddlLote.SelectedValue.ToString().Trim(), null, 0, 0, precioLaborTercero);
                        listaTerceros.Add(terceros);
                    }
                }
                novedadTransaccion = new cNovedadTransaccion(ddlFinca.SelectedValue, ddlFinca.SelectedItem.Text, novedad, nombreNovedad, ddlLote.SelectedValue.Trim(), ddlLote.SelectedItem.Text, ddlSeccion.SelectedValue.Trim(), ddlSeccion.SelectedItem.Text,
                   0, cantidad, listaTerceros, dlDetalle.Items.Count, uMedidad, pesoRacimo, txtFechaD.Text, Convert.ToDecimal(txvJornalesD.Text), precioLabor, 0);
                listaNovedadesTransaccion.Add(novedadTransaccion);
                this.Session["novedadLoteSesion"] = listaNovedadesTransaccion;
            }
            // cuando la lista de novedades  NO es nula
            else
            {
                listaNovedadesTransaccion = (List<cNovedadTransaccion>)this.Session["novedadLoteSesion"]; // cargo todas las novedades 
                listaTerceros = new List<Ctercero>();
                for (int x = 0; x < selTerceroCosecha.Items.Count; x++)
                {
                    if (selTerceroCosecha.Items[x].Selected)
                    {
                        precioLaborTercero = listaPrecios.SeleccionaPrecioNovedadAñoTerceroFincaLote(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaD.Text).Year,
                            Convert.ToInt32(selTerceroCosecha.Items[x].Value), Convert.ToDateTime(txtFechaD.Text), ddlFinca.SelectedValue, ddlLote.SelectedValue, ddlSeccion.SelectedValue);

                        if (precioLaborTercero == 0)
                            precioLaborTercero = listaPrecios.SeleccionaPrecioNovedadAñoTercero(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaD.Text).Year, Convert.ToInt32(selTerceroCosecha.Items[x].Value), Convert.ToDateTime(txtFechaD.Text));

                        if (precioLaborTercero <= 0)
                        {
                            CerroresGeneral.ManejoError(this, GetType(), "Error al cargar el detalle de la transacción debido a: la labor " + ddlNovedad.SelectedItem.ToString() + " no tiene precio para el año "
                                + Convert.ToDateTime(txtFechaD.Text).Year.ToString() + " para el tercero " + selTerceroCosecha.Items[x].Text + ", por favor revisar listado de precio por lote o por año, sino revisar parametros del trabajador, tienes que estar activo, ocacional o como contratista", "warning");
                            return;
                        }

                        terceros = new Ctercero(Convert.ToInt32(selTerceroCosecha.Items[x].Value), selTerceroCosecha.Items[x].Text, ddlLote.SelectedValue.ToString().Trim(), null, 0, 0, precioLaborTercero);
                        listaTerceros.Add(terceros);
                    }
                }

                novedadTransaccion = new cNovedadTransaccion(ddlFinca.SelectedValue, ddlFinca.SelectedItem.Text, novedad, nombreNovedad, ddlLote.SelectedValue.Trim(), ddlLote.SelectedItem.Text, ddlSeccion.SelectedValue.Trim(), ddlSeccion.SelectedItem.Text,
             0, cantidad, listaTerceros, dlDetalle.Items.Count, uMedidad, pesoRacimo, txtFechaD.Text, Convert.ToDecimal(txvJornalesD.Text), precioLabor, 0);
                listaNovedadesTransaccion.Add(novedadTransaccion);
                this.Session["novedadLoteSesion"] = listaNovedadesTransaccion;
                posicionNovedad++;
            }

            dlDetalle.DataSource = listaNovedadesTransaccion;
            dlDetalle.DataBind();

            foreach (DataListItem d in dlDetalle.Items)
            {
                foreach (cNovedadTransaccion nt in listaNovedadesTransaccion)
                {
                    if (((Label)d.FindControl("lblRegistro")).Text.Trim() == nt.Registro.ToString())
                    {
                        ((GridView)d.FindControl("gvLotes")).DataSource = nt.Terceros;
                        ((GridView)d.FindControl("gvLotes")).DataBind();
                    }
                }
            }
        }

        private void ManejoControlDetalle(bool manejo)
        {
            txtFechaD.Enabled = manejo;
            txvCantidadD.Enabled = manejo;
            txvJornalesD.Enabled = manejo;
            if (txvJornalesD.Enabled == false)
                txvJornalesD.Text = "0";
        }
        protected void cargarLotes()
        {
            try
            {
                this.ddlLote.DataSource = lotes.LotesSeccionFinca(this.ddlSeccion.SelectedValue.ToString().Trim(), Convert.ToInt32(this.Session["empresa"]), ddlFinca.SelectedValue.ToString().Trim());
                this.ddlLote.DataValueField = "codigo";
                this.ddlLote.DataTextField = "descripcion";
                this.ddlLote.DataBind();
                this.ddlLote.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }
        protected void cargarCombox()
        {
            cargarNovedades();

            try
            {
                DataView fincas = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("aFinca", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"]));
                fincas.RowFilter = "interna=1 and activo=1 and empresa=" + (Convert.ToInt16(this.Session["empresa"])).ToString();
                this.ddlFinca.DataSource = fincas;
                this.ddlFinca.DataValueField = "codigo";
                this.ddlFinca.DataTextField = "descripcion";
                this.ddlFinca.DataBind();
                this.ddlFinca.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }

            try
            {
                DataView dvUmedida = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("gUnidadMedida", "ppa"), "descripcion", Convert.ToInt32(Session["empresa"]));
                dvUmedida.RowFilter = "empresa=" + (Convert.ToInt16(this.Session["empresa"])).ToString();
                this.ddlUmedida.DataSource = dvUmedida;
                this.ddlUmedida.DataValueField = "codigo";
                this.ddlUmedida.DataTextField = "descripcion";
                this.ddlUmedida.DataBind();
                this.ddlUmedida.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
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
                ManejoErrorCatch(ex);
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

                this.ddlSeccion.DataSource = seccion.SeleccionaSesionesFinca(Convert.ToInt32(this.Session["empresa"]), ddlFinca.SelectedValue);
                this.ddlSeccion.DataValueField = "codigo";
                this.ddlSeccion.DataTextField = "descripcion";
                this.ddlSeccion.DataBind();
                this.ddlSeccion.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        private void ManejoError(string error, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), error, "warning");
            seguridad.InsertaLog(
                this.Session["usuario"].ToString(),
                operacion,
                ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "er",
                error, ObtenerIP(), Convert.ToInt32(Session["empresa"]));

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
            selTerceroCosecha.Visible = false;
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
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(), "ex",
                mensaje, ObtenerIP(), Convert.ToInt32(Session["empresa"]));
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
                ManejoErrorCatch(ex);
                return 1;
            }
        }
        private void actualizarDatos()
        {

            listaNovedadesTransaccion = (List<cNovedadTransaccion>)this.Session["novedadLoteSesion"];


            foreach (DataListItem dli in dlDetalle.Items)
            {

                for (int x = 0; x < listaNovedadesTransaccion.Count; x++)
                {
                    if (((Label)dli.FindControl("lblNovedad")).Text == listaNovedadesTransaccion[x].Codnovedad & ((Label)dli.FindControl("lblSeccion")).Text == listaNovedadesTransaccion[x].Codseccion
                        & ((Label)dli.FindControl("lblLote")).Text == listaNovedadesTransaccion[x].Codlote & ((Label)dli.FindControl("lblRegistro")).Text == listaNovedadesTransaccion[x].Registro.ToString())
                    {
                        listaNovedadesTransaccion[x].Racimos = 0;
                        listaNovedadesTransaccion[x].Jornal = Convert.ToDecimal(((TextBox)dli.FindControl("txvJornalesD")).Text);
                        listaNovedadesTransaccion[x].Cantidad = Convert.ToDecimal(((TextBox)dli.FindControl("txvCantidadG")).Text);

                        foreach (GridViewRow gvr in ((GridView)dli.FindControl("gvLotes")).Rows)
                        {
                            for (int y = 0; y < listaNovedadesTransaccion[x].Terceros.Count; y++)
                            {
                                if (listaNovedadesTransaccion[x].Terceros[y].Codtercero == Convert.ToInt32(gvr.Cells[1].Text))
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
                CcontrolesUsuario.HabilitarControles(this.upEncabezado.Controls);
                this.niimbImprimir.Visible = false;
                this.nilbNuevo.Visible = false;
                this.txtFecha.Visible = false;
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
        private string ConsecutivoTransaccion()
        {
            string numero = "";

            try
            {
                numero = transacciones.RetornaNumeroTransaccion(Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(this.Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
            return numero;
        }
        private void ComportamientoTransaccion()
        {
            upEncabezado.Visible = true;
            upDetalle.Visible = true;

            CcontrolesUsuario.HabilitarControles(this.upEncabezado.Controls);
            CcontrolesUsuario.LimpiarControles(this.upEncabezado.Controls);
            CcontrolesUsuario.HabilitarControles(this.upDetalle.Controls);
            CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
            //CcontrolesUsuario.ComportamientoCampoEntidad(this.upEncabezado.Controls, "aTransaccion", Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(this.Session["empresa"]));
            //CcontrolesUsuario.ComportamientoCampoEntidad(this.upDetalle.Controls, "aTransaccionNovedad", Convert.ToString(this.ddlTipoDocumento.SelectedValue), Convert.ToInt16(this.Session["empresa"]));
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
                    lbFechaD.Enabled = true;

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
                ManejoErrorCatch(ex);
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
            listaNovedadesTransaccion = (List<cNovedadTransaccion>)this.Session["novedadLoteSesion"];
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
                subRacimos += 0;

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
            listaNovedadesTransaccion = (List<cNovedadTransaccion>)this.Session["novedadLoteSesion"];
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
                        ((TextBox)dl.FindControl("txvJornalesD")).Enabled = !Convert.ToBoolean(NovedadConfig(19, ddlNovedad.SelectedValue));
                    }
                }

                foreach (DataListItem dl in dlDetalle.Items)
                {
                    noRacimos += 0;
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
                // CerroresGeneral.ManejoError(this, GetType(), "Liquidación generada exitosamente", "info");
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
                    CargaCampos();
                    TabRegistro();
                }
            }

        }
        protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                        nombrePaginaActual(), "I", Convert.ToInt32(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }

            this.Session["editar"] = false;
            LimpiarDDL();
            this.Session["novedadLoteSesion"] = null;
            ddlSeccion.DataSource = null;
            ddlSeccion.DataBind();
            ddlLote.DataSource = null;
            ddlLote.DataBind();
            this.Session["numerotransaccion"] = null;
            gvSubTotales.DataSource = null;
            gvSubTotales.DataBind();
            upDetalle.Visible = false;
            dlDetalle.DataSource = null;
            dlDetalle.DataBind();
            niimbImprimir.Visible = false;
            selTerceroCosecha.Visible = false;
            txtRemision.Enabled = true;
            CcontrolesUsuario.InhabilitarControles(this.upEncabezado.Controls);
            CcontrolesUsuario.LimpiarControles(this.upEncabezado.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upDetalle.Controls);
            CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
            cargarCombox();
            this.Session["lote"] = null;
            this.txtNumero.Text = ConsecutivoTransaccion();
            ComportamientoConsecutivo();
            ComportamientoTransaccion();
            ddlSeccion.Enabled = false;
            ddlSeccion.DataSource = null;
            ddlSeccion.DataBind();
            ddlLote.DataSource = null;
            ddlLote.DataBind();
            this.Session["subtotal"] = null;
            this.Session["numerotransaccion"] = null;
            ddlFinca.Enabled = false;
            this.Session["editar"] = null;
            txtFecha.Visible = true;
            txtFecha.Enabled = true;
            ddlFinca.Enabled = true;
            txtFechaD.Enabled = true;
        }
        protected void lbNuevo_Click(object sender, EventArgs e)
        {

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "I", Convert.ToInt32(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }
            CcontrolesUsuario.HabilitarControles(upEncabezado.Controls);
            this.Session["editar"] = false;
            ManejoEncabezado();
            CargarTipoTransaccion();
            LimpiarDDL();
            this.Session["novedadLoteSesion"] = null;
            ddlSeccion.DataSource = null;
            ddlSeccion.DataBind();
            ddlLote.DataSource = null;
            ddlLote.DataBind();
            this.Session["numerotransaccion"] = null;
            txtFecha.Enabled = false;
            txtFechaD.Enabled = false;
            gvSubTotales.DataSource = null;
            gvSubTotales.DataBind();
            niimbImprimir.Visible = false;


        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            InHabilitaEncabezado();
            upEncabezado.Visible = false;
            upDetalle.Visible = false;
            CcontrolesUsuario.LimpiarControles(this.upEncabezado.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upEncabezado.Controls);
            CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upDetalle.Controls);
            niimbImprimir.Visible = false;
            this.Session["transaccion"] = null;
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
            selTerceroCosecha.Visible = false;
            this.Session["editar"] = null;

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
            if (Convert.ToBoolean(NovedadConfig(12, ddlNovedad.SelectedValue)) == true)
            {
                if (ddlLote.SelectedValue.Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar un lote antes de continuar", "warning");
                    return;
                }
            }

            if (txvCantidadD.Enabled)
            {
                if (Convert.ToDecimal(txvCantidadD.Text.Trim()) == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "La cantidad debe ser diferente de cero", "warning");
                    return;
                }
            }
            if (txtFechaD.Enabled)
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

            if (listaPrecios.SeleccionaPrecioNovedadAño(Convert.ToInt32(Session["empresa"]), ddlNovedad.SelectedValue, Convert.ToDateTime(txtFechaD.Text).Year) == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "La labor seleccionada no tiene precio en el año, por favor registrar precio para continuar.", "warning");
                return;
            }

            if (txvJornalesD.Text.Length == 0)
                txvJornalesD.Text = "0";

            try
            {
                cargarDL();
                LiquidaTransaccioin(0, 0);
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }


            ddlLote.Enabled = true;
            ddlFinca.Enabled = true;
            ddlNovedad.Enabled = true;
            ddlFinca.Enabled = true;
            ddlSeccion.Enabled = true;
            lbFecha.Enabled = true;
            ConfiguracionNovedad(ddlNovedad.SelectedValue.ToString().Trim());
            LiquidaTransaccioin(0, 0);

        }

        protected void ddlNovedad_SelectedIndexChanged(object sender, EventArgs e)
        {
            manejoNovedad();
        }

        private void manejoNovedad()
        {
            CcontrolesUsuario.LimpiarControles(upDetalle.Controls);
            CcontrolesUsuario.HabilitarControles(upDetalle.Controls);
            ConfiguracionNovedad(ddlNovedad.SelectedValue.ToString().Trim());
            imbCargar.Enabled = true;
            gvSubTotales.DataSource = null;
            gvSubTotales.DataBind();

            CalcularSubtotal();
            ddlNovedad.Focus();
            //ScriptManager1.SetFocus(ddlNovedad);
        }
        protected void ddlSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarLotes();
            ddlSeccion.Focus();
            //  ScriptManager1.SetFocus(ddlSeccion);
        }

        protected void lbRegistrar_Click(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {

                int verificarCRJ = 0;

                if (dlDetalle.Items.Count <= 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "El nivel de detalle de la transacción debe tener por lo menos un registro", "warning");
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
            LiquidaTransaccioin(0, 0);
            Guardar();
        }
        protected void ddlLote_SelectedIndexChanged(object sender, EventArgs e)
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
            ddlLote.Focus();
        }

        private bool ValidaAñoLote()
        {
            int desde = 0, hasta = 0, añoSiembra = 0, difAño = 0;
            bool valor = false;

            if (Convert.ToBoolean(NovedadConfig(25, ddlNovedad.SelectedValue)) == true)
            {
                desde = Convert.ToInt32(NovedadConfig(26, ddlNovedad.SelectedValue));
                hasta = Convert.ToInt32(NovedadConfig(27, ddlNovedad.SelectedValue));
                añoSiembra = Convert.ToInt32(LoteConfig(5, ddlLote.SelectedValue));
                difAño = Convert.ToInt32(Convert.ToDateTime(txtFecha.Text).Year) - añoSiembra;
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
            int registro = Convert.ToInt32(((Label)dlDetalle.Items[e.Item.ItemIndex].FindControl("lblRegistro")).Text.Trim());
            listaNovedadesTransaccion = (List<cNovedadTransaccion>)this.Session["novedadLoteSesion"]; // cargo todas las novedades 

            foreach (cNovedadTransaccion nt in listaNovedadesTransaccion)
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
                subRacimos += 0;

                foreach (cNovedadTransaccion nt in listaNovedadesTransaccion)
                {
                    if (((Label)d.FindControl("lblNovedad")).Text.Trim() == nt.Codnovedad.ToString() & ((Label)d.FindControl("lblSeccion")).Text.Trim() == nt.Codseccion.ToString() &
                        ((Label)d.FindControl("lblLote")).Text.Trim() == nt.Codlote.ToString() & ((Label)d.FindControl("lblRegistro")).Text.Trim() == nt.Registro.ToString())
                    {
                        ((GridView)d.FindControl("gvLotes")).DataSource = nt.Terceros;
                        ((GridView)d.FindControl("gvLotes")).DataBind();
                    }
                }
            }
            this.Session["novedadLoteSesion"] = listaNovedadesTransaccion;
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
            this.Session["transaccion"] = null;
            Session["operadores"] = null;
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

            if (transacciones.validaEjecutarTransaccion(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text.Trim(), this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text.Trim(), Convert.ToInt32(Session["empresa"])) == 1)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Registro ejecutado no es posible su edición", "warning");
                return;
            }

            try
            {


                DateTime fecha = Convert.ToDateTime(this.gvTransaccion.Rows[e.RowIndex].Cells[4].Text);
                if (periodo.RetornaPeriodoCerradoNomina(Convert.ToInt32(fecha.Year), Convert.ToInt32(fecha.Month), Convert.ToInt16(this.Session["empresa"]), fecha) == 1)
                {
                    ManejoError("Periodo cerrado. No es posible editar transacciones", "A");
                    return;
                }

                CcontrolesUsuario.LimpiarControles(upConsulta.Controls);
                HabilitaEncabezado();

                this.Session["editar"] = true;
                this.Session["transaccion"] = null;
                txtNumero.Enabled = false;
                ddlTipoDocumento.Enabled = false;
                lbRegistrar.Visible = false;
                upConsulta.Visible = false;

                cargarCombox();
                ComportamientoTransaccion();
                txtFecha.Text = fecha.ToString();
                CargarTipoTransaccion();
                ddlTipoDocumento.SelectedValue = this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text.Trim();
                txtNumero.Text = this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text;
                txtObservacion.Enabled = true;
                cargarEncabezado();
                cargarDetalle();
                CalcularSubtotal();
                manejoFinca();
                txtRemision.Focus();
                lbRegistrar.Visible = true;
                imbLiquidar.Visible = true;

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los datos. Correspondiente a: " + ex.Message, "C");
            }
        }
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
            TabRegistro();
        }
        private void TabRegistro()
        {
            this.Session["editar"] = null;
            CcontrolesUsuario.LimpiarControles(upConsulta.Controls);
            gvTransaccion.DataSource = null;
            gvTransaccion.DataBind();
            this.Session["novedadLoteSesion"] = null;
            this.Session["transaccion"] = null;
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
                                CerroresGeneral.ManejoError(this, GetType(), "Registro Anulado satisfactoriamente", "info");
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
                                CerroresGeneral.ManejoError(this, GetType(), "Registro Anulado satisfactoriamente", "info");
                                BusquedaTransaccion();
                                ts.Complete();
                                break;
                            case 1:
                                CerroresGeneral.ManejoError(this, GetType(), "Error al anular la transacción. Operación no realizada", "error");
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
                    lbFecha.Enabled = false;
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
            if (txtFecha.Text.Trim().Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar una fecha para continuar", "warning");
                return;
            }
            cargarComboxDetalle();
            selTerceroCosecha.Visible = true;
            txtRemision.Enabled = true;
            txtRemision.ReadOnly = false;
            ddlFinca.Focus();
        }
        protected void dlDetalle_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                DataListItem dli = dlDetalle.Items[e.Item.ItemIndex];
                listaNovedadesTransaccion = (List<cNovedadTransaccion>)this.Session["novedadLoteSesion"];
                GridView gvTerceros = (GridView)dli.FindControl("gvLotes");
                int registro = Convert.ToInt32(((Label)dli.FindControl("lblRegistro")).Text);
                List<Ctercero> tercerosNovedad = listaNovedadesTransaccion[registro].Terceros;
                listaNovedadesTransaccion[registro].Jornal = Convert.ToDecimal(((TextBox)dli.FindControl("txvJornalesD")).Text);
                listaNovedadesTransaccion[registro].Cantidad = Convert.ToDecimal(((TextBox)dli.FindControl("txvCantidadG")).Text);
                string lote = ((Label)dli.FindControl("lblLote")).Text;
                string seccion = ((Label)dli.FindControl("lblSeccion")).Text;
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

                decimal precioLabor = listaPrecios.SeleccionaPrecioNovedadAñoTerceroFincaLote(Convert.ToInt32(Session["empresa"]), novedad, fecha.Year, Convert.ToInt32(ddlTercero.SelectedValue), fecha, ddlFinca.SelectedValue, lote, seccion);

                if (precioLabor == 0)
                    precioLabor = Convert.ToDecimal(listaPrecios.SeleccionaPrecioNovedadAñoTercero(Convert.ToInt32(Session["empresa"]), novedad, fecha.Year, Convert.ToInt32(ddlTercero.SelectedValue), fecha));

                Ctercero ter = new Ctercero(Convert.ToInt32(ddlTercero.SelectedValue), ddlTercero.SelectedItem.Text, lote, null, 0, 0, precioLabor);
                tercerosNovedad.Add(ter);
                listaNovedadesTransaccion[registro].Terceros = tercerosNovedad;
                dlDetalle.DataSource = listaNovedadesTransaccion;
                dlDetalle.DataBind();

                foreach (DataListItem d in dlDetalle.Items)
                {
                    foreach (cNovedadTransaccion nt in listaNovedadesTransaccion)
                    {
                        if (((Label)d.FindControl("lblNovedad")).Text.Trim() == nt.Codnovedad.ToString() & ((Label)d.FindControl("lblSeccion")).Text.Trim() == nt.Codseccion.ToString() &
                            ((Label)d.FindControl("lblLote")).Text.Trim() == nt.Codlote.ToString() & ((Label)d.FindControl("lblRegistro")).Text.Trim() == nt.Registro.ToString())
                        {
                            ((GridView)d.FindControl("gvLotes")).DataSource = nt.Terceros;
                            ((GridView)d.FindControl("gvLotes")).DataBind();
                        }
                    }
                }
                this.Session["novedadLoteSesion"] = listaNovedadesTransaccion;
                LiquidaTransaccioin(0, 0);
            }

            if (e.CommandName == "Update")
            {
                DataListItem dli = dlDetalle.Items[e.Item.ItemIndex];
                listaNovedadesTransaccion = (List<cNovedadTransaccion>)this.Session["novedadLoteSesion"];

                GridView gvTerceros = (GridView)dli.FindControl("gvLotes");
                int registro = Convert.ToInt32(((Label)dli.FindControl("lblRegistro")).Text);
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
                    foreach (cNovedadTransaccion nt in listaNovedadesTransaccion)
                    {
                        if (((Label)d.FindControl("lblNovedad")).Text.Trim() == nt.Codnovedad.ToString() & ((Label)d.FindControl("lblSeccion")).Text.Trim() == nt.Codseccion.ToString() &
                            ((Label)d.FindControl("lblLote")).Text.Trim() == nt.Codlote.ToString() & ((Label)d.FindControl("lblRegistro")).Text.Trim() == nt.Registro.ToString())
                        {
                            ((GridView)d.FindControl("gvLotes")).DataSource = nt.Terceros;
                            ((GridView)d.FindControl("gvLotes")).DataBind();
                        }
                    }
                }
                this.Session["novedadLoteSesion"] = listaNovedadesTransaccion;
                LiquidaTransaccioin(0, 0);
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

                CerroresGeneral.ManejoError(this, GetType(), "formato de fecha no valido..", "warning");
                txtFecha.Text = "";
                txtFecha.Focus();
                //ScriptManager1.SetFocus(txtFecha);
                return;
            }

            verificaPeriodoCerrado(Convert.ToInt32(Convert.ToDateTime(txtFecha.Text).Year),
                   Convert.ToInt32(Convert.ToDateTime(txtFecha.Text).Month), Convert.ToInt16(this.Session["empresa"]), Convert.ToDateTime(txtFecha.Text));
            ddlFinca.Enabled = true;
            //ScriptManager1.SetFocus(ddlFinca.ClientID);
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

        protected void gvTransaccion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTransaccion.PageIndex = e.NewPageIndex;
            BusquedaTransaccion();
            gvTransaccion.DataBind();
        }
        #endregion Eventos


    }
}