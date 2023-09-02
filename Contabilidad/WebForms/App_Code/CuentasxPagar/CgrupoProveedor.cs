using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.CuentasxPagar
{
    public class CgrupoProveedor
    {
        public CgrupoProveedor()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet("cxpGrupoProveedor", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";

            return dvEntidad;
        }

        public DataView SeleccionaProveedoresSinGrupo(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };
            return Cacceso.DataSetParametros("spSeleccionaProveedorSinGrupo", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public int VerificaProveedorGrupo(string grupo, string proveedor, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@proveedor", "@grupoProveedor" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, proveedor, grupo };
            return Convert.ToInt16(Cacceso.ExecProc("spVerificaGrupoProveedor", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public int EliminaProveedor(string cuadrilla, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@grupo" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, cuadrilla };

            return Convert.ToInt16(Cacceso.ExecProc("spEliminaProveedorGrupo", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }


    }
}