using MC.Insurance.DTO;
using MC.Insurance.Interfaces.Application;
using MC.Insurance.Interfaces.Domain;
using MC.Insurance.Interfaces.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MC.Insurance.ApplicationServices
{
	public class InsuranceFormatInputOutput: IInsuranceFormatInputOutput
	{
		public IInsuranceDomain InsuranceDomain { get; set; }
		public ISerializer Serializer { get; set; }

		public InsuranceFormatInputOutput(
			IInsuranceDomain InsuranceDomain,
			ISerializer Serializer)
		{
			this.InsuranceDomain = InsuranceDomain;
			this.Serializer = Serializer;
		}

		public ExternalResponse GetInsuranceFormatted(ExternalResponse httpResponse)
		{
            var result = httpResponse.Body;
            var obj = Serializer.DeserializeObject<dynamic>(result);

            if (obj != null)
            {
                DTO.Insurance insurance = Serializer.DeserializeObject<DTO.Insurance>(result);

                return new ExternalResponse
                {
                    IsSuccessStatusCode = httpResponse.IsSuccessStatusCode,
                    StatusCode = httpResponse.StatusCode,
                    Body = JsonConvert.SerializeObject(insurance)
                };
            }

            return new ExternalResponse
            {
                IsSuccessStatusCode = httpResponse.IsSuccessStatusCode,
                StatusCode = httpResponse.StatusCode,
                Body = httpResponse.Body
            };
        }

        public ExternalResponse GetInsurancesFormatted(ExternalResponse httpResponse)
        {
            if (httpResponse.IsSuccessStatusCode)
            {
                InsurancesResponse _insurances = new InsurancesResponse();

                var result = httpResponse.Body;
                if (result.Length > 0)
                {
                    var obj = Serializer.DeserializeObject<List<DTO.Insurance>>(result);
                    _insurances.Insurances = obj;

                    return new ExternalResponse
                    {
                        IsSuccessStatusCode = httpResponse.IsSuccessStatusCode,
                        StatusCode = httpResponse.StatusCode,
                        Body = JsonConvert.SerializeObject(_insurances)
                    };
                }

                return new ExternalResponse
                {
                    IsSuccessStatusCode = httpResponse.IsSuccessStatusCode,
                    StatusCode = httpResponse.StatusCode,
                    Body = JsonConvert.SerializeObject(_insurances)
                };
            }

            return httpResponse;
        }
    }
}
