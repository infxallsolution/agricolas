using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agronomico.WebForms.App_Code.Transaccion
{
    public class Csubtotal
    {
        public string novedades { get; set; }
        public string nombreNovedades { get; set; }
        public decimal subCantidad { get; set; }
        public decimal subRacimo { get; set; }
        public decimal subJornal { get; set; }
        public Csubtotal() { }
        public Csubtotal(string novedades, string nombreNovedades, decimal subCantidad, decimal subRacimo, decimal subJornal) { this.novedades = novedades; this.nombreNovedades = nombreNovedades; this.subCantidad = subCantidad; this.subRacimo = subRacimo; this.subJornal = subJornal; }
    }
}