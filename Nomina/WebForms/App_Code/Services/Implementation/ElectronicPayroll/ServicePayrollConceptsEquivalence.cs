using Newtonsoft.Json;
using Nomina.WebForms.App_Code.Context.Context;
using Nomina.WebForms.App_Code.Models.Payroll.DTO.Input;
using Nomina.WebForms.App_Code.Models.Payroll.DTO.Output;
using Nomina.WebForms.App_Code.Services.Interfaces.ElectronicPayroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;

namespace Nomina.WebForms.App_Code.Services.Implementation.ElectronicPayroll
{
    public class ServicePayrollConceptsEquivalence : IServicePayrollConceptsEquivalence
    {
        public ApplicationContext _context;
        public IGetDataFromType _getDataFromType;

        public ServicePayrollConceptsEquivalence()
        {
            if (_context == null) _context = new ApplicationContext();
            if (_getDataFromType == null) _getDataFromType = new GetDataFromType(_context);

        }

        public bool Delete(Guid Id)
        {
            var detail = _context.GroupConceptsDetailPE.FirstOrDefault(x => x.GroupConceptId == Id);
            if (detail == null) return false;
            _context.GroupConceptsDetailPE.Remove(detail);

            var group = _context.GroupConceptsPE.FirstOrDefault(x => x.Id == Id);
            if (group == null) return false;
            _context.GroupConceptsPE.Remove(group);
            _context.SaveChanges();

            return true;
        }

        public async Task<HttpResponseMessage> GenerateExcel(GenerateExcelInput input)
        {
            using (var client = new HttpClient())
            {

                var json = JsonConvert.SerializeObject(input);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var url = "http://localhost:5002/api/excel";

                var response = await client.PostAsync(url, data);

                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<GroupEquivalenceTableOutput> Get(int company)
        {

            return _context.GroupConceptsPE.Where(x => x.Company == company).Select(x => new GroupEquivalenceTableOutput()
            {
                EquivalenceConcept = x.EquivalenceConcept,
                TypeEquivalence = x.TypeEquivalence,
                Id = x.Id,
            }).ToList();

        }

        public GroupEquivalenceTableOutput GetById(Guid Id)
        {
            var equivalence = _context.GroupConceptsPE.Include(x => x.GroupConceptsDetailPE).FirstOrDefault(x => x.Id == Id);


            return new GroupEquivalenceTableOutput()
            {
                EquivalenceConcept = equivalence.EquivalenceConcept,
                GroupConceptsDetail = equivalence.GroupConceptsDetailPE.Select(x => new GroupConceptDetailOutput()
                {
                    ConceptId = x.ConceptId,
                }).ToList(),
                TypeEquivalence = equivalence.TypeEquivalence
            };
        }

        public List<GeneralCodeDescriptionOutput> GetConcept(int company)
        {
            return _getDataFromType.GetConcept(company);
        }

        public bool Save(CreateEquivalenceConceptInput input)
        {
            var detailresultbool = true;

            var dataExists = _context.GroupConceptsPE.FirstOrDefault(x => x.EquivalenceConcept == input.EquivalenceConcept
             && x.Company == input.Company && x.TypeEquivalence == input.TypeEquivalence
            );

            if (dataExists == null)
            {

                var result = _context.GroupConceptsPE.Add(new Models.Payroll.Models.PayrollElectronic.GroupConceptPE()
                {
                    Company = input.Company,
                    EquivalenceConcept = input.EquivalenceConcept,
                    TypeEquivalence = input.TypeEquivalence
                });

                if (result == null) return false;

                foreach (var i in input.GroupConceptsDetail)
                {
                    var detailResult = _context.GroupConceptsDetailPE.Add(new Models.Payroll.Models.PayrollElectronic.GroupConceptDetailPE()
                    {
                        Company = input.Company,
                        ConceptId = i.ConceptId,
                        GroupConceptId = result.Id
                    });

                    if (detailResult == null)
                    {
                        detailresultbool = false;
                    }
                }

                if (detailresultbool)
                    _context.SaveChanges();
            }
            else
            {
                dataExists.TypeEquivalence = input.TypeEquivalence;
                dataExists.EquivalenceConcept = input.EquivalenceConcept;
                var existingGroupDetailPe = _context.GroupConceptsDetailPE.Where(x => x.GroupConceptId == dataExists.Id).ToList();

                _context.GroupConceptsDetailPE.RemoveRange(existingGroupDetailPe);

                foreach (var i in input.GroupConceptsDetail)
                {
                    var detailResult = _context.GroupConceptsDetailPE.Add(new Models.Payroll.Models.PayrollElectronic.GroupConceptDetailPE()
                    {
                        Company = input.Company,
                        ConceptId = i.ConceptId,
                        GroupConceptId = dataExists.Id
                    });

                    if (detailResult == null)
                    {
                        detailresultbool = false;
                    }
                }

                if (detailresultbool)
                    _context.SaveChanges();

            }
            return true;

        }

        //async Task<HttpResponseMessage> IServicePayrollConceptsEquivalence.GenerateExcel(GenerateExcelInput input)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("http://localhost:5002");
        //        client.DefaultRequestHeaders.Add("Content-Type", "application/json;charset=UTF-8");

        //        var url = "repos/symfony/symfony/contributors";
        //        HttpResponseMessage response = await client.GetAsync(url);
        //        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            return response;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}
    }
}