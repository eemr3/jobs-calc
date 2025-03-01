using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Domain.Entities;
using JobsCalc.Domain.Interfaces.Repositories;
using JobsCalc.Domain.Interfaces.UseCases;
using JobsCalc.Domain.Services;

namespace JobsCalc.Application.UseCases.Planning;

public class UpdatePlanningUseCase : IUpdatePlanningUseCase
{
  private readonly IPlanningRepository _planningRepository;

  public UpdatePlanningUseCase(IPlanningRepository planningRepository)
  {
    _planningRepository = planningRepository;
  }
  
  public async Task<PlanningEntity> Execute(PlanningUpdateRequest request)
  {
    var valueHours = HourlyRateCalculator.CalculateHourlyRate(
      request.MonthlyBudget, request.VacationPerYear,
      request.HoursPerDay, request.DaysPerWeek);

    var planningUpdated = await _planningRepository.UpdatePlanningAsync(new PlanningEntity
    {
      PlanningId = request.PlanningId,
      MonthlyBudget = request.MonthlyBudget,
      VacationPerYear = request.VacationPerYear,
      HoursPerDay = request.HoursPerDay,
      DaysPerWeek = request.DaysPerWeek,
      ValueHour = valueHours,
      UserId = request.UserId
    });
    
    return planningUpdated;
  }
}