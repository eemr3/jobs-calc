using JobsCalc.Api.Http.Dtos;

namespace JobsCalc.Api.Application.Services.UserService;

public interface IUserService
{
  public Task<UserDtoResponse> AddUserAsync(UserDtoRequest userDto);
  public Task<UserDtoResponse> GetUserById(int userId);
  public Task<UserDtoResponse> UpdateUserAsync(UserDtoRequest user);
  public Task DeleteUser(int id);
}