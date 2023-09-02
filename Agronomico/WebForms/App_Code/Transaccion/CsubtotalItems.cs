using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Transaccion
{
    public class CsubtotalItems
    {
        private string lote;

        public string Lote
        {
            get { return lote; }
            set { lote = value; }
        }
        private decimal cantidad;

        public decimal Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        private string item;

        public string Item
        {
            get { return item; }
            set { item = value; }
        }

        public CsubtotalItems() { }
        public CsubtotalItems(string lote, decimal cantidad, string item)
        {
            this.cantidad = cantidad;
            this.lote = lote;
            this.item = item;
        }
    }
}