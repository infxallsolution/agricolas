using Microsoft.Extensions.DependencyInjection;
using Nomina.WebForms.Repository.Repository.Implementation;
using Nomina.WebForms.Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace Nomina.WebForms.Repository.RepositoryExtension
{
    public static class RepositoryExtension
    {
        public static void UseRepository(this IServiceCollection services, Type dbContextType)
        {
            services.AddScoped(typeof(DbContext), dbContextType);
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}