using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Administracion
{
    public class Ccuadrillas
    {
        public Ccuadrillas()
        {
            //
            // TODO: Add constructor logic here
            //
        }




        public int VerificaFuncionarioCuadrilla(string cuadrilla, int funcionario, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@funcionario", "@cuadrilla" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, funcionario, cuadrilla };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spVerificaFuncionarioCuadrilla",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public int EliminaFunncionarios(string cuadrilla, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@cuadrilla" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, cuadrilla };

            return Convert.ToInt32(Cacceso.ExecProc(
                "spEliminaFuncionariosCuadrilla",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "nCuadrilla",
                "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";

            return dvEntidad;
        }

        public DataView GetCuadrillasUsuario(string usuario, int empresa)
        {
            string[] iParametros = new string[] { "@usuario", "@empresa" };
            object[] objValores = new object[] { usuario, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaCuadrillasUsuario",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetFuncionariosCuadrilla(string cuadrilla, int empresa)
        {
            string[] iParametros = new string[] { "@cuadrilla", "@empresa" };
            object[] objValores = new object[] { cuadrilla, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaFuncionariosCuadrilla",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetFuncionariosDepartamento(string cuadrilla, int empresa)
        {
            string[] iParametros = new string[] { "@cuadrilla", "@empresa" };
            object[] objValores = new object[] { cuadrilla, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaFuncionariosDepartamento",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public string Consecutivo(string departamento, int empresa)
        {
            string[] iParametros = new string[] { "@departamento", "@empresa" };
            string[] oParametros = new string[] { "@consecutivo" };
            object[] objValores = new object[] { departamento, empresa };

            return Convert.ToString(Cacceso.ExecProc(
                "spConsecutivoCuadrilla",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }
    }
}