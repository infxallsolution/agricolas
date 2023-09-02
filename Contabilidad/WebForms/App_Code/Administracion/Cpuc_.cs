using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.Administracion
{
    public class Cpuc_
    {

        public Cpuc_()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = CentidadMetodos.EntidadGet(
                "cPuc",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "empresa =" + empresa + " and (codigo like '%" + texto + "%' or nombre like '%" + texto + "%')";

            return dvEntidad;
        }

        public DataView BuscarEntidad(string texto, string tipo)
        {
            DataView dvEntidad = CentidadMetodos.EntidadGet(
                "Cpuc",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "( codigo like '%" + texto + "%' or nombre like '%" + texto + "%' ) and tipo = '" + tipo + "' and activo = 1";

            return dvEntidad;
        }

        private DataSet GetPuc()
        {
            return CentidadMetodos.EntidadGet(
                "cPuc",
                "ppa");
        }

        public DataView GetPucDestino(string tipo, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@empresa" };
            object[] objValores = new object[] { tipo, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaPucTipo",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        private DataView GetPuc(string codigo, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "cPuc",
                "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa =" + empresa + " and codigo = '" + codigo + "'";

            return dvEntidad;
        }

        private DataView GetPucRaiz(string cuenta)
        {
            DataView dvPuc = new DataView();

            dvPuc = GetPuc().Tables[0].DefaultView;
            dvPuc.RowFilter = "raiz = '" + cuenta + "'";

            return dvPuc;
        }

        public int RegistraPuc(string codigo, string nombre, string tipo, bool tercero, bool centroCosto, bool iBase, bool activo, string claseCuenta, string operacion, int empresa)
        {
            string raiz = "", naturaleza = "", tipoCuenta = "";
            int nivel = 0, sBase = 0, sCcosto = 0, sTercero = 0, sActivo = 0;

            if (codigo.Length == 0 | nombre.Length == 0)
                return 5;
            else
            {
                if (codigo.Length == 1)
                {
                    raiz = "";
                    nivel = 1;
                    tipo = "M";
                }
                else
                {
                    if (codigo.Length == 2)
                    {
                        foreach (DataRowView registro in GetPuc(codigo.Substring(0, 1), empresa))
                        {
                            raiz = Convert.ToString(registro.Row.ItemArray.GetValue(1));
                            nivel = Convert.ToInt16(registro.Row.ItemArray.GetValue(5)) + 1;
                            tipoCuenta = Convert.ToString(registro.Row.ItemArray.GetValue(6));
                        }

                        if (raiz.Length == 0)
                            return 2;

                        if (tipoCuenta == "A")
                            return 3;

                        if (GetPucRaiz(codigo).Count > 0)
                            return 4;

                        if (GetPucRaiz(codigo).Count > 0 & tipo == "A")
                            return 4;
                    }
                    else
                    {
                        foreach (DataRowView registro in GetPuc(codigo.Substring(0, codigo.Length - 2), empresa))
                        {
                            raiz = Convert.ToString(registro.Row.ItemArray.GetValue(1));
                            nivel = Convert.ToInt16(registro.Row.ItemArray.GetValue(5)) + 1;
                            tipoCuenta = Convert.ToString(registro.Row.ItemArray.GetValue(6));
                        }

                        if (raiz.Length == 0)
                            return 2;

                        if (tipoCuenta == "A")
                            return 3;

                        if (GetPucRaiz(codigo).Count > 0 & tipo == "A")
                            return 4;
                    }
                }

                if (codigo.Substring(0, 1) == "1" | codigo.Substring(0, 1) == "5" | codigo.Substring(0, 1) == "6" | codigo.Substring(0, 1) == "7" | codigo.Substring(0, 1) == "8")
                    naturaleza = "D";
                else
                    naturaleza = "C";

                if (activo == true)
                    sActivo = 1;

                if (iBase == true)
                    sBase = 1;

                if (centroCosto == true)
                    sCcosto = 1;

                if (tercero == true)
                    sTercero = 1;

                object[] objValores = new object[] { sActivo, sBase, sCcosto, claseCuenta, codigo.Trim(), empresa, naturaleza, nivel, nombre.ToUpper(), raiz, sTercero, tipo };

                return CentidadMetodos.EntidadInsertUpdateDelete("cPuc", operacion, "ppa", objValores);
            }
        }

        public int VerificaCuentaEnMovimientos(string cuenta)
        {
            string[] iParametros = new string[] { "cuenta" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { cuenta };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spVerificaCuentaEnMovimientos",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }
    }
}