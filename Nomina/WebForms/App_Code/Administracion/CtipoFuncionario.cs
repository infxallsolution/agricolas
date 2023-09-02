using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class CtipoFuncionario
    {
        public CtipoFuncionario()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet("nFuncionarioTipo", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";

            return dvEntidad;
        }

        public string TipoFuncionarioConfig(string tipoTransaccion, int empresa)
        {
            string retorno = "";
            object[] objKey = new object[] { tipoTransaccion, empresa };

            foreach (DataRowView registro in CentidadMetodos.EntidadGetKey("nFuncionarioTipo", "ppa", objKey).Tables[0].DefaultView)
            {
                for (int i = 2; i < registro.Row.ItemArray.Length; i++)
                {
                    retorno = retorno + registro.Row.ItemArray.GetValue(i).ToString() + "*";
                }
            }

            return retorno;
        }

    }
}