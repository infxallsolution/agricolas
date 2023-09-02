using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Administracion
{
    public class CpromedioPeso
    {
        public CpromedioPeso()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public decimal valorPesoPeriodoJerarquia(int empresa, DateTime fecha, string lote)
        {
            string[] iParametros = new string[] { "@empresa", "@fecha", "@lote" };
            string[] oParametros = new string[] { "@valor" };
            object[] objValores = new object[] { empresa, fecha, lote };
            return Convert.ToDecimal(Cacceso.ExecProc("spSeleccionaPesoJerarquiaRacimosPeriodo", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }


        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();
            dvEntidad = CentidadMetodos.EntidadGet("aLotePesosPeriodo", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (finca like '%" + texto + "%' or lote like '%" + texto + "%')";
            return dvEntidad;
        }

        public decimal valorPromedioPeriodo(int empresa, int año, int mes, string lote, string tipo)
        {
            string[] iParametros = new string[] { "@empresa", "@año", "@mes", "@lote", "@tipo" };
            string[] oParametros = new string[] { "@valor" };
            object[] objValores = new object[] { empresa, año, mes, lote, tipo };
            return Convert.ToDecimal(Cacceso.ExecProc("spSeleccionaPesoPromedioLotePeriodo", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public decimal valorPesoPeriodo(int empresa, DateTime fecha, string lote, string finca)
        {
            string[] iParametros = new string[] { "@empresa", "@fecha", "@lote", "@finca" };
            string[] oParametros = new string[] { "@valor" };
            object[] objValores = new object[] { empresa, fecha, lote, finca };
            return Convert.ToDecimal(Cacceso.ExecProc("spSeleccionaPesoLoteRacimosPeriodo", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }
    }
}