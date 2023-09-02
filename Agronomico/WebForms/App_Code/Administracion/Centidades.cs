using System;
using System.Data;

/// <summary>
/// Summary description for Cjerarquia
/// </summary>
public class Centidades
{
    public Centidades()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public DataView BuscarEntidad(string texto, string entidad,int empresa)
    {
        DataView dvEntidad = new DataView();

        dvEntidad = CentidadMetodos.EntidadGet(
            entidad,
            "ppa").Tables[0].DefaultView;        
        dvEntidad.RowFilter = "empresa=" + Convert.ToString(empresa)  + " and (codigo like '%" + texto + "%' or descripcion like '%" + texto + "%')";

        return dvEntidad;
    }
    public DataView BuscarEntidad2(string texto, string entidad, int empresa)
    {
        DataView dvEntidad = new DataView();

        dvEntidad = CentidadMetodos.EntidadGet(
            entidad,
            "ppa").Tables[0].DefaultView;

        return dvEntidad;
    }
    public DataView BuscarEntidadA(string texto, string entidad)
    {
        DataView dvEntidad = new DataView();

        dvEntidad = CentidadMetodos.EntidadGet(
            entidad,
            "ppa").Tables[0].DefaultView;
        dvEntidad.RowFilter = "codigo like '%" + texto + "%' or descripcion like '%" + texto + "%'";

        return dvEntidad;
    }
    public DataView BuscarEntidadQ(string texto, string entidad)
    {
        DataView dvEntidad = new DataView();

        dvEntidad = CentidadMetodos.EntidadGet(
            entidad,
            "ppa").Tables[0].DefaultView;
        dvEntidad.RowFilter = "ano like '%" + texto + "%' or mes like '%" + texto + "%'";

        return dvEntidad;
    }

    public DataView BuscarEntidad()
    {
        return Cacceso.DataSet(
            "spSeleccionaEntidades",
            "ppa").Tables[0].DefaultView;
    }

    public DataSet GetEntidadesAuxiliares()
    {
        return Cacceso.DataSet(
            "spSeleccionaEntidadesGenerales",
            "ppa");
    }

    public DataView GetEntidadCampoEntidad(string entidad)
    {
        DataView dvEntidad = CcontrolesUsuario.OrdenarEntidadSinEmpresa(
            CentidadMetodos.EntidadGet("sysEntidadCampo", "ppa"),"campo");

        dvEntidad.RowFilter = "entidad = '" + entidad + "'";

        return dvEntidad;
    }
}
