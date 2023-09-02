using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class Ccontratos
    {
        public Ccontratos()
        {
            //
            // TODO: Add constructor logic here
            //
        }




        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "nContratos",
                "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";

            return dvEntidad;
        }

        public DataView RetornaDatosContratoSS(int empresa, string idtercero, int noContrato)
        {
            string[] iParametros = new string[] { "@empresa", "@tercero", "@noContrato" };
            object[] objValores = new object[] { empresa, idtercero, noContrato };

            return Cacceso.DataSetParametros(
                "spRetornaDatosContratoSeguridadSocial",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public string Consecutivo(int empresa, string idtercero)
        {
            string[] iParametros = new string[] { "@empresa", "@tercero" };
            string[] oParametros = new string[] { "@consecutivo" };
            object[] objValores = new object[] { empresa, idtercero };

            return Convert.ToString(Cacceso.ExecProc(
                "spConsecutivoContratoTercero",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int ClaseContratoValidar(string ClaseContrato, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@claseContrato" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, ClaseContrato };
            return Convert.ToInt16(Cacceso.ExecProc("spValidarClaseContrato", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public int ValidaValorSalarioIntegral(decimal salario, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@salario" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, salario };
            return Convert.ToInt16(Cacceso.ExecProc("spValidarValorSalarioIntegral", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public DataView RetornaDatosContrato(int empresa, string idtercero, int noContrato)
        {
            string[] iParametros = new string[] { "@empresa", "@tercero", "@noContrato" };
            object[] objValores = new object[] { empresa, idtercero, noContrato };

            return Cacceso.DataSetParametros(
                "spRetornaDatosContrato",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView RetornaContratosTercero(int empresa, string idtercero)
        {
            string[] iParametros = new string[] { "@empresa", "@tercero" };
            object[] objValores = new object[] { empresa, idtercero };

            return Cacceso.DataSetParametros(
                "spRetornaContratosTercero",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView RetornaContratosTerceroTerminoFijo(int empresa, string idtercero)
        {
            string[] iParametros = new string[] { "@empresa", "@tercero" };
            object[] objValores = new object[] { empresa, idtercero };

            return Cacceso.DataSetParametros(
                "spRetornaContratosTerceroTerminoFijo",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public int validaFuncionarioActivo(int empresa, string tercero)
        {
            string[] iParametros = new string[] { "@empresa", "@tercero" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, tercero };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spValidaFuncionarioActivo",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int ValidaContratosActivosTercero(int empresa, string tercero)
        {
            string[] iParametros = new string[] { "@empresa", "@tercero" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, tercero };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spValidaContratosActivosTercero",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }



        public string RetornaDiasClaseContrato(int empresa, string clase, DateTime fechain)
        {
            string[] iParametros = new string[] { "@empresa", "@clase", "@fechain" };
            string[] oParametros = new string[] { "@dias" };
            object[] objValores = new object[] { empresa, clase, fechain };

            return Convert.ToString(Cacceso.ExecProc(
                "spRetornaDatosClaseContrato",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public DataView SeleccionaDepartamentoxCC(int empresa, string ccosto)
        {
            string[] iParametros = new string[] { "@empresa", "@ccosto" };
            object[] objValores = new object[] { empresa, ccosto };

            return Cacceso.DataSetParametros(
                "spSeleccionaDepartamentoxCC",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public int VerificaClaseContrato(int empresa, string clase)
        {

            string[] iParametros = new string[] { "@empresa", "@clase" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, clase };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spVerificaClaseContrato",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }
    }
}