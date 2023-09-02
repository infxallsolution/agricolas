using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Almacen.WebForms.App_Code.Transaccion
{
    public class CconteoFisico
    {
        public CconteoFisico()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //

        }



        public int procesarConteo(int empresa, string codigo, string bodega, bool fisico, bool ciclico, string descripcion, DateTime fecha,
          string usuario, string linea, int noConteos, int conteo = 0)
        {
            string[] iParametros = new string[] { "@empresa",
        "@codigo",
        "@fisico",
        "@ciclico",
        "@descripcion",
        "@fecha",
        "@usuarioRegistro",
        "@bodega",
        "@linea",
        "@noConteos",
        "@Conteos"
        };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, codigo, fisico, ciclico, descripcion, fecha, usuario, bodega, linea, noConteos, conteo };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spProcesarConteo",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int Consecutivo(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spConcecutivoConteo",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int guardaItemConteo(int empresa, string consecutivo, string item, decimal conteo, int noConteo, string usuario)
        {
            string[] iParametros = new string[] { "@empresa", "@consecutivo", "@item", "@conteo", "@noConteo", "@usuario" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa, consecutivo, item, conteo, noConteo, usuario };

            return Convert.ToInt16(Cacceso.ExecProc(
                "SpGuardaConteoItem",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int anularConteo(int empresa, string consecutivo)
        {
            string[] iParametros = new string[] { "@empresa", "@consecutivo" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spAnularConteo",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public int cerrarConteo(int empresa, string consecutivo)
        {
            string[] iParametros = new string[] { "@empresa", "@consecutivo" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { empresa };

            return Convert.ToInt16(Cacceso.ExecProc(
                "spAnularConteo",
                iParametros,
                oParametros,
                objValores,
                "ppa").GetValue(0));
        }

        public DataView seleccionaConteosAbiertos(int empresa)
        {
            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaConteoFisicoAbierto",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView seleccionaConteoFisico(int empresa, string numero, string plan, string mayor)
        {
            string[] iParametros = new string[] { "@empresa", "@numero", "@plan", "@mayor" };
            object[] objValores = new object[] { empresa, numero, plan, mayor };

            return Cacceso.DataSetParametros(
                "spSeleccionaConteoFisico",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

        public DataView itemsRegistrados(int empresa, string numero, int noConteo)
        {
            string[] iParametros = new string[] { "@empresa", "@numero", "@noConteo" };
            object[] objValores = new object[] { empresa, numero, noConteo };

            return Cacceso.DataSetParametros(
                "spSeleccionaConteoFisicoRegistrados",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;
        }

    }
}