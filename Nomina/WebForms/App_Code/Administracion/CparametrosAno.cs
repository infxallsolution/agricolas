using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class CparametrosAno
    {
        public CparametrosAno()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }






        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();



            dvEntidad = CentidadMetodos.EntidadGet(
                "nParametrosAno",
                "ppa").Tables[0].DefaultView;

            if (texto != "")
                dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and ano =" + texto;
            else
                dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa);

            return dvEntidad;
        }

        public DataView BuscarEntidadFestivos(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();



            dvEntidad = CentidadMetodos.EntidadGet(
                "nFestivo",
                "ppa").Tables[0].DefaultView;


            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and fechaFiltro  like '%" + texto.Trim() + "%'";

            return dvEntidad;
        }

        public void GeneraDominicales(string ano, int empresa)
        {
            string[] iParametros = new string[] { "@ano", "@empresa" };
            string[] oParametros = new string[] { };
            object[] objValores = new object[] { ano, empresa };

            Cacceso.ExecProc(
                "spInsertaDominicales",
                iParametros,
                oParametros,
                objValores,
                "ppa");
        }
    }
}