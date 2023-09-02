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
    [Table("nTipoCotizanteNE")]
    public class TypeContributorPE : Entity<Guid>
    {
        [StringLength(50)]
        public string IdTypeContributor { get; set; }
        public int Company { get; set; }
        public string EquivalenceAL { get; set; }
        [ForeignKey("Company, IdTypeContributor")]
        public TypeContributor TypeContributor { get; set; }
    }
}