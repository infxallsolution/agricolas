using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Almacen.WebForms.App_Code.Parametros
{
    public class CtipoDocumento
    {
        public CtipoDocumento()
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

        public DataView GetTipoTransaccionModulo(int empresa)
        {
            DataView dvTipoTransaccion = CentidadMetodos.EntidadGet(
                "gTipoTransaccion",
                "ppa").Tables[0].DefaultView;

            dvTipoTransaccion.RowFilter = " empresa = " + Convert.ToString(empresa) + "and modulo = '" + ConfigurationManager.AppSettings["Modulo"].ToString() + "'";
            dvTipoTransaccion.Sort = "descripcion";

            return dvTipoTransaccion;
        }

        public string RetornaNumeroTransaccion(string tipoTransaccion, int empresa)
        {
            string[] iParametros = new string[] { "@tipoTransaccion", "@empresa" };
            string[] oParametros = new string[] { "@consecutivo" };
            object[] objValores = new object[] { tipoTransaccion, empresa };

            return Convert.ToString(Cacceso.ExecProc("spRetornaConsecutivoTransaccion", iParametros, oParametros, objValores, "ppa").GetValue(0));
        }

    }
}