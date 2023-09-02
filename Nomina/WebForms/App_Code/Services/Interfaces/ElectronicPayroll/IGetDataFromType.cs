using Nomina.WebForms.App_Code.Models.Payroll.DTO.Enum.PayrollElectronic.AL;
using Nomina.WebForms.App_Code.Models.Payroll.DTO.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Services.Interfaces.ElectronicPayroll
{
    public interface IGetDataFromType
    {
        List<GeneralCodeDescriptionOutput> GetDataFromType(int company, Entities entity);
        List<GeneralCodeDescriptionOutput> GetConcept(int company);

    }
}