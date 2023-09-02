using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class CformaPago
    {
        public CformaPago()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }




        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "gFormaPago",
                "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";

            return dvEntidad;
        }

        public int VerificaChequeFormaPago(int empresa, string formaPago)
        {
            string[] iParametros = new string[] { "@empresa", "@formaPago" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, formaPago };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spVerificaChequeFormaPago",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }
    }
}