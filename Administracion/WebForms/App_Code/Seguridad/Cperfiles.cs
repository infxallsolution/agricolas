using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Administracion.WebForms.App_Code.Seguridad
{
    public class Cperfiles
    {
        public Cperfiles()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public DataView BuscarEntidad(string texto)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "sPerfiles",
                "ppa").Tables[0].DefaultView;

            dvEntidad.RowFilter = "codigo like '%" + texto + "%' or descripcion like '%" + texto + "%'";

            return dvEntidad;
        }

        public DataView GetPermisosPerfilCab(string perfil)
        {
            string[] iParametros = new string[] { "@perfil" };
            object[] objValores = new object[] { perfil };

            return Cacceso.DataSetParametros(
                "spSeleccionaPerfilPermisosCab",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetPermisosPerfil(string perfil)
        {
            DataView dvPerfil = CentidadMetodos.EntidadGet(
                "perfilPermisos",
                "ppa").Tables[0].DefaultView;

            dvPerfil.RowFilter = "perfil = '" + perfil + "'";

            return dvPerfil;
        }

        public int InsertaPerfilPermisos(bool activo, string perfil, string sitio, string menu, string operacion, bool todasOperacion)
        {
            string[] iParametros = new string[] { "@activo", "@perfil", "@sitio", "@menu", "@operacion", "@todasOperaciones" };
            string[] oParametros = new string[] { "@Retorno" };
            object[] objValores = new object[] { activo, perfil, sitio, menu, operacion, todasOperacion };

            return Convert.ToInt16(Cacceso.ExecProc(
                "SpInsertaperfilPermisos",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int EliminaPermisosPerfilSitioMenu(string perfil, string sitio, string menu)
        {
            string[] iParametros = new string[] { "@perfil", "@sitio", "@menu" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { perfil, sitio, menu };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spEliminaPerfilPermisosSitioMenu",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView GetDepartamentosPerfil(string perfil)
        {
            string[] iParametros = new string[] { "@perfil" };
            object[] objValores = new object[] { perfil };

            return Cacceso.DataSetParametros(
                "spSeleccionaPerfilDepartamentos",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView GetUsuariosPerfil(string perfil, int empresa)
        {
            string[] iParametros = new string[] { "@perfil", "@empresa" };
            object[] objValores = new object[] { perfil, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaUsuariosPerfil",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }
    }
}