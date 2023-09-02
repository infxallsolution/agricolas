using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Administracion
{
    public class Cempresa
    {
        public Cempresa()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        public DataView SeleccionaEmpresasExtractoras()
        {
            DataView dvEmpresa = CentidadMetodos.EntidadGet("gEmpresa", "ppa").Tables[0].DefaultView;
            dvEmpresa.RowFilter = "extractora=1";
            dvEmpresa.Sort = "razonSocial";
            return dvEmpresa;
        }

        public DataView SeleccionaExtractoras(int empresa)
        {

            string[] iParametros = new string[] { "@empresa" };
            object[] objValores = new object[] { empresa };

            return Cacceso.DataSetParametros(
                "spSeleccionaExtractoraExterna",
                iParametros,
                objValores,
                "ppa").Tables[0].DefaultView;


        }

    }
}