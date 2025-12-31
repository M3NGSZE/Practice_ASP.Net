
using FluentValidation;
using SuperHeroAPI_DotNet6.Middlewares;
using SuperHeroAPI_DotNet6.Models.Dtos;
using SuperHeroAPI_DotNet6.Models.Requests;
using SuperHeroAPI_DotNet6.Services.Interfaces;
using SuperHeroAPI_DotNet6.Validators;

namespace SuperHeroAPI_DotNet6.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IValidator<UserRequest> _userValidator;

        public AuthService(IValidator<UserRequest> userValidator)
        {
            _userValidator = userValidator;
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


            return null;
            // Proceed with user creation...
        }
    }
}
