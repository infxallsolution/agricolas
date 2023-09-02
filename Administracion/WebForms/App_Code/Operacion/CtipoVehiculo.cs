using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Administracion.WebForms.App_Code.Operacion
{
    public class CtipoVehiculo
    {
        public CtipoVehiculo()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet("bTipoVehiculo", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "(codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";

            return dvEntidad;
        }

    }
}