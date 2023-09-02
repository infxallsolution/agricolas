using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.CuentasxCobrar
{
    public class Cfacturas
    {
        public Cfacturas()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        #region Instancias

        Citems item = new Citems();

        #endregion Instancias

        #region Atributos

        private string _bodega;
        private string _producto;
        private decimal _cantidad;
        private string _uMedida;
        private decimal _valorUnitario;
        private string _destino;
        private string _cCosto;
        private decimal _valorTotal;
        private string _detalle;
        private string _papeleta;
        private int _registro;
        private bool _anulado;

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

        #endregion Atributos



        public Cfacturas(string bodega, string producto, decimal cantidad, string uMedida, decimal valorUnitario, string destino, string cCosto, decimal valorTotal, string detalle, int registro, int empresa, bool anulado)
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

            if (producto.Trim().Length == 0)
                _detalle = detalle;
            else
                _detalle = item.RetornaDescripcion(_producto, empresa);
        }

        public Cfacturas(decimal cantidad, decimal valorTotal)
        {
            _cantidad = cantidad;
            _valorTotal = valorTotal;
        }

        public Cfacturas(string papeleta, string producto, decimal cantidad)
        {
            _papeleta = papeleta;
            _cantidad = cantidad;
            _producto = producto;
        }

        public List<Cfacturas> TotalizaTransaccion(List<Cfacturas> transaccionAlmacen)
        {
            List<Cfacturas> listaTotal = new List<Cfacturas>();

            decimal cantidadTotal = 0, valorTotal = 0;
            int i = 0;
            foreach (object registro in transaccionAlmacen.ToArray())
            {
                cantidadTotal += transaccionAlmacen[i].Cantidad;
                valorTotal += transaccionAlmacen[i].ValorTotal;
                i++;
            }
            Cfacturas total = new Cfacturas(cantidadTotal, valorTotal);
            listaTotal.Add(total);
            return listaTotal;
        }

    }
}