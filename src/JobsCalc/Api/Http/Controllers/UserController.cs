using JobsCalc.Api.Http.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace JobsCalc.Api.Http.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
  [HttpPost]
  public IActionResult AddUser([FromBody] UserDtoRequest user)
  {
    return Created("", user);
  }
}