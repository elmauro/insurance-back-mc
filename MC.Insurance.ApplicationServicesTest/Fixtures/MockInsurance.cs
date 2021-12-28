using AutoFixture;
using System;
using System.Collections.Generic;
using System.Text;

namespace MC.Insurance.ApplicationServicesTest.Fixtures
{
	public static class MockInsurance
	{
		public static Fixture fixture = new Fixture();

		public static object GetInsurance() {
			return fixture.Create<DTO.Insurance>();
		}

		public static object GetInsurances()
		{
			return new List<DTO.Insurance> {
				fixture.Create<DTO.Insurance>(),
				fixture.Create<DTO.Insurance>(),
				fixture.Create<DTO.Insurance>()
			};
		}

		public static object GetCustomerInsurances() {
			return new List<DTO.CustomerInsurance> {
				fixture.Create<DTO.CustomerInsurance>(),
				fixture.Create<DTO.CustomerInsurance>(),
			};
		}
	}
}
