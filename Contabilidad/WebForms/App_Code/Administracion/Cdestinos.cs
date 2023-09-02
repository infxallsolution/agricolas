using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.Administracion
{
    public class Cdestinos
    {

        public Cdestinos()
        {
            //
            // TODO: Add constructor logic here
            //
        }



        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "iDestino",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "empresa=" + empresa.ToString() + " and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%'";

            return dvEntidad;
        }

        public DataView BuscarEntidadNivel(string texto)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "iNivelDestino",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "descripcion like '%" + texto + "%'";

            return dvEntidad;
        }

        public DataView GetDestinoNivel(int nivel, int empresa)
        {
            string[] iParametros = new string[] { "@nivel", "@empresa" };
            object[] objValores = new object[] { nivel, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaDestinoNivel",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }
    }
}