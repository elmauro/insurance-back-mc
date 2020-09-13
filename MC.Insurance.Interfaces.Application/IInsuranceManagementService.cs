using MC.Insurance.DTO;
using System;
using System.Threading.Tasks;

namespace MC.Insurance.Interfaces.Application
{
	public interface IInsuranceManagementService
	{
		Task<ExternalResponse> GetInsurance(int insuranceId);
		Task<ExternalResponse> GetInsurances();
		Task<ExternalResponse> CreateInsurance(object insurance);
		Task<ExternalResponse> UpdateInsurance(int insuranceId, object insurance);
		Task<ExternalResponse> DeleteInsurance(int insuranceId);

		Task<ExternalResponse> GetCustomerInsurances(string document);
		Task<ExternalResponse> GetCustomers();
		Task<ExternalResponse> CreateCustomerInsurance(string document, object customerInsurance);
		Task<ExternalResponse> DeleteCustomerInsurance(string document, int insuranceId);
	}
}
