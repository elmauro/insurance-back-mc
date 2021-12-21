using MC.Insurance.ApplicationServicesTest.Fixtures;
using MC.Insurance.DTO;
using MC.Insurance.Infrastructure;
using MC.Insurance.Interfaces.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MC.Insurance.ApplicationServicesTest.MockServices
{
    class MockInsuranceRepository: IInsuranceRepository
	{
		private static MockInsuranceRepository instance = null;
        ISerializer Serializer { get; set; }

		public MockInsuranceRepository() {
            this.Serializer = new Serializer();
        }

        public static MockInsuranceRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MockInsuranceRepository();
                }
                return instance;
            }
        }

        public Task<IEnumerable> GetInsurances()
        {
            Task<IEnumerable> task = new Task<IEnumerable>(() =>
            {
                string obj = MockInsurance.GetInsurances();
                return Serializer.DeserializeObject<List<DTO.Insurance>>(obj);
            });
            task.Start();
            return task;
        }

        public Task<DTO.Insurance> GetInsuranceByID(int insuranceId)
        {
            Task<DTO.Insurance> task = new Task<DTO.Insurance>(() =>
            {
                string obj = MockInsurance.GetInsurance();
                return Serializer.DeserializeObject<DTO.Insurance>(obj);
            });
            task.Start();
            return task;
        }

        public Task<string> InsertInsurance(DTO.Insurance insurance)
        {
            Task<string> task = new Task<string>(() =>
            {
                return string.Empty;
            });
            task.Start();
            return task;
        }

        public Task<string> DeleteInsurance(int insuranceId)
        {
            Task<string> task = new Task<string>(() =>
            {
                return string.Empty;
            });
            task.Start();
            return task;
        }

        public Task<string> UpdateInsurance(DTO.Insurance insurance)
        {
            Task<string> task = new Task<string>(() =>
            {
                return string.Empty;
            });
            task.Start();
            return task;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable> GetCustomers()
        {
            CustomerResponses response = new CustomerResponses();
            response.Customers = new List<Customer>();

            response.Customers.Add(
                new Customer
                {
                    document = "98632674",
                    name = "Mauricio Cadavid"
                }
            );

            response.Customers.Add(
                new Customer
                {
                    document = "8288221",
                    name = "Hernan Cadavid"
                }
            );

            response.Customers.Add(
                new Customer
                {
                    document = "43160724",
                    name = "Maribel Gonzalez"
                }
            );

            Task<IEnumerable> task = new Task<IEnumerable>(() =>
            {
                return response.Customers;
            });
            task.Start();
            return task;
        }

        public Task<IEnumerable> GetCustomerByID(string document)
        {
            Task<IEnumerable> task = new Task<IEnumerable>(() =>
            {
                string obj = MockInsurance.GetCustomerInsurances();
                return Serializer.DeserializeObject<List<DTO.Insurance>>(obj);
            });
            task.Start();
            return task;
        }

        public Task<string> InsertCustomerInsurance(CustomerInsurance customerInsurance)
        {
            Task<string> task = new Task<string>(() =>
            {
                return string.Empty;
            });
            task.Start();
            return task;
        }

        public Task<string> DeleteCustomerInsurance(string document, int insuranceId)
        {
            Task<string> task = new Task<string>(() =>
            {
                return string.Empty;
            });
            task.Start();
            return task;
        }
    }
}
