using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Almacen.WebForms.App_Code.Transaccion
{
    public class Ctransacciones
    {
        public Ctransacciones()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public DataView GetCamposEntidades(string id1, string id2)
        {
            string[] iParametros = new string[] { "@id1", "@id2" };
            object[] objValores = new object[] { id1, id2 };
            return Cacceso.DataSetParametros("spSeleccionaCamposEntidadesII", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }


        public DataView SeleccionaFuncionariosSinCuadrilla(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros("spSeleccionaFuncionarioSinCuadrilla", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView GetOrdenCompra(string tipo, string requisicion, int empresa)
        {
            string[] iParametros = new string[] { "@solicitud", "@empresa", "@tipo" };
            object[] objValores = new object[] { requisicion, empresa, tipo };

            return Cacceso.DataSetParametros("spSeleccionaOrdenesCompraSolicitud", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView GetPorAprobar(string usuario, int empresa)
        {
            string[] iParametros = new string[] { "@usuario", "@empresa" };
            object[] objValores = new object[] { usuario, empresa };

            return Cacceso.DataSetParametros("spSeleccionaOcAprobar", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView SeleccionaEstudioComprasSolicitud(string solicitud, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@solicitud" };
            object[] objValores = new object[] { empresa, solicitud };
            return Cacceso.DataSetParametros("spSeleccionaEstudioComprasRequisicion", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public int InsertaOCO(string tipoRef, string numeroRef, string tipo, string numero, string periodo, int registro, string producto, bool ejecutado, string tipoEje, string numeroEje, int empresa)
        {
            string[] iParametros = new string[] { "@tipoReferencia", "@numeroReferencia", "@tipo", "@numero", "@periodo", "@registro", "@producto", "@ejecutado", "@tipoEjecutar", "@numeroEjecutar", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipoRef, numeroRef, tipo, numero, periodo, registro, producto, ejecutado, tipoEje, numeroEje, empresa };

            return Convert.ToInt16(Cacceso.ExecProc("spGuardaOrdenCompra", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public DataView GetCotizacionProveedorProducto(string requisicion, string proveedor, string producto, string cotizacion, int empresa)
        {
            string[] iParametros = new string[] { "@solicitud", "@proveedor", "@producto", "@cotizacion", "@empresa" };
            object[] objValores = new object[] { requisicion, proveedor, producto, cotizacion, empresa };

            return Cacceso.DataSetParametros("spSeleccionaCotizacionProductoProveedor", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView GetCotizacionProveedorImpuesto(string requisicion, string proveedor, string producto, string cotizacion, int empresa)
        {
            string[] iParametros = new string[] { "@solicitud", "@proveedor", "@producto", "@cotizacion", "@empresa" };
            object[] objValores = new object[] { requisicion, proveedor, producto, cotizacion, empresa };

            return Cacceso.DataSetParametros("spSeleccionaCotizacionImpuesto", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView SeleccionaEstudioComprasDetalle(string solicitud, string tercero, string sucursal, int empresa)
        {
            string[] iParametros = new string[] { "@solicitud", "@tercero", "@sucursal", "@empresa" };
            object[] objValores = new object[] { solicitud, tercero, sucursal, empresa };

            return Cacceso.DataSetParametros("spSeleccionaEstudioComprasDetalleSolicitud", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public int AnulaTransaccion(int empresa, string tipo, string numero, string usuario)
        {
            string[] iParametros = new string[] { "@empresa", "@tipo", "@numero", "@usuario" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, tipo, numero, usuario };
            return Convert.ToInt16(Cacceso.ExecProc("spAnulaTransaccionInventario", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }
        public DataView GetTransaccionSolicitudes(string where, int empresa)
        {
            string[] iParametros = new string[] { "@where", "@empresa" };
            object[] objValores = new object[] { where, empresa };
            return Cacceso.DataSetParametros("spSeleccionaTransaccionCompletaInventario", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView GetTransaccionCotizaciones(string where, int empresa)
        {
            string[] iParametros = new string[] { "@where", "@empresa" };
            object[] objValores = new object[] { where, empresa };
            return Cacceso.DataSetParametros("spSeleccionaTransaccionCotizaciones", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public int VerificaEdicionBorrado(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, empresa };
            return Convert.ToInt16(Cacceso.ExecProc("spVerificaEdicionBorradoAlmacen", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public object[] RetornaTerceroSucursal(int empresa, string numero)
        {
            string[] iParametros = new string[] { "@empresa", "@numero" };
            string[] oParametros = new string[] { "@proveedor", "@sucursal" };
            object[] objValores = new object[] { empresa, numero };
            object[] objSalidas = new object[2];

            objSalidas[0] = Cacceso.ExecProc("spRetornaTerceroSucursal", iParametros, oParametros, objValores, "ppa").GetValue(0);
            objSalidas[1] = Cacceso.ExecProc("spRetornaTerceroSucursal", iParametros, oParametros, objValores, "ppa").GetValue(1);
            return objSalidas;
        }

        public string RetornaNumeroTransaccion(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipoTransaccion", "@empresa" };
            string[] oParametros = new string[] { "@consecutivo" };
            object[] objValores = new object[] { tipoTransaccion, empresa };
            return Convert.ToString(Cacceso.ExecProc("spRetornaConsecutivoTransaccion", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public int ActualizaConsecutivo(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipoTransaccion, empresa };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spActualizaConsecutivoTransaccion",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView GetSaldoTotalProducto(string producto, int empresa)
        {
            string[] iParametros = new string[] { "@producto", "@empresa" };
            object[] objValores = new object[] { producto, empresa };

            return Cacceso.DataSetParametros(
                "spSaldoProductoTotal",
                iParametros,
                objValores,
                "planta").Tables[0].DefaultView;
        }

        public DataView SeleccionaOCEncabezadoActivas(int empresa, string sucursal, int tercero, string tipotransaccion)
        {
            string[] iParametros = new string[] { "@tercero", "@empresa", "@sucursal", "@tipoTransaccion" };
            object[] objValores = new object[] { tercero, empresa, sucursal, tipotransaccion };

            return Cacceso.DataSetParametros("spSeleccionaOCEncabezadoActivas", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView SeleccionaOCDetalleActivas(int empresa, string numero)
        {
            string[] iParametros = new string[] { "@empresa", "@numero" };
            object[] objValores = new object[] { empresa, numero };

            return Cacceso.DataSetParametros("spSeleccionaOCDetalleActivas", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView SeleccionaREQDetalleActivas(int empresa, string numero)
        {
            string[] iParametros = new string[] { "@empresa", "@numero" };
            object[] objValores = new object[] { empresa, numero };

            return Cacceso.DataSetParametros("spSeleccionaREQDetalleActivas", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public string RetornaSolicitante(string numero, int empresa)
        {
            string[] iParametros = new string[] { "@numero", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { numero, empresa };
            return Convert.ToString(Cacceso.ExecProc("spRetornaSolicitante", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }
        public DataView SeleccionaREQEncabezadoActivas(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros("spSeleccionaREQEncabezadoActivas", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView SeleccionaBodegaSaldoItem(int empresa, int producto)
        {
            string[] iParametros = new string[] { "@empresa", "@producto" };
            object[] objValores = new object[] { empresa, producto };

            return Cacceso.DataSetParametros("spSeleccionaBodegaSaldoItem", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public decimal SeleccionaBodegaSaldoItemCosto(int empresa, string producto, string bodega)
        {
            string[] iParametros = new string[] { "@empresa", "@producto", "@bodega" };
            string[] oParametros = new string[] { "@valorUnitario" };
            object[] objValores = new object[] { empresa, producto, bodega };
            string valor = Cacceso.ExecProc(
                "spSeleccionaBodegaSaldoItemCosto",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0).ToString();
            return Convert.ToDecimal(valor);
        }
        public decimal SeleccionaBodegaSaldoItemCantidad(int empresa, string producto, string bodega)
        {
            string[] iParametros = new string[] { "@empresa", "@producto", "@bodega" };
            string[] oParametros = new string[] { "@saldo" };
            object[] objValores = new object[] { empresa, producto, bodega };

            return Convert.ToDecimal(Cacceso.ExecProc(
                "spSeleccionaBodegaSaldoItemCantidad",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }
        public DataView GetCamposEA(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };
            return Cacceso.DataSetParametros("SpSeleccionaCamposConsultaEA", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView GetTransaccionEntradas(string where, int empresa)
        {
            string[] iParametros = new string[] { "@where", "@empresa" };
            object[] objValores = new object[] { where, empresa };
            return Cacceso.DataSetParametros("spSeleccionaTransaccionCompletaEA", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView GetTransaccionEntradasDirectas(string where, int empresa)
        {
            string[] iParametros = new string[] { "@where", "@empresa" };
            object[] objValores = new object[] { where, empresa };
            return Cacceso.DataSetParametros("spSeleccionaTransaccionCompletaED", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView GetCamposREQ(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };
            return Cacceso.DataSetParametros("SpSeleccionaCamposConsultaREQ", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView GetTransaccionReq(string where, int empresa)
        {
            string[] iParametros = new string[] { "@where", "@empresa" };
            object[] objValores = new object[] { where, empresa };
            return Cacceso.DataSetParametros("spSeleccionaTransaccionCompletaREQ", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView GetCamposSA(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };
            return Cacceso.DataSetParametros("SpSeleccionaCamposConsultaSA", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView GetTransaccionSA(string where, int empresa)
        {
            string[] iParametros = new string[] { "@where", "@empresa" };
            object[] objValores = new object[] { where, empresa };
            return Cacceso.DataSetParametros("spSeleccionaTransaccionCompletaSA", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView GetTransaccionDVA(string where, int empresa)
        {
            string[] iParametros = new string[] { "@where", "@empresa" };
            object[] objValores = new object[] { where, empresa };
            return Cacceso.DataSetParametros("spSeleccionaTransaccionCompletaDVA", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView GetTransaccionDVP(string where, int empresa)
        {
            string[] iParametros = new string[] { "@where", "@empresa" };
            object[] objValores = new object[] { where, empresa };
            return Cacceso.DataSetParametros("spSeleccionaTransaccionCompletaDVP", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView GetTransaccionesInventarioEncabezado(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };
            return Cacceso.DataSetParametros("spGetTransaccionesInventarioEncabezado", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView GetTransaccionesInventarioDetalle(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };
            return Cacceso.DataSetParametros("spGetTransaccionesInventarioDetalle", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView GetTransaccionesInventarioImpuesto(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };
            return Cacceso.DataSetParametros("spGetTransaccionesInventarioImpuesto", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView SeleccionaSAEncabezadoActivas(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros("spSeleccionaSAEncabezadoActivas", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView SeleccionaEAEncabezadoActivas(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros("spSeleccionaEAEncabezadoActivas", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView SeleccionaSADetalleActivas(int empresa, string numero)
        {
            string[] iParametros = new string[] { "@empresa", "@numero" };
            object[] objValores = new object[] { empresa, numero };

            return Cacceso.DataSetParametros("spSeleccionaSADetalleActivas", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView SeleccionaEADetalleActivas(int empresa, string numero)
        {
            string[] iParametros = new string[] { "@empresa", "@numero" };
            object[] objValores = new object[] { empresa, numero };

            return Cacceso.DataSetParametros("spSeleccionaEADetalleActivas", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public String SeleccionaFomatoTransaccion(int empresa, string tipo)
        {
            string[] iParametros = new string[] { "@empresa", "@tipo" };
            object[] objValores = new object[] { empresa, tipo };
            string[] oParametros = new string[] { "@formato" };

            return Convert.ToString(Cacceso.ExecProc(
                "spSeleccionaFomatoTransaccion",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }
        public int contabilizarEntradas(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spContabilizarEntradas",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }
        public int contabilizarSalidas(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spContabilizarSalidas",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }
        public int contabilizarDevolucionesAlmacen(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spContabilizarDevoluciones",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }
        public int contabilizarDevolucionesProveedor(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spContabilizarDevolucionesProveedor",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }
        public DataView ccostoDestino(string destino, int empresa)
        {

            string[] iParametros = new string[] { "@destino", "@empresa" };
            object[] objValores = new object[] { destino, empresa };

            return Cacceso.DataSetParametros("spSeleccionaCcostoDestinoGasto", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

    }
}