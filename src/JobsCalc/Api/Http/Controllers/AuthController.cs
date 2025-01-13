using JobsCalc.Api.Application.Services.AuthService;
using JobsCalc.Api.Application.Services.JWT;
using JobsCalc.Api.Http.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace JobsCalc.Api.Http.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{

  private readonly IAuthSevice _authSevice;

  public AuthController(IAuthSevice service)
  {
    _authSevice = service;
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginRequest login)
  {
    var result = await _authSevice.SigIn(login);

    return Ok(new { access_token = result.Token });
  }
}