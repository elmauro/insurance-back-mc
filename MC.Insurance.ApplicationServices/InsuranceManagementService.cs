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

		public async Task<ExternalResponse> UpdateInsurance(int insuranceId, object content)
		{
			DTO.Insurance insurance = Serializer.DeserializeObject<DTO.Insurance>(content.ToString());
			insurance = InsuranceDomain.UpdateInsuraceId(insuranceId, insurance);
			insurance = InsuranceDomain.AsignCoverage(insurance);
			return await InsuranceServiceResponse.UpdateInsurance(insurance);
		}

		public async Task<ExternalResponse> DeleteInsurance(int insuranceId)
		{
			return await InsuranceServiceResponse.DeleteInsurance(insuranceId);
		}

		public async Task<ExternalResponse> GetCustomerInsurances(string document)
		{
			ExternalResponse httpResponse = await InsuranceServiceResponse.GetCustomerInsurances(document);
			return InsuranceFormatInputOutput.GetCustomerFormatted(httpResponse);
		}

		public async Task<ExternalResponse> GetCustomers()
		{
			return InsuranceFormatInputOutput.GetDefaultCustomersFormatted();
		}

		public async Task<ExternalResponse> CreateCustomerInsurance(string document, object content)
		{
			DTO.CustomerInsurance customerInsurance = Serializer.DeserializeObject<DTO.CustomerInsurance>(content.ToString());
			customerInsurance = InsuranceDomain.UpdateValues(document, customerInsurance);
			return await InsuranceServiceResponse.CreateCustomerInsurance(customerInsurance);
		}

		public async Task<ExternalResponse> DeleteCustomerInsurance(string document, int insuranceId)
		{
			return await InsuranceServiceResponse.DeleteCustomerInsurance(document, insuranceId);
		}
	}
}
