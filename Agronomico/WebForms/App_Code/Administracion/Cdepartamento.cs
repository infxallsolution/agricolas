using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Cdepartamento
/// </summary>
public class Cdepartamento
{
    public Cdepartamento()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public DataView BuscarEntidad(string texto, string entidad, int empresa)
    {
        DataView dvEntidad = new DataView();
        dvEntidad = CentidadMetodos.EntidadGet("gDepartamento", "ppa").Tables[0].DefaultView;
        dvEntidad.RowFilter = "pais ='" + entidad + "' and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";
        return dvEntidad;
    }

}