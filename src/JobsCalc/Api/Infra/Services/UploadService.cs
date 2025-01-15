
using JobsCalc.Api.Http.Dtos;
using JobsCalc.Api.Infra.Database.Repositories;

namespace JobsCalc.Api.Infra.Services;
public class UploadService : IUploadService
{
  private readonly IUserRepository _userRepository;
  private readonly string _uploadDir = Path.Combine("upload", "avatar");
  public UploadService(IUserRepository userRepository)
  {
    _userRepository = userRepository;
    Directory.CreateDirectory(_uploadDir);
  }

  public async Task<string> UploadAvatarAsync(int userId, UploadFileDto avatar)
  {
    if (avatar.File == null || avatar.File.Length == 0)
    {
      throw new ArgumentException("No files were uploaded.");
    }

    if (avatar.File.Length > 2 * 1024 * 1024)
    {
      throw new ArgumentException("The file must be a maximum of 2 MB");
    }

    string[] allowedExtensions = [".jpg", ".jpeg", ".png"];
    string extension = Path.GetExtension(avatar.File.FileName).ToLower();
    if (!allowedExtensions.Contains(extension))
    {
      throw new ArgumentException("File format not allowed.");
    }

    var user = await _userRepository.GetUserById(userId);
    if (user is null) throw new KeyNotFoundException($"User with ID {userId} not found.");

    if (!string.IsNullOrEmpty(user.AvatarUrl))
    {
      var oldFilePath = Path.Combine(_uploadDir, Path.GetFileName(user.AvatarUrl));
      if (File.Exists(oldFilePath))
      {
        File.Delete(oldFilePath);
      }
    }


    string newFileName = $"{Guid.NewGuid()}{extension}";
    string newFilePath = Path.Combine(_uploadDir, newFileName);

    using (var stream = new FileStream(newFilePath, FileMode.Create))
    {
      await avatar.File.CopyToAsync(stream);
    }

    user.AvatarUrl = $"/{_uploadDir}/{newFileName}";
    await _userRepository.UpdateUser(userId, new UserPatchDto { AvatarUrl = user.AvatarUrl });

    return user.AvatarUrl;
  }

}