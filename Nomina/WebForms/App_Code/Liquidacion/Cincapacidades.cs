using Nomina.WebForms.App_Code.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Liquidacion
{
    public class Cincapacidades
    {
        public Cincapacidades()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        public int InsertaDetalleIncapacidad(int empresa, string tercero, int contrato, string numero, DateTime fecha, decimal valor, decimal valorPagar)
        {
            string[] iParametros = new string[] { "@empresa", "@tercero", "@contrato", "@numero", "@fecha", "@valor", "@valorPagar" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, tercero, contrato, numero, fecha, valor, valorPagar };

            return Convert.ToInt32(Cacceso.ExecProc("spInsertaDetalleAusentismo", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public int EliminaIncapacidadDetalle(string tercero, int contrato, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tercero", "@empresa", "@contrato", "@numero" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { tercero, empresa, contrato, numero };

            return Convert.ToInt32(Cacceso.ExecProc("spEliminaDetalleAusentismo", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }


        public DataView SelecccionaContratosTercero(int tercero, int empresa)
        {
            string[] iParametros = new string[] { "@tercero", "@empresa" };
            object[] objValores = new object[] { tercero, empresa };
            return Cacceso.DataSetParametros("spSeleccionaContratoTercero", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView seleccionaDetalleAusentismo(string tercero, int contrato, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tercero", "@empresa", "@contrato", "@numero" };
            object[] objValores = new object[] { tercero, empresa, contrato, numero };
            return Cacceso.DataSetParametros("spSeleccionaDetalleAusentismo", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView CalulaIncapacidadEmpleadoDetalleTemporal(int tercero, int empresa, DateTime fi, DateTime ff, decimal noDias, decimal diasPagos, string tipo, int contrato)
        {
            string[] iParametros = new string[] { "@tercero", "@empresa", "@fi", "@ff", "@noDias", "@diaPagos", "@tipoIncapacidad", "@contrato" };
            object[] objValores = new object[] { tercero, empresa, fi, ff, noDias, diasPagos, tipo, contrato };
            return Cacceso.DataSetParametros("spCalulaIncapacidadEmpleadoDetalleTemporal", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }


        public DataView RetornaDatos(string tercero, int contrato, int empresa, int numero)
        {
            string[] iParametros = new string[] { "@tercero", "@contrato", "@empresa", "@numero" };
            object[] objValores = new object[] { tercero, contrato, empresa, numero };

            return Cacceso.DataSetParametros("SpGetnIncapacidadkey", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet("nIncapacidad", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (nombreEmpleado like '%" + texto + "%' or identificacion like '%" + texto + "%' )";
            dvEntidad.Sort = "tercero, numero";
            return dvEntidad;
        }

        public string Consecutivo(int empresa, string idtercero)
        {
            string[] iParametros = new string[] { "@empresa", "@tercero" };
            string[] oParametros = new string[] { "@consecutivo" };
            object[] objValores = new object[] { empresa, idtercero };

            return Convert.ToString(Cacceso.ExecProc("spConsecutivoIncapacidadTercero", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public int validaRegistroIncapacidadFecha(int empresa, string idtercero, DateTime fi, DateTime ff)
        {
            string[] iParametros = new string[] { "@empresa", "@tercero", "@fi", "@ff" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, idtercero, fi, ff };

            return Convert.ToInt16(Cacceso.ExecProc("spValidaAusentismoTerceroFecha", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public DataView ProrrogaIncapacidadTercero(DateTime fecha, int empresa, string tercero)
        {
            string[] iParametros = new string[] { "@fecha", "@empresa", "@tercero" };
            object[] objValores = new object[] { fecha, empresa, tercero };

            return Cacceso.DataSetParametros("spSeleccionaIncapacidadesAño", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public object[] CalculaIncapacidad(int empresa, string tercero, decimal noDias, string tipoIncapacidad, DateTime fecha, int diaPago, int diaInicio)
        {
            string[] iParametros = new string[] { "@empresa", "@tercero", "@noDias", "@tipoIncapacidad", "@fecha", "@diaPagos", "@diainicio" };
            string[] oParametros = new string[] { "@valor", "@valorPago" };
            object[] objValores = new object[] { empresa, tercero, noDias, tipoIncapacidad, fecha, diaPago, diaInicio };
            object[] objRetono = new object[2];

            objRetono[0] = Convert.ToDecimal(Cacceso.ExecProc("spCalulaIncapacidadEmpleado", iParametros, oParametros, objValores, "ppa").GetValue(0));
            objRetono[1] = Convert.ToDecimal(Cacceso.ExecProc("spCalulaIncapacidadEmpleado", iParametros, oParametros, objValores, "ppa").GetValue(1));

            return objRetono;
        }
    }
}