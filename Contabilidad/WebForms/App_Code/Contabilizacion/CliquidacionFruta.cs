using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.Contabilizacion
{
    public class CliquidacionFruta
    {
        public CliquidacionFruta()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet("cLiquidacionFruta", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and numero like '%" + texto + "%'";

            return dvEntidad;
        }


        public void LiquidacionFruta(string productoreferencia, string producto, int empresa, string formaLiquidacion, DateTime fechaInicial, DateTime fechaFinal, string grupoProveedor, string proveedor, decimal precioReferencia,
          decimal pFrutaP, decimal pFrutaS, decimal pFrutaFN, decimal pFrutaN, decimal vFrutaP, decimal vFrutaS, decimal vFrutaFN, decimal vFrutaN, string surcursal, out int retorno, out string nombreTercero)
        {
            string[] iParametros = new string[] { "@productoreferencia", "@producto", "@empresa", "@formaLiquidacion", "@fechaInicial", "@fechaFinal", "@grupoProveedor", "@proveedor", "@precioReferencia", "@pFrutaP", "@pFrutaS", "@pFrutaFN", "@pFrutaN", "@vFrutaP", "@vFrutaS", "@vFrutaFN", "@vFrutaN", "@sucursal" };
            object[] objValores = new object[] { productoreferencia, producto, empresa, formaLiquidacion, fechaInicial, fechaFinal, grupoProveedor, proveedor, precioReferencia, pFrutaP, pFrutaS, pFrutaFN, pFrutaN, vFrutaP, vFrutaS, vFrutaFN, vFrutaN, surcursal };
            string[] oParametros = new string[] { "@retorno", "@nombreTercero" };
            object[] rerotnos = Cacceso.ExecProc("spLiquidacionFruta", iParametros, oParametros, objValores, "ppa");
            retorno = Convert.ToInt16(rerotnos.GetValue(0));
            nombreTercero = Convert.ToString(rerotnos.GetValue(1));
        }

        public void LiquidacionFrutaDefinitiva(string tipo, string numero, string usuario, string observacion, DateTime fecha, string productoReferencia, string producto,
            int empresa, string formaLiquidacion, DateTime fechaInicial, DateTime fechaFinal, string grupoProveedor, string proveedor, decimal precioReferencia,
          decimal pFrutaP, decimal pFrutaS, decimal pFrutaFN, decimal pFrutaN, decimal vFrutaP, decimal vFrutaS, decimal vFrutaFN, decimal vFrutaN, string sucursal, out int retorno, out string nombreTercero)
        {
            string[] iParametros = new string[] { "@empresa", "@formaLiquidacion", "@fechaInicial", "@fechaFinal", "@grupoProveedor", "@proveedor", "@precioReferencia", "@pFrutaP", "@pFrutaS", "@pFrutaFN", "@pFrutaN", "@vFrutaP", "@vFrutaS", "@vFrutaFN", "@vFrutaN",
        "@tipotransaccion","@numero","@usuario","@observacion","@fecha","@productoReferencia","@producto","@sucursal"};
            object[] objValores = new object[] { empresa, formaLiquidacion, fechaInicial, fechaFinal, grupoProveedor, proveedor, precioReferencia, pFrutaP, pFrutaS, pFrutaFN, pFrutaN, vFrutaP, vFrutaS, vFrutaFN, vFrutaN,
        tipo, numero, usuario, observacion, fecha, productoReferencia, producto,sucursal  };
            string[] oParametros = new string[] { "@retorno", "@nombreTercero" };
            object[] rerotnos = Cacceso.ExecProc("spLiquidacionFrutaDefinittiva", iParametros, oParametros, objValores, "ppa");
            retorno = Convert.ToInt16(rerotnos.GetValue(0));
            nombreTercero = Convert.ToString(rerotnos.GetValue(1));
        }





    }
}