using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.Presupuesto
{
    public class cParametrosPresupuesto
    {
        public cParametrosPresupuesto()
        {
            //
            // TODO: Add constructor logic here
            //
        }



        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = CentidadMetodos.EntidadGet(
                "cParametrosPresupuestoDetalle",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "empresa =" + empresa + " and (idcuentasiesa like '%" + texto + "%' or cuentasiesa like '%" + texto + "%' or cuentasiesa like '%" + texto + "%' or idcuentapresupuesto like '%" + texto + "%' or ccostosiesa like '%" + texto + "%' or idccostosiesa like '%" + texto + "%' or idccostopresupuesto like '%" + texto + "%' or ccostopresupuesto like '%" + texto + "%')";
            return dvEntidad;
        }
    }
}