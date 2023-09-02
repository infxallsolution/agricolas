using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.Administracion
{
    public class CImpRet
    {
        public CImpRet()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        public DataView BuscarEntidad(string texto, int empresa)
        {


            DataView dvEntidad = CentidadMetodos.EntidadGet(
                "cClaseIR",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "empresa =" + empresa.ToString() + "and  (descripcion like '%" + texto + "%')";


            return dvEntidad;
        }

        public DataView ReternaReferenciaImpuesto(string clase, int empresa)
        {
            string[] iParametros = new string[] { "@clase", "@empresa" };
            object[] objValores = new object[] { clase, empresa };

            return Cacceso.DataSetParametros(
                "spRetornaReferenciaImpuesto",
                iParametros,
                objValores,
                        "ppa").Tables[0].DefaultView;
        }

    }
}