using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Almacen.Models
{
    public class vsalidas
    {
        [Key]
        public int registro { get; set; }
        public DateTime fecha { get; set; }
        public string numero { get; set; }
        public string cadenaItems { get; set; }
        public double cantidad { get; set; }
        public double valorUnitario { get; set; }
        public double valorTotal { get; set; }
    }
}