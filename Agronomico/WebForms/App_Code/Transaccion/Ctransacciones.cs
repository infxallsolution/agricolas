using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Transaccion
{
    public class Ctransacciones
    {
        public Ctransacciones()
        {
        }

        public int AnulaTransaccion(string tipo, string numero, string usuario, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@usuario", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, usuario, empresa };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spAnulaTrnAgronomico",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public DataView RetornaEncabezadoTransaccionTiquete(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Cacceso.DataSetParametros("spRetornaEncabezadoTransaccionLaboresTiquete", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public int ReliquidaPreciosLabores(int empresa, DateTime fechaInicial, DateTime fechaFinal, string usuario)
        {
            string[] iParametros = new string[] { "@empresa", "@fechaInicial", "@fechaFinal", "@usuario" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, fechaInicial, fechaFinal, usuario };

            return Convert.ToInt32(Cacceso.ExecProc("spReliquidacionPrecioLaboresFecha", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public int ReliquidaJornales(int empresa, DateTime fechaInicial, DateTime fechaFinal, string usuario)
        {
            string[] iParametros = new string[] { "@empresa", "@fechaInicial", "@fechaFinal", "@usuario" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, fechaInicial, fechaFinal, usuario };

            return Convert.ToInt32(Cacceso.ExecProc("spReliquidacionJornalesFecha", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public DataView RetornaEncabezadoTransaccionTiqueteReferencia(string numero, int empresa)
        {
            string[] iParametros = new string[] { "@numero", "@empresa" };
            object[] objValores = new object[] { numero, empresa };
            return Cacceso.DataSetParametros("spRetornaEncabezadoTransaccionLaboresTiqueteReferencia", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView RetornaEncabezadoTransaccionLabores(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };
            return Cacceso.DataSetParametros("spRetornaEncabezadoTransaccionLabores", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView RetornaEncabezadoTransaccionLaboresReferencia(string numero, int empresa)
        {
            string[] iParametros = new string[] { "@numero", "@empresa" };
            object[] objValores = new object[] { numero, empresa };
            return Cacceso.DataSetParametros("spRetornaEncabezadoTransaccionLaboresReferencia", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView RetornaEncabezadoTransaccionLaboresDetalle(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Cacceso.DataSetParametros("spRetornaEncabezadoTransaccionLaboresDetalle", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }


        public DataView RetornaEncabezadoTransaccionLaboresDetalleCargue(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Cacceso.DataSetParametros("spRetornaEncabezadoTransaccionLaboresDetalleCargue", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView RetornaEncabezadoTransaccionLaboresDetalleReferencia(string numero, int empresa)
        {
            string[] iParametros = new string[] { "@numero", "@empresa" };
            object[] objValores = new object[] { numero, empresa };

            return Cacceso.DataSetParametros(
                "spRetornaEncabezadoTransaccionLaboresDetalleReferencia",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView RetornaEncabezadoTransaccionLaboresTerceroCargue(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Cacceso.DataSetParametros(
                "spRetornaEncabezadoTransaccionLaboresTerceroCargue",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public DataView RetornaEncabezadoTransaccionLaboresTerceroTransporte(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Cacceso.DataSetParametros(
                "spRetornaEncabezadoTransaccionLaboresTerceroTransporte",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView RetornaEncabezadoTransaccionLaboresTercero(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Cacceso.DataSetParametros(
                "spRetornaEncabezadoTransaccionLaboresTercero",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public DataView RetornaEncabezadoTransaccionLaboresTerceroReferencia(string numero, int empresa)
        {
            string[] iParametros = new string[] { "@numero", "@empresa" };
            object[] objValores = new object[] { numero, empresa };

            return Cacceso.DataSetParametros(
                "spRetornaEncabezadoTransaccionLaboresTerceroReferencia",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public int VerificaEdicionBorrado(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spVerificaEdicionBorradoTransaccionesLabores",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView GetTransaccionCompleta(string where, int empresa)
        {
            string[] iParametros = new string[] { "@where", "@empresa" };
            object[] objValores = new object[] { where, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaTransaccionCompletaSanidad",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView SeleccionaAjusteTrabajadores(DateTime fi, DateTime ff, string finca, int empresa)
        {
            string[] iParametros = new string[] { "@fi", "@ff", "@finca", "@empresa" };
            object[] objValores = new object[] { fi, ff, finca, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionAjusteTrabajadoresRendimiento",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public DataView GetTransaccionCompletaLabores(string where, int empresa)
        {
            string[] iParametros = new string[] { "@where", "@empresa" };
            object[] objValores = new object[] { where, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaTransaccioncompletaLabores",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetTransaccionCompletaTiquete(string where, int empresa)
        {
            string[] iParametros = new string[] { "@where", "@empresa" };
            object[] objValores = new object[] { where, empresa };
            return Cacceso.DataSetParametros("spSeleccionaTransaccionTiqueteAgronomico", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView GetCamposEntidades(string id1, string id2)
        {
            string[] iParametros = new string[] { "@id1", "@id2" };
            object[] objValores = new object[] { id1, id2 };
            return Cacceso.DataSetParametros("spSeleccionaCamposEntidadesII", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView GetTipoTransaccionModulo(int empresa)
        {
            DataView dvTipoTransaccion = CentidadMetodos.EntidadGet("gTipoTransaccion", "ppa").Tables[0].DefaultView;
            dvTipoTransaccion.RowFilter = "activo = True and modulo= '" + ConfigurationManager.AppSettings["modulo"].ToString() + "'" + " and empresa = " + empresa.ToString();
            dvTipoTransaccion.Sort = "descripcion";
            return dvTipoTransaccion;
        }

        public string RetornaNumeroTransaccion(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipoTransaccion", "@empresa" };
            string[] oParametros = new string[] { "@consecutivo" };
            object[] objValores = new object[] { tipoTransaccion, empresa };

            return Convert.ToString(Cacceso.ExecProc(
                "spRetornaConsecutivoTransaccion",
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

        public int RetornaReferenciaTipoTransaccion(string tipoTransaccion)
        {
            string[] iParametros = new string[] { "@tipoTransaccion", "@empresa" };
            string[] oParametros = new string[] { "@referencia" };
            object[] objValores = new object[] { tipoTransaccion };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spRetornaReferenciaTipoTransaccion",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int AnulaTransaccionCompleta(string tipo, string numero, string usuario, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@usuario", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, usuario, empresa };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spAnulaTrnAgronomico",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int EliminarTransaccionLabores(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spEliminarTransaccionLabores",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int validaEjecutarTransaccion(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, empresa };
            return Convert.ToInt32(Cacceso.ExecProc("spValidaEjecutarTransaccion", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public int ActualizaReferencia(string numero, int empresa)
        {
            string[] iParametros = new string[] { "@numero", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { numero, empresa };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spActualizaReferenciaTrnAgro",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView SelccionaTercernoNovedad(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "spSelccionaTercernoNovedad",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView SelccionaCuadrillasDestajo(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaCuadrillasDestajo",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView SeleccionaNovedadLoteRangoSiembra(string lote, int empresa, DateTime fechaLabor)
        {
            string[] iParametros = new string[] { "@lote", "@empresa", "@fechaLabor" };
            object[] objValores = new object[] { lote, empresa, fechaLabor };

            return Cacceso.DataSetParametros("spSeleccionaNovedadLoteRangoSiembra",
                iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView RetornaEncabezadoTransaccionSanidad(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };
            return Cacceso.DataSetParametros("spRetornaEncabezadoTransaccionSanidad", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public int ValidarNumeroPalmas(string concepto, string lote, string linea, int empresa, string cantidad)
        {
            string[] iParametros = new string[] { "@concepto", "@lote", "@linea", "@empresa", "@cantidad" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { concepto, lote, linea, empresa, cantidad };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spValidarNumeroPalmas",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView RetornaTransaccionCompletaSanidadDetalle(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Cacceso.DataSetParametros("spRetornaTransaccionCompletaSanidadDetalle", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public int AnulaTransaccionCompletaSanidad(string tipo, string numero, string usuario, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@usuario", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, usuario, empresa };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spAnulaTrnSanidad",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public int ApruebaTransaccionCompletaSanidad(string tipo, string numero, string usuario, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@usuario", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, usuario, empresa };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spApruebaTransaccionCompletaSanidad",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView RetornaEncabezadoTransaccionLaboresItems(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Cacceso.DataSetParametros(
                "spRetornaEncabezadoTransaccionLaboresItems",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }



    }
}