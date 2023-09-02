using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class cHuella
    {
        public cHuella() { }

        public static int InsertaHuella(int empresa, string funcionario, Byte[] huella, string tipohuella)
        {
            string[] iParametros = new string[] { "@empresa", "@funcionario", "@huella", "@tipoHuella" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, funcionario, huella, tipohuella };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spInsertaHuellaFuncionario",
                iParametros,
                oParametros,
                objValores, "ppa").GetValue(0));
        }

        public static int eliminaHuella(int empresa, string funcionario, string tipohuella)
        {
            string[] iParametros = new string[] { "@empresa", "@funcionario", "@tipoHuella" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, funcionario, tipohuella };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spEliminaHuellaFuncionario",
                iParametros,
                oParametros,
                objValores, "ppa").GetValue(0));
        }


        public static DataView retornaHuellas(int empresa)
        {

            string[] iParametros = new string[] { "@empresa" };
            string[] oParametros = new string[] { };
            object[] objValores = new object[] { empresa };

            var retorno = Cacceso.DataSetParametros(
                "spRetornaTodasHuellas",
                iParametros,
                objValores, "ppa").Tables[0].DefaultView as DataView;
            return retorno;
        }


    }
}