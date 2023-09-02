using Nomina.WebForms.App_Code.Models.Payroll.DTO.Enum.PayrollElectronic.AL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Services.Interfaces.ElectronicPayroll
{
    public interface IGetDataFromTypeByID
    {
        string GetDataFromTypeByIdMethod(int company, Entities entity, string Id);
    }
}