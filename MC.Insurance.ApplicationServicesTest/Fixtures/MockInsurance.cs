using System;
using System.Collections.Generic;
using System.Text;

namespace MC.Insurance.ApplicationServicesTest.Fixtures
{
	public static class MockInsurance
	{
		public static string InsuranceData =>
		@"
			{
				'insuranceId': 1,
				'name': 'Incendios A1',
				'description': 'Seguro de Incendios',
				'type': 2,
				'coverage': '50%',
				'start': '09/11/2020',
				'period': 12,
				'price': 200000,
				'risk': 4
			}
		";

		public static string InsurancesData =>
		@"
			[
				{
					'insuranceId': 1,
					'name': 'Incendios A1',
					'description': 'Seguro de Incendios',
					'type': 2,
					'coverage': '50%',
					'start': '09/11/2020',
					'period': 12,
					'price': 200000,
					'risk': 4
				},
				{
					'insuranceId': 2,
					'name': 'Incendios A2',
					'description': 'Seguro de Incendios',
					'type': 2,
					'coverage': '50%',
					'start': '09/11/2020',
					'period': 12,
					'price': 200000,
					'risk': 4
				},
				{
					'insuranceId': 3,
					'name': 'Incendios A3',
					'description': 'Seguro de Incendios',
					'type': 2,
					'coverage': '50%',
					'start': '09/11/2020',
					'period': 12,
					'price': 200000,
					'risk': 4
				}
			]
		";

		public static string CustomerInsurencesData =>
		@"
			[
				{
					'customerInsuranceId': 1,
					'document': '98632674',
					'customerName': 'Mauricio Cadavid',
					'insuranceId': 1,
					'name': 'Incendios A1',
					'description': 'Seguro de Incendios',
					'type': 2,
					'coverage': '50%',
					'start': '09/11/2020',
					'period': 12,
					'price': 200000,
					'risk': 4
				},
				{
					'customerInsuranceId': 1,
					'document': '98632674',
					'customerName': 'Mauricio Cadavid',
					'insuranceId': 2,
					'name': 'Incendios A2',
					'description': 'Seguro de Incendios',
					'type': 2,
					'coverage': '50%',
					'start': '09/11/2020',
					'period': 12,
					'price': 200000,
					'risk': 4
				}
			]
		";

		public static string GetInsurance() {
			return InsuranceData;
		}

		public static string GetInsurances()
		{
			return InsurancesData;
		}

		public static string GetCustomerInsurances() {
			return CustomerInsurencesData;
		}
	}
}
