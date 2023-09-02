using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Administracion.WebForms.App_Code.Administracion
{
    public class Ctiquete
    {
        public Ctiquete()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        public int VerificaTipoEntradaExtractora(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipoTransaccion", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipoTransaccion, empresa };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spVerificaTipoEntradaExtractora",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int VerificaTipoSalidaExtractora(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipoTransaccion", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipoTransaccion, empresa };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spVerificaTipoSalidaExtractora",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView SeleccionaProductoMovimiento(int producto, int empresa, string modulo)
        {
            string[] iParametros = new string[] { "@producto", "@empresa", "@modulo" };
            object[] objValores = new object[] { producto, empresa, modulo };

            return Cacceso.DataSetParametros(
                "SpSeleccionaMovimientosProduccion",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }
        public DataView SeleccionaProveedorTercero(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaProveedorTercero",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

    }
}