using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicProxy.AspNetCore.Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DynamicProxyController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromServices]ISampleService sampleService)
        {
            sampleService.Say("DynamicProxy");
            return Ok();
        }

        [HttpGet("async")]
        public async Task<IActionResult> GetAsync([FromServices] ISampleAsyncService sampleAsyncService)
        {
            await sampleAsyncService.SayAsync("DynamicProxy");
            return Ok();
        }
    }
}
