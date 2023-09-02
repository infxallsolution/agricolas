using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class CbancoPlano
    {
        public CbancoPlano()
        {
        }
        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();
            dvEntidad = CentidadMetodos.EntidadGet("nPlanoBanco", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (banco like '%" + texto + "%' or nombreBanco like '%" + texto + "%')";
            return dvEntidad;
        }


        public DataView SeleccionaCamposPlanoBanco()
        {
            string[] iParametros = new string[] { };
            object[] objValores = new object[] { };
            return Cacceso.DataSetParametros("spSeleccionaCamposVistaPlanoBancos", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView SeleccionaPlanoDetalleBanco(int empresa, string banco)
        {
            string[] iParametros = new string[] { "@empresa", "@banco" };
            object[] objValores = new object[] { empresa, banco };
            return Cacceso.DataSetParametros("spSeleccionaDetallePlanoBanco", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

    }
}