using Microsoft.EntityFrameworkCore;
using System;

namespace MC.Insurance.Infrastructure
{
    public class InsuranceContext : DbContext
	{
        public InsuranceContext(DbContextOptions<InsuranceContext> options)
        : base(options)
        { }
        public DbSet<DTO.Insurance> Insurances { get; set; }
        public DbSet<DTO.CustomerInsurance> CustomerInsurances { get; set; }
        public DbSet<DTO.tbCustomer> Customer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<DTO.Insurance>().HasData(
				new DTO.Insurance()
				{
					insuranceId = 1,
					name = "Incendios A1",
					description = "Seguro de Incendios",
					type = 2,
					coverage = "50%",
					start = new DateTime(2000, 9, 11),
					period = 12,
					price = 200000,
					risk = 4
				}
			);

			modelBuilder.Entity<DTO.CustomerInsurance>().HasData(
				new DTO.CustomerInsurance
				{
					customerInsuranceId = 1,
					document = "98632674",
					customerName = "Mauricio Cadavid",
					insuranceId = 1,
					name = "Incendios A1",
					description = "Seguro de Incendios",
					type = 2,
					coverage = "50%",
					start = new DateTime(2000, 9, 11),
					period = 12,
					price = 200000,
					risk = 4
				}
			);

			modelBuilder.Entity<DTO.tbCustomer>().HasData(
				new DTO.tbCustomer
				{
					customerId = 1,
					document = "98632674",
					customerName = "Mauricio Cadavid"
				}
			);
		}
    }
}
