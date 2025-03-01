using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Communication.DTOs.Responses;

namespace JobsCalc.Domain.Interfaces.Repositories;

public interface IFileStorageRepository
{
  Task<string> SaveFileAsync(int userId, FileUploadRequest request);
  Task DeleteFileAsync(string filePath);
}