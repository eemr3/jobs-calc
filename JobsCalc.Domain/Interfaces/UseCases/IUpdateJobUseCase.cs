using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Domain.Entities;

namespace JobsCalc.Domain.Interfaces.UseCases;

public interface IUpdateJobUseCase
{
  public Task<JobEntity> Execute(string jobId, JobUpdateRequest request);
}