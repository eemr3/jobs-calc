using JobsCalc.Api.Domain.Entities;
using JobsCalc.Api.Http.Dtos;
using JobsCalc.Api.Infra.Database.EntityFramework;
using Microsoft.EntityFrameworkCore;
using SystemKeyNotFoundException = System.Collections.Generic.KeyNotFoundException;

namespace JobsCalc.Api.Infra.Database.Repositories;

public class JobRepository : IJobRepository
{
  private readonly IAppDbContext _context;

  public JobRepository(IAppDbContext context)
  {
    _context = context;
  }

  public async Task<Job> AddJobAsync(Job job)
  {
    var jobAdd = await _context.Jobs.AddAsync(job);
    await _context.SaveChangesAsync();

    return jobAdd.Entity;
  }

  public async Task<Job?> GetJob(string jobId)
  {
    if (!Guid.TryParse(jobId, out var jobGuid))
    {
      throw new ArgumentException($"Invalid Job ID format: {jobId}");
    }

    var job = await _context.Jobs.FirstOrDefaultAsync(c => c.JobId.Equals(jobGuid));
    if (job is null) return null;

    return job;
  }

  public async Task<IEnumerable<Job>> GetJobsUser(int userId)
  {
    var jobs = await _context.Jobs.Where(jb => jb.UserId.Equals(userId)).ToListAsync();

    return jobs;
  }

  public async Task<Job> UpdateJob(string jobId, JobPatchDto jobPatch)
  {
    if (!Guid.TryParse(jobId, out var jobGuid))
    {
      throw new ArgumentException($"Invalid Job ID format: {jobId}");
    }
    var jobExists = await _context.Jobs.FirstOrDefaultAsync(jb => jb.JobId.Equals(jobGuid));
    if (jobExists is null) throw new SystemKeyNotFoundException($"Job with ID {jobId} not found");

    if (!string.IsNullOrEmpty(jobPatch.Name))
    {
      jobExists.Name = jobPatch.Name;
    }
    if (!string.IsNullOrEmpty(Convert.ToString(jobPatch.DailyHours)))
    {
      jobExists.DailyHours = jobPatch.DailyHours!.Value;
    }

    if (!string.IsNullOrEmpty(Convert.ToString(jobPatch.TotalHours)))
    {
      jobExists.TotalHours = jobPatch.TotalHours!.Value;
    }

    _context.Jobs.Update(jobExists);
    await _context.SaveChangesAsync();

    return jobExists;
  }

  public async Task DeleteJob(string jobId)
  {
    if (!Guid.TryParse(jobId, out var jobGuid))
    {
      throw new ArgumentException($"Invalid Job ID format: {jobId}");
    }
    var job = await _context.Jobs.FirstOrDefaultAsync(jb => jb.JobId.Equals(jobGuid));
    if (job is null) throw new SystemKeyNotFoundException($"JOb with ID {jobId} not found");

    _context.Jobs.Remove(job);
    await _context.SaveChangesAsync();
  }
}