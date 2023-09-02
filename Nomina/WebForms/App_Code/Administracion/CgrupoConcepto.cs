using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class CgrupoConcepto
    {
        public CgrupoConcepto()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }




        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "nGrupoConcepto",
                "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";

            return dvEntidad;
        }


        public int VerificaGrupoConcepto(string grupo, string concepto, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@concepto", "@grupo" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, concepto, grupo };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spVerificaGrupoConceptoNomina",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public int EliminaConceptosdelGrupo(string grupo, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@grupo" };
            string[] oParametros = new string[] { "@Retorno" };
            object[] objValores = new object[] { empresa, grupo };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spEliminaConcesptosdelGrupoNomina",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }



    }
}