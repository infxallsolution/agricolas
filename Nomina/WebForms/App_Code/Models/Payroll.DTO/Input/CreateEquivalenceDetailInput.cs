using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Models.Payroll.DTO.Input
{
    public class CreateEquivalenceDetailInput
    {
        public string ConceptId { get; set; }
        public Guid GroupConceptId { get; set; }
    }
}