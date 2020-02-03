using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Contoso.AADB2C.API.Models;
using Contoso.AADB2C.API.Services;
using Microsoft.AspNetCore.Http;

namespace Contoso.AADB2C.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly ILogger<IdentityController> _logger;
        private readonly IIdentityService _service;

        public IdentityController(IIdentityService service, ILogger<IdentityController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp()
        {
            try
            {
                var outputClaims = await _service.ProcessAsync(Request);
                return Ok(outputClaims);
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx, $"Error occurred in {nameof(SignUp)}");
                return Conflict(
                    new B2CResponseContent(
                        argEx.Message, 
                        HttpStatusCode.Conflict));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred in {nameof(SignUp)}");
                return StatusCode(
                    StatusCodes.Status500InternalServerError, 
                    new B2CResponseContent(
                        "Unexpected error occured", 
                        HttpStatusCode.InternalServerError));
            }
        }
    }
}