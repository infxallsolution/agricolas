using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Models.Payroll.Models.Parameters
{
    [Table("nSubTipoCotizante")]
    public class SubTypeContributor
    {
        [Key, Column(Order = 0)]
        public int empresa { get; set; }



        [Key, Column(Order = 1, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string codigo { get; set; }

        public string tipoCotizante { get; set; }

        public string descripcion { get; set; }
        public string observacion { get; set; }
        public bool activo { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string usuario { get; set; }
    }
}