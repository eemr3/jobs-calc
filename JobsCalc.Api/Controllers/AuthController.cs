using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Communication.DTOs.Responses;
using JobsCalc.Domain.Interfaces.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace JobsCalc.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly IAuthUseCase _authUseCase;

  public AuthController(IAuthUseCase authUseCase)
  {
    _authUseCase = authUseCase;
  }

  [HttpPost("login")]
  [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ErrorMessagesResponse), StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(typeof(ErrorMessagesResponse), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<LoginResponse>> SignIn([FromBody] LoginRequest request)
  {
    var token = await _authUseCase.Execute(request);

    return Ok(token);
  }
}