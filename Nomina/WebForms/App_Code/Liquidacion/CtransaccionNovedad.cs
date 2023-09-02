using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Liquidacion
{
    public class CtransaccionNovedad
    {
        #region Atributos

        private string _empleado, _concepto;
        private string _nombreConcepto, _nombreEmpleado;
        private decimal _cantidad;
        private int _añoFinal;
        private int _pInicial;
        private int _pFinal;
        private int _FR;
        private decimal _valor;
        private string _detalle;
        private int _registro;
        private bool _anulado;
        private int _añoInicial;

        public int AñoFinal
        {
            get { return _añoFinal; }
            set { _añoFinal = value; }
        }

        public int AñoInicial
        {
            get { return _añoInicial; }
            set { _añoInicial = value; }
        }


        public bool Anulado
        {
            get { return _anulado; }
            set { _anulado = value; }
        }

        public string Concepto
        {
            get { return _concepto; }
            set { _concepto = value; }
        }
        public string Empleado
        {
            get { return _empleado; }
            set { _empleado = value; }
        }
        public string NombreEmpleado
        {
            get { return _nombreEmpleado; }
            set { _nombreEmpleado = value; }
        }
        public string NombreConcepto
        {
            get { return _nombreConcepto; }
            set { _nombreConcepto = value; }
        }
        public decimal Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        public int PInicial
        {
            get { return _pInicial; }
            set { _pInicial = value; }
        }
        public int PFinal
        {
            get { return _pFinal; }
            set { _pFinal = value; }
        }
        public int FR
        {
            get { return _FR; }
            set { _FR = value; }
        }
        public decimal Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }
        public string Detalle
        {
            get { return _detalle; }
            set { _detalle = value; }
        }
        public int Registro
        {
            get { return _registro; }
            set { _registro = value; }
        }

        #endregion Atributos

        public CtransaccionNovedad()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public CtransaccionNovedad(string empleado, string concepto, string nombreConcepto, string nombreEmpleado, decimal cantidad,
            int añoInicial, int añoFinal, int pInicial, int pFinal, int FR, decimal valor, string detalle, int registro, bool anulado)
        {
            _empleado = empleado;
            _concepto = concepto;
            _nombreConcepto = nombreConcepto;
            _nombreEmpleado = nombreEmpleado;
            _cantidad = cantidad;
            _añoInicial = añoInicial;
            _pInicial = pInicial;
            _pFinal = pFinal;
            _FR = FR;
            _valor = valor;
            _detalle = detalle;
            _registro = registro;
            _anulado = anulado;
            _añoFinal = añoFinal;
        }
    }
}