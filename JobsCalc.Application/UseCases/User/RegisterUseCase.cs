using JobsCalc.Application.Exceptions;
using JobsCalc.Application.Validators;
using JobsCalc.Communication.DTOs.Requests;
using JobsCalc.Communication.DTOs.Responses;
using JobsCalc.Domain.Interfaces;
using JobsCalc.Domain.Entities;

namespace JobsCalc.Application.UseCases.User;

public class RegisterUseCase : IRegisterUseCase
{
    private readonly RegisterUserValidator _registerUserValidator;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    public RegisterUseCase(
        RegisterUserValidator registerUserValidator,
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _registerUserValidator = registerUserValidator;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserResponse> ExecuteAsync(UserRequest request)
    {
        await Validate(request);

        var user = new UserEntity
        {
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = _passwordHasher.HashPassword(request.Password),
        };
        var newUser = await _userRepository.RegisterUserAsync(user);

        return new UserResponse
        {
            UserId = newUser.UserId,
            FullName = newUser.FullName,
            Email = newUser.Email,
            AvatarUrl = newUser.AvatarUrl,
        };
    }

    private async Task Validate(UserRequest request)
    {
        var result = _registerUserValidator.Validate(request);

        var userExists = await _userRepository.GetUserByEmailAsync(request.Email);
        if (userExists is not null)
            throw new ConflictException($"Usuário com {request.Email} já esta cadastrado em nossa base de dados");

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}