
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SuperHeroAPI_DotNet6.Middlewares;
using SuperHeroAPI_DotNet6.Models.Dtos;
using SuperHeroAPI_DotNet6.Models.Entities;
using SuperHeroAPI_DotNet6.Models.Requests;
using SuperHeroAPI_DotNet6.Repositories.Interfaces;
using SuperHeroAPI_DotNet6.Services.Interfaces;
using SuperHeroAPI_DotNet6.Validators;

namespace SuperHeroAPI_DotNet6.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IValidator<UserRequest> _userValidator;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthService(IValidator<UserRequest> userValidator, IUserRepository userRepository, IMapper mapper)
        {
            _userValidator = userValidator;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public Task<AuthDTO> LoginAsync(AuthRequest authRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> RegisterAsync(UserRequest userRequest)
        {
            var result = await _userValidator.ValidateAsync(userRequest);

            if (!result.IsValid)
            {
                /*                throw new BadRequestException(
                                        string.Join("; ", result.Errors.Select(e => e.ErrorMessage))
                                    );*/
                var errors = result.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                );

                throw new BadRequestException(
                    "Validation failed",
                    errors
                );
            }

            var trimmedEmail = userRequest.Email.Trim();
            var trimmedUsername = userRequest.UserName.Trim();
            var trimmedPassword = userRequest.Password.Trim();

            var oldUser = await _userRepository.GetUserByEmailOrUsernameAsync(trimmedEmail, trimmedUsername);

            if (oldUser != null)
                throw new BadRequestException("Email or Username already used");

            var newUser = new User();
            var hashPassword = new PasswordHasher<User>()
                .HashPassword(newUser, trimmedPassword);

            newUser.Email = trimmedEmail;
            newUser.UserName = trimmedUsername;
            newUser.Password = trimmedPassword;

            return null;
            // Proceed with user creation...
        }
    }
}
