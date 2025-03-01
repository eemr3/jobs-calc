
using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Domain.Entities;

namespace JobsCalc.Domain.Interfaces.UseCases;

public interface ICreateJobUseCase
{
  public Task<JobEntity> Execute(JobRequest request);
}