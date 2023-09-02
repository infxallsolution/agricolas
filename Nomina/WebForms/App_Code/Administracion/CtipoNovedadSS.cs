using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class CtipoNovedadSS
    {
        public CtipoNovedadSS()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet("nParametroNovedadSeguridadSocial", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and  tipoNovedad like '%" + texto + "%'";

            return dvEntidad;
        }
    }
}