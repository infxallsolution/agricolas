using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class CestruturaCC
    {
        public CestruturaCC()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }



        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = CentidadMetodos.EntidadGet(
                "cEstructuraCCosto",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "empresa =" + empresa + " and descripcion like '%" + texto + "%'";

            return dvEntidad;
        }


        public string Consecutivo(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            string[] oParametros = new string[] { "@consecutivo" };
            object[] objValores = new object[] { empresa };

            return Convert.ToString(Cacceso.ExecProc(
                "spConsecutivoEstructuraCC",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

    }
}