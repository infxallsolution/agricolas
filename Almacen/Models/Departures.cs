using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Almacen.Models
{
    public class Departures
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public double Amount { get; set; }
        public double Unitvalue { get; set; }
        public double Totalvalue { get; set; }
        public string LedgerAccount { get; set; }
        public string CostCenter { get; set; }
    }
}