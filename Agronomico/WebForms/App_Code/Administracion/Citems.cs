using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Administracion
{
    public class Citems
    {
        public Citems()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }




        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "iItems",
                "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and  descripcion like '%" + texto + "%'";

            return dvEntidad;
        }

        public string Consecutivo(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            string[] oParametros = new string[] { "@consecutivo" };
            object[] objValores = new object[] { empresa };

            return Convert.ToString(Cacceso.ExecProc(
                "spRetornaConsecutivoItems",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public string RetornaPapeleta(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            string[] oParametros = new string[] { "@papeleta" };
            object[] objValores = new object[] { empresa };

            return Convert.ToString(Cacceso.ExecProc(
                "spRetornaPapepeletaCatalogo",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView ConsultaMayorPlan(string plan, int empresa)
        {
            string[] iParametros = new string[] { "@plan", "@empresa" };
            object[] objValores = new object[] { plan, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaMayoresPlanItems",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView ConsultaCriteriosItems(int item, int empresa)
        {
            string[] iParametros = new string[] { "@item", "@empresa" };
            object[] objValores = new object[] { item, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaCriteriosItem",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

    }
}