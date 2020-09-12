using MC.Insurance.Interfaces.Infrastructure;
using MC.Insurance.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MC.Insurance.Infrastructure
{
	public class InsuranceRepository : IInsuranceRepository
	{
		InsuranceContext DataContext;
		public InsuranceRepository() 
		{
			IniciarDataContext();
		}

		public void IniciarDataContext()
		{
			var options = new DbContextOptionsBuilder<InsuranceContext>()
		   .UseInMemoryDatabase(databaseName: "Insurance")
		   .Options;

			using (DataContext = new InsuranceContext(options))
			{
				var insurance = new DTO.Insurance
				{
					insuranceId = 1,
					name =  "Incendios A1",
					description = "Seguro de Incendios",
					type = 2,
					coverage = "50%",
					start = new DateTime(2000,9,11),
					period = 12,
					price =  200000,
					risk=  4
				};

				DataContext.Insurances.Add(insurance);
				DataContext.SaveChanges();

			}


			DataContext = new InsuranceContext(options);
		}
		public void DeleteInsurance(int insuranceId)
		{
			DTO.Insurance insurance = DataContext.Insurances.Find(insuranceId);
			DataContext.Insurances.Remove(insurance);
		}

		public DTO.Insurance GetInsuranceByID(int insuranceId)
		{
			return DataContext.Insurances.Find(insuranceId);
		}

		public IEnumerable GetInsurances()
		{
			return DataContext.Insurances.Local.ToList();
		}

		public void InsertInsurance(DTO.Insurance insurance)
		{
			DataContext.Insurances.Add(insurance);
		}

		public void Save()
		{
			DataContext.SaveChanges();
		}

		public void UpdateInsurance(DTO.Insurance insurance)
		{
			DataContext.Entry(insurance).State = EntityState.Modified;
		}
	}
}
