using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class Cafc
    {
        public Cafc()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }




        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "nEntidadAfc",
                "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";

            return dvEntidad;
        }
    }
}