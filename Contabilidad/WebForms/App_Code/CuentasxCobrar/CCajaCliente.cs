using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.CuentasxCobrar
{
    public class CCajaCliente
    {
        public CCajaCliente()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        #region Atributos

        private string _banco;
        private string _nombreBanco;
        private string _medioPago;
        private string _nombreMedioPago;
        private string _referencia;
        private decimal _valor;
        private DateTime _fechaDetalle;
        private string _cheque;
        private string _notas;
        private string _caja;
        private string _nombreCaja;
        private string _cuentaBanco;
        private string _nombreCuentaBanco;
        private int _registro;
        private bool _anulado;

        public string Banco
        {
            get
            {
                return _banco;
            }

            set
            {
                _banco = value;
            }
        }
        public string NombreBanco
        {
            get
            {
                return _nombreBanco;
            }

            set
            {
                _nombreBanco = value;
            }
        }
        public string MedioPago
        {
            get
            {
                return _medioPago;
            }

            set
            {
                _medioPago = value;
            }
        }
        public string NombreMedioPago
        {
            get
            {
                return _nombreMedioPago;
            }

            set
            {
                _nombreMedioPago = value;
            }
        }
        public string Referencia
        {
            get
            {
                return _referencia;
            }

            set
            {
                _referencia = value;
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
        public DateTime FechaDetalle
        {
            get
            {
                return _fechaDetalle;
            }

            set
            {
                _fechaDetalle = value;
            }
        }
        public string Cheque
        {
            get
            {
                return _cheque;
            }

            set
            {
                _cheque = value;
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
        public string Caja
        {
            get
            {
                return _caja;
            }

            set
            {
                _caja = value;
            }
        }
        public string NombreCaja
        {
            get
            {
                return _nombreCaja;
            }

            set
            {
                _nombreCaja = value;
            }
        }
        public string CuentaBanco
        {
            get
            {
                return _cuentaBanco;
            }

            set
            {
                _cuentaBanco = value;
            }
        }
        public string NombreCuentaBanco
        {
            get
            {
                return _nombreCuentaBanco;
            }

            set
            {
                _nombreCuentaBanco = value;
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


        #endregion Atributos


        public CCajaCliente(string banco, string nombreBanco, string medioPago, string nombreMedioPago, string referencia, decimal valor, DateTime fechaDetalle,
            string cheque, string notas, string caja, string nombreCaja, string cuentaBanco, string nombreCuentaBanco, int registro, bool anulado)
        {
            _anulado = anulado;
            _registro = registro;
            _banco = banco;
            _nombreBanco = nombreBanco;
            _medioPago = medioPago;
            _nombreMedioPago = nombreMedioPago;
            _referencia = referencia;
            _valor = valor;
            _fechaDetalle = fechaDetalle;
            _cheque = cheque;
            _notas = notas;
            _caja = caja;
            _nombreCaja = nombreCaja;
            _cuentaBanco = cuentaBanco;
            _nombreCuentaBanco = nombreCuentaBanco;
        }


        public DataView SeleccionaDetalleCxC(int empresa, string auxiliar, string tipoDocumento, string tercero, string sucursal, DateTime fechaInicial, DateTime fechaFinal,
            bool filtroFecha, bool filtroNumero, decimal numeroInicial, decimal numeroFinal)
        {
            string[] iParametros = new string[] { "@empresa", "@auxiliar", "@tipoDocumento", "@tercero", "@sucursal", "@fi", "@ff", "@filtroFecha", "@filtroNumero", "@ni", "@nf" };
            object[] objValores = new object[] { empresa, auxiliar, tipoDocumento, tercero, sucursal, fechaInicial, fechaFinal, filtroFecha, filtroNumero, numeroInicial, numeroFinal };
            return Cacceso.DataSetParametros("spSeleccionaCxCpendientePago", iParametros, objValores, "ppa").Tables[0].DefaultView;
        }

    }
}