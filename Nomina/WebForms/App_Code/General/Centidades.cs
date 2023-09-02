using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.General
{
    public class Centidades
    {


        public Centidades()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataView GetCamposEntidades(string id1, string id2)
        {
            string[] iParametros = new string[] { "@id1", "@id2" };
            object[] objValores = new object[] { id1, id2 };

            return Cacceso.DataSetParametros(
                "spSeleccionaCamposEntidadesII",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }
    }
}