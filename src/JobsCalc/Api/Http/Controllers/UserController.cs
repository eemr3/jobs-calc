using JobsCalc.Api.Application.Exceptions;
using JobsCalc.Api.Application.Services.UserService;
using JobsCalc.Api.Http.Dtos;
using JobsCalc.Api.Infra.Database.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JobsCalc.Api.Http.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
  private readonly IUserService _service;

  public UserController(IUserService service)
  {
    _service = service;
  }

  [HttpPost]
  public async Task<IActionResult> AddUser([FromBody] UserDtoRequest user)
  {
    try
    {
      var userCreated = await _service.AddUserAsync(user);

      return Created("", userCreated);

    }
    catch (ConflictException ex)
    {
      return Conflict(new { message = ex.Message });
    }
  }
}