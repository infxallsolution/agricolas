using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Liquidacion
{
    public class Cprestamo
    {
        public Cprestamo()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        public DataView SelecccionaContratosTercero(int tercero, int empresa)
        {
            string[] iParametros = new string[] { "@tercero", "@empresa" };
            object[] objValores = new object[] { tercero, empresa };
            return Cacceso.DataSetParametros("spSeleccionaContratoTercero", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView RetornaDatos(string numero, int empresa)
        {
            string[] iParametros = new string[] { "@codigo", "@empresa" };
            object[] objValores = new object[] { numero, empresa };

            return Cacceso.DataSetParametros("SpGetnPrestamokey", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();
            dvEntidad = CentidadMetodos.EntidadGet("nPrestamo", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (desConcepto like '%" + texto + "%' or desEmpleado like '%" + texto + "%')";

            return dvEntidad;
        }

        public DataView RetornaDatosPrestamo(string codigo, int empresa)
        {
            DataView dvEntidad = new DataView();
            object[] objValores = new object[] { codigo, empresa };
            dvEntidad = CentidadMetodos.EntidadGetKey("nPrestamo", "ppa", objValores).Tables[0].DefaultView;
            return dvEntidad;
        }

        public string Consecutivo(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            string[] oParametros = new string[] { "@consecutivo" };
            object[] objValores = new object[] { empresa };
            return Convert.ToString(Cacceso.ExecProc("spConsecutivoPrestamo", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

    }
}