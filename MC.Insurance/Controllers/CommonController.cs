using MC.Insurance.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace insurance_back_mc.Controllers
{
	public class CommonController: Controller
	{
        public async Task<IActionResult> CreateResponseWithCode(object response, HttpStatusCode statusCode)
        {
            return StatusCode((int)statusCode, response);
        }

        public async Task<IActionResult> CreateErrorMessageForException(Exception ex)
        {
            var error = new
            {
                exception = ex
            };

            // LLamamos a Splunk para almacenar los datos a Splunk
           ErrorMessage errorMessage = new ErrorMessage() { resultMsg = ex.Message };
            return StatusCode(500, errorMessage.resultMsg);
        }
    }
}
