using Almacen.WebForms.App_Code.Parametros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almacen.WebForms.App_Code.Transaccion
{
    public class CtransaccionAlmacen
    {
        public CtransaccionAlmacen()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        #region Instancias

        Citems item = new Citems();

        #endregion Instancias

        #region Atributos
        public decimal valorUnitarioVenta { get; set; }
        public decimal valorTotalVenta { get; set; }
        public string nombreBodega { get; set; }
        private string _bodega;
        private string _bodegaDestino;
        private string _nombreBodegaDestino;
        private string _producto;
        private decimal _cantidad;
        private string _uMedida;
        private decimal _valorUnitario;
        private string _destino;
        private string _nombreCcosto;
        private string _nombreUmedida;
        public string NombreDestino { get; set; }
        private string _cCosto;
        public string nombreCentroCosto { get; set; }
        private decimal _valorTotal;
        private string _detalle;
        private string _papeleta;
        private int _registro;
        private bool _anulado;
        private bool _oc;
        private decimal _saldo;
        private decimal _existencia;
        private string _tercero;
        private string _nombreTercero;
        private string nota;
        public string NombreProducto { get; set; }
        public bool check { get; set; }
        public decimal ValorDescuento { get; set; }
        public decimal ValorBruto { get; set; }
        public decimal pDescuento { get; set; }
        public decimal pImpuesto { get; set; }
        public decimal ValorImpuesto { get; set; }
        public decimal valorUltimaCompra { get; set; }
        public string tipoReferencia { get; set; }
        public string numeroReferencia { get; set; }
        public string conceptoImpuesto { get; set; }
        public decimal valorImpuesto { get; set; }
        public decimal valorNeto { get; set; }
        public bool Anulado
        {
            get { return _anulado; }
            set { _anulado = value; }
        }
        public int Registro
        {
            get { return _registro; }
            set { _registro = value; }
        }
        public string Papeleta
        {
            get { return _papeleta; }
            set { _papeleta = value; }
        }
        public string Detalle
        {
            get { return _detalle; }
            set { _detalle = value; }
        }
        public decimal ValorTotal
        {
            get { return _valorTotal; }
            set { _valorTotal = value; }
        }
        public string Ccosto
        {
            get { return _cCosto; }
            set { _cCosto = value; }
        }
        public string Destino
        {
            get { return _destino; }
            set { _destino = value; }
        }
        public decimal ValorUnitario
        {
            get { return _valorUnitario; }
            set { _valorUnitario = value; }
        }

        public string Umedida
        {
            get { return _uMedida; }
            set { _uMedida = value; }
        }
        public decimal Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        public string Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }
        public string Bodega
        {
            get { return _bodega; }
            set { _bodega = value; }
        }
        public bool Oc
        {
            get
            {
                return _oc;
            }

            set
            {
                _oc = value;
            }
        }
        public decimal Saldo
        {
            get
            {
                return _saldo;
            }

            set
            {
                _saldo = value;
            }
        }
        public decimal Existencia
        {
            get
            {
                return _existencia;
            }

            set
            {
                _existencia = value;
            }
        }
        public string Nota
        {
            get
            {
                return nota;
            }

            set
            {
                nota = value;
            }
        }
        public string Tercero
        {
            get
            {
                return _tercero;
            }

            set
            {
                _tercero = value;
            }
        }
        public string NombreTercero
        {
            get
            {
                return _nombreTercero;
            }

            set
            {
                _nombreTercero = value;
            }
        }
        public string BodegaDestino
        {
            get
            {
                return _bodegaDestino;
            }

            set
            {
                _bodegaDestino = value;
            }
        }
        public string NombreBodegaDestino
        {
            get
            {
                return _nombreBodegaDestino;
            }

            set
            {
                _nombreBodegaDestino = value;
            }
        }
        public string NombreUmedida
        {
            get
            {
                return _nombreUmedida;
            }

            set
            {
                _nombreUmedida = value;
            }
        }
        public string NombreCCosto
        {
            get
            {
                return _nombreCcosto;
            }

            set
            {
                _nombreCcosto = value;
            }
        }


        #endregion Atributos

        public CtransaccionAlmacen(string bodega, string producto, decimal cantidad, string uMedida, decimal valorUnitario, string destino,
            string cCosto, decimal valorTotal, string detalle, int registro, int empresa, bool anulado, string nota, string tercero, string nombreTerceroS)
        {
            _bodega = bodega;
            _producto = producto;
            _cantidad = cantidad;
            _uMedida = uMedida;
            _valorUnitario = valorUnitario;
            _destino = destino;
            _cCosto = cCosto;
            _valorTotal = (_valorUnitario * _cantidad);
            _anulado = anulado;
            _registro = registro;
            this.nota = nota;

            if (producto.Trim().Length == 0)
                _detalle = detalle;
            else
                _detalle = item.RetornaDescripcion(_producto, empresa);
        }

        public CtransaccionAlmacen(string bodega, string producto, decimal cantidad, string uMedida, decimal valorUnitario, string destino, string cCosto, decimal valorTotal, string detalle, int registro, int empresa, bool anulado, bool oc, string nota)
        {
            _bodega = bodega;
            _producto = producto;
            _cantidad = cantidad;
            _uMedida = uMedida;
            _valorUnitario = valorUnitario;
            _destino = destino;
            _cCosto = cCosto;
            _valorTotal = valorTotal;
            _anulado = anulado;
            _registro = registro;
            _oc = oc;
            this.nota = nota;
            if (producto.Trim().Length == 0)
                _detalle = detalle;
            else
                _detalle = item.RetornaDescripcion(_producto, empresa);
        }

        public CtransaccionAlmacen(decimal cantidad, decimal valorTotal)
        {
            _cantidad = cantidad;
            _valorTotal = valorTotal;
        }

        public CtransaccionAlmacen(string papeleta, string producto, decimal cantidad)
        {
            _papeleta = papeleta;
            _cantidad = cantidad;
            _producto = producto;
        }

        public List<CtransaccionAlmacen> TotalizaTransaccion(List<CtransaccionAlmacen> transaccionAlmacen)
        {
            List<CtransaccionAlmacen> listaTotal = new List<CtransaccionAlmacen>();

            decimal cantidadTotal = 0, valorTotal = 0;
            int i = 0;
            foreach (object registro in transaccionAlmacen.ToArray())
            {
                cantidadTotal += transaccionAlmacen[i].Cantidad;
                valorTotal += transaccionAlmacen[i].ValorTotal;
                i++;
            }
            CtransaccionAlmacen total = new CtransaccionAlmacen(cantidadTotal, valorTotal);
            listaTotal.Add(total);
            return listaTotal;
        }



    }
}