using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Almacen.WebForms.App_Code.Transaccion
{
    public class Cdestinos
    {
        public Cdestinos()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }



        public int ConsultaMostrarCuenta(string destino, Boolean inverson, int empresa)
        {
            string[] iParametros = new string[] { "@destino", "@inversion", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { destino, inverson, empresa };

            return Convert.ToInt16(Cacceso.ExecProc(
                "SpValidaCuentaPadreDestino",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView CuentasAuxiliares(string destino, Boolean inversion, int empresa)
        {
            string[] iParametros = new string[] { "@destino", "@inversion", "@empresa" };
            object[] objValores = new object[] { destino, inversion, empresa };

            return Cacceso.DataSetParametros(
                "SpSeleccionaCuentaDestinos",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public int ConsultaCuentaCentroCosto(string cuenta, int empresa)
        {
            string[] iParametros = new string[] { "@cuenta", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { cuenta, empresa };

            return Convert.ToInt16(Cacceso.ExecProc(
                "SpValidaCentroCostoCuenta",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int ValidaCuentaMayor(string cuenta, int empresa)
        {
            string[] iParametros = new string[] { "@cuenta", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { cuenta, empresa };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spValidaCuentaMayor",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int ValidaDestinoInversion(string destino, Boolean inversion, int empresa)
        {
            string[] iParametros = new string[] { "@destino", "@inversion", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { destino, inversion, empresa };

            return Convert.ToInt16(Cacceso.ExecProc(
                "SpValidaDestinosCuenta",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


    }
}