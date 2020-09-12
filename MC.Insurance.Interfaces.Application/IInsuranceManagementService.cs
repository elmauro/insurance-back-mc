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
		Task<ExternalResponse> UpdateInsurance(object insurance);
		Task<ExternalResponse> DeleteInsurance(int insuranceId);

		Task<ExternalResponse> GetCustomer(string document);
		Task<ExternalResponse> GetCustomers();
		Task<ExternalResponse> CreateCustomerInsurance(string document, object customerInsurance);
		Task<ExternalResponse> DeleteCustomerInsurance(string document, int insuranceId);
	}
}
