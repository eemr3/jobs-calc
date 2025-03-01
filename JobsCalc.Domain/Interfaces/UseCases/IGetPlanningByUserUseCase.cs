using JobsCalc.Domain.Entities;

namespace JobsCalc.Domain.Interfaces.UseCases;

public interface IGetPlanningByUserUseCase
{
  public Task<PlanningEntity> Execute(int userId);
}