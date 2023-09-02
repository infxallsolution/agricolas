using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Liquidacion
{
    public class Cvacaciones
    {
        public Cvacaciones()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }



        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "nVacaciones",
                "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (empleado  like '%" + texto + "%' or idEmpleado  like '%" + texto + "%')";

            return dvEntidad;
        }


        public string Consecutivo(int empresa, string empleado, DateTime periodoInicial, DateTime periodoFinal)
        {
            string[] iParametros = new string[] { "@empresa", "@empleado", "@periodoInicial", "@periodoFinal" };
            string[] oParametros = new string[] { "@consecutivo" };
            object[] objValores = new object[] { empresa, empleado, periodoInicial, periodoFinal };

            return Convert.ToString(Cacceso.ExecProc(
                "spConsecutivoVacaciones",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public string cargarPeriodo(int empresa, string empleado)
        {
            string[] iParametros = new string[] { "@empresa", "@empleado" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, empleado };

            return Convert.ToString(Cacceso.ExecProc(
                "spCargarPeriodoVacacionesEmpleado",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public string fechaFinalxDiaHabil(DateTime fechaInicial, int empresa, int noDias, int empleado)
        {

            string[] iParametros = new string[] { "@fechaInicial", "@empresa", "@noDias", "@empleado" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { fechaInicial, empresa, noDias, empleado };

            return Convert.ToString(Cacceso.ExecProc(
                "spfechaFinalxDiaHabil",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));

        }

        public Int32 verificaPeriodoVacacionEmpleado(int empresa, string empleado)
        {
            string[] iParametros = new string[] { "@empresa", "@empleado" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, empleado };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spVerificaPeriodoVacacionEmpleado",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public Int32 RetornaDiasFaltanteVacaciones(int empresa, string empleado)
        {
            string[] iParametros = new string[] { "@empresa", "@empleado" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, empleado };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spRetornaDiasFaltanteVacaciones",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public Int32 RetornaDiasVacaciones(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spRetornaDiasVacaciones",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public Int32 VerificaFechaVacaciones(int empresa, decimal empleado, DateTime fecha)
        {
            string[] iParametros = new string[] { "@empresa", "@empleado", "@fecha" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, empleado, fecha };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spVerificaFechaVacaciones",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }



        public Int32 RetornaDatosLiquidarVacaciones(int empresa, int empleado)
        {
            string[] iParametros = new string[] { "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spRetornaDatosLiquidarVacaciones",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView LiquidaConceptosBasicosVacaciones(int empresa, int empleado, DateTime fecha, int dias, DateTime fechaI, DateTime fechaF, decimal valorBase, int periodo, int año)
        {
            string[] iParametros = new string[] { "@empresa", "@tercero", "@fecha", "@noDias", "@fechaInicial", "@fechaFinal", "@valorBase", "@periodo", "@año" };
            object[] objValores = new object[] { empresa, empleado, fecha, dias, fechaI, fechaF, valorBase, periodo, año };

            return Cacceso.DataSetParametros(
                "spLiquidaConceptosBasicosVacaciones",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public DataView LiquidaSoloVacaciones(int empresa, int empleado, DateTime fecha, int dias, DateTime fechaI, DateTime fechaF, decimal valorBase, int tipo)
        {
            string[] iParametros = new string[] { "@empresa", "@tercero", "@fecha", "@noDias", "@fechaInicial", "@fechaFinal", "@valorBase", "@tipo" };
            object[] objValores = new object[] { empresa, empleado, fecha, dias, fechaI, fechaF, valorBase, tipo };

            return Cacceso.DataSetParametros(
                "spLiquidaSoloVacaciones",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public string RetornaSignoConcepto(int empresa, string concepto)
        {
            string[] iParametros = new string[] { "@empresa", "@concepto" };
            string[] oParametros = new string[] { "@signo" };
            object[] objValores = new object[] { empresa, concepto };

            return Convert.ToString(Cacceso.ExecProc(
                "spRetornaSignoConcepto",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int InsertaDetalleVacaciones(int empresa, DateTime periodoInicial, DateTime periodoFinal, Decimal empleado, int registro,
            string concepto, decimal cantidad, decimal valorUnitario, decimal valorTotal, string noPrestamo)
        {
            string[] iParametros = new string[] { "@empresa", "@periodoInicial", "@periodoFinal", "@empleado", "@registro", "@concepto", "@cantidad", "@valorUnitario", "@valorTotal", "@noPrestamo" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, periodoInicial, periodoFinal, empleado, registro, concepto, cantidad, valorUnitario, valorTotal, noPrestamo };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spInsertaDetalleVacaciones",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView RetornaVacacionePendientePeriodo(int empresa, int empleado, DateTime periodoInicial, DateTime periodoFinal)
        {
            string[] iParametros = new string[] { "@empresa", "@empleado", "@periodoInicial", "@periodoFinal" };

            object[] objValores = new object[] { empresa, empleado, periodoInicial, periodoFinal };

            return Cacceso.DataSetParametros(
                "spRetornaVacacionePendientePeriodo",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }



        public int AnulaVacaciones(int empresa, int empleado, DateTime periodoInicial, DateTime periodoFinal, int registro, string usuario)
        {
            string[] iParametros = new string[] { "@empresa", "@empleado", "@periodoInicial", "@periodoFinal", "@registro", "@usuario" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, empleado, periodoInicial, periodoFinal, registro, usuario };

            return Convert.ToInt32(Cacceso.ExecProc(
             "spAnulaVacaciones",
             iParametros,
             oParametros,
             objValores,
             "ppa").GetValue(0));
        }


        public int verificaVacacionesLiquidadas(int empresa, int empleado, DateTime periodoInicial, DateTime periodoFinal, int registro, string usuario)
        {
            string[] iParametros = new string[] { "@empresa", "@empleado", "@periodoInicial", "@periodoFinal", "@registro", "@usuario" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, empleado, periodoInicial, periodoFinal, registro, usuario };

            return Convert.ToInt32(Cacceso.ExecProc(
             "spverificaVacacionesLiquidadas",
             iParametros,
             oParametros,
             objValores,
             "ppa").GetValue(0));
        }

        public decimal RetornaBaseVariableVacaciones(int empresa, decimal empleado, DateTime fechaIVacacion)
        {
            string[] iParametros = new string[] { "@empresa", "@empleado", "@fechaIVacacion" };
            string[] oParametros = new string[] { "@valorBaseVacaciones" };
            object[] objValores = new object[] { empresa, empleado, fechaIVacacion };

            return Convert.ToDecimal(Cacceso.ExecProc(
                "spRetornaBaseVariableVacaciones",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public int AcumulaVacaciones(int empresa, decimal empleado, int año, int periodo, DateTime periodoInicial, DateTime periodoFinal, string observacion, string usuario)
        {
            string[] iParametros = new string[] { "@empresa", "@empleado", "@año", "@periodo", "@periodoInicial", "@periodoFinal", "@observacion", "@usuario" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, empleado, año, periodo, periodoInicial, periodoFinal, observacion, usuario };

            return Convert.ToInt32(Cacceso.ExecProc(
             "spAcumulaVacaciones",
             iParametros,
             oParametros,
             objValores,
             "ppa").GetValue(0));
        }


    }
}