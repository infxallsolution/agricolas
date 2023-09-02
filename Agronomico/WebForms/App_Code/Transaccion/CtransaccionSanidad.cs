using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Transaccion
{
    public class CtransaccionSanidad
    {

        #region Atributos

        private DateTime _fecha;
        private string _concepto;
        private string _conceptoNombre;
        private string _referencia;
        private string _cantidad;
        private string _uMedida;
        private string _linea;
        private string _detalle;
        private string _palma;
        private int _registro;
        private string _gCaracteristica;
        private string _nGrupoCaracteristica;
        private string _caracteristica;
        private string _nCaracteristica;

        public string Caracteristica
        {
            get { return _caracteristica; }
            set { _caracteristica = value; }
        }

        public string NCaracteristica
        {
            get { return _nCaracteristica; }
            set { _nCaracteristica = value; }
        }

        public string GCaracteristica
        {
            get { return _gCaracteristica; }
            set { _gCaracteristica = value; }
        }

        public string NGrupoCaracteristica
        {
            get { return _nGrupoCaracteristica; }
            set { _nGrupoCaracteristica = value; }
        }
        public string Referencia
        {
            get { return _referencia; }
            set { _referencia = value; }
        }

        public string ConceptoNombre
        {
            get { return _conceptoNombre; }
            set { _conceptoNombre = value; }
        }
        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        public string Concepto
        {
            get { return _concepto; }
            set { _concepto = value; }
        }
        public string Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        public string UMedida
        {
            get { return _uMedida; }
            set { _uMedida = value; }
        }

        public string Linea
        {
            get { return _linea; }
            set { _linea = value; }
        }
        public string Detalle
        {
            get { return _detalle; }
            set { _detalle = value; }
        }
        public string Palma
        {
            get { return _palma; }
            set { _palma = value; }
        }

        public int Registro
        {
            get { return _registro; }
            set { _registro = value; }
        }
        #endregion Atributos

        public CtransaccionSanidad()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public CtransaccionSanidad(DateTime fecha, string concepto, string conceptoNombre, string gCaracteristica, string nGCaracteristica,
            string caracteristica, string ncaracteristica, string cantidad, string uMedida, string linea, string detalle,
         string palma, int registro, string referencia)
        {
            _fecha = fecha;
            _concepto = concepto;
            _cantidad = cantidad;
            _uMedida = uMedida;
            _linea = linea;
            _detalle = detalle;
            _palma = palma;
            _registro = registro;
            _conceptoNombre = conceptoNombre;
            _referencia = referencia;
            _gCaracteristica = gCaracteristica;
            _nGrupoCaracteristica = nGCaracteristica;
            _caracteristica = caracteristica;
            _nCaracteristica = ncaracteristica;
        }
    }
}