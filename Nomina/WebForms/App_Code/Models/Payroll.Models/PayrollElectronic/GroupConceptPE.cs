using Nomina.WebForms.App_Code.Models.Payroll.DTO.Enum.PayrollElectronic.AL;
using Nomina.WebForms.Repository.RepositoryExtension;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Models.Payroll.Models.PayrollElectronic
{
    [Table("nGrupoConceptoNE")]
    public class GroupConceptPE : Entity<Guid>
    {
        public EnumTypeEquivalences TypeEquivalence { get; set; }
        public EnumConfigurationConcepts EquivalenceConcept { get; set; }
        public int Company { get; set; }

        public List<GroupConceptDetailPE> GroupConceptsDetailPE { get; set; }
    }
}