using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.GestionHumana
{
    public class Cprospecto
    {
        public Cprospecto()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = CentidadMetodos.EntidadGet("nProspecto", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa =" + empresa.ToString() + " and (identificacion like '%" + texto + "%' or nombreTercero like '%" + texto + "%')";
            return dvEntidad;
        }


        public DataView RetornaDatosProspecto(string tercero, int contrato, int empresa)
        {
            string[] iParametros = new string[] { "@tercero", "@contrato", "@empresa" };
            object[] objValores = new object[] { tercero, contrato, empresa };

            return Cacceso.DataSetParametros("SpGetnProspectokey", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }


        public DataView SelecccionaContratosTercero(int tercero, int empresa)
        {
            string[] iParametros = new string[] { "@tercero", "@empresa" };
            object[] objValores = new object[] { tercero, empresa };
            return Cacceso.DataSetParametros("spSeleccionaContratoTercero", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
    }
}