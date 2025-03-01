using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Communication.DTOs.Responses;

namespace JobsCalc.Domain.Interfaces.UseCases;

public interface IGetJobUseCase
{
  public Task<JobResponse> Execute(string jobId);
}