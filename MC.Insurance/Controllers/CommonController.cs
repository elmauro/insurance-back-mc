﻿using MC.Insurance.DTO;
using MC.Insurance.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace insurance_back_mc.Controllers
{
    public class CommonController: Controller
	{
        public CommonController() { }
        public IActionResult CreateResponse(Response response)
        {
            return StatusCode(response.StatusCode, response.Body);
        }
    }
}
