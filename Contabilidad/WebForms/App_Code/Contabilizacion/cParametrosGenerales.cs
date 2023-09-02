using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.Contabilizacion
{
    public class cParametrosGenerales
    {
        public cParametrosGenerales()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet("cParametroContaNomi", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and  ( desConcepto like '%" + texto + "%'" + " or   desCcosto like '%" + texto + "%'" + " or   desCcostoMayor like '%" + texto + "%'" + " or   desClase like '%" + texto + "%')";

            return dvEntidad;
        }

        public int VerificaClaseConceptosContabilizacion(string clase, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@clase" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, clase };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spVerificaClaseConceptosContabilizacion",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int VerificaClaseSeguridadSocial(string clase, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@clase" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, clase };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spVerificaClaseSeguridadSocial",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView RetornaConceptosLaboresxCentroCosto(int empresa, string centroCosto)
        {
            string[] iParametros = new string[] { "@empresa", "@centroCosto" };
            object[] objValores = new object[] { empresa, centroCosto };

            return Cacceso.DataSetParametros(
                "spRetornaConceptosLaboresxCentroCosto",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public DataView RetornaAuxiliaresPuc(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa, };

            return Cacceso.DataSetParametros(
                "spRetornaAuxiliaresPuc",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public DataView RetornaDatosParametroGenerales(int empresa, int codigo)
        {
            string[] iParametros = new string[] { "@empresa", "@codigo" };
            object[] objValores = new object[] { empresa, codigo };

            return Cacceso.DataSetParametros(
                "spRetornaDatosParametroGenerales",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }






    }
}