using System;
using System.Threading.Tasks;
using System.Collections;
using MC.Insurance.Interfaces.Infrastructure;
using MC.Insurance.Interfaces.Domain;
using MC.Insurance.DTO;

namespace MC.Insurance.Domain
{
    public class InsuranceDomain: IInsuranceDomain
    {
        public IInsuranceRepository InsuranceRepository { get; set; }

        public InsuranceDomain(
            IInsuranceRepository InsuranceRepository
        ) { 
            this.InsuranceRepository = InsuranceRepository;
        }

		public DTO.Insurance AsignCoverage(DTO.Insurance add)
		{
			if (add.risk == 4)
			{
				add.coverage = "50%";
			}
			else {
				add.coverage = "100%";
			}

			return add;
		}

        public async Task<string> DeleteCustomerInsurance(string document, int insuranceId)
        {
            string httpResponse = await InsuranceRepository.DeleteCustomerInsurance(document, insuranceId);
            return httpResponse;
        }

        public async Task<string> DeleteInsurance(int insuranceId)
        {
            string httpResponse = await InsuranceRepository.DeleteInsurance(insuranceId);
            return httpResponse;
        }

        public async Task<IEnumerable> GetCustomerByID(string document)
        {
            IEnumerable httpResponse = await InsuranceRepository.GetCustomerByID(document);
            return httpResponse;
        }

        public async Task<IEnumerable> GetCustomers()
        {
            IEnumerable httpResponse = await InsuranceRepository.GetCustomers();
            return httpResponse;
        }

        public async Task<DTO.Insurance> GetInsuranceByID(int insuranceId)
        {
            DTO.Insurance httpResponse = await InsuranceRepository.GetInsuranceByID(insuranceId);
            return httpResponse;
        }

        public async Task <IEnumerable> GetInsurances()
        {
            IEnumerable response = await InsuranceRepository.GetInsurances();
            return response;
        }

        public async Task<string> InsertCustomerInsurance(string document, DTO.CustomerInsurance customerInsurance)
        {
            customerInsurance = this.UpdateValues(document, customerInsurance);
            string httpResponse = await InsuranceRepository.InsertCustomerInsurance(customerInsurance);
            return httpResponse;
        }

        public async Task<string> InsertInsurance(DTO.Insurance insurance)
        {
            insurance = this.AsignCoverage(insurance);
            string httpResponse = await InsuranceRepository.InsertInsurance(insurance);
            return httpResponse;
        }

        public DTO.Insurance UpdateInsuraceId(int insuranceId, DTO.Insurance add)
		{
			add.insuranceId = insuranceId;
			return add;
		}

        public async Task<string> UpdateInsurance(int insuranceId, DTO.Insurance insurance)
        {
            insurance = this.UpdateInsuraceId(insuranceId, insurance);
            insurance = this.AsignCoverage(insurance);
            string httpResponse = await InsuranceRepository.UpdateInsurance(insurance);
            return httpResponse;
        }

        public CustomerInsurance UpdateValues(string document, CustomerInsurance add)
		{
			add.document = document;
			add.start = DateTime.Now;

			return add;
		}

        public Response CreateResponse(Enumerations.StatusCode StatusCode, object Response)
        {
            return new Response
            {
                StatusCode = (int)StatusCode,
                Body = Response
            };
        }
    }
}
