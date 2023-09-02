﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.CuentasxCobrar
{
    public class Ccajas
    {
        public Ccajas()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public DataView BuscarEntidad(string texto, int empresa)
        {

            DataView dvEntidad = CentidadMetodos.EntidadGet("cCaja", "ppa").Tables[0].DefaultView;
            dvEntidad.RowFilter = "empresa =" + empresa.ToString() + " and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";
            return dvEntidad;
        }
    }
}