using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class CcambiarSueldo
    {
        public CcambiarSueldo()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        public int CambiarSueldoTercero(int empresa, string ccosto, string empleado, int formaLiquidacion, int tipo, decimal porcentaje, decimal valor, decimal sueldoAnterior, decimal sueldoNuevo)
        {
            string[] iParametros = new string[] { "@empresa", "@ccosto", "@empleado", "@formaLiquidacion", "@tipo", "@sueldoAnterior", "@sueldoNuevo", "@porcentaje", "@valorSueldo" };
            object[] objValores = new object[] { empresa, ccosto, empleado, formaLiquidacion, tipo, sueldoAnterior, sueldoNuevo, porcentaje, valor };
            string[] oParametros = new string[] { "@retorno" };
            object[] rerotnos = Cacceso.ExecProc("spCambiarSueldo", iParametros, oParametros, objValores, "ppa");
            return Convert.ToInt16(rerotnos.GetValue(0));
        }

    }
}