using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class Clog
    {
        public Clog()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public DataView RetornaEncabezadoSNominaLog(int empresa, DateTime fechaInicial, DateTime fechaFinal, string filtro)
        {
            string[] iParametros = new string[] { "@empresa", "@fechaInicial", "@fechaFinal", "@filtro" };
            object[] objValores = new object[] { empresa, fechaInicial, fechaFinal, filtro };
            return Cacceso.DataSetParametros("spRetornaEncabezadoSNominaLog", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView RetornaDetalleSNominaLog(int empresa, int id)
        {
            string[] iParametros = new string[] { "@empresa", "@id" };
            object[] objValores = new object[] { empresa, id };

            return Cacceso.DataSetParametros("spRetornaDetalleSNominaLog", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
    }
}