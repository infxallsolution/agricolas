using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Transaccion
{
    public class CtipoTransaccion
    {
        public CtipoTransaccion()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }



        public DataView GetTipoTransaccionModulo(int empresa)
        {
            DataView dvTipoTransaccion = CentidadMetodos.EntidadGet(
                "gTipoTransaccion",
                "ppa").Tables[0].DefaultView;

            dvTipoTransaccion.RowFilter = "modulo = '" + ConfigurationManager.AppSettings["Modulo"].ToString() + "'and empresa =" + empresa;
            dvTipoTransaccion.Sort = "descripcion";

            return dvTipoTransaccion;
        }

        public string RetornaTipoBorrado(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipoTransaccion", "@empresa" };
            string[] oParametros = new string[] { "@tipoBorrado" };
            object[] objValores = new object[] { tipoTransaccion, empresa };

            return Convert.ToString(Cacceso.ExecProc(
                "spRetornaModoBorradoTransaccion",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView GetReferencia(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                RetornaDsTipoTransaccion(tipoTransaccion, empresa),
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        private string RetornaDsTipoTransaccion(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipoTransaccion", "@empresa" };
            string[] oParametros = new string[] { "@ds" };
            object[] objValores = new object[] { tipoTransaccion, empresa };

            return Convert.ToString(Cacceso.ExecProc(
                "spRetornaDsTipoTransaccion",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }
        public DataView SeleccionaNovedadTipoDocumentos(string tipo, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@tipo" };
            object[] objValores = new object[] { empresa, tipo };

            return Cacceso.DataSetParametros(
                "spSeleccionaNovedadesxTipo",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public string TipoTransaccionConfig(string tipoTransaccion, int empresa)
        {
            string retorno = "";
            object[] objKey = new object[] { empresa, tipoTransaccion };

            foreach (DataRowView registro in CentidadMetodos.EntidadGetKey(
                "gTipoTransaccionConfig",
                "ppa",
                objKey).Tables[0].DefaultView)
            {
                for (int i = 2; i < registro.Row.ItemArray.Length; i++)
                {
                    retorno = retorno + registro.Row.ItemArray.GetValue(i).ToString() + "*";
                }
            }

            return retorno;
        }

        public Boolean ValidaCantidadValor(int empresa, string concepto)
        {
            string[] iParametros = new string[] { "@empresa", "@concepto" };
            string[] oParametros = new string[] { "@valida" };
            object[] objValores = new object[] { empresa, concepto };

            if (Convert.ToInt16(Cacceso.ExecProc(
                "spValidaCantidadValorConcepto", iParametros, oParametros, objValores, "ppa").GetValue(0)) == 1)
                return true;
            else
                return false;
        }

    }
}