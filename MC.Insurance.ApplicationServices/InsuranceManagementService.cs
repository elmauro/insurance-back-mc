using MC.Insurance.DTO;
using MC.Insurance.Interfaces.Application;
using MC.Insurance.Interfaces.Domain;
using MC.Insurance.Interfaces.Infrastructure;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace MC.Insurance.ApplicationServices
{
	public class InsuranceManagementService : IInsuranceManagementService
	{
		public IInsuranceDomain InsuranceDomain { get; set; }
		public IInsuranceRepository InsuranceRepository { get; set; }
		public IServiceResponse ServiceResponse { get; set; }
		public ISerializer Serializer { get; set; }

		public InsuranceManagementService(
			IInsuranceDomain InsuranceDomain,
			IInsuranceRepository InsuranceRepository,
			IServiceResponse ServiceResponse,
			ISerializer Serializer
		) {
			this.InsuranceDomain = InsuranceDomain;
			this.InsuranceRepository = InsuranceRepository;

			this.ServiceResponse = ServiceResponse;
			this.Serializer = Serializer;
		}
		
		public async Task<ExternalResponse> GetInsurance(int insuranceId)
		{
			DTO.Insurance httpResponse = await InsuranceRepository.GetInsuranceByID(insuranceId);
			return ServiceResponse.CreateResponse(true, Enumerations.StatusCode.OK, httpResponse);
		}

		public async Task<ExternalResponse> GetInsurances()
		{
			IEnumerable httpResponse = await InsuranceRepository.GetInsurances();
			return ServiceResponse.CreateInsurancesResponse(true, Enumerations.StatusCode.OK, httpResponse);
		}

		public async Task<ExternalResponse> CreateInsurance(object content)
		{
			DTO.Insurance insurance = Serializer.DeserializeObject<DTO.Insurance>(content.ToString());
			insurance = InsuranceDomain.AsignCoverage(insurance);
			string httpResponse = await InsuranceRepository.InsertInsurance(insurance);
			return ServiceResponse.CreateResponse(true, Enumerations.StatusCode.NO_CONTENT, httpResponse);
		}

		public async Task<ExternalResponse> UpdateInsurance(int insuranceId, object content)
		{
			DTO.Insurance insurance = Serializer.DeserializeObject<DTO.Insurance>(content.ToString());
			insurance = InsuranceDomain.UpdateInsuraceId(insuranceId, insurance);
			insurance = InsuranceDomain.AsignCoverage(insurance);
			string httpResponse = await InsuranceRepository.UpdateInsurance(insurance);
			return ServiceResponse.CreateResponse(true, Enumerations.StatusCode.NO_CONTENT, httpResponse);
		}

		public async Task<ExternalResponse> DeleteInsurance(int insuranceId)
		{
			string httpResponse = await InsuranceRepository.DeleteInsurance(insuranceId);
			return ServiceResponse.CreateResponse(true, Enumerations.StatusCode.NO_CONTENT, httpResponse);
		}

		public async Task<ExternalResponse> GetCustomerInsurances(string document)
		{
			IEnumerable httpResponse = await InsuranceRepository.GetCustomerByID(document);
			return ServiceResponse.CreateCustomersInsuranceResponse(true, Enumerations.StatusCode.OK, httpResponse);
		}

		public async Task<ExternalResponse> GetCustomers()
		{
			IEnumerable httpResponse = await InsuranceRepository.GetCustomers();
			return ServiceResponse.CreateCustomersResponse(true, Enumerations.StatusCode.OK, httpResponse);
		}

		public async Task<ExternalResponse> CreateCustomerInsurance(string document, object content)
		{
			DTO.CustomerInsurance customerInsurance = Serializer.DeserializeObject<DTO.CustomerInsurance>(content.ToString());
			customerInsurance = InsuranceDomain.UpdateValues(document, customerInsurance);
			string httpResponse = await InsuranceRepository.InsertCustomerInsurance(customerInsurance);
			return ServiceResponse.CreateResponse(true, Enumerations.StatusCode.NO_CONTENT, httpResponse);
		}

		public async Task<ExternalResponse> DeleteCustomerInsurance(string document, int insuranceId)
		{
			string httpResponse = await InsuranceRepository.DeleteCustomerInsurance(document, insuranceId);
			return ServiceResponse.CreateResponse(true, Enumerations.StatusCode.NO_CONTENT, httpResponse);
		}
	}
}
