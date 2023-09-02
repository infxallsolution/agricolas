using Nomina.WebForms.App_Code.Models.Payroll.DTO.Enum.PayrollElectronic.AL;
using Nomina.WebForms.App_Code.Models.Payroll.DTO.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Services.Interfaces.ElectronicPayroll
{
    public interface IMethodByEntity
    {
        bool DeleteByEntityID(int company, Entities entity, Guid Id);
        bool Save(CreateEquivalenceInput input);
    }
}