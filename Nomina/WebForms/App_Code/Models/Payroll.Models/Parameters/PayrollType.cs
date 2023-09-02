using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Models.Payroll.Models.Parameters
{
    [Table("nTipoNomina")]
    public class PayrollType
    {
        [Key, Column(Order = 0)]
        public int empresa { get; set; }

        [Key, Column(Order = 1, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string codigo { get; set; }

        [StringLength(550)]
        [Column(TypeName = "VARCHAR")]
        public string descripcion { get; set; }
        [StringLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string periocidad { get; set; }
        [StringLength(5550)]
        [Column(TypeName = "VARCHAR")]
        public string observacion { get; set; }
        [StringLength(50)]
        [Column(TypeName = "NCHAR")]
        public string usuario { get; set; }
        public DateTime fechaRegistro { get; set; }
    }
}