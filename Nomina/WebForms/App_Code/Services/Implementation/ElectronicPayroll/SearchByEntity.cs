using Nomina.WebForms.App_Code.Context.Context;
using Nomina.WebForms.App_Code.Models.Payroll.DTO.Enum.PayrollElectronic.AL;
using Nomina.WebForms.App_Code.Models.Payroll.DTO.Output;
using Nomina.WebForms.App_Code.Services.Interfaces.ElectronicPayroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Nomina.WebForms.App_Code.Services.Implementation.ElectronicPayroll
{
    public class SearchByEntity : ISearchByEntity
    {
        ApplicationContext _context;
        public SearchByEntity(ApplicationContext context)
        {
            _context = context;
        }
        public List<GetEntitiesEquivalencesOutput> Search(int company, Entities entity)
        {
            switch (entity)
            {
                case Entities.City:
                    return _context.CityPE.Include(x => x.City).Where(x => x.Company == company).Select(x => new GetEntitiesEquivalencesOutput() { Id = x.Id, Code = x.CityId, Name = x.City.descripcion, Equivalence = x.EquivalenceAL }).ToList();

                case Entities.Country:
                    return _context.CountryPE.Include(x => x.Country).Where(x => x.Company == company).Select(x => new GetEntitiesEquivalencesOutput() { Id = x.Id, Code = x.CountryId, Name = x.Country.descripcion, Equivalence = x.EquivalenceAL }).ToList();

                case Entities.Deparment:
                    return _context.DepartmentPE.Include(x => x.Department).Where(x => x.Company == company).Select(x => new GetEntitiesEquivalencesOutput() { Id = x.Id, Code = x.DepartmentId, Name = x.Department.descripcion, Equivalence = x.EquivalenceAL }).ToList();

                case Entities.SubTypeContributibe:
                    return _context.TypeContributorPE.Include(x => x.TypeContributor).Where(x => x.Company == company).Select(x => new GetEntitiesEquivalencesOutput() { Id = x.Id, Code = x.IdTypeContributor, Name = x.TypeContributor.descripcion, Equivalence = x.EquivalenceAL }).ToList();

                case Entities.DocumentType:
                    return _context.DocumentTypePE.Include(x => x.DocumentType).Where(x => x.Company == company).Select(x => new GetEntitiesEquivalencesOutput() { Id = x.Id, Code = x.DocumentId, Name = x.DocumentType.descripcion, Equivalence = x.EquivalenceAL }).ToList();

                case Entities.PayrollType:
                    return _context.PayrollTypePE.Include(x => x.PayrrolType).Where(x => x.Company == company).Select(x => new GetEntitiesEquivalencesOutput() { Id = x.Id, Code = x.PayrollTypeId, Name = x.PayrrolType.descripcion, Equivalence = x.EquivalenceAL }).ToList();

                case Entities.PayType:
                    return _context.PayTypePE.Include(x => x.PayType).Where(x => x.Company == company).Select(x => new GetEntitiesEquivalencesOutput() { Id = x.Id, Code = x.PayTypeId, Name = x.PayType.descripcion, Equivalence = x.EquivalenceAL }).ToList();

                case Entities.Concepts:
                    return _context.ConceptPE.Include(x => x.Concept).Where(x => x.Company == company).Select(x => new GetEntitiesEquivalencesOutput() { Id = x.Id, Code = x.ConceptId, Name = x.Concept.descripcion, Equivalence = x.EquivalenceAL }).ToList();
                case Entities.Licences:
                    return _context.LicenceTypePE.Include(x => x.InabilityType).Where(x => x.Company == company).Select(x => new GetEntitiesEquivalencesOutput() { Id = x.Id, Code = x.LicenceTypeId, Name = x.InabilityType.descripcion, Equivalence = x.EquivalenceAL }).ToList();
                case Entities.Inability:
                    return _context.InabilityTypePE.Include(x => x.InabilityType).Where(x => x.Company == company).Select(x => new GetEntitiesEquivalencesOutput() { Id = x.Id, Code = x.InabilityTypeId, Name = x.InabilityType.descripcion, Equivalence = x.EquivalenceAL }).ToList();
                case Entities.ContractType:
                    return _context.ContractTypePE.Include(x => x.ContractType).Where(x => x.Company == company).Select(x => new GetEntitiesEquivalencesOutput() { Id = x.Id, Code = x.ContractTypeId, Name = x.ContractType.descripcion, Equivalence = x.EquivalenceAL }).ToList();

                default:
                    return null;
            }

        }
    }
}