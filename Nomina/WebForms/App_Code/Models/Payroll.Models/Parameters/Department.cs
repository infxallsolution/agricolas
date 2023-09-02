using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Models.Payroll.Models.Parameters
{
    [Table("gDepartamento")]
    public class Department
    {
        [Key, Column(Order = 0)]
        public int empresa { get; set; }
        [StringLength(50)]
        [Key, Column(Order = 1, TypeName = "VARCHAR")]
        public string pais { get; set; }
        [StringLength(50)]
        [Key, Column(Order = 2, TypeName = "VARCHAR")]
        public string codigo { get; set; }

        public string descripcion { get; set; }
    }
}