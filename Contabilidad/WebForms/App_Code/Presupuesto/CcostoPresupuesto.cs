using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.Presupuesto
{
    public class CcostoPresupuesto
    {

        public CcostoPresupuesto()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = CentidadMetodos.EntidadGet(
                "cCcostoPresupuesto",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "empresa =" + empresa + " and (codigo like '%" + texto + "%' or nombre like '%" + texto + "%')";

            return dvEntidad;
        }

        public int VerificaCuentaEnMovimientos(string cuenta)
        {
            string[] iParametros = new string[] { "cuenta" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { cuenta };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spVerificaCuentaEnMovimientos",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView RetornaMayoresCcostoPresupuesto(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "spRetornaMayoresCcostoPresupuesto",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView retornaCcostoPresupuesto(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "spRetornaCcostoPresupuesto",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public DataView RetornaCcostoSiesaCuenta(int empresa, string cuenta)
        {
            string[] iParametros = new string[] { "@empresa", "@cuenta" };
            object[] objValores = new object[] { empresa, cuenta };

            return Cacceso.DataSetParametros(
                "spRetornaCcostoSiesaCuenta",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView RetornaMCcostoSiesaCuenta(int empresa, string cuenta)
        {
            string[] iParametros = new string[] { "@empresa", "@cuenta" };
            object[] objValores = new object[] { empresa, cuenta };

            return Cacceso.DataSetParametros(
                "spRetornaMCcostoSiesaCuenta",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public int ValidaCcostoPlanPresupuestal(int empresa, string cuenta)
        {
            string[] iParametros = new string[] { "@empresa", "@cuenta" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, cuenta };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spValidaCuentaCcostoPresupuestal",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


    }
}