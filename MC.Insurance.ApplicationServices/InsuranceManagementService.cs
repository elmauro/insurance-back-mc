using MC.Insurance.DTO;
using MC.Insurance.Interfaces.Application;
using MC.Insurance.Interfaces.Domain;
using System.Collections;
using System.Threading.Tasks;

namespace MC.Insurance.ApplicationServices
{
    public class InsuranceManagementService : IInsuranceManagementService
	{
		public IInsuranceDomain InsuranceDomain { get; set; }

		public InsuranceManagementService(
			IInsuranceDomain InsuranceDomain
		) {
			this.InsuranceDomain = InsuranceDomain;
		}
		
		public async Task<Response> GetInsurance(int insuranceId)
		{
			DTO.Insurance httpResponse = await InsuranceDomain.GetInsuranceByID(insuranceId);
			return InsuranceDomain.CreateResponse(Enumerations.StatusCode.OK, httpResponse);
		}

		public async Task<Response> GetInsurances()
		{
			IEnumerable httpResponse = await InsuranceDomain.GetInsurances();
			return InsuranceDomain.CreateResponse(Enumerations.StatusCode.OK, httpResponse);
		}

		public async Task<Response> CreateInsurance(DTO.Insurance insurance)
		{
			string httpResponse = await InsuranceDomain.InsertInsurance(insurance);
			return InsuranceDomain.CreateResponse(Enumerations.StatusCode.NO_CONTENT, httpResponse);
		}

		public async Task<Response> UpdateInsurance(int insuranceId, DTO.Insurance insurance)
		{
			string httpResponse = await InsuranceDomain.UpdateInsurance(insuranceId, insurance);
			return InsuranceDomain.CreateResponse(Enumerations.StatusCode.NO_CONTENT, httpResponse);
		}

		public async Task<Response> DeleteInsurance(int insuranceId)
		{
			string httpResponse = await InsuranceDomain.DeleteInsurance(insuranceId);
			return InsuranceDomain.CreateResponse(Enumerations.StatusCode.NO_CONTENT, httpResponse);
		}

		public async Task<Response> GetCustomerInsurances(string document)
		{
			IEnumerable httpResponse = await InsuranceDomain.GetCustomerByID(document);
			return InsuranceDomain.CreateResponse(Enumerations.StatusCode.OK, httpResponse);
		}

		public async Task<Response> GetCustomers()
		{
			IEnumerable httpResponse = await InsuranceDomain.GetCustomers();
			return InsuranceDomain.CreateResponse(Enumerations.StatusCode.OK, httpResponse);
		}

		public async Task<Response> CreateCustomerInsurance(string document, DTO.CustomerInsurance customerInsurance)
		{
			string httpResponse = await InsuranceDomain.InsertCustomerInsurance(document, customerInsurance);
			return InsuranceDomain.CreateResponse(Enumerations.StatusCode.NO_CONTENT, httpResponse);
		}

		public async Task<Response> DeleteCustomerInsurance(string document, int insuranceId)
		{
			string httpResponse = await InsuranceDomain.DeleteCustomerInsurance(document, insuranceId);
			return InsuranceDomain.CreateResponse(Enumerations.StatusCode.NO_CONTENT, httpResponse);
		}

        public async Task<Response> Login(string userName, string password)
        {
			DTO.User httpResponse = await InsuranceDomain.Login(userName, password);
			return InsuranceDomain.CreateResponse(Enumerations.StatusCode.OK, httpResponse);
		}

        public async Task<Response> CreateTokenJWT(User user)
        {
			string httpResponse = await InsuranceDomain.CreateTokenJWT(user);
			return InsuranceDomain.CreateResponse(Enumerations.StatusCode.OK, httpResponse);
		}
    }
}
