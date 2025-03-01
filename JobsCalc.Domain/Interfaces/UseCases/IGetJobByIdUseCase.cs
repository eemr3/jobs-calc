using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Communication.DTOs.Responses;
using JobsCalc.Domain.Entities;

namespace JobsCalc.Domain.Interfaces.UseCases;

public interface IGetJobByIdUseCase
{
  public Task<JobEntity> Execute(string jobId);
}