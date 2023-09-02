using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Models.Payroll.DTO.Input
{
    public class GenerateExcelInput
    {
        [System.ComponentModel.DataAnnotations.Required]
        public Enum.PayrollElectronic.CreateExcelOptionType Type { get; set; }
        public string CostCenter { get; set; }
        public string MajorCostCenter { get; set; }
        public string Employed { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public int Company { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public int Year { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public int Month { get; set; }
    }
}