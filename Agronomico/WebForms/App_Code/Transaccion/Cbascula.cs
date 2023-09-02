using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Transaccion
{
    public class Cbascula
    {
        public Cbascula()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }



        public DataView SeleccionaTiquetesBascula(string tiquete, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@tiquete" };
            object[] objValores = new object[] { empresa, tiquete };

            return Cacceso.DataSetParametros(
                "spSelecciontaEntradaMateriaPrimaLabor",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView SeleccionaTiquetesBasculaExtractora(int extractora, int empresa, string tiquete)
        {
            string[] iParametros = new string[] { "@extractora", "@empresa", "@tiquete" };
            object[] objValores = new object[] { extractora, empresa, tiquete };

            return Cacceso.DataSetParametros(
                "spSeleccionaTiquetesBasculaExtractora",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }






    }
}