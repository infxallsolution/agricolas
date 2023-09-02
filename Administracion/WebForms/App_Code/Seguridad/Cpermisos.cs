using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Administracion.WebForms.App_Code.Seguridad
{
    public class Cpermisos
    {
        private string rowidmenu;
        private string operaciones;
        private bool chequeado;

        private string modulo;

        public string Rowidmenu
        {
            get
            {
                return rowidmenu;
            }

            set
            {
                rowidmenu = value;
            }
        }

        public string Operaciones
        {
            get
            {
                return operaciones;
            }

            set
            {
                operaciones = value;
            }
        }

        public bool Chequeado
        {
            get
            {
                return chequeado;
            }

            set
            {
                chequeado = value;
            }
        }

        public string Modulo
        {
            get
            {
                return modulo;
            }

            set
            {
                modulo = value;
            }
        }

        public Cpermisos()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public DataSet ModulosActivos()
        {
            return Cacceso.DataSet(
                "spSeleccionaModulosActivos",
                "ppa");
        }

        public DataSet PerfilesActivos()
        {
            return Cacceso.DataSet(
                "spSeleccionaPerfiilesActivos",
                "ppa");
        }

        public DataSet UsuariosActivos()
        {
            return Cacceso.DataSet(
                "spSeleccionaUsuariosActivos",
                "ppa");
        }

        public DataSet OperecionesActivos()
        {
            return Cacceso.DataSet(
                "spSeleccionaOperacionActivos",
                "ppa");
        }

        public DataSet SeleccionaMenuModulo(string modulo)
        {
            string[] iParametros = new string[] { "@modulo" };
            object[] objValores = new object[] { modulo };

            return Cacceso.DataSetParametros(
                "spSeleccionaMenuModulos",
                iParametros,
                objValores,
                "ppa");
        }

        public DataView SeleccionaPermisosPerfilEmpresa(string perfil, int empresa)
        {
            string[] iParametros = new string[] { "@perfil", "@empresa" };
            object[] objValores = new object[] { perfil, empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaPermisosPerfilEmpresa",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public int ValidaModulos(string modulo)
        {
            string[] iParametros = new string[] { "@modulo" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { modulo };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spValidaMenuModulos",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataSet SeleccionaMenu()
        {
            string[] iParametros = new string[] { };
            object[] objValores = new object[] { };

            return Cacceso.DataSetParametros(
                "spSeleccionaMenu",
                iParametros,
                objValores,
                "ppa");
        }

        public DataSet SeleciconaModulosActivos()
        {
            string[] iParametros = new string[] { };
            object[] objValores = new object[] { };

            return Cacceso.DataSetParametros(
                "spSeleccionaModulosActivos",
                iParametros,
                objValores,
                "ppa");
        }

        public DataView SeleciconaOperacionesMenu(int menu)
        {
            string[] iParametros = new string[] { "@menu" };
            object[] objValores = new object[] { menu };

            return Cacceso.DataSetParametros(
                "spSeleccionaOperacionesMenu",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }





    }
}