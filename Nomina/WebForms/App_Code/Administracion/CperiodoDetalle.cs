using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Administracion
{
    public class CperiodoDetalle
    {

        private int _año;

        public int Año
        {
            get { return _año; }
            set { _año = value; }
        }
        private int _mes;

        public int Mes
        {
            get { return _mes; }
            set { _mes = value; }
        }
        private int _noPeriodo;

        public int NoPeriodo
        {
            get { return _noPeriodo; }
            set { _noPeriodo = value; }
        }
        private DateTime _fechaInicial;

        public DateTime FechaInicial
        {
            get { return _fechaInicial; }
            set { _fechaInicial = value; }
        }
        private DateTime _fechaFinal;

        public DateTime FechaFinal
        {
            get { return _fechaFinal; }
            set { _fechaFinal = value; }
        }
        private DateTime _fechaCorte;

        public DateTime FechaCorte
        {
            get { return _fechaCorte; }
            set { _fechaCorte = value; }
        }
        private DateTime _fechaPago;

        public DateTime FechaPago
        {
            get { return _fechaPago; }
            set { _fechaPago = value; }
        }
        private bool _cerrado;

        public bool Cerrado
        {
            get { return _cerrado; }
            set { _cerrado = value; }
        }

        public CperiodoDetalle()
        {
        }

        public CperiodoDetalle(int año, int mes, int noPeriodo, DateTime fechaInicial, DateTime fechaFinal, DateTime fechaCorte, DateTime fechaPago, bool cerrado)
        {
            _año = año;
            _mes = mes;
            _noPeriodo = noPeriodo;
            _fechaInicial = fechaInicial;
            _fechaFinal = fechaFinal;
            _fechaCorte = fechaCorte;
            _fechaPago = fechaPago;
            _cerrado = cerrado;

        }
    }
}