using Nomina.WebForms.App_Code.Context.Context;
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
    public class GetDataFromType : IGetDataFromType
    {
        ApplicationContext _context;
        public GetDataFromType(ApplicationContext context)
        {
            _context = context;
        }
        List<GeneralCodeDescriptionOutput> IGetDataFromType.GetDataFromType(int company, Entities entity)
        {
            switch (entity)
            {
                case Entities.City:
                    return GetCities(company)?.OrderBy(x => x.Descripcion).ToList();

                case Entities.Country:
                    return GetCountries(company)?.OrderBy(x => x.Descripcion).ToList();

                case Entities.Deparment:
                    return GetDeparments(company)?.OrderBy(x => x.Descripcion).ToList();
                case Entities.SubTypeContributibe:
                    return GetSubTypeContribute(company)?.OrderBy(x => x.Descripcion).ToList();

                case Entities.DocumentType:
                    return GetDocumentType(company)?.OrderBy(x => x.Descripcion).ToList();

                case Entities.PayrollType:
                    return GetPayrollType(company)?.OrderBy(x => x.Descripcion).ToList();

                case Entities.PayType:
                    return GetPayType(company)?.OrderBy(x => x.Descripcion).ToList();

                case Entities.Concepts:
                    return GetConcept(company)?.OrderBy(x => x.Descripcion).ToList();
                case Entities.Inability:
                    return GetInability(company)?.OrderBy(x => x.Descripcion).ToList();
                case Entities.Licences:
                    return GetInability(company, false)?.OrderBy(x => x.Descripcion).ToList();
                case Entities.ContractType:
                    return GetContractType(company)?.OrderBy(x => x.Descripcion).ToList();

                default:
                    return null;
            }
        }

        public List<GeneralCodeDescriptionOutput> GetContractType(int company)
        {
            var result = _context.ContractType
                        .Where(x => x.empresa == company)
                        .Select(x => new GeneralCodeDescriptionOutput()
                        {
                            Codigo = x.codigo,
                            Descripcion = x.descripcion,
                        }).ToList();

            return result;
            ;
        }
        public List<GeneralCodeDescriptionOutput> GetInability(int company, bool inability = true)
        {
            var result = _context.InabilityType.WhereIf(inability, x => x.afectaNovedadSS != "LMA")
                        .Where(x => x.empresa == company)
                        .Select(x => new GeneralCodeDescriptionOutput()
                        {
                            Codigo = x.codigo,
                            Descripcion = x.descripcion,
                        }).ToList();

            return result;
            ;
        }

        public List<GeneralCodeDescriptionOutput> GetCountries(int company)
        {
            return _context.Country.Where(x => x.empresa == company).Select(x => new GeneralCodeDescriptionOutput()
            {
                Codigo = x.codigo,
                Descripcion = x.descripcion,
            }).ToList();
        }

        public List<GeneralCodeDescriptionOutput> GetCities(int company)
        {
            return _context.City.Where(x => x.empresa == company).Select(x => new GeneralCodeDescriptionOutput()
            {
                Codigo = x.codigo,
                Descripcion = x.descripcion + " - " + _context.Deparment.FirstOrDefault(w => w.codigo == x.departamento && w.empresa == company).descripcion
            }).ToList();
        }
        public List<GeneralCodeDescriptionOutput> GetDeparments(int company)
        {
            return _context.Deparment.Where(x => x.empresa == company).Select(x => new GeneralCodeDescriptionOutput()
            {
                Codigo = x.codigo,
                Descripcion = x.descripcion + " - " + _context.Country.FirstOrDefault(w => w.codigo == x.pais && w.empresa == company).descripcion,
            }).ToList();
        }
        public List<GeneralCodeDescriptionOutput> GetSubTypeContribute(int company)
        {
            return _context.TypeContributor.Where(x => x.empresa == company).Select(x => new GeneralCodeDescriptionOutput()
            {
                Codigo = x.codigo,
                Descripcion = x.descripcion,
            }).ToList();
        }

        public List<GeneralCodeDescriptionOutput> GetDocumentType(int company)
        {
            return _context.DocumentType.Where(x => x.empresa == company).Select(x => new GeneralCodeDescriptionOutput()
            {
                Codigo = x.codigo,
                Descripcion = x.descripcion,
            }).ToList();
        }

        private List<GeneralCodeDescriptionOutput> GetPayrollType(int company)
        {
            return _context.PayrollType.Where(x => x.empresa == company).Select(x => new GeneralCodeDescriptionOutput()
            {
                Codigo = x.codigo,
                Descripcion = x.descripcion,
            }).ToList();
        }

        private List<GeneralCodeDescriptionOutput> GetPayType(int company)
        {
            return _context.PayType.Where(x => x.empresa == company).Select(x => new GeneralCodeDescriptionOutput()
            {
                Codigo = x.codigo,
                Descripcion = x.descripcion,
            }).ToList();
        }

        public List<GeneralCodeDescriptionOutput> GetConcept(int company)
        {
            return _context.Concept.Where(x => x.empresa == company && x.activo).Select(x => new GeneralCodeDescriptionOutput()
            {
                Codigo = x.codigo,
                Descripcion = x.descripcion,
            }).ToList();
        }


    }
}