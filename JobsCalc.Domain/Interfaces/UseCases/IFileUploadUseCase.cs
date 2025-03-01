using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Communication.DTOs.Responses;

namespace JobsCalc.Domain.Interfaces.UseCases;

public interface IFileUploadUseCase
{
  Task<FileUploadResponse> Execute(int userId, FileUploadRequest request);
}