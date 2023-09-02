using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.GestionHumana
{
    public class Cparentesco

    {
        public Cparentesco()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = CentidadMetodos.EntidadGet("nParentesco", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa =" + empresa.ToString() + " and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";
            return dvEntidad;
        }
    }
}