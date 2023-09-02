using Nomina.WebForms.App_Code.Models.Payroll.DTO.Enum.PayrollElectronic.AL;
using Nomina.WebForms.App_Code.Models.Payroll.DTO.Input;
using Nomina.WebForms.App_Code.Models.Payroll.DTO.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Services.Interfaces.ElectronicPayroll
{
    public interface IPayRollService
    {
        List<GeneralCodeDescriptionOutput> GetDataFromType(int company, Entities entity);
        bool DeleteByEntityID(int company, Entities entity, Guid Id);
        List<GetEntitiesEquivalencesOutput> SearchByEntity(int company, Entities entity);
        string GetDataFromTypeById(int company, Entities entity, string Id);
        List<GeneralCodeDescriptionOutput> GetValuesFromType(int company, Entities entity);

        List<GeneralCodeDescriptionOutput> GetValuesFromEquivalence(EnumTypeEquivalences typeEquivalences = EnumTypeEquivalences.LineContributor, Entities entity = Entities.City);
        List<EntityOutput> GetEntities();
        List<EntityOutput> GetTypeEquivalences();
        bool Save(CreateEquivalenceInput input);
        List<EntityOutput> GetDataByEquivalenceType(EnumTypeEquivalences typeEquivalences);
    }
}