using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.General
{
    public class Cgeneral
    {
        public Cgeneral()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public DataView retornaDatosReporteador(string modulo)
        {
            string[] iParametros = new string[] { "@modulo" };
            object[] objValores = new object[] { modulo };

            return Cacceso.DataSetParametros("spRetornaDatosReporte", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public string RetornoParametroGeneral(string campo, int empresa)
        {
            DataView dvEntidad = new DataView();
            string[] iParametros = new string[] { "@campo", "@empresa" };
            object[] objValores = new object[] { campo, empresa };
            dvEntidad = Cacceso.DataSetParametros("spRetornovalorParametrosGeneral", iParametros, objValores, "ppa").Tables[0].DefaultView;
            return dvEntidad.Table.Rows[0].ItemArray[0].ToString();
        }

        static public string RetornaConsecutivoAutomatico(string tabla, string nombreCampo, int empresa)
        {
            DataView dvEntidad = new DataView();
            string[] iParametros = new string[] { "@tabla", "@campo", "@empresa" };
            object[] objValores = new object[] { tabla, nombreCampo, empresa };
            dvEntidad = Cacceso.DataSetParametros("spRetornaConsecutivoAutomatico", iParametros, objValores, "ppa").Tables[0].DefaultView;
            return dvEntidad.Table.Rows[0].ItemArray[0].ToString();
        }
    }
}