using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MC.Insurance.Interfaces.Infrastructure
{
	public interface IInsuranceRepository
	{
        Task<IEnumerable> GetInsurances();
        Task<DTO.Insurance> GetInsuranceByID(int insuranceId);
        Task<string> InsertInsurance(DTO.Insurance insurance);
        Task<string> DeleteInsurance(int insuranceId);
        Task<string> UpdateInsurance(DTO.Insurance insurance);
        void Save();

        Task<IEnumerable> GetCustomers();
        Task<IEnumerable> GetCustomerByID(string document);
        Task<string> InsertCustomerInsurance(DTO.CustomerInsurance customerInsurance);
        Task<string> DeleteCustomerInsurance(string document, int insuranceId);
    }
}
