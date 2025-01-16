using JobsCalc.Api.Domain.Entities;
using JobsCalc.Api.Http.Dtos;

namespace JobsCalc.Api.Application.Services.JobService;

public interface IJobService
{
  public Task<Job> AddJobAsync(int userId, JobDtoRequest job);
  public Task<IEnumerable<JobDtoResponse>?> GetJobsUser(int userId);
  public Task<Job?> GetJob(int jobId);
  public Task<Job> UpdateJob(int jobId, JobPatchDto jobPatch);
  public Task DeleteJob(int jobId);
}