using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Domain.Entities;
using JobsCalc.Domain.Interfaces.Repositories;
using JobsCalc.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JobsCalc.Infrastructure.Repositories;

public class JobRepository : IJobRepository
{
  private readonly IDbContextFactory<ApiDbContext> _dbContextFactory;

  public JobRepository(IDbContextFactory<ApiDbContext> dbContextFactory)
  {
    _dbContextFactory = dbContextFactory;
  }
  
  public async Task<JobEntity> AddJobAsync(JobEntity request)
  {
    await using var context = await _dbContextFactory.CreateDbContextAsync();
    var job = await context.Jobs.AddAsync(request);
    
    await context.SaveChangesAsync();
    
    return job.Entity;
  }

  public async Task<IEnumerable<JobEntity>> GetJobsUser(int userId)
  {
    await using var context = await _dbContextFactory.CreateDbContextAsync();
    var jobs = await context.Jobs.Where(job => job.UserId.Equals(userId)).ToListAsync();

    return jobs;
  }

  public async Task<JobEntity?> GetJob(string jobId)
  {
    if (!Guid.TryParse(jobId, out var jobGuid))
    {
      throw new ArgumentException($"Invalid job id: {jobId}");
    }
    
    await using var context = await _dbContextFactory.CreateDbContextAsync();
    var job = await context.Jobs.FirstOrDefaultAsync(jb => jb.JobId.Equals(jobGuid));
    
    return job;
  }
  
  public async Task<JobEntity> UpdateJob(string jobId, JobEntity request)
  {
    await using var context = await _dbContextFactory.CreateDbContextAsync();

    context.Jobs.Update(request);
    await context.SaveChangesAsync();
    
    return request;
  }

  public async Task DeleteJob(JobEntity request)
  {
    await using var context = await _dbContextFactory.CreateDbContextAsync();

    context.Jobs.Remove(request);
    await context.SaveChangesAsync();
  }
}