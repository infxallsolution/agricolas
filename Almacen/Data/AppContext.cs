using Almacen.Models;

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Almacen.Context
{
    public class AppContext : DbContext
    {

        public AppContext() : base("Server=localhost;Database=InfosAgricola;Trusted_Connection=True;")
        {
            Database.SetInitializer<AppContext>(null);
        }


        public DbSet<vsalidas> Vsalidas { get; set; }
        public DbSet<Departures> Departures { get; set; }

    }
}