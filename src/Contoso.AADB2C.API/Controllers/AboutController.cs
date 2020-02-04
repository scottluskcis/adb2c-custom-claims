using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Contoso.AADB2C.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AboutController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName();
            var name = assemblyName.Name;
            var version = assemblyName.Version.ToString();

            var info = new 
            { 
                name = name,
                version = version
            };
            
            return Ok(info);
        }
    }
}