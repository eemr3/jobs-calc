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
    public RegisterUseCase(
        RegisterUserValidator registerUserValidator, 
        IUserRepository userRepository)
    {
        _registerUserValidator = registerUserValidator;
        _userRepository = userRepository;
    }
    
    public async Task<UserResponse> ExecuteAsync(UserRequest request)
    {
        Validate(request);
        
        var user = new UserEntity
        {
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = request.Password,
        };
        var newUser = await _userRepository.RegisterUserAsync(user);
        
        return new UserResponse
        {
            FullName = user.FullName,
            Email = user.Email,
            AvatarUrl = user.AvatarUrl,
        };
    }

    private void  Validate(UserRequest request)
    {
        var result = _registerUserValidator.Validate(request);
        
        var userExists = _userRepository.GetUserByEmailAsync(request.Email);
        if(userExists is not null) 
            throw new ConflictException($"Usuário com {request.Email} já esta cadastrado em nossa base de dados");
        
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}