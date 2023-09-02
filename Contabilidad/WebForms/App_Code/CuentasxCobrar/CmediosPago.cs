using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.CuentasxCobrar
{
    public class CmediosPago
    {
        public CmediosPago()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }



        public DataView BuscarEntidad(string texto, int empresa)
        {

            DataView dvEntidad = CentidadMetodos.EntidadGet("cMediosPago", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa =" + empresa.ToString() + " and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";
            return dvEntidad;
        }

        public string TipoMedioPago(string medioPago, int empresa)
        {
            string[] iParametros = new string[] { "@medioPago", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { medioPago, empresa };
            return Convert.ToString(Cacceso.ExecProc("spSeleccioTipoMedioPago", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }


        public string RetornaAuxiliarCuentaBanco(string cuentaBanco, int empresa)
        {
            string[] iParametros = new string[] { "@cuentaBanco", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { cuentaBanco, empresa };
            return Convert.ToString(Cacceso.ExecProc("spRetoraAuxiliarCuentaBanco", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public string RetornaAuxiliarCaja(string caja, int empresa)
        {
            string[] iParametros = new string[] { "@caja", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { caja, empresa };
            return Convert.ToString(Cacceso.ExecProc("spRetoraAuxiliarCaja", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }



    }
}