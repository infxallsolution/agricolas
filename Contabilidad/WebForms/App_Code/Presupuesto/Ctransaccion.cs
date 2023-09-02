using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.Presupuesto
{
    public class Ctransaccion
    {
        public Ctransaccion()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        public int SeleccionaControlAnalisis(string transaccion)
        {
            string[] iParametros = new string[] { "@transaccion" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { transaccion };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spSeleccionaControlAnalisis",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public void SaldoProduccionMovimiento(string tipo, string numero)
        {
            string[] iParametros = new string[] { "@tipo", "@numero" };
            string[] oParametros = new string[] { };
            object[] objValores = new object[] { tipo, numero };

            Cacceso.ExecProc(
                "spRecalculaSaldoProduccionMovimiento",
                iParametros,
                oParametros,
                objValores,
                "ppa");
        }

        public void SaldoProduccionMovimientoElimina(string tipo, string numero)
        {
            string[] iParametros = new string[] { "@tipo", "@numero" };
            string[] oParametros = new string[] { };
            object[] objValores = new object[] { tipo, numero };

            Cacceso.ExecProc(
                "spRecalculaSaldoProduccionMovimientoBorrado",
                iParametros,
                oParametros,
                objValores,
                "ppa");
        }

        public DataView GetTransaccionCompleta(string where)
        {
            string[] iParametros = new string[] { "@where" };
            object[] objValores = new object[] { where };

            return Cacceso.DataSetParametros(
                "spSeleccionaTransaccionCompletaProduccion",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public void RecalculaSaldoProduccion()
        {
            string[] iParametros = new string[] { };
            string[] oParametros = new string[] { };
            object[] objValores = new object[] { };

            Cacceso.ExecProc(
                "spRecalculaSaldoProduccion",
                iParametros,
                oParametros,
                objValores,
                "ppa");
        }

        public DataView EjecutaFormulaP(string jerarquia, string variable, string varObj, string modo)
        {
            string[] iParametros = new string[] { "@jerarquia", "@variableP", "@objVar", "@modo" };
            object[] objValores = new object[] { jerarquia, variable, varObj, modo };

            return Cacceso.DataSetParametros(
                "spEjecutaFormulaP",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView RetornaRegistroFeccha(string jerarquia, DateTime fecha, string analisis, string tipo)
        {
            string[] iParametros = new string[] { "@jerarquia", "@fecha", "@analisis", "@tipo" };
            object[] objValores = new object[] { jerarquia, fecha, analisis, tipo };

            return Cacceso.DataSetParametros(
                "spRetornaAnalisisRegistradoFecha",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public void VerificaFormulaPresupuesto(string jerarquia, string formula, string modo, out string expresion, int empresa, int año, int mes)
        {
            string[] iParametros = new string[] { "@jerarquia", "@formula", "@modo", "@empresa", "@año", "@mes" };
            string[] oParametros = new string[] { "@expresion" };
            object[] objValores = new object[] { jerarquia, formula, modo, empresa, año, mes };

            expresion = Convert.ToString(Cacceso.ExecProc(
                "spVerificaFormulaPresupuesto",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView GetJerarquiaBodega(string bodega, string jerarquia)
        {
            string[] iParametros = new string[] { "@bodega", "@jerarquia" };
            object[] objValores = new object[] { bodega, jerarquia };

            return Cacceso.DataSetParametros(
                "spSeleccionaJerarquiaBodega",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetJerarquiaBodegaCab(string bodega)
        {
            string[] iParametros = new string[] { "@bodega" };
            object[] objValores = new object[] { bodega };

            return Cacceso.DataSetParametros(
                "spSeleccionaJerarquiaBodegaCab",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public object[] DatosJerarquiaVariable(object[] parametros)
        {
            DataView dvVariables = CentidadMetodos.EntidadGetKey(
                "pJerarquiaVariable",
                "ppa",
                parametros).Tables[0].DefaultView;

            object[] objResultado = new object[dvVariables.Table.Columns.Count];

            foreach (DataRowView registro in dvVariables)
            {
                for (int i = 0; i < dvVariables.Table.Columns.Count; i++)
                {
                    objResultado.SetValue(registro.Row.ItemArray.GetValue(i), i);
                }
            }

            return objResultado;
        }

        public object[] DatosProductoMovmineto(object[] parametros)
        {
            DataView dvAnalisis = CentidadMetodos.EntidadGetKey(
                "pProductoMovimientoContable", "ppa", parametros).Tables[0].DefaultView;

            object[] objResultado = new object[dvAnalisis.Table.Columns.Count];

            foreach (DataRowView registro in dvAnalisis)
            {
                for (int i = 0; i < dvAnalisis.Table.Columns.Count; i++)
                {
                    objResultado.SetValue(registro.Row.ItemArray.GetValue(i), i);
                }
            }
            return objResultado;
        }

        public DataView GetVariablesResultadoJerarquia(int jerarquia)
        {
            string[] iParametros = new string[] { "@jerarquia" };
            object[] objValores = new object[] { jerarquia };

            return Cacceso.DataSetParametros(
                "spSeleccionaVariableResultadoJerarquia",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public int InsertaTransaccionVariables(string tipo, string numero, DateTime fecha, string bodega, string producto, string observacion,
            string funcionario)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@fecha", "@bodega", "@producto", "@observacion", "@funcionario" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, fecha, bodega, producto, observacion, funcionario };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spInsertaTransaccionProVariables",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView GetJerarquiaTransaccionCab(string transaccion)
        {
            string[] iParametros = new string[] { "@transaccion" };
            object[] objValores = new object[] { transaccion };

            return Cacceso.DataSetParametros(
                "spSeleccionaTransaccionAnalisisCab",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetJerarquiaTransaccion(string transaccion, string jerarquia)
        {
            string[] iParametros = new string[] { "@transaccion", "@jerarquia" };
            object[] objValores = new object[] { transaccion, jerarquia };

            return Cacceso.DataSetParametros(
                "spSeleccionaJerarquiaTransaccion",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public DataView GetMovimientoResultadoProductoMostrar(string producto)
        {
            string[] iParametros = new string[] { "@producto" };
            object[] objValores = new object[] { producto };

            return Cacceso.DataSetParametros(
                "spSeleccionaMovimientosResultadoProductoMostrar",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public DataView GetMovimientoResultadoProducto(string producto)
        {
            string[] iParametros = new string[] { "@producto" };
            object[] objValores = new object[] { producto };

            return Cacceso.DataSetParametros(
                "spSeleccionaMovimientosResultadoProducto",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }



        public DataView EjecutaFormulaA(string jerarquia, string variable, string varObj, string modo, DateTime fecha)
        {
            string[] iParametros = new string[] { "@producto", "@movimiento", "@objVar", "@modo", "@fecha" };
            object[] objValores = new object[] { jerarquia, variable, varObj, modo, fecha };

            return Cacceso.DataSetParametros(
                "spEjecutaFormulaProduccion",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView EjecutaFormulaA(string jerarquia, string variable, string varObj, string modo, DateTime fecha, int empresa)
        {
            string[] iParametros = new string[] { "@producto", "@movimiento", "@objVar", "@modo", "@fecha", "@empresa" };
            object[] objValores = new object[] { jerarquia, variable, varObj, modo, fecha, empresa };

            return Cacceso.DataSetParametros(
                "spEjecutaFormulaProduccion",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetItemRetornaDatos()
        {
            return Cacceso.DataSet(
                "spSeleccionaItemsFretornaDatos",
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetItemRetornaDatosLaboratorio(int orden, int empresa)
        {

            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            DataView dvDatos = Cacceso.DataSetParametros(
                "spSeleccionaItemsFretornaDatosPresupuesto",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;

            dvDatos.RowFilter = "orden = " + Convert.ToString(orden);

            return dvDatos;
        }

        public DataView GetItemRetornaDatos(int orden, int empresa)
        {

            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            DataView dvDatos = Cacceso.DataSetParametros(
                "spSeleccionaItemsFretornaDatos",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;

            dvDatos.RowFilter = "orden = " + Convert.ToString(orden);

            return dvDatos;
        }

        public int RetornaVariableDiscreta(string variable, int jerarquia)
        {
            string[] iParametros = new string[] { "@variable", "@jerarquia" };
            string[] oParametros = new string[] { "@discreto" };
            object[] objValores = new object[] { variable, jerarquia };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spRetornaAnalisisDiscreto",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public decimal RetornaValorAnalisisDiscreto(string tipo, int jerarquia, string analisis)
        {
            string[] iParametros = new string[] { "@tipo", "@jerarquia", "@analisis" };
            string[] oParametros = new string[] { "@valor" };
            object[] objValores = new object[] { tipo, jerarquia, analisis };

            return Convert.ToDecimal(Cacceso.ExecProc(
                "spRetornaValorAnalisisDiscreto",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView SeleccionaTipoTransaccionParametros(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaTipoTransaccionParametros",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView seleccionaItemsTipoTransaccion(string tipotransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipoTrasaccion", "@empresa" };
            object[] objValores = new object[] { tipotransaccion, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaItemsTipoTransaccion",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public DataView seleccionaVariablesTransaccionItems(string tipotransaccion, string item, int empresa)
        {
            string[] iParametros = new string[] { "@tipotransaccion", "@item", "@empresa" };
            object[] objValores = new object[] { tipotransaccion, item, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaVariablesTransaccionItems",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public DataView seleccionaVariablesTransaccionItemsDetalle(string movimiento, string tipotransaccion, string item, int empresa)
        {
            string[] iParametros = new string[] { "@movimiento", "@tipotransaccion", "@item", "@empresa" };
            object[] objValores = new object[] { movimiento, tipotransaccion, item, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaVariablesTransaccionItemsDetalle",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

    }
}