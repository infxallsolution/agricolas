using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class CcontratistaSS
    {
        public CcontratistaSS()
        {
            //
            // TODO: Add constructor logic here
            //
        }



        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet("nContratistaSeguridadSocial", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and descripcion like '%" + texto + "%'";

            return dvEntidad;
        }


    }
}