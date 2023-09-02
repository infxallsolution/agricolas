using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Models.Payroll.Models.Parameters
{
    [Table("gTipoDocumento")]
    public class DocumentType
    {
        [Key, Column(Order = 0)]
        public int empresa { get; set; }

        [Key, Column(Order = 1, TypeName = "VARCHAR")]
        [StringLength(50)]
        public string codigo { get; set; }
        [StringLength(250)]
        public string descripcion { get; set; }
        [StringLength(50)]
        public string descripcionCorta { get; set; }
        public int codigoTD { get; set; }
        public bool mNit { get; set; }
        [StringLength(50)]
        public string equivalencia { get; set; }
    }
}