using Nomina.WebForms.App_Code.Models.Payroll.Models.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Nomina.WebForms.Repository.RepositoryExtension;

namespace Nomina.WebForms.App_Code.Models.Payroll.Models.PayrollElectronic
{
    [Table("nClaseContratoPE")]
    public class TypeContractPE : Entity<Guid>
    {
        public int Company { get; set; }
        [StringLength(50)]
        public string TypeContractId { get; set; }
        public string EquivalenceAL { get; set; }

        [ForeignKey("Company,TypeContractId")]
        public TypeContract TypeContract { get; set; }
    }
}