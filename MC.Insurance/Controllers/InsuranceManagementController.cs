using MC.Insurance.DTO;
using MC.Insurance.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace insurance_back_mc.Controllers
{
    public class InsuranceManagementController : CommonController
	{
		IInsuranceManagementService insuranceManagementService { get; set; }

        public InsuranceManagementController(
            IInsuranceManagementService insuranceManagementService
        )
        {
            this.insuranceManagementService = insuranceManagementService;
        }

        [HttpGet]
		[Route("/insurances/{insuranceId}")]
		public async Task<IActionResult> getInsurance([FromRoute] int insuranceId)
		{
            try
            {
                ExternalResponse httpResponse = await insuranceManagementService.GetInsurance(insuranceId);

                if (httpResponse.IsSuccessStatusCode)
                {
                    Insurance insurance = JsonConvert.DeserializeObject<Insurance>(httpResponse.Body);

                    return await CreateResponseWithCode(insurance, (HttpStatusCode)httpResponse.StatusCode);
                }
                else
                {
                    var result = httpResponse.Body;
                    var obj = JsonConvert.DeserializeObject<dynamic>(result);

                    return await CreateResponseWithCode(obj, (HttpStatusCode)httpResponse.StatusCode);
                }
            }
            catch (Exception ex) {
                return await CreateErrorMessageForException(ex);
            }
        }

        [HttpGet]
        [Route("/insurances")]
        public async Task<IActionResult> getInsurances()
        {
            try
            {
                ExternalResponse httpResponse = await insuranceManagementService.GetInsurances();

                if (httpResponse.IsSuccessStatusCode)
                {
                    InsurancesResponse insurances = JsonConvert.DeserializeObject<InsurancesResponse>(httpResponse.Body);

                    return await CreateResponseWithCode(insurances.Insurances, (HttpStatusCode)httpResponse.StatusCode);
                }
                else
                {
                    var result = httpResponse.Body;
                    var obj = JsonConvert.DeserializeObject<dynamic>(result);

                    return await CreateResponseWithCode(obj, (HttpStatusCode)httpResponse.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return await CreateErrorMessageForException(ex);
            }
        }

        [HttpPost]
        [Route("insurances")]
        public async Task<IActionResult> CreateInsurance([FromBody] object content)
        {
            try
            {
                ExternalResponse httpResponse = await insuranceManagementService.CreateInsurance(content);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = httpResponse.Body;
                    var obj = JsonConvert.DeserializeObject<dynamic>(result);

                    return await CreateResponseWithCode(obj, (HttpStatusCode)httpResponse.StatusCode);
                }
                else
                {
                    var result = httpResponse.Body;
                    var obj = JsonConvert.DeserializeObject<dynamic>(result);

                    return await CreateResponseWithCode(obj, (HttpStatusCode)httpResponse.StatusCode);
                }

            }
            catch (Exception ex)
            {
                return await CreateErrorMessageForException(ex);
            }
        }

        [HttpPut]
        [Route("insurances")]
        public async Task<IActionResult> UpdateInsurance([FromBody] object content)
        {
            try
            {
                ExternalResponse httpResponse = await insuranceManagementService.UpdateInsurance(content);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = httpResponse.Body;
                    var obj = JsonConvert.DeserializeObject<dynamic>(result);

                    return await CreateResponseWithCode(obj, (HttpStatusCode)httpResponse.StatusCode);
                }
                else
                {
                    var result = httpResponse.Body;
                    var obj = JsonConvert.DeserializeObject<dynamic>(result);

                    return await CreateResponseWithCode(obj, (HttpStatusCode)httpResponse.StatusCode);
                }

            }
            catch (Exception ex)
            {
                return await CreateErrorMessageForException(ex);
            }
        }

        [HttpDelete]
        [Route("insurances/{insuranceId}")]
        public async Task<IActionResult> DeleteInsurance([FromRoute] int insuranceId)
        {
            try
            {
                ExternalResponse httpResponse = await insuranceManagementService.DeleteInsurance(insuranceId);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = httpResponse.Body;
                    var obj = JsonConvert.DeserializeObject<dynamic>(result);

                    return await CreateResponseWithCode(obj, (HttpStatusCode)httpResponse.StatusCode);
                }
                else
                {
                    var result = httpResponse.Body;
                    var obj = JsonConvert.DeserializeObject<dynamic>(result);

                    return await CreateResponseWithCode(obj, (HttpStatusCode)httpResponse.StatusCode);
                }

            }
            catch (Exception ex)
            {
                return await CreateErrorMessageForException(ex);
            }
        }
    }
}
