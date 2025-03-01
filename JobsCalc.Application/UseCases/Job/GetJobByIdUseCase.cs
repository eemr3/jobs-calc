using JobsCalc.Application.Exceptions;
using JobsCalc.Communication.DTOs.Responses;
using JobsCalc.Domain.Entities;
using JobsCalc.Domain.Interfaces.Repositories;
using JobsCalc.Domain.Interfaces.UseCases;

namespace JobsCalc.Application.UseCases.Job;

public class GetJobByIdUseCase :IGetJobByIdUseCase
{
  private readonly IJobRepository _jobRepository;

  public GetJobByIdUseCase(IJobRepository jobRepository)
  {
    _jobRepository = jobRepository;
  }
  
  public async Task<JobEntity> Execute(string jobId)
  {
   var job = await _jobRepository.GetJob(jobId);
   if(job is null) throw new NotFoundException($"Job {jobId} não encontrado");

   return job;
  }
}