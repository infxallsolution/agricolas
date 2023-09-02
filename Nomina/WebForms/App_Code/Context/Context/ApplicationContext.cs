using Nomina.WebForms.App_Code.Models.Payroll.Models.Parameters;
using Nomina.WebForms.App_Code.Models.Payroll.Models.PayrollElectronic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Nomina.WebForms.App_Code.Context.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Country> Country { get; set; }

        public DbSet<Department> Deparment { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<CountryPE> CountryPE { get; set; }
        public DbSet<DepartmentPE> DepartmentPE { get; set; }
        public DbSet<CityPE> CityPE { get; set; }
        public DbSet<SubTypeContributor> SubTypeContributor { get; set; }
        public DbSet<SubTypeContributorPE> SubTypeContributorPE { get; set; }
        public DbSet<ValueFromEquivalence> ValueFromEquivalence { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }
        public DbSet<DocumentTypePE> DocumentTypePE { get; set; }
        public DbSet<PayrollType> PayrollType { get; set; }
        public DbSet<PayrollTypePE> PayrollTypePE { get; set; }
        public DbSet<PayType> PayType { get; set; }
        public DbSet<PayTypePE> PayTypePE { get; set; }
        public DbSet<Concept> Concept { get; set; }
        public DbSet<ConceptPE> ConceptPE { get; set; }
        public DbSet<InabilityType> InabilityType { get; set; }
        public DbSet<InabilityTypePE> InabilityTypePE { get; set; }
        public DbSet<LicenceTypePE> LicenceTypePE { get; set; }
        public DbSet<GroupConceptDetailPE> GroupConceptsDetailPE { get; set; }
        public DbSet<GroupConceptPE> GroupConceptsPE { get; set; }
        public DbSet<TypeContributor> TypeContributor { get; set; }
        public DbSet<TypeContributorPE> TypeContributorPE { get; set; }
        public DbSet<ContractType> ContractType { get; set; }
        public DbSet<ContractTypePE> ContractTypePE { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Ignore<Country>();
            //modelBuilder.Ignore<Department>();
            //modelBuilder.Ignore<City>();
            //modelBuilder.Ignore<SubTypeContributor>();
        }
    }
}