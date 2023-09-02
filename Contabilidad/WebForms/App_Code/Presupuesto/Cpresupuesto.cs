using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.Presupuesto
{
    public class Cpresupuesto
    {

        public Cpresupuesto()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = CentidadMetodos.EntidadGet(
                "cPuc",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "empresa =" + empresa + " and (codigo like '%" + texto + "%' or nombre like '%" + texto + "%')";

            return dvEntidad;
        }

        public DataView BuscarEntidad(string texto, string tipo)
        {
            DataView dvEntidad = CentidadMetodos.EntidadGet(
                "Cpuc",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "( codigo like '%" + texto + "%' or nombre like '%" + texto + "%' ) and tipo = '" + tipo + "' and activo = 1";

            return dvEntidad;
        }

        private DataSet GetPuc()
        {
            return CentidadMetodos.EntidadGet(
                "cPuc",
                "ppa");
        }

        public DataView GetPucDestino(string tipo, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@empresa" };
            object[] objValores = new object[] { tipo, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaPucTipo",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        private DataView GetPuc(string codigo, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "cPuc",
                "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa =" + empresa + " and codigo = '" + codigo + "'";

            return dvEntidad;
        }

        private DataView GetPucRaiz(string cuenta)
        {
            DataView dvPuc = new DataView();

            dvPuc = GetPuc().Tables[0].DefaultView;
            dvPuc.RowFilter = "raiz = '" + cuenta + "'";

            return dvPuc;
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


        public DataView GetPresupuesto(int empresa, string usuario, int año, string cuenta)
        {
            string[] iParametros = new string[] { "@empresa", "@usuario", "@año", "@cuenta" };
            object[] objValores = new object[] { empresa, usuario, año, cuenta };

            return Cacceso.DataSetParametros(
                "spSeleccionaPresupuesto",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetPresupuestoDetalle(int empresa, string usuario, int año, string cuenta)
        {
            string[] iParametros = new string[] { "@empresa", "@usuario", "@año", "@cuenta" };
            object[] objValores = new object[] { empresa, usuario, año, cuenta };

            return Cacceso.DataSetParametros(
                "spSeleccionaPresupuestoDetalle",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView SeleccionaAñosAbiertos(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaAñosAbiertos",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public int DeletecPresupuestoAnual(string cuenta, int empresa)
        {
            string[] iParametros = new string[] { "@cuentaPresupueto", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { cuenta, empresa };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spDeletecPresupuestoAnual",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int InsertacPresupuestoAnual(int empresa, string cuentapresupuesto, string ccostopresupuesto,
            int año, int mes, decimal valor, string usuario)
        {
            string[] iParametros = new string[] { "@empresa",
       "@cuentaPresupueto","@ccostoPresupuesto","@año", "@mes", "@valor" ,"@usuario"  };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa,  cuentapresupuesto,  ccostopresupuesto,
         año,  mes, valor,    usuario };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spInsertacPresupuestoAnual",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

    }
}