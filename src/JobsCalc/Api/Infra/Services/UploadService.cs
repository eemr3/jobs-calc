
using JobsCalc.Api.Http.Dtos;
using JobsCalc.Api.Infra.Database.Repositories;
using IOPath = System.IO.Path;
using SystemKeyNotFoundException = System.Collections.Generic.KeyNotFoundException;

namespace JobsCalc.Api.Infra.Services;
public class UploadService : IUploadService
{
  private readonly IUserRepository _userRepository;
  private readonly string _uploadDir = IOPath.Combine("upload", "avatar");
  public UploadService(IUserRepository userRepository)
  {
    _userRepository = userRepository;
    Directory.CreateDirectory(_uploadDir);
  }

  public async Task<string> UploadAvatarRestAsync(int userId, IFormFile file)
  {
    if (file == null)
    {
      throw new ArgumentException("No files were uploaded.");
    }
    return await UploadAvatarAsync(userId, file);
  }

  public async Task<string> UploadAvatarGraphQLAsync(int userId, IFile file)
  {
    return await UploadAvatarAsync(userId, file);
  }

  private async Task<string> UploadAvatarAsync(int userId, object file)
  {
    if (file == null)
    {
      throw new ArgumentException("No files were uploaded.");
    }

    // Determina qual arquivo usar (GraphQL ou REST)
    long fileSize;
    string fileName;

    switch (file)
    {
      case IFormFile formFile:
        fileSize = formFile?.Length ?? throw new ArgumentException("File is null.");
        fileName = formFile.FileName;
        break;
      case IFile gqlFile:
        fileSize = gqlFile.Length ?? throw new ArgumentException("File size is null.");
        fileName = gqlFile.Name;
        break;
      default:
        throw new ArgumentException("Formato de arquivo desconhecido.");
    }

    // Validação de tamanho
    if (fileSize > 2 * 1024 * 1024)
    {
      throw new ArgumentException("O arquivo deve ter no máximo 2 MB.");
    }

    // Validação de formato
    string extension = IOPath.GetExtension(fileName).ToLower();
    string[] allowedExtensions = [".jpg", ".jpeg", ".png"];

    if (!allowedExtensions.Contains(extension))
    {
      throw new ArgumentException("Formato de arquivo não permitido.");
    }

    var user = await _userRepository.GetUserById(userId);
    if (user is null) throw new SystemKeyNotFoundException($"User with ID {userId} not found.");

    if (!string.IsNullOrEmpty(user.AvatarUrl))
    {
      var oldFilePath = IOPath.Combine(_uploadDir, IOPath.GetFileName(user.AvatarUrl));
      if (File.Exists(oldFilePath))
      {
        File.Delete(oldFilePath);
      }
    }


    string newFileName = $"{Guid.NewGuid()}{extension}";
    string newFilePath = IOPath.Combine(_uploadDir, newFileName);

    // Salva o arquivo
    using (var stream = new FileStream(newFilePath, FileMode.Create))
    {
      if (file is IFormFile fFile)
      {
        await fFile.CopyToAsync(stream);
      }
      else if (file is IFile gqlF)
      {
        await gqlF.CopyToAsync(stream);
      }
    }

    user.AvatarUrl = $"/{_uploadDir}/{newFileName}";
    await _userRepository.UpdateUser(userId, new UserPatchDto { AvatarUrl = user.AvatarUrl });

    return user.AvatarUrl;
  }

}