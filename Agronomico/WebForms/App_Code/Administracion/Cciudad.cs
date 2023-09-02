using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Class1
/// </summary>
public class Cciudad
{
    public Cciudad()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

       public DataView BuscarEntidad(string texto, string entidad, int empresa)
    {
        DataView dvEntidad = new DataView();

        dvEntidad = CentidadMetodos.EntidadGet("gCiudad", "ppa").Tables[0].DefaultView;
        dvEntidad.RowFilter = "departamento ='" + entidad + "' and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";
        return dvEntidad;
    }
}