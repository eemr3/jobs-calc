using JobsCalc.Api.Application.Utils;
using JobsCalc.Api.Domain.Entities;
using JobsCalc.Api.Http.Dtos;
using JobsCalc.Api.Infra.Database.Repositories;

namespace JobsCalc.Api.Application.Services.PlanningService;

public class PlanningService : IPlanningService
{
  private readonly IPlanningRepository _repository;

  public PlanningService(IPlanningRepository repository)
  {
    _repository = repository;
  }

  public async Task<Planning> AddPlanningAsync(PlanningDtoRequest planningDto)
  {
    decimal valueHour = JobUtils.CalculateHourlyRate(
      planningDto.MonthlyBudget,
      planningDto.VacationPerYear ?? 0,
      planningDto.HoursPerDay,
      planningDto.DaysPerWeek);

    var planningData = new Planning
    {
      DaysPerWeek = planningDto.DaysPerWeek,
      HoursPerDay = planningDto.HoursPerDay,
      MonthlyBudget = planningDto.MonthlyBudget,
      VacationPerYear = planningDto.VacationPerYear,
      UserId = planningDto.UserId,
      ValueHour = valueHour

    };
    var planning = await _repository.AddPlanningAsync(planningData);

    return planning;
  }

  public async Task<Planning> GetPlanningByUserIdAsync(int userId)
  {
    var planning = await _repository.GetPlanningByUserAsync(userId);

    if (planning is null) return new Planning
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

  public async Task<Planning> UpdatePlanningAsync(PlanningUpdateDto planningDto)
  {

    decimal valueHour = JobUtils.CalculateHourlyRate(
      planningDto.MonthlyBudget,
      planningDto.VacationPerYear ?? 0,
      planningDto.HoursPerDay,
      planningDto.DaysPerWeek);

    var planning = await _repository.UpdatePlanningAsync(new Planning
    {
      PlanningId = planningDto.PlanningId,
      DaysPerWeek = planningDto.DaysPerWeek,
      HoursPerDay = planningDto.HoursPerDay,
      MonthlyBudget = planningDto.MonthlyBudget,
      UserId = planningDto.UserId!.Value,
      VacationPerYear = planningDto.VacationPerYear,
      ValueHour = valueHour,
    });

    if (planning is null) throw new KeyNotFoundException($"Planning userId {planningDto.UserId.Value} not found");

    return planning;
  }
}