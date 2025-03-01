using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Communication.DTOs.Responses;

namespace JobsCalc.Domain.Interfaces.UseCases;

public interface IUserRegisterUseCase
{
    public Task<UserResponse> ExecuteAsync(UserRequest request);
}