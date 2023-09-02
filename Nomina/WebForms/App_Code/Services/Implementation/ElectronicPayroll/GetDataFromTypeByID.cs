using Nomina.WebForms.App_Code.Context.Context;
using Nomina.WebForms.App_Code.Models.Payroll.DTO.Enum.PayrollElectronic.AL;
using Nomina.WebForms.App_Code.Services.Interfaces.ElectronicPayroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Services.Implementation.ElectronicPayroll
{
    public class GetDataFromTypeByID : IGetDataFromTypeByID
    {
        ApplicationContext _context;
        public GetDataFromTypeByID(ApplicationContext context)
        {
            _context = context;
        }
        public string GetDataFromTypeByIdMethod(int company, Entities entity, string Id)
        {
            switch (entity)
            {
                case Entities.City:
                    return GetValueCityPEById(company, Id);

                case Entities.Country:
                    return GetValueCountryPEById(company, Id);

                case Entities.Deparment:
                    return GetValueDeparmentPEById(company, Id);
                case Entities.SubTypeContributibe:
                    return GetValueSubTypeContributorPEById(company, Id);

                case Entities.DocumentType:
                    return GetValueDocumentTypePEById(company, Id);

                case Entities.PayrollType:
                    return GetValuePayrollTypePEById(company, Id);

                case Entities.PayType:
                    return GetValuePayTypePEById(company, Id);

                case Entities.Concepts:
                    return GetValuePayTypePEById(company, Id);

                case Entities.Inability:
                    return GetValuePayTypePEById(company, Id);


                case Entities.Licences:
                    return GetValueLicencePEById(company, Id);

                case Entities.ContractType:
                    return GetValueLicencePEById(company, Id);

                default:
                    return null;
            }

        }

        public string GetValueContractTypePEById(int company, string Id)
        {
            var city = _context.ContractTypePE.FirstOrDefault(x => x.Company == company && x.ContractTypeId == Id);

            if (city == null) return null;
            else return city.EquivalenceAL;
        }

        public string GetValueInhabilityPEById(int company, string Id)
        {
            var city = _context.InabilityTypePE.FirstOrDefault(x => x.Company == company && x.InabilityTypeId == Id);

            if (city == null) return null;
            else return city.EquivalenceAL;
        }

        public string GetValueLicencePEById(int company, string Id)
        {
            var city = _context.LicenceTypePE.FirstOrDefault(x => x.Company == company && x.LicenceTypeId == Id);

            if (city == null) return null;
            else return city.EquivalenceAL;
        }

        public string GetValueConceptPEById(int company, string Id)
        {
            var city = _context.ConceptPE.FirstOrDefault(x => x.Company == company && x.ConceptId == Id);

            if (city == null) return null;
            else return city.EquivalenceAL;
        }

        public string GetValueCityPEById(int company, string Id)
        {
            var city = _context.CityPE.FirstOrDefault(x => x.Company == company && x.CityId == Id);

            if (city == null) return null;
            else return city.EquivalenceAL;
        }
        public string GetValueCountryPEById(int company, string Id)
        {
            var country = _context.CountryPE.FirstOrDefault(x => x.Company == company && x.CountryId == Id);

            if (country == null) return null;
            else return country.EquivalenceAL;
        }
        public string GetValueSubTypeContributorPEById(int company, string Id)
        {
            var subTypeContributor = _context.SubTypeContributorPE.FirstOrDefault(x => x.Company == company && x.IdSubTypeContributor == Id);

            if (subTypeContributor == null) return null;
            else return subTypeContributor.EquivalenceAL;
        }
        public string GetValueDeparmentPEById(int company, string Id)
        {
            var department = _context.DepartmentPE.FirstOrDefault(x => x.Company == company && x.DepartmentId == Id);
            if (department == null) return null;
            else return department.EquivalenceAL;
        }
        public string GetValueDocumentTypePEById(int company, string Id)
        {
            var subTypeContributor = _context.DocumentTypePE.FirstOrDefault(x => x.Company == company && x.DocumentId == Id);
            if (subTypeContributor == null) return null;
            else return subTypeContributor.EquivalenceAL;
        }
        private string GetValuePayrollTypePEById(int company, string Id)
        {
            var payrollType = _context.PayrollTypePE.FirstOrDefault(x => x.Company == company && x.PayrollTypeId == Id);
            if (payrollType == null) return null;
            else return payrollType.EquivalenceAL;
        }
        private string GetValuePayTypePEById(int company, string Id)
        {
            var payrollType = _context.PayTypePE.FirstOrDefault(x => x.Company == company && x.PayTypeId == Id);

            if (payrollType == null) return null;
            else return payrollType.EquivalenceAL;
        }
    }
}