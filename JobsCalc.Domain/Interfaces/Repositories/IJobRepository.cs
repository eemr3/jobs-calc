using JobsCalc.Domain.Entities;

namespace JobsCalc.Domain.Interfaces.Repositories;

public interface IJobRepository
{
  public Task<JobEntity> AddJobAsync(JobEntity request);
  public Task<IEnumerable<JobEntity>> GetJobsUser(int userId);
  public Task<JobEntity?> GetJob(string jobId);
  public Task<JobEntity> UpdateJob(JobEntity request);
  public Task DeleteJob(JobEntity request);
}