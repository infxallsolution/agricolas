using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Almacen.WebForms.App_Code.Parametros
{
    public class Cdestino
    {
        public Cdestino()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }




        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "iDestino",
                "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and  (descripcion like '%" + texto + "%'  or codigo  like '% " + texto + "%')";

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