using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.Administracion
{
    public class CloteCcostoSiigo
    {
        public CloteCcostoSiigo()
        {
        }


        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();
            dvEntidad = CentidadMetodos.EntidadGet("aLoteCcostoSigo", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa = " + Convert.ToString(empresa) + "and ( lote like '%" + texto + "%' and mccostosigo like '%" + texto + "%'" + " or accostosigo like '%" + texto + "%')";
            return dvEntidad;
        }

    }
}