using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.Administracion
{
    public class CConfigClaseIR
    {
        public CConfigClaseIR()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = CentidadMetodos.EntidadGet("cConfigClaseIR", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa =" + empresa.ToString() + " and (descripcion like '%" + texto + "%')";
            return dvEntidad;
        }

        public DataView ValoresClasesConfig(int clase, int empresa)
        {
            string[] iParametros = new string[] { "@clase", "@empresa" };
            object[] objValores = new object[] { clase, empresa };
            return Cacceso.DataSetParametros("spValoresClasesConfig", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }


        public int ManejaLlaveClase(int clase, int empresa, string valor)
        {
            string[] iParametros = new string[] { "@clase", "@empresa", "@valor" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { clase, empresa, valor };
            return Convert.ToInt16(Cacceso.ExecProc("spManejaLlaveClase", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public DataView ConceptosClase(int clase, int empresa)
        {
            string[] iParametros = new string[] { "@clase", "@empresa" };
            object[] objValores = new object[] { clase, empresa };
            return Cacceso.DataSetParametros("spSeleccionaConceptosClase", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }



    }
}