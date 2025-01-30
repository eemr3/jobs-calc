using JobsCalc.Api.Application.Utils;
using JobsCalc.Api.Domain.Entities;
using JobsCalc.Api.Http.Dtos;
using JobsCalc.Api.Infra.Database.Repositories;
using SystemKeyNotFoundException = System.Collections.Generic.KeyNotFoundException;

namespace JobsCalc.Api.Application.Services.JobService;

public class JobService : IJobService
{
  private readonly IJobRepository _jobRepository;
  private readonly IPlanningRepository _planningRepository;

  public JobService(IJobRepository jbRepository, IPlanningRepository planningRepository)
  {
    _jobRepository = jbRepository;
    _planningRepository = planningRepository;
  }

  public async Task<Job> AddJobAsync(int userId, JobDtoRequest job)
  {
    var newJob = new Job
    {
      Name = job.Name,
      DailyHours = job.DailyHours,
      TotalHours = job.TotalHours,
      UserId = userId
    };

    return await _jobRepository.AddJobAsync(newJob);
  }

  public async Task<Job?> GetJob(string jobId)
  {
    var job = await _jobRepository.GetJob(jobId);
    if (job is null) throw new SystemKeyNotFoundException($"Job with ID {jobId} not found");

    return job;
  }

  public async Task<IEnumerable<JobDtoResponse>?> GetJobsUser(int userId)
  {

    var planning = await _planningRepository.GetPlanningByUserAsync(userId);

    if (planning is null) throw new SystemKeyNotFoundException($"Planning user ID {userId} not found");

    var jobs = await _jobRepository.GetJobsUser(userId);

    var resultJobs = new List<JobDtoResponse>();

    foreach (var job in jobs)
    {
      var remaining = JobUtils.RemainingDays(job.TotalHours, job.DailyHours, job);
      resultJobs.Add(new JobDtoResponse
      {
        JobId = job.JobId,
        Name = job.Name!,
        DailyHours = job.DailyHours,
        TotalHours = job.TotalHours,
        RemainingDays = remaining,
        ValueJob = JobUtils.CalculateBudget(job.TotalHours, planning.ValueHour),
        Status = remaining <= 0 ? false : true,
        UserId = job.UserId!.Value
      });
    }

    return resultJobs;
  }

  public async Task<Job> UpdateJob(string jobId, JobPatchDto jobPatch)
  {
    return await _jobRepository.UpdateJob(jobId, jobPatch);
  }

  public async Task DeleteJob(string jobId)
  {
    await _jobRepository.DeleteJob(jobId);
  }

}