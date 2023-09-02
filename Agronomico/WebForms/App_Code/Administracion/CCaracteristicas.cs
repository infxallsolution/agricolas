using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Administracion
{
    public class Ccaracteristicas
    {
        public Ccaracteristicas()
        {
        }
        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();
            dvEntidad = CentidadMetodos.EntidadGet("aCaracteristica", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and  descripcion like '%" + texto + "%'";
            return dvEntidad;
        }

        public string Consecutivo(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            string[] oParametros = new string[] { "@consecutivo" };
            object[] objValores = new object[] { empresa };
            return Convert.ToString(Cacceso.ExecProc("spConsecutivoCaracteristica", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public DataView RetornaCaracteristicaGrupo(int gCaracteristica, int empresa)
        {

            string[] iParametros = new string[] { "@gCaracteristica", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { gCaracteristica, empresa };


            return Cacceso.DataSetParametros(
                "spRetornaCaracteristicaGrupo",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;

        }


    }

}