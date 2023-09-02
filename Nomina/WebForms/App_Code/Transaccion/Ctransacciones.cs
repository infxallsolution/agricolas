using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Transaccion
{
    public class Ctransacciones
    {

        public Ctransacciones()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public DataView RetornaEncabezadoTransaccionTiquete(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Cacceso.DataSetParametros(
                "spRetornaEncabezadoTransaccionLaboresTiquete",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public int AnulaLiquidacionCesantias(string tipo, string numero, string usuario, int empresa, int año, int periodo)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@usuario", "@empresa", "@año", "@periodo" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, usuario, empresa, año, periodo };
            return Convert.ToInt16(Cacceso.ExecProc("spAnulaLiquidacionCesantias", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public DataView RetornaEncabezadoTransaccionTiqueteReferencia(string numero, int empresa)
        {
            string[] iParametros = new string[] { "@numero", "@empresa" };
            object[] objValores = new object[] { numero, empresa };

            return Cacceso.DataSetParametros(
                "spRetornaEncabezadoTransaccionLaboresTiqueteReferencia",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView RetornaEncabezadoTransaccionLabores(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Cacceso.DataSetParametros(
                "spRetornaEncabezadoTransaccionLabores",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView RetornaEncabezadoTransaccionLaboresReferencia(string numero, int empresa)
        {
            string[] iParametros = new string[] { "@numero", "@empresa" };
            object[] objValores = new object[] { numero, empresa };

            return Cacceso.DataSetParametros(
                "spRetornaEncabezadoTransaccionLaboresReferencia",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView RetornaEncabezadoTransaccionLaboresDetalle(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Cacceso.DataSetParametros(
                "spRetornaEncabezadoTransaccionLaboresDetalle",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
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

            return Convert.ToInt16(Cacceso.ExecProc(
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

            return Cacceso.DataSetParametros("spSeleccionaTransaccionCompletaNovedades", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }


        public DataView GetTransaccionCompletaLiquidacion(string where, int empresa)
        {
            string[] iParametros = new string[] { "@where", "@empresa" };
            object[] objValores = new object[] { where, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaTransaccionCompletaLiquidacionNomina",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public DataView GetTransaccionCompletaLabores(string where, int empresa)
        {
            string[] iParametros = new string[] { "@where", "@empresa" };
            object[] objValores = new object[] { where, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaTransaccionCompletaLabores",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetCamposEntidades(string id1, string id2)
        {
            string[] iParametros = new string[] { "@id1", "@id2" };
            object[] objValores = new object[] { id1, id2 };

            return Cacceso.DataSetParametros(
                "spSeleccionaCamposEntidadesII",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public DataView GetTipoTransaccionModulo(int empresa)
        {
            DataView dvTipoTransaccion = CentidadMetodos.EntidadGet(
                "gTipoTransaccion",
                "ppa").Tables[0].DefaultView;

            dvTipoTransaccion.RowFilter = "modulo= '" + ConfigurationManager.AppSettings["modulo"].ToString() + "'" + " and empresa = " + empresa.ToString();
            dvTipoTransaccion.Sort = "descripcion";

            return dvTipoTransaccion;
        }

        public string RetornaNumeroTransaccion(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipoTransaccion", "@empresa" };
            string[] oParametros = new string[] { "@consecutivo" };
            object[] objValores = new object[] { tipoTransaccion, empresa };

            return Convert.ToString(Cacceso.ExecProc("spRetornaConsecutivoTransaccion", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public int ActualizaConsecutivo(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipoTransaccion, empresa };

            return Convert.ToInt16(Cacceso.ExecProc(
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

            return Convert.ToInt16(Cacceso.ExecProc(
                "spRetornaReferenciaTipoTransaccion",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView SeleccionanNovedadesDetalle(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Cacceso.DataSetParametros("spSeleccionanNovedadesDetalle",
                iParametros, objValores, "ppa").Tables[0].DefaultView;
        }


        public int AnulaTransaccion(string tipo, string numero, string usuario, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@usuario", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, usuario, empresa };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spAnulaTrnNovedades",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int AnulaLiquidacionDefinitiva(string tipo, string numero, string usuario, int empresa, int año, int mes, int periodo)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@usuario", "@empresa", "@año", "@mes", "@periodo" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, usuario, empresa, año, mes, periodo };
            return Convert.ToInt16(Cacceso.ExecProc("spAnulaLiquidacionDefinitiva", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }
        public int AnulaLiquidacionPrima(string tipo, string numero, string usuario, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@usuario", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, usuario, empresa };
            return Convert.ToInt16(Cacceso.ExecProc("spAnulaLiquidacionPrima", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }
        public int EliminarTransaccionLabores(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spEliminarTransaccionLabores",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int ActualizaReferencia(string numero, int empresa)
        {
            string[] iParametros = new string[] { "@numero", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { numero, empresa };

            return Convert.ToInt16(Cacceso.ExecProc(
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

        public int EditaEncabezado(string tipo, string numero, DateTime fecha, string empleado, string concepto, string ccosto, string remision, string observacion, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@fecha", "@empleado", "@concepto", "@ccosto", "@remision", "@observacion", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tipo, numero, fecha, empleado, concepto, ccosto, remision, observacion, empresa };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spEditaEncabezadoNovedades",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int EditaDetalle(string tipo, string numero, int registro, decimal cantidad, decimal valor, string concepto, string empleado, string detalle, decimal frecuencia,
                                        int periodoInicial, int periodoFinal, int añoInicial, int añoFinal, int empresa, bool anulado)
        {
            string[] iParametros = new string[] { "@anulado","@añoInicial","@añoFinal","@cantidad","@concepto","@detalle","@empleado","@empresa","@frecuencia","@numero",
                                                    "@periodoFinal","@periodoInicial","@registro","@tipo","@valor" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { anulado, añoInicial, añoFinal, cantidad, concepto, detalle, empleado, empresa, frecuencia, numero, periodoFinal, periodoInicial, registro, tipo, valor };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spEditaDetalleNovedades",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public DataView SeleccionaDocumentosNominaxPeriodo(int empresa, int año, int periodo)
        {
            string[] iParametros = new string[] { "@empresa", "@año", "@periodo" };
            object[] objValores = new object[] { empresa, año, periodo };

            return Cacceso.DataSetParametros(
                "spSeleccionaDocumentosNominaxPeriodo",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public int actualizaLiquidacionCesantias(int empresa, string documento, int empleado, string usuario, decimal diasCesantias, decimal valorCesantias, decimal valorIntereses,
            decimal basecesantias, decimal valorPromedio, int diasPromedio)
        {
            string[] iParametros = new string[] { "@empresa",
"@documento",
"@empleado",
"@usuario",
"@diasCesantias",
"@valorCesantias",
"@valorIntereses",
"@valorBase",
"@valorPromedio",
"@diasPromedio",

        };
            object[] objValores = new object[] { empresa, documento, empleado, usuario,
            diasCesantias, valorCesantias, valorIntereses, basecesantias, valorPromedio, diasPromedio  };
            string[] oParametros = new string[] { "@retorno" };


            return Convert.ToInt32(Cacceso.ExecProc("spActualizaLiquidacionCesantiasDefinitiva", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }





    }
}