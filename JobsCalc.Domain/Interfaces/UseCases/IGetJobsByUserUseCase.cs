using JobsCalc.Communication.DTOs.Responses;

namespace JobsCalc.Domain.Interfaces.UseCases;

public interface IGetJobsByUserUseCase
{
  public Task<IEnumerable<JobResponse>> Execute(int userId);
}