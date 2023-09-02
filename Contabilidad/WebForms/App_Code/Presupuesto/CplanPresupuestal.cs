using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.Presupuesto
{
    public class CplanPresupuestal
    {

        public CplanPresupuestal()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = CentidadMetodos.EntidadGet(
                "cPlanPresupuestal",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "empresa =" + empresa + " and (codigo like '%" + texto + "%' or nombre like '%" + texto + "%')";

            return dvEntidad;
        }

        public DataView RetornaMayoresPlanPresupuesto(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "spRetornaMayoresPlanPresupuesto",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView RetornaPlanPresupuesto(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "spRetornaPlanPresupuesto",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView RetornaPlanPresupuestoManejanCcosto(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "spRetornaPlanPresupuestoManejanCcosto",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public int ValidaCuentaPlanPresupuestal(int empresa, string cuenta)
        {
            string[] iParametros = new string[] { "@empresa", "@cuenta" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, cuenta };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spValidaCuentaPlanPresupuestal",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView retornaCuentaPresupuesto(int empresa, string cuenta)
        {
            string[] iParametros = new string[] { "@empresa", "@cuenta" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, cuenta };

            return Cacceso.DataSetParametros(
                "spRetornaCuentaPresupuesto",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }
    }
}