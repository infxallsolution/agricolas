using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class cProrroga
    {
        public cProrroga()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }



        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();
            dvEntidad = CentidadMetodos.EntidadGet("nProrroga", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and descripcion like '%" + texto + "%'";
            return dvEntidad;
        }

        public DataView RetornaDatosProRet(int empresa, string idtercero, int noContrato, string tipo)
        {
            string[] iParametros = new string[] { "@empresa", "@tercero", "@noContrato", "@tipo" };
            object[] objValores = new object[] { empresa, idtercero, noContrato, tipo };

            return Cacceso.DataSetParametros(
                "spRetornaDatosProRet",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public string Consecutivo(int empresa, string idtercero, string contrato, string tipo)
        {
            string[] iParametros = new string[] { "@empresa", "@tercero", "@contrato", "@tipo" };
            string[] oParametros = new string[] { "@consecutivo" };
            object[] objValores = new object[] { empresa, idtercero, contrato, tipo };

            return Convert.ToString(Cacceso.ExecProc(
                "spConsecutivoProrrogaContratoTercero",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }


        public string verificaUltimaFechaProrroga(int empresa, string idtercero, string contrato, string tipo)
        {
            string[] iParametros = new string[] { "@empresa", "@tercero", "@contrato", "@tipo" };
            string[] oParametros = new string[] { "@fecha" };
            object[] objValores = new object[] { empresa, idtercero, contrato, tipo };

            return Convert.ToString(Cacceso.ExecProc(
                "spVerificaUltimaFechaProrroga",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }
    }
}