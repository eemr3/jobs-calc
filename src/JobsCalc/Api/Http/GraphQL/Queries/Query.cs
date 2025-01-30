using System.Security.Claims;
using HotChocolate.AspNetCore;
using HotChocolate.Authorization;
using JobsCalc.Api.Application.Services.JobService;
using JobsCalc.Api.Application.Services.PlanningService;
using JobsCalc.Api.Application.Services.UserService;
using JobsCalc.Api.Domain.Entities;
using JobsCalc.Api.Http.Dtos;

namespace JobsCalc.Api.Http.GraphQL.Queries;

public class Query
{
  [GraphQLDescription("Retorna as informações do usuário logado")]
  [Authorize]
  public async Task<UserDtoResponse> GetMe([Service] IUserService userService, ClaimsPrincipal claimsPrincipal)
  {
    try
    {
      var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
      var user = await userService.GetUserByEmail(email!);

      return user;
    }
    catch (Exception)
    {
      throw new UnauthorizedAccessException("Acesso não autorizado");
    }

  }

  [GraphQLDescription("Retorna o planejamento do usuário logado")]
  [Authorize]
  public async Task<Planning> GetPlanning([Service] IPlanningService planningService, ClaimsPrincipal claimsPrincipal)
  {
    var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

    var planning = await planningService.GetPlanningByUserIdAsync(int.Parse(userId!));

    return planning;
  }

  [GraphQLDescription("Retorna todos os trabalhos do usuário logado")]
  [Authorize]
  public async Task<IEnumerable<JobDtoResponse>> GetAllJobs([Service] IJobService jobService, ClaimsPrincipal claimsPrincipal)
  {
    var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
    var jobs = await jobService.GetJobsUser(int.Parse(userId!));
    var defaultList = new List<JobDtoResponse>();
    return jobs ?? defaultList;
  }

  [GraphQLDescription("Retorna um job pelo ID")]
  [Authorize]
  public async Task<Job> GetJob([Service] IJobService jobService, string jobId)
  {
    try
    {
      var job = await jobService.GetJob(jobId);

      return job!;

    }
    catch (Exception)
    {
      throw new GraphQLRequestException($"Job with ID {jobId} not found");
    }
  }
}