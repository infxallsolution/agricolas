using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class CparametrosGeneral
    {
        public CparametrosGeneral()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }



        public DataView BuscarEntidad(int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "nParametrosGeneral",
                "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa);

            return dvEntidad;
        }

        public int ValidarEmpresaParametros(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            string[] oParametros = new string[] { "@Retorno" };
            object[] objValores = new object[] { empresa };
            return Convert.ToInt16(Cacceso.ExecProc("spValidaParametrosGeneralEmpresa", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }




    }
}