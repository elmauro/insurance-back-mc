using MC.Insurance.DTO;
using MC.Insurance.Interfaces.Application;
using MC.Insurance.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<InsuranceManagementController> _logger;
        private readonly ISplunkLogger splunkLogger;

        IInsuranceManagementService insuranceManagementService { get; set; }

        public InsuranceManagementController(
            ILogger<InsuranceManagementController> logger,
            IInsuranceManagementService insuranceManagementService,
            ISplunkLogger splunkLogger
        )
        {
            _logger = logger;
            this.insuranceManagementService = insuranceManagementService;
            this.splunkLogger = splunkLogger;
        }

        [HttpGet]
		[Route("/insurances/{insuranceId}")]
		public async Task<IActionResult> getInsurance([FromRoute] int insuranceId)
		{
            try
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                splunkLogger.LogInformation(environment);

                ExternalResponse httpResponse = await insuranceManagementService.GetInsurance(insuranceId);

                if (httpResponse.IsSuccessStatusCode)
                {
                    Insurance insurance = JsonConvert.DeserializeObject<Insurance>(httpResponse.Body);
                    
                    _logger.LogInformation("Logging Insurance status {status} for {insurance}", httpResponse.StatusCode, httpResponse.Body);
                    splunkLogger.LogInformation("Logging Insurance status {status} for {insurance}", httpResponse.StatusCode, httpResponse.Body);
                    
                    return await CreateResponseWithCode(insurance, httpResponse.StatusCode);
                }
                else
                {
                    var result = httpResponse.Body;
                    var obj = JsonConvert.DeserializeObject<dynamic>(result);

                    return await CreateResponseWithCode(obj, httpResponse.StatusCode);
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

                    return await CreateResponseWithCode(insurances.Insurances, httpResponse.StatusCode);
                }
                else
                {
                    var result = httpResponse.Body;
                    var obj = JsonConvert.DeserializeObject<dynamic>(result);

                    return await CreateResponseWithCode(obj, httpResponse.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return await CreateErrorMessageForException(ex);
            }
        }

        [HttpPost]
        [Route("/insurances")]
        public async Task<IActionResult> CreateInsurance([FromBody] object content)
        {
            try
            {
                ExternalResponse httpResponse = await insuranceManagementService.CreateInsurance(content);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = httpResponse.Body;
                    var obj = JsonConvert.DeserializeObject<dynamic>(result);

                    return await CreateResponseWithCode(obj, httpResponse.StatusCode);
                }
                else
                {
                    var result = httpResponse.Body;
                    var obj = JsonConvert.DeserializeObject<dynamic>(result);

                    return await CreateResponseWithCode(obj, httpResponse.StatusCode);
                }

            }
            catch (Exception ex)
            {
                splunkLogger.LogError("{message} - {inner}", ex.Message, ex.InnerException.ToString());
                return await CreateErrorMessageForException(ex);
            }
        }

        [HttpPut]
        [Route("/insurances/{insuranceId}")]
        public async Task<IActionResult> UpdateInsurance([FromRoute] int insuranceId, [FromBody] object content)
        {
            try
            {
                ExternalResponse httpResponse = await insuranceManagementService.UpdateInsurance(insuranceId, content);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = httpResponse.Body;
                    var obj = JsonConvert.DeserializeObject<dynamic>(result);

                    return await CreateResponseWithCode(obj, httpResponse.StatusCode);
                }
                else
                {
                    var result = httpResponse.Body;
                    var obj = JsonConvert.DeserializeObject<dynamic>(result);

                    return await CreateResponseWithCode(obj, httpResponse.StatusCode);
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

                    return await CreateResponseWithCode(obj, httpResponse.StatusCode);
                }
                else
                {
                    var result = httpResponse.Body;
                    var obj = JsonConvert.DeserializeObject<dynamic>(result);

                    return await CreateResponseWithCode(obj, httpResponse.StatusCode);
                }

            }
            catch (Exception ex)
            {
                return await CreateErrorMessageForException(ex);
            }
        }

        [HttpGet]
        [Route("/customers/{document}/insurances")]
        public async Task<IActionResult> getCustomer([FromRoute] string document)
        {
            try
            {
                ExternalResponse httpResponse = await insuranceManagementService.GetCustomerInsurances(document);

                if (httpResponse.IsSuccessStatusCode)
                {
                    CustomerInsuranceResponse customer = JsonConvert.DeserializeObject<CustomerInsuranceResponse>(httpResponse.Body);

                    return await CreateResponseWithCode(customer.CustomerInsurance, httpResponse.StatusCode);
                }
                else
                {
                    var result = httpResponse.Body;
                    var obj = JsonConvert.DeserializeObject<dynamic>(result);

                    return await CreateResponseWithCode(obj, httpResponse.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return await CreateErrorMessageForException(ex);
            }
        }

        [HttpGet]
        [Route("/customers")]
        public async Task<IActionResult> getCustomers([FromRoute] string document)
        {
            try
            {
                ExternalResponse httpResponse = await insuranceManagementService.GetCustomers();

                if (httpResponse.IsSuccessStatusCode)
                {
                    CustomerResponses customers = JsonConvert.DeserializeObject<CustomerResponses>(httpResponse.Body);

                    return await CreateResponseWithCode(customers.Customers, httpResponse.StatusCode);
                }
                else
                {
                    var result = httpResponse.Body;
                    var obj = JsonConvert.DeserializeObject<dynamic>(result);

                    return await CreateResponseWithCode(obj, httpResponse.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return await CreateErrorMessageForException(ex);
            }
        }

        [HttpPost]
        [Route("/customers/{document}/insurances")]
        public async Task<IActionResult> CreateCustomerInsurance([FromRoute]string document, [FromBody] object content)
        {
            try
            {
                ExternalResponse httpResponse = await insuranceManagementService.CreateCustomerInsurance(document, content);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = httpResponse.Body;
                    var obj = JsonConvert.DeserializeObject<dynamic>(result);

                    return await CreateResponseWithCode(obj, httpResponse.StatusCode);
                }
                else
                {
                    var result = httpResponse.Body;
                    var obj = JsonConvert.DeserializeObject<dynamic>(result);

                    return await CreateResponseWithCode(obj, httpResponse.StatusCode);
                }

            }
            catch (Exception ex)
            {
                return await CreateErrorMessageForException(ex);
            }
        }

        [HttpDelete]
        [Route("customers/{document}/insurances/{insuranceId}")]
        public async Task<IActionResult> DeleteCustomerInsurance([FromRoute] string document, int insuranceId)
        {
            try
            {
                ExternalResponse httpResponse = await insuranceManagementService.DeleteCustomerInsurance(document, insuranceId);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = httpResponse.Body;
                    var obj = JsonConvert.DeserializeObject<dynamic>(result);

                    return await CreateResponseWithCode(obj, httpResponse.StatusCode);
                }
                else
                {
                    var result = httpResponse.Body;
                    var obj = JsonConvert.DeserializeObject<dynamic>(result);

                    return await CreateResponseWithCode(obj, httpResponse.StatusCode);
                }

            }
            catch (Exception ex)
            {
                return await CreateErrorMessageForException(ex);
            }
        }
    }
}
