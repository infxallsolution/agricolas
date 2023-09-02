using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Almacen.WebForms.App_Code.Transaccion
{
    public class CtipoTransaccion
    {
        public CtipoTransaccion()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        public static DataView cargarNumeroTipoOrdenTrabajo(int empresa, string tipo)
        {
            string[] iParametros = new string[] { "@empresa", "@tipo" };
            object[] objValores = new object[] { empresa, tipo };

            return Cacceso.DataSetParametros("spSeleccionaNumeroAdicionarMtto", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView GetTipoTransaccionModulo(int empresa)
        {
            DataView dvTipoTransaccion = CentidadMetodos.EntidadGet("gTipoTransaccion", "ppa").Tables[0].DefaultView;
            dvTipoTransaccion.RowFilter = " empresa = " + Convert.ToString(empresa) + "and modulo = '" + ConfigurationManager.AppSettings["Modulo"].ToString() + "'";
            dvTipoTransaccion.Sort = "descripcion";

            return dvTipoTransaccion;
        }

        public DataView SolicitudesSinOrdenCompra(int empresa, string tipo)
        {
            string[] iParametros = new string[] { "@empresa", "@tipo" };
            object[] objValores = new object[] { empresa, tipo };

            return Cacceso.DataSetParametros("psSeleccionasolicitudesSinOrden", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView SolicitudesSinOrdenCompraTipo(int empresa, string tipo)
        {
            string[] iParametros = new string[] { "@empresa", "@tipo" };
            object[] objValores = new object[] { empresa, tipo };

            return Cacceso.DataSetParametros("psSeleccionasolicitudesSinOrdenTipo", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }


        public DataView GetTipoTransaccionModuloForma(int empresa, int forma)
        {
            string[] iParametros = new string[] { "@empresa", "@forma", "@modulo" };
            object[] objValores = new object[] { empresa, forma, ConfigurationManager.AppSettings["Modulo"].ToString() };

            return Cacceso.DataSetParametros("spSeleccionTipoTransacionFormaModulo", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView GetTipoTransaccionModuloMantenimiento(int empresa, int forma)
        {
            string[] iParametros = new string[] { "@empresa", "@forma", "@modulo" };
            object[] objValores = new object[] { empresa, forma, ConfigurationManager.AppSettings["ModuloMto"].ToString() };

            return Cacceso.DataSetParametros("spSeleccionTipoTransacionFormaModulo", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }


        public string TipoTransaccionConfig(string tipoTransaccion, int empresa)
        {
            string retorno = "";
            object[] objKey = new object[] { empresa, tipoTransaccion };

            foreach (DataRowView registro in CentidadMetodos.EntidadGetKey("gTipoTransaccionConfig", "ppa", objKey).Tables[0].DefaultView)
            {
                for (int i = 2; i < registro.Row.ItemArray.Length; i++)
                {
                    retorno = retorno + registro.Row.ItemArray.GetValue(i).ToString() + "*";
                }
            }

            return retorno;
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

        public DataView GetReferenciaTercero(string tercero, string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tercero", "@empresa" };
            object[] objValores = new object[] { tercero, empresa };

            return Cacceso.DataSetParametros(
                RetornaDsTipoTransaccion(tipoTransaccion, empresa),
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetReferencia(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };
            return Cacceso.DataSetParametros(RetornaDsTipoTransaccion(tipoTransaccion, empresa), iParametros, objValores, "ppa").Tables[0].DefaultView;
        }


        public string RetornaTipoBorrado(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipoTransaccion", "@empresa" };
            string[] oParametros = new string[] { "@tipoBorrado" };
            object[] objValores = new object[] { tipoTransaccion, empresa };
            return Convert.ToString(Cacceso.ExecProc("spRetornaModoBorradoTransaccion", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public int RetornaReferenciaTipoTransaccion(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipoTransaccion", "@empresa" };
            string[] oParametros = new string[] { "@referencia" };
            object[] objValores = new object[] { tipoTransaccion, empresa };

            return Convert.ToInt16(Cacceso.ExecProc("spRetornaReferenciaTipoTransaccion", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }


        public int RetornavalidacionRegistro(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@empresa" };
            string[] oParametros = new string[] { "@retorna" };
            object[] objValores = new object[] { tipoTransaccion, empresa };

            return Convert.ToInt16(Cacceso.ExecProc("SpRetornaDiaSemanaTransaccion", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }


        public DataView ExecReferenciaDetalle(string sp, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@numero", "@empresa" };
            object[] objValores = new object[] { numero, empresa };

            return Cacceso.DataSetParametros(sp, iParametros, objValores, "ppa").Tables[0].DefaultView;
        }


        public DataView DatosEditarCotizacion(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Cacceso.DataSetParametros("SpGetiTransaccionDetallekey", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView datosImpuestoEditarCotizacion(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Cacceso.DataSetParametros("SpGetiTransaccionImpuestokey", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView SeleccionaImpuestoItemTercero(int empresa, string sucursal, int tercero, int item)
        {
            string[] iParametros = new string[] { "@tercero", "@empresa", "@item", "@sucursal" };
            object[] objValores = new object[] { tercero, empresa, item, sucursal };

            return Cacceso.DataSetParametros("spSeleccionaImpuestoRetecionTerceroItem", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView SeleccionaSoloImpuestoItemTercero(int empresa, string sucursal, int tercero, int item, decimal valorItem)
        {
            string[] iParametros = new string[] { "@tercero", "@empresa", "@item", "@sucursal", "@valorItem" };
            object[] objValores = new object[] { tercero, empresa, item, sucursal, valorItem };

            return Cacceso.DataSetParametros("spSeleccionaSoloImpuestoTerceroItem", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public int ActualizaConsecutivo(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipoTransaccion, empresa };

            return Convert.ToInt16(Cacceso.ExecProc("spActualizaConsecutivoTransaccion", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

    }
}