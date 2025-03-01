using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Domain.Entities;

namespace JobsCalc.Domain.Interfaces.UseCases;

public interface ICreatePlanningUseCase
{
  public Task<PlanningEntity> Execute(int userId, PlanningRequest request);
}