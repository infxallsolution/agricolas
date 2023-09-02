using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Models.Payroll.DTO.Output
{
    public class GetEntitiesEquivalencesOutput
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Equivalence { get; set; }

    }
}