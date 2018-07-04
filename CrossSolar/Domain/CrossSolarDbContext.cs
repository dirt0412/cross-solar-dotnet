using CrossSolar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CrossSolar.Domain
{
    public class CrossSolarDbContext : DbContext
    {
        public CrossSolarDbContext()
        {
        }

        public CrossSolarDbContext(DbContextOptions<CrossSolarDbContext> options) : base(options)
        {
        }

        public DbSet<Panel> Panels { get; set; }
        public DbSet<OneHourElectricity> OneHourElectricitys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}