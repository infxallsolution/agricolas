using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Administracion
{
    public class Clineas
    {
        private int _linea;
        private string _lote;
        private int _noPalma;
        private bool _orden;

        public bool Orden
        {
            get { return _orden; }
            set { _orden = value; }
        }


        public Clineas(int Linea, string Lote, int NoPalma, bool orden)
        {
            _linea = Linea;
            _lote = Lote;
            _noPalma = NoPalma;
            _orden = orden;

        }

        public int Linea
        {
            get { return _linea; }
            set { _linea = value; }
        }
        public string Lote
        {
            get { return _lote; }
            set { _lote = value; }
        }

        public int NoPalma
        {
            get { return _noPalma; }
            set { _noPalma = value; }
        }


    }
}