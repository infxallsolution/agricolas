using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Service.DataAcces.Properties;

public class Cacceso
{


    private static Boolean verificaconexion(string cadena)
    {
        try
        {
            SqlConnection cnn = new SqlConnection(cadena);
            cnn.Open();
            cnn.Close();
            return true;
        }
        catch
        {
            return false;
        }

    }

    private static string GetCadenaConexion(string Conexion)
    {
        string CadenaConexion = "";
        Cacceso a = new Cacceso();
        string indice = "";


        if (indice.Trim().Length == 0)
        {

            switch (Conexion)
            {
                case "ppa":
                    CadenaConexion = Settings.Default.CadenaConexion;
                    break;
            }
        }

        return CadenaConexion;
    }

    public static DataSet DataSet(string SpNombre, string Conexion)
    {
        SqlDatabase BdSql = new SqlDatabase(GetCadenaConexion(Conexion));
        DbCommand comando;

        comando = BdSql.GetStoredProcCommand(SpNombre);
        comando.CommandTimeout = 60;

        return BdSql.ExecuteDataSet(comando);
    }


    public static DataSet DataSetParametros(string SpNombre, string[] IParametros, object[] ObjValores, string Conexion)
    {
        SqlDatabase BdSql = new SqlDatabase(GetCadenaConexion(Conexion));
        DbCommand comando;
        int i;
        comando = BdSql.GetStoredProcCommand(SpNombre);
        i = 0;

        foreach (string ParIn in IParametros)
        {
            if (ObjValores.GetValue(i) == null)
                BdSql.AddInParameter(comando, ParIn, DbType.Boolean, null);
            else
            {
                if (ObjValores.GetValue(i).GetType() == typeof(string))
                    BdSql.AddInParameter(comando, ParIn, DbType.String, ObjValores.GetValue(i));
                else if (ObjValores.GetValue(i).GetType() == typeof(DateTime))
                    BdSql.AddInParameter(comando, ParIn, DbType.Date, ObjValores.GetValue(i));
                else if (ObjValores.GetValue(i).GetType() == typeof(int))
                    BdSql.AddInParameter(comando, ParIn, DbType.Int32, ObjValores.GetValue(i));
                else if (ObjValores.GetValue(i).GetType() == typeof(bool))
                    BdSql.AddInParameter(comando, ParIn, DbType.Boolean, ObjValores.GetValue(i));
                else if (ObjValores.GetValue(i).GetType() == typeof(decimal))
                    BdSql.AddInParameter(comando, ParIn, DbType.Decimal, ObjValores.GetValue(i));
                else if (ObjValores.GetValue(i).GetType() == typeof(Byte[]))
                    BdSql.AddInParameter(comando, ParIn, DbType.Binary, ObjValores.GetValue(i));
                else
                    BdSql.AddInParameter(comando, ParIn, DbType.Decimal, ObjValores.GetValue(i));
            }

            i++;
        }
        comando.CommandTimeout = 60;
        BdSql.ExecuteNonQuery(comando);

        return BdSql.ExecuteDataSet(comando);
    }

    public static object[] ExecProc(string SpNombre, string[] IParametros, string[] OParametros, object[] ObjValores, string Conexion)
    {
        SqlDatabase BdSql = new SqlDatabase(GetCadenaConexion(Conexion));
        DbCommand comando;
        Object[] ObjRetorno = new object[OParametros.GetLength(0)];
        int i;

        comando = BdSql.GetStoredProcCommand(SpNombre);
        comando.CommandTimeout = 300;

        i = 0;

        foreach (string ParIn in IParametros)
        {
            if (ObjValores.GetValue(i) == null)
                BdSql.AddInParameter(comando, ParIn, DbType.Boolean, null);
            else
            {
                if (ObjValores.GetValue(i).GetType() == typeof(string))
                    BdSql.AddInParameter(comando, ParIn, DbType.String, ObjValores.GetValue(i));
                else if (ObjValores.GetValue(i).GetType() == typeof(DateTime))
                    BdSql.AddInParameter(comando, ParIn, DbType.Date, ObjValores.GetValue(i));
                else if (ObjValores.GetValue(i).GetType() == typeof(int))
                    BdSql.AddInParameter(comando, ParIn, DbType.Int32, ObjValores.GetValue(i));
                else if (ObjValores.GetValue(i).GetType() == typeof(bool))
                    BdSql.AddInParameter(comando, ParIn, DbType.Boolean, ObjValores.GetValue(i));
                else if (ObjValores.GetValue(i).GetType() == typeof(decimal))
                    BdSql.AddInParameter(comando, ParIn, DbType.Decimal, ObjValores.GetValue(i));
                else if (ObjValores.GetValue(i).GetType() == typeof(Byte[]))
                    BdSql.AddInParameter(comando, ParIn, DbType.Binary, ObjValores.GetValue(i));
                else
                    BdSql.AddInParameter(comando, ParIn, DbType.Decimal, ObjValores.GetValue(i));
            }

            i++;
        }

        foreach (string Opar in OParametros)
        {
            BdSql.AddOutParameter(comando, Opar, DbType.String, 256);
        }

        BdSql.ExecuteNonQuery(comando);

        i = 0;

        foreach (string Opar in OParametros)
        {
            ObjRetorno.SetValue(BdSql.GetParameterValue(comando, Opar), i);
            i++;
        }

        return ObjRetorno;
    }
}

