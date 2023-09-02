using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Administracion
{
    public class CListaPreciosLote
    {
        public CListaPreciosLote()
        {

        }


        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet("aNovedadPrecio", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (novedad like '%" + texto + "%' or nombreFinca like '%" + texto + "%' or lote like '%" + texto + "%' or nombreNovedad like '%" + texto + "%')";

            return dvEntidad;
        }

        public decimal SeleccionaPrecioNovedadAño(int empresa, string novedad, int año)
        {
            string[] iParametros = new string[] { "@empresa", "@novedad", "@año" };
            string[] oParametros = new string[] { "@precio" };
            object[] objValores = new object[] { empresa, novedad, año };
            return Convert.ToDecimal(Cacceso.ExecProc("spSeleccionaPrecioNovedadAño", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public decimal SeleccionaPrecioNovedadAñoTercero(int empresa, string novedad, int año, int tercero, DateTime fechaNovedad)
        {
            string[] iParametros = new string[] { "@empresa", "@novedad", "@año", "@tercero", "@fechaNovedad" };
            string[] oParametros = new string[] { "@precio" };
            object[] objValores = new object[] { empresa, novedad, año, tercero, fechaNovedad };
            return Convert.ToDecimal(Cacceso.ExecProc("spSeleccionaPrecioNovedadAñoTercero", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public DataView SeleccionaListaPreciosLote(string finca, string novedad, string seccion, int empresa)
        {
            DataView dvEntidad = new DataView();

            string[] iParametros = new string[] { "@finca", "@novedad", "@seccion", "@empresa" };
            object[] objValores = new object[] { finca, novedad, seccion, empresa };

            return Cacceso.DataSetParametros("spSeleccionaListaPreciosLote", iParametros, objValores, "ppa").Tables[0].DefaultView;

        }

        public object InsertaListaPrecios(int empresa, DateTime fechaRegistro, string finca,
          string lote, bool modificado, string novedad, decimal precioContratistas, decimal precioTercero, int registro, string sesion, string usuario)
        {
            DataView dvEntidad = new DataView();

            string[] iParametros = new string[] { "@empresa", "@fechaRegistro", "@finca", "@lote", "@modificado", "@novedad", "@precioContratistas", "@precioTerceros", "@registro", "@sesion", "@usuario" };
            string[] OParametros = new string[] { "@Retorno" };

            object[] objValores = new object[] {  empresa, fechaRegistro, finca,
         lote, modificado , novedad, precioContratistas, precioTercero, registro, sesion, usuario };

            return Cacceso.ExecProc("SpInsertaListaPrecio", iParametros, OParametros, objValores, "ppa").GetValue(0);

        }
    }
}