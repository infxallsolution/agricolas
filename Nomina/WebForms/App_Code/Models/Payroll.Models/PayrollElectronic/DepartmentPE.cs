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
    [Table("gDepartamentoNE")]
    public class DepartmentPE : Entity<Guid>
    {
        [StringLength(50)]
        public string DepartmentId { get; set; }
        [StringLength(50)]
        public string CountryId { get; set; }
        public int Company { get; set; }
        public string EquivalenceAL { get; set; }

        [ForeignKey("Company,CountryId, DepartmentId")]
        public Department Department { get; set; }
    }
}