using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Domain.Entities;
using JobsCalc.Domain.Interfaces.Repositories;
using JobsCalc.Domain.Interfaces.UseCases;

namespace JobsCalc.Application.UseCases.Job;

public class CreateJobUseCase : ICreateJobUseCase
{
  private readonly IJobRepository _jobRepository;

  public CreateJobUseCase(IJobRepository jobRepository)
  {
    _jobRepository = jobRepository;
  }
  
  public async Task<JobEntity> Execute(JobRequest request)
  {
    var newJob = new JobEntity
    {
      Name = request.Name,
      DailyHours = request.DailyHours,
      TotalHours = request.TotalHours,
      UserId = request.UserId
    };
    
    var result = await _jobRepository.AddJobAsync(newJob);
    
    return result;
  }
}