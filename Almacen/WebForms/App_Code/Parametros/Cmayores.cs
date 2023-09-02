using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Almacen.WebForms.App_Code.Parametros
{
    public class Cmayores
    {
        public Cmayores()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }



        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "iMayorItem",
                "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and  descripcion like '%" + texto + "%' and produccion=0";

            return dvEntidad;
        }

        public string Consecutivo(string planes, int empresa)
        {
            string[] iParametros = new string[] { "@planes", "@empresa" };
            string[] oParametros = new string[] { "@consecutivo" };
            object[] objValores = new object[] { planes, empresa };

            return Convert.ToString(Cacceso.ExecProc(
                "spConsecutivoMayorItems",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

    }
}