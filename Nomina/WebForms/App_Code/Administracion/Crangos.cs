using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class Crangos
    {

        private int _numero;

        public int Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }
        private decimal _rInicio;

        public decimal RInicio
        {
            get { return _rInicio; }
            set { _rInicio = value; }
        }
        private decimal _rFinal;

        public decimal RFinal
        {
            get { return _rFinal; }
            set { _rFinal = value; }
        }
        private decimal _porcentaje;

        public decimal Porcentaje
        {
            get { return _porcentaje; }
            set { _porcentaje = value; }
        }
        private decimal _valor;

        public decimal Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }
        private bool _por;

        public bool Por
        {
            get { return _por; }
            set { _por = value; }
        }

        public Crangos(int numero, decimal rInicio, decimal rFinal, decimal porcentaje, decimal valor, bool por)
        {
            _numero = numero;
            _rInicio = rInicio;
            _rFinal = rFinal;
            _porcentaje = porcentaje;
            _valor = valor;
            _por = por;
        }
    }
}