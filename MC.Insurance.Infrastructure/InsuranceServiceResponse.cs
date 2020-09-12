using MC.Insurance.DTO;
using MC.Insurance.Interfaces.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MC.Insurance.Infrastructure
{
	public class InsuranceServiceResponse : IInsuranceServiceResponse
	{
		public IInsuranceRepository InsuranceRepository { get; set; }
		public InsuranceServiceResponse(IInsuranceRepository InsuranceRepository)
		{
			this.InsuranceRepository = InsuranceRepository;
		}
		public Task<ExternalResponse> GetInsurance(int insuranceId)
		{
            var response = InsuranceRepository.GetInsuranceByID(insuranceId);

            Task<ExternalResponse> task = new Task<ExternalResponse>(() =>
            {
                return new ExternalResponse
                {
                    IsSuccessStatusCode = true,
                    StatusCode = 200,
                    Body = JsonConvert.SerializeObject(response)
                };
            });
            task.Start();
            return task;
        }

        public Task<ExternalResponse> GetInsurances()
        {
            var response = InsuranceRepository.GetInsurances();

            Task<ExternalResponse> task = new Task<ExternalResponse>(() =>
            {
                return new ExternalResponse
                {
                    IsSuccessStatusCode = true,
                    StatusCode = 200,
                    Body = JsonConvert.SerializeObject(response)
                };
            });
            task.Start();
            return task;
        }

        public Task<ExternalResponse> CreateInsurance(DTO.Insurance insurance)
        {
           InsuranceRepository.InsertInsurance(insurance);

            Task<ExternalResponse> task = new Task<ExternalResponse>(() =>
            {
                return new ExternalResponse
                {
                    IsSuccessStatusCode = true,
                    StatusCode = 201,
                    Body = ""
                };
            });
            task.Start();
            return task;
        }

        public Task<ExternalResponse> UpdateInsurance(DTO.Insurance insurance)
        {
            throw new NotImplementedException();
        }

        public Task<ExternalResponse> DeleteInsurance(int insuranceId)
        {
            throw new NotImplementedException();
        }
    }
}
