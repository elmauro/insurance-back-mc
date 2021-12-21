using MC.Insurance.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MC.Insurance.Interfaces.Infrastructure
{
    public interface IServiceResponse
    {
        ExternalResponse CreateResponse(bool IsSuccessStatusCode, Enumerations.StatusCode StatusCode, object Response);
        ExternalResponse CreateInsurancesResponse(bool IsSuccessStatusCode, Enumerations.StatusCode StatusCode, object Response);
        ExternalResponse CreateCustomersResponse(bool IsSuccessStatusCode, Enumerations.StatusCode StatusCode, object Response);
        ExternalResponse CreateCustomersInsuranceResponse(bool IsSuccessStatusCode, Enumerations.StatusCode StatusCode, object Response);
    }
}
