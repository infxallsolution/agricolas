using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.CuentasxCobrar
{
    public class CcajaOtros
    {
        public CcajaOtros()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public CcajaOtros(string auxiliar, string nombreCuenta, decimal valor, int registro, string notas, bool anulado)
        {
            _auxiliar = auxiliar;
            _nombreCuenta = nombreCuenta;
            _valor = valor;
            _registro = registro;
            _notas = notas;
            _anulado = anulado;
        }

        private string _auxiliar;
        private string _nombreCuenta;
        private decimal _valor;
        private int _registro;
        private string _notas;
        private bool _anulado;

        public string Auxiliar
        {
            get
            {
                return _auxiliar;
            }

            set
            {
                _auxiliar = value;
            }
        }

        public string NombreCuenta
        {
            get
            {
                return _nombreCuenta;
            }

            set
            {
                _nombreCuenta = value;
            }
        }

        public decimal Valor
        {
            get
            {
                return _valor;
            }

            set
            {
                _valor = value;
            }
        }

        public int Registro
        {
            get
            {
                return _registro;
            }

            set
            {
                _registro = value;
            }
        }

        public string Notas
        {
            get
            {
                return _notas;
            }

            set
            {
                _notas = value;
            }
        }

        public bool Anulado
        {
            get
            {
                return _anulado;
            }

            set
            {
                _anulado = value;
            }
        }
    }
}