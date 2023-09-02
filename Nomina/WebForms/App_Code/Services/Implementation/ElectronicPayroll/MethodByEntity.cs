using Nomina.WebForms.App_Code.Context.Context;
using Nomina.WebForms.App_Code.Models.Payroll.DTO.Enum.PayrollElectronic.AL;
using Nomina.WebForms.App_Code.Models.Payroll.DTO.Input;
using Nomina.WebForms.App_Code.Models.Payroll.Models.PayrollElectronic;
using Nomina.WebForms.App_Code.Services.Interfaces.ElectronicPayroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Services.Implementation.ElectronicPayroll
{
    public class MethodByEntity : IMethodByEntity
    {
        public ApplicationContext _context;
        public MethodByEntity(ApplicationContext context)
        {
            _context = context;
        }
        public bool DeleteByEntityID(int company, Entities entity, Guid Id)
        {
            switch (entity)
            {
                case Entities.City:
                    var city = _context.CityPE.FirstOrDefault(x => x.Id == Id);
                    if (city == null) return false;
                    _context.CityPE.Remove(city);
                    _context.SaveChanges();
                    return true;

                case Entities.Country:
                    var country = _context.CountryPE.FirstOrDefault(x => x.Id == Id);
                    if (country == null) return false;
                    _context.CountryPE.Remove(country);
                    return true;

                case Entities.Deparment:
                    var department = _context.DepartmentPE.FirstOrDefault(x => x.Id == Id);
                    if (department == null) return false;
                    _context.DepartmentPE.Remove(department);
                    _context.SaveChanges();
                    return true;
                case Entities.SubTypeContributibe:
                    var subTypeContributor = _context.TypeContributorPE.FirstOrDefault(x => x.Id == Id);
                    if (subTypeContributor == null) return false;
                    _context.TypeContributorPE.Remove(subTypeContributor);
                    _context.SaveChanges();
                    return true;

                case Entities.DocumentType:
                    var documentType = _context.DocumentTypePE.FirstOrDefault(x => x.Id == Id);
                    if (documentType == null) return false;
                    _context.DocumentTypePE.Remove(documentType);
                    _context.SaveChanges();
                    return true;

                case Entities.PayrollType:
                    var payrollType = _context.PayrollTypePE.FirstOrDefault(x => x.Id == Id);
                    if (payrollType == null) return false;
                    _context.PayrollTypePE.Remove(payrollType);
                    _context.SaveChanges();
                    return true;

                case Entities.PayType:
                    var payType = _context.PayTypePE.FirstOrDefault(x => x.Id == Id);
                    if (payType == null) return false;
                    _context.PayTypePE.Remove(payType);
                    _context.SaveChanges();
                    return true;

                case Entities.Concepts:
                    var concept = _context.ConceptPE.FirstOrDefault(x => x.Id == Id);
                    if (concept == null) return false;
                    _context.ConceptPE.Remove(concept);
                    _context.SaveChanges();
                    return true;

                case Entities.Licences:
                    var licence = _context.LicenceTypePE.FirstOrDefault(x => x.Id == Id);
                    if (licence == null) return false;
                    _context.LicenceTypePE.Remove(licence);
                    _context.SaveChanges();
                    return true;

                case Entities.Inability:
                    var inability = _context.InabilityTypePE.FirstOrDefault(x => x.Id == Id);
                    if (inability == null) return false;
                    _context.InabilityTypePE.Remove(inability);
                    _context.SaveChanges();
                    return true;

                case Entities.ContractType:
                    var contractType = _context.ContractTypePE.FirstOrDefault(x => x.Id == Id);
                    if (contractType == null) return false;
                    _context.ContractTypePE.Remove(contractType);
                    _context.SaveChanges();
                    return true;

                default:
                    return false;
            }
        }

        public bool Save(CreateEquivalenceInput input)
        {
            var result = true;
            switch (input.Entity)
            {
                case Entities.City:
                    result = SaveCity(input);
                    break;

                case Entities.Country:
                    result = SaveCountry(input);

                    break;
                case Entities.Deparment:
                    result = SaveDeparment(input);
                    break;
                case Entities.SubTypeContributibe:
                    result = SaveContributorType(input);
                    break;

                case Entities.DocumentType:
                    result = SaveDocumentType(input);
                    break;

                case Entities.PayrollType:
                    result = SavePayrollType(input);
                    break;

                case Entities.PayType:
                    result = SavePayType(input);
                    break;

                case Entities.Concepts:
                    result = SaveConcept(input);
                    break;

                case Entities.Inability:
                    result = SaveInabilityTypePE(input);
                    break;

                case Entities.Licences:
                    result = SaveLicenceTypePE(input);
                    break;

                case Entities.ContractType:
                    result = SaveContractPE(input);
                    break;


                default:
                    break;
            }

            return result;
        }

        private bool SaveContractPE(CreateEquivalenceInput input)
        {
            var exists = _context.ContractTypePE.FirstOrDefault(x => x.ContractTypeId == input.Id && x.Company == input.Company);

            if (exists != null)
            {
                exists.EquivalenceAL = input.Value;
                _context.SaveChanges();

                return true;
            }
            else
            {
                var result = _context.ContractTypePE.Add(new ContractTypePE()
                {
                    ContractTypeId = input.Id,
                    Company = input.Company,
                    EquivalenceAL = input.Value
                });

                _context.SaveChanges();

                if (result != null)
                    return true;
                else return false;
            }
        }
        private bool SaveLicenceTypePE(CreateEquivalenceInput input)
        {
            var exists = _context.LicenceTypePE.FirstOrDefault(x => x.LicenceTypeId == input.Id && x.Company == input.Company);

            if (exists != null)
            {
                exists.EquivalenceAL = input.Value;
                _context.SaveChanges();

                return true;
            }
            else
            {
                var result = _context.LicenceTypePE.Add(new LicenceTypePE()
                {
                    LicenceTypeId = input.Id,
                    Company = input.Company,
                    EquivalenceAL = input.Value
                });

                _context.SaveChanges();

                if (result != null)
                    return true;
                else return false;
            }
        }
        private bool SaveInabilityTypePE(CreateEquivalenceInput input)
        {
            var exists = _context.InabilityTypePE.FirstOrDefault(x => x.InabilityTypeId == input.Id && x.Company == input.Company);

            if (exists != null)
            {
                exists.EquivalenceAL = input.Value;
                _context.SaveChanges();

                return true;
            }
            else
            {
                var result = _context.InabilityTypePE.Add(new InabilityTypePE()
                {
                    InabilityTypeId = input.Id,
                    Company = input.Company,
                    EquivalenceAL = input.Value
                });

                _context.SaveChanges();

                if (result != null)
                    return true;
                else return false;
            }
        }

        private bool SaveConcept(CreateEquivalenceInput input)
        {
            var exists = _context.ConceptPE.FirstOrDefault(x => x.ConceptId == input.Id && x.Company == input.Company);

            if (exists != null)
            {
                exists.EquivalenceAL = input.Value;
                _context.SaveChanges();

                return true;
            }
            else
            {
                var result = _context.ConceptPE.Add(new ConceptPE()
                {
                    ConceptId = input.Id,
                    Company = input.Company,
                    EquivalenceAL = input.Value
                });

                _context.SaveChanges();

                if (result != null)
                    return true;
                else return false;
            }
        }

        private bool SavePayType(CreateEquivalenceInput input)
        {
            var exists = _context.PayTypePE.FirstOrDefault(x => x.PayTypeId == input.Id && x.Company == input.Company);

            if (exists != null)
            {
                exists.EquivalenceAL = input.Value;
                _context.SaveChanges();

                return true;
            }
            else
            {
                var result = _context.PayTypePE.Add(new PayTypePE()
                {
                    PayTypeId = input.Id,
                    Company = input.Company,
                    EquivalenceAL = input.Value
                });

                _context.SaveChanges();

                if (result != null)
                    return true;
                else return false;
            }
        }

        private bool SavePayrollType(CreateEquivalenceInput input)
        {
            var exists = _context.PayrollTypePE.FirstOrDefault(x => x.PayrollTypeId == input.Id && x.Company == input.Company);

            if (exists != null)
            {
                exists.EquivalenceAL = input.Value;
                _context.SaveChanges();

                return true;
            }
            else
            {
                var result = _context.PayrollTypePE.Add(new PayrollTypePE()
                {
                    PayrollTypeId = input.Id,
                    Company = input.Company,
                    EquivalenceAL = input.Value
                });

                _context.SaveChanges();

                if (result != null)
                    return true;
                else return false;
            }
        }

        public bool SaveCity(CreateEquivalenceInput input)
        {
            var exitsCity = _context.CityPE.FirstOrDefault(x => x.CityId == input.Id && x.Company == input.Company);

            if (exitsCity != null)
            {
                exitsCity.EquivalenceAL = input.Value;
                _context.SaveChanges();

                return true;
            }
            else
            {
                var result = _context.CityPE.Add(new CityPE()
                {
                    CityId = input.Id,
                    Company = input.Company,
                    EquivalenceAL = input.Value
                });

                _context.SaveChanges();

                if (result != null)
                    return true;
                else return false;
            }

        }

        public bool SaveCountry(CreateEquivalenceInput input)
        {
            var exitsCity = _context.CountryPE.FirstOrDefault(x => x.CountryId == input.Id && x.Company == input.Company);

            if (exitsCity != null)
            {
                exitsCity.EquivalenceAL = input.Value;
                _context.SaveChanges();

                return true;
            }
            else
            {
                var result = _context.CountryPE.Add(new CountryPE()
                {
                    CountryId = input.Id,
                    Company = input.Company,
                    EquivalenceAL = input.Value
                });

                _context.SaveChanges();

                if (result != null)
                    return true;
                else return false;
            }

        }

        public bool SaveDeparment(CreateEquivalenceInput input)
        {
            var exitsCity = _context.DepartmentPE.FirstOrDefault(x => x.DepartmentId == input.Id && x.Company == input.Company);

            if (exitsCity != null)
            {
                exitsCity.EquivalenceAL = input.Value;
                _context.SaveChanges();

                return true;
            }
            else
            {
                var result = _context.DepartmentPE.Add(new DepartmentPE()
                {
                    DepartmentId = input.Id,
                    Company = input.Company,
                    EquivalenceAL = input.Value
                });

                _context.SaveChanges();

                if (result != null)
                    return true;
                else return false;
            }

        }

        public bool SaveContributorType(CreateEquivalenceInput input)
        {
            var exits = _context.TypeContributorPE.FirstOrDefault(x => x.IdTypeContributor == input.Id && x.Company == input.Company);

            if (exits != null)
            {
                exits.EquivalenceAL = input.Value;
                _context.SaveChanges();

                return true;
            }
            else
            {
                var result = _context.TypeContributorPE.Add(new TypeContributorPE()
                {
                    IdTypeContributor = input.Id,
                    Company = input.Company,
                    EquivalenceAL = input.Value
                });

                _context.SaveChanges();

                if (result != null)
                    return true;
                else return false;
            }

        }

        public bool SaveDocumentType(CreateEquivalenceInput input)
        {
            var exits = _context.DocumentTypePE.FirstOrDefault(x => x.DocumentId == input.Id && x.Company == input.Company);

            if (exits != null)
            {
                exits.EquivalenceAL = input.Value;
                _context.SaveChanges();

                return true;
            }
            else
            {
                var result = _context.DocumentTypePE.Add(new DocumentTypePE()
                {
                    DocumentId = input.Id,
                    Company = input.Company,
                    EquivalenceAL = input.Value
                });

                _context.SaveChanges();

                if (result != null)
                    return true;
                else return false;
            }

        }

    }
}