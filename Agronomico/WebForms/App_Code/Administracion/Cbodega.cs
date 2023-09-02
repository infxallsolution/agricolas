using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Administracion
{
    public class Cbodega
    {
        public Cbodega()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }



        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet("iBodega", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and  descripcion like '%" + texto + "%'";

            return dvEntidad;
        }


        public DataView SeleccionaAuxiliaresBodega(int empresa, string bodega)
        {
            string[] iParametros = new string[] { "@empresa", "@bodega" };
            object[] objValores = new object[] { empresa, bodega };

            return Cacceso.DataSetParametros(
                 "spSeleccionaAuxiliaresBodega",
                 iParametros,
                 objValores,
                 "ppa").Tables[0].DefaultView;
        }

    }
}