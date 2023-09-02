using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class Cperiodos
    {
        public Cperiodos()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }




        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();
            dvEntidad = CentidadMetodos.EntidadGet("nPeriodoDetalle", "ppa").Tables[0].DefaultView;
            if (texto != "")
                dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and periodo  like '%" + texto + "%'";
            else
                dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa);

            return dvEntidad;
        }

        public int RetornaPeriodoCerrado(int año, int mes, int empresa)
        {
            string[] iParametros = new string[] { "@año", "@mes", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { año, mes, empresa };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spRetornaPeriodoCerrado",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int RetornaPeriodoCerradoNomina(int año, int mes, int empresa, DateTime fecha)
        {
            string[] iParametros = new string[] { "@año", "@mes", "@empresa", "@fecha", "@agro" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { año, mes, empresa, fecha, true };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spRetornaPeriodoCerradoNomina",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int RetornaPeriodoCerradoNominaSinAgro(int año, int mes, int empresa, DateTime fecha)
        {
            string[] iParametros = new string[] { "@año", "@mes", "@empresa", "@fecha", "@agro" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { año, mes, empresa, fecha, false };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spRetornaPeriodoCerradoNomina",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataSet PeriodoMesAbierto(int año, int empresa)
        {
            string[] iParametros = new string[] { "@año", "@empresa" };
            object[] objValores = new object[] { año, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaMesAbiertos",
                iParametros,
                objValores,
                "ppa");
        }
        public DataView PeriodoAñoAbierto(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaAñosAbiertos",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }
        public DataView PeriodoDetalle(int empresa, int año)
        {
            string[] iParametros = new string[] { "@empresa", "@año" };
            object[] objValores = new object[] { empresa, año };

            return Cacceso.DataSetParametros(
                "spSeleccionaPeriodoDetalle",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }
        public int TipoNomidaDias(string tipoNomina, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@tipoNomina" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, tipoNomina };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spSeleccionaNoDiasTipoNomina",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }
        public int noPeriodosAño(int año, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@año" };
            string[] oParametros = new string[] { "@noPeriodo" };
            object[] objValores = new object[] { empresa, año };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spSeleccionaNoPeriodosAñoNomina",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public int consecutivoPeriodoAño(int empresa, int año)
        {
            string[] iParametros = new string[] { "@empresa", "@año" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, año };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spconsecutivoPeriodoAño",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }



        public int NoPeriodoAñoMes(int año, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@año" };
            string[] oParametros = new string[] { "@noPeriodo" };
            object[] objValores = new object[] { empresa, año };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spSeleccionaNoPeriodoNomina",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int EliminaPeriodoAño(int año, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@año" };
            string[] oParametros = new string[] { "@Retorno" };
            object[] objValores = new object[] { empresa, año };

            return Convert.ToInt16(Cacceso.ExecProc(
                "SpDeletenPeriodoDetalleAño",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView PeriodoAñoAbiertoNomina(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaAñosAbiertosNomina",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataSet PeriodoMesAbiertoNomina(int año, int empresa)
        {
            string[] iParametros = new string[] { "@año", "@empresa" };
            object[] objValores = new object[] { año, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaMesAbiertosNomina",
                iParametros,
                objValores,
                "ppa");
        }

        public DataSet PeriodosAbiertoNomina(int año, int mes, int empresa)
        {
            string[] iParametros = new string[] { "@año", "@empresa", "@mes" };
            object[] objValores = new object[] { año, empresa, mes };

            return Cacceso.DataSetParametros(
                "spSeleccionaAbiertosNomina",
                iParametros,
                objValores,
                "ppa");
        }

        public DataSet PeriodosAbiertoNominaAño(int año, int empresa)
        {
            string[] iParametros = new string[] { "@año", "@empresa" };
            object[] objValores = new object[] { año, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaAbiertosNominaAño",
                iParametros,
                objValores,
                "ppa");
        }

        public DataSet PeriodoNominaAño(int año, int empresa)
        {
            string[] iParametros = new string[] { "@año", "@empresa" };
            object[] objValores = new object[] { año, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaNominaAño",
                iParametros,
                objValores,
                "ppa");
        }

        public int RetornaMesPeriodoNomina(int año, int noPeriodo, int empresa)
        {
            string[] iParametros = new string[] { "@año", "@noPeriodo", "@empresa" };
            object[] objValores = new object[] { año, noPeriodo, empresa };
            string[] oParametros = new string[] { "@Retorno" };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spRetornaMesPeriodoNomina",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView PeriodoAñoCerradoNomina(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaAñosCerradoNomina",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

    }
}