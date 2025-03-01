using System.Security.Claims;
using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Communication.DTOs.Responses;
using JobsCalc.Domain.Entities;
using JobsCalc.Domain.Interfaces.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobsCalc.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlanningController : ControllerBase
{
  private readonly ICreatePlanningUseCase _createPlanningUseCase;
  private readonly IGetPlanningByUserUseCase _getPlanningByUserUseCase;
  private readonly IUpdatePlanningUseCase _updatePlanningUseCase;

  public PlanningController(ICreatePlanningUseCase createPlanningUseCase, 
      IGetPlanningByUserUseCase getPlanningByUserUseCase, 
      IUpdatePlanningUseCase updatePlanningUseCase)
  {
    _createPlanningUseCase = createPlanningUseCase;
    _getPlanningByUserUseCase = getPlanningByUserUseCase;
    _updatePlanningUseCase = updatePlanningUseCase;
  }
  
  [HttpPost]
  [Authorize]
  [ProducesResponseType(typeof(PlanningEntity),StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ErrorMessagesResponse),StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(typeof(ErrorMessagesResponse),StatusCodes.Status409Conflict)]
  public async Task<ActionResult<PlanningRequest>> CreatePlanning([FromBody] PlanningRequest request)
  {
    var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

    var result = await _createPlanningUseCase.Execute(int.Parse(userId), request);
    
    return Created("", result);
  }

  [HttpGet]
  [Authorize]
  [ProducesResponseType(typeof(PlanningEntity), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ErrorMessagesResponse), StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(typeof(ErrorMessagesResponse), StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetPlannings()
  {
    var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
    var result = await _getPlanningByUserUseCase.Execute(int.Parse(userId));
    
    return Ok(result);
  }

  [HttpPut]
  [Authorize]
  [ProducesResponseType(typeof(PlanningEntity), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ErrorMessagesResponse), StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(typeof(ErrorMessagesResponse), StatusCodes.Status404NotFound)]
  public async Task<ActionResult<PlanningUpdateRequest>> UpdatePlanning([FromBody] PlanningUpdateRequest request)
  {
    var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
    request.UserId = int.Parse(userId);
    var result = await _updatePlanningUseCase.Execute(request);
    
    return Ok(result);
  }
}