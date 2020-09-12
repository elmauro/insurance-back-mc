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
			DTO.Insurance _insurance = DataContext.Insurances.Find(insurance.insuranceId);
			_insurance.name = insurance.name;
			_insurance.description = insurance.description;
			_insurance.type = insurance.type;
			_insurance.coverage = insurance.coverage;
			_insurance.start = insurance.start;
			_insurance.period = insurance.period;
			_insurance.price = insurance.price;
			_insurance.risk = insurance.risk;

			DataContext.Entry(_insurance).State = EntityState.Modified;
		}

		public IEnumerable GetCustomers()
		{
			throw new NotImplementedException();
		}

		public IEnumerable GetCustomerByID(string document)
		{
			return DataContext.CustomerInsurances.Where(c => c.document == document);
		}

		public void InsertCustomerInsurance(DTO.CustomerInsurance customerInsurance)
		{
			DataContext.CustomerInsurances.Add(customerInsurance);
		}

		public void DeleteCustomerInsurance(int customerInsuranceId)
		{
			DTO.CustomerInsurance customerInsurance = DataContext.CustomerInsurances.Find(customerInsuranceId);
			DataContext.CustomerInsurances.Remove(customerInsurance);
		}
	}
}
