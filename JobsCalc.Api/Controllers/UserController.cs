using System.Security.Claims;
using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Communication.DTOs.Responses;
using JobsCalc.Domain.Interfaces.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobsCalc.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
  private readonly IUserRegisterUseCase _userRegisterUseCase;
  private readonly IGetUserByIdUseCase _getUserUseCase;

  public UserController(IUserRegisterUseCase userRegisterUseCase, IGetUserByIdUseCase getUserUseCase)
  {
    _userRegisterUseCase = userRegisterUseCase;
    _getUserUseCase = getUserUseCase;
  }

  [HttpPost]
  [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ErrorMessagesResponse), StatusCodes.Status409Conflict)]
  [ProducesResponseType(typeof(ErrorMessagesResponse), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Register([FromBody] UserRequest request)
  {
    var user = await _userRegisterUseCase.ExecuteAsync(request);
    return Ok(user);
  }

  [HttpGet("me")]
  [Authorize]
  [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ErrorMessagesResponse), StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(typeof(ErrorMessagesResponse), StatusCodes.Status404NotFound)]
  public async Task<ActionResult<UserResponse>> GetUserById()
  {
    var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
    var user = await _getUserUseCase.Execute(int.Parse(userId));

    return Ok(user);
  }
}