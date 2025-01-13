using JobsCalc.Api.Domain.Entities;
using JobsCalc.Api.Http.Dtos;

namespace JobsCalc.Api.Application.Services.PlanningService;

public interface IPlanningService
{
  public Task<Planning> AddPlanningAsync(PlanningDtoRequest planningDto);
  public Task<Planning> GetPlanningByUserIdAsync(int userId);
  public Task<Planning> UpdatePlanningAsync(PlanningUpdateDto planningDto);
}

