using Nomina.WebForms.App_Code.Models.Payroll.DTO.Enum.PayrollElectronic.AL;
using Nomina.WebForms.Repository.RepositoryExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Models.Payroll.DTO.Output
{
    public class GroupEquivalenceTableOutput
    {
        public Guid Id { get; set; }
        public EnumConfigurationConcepts EquivalenceConcept { get; set; }
        public EnumTypeEquivalences TypeEquivalence { get; set; }
        public string NameEquivalence { get { return EquivalenceConcept.ToString(); } }
        public string NameTypeEquivalence { get { return EnumExtension.GetEnumDisplayName(TypeEquivalence); } }

        public List<GroupConceptDetailOutput> GroupConceptsDetail { get; set; }
    }
}