using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class cFuncionario
{
    private string funcionario;
    private byte[] huella;
    private string estado;
    private string metodo;
    private int intento;
    private string imagehuella;

    public string Funcionario
    {
        get
        {
            return funcionario;
        }

        set
        {
            funcionario = value;
        }
    }

    public byte[] Huella
    {
        get
        {
            return huella;
        }

        set
        {
            huella = value;
        }
    }

    public string Estado
    {
        get
        {
            return estado;
        }

        set
        {
            estado = value;
        }
    }

    public string Metodo
    {
        get
        {
            return metodo;
        }

        set
        {
            metodo = value;
        }
    }

    public int Intento
    {
        get
        {
            return intento;
        }

        set
        {
            intento = value;
        }
    }

    public string Imagehuella
    {
        get
        {
            return imagehuella;
        }

        set
        {
            imagehuella = value;
        }
    }

    public static DataView SeleccionaFuncionaros(int empresa )
    {
        string[] iParametros = new string[] { "@empresa" };
        object[] objValores = new object[] { empresa };

        return Cacceso.DataSetParametros(
            "spSeleccionaFuncionarioHuella",
            iParametros,
            objValores,
            "ppa").Tables[0].DefaultView;
    }

    public static string RetornaNombreFuncionario(string idFuncionario, int empresa)
    {
        string[] iParametros = new string[] { "@idFuncionario", "@empresa" };
        string[] oParametros = new string[] { "@nombre" };
        object[] objValores = new object[] { idFuncionario, empresa };

        return Convert.ToString(Cacceso.ExecProc(
            "spRetornaNombreFuncionario",
            iParametros,
            oParametros,
            objValores, "ppa").GetValue(0));
    }

    public static int RetornaFuncionarioActivo(string idFuncionario)
    {
        string[] iParametros = new string[] { "@idFuncionario" };
        string[] oParametros = new string[] { "@activo" };
        object[] objValores = new object[] { idFuncionario };

        return Convert.ToInt16(Cacceso.ExecProc(
            "spRetornaFuncionarioActivo",
            iParametros,
            oParametros,
            objValores, "ppa").GetValue(0));
    }

    public static int RetornaFuncionarioTurno(string idFuncionario)
    {
        string[] iParametros = new string[] { "@idFuncionario" };
        string[] oParametros = new string[] { "@turno" };
        object[] objValores = new object[] { idFuncionario };

        return Convert.ToInt16(Cacceso.ExecProc(
            "spRetornaFuncionarioTurno",
            iParametros,
            oParametros,
            objValores, "ppa").GetValue(0));
    }

    public static int RetornaVerificaEntradaNoTurno(string idFuncionario, DateTime fecha)
    {
        string[] iParametros = new string[] { "@idFuncionario", "@fecha" };
        string[] oParametros = new string[] { "@retorno" };
        object[] objValores = new object[] { idFuncionario, fecha };

        return Convert.ToInt16(Cacceso.ExecProc(
            "spVerificaEntradaSinTurno",
            iParametros,
            oParametros,
            objValores, "ppa").GetValue(0));
    }

    public static int RetornaVerificaEntradaTurno(string idFuncionario, DateTime fecha)
    {
        string[] iParametros = new string[] { "@idFuncionario", "@fecha" };
        string[] oParametros = new string[] { "@retorno" };
        object[] objValores = new object[] { idFuncionario, fecha };

        return Convert.ToInt16(Cacceso.ExecProc(
            "spVerificaEntradaTurno",
            iParametros,
            oParametros,
            objValores, "ppa").GetValue(0));
    }


    public static int ValidacionesRegistroFuncionarioPorteria(string idFuncionario, int empresa)
    {
        string[] iParametros = new string[] { "@funcionario", "@empresa" };
        string[] oParametros = new string[] { "@retorno" };
        object[] objValores = new object[] { idFuncionario, empresa };

        return Convert.ToInt16(Cacceso.ExecProc(
            "spValidacionesRegistroFuncionarioPorteria",
            iParametros,
            oParametros,
            objValores, "ppa").GetValue(0));
    }


}
