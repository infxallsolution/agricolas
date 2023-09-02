using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.Administracion
{
    public class Cterceros
    {

        public string id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public Cterceros()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = CentidadMetodos.EntidadGet(
                "cTercero",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "empresa=" + empresa + " and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";
            dvEntidad.Sort = "descripcion";

            return dvEntidad;
        }


        public DataView SeleccionaClienteFiltro(int empresa, string filtro)
        {
            string[] iParametros = new string[] { "@empresa", "@filtro" };
            object[] objValores = new object[] { empresa, filtro };

            return Cacceso.DataSetParametros("spSeleccionaClientesFiltro", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView SeleccionaProveedoresFiltro(int empresa, string filtro)
        {
            string[] iParametros = new string[] { "@empresa", "@filtro" };
            object[] objValores = new object[] { empresa, filtro };

            return Cacceso.DataSetParametros("spSeleccionaProveedoresFiltro", iParametros, objValores, "ppa").Tables[0].DefaultView;
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

            return Convert.ToInt32(resultado.GetValue(0));

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

        public int RetornaDocumentoTercero(string codigo, int empresa)
        {

            string[] iParametros = new string[] { "@codigo", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { codigo, empresa };

            object[] resultado = Cacceso.ExecProc(
                "spRetornaDocumentoTercero",
                iParametros,
                oParametros,
                objValores,
                "ppa");

            return Convert.ToInt16(resultado.GetValue(0));

        }


        public DataView SeleccionaTercerosProveedor(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaTercerosProveedor",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }
        public DataView SeleccionaTercerosClientes(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaTercerosClientes",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public DataView SeleccionaTerceroActivos(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaTerceroActivos",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView SeleccionaSucursalProveedor(int empresa, string tercero)
        {
            string[] iParametros = new string[] { "@empresa", "@tercero" };
            object[] objValores = new object[] { empresa, tercero };

            return Cacceso.DataSetParametros(
                "spSeleccionaSucursalProveedor",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView SeleccionaSucursalCliente(int empresa, string tercero)
        {
            string[] iParametros = new string[] { "@empresa", "@tercero" };
            object[] objValores = new object[] { empresa, tercero };

            return Cacceso.DataSetParametros(
                "spSeleccionaSucursalCliente",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }
        public DataView RetornaDatosTerceroCodigo(string codigo, int empresa)
        {
            string[] iParametros = new string[] { "@codigo", "@empresa" };
            object[] objValores = new object[] { codigo, empresa };

            return Cacceso.DataSetParametros(
                "spRetornaDatosTerceroCodigo",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
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

        public int verificaCodigoTercero(string codigo, int empresa)
        {
            string[] iParametros = new string[] { "@codigo", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { codigo, empresa };

            object[] resultado = Cacceso.ExecProc(
                "spVerificaCodigoTercero",
                iParametros,
                oParametros,
                objValores,
                "ppa");

            return Convert.ToInt16(resultado.GetValue(0));
        }



    }
}