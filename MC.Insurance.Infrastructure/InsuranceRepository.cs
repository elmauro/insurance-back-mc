using MC.Insurance.Interfaces.Infrastructure;
using MC.Insurance.DTO;
using System.Collections.Generic;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace MC.Insurance.Infrastructure
{
    public class InsuranceRepository : IInsuranceRepository
	{
		InsuranceContext DataContext;
		private IOptions<ConnectionStrings> settings;
		static int insuranceId=0;

		public InsuranceRepository(IOptions<ConnectionStrings> settings) 
		{
			this.settings = settings;
			IniciarDataContext();
		}

		public void IniciarDataContext()
		{
			ConnectionStrings cn = this.settings.Value;
			
			var options = new DbContextOptionsBuilder<InsuranceContext>()
		   .UseSqlServer(cn.DefaultDatabase)
		   .Options;

			DataContext = new InsuranceContext(options);
		}
		public Task<string> DeleteInsurance(int insuranceId)
		{
			DTO.Insurance insurance = DataContext.Insurances.Find(insuranceId);
			DataContext.Insurances.Remove(insurance);
			DataContext.SaveChanges();

			Task<string> task = new Task<string>(() =>
			{
				return string.Empty;
			});
			task.Start();
			return task;
		}

		public Task<DTO.Insurance> GetInsuranceByID(int insuranceId)
		{
			var response = DataContext.Insurances.Find(insuranceId);

			Task<DTO.Insurance> task = new Task<DTO.Insurance>(() =>
			{
				return response;
			});
			task.Start();
			return task;
		}

		public Task<IEnumerable> GetInsurances()
		{
			var response = DataContext.Insurances.ToList();

			Task<IEnumerable> task = new Task<IEnumerable>(() =>
			{
				return response;
			});
			task.Start();
			return task;
		}

		public Task<string> InsertInsurance(DTO.Insurance insurance)
		{
			insuranceId++;
			insurance.insuranceId = insuranceId;
			DataContext.Insurances.Add(insurance);
			DataContext.SaveChanges();

			Task<string> task = new Task<string>(() =>
			{
				return string.Empty;
			});
			task.Start();
			return task;
		}

		public Task<string> UpdateInsurance(DTO.Insurance insurance)
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
			DataContext.SaveChanges();

			Task<string> task = new Task<string>(() =>
			{
				return string.Empty;
			});
			task.Start();
			return task;
		}

		public Task<IEnumerable> GetCustomers()
		{
			CustomerResponses response = new CustomerResponses();
			response.Customers = new List<Customer>();

			response.Customers.Add(
				new Customer
				{
					document = "98632674",
					name = "Mauricio Cadavid"
				}
			);

			response.Customers.Add(
				new Customer
				{
					document = "8288221",
					name = "Hernan Cadavid"
				}
			);

			response.Customers.Add(
				new Customer
				{
					document = "43160724",
					name = "Maribel Gonzalez"
				}
			);

			Task<IEnumerable> task = new Task<IEnumerable>(() =>
			{
				return response.Customers;
			});
			task.Start();
			return task;
		}

		public Task<IEnumerable> GetCustomerByID(string document)
		{
			Task<IEnumerable> task = new Task<IEnumerable>(() =>
			{
				return DataContext.CustomerInsurances.Where(c => c.document == document);
			});
			task.Start();
			return task;
		}

		public Task<string> InsertCustomerInsurance(DTO.CustomerInsurance customerInsurance)
		{
			DataContext.CustomerInsurances.Add(customerInsurance);
			DataContext.SaveChanges();

			Task<string> task = new Task<string>(() =>
			{
				return string.Empty;
			});
			task.Start();
			return task;
		}

		public Task<string> DeleteCustomerInsurance(string document, int insuranceId)
		{
			DTO.CustomerInsurance customerInsurance = DataContext.CustomerInsurances.Where(c => c.document == document && c.insuranceId == insuranceId).First();
			DataContext.CustomerInsurances.Remove(customerInsurance);
			DataContext.SaveChanges();

			Task<string> task = new Task<string>(() =>
			{
				return string.Empty;
			});
			task.Start();
			return task;
		}
    }
}
