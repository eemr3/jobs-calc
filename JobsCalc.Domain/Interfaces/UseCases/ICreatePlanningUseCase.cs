using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Domain.Entities;

namespace JobsCalc.Domain.Interfaces.UseCases;

public interface ICreatePlanningUseCase
{
  public Task<PlanningEntity> AddPlanningAsync(int userId, PlanningRequest request);
}