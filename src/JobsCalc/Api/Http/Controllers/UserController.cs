using System.Security.Claims;
using JobsCalc.Api.Application.Services.UserService;
using JobsCalc.Api.Http.Dtos;
using JobsCalc.Api.Http.Filters;
using JobsCalc.Api.Infra.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobsCalc.Api.Http.Controllers;

[ApiController]
[Route("api/v1/users")]
[TypeFilter(typeof(CustomExceptionFilter))]
public class UserController : ControllerBase
{
  private readonly IUserService _service;
  private readonly IUploadService _uploadService;
  public UserController(IUserService service, IUploadService uploadService)
  {
    _service = service;
    _uploadService = uploadService;
  }


  [HttpPost]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ConflictResponse))]
  public async Task<ActionResult<UserDtoResponse>> AddUser([FromBody] UserDtoRequest user)
  {

    var userCreated = await _service.AddUserAsync(user);

    return Created("", userCreated);
  }

  [HttpPut("upload-avatar")]
  [Authorize]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResponse))]
  public async Task<ActionResult<UploadAvatarDto>> UploadAvatar([FromForm] UploadFileDto file)
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var userId = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

    string avatarUrl = await _uploadService.UploadAvatarAsync(int.Parse(userId!), file);

    return Ok(new { message = "Avatar updated successfully.", avatar_url = avatarUrl });
  }

  [HttpPut("me/update")]
  [Authorize]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResponse))]
  public async Task<ActionResult<UserDtoResponse>> UpdateUser([FromBody] UserPatchDto userPatch)
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var userId = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
    var userUpdated = await _service.UpdateUserAsync(int.Parse(userId!), userPatch);

    return Ok(userUpdated);
  }

  [HttpGet("me")]
  [Authorize]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResponse))]
  public async Task<ActionResult<UserDtoResponse>> GetMe()
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var email = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

    var user = await _service.GetUserByEmail(email!);

    return Ok(user);
  }
}