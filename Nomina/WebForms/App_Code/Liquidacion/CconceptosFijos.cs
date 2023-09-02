using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Liquidacion
{
    public class CconceptosFijos
    {
        public CconceptosFijos()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "nConceptosFijos",
                "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (centroCosto like '%" + texto + "%' or descripcion like '%" + texto + "%')";

            return dvEntidad;
        }



        public int VerificaConceptosFijos(string departamento, int año, int mes, int periodo, string concepto, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@centroCosto", "@año", "@mes", "@periodo", "@concepto" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, departamento, año, mes, periodo, concepto };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spVerificaConceptosFijosDetalle",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public int EliminaConceptosFijos(string departamento, int año, int mes, int periodo, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@centrocosto", "@año", "@mes", "@periodo" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, departamento, año, mes, periodo };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spEliminaConceptosFijosDetalle",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public DataView GetConceptosFijos(string departamento, int año, int mes, int periodo, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@departamento", "@año", "@mes", "@periodo" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, departamento, año, mes, periodo };

            return Cacceso.DataSetParametros(
                "spSeleccionaConceptosFijosDetalle",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }



    }
}