using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Domain.Entities;
using JobsCalc.Domain.Interfaces.Repositories;
using JobsCalc.Domain.Interfaces.UseCases;
using System.Threading.Tasks;

namespace JobsCalc.Application.UseCases.Job
{
  public class UpdateJobUseCase : IUpdateJobUseCase
  {
    private readonly IJobRepository _jobRepository;

    public UpdateJobUseCase(IJobRepository jobRepository)
    {
      _jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
    }

    public async Task<JobEntity> Execute(string jobId, JobUpdateRequest request)
    {
      if (string.IsNullOrWhiteSpace(jobId))
        throw new ArgumentException("Job ID não pode ser nulo ou vazio.", nameof(jobId));

      if (request == null)
        throw new ArgumentNullException(nameof(request), "Request não pode ser nulo.");

      var jobExists = await _jobRepository.GetJob(jobId);
      if (jobExists == null)
        throw new KeyNotFoundException($"Job com o ID {jobId} não encontrado.");

      UpdateJobProperties(jobExists, request);

      var jobUpdated = await _jobRepository.UpdateJob(jobExists);

      return jobUpdated;
    }

    private static void UpdateJobProperties(JobEntity job, JobUpdateRequest request)
    {
      if (!string.IsNullOrWhiteSpace(request.Name))
      {
        job.Name = request.Name;
      }

      if (request.DailyHours.HasValue)
      {
        job.DailyHours = request.DailyHours.Value;
      }

      if (request.TotalHours.HasValue)
      {
        job.TotalHours = request.TotalHours.Value;
      }
    }
  }
}