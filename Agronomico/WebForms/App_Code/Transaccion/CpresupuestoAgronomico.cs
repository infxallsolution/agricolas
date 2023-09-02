using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Transaccion
{
    public class CpresupuestoAgronomico
    {
        public CpresupuestoAgronomico()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public DataView getNivelAplicacionCosecha(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros("spGetNivelAplicacionCosecha", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }


        public DataView getNivelAplicacionActividad(string actividad, int empresa)
        {
            string[] iParametros = new string[] { "@actividad", "@empresa" };
            object[] objValores = new object[] { actividad, empresa };

            return Cacceso.DataSetParametros("spGetNivelAplicacionActividad", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

    }
}