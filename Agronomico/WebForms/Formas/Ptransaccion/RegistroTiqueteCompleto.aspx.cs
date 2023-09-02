using Agronomico.seguridadinfos;
using Agronomico.WebForms.App_Code.Administracion;
using Agronomico.WebForms.App_Code.General;
using Agronomico.WebForms.App_Code.Transaccion;
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

namespace Agronomico.WebForms.Formas.Ptransaccion
{
    public partial class RegistroTiqueteCompleto : BasePage
    {
        #region Instancias
        CentidadMetodos CentidadMetodos = new CentidadMetodos();
        Security seguridad = new Security();
        List<CterceroTiquete> listaTerceros = new List<CterceroTiquete>();
        List<CterceroTiquete> listaTercerosCargue = new List<CterceroTiquete>();
        List<CterceroTiquete> listaTercerosTransporte = new List<CterceroTiquete>();
        Ccuadrillas cuadrillas = new Ccuadrillas();
        CterceroTiquete terceros;
        CnovedadCosecha novedadCosecha = new CnovedadCosecha();
        CnovedadTransporte novedadTransporte = new CnovedadTransporte();
        CnovedadCargue novedadCargue = new CnovedadCargue();
        List<CnovedadCosecha> listaNovedadesCosecha = new List<CnovedadCosecha>();
        List<CnovedadTransporte> listaNovedadesTransporte = new List<CnovedadTransporte>();
        List<CnovedadCargue> listaNovedadesCargue = new List<CnovedadCargue>();
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
        List<Csubtotales> subtotal = new List<Csubtotales>();
        Coperadores operadores = new Coperadores();
        Cperiodos periodo = new Cperiodos();
        CpromedioPeso peso = new CpromedioPeso();
        #endregion Instancias

        #region Metodos
        private void cargarComboxDetalle()
        {
            cargarSesiones();
            cargarLotes();
            ddlLote.Enabled = true;
        }
        private void manejoFinca()
        {
            cargarComboxDetalle();
            selTerceroCosecha.Visible = true;
            ddlFinca.Focus();
        }
        protected void Guardar()
        {
            string operacion = "inserta";
            bool interno = false;
            bool verificaEncabezado = false;
            bool verificaDetalle = false;
            bool verificaBascula = false;
            upBascula.Update();
            upDetalle.Update();
            upRecolector.Update();
            try
            {
                Session["numeroEditar"] = hfNumero.Value;
                using (TransactionScope ts = new TransactionScope())
                {
                    string referencia = null;
                    string remision = null;
                    int registroNovedadFull = 0;
                    string tipo = ConfigurationManager.AppSettings["RegistroBascula"].ToString();
                    DateTime fecha;
                    DateTime fechaF;

                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                    {
                        operacion = "actualiza";
                        numerotransaccion = Session["numeroEditar"].ToString();
                        this.Session["numerotransaccion"] = numerotransaccion;

                        object[] objValoDeleteNovedad = new object[] { Convert.ToInt32(Session["empresa"]), numerotransaccion, tipo };
                        switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionNovedad", "elimina", "ppa", objValoDeleteNovedad))
                        {
                            case 1:
                                ManejoError("Error al eliminar la novedad registrada", "E");
                                break;
                        }

                        object[] objValoDeleteTerceroNovedad = new object[] { Convert.ToInt32(Session["empresa"]), numerotransaccion, tipo };
                        switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionTercero", "elimina", "ppa", objValoDeleteTerceroNovedad))
                        {
                            case 1:
                                ManejoError("Error al eliminar los terceros de la novedad registrada", "E");
                                break;
                        }

                        object[] objValoDeleteTerceroBascula = new object[] { Convert.ToInt32(Session["empresa"]), numerotransaccion, tipo };
                        switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionBascula", "elimina", "ppa", objValoDeleteTerceroBascula))
                        {
                            case 1:
                                ManejoError("Error al eliminar registro de bascula", "E");
                                break;
                        }

                        fecha = Convert.ToDateTime(txtFecha.Text);
                        fechaF = Convert.ToDateTime(txtFecha.Text);
                    }
                    else
                    {
                        operacion = "inserta";
                        fecha = Convert.ToDateTime(txtFechaTiqueteI.Text);
                        fechaF = Convert.ToDateTime(txtFechaTiqueteI.Text);
                        numerotransaccion = transacciones.RetornaNumeroTransaccion(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Convert.ToInt32(Session["empresa"]));
                        this.Session["numerotransaccion"] = numerotransaccion;
                    }

                    if (rblBascula.SelectedValue == "1")
                        interno = true;

                    remision = txtRemision.Text;


                    object[] objValo = new object[]{ false, // @anulado	bit
                                                     Convert.ToUInt32(fecha.Year), //@año	int
                                                     decimal.Round(Convert.ToDecimal(txvPneto.Text)),  //@cantidad	int
                                                     Convert.ToInt32(this.Session["empresa"]),   //@empresa	int
                                                     fecha,   //@fecha	date
                                                     fecha,  //@fechaAnulado	datetime
                                                     fechaF,  //@fechaFinal	date
                                                     DateTime.Now,   //@fechaRegistro	datetime
                                                     ddlFinca.SelectedValue.Trim(),   //@finca	char
                                                     decimal.Round(Convert.ToDecimal(0),0),   //@jornal	int
                                                     Convert.ToUInt32(fecha.Month),  //@mes	int
                                                     numerotransaccion,   //@numero	varchar
                                                     txtObservacion.Text,   //@observacion	varchar
                                                     0,   //@precio	money
                                                     decimal.Round(Convert.ToDecimal(txvRacimosTiquete.Text),0),   //@racimos	int
                                                     referencia,   //@referencia	varchar
                                                     remision,   //@remision	varchar
                                                     ConfigurationManager.AppSettings["RegistroBascula"].ToString(),   //@tipo	varchar
                                                     null,   //@usuarioAnulado	varchar
                                                     this.Session["usuario"].ToString(),   //@usuarioRegistro	varchar
                                                     0   //@valorTotal	money
                              };

                    switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionCompleta", operacion, "ppa", objValo))
                    {
                        case 0:
                            string extractora = ddlExtractoraTiquete.SelectedValue.Trim();
                            if (Convert.ToInt32(rblBascula.SelectedValue) == 1)
                                extractora = ddlExtractoraFiltro.SelectedValue.Trim();

                            object[] objValores = new object[]{
                            null,    //@codigoConductor
                            Convert.ToInt32( Session["empresa"]),    //@empresa
                            extractora ,   //@empresaExtractora
                            Convert.ToDateTime(txtFechaTiqueteI.Text),//@fecha
                            interno,//@interno
                            null,   //@nombreConductor
                            numerotransaccion,   //@numero
                            Convert.ToDecimal(txvPbruto.Text),   //@pesoBruto
                            Convert.ToDecimal(txvPneto.Text),  //@pesoNeto
                            Convert.ToDecimal(txvPtara.Text),   //@pesoTara
                            Convert.ToDecimal(txvRacimosTiquete.Text),   //@racimos
                            txtRemolque.Text,  //@remolque
                            Convert.ToDecimal(txvSacos.Text),   //@sacos
                            Convert.ToInt32( ddlExtractoraTiquete.SelectedValue.Trim()),   //@terceroExtractrora
                            ConfigurationManager.AppSettings["RegistroBascula"].ToString(),   //@tipo
                            txtTiquete.Text,   //@tiquete
                            txtVehiculo.Text   //@vehiculo
                        };

                            switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionBasculaCompleta", "inserta", "ppa", objValores))
                            {
                                case 0:
                                    DateTime fechaDetalle = new DateTime();
                                    decimal cantidad = 0, jornales = 0, racimos = 0, pesoRacimo = 0, precioLabor = 0;
                                    string lote = "", novedad = "", seccion = "", umedida = "";
                                    int registroNovedadCosecha;
                                    GridView gvTerceros = new GridView();
                                    registroNovedadFull = 0;
                                    #region cosecha
                                    foreach (DataListItem dl in dlDetalleCosecha.Items)
                                    {
                                        registroNovedadCosecha = Convert.ToInt32(((Label)dl.FindControl("lblRegistro")).Text);

                                        if (((TextBox)dl.FindControl("txvCantidadG")) != null)
                                            cantidad = Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text);
                                        if (((TextBox)dl.FindControl("txvJornalesD")) != null)
                                            jornales = Convert.ToDecimal(((TextBox)dl.FindControl("txvJornalesD")).Text);
                                        if (((Label)dl.FindControl("lblLote")) != null)
                                            lote = ((Label)dl.FindControl("lblLote")).Text;
                                        if (((Label)dl.FindControl("lblPesoPromedio")) != null)
                                            pesoRacimo = Convert.ToDecimal(((Label)dl.FindControl("lblPesoPromedio")).Text);
                                        if (((TextBox)dl.FindControl("txvRacimoG")) != null)
                                            racimos = decimal.Round(Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text), 0);
                                        if (((Label)dl.FindControl("lblSeccion")) != null)
                                            seccion = ((Label)dl.FindControl("lblSeccion")).Text;
                                        if (((Label)dl.FindControl("lblUmedida")) != null)
                                            umedida = ((Label)dl.FindControl("lblUmedida")).Text;
                                        if (((Label)dl.FindControl("lblNovedadCosecha")) != null)
                                            novedad = ((Label)dl.FindControl("lblNovedadCosecha")).Text;
                                        if (((Label)dl.FindControl("lblPrecioLaborCosecha")) != null)
                                            precioLabor = Convert.ToDecimal(((Label)dl.FindControl("lblPrecioLaborCosecha")).Text);
                                        if (((Label)dl.FindControl("lblFechaDCosecha")) != null)
                                            fechaDetalle = Convert.ToDateTime(((Label)dl.FindControl("lblFechaDCosecha")).Text);
                                        if (lote.Trim().Length == 0)
                                            lote = null;

                                        object[] objValoresCosecha = new object[]{
                                                Convert.ToInt32( fechaDetalle.Year.ToString()),   //@año
                                                cantidad, //@cantidad
                                                false,   //@ejecutado
                                                Convert.ToInt32( Session["empresa"]),     //@empresa
                                                fechaDetalle,    //@fecha
                                                jornales,  //@jornales
                                                lote,    //@lote
                                                Convert.ToInt32( fechaDetalle.Month.ToString()) ,  //@mes
                                                novedad,    //@novedad
                                                numerotransaccion,    //@numero
                                                pesoRacimo, //@pesoRacimo
                                                precioLabor,
                                                racimos,    //@racimos
                                                 registroNovedadCosecha,   //@registro
                                                   registroNovedadFull,  //@registroNovedad
                                                cantidad,    //@saldo
                                                seccion,   //@seccion
                                                ConfigurationManager.AppSettings["RegistroBascula"].ToString(),    //@tipo
                                                umedida //@uMedida
                                            };

                                        switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionNovedadCompleta", "inserta", "ppa", objValoresCosecha))
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
                                                        if (!(gv.Cells[4].Text.Trim().Length == 0) & gv.Cells[4].Text.Trim() != "&nbsp;")
                                                            precioLabor = Convert.ToDecimal(gv.Cells[4].Text.Trim());
                                                        if (cantidadT != 0)
                                                        {
                                                            object[] objValoresTerceroCosecha = new object[]{
                                                                    Convert.ToInt32( fechaDetalle.Year.ToString()),  //@año
                                                                    cantidadT, //@cantidad
                                                                    false, //@ejecutado
                                                                    Convert.ToInt32( Session["empresa"]), //@empresa
                                                                    fechaDetalle,    //@fecha
                                                                ddlFinca.SelectedValue,
                                                                    jornalT, //@jornales
                                                                    lote,//@lote
                                                                    Convert.ToInt32( fechaDetalle.Month.ToString()),//@mes
                                                                    novedad,//@novedad
                                                                    numerotransaccion, //@numero
                                                                    precioLabor,
                                                                    registroNovedadFull,//@registro
                                                                    registroNovedadCosecha, //@registro novedad
                                                                    cantidadT,//@saldo
                                                                    seccion,//@seccion
                                                                    gv.Cells[0].Text.Trim(),//@tercero
                                                                    ConfigurationManager.AppSettings["RegistroBascula"].ToString(),    //@tipo
                                                                    cuadrilla//@cuadrilla
                                                            };
                                                            switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionTerceroCompleta", "inserta", "ppa", objValoresTerceroCosecha))
                                                            {
                                                                case 1:
                                                                    ManejoError("Error al insertar el detalle de la transacción", "I");
                                                                    verificaDetalle = true;
                                                                    break;
                                                            }
                                                        }
                                                    }
                                                }

                                                break;
                                            case 1:
                                                ManejoError("Error al insertar el detalle de la transacción", "I");
                                                verificaBascula = true;
                                                break;
                                        }

                                        registroNovedadFull++;
                                    }
                                    #endregion
                                    #region cargue
                                    DateTime fechaDetalleCargue = new DateTime();
                                    decimal cantidadCargue = 0, jornalesCargue = 0, racimosCargue = 0, pesoRacimoCargue = 0, precioLaborCargue = 0;
                                    string loteCargue = "", novedadCargue = "", seccionCargue = "", umedidaCargue = "";
                                    int registroNovedadCargue;
                                    GridView gvTercerosCargue = new GridView();
                                    foreach (DataListItem dlCargue in dlDetalleCargue.Items)
                                    {
                                        registroNovedadCargue = Convert.ToInt32(((Label)dlCargue.FindControl("lblRegistro")).Text);
                                        if (((TextBox)dlCargue.FindControl("txvCantidadG")) != null)
                                            cantidadCargue = Convert.ToDecimal(((TextBox)dlCargue.FindControl("txvCantidadG")).Text);
                                        if (((TextBox)dlCargue.FindControl("txvJornalesD")) != null)
                                            jornalesCargue = Convert.ToDecimal(((TextBox)dlCargue.FindControl("txvJornalesD")).Text);
                                        if (((Label)dlCargue.FindControl("lblLote")) != null)
                                            loteCargue = ((Label)dlCargue.FindControl("lblLote")).Text;
                                        if (((Label)dlCargue.FindControl("lblPesoPromedio")) != null)
                                            pesoRacimoCargue = Convert.ToDecimal(((Label)dlCargue.FindControl("lblPesoPromedio")).Text);
                                        if (((TextBox)dlCargue.FindControl("txvRacimoG")) != null)
                                            racimosCargue = decimal.Round(Convert.ToDecimal(((TextBox)dlCargue.FindControl("txvRacimoG")).Text), 0);
                                        if (((Label)dlCargue.FindControl("lblSeccion")) != null)
                                            seccionCargue = ((Label)dlCargue.FindControl("lblSeccion")).Text;
                                        if (((Label)dlCargue.FindControl("lblUmedida")) != null)
                                            umedidaCargue = ((Label)dlCargue.FindControl("lblUmedida")).Text;
                                        if (((Label)dlCargue.FindControl("lblNovedad")) != null)
                                            novedadCargue = ((Label)dlCargue.FindControl("lblNovedad")).Text;
                                        if (((Label)dlCargue.FindControl("lblPrecioLabor")) != null)
                                            precioLaborCargue = Convert.ToDecimal(((Label)dlCargue.FindControl("lblPrecioLabor")).Text);
                                        if (((Label)dlCargue.FindControl("lblFechaD")) != null)
                                            fechaDetalleCargue = Convert.ToDateTime(((Label)dlCargue.FindControl("lblFechaD")).Text);
                                        if (lote.Trim().Length == 0)
                                            loteCargue = null;

                                        object[] objValoresCargue = new object[]{
                                                Convert.ToInt32( fechaDetalle.Year.ToString()),   //@año
                                                cantidadCargue, //@cantidad
                                                false,   //@ejecutado
                                                Convert.ToInt32( Session["empresa"]),     //@empresa
                                                fechaDetalleCargue,    //@fecha
                                                jornalesCargue,  //@jornales
                                                loteCargue,    //@lote
                                                Convert.ToInt32( fechaDetalle.Month.ToString()) ,  //@mes
                                                novedadCargue,    //@novedad
                                                numerotransaccion,    //@numero
                                                pesoRacimoCargue, //@pesoRacimo
                                                precioLaborCargue,
                                                racimosCargue,    //@racimos
                                                registroNovedadCargue,   //@registro
                                                registroNovedadFull,  //@registroNovedad
                                                cantidadCargue,    //@saldo
                                                seccionCargue,   //@seccion
                                                ConfigurationManager.AppSettings["RegistroBascula"].ToString(),    //@tipo
                                                umedidaCargue //@uMedida
                                            };

                                        switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionNovedadCompleta", "inserta", "ppa", objValoresCargue))
                                        {
                                            case 0:
                                                decimal cantidadTCargue = 0, jornalTCargue = 0;
                                                string cuadrillaCargue = null;
                                                if (((GridView)dlCargue.FindControl("gvLotes")) != null)
                                                {
                                                    foreach (GridViewRow gv in ((GridView)dlCargue.FindControl("gvLotes")).Rows)
                                                    {
                                                        if (((TextBox)gv.FindControl("txtCantidad")) != null)
                                                            cantidadTCargue = Convert.ToDecimal(((TextBox)gv.FindControl("txtCantidad")).Text);
                                                        if (((TextBox)gv.FindControl("txtJornal")) != null)
                                                            jornalTCargue = Convert.ToDecimal(((TextBox)gv.FindControl("txtJornal")).Text);
                                                        if (!(gv.Cells[4].Text.Trim().Length == 0) & gv.Cells[4].Text.Trim() != "&nbsp;")
                                                            precioLaborCargue = Convert.ToDecimal(gv.Cells[4].Text.Trim());
                                                        if (cantidadCargue != 0)
                                                        {
                                                            object[] objValoresTerceroCosecha = new object[]{
                                                                    Convert.ToInt32( fechaDetalle.Year.ToString()),  //@año
                                                                    cantidadTCargue, //@cantidad
                                                                    false, //@ejecutado
                                                                    Convert.ToInt32( Session["empresa"]), //@empresa
                                                                    fechaDetalle,    //@fecha
                                                                ddlFinca.SelectedValue,
                                                                    jornalTCargue, //@jornales
                                                                    loteCargue,//@lote
                                                                    Convert.ToInt32( fechaDetalle.Month.ToString()),//@mes
                                                                    novedadCargue,//@novedad
                                                                    numerotransaccion, //@numero
                                                                    precioLaborCargue,
                                                                    registroNovedadFull,//@registro
                                                                    registroNovedadCargue, //@registro novedad
                                                                    cantidadTCargue,//@saldo
                                                                    seccionCargue,//@seccion
                                                                    gv.Cells[0].Text.Trim(),//@tercero
                                                                    ConfigurationManager.AppSettings["RegistroBascula"].ToString(),    //@tipo
                                                                    cuadrillaCargue//@cuadrilla
                                                            };
                                                            switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionTerceroCompleta", "inserta", "ppa", objValoresTerceroCosecha))
                                                            {
                                                                case 1:
                                                                    ManejoError("Error al insertar el detalle de la transaccción", "I");
                                                                    verificaDetalle = true;
                                                                    break;
                                                            }
                                                        }
                                                    }
                                                }
                                                break;
                                            case 1:
                                                ManejoError("Error al insertar el detalle de la transaccción", "I");
                                                verificaDetalle = true;
                                                break;
                                        }
                                        registroNovedadFull++;
                                    }
                                    #endregion
                                    #region transporte
                                    DateTime fechaDetalleTransporte = new DateTime();
                                    decimal cantidadTransporte = 0, jornalesTransporte = 0, racimosTransporte = 0, pesoRacimoTransporte = 0, precioLaborTransporte = 0;
                                    string loteTransporte = "", novedadTransporte = "", seccionTransporte = "", umedidaTransporte = "";
                                    GridView gvTercerosTransporte = new GridView();
                                    int registroNovedadTransporte;
                                    foreach (DataListItem dlTransporte in dlDetalleTransporte.Items)
                                    {
                                        registroNovedadTransporte = Convert.ToInt32(((Label)dlTransporte.FindControl("lblRegistro")).Text);
                                        if (((TextBox)dlTransporte.FindControl("txvCantidadG")) != null)
                                            cantidadTransporte = Convert.ToDecimal(((TextBox)dlTransporte.FindControl("txvCantidadG")).Text);
                                        if (((TextBox)dlTransporte.FindControl("txvJornalesD")) != null)
                                            jornalesTransporte = Convert.ToDecimal(((TextBox)dlTransporte.FindControl("txvJornalesD")).Text);
                                        if (((Label)dlTransporte.FindControl("lblLote")) != null)
                                            loteTransporte = ((Label)dlTransporte.FindControl("lblLote")).Text;
                                        if (((Label)dlTransporte.FindControl("lblPesoPromedio")) != null)
                                            pesoRacimoTransporte = Convert.ToDecimal(((Label)dlTransporte.FindControl("lblPesoPromedio")).Text);
                                        if (((TextBox)dlTransporte.FindControl("txvRacimoG")) != null)
                                            racimosTransporte = decimal.Round(Convert.ToDecimal(((TextBox)dlTransporte.FindControl("txvRacimoG")).Text), 0);
                                        if (((Label)dlTransporte.FindControl("lblSeccion")) != null)
                                            seccionTransporte = ((Label)dlTransporte.FindControl("lblSeccion")).Text;
                                        if (((Label)dlTransporte.FindControl("lblUmedida")) != null)
                                            umedidaTransporte = ((Label)dlTransporte.FindControl("lblUmedida")).Text;
                                        if (((Label)dlTransporte.FindControl("lblNovedad")) != null)
                                            novedadTransporte = ((Label)dlTransporte.FindControl("lblNovedad")).Text;
                                        if (((Label)dlTransporte.FindControl("lblPrecioLabor")) != null)
                                            precioLaborTransporte = Convert.ToDecimal(((Label)dlTransporte.FindControl("lblPrecioLabor")).Text);
                                        if (((Label)dlTransporte.FindControl("lblFechaD")) != null)
                                            fechaDetalleTransporte = Convert.ToDateTime(((Label)dlTransporte.FindControl("lblFechaD")).Text);
                                        if (lote.Trim().Length == 0)
                                            loteTransporte = null;

                                        object[] objValoresTransporte = new object[]{
                                                Convert.ToInt32( fechaDetalle.Year.ToString()),   //@año
                                                cantidadTransporte, //@cantidad
                                                false,   //@ejecutado
                                                Convert.ToInt32( Session["empresa"]),     //@empresa
                                                fechaDetalleTransporte,    //@fecha
                                                jornalesTransporte,  //@jornales
                                                loteTransporte,    //@lote
                                                Convert.ToInt32( fechaDetalle.Month.ToString()) ,  //@mes
                                                novedadTransporte,    //@novedad
                                                numerotransaccion,    //@numero
                                                pesoRacimoTransporte, //@pesoRacimo
                                                precioLaborTransporte,
                                                racimosTransporte,    //@racimos
                                                  registroNovedadTransporte ,   //@registro
                                               registroNovedadFull ,  //@registroNovedad
                                                cantidadTransporte,    //@saldo
                                                seccionTransporte,   //@seccion
                                                ConfigurationManager.AppSettings["RegistroBascula"].ToString(),    //@tipo
                                                umedidaTransporte //@uMedida
                                            };

                                        switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionNovedadCompleta", "inserta", "ppa", objValoresTransporte))
                                        {
                                            case 0:
                                                decimal cantidadTTransporte = 0, jornalTTransporte = 0;
                                                string cuadrillaTransporte = null;
                                                if (((GridView)dlTransporte.FindControl("gvLotes")) != null)
                                                {
                                                    foreach (GridViewRow gv in ((GridView)dlTransporte.FindControl("gvLotes")).Rows)
                                                    {
                                                        if (((TextBox)gv.FindControl("txtCantidad")) != null)
                                                            cantidadTTransporte = Convert.ToDecimal(((TextBox)gv.FindControl("txtCantidad")).Text);
                                                        if (((TextBox)gv.FindControl("txtJornal")) != null)
                                                            jornalTTransporte = Convert.ToDecimal(((TextBox)gv.FindControl("txtJornal")).Text);
                                                        if (!(gv.Cells[4].Text.Trim().Length == 0) & gv.Cells[4].Text.Trim() != "&nbsp;")
                                                            precioLaborTransporte = Convert.ToDecimal(gv.Cells[4].Text.Trim());
                                                        if (cantidadTransporte != 0)
                                                        {
                                                            object[] objValoresTerceroCosecha = new object[]{
                                                                    Convert.ToInt32( fechaDetalle.Year.ToString()),  //@año
                                                                    cantidadTTransporte, //@cantidad
                                                                    false, //@ejecutado
                                                                    Convert.ToInt32( Session["empresa"]), //@empresa
                                                                    fechaDetalle,    //@fecha
                                                                ddlFinca.SelectedValue,
                                                                    jornalTTransporte, //@jornales
                                                                    loteTransporte,//@lote
                                                                    Convert.ToInt32( fechaDetalle.Month.ToString()),//@mes
                                                                    novedadTransporte,//@novedad
                                                                    numerotransaccion, //@numero
                                                                    precioLaborTransporte,
                                                                    registroNovedadFull,//@registro
                                                                    registroNovedadTransporte, //@registro novedad
                                                                    cantidadTTransporte,//@saldo
                                                                    seccionTransporte,//@seccion
                                                                    gv.Cells[0].Text.Trim(),//@tercero
                                                                    ConfigurationManager.AppSettings["RegistroBascula"].ToString(),    //@tipo
                                                                    cuadrillaTransporte//@cuadrilla
                                                            };
                                                            switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionTerceroCompleta", "inserta", "ppa", objValoresTerceroCosecha))
                                                            {
                                                                case 1:
                                                                    ManejoError("Error al insertar el detalle de la transaccción", "I");
                                                                    verificaDetalle = true;
                                                                    break;
                                                            }
                                                        }
                                                    }
                                                }
                                                break;
                                            case 1:
                                                ManejoError("Error al insertar el detalle de la transaccción", "I");
                                                verificaBascula = true;
                                                break;
                                        }
                                        registroNovedadFull++;
                                    }
                                    #endregion
                                    break;
                                case 1:
                                    ManejoError("Error al insertar el encabezado de la transaccción", "I");
                                    break;

                            }
                            break;
                        case 1:
                            ManejoError("Error al insertar el detalle de la transaccción", "I");
                            break;
                    }

                    if (verificaEncabezado == false & verificaDetalle == false & verificaBascula == false)
                    {
                        if (!Convert.ToBoolean(this.Session["editar"]))
                        {
                            transacciones.ActualizaConsecutivo(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Convert.ToInt32(Session["empresa"]));
                        }
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
        private void TabRegistro()
        {
            this.Session["editar"] = null;
            tabs.Visible = false;
            CcontrolesUsuario.LimpiarControles(upConsulta.Controls);
            gvTransaccion.DataSource = null;
            gvTransaccion.DataBind();
            this.Session["novedadCosecha"] = null;
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
            this.nilblMensajeEdicion.Text = "";
            this.niimbImprimir.Visible = false;
            this.nilbNuevo.Visible = true;
            this.imbConsulta.Visible = true;
            this.upGeneral.Visible = true;
            this.upConsulta.Visible = false;
            this.upBascula.Visible = false;
            this.upDetalle.Visible = false;
            this.upRecolector.Visible = false;
            this.niimbRegistro.Enabled = false;
            this.imbConsulta.Enabled = true;
        }
        private void cargarEncabezado()
        {
            upRecolector.Visible = true;
            DataView dvEncabezado = transacciones.RetornaEncabezadoTransaccionLabores(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Session["numeroEditar"].ToString(), Convert.ToInt32(Session["empresa"]));
            foreach (DataRowView registro in dvEncabezado)
            {
                ddlFinca.SelectedValue = registro.Row.ItemArray.GetValue(7).ToString().Trim();
                txtObservacion.Text = registro.Row.ItemArray.GetValue(15).ToString();
                txtFecha.Text = Convert.ToDateTime(registro.Row.ItemArray.GetValue(5).ToString()).ToShortDateString();
                txtRemision.Text = registro.Row.ItemArray.GetValue(14).ToString();
            }

            DataView dvCargador = transacciones.RetornaEncabezadoTransaccionLaboresDetalleCargue(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Session["numeroEditar"].ToString(), Convert.ToInt32(Session["empresa"]));
            DataView dvTerceros = transacciones.RetornaEncabezadoTransaccionLaboresTerceroCargue(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Session["numeroEditar"].ToString(), Convert.ToInt32(Session["empresa"]));
            DataView dvTransporte = transacciones.RetornaEncabezadoTransaccionLaboresTerceroTransporte(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Session["numeroEditar"].ToString(), Convert.ToInt32(Session["empresa"]));


            foreach (DataRowView registro in dvTransporte)
            {
                manejoLaborTransporte();
                ddlLaborTransporte.SelectedValue = registro.Row.ItemArray.GetValue(0).ToString();
            }

            foreach (DataRowView registro in dvCargador)
            {
                ddlLaborCargue.SelectedValue = registro[2].ToString().Trim();
                txtFechaCargue.Text = Convert.ToDateTime(registro.Row.ItemArray.GetValue(11).ToString()).ToShortDateString();
                calendarCargadores.SelectedDate = Convert.ToDateTime(registro.Row.ItemArray.GetValue(11).ToString());
            }
            foreach (DataRowView dr in dvTerceros)
            {
                for (int x = 0; x < this.selTerceroCargue.Items.Count; x++)
                {
                    if (dr.Row.ItemArray.GetValue(1).ToString() == selTerceroCargue.Items[x].Value)
                        selTerceroCargue.Items[x].Selected = true;
                }
            }

        }
        private void cargarDetalle()
        {
            List<CnovedadTiquete> listaNT = new List<CnovedadTiquete>();
            CnovedadTiquete novedadTran;
            List<CterceroTiquete> listaTerCosecha = null;
            List<CterceroTiquete> listaTerCargue = null;
            List<CterceroTiquete> listaTerTransporte = null;
            CterceroTiquete terCosecha;
            upRecolector.Visible = true;
            DataView dvNovedad = transacciones.RetornaEncabezadoTransaccionLaboresDetalle(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Session["numeroEditar"].ToString(), Convert.ToInt32(Session["empresa"]));
            DataView dvTerceros = transacciones.RetornaEncabezadoTransaccionLaboresTercero(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Session["numeroEditar"].ToString(), Convert.ToInt32(Session["empresa"]));
            int x = 0;

            string tlote = "", tcuadrilla = "";
            decimal tjornal = 0, tcantidad = 0;

            string novedad = "", desnovedad = "", lote = "", deslote = "", secion = "", desseccion = "", racimos = "", uMedida = "", fecha = "", fechaCargue = "", fechaTransporte = "", novedadCargue = "", nombreNovedadCargue = "", novedadTransporte = "", nombreNovedadTransporte = "";
            decimal cantidad = 0, jornal = 0, pesoRacimo = 0, precioLabor = 0, precioLaborTercero = 0, precioCargue = 0, precioTransporte = 0;
            int registroNovedad = 0, registroNT = 0, registroValida = 0;

            foreach (DataRowView registro in dvNovedad)
            {
                listaTerCosecha = new List<CterceroTiquete>();
                listaTerCargue = new List<CterceroTiquete>();
                listaTerTransporte = new List<CterceroTiquete>();
                if (!(registro.Row.ItemArray.GetValue(2) is DBNull) && Convert.ToDecimal(registro.Row.ItemArray.GetValue(15)) == 2)
                {
                    novedad = registro.Row.ItemArray.GetValue(2).ToString();
                    desnovedad = registro.Row.ItemArray.GetValue(3).ToString();
                }
                if (!(registro.Row.ItemArray.GetValue(2) is DBNull) && Convert.ToDecimal(registro.Row.ItemArray.GetValue(15)) == 3)
                {
                    novedadCargue = registro.Row.ItemArray.GetValue(2).ToString();
                    nombreNovedadCargue = registro.Row.ItemArray.GetValue(3).ToString();
                }
                if (!(registro.Row.ItemArray.GetValue(2) is DBNull) && Convert.ToDecimal(registro.Row.ItemArray.GetValue(15)) == 4)
                {
                    novedadTransporte = registro.Row.ItemArray.GetValue(2).ToString();
                    nombreNovedadTransporte = registro.Row.ItemArray.GetValue(3).ToString();
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
                    registroNovedad = Convert.ToInt32(registro.Row.ItemArray.GetValue(14).ToString());

                if (!(registro.Row.ItemArray.GetValue(16) is DBNull) && Convert.ToDecimal(registro.Row.ItemArray.GetValue(15)) == 2)
                    precioLabor = Convert.ToDecimal(registro.Row.ItemArray.GetValue(16).ToString());
                if (!(registro.Row.ItemArray.GetValue(16) is DBNull) && Convert.ToDecimal(registro.Row.ItemArray.GetValue(15)) == 3)
                    precioCargue = Convert.ToDecimal(registro.Row.ItemArray.GetValue(16).ToString());
                if (!(registro.Row.ItemArray.GetValue(16) is DBNull) && Convert.ToDecimal(registro.Row.ItemArray.GetValue(15)) == 4)
                    precioTransporte = Convert.ToDecimal(registro.Row.ItemArray.GetValue(16).ToString());

                foreach (DataRowView registrotercero in dvTerceros)
                {
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


                    terCosecha = new CterceroTiquete(Convert.ToInt32(registrotercero.Row.ItemArray.GetValue(1)), registrotercero.Row.ItemArray.GetValue(2).ToString(), tlote, tcuadrilla, tcantidad, tjornal, precioLaborTercero, novedad);
                    if (Convert.ToDecimal(registro.Row.ItemArray.GetValue(15)) == 2)
                    {
                        if (novedad == registrotercero.Row.ItemArray.GetValue(0).ToString().Trim() && lote == tlote & registroNovedad == registroNT)
                            listaTerCosecha.Add(terCosecha);
                    }
                    if (Convert.ToDecimal(registro.Row.ItemArray.GetValue(15)) == 3)
                    {
                        if (novedad == registrotercero.Row.ItemArray.GetValue(0).ToString().Trim() && lote == tlote & registroNovedad == registroNT)
                            listaTerTransporte.Add(terCosecha);
                    }
                    if (Convert.ToDecimal(registro.Row.ItemArray.GetValue(15)) == 4)
                    {
                        if (novedad == registrotercero.Row.ItemArray.GetValue(0).ToString().Trim() && lote == tlote & registroNovedad == registroNT)
                            listaTerTransporte.Add(terCosecha);
                    }

                }

                if (registroValida != registroNovedad)
                {
                    novedadTran = new CnovedadTiquete(novedad, desnovedad, lote, deslote, secion, desseccion, Convert.ToDecimal(racimos), Convert.ToDecimal(cantidad),
                        listaTerCosecha, listaTercerosCargue, listaTercerosTransporte, x, uMedida,
                        Convert.ToDecimal(pesoRacimo), fecha, Convert.ToDecimal(jornal), precioLabor, registroNovedad, novedadCargue, nombreNovedadCargue,
                        fechaCargue, precioCargue, novedadTransporte, nombreNovedadTransporte, fechaTransporte, precioTransporte);
                    listaNT.Add(novedadTran);
                    registroValida = registroNovedad;
                }
                x++;
            }
            dlDetalleCosecha.DataSource = listaNT;
            dlDetalleCosecha.DataBind();
            dlDetalleCosecha.Visible = true;

            foreach (DataListItem d in dlDetalleCosecha.Items)
            {
                foreach (CnovedadTiquete nt in listaNT)
                {
                    if (((Label)d.FindControl("lblRegistro")).Text.Trim() == nt.Registro.ToString())
                    {
                        ((GridView)d.FindControl("gvLotes")).DataSource = nt.Terceros;
                        ((GridView)d.FindControl("gvLotes")).DataBind();

                        ((GridView)d.FindControl("gvCargue")).DataSource = nt.TercerosCargue;
                        ((GridView)d.FindControl("gvCargue")).DataBind();

                        ((GridView)d.FindControl("gvTransporte")).DataSource = nt.TerceroTransporte;
                        ((GridView)d.FindControl("gvTransporte")).DataBind();
                    }
                }
            }
            this.Session["novedadCosecha"] = listaNT;
        }
        private void cargarTiqueteDetalle()
        {
            CcontrolesUsuario.HabilitarControles(upTiquete.Controls);
            CcontrolesUsuario.LimpiarControles(upBascula.Controls);
            CcontrolesUsuario.InhabilitarUsoControles(upTiquete.Controls);
            CcontrolesUsuario.LimpiarControles(upTiquete.Controls);

            DataView dvEncabezado = transacciones.RetornaEncabezadoTransaccionTiquete(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Session["numeroEditar"].ToString(), Convert.ToInt32(Session["empresa"]));

            if (dvEncabezado.Count <= 0)
            {
                upBascula.Visible = false;
                upTiquete.Visible = false;
            }
            else
            {
                upBascula.Visible = true;
                upTiquete.Visible = true;
            }

            foreach (DataRowView registro in dvEncabezado)
            {
                ddlExtractoraFiltro.SelectedValue = registro.Row.ItemArray.GetValue(3).ToString().Trim();
                ddlExtractoraTiquete.SelectedValue = registro.Row.ItemArray.GetValue(4).ToString().Trim();
                txtTiquete.Text = registro.Row.ItemArray.GetValue(5).ToString().Trim();
                txvPbruto.Text = registro.Row.ItemArray.GetValue(6).ToString().Trim();
                txvPtara.Text = registro.Row.ItemArray.GetValue(7).ToString().Trim();
                txvPneto.Text = registro.Row.ItemArray.GetValue(8).ToString().Trim();
                txvSacos.Text = registro.Row.ItemArray.GetValue(9).ToString().Trim();
                txvRacimosTiquete.Text = registro.Row.ItemArray.GetValue(10).ToString().Trim();
                txtVehiculo.Text = registro.Row.ItemArray.GetValue(13).ToString().Trim();
                txtRemolque.Text = registro.Row.ItemArray.GetValue(14).ToString().Trim();
                txtFechaTiqueteI.Text = registro.Row.ItemArray.GetValue(15).ToString().Trim();
                string interno = "";
                if (Convert.ToBoolean(registro.Row.ItemArray.GetValue(16).ToString().Trim()))
                    interno = "1";
                else
                    interno = "2";

                rblBascula.SelectedValue = interno;
            }
        }
        protected void cargarExtractoras()
        {
            try
            {
                ddlExtractoraFiltro.DataSource = empresa.SeleccionaEmpresasExtractoras();
                ddlExtractoraFiltro.DataValueField = "id";
                ddlExtractoraFiltro.DataTextField = "razonSocial";
                ddlExtractoraFiltro.DataBind();
                ddlExtractoraFiltro.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar terceros. Correspondiente a: " + ex.Message, "C");
            }
        }
        protected void cargarTiquetes(string filtro)
        {
            if (ddlExtractoraFiltro.SelectedValue.Trim().Length > 0)
            {
                DataView tiquetes = bascula.SeleccionaTiquetesBasculaExtractora(Convert.ToInt32(ddlExtractoraFiltro.SelectedValue.Trim()), Convert.ToInt32(Session["empresa"]), txtFiltroBascula.Text);
                gvTiquetes.DataSource = tiquetes;
                gvTiquetes.DataBind();
                if (!(this.gvTiquetes.Rows.Count > 0))
                    this.nilblInformacion.Text = "El número del tiquete ya fue registrado o el tiquete no se encuentra en la base de datos";
                gvTiquetes.Visible = true;
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
                cadena = tipoTransaccion.TipoTransaccionConfig(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Convert.ToInt32(Session["empresa"])).ToString();
                retorno = cadena.Split(comodin, indice).GetValue(posicion - 1);
                return retorno;
            }
            catch (Exception ex)
            {
                ManejoError("Error al recuperar posición de configuración de tipo de transacción. Correspondiente a: " + ex.Message, "C");
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
            catch (Exception)
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
                nilblInformacionDetalle.Text = "Error al cargar el lote debida a: " + ex;
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
        private List<CnovedadCosecha> reasignarRegistrosCosecha(List<CnovedadCosecha> listaNovedadesTransaccion)
        {
            int z = 0;
            foreach (CnovedadCosecha ln in listaNovedadesTransaccion)
            {
                ln.Registro = z;
                z++;
            }
            return listaNovedadesTransaccion;
        }
        private List<CnovedadTransporte> reasignarRegistrosTransporte(List<CnovedadTransporte> listaNovedadesTransaccion)
        {
            int z = 0;
            foreach (CnovedadTransporte ln in listaNovedadesTransaccion)
            {
                ln.Registro = z;
                z++;
            }
            return listaNovedadesTransaccion;
        }
        private List<CnovedadCargue> reasignarRegistrosCargue(List<CnovedadCargue> listaNovedadesTransaccion)
        {
            int z = 0;
            foreach (CnovedadCargue ln in listaNovedadesTransaccion)
            {
                ln.Registro = z;
                z++;
            }
            return listaNovedadesTransaccion;
        }
        protected void cargarDL()
        {
            int posicionNovedad = 0;
            nilblInformacionDetalle.Text = "";
            decimal pesoRacimo = 0, cantidad = 0, precioLabor = 0;
            string novedad = null, nombreNovedad = null, uMedidad = null;

            if (transacciones.SeleccionaNovedadLoteRangoSiembra(this.ddlLote.SelectedValue.Trim(), Convert.ToInt32(Session["empresa"]), Convert.ToDateTime(txtFechaCosecha.Text)).Table.Rows.Count > 0)
            {
                foreach (DataRowView registro in transacciones.SeleccionaNovedadLoteRangoSiembra(this.ddlLote.SelectedValue.Trim(), Convert.ToInt32(Session["empresa"]), Convert.ToDateTime(txtFechaCosecha.Text)))
                {
                    novedad = registro.Row.ItemArray.GetValue(1).ToString();
                    nombreNovedad = registro.Row.ItemArray.GetValue(2).ToString();
                    uMedidad = registro.Row.ItemArray.GetValue(5).ToString();
                    precioLabor = listaPrecios.SeleccionaPrecioNovedadAño(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaCosecha.Text).Year);

                    if (listaPrecios.SeleccionaPrecioNovedadAño(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaCosecha.Text).Year) == 0)
                    {
                        this.nilblInformacionDetalle.Text = "La labor seleccionada no tiene precio en el año, por favor registrar precio para continuar.";
                        return;
                    }
                }
            }
            else
            {
                nilblInformacionDetalle.Text = "No hay una asociación de una labor de cosecha con el rango de siembra del lote";
                return;
            }

            if (txvRacimosCosecha.Enabled)
            {
                if (Convert.ToBoolean(NovedadConfig(19, novedad)) == true)
                {
                    if (Convert.ToDecimal(txvRacimosCosecha.Text.Trim()) == 0)
                    {
                        nilblInformacionDetalle.Text = "Los racimos deben ser diferente de cero";
                        return;
                    }
                }
            }

            if (this.Session["novedadCosecha"] == null)
            {
                for (int x = 0; x < selTerceroCosecha.Items.Count; x++)
                {
                    if (selTerceroCosecha.Items[x].Selected)
                    {
                        precioLabor = listaPrecios.SeleccionaPrecioNovedadAñoTercero(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaCosecha.Text).Year, Convert.ToInt32(selTerceroCosecha.Items[x].Value), Convert.ToDateTime(txtFechaCosecha.Text));
                        terceros = new CterceroTiquete(Convert.ToInt32(selTerceroCosecha.Items[x].Value), selTerceroCosecha.Items[x].Text, ddlLote.SelectedValue.ToString().Trim(), null, 0, 0, precioLabor, novedad);
                        listaTerceros.Add(terceros);
                    }
                }

                Session["registroNC"] = dlDetalleCosecha.Items.Count;
                novedadCosecha = new CnovedadCosecha(novedad, nombreNovedad, ddlLote.SelectedValue.Trim(), ddlLote.SelectedItem.Text, ddlSeccion.SelectedValue.Trim(), ddlSeccion.SelectedItem.Text,
                     Convert.ToDecimal(txvRacimosCosecha.Text), cantidad, listaTerceros, listaNovedadesCosecha.Count + 1, uMedidad, pesoRacimo, txtFechaCosecha.Text, Convert.ToDecimal(txvJornalesD.Text), precioLabor);
                listaNovedadesCosecha.Add(novedadCosecha);
                this.Session["novedadCosecha"] = listaNovedadesCosecha;
            }
            else
            {
                listaNovedadesCosecha = (List<CnovedadCosecha>)this.Session["novedadCosecha"];

                int registroCosecha = listaNovedadesCosecha.Max(x => x.Registro);

                for (int x = 0; x < selTerceroCosecha.Items.Count; x++)
                {
                    if (selTerceroCosecha.Items[x].Selected)
                    {
                        precioLabor = listaPrecios.SeleccionaPrecioNovedadAñoTercero(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaCosecha.Text).Year, Convert.ToInt32(selTerceroCosecha.Items[x].Value), Convert.ToDateTime(txtFechaCosecha.Text));
                        terceros = new CterceroTiquete(Convert.ToInt32(selTerceroCosecha.Items[x].Value), selTerceroCosecha.Items[x].Text, ddlLote.SelectedValue.ToString().Trim(), null, 0, 0, precioLabor, novedad);
                        listaTerceros.Add(terceros);
                    }
                }

                Session["registroNC"] = dlDetalleCosecha.Items.Count;
                novedadCosecha = new CnovedadCosecha(novedad, nombreNovedad, ddlLote.SelectedValue.Trim(), ddlLote.SelectedItem.Text, ddlSeccion.SelectedValue.Trim(), ddlSeccion.SelectedItem.Text,
                  Convert.ToDecimal(txvRacimosCosecha.Text), cantidad, listaTerceros, registroCosecha + 1, uMedidad, pesoRacimo, txtFechaCosecha.Text, Convert.ToDecimal(txvJornalesD.Text), precioLabor);
                listaNovedadesCosecha.Add(novedadCosecha);
                this.Session["novedadCosecha"] = listaNovedadesCosecha;
                posicionNovedad++;
            }
            dlDetalleCosecha.DataSource = listaNovedadesCosecha;
            dlDetalleCosecha.DataBind();

            foreach (DataListItem d in dlDetalleCosecha.Items)
            {
                foreach (CnovedadCosecha nt in listaNovedadesCosecha)
                {
                    if (((Label)d.FindControl("lblRegistro")).Text.Trim() == nt.Registro.ToString())
                    {
                        ((GridView)d.FindControl("gvLotes")).DataSource = nt.Terceros;
                        ((GridView)d.FindControl("gvLotes")).DataBind();
                    }
                }


            }
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
                ManejoError("Error al cargar lotes. Correspondiente a: " + ex.Message, "C");
            }
        }
        protected void cargarCombox()
        {
            try
            {
                DataView dvLaborCargador = tipoTransaccion.SeleccionaNovedadTipoDocumentos(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Convert.ToInt32(Session["empresa"]));
                dvLaborCargador.RowFilter = "claseLabor=3 and empresa=" + Session["empresa"].ToString();
                this.ddlLaborCargue.DataSource = dvLaborCargador;
                this.ddlLaborCargue.DataValueField = "codigo";
                this.ddlLaborCargue.DataTextField = "descripcion";
                this.ddlLaborCargue.DataBind();
                this.ddlLaborCargue.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar Novedades. Correspondiente a: " + ex.Message, "C");
            }
            try
            {
                if (rblBascula.SelectedValue == "1")
                {
                    DataView dvextractoratiquete = empresa.SeleccionaEmpresasExtractoras();
                    this.ddlExtractoraTiquete.DataSource = dvextractoratiquete;
                    this.ddlExtractoraTiquete.DataValueField = "id";
                    this.ddlExtractoraTiquete.DataTextField = "razonSocial";
                    this.ddlExtractoraTiquete.DataBind();
                    this.ddlExtractoraTiquete.Items.Insert(0, new ListItem("", ""));
                }
                else
                {
                    DataView dvextractoratiquete = empresa.SeleccionaExtractoras(Convert.ToInt32(Session["empresa"]));
                    this.ddlExtractoraTiquete.DataSource = dvextractoratiquete;
                    this.ddlExtractoraTiquete.DataValueField = "id";
                    this.ddlExtractoraTiquete.DataTextField = "razonSocial";
                    this.ddlExtractoraTiquete.DataBind();
                    this.ddlExtractoraTiquete.Items.Insert(0, new ListItem("", ""));
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar extractoras. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                DataView fincas = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("aFinca", "ppa"), "descripcion", Convert.ToInt32(Session["empresa"]));
                fincas.RowFilter = "interna = True and empresa=" + (Session["empresa"]).ToString();
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
                this.selTerceroCosecha.DataSource = transacciones.SelccionaTercernoNovedad(Convert.ToInt32(Session["empresa"]));
                this.selTerceroCosecha.DataValueField = "id";
                this.selTerceroCosecha.DataTextField = "cadena";
                this.selTerceroCosecha.DataBind();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tercero de cosecha. Correspondiente a: " + ex.Message, "C");
            }
            cargarExtractoras();
        }
        private void cargarCosecha()
        {
            try
            {
                this.selTerceroCosecha.DataSource = transacciones.SelccionaTercernoNovedad(Convert.ToInt32(Session["empresa"]));
                this.selTerceroCosecha.DataValueField = "id";
                this.selTerceroCosecha.DataTextField = "cadena";
                this.selTerceroCosecha.DataBind();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tercero de cosecha. Correspondiente a: " + ex.Message, "C");
            }

        }
        protected void cargarSesiones()
        {
            if (ddlFinca.SelectedValue.Trim().Length == 0)
            {
                nilblInformacion.Text = "Debe seleccionar una finca";
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
            this.Session["error"] = error;
            this.Session["paginaAnterior"] = this.Page.Request.FilePath.ToString();

            if (imbConsulta.Enabled == false)
            {
                upConsulta.Visible = true;
                nilbNuevo.Visible = false;
                TabConsulta();
            }
            else
            {
                upConsulta.Visible = false;
                nilbNuevo.Visible = true;
                TabRegistro();
            }
            limpiarSubtotal();
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "er", error, ip.ObtenerIP(), Convert.ToInt32(Session["empresa"]));
            this.Response.Redirect("~/Agronomico/Error.aspx", false);
        }
        private void ManejoExito(string mensaje, string operacion)
        {

            CcontrolesUsuario.InhabilitarControles(this.upBascula.Controls);
            CcontrolesUsuario.LimpiarControles(this.upBascula.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upDetalle.Controls);
            CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upRecolector.Controls);
            CcontrolesUsuario.LimpiarControles(this.upRecolector.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upTransporte.Controls);
            CcontrolesUsuario.LimpiarControles(this.upTransporte.Controls);
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            this.Session["editar"] = null;
            tabs.Visible = false;
            upBascula.Visible = false;
            upDetalle.Visible = false;
            upTransporte.Visible = false;
            upRecolector.Visible = false;
            dlDetalleCosecha.DataSource = null;
            dlDetalleCosecha.DataBind();
            this.nilbNuevo.Visible = true;
            this.nilblInformacion.Visible = true;
            this.nilblInformacion.ForeColor = Color.Green;
            nilbNuevo.Visible = true;
            lbCancelar.Visible = false;
            lbRegistrar.Visible = false;
            niimbImprimir.Visible = true;
            limpiarSubtotal();
            if (imbConsulta.Enabled == false)
            {
                upConsulta.Visible = true;
                nilbNuevo.Visible = false;
                TabConsulta();
            }
            else
            {
                upConsulta.Visible = false;
                nilbNuevo.Visible = true;
                TabRegistro();
            }
            this.nilblInformacion.Text = mensaje;
            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "ex", mensaje, ip.ObtenerIP(), Convert.ToInt32(Session["empresa"]));

        }
        private void ManejoEncabezado()
        {
            HabilitaEncabezado();
        }
        private void HabilitaEncabezado()
        {
            this.nilblInformacion.Text = "";
            this.nilblInformacion.ForeColor = System.Drawing.Color.Red;
            this.lbCancelar.Visible = true;
            this.nilbNuevo.Visible = false;
            this.lbRegistrar.Visible = true;
            this.Session["transaccion"] = null;

        }
        private void InHabilitaEncabezado()
        {
            this.nilblInformacion.Text = "";
            this.lbCancelar.Visible = false;
            this.nilbNuevo.Visible = true;
            this.nilbNuevo.Focus();
            upTiquete.Visible = false;
            upPestana.Visible = false;

        }
        private string ConsecutivoTransaccion()
        {
            string numero = "";

            try
            {
                numero = transacciones.RetornaNumeroTransaccion(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Convert.ToInt32(Session["empresa"]));
            }
            catch (Exception ex)
            {
                ManejoError("Error al obtener el número de transacción. Correspondiente a: " + ex.Message, "C");
            }

            return numero;
        }
        private void ComportamientoTransaccion()
        {
            upRecolector.Visible = true;
            upDetalle.Visible = true;
            CcontrolesUsuario.ComportamientoCampoEntidad(this.upRecolector.Controls, "aTransaccion",
                              ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Convert.ToInt32(Session["empresa"]));
            CcontrolesUsuario.ComportamientoCampoEntidad(this.upDetalle.Controls, "aTransaccionNovedad",
                ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Convert.ToInt32(Session["empresa"]));
        }
        private void ManejoRBLbascula()
        {
            CcontrolesUsuario.HabilitarControles(upBascula.Controls);
            if (Convert.ToInt32(rblBascula.SelectedValue) == 1)
            {
                ddlExtractoraFiltro.Enabled = true;
                ddlExtractoraFiltro.Visible = true;
                lblExtractoraFiltro.Visible = true;
                lblFiltroBusqueda.Visible = true;
                txtFiltroBascula.Visible = true;
                upTiquete.Visible = false;
                imbBuscarBascula.Visible = true;
                CcontrolesUsuario.InhabilitarControles(upTiquete.Controls);
            }
            else
            {
                lbFechaTiqueteI.Enabled = true;
                ddlExtractoraFiltro.Visible = false;
                lblExtractoraFiltro.Visible = false;
                lblFiltroBusqueda.Visible = false;
                txtFiltroBascula.Visible = false;
                gvTiquetes.Visible = false;
                upTiquete.Visible = true;
                imbBuscarBascula.Visible = false;
                CcontrolesUsuario.HabilitarControles(upTiquete.Controls);
                CcontrolesUsuario.LimpiarControles(upTiquete.Controls);
                txvPneto.Enabled = false;
                gvTiquetes.DataSource = null;
                gvTiquetes.DataBind();
                cargarExtractora();
                gvTiquetes.DataSource = null;
                gvTiquetes.DataBind();
                manejoRecolector();
                manejoLaborCargue();
                cargarCombox();

            }
        }
        private void cargarExtractora()
        {
            try
            {
                DataView dvextractoratiquete = empresa.SeleccionaExtractoras(Convert.ToInt32(Session["empresa"]));
                this.ddlExtractoraTiquete.DataSource = dvextractoratiquete;
                this.ddlExtractoraTiquete.DataValueField = "id";
                this.ddlExtractoraTiquete.DataTextField = "razonSocial";
                this.ddlExtractoraTiquete.DataBind();
                this.ddlExtractoraTiquete.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar extractoras. Correspondiente a: " + ex.Message, "C");
            }
        }
        private void LimpiarDDL()
        {

            ddlSeccion.DataSource = null;
            ddlSeccion.DataBind();

            ddlLote.DataSource = null;
            ddlLote.DataBind();
        }
        private void repartirCantidadesCosecha()
        {

            decimal diferencia = 0, cantidadTotal = 0, cantidadTotalAsignada = 0, cantidadTercero = 0,
                cantidadJornales = 0, jornalesTotales = 0, jornalesAsignados = 0, diferenciaJornal, subCantidad = 0, subRacimos = 0, subjornales = 0;
            int noTerceros = 0, contarN = 0, cantidadDL = 0;

            listaNovedadesCosecha = (List<CnovedadCosecha>)this.Session["novedadCosecha"];

            foreach (DataListItem dl in dlDetalleCosecha.Items)
            {
                cantidadDL += Convert.ToInt32(Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text));
            }
            contarN = dlDetalleCosecha.Items.Count;
            diferencia = Convert.ToDecimal(txvPneto.Text) - cantidadDL;

            int ramdom = new Random().Next(1, contarN);
            int w = 0;
            foreach (DataListItem dl in dlDetalleCosecha.Items)
            {
                if (w == ramdom)
                    ((TextBox)dl.FindControl("txvCantidadG")).Text = (Convert.ToInt64(Convert.ToInt64(Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text)) + diferencia)).ToString();
                w++;
            }

            foreach (DataListItem dl in dlDetalleCosecha.Items)
            {
                cantidadTotal = Convert.ToInt32(Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text));
                cantidadTotalAsignada = 0;
                jornalesAsignados = 0;
                jornalesTotales = Convert.ToDecimal(((TextBox)dl.FindControl("txvJornalesD")).Text);
                noTerceros = 0;
                diferencia = 0;
                cantidadTercero = 0;

                subCantidad += cantidadTotal;
                subjornales += jornalesTotales;
                subRacimos += Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text);

                noTerceros = ((GridView)dl.FindControl("gvLotes")).Rows.Count;

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
            actualizarDatosCosecha();
            CalcularSubtotalCosecha();
        }
        private void CalcularSubtotalCosecha()
        {
            listaNovedadesCosecha = (List<CnovedadCosecha>)this.Session["novedadCosecha"];
            gvSubTotales.DataSource = null;
            gvSubTotales.DataBind();
            subtotal = new List<Csubtotales>();
            decimal subCantidad = 0, subRacimos = 0, subjornales = 0;
            Csubtotales sub;
            bool validarNovedad = false;
            int posicionSubtotal = 0;

            try
            {
                if (listaNovedadesCosecha != null)
                {
                    for (int x = 0; x < listaNovedadesCosecha.Count; x++)
                    {
                        subRacimos = 0;
                        subCantidad = 0;
                        subjornales = 0;
                        for (int z = 0; z < subtotal.Count; z++)
                        {
                            if (listaNovedadesCosecha[x].Codnovedad == subtotal[z].novedades)
                            {
                                validarNovedad = true;
                                posicionSubtotal = z;
                                break;
                            }
                        }

                        if (validarNovedad)
                        {
                            subCantidad += listaNovedadesCosecha[x].Cantidad;
                            subjornales += listaNovedadesCosecha[x].Jornal;
                            subRacimos += listaNovedadesCosecha[x].Racimos;
                            subtotal[posicionSubtotal].subCantidad = subtotal[posicionSubtotal].subCantidad + subCantidad;
                            subtotal[posicionSubtotal].subJornal = subtotal[posicionSubtotal].subJornal + subjornales;
                            subtotal[posicionSubtotal].subRacimo = subtotal[posicionSubtotal].subRacimo + subRacimos;
                            validarNovedad = false;
                        }
                        else
                        {
                            subCantidad += listaNovedadesCosecha[x].Cantidad;
                            subjornales += listaNovedadesCosecha[x].Jornal;
                            subRacimos += listaNovedadesCosecha[x].Racimos;
                            sub = new Csubtotales(listaNovedadesCosecha[x].Codnovedad, listaNovedadesCosecha[x].Desnovedad, subCantidad, subRacimos, subjornales);
                            subtotal.Add(sub);
                        }


                    }

                    gvSubTotales.DataSource = subtotal;
                    gvSubTotales.DataBind();
                    this.Session["subtotal"] = subtotal;

                }
            }
            catch (Exception ex)
            {
                nilblInformacionDetalle.Text = "Error al cargar subtotal debido a: " + ex.Message;
            }
        }
        private void LiquidaTransaccioinCosecha(decimal racimos, decimal cant)
        {
            try
            {
                decimal noRacimos = 0;
                decimal cantidad = 0, pPromedio = 0;
                decimal totalKilos = 0, cantidadP = 0;
                List<int> listacantidadesRepartidas = new List<int>();
                List<Decimal> difKilosLote = null;
                difKilosLote = new List<decimal>();
                decimal difTotalKg = 0;

                foreach (DataListItem dl in dlDetalleCosecha.Items)
                {
                    ((TextBox)dl.FindControl("txvCantidadG")).Enabled = false;
                    ((TextBox)dl.FindControl("txvRacimoG")).Enabled = false;
                    ((TextBox)dl.FindControl("txvJornalesD")).Enabled = false;
                }

                foreach (DataListItem dl in dlDetalleCosecha.Items)
                {
                    noRacimos += Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text);
                    cantidad += Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text);
                }

                if ((noRacimos + racimos) > Convert.ToDecimal(txvRacimosTiquete.Text))
                {
                    nilblInformacionDetalle.Text = "El número de racimos no puede ser mayor al tiquete";
                    return;
                }

                if (dlDetalleCosecha.Items.Count > 0)
                {
                    totalKilos = 0;
                    cantidadP = 0;

                    foreach (DataListItem dl in dlDetalleCosecha.Items)
                    {
                        if (((Label)dl.FindControl("lblLote")).Text.Trim().Length > 0)
                        {
                            pPromedio = ObtenerPesoPromedio(((Label)dl.FindControl("lblLote")).Text, Convert.ToDateTime(((Label)dl.FindControl("lblFechaDCosecha")).Text));
                            ((Label)dl.FindControl("lblPesoPromedio")).Text = Convert.ToString(pPromedio);
                            ((TextBox)dl.FindControl("txvCantidadG")).Text = Convert.ToString(Convert.ToInt64(Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text) * pPromedio));
                            cantidadP = Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text);
                            totalKilos += cantidadP;
                            ((Label)dl.FindControl("lblpRacimos")).Text = Convert.ToString(Decimal.Round((Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text) / Convert.ToDecimal(txvRacimosTiquete.Text)), 4));
                        }
                    }

                    if (Convert.ToDecimal(txvPneto.Text.Trim()) > 0)
                    {
                        difTotalKg = Convert.ToDecimal(Convert.ToDecimal(txvPneto.Text) - totalKilos);
                    }

                    foreach (DataListItem dl in dlDetalleCosecha.Items)
                    {
                        if (((Label)dl.FindControl("lblLote")).Text.Trim().Length > 0)
                        {
                            decimal ppLoteNovedad = Convert.ToDecimal(((Label)dl.FindControl("lblpRacimos")).Text);
                            decimal diferencia = Convert.ToDecimal(Decimal.Round((Convert.ToDecimal(difTotalKg) * ppLoteNovedad), 0));
                            ((Label)dl.FindControl("lblDifKilosCosecha")).Text = Convert.ToString(Decimal.Round(Convert.ToDecimal(difTotalKg * ppLoteNovedad), 0));
                            decimal cantidadm = Convert.ToDecimal(Convert.ToInt64(Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text) + Convert.ToDecimal(((Label)dl.FindControl("lblDifKilosCosecha")).Text)));
                            ((TextBox)dl.FindControl("txvCantidadG")).Text = Convert.ToString(Convert.ToInt64(Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text) + Convert.ToDecimal(((Label)dl.FindControl("lblDifKilosCosecha")).Text)));
                        }
                    }
                }

                repartirCantidadesCosecha();
            }
            catch (Exception ex)
            {
                nilblInformacionDetalle.Text = "Error al liquidar transaccion debido a: " + ex;
            }
        }
        private void actualizarDatosCosecha()
        {
            listaNovedadesCosecha = (List<CnovedadCosecha>)this.Session["novedadCosecha"];
            foreach (DataListItem dli in dlDetalleCosecha.Items)
            {
                for (int x = 0; x < listaNovedadesCosecha.Count; x++)
                {
                    if (((Label)dli.FindControl("lblRegistro")).Text == listaNovedadesCosecha[x].Registro.ToString())
                    {
                        listaNovedadesCosecha[x].Racimos = Convert.ToDecimal(((TextBox)dli.FindControl("txvRacimoG")).Text);
                        listaNovedadesCosecha[x].Jornal = Convert.ToDecimal(((TextBox)dli.FindControl("txvJornalesD")).Text);
                        listaNovedadesCosecha[x].Cantidad = Convert.ToDecimal(((TextBox)dli.FindControl("txvCantidadG")).Text);

                        foreach (GridViewRow gvr in ((GridView)dli.FindControl("gvLotes")).Rows)
                        {
                            for (int y = 0; y < listaNovedadesCosecha[x].Terceros.Count; y++)
                            {
                                if (listaNovedadesCosecha[x].Terceros[y].Codtercero == Convert.ToInt32(gvr.Cells[0].Text))
                                {
                                    listaNovedadesCosecha[x].Terceros[y].Cantidad = Convert.ToDecimal(((TextBox)gvr.FindControl("txtCantidad")).Text);
                                    listaNovedadesCosecha[x].Terceros[y].Jornal = Convert.ToDecimal(((TextBox)gvr.FindControl("txtJornal")).Text);
                                }
                            }
                        }

                    }
                }
            }
        }
        private void BusquedaTransaccion()
        {
            try
            {
                if (this.gvParametros.Rows.Count > 0)
                {
                    string where = operadores.FormatoWhere((List<Coperadores>)Session["operadores"]);

                    this.gvTransaccion.DataSource = transacciones.GetTransaccionCompletaTiquete(where, Convert.ToInt32(Session["empresa"]));
                    this.gvTransaccion.DataBind();

                    this.nilblRegistros.Text = "Nro. Registros " + Convert.ToString(this.gvTransaccion.Rows.Count);

                    EstadoInicialGrillaTransacciones();
                }
                else
                {
                    this.nilblMensajeEdicion.Text = "Debe agregar un filtro antes de la busqueda";
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al procesar la consulta de transacciones. Correspondiente a: " + ex.Message, "C");
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

        #endregion Metodos

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {
                if (!IsPostBack)
                    TabRegistro();
                else
                {
                    if (Request.Form[TabName.UniqueID] == "tabs-1")
                        TabName.Value = "1";

                    if (Request.Form[TabName.UniqueID] == "tabs-2")
                        TabName.Value = "2";

                    if (Request.Form[TabName.UniqueID] == "tabs-3")
                        TabName.Value = "0";
                }

            }


        }
        protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(), nombrePaginaActual(), "I", Convert.ToInt32(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }
            nilblInformacion.Text = "";
            this.Session["editar"] = false;
            LimpiarDDL();
            this.Session["novedadCosecha"] = null;
            this.Session["novedadTransporte"] = null;
            this.Session["novedadCargue"] = null;
            this.nilblInformacion.Text = "";
            this.nilblInformacionDetalle.Text = "";
            ddlSeccion.DataSource = null;
            ddlSeccion.DataBind();
            ddlLote.DataSource = null;
            ddlLote.DataBind();
            gvTiquetes.DataSource = null;
            gvTiquetes.DataBind();
            this.Session["numerotransaccion"] = null;
            this.nilblInformacion.ForeColor = Color.Black;
            this.nilblInformacion.ForeColor = Color.Red;
            txtFechaCosecha.Enabled = false;
            txtFechaTiqueteI.Enabled = false;
            upBascula.Visible = false;
            upDetalle.Visible = false;
            upTiquete.Visible = false;
            dlDetalleCosecha.DataSource = null;
            dlDetalleCosecha.DataBind();
            niimbImprimir.Visible = false;

            CcontrolesUsuario.InhabilitarControles(this.upRecolector.Controls);
            CcontrolesUsuario.LimpiarControles(this.upRecolector.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upDetalle.Controls);
            CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
            cargarCombox();
            this.Session["lote"] = null;
            ComportamientoTransaccion();
            txtFechaTiqueteI.Enabled = false;
            ddlSeccion.Enabled = false;
            upBascula.Visible = Convert.ToBoolean(TipoTransaccionConfig(25));
            rblBascula.Visible = Convert.ToBoolean(TipoTransaccionConfig(25));
            ddlSeccion.DataSource = null;
            ddlSeccion.DataBind();
            ddlLote.DataSource = null;
            ddlLote.DataBind();
            ManejoRBLbascula();
            upBascula.Update();
            cargarExtractoras();
            this.Session["subtotal"] = null;
            nilblInformacion.Text = "";
            this.Session["numerotransaccion"] = null;
            txtFechaCosecha.Enabled = false;
            txtFechaTiqueteI.Enabled = false;
        }
        protected void lbNuevo_Click(object sender, ImageClickEventArgs e)
        {

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                        nombrePaginaActual(), "I", Convert.ToInt32(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }
            this.Session["editar"] = null;
            ManejoEncabezado();
            LimpiarDDL();
            this.Session["novedadCosecha"] = null;
            this.Session["novedadTransporte"] = null;
            this.Session["novedadCargue"] = null;
            this.nilblInformacion.Text = "";
            this.nilblInformacionDetalle.Text = "";
            ddlSeccion.DataSource = null;
            ddlSeccion.DataBind();
            ddlLote.DataSource = null;
            ddlLote.DataBind();
            gvTiquetes.DataSource = null;
            gvTiquetes.DataBind();
            this.Session["numerotransaccion"] = null;
            this.nilblInformacion.ForeColor = Color.Red;
            txtFechaCosecha.Enabled = false;
            txtFechaTiqueteI.Enabled = false;
            niimbImprimir.Visible = false;
            upBascula.Visible = Convert.ToBoolean(TipoTransaccionConfig(25));
            rblBascula.Visible = Convert.ToBoolean(TipoTransaccionConfig(25));
            cargarExtractoras();
            ManejoRBLbascula();
            limpiarSubtotal();
        }
        protected void lbCancelar_Click(object sender, ImageClickEventArgs e)
        {
            InHabilitaEncabezado();
            upRecolector.Visible = false;
            upTiquete.Visible = false;
            upDetalle.Visible = false;
            upTransporte.Visible = false;
            ManejoRBLbascula();
            CcontrolesUsuario.LimpiarControles(this.upRecolector.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upRecolector.Controls);
            CcontrolesUsuario.LimpiarControles(this.upTransporte.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upTransporte.Controls);
            CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upDetalle.Controls);
            CcontrolesUsuario.LimpiarControles(this.upTiquete.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upTiquete.Controls);
            CcontrolesUsuario.LimpiarControles(this.upBascula.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upBascula.Controls);
            this.Session["editar"] = null;
            this.upBascula.Visible = false;
            niimbImprimir.Visible = false;
            this.Session["transaccion"] = null;
            this.dlDetalleCosecha.DataSource = null;
            this.dlDetalleCosecha.DataBind();
            gvTiquetes.DataSource = null;
            gvTiquetes.DataBind();
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
            this.nilblInformacion.ForeColor = Color.Red;
            txtFechaCosecha.Enabled = false;
            txtFechaTiqueteI.Enabled = false;
            limpiarMensajes();

            limpiarSubtotal();

            if (imbConsulta.Enabled == false)
            {
                upConsulta.Visible = true;
                nilbNuevo.Visible = false;
                TabConsulta();
            }
            else
            {
                upConsulta.Visible = false;
                nilbNuevo.Visible = true;
                TabRegistro();
            }
        }
        private void limpiarSubtotal()
        {
            gvSubTotales.DataSource = null;
            gvSubTotales.DataBind();
        }
        protected void ddlFinca_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarComboxDetalle();
        }
        protected void imbCargar_Click(object sender, ImageClickEventArgs e)
        {
            cargarDL();
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
                ManejoError("Error al recuperar posición de configuración de la novedad. Correspondiente a: " + ex.Message, "C");
                return null;
            }
        }
        protected void imbCargar_Click1(object sender, ImageClickEventArgs e)
        {
            nilblInformacionDetalle.Text = "";
            limpiarMensajes();
            nilblInformacionDetalle.Visible = true;
            nilblInformacionDetalle.Text = "";
            nilblInformacionDetalle.ForeColor = Color.Red;

            if (ddlLote.SelectedValue.Length == 0)
            {
                nilblInformacionDetalle.Text = "Debe seleccionar un lote antes de continuar";
                return;
            }

            if (Convert.ToDecimal(txvPneto.Text.Trim()) == 0)
            {
                nilblInformacionDetalle.Text = "La cantidad debe ser diferente de cero";
                return;
            }

            if (txtFechaCosecha.Enabled)
            {
                if (txtFechaCosecha.Text.Length == 0)
                {
                    nilblInformacionDetalle.Text = "debe ingresar una fecha para continuar";
                    return;
                }
            }

            if (Convert.ToDecimal(txvRacimosCosecha.Text) > Convert.ToDecimal(txvRacimosTiquete.Text))
            {
                nilblInformacionDetalle.Text = "El número de racimos no puede ser mayor al tiquete";
                return;
            }
            decimal cantidadTotal = 0, jornalTotal = 0, racimosTotal = 0, subRacimos = 0, subcantidad = 0;
            int contador = 0;
            cantidadTotal = Convert.ToDecimal(txvPneto.Text);
            jornalTotal = Convert.ToDecimal(txvJornalesD.Text);
            racimosTotal = Convert.ToDecimal(txvRacimosTiquete.Text);


            foreach (DataListItem dl in dlDetalleCosecha.Items)
            {
                subRacimos += Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text);
            }


            if (subRacimos + Convert.ToDecimal(txvRacimosCosecha.Text) > racimosTotal & Convert.ToDecimal(txvRacimosTiquete.Text.Trim()) >= 0 & txvRacimosCosecha.Enabled == true)
            {
                this.nilblInformacionDetalle.Text = "Los racimos no deben ser mayor a los del tiquete";
                return;
            }

            for (int x = 0; x < selTerceroCosecha.Items.Count; x++)
            {
                if (selTerceroCosecha.Items[x].Selected)
                    contador++;
            }
            if (contador == 0)
            {
                this.nilblInformacionDetalle.Text = "Debe selecionar un trabajador para continuar";
                return;
            }

            if (transacciones.SeleccionaNovedadLoteRangoSiembra(this.ddlLote.SelectedValue.Trim(), Convert.ToInt32(Session["empresa"]), Convert.ToDateTime(txtFechaCosecha.Text)).Table.Rows.Count <= 0)
            {
                nilblInformacionDetalle.Text = "No hay una asociación de una labor de cosecha con el rango de siembra del lote";
                return;
            }


            if (ObtenerPesoPromedio(ddlLote.SelectedValue, Convert.ToDateTime(txtFechaCosecha.Text)) == 0)
            {
                nilblInformacionDetalle.Text = "El lote seleccionado no tiene peso promedio en el periodo de la fecha";
                return;
            }

            cargarDL();
            LiquidaTransaccioinCosecha(0, 0);
            nilblInformacionDetalle.Text = "liquidación generada exitosamente";
            nilblInformacionDetalle.ForeColor = Color.Green;
            habilitaCosecha(false);
        }
        private void validarRegistros()
        {
            decimal racimosCosecha = 0, racimosCargue = 0, racimosTransporte = 0;

            foreach (DataListItem dl in dlDetalleCosecha.Items)
            {
                racimosCosecha += Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text);
            }
            foreach (DataListItem dl in dlDetalleCargue.Items)
            {
                racimosCargue += Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text);
            }
            foreach (DataListItem dl in dlDetalleTransporte.Items)
            {
                racimosTransporte += Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text);
            }

            if (racimosCosecha == racimosCargue && racimosCosecha == racimosTransporte)
            {
                habilitaCosecha(true);
                cargarCosecha();
                manejoLaborCargue();
                manejoLaborTransporte();
            }
            else
                habilitaCosecha(false);
        }
        private void habilitaCosecha(bool estado)
        {
            ddlLote.Enabled = estado;
            ddlFinca.Enabled = estado;
            //        ddlFinca.Enabled = estado;
            ddlSeccion.Enabled = estado;
            txvJornalesD.Enabled = estado;
            txvRacimosCosecha.Enabled = estado;
            imbCargar.Enabled = estado;
            txtFechaCosecha.Enabled = estado;
            lbFechaD.Enabled = estado;
            if (estado == true)
            {
                txvJornalesD.Text = "0";
                txvRacimosCosecha.Text = "0";
                txtFechaCosecha.Text = "";
                calendarFechaCosecha.Visible = false;
                cargarSesiones();
                cargarLotes();
                ScriptManager1.SetFocus(ddlFinca);
                Session["estadoCosecha"] = false;
                Session["registroNC"] = null;

            }
            else
                Session["estadoCosecha"] = true;



        }
        private void limpiarMensajes()
        {
            nilblInformacionDetalle.Text = "";
            nilblInformacionCargadores.Text = "";
            nilblInformacion.Text = "";
        }
        protected void ddlNovedad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CcontrolesUsuario.LimpiarControles(upDetalle.Controls);
            CcontrolesUsuario.HabilitarControles(upDetalle.Controls);
            imbCargar.Enabled = true;
            nilblInformacionDetalle.Text = "";
        }
        protected void ddlSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarLotes();
        }
        protected void rblBascula_SelectedIndexChanged(object sender, EventArgs e)
        {
            ManejoRBLbascula();

        }
        protected void imbBuscarBascula_Click(object sender, ImageClickEventArgs e)
        {
            cargarTiquetes(txtFiltroBascula.Text);
        }
        protected void dlDetalle_DeleteCommand(object source, DataListCommandEventArgs e)
        {
            try
            {
                bool encontrocosecha = false;
                bool encontrotransporte = false;
                bool encontrocargue = false;
                int posicionNovedad = 0;
                int registro = Convert.ToInt32(((Label)dlDetalleCosecha.Items[e.Item.ItemIndex].FindControl("lblRegistro")).Text.Trim());

                if (listaNovedadesCosecha.Count == 0)
                    nilblInformacionDetalle.Text = "";

                listaNovedadesTransporte = (List<CnovedadTransporte>)this.Session["novedadTransporte"];
                if (listaNovedadesTransporte.Count > 0)
                {
                    posicionNovedad = 0;
                    for (int x = 0; x < listaNovedadesTransporte.Count; x++)
                    {
                        if (registro == listaNovedadesTransporte[x].Registro)
                        {
                            posicionNovedad = x;
                            encontrotransporte = true;
                            break;
                        }

                    }

                    if (encontrotransporte)
                    {
                        listaNovedadesTransporte.RemoveAt(posicionNovedad);
                        //listaNovedadesTransporte = reasignarRegistrosTransporte(listaNovedadesTransporte);
                        dlDetalleTransporte.DataSource = listaNovedadesTransporte;
                        dlDetalleTransporte.DataBind();

                        foreach (DataListItem d in dlDetalleTransporte.Items)
                        {
                            foreach (CnovedadTransporte nt in listaNovedadesTransporte)
                            {
                                if (((Label)d.FindControl("lblRegistro")).Text.Trim() == nt.Registro.ToString())
                                {
                                    ((GridView)d.FindControl("gvLotes")).DataSource = nt.Terceros;
                                    ((GridView)d.FindControl("gvLotes")).DataBind();
                                }
                            }
                        }
                        this.Session["novedadTransporte"] = listaNovedadesTransporte;
                        LiquidaTransaccioinTransporte(0, 0);
                    }
                }
                listaNovedadesCargue = (List<CnovedadCargue>)this.Session["novedadCargue"];
                if (listaNovedadesCargue.Count > 0)
                {
                    posicionNovedad = 0;
                    for (int x = 0; x < listaNovedadesCargue.Count; x++)
                    {
                        if (registro == listaNovedadesCargue[x].Registro)
                        {
                            posicionNovedad = x;
                            encontrocargue = true;
                            break;
                        }
                    }

                    if (encontrocargue)
                    {
                        listaNovedadesCargue.RemoveAt(posicionNovedad);
                        //listaNovedadesCargue = reasignarRegistrosCargue(listaNovedadesCargue);
                        dlDetalleCargue.DataSource = listaNovedadesCargue;
                        dlDetalleCargue.DataBind();

                        foreach (DataListItem d in dlDetalleCargue.Items)
                        {
                            foreach (CnovedadCargue nt in listaNovedadesCargue)
                            {
                                if (((Label)d.FindControl("lblRegistro")).Text.Trim() == nt.Registro.ToString())
                                {
                                    ((GridView)d.FindControl("gvLotes")).DataSource = nt.Terceros;
                                    ((GridView)d.FindControl("gvLotes")).DataBind();
                                }
                            }
                        }
                        this.Session["novedadCargue"] = listaNovedadesCargue;
                        LiquidaTransaccioinCargue(0, 0);
                    }
                }

                listaNovedadesCosecha = (List<CnovedadCosecha>)this.Session["novedadCosecha"];

                for (int x = 0; x < listaNovedadesCosecha.Count; x++)
                {
                    if (registro == listaNovedadesCosecha[x].Registro)
                    {
                        posicionNovedad = x;
                        encontrocosecha = true;
                        break;
                    }
                }

                if (encontrocosecha)
                {
                    listaNovedadesCosecha.RemoveAt(posicionNovedad);
                    //listaNovedadesCosecha = reasignarRegistrosCosecha(listaNovedadesCosecha);
                    dlDetalleCosecha.DataSource = listaNovedadesCosecha;
                    dlDetalleCosecha.DataBind();
                    Session["registroNC"] = dlDetalleCosecha.Items.Count;
                    foreach (DataListItem d in dlDetalleCosecha.Items)
                    {
                        foreach (CnovedadCosecha nt in listaNovedadesCosecha)
                        {
                            if (((Label)d.FindControl("lblRegistro")).Text.Trim() == nt.Registro.ToString())
                            {
                                ((GridView)d.FindControl("gvLotes")).DataSource = nt.Terceros;
                                ((GridView)d.FindControl("gvLotes")).DataBind();
                            }
                        }
                    }
                    this.Session["novedadCosecha"] = listaNovedadesCosecha;
                    if (listaNovedadesCosecha.Count > 0)
                        LiquidaTransaccioinCosecha(0, 0);
                }
                habilitaCosecha(true);
            }
            catch (Exception ex)
            {
                nilblInformacionDetalle.Text = "Error al eliminar el registro, debido a: " + ex.ToString();
            }

        }
        protected void txvPtara_TextChanged(object sender, EventArgs e)
        {
            txvPneto.Text = Convert.ToString(Convert.ToDecimal(txvPbruto.Text) - Convert.ToDecimal(txvPtara.Text));
        }
        protected void txvPbruto_TextChanged(object sender, EventArgs e)
        {
            txvPneto.Text = Convert.ToString(Convert.ToDecimal(txvPbruto.Text) - Convert.ToDecimal(txvPtara.Text));
        }
        protected void imbLiquidar_Click(object sender, ImageClickEventArgs e)
        {
            LiquidaTransaccioinCosecha(0, 0);

        }
        protected void ddlExtractoraFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarTiquetes("");
        }
        protected void gvTiquetes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CcontrolesUsuario.HabilitarControles(upTiquete.Controls);
            CcontrolesUsuario.InhabilitarUsoControles(upTiquete.Controls);
            string ddlexfiltro = ddlExtractoraFiltro.SelectedValue.Trim();
            cargarCombox();
            upTiquete.Visible = true;
            upPestana.Visible = true;
            lbFecha.Enabled = true;
            txtFecha.Enabled = true;
            txtRemision.Enabled = true;
            txtObservacion.Enabled = true;
            try
            {
                if (this.gvTiquetes.SelectedRow.Cells[1].Text != "&nbsp;")
                {
                    try
                    {
                        ddlExtractoraTiquete.SelectedValue = Server.HtmlDecode(this.gvTiquetes.SelectedRow.Cells[1].Text.Trim());
                    }
                    catch (Exception)
                    {
                        cargarExtractora();
                        ddlExtractoraTiquete.SelectedValue = Server.HtmlDecode(this.gvTiquetes.SelectedRow.Cells[1].Text.Trim());
                    }
                    try
                    {
                        ddlExtractoraTiquete.SelectedValue = Server.HtmlDecode(this.gvTiquetes.SelectedRow.Cells[1].Text.Trim());
                    }
                    catch (Exception)
                    {
                        cargarExtractora();
                        ddlExtractoraFiltro.SelectedValue = ddlexfiltro;
                    }
                }
                else
                    ddlExtractoraTiquete.SelectedValue = "";

                if (this.gvTiquetes.SelectedRow.Cells[2].Text != "&nbsp;")
                    txtTiquete.Text = Server.HtmlDecode(this.gvTiquetes.SelectedRow.Cells[2].Text);
                else
                    txtTiquete.Text = "";

                if (this.gvTiquetes.SelectedRow.Cells[3].Text != "&nbsp;")
                {
                    niCalendarFechaTiqueteI.SelectedDate = Convert.ToDateTime(Server.HtmlDecode(this.gvTiquetes.SelectedRow.Cells[3].Text));
                    txtFechaTiqueteI.Text = Server.HtmlDecode(this.gvTiquetes.SelectedRow.Cells[3].Text);
                }
                else
                    txtFechaTiqueteI.Text = "";

                if (this.gvTiquetes.SelectedRow.Cells[4].Text != "&nbsp;")
                    txtVehiculo.Text = Server.HtmlDecode(this.gvTiquetes.SelectedRow.Cells[4].Text);
                else
                    txtVehiculo.Text = "";

                if (this.gvTiquetes.SelectedRow.Cells[5].Text != "&nbsp;")
                    txtRemolque.Text = Server.HtmlDecode(this.gvTiquetes.SelectedRow.Cells[5].Text);
                else
                    txtVehiculo.Text = "";

                if (this.gvTiquetes.SelectedRow.Cells[6].Text != "&nbsp;")
                    txvPbruto.Text = Server.HtmlDecode(Convert.ToDecimal(this.gvTiquetes.SelectedRow.Cells[6].Text).ToString());
                else
                    txvPbruto.Text = "0";

                if (this.gvTiquetes.SelectedRow.Cells[7].Text != "&nbsp;")
                    txvPtara.Text = Server.HtmlDecode(Convert.ToDecimal(this.gvTiquetes.SelectedRow.Cells[7].Text).ToString());
                else
                    txvPtara.Text = "0";

                if (this.gvTiquetes.SelectedRow.Cells[8].Text != "&nbsp;")
                    txvPneto.Text = Server.HtmlDecode(Convert.ToDecimal(this.gvTiquetes.SelectedRow.Cells[8].Text).ToString());
                else
                    txvPneto.Text = "0";

                if (this.gvTiquetes.SelectedRow.Cells[9].Text != "&nbsp;")
                    txvSacos.Text = Server.HtmlDecode(Convert.ToDecimal(this.gvTiquetes.SelectedRow.Cells[9].Text).ToString());
                else
                    txvSacos.Text = "0";

                if (this.gvTiquetes.SelectedRow.Cells[10].Text != "&nbsp;")
                    txvRacimosTiquete.Text = Server.HtmlDecode(Convert.ToDecimal(this.gvTiquetes.SelectedRow.Cells[10].Text).ToString());
                else
                    txvRacimosTiquete.Text = "0";

                if (this.gvTiquetes.SelectedRow.Cells[11].Text != "&nbsp;")
                {
                    ddlFinca.SelectedValue = this.gvTiquetes.SelectedRow.Cells[11].Text;
                    cargarSesiones();
                    cargarLotes();
                }

                gvTiquetes.DataSource = null;
                gvTiquetes.DataBind();
                manejoRecolector();
                manejoLaborCargue();
                manejoLaborTransporte();
                tabs.Visible = true;

                if (txtFechaTiqueteI.Text.Trim().Length == 0)
                {
                    selTerceroCosecha.Visible = false;
                    nilblInformacion.Text = "Debe seleccionar una fecha para continuar";
                    return;
                }
                else
                    selTerceroCosecha.Visible = true;

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }
        private void manejoRecolector()
        {
            upRecolector.Visible = true;
            upDetalle.Visible = true;
            upTransporte.Visible = true;
            CcontrolesUsuario.LimpiarControles(this.upRecolector.Controls);
            CcontrolesUsuario.HabilitarControles(this.upRecolector.Controls);
            CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
            CcontrolesUsuario.HabilitarControles(this.upDetalle.Controls);
            CcontrolesUsuario.LimpiarControles(this.upTransporte.Controls);
            CcontrolesUsuario.HabilitarControles(this.upTransporte.Controls);
            selTerceroCargue.Visible = true;
        }
        protected void lbFecha_Click(object sender, EventArgs e)
        {
            this.niCalendarFechaTiqueteI.Visible = true;
            this.txtFechaTiqueteI.Visible = false;
            this.niCalendarFechaTiqueteI.SelectedDate = Convert.ToDateTime(null);
            this.niCalendarFechaTiqueteI.Enabled = true;
        }
        protected void lbFechaD_Click(object sender, EventArgs e)
        {
            this.calendarFechaCosecha.Visible = true;
            this.txtFechaCosecha.Visible = false;
            this.calendarFechaCosecha.SelectedDate = Convert.ToDateTime(null);
        }
        protected void niCalendarFechaD_SelectionChanged(object sender, EventArgs e)
        {
            string fecha = calendarFechaCosecha.SelectedDate.ToShortDateString();

            if (upBascula.Visible == true)
            {
                if (txtFechaTiqueteI.Text.Trim().Length == 0)
                {
                    nilblInformacionDetalle.Text = "Debe seleccionar una fecha de tiquete para continuar";
                    return;
                }
                if ((Convert.ToDateTime(txtFechaTiqueteI.Text).Date < Convert.ToDateTime(fecha)))
                {
                    nilblInformacionDetalle.Text = "La fecha de la novedad no puede ser mayo a la del tiquete";
                    return;
                }
            }
            nilblInformacionDetalle.Text = "";
            this.calendarFechaCosecha.Visible = false;
            this.txtFechaCosecha.Visible = true;
            this.txtFechaCosecha.Text = this.calendarFechaCosecha.SelectedDate.ToShortDateString();
        }
        protected void niCalendarFechaTiqueteI_SelectionChanged1(object sender, EventArgs e)
        {
            this.niCalendarFechaTiqueteI.Visible = false;
            this.txtFechaTiqueteI.Visible = true;
            this.txtFechaTiqueteI.Text = this.niCalendarFechaTiqueteI.SelectedDate.ToShortDateString();
            this.txtFechaTiqueteI.Enabled = false;

            verificaPeriodoCerrado(Convert.ToInt32(this.niCalendarFechaTiqueteI.SelectedDate.Year), Convert.ToInt32(this.niCalendarFechaTiqueteI.SelectedDate.Month), Convert.ToInt32(Session["empresa"]), niCalendarFechaTiqueteI.SelectedDate);
        }
        protected void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataListItem dli in dlDetalleCosecha.Items)
            {
                if (((CheckBox)dli.FindControl("chkSeleccionar")).Checked == true)
                {
                    cargarSesiones();
                    cargarLotes();
                    string novedad = ((Label)dli.FindControl("lblNovedadCargue")).Text;
                    string seccion = ((Label)dli.FindControl("lblSeccion")).Text;
                    string lote = ((Label)dli.FindControl("lblLote")).Text;
                    string fecha = Convert.ToDateTime(((Label)dli.FindControl("lblFechaD")).Text).ToShortDateString();
                    ddlSeccion.SelectedValue = seccion.Trim();
                    ddlLote.SelectedValue = lote;
                    txtFechaCosecha.Text = fecha;
                    ddlLote.Enabled = false;
                    ddlFinca.Enabled = false;
                    ddlFinca.Enabled = false;
                    ddlSeccion.Enabled = false;
                    txvRacimosCosecha.ReadOnly = false;
                    txvJornalesD.ReadOnly = false;
                    LiquidaTransaccioinCosecha(0, 0);
                }

            }
        }
        protected void lbCancelarD_Click(object sender, ImageClickEventArgs e)
        {
            cargarComboxDetalle();
        }
        protected void ddlFinca_SelectedIndexChanged2(object sender, EventArgs e)
        {

            if (txtFechaTiqueteI.Text.Trim().Length == 0)
            {
                selTerceroCosecha.Visible = false;
                nilblInformacion.Text = "Debe seleccionar una fecha para continuar";
                return;
            }
            else
                selTerceroCosecha.Visible = true;
            cargarComboxDetalle();
        }
        protected void chkLaborTransporte_CheckedChanged(object sender, EventArgs e)
        {
            manejoLaborTransporte();
        }
        protected void niimbImprimir_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected void lbFechaCargadores_Click(object sender, EventArgs e)
        {
            this.calendarCargadores.Visible = true;
            this.txtFechaCargue.Visible = false;
            this.calendarCargadores.SelectedDate = Convert.ToDateTime(null);
        }
        protected void calendarCargadores_SelectionChanged(object sender, EventArgs e)
        {
            string fecha = calendarCargadores.SelectedDate.ToShortDateString();

            if (upBascula.Visible == true)
            {

                if (txtFechaTiqueteI.Text.Trim().Length == 0)
                {
                    nilblInformacionCargadores.Text = "Debe seleccionar una fecha de tiquete para continuar";
                    return;
                }

                if ((Convert.ToDateTime(txtFechaTiqueteI.Text).Date < Convert.ToDateTime(fecha)))
                {
                    nilblInformacionCargadores.Text = "La fecha de la novedad no puede ser mayo a la del tiquete";
                    return;
                }
            }


            this.calendarCargadores.Visible = false;
            this.txtFechaCargue.Visible = true;
            this.txtFechaCargue.Text = this.calendarCargadores.SelectedDate.ToShortDateString();
            //this.txtFechaD.Enabled = false;
        }
        protected void txtFechaD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFechaCosecha.Text);
            }
            catch (Exception)
            {
                nilblInformacionDetalle.Text = "Formato de fecha invalido";
                return;

            }
            string fecha = Convert.ToDateTime(txtFechaCosecha.Text).ToShortDateString();
            if (upBascula.Visible == true)
            {

                if (txtFechaTiqueteI.Text.Trim().Length == 0)
                {
                    nilblInformacionDetalle.Text = "Debe seleccionar una fecha de tiquete para continuar";
                    return;
                }

                if ((Convert.ToDateTime(txtFechaTiqueteI.Text).Date < Convert.ToDateTime(fecha)))
                {
                    nilblInformacionDetalle.Text = "La fecha de cosecha no puede ser mayo a la del tiquete";
                    txtFechaCosecha.Text = "";
                    txtFechaCosecha.Focus();
                    return;
                }
            }
            ScriptManager1.SetFocus(txvRacimosCosecha);
        }
        protected void txtFechaCargadores_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFechaCargue.Text);
                txvRacimoCargue.Focus();

            }
            catch
            {
                nilblInformacionCargadores.Text = "Formato de fecha no valido";
                txtFechaCargue.Text = "";
                txtFechaCargue.Focus();
                return;
            }

            if (Convert.ToDateTime(txtFechaCargue.Text) < Convert.ToDateTime(txtFechaCosecha.Text))
            {
                nilblInformacionCargadores.Text = "La fecha de cargue no puede ser menor a la cosecha";
                txtFechaCargue.Text = "";
                txtFechaCargue.Focus();
                return;
            }

            if (Convert.ToDateTime(txtFechaCargue.Text) > Convert.ToDateTime(txtFechaTiqueteI.Text))
            {
                nilblInformacionCargadores.Text = "La fecha de cargue no puede ser mayor a la del tiquete";
                txtFechaCargue.Text = "";
                txtFechaCargue.Focus();
                return;
            }

        }
        protected void niimbAdicionar_Click(object sender, ImageClickEventArgs e)
        {
            foreach (GridViewRow registro in this.gvParametros.Rows)
            {
                if (Convert.ToString(this.niddlCampo.SelectedValue) == registro.Cells[1].Text && Convert.ToString(this.niddlOperador.SelectedValue) == Server.HtmlDecode(registro.Cells[2].Text) &&
                    this.nitxtValor1.Text == registro.Cells[3].Text)
                    return;
            }

            operadores = new Coperadores(Convert.ToString(this.niddlCampo.SelectedValue), Server.HtmlDecode(Convert.ToString(this.niddlOperador.SelectedValue)),
                this.nitxtValor1.Text, this.nitxtValor2.Text);

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
            this.nilblMensajeEdicion.Text = "";

            EstadoInicialGrillaTransacciones();
            imbBusqueda.Focus();
        }
        protected void imbBusqueda_Click(object sender, ImageClickEventArgs e)
        {
            this.nilblMensajeEdicion.Text = "";
            BusquedaTransaccion();
        }
        protected void imbConsulta_Click(object sender, ImageClickEventArgs e)
        {
            TabConsulta();
        }
        private void TabConsulta()
        {
            tabs.Visible = false;
            CcontrolesUsuario.HabilitarControles(upConsulta.Controls);
            upGeneral.Visible = true;
            this.upBascula.Visible = false;
            this.upDetalle.Visible = false;
            this.upRecolector.Visible = false;
            this.upConsulta.Visible = true;
            this.niimbRegistro.BorderStyle = BorderStyle.None;
            this.imbConsulta.BorderStyle = BorderStyle.Solid;
            this.imbConsulta.BorderColor = System.Drawing.Color.Silver;
            this.imbConsulta.BorderWidth = Unit.Pixel(1);
            imbBusqueda.Visible = true;
            nitxtValor2.Visible = false;
            this.niimbRegistro.Enabled = true;
            lbCancelar.Visible = false;
            lbRegistrar.Visible = false;
            this.Session["transaccion"] = null;
            this.gvSubTotales.DataSource = null;
            this.gvSubTotales.DataBind();
            this.lbRegistrar.Enabled = true;
            this.gvTiquetes.DataSource = null;
            this.gvTiquetes.DataBind();
            this.nilbNuevo.Visible = false;
            this.niimbImprimir.Visible = false;
            this.imbConsulta.Enabled = false;
            this.nilblInformacion.Text = "";
            gvParametros.DataSource = null;
            gvParametros.DataBind();
            gvTransaccion.DataSource = null;
            gvTransaccion.DataBind();
            this.Session["operadores"] = null;
        }
        protected void niimbRegistro_Click(object sender, ImageClickEventArgs e)
        {
            TabRegistro();
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
                    this.imbBusqueda.Visible = false;
                EstadoInicialGrillaTransacciones();
            }
            catch
            {
            }
        }
        protected void gvTransaccion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.nilblMensajeEdicion.Text = "";

            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    this.Session["numerotransaccion"] = this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text;
                    if (transacciones.VerificaEdicionBorrado(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text, Convert.ToInt32(Session["empresa"])) != 0)
                    {
                        this.nilblMensajeEdicion.Text = "Transacción ejecutada / anulada no es posible su edición";
                        return;
                    }

                    if (tipoTransaccion.RetornaTipoBorrado(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, Convert.ToInt32(Session["empresa"])) == "E")
                    {
                        switch (transacciones.EliminarTransaccionLabores(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text, Convert.ToInt32(Session["empresa"])))
                        {
                            case 0:
                                nilblMensajeEdicion.Text = "Registro Anulado satisfactoriamente";
                                BusquedaTransaccion();
                                ts.Complete();
                                break;
                            case 1:
                                nilblMensajeEdicion.Text = "Error al eliminar registros ";
                                break;
                        }
                    }
                    else
                    {
                        switch (transacciones.AnulaTransaccion(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text, this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text, this.Session["usuario"].ToString().Trim(), Convert.ToInt32(Session["empresa"])))
                        {
                            case 0:
                                nilblMensajeEdicion.Text = "Registro Anulado satisfactoriamente";
                                BusquedaTransaccion();
                                ts.Complete();
                                break;
                            case 1:
                                nilblMensajeEdicion.Text = "Error al anular la transacción. Operación no realizada";
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ManejoError("Error al eliminar el registro. Correspondiente a: " + ex.Message, "E");
                }
            }

        }
        protected void gvTransaccion_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            this.Session["editar"] = null;
            bool anulado = false;
            foreach (Control objControl in gvTransaccion.Rows[e.RowIndex].Cells[7].Controls)
            {
                anulado = ((CheckBox)objControl).Checked;
            }

            if (anulado == true)
            {
                CcontrolesUsuario.MensajeError("Registro anulado no es posible su edición", nilblMensajeEdicion);
                return;
            }

            if (transacciones.validaEjecutarTransaccion(this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text.Trim(), this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text.Trim(), Convert.ToInt32(Session["empresa"])) == 1)
            {
                CcontrolesUsuario.MensajeError("Registro ejecutado no es posible su edición", nilblMensajeEdicion);
                return;
            }

            try
            {
                DateTime fecha = Convert.ToDateTime(this.gvTransaccion.Rows[e.RowIndex].Cells[4].Text);

                if (periodo.RetornaPeriodoCerradoNomina(fecha.Year, fecha.Month, Convert.ToInt32(Session["empresa"]), fecha) == 1)
                {
                    ManejoError("Periodo cerrado de nomina. No es posible realizar nuevas transacciones", "I");
                }
                else
                {
                    manejoEdicion();
                    hfTipo.Value = this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text.Trim();
                    hfNumero.Value = this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text.Trim();
                    cargarTiqueteGuardado();
                    DataView dvEncabezado = transacciones.RetornaEncabezadoTransaccionLabores(hfTipo.Value, hfNumero.Value, Convert.ToInt32(Session["empresa"]));

                    foreach (DataRowView drv in dvEncabezado)
                    {
                        if (drv.Row.ItemArray.GetValue(17).ToString() != "&nbsp;")
                        {
                            txtRemision.Text = drv.Row.ItemArray.GetValue(17).ToString();
                        }
                        if (drv.Row.ItemArray.GetValue(5).ToString() != "&nbsp;")
                        {
                            txtFecha.Text = Convert.ToDateTime(drv.Row.ItemArray.GetValue(5)).ToShortDateString();
                        }

                        if (drv.Row.ItemArray.GetValue(15).ToString() != "&nbsp;")
                        {
                            txtObservacion.Text = drv.Row.ItemArray.GetValue(15).ToString();
                        }
                    }


                    cargarDLTransporteEdicion();
                    cargarDLCargueEdicion();
                    cargarDLCosechaEdicion();
                    this.Session["editar"] = true;
                    habilitaCosecha(true);
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los datos. Correspondiente a: " + ex.Message, "C");
            }

        }

        private void cargarDLCosechaEdicion()
        {
            int posicionNovedad = 0;
            nilblInformacionDetalle.Text = "";
            decimal pesoRacimo = 0, cantidad = 0, precioLabor = 0, racimos = 0, jornales = 0;
            string novedad = null, nombreNovedad = null, uMedidad = null, lote = null, fechaDetalle = null, tercerodetalle = null, nombreTerceroDetalle = null,
                 nombreLote = null, seccion = null, nombreSeccion = null;
            listaTerceros = null;

            DataView dvDetalleE = transacciones.RetornaEncabezadoTransaccionLaboresDetalle(hfTipo.Value, hfNumero.Value, Convert.ToInt32(Session["empresa"]));
            DataView dvDetalleD = transacciones.RetornaEncabezadoTransaccionLaboresTercero(hfTipo.Value, hfNumero.Value, Convert.ToInt32(Session["empresa"]));
            DataView dvCosechaD = dvDetalleD;
            DataView dvCosechaE = dvDetalleE;
            dvCosechaD.RowFilter = "claseLabor=2";
            dvCosechaE.RowFilter = "claseLabor=2";
            dvCosechaD.Sort = "registronovedad";
            dvDetalleE.Sort = "registronovedad";
            listaNovedadesCosecha = new List<CnovedadCosecha>();
            foreach (DataRowView drv in dvCosechaE)
            {
                listaTerceros = new List<CterceroTiquete>();
                foreach (DataRowView drvt in dvCosechaD)
                {
                    if (Convert.ToDecimal(drv.Row.ItemArray.GetValue(17)) == Convert.ToDecimal(drvt.Row.ItemArray.GetValue(12)))
                    {
                        tercerodetalle = drvt.Row.ItemArray.GetValue(1).ToString();
                        nombreTerceroDetalle = drvt.Row.ItemArray.GetValue(2).ToString();
                        novedad = drvt.Row.ItemArray.GetValue(0).ToString();
                        precioLabor = Convert.ToDecimal(drvt.Row.ItemArray.GetValue(9).ToString());
                        lote = drvt.Row.ItemArray.GetValue(3).ToString();
                        terceros = new CterceroTiquete(Convert.ToInt32(tercerodetalle), nombreTerceroDetalle, lote, null, 0, 0, precioLabor, novedad);
                        listaTerceros.Add(terceros);
                    }
                }

                novedad = drv.Row.ItemArray.GetValue(2).ToString();
                lote = drv.Row.ItemArray.GetValue(4).ToString();
                fechaDetalle = drv.Row.ItemArray.GetValue(11).ToString();
                precioLabor = Convert.ToDecimal(drv.Row.ItemArray.GetValue(16).ToString());
                nombreNovedad = drv.Row.ItemArray.GetValue(3).ToString();
                nombreLote = drv.Row.ItemArray.GetValue(5).ToString();
                seccion = drv.Row.ItemArray.GetValue(7).ToString();
                nombreSeccion = drv.Row.ItemArray.GetValue(8).ToString();
                racimos = Convert.ToDecimal(drv.Row.ItemArray.GetValue(6).ToString());
                cantidad = Convert.ToDecimal(drv.Row.ItemArray.GetValue(10).ToString());
                uMedidad = drv.Row.ItemArray.GetValue(9).ToString();
                jornales = Convert.ToDecimal(drv.Row.ItemArray.GetValue(13).ToString());


                if (this.Session["novedadCosecha"] == null)
                {
                    novedadCosecha = new CnovedadCosecha(novedad, nombreNovedad, lote, nombreLote, seccion, nombreSeccion,
                        racimos, cantidad, listaTerceros, Convert.ToInt16(drv.Row.ItemArray.GetValue(17)), uMedidad, pesoRacimo, fechaDetalle, jornales, precioLabor);
                    listaNovedadesCosecha.Add(novedadCosecha);
                    this.Session["novedadCosecha"] = listaNovedadesCosecha;

                }
                else
                {
                    listaNovedadesCosecha = (List<CnovedadCosecha>)this.Session["novedadCosecha"];

                    novedadCosecha = new CnovedadCosecha(novedad, nombreNovedad, lote, nombreLote, seccion, nombreSeccion,
                     racimos, cantidad, listaTerceros, Convert.ToInt16(drv.Row.ItemArray.GetValue(17)), uMedidad, pesoRacimo, fechaDetalle, jornales, precioLabor);
                    listaNovedadesCosecha.Add(novedadCosecha);
                    this.Session["novedadCosecha"] = listaNovedadesCosecha;
                    posicionNovedad++;
                }

                dlDetalleCosecha.DataSource = listaNovedadesCosecha;
                dlDetalleCosecha.DataBind();
                Session["registroNC"] = dlDetalleCosecha.Items.Count;
                foreach (DataListItem d in dlDetalleCosecha.Items)
                {
                    foreach (CnovedadCosecha nt in listaNovedadesCosecha)
                    {
                        if (((Label)d.FindControl("lblRegistro")).Text.Trim() == nt.Registro.ToString())
                        {
                            ((GridView)d.FindControl("gvLotes")).DataSource = nt.Terceros;
                            ((GridView)d.FindControl("gvLotes")).DataBind();
                        }
                    }


                }
            }
            LiquidaTransaccioinCosecha(0, 0);
            habilitaCosecha(false);
        }
        private void cargarDLCargueEdicion()
        {
            int posicionNovedad = 0;
            nilblInformacionDetalle.Text = "";
            decimal pesoRacimo = 0, cantidad = 0, precioLabor = 0, racimos = 0, jornales = 0;
            string novedad = null, nombreNovedad = null, uMedidad = null, lote = null, fechaDetalle = null, tercerodetalle = null, nombreTerceroDetalle = null,
                 nombreLote = null, seccion = null, nombreSeccion = null;
            listaTerceros = null;

            DataView dvDetalleE = transacciones.RetornaEncabezadoTransaccionLaboresDetalle(hfTipo.Value, hfNumero.Value, Convert.ToInt32(Session["empresa"]));
            DataView dvDetalleD = transacciones.RetornaEncabezadoTransaccionLaboresTercero(hfTipo.Value, hfNumero.Value, Convert.ToInt32(Session["empresa"]));
            DataView dvCosechaD = dvDetalleD;
            DataView dvCosechaE = dvDetalleE;
            dvCosechaD.RowFilter = "claseLabor=3";
            dvCosechaE.RowFilter = "claseLabor=3";

            listaNovedadesCargue = new List<CnovedadCargue>();
            foreach (DataRowView drv in dvCosechaE)
            {
                listaTerceros = new List<CterceroTiquete>();
                foreach (DataRowView drvt in dvCosechaD)
                {
                    if (Convert.ToDecimal(drv.Row.ItemArray.GetValue(17)) == Convert.ToDecimal(drvt.Row.ItemArray.GetValue(12)))
                    {
                        tercerodetalle = drvt.Row.ItemArray.GetValue(1).ToString();
                        nombreTerceroDetalle = drvt.Row.ItemArray.GetValue(2).ToString();
                        novedad = drvt.Row.ItemArray.GetValue(0).ToString();
                        precioLabor = Convert.ToDecimal(drvt.Row.ItemArray.GetValue(9).ToString());
                        lote = drvt.Row.ItemArray.GetValue(3).ToString();
                        terceros = new CterceroTiquete(Convert.ToInt32(tercerodetalle), nombreTerceroDetalle, lote, null, 0, 0, precioLabor, novedad);
                        listaTerceros.Add(terceros);
                    }
                }

                novedad = drv.Row.ItemArray.GetValue(2).ToString();
                lote = drv.Row.ItemArray.GetValue(4).ToString();
                fechaDetalle = drv.Row.ItemArray.GetValue(11).ToString();
                precioLabor = Convert.ToDecimal(drv.Row.ItemArray.GetValue(16).ToString());
                nombreNovedad = drv.Row.ItemArray.GetValue(3).ToString();
                nombreLote = drv.Row.ItemArray.GetValue(5).ToString();
                seccion = drv.Row.ItemArray.GetValue(7).ToString();
                nombreSeccion = drv.Row.ItemArray.GetValue(8).ToString();
                racimos = Convert.ToDecimal(drv.Row.ItemArray.GetValue(6).ToString());
                cantidad = Convert.ToDecimal(drv.Row.ItemArray.GetValue(10).ToString());
                uMedidad = drv.Row.ItemArray.GetValue(9).ToString();
                jornales = Convert.ToDecimal(drv.Row.ItemArray.GetValue(13).ToString());


                if (this.Session["novedadCargue"] == null)
                {
                    novedadCargue = new CnovedadCargue(novedad, nombreNovedad, lote, nombreLote, seccion, nombreSeccion,
                        racimos, cantidad, listaTerceros, Convert.ToInt16(drv.Row.ItemArray.GetValue(17)), uMedidad, pesoRacimo, fechaDetalle, jornales, precioLabor, listaNovedadesCargue.Count);
                    listaNovedadesCargue.Add(novedadCargue);
                    this.Session["novedadCargue"] = listaNovedadesCargue;
                }
                else
                {
                    listaNovedadesCargue = (List<CnovedadCargue>)this.Session["novedadCargue"];

                    novedadCargue = new CnovedadCargue(novedad, nombreNovedad, lote, nombreLote, seccion, nombreSeccion,
                     racimos, cantidad, listaTerceros, Convert.ToInt16(drv.Row.ItemArray.GetValue(17)), uMedidad, pesoRacimo, fechaDetalle, jornales, precioLabor, listaNovedadesCargue.Count);
                    listaNovedadesCargue.Add(novedadCargue);
                    this.Session["novedadCargue"] = listaNovedadesCargue;
                    posicionNovedad++;
                }

                dlDetalleCargue.DataSource = listaNovedadesCargue;
                dlDetalleCargue.DataBind();

                foreach (DataListItem d in dlDetalleCargue.Items)
                {
                    foreach (CnovedadCargue nt in listaNovedadesCargue)
                    {
                        var lblregistrocosecha = d.FindControl("lblRegistro") as Label;
                        if (lblregistrocosecha.Text.Trim() == nt.Registro.ToString())
                        {
                            ((GridView)d.FindControl("gvLotes")).DataSource = nt.Terceros;
                            ((GridView)d.FindControl("gvLotes")).DataBind();
                        }
                    }
                }
            }
            LiquidaTransaccioinCargue(0, 0);
        }
        private void cargarDLTransporteEdicion()
        {
            int posicionNovedad = 0;
            nilblInformacionDetalle.Text = "";
            decimal pesoRacimo = 0, cantidad = 0, precioLabor = 0, racimos = 0, jornales = 0;
            string novedad = null, nombreNovedad = null, uMedidad = null, lote = null, fechaDetalle = null, tercerodetalle = null, nombreTerceroDetalle = null,
                 nombreLote = null, seccion = null, nombreSeccion = null;
            listaTerceros = null;

            DataView dvDetalleE = transacciones.RetornaEncabezadoTransaccionLaboresDetalle(hfTipo.Value, hfNumero.Value, Convert.ToInt32(Session["empresa"]));
            DataView dvDetalleD = transacciones.RetornaEncabezadoTransaccionLaboresTercero(hfTipo.Value, hfNumero.Value, Convert.ToInt32(Session["empresa"]));
            DataView dvCosechaD = dvDetalleD;
            DataView dvCosechaE = dvDetalleE;
            dvCosechaD.RowFilter = "claseLabor=4";
            dvCosechaE.RowFilter = "claseLabor=4";

            listaNovedadesTransporte = new List<CnovedadTransporte>();
            foreach (DataRowView drv in dvCosechaE)
            {
                listaTerceros = new List<CterceroTiquete>();
                foreach (DataRowView drvt in dvCosechaD)
                {
                    if (Convert.ToDecimal(drv.Row.ItemArray.GetValue(17)) == Convert.ToDecimal(drvt.Row.ItemArray.GetValue(12)))
                    {
                        tercerodetalle = drvt.Row.ItemArray.GetValue(1).ToString();
                        nombreTerceroDetalle = drvt.Row.ItemArray.GetValue(2).ToString();
                        novedad = drvt.Row.ItemArray.GetValue(0).ToString();
                        precioLabor = Convert.ToDecimal(drvt.Row.ItemArray.GetValue(9).ToString());
                        lote = drvt.Row.ItemArray.GetValue(3).ToString();
                        terceros = new CterceroTiquete(Convert.ToInt32(tercerodetalle), nombreTerceroDetalle, lote, null, 0, 0, precioLabor, novedad);
                        listaTerceros.Add(terceros);
                    }
                }

                novedad = drv.Row.ItemArray.GetValue(2).ToString();
                lote = drv.Row.ItemArray.GetValue(4).ToString();
                fechaDetalle = drv.Row.ItemArray.GetValue(11).ToString();
                precioLabor = Convert.ToDecimal(drv.Row.ItemArray.GetValue(16).ToString());
                nombreNovedad = drv.Row.ItemArray.GetValue(3).ToString();
                nombreLote = drv.Row.ItemArray.GetValue(5).ToString();
                seccion = drv.Row.ItemArray.GetValue(7).ToString();
                nombreSeccion = drv.Row.ItemArray.GetValue(8).ToString();
                racimos = Convert.ToDecimal(drv.Row.ItemArray.GetValue(6).ToString());
                cantidad = Convert.ToDecimal(drv.Row.ItemArray.GetValue(10).ToString());
                uMedidad = drv.Row.ItemArray.GetValue(9).ToString();
                jornales = Convert.ToDecimal(drv.Row.ItemArray.GetValue(13).ToString());


                if (this.Session["novedadTransporte"] == null)
                {
                    novedadTransporte = new CnovedadTransporte(novedad, nombreNovedad, lote, nombreLote, seccion, nombreSeccion,
                        racimos, cantidad, listaTerceros, Convert.ToInt32(drv.Row.ItemArray.GetValue(17)), uMedidad, pesoRacimo, fechaDetalle, jornales, precioLabor, listaNovedadesTransporte.Count);
                    listaNovedadesTransporte.Add(novedadTransporte);
                    this.Session["novedadTransporte"] = listaNovedadesTransporte;
                }
                else
                {
                    listaNovedadesTransporte = (List<CnovedadTransporte>)this.Session["novedadTransporte"];

                    novedadTransporte = new CnovedadTransporte(novedad, nombreNovedad, lote, nombreLote, seccion, nombreSeccion,
                     racimos, cantidad, listaTerceros, Convert.ToInt32(drv.Row.ItemArray.GetValue(17)), uMedidad, pesoRacimo, fechaDetalle, jornales, precioLabor, listaNovedadesTransporte.Count);
                    listaNovedadesTransporte.Add(novedadTransporte);
                    this.Session["novedadTransporte"] = listaNovedadesTransporte;
                    posicionNovedad++;
                }
                listaNovedadesTransporte = listaNovedadesTransporte.OrderBy(x => x.Registro).ToList();
                dlDetalleTransporte.DataSource = listaNovedadesTransporte;
                dlDetalleTransporte.DataBind();

                foreach (DataListItem d in dlDetalleTransporte.Items)
                {
                    foreach (CnovedadTransporte nt in listaNovedadesTransporte)
                    {
                        if (((Label)d.FindControl("lblRegistro")).Text.Trim() == nt.Registro.ToString())
                        {
                            ((GridView)d.FindControl("gvLotes")).DataSource = nt.Terceros;
                            ((GridView)d.FindControl("gvLotes")).DataBind();
                        }
                    }


                }
            }
            LiquidaTransaccioinTransporte(0, 0);
        }

        private void cargarTiqueteGuardado()
        {
            CcontrolesUsuario.HabilitarControles(upTiquete.Controls);
            CcontrolesUsuario.InhabilitarUsoControles(upTiquete.Controls);
            cargarExtractoras();
            cargarCombox();
            upTiquete.Visible = true;
            upPestana.Visible = true;
            lbFecha.Enabled = true;
            txtFecha.Enabled = true;
            txtRemision.Enabled = true;
            txtObservacion.Enabled = true;
            try
            {
                DataView dvTiqueteregistrado = transacciones.RetornaEncabezadoTransaccionTiquete(hfTipo.Value, hfNumero.Value, Convert.ToInt32(Session["empresa"]));

                foreach (DataRowView dvr in dvTiqueteregistrado)
                {
                    if (dvr.Row.ItemArray.GetValue(3).ToString() != "&nbsp;")
                    {
                        ddlExtractoraFiltro.SelectedValue = Server.HtmlDecode(dvr.Row.ItemArray.GetValue(3).ToString());
                    }
                    else
                        ddlExtractoraTiquete.SelectedValue = "";

                    if (dvr.Row.ItemArray.GetValue(4).ToString() != "&nbsp;")
                    {
                        ddlExtractoraTiquete.SelectedValue = Server.HtmlDecode(dvr.Row.ItemArray.GetValue(4).ToString());
                    }
                    else
                        ddlExtractoraTiquete.SelectedValue = "";

                    if (dvr.Row.ItemArray.GetValue(5).ToString() != "&nbsp;")
                        txtTiquete.Text = Server.HtmlDecode(dvr.Row.ItemArray.GetValue(5).ToString());
                    else
                        txtTiquete.Text = "";

                    if (dvr.Row.ItemArray.GetValue(15).ToString() != "&nbsp;")
                    {
                        niCalendarFechaTiqueteI.SelectedDate = Convert.ToDateTime(Server.HtmlDecode(dvr.Row.ItemArray.GetValue(15).ToString()));
                        txtFechaTiqueteI.Text = Server.HtmlDecode(dvr.Row.ItemArray.GetValue(15).ToString());
                    }
                    else
                        txtFechaTiqueteI.Text = "";

                    if (dvr.Row.ItemArray.GetValue(13).ToString() != "&nbsp;")
                        txtVehiculo.Text = Server.HtmlDecode(dvr.Row.ItemArray.GetValue(13).ToString());
                    else
                        txtVehiculo.Text = "";

                    if (dvr.Row.ItemArray.GetValue(14).ToString() != "&nbsp;")
                        txtRemolque.Text = Server.HtmlDecode(dvr.Row.ItemArray.GetValue(14).ToString());
                    else
                        txtRemolque.Text = "";

                    if (dvr.Row.ItemArray.GetValue(6).ToString() != "&nbsp;")
                        txvPbruto.Text = Server.HtmlDecode(Convert.ToDecimal(dvr.Row.ItemArray.GetValue(6).ToString()).ToString());
                    else
                        txvPbruto.Text = "0";

                    if (dvr.Row.ItemArray.GetValue(7).ToString() != "&nbsp;")
                        txvPtara.Text = Server.HtmlDecode(Convert.ToDecimal(dvr.Row.ItemArray.GetValue(7).ToString()).ToString());
                    else
                        txvPtara.Text = "0";

                    if (dvr.Row.ItemArray.GetValue(8).ToString() != "&nbsp;")
                        txvPneto.Text = Server.HtmlDecode(Convert.ToDecimal(dvr.Row.ItemArray.GetValue(8).ToString()).ToString());
                    else
                        txvPneto.Text = "0";

                    if (dvr.Row.ItemArray.GetValue(9).ToString() != "&nbsp;")
                        txvSacos.Text = Server.HtmlDecode(Convert.ToDecimal(dvr.Row.ItemArray.GetValue(9).ToString()).ToString());
                    else
                        txvSacos.Text = "0";

                    if (dvr.Row.ItemArray.GetValue(10).ToString() != "&nbsp;")
                        txvRacimosTiquete.Text = Server.HtmlDecode(Convert.ToDecimal(dvr.Row.ItemArray.GetValue(10).ToString()).ToString());
                    else
                        txvRacimosTiquete.Text = "0";

                    if (dvr.Row.ItemArray.GetValue(17).ToString() != "&nbsp;")
                    {
                        ddlFinca.SelectedValue = dvr.Row.ItemArray.GetValue(17).ToString();
                        cargarSesiones();
                        cargarLotes();
                    }

                    gvTiquetes.DataSource = null;
                    gvTiquetes.DataBind();
                    manejoRecolector();
                    manejoLaborCargue();
                    manejoLaborTransporte();
                    tabs.Visible = true;

                    if (txtFechaTiqueteI.Text.Trim().Length == 0)
                    {
                        selTerceroCosecha.Visible = false;
                        nilblInformacion.Text = "Debe seleccionar una fecha para continuar";
                        return;
                    }
                    else
                        selTerceroCosecha.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar los campos correspondiente a: " + ex.Message, "C");
            }
        }

        private void manejoEdicion()
        {
            this.Session["editar"] = true;
            ManejoEncabezado();
            LimpiarDDL();
            this.Session["novedadCosecha"] = null;
            this.Session["novedadTransporte"] = null;
            this.Session["novedadCargue"] = null;
            this.nilblInformacion.Text = "";
            this.nilblInformacionDetalle.Text = "";
            ddlSeccion.DataSource = null;
            ddlSeccion.DataBind();
            ddlLote.DataSource = null;
            ddlLote.DataBind();
            gvTiquetes.DataSource = null;
            gvTiquetes.DataBind();
            this.Session["numerotransaccion"] = null;
            this.nilblInformacion.ForeColor = Color.Red;
            txtFechaCosecha.Enabled = false;
            txtFechaTiqueteI.Enabled = false;
            niimbImprimir.Visible = false;
            upBascula.Visible = Convert.ToBoolean(TipoTransaccionConfig(25));
            rblBascula.Visible = Convert.ToBoolean(TipoTransaccionConfig(25));
            cargarExtractoras();
            ManejoRBLbascula();
            limpiarSubtotal();
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
        protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTiquetes.PageIndex = e.NewPageIndex;
            cargarTiquetes(txtFiltroBascula.Text);
            gvTiquetes.DataBind();
        }
        protected void txtFiltroBascula_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltroBascula.Text.Length > 0)
                cargarTiquetes(txtFiltroBascula.Text);
            ScriptManager1.SetFocus(txtFiltroBascula);

        }
        protected void lbRegistrar_Click1(object sender, ImageClickEventArgs e)
        {
            limpiarMensajes();
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {
                if (dlDetalleCosecha.Items.Count <= 0)
                {
                    nilblInformacion.Text = "El nivel de detalle de la transacción de cosecha debe tener por lo menos un registro";
                    return;
                }

                if (txtFecha.Text.Length == 0)
                {
                    nilblInformacion.Text = "Debe seleccionar o digitar fecha de la transacción";
                    return;
                }

                LiquidaTransaccioinCosecha(0, 0);
                LiquidaTransaccioinCargue(0, 0);
                LiquidaTransaccioinTransporte(0, 0);


                decimal subCantidad = 0, subRacimo = 0;
                int verificarCRJ = 0;

                foreach (DataListItem dl in dlDetalleCosecha.Items)
                {
                    subRacimo += Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text);
                    subCantidad += Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text);

                }

                if (!(Convert.ToDecimal(txvPneto.Text) == subCantidad))
                    verificarCRJ = 1;
                if (!(Convert.ToDecimal(txvRacimosTiquete.Text) == subRacimo))
                    verificarCRJ = 2;
                switch (verificarCRJ)
                {
                    case 1:
                        nilblInformacion.Text = "La cantidad no puede ser diferentes en el detalle de la transacción";
                        return;
                    case 2:
                        nilblInformacion.Text = "Los racimos no pueden ser diferentes en el detalle de la transacción";
                        return;
                    case 3:
                        nilblInformacion.Text = "Cantidad de jornales no pueden  ser diferentes en el detalle de la transacción";
                        return;
                }

                if (Convert.ToBoolean(this.Session["editar"]) == false)
                    if (!(Session["registroNC"] == null))
                    {
                        nilblInformacion.Text = "Debe completar el registro, de transporte y cargue o descarge.";
                        return;
                    }


                if (upBascula.Visible == true)
                {
                    if (Convert.ToInt32(rblBascula.SelectedValue) == 1)
                    {
                        if (this.ddlExtractoraFiltro.SelectedValue.Trim().Length == 0 || this.ddlExtractoraTiquete.SelectedValue.Trim().Length == 0 || this.txvPbruto.Text.Trim().Length == 0 ||
                         this.txtVehiculo.Text.Trim().Length == 0 || this.txvPtara.Text.Trim().Length == 0 || this.txvRacimosTiquete.Text.Trim().Length == 0 || this.txvSacos.Text.Trim().Length == 0 || this.txtFechaTiqueteI.Text.Trim().Length == 0)
                        {
                            nilblInformacion.Text = "Campos vacios en el tiquete por favor correjir";
                            return;
                        }
                    }
                    else
                    {
                        if (this.ddlExtractoraTiquete.SelectedValue.Trim().Length == 0 || this.txvPbruto.Text.Trim().Length == 0 || this.txtVehiculo.Text.Trim().Length == 0 ||
                         this.txvPtara.Text.Trim().Length == 0 || this.txvRacimosTiquete.Text.Trim().Length == 0 || this.txvSacos.Text.Trim().Length == 0 || this.txtFechaTiqueteI.Text.Trim().Length == 0)
                        {
                            nilblInformacion.Text = "Campos vacios en el tiquete por favor correjir";
                            return;
                        }
                    }
                }

                Guardar();
            }
        }
        protected void chkLaborCargue_CheckedChanged(object sender, EventArgs e)
        {
            manejoLaborCargue();
        }
        private void manejoLaborCargue()
        {
            try
            {
                ddlLaborCargue.Enabled = true;
                selTerceroCargue.Visible = true;
                lbFechaCargadores.Enabled = true;
                txtFechaCargue.Enabled = true;
                txtFechaCargue.Text = "";
                txvRacimoCargue.Text = "0";
                DataView dvLaborCargador = tipoTransaccion.SeleccionaNovedadTipoDocumentos(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Convert.ToInt32(Session["empresa"]));
                dvLaborCargador.RowFilter = "claseLabor=3 and empresa =" + Session["empresa"].ToString();
                this.ddlLaborCargue.DataSource = dvLaborCargador;
                this.ddlLaborCargue.DataValueField = "codigo";
                this.ddlLaborCargue.DataTextField = "descripcion";
                this.ddlLaborCargue.DataBind();
                this.ddlLaborCargue.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar Novedades. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.selTerceroCargue.DataSource = transacciones.SelccionaTercernoNovedad(Convert.ToInt32(Session["empresa"]));
                this.selTerceroCargue.DataValueField = "id";
                this.selTerceroCargue.DataTextField = "cadena";
                this.selTerceroCargue.DataBind();

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tercero de cargadores. Correspondiente a: " + ex.Message, "C");
            }

        }
        private void manejoLaborTransporte()
        {

            try
            {
                ddlLaborTransporte.Enabled = true;
                selTerceroTransporte.Visible = true;
                lbFechaTransporte.Enabled = true;
                txtFechaTransporte.Enabled = true;
                txtFechaTransporte.Text = "";
                txvRacimoTransporte.Text = "0";
                DataView dvLaborTransporte = tipoTransaccion.SeleccionaNovedadTipoDocumentos(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Convert.ToInt32(Session["empresa"]));
                dvLaborTransporte.RowFilter = "claseLabor=4 and empresa=" + Session["empresa"].ToString();
                this.ddlLaborTransporte.DataSource = dvLaborTransporte;
                this.ddlLaborTransporte.DataValueField = "codigo";
                this.ddlLaborTransporte.DataTextField = "descripcion";
                this.ddlLaborTransporte.DataBind();
                this.ddlLaborTransporte.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar Novedades. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                DataView conductor = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nfuncionario", "ppa"), "descripcion", Convert.ToInt32(Session["empresa"]));
                conductor.RowFilter = "conductor=1 and empresa =" + (Convert.ToInt32(Session["empresa"])).ToString();
                this.selTerceroTransporte.DataSource = conductor;
                this.selTerceroTransporte.DataValueField = "tercero";
                this.selTerceroTransporte.DataTextField = "cadena";
                this.selTerceroTransporte.DataBind();

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tercero de transporte. Correspondiente a: " + ex.Message, "C");
            }

        }
        protected void niCalendarFecha_SelectionChanged(object sender, EventArgs e)
        {
            this.niCalendarFecha.Visible = false;
            this.txtFecha.Visible = true;
            this.txtFecha.Text = this.niCalendarFecha.SelectedDate.ToShortDateString();
            verificaPeriodoCerrado(Convert.ToInt32(this.niCalendarFecha.SelectedDate.Year),
                   Convert.ToInt32(this.niCalendarFecha.SelectedDate.Month), Convert.ToInt32(Session["empresa"]), Convert.ToDateTime(txtFecha.Text));
            ddlFinca.Enabled = true;
        }
        private void verificaPeriodoCerrado(int año, int mes, int empresa, DateTime fecha)
        {
            if (periodo.RetornaPeriodoCerradoNomina(año, mes, empresa, fecha) == 1)
                ManejoError("Periodo cerrado de nomina. No es posible realizar nuevas transacciones", "I");

            if (periodo.RetornaPeriodoCerradoNomina(año, mes, empresa, fecha) == 2)
                ManejoError("Periodo de nomina no existe. No es posible realizar nuevas transacciones", "I");

        }
        protected void txtFecha_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFecha.Text);
            }
            catch
            {

                nilblInformacion.Text = "formato de fecha no valido..";
                txtFecha.Text = "";
                txtFecha.Focus();
                ScriptManager1.SetFocus(txtFecha);
                return;
            }

            verificaPeriodoCerrado(Convert.ToInt32(Convert.ToDateTime(txtFecha.Text).Year),
                   Convert.ToInt32(Convert.ToDateTime(txtFecha.Text).Month), Convert.ToInt32(Session["empresa"]), Convert.ToDateTime(txtFecha.Text));
            ddlFinca.Enabled = true;
            ScriptManager1.SetFocus(txtRemision.ClientID);
            txtRemision.Focus();
        }
        protected void lbFecha_Click2(object sender, EventArgs e)
        {
            this.niCalendarFecha.Visible = true;
            this.txtFecha.Visible = false;
            this.niCalendarFecha.SelectedDate = Convert.ToDateTime(null);
        }
        protected void gvTransaccion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTransaccion.PageIndex = e.NewPageIndex;
            BusquedaTransaccion();
            gvTransaccion.DataBind();
        }
        protected void txvJornalesD_TextChanged(object sender, EventArgs e)
        {
            LiquidaTransaccioinCosecha(0, 0);
            ddlLote.Enabled = true;
            ddlFinca.Enabled = true;
            ddlFinca.Enabled = true;
            ddlSeccion.Enabled = true;
        }
        protected void txvRacimoG_TextChanged(object sender, EventArgs e)
        {
            LiquidaTransaccioinCosecha(0, 0);
            ddlLote.Enabled = true;
            ddlFinca.Enabled = true;
            ddlFinca.Enabled = true;
            ddlSeccion.Enabled = true;
        }
        protected void calendarTransporte_SelectionChanged(object sender, EventArgs e)
        {
            string fecha = calendarTransporte.SelectedDate.ToShortDateString();

            if (upBascula.Visible == true)
            {
                if (txtFechaTiqueteI.Text.Trim().Length == 0)
                {
                    nilblinformacionTranportador.Text = "Debe seleccionar una fecha de tiquete para continuar";
                    return;
                }

                if ((Convert.ToDateTime(txtFechaTiqueteI.Text).Date < Convert.ToDateTime(fecha)))
                {
                    nilblinformacionTranportador.Text = "La fecha de la novedad no puede ser mayo a la del tiquete";
                    return;
                }
            }

            this.calendarTransporte.Visible = false;
            this.txtFechaTransporte.Visible = true;
            this.txtFechaTransporte.Text = this.calendarTransporte.SelectedDate.ToShortDateString();
        }
        protected void txtFechaTransporte_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFechaTransporte.Text);
                txvRacimoTransporte.Focus();

            }
            catch
            {
                nilblinformacionTranportador.Text = "Formato de fecha no valido";
                txtFechaTransporte.Text = "";
                txtFechaTransporte.Focus();
                return;
            }

            if (Convert.ToDateTime(txtFechaTransporte.Text) < Convert.ToDateTime(txtFechaCosecha.Text))
            {
                nilblinformacionTranportador.Text = "La fecha de cargue no puede ser menor a la cosecha";
                txtFechaTransporte.Text = "";
                txtFechaTransporte.Focus();
                return;
            }

            if (Convert.ToDateTime(txtFechaTransporte.Text) > Convert.ToDateTime(txtFechaTiqueteI.Text))
            {
                nilblinformacionTranportador.Text = "La fecha de cargue no puede ser mayor a la del tiquete";
                txtFechaTransporte.Text = "";
                txtFechaTransporte.Focus();
                return;
            }
        }
        protected void lbFechaTransporte_Click(object sender, EventArgs e)
        {
            this.calendarTransporte.Visible = true;
            this.txtFechaTransporte.Visible = false;
            this.calendarTransporte.SelectedDate = Convert.ToDateTime(null);
        }
        protected void imbCargarT_Click(object sender, ImageClickEventArgs e)
        {
            if (Convert.ToBoolean(Session["estadoCosecha"]) == true && Session["registroNC"] == null)
            {
                nilblinformacionTranportador.Text = "Debe seleccionar un lote antes de continuar";
                return;
            }

            limpiarMensajes();
            nilblinformacionTranportador.Visible = true;
            nilblinformacionTranportador.Text = "";
            nilblinformacionTranportador.ForeColor = Color.Red;


            if (txtFechaTransporte.Enabled)
            {
                if (txtFechaTransporte.Text.Length == 0)
                {
                    nilblinformacionTranportador.Text = "debe ingresar una fecha para continuar";
                    return;
                }
            }

            //if (Convert.ToDecimal(txvRacimoTransporte.Text) > Convert.ToDecimal(txvRacimosCosecha.Text))
            //{
            //    nilblinformacionTranportador.Text = "El número de racimos no puede ser mayor al de Cosecha";
            //    return;
            //}

            decimal cantidadTotal = 0, jornalTotal = 0, racimosTotal = 0, subRacimos = 0;
            int contador = 0;
            cantidadTotal = Convert.ToDecimal(txvPneto.Text);
            jornalTotal = Convert.ToDecimal(txvJornalesD.Text);
            racimosTotal = Convert.ToDecimal(txvRacimosTiquete.Text);


            foreach (DataListItem dl in dlDetalleTransporte.Items)
            {
                if (((Label)dl.FindControl("lblRegistroCosecha")).Text.Trim() == Session["registroNC"].ToString())
                    subRacimos += Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text);
            }


            if (Convert.ToDecimal(txvRacimoTransporte.Text) > Convert.ToDecimal(txvRacimosCosecha.Text))
            {
                this.nilblinformacionTranportador.Text = "Los racimos no deben ser mayor a los de la cosecha, por favor corrija";
                return;
            }

            contador = 0;
            if (this.ddlLaborTransporte.SelectedValue.Trim().Length == 0)
            {
                nilblInformacionDetalle.Text = "Seleccione una labor de transporte";
                return;
            }
            for (int x = 0; x < selTerceroTransporte.Items.Count; x++)
            {
                if (selTerceroTransporte.Items[x].Selected)
                    contador++;
            }
            if (contador == 0)
            {
                this.nilblinformacionTranportador.Text = "Debe selecionar un transportador para continuar";
                return;
            }

            cargarDLTransporte();
            LiquidaTransaccioinTransporte(0, 0);
            validarRegistros();
        }
        protected void cargarDLTransporte()
        {
            int posicionNovedad = 0;
            nilblInformacionDetalle.Text = "";
            decimal pesoRacimo = 0, cantidad = 0, precioLabor = 0;
            string novedad = null, nombreNovedad = null, uMedidad = null;

            novedad = ddlLaborTransporte.SelectedValue.Trim();
            DataView dvNovedad = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("aNovedad", "ppa"), "descripcion", Convert.ToInt32(Session["empresa"]));
            dvNovedad.RowFilter = "empresa=" + Session["empresa"].ToString() + " and codigo='" + novedad + "'";
            foreach (DataRowView registro in dvNovedad)
            {
                nombreNovedad = registro.Row.ItemArray.GetValue(2).ToString();
                uMedidad = registro.Row.ItemArray.GetValue(5).ToString();
                precioLabor = listaPrecios.SeleccionaPrecioNovedadAño(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaTransporte.Text).Year);
                if (listaPrecios.SeleccionaPrecioNovedadAño(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaTransporte.Text).Year) == 0)
                {
                    this.nilblinformacionTranportador.Text = "La labor seleccionada no tiene precio en el año, por favor registrar precio para continuar.";
                    return;
                }
            }

            if (this.Session["novedadTransporte"] == null)
            {
                for (int x = 0; x < selTerceroTransporte.Items.Count; x++)
                {
                    if (selTerceroTransporte.Items[x].Selected)
                    {
                        precioLabor = listaPrecios.SeleccionaPrecioNovedadAñoTercero(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaTransporte.Text).Year, Convert.ToInt32(selTerceroTransporte.Items[x].Value), Convert.ToDateTime(txtFechaTransporte.Text));
                        terceros = new CterceroTiquete(Convert.ToInt32(selTerceroTransporte.Items[x].Value), selTerceroTransporte.Items[x].Text, ddlLote.SelectedValue.ToString().Trim(), null, 0, 0, precioLabor, novedad);
                        listaTerceros.Add(terceros);
                    }
                }

                novedadTransporte = new CnovedadTransporte(novedad, nombreNovedad, ddlLote.SelectedValue.Trim(), ddlLote.SelectedItem.Text, ddlSeccion.SelectedValue.Trim(), ddlSeccion.SelectedItem.Text,
                     Convert.ToDecimal(txvRacimoTransporte.Text), cantidad, listaTerceros, dlDetalleTransporte.Items.Count, uMedidad, pesoRacimo, txtFechaTransporte.Text, 0, precioLabor, Convert.ToInt32(Session["registroNC"]));
                listaNovedadesTransporte.Add(novedadTransporte);
                this.Session["novedadTransporte"] = listaNovedadesTransporte;
            }
            else
            {
                listaNovedadesTransporte = (List<CnovedadTransporte>)this.Session["novedadTransporte"];
                int registrotransporte = listaNovedadesTransporte.Max(x => x.Registro);
                for (int x = 0; x < selTerceroTransporte.Items.Count; x++)
                {
                    if (selTerceroTransporte.Items[x].Selected)
                    {
                        precioLabor = listaPrecios.SeleccionaPrecioNovedadAñoTercero(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaTransporte.Text).Year, Convert.ToInt32(selTerceroTransporte.Items[x].Value), Convert.ToDateTime(txtFechaTransporte.Text));
                        terceros = new CterceroTiquete(Convert.ToInt32(selTerceroTransporte.Items[x].Value), selTerceroTransporte.Items[x].Text, ddlLote.SelectedValue.ToString().Trim(), null, 0, 0, precioLabor, novedad);
                        listaTerceros.Add(terceros);
                    }
                }


                novedadTransporte = new CnovedadTransporte(novedad, nombreNovedad, ddlLote.SelectedValue.Trim(), ddlLote.SelectedItem.Text, ddlSeccion.SelectedValue.Trim(), ddlSeccion.SelectedItem.Text,
                  Convert.ToDecimal(txvRacimoTransporte.Text), cantidad, listaTerceros, registrotransporte + 1, uMedidad, pesoRacimo, txtFechaTransporte.Text, 0, precioLabor, Convert.ToInt32(Session["registroNC"]));
                listaNovedadesTransporte.Add(novedadTransporte);
                this.Session["novedadTransporte"] = listaNovedadesTransporte;
                posicionNovedad++;
            }
            dlDetalleTransporte.DataSource = listaNovedadesTransporte;
            dlDetalleTransporte.DataBind();

            foreach (DataListItem d in dlDetalleTransporte.Items)
            {
                foreach (CnovedadTransporte nt in listaNovedadesTransporte)
                {
                    if (((Label)d.FindControl("lblRegistro")).Text.Trim() == nt.Registro.ToString())
                    {
                        ((GridView)d.FindControl("gvLotes")).DataSource = nt.Terceros;
                        ((GridView)d.FindControl("gvLotes")).DataBind();
                    }
                }


            }
        }
        private void LiquidaTransaccioinTransporte(decimal racimos, decimal cant)
        {
            try
            {
                decimal noRacimos = 0;
                decimal cantidad = 0, pPromedio = 0;
                decimal totalKilos = 0, cantidadP = 0;
                List<int> listacantidadesRepartidas = new List<int>();
                List<Decimal> difKilosLote = null;
                difKilosLote = new List<decimal>();
                decimal difTotalKg = 0;

                foreach (DataListItem dl in dlDetalleTransporte.Items)
                {
                    ((TextBox)dl.FindControl("txvCantidadG")).Enabled = false;
                    ((TextBox)dl.FindControl("txvRacimoG")).Enabled = false;
                    ((TextBox)dl.FindControl("txvJornalesD")).Enabled = false;
                }

                foreach (DataListItem dl in dlDetalleTransporte.Items)
                {
                    noRacimos += Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text);
                    cantidad += Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text);
                }

                if ((noRacimos + racimos) > Convert.ToDecimal(txvRacimosTiquete.Text))
                {
                    nilblInformacionDetalle.Text = "El número de racimos no puede ser mayor al tiquete";
                    return;
                }

                if (dlDetalleTransporte.Items.Count > 0)
                {
                    totalKilos = 0;
                    cantidadP = 0;

                    foreach (DataListItem dl in dlDetalleTransporte.Items)
                    {
                        if (((Label)dl.FindControl("lblLote")).Text.Trim().Length > 0)
                        {
                            pPromedio = ObtenerPesoPromedio(((Label)dl.FindControl("lblLote")).Text, Convert.ToDateTime(((Label)dl.FindControl("lblFechaD")).Text));
                            ((Label)dl.FindControl("lblPesoPromedio")).Text = Convert.ToString(pPromedio);
                            ((TextBox)dl.FindControl("txvCantidadG")).Text = Convert.ToString(Convert.ToInt64(Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text) * pPromedio));
                            cantidadP = Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text);
                            totalKilos += cantidadP;
                            ((Label)dl.FindControl("lblpRacimos")).Text = Convert.ToString(Decimal.Round((Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text) / Convert.ToDecimal(txvRacimosTiquete.Text)), 4));
                        }
                    }

                    if (Convert.ToDecimal(txvPneto.Text.Trim()) > 0)
                    {
                        difTotalKg = Convert.ToDecimal(Convert.ToDecimal(txvPneto.Text) - totalKilos);
                    }

                    foreach (DataListItem dl in dlDetalleTransporte.Items)
                    {
                        if (((Label)dl.FindControl("lblLote")).Text.Trim().Length > 0)
                        {
                            decimal ppLoteNovedad = Convert.ToDecimal(((Label)dl.FindControl("lblpRacimos")).Text);
                            decimal diferencia = Convert.ToDecimal(Decimal.Round((Convert.ToDecimal(difTotalKg) * ppLoteNovedad), 0));
                            ((Label)dl.FindControl("lblDifKilos")).Text = Convert.ToString(Decimal.Round(Convert.ToDecimal(difTotalKg * ppLoteNovedad), 0));
                            decimal cantidadm = Convert.ToDecimal(Convert.ToInt64(Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text) + Convert.ToDecimal(((Label)dl.FindControl("lblDifKilos")).Text)));
                            ((TextBox)dl.FindControl("txvCantidadG")).Text = Convert.ToString(Convert.ToInt64(Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text) + Convert.ToDecimal(((Label)dl.FindControl("lblDifKilos")).Text)));
                        }
                    }
                }

                repartirCantidadesTransporte();
            }
            catch (Exception ex)
            {
                nilblInformacionDetalle.Text = "Error al liquidar transaccion debido a: " + ex;
            }
        }
        private void repartirCantidadesTransporte()
        {

            decimal diferencia = 0, cantidadTotal = 0, cantidadTotalAsignada = 0, cantidadTercero = 0,
                cantidadJornales = 0, jornalesTotales = 0, jornalesAsignados = 0, diferenciaJornal, subCantidad = 0, subRacimos = 0, subjornales = 0;
            int noTerceros = 0, contarN = 0, cantidadDL = 0;

            listaNovedadesTransporte = (List<CnovedadTransporte>)this.Session["novedadTransporte"];

            foreach (DataListItem dl in dlDetalleTransporte.Items)
            {
                cantidadDL += Convert.ToInt32(Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text));
            }
            contarN = dlDetalleTransporte.Items.Count;
            diferencia = Convert.ToDecimal(txvPneto.Text) - cantidadDL;

            int ramdom = new Random().Next(1, contarN);
            int w = 0;
            foreach (DataListItem dl in dlDetalleTransporte.Items)
            {
                if (w == ramdom)
                    ((TextBox)dl.FindControl("txvCantidadG")).Text = (Convert.ToInt64(Convert.ToInt64(Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text)) + diferencia)).ToString();
                w++;
            }

            foreach (DataListItem dl in dlDetalleTransporte.Items)
            {
                cantidadTotal = Convert.ToInt32(Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text));
                cantidadTotalAsignada = 0;
                jornalesAsignados = 0;
                jornalesTotales = Convert.ToDecimal(((TextBox)dl.FindControl("txvJornalesD")).Text);
                noTerceros = 0;
                diferencia = 0;
                cantidadTercero = 0;

                subCantidad += cantidadTotal;
                subjornales += jornalesTotales;
                subRacimos += Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text);

                noTerceros = ((GridView)dl.FindControl("gvLotes")).Rows.Count;

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
            actualizarDatosTransporte();
            CalcularSubtotalTransporte();
        }
        private void CalcularSubtotalTransporte()
        {
            listaNovedadesTransporte = (List<CnovedadTransporte>)this.Session["novedadTransporte"];
            gvSubTotales.DataSource = null;
            gvSubTotales.DataBind();
            subtotal = new List<Csubtotales>();
            decimal subCantidad = 0, subRacimos = 0, subjornales = 0;
            Csubtotales sub;
            bool validarNovedad = false;
            int posicionSubtotal = 0;

            try
            {
                if (listaNovedadesTransporte != null)
                {
                    for (int x = 0; x < listaNovedadesTransporte.Count; x++)
                    {
                        subRacimos = 0;
                        subCantidad = 0;
                        subjornales = 0;
                        for (int z = 0; z < subtotal.Count; z++)
                        {
                            if (listaNovedadesTransporte[x].Codnovedad == subtotal[z].novedades)
                            {
                                validarNovedad = true;
                                posicionSubtotal = z;
                                break;
                            }
                        }

                        if (validarNovedad)
                        {
                            subCantidad += listaNovedadesTransporte[x].Cantidad;
                            subjornales += listaNovedadesTransporte[x].Jornal;
                            subRacimos += listaNovedadesTransporte[x].Racimos;
                            subtotal[posicionSubtotal].subCantidad = subtotal[posicionSubtotal].subCantidad + subCantidad;
                            subtotal[posicionSubtotal].subJornal = subtotal[posicionSubtotal].subJornal + subjornales;
                            subtotal[posicionSubtotal].subRacimo = subtotal[posicionSubtotal].subRacimo + subRacimos;
                            validarNovedad = false;
                        }
                        else
                        {
                            subCantidad += listaNovedadesTransporte[x].Cantidad;
                            subjornales += listaNovedadesTransporte[x].Jornal;
                            subRacimos += listaNovedadesTransporte[x].Racimos;
                            sub = new Csubtotales(listaNovedadesTransporte[x].Codnovedad, listaNovedadesTransporte[x].Desnovedad, subCantidad, subRacimos, subjornales);
                            subtotal.Add(sub);
                        }


                    }

                    gvSubTotalesT.DataSource = subtotal;
                    gvSubTotalesT.DataBind();
                    this.Session["subtotalTransporte"] = subtotal;

                }
            }
            catch (Exception ex)
            {
                nilblInformacionDetalle.Text = "Error al cargar subtotal debido a: " + ex.Message;
            }
        }
        private void actualizarDatosTransporte()
        {
            listaNovedadesTransporte = (List<CnovedadTransporte>)this.Session["novedadTransporte"];
            foreach (DataListItem dli in dlDetalleTransporte.Items)
            {
                for (int x = 0; x < listaNovedadesTransporte.Count; x++)
                {
                    if (((Label)dli.FindControl("lblRegistro")).Text == listaNovedadesTransporte[x].Registro.ToString())
                    {
                        listaNovedadesTransporte[x].Racimos = Convert.ToDecimal(((TextBox)dli.FindControl("txvRacimoG")).Text);
                        listaNovedadesTransporte[x].Jornal = Convert.ToDecimal(((TextBox)dli.FindControl("txvJornalesD")).Text);
                        listaNovedadesTransporte[x].Cantidad = Convert.ToDecimal(((TextBox)dli.FindControl("txvCantidadG")).Text);

                        foreach (GridViewRow gvr in ((GridView)dli.FindControl("gvLotes")).Rows)
                        {
                            for (int y = 0; y < listaNovedadesTransporte[x].Terceros.Count; y++)
                            {
                                if (listaNovedadesTransporte[x].Terceros[y].Codtercero == Convert.ToInt32(gvr.Cells[0].Text))
                                {
                                    listaNovedadesTransporte[x].Terceros[y].Cantidad = Convert.ToDecimal(((TextBox)gvr.FindControl("txtCantidad")).Text);
                                    listaNovedadesTransporte[x].Terceros[y].Jornal = Convert.ToDecimal(((TextBox)gvr.FindControl("txtJornal")).Text);
                                }
                            }
                        }

                    }
                }
            }
        }
        protected void imbLiquidarT_Click(object sender, ImageClickEventArgs e)
        {
            LiquidaTransaccioinTransporte(0, 0);
        }
        protected void imbLiquidarCargue_Click(object sender, ImageClickEventArgs e)
        {
            LiquidaTransaccioinCargue(0, 0);
        }
        protected void imbCargarCargadores_Click(object sender, ImageClickEventArgs e)
        {
            if (Convert.ToBoolean(Session["estadoCosecha"]) == true && Session["registroNC"] == null)
            {
                nilblInformacionCargadores.Text = "Debe seleccionar un lote antes de continuar";
                return;
            }

            limpiarMensajes();
            nilblInformacionCargadores.Visible = true;
            nilblInformacionCargadores.Text = "";
            nilblInformacionCargadores.ForeColor = Color.Red;


            if (txtFechaCargue.Enabled)
            {
                if (txtFechaCargue.Text.Length == 0)
                {
                    nilblInformacionCargadores.Text = "debe ingresar una fecha para continuar";
                    return;
                }
            }

            //if (Convert.ToDecimal(txvRacimoCargue.Text) > Convert.ToDecimal(txvRacimosCosecha.Text))
            //{
            //    nilblInformacionCargadores.Text = "El número de racimos no puede ser mayor al de Cosecha";
            //    return;
            //}

            decimal cantidadTotal = 0, jornalTotal = 0, racimosTotal = 0, subRacimos = 0;
            int contador = 0;
            cantidadTotal = Convert.ToDecimal(txvPneto.Text);
            jornalTotal = Convert.ToDecimal(txvJornalesD.Text);
            racimosTotal = Convert.ToDecimal(txvRacimosTiquete.Text);


            foreach (DataListItem dl in dlDetalleCargue.Items)
            {
                if (((Label)dl.FindControl("lblRegistroCosecha")).Text.Trim() == Session["registroNC"].ToString())
                    subRacimos += Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text);
            }


            //if (subRacimos + Convert.ToDecimal(txvRacimoCargue.Text) > Convert.ToDecimal(txvRacimosCosecha.Text))
            //{
            //    this.nilblInformacionCargadores.Text = "Los racimos no deben ser mayor a los de la cosecha, por favor corrija";
            //    return;
            //}

            contador = 0;
            if (this.ddlLaborCargue.SelectedValue.Trim().Length == 0)
            {
                nilblInformacionDetalle.Text = "Seleccione una labor de transporte";
                return;
            }
            for (int x = 0; x < selTerceroCargue.Items.Count; x++)
            {
                if (selTerceroCargue.Items[x].Selected)
                    contador++;
            }
            if (contador == 0)
            {
                this.nilblInformacionCargadores.Text = "Debe selecionar un transportador para continuar";
                return;
            }

            cargarDLCargue();
            LiquidaTransaccioinCargue(0, 0);
            validarRegistros();
        }
        protected void cargarDLCargue()
        {
            int posicionNovedad = 0;
            nilblInformacionDetalle.Text = "";
            decimal pesoRacimo = 0, cantidad = 0, precioLabor = 0;
            string novedad = null, nombreNovedad = null, uMedidad = null;

            novedad = ddlLaborCargue.SelectedValue.Trim();
            DataView dvNovedad = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("aNovedad", "ppa"), "descripcion", Convert.ToInt32(Session["empresa"]));
            dvNovedad.RowFilter = "empresa=" + Session["empresa"].ToString() + " and codigo='" + novedad + "'";
            foreach (DataRowView registro in dvNovedad)
            {
                nombreNovedad = registro.Row.ItemArray.GetValue(2).ToString();
                uMedidad = registro.Row.ItemArray.GetValue(5).ToString();
                precioLabor = listaPrecios.SeleccionaPrecioNovedadAño(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaCargue.Text).Year);
                if (listaPrecios.SeleccionaPrecioNovedadAño(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaCosecha.Text).Year) == 0)
                {
                    this.nilblInformacionCargadores.Text = "La labor seleccionada no tiene precio en el año, por favor registrar precio para continuar.";
                    return;
                }
            }

            if (this.Session["novedadCargue"] == null)
            {
                for (int x = 0; x < selTerceroCargue.Items.Count; x++)
                {
                    if (selTerceroCargue.Items[x].Selected)
                    {
                        precioLabor = listaPrecios.SeleccionaPrecioNovedadAñoTercero(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaCargue.Text).Year, Convert.ToInt32(selTerceroCargue.Items[x].Value), Convert.ToDateTime(txtFechaCargue.Text));
                        terceros = new CterceroTiquete(Convert.ToInt32(selTerceroCargue.Items[x].Value), selTerceroCargue.Items[x].Text, ddlLote.SelectedValue.ToString().Trim(), null, 0, 0, precioLabor, novedad);
                        listaTerceros.Add(terceros);
                    }
                }

                novedadCargue = new CnovedadCargue(novedad, nombreNovedad, ddlLote.SelectedValue.Trim(), ddlLote.SelectedItem.Text, ddlSeccion.SelectedValue.Trim(), ddlSeccion.SelectedItem.Text,
                     Convert.ToDecimal(txvRacimoCargue.Text), cantidad, listaTerceros, dlDetalleCargue.Items.Count, uMedidad, pesoRacimo, txtFechaCargue.Text, 0, precioLabor, Convert.ToInt32(Session["registroNC"]));
                listaNovedadesCargue.Add(novedadCargue);
                this.Session["novedadCargue"] = listaNovedadesCargue;
            }
            else
            {
                listaNovedadesCargue = (List<CnovedadCargue>)this.Session["novedadCargue"];
                int registrocargue = listaNovedadesCargue.Max(x => x.Registro);
                for (int x = 0; x < selTerceroCargue.Items.Count; x++)
                {
                    if (selTerceroCargue.Items[x].Selected)
                    {
                        precioLabor = listaPrecios.SeleccionaPrecioNovedadAñoTercero(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaCargue.Text).Year, Convert.ToInt32(selTerceroCargue.Items[x].Value), Convert.ToDateTime(txtFechaCargue.Text));
                        terceros = new CterceroTiquete(Convert.ToInt32(selTerceroCargue.Items[x].Value), selTerceroCargue.Items[x].Text, ddlLote.SelectedValue.ToString().Trim(), null, 0, 0, precioLabor, novedad);
                        listaTerceros.Add(terceros);
                    }
                }

                novedadCargue = new CnovedadCargue(novedad, nombreNovedad, ddlLote.SelectedValue.Trim(), ddlLote.SelectedItem.Text, ddlSeccion.SelectedValue.Trim(), ddlSeccion.SelectedItem.Text,
                  Convert.ToDecimal(txvRacimoCargue.Text), cantidad, listaTerceros, registrocargue + 1, uMedidad, pesoRacimo, txtFechaCargue.Text, 0, precioLabor, Convert.ToInt32(Session["registroNC"]));
                listaNovedadesCargue.Add(novedadCargue);
                this.Session["novedadCargue"] = listaNovedadesCargue;
                posicionNovedad++;
            }
            dlDetalleCargue.DataSource = listaNovedadesCargue;
            dlDetalleCargue.DataBind();

            foreach (DataListItem d in dlDetalleCargue.Items)
            {
                foreach (CnovedadCargue nt in listaNovedadesCargue)
                {
                    if (((Label)d.FindControl("lblRegistro")).Text.Trim() == nt.Registro.ToString())
                    {
                        ((GridView)d.FindControl("gvLotes")).DataSource = nt.Terceros;
                        ((GridView)d.FindControl("gvLotes")).DataBind();
                    }
                }


            }
        }
        private void LiquidaTransaccioinCargue(decimal racimos, decimal cant)
        {
            try
            {
                decimal noRacimos = 0;
                decimal cantidad = 0, pPromedio = 0;
                decimal totalKilos = 0, cantidadP = 0;
                List<int> listacantidadesRepartidas = new List<int>();
                List<Decimal> difKilosLote = null;
                difKilosLote = new List<decimal>();
                decimal difTotalKg = 0;

                foreach (DataListItem dl in dlDetalleCargue.Items)
                {
                    ((TextBox)dl.FindControl("txvCantidadG")).Enabled = false;
                    ((TextBox)dl.FindControl("txvRacimoG")).Enabled = false;
                    ((TextBox)dl.FindControl("txvJornalesD")).Enabled = false;
                }

                foreach (DataListItem dl in dlDetalleCargue.Items)
                {
                    noRacimos += Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text);
                    cantidad += Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text);
                }

                if ((noRacimos + racimos) > Convert.ToDecimal(txvRacimosTiquete.Text))
                {
                    nilblInformacionDetalle.Text = "El número de racimos no puede ser mayor al tiquete";
                    return;
                }

                if (dlDetalleCargue.Items.Count > 0)
                {
                    totalKilos = 0;
                    cantidadP = 0;

                    foreach (DataListItem dl in dlDetalleCargue.Items)
                    {
                        if (((Label)dl.FindControl("lblLote")).Text.Trim().Length > 0)
                        {
                            pPromedio = ObtenerPesoPromedio(((Label)dl.FindControl("lblLote")).Text, Convert.ToDateTime(((Label)dl.FindControl("lblFechaD")).Text));
                            ((Label)dl.FindControl("lblPesoPromedio")).Text = Convert.ToString(pPromedio);
                            ((TextBox)dl.FindControl("txvCantidadG")).Text = Convert.ToString(Convert.ToInt64(Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text) * pPromedio));
                            cantidadP = Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text);
                            totalKilos += cantidadP;
                            ((Label)dl.FindControl("lblpRacimos")).Text = Convert.ToString(Decimal.Round((Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text) / Convert.ToDecimal(txvRacimosTiquete.Text)), 4));
                        }
                    }

                    if (Convert.ToDecimal(txvPneto.Text.Trim()) > 0)
                    {
                        difTotalKg = Convert.ToDecimal(Convert.ToDecimal(txvPneto.Text) - totalKilos);
                    }

                    foreach (DataListItem dl in dlDetalleCargue.Items)
                    {
                        if (((Label)dl.FindControl("lblLote")).Text.Trim().Length > 0)
                        {
                            decimal ppLoteNovedad = Convert.ToDecimal(((Label)dl.FindControl("lblpRacimos")).Text);
                            decimal diferencia = Convert.ToDecimal(Decimal.Round((Convert.ToDecimal(difTotalKg) * ppLoteNovedad), 0));
                            ((Label)dl.FindControl("lblDifKilos")).Text = Convert.ToString(Decimal.Round(Convert.ToDecimal(difTotalKg * ppLoteNovedad), 0));
                            decimal cantidadm = Convert.ToDecimal(Convert.ToInt64(Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text) + Convert.ToDecimal(((Label)dl.FindControl("lblDifKilos")).Text)));
                            ((TextBox)dl.FindControl("txvCantidadG")).Text = Convert.ToString(Convert.ToInt64(Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text) + Convert.ToDecimal(((Label)dl.FindControl("lblDifKilos")).Text)));
                        }
                    }
                }

                repartirCantidadesCargue();
            }
            catch (Exception ex)
            {
                nilblInformacionDetalle.Text = "Error al liquidar transaccion debido a: " + ex;
            }
        }
        private void repartirCantidadesCargue()
        {

            decimal diferencia = 0, cantidadTotal = 0, cantidadTotalAsignada = 0, cantidadTercero = 0,
                cantidadJornales = 0, jornalesTotales = 0, jornalesAsignados = 0, diferenciaJornal, subCantidad = 0, subRacimos = 0, subjornales = 0;
            int noTerceros = 0, contarN = 0, cantidadDL = 0, ramdom = 0;

            listaNovedadesCargue = (List<CnovedadCargue>)this.Session["novedadCargue"];

            foreach (DataListItem dl in dlDetalleCargue.Items)
            {
                cantidadDL += Convert.ToInt32(Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text));
            }
            contarN = listaNovedadesCargue.Count;
            diferencia = Convert.ToDecimal(txvPneto.Text) - cantidadDL;
            if (contarN > 0)
                ramdom = new Random().Next(1, contarN);
            int w = 0;
            foreach (DataListItem dl in dlDetalleCargue.Items)
            {
                if (w == ramdom)
                    ((TextBox)dl.FindControl("txvCantidadG")).Text = (Convert.ToInt64(Convert.ToInt64(Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text)) + diferencia)).ToString();
                w++;
            }

            foreach (DataListItem dl in dlDetalleCargue.Items)
            {
                cantidadTotal = Convert.ToInt32(Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text));
                cantidadTotalAsignada = 0;
                jornalesAsignados = 0;
                jornalesTotales = Convert.ToDecimal(((TextBox)dl.FindControl("txvJornalesD")).Text);
                noTerceros = 0;
                diferencia = 0;
                cantidadTercero = 0;

                subCantidad += cantidadTotal;
                subjornales += jornalesTotales;
                subRacimos += Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text);

                noTerceros = ((GridView)dl.FindControl("gvLotes")).Rows.Count;

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
            actualizarDatosCargue();
            CalcularSubtotalCargue();
        }
        private void CalcularSubtotalCargue()
        {
            listaNovedadesCargue = (List<CnovedadCargue>)this.Session["novedadCargue"];
            gvSubTotales.DataSource = null;
            gvSubTotales.DataBind();
            subtotal = new List<Csubtotales>();
            decimal subCantidad = 0, subRacimos = 0, subjornales = 0;
            Csubtotales sub;
            bool validarNovedad = false;
            int posicionSubtotal = 0;

            try
            {
                if (listaNovedadesCargue != null)
                {
                    for (int x = 0; x < listaNovedadesCargue.Count; x++)
                    {
                        subRacimos = 0;
                        subCantidad = 0;
                        subjornales = 0;
                        for (int z = 0; z < subtotal.Count; z++)
                        {
                            if (listaNovedadesCargue[x].Codnovedad == subtotal[z].novedades)
                            {
                                validarNovedad = true;
                                posicionSubtotal = z;
                                break;
                            }
                        }

                        if (validarNovedad)
                        {
                            subCantidad += listaNovedadesCargue[x].Cantidad;
                            subjornales += listaNovedadesCargue[x].Jornal;
                            subRacimos += listaNovedadesCargue[x].Racimos;
                            subtotal[posicionSubtotal].subCantidad = subtotal[posicionSubtotal].subCantidad + subCantidad;
                            subtotal[posicionSubtotal].subJornal = subtotal[posicionSubtotal].subJornal + subjornales;
                            subtotal[posicionSubtotal].subRacimo = subtotal[posicionSubtotal].subRacimo + subRacimos;
                            validarNovedad = false;
                        }
                        else
                        {
                            subCantidad += listaNovedadesCargue[x].Cantidad;
                            subjornales += listaNovedadesCargue[x].Jornal;
                            subRacimos += listaNovedadesCargue[x].Racimos;
                            sub = new Csubtotales(listaNovedadesCargue[x].Codnovedad, listaNovedadesCargue[x].Desnovedad, subCantidad, subRacimos, subjornales);
                            subtotal.Add(sub);
                        }


                    }

                    gvSubTotalesCargue.DataSource = subtotal;
                    gvSubTotalesCargue.DataBind();
                    this.Session["subtotalCargue"] = subtotal;

                }
            }
            catch (Exception ex)
            {
                nilblInformacionDetalle.Text = "Error al cargar subtotal debido a: " + ex.Message;
            }
        }
        private void actualizarDatosCargue()
        {
            listaNovedadesCargue = (List<CnovedadCargue>)this.Session["novedadCargue"];
            foreach (DataListItem dli in dlDetalleCargue.Items)
            {
                for (int x = 0; x < listaNovedadesCargue.Count; x++)
                {
                    if (((Label)dli.FindControl("lblRegistro")).Text == listaNovedadesCargue[x].Registro.ToString())
                    {
                        listaNovedadesCargue[x].Racimos = Convert.ToDecimal(((TextBox)dli.FindControl("txvRacimoG")).Text);
                        listaNovedadesCargue[x].Jornal = Convert.ToDecimal(((TextBox)dli.FindControl("txvJornalesD")).Text);
                        listaNovedadesCargue[x].Cantidad = Convert.ToDecimal(((TextBox)dli.FindControl("txvCantidadG")).Text);

                        foreach (GridViewRow gvr in ((GridView)dli.FindControl("gvLotes")).Rows)
                        {
                            for (int y = 0; y < listaNovedadesCargue[x].Terceros.Count; y++)
                            {
                                if (listaNovedadesCargue[x].Terceros[y].Codtercero == Convert.ToInt32(gvr.Cells[0].Text))
                                {
                                    listaNovedadesCargue[x].Terceros[y].Cantidad = Convert.ToDecimal(((TextBox)gvr.FindControl("txtCantidad")).Text);
                                    listaNovedadesCargue[x].Terceros[y].Jornal = Convert.ToDecimal(((TextBox)gvr.FindControl("txtJornal")).Text);
                                }
                            }
                        }

                    }
                }
            }
        }
        protected void dlDetalleTransporte_DeleteCommand(object source, DataListCommandEventArgs e)
        {
            if (Session["registroNC"] == null)
            {
                nilblinformacionTranportador.Text = "No es posible eliminar el registro hay una asociacion con registro de cosecha, elimine todos los registros desde la cosecha.";
                return;
            }

            int posicionNovedad = 0;
            int registro = Convert.ToInt32(((Label)dlDetalleTransporte.Items[e.Item.ItemIndex].FindControl("lblRegistro")).Text.Trim());
            listaNovedadesTransporte = (List<CnovedadTransporte>)this.Session["novedadTransporte"];
            foreach (CnovedadTransporte nt in listaNovedadesTransporte)
            {
                if (registro == nt.Registro)
                    break;
                posicionNovedad++;
            }

            listaNovedadesTransporte.RemoveAt(posicionNovedad);
            //listaNovedadesTransporte = reasignarRegistrosTransporte(listaNovedadesTransporte);
            dlDetalleTransporte.DataSource = listaNovedadesTransporte;
            dlDetalleTransporte.DataBind();
            if (listaNovedadesTransporte.Count == 0)
                nilblInformacionDetalle.Text = "";

            decimal cantidadTotal = 0, jornalesTotales = 0, subCantidad = 0, subJornales = 0, subRacimos = 0;

            foreach (DataListItem d in dlDetalleTransporte.Items)
            {
                cantidadTotal = Convert.ToDecimal(((TextBox)d.FindControl("txvCantidadG")).Text);
                jornalesTotales = Convert.ToDecimal(((TextBox)d.FindControl("txvJornalesD")).Text);
                subCantidad += cantidadTotal;
                subJornales += jornalesTotales;
                subRacimos += Convert.ToDecimal(((TextBox)d.FindControl("txvRacimoG")).Text);

                foreach (CnovedadTransporte nt in listaNovedadesTransporte)
                {
                    if (((Label)d.FindControl("lblRegistro")).Text.Trim() == nt.Registro.ToString())
                    {
                        ((GridView)d.FindControl("gvLotes")).DataSource = nt.Terceros;
                        ((GridView)d.FindControl("gvLotes")).DataBind();
                    }
                }
            }
            this.Session["novedadTransporte"] = listaNovedadesTransporte;
            LiquidaTransaccioinTransporte(0, 0);
            validarRegistros();
        }
        protected void dlDetalleCargue_DeleteCommand(object source, DataListCommandEventArgs e)
        {
            int posicionNovedad = 0;
            int registro = Convert.ToInt32(((Label)dlDetalleCargue.Items[e.Item.ItemIndex].FindControl("lblRegistro")).Text.Trim());
            listaNovedadesCargue = (List<CnovedadCargue>)this.Session["novedadCargue"];
            foreach (CnovedadCargue nt in listaNovedadesCargue)
            {
                if (registro == nt.Registro)
                    break;
                posicionNovedad++;
            }

            listaNovedadesCargue.RemoveAt(posicionNovedad);
            //listaNovedadesCargue = reasignarRegistrosCargue(listaNovedadesCargue);
            dlDetalleCargue.DataSource = listaNovedadesCargue;
            dlDetalleCargue.DataBind();
            if (listaNovedadesCargue.Count == 0)
                nilblInformacionDetalle.Text = "";

            decimal cantidadTotal = 0, jornalesTotales = 0, subCantidad = 0, subJornales = 0, subRacimos = 0;

            foreach (DataListItem d in dlDetalleCargue.Items)
            {
                cantidadTotal = Convert.ToDecimal(((TextBox)d.FindControl("txvCantidadG")).Text);
                jornalesTotales = Convert.ToDecimal(((TextBox)d.FindControl("txvJornalesD")).Text);
                subCantidad += cantidadTotal;
                subJornales += jornalesTotales;
                subRacimos += Convert.ToDecimal(((TextBox)d.FindControl("txvRacimoG")).Text);

                foreach (CnovedadCargue nt in listaNovedadesCargue)
                {
                    if (((Label)d.FindControl("lblRegistro")).Text.Trim() == nt.Registro.ToString())
                    {
                        ((GridView)d.FindControl("gvLotes")).DataSource = nt.Terceros;
                        ((GridView)d.FindControl("gvLotes")).DataBind();
                    }
                }
            }
            this.Session["novedadCargue"] = listaNovedadesCargue;
            LiquidaTransaccioinCargue(0, 0);
            validarRegistros();
        }
        protected void lbCancelarT_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ddlLaborTransporte.Enabled = true;
                selTerceroTransporte.Visible = true;
                lbFechaTransporte.Enabled = true;
                txtFechaTransporte.Enabled = true;
                DataView dvLaborTransporte = tipoTransaccion.SeleccionaNovedadTipoDocumentos(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Convert.ToInt32(Session["empresa"]));
                dvLaborTransporte.RowFilter = "claseLabor=4 and empresa=" + Session["empresa"].ToString();
                this.ddlLaborTransporte.DataSource = dvLaborTransporte;
                this.ddlLaborTransporte.DataValueField = "codigo";
                this.ddlLaborTransporte.DataTextField = "descripcion";
                this.ddlLaborTransporte.DataBind();
                this.ddlLaborTransporte.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar Novedades. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                DataView conductor = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nfuncionario", "ppa"), "descripcion", Convert.ToInt32(Session["empresa"]));
                conductor.RowFilter = "conductor=1 and empresa =" + (Convert.ToInt32(Session["empresa"])).ToString();
                this.selTerceroTransporte.DataSource = conductor;
                this.selTerceroTransporte.DataValueField = "tercero";
                this.selTerceroTransporte.DataTextField = "cadena";
                this.selTerceroTransporte.DataBind();

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tercero de transporte. Correspondiente a: " + ex.Message, "C");
            }

            txtFechaTransporte.Visible = true;
            txtFechaTransporte.Text = "";
            txvRacimoTransporte.Text = "0";
            calendarTransporte.Visible = false;
        }
        protected void lbCancelarDCC_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ddlLaborCargue.Enabled = true;
                selTerceroCargue.Visible = true;
                lbFechaCargadores.Enabled = true;
                txtFechaCargue.Enabled = true;
                DataView dvLaborCargue = tipoTransaccion.SeleccionaNovedadTipoDocumentos(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Convert.ToInt32(Session["empresa"]));
                dvLaborCargue.RowFilter = "claseLabor=3 and empresa=" + Session["empresa"].ToString();
                this.ddlLaborCargue.DataSource = dvLaborCargue;
                this.ddlLaborCargue.DataValueField = "codigo";
                this.ddlLaborCargue.DataTextField = "descripcion";
                this.ddlLaborCargue.DataBind();
                this.ddlLaborCargue.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar Novedades. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                DataView conductor = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nfuncionario", "ppa"), "descripcion", Convert.ToInt32(Session["empresa"]));
                conductor.RowFilter = "conductor=1 and empresa =" + (Convert.ToInt32(Session["empresa"])).ToString();
                this.selTerceroCargue.DataSource = conductor;
                this.selTerceroCargue.DataValueField = "tercero";
                this.selTerceroCargue.DataTextField = "cadena";
                this.selTerceroCargue.DataBind();

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tercero de cargadores. Correspondiente a: " + ex.Message, "C");
            }

            txtFechaCargue.Visible = true;
            txtFechaCargue.Text = "";
            txvRacimoCargue.Text = "0";
            calendarCargadores.Visible = false;
        }

        #endregion eventos





    }
}