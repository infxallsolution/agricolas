using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CSello
/// </summary>
public class SELLO
{
    private string _sello;
    private bool _anulado;

    public bool Anulado
    {
        get { return _anulado; }
        set { _anulado = value; }
    }

    public string Sello
    {
        get { return _sello; }
        set { _sello = value; }
    }

    public SELLO(string sello, bool anulado)
    {
        this._sello = sello;
        this._anulado = anulado;
    }
    public SELLO()
    {
    }
}