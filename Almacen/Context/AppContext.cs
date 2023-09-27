using Almacen.Models;

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Almacen.Context
{
    public class AppContext : DbContext
    {
       
            public AppContext() : base("Agricola")
            {
            }

         
            public DbSet<Account> Accounts { get; set; }
            public DbSet<vsalidas> Vsalidas { get; set; }


    }
}