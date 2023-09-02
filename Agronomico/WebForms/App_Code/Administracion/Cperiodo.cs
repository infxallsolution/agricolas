using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Administracion
{
    public class Cperiodo
    {

        public Cperiodo()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = CentidadMetodos.EntidadGet("aPeriodo", "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "empresa = " + Convert.ToString(empresa) + " and periodo like '%" + texto + "%'";

            return dvEntidad;
        }

        public DataView GetAnosPeriodos()
        {
            return Cacceso.DataSet("spSeleccionaAnosCperiodosAgro", "ppa").Tables[0].DefaultView;
        }



        public int AbrirCerrarPeriodosAno(int ano, int empresa, out int conteo, bool cerrado)
        {
            string[] iParametros = new string[] { "@ano", "@empresa", "@cerrado" };
            string[] oParametros = new string[] { "@retorno", "@conteo" };
            object[] objValores = new object[] { ano, empresa, cerrado };

            object[] resultado = Cacceso.ExecProc("spAbrirPeriodosAnoAgro", iParametros, oParametros, objValores, "ppa");
            conteo = Convert.ToInt32(resultado.GetValue(1));
            return Convert.ToInt32(resultado.GetValue(0));
        }

        public int GenerarPeriodosAno(int ano, out int conteo, int empresa)
        {
            string[] iParametros = new string[] { "@ano", "@empresa" };
            string[] oParametros = new string[] { "@retorno", "@conteo" };
            object[] objValores = new object[] { ano, empresa };

            object[] resultado = Cacceso.ExecProc("spGeneraPeriodosAnoAgro", iParametros, oParametros, objValores, "ppa");
            conteo = Convert.ToInt32(resultado.GetValue(1));
            return Convert.ToInt32(resultado.GetValue(0));
        }

        public int EliminarPeriodosAno(int año, int empresa)
        {
            string[] iParametros = new string[] { "@año", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { año, empresa };
            object[] resultado = Cacceso.ExecProc("spEliminarPeriodosAnoAgro", iParametros, oParametros, objValores, "ppa");
            return Convert.ToInt32(resultado.GetValue(0));
        }

        public int RetornaPeriodoCerrado(string periodo)
        {
            string[] iParametros = new string[] { "@periodo" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { periodo };

            return Convert.ToInt32(Cacceso.ExecProc("spRetornaPeriodoCerradoAgro", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }
    }
}