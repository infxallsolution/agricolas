using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.Administracion
{
    public class CConceptoIR
    {
        public CConceptoIR()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public DataView seleccionaValoresClase(int clase, int empresa)
        {
            string[] iParametros = new string[] { "@clase", "@empresa" };
            object[] objValores = new object[] { clase, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaValoresClase",
                iParametros,
                objValores,
                        "ppa").Tables[0].DefaultView;
        }


        public DataView BuscarEntidad(string texto, int empresa)
        {


            DataView dvEntidad = CentidadMetodos.EntidadGet(
                "cConceptoIR",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "empresa =" + empresa.ToString() + " and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";

            return dvEntidad;
        }
    }
}