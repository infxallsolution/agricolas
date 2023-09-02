using Nomina.WebForms.App_Code.Models.Payroll.DTO.Input;
using Nomina.WebForms.App_Code.Models.Payroll.DTO.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Nomina.WebForms.App_Code.Services.Interfaces.ElectronicPayroll
{
    public interface IServicePayrollConceptsEquivalence
    {
        bool Save(CreateEquivalenceConceptInput input);
        bool Delete(Guid Id);
        List<GroupEquivalenceTableOutput> Get(int company);
        GroupEquivalenceTableOutput GetById(Guid Id);

        List<GeneralCodeDescriptionOutput> GetConcept(int company);
        Task<HttpResponseMessage> GenerateExcel(GenerateExcelInput input);
    }
}