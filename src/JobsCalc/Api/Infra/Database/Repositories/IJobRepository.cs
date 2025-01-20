using JobsCalc.Api.Domain.Entities;
using JobsCalc.Api.Http.Dtos;

namespace JobsCalc.Api.Infra.Database.Repositories;

public interface IJobRepository
{
  public Task<Job> AddJobAsync(Job job);
  public Task<IEnumerable<Job>> GetJobsUser(int userId);
  public Task<Job?> GetJob(string jobId);
  public Task<Job> UpdateJob(string jobId, JobPatchDto jobPatch);
  public Task DeleteJob(string jobId);
}