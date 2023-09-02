using Nomina.WebForms.App_Code.Models.Payroll.DTO.Enum.PayrollElectronic.AL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Models.Payroll.DTO.Input
{
    public class CreateEquivalenceConceptInput
    {
        public EnumTypeEquivalences TypeEquivalence { get; set; }
        public EnumConfigurationConcepts EquivalenceConcept { get; set; }
        public int Company { get; set; }

        public List<CreateEquivalenceDetailInput> GroupConceptsDetail { get; set; }
    }
}