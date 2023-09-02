using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.General
{
    public class cMenu
    {
        public cMenu()
        {

        }

        public DataView seleccionaIconoNombreModulo(string codMenu)
        {
            string[] iParametros = new string[] { "@codigo" };
            object[] objValores = new object[] { codMenu };

            return Cacceso.DataSetParametros("SpGetsModuloskey", iParametros, objValores, "ppa").Tables[0].DefaultView;
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