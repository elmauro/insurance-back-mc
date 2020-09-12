using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MC.Insurance.Interfaces.Infrastructure
{
	public interface IInsuranceRepository
	{
        IEnumerable GetInsurances();
        DTO.Insurance GetInsuranceByID(int insuranceId);
        void InsertInsurance(DTO.Insurance insurance);
        void DeleteInsurance(int insuranceId);
        void UpdateInsurance(DTO.Insurance insurance);
        void Save();

        IEnumerable GetCustomers();
        IEnumerable GetCustomerByID(string document);
        void InsertCustomerInsurance(DTO.CustomerInsurance customerInsurance);
        void DeleteCustomerInsurance(int customerInsuranceId);
    }
}
