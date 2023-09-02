using Nomina.WebForms.App_Code.Models.Payroll.DTO.Enum.PayrollElectronic.AL;
using Nomina.WebForms.Repository.RepositoryExtension;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Models.Payroll.Models.PayrollElectronic
{
    [Table("gValoresPorEquivalenciaNE")]
    public class ValueFromEquivalence : Entity<Guid>
    {
        [Required]
        public EnumTypeEquivalences Type { get; set; }
        [Required]
        public Entities Entity { get; set; }
        [Required]
        public string Key { get; set; }
        public string ExtraKey { get; set; }
        [Required]
        public string Value { get; set; }
        public string Parent { get; set; }
        public Entities EntityParent { get; set; }
    }
}