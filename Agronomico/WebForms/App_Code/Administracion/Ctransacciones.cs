using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de transacciones
/// </summary>
public class Ctransacciones
{

    public Ctransacciones()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public DataView GetTipoTransaccionModulo(int empresa)
    {
        DataView dvTipoTransaccion = CentidadMetodos.EntidadGet(
            "gTipoTransaccion",
            "ppa").Tables[0].DefaultView;

        dvTipoTransaccion.RowFilter = "empresa = " + empresa.ToString();
        dvTipoTransaccion.Sort = "descripcion";

        return dvTipoTransaccion;
    }

    public string RetornaNumeroTransaccion(string tipoTransaccion, int empresa)
    {
        string[] iParametros = new string[] { "@tipoTransaccion", "@empresa" };
        string[] oParametros = new string[] { "@consecutivo" };
        object[] objValores = new object[] { tipoTransaccion, empresa };

        return Convert.ToString(Cacceso.ExecProc(
            "spRetornaConsecutivoTransaccion",
            iParametros,
            oParametros,
            objValores,
            "ppa").GetValue(0));
    }

    public string retornaFormatoTipoTransaccion(string tipoTransaccion, int empresa)
    {
        string[] iParametros = new string[] { "@tipoTransaccion", "@empresa" };
        string[] oParametros = new string[] { "@retorno" };
        object[] objValores = new object[] { tipoTransaccion, empresa };

        return Convert.ToString(Cacceso.ExecProc(
            "spRetornaFormatoTipoTransaccion",
            iParametros,
            oParametros,
            objValores,
            "ppa").GetValue(0));
    }

    public string TipoTransaccionConfig(string tipoTransaccion, int empresa)
    {
        string retorno = "";
        object[] objKey = new object[] { empresa, tipoTransaccion };

        foreach (DataRowView registro in CentidadMetodos.EntidadGetKey(
            "gTipoTransaccionConfig",
            "ppa",
            objKey).Tables[0].DefaultView)
        {
            for (int i = 2; i < registro.Row.ItemArray.Length; i++)
            {
                retorno = retorno + registro.Row.ItemArray.GetValue(i).ToString() + "*";
            }
        }

        return retorno;
    }

}