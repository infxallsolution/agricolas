using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Administracion.WebForms.App_Code.Operacion
{
    public class Cremision
    {
        public Cremision()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "bRemision",
                "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";

            return dvEntidad;
        }

        public DataView ConteoEstado(string estado, int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            DataView dvRemision = Cacceso.DataSetParametros(
                "spRetornaConteoRemisionMp", iParametros, objValores,
                "ppa").Tables[0].DefaultView;

            dvRemision.RowFilter = "estado like '%" + estado + "%'";

            return dvRemision;
        }

        public DataView OperadoresLogistico(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            DataView dvRemision = Cacceso.DataSetParametros(
                "spSeleccionaOperadoresLogistico", iParametros, objValores,
                "ppa").Tables[0].DefaultView;

            return dvRemision;
        }

        public int GeneraRemision(int numero, string usuario, int empresa)
        {
            string[] iParametros = new string[] { "@nroremision", "@usuario", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { numero, usuario, empresa };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spGeneraRemision",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int CambiaEstadoRemisiones(string estadoAnterior, string estadoNuevo, string asignado, int empresa)
        {
            string[] iParametros = new string[] { "@estadoAnterior", "@estadoNuevo", "@asignado", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { estadoAnterior, estadoNuevo, asignado, empresa };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spCambiaEstadoRemisionesMp",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

    }
}