using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Almacen.WebForms.App_Code.Parametros
{
    public class Cplanes
    {
        public Cplanes()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }




        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "iPlanItem",
                "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and  descripcion like '%" + texto + "%' and produccion=0";

            return dvEntidad;
        }

        public string Consecutivo(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            string[] oParametros = new string[] { "@consecutivo" };
            object[] objValores = new object[] { empresa };

            return Convert.ToString(Cacceso.ExecProc(
                "spConsecutivoPlanesItems",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }
    }
}