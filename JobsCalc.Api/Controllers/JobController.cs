using System.Security.Claims;
using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Communication.DTOs.Responses;
using JobsCalc.Domain.Entities;
using JobsCalc.Domain.Interfaces.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobsCalc.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobController : ControllerBase
{
  private readonly ICreateJobUseCase _createJobUseCase;
  private readonly IGetJobsByUserUseCase _getJobsByUserUseCase;
  private readonly IGetJobByIdUseCase _getJobByIdUseCase;
  private readonly IUpdateJobUseCase _updateJobUseCase;
  private readonly IDeleteJobUseCase _deleteJobUseCase;
  public JobController(ICreateJobUseCase createJobUseCase, 
    IGetJobsByUserUseCase getJobsByUserUseCase,  IGetJobByIdUseCase getJobByIdUseCase, 
    IUpdateJobUseCase updateJobUseCase, IDeleteJobUseCase deleteJobUseCase)
  {
    _createJobUseCase = createJobUseCase;
    _getJobsByUserUseCase = getJobsByUserUseCase;
    _getJobByIdUseCase = getJobByIdUseCase;
    _updateJobUseCase = updateJobUseCase;
    _deleteJobUseCase = deleteJobUseCase;
  }
  
  
  [HttpPost]
  [Authorize]
  [ProducesResponseType(typeof(JobEntity),StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ErrorMessagesResponse),StatusCodes.Status401Unauthorized)]
  public async Task<ActionResult<JobEntity>> CreateJob([FromBody] JobRequest request)
  {
    var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
    request.UserId = int.Parse(userId);
    
    var result = await _createJobUseCase.Execute(request);
    
    return Created("", result);
  }

  [HttpGet]
  [Authorize]
  [ProducesResponseType(typeof(List<JobResponse>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ErrorMessagesResponse), StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(typeof(ErrorMessagesResponse), StatusCodes.Status404NotFound)]
  public async Task<ActionResult<List<JobResponse>>> GetJobs()
  {
    var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
    var result = await _getJobsByUserUseCase.Execute(int.Parse(userId));
    
    return Ok(result);
  }

  [HttpGet("{jobId}")]
  [Authorize]
  [ProducesResponseType(typeof(List<JobEntity>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ErrorMessagesResponse), StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(typeof(ErrorMessagesResponse), StatusCodes.Status404NotFound)]
  public async Task<ActionResult<JobEntity>> GetJobsByUser(string jobId)
  {
    var result = await _getJobByIdUseCase.Execute(jobId);

    return Ok(result);
  }

  [HttpPut("{jobId}")]
  [Authorize]
  [ProducesResponseType(typeof(JobEntity), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ErrorMessagesResponse), StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(typeof(ErrorMessagesResponse), StatusCodes.Status404NotFound)]
  public async Task<ActionResult<JobEntity>> UpdateJob(string jobId, [FromBody] JobUpdateRequest request)
  {
    var result = await _updateJobUseCase.Execute(jobId, request);
    
    return Ok(result);
  }

  [HttpDelete("{jobId}")]
  [Authorize]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ErrorMessagesResponse), StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(typeof(ErrorMessagesResponse), StatusCodes.Status404NotFound)]
  public async Task<IActionResult> DeleteJob(string jobId)
  {
    await _deleteJobUseCase.Execute(jobId);
    return NoContent();
  }
}