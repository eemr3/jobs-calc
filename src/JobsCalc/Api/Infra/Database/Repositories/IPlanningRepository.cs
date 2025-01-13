using JobsCalc.Api.Domain.Entities;

namespace JobsCalc.Api.Infra.Database.Repositories;

public interface IPlanningRepository
{
  public Task<Planning> AddPlanningAsync(Planning planning);
  public Task<Planning?> GetPlanningByUserAsync(int userId);
  public Task<Planning?> UpdatePlanningAsync(Planning planning);
}