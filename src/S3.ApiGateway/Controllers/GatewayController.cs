using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using S3.ApiGateway.Models;

namespace S3.ApiGateway.Controllers
{
    [Route("")]
    public class GatewayController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Gateway service running...");
    }
}
