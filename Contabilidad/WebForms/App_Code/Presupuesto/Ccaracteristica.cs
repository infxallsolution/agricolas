using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.Presupuesto
{
    public class Ccaracteristica
    {
        public Ccaracteristica()
        {
        }



        public DataView BuscaEntidad(string texto)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "pCaracteristica",
                "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "codigo like '%" + texto + "%' or descripcion like '%" + texto + "%'";

            return dvEntidad;
        }

        public DataView BuscaEntidadClasificacion(string texto)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "pClasificacionCaracteristica",
                "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "codigo like '%" + texto + "%' or descripcion like '%" + texto + "%'";

            return dvEntidad;
        }

        public DataView BuscaEntidadVariable(string texto)
        {
            DataView dvEntidad = CentidadMetodos.EntidadGet(
               "pVariable",
               "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "codigo like '%" + texto + "%' or descripcion like '%" + texto + "%'";


            return dvEntidad;
        }

        public int IndicaCaracteristicaReferencia(string caracteristica)
        {
            string[] iParametros = new string[] { "@caracteristica" };
            string[] oPrametros = new string[] { "@retorno" };
            object[] objValores = new object[] { caracteristica };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spRetornaReferenciaCaracteristicaP",
                iParametros,
                oPrametros,
                objValores,
                "ppa").GetValue(0));
        }

        public string RetornaEntidadCaracteristica(string caracteristica)
        {
            string[] iParametros = new string[] { "@caracteristica" };
            string[] oParametros = new string[] { "@entidad" };
            object[] objValores = new object[] { caracteristica };

            return Convert.ToString(Cacceso.ExecProc(
                "spRetornaEntidadReferenciaCaracteristicaP",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public string RetornaUmedida(string caracteristica)
        {
            string[] iParametros = new string[] { "@caracteristica" };
            string[] oParametros = new string[] { "@uMedida" };
            object[] objValores = new object[] { caracteristica };

            return Convert.ToString(Cacceso.ExecProc(
                "spRetornaUmedidaCaracteristicaP",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public string RetornaTipo(string caracteristica)
        {
            string[] iParametros = new string[] { "@caracteristica" };
            string[] oParametros = new string[] { "@tipo" };
            object[] objValores = new object[] { caracteristica };

            return Convert.ToString(Cacceso.ExecProc(
                "spRetornaTipoCaracteristicaP",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public string RetornaUmedidaVariable(string variable)
        {
            string[] iParametros = new string[] { "@variable" };
            string[] oParametros = new string[] { "@uMedida" };
            object[] objValores = new object[] { variable };

            return Convert.ToString(Cacceso.ExecProc(
                "spRetornaUmedidaVariableP",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public string RetornaUmedidaAnalisis(string analisis, int empresa)
        {
            string[] iParametros = new string[] { "@variable", "@empresa" };
            string[] oParametros = new string[] { "@uMedida" };
            object[] objValores = new object[] { analisis, empresa };

            return Convert.ToString(Cacceso.ExecProc(
                "spRetornaUmedidaAnalisisP",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataSet GetMovimientoProductoContable(string producto, string periodicidad, string tipotransaccion, int empresa, string modulo)
        {
            string[] iParametros = new string[] { "@producto", "@empresa", "@modulo", "@periodicidad", "@tipoTransaccion" };
            object[] objValores = new object[] { producto, empresa, modulo, periodicidad, tipotransaccion };

            return Cacceso.DataSetParametros(
                "spSeleccionaMovimientosProductoContable",
                iParametros,
                objValores,
                "ppa");
        }
    }
}