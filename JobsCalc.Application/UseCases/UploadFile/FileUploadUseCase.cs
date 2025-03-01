using JobsCalc.Application.Exceptions;
using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Communication.DTOs.Responses;
using JobsCalc.Domain.Interfaces.Repositories;
using JobsCalc.Domain.Interfaces.UseCases;
using Microsoft.Extensions.Configuration;

namespace JobsCalc.Application.UseCases.UploadFile;

public class FileUploadUseCase : IFileUploadUseCase
{
  private readonly long _maxFileSize;
  private readonly string[] _allowedExtensions = [".jpg", ".jpeg", ".png"];
  private readonly IGetUserByIdUseCase _getUserByIdUseCase;
  private readonly IUserUpdateUseCase _userUpdateUseCase;
  private readonly IFileStorageRepository _fileStorageRepository;

  public FileUploadUseCase(IGetUserByIdUseCase getUserByIdUseCase, IUserUpdateUseCase userUpdateUseCase, 
    IFileStorageRepository fileStorageRepository, IConfiguration configuration)
  {
    _fileStorageRepository = fileStorageRepository;
    _getUserByIdUseCase = getUserByIdUseCase;
    _userUpdateUseCase = userUpdateUseCase;
    _maxFileSize = configuration.GetValue<long>("FileStorage:MaxFileSize");
  }

  public async Task<FileUploadResponse> Execute(int userId, FileUploadRequest request)
  {
    ValidateFile(request);
    ValidateFileSize(request.FileStream!);
    ValidateFileExtension(request.FileName!);

    var user = await _getUserByIdUseCase.Execute(userId)
        ?? throw new NotFoundException($"O usuário com ID {userId} não foi encontrado");
    
    await DeleteOldAvatarExistsAsync(user);
    
    var newFilePath = await SaveAvatarAsync(userId, request.FileStream!, request.FileName!);

    await _userUpdateUseCase.Execute(userId, new UserPathRequest {
      AvatarUrl = newFilePath
    });
    
    return new FileUploadResponse
    {
      FileUrl = newFilePath,
      FileName = request.FileName!
    };
  }
  private static void ValidateFile(FileUploadRequest request)
  {
    if (request.FileStream is null || request.FileName is null || request.FileName.Length == 0)
    {
      throw new InvalidFileException("Nenhum arquivo foi carregado.");
    }
  }

  private void ValidateFileSize(Stream fileStream)
  {
    if (fileStream.Length > _maxFileSize)
    {
      throw new FileSizeExceededException($"O arquivo deve ter no máximo {_maxFileSize / 1024 / 1024} MB.");
    }
  }

  private void ValidateFileExtension(string fileName)
  {
    var extension = Path.GetExtension(fileName).ToLower();
    if (!_allowedExtensions.Contains(extension))
    {
      throw new FileFormatNotAllowedException("Formato de arquivo não permitido.");
    }
  }

  private async Task DeleteOldAvatarExistsAsync(UserResponse user)
  {
    if (!string.IsNullOrEmpty(user.AvatarUrl))
    {
      await _fileStorageRepository.DeleteFileAsync(user.AvatarUrl);
    }
  }

  private async Task<string> SaveAvatarAsync(int userId, Stream fileStream, string fileName)
  {
    var newFileName = $"{userId}-{Guid.NewGuid()}{Path.GetExtension(fileName)}";
    return await _fileStorageRepository.SaveFileAsync(userId, new FileUploadRequest
    {
      FileName = newFileName,
      FileStream = fileStream
    });
  }
}