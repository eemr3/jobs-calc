using JobsCalc.Api.Http.Dtos;

namespace JobsCalc.Api.Infra.Services;

public interface IUploadService
{
  Task<string> UploadAvatarAsync(int userId, UploadFileDto avatar);
}