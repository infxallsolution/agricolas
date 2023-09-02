using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.CuentasxCobrar
{
    public class CgrupoCliente
    {
        public CgrupoCliente()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet("cxcGrupoCliente", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";

            return dvEntidad;
        }

        public DataView SeleccionaProveedoresSinGrupo(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };
            return Cacceso.DataSetParametros("spSeleccionaClienteSinGrupo", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public int VerificaProveedorGrupo(string grupo, int proveedor, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@cliente", "@grupocliente" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, proveedor, grupo };
            return Convert.ToInt16(Cacceso.ExecProc("spVerificaGrupoCliente", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public int EliminaProveedor(string cuadrilla, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@grupo" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, cuadrilla };

            return Convert.ToInt16(Cacceso.ExecProc("spEliminaClienteGrupo", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }


    }
}