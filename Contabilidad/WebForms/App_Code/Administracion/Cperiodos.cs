using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.Administracion
{
    public class Cperiodos
    {


        public Cperiodos()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int verificaPeriodoCreado(int empresa, DateTime fecha)
        {
            string[] iParametros = new string[] { "@empresa", "@fecha" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, fecha };

            return Convert.ToInt16(Cacceso.ExecProc(
                    "spverificaPeriodoCreado",
                    iParametros,
                    oParametros,
                    objValores,
                    "ppa").GetValue(0));
        }


        public int verificaPeriodoCerrado(int empresa, DateTime fecha)
        {
            string[] iParametros = new string[] { "@empresa", "@fecha" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, fecha };

            return Convert.ToInt16(Cacceso.ExecProc(
                    "spverificaPeriodoCerrado",
                    iParametros,
                    oParametros,
                    objValores,
                    "ppa").GetValue(0));
        }
        public DataSet GetMesPeriodos(int empresa, int ano)
        {
            string[] iParametros = new string[] { "@empresa", "@año" };
            object[] objValores = new object[] { empresa, ano };
            return Cacceso.DataSetParametros("spSeleccionaMesPeriodos", iParametros, objValores, "ppa");
        }


        public int RetornaPeriodoCerrado(int ano, int mes, int empresa)
        {
            string[] iParametros = new string[] { "@año", "@mes", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { ano, mes, empresa };
            return Convert.ToInt16(Cacceso.ExecProc("spRetornaPeriodoCerradoConta", iParametros, oParametros, objValores, "ppa").GetValue(0));
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

        public DataView PeriodosCeradoNominaAño(int año, int empresa, string tipo)
        {
            string[] iParametros = new string[] { "@año", "@empresa", "@tipo" };
            object[] objValores = new object[] { año, empresa, tipo };

            return Cacceso.DataSetParametros(
                "spSeleccionaPeriodosContabilizacion",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public string RetornaNombreArchivoPlano(int empresa, int año, int periodo)
        {
            string[] iParametros = new string[] { "@empresa", "@año", "@periodo" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, año, periodo };

            return Convert.ToString(Cacceso.ExecProc(
                    "spRetornaNombreArchivoPlanoConta",
                    iParametros,
                    oParametros,
                    objValores,
                    "ppa").GetValue(0));
        }

        public int GenerarPeriodosAno(int ano, int empresa)
        {
            string[] iParametros = new string[] { "@ano", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { ano, empresa };

            object[] resultado = Cacceso.ExecProc(
                "spGeneraPeriodosAno",
                iParametros,
                oParametros,
                objValores,
                "ppa");
            return Convert.ToInt16(resultado.GetValue(0));
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = CentidadMetodos.EntidadGet(
                "cPeriodo",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "empresa = " + Convert.ToString(empresa) + " and periodo like '%" + texto + "%'";

            return dvEntidad;
        }

        public DataSet GetAnosPeriodos(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };
            return Cacceso.DataSetParametros("spSeleccionaAnosCperiodos", iParametros, objValores, "ppa");
        }



        public DataView GetAnosPeriodos()
        {
            return Cacceso.DataSet(
                "spSeleccionaAnosCperiodos",
                "ppa").Tables[0].DefaultView;
        }


        public DataSet PeriodoañoCerradoNomina(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };
            return Cacceso.DataSetParametros("spSeleccionaAnosCperiodos", iParametros, objValores, "ppa");
        }
        public DataSet PeriodosSeguridadSocial(int año, int empresa)
        {
            string[] iParametros = new string[] { "@año", "@empresa" };
            object[] objValores = new object[] { año, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionarPeriodosSeguridadSocial",
                iParametros,
                objValores,
                "ppa");
        }

        public int AbrirCerrarPeriodosAno(int ano, int empresa, out int conteo, bool cerrado)
        {
            string[] iParametros = new string[] { "@ano", "@empresa", "@cerrado" };
            string[] oParametros = new string[] { "@retorno", "@conteo" };
            object[] objValores = new object[] { ano, empresa, cerrado };

            object[] resultado = Cacceso.ExecProc(
                "spAbrirPeriodosAno",
                iParametros,
                oParametros,
                objValores,
                "ppa");

            conteo = Convert.ToInt16(resultado.GetValue(1));

            return Convert.ToInt16(resultado.GetValue(0));
        }

        public int GenerarPeriodosAno(int ano, out int conteo, int empresa)
        {
            string[] iParametros = new string[] { "@ano", "@empresa" };
            string[] oParametros = new string[] { "@retorno", "@conteo" };
            object[] objValores = new object[] { ano, empresa };

            object[] resultado = Cacceso.ExecProc(
                "spGeneraPeriodosAno",
                iParametros,
                oParametros,
                objValores,
                "ppa");

            conteo = Convert.ToInt16(resultado.GetValue(1));

            return Convert.ToInt16(resultado.GetValue(0));
        }

        public int EliminarPeriodosAno(int año, int empresa)
        {
            string[] iParametros = new string[] { "@año", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { año, empresa };

            object[] resultado = Cacceso.ExecProc(
                "spEliminarPeriodosAno",
                iParametros,
                oParametros,
                objValores,
                "ppa");

            return Convert.ToInt16(resultado.GetValue(0));
        }

    }
}