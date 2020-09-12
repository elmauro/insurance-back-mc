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
	}
}
