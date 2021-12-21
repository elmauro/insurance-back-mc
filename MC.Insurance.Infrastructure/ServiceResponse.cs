using MC.Insurance.DTO;
using MC.Insurance.Interfaces.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MC.Insurance.Infrastructure
{
    public class ServiceResponse : IServiceResponse
    {
        public ISerializer Serializer { get; set; }

        public ServiceResponse(
            ISerializer Serializer)
        {
            this.Serializer = Serializer;
        }
        public ExternalResponse CreateResponse(bool IsSuccessStatusCode, Enumerations.StatusCode StatusCode, object Response)
        {
            var response = Response;

            return new ExternalResponse
            {
                IsSuccessStatusCode = IsSuccessStatusCode,
                StatusCode = (int) StatusCode,
                Body = JsonConvert.SerializeObject(response)
            };
        }

        public ExternalResponse CreateInsurancesResponse(bool IsSuccessStatusCode, Enumerations.StatusCode StatusCode, object Response)
        {
            string _Body = JsonConvert.SerializeObject(Response);
            InsurancesResponse _insurances = new InsurancesResponse();
            var result = _Body;

            if (result.Length > 0)
            {
                var obj = Serializer.DeserializeObject<List<DTO.Insurance>>(result);
                _insurances.Insurances = obj;

                return new ExternalResponse
                {
                    IsSuccessStatusCode = IsSuccessStatusCode,
                    StatusCode = (int) StatusCode,
                    Body = JsonConvert.SerializeObject(_insurances)
                };
            }

            return new ExternalResponse
            {
                IsSuccessStatusCode = IsSuccessStatusCode,
                StatusCode = (int) StatusCode,
                Body = JsonConvert.SerializeObject(_insurances)
            }; 
        }

        public ExternalResponse CreateCustomersResponse(bool IsSuccessStatusCode, Enumerations.StatusCode StatusCode, object Response)
        {
            string _Body = JsonConvert.SerializeObject(Response);
            CustomerResponses _customer = new CustomerResponses();
            var result = _Body;

            if (result.Length > 0)
            {
                var obj = Serializer.DeserializeObject<List<DTO.Customer>>(result);
                _customer.Customers = obj;

                return new ExternalResponse
                {
                    IsSuccessStatusCode = IsSuccessStatusCode,
                    StatusCode = (int)StatusCode,
                    Body = JsonConvert.SerializeObject(_customer)
                };
            }

            return new ExternalResponse
            {
                IsSuccessStatusCode = IsSuccessStatusCode,
                StatusCode = (int)StatusCode,
                Body = JsonConvert.SerializeObject(_customer)
            };
        }

        public ExternalResponse CreateCustomersInsuranceResponse(bool IsSuccessStatusCode, Enumerations.StatusCode StatusCode, object Response)
        {
            string _Body = JsonConvert.SerializeObject(Response);
            CustomerInsuranceResponse _customerInsurance = new CustomerInsuranceResponse();
            var result = _Body;

            if (result.Length > 0)
            {
                var obj = Serializer.DeserializeObject<List<DTO.CustomerInsurance>>(result);
                _customerInsurance.CustomerInsurance = obj;

                return new ExternalResponse
                {
                    IsSuccessStatusCode = IsSuccessStatusCode,
                    StatusCode = (int) StatusCode,
                    Body = JsonConvert.SerializeObject(_customerInsurance)
                };
            }

            return new ExternalResponse
            {
                IsSuccessStatusCode = IsSuccessStatusCode,
                StatusCode = (int) StatusCode,
                Body = JsonConvert.SerializeObject(_customerInsurance)
            };
        }
    }
}
