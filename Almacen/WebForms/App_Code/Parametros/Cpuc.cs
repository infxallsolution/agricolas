using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Almacen.WebForms.App_Code.Parametros
{
    public class Cpuc
    {
        public Cpuc()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        public DataView GetPuc(string tipo, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@empresa" };
            object[] objValores = new object[] { tipo, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaPucTipo",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }
    }
}