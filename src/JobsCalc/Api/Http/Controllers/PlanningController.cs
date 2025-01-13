using System.Security.Claims;
using JobsCalc.Api.Application.Services.PlanningService;
using JobsCalc.Api.Http.Dtos;
using JobsCalc.Api.Http.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobsCalc.Api.Http.Controllers;

[ApiController]
[Route("api/v1/profile")]
[TypeFilter(typeof(CustomExceptionFilter))]
public class PlanningController : ControllerBase
{
  private readonly IPlanningService _service;

  public PlanningController(IPlanningService service)
  {
    _service = service;
  }

  [HttpPost]
  [Authorize]
  public async Task<IActionResult> CreatePlanning([FromBody] PlanningDtoRequest planningDto)
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var userId = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

    planningDto.UserId = Convert.ToInt32(userId);

    var planning = await _service.AddPlanningAsync(planningDto);

    return Created("", planning);
  }

  [HttpGet]
  [Authorize]
  public async Task<IActionResult> GetPlanningByUserId()
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var userId = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

    var planning = await _service.GetPlanningByUserIdAsync(Convert.ToInt32(userId));

    return Ok(planning);
  }

  [HttpPut]
  [Authorize]
  public async Task<IActionResult> UpdatePlanning([FromBody] PlanningUpdateDto plannigDto)
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var userId = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

    plannigDto.UserId = Convert.ToInt32(userId);
    var planning = await _service.UpdatePlanningAsync(plannigDto);
    return Ok(planning);
  }
}