using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

public class CentidadMetodos
{
    public static DataSet EntidadGet(string entidad, string dBase)
    {
        return Cacceso.DataSet(
            Convert.ToString(Cmapeo.GetEntidadMetodos(entidad, dBase).GetValue(0)),
            dBase);
    }

    public static DataSet EntidadGetWhere(string entidad, string dBase, object[] valores)
    {
        string procedimiento = Convert.ToString(Cmapeo.GetEntidadMetodos(entidad, dBase).GetValue(5));
        int i = 0;

        string[] iParametros = new string[Cmapeo.GetProcedimientoParametros(procedimiento, 0, dBase).Count];

        foreach (DataRowView registro in Cmapeo.GetProcedimientoParametros(procedimiento, 0, dBase))
        {
            iParametros.SetValue(
                Convert.ToString(registro.Row.ItemArray.GetValue(1)),
                i);

            i++;
        }

        return Cacceso.DataSetParametros(
            procedimiento,
            iParametros,
            valores,
            dBase);
    }

    public static DataSet EntidadGetKey(string entidad, string dBase, object[] valores)
    {
        string procedimiento = Convert.ToString(Cmapeo.GetEntidadMetodos(entidad, dBase).GetValue(4));
        int i = 0;

        string[] iParametros = new string[Cmapeo.GetProcedimientoParametros(procedimiento, 0, dBase).Count];

        foreach (DataRowView registro in Cmapeo.GetProcedimientoParametros(procedimiento, 0, dBase))
        {
            iParametros.SetValue(
                Convert.ToString(registro.Row.ItemArray.GetValue(1)),
                i);

            i++;
        }

        return Cacceso.DataSetParametros(
            procedimiento,
            iParametros,
            valores,
            dBase);
    }

    public static int EntidadInsertUpdateDelete(string entidad, string operacion, string dBase, object[] valores)
    {
        string procedimiento = "";
        int i = 0;

        switch (operacion)
        {
            case "inserta":

                procedimiento = Convert.ToString(Cmapeo.GetEntidadMetodos(entidad, dBase).GetValue(1));

                break;

            case "actualiza":

                procedimiento = Convert.ToString(Cmapeo.GetEntidadMetodos(entidad, dBase).GetValue(2));

                break;

            case "elimina":

                procedimiento = Convert.ToString(Cmapeo.GetEntidadMetodos(entidad, dBase).GetValue(3));

                break;
        }

        string[] iParametros = new string[Cmapeo.GetProcedimientoParametros(procedimiento, 0, dBase).Count];

        foreach (DataRowView registro in Cmapeo.GetProcedimientoParametros(procedimiento, 0, dBase))
        {
            iParametros.SetValue(
                Convert.ToString(registro.Row.ItemArray.GetValue(1)),
                i);

            i++;
        }

        string[] oParametros = new string[Cmapeo.GetProcedimientoParametros(procedimiento, 1, dBase).Count];
        i = 0;

        foreach (DataRowView registro in Cmapeo.GetProcedimientoParametros(procedimiento, 1, dBase))
        {
            oParametros.SetValue(
                Convert.ToString(registro.Row.ItemArray.GetValue(1)),
                i);

            i++;
        }

        return Convert.ToInt16(Cacceso.ExecProc(
            procedimiento,
            iParametros,
            oParametros,
            valores,
            dBase).GetValue(0));
    }
}

