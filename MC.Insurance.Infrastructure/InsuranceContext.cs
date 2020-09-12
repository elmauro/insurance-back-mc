using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MC.Insurance.Infrastructure
{
	public class InsuranceContext : DbContext
	{
        public InsuranceContext(DbContextOptions<InsuranceContext> options)
        : base(options)
        { }
        public DbSet<DTO.Insurance> Insurances { get; set; }
        public DbSet<DTO.CustomerInsurance> CustomerInsurances { get; set; }
    }
}
