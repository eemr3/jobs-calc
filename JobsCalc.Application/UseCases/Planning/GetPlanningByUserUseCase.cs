using JobsCalc.Domain.Entities;
using JobsCalc.Domain.Interfaces.Repositories;
using JobsCalc.Domain.Interfaces.UseCases;

namespace JobsCalc.Application.UseCases.Planning;

public class GetPlanningByUserUseCase : IGetPlanningByUserUseCase
{
  private readonly IPlanningRepository _planningRepository;

  public GetPlanningByUserUseCase(IPlanningRepository planningRepository)
  {
    _planningRepository = planningRepository;
  }
  
  public async Task<PlanningEntity> Execute(int userId)
  {
    var planning = await _planningRepository.GetPlanningByUserIdAsync(userId);
    
    if(planning is null) return new PlanningEntity
    {
      DaysPerWeek = 0,
      HoursPerDay = 0,
      MonthlyBudget = 0,
      VacationPerYear = 0,
      ValueHour = 0,
      UserId = userId
    };
    
    return planning;
  }
}