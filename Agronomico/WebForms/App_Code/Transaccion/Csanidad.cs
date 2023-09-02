using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Transaccion
{
    public class Csanidad
    {
        public Csanidad()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        public int EnviaCorreo(string numero)
        {
            string[] iParametros = new string[] { "@numero" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { numero };

            return Convert.ToInt32(Cacceso.ExecProc("spEnviarCorreosOCO", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public DataView GetTransaccionCompleta(string where)
        {
            string[] iParametros = new string[] { "@where" };
            object[] objValores = new object[] { where };

            return Cacceso.DataSetParametros(
                "spSeleccionaTransaccionCompletaAlmacen",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public int InsertaReferencia(string tipoRef, string numeroRef, string tipo, string numero, int ano, int mes, int registro, string producto,
            decimal vlUnitario, decimal pIva, decimal pRteFte, decimal vlIva, decimal vlRteFte, decimal vlTotal, decimal vlNeto, decimal cantidad,
            string bodega, int registroRef)
        {
            string[] iParametros = new string[] { "@tiporef", "@numeroRef", "@tipo", "@numero", "@ano", "@mes", "@registro", "@producto", "@vlUnitario", "@pIva", "@vlIva", "@pDescuento", "@valorDescuento", "@vlTotal", "@vlNeto", "@cantidad", "@bodega", "@registroRef" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipoRef, numeroRef, tipo, numero, ano, mes, registro, producto, vlUnitario, pIva, pRteFte, vlIva, vlRteFte, vlTotal, vlNeto, cantidad, bodega, registroRef };

            return Convert.ToInt32(Cacceso.ExecProc("spGuardaReferenciaiTransaccionDetalle", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }


        public int ActualizaMovimientosAlmacen(string tipoRef, string numeroRef, string tipo, string numero, string periodo, int registro, string producto,
           decimal vlUnitario, decimal pIva, decimal pRteFte, decimal vlIva, decimal vlRteFte, decimal vlTotal, decimal vlNeto, decimal cantidad,
           string bodega, int registroRef)
        {
            string[] iParametros = new string[] { "@tiporef", "@numeroRef", "@tipo", "@numero", "@periodo", "@registro", "@producto", "@vlUnitario",
            "@pIva", "@pRteFte", "@vlIva", "@vlRteFte", "@vlTotal", "@vlNeto", "@cantidad", "@bodega", "@registroRef" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipoRef, numeroRef, tipo, numero, periodo, registro, producto, vlUnitario, pIva, pRteFte, vlIva,
            vlRteFte, vlTotal, vlNeto, cantidad, bodega, registroRef };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spGuardaReferenciaiTransaccionDetalle",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int ActualizaCotizacion(string tipo, string numero, int registro, string producto,
           decimal vlUnitario, decimal pIva, decimal vlIva, decimal vlTotal, decimal vlNeto, decimal cantidad)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@registro", "@producto", "@vlUnitario",
            "@pIva", "@valorIva", "@vlTotal", "@vlNeto", "@cantidad" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, registro, producto, vlUnitario, pIva, vlIva,
            vlTotal, vlNeto, cantidad};

            return Convert.ToInt32(Cacceso.ExecProc(
                "SpActualizaiCotizacionDetalle",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public int RetornavalidacionRegistro(string tipoTransaccion)
        {
            string[] iParametros = new string[] { "@tipo" };
            string[] oParametros = new string[] { "@retorna" };
            object[] objValores = new object[] { tipoTransaccion };

            return Convert.ToInt32(Cacceso.ExecProc(
                "SpRetornaDiaSemanaRequisicion",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }



        public int InsertaReferencia(string tipoRef, string numeroRef, string tipo, string numero, string periodo, int registro, string producto,
            bool ejecutado, string tipoEje, string numeroEje)
        {
            string[] iParametros = new string[] { "@tiporef", "@numeroRef", "@tipo", "@numero", "@periodo", "@registro", "@producto", "@ejecutado",
            "@tipoEje", "@numeroEje" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipoRef, numeroRef, tipo, numero, periodo, registro, producto, ejecutado, tipoEje, numeroEje };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spGuardaReferenciaiTransaccionDetalleA",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView GetEstudioCompras(string requisicion)
        {
            string[] iParametros = new string[] { "@requisicion" };
            object[] objValores = new object[] { requisicion };

            return Cacceso.DataSetParametros(
                "spSeleccionaEstudioComprasRequisicion",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetEstudioComprasDetalle(string requisicion, string tercero)
        {
            string[] iParametros = new string[] { "@requisicion", "@tercero" };
            object[] objValores = new object[] { requisicion, tercero };

            return Cacceso.DataSetParametros(
                "spSeleccionaEstudioComprasRequisicionDet",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetSaldoTotalProducto(string producto)
        {
            string[] iParametros = new string[] { "@producto" };
            object[] objValores = new object[] { producto };

            return Cacceso.DataSetParametros(
                "spSaldoProductoTotal",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetSaldoTotalProductoFecha(string producto, DateTime fecha)
        {
            string[] iParametros = new string[] { "@producto", "@fecha" };
            object[] objValores = new object[] { producto, fecha };

            return Cacceso.DataSetParametros(
                "spSaldoProductoTotalFecha",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public DataView RequerimientoSaldos(string producto)
        {
            string[] iParametros = new string[] { "@producto" };
            object[] objValores = new object[] { producto };

            return Cacceso.DataSetParametros(
                "SpSeleccionaRequerimientosPendientesSaldo",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView CompraSaldos(string producto)
        {
            string[] iParametros = new string[] { "@producto" };
            object[] objValores = new object[] { producto };

            return Cacceso.DataSetParametros(
                "SpSeleccionaComprasPendientesSaldo",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView RequisicionSaldos(string producto)
        {
            string[] iParametros = new string[] { "@producto" };
            object[] objValores = new object[] { producto };

            return Cacceso.DataSetParametros(
                "SpSeleccionaRequiPendientesSaldo",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetReqPendiente(string numero)
        {
            string[] iParametros = new string[] { "@numero" };
            object[] objValores = new object[] { numero };

            return Cacceso.DataSetParametros(
                "spSeleccionaReqPendientesDet",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetDvProveedor(string numero)
        {
            string[] iParametros = new string[] { "@numero" };
            object[] objValores = new object[] { numero };

            return Cacceso.DataSetParametros(
                "spSeleccionaEntradasDevProveedorDet",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetDvAlmacen(string numero)
        {
            string[] iParametros = new string[] { "@numero" };
            object[] objValores = new object[] { numero };

            return Cacceso.DataSetParametros(
                "spSeleccionaSalidasDevTerceroDet",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetCotizacionProveedorProducto(string requisicion, string proveedor, string producto, string cotizacion)
        {
            string[] iParametros = new string[] { "@requisicion", "@proveedor", "@producto", "@cotizacion" };
            object[] objValores = new object[] { requisicion, proveedor, producto, cotizacion };

            return Cacceso.DataSetParametros(
                "spSeleccionaCotizacionProductoProveedor",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }



        public decimal RetornaSaldoProductoBodega(string producto, string bodega, DateTime fecha)
        {
            string[] iParametros = new string[] { "@producto", "@bodega", "@fecha" };
            string[] oParametros = new string[] { "@saldo" };
            object[] objValores = new object[] { producto, bodega, fecha };

            return Convert.ToDecimal(Cacceso.ExecProc(
                "spSaldoProductoBodega",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int VerificaEdicionBorrado(string tipo, string numero)
        {
            string[] iParametros = new string[] { "@tipo", "@numero" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spVerificaEdicionBorradoAlmacen",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView GetTotalTransaccion(string tipo, string numero)
        {
            string[] iParametros = new string[] { "@tipo", "@numero" };
            object[] objValores = new object[] { tipo, numero };

            return Cacceso.DataSetParametros(
                "spSeleccionaTotalTrnInv",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView SeleccionaiTransaccionDetalle(string tipo, string numero)
        {
            string[] iParametros = new string[] { "@tipo", "@numero" };
            object[] objValores = new object[] { tipo, numero };

            return Cacceso.DataSetParametros(
                "spSeleccionaiTransaccionDetalle",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetSalidasEdicion(string tipoTransaccion, DateTime fechaI, DateTime fechaF)
        {
            string[] iParametros = new string[] { "@tipo", "@fechaI", "@fechaF" };
            object[] objValores = new object[] { tipoTransaccion, fechaI, fechaF };

            return Cacceso.DataSetParametros(
                "spSeleccionaSalidasEdicion",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public int ActualizaDestinoSalidas(string numero, string producto, string destino, bool inversion, string observacion, int registro, string cuenta, string ccosto)
        {
            string[] iParametros = new string[] { "@numero", "@producto", "@destino", "@inversion", "@observacion", "@registro", "@cuenta", "@ccosto" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { numero, producto, destino, inversion, observacion, registro, cuenta, ccosto };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spActualizaDestinoSalidas",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public void ContabilizaAlmacen(string periodo)
        {
            string[] iParametros = new string[] { "@periodo" };
            string[] oParametros = new string[] { };
            object[] objValores = new object[] { periodo };

            Cacceso.ExecProc(
                "spContabilizaSalidas",
                iParametros,
                oParametros,
                objValores,
                "ppa");
        }

        public DataView GetContable(string periodo)
        {
            string[] iParametros = new string[] { "@periodo" };
            object[] objValores = new object[] { periodo };

            return Cacceso.DataSetParametros(
                "spSeleccionaContableAlmacen",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetSaldosBodegaTotal(string producto, int ano, int mes)
        {
            string[] iParametros = new string[] { "@producto", "@ano", "@mes" };
            object[] objValores = new object[] { producto, ano, mes };

            return Cacceso.DataSetParametros(
                "spSaldoProductoTotalBodega",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public void CalculaSaldoProductoBodega(int ano, int mes, string producto, string bodega, decimal costo)
        {
            string[] iParametros = new string[] { "@ano", "@mes", "@producto", "@bodega", "@costo" };
            object[] objValores = new object[] { ano, mes, producto, bodega, costo };
            string[] oParametros = new string[] { };

            Cacceso.ExecProc(
                "spCalculaiSaldoPeriodoProductoBodegaCosto",
                iParametros,
                oParametros,
                objValores,
                "ppa");
        }

        public void CalculaSaldoProductoB(string producto)
        {
            string[] iParametros = new string[] { "@PRODUCTOCAL" };
            object[] objValores = new object[] { producto };
            string[] oParametros = new string[] { };

            Cacceso.ExecProc(
                "SpcalculaSaldoProductoMovimiento",
                iParametros,
                oParametros,
                objValores,
                "ppa");
        }

        public int InsertaSalida(int ano, int mes, string tipo, string numero, string producto, decimal cantidad, string uMedida, string bodega, string docRef)
        {
            string[] iParametros = new string[] { "@ano", "@mes", "@tipo", "@numero", "@producto", "@cantidad", "@uMedida", "@bodega", "@docRef" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { ano, mes, tipo, numero, producto, cantidad, uMedida, bodega, docRef };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spInsertaSalidaAlmacen",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public void CalculaSaldoMovimiento(int ano, int mes, string producto, string numero, int registro, string tipo)
        {
            string[] iParametros = new string[] { "@ano", "@mes", "@producto", "@numero", "@registro", "@tipo" };
            object[] objValores = new object[] { ano, mes, producto, numero, registro, tipo };
            string[] oParametros = new string[] { };

            Cacceso.ExecProc(
                "SpcalculaSaldoProductoMovimiento",
                iParametros,
                oParametros,
                objValores,
                "ppa");
        }

        public int InsertaConteoFisico1(DateTime fecha, string usuario, string papeleta, string bodega, decimal conteo, string operario)
        {
            string[] iParametros = new string[] { "@fecha", "@usuario", "@papeleta", "@bodega", "@conteo", "@operario" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { fecha, usuario, papeleta, bodega, conteo, operario };

            return Convert.ToInt32(Cacceso.ExecProc(
                "SpActualizaiConteoFisico",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView GetOperarioConteoFecha(DateTime fecha)
        {
            DataView dvOperario = CentidadMetodos.EntidadGet(
                "iConteoOperario",
                "ppa").Tables[0].DefaultView;

            dvOperario.RowFilter = "fecha = '" + fecha + "'";
            dvOperario.Sort = "operario";

            return dvOperario;
        }

        public string RetornaDestinoDetalle(string numero, string producto)
        {
            string[] iParametros = new string[] { "@numero", "@producto" };
            string[] oParametros = new string[] { "@destino" };
            object[] objValores = new object[] { numero, producto };

            return Convert.ToString(Cacceso.ExecProc(
                "spRetornaDestinoDetalleAlmacen",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int AnulaTransaccion(string periodo, string tipo, string numero, string usuario)
        {
            string[] iParametros = new string[] { "@periodo", "@tipo", "@numero", "@usuario" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { periodo, tipo, numero, usuario };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spAnulaTrnAlmacen",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public string RetornaObservacion(string tipo, string numero)
        {
            string[] iParametros = new string[] { "@tipo", "@numero" };
            string[] oParametros = new string[] { "@observacion" };
            object[] objValores = new object[] { tipo, numero };

            return Convert.ToString(Cacceso.ExecProc(
                "spRetornaObservacionTrnAlmacen",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int EditaEncabezado(string periodo, string tipo, string numero, DateTime fecha, string observacion)
        {
            string[] iParametros = new string[] { "@periodo", "@tipo", "@numero", "@fecha", "@observacion" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { periodo, tipo, numero, fecha, observacion };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spEditaEncabezadoAlmacen",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int EditaDetalle(string periodo, string tipo, string numero, int registro, decimal cantidad, bool eliminar)
        {
            string[] iParametros = new string[] { "@periodo", "@tipo", "@numero", "@registro", "@cantidad", "@eliminar" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { periodo, tipo, numero, registro, cantidad, eliminar };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spEditaDetalleAlmacen",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public DataView GetProveedorAC(int empresa)
        {

            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "SpSeleccionaProveedorAC",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;


        }

        public DataView GetPeriodosAbiertos(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "SpSeleccionaPeriodosabiertos",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;


        }

        public DataView SalidasAC(string proveedor, string periodo)
        {

            string[] iParametros = new string[] { "@proveedor", "@periodo" };
            object[] objValores = new object[] { proveedor, periodo };

            return Cacceso.DataSetParametros(
                "SpSeleccionaSalidasACproveedor",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;


        }

        public int EjecutaRequisicion(string periodo, string proveedor, string observacion)
        {
            string[] iParametros = new string[] { "@periodo", "@proveedor", "@observacion" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { periodo, proveedor, observacion };

            return Convert.ToInt32(Cacceso.ExecProc(
                "SpInsertaRequisicionContizaciondeSalidasAC",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView GetDetalleSalida(string numero)
        {
            string[] iParametros = new string[] { "@numero" };
            object[] objValores = new object[] { numero };

            return Cacceso.DataSetParametros(
                "spSeleccionaDetalleSalida",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetDetalleUltimaCompra(string numero)
        {
            string[] iParametros = new string[] { "@numero" };
            object[] objValores = new object[] { numero };

            return Cacceso.DataSetParametros(
                "spSeleccionaUltimasComprasDetalle",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }



    }
}