using MC.Insurance.DTO;
using System.Threading.Tasks;

namespace MC.Insurance.Interfaces.Application
{
    public interface IInsuranceManagementService
	{
		Task<Response> GetInsurance(int insuranceId);
		Task<Response> GetInsurances();
		Task<Response> CreateInsurance(DTO.Insurance insurance);
		Task<Response> UpdateInsurance(int insuranceId, DTO.Insurance insurance);
		Task<Response> DeleteInsurance(int insuranceId);

		Task<Response> GetCustomerInsurances(string document);
		Task<Response> GetCustomers();
		Task<Response> CreateCustomerInsurance(string document, DTO.CustomerInsurance customerInsurance);
		Task<Response> DeleteCustomerInsurance(string document, int insuranceId);

		Task<Response> Login(string userName, string password);
		Task<Response> CreateTokenJWT(DTO.User user);
	}
}
