using JobsCalc.Api.Application.Services.AuthService;
using JobsCalc.Api.Application.Services.JWT;
using JobsCalc.Api.Http.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace JobsCalc.Api.Http.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{

  private readonly IAuthService _authSevice;

  public AuthController(IAuthService service)
  {
    _authSevice = service;
  }

  [HttpPost("login")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResponse))]
  public async Task<ActionResult<LoginDtoResponse>> Login([FromBody] LoginDtoRequest login)
  {
    var token = await _authSevice.SignIn(login);

    return Ok(new { access_token = token });
  }
}