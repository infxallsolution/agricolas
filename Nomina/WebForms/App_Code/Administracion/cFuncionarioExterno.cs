﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class cFuncionarioExterno
    {
        public cFuncionarioExterno()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {
            DataView dvEntidad = new DataView();

            dvEntidad = CentidadMetodos.EntidadGet(
                "nFuncionarioExterno",
                "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and (codigo like '%" + texto + "%' or nombre like '%" + texto + "%')";

            return dvEntidad;
        }
    }
}