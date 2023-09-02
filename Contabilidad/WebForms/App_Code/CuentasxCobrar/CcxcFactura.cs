using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contabilidad.WebForms.App_Code.CuentasxCobrar
{
    public class CcxcFactura
    {
        public CcxcFactura()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        #region Atributos

        private string _cuenta;
        private decimal _debito;
        private decimal _credito;

        public string Cuenta
        {
            get { return _cuenta; }
            set { _cuenta = value; }
        }
        public decimal Debito
        {
            get { return _debito; }
            set { _debito = value; }
        }
        public decimal Credito
        {
            get { return _credito; }
            set { _credito = value; }
        }

        #endregion Atributos



        public CcxcFactura(string cuenta, decimal debito, decimal credito)
        {
            _cuenta = cuenta;
            _debito = debito;
            _credito = credito;
        }

        //public List<CcxcFactura> TotalizaTransaccion(List<Cfacturas> transaccionAlmacen)
        //{
        //    List<CcxcFactura> listaTotal = new List<CcxcFactura>();

        //    decimal cantidadTotal = 0, valorTotal = 0;
        //    int i = 0;
        //    foreach (object registro in transaccionAlmacen.ToArray())
        //    {
        //        cantidadTotal += transaccionAlmacen[i].Cantidad;
        //        valorTotal += transaccionAlmacen[i].ValorTotal;
        //        i++;
        //    }
        //    CcxcFactura total = new CcxcFactura(cantidadTotal, valorTotal);
        //    listaTotal.Add(total);
        //    return listaTotal;
        //}

    }
}