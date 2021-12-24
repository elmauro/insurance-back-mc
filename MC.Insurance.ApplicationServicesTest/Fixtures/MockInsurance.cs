using System;
using System.Collections.Generic;
using System.Text;

namespace MC.Insurance.ApplicationServicesTest.Fixtures
{
	public static class MockInsurance
	{
		public static DTO.Insurance Insurance = new DTO.Insurance() {
			insuranceId = 1,
			name = "Incendios A1",
			description = "Seguro de Incendios",
			type = 2,
			coverage = "50%",
			start = new DateTime(2020, 9, 11),
			period = 12,
			price = 200000,
			risk = 4
		};

		public static List<DTO.Insurance> InsurancesData = new List<DTO.Insurance>() { 
			new DTO.Insurance() {
				insuranceId = 1,
				name = "Incendios A1",
				description = "Seguro de Incendios",
				type = 2,
				coverage = "50%",
				start = new DateTime(2020, 9, 11),
				period = 12,
				price = 200000,
				risk = 4
			},
			new DTO.Insurance() {
				insuranceId = 1,
				name = "Incendios A2",
				description = "Seguro de Incendios",
				type = 2,
				coverage = "50%",
				start = new DateTime(2020, 9, 11),
				period = 12,
				price = 200000,
				risk = 4
			},
			new DTO.Insurance() {
				insuranceId = 1,
				name = "Incendios A3",
				description = "Seguro de Incendios",
				type = 2,
				coverage = "50%",
				start = new DateTime(2020, 9, 11),
				period = 12,
				price = 200000,
				risk = 4
			}
		};

		public static List<DTO.CustomerInsurance> CustomerInsurencesData = new List<DTO.CustomerInsurance>() {
			new DTO.CustomerInsurance() {
				customerInsuranceId = 1,
				document = "98632674",
				customerName = "Mauricio Cadavid",
				insuranceId = 1,
				name = "Incendios A1",
				description = "Seguro de Incendios",
				type = 2,
				coverage = "50%",
				start = new DateTime(2020, 9, 11),
				period = 12,
				price = 200000,
				risk = 4
			},
			new DTO.CustomerInsurance() {
				customerInsuranceId = 1,
				document = "98632674",
				customerName = "Mauricio Cadavid",
				insuranceId = 2,
				name = "Incendios A2",
				description = "Seguro de Incendios",
				type = 2,
				coverage = "50%",
				start = new DateTime(2020, 9, 11),
				period = 12,
				price = 200000,
				risk = 4
			}
		};

		public static object GetInsurance() {
			return Insurance;
		}

		public static object GetInsurances()
		{
			return InsurancesData;
		}

		public static object GetCustomerInsurances() {
			return CustomerInsurencesData;
		}
	}
}
