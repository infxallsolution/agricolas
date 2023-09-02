using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class CcentroCosto
    {
        public CcentroCosto()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }



        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "cCentrosCosto",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "empresa = " + Convert.ToString(empresa) + "and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";

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

        public DataView CentroCostoNivel(int nivel, int empresa)
        {
            string[] iParametros = new string[] { "@nivel", "@empresa" };
            object[] objValores = new object[] { nivel, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaCentroCostoNivel",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

    }
}