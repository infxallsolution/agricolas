using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Administracion.WebForms.App_Code.Administracion
{
    public class CtiposTransaccion
    {
        public CtiposTransaccion()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet("gTipoTransaccion", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";

            return dvEntidad;
        }

        public void PasarInformacionSiesa()
        {
            string[] iParametros = new string[] { };
            string[] oParametros = new string[] { };
            object[] objValores = new object[] { };

            Cacceso.ExecProc("SpSincronizaInformacionSiesa", iParametros, oParametros, objValores, "ppa");
        }

        public void SicronizaCasino()
        {
            string[] iParametros = new string[] { };
            string[] oParametros = new string[] { };
            object[] objValores = new object[] { };

            Cacceso.ExecProc("spSincronizaCasino", iParametros, oParametros, objValores, "ppa");
        }


        public DataView BuscarEntidadCampo(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();
            dvEntidad = CentidadMetodos.EntidadGet("gTipoTransaccionCampo", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) +
                " and (tipoTransaccion like '%" + texto + "%' or entidad like '%" + texto + "%' or campo like '%"
            + texto + "%' or nombreTransaccion like '%" + texto + "%')";
            return dvEntidad;
        }

        public DataView BuscarCampo(string texto)
        {
            DataView dvEntidad = new DataView();
            dvEntidad = CentidadMetodos.EntidadGet("sysEntidadCampo", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "entidad like '%" + texto + "%' or campo like '%" + texto + "%'";
            return dvEntidad;
        }

        public DataView BuscarEntidadConfig(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "gTipoTransaccionConfig",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and tipoTransaccion like '%" + texto + "%'";

            return dvEntidad;
        }

        public string RetornaConsecutivo(string tipoTransaccion)
        {
            string[] iParametros = new string[] { "@tipoTransaccion" };
            string[] oParametros = new string[] { "@consecutivo" };
            object[] objValores = new object[] { tipoTransaccion };

            return Convert.ToString(
                Cacceso.ExecProc(
                    "spRetornaConsecutivoTransaccion",
                    iParametros,
                    oParametros,
                    objValores,
                    "ppa").GetValue(0));
        }

        public DataView BuscarEntidadTransaccionProducto(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "gTipoTransaccionProducto",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (tipo like '%" + texto + "%' or descripcion like '%" + texto + "%')";

            return dvEntidad;
        }


        public DataView BuscarEntidadTransaccionBodega(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "iBodegaTipoTransaccion",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + "  and tipo like '%" + texto + "%'";

            return dvEntidad;
        }

        public int VerificaProductoTipo(string tipo, string producto, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@tipo", "@producto" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, tipo, producto };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spVerificaProductoTipoTransaccion",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int VerificaBodegaTipo(string tipo, string bodega, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@tipo", "@bodega" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, tipo, bodega };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spVerificaBodegaTipoTransaccion",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int EliminaProductoTipo(string tipo, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@tipo" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, tipo };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spEliminaProductoTipo",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }
    }
}