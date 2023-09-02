using Nomina.WebForms.App_Code.Context.Context;
using Nomina.WebForms.App_Code.Models.Payroll.DTO.Enum.PayrollElectronic.AL;
using Nomina.WebForms.App_Code.Models.Payroll.DTO.Input;
using Nomina.WebForms.App_Code.Models.Payroll.DTO.Output;
using Nomina.WebForms.App_Code.Services.Interfaces.ElectronicPayroll;
using Nomina.WebForms.Repository.RepositoryExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Services.Implementation.ElectronicPayroll
{
    public class PayRollService : IPayRollService
    {
        public ApplicationContext _context;
        public IGetDataFromType _serviceGetDataFromType;
        public ISearchByEntity _serviceSearchByEntity;
        public IMethodByEntity _serviceMethodByEntity;
        public IGetDataFromTypeByID _serviceGetDataFromTypeByID;
        public IServiceEquivalenceType _serviceEquivalenceType;
        public PayRollService()
        {
            if (_context == null) _context = new ApplicationContext();
            if (_serviceGetDataFromType == null) _serviceGetDataFromType = new GetDataFromType(_context);
            if (_serviceSearchByEntity == null) _serviceSearchByEntity = new SearchByEntity(_context);
            if (_serviceMethodByEntity == null) _serviceMethodByEntity = new MethodByEntity(_context);
            if (_serviceGetDataFromTypeByID == null) _serviceGetDataFromTypeByID = new GetDataFromTypeByID(_context);
            if (_serviceEquivalenceType == null) _serviceEquivalenceType = new ServiceEquivalenceType();
        }


        /// <summary>
        /// Method search by entity
        /// </summary>
        /// <param name="company"></param>
        /// <param name="entity"></param>
        /// <returns></returns>

        public List<GetEntitiesEquivalencesOutput> SearchByEntity(int company, Entities entity)
        {
            return _serviceSearchByEntity.Search(company, entity);
        }


        /// <summary>
        /// Method delete equivalences
        /// </summary>
        /// <param name="company"></param>
        /// <param name="entity"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DeleteByEntityID(int company, Entities entity, Guid Id)
        {
            return _serviceMethodByEntity.DeleteByEntityID(company, entity, Id);
        }

        /// <summary>
        /// Method for get data from entity type
        /// </summary>
        /// <param name="company"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<GeneralCodeDescriptionOutput> GetDataFromType(int company, Entities entity)
        {
            return _serviceGetDataFromType.GetDataFromType(company, entity);
        }
        /// <summary>
        /// Method for get data from entity type but send the ID of entity
        /// </summary>
        /// <param name="company"></param>
        /// <param name="entity"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string GetDataFromTypeById(int company, Entities entity, string Id)
        {
            return _serviceGetDataFromTypeByID.GetDataFromTypeByIdMethod(company, entity, Id);
        }

        /// <summary>
        /// Get values from type entity
        /// </summary>
        /// <param name="company"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<GeneralCodeDescriptionOutput> GetValuesFromType(int company, Entities entity)
        {
            return GetValuesFromEquivalence(entity: entity)?.OrderBy(x => x.Descripcion).ToList();
        }

        /// <summary>
        /// get equivalences values from equivalence type
        /// </summary>
        /// <param name="typeEquivalences"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<GeneralCodeDescriptionOutput> GetValuesFromEquivalence(EnumTypeEquivalences typeEquivalences = EnumTypeEquivalences.LineContributor, Entities entity = Entities.City)
        {
            if (entity == Entities.Deparment || entity == Entities.Country)
            {
                return _context.ValueFromEquivalence.Where(x => x.Entity == entity && x.Type == typeEquivalences).Select(x => new GeneralCodeDescriptionOutput()
                {
                    Codigo = x.ExtraKey,
                    Descripcion = x.Value,
                }).ToList();
            }
            else
                return _context.ValueFromEquivalence.Where(x => x.Entity == entity && x.Type == typeEquivalences).Select(x => new GeneralCodeDescriptionOutput()
                {
                    Codigo = x.Key,
                    Descripcion = x.Value,
                }).ToList();
        }

        /// <summary>
        /// Get entities from enum
        /// </summary>
        /// <returns></returns>
        public List<EntityOutput> GetEntities()
        {
            return ((Entities[])Enum.GetValues(typeof(Entities))).Select(x => new EntityOutput() { Id = (int)x, Name = EnumExtension.GetEnumDisplayName(x) }).ToList();
        }

        public List<EntityOutput> GetTypeEquivalences()
        {
            return ((EnumTypeEquivalences[])Enum.GetValues(typeof(EnumTypeEquivalences))).Select(x => new EntityOutput() { Id = (int)x, Name = EnumExtension.GetEnumDisplayName(x) }).ToList();
        }

        /// <summary>
        /// Save data by type of entity
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Save(CreateEquivalenceInput input)
        {
            return _serviceMethodByEntity.Save(input);
        }

        public List<EntityOutput> GetDataByEquivalenceType(EnumTypeEquivalences typeEquivalences)
        {
            return _serviceEquivalenceType.GetConceptsFromEquivalence(typeEquivalences);
        }

    }
}