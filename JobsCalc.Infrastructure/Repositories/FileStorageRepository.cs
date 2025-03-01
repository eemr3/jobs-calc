using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;

namespace JobsCalc.Infrastructure.Repositories;

public class FileStorageRepository: IFileStorageRepository
{
  private readonly string _uploadDir;

  public FileStorageRepository(IConfiguration configuration)
  {
    _uploadDir = configuration["FileStorage:UploadDir"] ?? throw new InvalidOperationException();
    Directory.CreateDirectory(_uploadDir);
  }
  
  public async Task<string> SaveFileAsync(int userId, FileUploadRequest request)
  {
    var filePath = Path.Combine(_uploadDir, request.FileName!);
    await using (var outputStream = new FileStream(filePath, FileMode.Create))
    {
      await request.FileStream!.CopyToAsync(outputStream);
    }

    return $"/{_uploadDir}/{request.FileName}";
  }

  public Task DeleteFileAsync(string filePath)
  {
    var physicalPath = Path.Combine(_uploadDir, Path.GetFileName(filePath));
    if (File.Exists(physicalPath))
    {
      File.Delete(physicalPath);
    }
    
    return Task.CompletedTask;
  }
}