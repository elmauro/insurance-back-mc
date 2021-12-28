using MC.Insurance.DTO;
using MC.Insurance.Domain;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using MC.Insurance.Interfaces.Application;
using MC.Insurance.ApplicationServices;
using MC.Insurance.Interfaces.Domain;
using Moq;
using MC.Insurance.ApplicationServicesTest.Fixtures;
using System.Collections;
using System.Collections.Generic;
using MC.Insurance.Interfaces.Infrastructure;
using insurance_back_mc.Controllers;
using MC.Insurance.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using AutoFixture;

namespace MC.Insurance.ApplicationServicesTest
{
    [TestFixture]
	public class InsuranceManagementServiceTest
	{
		InsuranceManagementController controller;
		IInsuranceManagementService insuranceManagementService;
		ISplunkLogger splunkLogger;
		
		DTO.Insurance insurance;
		DTO.CustomerInsurance customerInsurance;

		[SetUp]
		public void Init()
		{
			var mock = new Mock<IInsuranceRepository>();

			object obj = MockInsurance.GetInsurances();
			IEnumerable InsuranceList = (List<DTO.Insurance>) obj;
			mock.Setup(i => i.GetInsurances()).Returns(Task.FromResult(InsuranceList));

			obj = MockInsurance.GetInsurance();
			DTO.Insurance ins = (DTO.Insurance) obj;
			mock.Setup(i => i.GetInsuranceByID(1)).Returns(Task.FromResult(ins));

			var fixture = new Fixture();
			insurance = fixture.Create<DTO.Insurance>();

			mock.Setup(i => i.InsertInsurance(insurance)).Returns(Task.FromResult(String.Empty));
			mock.Setup(i => i.DeleteInsurance(1)).Returns(Task.FromResult(String.Empty));
			mock.Setup(i => i.UpdateInsurance(insurance)).Returns(Task.FromResult(String.Empty));
			
			ListResponse<Customer> response = new ListResponse<Customer>();
			response.List = new List<Customer> {
				fixture.Create<Customer>(),
				fixture.Create<Customer>(),
				fixture.Create<Customer>()
			};

			mock.Setup(i => i.GetCustomers()).Returns(Task.FromResult((IEnumerable) response.List));

			obj = MockInsurance.GetCustomerInsurances();
			IEnumerable CustomerInsurancesList = (List<DTO.CustomerInsurance>) obj;
			mock.Setup(i => i.GetCustomerByID("98632674")).Returns(Task.FromResult(CustomerInsurancesList));

			customerInsurance = fixture.Create<DTO.CustomerInsurance>();

			mock.Setup(i => i.InsertCustomerInsurance(customerInsurance)).Returns(Task.FromResult(String.Empty));
			mock.Setup(i => i.DeleteCustomerInsurance("98632674", 1)).Returns(Task.FromResult(String.Empty));

			IInsuranceDomain InsuranceDomain = new InsuranceDomain(mock.Object);

			insuranceManagementService = new InsuranceManagementService(
				InsuranceDomain
			);

			IOptions<SplunkConfig> someOptions = Options.Create<SplunkConfig>(new SplunkConfig() { 
				Url = "http://192.168.1.3:8088/services/collector",
				Token = "403a559e-9ed7-4ef8-9be2-7cb2f969d416"
			});
			splunkLogger = new SplunkLogger(someOptions);

			ILogger<InsuranceManagementController> logger = Mock.Of<ILogger<InsuranceManagementController>>();

			controller = new InsuranceManagementController(logger, insuranceManagementService, splunkLogger);
		}

		[Test]
		public void GetInsurance()
		{
			var httpResponse = controller.getInsurance(1);
			DTO.Insurance obj = (DTO.Insurance)((ObjectResult)httpResponse.Result).Value;

			Assert.NotNull(obj.name);
		}

		[Test]
		public void GetInsurances()
		{
			var httpResponse = controller.getInsurances();
			List<DTO.Insurance> obj = (List<DTO.Insurance>)((ObjectResult)httpResponse.Result).Value;

			Assert.AreEqual(3, obj.Count);
		}

		[Test]
		public void CreateInsurance()
		{
			var httpResponse = controller.CreateInsurance(insurance);
			Assert.AreEqual(204, ((ObjectResult) httpResponse.Result).StatusCode);
		}

		[Test]
		public void UpdateInsurance()
		{
			var httpResponse = controller.UpdateInsurance(1, insurance);
			Assert.AreEqual(204, ((ObjectResult)httpResponse.Result).StatusCode);
		}

		[Test]
		public void DeleteInsurance()
		{
			var httpResponse = controller.DeleteInsurance(1); 
			Assert.AreEqual(204, ((ObjectResult)httpResponse.Result).StatusCode);
		}

		[Test]
		public void GetCustomerInsurances()
		{
			var httpResponse = controller.getCustomer("98632674");
			List<DTO.CustomerInsurance> obj = (List<DTO.CustomerInsurance>)((ObjectResult)httpResponse.Result).Value;

			Assert.AreEqual(2, obj.Count);
		}

		[Test]
		public void CreateCustomerInsurance()
		{
			var httpResponse = controller.CreateCustomerInsurance("98632673", customerInsurance);
			Assert.AreEqual(204, ((ObjectResult)httpResponse.Result).StatusCode);
		}

		[Test]
		public void DeleteCustomerInsurance()
		{
			var httpResponse = controller.DeleteCustomerInsurance("98632673", 1);
			Assert.AreEqual(204, ((ObjectResult)httpResponse.Result).StatusCode);
		}

        [Test]
        public void GetCustomers()
        {
            var httpResponse = controller.getCustomers();
            List<DTO.Customer> obj = (List<DTO.Customer>)((ObjectResult)httpResponse.Result).Value;

            Assert.AreEqual(3, obj.Count);
        }
    }
}
