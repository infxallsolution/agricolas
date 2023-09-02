using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.Administracion
{
    public class CtipoTransaccion
    {
        public CtipoTransaccion()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        public DataView GetTipoTransaccionContable(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros("spGetTipoTransaccionContable", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }


        public DataView SeleccionaDocuemntoReferenciaFacturasTercero(int empresa, string tercero, string sucursal)
        {
            string[] iParametros = new string[] { "@empresa", "@tercero", "@sucursal" };
            object[] objValores = new object[] { empresa, tercero, sucursal };

            return Cacceso.DataSetParametros("spSeleccionaFacturasReferenciaTercero", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView SeleccionaCxCItemTercero(int empresa, string sucursal, string tercero, string item, decimal valor)
        {
            string[] iParametros = new string[] { "@tercero", "@empresa", "@item", "@sucursal", "@valor" };
            object[] objValores = new object[] { tercero, empresa, item, sucursal, valor };

            return Cacceso.DataSetParametros("spSeleccionaCxCfactura", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public int ActualizaConsecutivo(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipoTransaccion, empresa };

            return Convert.ToInt16(Cacceso.ExecProc("spActualizaConsecutivoTransaccion", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public DataView SeleccionaImpuestoItemClinte(int empresa, string sucursal, int tercero, int item)
        {
            string[] iParametros = new string[] { "@tercero", "@empresa", "@item", "@sucursal" };
            object[] objValores = new object[] { tercero, empresa, item, sucursal };

            return Cacceso.DataSetParametros("spSeleccionaImpuestoRetecionTerceroItemCliente", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public int RetornavalidacionRegistro(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@empresa" };
            string[] oParametros = new string[] { "@retorna" };
            object[] objValores = new object[] { tipoTransaccion, empresa };

            return Convert.ToInt16(Cacceso.ExecProc("SpRetornaDiaSemanaTransaccion", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }


        public DataView GetTipoTransaccionModuloForma(int empresa, int forma)
        {
            string[] iParametros = new string[] { "@empresa", "@forma", "@modulo" };
            object[] objValores = new object[] { empresa, forma, ConfigurationManager.AppSettings["Modulo"].ToString() };

            return Cacceso.DataSetParametros("spSeleccionTipoTransacionFormaModulo", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }


        public DataView GetTipoTransaccionModulo(int empresa)
        {
            DataView dvTipoTransaccion = CentidadMetodos.EntidadGet(
                "gTipoTransaccion",
                "ppa").Tables[0].DefaultView;

            dvTipoTransaccion.RowFilter = "modulo = '" + ConfigurationManager.AppSettings["ModuloNomina"].ToString() + "'and empresa =" + empresa;
            dvTipoTransaccion.Sort = "descripcion";

            return dvTipoTransaccion;
        }

        public DataView GetTipoTransaccionModuloContable(int empresa)
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