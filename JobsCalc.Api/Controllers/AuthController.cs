using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Domain.Interfaces;
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
  public async Task<IActionResult> SignIn([FromBody] LoginRequest request)
  {
    var token = await _authUseCase.Execute(request);

    return Ok(token);
  }
}