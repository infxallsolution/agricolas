using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class Cfuncionario
    {
        public Cfuncionario()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet("nFuncionarios", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";
            dvEntidad.Sort = "descripcion";
            return dvEntidad;
        }

        public int validaFuncionarioEmpresa(int empresa, string empresaPermitida, string tipo, string funcionario)
        {
            string[] iParametros = new string[] { "@empresa", "@empresaPermitida", "@tipo", "@funcionario" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, empresaPermitida, tipo, funcionario };

            return Convert.ToInt16(Cacceso.ExecProc("spValidaFuncionarioEmpresas", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }
        public int VerificaFuncionarioExistente(int empresa, string funcionario, string tipo)
        {
            string[] iParametros = new string[] { "@empresa", "@funcionario", "@tipo" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, funcionario, tipo };

            return Convert.ToInt16(Cacceso.ExecProc("spVerificaFuncionarioExistente", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }
        public System.Data.DataView SeleccionaTerceros(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };
            return Cacceso.DataSetParametros("spSeleccionaTerceros", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView ProveedoreesContratista(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };
            return Cacceso.DataSetParametros("spSeleccionaProveedoresAcceso", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView SeleccionaCentroCosto(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };
            return Cacceso.DataSetParametros("spSeleccionaCentroCosto", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView SeleccionaCargos(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };
            return Cacceso.DataSetParametros("spSeleccionaCargos", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView RetornaDatosProspecto(string codigo, string tipo, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@codigo", "@empresa" };
            object[] objValores = new object[] { tipo, codigo, empresa };

            return Cacceso.DataSetParametros("SpGetnFuncionarioskey", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView RetornaDatosTercero(string codigo, int empresa)
        {
            string[] iParametros = new string[] { "@codigo", "@empresa" };
            object[] objValores = new object[] { codigo, empresa };

            return Cacceso.DataSetParametros("spSeleccionaTercerosCodigo", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView SeleccionaFuncionarioTipo(int empresa, bool funcionario, bool contratista, bool visitante)
        {
            string[] iParametros = new string[] { "@empresa", "@funcionario", "@contratista", "@visitante" };
            object[] objValores = new object[] { empresa };
            return Cacceso.DataSetParametros("spSeleccionaFuncionarioTipo", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

    }
}