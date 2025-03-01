
using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Communication.DTOs.Responses;

namespace JobsCalc.Domain.Interfaces.UseCases;

public interface ICreateJobUseCase
{
  public Task<JobResponse> Execute(JobRequest request);
}