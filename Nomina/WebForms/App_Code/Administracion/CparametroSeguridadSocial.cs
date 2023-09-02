using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class CparametroSeguridadSocial
    {
        public CparametroSeguridadSocial()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }




        public int VerificaConceptosTipo(string tipo, string concepto, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@concepto", "@tipo" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, concepto, tipo };

            return Convert.ToInt16(Cacceso.ExecProc("spVerificaConceptosSeguridadSocial", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();
            dvEntidad = CentidadMetodos.EntidadGet("nParametroSeguridadSocial", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";

            return dvEntidad;
        }

        public int EliminaParametros(string tipo, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@tipo" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, tipo };

            return Convert.ToInt16(Cacceso.ExecProc("spEliminaParametrosSeguridadSocial", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }



    }
}