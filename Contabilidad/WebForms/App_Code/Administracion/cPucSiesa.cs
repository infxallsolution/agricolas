using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.Administracion
{
    public class cPucSiesa
    {


        public cPucSiesa()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public DataView RetornaPucSiesa(int empresa, string nivel1, string nivel2, string nivel3, string nivel4)
        {
            string[] iParametros = new string[] { "@empresa", "@nivel1", "@nivel2", "@nivel3", "@nivel4" };
            object[] objValores = new object[] { empresa, nivel1, nivel2, nivel3, nivel4 };

            return Cacceso.DataSetParametros(
                "spRetornaPucSiesa",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView RetornaPucSiesaNivel(int empresa, int nivel)
        {
            string[] iParametros = new string[] { "@empresa", "@nivel" };
            object[] objValores = new object[] { empresa, nivel };

            return Cacceso.DataSetParametros(
                "spRetornaPucSiesaNivel",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView retornaCcostosSiesa(int empresa, string cuenta)
        {
            string[] iParametros = new string[] { "@empresa", "@cuenta" };
            object[] objValores = new object[] { empresa, cuenta };

            return Cacceso.DataSetParametros(
                "spRetornaPucSiesaNivel",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }




    }
}