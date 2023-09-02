using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Almacen.WebForms.App_Code.Parametros
{
    public class Cterceros
    {
        public Cterceros()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        private string codigo;
        private string descripcion;

        public string Codigo
        {
            get
            {
                return codigo;
            }

            set
            {
                codigo = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return descripcion;
            }

            set
            {
                descripcion = value;
            }
        }

        public DataView getProveedores()
        {
            DataView dvProveedor = CentidadMetodos.EntidadGet("cTercero", "ppa").Tables[0].DefaultView;
            return dvProveedor;
        }

        public DataView SeleccionaProveedoresFiltro(int empresa, string filtro)
        {
            string[] iParametros = new string[] { "@empresa", "@filtro" };
            object[] objValores = new object[] { empresa, filtro };

            return Cacceso.DataSetParametros(
                "spSeleccionaProveedoresFiltro",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView SeleccionaSucursalProveedor(string proveedor, int empresa)
        {
            string[] iParametros = new string[] { "@tercero", "@empresa" };
            object[] objValores = new object[] { proveedor, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaSucursalProveedor",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }
    }
}