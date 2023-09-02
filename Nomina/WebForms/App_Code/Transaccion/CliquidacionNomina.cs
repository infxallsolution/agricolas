using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Transaccion
{
    public class CliquidacionNomina
    {
        public CliquidacionNomina()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }




        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet("nLiquidacionNomina", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and numero like '%" + texto + "%'";

            return dvEntidad;
        }


        public DataView BuscarEntidadCesantias(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet("nLiquidacionCesantia", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and numero like '%" + texto + "%'";

            return dvEntidad;
        }


        public DataView BuscarEntidadContrato(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();
            dvEntidad = CentidadMetodos.EntidadGet("nLiquidacionNomina", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + "and tipo='" + ConfigurationManager.AppSettings["TipoTransaccionContrato"].ToString() + "' and numero like '%" + texto + "%'";
            dvEntidad.Sort = "fechaRegistro";
            return dvEntidad;
        }


        public DataView BuscarEntidadPrima(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet("nLiquidacionPrima", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and numero like '%" + texto + "%'";

            return dvEntidad;
        }



        public void LiquidacionNomina(int año, int noPeriodo, int empresa, string ccosto, string empleado, int mes, DateTime fecha, int formaLiquidacion, out int retorno, out string nombreTercero)
        {
            string[] iParametros = new string[] { "@empresa", "@año", "@mes", "@periodo", "@ccosto", "@empleado", "@fecheGeneral", "@formaLiquidacion" };
            object[] objValores = new object[] { empresa, año, mes, noPeriodo, ccosto, empleado, fecha, formaLiquidacion };
            string[] oParametros = new string[] { "@retorno", "@nombreTercero" };
            object[] rerotnos = Cacceso.ExecProc("spLiquidacionNominaPeriodo", iParametros, oParametros, objValores, "ppa");
            retorno = Convert.ToInt16(rerotnos.GetValue(0));
            nombreTercero = Convert.ToString(rerotnos.GetValue(1));
        }

        public void LiquidacionNominaDefinitiva(int año, int noPeriodo, int empresa, string ccosto, string empleado, int mes, DateTime fecha, string tipo, string usuario, string observacion, int formaLiquidacion, out int retorno, out string nombreTercero, out string numero)
        {
            string[] iParametros = new string[] { "@empresa", "@año", "@mes", "@periodo", "@ccosto", "@empleado", "@fecheGeneral", "@tipoTransaccion", "@usuario", "@observacion", "@formaliquidacion" };
            object[] objValores = new object[] { empresa, año, mes, noPeriodo, ccosto, empleado, fecha, tipo, usuario, observacion, formaLiquidacion };
            string[] oParametros = new string[] { "@retorno", "@nombreTercero", "@numero" };
            object[] rerotnos = Cacceso.ExecProc("spLiquidacionNominaPeriodoDefinitiva", iParametros, oParametros, objValores, "ppa");
            retorno = Convert.ToInt16(rerotnos.GetValue(0));
            nombreTercero = Convert.ToString(rerotnos.GetValue(1));
            numero = Convert.ToString(rerotnos.GetValue(2));
        }

        public void LiquidacionContratoDefinitiva(int año, int noPeriodo, int empresa, string empleado, int mes, DateTime fecha, string tipo, string usuario, string observacion, bool liquidaNomina, int contrato, out int retorno, out string numero)
        {
            string[] iParametros = new string[] { "@empresa", "@año", "@mes", "@periodo", "@empleado", "@fechaGeneral", "@tipoTransaccion", "@usuario", "@observacion", "@liquidacionNomina", "@noContrato" };
            object[] objValores = new object[] { empresa, año, mes, noPeriodo, empleado, fecha, tipo, usuario, observacion, liquidaNomina, contrato };
            string[] oParametros = new string[] { "@retorno", "@numero" };
            object[] rerotnos = Cacceso.ExecProc("spLiquidacionContratoDefinitiva", iParametros, oParametros, objValores, "ppa");
            retorno = Convert.ToInt16(rerotnos.GetValue(0));
            numero = Convert.ToString(rerotnos.GetValue(1));
        }

        public int InsertaConceptoLiquidacionContrato(string tercero, string concepto, decimal valor, int empresa, decimal cantidad, decimal valorUnitario)
        {
            string[] iParametros = new string[] { "@empresa", "@concepto", "@tercero", "@valor", "@valorUnitario", "@cantidad" };
            object[] objValores = new object[] { empresa, concepto, tercero, valor, valorUnitario, cantidad };
            string[] oParametros = new string[] { "@retorno" };
            return Convert.ToInt32(Cacceso.ExecProc("spInsertaConceptoLiquidacionContrato", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public int EliminaConceptoLiquidacionContrato(int tercero, string concepto, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@concepto", "@tercero" };
            object[] objValores = new object[] { empresa, concepto, tercero };
            string[] oParametros = new string[] { "@retorno" };
            return Convert.ToInt32(Cacceso.ExecProc("spEliminaConceptoLiquidacionContrato", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public void LiquidacionPrimas(int añoInicial, int añoFinal, int periodoInicial, int periodoFinal, int empresa, string ccosto, string empleado, DateTime fecha, int formaLiquidacion, out int retorno)
        {
            string[] iParametros = new string[] { "@empresa", "@añoInicial", "@añoFinal", "@periodoInicial", "@periodoFinal", "@ccosto", "@empleado", "@fechaGeneral", "@formaliquidacion" };
            object[] objValores = new object[] { empresa, añoInicial, añoFinal, periodoInicial, periodoFinal, ccosto, empleado, fecha, formaLiquidacion };
            string[] oParametros = new string[] { "@retorno" };
            object[] rerotnos = Cacceso.ExecProc("spLiquidacionPrimas", iParametros, oParametros, objValores, "ppa");
            retorno = Convert.ToInt16(rerotnos.GetValue(0));
        }

        public void LiquidacionPrimasDefinitiva(int añoPago, int periodoPago, int añoInicial, int añoFinal, int periodoInicial, int periodoFinal, int empresa, string ccosto, string empleado, DateTime fecha, int formaLiquidacion, string tipo, string usuario, string observacion, out int retorno, out string numero)
        {
            string[] iParametros = new string[] { "@añoPago", "@periodoPago", "@empresa", "@añoInicial", "@añoFinal", "@periodoInicial",
            "@periodoFinal", "@ccosto", "@empleado", "@fechaGeneral", "@formaliquidacion",
            "@tipoTransaccion", "@usuario", "@observacion" };
            object[] objValores = new object[] {añoPago,periodoPago,empresa, añoInicial, añoFinal, periodoInicial, periodoFinal,
            ccosto, empleado, fecha,  formaLiquidacion,tipo, usuario, observacion };
            string[] oParametros = new string[] { "@retorno", "@numero" };
            object[] rerotnos = Cacceso.ExecProc("spLiquidacionPrimasDefinitiva", iParametros, oParametros, objValores, "ppa");
            retorno = Convert.ToInt16(rerotnos.GetValue(0));
            numero = Convert.ToString(rerotnos.GetValue(1));
        }

        public void LiquidacionCesantias(int año, int empresa, string ccosto, string empleado, DateTime fecha, int formaLiquidacion, bool sueldoActual, out int retorno)
        {
            string[] iParametros = new string[] { "@empresa", "@año", "@ccosto", "@empleado", "@fechaGeneral", "@formaliquidacion", "@sueldoActual" };
            object[] objValores = new object[] { empresa, año, ccosto, empleado, fecha, formaLiquidacion, sueldoActual };
            string[] oParametros = new string[] { "@retorno" };
            object[] rerotnos = Cacceso.ExecProc("spLiquidacionCesantias", iParametros, oParametros, objValores, "ppa");
            retorno = Convert.ToInt16(rerotnos.GetValue(0));
        }

        public void LiquidacionCesantiasDefinitiva(int añoPago, int periodoPago, int año, int empresa, string ccosto, string empleado, DateTime fecha, int formaLiquidacion, string tipo, string usuario, string observacion,
            bool sueldoActual, bool paganomina, out int retorno, out string numero)
        {
            string[] iParametros = new string[] { "@empresa", "@año", "@ccosto", "@empleado", "@fechaGeneral", "@formaliquidacion", "@sueldoActual",
            "@tipoTransaccion", "@usuario", "@observacion","@añoPago", "@periodoPago"  , "@pagaNomina"};
            object[] objValores = new object[] { empresa, año, ccosto, empleado, fecha, formaLiquidacion, sueldoActual, tipo, usuario, observacion, añoPago, periodoPago, paganomina };
            string[] oParametros = new string[] { "@retorno", "@numero" };
            object[] rerotnos = Cacceso.ExecProc("spLiquidacionCesantiasDefinitiva", iParametros, oParametros, objValores, "ppa");
            retorno = Convert.ToInt16(rerotnos.GetValue(0));
            numero = Convert.ToString(rerotnos.GetValue(1));
        }

        public int RetornaMesPeriodoNomina(int año, int noPeriodo, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@año", "@noPeriodo" };
            object[] objValores = new object[] { empresa, año, noPeriodo };
            string[] oParametros = new string[] { "@retorno" };


            return Convert.ToInt32(Cacceso.ExecProc("spRetornaMesPeriodoNomina", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }


        public DataView getLiquidacionNomina(int noPeriodo, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@noPeriodo" };
            object[] objValores = new object[] { empresa, noPeriodo };

            return Cacceso.DataSetParametros("spGetLiquidacionNomina", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView PreliquidarContrato(int noPeriodo, int empresa, int año, int tercero, bool liquidaNomina, DateTime fecha, int contrato)
        {
            string[] iParametros = new string[] { "@empresa", "@noPeriodo", "@año", "@tercero", "@liquidaNomina", "@fecha", "@contrato" };
            object[] objValores = new object[] { empresa, noPeriodo, año, tercero, liquidaNomina, fecha, contrato };

            return Cacceso.DataSetParametros("spLiquidacionContratoTrabajador", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView cargarConceptosLiquidacionContrato(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros("spSeleccionaConceptoLiquidacionContrato", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public int DeleteTmpLiquidacion(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };
            string[] oParametros = new string[] { "@retorno" };


            return Convert.ToInt32(Cacceso.ExecProc("SpDeleteTmpLiquidacion", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public int VerificaTmpLiquidacion(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };
            string[] oParametros = new string[] { "@retorno" };

            return Convert.ToInt32(Cacceso.ExecProc("spVerificaTmpLiquidacion", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }


        public DataView getDoctoCesantiasAño(int año, int empresa)
        {
            string[] iParametros = new string[] { "@año", "@empresa" };
            object[] objValores = new object[] { año, empresa };

            return Cacceso.DataSetParametros("spSeleccionaNumeroLiquidacionPeriodoCesantias", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView getCesantiasDefinitiva(int año, int empresa, string numero)
        {
            string[] iParametros = new string[] { "@año", "@empresa", "@numero" };
            object[] objValores = new object[] { año, empresa, numero };

            return Cacceso.DataSetParametros("spSeleccionaLiquidacionCesantias", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public int actualizaLiquidacionCesantias(int empresa, string documento, int empleado, string usuario, decimal diasCesantias, decimal valorCesantias, decimal valorIntereses,
            decimal basecesantias, decimal valorPromedio, int diasPromedio)
        {
            string[] iParametros = new string[] { "@empresa",
"@documento",
"@empleado",
"@usuario",
"@diasCesantias",
"@valorCesantias",
"@valorIntereses",
"@valorBase",
"@valorPromedio",
"@diasPromedio",

        };
            object[] objValores = new object[] { empresa, documento, empleado, usuario,
            diasCesantias, valorCesantias, valorIntereses, basecesantias, valorPromedio, diasPromedio  };
            string[] oParametros = new string[] { "@retorno" };


            return Convert.ToInt32(Cacceso.ExecProc("spActualizaLiquidacionCesantiasDefinitiva", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public int eliminaLiquidacionEmpleado(int empresa, string documento, int empleado)
        {
            string[] iParametros = new string[] { "@empresa",
"@documento",
"@empleado"
        };
            object[] objValores = new object[] { empresa, documento, empleado };
            string[] oParametros = new string[] { "@retorno" };


            return Convert.ToInt32(Cacceso.ExecProc("spEliminaCesantiasEmpleado", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

    }
}