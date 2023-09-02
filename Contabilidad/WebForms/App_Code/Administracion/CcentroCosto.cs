using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.Administracion
{
    public class CcentroCosto
    {
        public CcentroCosto()
        {
        }




        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();
            dvEntidad = CentidadMetodos.EntidadGet("cCentrosCosto", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa = " + Convert.ToString(empresa) + "and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";
            return dvEntidad;
        }

        public DataView BuscarEntidadContable(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();
            dvEntidad = CentidadMetodos.EntidadGet("cCentrosCostoContable", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa = " + Convert.ToString(empresa) + "and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";
            return dvEntidad;
        }

        public DataView SeleccionaCcosto(int empresa, int auxiliar)
        {
            string[] iParametros = new string[] { "@empresa", "@auxiliar" };
            object[] objValores = new object[] { empresa, auxiliar };
            return Cacceso.DataSetParametros("spSeleccionaCcosto", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView BuscarEntidadNivel(string texto)
        {
            DataView dvEntidad = new DataView();
            dvEntidad = CentidadMetodos.EntidadGet("iNivelDestino", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "descripcion like '%" + texto + "%'";
            return dvEntidad;
        }

        public DataView CentroCostoNivel(string nivel, int empresa)
        {
            string[] iParametros = new string[] { "@nivel", "@empresa" };
            object[] objValores = new object[] { nivel, empresa };
            return Cacceso.DataSetParametros("spSeleccionaCentroCostoNivelContable", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

    }
}