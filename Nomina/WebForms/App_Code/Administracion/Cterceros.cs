using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class Cterceros
    {

        public Cterceros()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int RetornaTipoDocumentoNit(string codigo, int empresa)
        {
            string[] iParametros = new string[] { "@codigo", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { codigo, empresa };

            object[] resultado = Cacceso.ExecProc(
                "spRetornaTipoDocumentoNit",
                iParametros,
                oParametros,
                objValores,
                "ppa");

            return Convert.ToInt16(resultado.GetValue(0));
        }
        public DataView RetornaDatosTercero(string tercero, int empresa)
        {
            string[] iParametros = new string[] { "@tercero", "@empresa" };
            object[] objValores = new object[] { tercero, empresa };

            return Cacceso.DataSetParametros(
                "spRetornaDatosTercero",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = CentidadMetodos.EntidadGet(
                "cTercero",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "empresa=" + empresa + " and empleado=1 and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";
            dvEntidad.Sort = "descripcion";

            return dvEntidad;
        }

        public DataRow BuscarFoto(int foto)
        {
            object[] objKey = new object[] { foto };
            DataView dvEntidad = CentidadMetodos.EntidadGetKey(
                "gFoto",
                "ppa", objKey).Tables[0].DefaultView;

            return dvEntidad.Table.Rows.Count > 0 ? dvEntidad.Table.Rows[0] : null;
        }

        public int VerificaTercero(string tercero, out string nombre)
        {
            object[] objKey = new object[] { tercero };

            DataView dvTerceros = CentidadMetodos.EntidadGetKey(
                "cTerceros",
                "ppa",
                objKey).Tables[0].DefaultView;

            dvTerceros.RowFilter = "estado = 'S'";

            foreach (DataRowView registro in dvTerceros)
            {
                nombre = Convert.ToString(registro.Row.ItemArray.GetValue(1));
                return 1;
            }

            nombre = "!! Tercero Inexistente !!";
            return 0;
        }

        public int RetornaConsecutivoIdtercero(int empresa)
        {

            string[] iParametros = new string[] { "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa };

            object[] resultado = Cacceso.ExecProc(
                "spRetornaConsecutivoIdTercero",
                iParametros,
                oParametros,
                objValores,
                "ppa");

            return Convert.ToInt16(resultado.GetValue(0));

        }


        public int RetornaCodigoTercero(string codigo, int empresa)
        {

            string[] iParametros = new string[] { "@codigo", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { codigo, empresa };

            object[] resultado = Cacceso.ExecProc(
                "spRetornaCodigoTercero",
                iParametros,
                oParametros,
                objValores,
                "ppa");

            return Convert.ToInt16(resultado.GetValue(0));

        }

    }
}