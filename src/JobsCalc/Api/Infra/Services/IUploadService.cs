namespace JobsCalc.Api.Infra.Services;

public interface IUploadService
{
  public Task<string> UploadAvatarRestAsync(int userId, IFormFile file);

  public Task<string> UploadAvatarGraphQLAsync(int userId, IFile file);
}