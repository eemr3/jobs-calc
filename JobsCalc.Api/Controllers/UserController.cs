using JobsCalc.Api.Filters;
using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Communication.DTOs.Responses;
using JobsCalc.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobsCalc.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
  private readonly IRegisterUseCase _registerUseCase;

  public UserController(IRegisterUseCase registerUseCase)
  {
    _registerUseCase = registerUseCase;
  }

  [HttpPost]
  [ProducesResponseType(typeof(ErrorMessagesResponse), StatusCodes.Status409Conflict)]
  [ProducesResponseType(typeof(ErrorMessagesResponse), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Register([FromBody] UserRequest request)
  {
    var user = await _registerUseCase.ExecuteAsync(request);
    return Ok(user);
  }
}