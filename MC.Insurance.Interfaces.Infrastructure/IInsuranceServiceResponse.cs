using MC.Insurance.DTO;
using System;
using System.Threading.Tasks;

namespace MC.Insurance.Interfaces.Infrastructure
{
	public interface IInsuranceServiceResponse
	{
		Task<ExternalResponse> GetInsurance(int insuranceId);
		Task<ExternalResponse> GetInsurances();
		Task<ExternalResponse> CreateInsurance(DTO.Insurance insurance);
		Task<ExternalResponse> UpdateInsurance(DTO.Insurance insurance);
		Task<ExternalResponse> DeleteInsurance(int insuranceId);

		Task<ExternalResponse> GetCustomerInsurances(string document);
		Task<ExternalResponse> GetCustomers();
		Task<ExternalResponse> CreateCustomerInsurance(DTO.CustomerInsurance customerInsurance);
		Task<ExternalResponse> DeleteCustomerInsurance(string document, int insuranceId);
	}
}
