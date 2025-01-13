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
  public async Task<IActionResult> Login([FromBody] LoginDtoRequest login)
  {
    var token = await _authSevice.SignIn(login);

    return Ok(new { access_token = token });
  }
}