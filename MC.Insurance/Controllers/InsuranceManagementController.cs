using MC.Insurance.DTO;
using MC.Insurance.Interfaces.Application;
using MC.Insurance.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
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
                Response httpResponse = await insuranceManagementService.GetInsurance(insuranceId);
                _logger.LogInformation("Logging Insurance status {status} for {insurance}", httpResponse.StatusCode, httpResponse.Body);
                splunkLogger.LogInformation("Logging Insurance status {status} for {insurance}", httpResponse.StatusCode, httpResponse.Body);
                return CreateResponse(httpResponse);
            }
            catch (Exception ex) {
                return CreateErrorMessageForException(ex);
            }
        }

        [HttpGet]
        [Route("/insurances")]
        public async Task<IActionResult> getInsurances()
        {
            try
            {
                Response httpResponse = await insuranceManagementService.GetInsurances();
                return CreateResponse(httpResponse);
            }
            catch (Exception ex)
            {
                return CreateErrorMessageForException(ex);
            }
        }

        [HttpPost]
        [Route("/insurances")]
        public async Task<IActionResult> CreateInsurance(Insurance content)
        {
            try
            {
                Response httpResponse = await insuranceManagementService.CreateInsurance(content);
                return CreateResponse(httpResponse);
            }
            catch (Exception ex)
            {
                splunkLogger.LogError("{message} - {inner}", ex.Message, ex.InnerException.ToString());
                return CreateErrorMessageForException(ex);
            }
        }

        [HttpPut]
        [Route("/insurances/{insuranceId}")]
        public async Task<IActionResult> UpdateInsurance([FromRoute] int insuranceId, Insurance insurance)
        {
            try
            {
                Response httpResponse = await insuranceManagementService.UpdateInsurance(insuranceId, insurance);
                return CreateResponse(httpResponse);
            }
            catch (Exception ex)
            {
                return CreateErrorMessageForException(ex);
            }
        }

        [HttpDelete]
        [Route("insurances/{insuranceId}")]
        public async Task<IActionResult> DeleteInsurance([FromRoute] int insuranceId)
        {
            try
            {
                Response httpResponse = await insuranceManagementService.DeleteInsurance(insuranceId);
                return CreateResponse(httpResponse);
            }
            catch (Exception ex)
            {
                return CreateErrorMessageForException(ex);
            }
        }

        [HttpGet]
        [Route("/customers/{document}/insurances")]
        public async Task<IActionResult> getCustomer([FromRoute] string document)
        {
            try
            {
                Response httpResponse = await insuranceManagementService.GetCustomerInsurances(document);
                return CreateResponse(httpResponse);
            }
            catch (Exception ex)
            {
                return CreateErrorMessageForException(ex);
            }
        }

        [HttpGet]
        [Route("/customers")]
        public async Task<IActionResult> getCustomers()
        {
            try
            {
                Response httpResponse = await insuranceManagementService.GetCustomers();
                return CreateResponse(httpResponse);
            }
            catch (Exception ex)
            {
                return CreateErrorMessageForException(ex);
            }
        }

        [HttpPost]
        [Route("/customers/{document}/insurances")]
        public async Task<IActionResult> CreateCustomerInsurance([FromRoute]string document, CustomerInsurance customerInsurance)
        {
            try
            {
                Response httpResponse = await insuranceManagementService.CreateCustomerInsurance(document, customerInsurance);
                return CreateResponse(httpResponse);
            }
            catch (Exception ex)
            {
                return CreateErrorMessageForException(ex);
            }
        }

        [HttpDelete]
        [Route("customers/{document}/insurances/{insuranceId}")]
        public async Task<IActionResult> DeleteCustomerInsurance([FromRoute] string document, int insuranceId)
        {
            try
            {
                Response httpResponse = await insuranceManagementService.DeleteCustomerInsurance(document, insuranceId);
                return CreateResponse(httpResponse);
            }
            catch (Exception ex)
            {
                return CreateErrorMessageForException(ex);
            }
        }
    }
}
