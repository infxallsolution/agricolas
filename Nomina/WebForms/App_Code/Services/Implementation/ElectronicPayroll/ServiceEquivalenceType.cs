using Nomina.WebForms.App_Code.Models.Payroll.DTO.Enum.PayrollElectronic.AL;
using Nomina.WebForms.App_Code.Models.Payroll.DTO.Output;
using Nomina.WebForms.App_Code.Services.Interfaces.ElectronicPayroll;
using Nomina.WebForms.Repository.RepositoryExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Services.Implementation.ElectronicPayroll
{
    public class ServiceEquivalenceType : IServiceEquivalenceType
    {
        List<EntityOutput> IServiceEquivalenceType.GetConceptsFromEquivalence(EnumTypeEquivalences typeEquivalences)
        {
            switch (typeEquivalences)
            {
                case EnumTypeEquivalences.LineContributor:
                    return ((EnumConfigurationConcepts[])Enum.GetValues(typeof(EnumConfigurationConcepts))).Select(x => new EntityOutput() { Id = (int)x, Name = EnumExtension.GetEnumDisplayName(x) }).ToList();

                default:
                    return null;
            }
        }
    }
}