using JobsCalc.Domain.Entities;

namespace JobsCalc.Domain.Interfaces.Repositories;

public interface IPlanningRepository
{
  public Task<PlanningEntity> CreatePlanningAsync(PlanningEntity planningEntity);
  public Task<PlanningEntity> GetPlanningByUserIdAsync(int userId);
  public Task<PlanningEntity> UpdatePlanningAsync(PlanningEntity planningEntity);
}