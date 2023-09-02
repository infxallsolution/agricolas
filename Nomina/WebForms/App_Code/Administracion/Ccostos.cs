using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class Ccostos
    {
        public Ccostos()
        {
            //
            // TODO: Add constructor logic here
            //
        }



        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "nCcostoNomina",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";

            return dvEntidad;
        }

        public string Consecutivo(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            string[] oParametros = new string[] { "@consecutivo" };
            object[] objValores = new object[] { empresa };

            return Convert.ToString(Cacceso.ExecProc(
                "spConsecutivoCcostoNomina",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }
    }
}