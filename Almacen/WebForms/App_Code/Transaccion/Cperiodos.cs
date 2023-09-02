using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Almacen.WebForms.App_Code.Transaccion
{
    public class Cperiodos
    {
        public Cperiodos()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public int RetornaPeriodoCerrado(int año, int mes, int empresa)
        {
            string[] iParametros = new string[] { "@año", "@mes", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { año, mes, empresa };
            return Convert.ToInt16(Cacceso.ExecProc("spRetornaPeriodoCerradoConta", iParametros, oParametros, objValores, "ppa").GetValue(0));
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
    }
}