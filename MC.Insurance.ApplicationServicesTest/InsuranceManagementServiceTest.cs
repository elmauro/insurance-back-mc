using MC.Insurance.DTO;
using MC.Insurance.Domain;
using MC.Insurance.Infrastructure;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using MC.Insurance.Interfaces.Application;
using MC.Insurance.Interfaces.Infrastructure;
using MC.Insurance.ApplicationServicesTest.MockServices;
using MC.Insurance.ApplicationServices;
using MC.Insurance.Interfaces.Domain;

namespace MC.Insurance.ApplicationServicesTest
{
    [TestFixture]
	public class InsuranceManagementServiceTest
	{
		IInsuranceManagementService insuranceManagementService;
		
		DTO.Insurance insurance;
		DTO.CustomerInsurance customerInsurance;

		[SetUp]
		public void Init()
		{
			IInsuranceDomain InsuranceDomain = new InsuranceDomain();
			ISerializer Serializer = new Serializer();
			IServiceResponse ServiceResponse = new ServiceResponse(Serializer);
			IInsuranceRepository InsuranceRepository = new MockInsuranceRepository();

			insuranceManagementService = new InsuranceManagementService(
				InsuranceDomain,
				InsuranceRepository,
				ServiceResponse,
				Serializer
			);

			insurance = new DTO.Insurance
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
			};

			customerInsurance = new DTO.CustomerInsurance
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
			};
		}

		[Test]
		public async Task GetInsurance()
		{
			int insuranceId = 1;

			ExternalResponse me = await insuranceManagementService.GetInsurance(insuranceId);
			MC.Insurance.DTO.Insurance insurance = JsonConvert.DeserializeObject<DTO.Insurance>(me.Body);

			Assert.AreEqual("Incendios A1", insurance.name);
		}

		[Test]
		public async Task GetInsurances()
		{
			ExternalResponse Response = await insuranceManagementService.GetInsurances();
			InsurancesResponse obj = JsonConvert.DeserializeObject<InsurancesResponse>(Response.Body);

			Assert.AreEqual(3, obj.Insurances.Count);
		}

		[Test]
		public async Task CreateInsurance()
		{
			var json = JsonConvert.SerializeObject(insurance);

			ExternalResponse httpResponse = await insuranceManagementService.CreateInsurance(json);
			Assert.AreEqual(204, httpResponse.StatusCode);
		}

		[Test]
		public async Task UpdateInsurance()
		{
			int insuranceId = 1;
			var json = JsonConvert.SerializeObject(insurance);

			ExternalResponse httpResponse = await insuranceManagementService.UpdateInsurance(insuranceId, json);
			Assert.AreEqual(204, httpResponse.StatusCode);
		}

		[Test]
		public async Task DeleteInsurance()
		{
			int insuranceId = 1;

			ExternalResponse httpResponse = await insuranceManagementService.DeleteInsurance(insuranceId);
			Assert.AreEqual(204, httpResponse.StatusCode);
		}

		[Test]
		public async Task GetCustomerInsurances()
		{
			string document = "98632674";

			ExternalResponse me = await insuranceManagementService.GetCustomerInsurances(document);
			CustomerInsuranceResponse obj = JsonConvert.DeserializeObject<CustomerInsuranceResponse>(me.Body);

			Assert.AreEqual(2, obj.CustomerInsurance.Count);
		}

		[Test]
		public async Task CreateCustomerInsurance()
		{
			string document = "98632674";
			var json = JsonConvert.SerializeObject(customerInsurance);

			ExternalResponse httpResponse = await insuranceManagementService.CreateCustomerInsurance(document, json);
			Assert.AreEqual(204, httpResponse.StatusCode);
		}

		[Test]
		public async Task DeleteCustomerInsurance()
		{
			string document = "98632674";
			int insuranceId = 1;

			ExternalResponse httpResponse = await insuranceManagementService.DeleteCustomerInsurance(document, insuranceId);
			Assert.AreEqual(204, httpResponse.StatusCode);
		}

		[Test]
		public async Task GetCustomers()
		{
			ExternalResponse customers = await insuranceManagementService.GetCustomers();
			var obj = JsonConvert.DeserializeObject<dynamic>(customers.Body);

			Assert.AreEqual(3, obj.Customers.Count);
		}
	}
}
