using Microsoft.EntityFrameworkCore;
using PruebaBIAAPI.DataModelCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaBIAAPI.DataModelCore.Context
{

    public class ApplicationDbContext : DbContext
    {
        public SeedData SeenData { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.HasDefaultSchema(Config.GetValue<string>("SchemaName"));
            SeenData = new SeedData();
            SeenData.SeedDataBD(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
        public void SeedDataBD(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<EnergyConsumption>()
                .HasData(new List<EnergyConsumption>
                {
                   new EnergyConsumption(){Id =1, Active_energy =594.5484, Meter_date =new DateTime(2022, 10, 12, 02, 50, 06)  }

                });
        }

        public DbSet<EnergyConsumption> EnergyConsumption { get; set; }
    }

}