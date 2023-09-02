using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Liquidacion
{
    public class CModificacionPrimas
    {
        public CModificacionPrimas() { }

        public DataSet CargarDetallePrima(string empresa, string tipo, string numero)
        {
            string[] iParametros = new string[] { "@empresa", "@numero", "@tipo" };
            object[] objValores = new object[] { empresa, numero, tipo };

            return Cacceso.DataSetParametros(
                "SpSeleccionaDetalleLiquidacionPrima",
                iParametros,
                objValores,
                "ppa");
        }

        public DataRow CargarCabeceraPrima(string empresa, string tipo, string numero)
        {
            string[] iParametros = new string[] { "@empresa", "@numero", "@tipo" };
            object[] objValores = new object[] { empresa, numero, tipo };

            var ds = Cacceso.DataSetParametros(
                "SpSeleccionaCabeceraLiquidacionPrima",
                iParametros,
                objValores,
                "ppa");
            return ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0] : null;
        }

        public DataSet CargarTerceros(string empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            var ds = Cacceso.DataSetParametros(
                "SpSeleccionaTercerosModificacionPrima",
                iParametros,
                objValores,
                "ppa");
            return ds;
        }

        public void GuardarCambios(DataTable table)
        {
            string[] iParametros = new string[] { "@DataTable" };
            string[] oParametros = new string[] { };
            object[] objValores = new object[] { table };

            var ds = Cacceso.ExecProc(
                "SpActualizaDetalleLiquidacionPrima",
                iParametros,
                oParametros,
                objValores,
                "ppa");
        }

        public DataRow CargarInformacionTercero(string empresa, string tercero)
        {
            string[] iParametros = new string[] { "@empresa", "@tercero" };
            object[] objValores = new object[] { empresa, tercero };

            var ds = Cacceso.DataSetParametros(
                "SpGetDetalleTerceroActualizacionLiquidacion",
                iParametros,
                objValores,
                "ppa");
            return ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0] : null;
        }
    }
}