using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almacen.WebForms.App_Code.Transaccion
{
    public class Cimpuestos
    {
        private string _concepto;

        public string Concepto
        {
            get { return _concepto; }
            set { _concepto = value; }
        }
        private string _nombreConcepto;

        public string NombreConcepto
        {
            get { return _nombreConcepto; }
            set { _nombreConcepto = value; }
        }
        private decimal _baseMinima;

        public decimal BaseMinima
        {
            get { return _baseMinima; }
            set { _baseMinima = value; }
        }
        private decimal _valorBruto;

        public decimal ValorBruto
        {
            get { return _valorBruto; }
            set { _valorBruto = value; }
        }
        private string _clase;

        public string Clase
        {
            get { return _clase; }
            set { _clase = value; }
        }
        private decimal _valorImpuesto;

        public decimal ValorImpuesto
        {
            get { return _valorImpuesto; }
            set { _valorImpuesto = value; }
        }
        private decimal _tasa;

        public decimal Tasa
        {
            get { return _tasa; }
            set { _tasa = value; }
        }
        private decimal _baseGravable;

        public decimal BaseGravable
        {
            get { return _baseGravable; }
            set { _baseGravable = value; }
        }

        public Cimpuestos()
        { }

        public Cimpuestos(string concepto, string nombreConcepto, string clase, decimal baseGravable, decimal tasa, decimal baseMinima, decimal valoBruto, decimal valorImpuesto)
        {
            _concepto = concepto;
            _nombreConcepto = nombreConcepto;
            _clase = clase;
            _tasa = tasa;
            _baseGravable = baseGravable;
            _valorBruto = valoBruto;
            _valorImpuesto = valorImpuesto;
        }
    }
}