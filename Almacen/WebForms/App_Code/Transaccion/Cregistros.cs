using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almacen.WebForms.App_Code.Transaccion
{
    public class Cregistros
    {
        public Cregistros()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public int registro { get; set; }

        public string idItem { get; set; }
        public string nombreItem { get; set; }
        public string idBodega { get; set; }
        public string nombreBodega { get; set; }
        public string idDestino { get; set; }
        public string nombreDestino { get; set; }
        public string idCcosto { get; set; }
        public string nombreCcosto { get; set; }
        public string tipoReferencia { get; set; }
        public string numeroReferencia { get; set; }
        public string observacion { get; set; }
        public string detalle { get; set; }
        public string nota { get; set; }
        public string idTercero { get; set; }
        public string nombreTercero { get; set; }
        public string idUmedida { get; set; }
        public string nombreUmedida { get; set; }
        public string idImpuesto { get; set; }
        public string nombreImpuesto { get; set; }

        public decimal cantidad { get; set; }
        public decimal valorUnitario { get; set; }
        public decimal valorTotal { get; set; }
        public decimal valorBruto { get; set; }
        public decimal valorDescuento { get; set; }
        public decimal valorUltimaCompra { get; set; }
        public decimal pDescuento { get; set; }
        public decimal pImpuesto { get; set; }
        public decimal valorImpuesto { get; set; }
        public decimal saldo { get; set; }

        public bool check { get; set; }
        public bool anulado { get; set; }
        public bool oc { get; set; }

    }
}