using JobsCalc.Communication.DTOs;
using JobsCalc.Communication.DTOs.Responses;

namespace JobsCalc.Domain.Interfaces;

public interface IRegisterUseCase
{
    public Task<UserResponse> ExecuteAsync(UserRequest request);
}