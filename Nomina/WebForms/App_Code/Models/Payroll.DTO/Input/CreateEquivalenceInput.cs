using Nomina.WebForms.App_Code.Models.Payroll.DTO.Enum.PayrollElectronic.AL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Models.Payroll.DTO.Input
{
    public class CreateEquivalenceInput
    {
        public int Company { get; set; }
        public string Id { get; set; }
        public string Value { get; set; }
        public Entities Entity { get; set; }
    }
}