﻿using MC.Insurance.DTO;
using MC.Insurance.Domain;
using MC.Insurance.Infrastructure;
using MC.Insurance.ApplicationServicesTest;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net.Http;
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

		[SetUp]
		public void Init()
		{
			IInsuranceDomain InsuranceDomain = new InsuranceDomain();
			ISerializer Serializer = new Serializer();
			IInsuranceFormatInputOutput InsuranceFormatInputOutput = new InsuranceFormatInputOutput(InsuranceDomain, Serializer);

			IInsuranceServiceResponse InsuranceServiceResponse = MockInsuranceServiceResponse.Instance;
			
			insuranceManagementService = new InsuranceManagementService(
				InsuranceDomain, 
				InsuranceFormatInputOutput, 
				InsuranceServiceResponse,
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
		public async Task CreateInsurances()
		{
			var json = JsonConvert.SerializeObject(insurance);

			ExternalResponse httpResponse = await insuranceManagementService.CreateInsurance(json);
			Assert.AreEqual(201, httpResponse.StatusCode);
		}
	}
}
