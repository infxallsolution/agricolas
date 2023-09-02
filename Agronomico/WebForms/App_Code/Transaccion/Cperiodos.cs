using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Transaccion
{
    public class Cperiodos
    {
        public Cperiodos()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }



        //public int RetornaPeriodoCerrado(int año, int mes, int empresa)
        //{
        //    string[] iParametros = new string[] { "@año", "@mes", "@empresa" };
        //    string[] oParametros = new string[] { "@retorno" };
        //    object[] objValores = new object[] { año, mes, empresa };

        //    return Convert.ToInt32(Cacceso.ExecProc(
        //        "spRetornaPeriodoCerrado",
        //        iParametros,
        //        oParametros,
        //        objValores,
        //        "ppa").GetValue(0));
        //}

        public int RetornaPeriodoCerradoNomina(int año, int mes, int empresa, DateTime fecha)
        {
            string[] iParametros = new string[] { "@año", "@mes", "@empresa", "@fecha", "@agro" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { año, mes, empresa, fecha, true };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spRetornaPeriodoCerradoNomina",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int TrasladaSaldos(int ano, int mes, int empresa)
        {
            string[] iParametros = new string[] { "@ano", "@mes" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { ano, mes };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spTrasladaSaldosPeriodo",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView GetPeriodosActivos(int empresa)
        {
            DataView dvPeriodo = CentidadMetodos.EntidadGet(
                "cPeriodos",
                "ppa").Tables[0].DefaultView;

            dvPeriodo.RowFilter = "cerrado = False and empresa=" + Convert.ToString(empresa);

            return dvPeriodo;
        }

        public DataView GetAnosPeriodos(int empresa)
        {

            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaAnosCperiodos",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;


        }
    }
}