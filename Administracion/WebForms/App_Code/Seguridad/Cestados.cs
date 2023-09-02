using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Administracion.WebForms.App_Code.Seguridad
{
    public class Cestados
    {
        public Cestados()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataView BuscarEntidad(string texto)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "sEstados",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "estado like '%" + texto + "%' or descripcion like '%" + texto + "%'";

            return dvEntidad;
        }
    }
}