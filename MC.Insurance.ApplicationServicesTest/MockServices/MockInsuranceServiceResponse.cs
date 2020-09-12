using MC.Insurance.ApplicationServicesTest.Fixtures;
using MC.Insurance.DTO;
using MC.Insurance.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MC.Insurance.ApplicationServicesTest.MockServices
{
	class MockInsuranceServiceResponse: IInsuranceServiceResponse
	{
		private static MockInsuranceServiceResponse instance = null;

		public MockInsuranceServiceResponse() { }

        public static MockInsuranceServiceResponse Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MockInsuranceServiceResponse();
                }
                return instance;
            }
        }

        public async Task<ExternalResponse> GetInsurance(int insuranceId)
        {
            return new ExternalResponse
            {
                IsSuccessStatusCode = true,
                StatusCode = 200,
                Body = MockInsurance.GetInsurance()
            };
        }

        public async Task<ExternalResponse> CreateInsurance(DTO.Insurance insurance)
        {
            return new ExternalResponse
            {
                IsSuccessStatusCode = true,
                StatusCode = 201,
                Body = ""
            };
        }

        public async Task<ExternalResponse> GetInsurances()
        {
            return new ExternalResponse
            {
                IsSuccessStatusCode = true,
                StatusCode = 200,
                Body = MockInsurance.GetInsurances()
            };
        }

        public async Task<ExternalResponse> UpdateInsurance(DTO.Insurance insurance)
        {
            return new ExternalResponse
            {
                IsSuccessStatusCode = true,
                StatusCode = 204,
                Body = ""
            };
        }

        public async Task<ExternalResponse> DeleteInsurance(int insuranceId)
        {
            return new ExternalResponse
            {
                IsSuccessStatusCode = true,
                StatusCode = 200,
                Body = ""
            };
        }
    }
}
