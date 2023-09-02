using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class Cembargos
    {
        public Cembargos()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }



        public DataView SelecccionaContratosTercero(int tercero, int empresa)
        {
            string[] iParametros = new string[] { "@tercero", "@empresa" };
            object[] objValores = new object[] { tercero, empresa };
            return Cacceso.DataSetParametros("spSeleccionaContratoTercero", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }


        public DataView RetornaDatos(string tercero, int contrato, int empresa, int numero)
        {
            string[] iParametros = new string[] { "@tercero", "@contrato", "@empresa", "@numero" };
            object[] objValores = new object[] { tercero, contrato, empresa, numero };

            return Cacceso.DataSetParametros("SpGetnEmbargoskey", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "nEmbargos",
                "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and ( (desempleado  like '%" + texto + "%') or (codEmpleado  like '%" + texto + "%'))";

            return dvEntidad;
        }

        public string Consecutivo(int empresa, string idtercero)
        {
            string[] iParametros = new string[] { "@empresa", "@tercero" };
            string[] oParametros = new string[] { "@consecutivo" };
            object[] objValores = new object[] { empresa, idtercero };

            return Convert.ToString(Cacceso.ExecProc(
                "spConsecutivoEmbargoTercero",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView RetornaDatosEmbargos(int empresa, string empleado, string tipo, string codigo)
        {
            string[] iParametros = new string[] { "@empresa", "@empleado", "@tipo", "@codigo" };
            object[] objValores = new object[] { empresa, empleado, tipo, codigo };

            return Cacceso.DataSetParametros(
                "spRetornaDatosEmbargos",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }
    }
}