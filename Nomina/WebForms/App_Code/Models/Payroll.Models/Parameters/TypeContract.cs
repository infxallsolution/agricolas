using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Models.Payroll.Models.Parameters
{
    [Table("nClaseContrato")]
    public class TypeContract
    {
        [Key, Column(Order = 0)]
        public int empresa { get; set; }

        [Key, Column(Order = 1, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public bool terminoFijo { get; set; }
        public bool activo { get; set; }
        public bool electivaProduccion { get; set; }
        public bool porcentaje { get; set; }
        public bool porcentajeSS { get; set; }
    }
}