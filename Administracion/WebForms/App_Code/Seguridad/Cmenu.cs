using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Administracion.WebForms.App_Code.Seguridad
{
    public class Cmenu
    {
        public Cmenu()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public DataView seleccionaIconoNombreModulo(string codMenu)
        {
            string[] iParametros = new string[] { "@codigo" };
            object[] objValores = new object[] { codMenu };

            return Cacceso.DataSetParametros("SpGetsModuloskey", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView GetMenuSitio(string modulo)
        {
            string[] iParametros = new string[] { "@modulo" };
            object[] objValores = new object[] { modulo };

            return Cacceso.DataSetParametros(
                "spSeleccionaMenuSitio",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public int ActualizaIdSysUsuario(string id, string idSys, string idSysNew)
        {
            string[] iParametros = new string[] { "@id", "@idSys", "@idSysNew" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { id, idSys, idSysNew };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spActualizaUsuarios",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public void InsertasMenus(bool activo, DateTime fecharegistro, string modulo, bool mweb, string nombre, string padre, string pagina, string usuarioRegistro, out int retorno, out string rowid)
        {


            string[] iParametros = new string[] { "@activo",
            "@fechaRegistro",
            "@modulo",
            "@mWeb",
            "@nombre",
            "@padre",
            "@pagina",
            "@usuarioRegistro",
            };

            string[] oParametros = new string[] { "@retorno", "@rowidret" };
            object[] objValores = new object[] { activo, fecharegistro, modulo, mweb, nombre, padre, pagina, usuarioRegistro };


            var ret = Cacceso.ExecProc(
                  "SpInsertasMenus",
                  iParametros,
                  oParametros,
                  objValores,
                  "ppa");

            retorno = Convert.ToInt16(ret[0]);
            rowid = Convert.ToString(ret[1]);


        }


        public int validaSitioOperacion(string sitio, string operacion)
        {
            string[] iParametros = new string[] { "@sitio", "@operacion" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { sitio, operacion };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spValidaSitiosOperacion",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }




        public string RetornaNombreUsuario(string id)
        {
            string[] iParametros = new string[] { "@id" };
            string[] oParametros = new string[] { "@nombre" };
            object[] objValores = new object[] { id };

            return Convert.ToString(
                Cacceso.ExecProc(
                    "spRetornaNombreUsuario",
                    iParametros,
                    oParametros,
                    objValores,
                    "ppa").GetValue(0));
        }

        public string RetornaNombreEmpresa(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            string[] oParametros = new string[] { "@nombreEmpresa" };
            object[] objValores = new object[] { empresa };

            return Convert.ToString(
                Cacceso.ExecProc(
                    "spRetornaNombreEmpresa",
                    iParametros,
                    oParametros,
                    objValores,
                    "ppa").GetValue(0));
        }

        public int RetornaCodigoEmpresaUsuario(string usuario)
        {
            string[] iParametros = new string[] { "@usuario" };
            string[] oParametros = new string[] { "@codigoEmpresa" };
            object[] objValores = new object[] { usuario };

            return Convert.ToInt16(
                Cacceso.ExecProc(
                    "spRetornaCodigoEmpresaUsuario",
                    iParametros,
                    oParametros,
                    objValores,
                    "ppa").GetValue(0));
        }


        public DataView SeleccionaEmpresaUsuario(string usuario)
        {
            string[] iParametros = new string[] { "@usuario" };
            object[] objValores = new object[] { usuario };

            return Cacceso.DataSetParametros(
                "spSeleccionEmpresaUsuarioPermisos",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public DataView SeleccionaMenu(string usuario, string clave)
        {
            string[] iParametros = new string[] { "@usuario", "@clave" };
            object[] objValores = new object[] { usuario, clave };

            return Cacceso.DataSetParametros(
                "SpseleccioaSitiosInfos",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }


        public string SeleccionaFoto(string usuario)
        {
            string[] iParametros = new string[] { "@usuario" };
            string[] oParametros = new string[] { "@foto" };
            object[] objValores = new object[] { usuario };

            return Convert.ToString(
                Cacceso.ExecProc(
                    "spretornafoto",
                    iParametros,
                    oParametros,
                    objValores,
                    "ppa").GetValue(0));
        }

        public string SeleccionaMenuPrincipal(string usuario, string clave, int empresa)
        {
            string[] iParametros = new string[] { "@usuario", "@clave", "@empresa" };
            string[] oParametros = new string[] { "@menuPrincipal" };
            object[] objValores = new object[] { usuario, clave, empresa };

            return Convert.ToString(
                Cacceso.ExecProc(
                    "spSeleccionMenuPrincipal",
                    iParametros,
                    oParametros,
                    objValores,
                    "ppa").GetValue(0));
        }

    }
}