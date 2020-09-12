using MC.Insurance.DTO;
using MC.Insurance.Infrastructure;
using MC.Insurance.Interfaces.Application;
using MC.Insurance.Interfaces.Domain;
using MC.Insurance.Interfaces.Infrastructure;
using System;
using System.Threading.Tasks;

namespace MC.Insurance.ApplicationServices
{
	public class InsuranceManagementService : IInsuranceManagementService
	{
		public IInsuranceDomain InsuranceDomain { get; set; }
		public IInsuranceFormatInputOutput InsuranceFormatInputOutput { get; set; }
		public IInsuranceServiceResponse InsuranceServiceResponse { get; set; }
		public ISerializer Serializer { get; set; }

		public InsuranceManagementService(
			IInsuranceDomain InsuranceDomain,
			IInsuranceFormatInputOutput InsuranceFormatInputOutput,
			IInsuranceServiceResponse InsuranceServiceResponse,
			ISerializer Serializer
		) {
			this.InsuranceDomain = InsuranceDomain;
			this.InsuranceFormatInputOutput = InsuranceFormatInputOutput;
			this.InsuranceServiceResponse = InsuranceServiceResponse;
			this.Serializer = Serializer;
		}
		
		public async Task<ExternalResponse> GetInsurance(int insuranceId)
		{
			ExternalResponse httpResponse = await InsuranceServiceResponse.GetInsurance(insuranceId);
			return InsuranceFormatInputOutput.GetInsuranceFormatted(httpResponse);
		}

		public async Task<ExternalResponse> GetInsurances()
		{
			ExternalResponse httpResponse = await InsuranceServiceResponse.GetInsurances();
			return InsuranceFormatInputOutput.GetInsurancesFormatted(httpResponse);
		}

		public async Task<ExternalResponse> CreateInsurance(object content)
		{
			DTO.Insurance insurance = Serializer.DeserializeObject<DTO.Insurance>(content.ToString());
			insurance = InsuranceDomain.AsignCoverage(insurance);
			return await InsuranceServiceResponse.CreateInsurance(insurance);
		}

		public async Task<ExternalResponse> UpdateInsurance(object insurance)
		{
			throw new NotImplementedException();
		}

		public async Task<ExternalResponse> DeleteInsurance(int insuranceId)
		{
			throw new NotImplementedException();
		}
	}
}
