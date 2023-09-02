using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.Administracion
{
    public class Cproveedor
    {

        public Cproveedor()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = CentidadMetodos.EntidadGet(
                "cxpProveedor",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "empresa=" + empresa + " and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";
            dvEntidad.Sort = "descripcion";

            return dvEntidad;
        }

        public int VerificaClaseIR(string proveedor, int tercero, int clase, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@tercero", "@proveedor", "@clase" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, tercero, proveedor, clase };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spVerificaProveedorClaseIR",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int EliminaClaseProveedor(string proveedor, int tercero, int empresa)
        {
            string[] iParametros = new string[] { "@empresa", "@tercero", "@proveedor" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, tercero, proveedor };

            return Convert.ToInt16(Cacceso.ExecProc(
                "SpDeleteProveedorCalseIR",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView ConceptosClase(int clase, int empresa)
        {
            string[] iParametros = new string[] { "@clase", "@empresa" };
            object[] objValores = new object[] { clase, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaConceptosClase",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView TerceroClase(int clase, string proveedor, int tercero, int empresa)
        {
            string[] iParametros = new string[] { "@clase", "@proveedor", "@tercero", "@empresa" };
            object[] objValores = new object[] { clase, proveedor, tercero, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaclaseProveedor",
                iParametros,
                objValores,
                        "ppa").Tables[0].DefaultView;
        }



    }
}