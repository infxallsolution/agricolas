using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class Ccontratistas
    {
        public Ccontratistas()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        public DataView BuscaRegistros(string texto, DateTime fi, DateTime ff, int empresa)
        {
            string[] iParametros = new string[] { "@fi", "@ff", "@filtro", "@empresa" };
            string[] oParametros = new string[] { };
            object[] objValores = new object[] { fi, ff, texto, empresa };
            return Cacceso.DataSetParametros("spSeleccionaRegistroContratistasFecha", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }


        public int GuardaRegistroManual(DateTime fecha, DateTime fechaE, DateTime fechaS, string funcionario, string tipoEntrada, int empresa)
        {
            string[] iParametros = new string[] { "@fecha", "@fechaEntrada", "@fechaSalida", "@funcionario", "@tipoEntrada", "@empresa" };
            string[] oParametros = new string[] { "@Retorno" };
            object[] objValores = new object[] { fecha, fechaE, fechaS, funcionario, tipoEntrada, empresa };

            return Convert.ToInt16(Cacceso.ExecProc("SpInsertanRegistroContratista", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public int ActualizaRegistroManual(DateTime fecha, DateTime fechaE, DateTime fechaS, string funcionario, string tipoEntrada, int empresa)
        {
            string[] iParametros = new string[] { "@fecha", "@fechaEntrada", "@fechaSalida", "@funcionario", "@tipoEntrada", "@empresa" };
            string[] oParametros = new string[] { "@Retorno" };
            object[] objValores = new object[] { fecha, fechaE, fechaS, funcionario, tipoEntrada, empresa };

            return Convert.ToInt16(Cacceso.ExecProc("SpActualizanRegistroContratista", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }
    }
}