using MC.Insurance.DTO;
using MC.Insurance.Interfaces.Application;
using MC.Insurance.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace insurance_back_mc.Controllers
{
    public class InsuranceManagementController : CommonController
	{
        private readonly ILogger<InsuranceManagementController> _logger;

        IInsuranceManagementService insuranceManagementService { get; set; }

        public InsuranceManagementController(
            ILogger<InsuranceManagementController> logger,
            IInsuranceManagementService insuranceManagementService
        )
        {
            _logger = logger;
            this.insuranceManagementService = insuranceManagementService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Route("/insurances/{insuranceId}")]
		public async Task<IActionResult> getInsurance([FromRoute] int insuranceId)
		{
            Response httpResponse = await insuranceManagementService.GetInsurance(insuranceId);
            _logger.LogInformation("Logging Insurance status {status} for {insurance}", httpResponse.StatusCode, httpResponse.Body);
                
            return CreateResponse(httpResponse);
        }

        [HttpGet]
        [Route("/countries")]
        public async Task<IActionResult> getInsurances()
        {
            Response httpResponse = new Response(); //await insuranceManagementService.GetInsurances();
            httpResponse.StatusCode = 200;
            var Body = new {
                insuranceId = 1,
                name = "Incendios A1",
                description = "Seguro de Incendios",
                type = 2,
                coverage = "50%",
                start = "2000-09-11T00:00:00",
                period = 12,
                price = 200000,
                risk = 4
            };
            ArrayList arr = new ArrayList();
            arr.Add(Body);
            httpResponse.Body = arr;
            return CreateResponse(httpResponse);
        }

        [HttpPost]
        [Route("/insurances")]
        public async Task<IActionResult> CreateInsurance([FromBody]Insurance content)
        {
            Response httpResponse = await insuranceManagementService.CreateInsurance(content);
            return CreateResponse(httpResponse);
        }

        [HttpPut]
        [Route("/insurances/{insuranceId}")]
        public async Task<IActionResult> UpdateInsurance([FromRoute]int insuranceId, [FromBody]Insurance insurance)
        {
            Response httpResponse = await insuranceManagementService.UpdateInsurance(insuranceId, insurance);
            return CreateResponse(httpResponse);
        }

        [HttpDelete]
        [Route("insurances/{insuranceId}")]
        public async Task<IActionResult> DeleteInsurance([FromRoute] int insuranceId)
        {
            Response httpResponse = await insuranceManagementService.DeleteInsurance(insuranceId);
            return CreateResponse(httpResponse);
        }

        [HttpGet]
        [Route("/customers/{document}/insurances")]
        public async Task<IActionResult> getCustomer([FromRoute] string document)
        {
            Response httpResponse = await insuranceManagementService.GetCustomerInsurances(document);
            return CreateResponse(httpResponse);
        }

        [HttpGet]
        [Route("/customers")]
        public async Task<IActionResult> getCustomers()
        {
            Response httpResponse = await insuranceManagementService.GetCustomers();
            return CreateResponse(httpResponse);
        }

        [HttpPost]
        [Route("/customers/{document}/insurances")]
        public async Task<IActionResult> CreateCustomerInsurance([FromRoute]string document, [FromBody]CustomerInsurance customerInsurance)
        {
            Response httpResponse = await insuranceManagementService.CreateCustomerInsurance(document, customerInsurance);
            return CreateResponse(httpResponse);
        }

        [HttpDelete]
        [Route("customers/{document}/insurances/{insuranceId}")]
        public async Task<IActionResult> DeleteCustomerInsurance([FromRoute] string document, int insuranceId)
        {
            Response httpResponse = await insuranceManagementService.DeleteCustomerInsurance(document, insuranceId);
            return CreateResponse(httpResponse);
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login([FromBody]UserLogin loginUser)
        {
            Response httpResponse = await insuranceManagementService.Login(loginUser.userName, loginUser.password);
            Response jwtResponse = await insuranceManagementService.CreateTokenJWT((User)httpResponse.Body);
            jwtResponse.Body = new { token = jwtResponse.Body };

            return CreateResponse(jwtResponse);
        }
    }
}
