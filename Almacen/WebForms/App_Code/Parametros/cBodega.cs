using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Almacen.WebForms.App_Code.Parametros
{
    public class cBodega
    {
        public cBodega()
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


        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = CentidadMetodos.EntidadGet("iBodega", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and codigo like '%" + texto + "%'";

            return dvEntidad;
        }


        public DataView SeleccionaBodegaTipoTransaccion(string tipo, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@empresa" };
            object[] objValores = new object[] { tipo, empresa };

            return Cacceso.DataSetParametros("spSeleccionaTipoTransaccionBodega", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

    }
}