using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Transaccion
{
    public class CtransaccionNovedad
    {
        #region Atributos

        private string _empleado, _labor;
        private string _nombreLabor, _nombreEmpleado;
        private string _lote;
        private string _uMedida;
        private DateTime _fecha;
        private decimal _cantidad;
        private decimal _jornales;
        private bool _noAplicaJornal;

        public bool NoAplicaJornal
        {
            get { return _noAplicaJornal; }
            set { _noAplicaJornal = value; }
        }

        public decimal Jornales
        {
            get { return _jornales; }
            set { _jornales = value; }
        }
        private int _registro;
        private bool _anulado;
        private string _seccion;

        public string Seccion
        {
            get { return _seccion; }
            set { _seccion = value; }
        }

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        public bool Anulado
        {
            get { return _anulado; }
            set { _anulado = value; }
        }

        public string UMedida
        {
            get { return _uMedida; }
            set { _uMedida = value; }
        }
        public string Lote
        {
            get { return _lote; }
            set { _lote = value; }
        }
        public string Labor
        {
            get { return _labor; }
            set { _labor = value; }
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
        public string NombreLabor
        {
            get { return _nombreLabor; }
            set { _nombreLabor = value; }
        }
        public decimal Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
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

        public CtransaccionNovedad(string empleado, string labor, string nombreLabor, string nombreEmpleado, decimal cantidad, string uMedida, string lote, DateTime fecha,
            decimal jornales, string seccion, int registro, bool anulado, bool noAplicaJornal)
        {
            _empleado = empleado;
            _labor = labor;
            _nombreLabor = nombreLabor;
            _nombreEmpleado = nombreEmpleado;
            _cantidad = cantidad;
            _lote = lote;
            _jornales = jornales;
            _seccion = seccion;
            _uMedida = uMedida;
            _registro = registro;
            _anulado = anulado;
            _fecha = fecha;
            _noAplicaJornal = noAplicaJornal;
        }
    }
}