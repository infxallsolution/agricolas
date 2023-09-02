using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Administracion.WebForms.App_Code.Seguridad
{
    public class Cmodulos
    {
        public Cmodulos()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public DataView BuscarEntidad(string texto)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "sModulos",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "codigo like '%" + texto + "%' or descripcion like '%" + texto + "%'";

            return dvEntidad;
        }


        public DataView GetPermisosPerfilCab(string perfil)
        {
            string[] iParametros = new string[] { "@perfil" };
            object[] objValores = new object[] { perfil };

            return Cacceso.DataSetParametros(
                "SpInsertagFoto",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

    }
}