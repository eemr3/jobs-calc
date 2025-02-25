using Microsoft.AspNetCore.Mvc;
using JobsCalc.Infrastructure.Persistence;
using JobsCalc.Application;
using JobsCalc.Core.Services;
namespace JobsCalc.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SomaController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() {
            var mult = new Calculate();


            var result = mult.CalculateNumber(4, 8);
            return Ok(result);
        }
    }
}
