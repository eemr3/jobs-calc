using System.Security.Claims;
using HotChocolate.Authorization;
using JobsCalc.Api.Application.Exceptions;
using JobsCalc.Api.Application.Services.AuthService;
using JobsCalc.Api.Application.Services.JobService;
using JobsCalc.Api.Application.Services.PlanningService;
using JobsCalc.Api.Application.Services.UserService;
using JobsCalc.Api.Domain.Entities;
using JobsCalc.Api.Http.Dtos;
using JobsCalc.Api.Infra.Services;

namespace JobsCalc.Api.Http.GraphQL.Mutations;

public class Mutation
{
  [GraphQLDescription("Autentica um usuário e retorna um token JWT.")]
  public async Task<LoginDtoResponse> Login([Service] IAuthService authService, LoginDtoRequest input)
  {
    var token = await authService.SignIn(input);

    return new LoginDtoResponse
    {
      access_token = token
    };
  }

  [GraphQLDescription("Cria um novo usuário no sistema.")]
  public async Task<UserDtoResponse> RegisterUser([Service] IUserService userService, UserDtoRequest input)
  {
    var newUser = await userService.AddUserAsync(input);

    return newUser;
  }

  [GraphQLDescription("Atualiza as informações de um usuário existente.")]
  [Authorize]
  public async Task<UserDtoResponse> UpdateUser([Service] IUserService userService,
  UserPatchDto input, ClaimsPrincipal claimsPrincipal)
  {
    var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

    var updatedUser = await userService.UpdateUserAsync(int.Parse(userId!), input);

    return updatedUser;
  }

  [GraphQLDescription("Faz o upload do avatar do usuário autenticado.")]
  [Authorize]
  public async Task<UploadAvatarDto> UploadAvatar(
    [Service] IUploadService uploadService,
    [GraphQLDescription("Arquivo de imagem para o avatar.")]
    [GraphQLNonNullType] IFile file,
    ClaimsPrincipal claimsPrincipal
  )
  {
    try
    {
      var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
      var avatarUrl = await uploadService.UploadAvatarGraphQLAsync(int.Parse(userId!), file);

      return new UploadAvatarDto
      {
        message = "Avatar updated successfully.",
        avatar_url = avatarUrl
      };
    }
    catch (ArgumentException ex)
    {
      throw new GraphQLException(ex.Message);
    }
  }

  [GraphQLDescription("Cria o planejamento do usuário logado.")]
  [Authorize]
  public async Task<Planning> CreatePlanning(
              [Service] IPlanningService planningService,
              PlanningDtoRequest input,
              ClaimsPrincipal claimsPrincipal)
  {
    try
    {
      var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
      input.UserId = int.Parse(userId!);

      var newPlanning = await planningService.AddPlanningAsync(input);

      return newPlanning;
    }
    catch (ConflictException ex)
    {
      throw new GraphQLException(ex.Message);
    }
  }

  [GraphQLDescription("Realiza o update do planejamento do usuário logado.")]
  [Authorize]
  public async Task<Planning> UpdatePlanning([Service] IPlanningService planningService,
              PlanningUpdateDto input,
              ClaimsPrincipal claimsPrincipal)
  {
    try
    {

      var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
      input.UserId = int.Parse(userId!);

      var updatedPlanning = await planningService.UpdatePlanningAsync(input);

      return updatedPlanning;
    }
    catch (Exception ex)
    {

      throw new GraphQLException(ex.Message);
    }
  }

  [GraphQLDescription("Cria um novo job e retorna o job criado.")]
  [Authorize]
  public async Task<Job> CreateJob(
      [Service] IJobService jobService,
      ClaimsPrincipal claimsPrincipal,
      JobDtoRequest input
   )
  {
    try
    {
      var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
      var jobCreated = await jobService.AddJobAsync(int.Parse(userId!), input);

      return jobCreated;
    }
    catch (Exception ex)
    {
      throw new GraphQLException(ex.Message);
    }
  }

  [GraphQLDescription("Atualiza um job existente.")]
  [Authorize]
  public async Task<Job> UpdateJob(
     [Service] IJobService jobService,
     JobPatchDto input,
     ClaimsPrincipal claimsPrincipal,
     string jobId
  )
  {
    try
    {
      var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
      var jobUpdated = await jobService.UpdateJob(jobId, input);

      return jobUpdated;
    }
    catch (Exception ex)
    {

      throw new GraphQLException(ex.Message);
    }
  }

  [GraphQLDescription("Deleta um job existente.")]
  [Authorize]
  public async Task<bool> DeleteJob([Service] IJobService jobService, string jobId)
  {
    try
    {
      await jobService.DeleteJob(jobId);

      return true;
    }
    catch (Exception ex)
    {

      throw new GraphQLException(ex.Message);
    }
  }
}