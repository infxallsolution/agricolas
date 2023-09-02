using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Transaccion
{
    public class CsanidadTransaccion
    {
        public CsanidadTransaccion()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }



        public DataView BuscarEntidadVigencia(string texto, int empresa)
        {
            DataView dvEntidad = CentidadMetodos.EntidadGet(
                "iTipoTransaccionVigencia",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (tipoTransaccion like '%" + texto + "%')";

            return dvEntidad;
        }

        public string RetornaConsecutivo(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipoTransaccion", "@empresa" };
            string[] oParametros = new string[] { "@consecutivo" };
            object[] objValores = new object[] { tipoTransaccion, empresa };

            return Convert.ToString(
                Cacceso.ExecProc(
                    "spRetornaConsecutivoTransaccion",
                    iParametros,
                    oParametros,
                    objValores,
                    "ppa").GetValue(0));
        }

        public int RetornaNaturalezaTransaccion(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipoTransaccion", "@empresa" };
            string[] oParametros = new string[] { "@naturaleza" };
            object[] objValores = new object[] { tipoTransaccion, empresa };

            return Convert.ToInt32(
                Cacceso.ExecProc(
                    "spRetornaNaturalezaTransaccion",
                    iParametros,
                    oParametros,
                    objValores,
                    "ppa").GetValue(0));
        }

        public int RetornaVigenciaTransaccion(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipoTransaccion", "@empresa" };
            string[] oParametros = new string[] { "@vigencia" };
            object[] objValores = new object[] { tipoTransaccion, empresa };

            return Convert.ToInt32(
                Cacceso.ExecProc(
                    "spRetornaVigenciaTransaccion",
                    iParametros,
                    oParametros,
                    objValores,
                    "ppa").GetValue(0));
        }

        public string RetornaTerceroUsuario(string usuario, int empresa)
        {
            string[] iParametros = new string[] { "@usuario", "@empresa" };
            string[] oParametros = new string[] { "@tercero" };
            object[] objValores = new object[] { usuario, empresa };

            return Convert.ToString(
                Cacceso.ExecProc(
                    "spRetornaTerceroUsuario",
                    iParametros,
                    oParametros,
                    objValores,
                    "ppa").GetValue(0));
        }

        public string RetornaTipotransaccion(string numero, int empresa)
        {
            string[] iParametros = new string[] { "@numero", "@empresa" };
            string[] oParametros = new string[] { "@tipo" };
            object[] objValores = new object[] { numero, empresa };

            return Convert.ToString(
                Cacceso.ExecProc(
                    "SpRetornaTipoTransaccionDocRef",
                    iParametros,
                    oParametros,
                    objValores,
                    "ppa").GetValue(0));
        }

        public int ActualizaConsecutivo(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipoTransaccion, empresa };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spActualizaConsecutivoTransaccion",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView GetProductoTipoTransaccion(string tipo, int empresa)
        {
            string[] iParametros = new string[] { "@transaccion", "@empresa" };
            object[] objValores = new object[] { tipo, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaProductoTransaccion",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetTipoTransaccionModuloImpresion(string modulo, int empresa)
        {
            string[] iParametros = new string[] { "@modulo", "@empresa" };
            object[] objValores = new object[] { modulo, empresa };

            return Cacceso.DataSetParametros(
                "SpSeleccionaTipoTransacciomImp",
                iParametros, objValores,
                "ppa").Tables[0].DefaultView;
        }

        public int ValidaCantidadRegistros(string numeroRef, string tercero, int empresa)
        {
            string[] iParametros = new string[] { "@numeroRef", "@tercero", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { numeroRef, tercero, empresa };

            return Convert.ToInt32(
                Cacceso.ExecProc(
                    "spVelidaCantidadRegistrosReferencia",
                    iParametros,
                    oParametros,
                    objValores,
                    "ppa").GetValue(0));
        }


        public decimal ValidaCantidadMaximaRegistro(string tipo, string numero, int registro, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@registro", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, registro, empresa };

            return Convert.ToDecimal(
                Cacceso.ExecProc(
                    "spRetornaSaldoTipoMovimientosProducto",
                    iParametros,
                    oParametros,
                    objValores,
                    "ppa").GetValue(0));
        }

        public DataView GetTipoTransaccionModulo(int empresa)
        {
            DataView dvTipoTransaccion = CentidadMetodos.EntidadGet(
                "gTipoTransaccion",
                "ppa").Tables[0].DefaultView;

            dvTipoTransaccion.RowFilter = "empresa=" + Convert.ToString(empresa) + " and  modulo = '" + ConfigurationManager.AppSettings["Modulo"].ToString() + "'";
            dvTipoTransaccion.Sort = "descripcion";

            return dvTipoTransaccion;
        }

        public int RetornaReferenciaTipoTransaccion(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipoTransaccion", "@empresa" };
            string[] oParametros = new string[] { "@referencia" };
            object[] objValores = new object[] { tipoTransaccion, empresa };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spRetornaReferenciaTipoTransaccion",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int RetornavalidacionRegistro(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@empresa" };
            string[] oParametros = new string[] { "@retorna" };
            object[] objValores = new object[] { tipoTransaccion, empresa };

            return Convert.ToInt32(Cacceso.ExecProc(
                "SpRetornaDiaSemanaRequisicion",
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

        public DataView GetReferencia(string tercero, string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tercero", "@empresa" };
            object[] objValores = new object[] { tercero, empresa };

            return Cacceso.DataSetParametros(
                RetornaDsTipoTransaccion(tipoTransaccion, empresa),
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView ExecReferenciaDetalle(string sp, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@numero", "@empresa" };
            object[] objValores = new object[] { numero, empresa };

            return Cacceso.DataSetParametros(
                sp,
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





    }
}