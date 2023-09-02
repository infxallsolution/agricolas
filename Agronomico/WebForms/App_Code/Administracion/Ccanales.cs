using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Administracion
{
    public class Ccanales
    {

        private string _tipoCanal;

        public string TipoCanal
        {
            get { return _tipoCanal; }
            set { _tipoCanal = value; }
        }
        private int _numero;

        public int Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }
        private decimal _metros;

        public decimal Metros
        {
            get { return _metros; }
            set { _metros = value; }
        }

        public string NombreTipoCanal
        {
            get
            {
                return _nombreTipoCanal;
            }

            set
            {
                _nombreTipoCanal = value;
            }
        }

        private string _nombreTipoCanal;


        public Ccanales(string tipoCanal, int numero, decimal metros, string nombreTipoCanal)
        {
            _tipoCanal = tipoCanal;
            _numero = numero;
            _metros = metros;
            _nombreTipoCanal = nombreTipoCanal;
        }
    }
}