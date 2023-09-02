using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Administracion.WebForms.App_Code.General
{
    public class Cgeneral
    {
        public Cgeneral()
        {
        }

        public DataView spRetornaTipoTransaccionFormatoModulo(int empresa, string modulo)
        {
            DataView dvEntidad = new DataView();
            string[] iParametros = new string[] { "@empresa", "@modulo" };
            object[] objValores = new object[] { empresa, modulo };
            dvEntidad = Cacceso.DataSetParametros("spRetornaTipoTransaccionFormatoModulo", iParametros, objValores, "ppa").Tables[0].DefaultView;
            return dvEntidad;

        }

        public DataView spRetornaModuloTipoTransaccion(int empresa, string tipoTransaccion)
        {
            DataView dvEntidad = new DataView();
            string[] iParametros = new string[] { "@empresa", "@tipotransaccion" };
            object[] objValores = new object[] { empresa, tipoTransaccion };
            dvEntidad = Cacceso.DataSetParametros("spspRetornaModuloTipoTransaccion", iParametros, objValores, "ppa").Tables[0].DefaultView;
            return dvEntidad;

        }

        public DataView retornaDatosReporteador(string modulo)
        {
            string[] iParametros = new string[] { "@modulo" };
            object[] objValores = new object[] { modulo };

            return Cacceso.DataSetParametros(
                "spRetornaDatosReporte",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        static public string RetornaConsecutivoAutomatico(string tabla, string nombreCampo, int empresa)
        {
            DataView dvEntidad = new DataView();
            string[] iParametros = new string[] { "@tabla", "@campo", "@empresa" };
            object[] objValores = new object[] { tabla, nombreCampo, empresa };
            dvEntidad = Cacceso.DataSetParametros("spRetornaConsecutivoAutomatico", iParametros, objValores, "ppa").Tables[0].DefaultView;
            return dvEntidad.Table.Rows[0].ItemArray[0].ToString();

        }

        public string RetornoParametroGeneral(string campo, int empresa)
        {
            DataView dvEntidad = new DataView();
            string[] iParametros = new string[] { "@campo", "@empresa" };
            object[] objValores = new object[] { campo, empresa };
            dvEntidad = Cacceso.DataSetParametros("spRetornovalorParametrosGeneral", iParametros, objValores, "ppa").Tables[0].DefaultView;
            return dvEntidad.Table.Rows[0].ItemArray[0].ToString();
        }

        static public string RetornaConsecutivoAutomaticoSinEmpresa(string tabla, string nombreCampo)
        {
            DataView dvEntidad = new DataView();
            string[] iParametros = new string[] { "@tabla", "@campo" };
            object[] objValores = new object[] { tabla, nombreCampo };
            dvEntidad = Cacceso.DataSetParametros("spRetornaConsecutivoAutomaticoSinEmpresa", iParametros, objValores, "ppa").Tables[0].DefaultView;
            return dvEntidad.Table.Rows[0].ItemArray[0].ToString();
        }
    }
}