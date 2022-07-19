using CarWebApi.Models;
using ModelWebApi.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ModelWebApi.DAL
{
    public class CarContext : DbContext
    {
        public CarContext() : base("DefaultConnection")
        {
        }

        public DbSet<Car> Cars { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}

