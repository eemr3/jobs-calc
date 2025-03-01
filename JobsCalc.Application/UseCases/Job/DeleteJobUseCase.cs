using JobsCalc.Application.Exceptions;
using JobsCalc.Domain.Interfaces.Repositories;
using JobsCalc.Domain.Interfaces.UseCases;

namespace JobsCalc.Application.UseCases.Job;

public class DeleteJobUseCase : IDeleteJobUseCase
{
  private readonly IJobRepository _jobRepository;

  public DeleteJobUseCase(IJobRepository jobRepository)
  {
    _jobRepository = jobRepository;
  }
  
  public async Task Execute(string jobId)
  {
    var job = await _jobRepository.GetJob(jobId);
    if(job is null) throw new NotFoundException($"Job com ID {jobId} não encontrado");
    
    await _jobRepository.DeleteJob(job);
  }
}