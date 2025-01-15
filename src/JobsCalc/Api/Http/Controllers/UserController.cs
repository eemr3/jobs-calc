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
  public async Task<IActionResult> AddUser([FromBody] UserDtoRequest user)
  {

    var userCreated = await _service.AddUserAsync(user);

    return Created("", userCreated);
  }

  [HttpPut("upload-avatar")]
  [Authorize]
  public async Task<IActionResult> UploadAvatar([FromForm] UploadFileDto file)
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var userId = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

    string avatarUrl = await _uploadService.UploadAvatarAsync(int.Parse(userId!), file);

    return Ok(new { message = "Avatar updated successfully.", avatar_url = avatarUrl });
  }

  [HttpPut("me/update")]
  [Authorize]
  public async Task<IActionResult> UpdateUser([FromBody] UserPatchDto userPatch)
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var userId = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
    var userUpdated = await _service.UpdateUserAsync(int.Parse(userId!), userPatch);

    return Ok(userUpdated);
  }

  [HttpGet("me")]
  [Authorize]
  public async Task<IActionResult> GetMe()
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var email = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

    var user = await _service.GetUserByEmail(email!);

    return Ok(user);
  }
}