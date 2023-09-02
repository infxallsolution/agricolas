using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

public class Cmapeo
{
    public static string[] GetEntidadMetodos(string entidad, string dBase)
    {
        DataView dvEntidad = new DataView();
        string[] retorno = new string[5];
        string conexion = "";

        if (dBase == "ppa")
        {
            conexion = "InfoS";
        }


        dvEntidad = Cacceso.DataSet(
            "SpGetsysEntidadMetodos",
            dBase).Tables[0].DefaultView;
        dvEntidad.RowFilter = "entidad = '" + entidad + "' and dBase = '" + conexion + "'";

        foreach (DataRowView registro in dvEntidad)
        {
            retorno.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(2)), 0);
            retorno.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(3)), 1);
            retorno.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(4)), 2);
            retorno.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(5)), 3);
            retorno.SetValue(Convert.ToString(registro.Row.ItemArray.GetValue(6)), 4);
        }

        return retorno;
    }

    public static DataView GetProcedimientoParametros(string procedimiento, int tipo, string dBase)
    {
        DataView dvParametros = new DataView();
        string[] iParametros = new string[] { "@name", "@type" };
        object[] objValores = new object[] { procedimiento, tipo };

        dvParametros = Cacceso.DataSetParametros(
            "spSysObtenerParametros",
            iParametros,
            objValores,
            dBase).Tables[0].DefaultView;

        return dvParametros;
    }
}

