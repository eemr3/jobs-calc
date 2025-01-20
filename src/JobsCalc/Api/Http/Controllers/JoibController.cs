using System.Security.Claims;
using JobsCalc.Api.Application.Services.JobService;
using JobsCalc.Api.Http.Dtos;
using JobsCalc.Api.Http.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobsCalc.Api.Http.Controllers;

[ApiController]
[Route("api/v1/jobs")]
[TypeFilter(typeof(CustomExceptionFilter))]
public class JoibController : ControllerBase
{
  private readonly IJobService _servece;

  public JoibController(IJobService service)
  {
    _servece = service;
  }

  [HttpPost]
  [Authorize]
  public async Task<IActionResult> CreateJob([FromBody] JobDtoRequest jobDto)
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var userId = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

    var job = await _servece.AddJobAsync(int.Parse(userId!), jobDto);

    return Created("", job);
  }

  [HttpGet]
  [Authorize]
  public async Task<IActionResult> GetJobsUser()
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var userId = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

    var jobs = await _servece.GetJobsUser(int.Parse(userId!));

    return Ok(jobs);
  }

  [HttpGet("{jobId}")]
  [Authorize]
  public async Task<IActionResult> GetJobById(string jobId)
  {
    var job = await _servece.GetJob(jobId);

    return Ok(job);
  }

  [HttpPut("{jobId}")]
  [Authorize]
  public async Task<IActionResult> UpdateJob(string jobId, [FromBody] JobPatchDto jobDto)
  {
    var job = await _servece.UpdateJob(jobId, jobDto);

    return Ok(job);
  }

  [HttpDelete("{jobId}")]
  [Authorize]
  public async Task<IActionResult> DeleteJob(string jobId)
  {
    await _servece.DeleteJob(jobId);

    return NoContent();
  }
}