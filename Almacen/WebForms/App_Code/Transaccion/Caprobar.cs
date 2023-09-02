using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Almacen.WebForms.App_Code.Transaccion
{
    public class Caprobar
    {
        public Caprobar()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public DataView GetPorAprobar(string tipo, int empresa, string numero)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Cacceso.DataSetParametros("spSeleccionaDocumentosAprobar", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView GetPorAprobarOrdenes(string tipo, int empresa, string numero)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Cacceso.DataSetParametros("spSeleccionaDocumentosAprobar", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView SeleccionaObservacionSolicitudes(string tipo, int empresa, string numero)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };
            return Cacceso.DataSetParametros("spSeleccionaObservacionSolicitudes", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView SeleccionaOrdenDetalle(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };
            return Cacceso.DataSetParametros("spSeleccionaDetalleOrdenCompraAprobar", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView SeleccionaImpuestoOrden(string tipo, string numero, int empresa)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };
            return Cacceso.DataSetParametros("spSeleccionaImpuestoOrdenCompraAprobar", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView GetCotizacionEditar(string tipo, int empresa, string numero)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };

            return Cacceso.DataSetParametros("spSeleccionaEditarCotizacion", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public DataView SeleccionaObservacionRequi(string numero, string tipo, int empresa)
        {
            string[] iParametros = new string[] { "@numero", "@tipo", "@empresa" };
            object[] objValores = new object[] { numero, tipo, empresa };

            return Cacceso.DataSetParametros("spSeleccionaObservacionRequi", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }
        public DataView cargarTipoDocumento(int empresa, string modulo)
        {
            string[] iParametros = new string[] { "@modulo", "@empresa" };
            object[] objValores = new object[] { modulo, empresa };

            return Cacceso.DataSetParametros("spSeleccionaDocumentoAprobar", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }


        public DataView GetPorAprobarDetalle(string tipo, int empresa, string numero)
        {
            string[] iParametros = new string[] { "@tipo", "@numero", "@empresa" };
            object[] objValores = new object[] { tipo, numero, empresa };
            return Cacceso.DataSetParametros("spSeleccionaDocumentoAprobarDetalle", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

        public int Aprueba(string tipo, string numero, string producto, decimal cantidad, string usuario, int registro, int empresa)
        {
            string[] iParametros = new string[] { "@numero", "@producto", "@cantidad", "@usuario", "@registro", "@tipo", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { numero, producto, cantidad, usuario, registro, tipo, empresa };
            return Convert.ToInt16(Cacceso.ExecProc("spApruebaDocumento", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

        public int ApruebaOrden(string tipo, string numero, string producto, decimal cantidad, string usuario, int registro, int empresa)
        {
            string[] iParametros = new string[] { "@numero", "@producto", "@cantidad", "@usuario", "@registro", "@tipo", "@empresa" };
            string[] oParametros = new string[] { "@retorno" };
            object[] objValores = new object[] { numero, producto, cantidad, usuario, registro, tipo, empresa };
            return Convert.ToInt16(Cacceso.ExecProc("spApruebaDocumento", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }
    }
}