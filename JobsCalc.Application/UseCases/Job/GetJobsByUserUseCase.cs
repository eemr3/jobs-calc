using JobsCalc.Application.Exceptions;
using JobsCalc.Communication.DTOs.Responses;
using JobsCalc.Domain.Entities;
using JobsCalc.Domain.Interfaces.Repositories;
using JobsCalc.Domain.Interfaces.UseCases;
using JobsCalc.Domain.Services;

namespace JobsCalc.Application.UseCases.Job;

public class GetJobsByUserUseCase : IGetJobsByUserUseCase
{
  private readonly IJobRepository _jobRepository;
  private readonly IPlanningRepository _planningRepository;

  public GetJobsByUserUseCase(IJobRepository jobRepository, IPlanningRepository planningRepository)
  {
    _jobRepository = jobRepository;
    _planningRepository = planningRepository;
  }
  
  public async Task<IEnumerable<JobResponse>> Execute(int userId)
  {
    var planning = await ValidatePlanningJob(userId);
    var jobs = await _jobRepository.GetJobsUser(userId);

    return (from job in jobs
      let remaining = RemainingDaysCalculator.RemainingDays(job, job.DailyHours, job.TotalHours)
      select new JobResponse
      {
        JobId = job.JobId,
        Name = job.Name,
        DailyHours = job.DailyHours,
        TotalHours = job.TotalHours,
        RemainingDays = remaining,
        ValueJob = CalculateBudget(job.TotalHours, planning.ValueHour),
        Status = remaining > 0,
        UserId = job.UserId!.Value
      }).ToList();
  }

  private async Task<PlanningEntity> ValidatePlanningJob(int userId)
  {
    var planning = await _planningRepository.GetPlanningByUserIdAsync(userId);

    if (planning is null) throw new NotFoundException($"Não foi encontrado planejamento para o usuário de ID {userId}");
    
    return planning;
  }
  
  private  static decimal CalculateBudget(int totalHours, decimal valueHour)
  {
    return valueHour * totalHours;
  }
}