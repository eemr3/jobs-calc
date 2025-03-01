using JobsCalc.Application.Exceptions;
using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Domain.Entities;
using JobsCalc.Domain.Interfaces.Repositories;
using JobsCalc.Domain.Interfaces.UseCases;
using JobsCalc.Domain.Services;

namespace JobsCalc.Application.UseCases.Planning;

public class CreatePlanningUseCase : ICreatePlanningUseCase
{
  private readonly IPlanningRepository _planningRepository;

  public CreatePlanningUseCase(IPlanningRepository planningRepository)
  {
    _planningRepository = planningRepository;
  }
  
  public async Task<PlanningEntity> Execute(int userId, PlanningRequest request)
  {
    var planning = await  _planningRepository.GetPlanningByUserIdAsync(userId);

    if (planning is not null) throw new ConflictException($"Usuário de ID {{userId}} já possui planejamento");

    var valueHour = HourlyRateCalculator.CalculateHourlyRate(
      request.MonthlyBudget, request.VacationPerYear, request.HoursPerDay, request.DaysPerWeek);
    
    var newPlanning = new PlanningEntity
    {
      UserId = userId,
      MonthlyBudget = request.MonthlyBudget,
      DaysPerWeek = request.DaysPerWeek,
      HoursPerDay = request.HoursPerDay,
      VacationPerYear = request.VacationPerYear,
      ValueHour = valueHour,
    };
    var result = await _planningRepository.CreatePlanningAsync(newPlanning);
    
    return result;
  }
  
}