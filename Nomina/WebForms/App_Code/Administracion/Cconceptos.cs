using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class Cconceptos
    {
        public Cconceptos()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }




        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "nConcepto",
                "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";

            return dvEntidad;
        }

        public int EliminaRangosConcepto(int empresa, string concepto)
        {
            string[] iParametros = new string[] { "@empresa", "@concepto" };
            string[] oParametros = new string[] { "@Retorno" };
            object[] objValores = new object[] { empresa, concepto };

            return Convert.ToInt16(Cacceso.ExecProc(
                "SpDeletenConceptoRangoTodo",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView SeleccionaRangoConcepto(int empresa, string codigo)
        {
            string[] iParametros = new string[] { "@empresa", "@concepto" };
            object[] objValores = new object[] { empresa, codigo };

            return Cacceso.DataSetParametros(
                "pSeleccionaRangosConcepto",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public DataSet SeleccionaConceptoxCcosto(int empresa, string codigo)
        {
            string[] iParametros = new string[] { "@empresa", "@ccosto" };
            object[] objValores = new object[] { empresa, codigo };

            return Cacceso.DataSetParametros(
                "spSeleccionaConceptoxCcosto",
                iParametros,
                objValores,
                "ppa");
        }

        public DataView SeleccionaConceptoEquivalentes(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaConceptoEquivalentes",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


    }
}