using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Almacen.WebForms.App_Code.General
{
    public class Cgeneral
    {
        public Cgeneral()
        {
        }

        public DataView spRetornaTipoTransaccionFormatoModulo(int empresa, string modulo)
        {
            DataView dvEntidad = new DataView();
            string[] iParametros = new string[] { "@empresa", "@modulo" };
            object[] objValores = new object[] { empresa, modulo };
            dvEntidad = Cacceso.DataSetParametros("spRetornaTipoTransaccionFormatoModulo", iParametros, objValores, "ppa").Tables[0].DefaultView;
            return dvEntidad;

        }

        public DataView retornaDatosReporteador(string modulo)
        {
            string[] iParametros = new string[] { "@modulo" };
            object[] objValores = new object[] { modulo };

            return Cacceso.DataSetParametros("spRetornaDatosReporte", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        static public string RetornaConsecutivoAutomatico(string tabla, string nombreCampo, int empresa)
        {
            DataView dvEntidad = new DataView();
            string[] iParametros = new string[] { "@tabla", "@campo", "@empresa" };
            object[] objValores = new object[] { tabla, nombreCampo, empresa };
            dvEntidad = Cacceso.DataSetParametros("spRetornaConsecutivoAutomatico", iParametros, objValores, "ppa").Tables[0].DefaultView;
            return dvEntidad.Table.Rows[0].ItemArray[0].ToString();

        }



        public string retornaFormatoTipoTransaccion(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipoTransaccion", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipoTransaccion, empresa };

            return Convert.ToString(Cacceso.ExecProc("spRetornaFormatoTipoTransaccion", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }




        public int ActualizaConsecutivo(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipoTransaccion, empresa };

            return Convert.ToInt16(Cacceso.ExecProc("spActualizaConsecutivoTransaccion", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }


        public string RetornaConsecutivo(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipoTransaccion", "@empresa" };
            string[] oParametros = new string[] { "@consecutivo" };
            object[] objValores = new object[] { tipoTransaccion, empresa };
            return Convert.ToString(Cacceso.ExecProc("spRetornaConsecutivoTransaccion", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }


        public string RetornoParametroGeneral(string campo, int empresa)
        {
            DataView dvEntidad = new DataView();
            string[] iParametros = new string[] { "@campo", "@empresa" };
            object[] objValores = new object[] { campo, empresa };
            dvEntidad = Cacceso.DataSetParametros("spRetornovalorParametrosGeneral", iParametros, objValores, "ppa").Tables[0].DefaultView;
            return dvEntidad.Table.Rows[0].ItemArray[0].ToString();
        }

        static public string RetornaConsecutivoAutomaticoSinEmpresa(string tabla, string nombreCampo)
        {
            DataView dvEntidad = new DataView();
            string[] iParametros = new string[] { "@tabla", "@campo" };
            object[] objValores = new object[] { tabla, nombreCampo };
            dvEntidad = Cacceso.DataSetParametros("spRetornaConsecutivoAutomaticoSinEmpresa", iParametros, objValores, "ppa").Tables[0].DefaultView;
            return dvEntidad.Table.Rows[0].ItemArray[0].ToString();
        }

        public DataView SeleccionaTerceros(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };
            return Cacceso.DataSetParametros("spSeleccionaTercerosGeneral", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
    }
}