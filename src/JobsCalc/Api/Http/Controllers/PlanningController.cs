using System.Security.Claims;
using JobsCalc.Api.Application.Services.PlanningService;
using JobsCalc.Api.Domain.Entities;
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
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResponse))]
  public async Task<ActionResult<Planning>> CreatePlanning([FromBody] PlanningDtoRequest planningDto)
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var userId = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

    planningDto.UserId = Convert.ToInt32(userId);

    var planning = await _service.AddPlanningAsync(planningDto);

    return Created("", planning);
  }

  [HttpGet]
  [Authorize]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResponse))]
  public async Task<ActionResult<Planning>> GetPlanningByUserId()
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var userId = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

    var planning = await _service.GetPlanningByUserIdAsync(Convert.ToInt32(userId));

    return Ok(planning);
  }

  [HttpPut]
  [Authorize]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResponse))]
  [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResponse))]
  public async Task<ActionResult<Planning>> UpdatePlanning([FromBody] PlanningUpdateDto plannigDto)
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var userId = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

    plannigDto.UserId = Convert.ToInt32(userId);
    var planning = await _service.UpdatePlanningAsync(plannigDto);
    return Ok(planning);
  }
}