using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CparametrosGeneral
/// </summary>
public class CparametrosGeneral
{
    public CparametrosGeneral()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public DataView BuscarEntidad(int empresa)
    {
        DataView dvEntidad = new DataView();

        dvEntidad = CentidadMetodos.EntidadGet(
            "gParametrosGenerales",
            "ppa").Tables[0].DefaultView;
        dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa);

        return dvEntidad;
    }
    public DataView BuscarEntidad(string texto,  int empresa)
    {
        DataView dvEntidad = new DataView();

        dvEntidad = CentidadMetodos.EntidadGet(
            "gConfigParametrosGenerales",
            "ppa").Tables[0].DefaultView;
        dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa) + " and nombre like '%" + texto + "%'";

        return dvEntidad;
    }

    public DataView RetornaTablasInfos()
    {
        string[] iParametros = new string[] { };
        object[] objValores = new object[] { };

        return Cacceso.DataSetParametros(
            "spRetornaTablasInfos",
            iParametros,
            objValores,
            "ppa").Tables[0].DefaultView;
    }

    public DataView SeleccionaCamposEntidades(string id1, string id2)
    {
        string[] iParametros = new string[] {"@id1","@id2" };
        object[] objValores = new object[] {id1, id2 };

        return Cacceso.DataSetParametros(
            "spSeleccionaCamposEntidadesII",
            iParametros,
            objValores,
            "ppa").Tables[0].DefaultView;
    }

}
