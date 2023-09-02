using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.CuentasxPagar
{
    public class CplanoBanco
    {
        private int _registro;

        public int Registro
        {
            get { return _registro; }
            set { _registro = value; }
        }
        private string _campo;

        public string Campo
        {
            get { return _campo; }
            set { _campo = value; }
        }
        private int _tipo;

        public int Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }
        private decimal _inicio;

        public decimal Inicio
        {
            get { return _inicio; }
            set { _inicio = value; }
        }
        private decimal _longitud;

        public decimal Longitud
        {
            get { return _longitud; }
            set { _longitud = value; }
        }
        private bool _mValor;

        public bool MValor
        {
            get { return _mValor; }
            set { _mValor = value; }
        }
        private string _valor;

        public string Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        public CplanoBanco()
        {
        }

        public CplanoBanco(int registro, string campo, int tipo, decimal inicio, decimal longitud, bool mValor, string valor)
        {
            _registro = registro;
            _campo = campo;
            _tipo = tipo;
            _inicio = inicio;
            _longitud = longitud;
            _mValor = mValor;
            _valor = valor;
        }
    }
}