using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Programacion
{
    public class Cturnos
    {
        public Cturnos()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }




        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();
            dvEntidad = CentidadMetodos.EntidadGet("nTurno", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";
            return dvEntidad;
        }

        public string Consecutivo(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            string[] oParametros = new string[] { "@consecutivo" };
            object[] objValores = new object[] { empresa };
            return Convert.ToString(Cacceso.ExecProc("spConsecutivoTurno", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public DataView GetDepartamentosTurno(string turno, int empresa)
        {
            string[] iParametros = new string[] { "@turno", "@empresa" };
            object[] objValores = new object[] { turno, empresa };
            return Cacceso.DataSetParametros("spSeleccionaTurnoDepartamentos", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView GetTurnosUsuario(string usuario, int empresa)
        {
            string[] iParametros = new string[] { "@usuario", "@empresa" };
            object[] objValores = new object[] { usuario, empresa };
            return Cacceso.DataSetParametros("spSeleccionaTurnosUsuario", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }


    }
}