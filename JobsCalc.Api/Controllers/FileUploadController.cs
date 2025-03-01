using System.Security.Claims;
using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Communication.DTOs.Responses;
using JobsCalc.Domain.Interfaces.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobsCalc.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class FileUploadController : ControllerBase
{
  private readonly IFileUploadUseCase _fileUploadUseCase;

  public FileUploadController(IFileUploadUseCase fileUploadUseCase)
  {
    _fileUploadUseCase = fileUploadUseCase;
  }

  [HttpPost("avatar")]
  [Authorize]
  [ProducesResponseType(typeof(FileUploadResponse), StatusCodes.Status200OK, "image/jpeg")]
  public async Task<IActionResult> UploadAvatar(IFormFile file)
  {
    var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
    await using var fileStream = file.OpenReadStream();
    var request = new FileUploadRequest
    {
      FileName = file.FileName,
      FileStream = fileStream,
    };
    
    var result = await _fileUploadUseCase.Execute(int.Parse(userId),request);
    
    return Ok(result);
  }
}