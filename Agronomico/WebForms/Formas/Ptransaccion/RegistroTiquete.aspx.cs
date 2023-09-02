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
    public partial class RegistroTiquete : BasePage
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
        List<Csubtotales> subtotal = new List<Csubtotales>();
        Coperadores operadores = new Coperadores();
        Cperiodos periodo = new Cperiodos();
        CpromedioPeso peso = new CpromedioPeso();
        Cgeneral general = new Cgeneral();
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
            bool verifica = false;
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {

                    string referencia = null;
                    string tipo = ConfigurationManager.AppSettings["RegistroBascula"].ToString();

                    if (Convert.ToBoolean(this.Session["editar"]) == true)
                    {
                        operacion = "actualiza";
                        numerotransaccion = Session["numeroEditar"].ToString();
                        this.Session["numerotransaccion"] = numerotransaccion;

                        object[] objValoDeleteNovedad = new object[] { Convert.ToInt16(this.Session["empresa"]), numerotransaccion, tipo };
                        switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionNovedad", "elimina", "ppa", objValoDeleteNovedad))
                        {
                            case 1:
                                ManejoError("Error al eliminar la novedad registrada", "E");
                                verifica = true;
                                break;
                        }

                        object[] objValoDeleteTerceroNovedad = new object[] { Convert.ToInt16(this.Session["empresa"]), numerotransaccion, tipo };
                        switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionTercero", "elimina", "ppa", objValoDeleteTerceroNovedad))
                        {
                            case 1:
                                ManejoError("Error al eliminar los terceros de la novedad registrada", "E");
                                verifica = true;
                                break;
                        }

                        object[] objValoDeleteTerceroBascula = new object[] { Convert.ToInt16(this.Session["empresa"]), numerotransaccion, tipo };
                        switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionBascula", "elimina", "ppa", objValoDeleteTerceroBascula))
                        {
                            case 1:
                                ManejoError("Error al eliminar registro de bascula", "E");
                                verifica = true;
                                break;
                        }


                    }
                    else
                    {
                        numerotransaccion = transacciones.RetornaNumeroTransaccion(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Convert.ToInt16(this.Session["empresa"]));
                        this.Session["numerotransaccion"] = numerotransaccion;
                    }

                    if (upBascula.Visible == true)
                    {
                        if (rblBascula.SelectedValue == "1")
                            interno = true;

                        DateTime fecha = Convert.ToDateTime(txtFechaTiqueteI.Text);
                        DateTime fechaF = Convert.ToDateTime(txtFechaTiqueteI.Text);

                        object[] objValo = new object[]{ false, // @anulado	bit
                                                     Convert.ToUInt32(fecha.Year), //@año	int
                                                     Convert.ToInt32(this.Session["empresa"]),   //@empresa	int
                                                     fecha,   //@fecha	date
                                                     fecha,  //@fechaAnulado	datetime
                                                     fechaF,  //@fechaFinal	date
                                                     DateTime.Now,   //@fechaRegistro	datetime
                                                     Convert.ToUInt32(fecha.Month),  //@mes	int
                                                     numerotransaccion,   //@numero	varchar
                                                     txtObservacion.Text,   //@observacion	varchar
                                                     referencia,   //@referencia	varchar
                                                     txtRemision.Text,   //@remision	varchar
                                                     ConfigurationManager.AppSettings["RegistroBascula"].ToString(),   //@tipo	varchar
                                                     null,   //@usuarioAnulado	varchar
                                                     this.Session["usuario"].ToString()   //@usuarioRegistro	varchar
                              };

                        switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccion", operacion, "ppa", objValo))
                        {
                            case 0:
                                string extractora = ddlExtractoraTiquete.SelectedValue.Trim();
                                if (Convert.ToInt32(rblBascula.SelectedValue) == 1)
                                    extractora = ddlExtractoraFiltro.SelectedValue.Trim();

                                object[] objValores = new object[]{
                                     null,    //@codigoConductor
                                     Convert.ToInt16(this.Session["empresa"]),    //@empresa
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

                                switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionBascula", "inserta", "ppa", objValores))
                                {
                                    case 0:
                                        DateTime fechaDetalle = new DateTime();
                                        decimal cantidad = 0, jornales = 0, racimos = 0, pesoRacimo = 0, precioLabor = 0, sacos = 0;
                                        string lote = "", novedad = "", seccion = "", umedida = "", finca = "";
                                        GridView gvTerceros = new GridView();
                                        int cont = 1;
                                        foreach (DataListItem dl in dlDetalle.Items)
                                        {
                                            if (((Label)dl.FindControl("lblFechaD")) != null)
                                                fechaDetalle = Convert.ToDateTime(((Label)dl.FindControl("lblFechaD")).Text);

                                            if (((TextBox)dl.FindControl("txvCantidadG")) != null)
                                                cantidad = Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text);

                                            if (((TextBox)dl.FindControl("txvJornalesD")) != null)
                                                jornales = Convert.ToDecimal(((TextBox)dl.FindControl("txvJornalesD")).Text);
                                            if (((TextBox)dl.FindControl("txvSacosG")) != null)
                                                sacos = Convert.ToDecimal(((TextBox)dl.FindControl("txvSacosG")).Text);

                                            if (((Label)dl.FindControl("lblLote")) != null)
                                                lote = ((Label)dl.FindControl("lblLote")).Text;

                                            if (((Label)dl.FindControl("lblPesoPromedio")) != null)
                                                pesoRacimo = Convert.ToDecimal(((Label)dl.FindControl("lblPesoPromedio")).Text);

                                            if (((Label)dl.FindControl("lblNovedad")) != null)
                                                novedad = ((Label)dl.FindControl("lblNovedad")).Text;

                                            if (((Label)dl.FindControl("lblFinca")) != null)
                                                finca = ((Label)dl.FindControl("lblFinca")).Text;

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

                                            object[] objValoresNovedad = new object[]{
                                                Convert.ToInt32( fecha.Year.ToString()),   //@año
                                                cantidad, //@cantidad
                                                false,   //@ejecutado
                                                Convert.ToInt16(this.Session["empresa"]),     //@empresa
                                                fechaDetalle,    //@fecha
                                                finca,
                                                jornales,  //@jornales
                                                lote,    //@lote
                                                Convert.ToInt32( fecha.Month.ToString()) ,  //@mes
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
                                                ConfigurationManager.AppSettings["RegistroBascula"].ToString(),    //@tipo
                                                umedida //@uMedida
                                             };

                                            switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionNovedad", "inserta", "ppa", objValoresNovedad))
                                            {
                                                case 0:

                                                    decimal cantidadT = 0, jornalT = 0;
                                                    string cuadrilla = null;
                                                    if (((GridView)dl.FindControl("gvLotes")) != null)

                                                        foreach (GridViewRow gv in ((GridView)dl.FindControl("gvLotes")).Rows)
                                                        {

                                                            if (((TextBox)gv.FindControl("txtCantidad")) != null)
                                                                cantidadT = Convert.ToDecimal(((TextBox)gv.FindControl("txtCantidad")).Text);

                                                            if (((TextBox)gv.FindControl("txtJornal")) != null)
                                                                jornalT = Convert.ToDecimal(((TextBox)gv.FindControl("txtJornal")).Text);

                                                            if (!(gv.Cells[3].Text.Trim().Length == 0) & gv.Cells[3].Text.Trim() != "&nbsp;")
                                                                cuadrilla = gv.Cells[3].Text.Trim();

                                                            if (!(gv.Cells[5].Text.Trim().Length == 0) & gv.Cells[5].Text.Trim() != "&nbsp;")
                                                                precioLabor = listaPrecios.SeleccionaPrecioNovedadAñoTerceroFincaLote(Convert.ToInt32(Session["empresa"]), novedad,
                                                            fechaDetalle.Year, Convert.ToInt32(gv.Cells[1].Text.Trim()), fechaDetalle, finca, lote, seccion);


                                                            if (cantidadT != 0)
                                                            {
                                                                object[] objValoresTercero = new object[]{
                                                                      Convert.ToInt32( fecha.Year.ToString()),  //@año
                                                                       cantidadT, //@cantidad
                                                                       false, //@ejecutado
                                                                       Convert.ToInt16(this.Session["empresa"]), //@empresa
                                                                       fechaDetalle,
                                                                       finca,
                                                                        jornalT, //@jornales
                                                                        lote,//@lote
                                                                        Convert.ToInt32( fecha.Month.ToString()),//@mes
                                                                        novedad,//@novedad
                                                                        numerotransaccion, //@numero
                                                                        precioLabor,
                                                                        racimos,
                                                                        gv.RowIndex,//@registro
                                                                        dl.ItemIndex, //@registro novedad
                                                                        cantidadT,//@saldo
                                                                        seccion,//@seccion
                                                                        gv.Cells[1].Text.Trim(),//@tercero
                                                                        ConfigurationManager.AppSettings["RegistroBascula"].ToString(),    //@tipo
                                                                        cuadrilla//@cuadrilla
                                                                 };

                                                                switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionTercero", "inserta", "ppa", objValoresTercero))
                                                                {
                                                                    case 1:
                                                                        ManejoError("Error al insertar el detalle de la transacción", "I");
                                                                        verifica = true;
                                                                        break;
                                                                }
                                                            }
                                                        }
                                                    break;
                                                case 1:
                                                    ManejoError("Error al insertar el detalle de la transacción", "I");
                                                    verifica = true;
                                                    break;
                                            }

                                            cont += 1;
                                            if (chkLaborCargue.Checked)
                                            {
                                                DateTime fechaCargue = new DateTime();
                                                fechaCargue = Convert.ToDateTime(txtFechaCargadores.Text);
                                                precioLabor = listaPrecios.SeleccionaPrecioNovedadAño(Convert.ToInt32(Session["empresa"]), ddlLaborCargadores.SelectedValue, Convert.ToInt32(fechaCargue.Year.ToString()));

                                                object[] objValoresCargue = new object[]{
                                                        Convert.ToInt32( fecha.Year.ToString()),   //@año
                                                        cantidad, //@cantidad
                                                        false,   //@ejecutado
                                                        Convert.ToInt16(this.Session["empresa"]),     //@empresa
                                                          fechaCargue,
                                                         finca,
                                                        1,  //@jornales
                                                        lote,    //@lote
                                                        Convert.ToInt32( fecha.Month.ToString()) ,  //@mes
                                                        ddlLaborCargadores.SelectedValue,    //@novedad
                                                        numerotransaccion,    //@numero
                                                        pesoRacimo, //@pesoRacimo
                                                        precioLabor,
                                                        racimos,    //@racimos
                                                        dlDetalle.Items.Count + cont,   //@registro
                                                        0,
                                                        0,
                                                        cantidad,    //@saldo
                                                        seccion,   //@seccion
                                                        ConfigurationManager.AppSettings["RegistroBascula"].ToString(),    //@tipo
                                                        umedida //@uMedida
                                                            };


                                                switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionNovedad", "inserta", "ppa", objValoresCargue))
                                                {
                                                    case 0:

                                                        int contTercero = 0;
                                                        string cuadrilla = null;
                                                        DataView dvTerceroCuadrilla = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("nCuadrillaFuncionario", "ppa"), "funcionario", Convert.ToInt16(this.Session["empresa"]));
                                                        dvTerceroCuadrilla.RowFilter = "cuadrilla='" + ddlCuadrillaCargue.SelectedValue.ToString() + "' and empresa=" + (Convert.ToInt16(this.Session["empresa"])).ToString();

                                                        int r = 0;
                                                        for (int x = 0; x < selTerceroCargue.Items.Count; x++)
                                                        {
                                                            if (selTerceroCargue.Items[x].Selected)
                                                                contTercero++;
                                                        }
                                                        contTercero = contTercero + dvTerceroCuadrilla.Count;
                                                        decimal cantidadT = cantidad / contTercero, jornalT = 0;
                                                        cuadrilla = ddlCuadrillaCargue.SelectedValue;

                                                        if (dvTerceroCuadrilla.Count > 0)
                                                        {
                                                            foreach (DataRowView registro in dvTerceroCuadrilla)
                                                            {
                                                                precioLabor = listaPrecios.SeleccionaPrecioNovedadAñoTerceroFincaLote(Convert.ToInt32(Session["empresa"]), ddlLaborCargadores.SelectedValue, fechaCargue.Year,
                                                                                       Convert.ToInt32(registro.Row.ItemArray.GetValue(2).ToString()), fechaCargue, finca, lote, seccion);

                                                                object[] objValores2 = new object[]{
                                                                    Convert.ToInt32( fechaCargue.Year.ToString()),  //@año
                                                                    cantidadT, //@cantidad
                                                                    false, //@ejecutado
                                                                    Convert.ToInt16(this.Session["empresa"]), //@empresa
                                                                    fechaCargue,
                                                                    finca,
                                                                    jornalT, //@jornales
                                                                    lote,//@lote
                                                                    Convert.ToInt32( fechaCargue.Month.ToString()),//@mes
                                                                    ddlLaborCargadores.SelectedValue,//@novedad
                                                                    numerotransaccion, //@numero
                                                                    precioLabor,
                                                                    racimos,
                                                                    r,//@registro
                                                                    dlDetalle.Items.Count + cont,  //@registro novedad
                                                                    cantidadT,//@saldo
                                                                    seccion,//@seccion
                                                                   Convert.ToString(registro.Row.ItemArray.GetValue(2).ToString()),//@tercero
                                                                    ConfigurationManager.AppSettings["RegistroBascula"].ToString(),    //@tipo
                                                                    cuadrilla//@cuadrilla
                                                                    };

                                                                switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionTercero", "inserta", "ppa", objValores2))
                                                                {
                                                                    case 1:
                                                                        ManejoError("Error al insertar el detalle de la transacción", "I");
                                                                        verifica = true;
                                                                        break;
                                                                }
                                                                r++;
                                                            }
                                                        }
                                                        cuadrilla = null;
                                                        for (int x = 0; x < selTerceroCargue.Items.Count; x++)
                                                        {
                                                            if (selTerceroCargue.Items[x].Selected)
                                                            {
                                                                precioLabor = listaPrecios.SeleccionaPrecioNovedadAñoTerceroFincaLote(Convert.ToInt32(Session["empresa"]), ddlLaborCargadores.SelectedValue, fechaCargue.Year,
                                                                                       Convert.ToInt32(selTerceroCargue.Items[x].Value), fechaCargue, finca, lote, seccion);

                                                                object[] objValores2 = new object[]{
                                                                    Convert.ToInt32( fechaCargue.Year.ToString()),  //@año
                                                                    cantidadT, //@cantidad
                                                                    false, //@ejecutado
                                                                    Convert.ToInt16(this.Session["empresa"]), //@empresa
                                                                    fechaCargue,
                                                                    finca,
                                                                    jornalT, //@jornales
                                                                    lote,//@lote
                                                                    Convert.ToInt32( fechaCargue.Month.ToString()),//@mes
                                                                    ddlLaborCargadores.SelectedValue,//@novedad
                                                                    numerotransaccion, //@numero
                                                                    precioLabor,
                                                                    racimos,
                                                                    r,//@registro
                                                                    dlDetalle.Items.Count + cont,  //@registro novedad
                                                                    cantidadT,//@saldo
                                                                    seccion,//@seccion
                                                                   Convert.ToString(selTerceroCargue.Items[x].Value),//@tercero
                                                                    ConfigurationManager.AppSettings["RegistroBascula"].ToString(),    //@tipo
                                                                    cuadrilla//@cuadrilla
                                                                    };

                                                                switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionTercero", "inserta", "ppa", objValores2))
                                                                {
                                                                    case 1:
                                                                        ManejoError("Error al insertar el detalle de la transacción", "I");
                                                                        verifica = true;
                                                                        break;
                                                                }
                                                                r++;
                                                            }
                                                        }

                                                        break;
                                                    case 1:
                                                        ManejoError("Error al insertar el detalle de la transacción", "I");
                                                        verifica = true;
                                                        break;
                                                }
                                            }
                                            cont += 1;
                                            if (chkLaborTransporte.Checked)
                                            {
                                                DateTime fechaTransporte = new DateTime();
                                                fechaTransporte = Convert.ToDateTime(txtFechaTransporte.Text);

                                                //racimos = Convert.ToDecimal(txvRacimosTiquete.Text);
                                                precioLabor = listaPrecios.SeleccionaPrecioNovedadAño(Convert.ToInt32(Session["empresa"]), ddlLaborTransporte.SelectedValue, Convert.ToInt32(fechaTransporte.Year.ToString()));

                                                object[] objValoresTransporte = new object[]{
                                                Convert.ToInt32( fechaTransporte.Year.ToString()),   //@año
                                                cantidad, //@cantidad
                                                false,   //@ejecutado
                                                Convert.ToInt16(this.Session["empresa"]),     //@empresa
                                                fechaTransporte,    //@fecha
                                                finca,
                                                1,  //@jornales
                                                lote,    //@lote
                                                Convert.ToInt32( fechaTransporte.Month.ToString()) ,  //@mes
                                                ddlLaborTransporte.SelectedValue,    //@novedad
                                                numerotransaccion,    //@numero
                                                0, //@pesoRacimo
                                                precioLabor,
                                                racimos,    //@racimos
                                                dlDetalle.Items.Count + cont,   //@registro
                                                0,
                                                0,
                                                cantidad,    //@saldo
                                                seccion,   //@seccion
                                                ConfigurationManager.AppSettings["RegistroBascula"].ToString(),    //@tipo
                                                umedida //@uMedida
                                             };

                                                switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionNovedad", "inserta", "ppa", objValoresTransporte))
                                                {
                                                    case 0:

                                                        int contTercero = 0;

                                                        string cuadrilla = null;
                                                        DataView dvTerceroCuadrillaTransporte = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("nCuadrillaFuncionario", "ppa"), "funcionario", Convert.ToInt16(this.Session["empresa"]));
                                                        dvTerceroCuadrillaTransporte.RowFilter = "cuadrilla='" + ddlCuadrillaTransporte.SelectedValue.ToString() + "' and empresa=" + (Convert.ToInt16(this.Session["empresa"])).ToString();

                                                        for (int x = 0; x < selTerceroTransporte.Items.Count; x++)
                                                        {
                                                            if (selTerceroTransporte.Items[x].Selected)
                                                                contTercero++;
                                                        }

                                                        contTercero = contTercero + dvTerceroCuadrillaTransporte.Count;
                                                        decimal cantidadT = cantidad / contTercero, jornalT = 0;
                                                        cuadrilla = ddlCuadrillaTransporte.SelectedValue;

                                                        int r = 0;

                                                        if (dvTerceroCuadrillaTransporte.Count > 0)
                                                        {

                                                            foreach (DataRowView registro in dvTerceroCuadrillaTransporte)
                                                            {
                                                                precioLabor = listaPrecios.SeleccionaPrecioNovedadAñoTerceroFincaLote(Convert.ToInt32(Session["empresa"]), ddlLaborTransporte.SelectedValue, fechaTransporte.Year,
                                                                                      Convert.ToInt32(registro.Row.ItemArray.GetValue(2).ToString()), fechaTransporte, finca, lote, seccion);

                                                                object[] objValores2 = new object[]{
                                                                    Convert.ToInt32( fechaTransporte.Year.ToString()),  //@año
                                                                    cantidadT, //@cantidad
                                                                    false, //@ejecutado
                                                                    Convert.ToInt16(this.Session["empresa"]), //@empresa
                                                                    fechaTransporte,
                                                                    finca,
                                                                    jornalT, //@jornales
                                                                    lote,//@lote
                                                                    Convert.ToInt32( fechaTransporte.Month.ToString()),//@mes
                                                                    ddlLaborTransporte.SelectedValue,//@novedad
                                                                    numerotransaccion, //@numero
                                                                    precioLabor,
                                                                    racimos,
                                                                    r,//@registro
                                                                    dlDetalle.Items.Count + cont,  //@registro novedad
                                                                    cantidadT,//@saldo
                                                                    seccion,//@seccion
                                                                   Convert.ToString(registro.Row.ItemArray.GetValue(2).ToString()),//@tercero
                                                                    ConfigurationManager.AppSettings["RegistroBascula"].ToString(),    //@tipo
                                                                    cuadrilla//@cuadrilla
                                                                    };

                                                                switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionTercero", "inserta", "ppa", objValores2))
                                                                {
                                                                    case 1:
                                                                        ManejoError("Error al insertar el detalle de la transacción", "I");
                                                                        verifica = true;
                                                                        break;
                                                                }
                                                                r++;
                                                            }
                                                        }

                                                        cuadrilla = null;
                                                        for (int x = 0; x < selTerceroTransporte.Items.Count; x++)
                                                        {
                                                            if (selTerceroTransporte.Items[x].Selected)
                                                            {
                                                                precioLabor = listaPrecios.SeleccionaPrecioNovedadAñoTerceroFincaLote(Convert.ToInt32(Session["empresa"]), ddlLaborTransporte.SelectedValue, fechaTransporte.Year,
                                                                                       Convert.ToInt32(selTerceroTransporte.Items[x].Value), fechaTransporte, finca, lote, seccion);

                                                                object[] objValores2 = new object[]{
                                                                    Convert.ToInt32( fechaTransporte.Year.ToString()),  //@año
                                                                    cantidadT, //@cantidad
                                                                    false, //@ejecutado
                                                                    Convert.ToInt16(this.Session["empresa"]), //@empresa
                                                                    fechaTransporte,
                                                                    finca,
                                                                    jornalT, //@jornales
                                                                    lote,//@lote
                                                                    Convert.ToInt32( fechaTransporte.Month.ToString()),//@mes
                                                                    ddlLaborTransporte.SelectedValue,//@novedad
                                                                    numerotransaccion, //@numero
                                                                    precioLabor,
                                                                    racimos,
                                                                    r,//@registro
                                                                    dlDetalle.Items.Count + cont,  //@registro novedad
                                                                    cantidadT,//@saldo
                                                                    seccion,//@seccion
                                                                   Convert.ToString(selTerceroTransporte.Items[x].Value),//@tercero
                                                                    ConfigurationManager.AppSettings["RegistroBascula"].ToString(),    //@tipo
                                                                    cuadrilla//@cuadrilla
                                                                    };

                                                                switch (CentidadMetodos.EntidadInsertUpdateDelete("aTransaccionTercero", "inserta", "ppa", objValores2))
                                                                {
                                                                    case 1:
                                                                        ManejoError("Error al insertar el detalle de la transacción", "I");
                                                                        verifica = true;
                                                                        break;
                                                                }
                                                                r++;
                                                            }

                                                        }
                                                        break;
                                                    case 1:
                                                        ManejoError("Error al insertar el detalle de la transacción", "I");
                                                        verifica = true;
                                                        break;
                                                }
                                            }
                                        }
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


                    }


                    if (verifica == false)
                    {
                        if (Convert.ToBoolean(this.Session["editar"]) == true)
                            ts.Complete();
                        else
                        {
                            transacciones.ActualizaConsecutivo(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Convert.ToInt16(this.Session["empresa"]));
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
            this.upBascula.Visible = false;
            this.upDetalle.Visible = false;
            this.upRecolector.Visible = false;
            this.niimbRegistro.Enabled = false;
            this.imbConsulta.Enabled = true;
        }
        private void cargarEncabezado()
        {
            upRecolector.Visible = true;
            DataView dvEncabezado = transacciones.RetornaEncabezadoTransaccionLabores(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Session["numeroEditar"].ToString(), Convert.ToInt16(this.Session["empresa"]));
            foreach (DataRowView registro in dvEncabezado)
            {
                // ddlFinca.SelectedValue = registro.Row.ItemArray.GetValue(7).ToString().Trim();
                txtFecha.Text = Convert.ToDateTime(registro.Row.ItemArray.GetValue(5).ToString()).ToShortDateString();
                txtObservacion.Text = registro.Row.ItemArray.GetValue(9).ToString();
                txtRemision.Text = registro.Row.ItemArray.GetValue(8).ToString();


            }

            DataView dvTerceros = transacciones.RetornaEncabezadoTransaccionLaboresTerceroCargue(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Session["numeroEditar"].ToString(), Convert.ToInt16(this.Session["empresa"]));
            DataView dvTransporte = transacciones.RetornaEncabezadoTransaccionLaboresTerceroTransporte(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Session["numeroEditar"].ToString(), Convert.ToInt16(this.Session["empresa"]));


            foreach (DataRowView registro in dvTransporte)
            {
                this.chkLaborTransporte.Checked = true;
                manejoLaborTransporte();
                ddlLaborTransporte.SelectedValue = registro[8].ToString().Trim();
                txtFechaTransporte.Text = Convert.ToDateTime(registro.Row.ItemArray.GetValue(9).ToString()).ToShortDateString();
                if (!(registro.Row.ItemArray.GetValue(4) is DBNull))
                    ddlCuadrillaTransporte.SelectedValue = registro.Row.ItemArray.GetValue(4).ToString();
            }

            foreach (DataRowView registro in dvTerceros)
            {
                this.chkLaborCargue.Checked = true;
                ddlLaborCargadores.SelectedValue = registro[8].ToString().Trim();
                txtFechaCargadores.Text = Convert.ToDateTime(registro.Row.ItemArray.GetValue(9).ToString()).ToShortDateString();
                if (!(registro.Row.ItemArray.GetValue(4) is DBNull))
                    ddlCuadrillaCargue.SelectedValue = registro.Row.ItemArray.GetValue(4).ToString();
            }


            foreach (DataRowView dr in dvTerceros)
            {
                for (int x = 0; x < this.selTerceroCargue.Items.Count; x++)
                {
                    if (dr.Row.ItemArray.GetValue(1).ToString() == selTerceroCargue.Items[x].Value)
                    {
                        if (ddlCuadrillaCargue.SelectedValue.Length > 0)
                        {
                            DataView dvTerceroCuadrilla = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("nCuadrillaFuncionario", "ppa"), "funcionario", Convert.ToInt16(this.Session["empresa"]));
                            dvTerceroCuadrilla.RowFilter = "funcionario = " + selTerceroCargue.Items[x].Value + " and cuadrilla='" + ddlCuadrillaCargue.SelectedValue.ToString() +
                                "' and empresa=" + (Convert.ToInt16(this.Session["empresa"])).ToString();
                            if (dvTerceroCuadrilla.Count == 0)
                                selTerceroCargue.Items[x].Selected = true;
                        }
                        else
                            selTerceroCargue.Items[x].Selected = true;
                    }
                }
            }

            foreach (DataRowView dr in dvTransporte)
            {
                for (int x = 0; x < this.selTerceroTransporte.Items.Count; x++)
                {
                    if (dr.Row.ItemArray.GetValue(1).ToString() == selTerceroTransporte.Items[x].Value)
                    {
                        if (ddlCuadrillaTransporte.SelectedValue.Length > 0)
                        {
                            DataView dvTerceroCuadrilla = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("nCuadrillaFuncionario", "ppa"), "funcionario", Convert.ToInt16(this.Session["empresa"]));
                            dvTerceroCuadrilla.RowFilter = "funcionario = " + selTerceroTransporte.Items[x].Value + " and cuadrilla='" + ddlCuadrillaTransporte.SelectedValue.ToString() +
                                "' and empresa=" + (Convert.ToInt16(this.Session["empresa"])).ToString();
                            if (dvTerceroCuadrilla.Count == 0)
                                selTerceroTransporte.Items[x].Selected = true;
                        }
                        else
                            selTerceroTransporte.Items[x].Selected = true;
                    }
                }
            }

        }
        private void cargarDetalle()
        {
            List<cNovedadTransaccion> listaNT = new List<cNovedadTransaccion>();
            cNovedadTransaccion novedadTran;
            List<Ctercero> listaTer = null;
            Ctercero ter;
            upRecolector.Visible = true;
            DataView dvNovedad = transacciones.RetornaEncabezadoTransaccionLaboresDetalle(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Session["numeroEditar"].ToString(), Convert.ToInt16(this.Session["empresa"]));
            dvNovedad.RowFilter = "claseLabor=2";//and empresa=" + Session["empresa"].ToString();
            DataView dvTerceros = transacciones.RetornaEncabezadoTransaccionLaboresTercero(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Session["numeroEditar"].ToString(), Convert.ToInt16(this.Session["empresa"]));
            dvTerceros.RowFilter = "claseLabor=2";// and empresa=" + Session["empresa"].ToString();
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
                    fecha = registro.Row.ItemArray.GetValue(11).ToString();

                if (!(registro.Row.ItemArray.GetValue(13) is DBNull))
                    jornal = Convert.ToDecimal(registro.Row.ItemArray.GetValue(13).ToString());

                if (!(registro.Row.ItemArray.GetValue(14) is DBNull))
                    registroNovedad = Convert.ToInt32(registro.Row.ItemArray.GetValue(14).ToString());

                if (!(registro.Row.ItemArray.GetValue(16) is DBNull))
                    precioLabor = Convert.ToDecimal(registro.Row.ItemArray.GetValue(16).ToString());
                if (!(registro.Row.ItemArray.GetValue(10) is DBNull))
                    finca = Convert.ToString(registro.Row.ItemArray.GetValue(17).ToString());

                if (!(registro.Row.ItemArray.GetValue(11) is DBNull))
                    nombreFinca = Convert.ToString(registro.Row.ItemArray.GetValue(18).ToString());

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
                    if (((Label)d.FindControl("lblRegistro")).Text.Trim() == nt.Registro.ToString())
                    {
                        ((GridView)d.FindControl("gvLotes")).DataSource = nt.Terceros;
                        ((GridView)d.FindControl("gvLotes")).DataBind();
                    }
                }
            }
            this.Session["novedadLoteSesion"] = listaNT;
        }


        private void cargarTiqueteDetalle()
        {
            CcontrolesUsuario.HabilitarControles(upTiquete.Controls);
            CcontrolesUsuario.LimpiarControles(upBascula.Controls);
            CcontrolesUsuario.InhabilitarUsoControles(upTiquete.Controls);
            CcontrolesUsuario.LimpiarControles(upTiquete.Controls);

            DataView dvEncabezado = transacciones.RetornaEncabezadoTransaccionTiquete(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Session["numeroEditar"].ToString(), Convert.ToInt16(this.Session["empresa"]));

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
                try
                {
                    ddlExtractoraFiltro.DataSource = empresa.SeleccionaEmpresasExtractoras();
                    ddlExtractoraFiltro.DataValueField = "id";
                    ddlExtractoraFiltro.DataTextField = "razonSocial";
                    ddlExtractoraFiltro.DataBind();
                    ddlExtractoraFiltro.Items.Insert(0, new ListItem("", ""));

                    ddlExtractoraTiquete.DataSource = empresa.SeleccionaExtractoras(Convert.ToInt32(this.Session["empresa"]));
                    ddlExtractoraTiquete.DataValueField = "id";
                    ddlExtractoraTiquete.DataTextField = "razonSocial";
                    ddlExtractoraTiquete.DataBind();
                    ddlExtractoraTiquete.Items.Insert(0, new ListItem("", ""));
                    ddlExtractoraFiltro.SelectedValue = registro.Row.ItemArray.GetValue(3).ToString().Trim();
                    ddlExtractoraTiquete.SelectedValue = registro.Row.ItemArray.GetValue(4).ToString().Trim();

                }
                catch (Exception ex)
                {
                    ddlExtractoraFiltro.DataSource = empresa.SeleccionaEmpresasExtractoras();
                    ddlExtractoraFiltro.DataValueField = "id";
                    ddlExtractoraFiltro.DataTextField = "razonSocial";
                    ddlExtractoraFiltro.DataBind();
                    ddlExtractoraFiltro.Items.Insert(0, new ListItem("", ""));

                    ddlExtractoraTiquete.DataSource = empresa.SeleccionaEmpresasExtractoras();
                    ddlExtractoraTiquete.DataValueField = "id";
                    ddlExtractoraTiquete.DataTextField = "razonSocial";
                    ddlExtractoraTiquete.DataBind();
                    ddlExtractoraTiquete.Items.Insert(0, new ListItem("", ""));
                    ddlExtractoraFiltro.SelectedValue = registro.Row.ItemArray.GetValue(3).ToString().Trim();
                    ddlExtractoraTiquete.SelectedValue = registro.Row.ItemArray.GetValue(4).ToString().Trim();
                }
                txtTiquete.Text = registro.Row.ItemArray.GetValue(5).ToString().Trim();
                txvPbruto.Text = registro.Row.ItemArray.GetValue(6).ToString().Trim();
                txvPtara.Text = registro.Row.ItemArray.GetValue(7).ToString().Trim();
                txvPneto.Text = registro.Row.ItemArray.GetValue(8).ToString().Trim();
                txvSacos.Text = registro.Row.ItemArray.GetValue(9).ToString().Trim();
                txvRacimosTiquete.Text = registro.Row.ItemArray.GetValue(10).ToString().Trim();
                //ddlConductor.SelectedValue = registro.Row.ItemArray.GetValue(11).ToString().Trim();
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

            txvPbruto.Enabled = false;
            txvPtara.Enabled = false;
            txvPneto.Enabled = false;
            txtFechaTiqueteI.Enabled = false;
            txtTiquete.Enabled = false;
            ddlExtractoraTiquete.Enabled = false;
            txtRemolque.Enabled = false;
            txtVehiculo.Enabled = false;

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
            try
            {
                if (ddlExtractoraFiltro.SelectedValue.Trim().Length > 0)
                {
                    DataView tiquetes = bascula.SeleccionaTiquetesBasculaExtractora(Convert.ToInt32(ddlExtractoraFiltro.SelectedValue.Trim()), Convert.ToInt16(this.Session["empresa"]), txtFiltroBascula.Text);
                    gvTiquetes.DataSource = tiquetes;
                    gvTiquetes.DataBind();
                    if (this.gvTiquetes.Rows.Count > 0)
                        this.nilbInofrmacionBascula.Text = this.gvTiquetes.Rows.Count.ToString() + " Registros encontrados";
                    else
                        CerroresGeneral.ManejoError(this, GetType(), "El número del tiquete ya fue registrado o el tiquete no se encuentra en la base de datos", "warning");
                    gvTiquetes.Visible = true;
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
        private decimal ObtenerPesoPromedio(string lote, DateTime fecha, string finca)
        {
            try
            {
                string manejaPesoPromedio = "0";
                decimal racimos = 0, retorno = 0;
                manejaPesoPromedio = general.RetornoParametroGeneral("tablaPesoPromedio", Convert.ToInt16(Session["empresa"]));

                if (manejaPesoPromedio == "1")
                    retorno = Convert.ToDecimal(peso.valorPesoPeriodo(Convert.ToInt32(Session["empresa"]), fecha, lote, finca));
                else
                {
                    if (Convert.ToDecimal(txvRacimosTiquete.Text) > 0)
                        racimos = Convert.ToDecimal(txvRacimosTiquete.Text);
                    else
                        racimos = Convert.ToDecimal(txvSacos.Text);

                    if (Convert.ToDecimal(txvRacimosTiquete.Text) == 0 && Convert.ToDecimal(txvSacos.Text) == 0)
                        racimos = 1;

                    retorno = Convert.ToDecimal(Convert.ToDecimal(txvPneto.Text) / racimos);
                }

                return Math.Round(retorno, 2);
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
        protected void cargarDL()
        {
            int posicionNovedad = 0;
            nilblInformacionDetalle.Text = "";
            decimal pesoRacimo = 0, cantidad = 0, precioLabor = 0;
            string novedad = null, nombreNovedad = null, uMedidad = null;

            if (transacciones.SeleccionaNovedadLoteRangoSiembra(this.ddlLote.SelectedValue.Trim(), Convert.ToInt16(this.Session["empresa"]), Convert.ToDateTime(txtFechaD.Text)).Table.Rows.Count > 0)
            {
                foreach (DataRowView registro in transacciones.SeleccionaNovedadLoteRangoSiembra(this.ddlLote.SelectedValue.Trim(), Convert.ToInt16(this.Session["empresa"]), Convert.ToDateTime(txtFechaD.Text)))
                {
                    novedad = registro.Row.ItemArray.GetValue(1).ToString();
                    nombreNovedad = registro.Row.ItemArray.GetValue(2).ToString();
                    uMedidad = registro.Row.ItemArray.GetValue(5).ToString();
                    precioLabor = listaPrecios.SeleccionaPrecioNovedadAño(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaD.Text).Year);

                    if (listaPrecios.SeleccionaPrecioNovedadAño(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaD.Text).Year) == 0)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "La labor seleccionada no tiene precio en el año, por favor registrar precio para continuar.", "warning");
                        return;
                    }
                }
            }
            else
            {
                CerroresGeneral.ManejoError(this, GetType(), "No hay una asociación de una labor de cosecha con el rango de siembra del lote", "warning");
                return;
            }

            if (txvRacimosD.Enabled)
            {
                if (Convert.ToBoolean(NovedadConfig(19, novedad)) == true)
                {
                    if (Convert.ToDecimal(txvRacimosD.Text.Trim()) == 0)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "Los racimos deben ser diferente de cero", "warning");
                        return;
                    }
                }
            }


            if (this.Session["novedadLoteSesion"] == null)
            {
                if (ddlCuadrilla.SelectedValue.Length > 0)
                {
                    DataView dvTerceroCuadrilla = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("nCuadrillaFuncionario", "ppa"), "funcionario", Convert.ToInt16(this.Session["empresa"]));
                    dvTerceroCuadrilla.RowFilter = "cuadrilla='" + ddlCuadrilla.SelectedValue.ToString() + "' and empresa=" + (Convert.ToInt16(this.Session["empresa"])).ToString();
                    if (dvTerceroCuadrilla.Count > 0)
                    {
                        foreach (DataRowView registro in dvTerceroCuadrilla)
                        {
                            precioLabor = listaPrecios.SeleccionaPrecioNovedadAñoTerceroFincaLote(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaD.Text).Year,
                                                   Convert.ToInt32(registro.Row.ItemArray.GetValue(2).ToString()), Convert.ToDateTime(txtFechaD.Text), ddlFinca.SelectedValue, ddlLote.SelectedValue, ddlSeccion.SelectedValue);
                            terceros = new Ctercero(Convert.ToInt32(registro.Row.ItemArray.GetValue(2).ToString()), registro.Row.ItemArray.GetValue(3).ToString(), ddlLote.SelectedValue.ToString().Trim(), ddlCuadrilla.SelectedValue, 0, 0, precioLabor);
                            listaTerceros.Add(terceros);
                        }
                    }
                }

                for (int x = 0; x < selTerceroCosecha.Items.Count; x++)
                {
                    if (selTerceroCosecha.Items[x].Selected)
                    {
                        precioLabor = listaPrecios.SeleccionaPrecioNovedadAñoTerceroFincaLote(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaD.Text).Year,
                          Convert.ToInt32(selTerceroCosecha.Items[x].Value), Convert.ToDateTime(txtFechaD.Text), ddlFinca.SelectedValue, ddlLote.SelectedValue, ddlSeccion.SelectedValue);

                        if (precioLabor == 0)
                            precioLabor = listaPrecios.SeleccionaPrecioNovedadAñoTercero(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaD.Text).Year, Convert.ToInt32(selTerceroCosecha.Items[x].Value), Convert.ToDateTime(txtFechaD.Text));
                        terceros = new Ctercero(Convert.ToInt32(selTerceroCosecha.Items[x].Value), selTerceroCosecha.Items[x].Text, ddlLote.SelectedValue.ToString().Trim(), null, 0, 0, precioLabor);
                        listaTerceros.Add(terceros);
                    }
                }

                novedadTransaccion = new cNovedadTransaccion(ddlFinca.SelectedValue, ddlFinca.SelectedItem.Text, novedad, nombreNovedad, ddlLote.SelectedValue.Trim(), ddlLote.SelectedItem.Text, ddlSeccion.SelectedValue.Trim(), ddlSeccion.SelectedItem.Text,
                        Convert.ToDecimal(txvRacimosD.Text), cantidad, listaTerceros, dlDetalle.Items.Count, uMedidad, pesoRacimo, txtFechaD.Text, Convert.ToDecimal(0), precioLabor, Convert.ToDecimal(txvSacosD.Text));
                listaNovedadesTransaccion.Add(novedadTransaccion);
                this.Session["novedadLoteSesion"] = listaNovedadesTransaccion;
            }
            else
            {
                listaNovedadesTransaccion = (List<cNovedadTransaccion>)this.Session["novedadLoteSesion"]; // cargo todas las novedades 
                listaTerceros = new List<Ctercero>();

                if (ddlCuadrilla.SelectedValue.Length > 0)
                {
                    DataView dvTerceroCuadrilla = CcontrolesUsuario.OrdenarEntidad(CentidadMetodos.EntidadGet("nCuadrillaFuncionario", "ppa"), "funcionario", Convert.ToInt16(this.Session["empresa"]));
                    dvTerceroCuadrilla.RowFilter = "cuadrilla='" + ddlCuadrilla.SelectedValue.ToString() + "' and empresa=" + (Convert.ToInt16(this.Session["empresa"])).ToString();
                    if (dvTerceroCuadrilla.Count > 0)
                    {
                        foreach (DataRowView registro in dvTerceroCuadrilla)
                        {
                            precioLabor = listaPrecios.SeleccionaPrecioNovedadAñoTerceroFincaLote(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaD.Text).Year,
                                                   Convert.ToInt32(registro.Row.ItemArray.GetValue(2).ToString()), Convert.ToDateTime(txtFechaD.Text), ddlFinca.SelectedValue, ddlLote.SelectedValue, ddlSeccion.SelectedValue);
                            terceros = new Ctercero(Convert.ToInt32(registro.Row.ItemArray.GetValue(2).ToString()), registro.Row.ItemArray.GetValue(3).ToString(), ddlLote.SelectedValue.ToString().Trim(), ddlCuadrilla.SelectedValue, 0, 0, precioLabor);
                            listaTerceros.Add(terceros);
                        }
                    }
                }


                for (int x = 0; x < selTerceroCosecha.Items.Count; x++)
                {
                    if (selTerceroCosecha.Items[x].Selected)
                    {
                        precioLabor = listaPrecios.SeleccionaPrecioNovedadAñoTerceroFincaLote(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaD.Text).Year,
                          Convert.ToInt32(selTerceroCosecha.Items[x].Value), Convert.ToDateTime(txtFechaD.Text), ddlFinca.SelectedValue, ddlLote.SelectedValue, ddlSeccion.SelectedValue);

                        if (precioLabor == 0)
                            precioLabor = listaPrecios.SeleccionaPrecioNovedadAñoTercero(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(txtFechaD.Text).Year, Convert.ToInt32(selTerceroCosecha.Items[x].Value), Convert.ToDateTime(txtFechaD.Text));
                        terceros = new Ctercero(Convert.ToInt32(selTerceroCosecha.Items[x].Value), selTerceroCosecha.Items[x].Text, ddlLote.SelectedValue.ToString().Trim(), null, 0, 0, precioLabor);
                        listaTerceros.Add(terceros);
                    }
                }
                novedadTransaccion = new cNovedadTransaccion(ddlFinca.SelectedValue, ddlFinca.SelectedItem.Text, novedad, nombreNovedad, ddlLote.SelectedValue.Trim(), ddlLote.SelectedItem.Text, ddlSeccion.SelectedValue.Trim(), ddlSeccion.SelectedItem.Text,
                Convert.ToDecimal(txvRacimosD.Text), cantidad, listaTerceros, dlDetalle.Items.Count, uMedidad, pesoRacimo, txtFechaD.Text, Convert.ToDecimal(0), precioLabor, Convert.ToDecimal(txvSacosD.Text));
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

            try
            {
                this.selTerceroCosecha.DataSource = transacciones.SelccionaTercernoNovedad(Convert.ToInt16(this.Session["empresa"]));
                this.selTerceroCosecha.DataValueField = "id";
                this.selTerceroCosecha.DataTextField = "cadena";
                this.selTerceroCosecha.DataBind();

                this.selTerceroCargue.DataSource = transacciones.SelccionaTercernoNovedad(Convert.ToInt16(this.Session["empresa"]));
                this.selTerceroCargue.DataValueField = "id";
                this.selTerceroCargue.DataTextField = "cadena";
                this.selTerceroCargue.DataBind();

                this.selTerceroTransporte.DataSource = transacciones.SelccionaTercernoNovedad(Convert.ToInt16(this.Session["empresa"]));
                this.selTerceroTransporte.DataValueField = "id";
                this.selTerceroTransporte.DataTextField = "cadena";
                this.selTerceroTransporte.DataBind();

            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tercero de cosecha. Correspondiente a: " + ex.Message, "C");
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

        private void cargarTercerosCargue()
        {

            try
            {

                this.selTerceroCargue.DataSource = transacciones.SelccionaTercernoNovedad(Convert.ToInt16(this.Session["empresa"]));
                this.selTerceroCargue.DataValueField = "id";
                this.selTerceroCargue.DataTextField = "cadena";
                this.selTerceroCargue.DataBind();

                this.selTerceroTransporte.DataSource = transacciones.SelccionaTercernoNovedad(Convert.ToInt16(this.Session["empresa"]));
                this.selTerceroTransporte.DataValueField = "id";
                this.selTerceroTransporte.DataTextField = "cadena";
                this.selTerceroTransporte.DataBind();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tercero de cosecha. Correspondiente a: " + ex.Message, "C");
            }
        }
        protected void cargarCombox()
        {
            try
            {
                DataView dvLaborCargador = tipoTransaccion.SeleccionaNovedadTipoDocumentos(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Convert.ToInt32(Session["empresa"]));
                dvLaborCargador.RowFilter = "claseLabor=3 and empresa=" + Session["empresa"].ToString();
                this.ddlLaborCargadores.DataSource = dvLaborCargador;
                this.ddlLaborCargadores.DataValueField = "codigo";
                this.ddlLaborCargadores.DataTextField = "descripcion";
                this.ddlLaborCargadores.DataBind();
                this.ddlLaborCargadores.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar Novedades. Correspondiente a: " + ex.Message, "C");
            }
            try
            {
                this.ddlExtractoraTiquete.DataSource = empresa.SeleccionaEmpresasExtractoras();
                this.ddlExtractoraTiquete.DataValueField = "id";
                this.ddlExtractoraTiquete.DataTextField = "razonSocial";
                this.ddlExtractoraTiquete.DataBind();
                this.ddlExtractoraTiquete.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar extractoras. Correspondiente a: " + ex.Message, "C");
            }

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
                this.selTerceroCosecha.DataSource = transacciones.SelccionaTercernoNovedad(Convert.ToInt16(this.Session["empresa"]));
                this.selTerceroCosecha.DataValueField = "id";
                this.selTerceroCosecha.DataTextField = "cadena";
                this.selTerceroCosecha.DataBind();
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar tercero de cosecha. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlCuadrillaCargue.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nCuadrilla", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"])); ;
                this.ddlCuadrillaCargue.DataValueField = "codigo";
                this.ddlCuadrillaCargue.DataTextField = "descripcion";
                this.ddlCuadrillaCargue.DataBind();
                this.ddlCuadrillaCargue.Items.Insert(0, new ListItem("Seleccione una opción", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar Cuadrilla. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlCuadrillaTransporte.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nCuadrilla", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"])); ;
                this.ddlCuadrillaTransporte.DataValueField = "codigo";
                this.ddlCuadrillaTransporte.DataTextField = "descripcion";
                this.ddlCuadrillaTransporte.DataBind();
                this.ddlCuadrillaTransporte.Items.Insert(0, new ListItem("Seleccione una opción", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar Cuadrilla. Correspondiente a: " + ex.Message, "C");
            }

            try
            {
                this.ddlCuadrilla.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nCuadrilla", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"])); ;
                this.ddlCuadrilla.DataValueField = "codigo";
                this.ddlCuadrilla.DataTextField = "descripcion";
                this.ddlCuadrilla.DataBind();
                this.ddlCuadrilla.Items.Insert(0, new ListItem("Seleccione una opción", ""));
            }
            catch (Exception ex)
            {
                ManejoError("Error al cargar Cuadrilla. Correspondiente a: " + ex.Message, "C");
            }


            cargarExtractoras();
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

        private void ManejoErrores(string error, string operacion)
        {
            ManejoError(error, "error");
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
                "er", error, ObtenerIP(), Convert.ToInt32(Session["empresa"]));

        }
        private void ManejoExito(string mensaje, string operacion)
        {
            CerroresGeneral.ManejoError(this, GetType(), mensaje, "info");
            CcontrolesUsuario.InhabilitarControles(this.upBascula.Controls);
            CcontrolesUsuario.LimpiarControles(this.upBascula.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upDetalle.Controls);
            CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upRecolector.Controls);
            CcontrolesUsuario.LimpiarControles(this.upRecolector.Controls);
            CcontrolesUsuario.InhabilitarControles(this.Page.Controls);
            CcontrolesUsuario.LimpiarControles(this.Page.Controls);
            upBascula.Visible = false;
            upDetalle.Visible = false;
            upRecolector.Visible = false;
            dlDetalle.DataSource = null;
            dlDetalle.DataBind();
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

            seguridad.InsertaLog(this.Session["usuario"].ToString(), operacion, ConfigurationManager.AppSettings["Modulo"].ToString() + '-' + this.Page.ToString(),
                "ex", mensaje, ObtenerIP(), Convert.ToInt32(Session["empresa"]));

        }
        private void ManejoEncabezado()
        {
            HabilitaEncabezado();
        }
        private void actualizarDatos()
        {

            listaNovedadesTransaccion = (List<cNovedadTransaccion>)this.Session["novedadLoteSesion"];
            foreach (DataListItem dli in dlDetalle.Items)
            {
                for (int x = 0; x < listaNovedadesTransaccion.Count; x++)
                {
                    if (((Label)dli.FindControl("lblNovedad")).Text == listaNovedadesTransaccion[x].Codnovedad
                        & ((Label)dli.FindControl("lblSeccion")).Text == listaNovedadesTransaccion[x].Codseccion
                        & ((Label)dli.FindControl("lblLote")).Text == listaNovedadesTransaccion[x].Codlote
                        & ((Label)dli.FindControl("lblRegistro")).Text == listaNovedadesTransaccion[x].Registro.ToString())
                    {
                        listaNovedadesTransaccion[x].Racimos = Convert.ToDecimal(((TextBox)dli.FindControl("txvRacimoG")).Text);
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
        private void HabilitaEncabezado()
        {
            this.nilblInformacion.Text = "";
            this.nilblInformacion.ForeColor = System.Drawing.Color.Red;
            this.lbCancelar.Visible = true;
            this.nilbNuevo.Visible = false;
            this.lbRegistrar.Visible = true;
            this.Session["transaccion"] = null;
            tbCabeza.Visible = true;

        }
        private void InHabilitaEncabezado()
        {
            this.nilblInformacion.Text = "";
            this.lbCancelar.Visible = false;
            this.nilbNuevo.Visible = true;
            this.nilbNuevo.Focus();
            upTiquete.Visible = false;

        }
        private string ConsecutivoTransaccion()
        {
            string numero = "";

            try
            {
                numero = transacciones.RetornaNumeroTransaccion(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Convert.ToInt16(this.Session["empresa"]));
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
                              ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Convert.ToInt16(this.Session["empresa"]));
            CcontrolesUsuario.ComportamientoCampoEntidad(this.upDetalle.Controls, "aTransaccionNovedad",
                ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Convert.ToInt16(this.Session["empresa"]));
        }
        private void ManejoRBLbascula()
        {
            CcontrolesUsuario.HabilitarControles(upBascula.Controls);
            nilblInformacion.Text = "";
            if (Convert.ToInt32(rblBascula.SelectedValue) == 1)
            {

                upRecolector.Visible = false;
                upDetalle.Visible = false;
                ddlExtractoraFiltro.Enabled = true;
                ddlExtractoraFiltro.Visible = true;
                lblExtractoraFiltro.Visible = true;
                lblFiltroBusqueda.Visible = true;
                txtFiltroBascula.Visible = true;
                upTiquete.Visible = false;
                imbBuscarBascula.Visible = true;
                tbCabeza.Visible = false;
                CcontrolesUsuario.InhabilitarControles(upTiquete.Controls);
                txtFecha.Text = "";
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
                tbCabeza.Visible = true;
                imbBuscarBascula.Visible = false;
                CcontrolesUsuario.HabilitarControles(upTiquete.Controls);
                CcontrolesUsuario.LimpiarControles(upTiquete.Controls);
                txvPneto.Enabled = false;
                gvTiquetes.DataSource = null;
                gvTiquetes.DataBind();
                gvTiquetes.DataSource = null;
                gvTiquetes.DataBind();
                manejoRecolector();
                chkLaborCargue.Checked = true;
                manejoLaborCargue();
                cargarCombox();
                txtFecha.Text = "";
                cargarExtractora();
            }
            cargarTercerosCargue();
            txtFecha.Enabled = true;
        }

        private void cargarExtractora()
        {
            try
            {
                if (rblBascula.SelectedValue == "2")
                {
                    DataView dvextractoratiquete = empresa.SeleccionaExtractoras(Convert.ToInt32(Session["empresa"]));
                    this.ddlExtractoraTiquete.DataSource = dvextractoratiquete;
                    this.ddlExtractoraTiquete.DataValueField = "id";
                    this.ddlExtractoraTiquete.DataTextField = "razonSocial";
                    this.ddlExtractoraTiquete.DataBind();
                    this.ddlExtractoraTiquete.Items.Insert(0, new ListItem("", ""));
                }
                else
                {
                    DataView dvextractoratiquete = empresa.SeleccionaEmpresasExtractoras();
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

            decimal diferencia = 0, cantidadTotal = 0, cantidadTotalAsignada = 0, cantidadTercero = 0;
            decimal cantidadJornales = 0, jornalesTotales = 0, jornalesAsignados = 0, diferenciaJornal;
            int noTerceros = 0;
            decimal subCantidad = 0;
            decimal subRacimos = 0;
            decimal subjornales = 0;
            Csubtotales subT;
            bool validarN = false;
            int pn = 0;
            listaNovedadesTransaccion = (List<cNovedadTransaccion>)this.Session["novedadLoteSesion"];
            int contarN = 0;


            int cantidadDL = 0;

            foreach (DataListItem dl in dlDetalle.Items)
            {
                cantidadDL += Convert.ToInt32(Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text));
            }
            contarN = dlDetalle.Items.Count;

            diferencia = Convert.ToDecimal(txvPneto.Text) - cantidadDL;

            if (contarN == 0)
                contarN = 1;
            int ramdom = new Random().Next(1, contarN);
            int w = 0;
            foreach (DataListItem dl in dlDetalle.Items)
            {
                if (w == ramdom)
                    ((TextBox)dl.FindControl("txvCantidadG")).Text = (Convert.ToInt64(Convert.ToInt64(Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text)) + diferencia)).ToString();
                w++;
            }

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
                    subT = new Csubtotales(((Label)dl.FindControl("lblNovedad")).Text, ((Label)dl.FindControl("lblDesNovedad")).Text, Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text), 0, 0);
                    subtotal.Add(subT);
                }

            }


            foreach (DataListItem dl in dlDetalle.Items)
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
            actualizarDatos();
            CalcularSubtotal();
        }
        private void CalcularSubtotal()
        {
            listaNovedadesTransaccion = (List<cNovedadTransaccion>)this.Session["novedadLoteSesion"];
            gvSubTotales.DataSource = null;
            gvSubTotales.DataBind();
            subtotal = new List<Csubtotales>();
            decimal subCantidad = 0;
            decimal subRacimos = 0;
            decimal subjornales = 0;

            Csubtotales sub;

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
                        sub = new Csubtotales(listaNovedadesTransaccion[x].Codnovedad, listaNovedadesTransaccion[x].Desnovedad, subCantidad, subRacimos, subjornales);
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
                decimal noRacimos = 0;
                decimal cantidad = 0, pPromedio = 0;
                decimal totalKilos = 0, cantidadP = 0;
                List<int> listacantidadesRepartidas = new List<int>();
                List<Decimal> difKilosLote = null;
                difKilosLote = new List<decimal>();
                decimal difTotalKg = 0;

                foreach (DataListItem dl in dlDetalle.Items)
                    ((TextBox)dl.FindControl("txvCantidadG")).Enabled = false;

                foreach (DataListItem dl in dlDetalle.Items)
                {
                    noRacimos += Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text);
                    cantidad += Convert.ToDecimal(((TextBox)dl.FindControl("txvCantidadG")).Text);
                }

                if ((noRacimos + racimos) > Convert.ToDecimal(txvRacimosTiquete.Text))
                {
                    CerroresGeneral.ManejoError(this, GetType(), "El número de racimos no puede ser mayor al tiquete", "warning");
                    return;
                }

                if (dlDetalle.Items.Count > 0)
                {
                    totalKilos = 0;
                    cantidadP = 0;

                    foreach (DataListItem dl in dlDetalle.Items)
                    {
                        if (((Label)dl.FindControl("lblLote")).Text.Trim().Length > 0)
                        {
                            pPromedio = ObtenerPesoPromedio(((Label)dl.FindControl("lblLote")).Text, Convert.ToDateTime(((Label)dl.FindControl("lblFechaD")).Text), ((Label)dl.FindControl("lblFinca")).Text);
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

                    foreach (DataListItem dl in dlDetalle.Items)
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

                foreach (DataListItem d in dlDetalle.Items)
                {
                    ((DropDownList)d.FindControl("ddlTerceroGrilla")).DataSource = transacciones.SelccionaTercernoNovedad(Convert.ToInt16(this.Session["empresa"]));
                    ((DropDownList)d.FindControl("ddlTerceroGrilla")).DataValueField = "id";
                    ((DropDownList)d.FindControl("ddlTerceroGrilla")).DataTextField = "cadena";
                    ((DropDownList)d.FindControl("ddlTerceroGrilla")).DataBind();
                }

                repartirCantidades();
                nilblInformacionDetalle.Text = "liquidación generada exitosamente";
                nilblInformacionDetalle.ForeColor = Color.Green;
                txvSacosD.Text = "0";
                txvRacimosD.Text = "0";

            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
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
                    CerroresGeneral.ManejoError(this, GetType(), "Debe agregar un filtro antes de la busqueda", "warning");
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
                {
                    TabRegistro();
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
            this.Session["novedadLoteSesion"] = null;
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
            txtFechaD.Enabled = false;
            txtFechaTiqueteI.Enabled = false;
            upBascula.Visible = false;
            upDetalle.Visible = false;
            upTiquete.Visible = false;
            dlDetalle.DataSource = null;
            dlDetalle.DataBind();
            niimbImprimir.Visible = false;

            CcontrolesUsuario.InhabilitarControles(this.upRecolector.Controls);
            CcontrolesUsuario.LimpiarControles(this.upRecolector.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upDetalle.Controls);
            CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
            cargarCombox();
            cargarTercerosCargue();
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
            cargarExtractoras();
            this.Session["subtotal"] = null;
            nilblInformacion.Text = "";
            this.Session["numerotransaccion"] = null;
            txtFechaD.Enabled = false;
            txtFechaTiqueteI.Enabled = false;
        }
        protected void lbNuevo_Click(object sender, EventArgs e)
        {

            if (seguridad.VerificaAccesoOperacion(this.Session["usuario"].ToString(), ConfigurationManager.AppSettings["Modulo"].ToString(),
                                        nombrePaginaActual(), "I", Convert.ToInt32(Session["empresa"])) == 0)
            {
                ManejoError("Usuario no autorizado para ejecutar esta operación", "C");
                return;
            }
            this.Session["editar"] = false;
            ManejoEncabezado();
            LimpiarDDL();
            this.Session["novedadLoteSesion"] = null;
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
            txtFechaD.Enabled = false;
            txtFechaTiqueteI.Enabled = false;
            niimbImprimir.Visible = false;
            upBascula.Visible = Convert.ToBoolean(TipoTransaccionConfig(25));
            rblBascula.Visible = Convert.ToBoolean(TipoTransaccionConfig(25));
            rblBascula.SelectedValue = "1";
            txtFecha.Text = "";
            tbCabeza.Visible = false;
            cargarExtractoras();
            ManejoRBLbascula();
            limpiarSubtotal();
            tbFiltroTiquete.Visible = true;
        }
        protected void lbCancelar_Click(object sender, EventArgs e)
        {
            InHabilitaEncabezado();
            upRecolector.Visible = false;
            upTiquete.Visible = false;
            upDetalle.Visible = false;
            ManejoRBLbascula();
            CcontrolesUsuario.LimpiarControles(this.upRecolector.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upRecolector.Controls);
            CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upDetalle.Controls);
            CcontrolesUsuario.LimpiarControles(this.upTiquete.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upTiquete.Controls);
            CcontrolesUsuario.LimpiarControles(this.upBascula.Controls);
            CcontrolesUsuario.InhabilitarControles(this.upBascula.Controls);

            this.upBascula.Visible = false;
            niimbImprimir.Visible = false;
            this.Session["transaccion"] = null;
            this.dlDetalle.DataSource = null;
            this.dlDetalle.DataBind();
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
            txtFechaD.Enabled = false;
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
        protected void imbCargar_Click(object sender, EventArgs e)
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
                ManejoError("Error al recuperar posición de configuración del lote. Correspondiente a: " + ex.Message, "C");
                return null;
            }
        }

        protected void imbCargar_Click1(object sender, EventArgs e)
        {
            limpiarMensajes();
            nilblInformacionDetalle.Visible = true;
            nilblInformacionDetalle.Text = "";
            nilblInformacionDetalle.ForeColor = Color.Red;


            if (txvSacosD.Text.Length == 0)
                txvSacosD.Text = "0";

            if (ddlLote.SelectedValue.Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar un lote antes de continuar", "warning");
                return;
            }

            if (Convert.ToDecimal(txvPneto.Text.Trim()) == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "La cantidad debe ser diferente de cero", "warning");
                return;
            }

            if (txtFechaD.Enabled)
            {
                if (txtFechaD.Text.Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "debe ingresar una fecha para continuar", "warning");
                    return;
                }
            }

            if (Convert.ToDecimal(txvRacimosD.Text) > Convert.ToDecimal(txvRacimosTiquete.Text))
            {
                CerroresGeneral.ManejoError(this, GetType(), "El número de racimos no puede ser mayor al tiquete", "warning");
                return;
            }
            decimal cantidadTotal = 0, jornalTotal = 0, racimosTotal = 0, subRacimos = 0, subcantidad = 0;
            int contador = 0;
            cantidadTotal = Convert.ToDecimal(txvPneto.Text);
            jornalTotal = Convert.ToDecimal(txvSacosD.Text);
            racimosTotal = Convert.ToDecimal(txvRacimosTiquete.Text);


            foreach (DataListItem dl in dlDetalle.Items)
            {
                subRacimos += Convert.ToDecimal(((TextBox)dl.FindControl("txvRacimoG")).Text);
            }


            if (subRacimos + Convert.ToDecimal(txvRacimosD.Text) > racimosTotal & Convert.ToDecimal(txvRacimosTiquete.Text.Trim()) >= 0 & txvRacimosD.Enabled == true)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Los racimos no deben ser mayor a los del tiquete", "warning");
                return;
            }

            for (int x = 0; x < selTerceroCosecha.Items.Count; x++)
            {
                if (selTerceroCosecha.Items[x].Selected)
                    contador++;
            }

            if (contador == 0 && ddlCuadrilla.SelectedValue.Length == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "Debe selecionar un trabajador o una cuadrilla para continuar", "warning");
                return;
            }


            if (transacciones.SeleccionaNovedadLoteRangoSiembra(this.ddlLote.SelectedValue.Trim(), Convert.ToInt16(this.Session["empresa"]), Convert.ToDateTime(txtFechaD.Text)).Table.Rows.Count <= 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "No hay una asociación de una labor de cosecha con el rango de siembra del lote", "warning");
                return;
            }

            if (ObtenerPesoPromedio(ddlLote.SelectedValue, Convert.ToDateTime(txtFechaD.Text), ddlFinca.SelectedValue) == 0)
            {
                CerroresGeneral.ManejoError(this, GetType(), "El lote seleccionado no tiene peso promedio en el periodo de la fecha", "warning");
                return;
            }

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
            ddlFinca.Enabled = true;
            ddlSeccion.Enabled = true;
        }

        private void limpiarMensajes()
        {
            nilblInformacionDetalle.Text = "";
            nilblInformacionCargadoresTiq.Text = "";
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
        protected void imbBuscarBascula_Click(object sender, EventArgs e)
        {
            cargarTiquetes(txtFiltroBascula.Text);
        }
        protected void lbRegistrar_Click(object sender, EventArgs e)
        {

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
            if (listaNovedadesTransaccion.Count == 0)
                nilblInformacionDetalle.Text = "";

            decimal cantidadTotal = 0, jornalesTotales = 0, subCantidad = 0, subJornales = 0, subRacimos = 0;

            foreach (DataListItem d in dlDetalle.Items)
            {
                cantidadTotal = Convert.ToDecimal(((TextBox)d.FindControl("txvCantidadG")).Text);
                jornalesTotales = Convert.ToDecimal(((TextBox)d.FindControl("txvJornalesD")).Text);
                subCantidad += cantidadTotal;
                subJornales += jornalesTotales;
                subRacimos += Convert.ToDecimal(((TextBox)d.FindControl("txvRacimoG")).Text);

                foreach (cNovedadTransaccion nt in listaNovedadesTransaccion)
                {
                    if (((Label)d.FindControl("lblNovedad")).Text.Trim() == nt.Codnovedad.ToString() & ((Label)d.FindControl("lblSeccion")).Text.Trim() == nt.Codseccion.ToString() &
                        ((Label)d.FindControl("lblLote")).Text.Trim() == nt.Codlote.ToString() & ((Label)d.FindControl("lblRegistro")).Text.Trim() == nt.Registro.ToString()
                        )
                    {
                        ((GridView)d.FindControl("gvLotes")).DataSource = nt.Terceros;
                        ((GridView)d.FindControl("gvLotes")).DataBind();
                    }
                }
            }
            this.Session["novedadLoteSesion"] = listaNovedadesTransaccion;
            LiquidaTransaccioin(0, 0);
        }
        protected void txvPtara_TextChanged(object sender, EventArgs e)
        {
            txvPneto.Text = Convert.ToString(Convert.ToDecimal(txvPbruto.Text) - Convert.ToDecimal(txvPtara.Text));
        }
        protected void txvPbruto_TextChanged(object sender, EventArgs e)
        {
            txvPneto.Text = Convert.ToString(Convert.ToDecimal(txvPbruto.Text) - Convert.ToDecimal(txvPtara.Text));
        }
        protected void imbLiquidar_Click(object sender, EventArgs e)
        {
            LiquidaTransaccioin(0, 0);
            ddlLote.Enabled = true;
            ddlFinca.Enabled = true;
            ddlFinca.Enabled = true;
            ddlSeccion.Enabled = true;
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
            chkLaborTransporte.Enabled = true;
            lbFecha.Enabled = true;
            txtFecha.Enabled = true;
            txtRemision.Enabled = true;
            tbCabeza.Visible = true;
            txvPbruto.Enabled = false;
            txvPtara.Enabled = false;
            txvPneto.Enabled = false;
            txtFechaTiqueteI.Enabled = false;
            txtTiquete.Enabled = false;


            txtObservacion.Enabled = true;
            try
            {
                if (this.gvTiquetes.SelectedRow.Cells[1].Text != "&nbsp;")
                {
                    ddlExtractoraTiquete.SelectedValue = Server.HtmlDecode(this.gvTiquetes.SelectedRow.Cells[1].Text.Trim());
                    ddlExtractoraFiltro.SelectedValue = ddlexfiltro;
                }
                else
                    ddlExtractoraTiquete.SelectedValue = "";

                if (this.gvTiquetes.SelectedRow.Cells[3].Text != "&nbsp;")
                    txtTiquete.Text = Server.HtmlDecode(this.gvTiquetes.SelectedRow.Cells[3].Text);
                else
                    txtTiquete.Text = "";

                if (this.gvTiquetes.SelectedRow.Cells[4].Text != "&nbsp;")
                    txtFechaTiqueteI.Text = Server.HtmlDecode(this.gvTiquetes.SelectedRow.Cells[4].Text);
                else
                    txtFechaTiqueteI.Text = "";

                if (this.gvTiquetes.SelectedRow.Cells[5].Text != "&nbsp;")
                    txtVehiculo.Text = Server.HtmlDecode(this.gvTiquetes.SelectedRow.Cells[5].Text);
                else
                    txtVehiculo.Text = "";

                if (this.gvTiquetes.SelectedRow.Cells[6].Text != "&nbsp;")
                    txtRemolque.Text = Server.HtmlDecode(this.gvTiquetes.SelectedRow.Cells[6].Text);
                else
                    txtVehiculo.Text = "";


                if (this.gvTiquetes.SelectedRow.Cells[7].Text != "&nbsp;")
                    txvPbruto.Text = Server.HtmlDecode(Convert.ToDecimal(this.gvTiquetes.SelectedRow.Cells[7].Text).ToString());
                else
                    txvPbruto.Text = "0";

                if (this.gvTiquetes.SelectedRow.Cells[8].Text != "&nbsp;")
                    txvPtara.Text = Server.HtmlDecode(Convert.ToDecimal(this.gvTiquetes.SelectedRow.Cells[8].Text).ToString());
                else
                    txvPtara.Text = "0";

                if (this.gvTiquetes.SelectedRow.Cells[9].Text != "&nbsp;")
                    txvPneto.Text = Server.HtmlDecode(Convert.ToDecimal(this.gvTiquetes.SelectedRow.Cells[9].Text).ToString());
                else
                    txvPneto.Text = "0";

                if (this.gvTiquetes.SelectedRow.Cells[10].Text != "&nbsp;")
                    txvSacos.Text = Server.HtmlDecode(Convert.ToDecimal(this.gvTiquetes.SelectedRow.Cells[10].Text).ToString());
                else
                    txvSacos.Text = "0";

                if (this.gvTiquetes.SelectedRow.Cells[11].Text != "&nbsp;")
                    txvRacimosTiquete.Text = Server.HtmlDecode(Convert.ToDecimal(this.gvTiquetes.SelectedRow.Cells[11].Text).ToString());
                else
                    txvRacimosTiquete.Text = "0";

                //if (this.gvTiquetes.SelectedRow.Cells[12].Text != "&nbsp;")
                //{
                //    DataView dvTercero = transacciones.SelccionaTercernoNovedad(Convert.ToInt32(Session["empresa"]));
                //    dvTercero.RowFilter = "codigo ='" + Server.HtmlDecode(this.gvTiquetes.SelectedRow.Cells[12].Text.Trim()) + "'";
                //    foreach (DataRowView ter in dvTercero)
                //    {
                //        if (ddlConductor.SelectedValue == ter.Row.ItemArray.GetValue(0).ToString().Trim())
                //            ddlConductor.SelectedValue = ter.Row.ItemArray.GetValue(0).ToString().Trim();
                //    }
                //}

                gvTiquetes.DataSource = null;
                gvTiquetes.DataBind();
                manejoRecolector();
                chkLaborCargue.Checked = true;
                manejoLaborCargue();
                cargarTercerosCargue();
            }
            catch (Exception ex)
            {
                ManejoErrorCatch(ex);
            }
        }

        private void manejoRecolector()
        {
            upRecolector.Visible = true;
            upDetalle.Visible = true;
            CcontrolesUsuario.LimpiarControles(this.upRecolector.Controls);
            CcontrolesUsuario.HabilitarControles(this.upRecolector.Controls);
            CcontrolesUsuario.LimpiarControles(this.upDetalle.Controls);
            CcontrolesUsuario.HabilitarControles(this.upDetalle.Controls);
            //ddlcu.Visible = true;
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
                    ddlSeccion.SelectedValue = seccion.Trim();
                    ddlLote.SelectedValue = lote;
                    txtFechaD.Text = fecha;
                    ddlLote.Enabled = false;
                    ddlFinca.Enabled = false;
                    ddlFinca.Enabled = false;
                    ddlSeccion.Enabled = false;
                    txvRacimosD.ReadOnly = false;
                    txvSacosD.ReadOnly = false;

                    LiquidaTransaccioin(0, 0);
                }

            }
        }
        protected void lbCancelarD_Click(object sender, EventArgs e)
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
            {
                selTerceroCosecha.Visible = true;

            }

            cargarComboxDetalle();
        }
        protected void chkLaborTransporte_CheckedChanged(object sender, EventArgs e)
        {
            manejoLaborTransporte();
        }

        private void manejoLaborTransporte()
        {
            if (chkLaborTransporte.Checked)
            {
                try
                {
                    ddlLaborTransporte.Enabled = true;
                    ddlCuadrillaTransporte.Enabled = true;
                    txtFechaTransporte.Enabled = true;
                    this.ddlLaborTransporte.DataSource = tipoTransaccion.SeleccionaNovedadTipoDocumentos(4, Convert.ToInt32(Session["empresa"]));
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
                    this.ddlCuadrillaTransporte.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nCuadrilla", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"])); ;
                    this.ddlCuadrillaTransporte.DataValueField = "codigo";
                    this.ddlCuadrillaTransporte.DataTextField = "descripcion";
                    this.ddlCuadrillaTransporte.DataBind();
                    this.ddlCuadrillaTransporte.Items.Insert(0, new ListItem("Seleccione una opción", ""));
                }
                catch (Exception ex)
                {
                    ManejoError("Error al cargar Cuadrilla. Correspondiente a: " + ex.Message, "C");
                }
                selTerceroTransporte.Visible = true;

            }
            else
            {
                this.ddlLaborTransporte.DataSource = null;
                ddlLaborTransporte.DataBind();
                ddlLaborTransporte.Enabled = false;
                this.ddlCuadrilla.DataSource = null;
                ddlCuadrillaTransporte.DataBind();
                ddlCuadrillaTransporte.Enabled = false;
                selTerceroTransporte.Visible = false;
                txtFechaTransporte.Enabled = false;
            }
        }
        protected void niimbImprimir_Click(object sender, EventArgs e)
        {

        }

        protected void txtFechaD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFechaD.Text);
            }
            catch (Exception)
            {
                nilblInformacionDetalle.Text = "Formato de fecha invalido";
                return;

            }
            string fecha = Convert.ToDateTime(txtFechaD.Text).ToShortDateString();
            if (upBascula.Visible == true)
            {

                if (txtFechaTiqueteI.Text.Trim().Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar una fecha de tiquete para continuar", "warning");
                    return;
                }

                if ((Convert.ToDateTime(txtFechaTiqueteI.Text).Date < Convert.ToDateTime(fecha)))
                {
                    CerroresGeneral.ManejoError(this, GetType(), "La fecha de cosecha no puede ser mayor a la del tiquete", "warning");
                    txtFechaD.Text = "";
                    txtFechaD.Focus();
                    return;
                }
            }
        }
        protected void txtFechaCargadores_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDateTime(txtFechaCargadores.Text);

            }
            catch
            {
                nilblInformacionCargadoresTiq.Text = "Formato de fecha no valido";
                txtFechaCargadores.Text = "";
                txtFechaCargadores.Focus();
                return;
            }

            if (Convert.ToDateTime(txtFechaCargadores.Text) > Convert.ToDateTime(txtFechaTiqueteI.Text))
            {
                nilblInformacionCargadoresTiq.Text = "La fecha de cargue no puede ser mayor a la del tiquete";
                txtFechaCargadores.Text = "";
                txtFechaCargadores.Focus();
                return;
            }

        }
        protected void dlDetalle_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                DataListItem dli = dlDetalle.Items[e.Item.ItemIndex];
                listaNovedadesTransaccion = (List<cNovedadTransaccion>)this.Session["novedadLoteSesion"];
                GridView gvTerceros = (GridView)dli.FindControl("gvLotes");
                int registro = Convert.ToInt32(((Label)dli.FindControl("lblRegistro")).Text);
                decimal jornalD = Convert.ToDecimal(((TextBox)dli.FindControl("txvJornalesD")).Text);
                listaNovedadesTransaccion[registro].Jornal = jornalD;
                List<Ctercero> tercerosNovedad = listaNovedadesTransaccion[registro].Terceros;
                string lote = ((Label)dli.FindControl("lblLote")).Text;
                string seccion = ((Label)dli.FindControl("lblSeccion")).Text;
                DropDownList ddlTercero = ((DropDownList)dli.FindControl("ddlTerceroGrilla"));
                string novedad = ((Label)dli.FindControl("lblNovedad")).Text.Trim();
                DateTime fecha = Convert.ToDateTime(((Label)dli.FindControl("lblFechaD")).Text);

                foreach (GridViewRow gv in gvTerceros.Rows)
                {
                    if (gv.Cells[1].Text == ddlTercero.SelectedValue)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "El trabajador seleccionado ya se encuentra en la grilla, por favor corrija", "warning");
                        nilblInformacionDetalle.ForeColor = Color.Red;
                        return;
                    }
                }

                decimal precioLabor = listaPrecios.SeleccionaPrecioNovedadAñoTerceroFincaLote(Convert.ToInt32(Session["empresa"]), novedad, fecha.Year, Convert.ToInt32(ddlTercero.SelectedValue), fecha, ddlFinca.SelectedValue, lote, seccion);

                if (precioLabor == 0)

                    precioLabor = listaPrecios.SeleccionaPrecioNovedadAñoTercero(Convert.ToInt32(Session["empresa"]), novedad, Convert.ToDateTime(((Label)dli.FindControl("lblFechaD")).Text).Year,
                    Convert.ToInt32(ddlTercero.SelectedValue), Convert.ToDateTime(((Label)dli.FindControl("lblFechaD")).Text));

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
                int contador = 0;
                DataListItem dli = dlDetalle.Items[e.Item.ItemIndex];
                listaNovedadesTransaccion = (List<cNovedadTransaccion>)this.Session["novedadLoteSesion"];

                GridView gvTerceros = (GridView)dli.FindControl("gvLotes");

                int registro = Convert.ToInt32(((Label)dli.FindControl("lblRegistro")).Text);
                foreach (GridViewRow gv in gvTerceros.Rows)
                {
                    if (((CheckBox)gv.FindControl("chkSeleccion")).Checked)
                        contador += 1;
                }
                if (contador <= 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe chequear un tercero antes de eliminar", "warning");
                    nilblInformacionDetalle.ForeColor = Color.Red;
                    return;
                }
                List<Ctercero> tercerosNovedad = listaNovedadesTransaccion[registro].Terceros;
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
        protected void niimbAdicionar_Click(object sender, EventArgs e)
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

            EstadoInicialGrillaTransacciones();
            imbBusqueda.Focus();
        }
        protected void imbBusqueda_Click(object sender, EventArgs e)
        {
            BusquedaTransaccion();
        }
        protected void imbConsulta_Click(object sender, EventArgs e)
        {
            TabConsulta();
        }

        private void TabConsulta()
        {
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
        protected void niimbRegistro_Click(object sender, EventArgs e)
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

                if (periodo.RetornaPeriodoCerradoNomina(fecha.Year, fecha.Month, Convert.ToInt32(Session["empresa"]), fecha) == 1)
                {
                    ManejoError("Periodo cerrado de nomina. No es posible realizar nuevas transacciones", "I");
                }
                else
                {
                    HabilitaEncabezado();
                    this.nilblInformacion.Text = "";
                    this.Session["editar"] = true;
                    this.Session["transaccion"] = null;
                    lbRegistrar.Visible = false;
                    CcontrolesUsuario.LimpiarControles(upConsulta.Controls);
                    upConsulta.Visible = false;
                    imbConsulta.Enabled = false;
                    nilblRegistros.Enabled = true;
                    tbFiltroTiquete.Visible = false;
                    cargarCombox();
                    cargarTercerosCargue();
                    ComportamientoTransaccion();
                    Session["tipoEditar"] = this.gvTransaccion.Rows[e.RowIndex].Cells[2].Text;
                    Session["numeroEditar"] = this.gvTransaccion.Rows[e.RowIndex].Cells[3].Text;
                    manejoRecolector();
                    cargarDetalle();
                    ManejoRBLbascula();
                    cargarTiqueteDetalle();
                    tbCabeza.Visible = true;
                    upDetalle.Visible = true;
                    cargarEncabezado();



                    foreach (DataListItem d in dlDetalle.Items)
                    {
                        GridView lotes = ((GridView)d.FindControl("gvLotes"));
                        foreach (GridViewRow gvr in lotes.Rows)
                        {
                            ((TextBox)gvr.Cells[4].FindControl("txtCantidad")).Enabled = false;
                        }
                    }
                    CcontrolesUsuario.HabilitarUsoControles(dlDetalle.Controls);
                    txtFecha.Enabled = false;

                    selTerceroCosecha.Visible = true;
                    CalcularSubtotal();
                    manejoFinca();
                    LiquidaTransaccioin(0, 0);
                    lbRegistrar.Visible = true;
                }
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



        protected void lbRegistrar_Click1(object sender, EventArgs e)
        {
            limpiarMensajes();
            if (this.Session["usuario"] == null)
                this.Response.Redirect("~/WebForms/Inicio.aspx");
            else
            {
                int verificarCRJ = 0;
                if (dlDetalle.Items.Count <= 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "El nivel de detalle de la transacción de cosecha debe tener por lo menos un registro", "warning");
                    return;
                }

                if (txtFecha.Text.Length == 0)
                {
                    CerroresGeneral.ManejoError(this, GetType(), "Debe seleccionar o digitar fecha de la transacción", "warning");
                    return;
                }


                LiquidaTransaccioin(0, 0);

                List<Csubtotales> subtotal = (List<Csubtotales>)this.Session["subtotal"];

                decimal subCantidad = 0;
                decimal subRacimo = 0;

                for (int x = 0; x < subtotal.Count; x++)
                {
                    subCantidad += subtotal[x].subCantidad;
                    subRacimo += subtotal[x].subRacimo;
                }

                if (!(Convert.ToDecimal(txvPneto.Text) == subCantidad))
                    verificarCRJ = 1;
                if (!(Convert.ToDecimal(txvRacimosTiquete.Text) == subRacimo))
                    verificarCRJ = 2;
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

                if (upBascula.Visible == true)
                {
                    if (Convert.ToInt32(rblBascula.SelectedValue) == 1)
                    {
                        if (this.ddlExtractoraFiltro.SelectedValue.Trim().Length == 0 || this.ddlExtractoraTiquete.SelectedValue.Trim().Length == 0 || this.txvPbruto.Text.Trim().Length == 0 ||
                         this.txtVehiculo.Text.Trim().Length == 0 || this.txvPtara.Text.Trim().Length == 0 || this.txvRacimosTiquete.Text.Trim().Length == 0 || this.txvSacos.Text.Trim().Length == 0 || this.txtFechaTiqueteI.Text.Trim().Length == 0)
                        {
                            CerroresGeneral.ManejoError(this, GetType(), "Campos vacios en el tiquete por favor corregir", "warning");
                            return;
                        }

                        if (chkLaborTransporte.Checked)
                        {
                            int contador = 0;

                            for (int x = 0; x < selTerceroTransporte.Items.Count; x++)
                            {
                                if (selTerceroTransporte.Items[x].Selected)
                                    contador++;
                            }

                            if (contador == 0 && ddlCuadrillaTransporte.SelectedValue.Length == 0)
                            {
                                CerroresGeneral.ManejoError(this, GetType(), "Debe selecionar un trabajador o una cuadrilla de transporte", "warning");
                                return;
                            }

                            if (ddlLaborTransporte.SelectedValue.Length == 0 || txtFechaTransporte.Text.Length == 0)
                            {
                                CerroresGeneral.ManejoError(this, GetType(), "Campos vacios en transporte por favor corregir", "warning");
                                return;
                            }

                        }
                    }
                    else
                    {
                        if (this.ddlExtractoraTiquete.SelectedValue.Trim().Length == 0 || this.txvPbruto.Text.Trim().Length == 0 || this.txtVehiculo.Text.Trim().Length == 0 ||
                         this.txvPtara.Text.Trim().Length == 0 || this.txvRacimosTiquete.Text.Trim().Length == 0 || this.txvSacos.Text.Trim().Length == 0 || this.txtFechaTiqueteI.Text.Trim().Length == 0)
                        {
                            CerroresGeneral.ManejoError(this, GetType(), "Campos vacios en el tiquete por favor corregir", "warning");
                            return;
                        }
                    }
                }

                if (upRecolector.Visible == true)
                {
                    try
                    {
                        if (chkLaborCargue.Checked == true)
                        {
                            DateTime fechaCosecha = Convert.ToDateTime(txtFechaCargadores.Text);
                            if (ddlLaborCargadores.SelectedValue.Length == 0 || txtFechaCargadores.Text.Length == 0)
                            {
                                CerroresGeneral.ManejoError(this, GetType(), "Campos vacios en cargadores por favor corregir", "warning");
                                return;
                            }

                            int contador = 0;

                            for (int x = 0; x < selTerceroCargue.Items.Count; x++)
                            {
                                if (selTerceroCargue.Items[x].Selected)
                                    contador++;
                            }

                            if (contador == 0 && ddlCuadrillaCargue.SelectedValue.Length == 0)
                            {
                                CerroresGeneral.ManejoError(this, GetType(), "Debe selecionar un trabajador o una cuadrilla de cargue", "warning");
                                return;
                            }

                        }
                    }
                    catch (Exception)
                    {
                        CerroresGeneral.ManejoError(this, GetType(), "La fecha de la cosecha no tiene el formato correcto (DD/MM/YYYY), por favor corrija", "warning");
                        return;
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
            if (chkLaborCargue.Checked)
            {
                try
                {
                    ddlLaborCargadores.Enabled = true;
                    selTerceroCargue.Visible = true;
                    lbFechaCargadores.Enabled = true;
                    txtFechaCargadores.Enabled = true;
                    DataView dvLaborCargador = tipoTransaccion.SeleccionaNovedadTipoDocumentos(ConfigurationManager.AppSettings["RegistroBascula"].ToString(), Convert.ToInt32(Session["empresa"]));
                    dvLaborCargador.RowFilter = "claseLabor=3 and empresa =" + Session["empresa"].ToString();
                    this.ddlLaborCargadores.DataSource = dvLaborCargador;
                    this.ddlLaborCargadores.DataValueField = "codigo";
                    this.ddlLaborCargadores.DataTextField = "descripcion";
                    this.ddlLaborCargadores.DataBind();
                    this.ddlLaborCargadores.Items.Insert(0, new ListItem("", ""));
                }
                catch (Exception ex)
                {
                    ManejoError("Error al cargar Novedades. Correspondiente a: " + ex.Message, "C");
                }

                try
                {
                    this.ddlCuadrillaCargue.DataSource = CcontrolesUsuario.OrdenarEntidadyActivos(CentidadMetodos.EntidadGet("nCuadrilla", "ppa"), "descripcion", Convert.ToInt16(this.Session["empresa"])); ;
                    this.ddlCuadrillaCargue.DataValueField = "codigo";
                    this.ddlCuadrillaCargue.DataTextField = "descripcion";
                    this.ddlCuadrillaCargue.DataBind();
                    this.ddlCuadrillaCargue.Items.Insert(0, new ListItem("Seleccione una opción", ""));
                }
                catch (Exception ex)
                {
                    ManejoError("Error al cargar Cuadrilla. Correspondiente a: " + ex.Message, "C");
                }

            }
            else
            {
                ddlLaborCargadores.Enabled = false;
                selTerceroCargue.Visible = false;
                lbFechaCargadores.Enabled = false;
                txtFechaCargadores.Enabled = false;
                this.ddlLaborTransporte.DataSource = null;
                ddlLaborTransporte.DataBind();
                txtFechaCargadores.Text = "";
                ddlCuadrillaCargue.Enabled = false;
            }
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
                return;
            }

            try
            {
                verificaPeriodoCerrado(Convert.ToInt32(Convert.ToDateTime(txtFecha.Text).Year),
                       Convert.ToInt32(Convert.ToDateTime(txtFecha.Text).Month), Convert.ToInt16(this.Session["empresa"]), Convert.ToDateTime(txtFecha.Text));
                ddlFinca.Enabled = true;
                txtRemision.Focus();
            }
            catch
            {
            }
        }



        protected void gvTransaccion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTransaccion.PageIndex = e.NewPageIndex;
            BusquedaTransaccion();
            gvTransaccion.DataBind();
        }

        protected void txvJornalesD_TextChanged(object sender, EventArgs e)
        {
            LiquidaTransaccioin(0, 0);
            ddlLote.Enabled = true;
            ddlFinca.Enabled = true;
            ddlFinca.Enabled = true;
            ddlSeccion.Enabled = true;
        }
        protected void txvRacimoG_TextChanged(object sender, EventArgs e)
        {
            LiquidaTransaccioin(0, 0);
            ddlLote.Enabled = true;
            ddlFinca.Enabled = true;
            ddlFinca.Enabled = true;
            ddlSeccion.Enabled = true;
        }
        #endregion eventos
    }
}