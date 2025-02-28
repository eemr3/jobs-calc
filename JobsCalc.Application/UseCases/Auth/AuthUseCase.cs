using JobsCalc.Application.Exceptions;
using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Communication.DTOs.Responses;
using JobsCalc.Domain.Entities;
using JobsCalc.Domain.Interfaces;

namespace JobsCalc.Application.UseCases.Auth;

public class AuthUseCase : IAuthUseCase
{
  private readonly IUserRepository _userRepository;
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IPasswordHasher _passwordHasher;

  public AuthUseCase(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator, IPasswordHasher passwordHasher)
  {
    _userRepository = userRepository;
    _jwtTokenGenerator = jwtTokenGenerator;
    _passwordHasher = passwordHasher;
  }

  public async Task<LoginResponse> Execute(LoginRequest request)
  {
    var user = await ValidateRequest(request);

    return new LoginResponse
    {
      access_token = _jwtTokenGenerator.GenerateToken(user.UserId)
    };
  }

  private async Task<UserEntity> ValidateRequest(LoginRequest request)
  {
    if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
      throw new BadRequestException("Email e senha são obrigatórios.");

    var email = request.Email.Trim().ToLower();
    var user = await _userRepository.GetUserByEmailAsync(email);

    if (user is null || !_passwordHasher.VerifyHashedPassword(user.PasswordHash, request.Password)) throw new InvalidLoginException();

    return user;
  }
}