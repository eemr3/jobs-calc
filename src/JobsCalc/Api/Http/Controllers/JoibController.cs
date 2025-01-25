using System.Security.Claims;
using JobsCalc.Api.Application.Services.JobService;
using JobsCalc.Api.Domain.Entities;
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
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResponse))]
  public async Task<ActionResult<Job>> CreateJob([FromBody] JobDtoRequest jobDto)
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var userId = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

    var job = await _servece.AddJobAsync(int.Parse(userId!), jobDto);

    return Created("", job);
  }

  [HttpGet]
  [Authorize]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResponse))]
  [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResponse))]
  public async Task<ActionResult<IEnumerable<JobDtoResponse>>> GetJobsUser()
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var userId = token?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

    var jobs = await _servece.GetJobsUser(int.Parse(userId!));

    return Ok(jobs);
  }

  [HttpGet("{jobId}")]
  [Authorize]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResponse))]
  [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResponse))]
  public async Task<ActionResult<Job>> GetJobById(string jobId)
  {
    var job = await _servece.GetJob(jobId);

    return Ok(job);
  }

  [HttpPut("{jobId}")]
  [Authorize]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResponse))]
  public async Task<ActionResult<Job>> UpdateJob(string jobId, [FromBody] JobPatchDto jobDto)
  {
    var job = await _servece.UpdateJob(jobId, jobDto);

    return Ok(job);
  }

  [HttpDelete("{jobId}")]
  [Authorize]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResponse))]
  public async Task<IActionResult> DeleteJob(string jobId)
  {
    await _servece.DeleteJob(jobId);

    return NoContent();
  }
}