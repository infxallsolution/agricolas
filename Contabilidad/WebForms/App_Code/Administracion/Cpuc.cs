using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.Administracion
{
    public class Cpuc
    {


        public string id { get; set; }
        public string codigo { get; set; }
        public string Nombre { get; set; }
        public Cpuc()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = CentidadMetodos.EntidadGet("cPuc", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa =" + empresa + " and (codigo like '" + texto + "%' or descripcion like '%" + texto + "%')";
            return dvEntidad;
        }

        public DataView GetPuc(string texto, int empresa)
        {
            DataView dvEntidad = CentidadMetodos.EntidadGet("cPuc", "ppa").Tables[0].DefaultView;
            try
            {
                dvEntidad.RowFilter = "empresa =" + empresa + " and (codigo like '" + texto + "%' or descripcion like '%" + texto + "%') and auxiliar=1";
                return dvEntidad;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataView GetPucCodigo(string texto, int empresa)
        {
            DataView dvEntidad = CentidadMetodos.EntidadGet("cPuc", "ppa").Tables[0].DefaultView;
            try
            {
                dvEntidad.RowFilter = "empresa =" + empresa + " and codigo ='" + texto + "'";
                return dvEntidad;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataView DatosCuenta(string cuenta, int empresa)
        {
            string[] iParametros = new string[] { "@codigo", "@empresa" };
            object[] objValores = new object[] { cuenta, empresa };
            return Cacceso.DataSetParametros("SpGetcPuckey", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        private DataSet GetPuc()
        {
            return CentidadMetodos.EntidadGet("cPuc", "ppa");
        }

        public DataView GetPucDestino(string tipo, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@empresa" };
            object[] objValores = new object[] { tipo, empresa };
            return Cacceso.DataSetParametros("spSeleccionaPucTipo", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }


        private DataView GetPucRaiz(string cuenta)
        {
            DataView dvPuc = new DataView();

            dvPuc = GetPuc().Tables[0].DefaultView;
            dvPuc.RowFilter = "raiz = '" + cuenta + "'";

            return dvPuc;
        }

        public int RegistraPuc(bool activo, bool auxiliar, decimal baseIR, string claseIR, string codigo, string nombre, bool disponible, int empresa, string grupoCC, bool manejaIR, bool mCcosto,
            bool mTercero, string naturaleza, decimal nivel, string notas, string plan, string raiz, string saldoTercero, decimal tasaIR, string tipoCuenta, string tipoDisponible, string tipoIR, string tipoManejoTercero, string operacion)
        {
            bool validarAuxiliar = false;
            if (codigo.Length == 0 | nombre.Length == 0)
                return 5;
            else
            {
                if (codigo.Length == 1)
                {
                    raiz = "";
                    nivel = 1;
                    auxiliar = false;
                }
                else
                {
                    if (codigo.Length == 2)
                    {
                        foreach (DataRowView registro in GetPucCodigo(codigo.Substring(0, 1), empresa))
                        {
                            raiz = Convert.ToString(registro.Row.ItemArray.GetValue(1));
                            nivel = Convert.ToInt16(registro.Row.ItemArray.GetValue(6)) + 1;
                            tipoCuenta = Convert.ToString(registro.Row.ItemArray.GetValue(7));
                            validarAuxiliar = Convert.ToBoolean(registro.Row.ItemArray.GetValue(22));
                        }
                        if (raiz.Length == 0)
                            return 2;

                        if (GetPucRaiz(codigo).Count > 0 & validarAuxiliar == true)
                            return 4;
                    }
                    else
                    {
                        foreach (DataRowView registro in GetPucCodigo(codigo.Substring(0, codigo.Length - 2), empresa))
                        {
                            raiz = Convert.ToString(registro.Row.ItemArray.GetValue(1));
                            nivel = Convert.ToInt16(registro.Row.ItemArray.GetValue(6)) + 1;
                            tipoCuenta = Convert.ToString(registro.Row.ItemArray.GetValue(7));
                            validarAuxiliar = Convert.ToBoolean(registro.Row.ItemArray.GetValue(22));
                        }

                        if (raiz.Length == 0)
                            return 2;

                        if (GetPucRaiz(codigo).Count > 0 & validarAuxiliar == true)
                            return 4;
                    }
                }

                if (codigo.Substring(0, 1) == "1" | codigo.Substring(0, 1) == "5" | codigo.Substring(0, 1) == "6" | codigo.Substring(0, 1) == "7" | codigo.Substring(0, 1) == "8")
                    naturaleza = "D";
                else
                    naturaleza = "C";

                object[] objValores = new object[] {  activo,  auxiliar,  baseIR, claseIR , codigo,  nombre,  disponible,  empresa,  grupoCC,  manejaIR,  mCcosto,
         mTercero,  naturaleza,  nivel, notas,  plan, raiz, saldoTercero, tasaIR,  tipoCuenta,  tipoDisponible,  tipoIR,  tipoManejoTercero };

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

        public int verificaAuxiliar(string cuenta, int empresa)
        {
            string[] iParametros = new string[] { "@cuenta", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { cuenta, empresa };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spverificaAuxiliar",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }




        public string spSeleccionaConfigCuenta(int empresa, string cuenta)
        {

            string[] iParametros = new string[] { "@empresa", "@cuenta" };
            object[] objValores = new object[] { empresa, cuenta };

            string retorno = "";

            foreach (DataRowView registro in Cacceso.DataSetParametros(
                "spSeleccionaConfigCuenta",
                 iParametros,
                 objValores,
                "ppa"
                ).Tables[0].DefaultView)
            {
                for (int i = 2; i < registro.Row.ItemArray.Length; i++)
                {
                    retorno = retorno + registro.Row.ItemArray.GetValue(i).ToString() + "*";
                }
            }

            return retorno;

        }
    }
}