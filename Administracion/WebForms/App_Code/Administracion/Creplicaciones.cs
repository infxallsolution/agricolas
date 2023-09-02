using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Administracion.WebForms.App_Code.Administracion
{
    public class Creplicaciones
    {

        public Creplicaciones()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public DataView BuscarEntidadCampo(string texto)
        {
            DataView dvTipoTransaccion = CentidadMetodos.EntidadGet(
                "sReplicacion",
                "ppa").Tables[0].DefaultView;

            dvTipoTransaccion.RowFilter = "tabla like '%" + texto + "%'";
            return dvTipoTransaccion;
        }


        public string[] NoRegistros(string empresaA, string tabla)
        {
            string[] noRegistros = new string[1];
            string[] iParametros = new string[] { "@empresaA", "@tabla" };
            object[] objValores = new object[] { empresaA, tabla };

            foreach (DataRowView registro in Cacceso.DataSetParametros(
                "spNoRegistrosTablaReplicar", iParametros, objValores,
                "ppa").Tables[0].DefaultView)
            {
                noRegistros.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(0)), 0);

            }

            return noRegistros;
        }


        public int AgregarRegistro(string empresa, string empresaB, string tabla)
        {
            string[] iParametros = new string[] { "@empresaA", "@empresaB", "@tabla" };
            string[] oParametros = new string[] { "@Retorno" };
            object[] objValores = new object[] { empresa, empresaB, tabla };

            return Convert.ToInt16(
                Cacceso.ExecProc(
                    "spInsertaTablaReplicar",
                    iParametros,
                    oParametros,
                    objValores,
                    "ppa").GetValue(0));
        }



    }
}