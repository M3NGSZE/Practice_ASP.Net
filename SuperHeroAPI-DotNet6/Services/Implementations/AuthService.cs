
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SuperHeroAPI_DotNet6.Auth;
using SuperHeroAPI_DotNet6.Data;
using SuperHeroAPI_DotNet6.Middlewares;
using SuperHeroAPI_DotNet6.Models.Dtos;
using SuperHeroAPI_DotNet6.Models.Entities;
using SuperHeroAPI_DotNet6.Models.Requests;
using SuperHeroAPI_DotNet6.Repositories.Interfaces;
using SuperHeroAPI_DotNet6.Services.Interfaces;
using SuperHeroAPI_DotNet6.Validators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SuperHeroAPI_DotNet6.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IValidator<UserRequest> _userValidator;
        private readonly IValidator<AuthRequest> _authValidator;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        private readonly IAuthRepository _authRepository;
        private readonly JwtService _jwtService;
        private readonly DataContext _dataContext;


        public AuthService(
            IValidator<UserRequest> userValidator, 
            IValidator<AuthRequest> authValidator, 
            IUserRepository userRepository, 
            IMapper mapper, 
            IRoleRepository roleRepository, 
            IAuthRepository authRepository, 
            JwtService jwtService,
            DataContext dataContext
            )
        {
            _userValidator = userValidator;
            _authValidator = authValidator;
            _userRepository = userRepository;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _authRepository = authRepository;
            _jwtService = jwtService;
            _dataContext = dataContext;
        }

        public async Task<AuthDTO> LoginAsync(AuthRequest authRequest)
        {
            var result = await _authValidator.ValidateAsync(authRequest);

            if (!result.IsValid)
            {
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

            authRequest.Email = authRequest.Email?.Trim() ?? string.Empty;
            authRequest.Password = authRequest.Password?.Trim() ?? string.Empty;

            var login = await _userRepository.GetUserByEmailAsync(authRequest.Email.ToLower());

            if (login == null)
                throw new NotFoundException("Email not found");

            if (new PasswordHasher<User>().VerifyHashedPassword(login, login.Password, authRequest.Password) == PasswordVerificationResult.Failed)
                throw new BadRequestException("Invalid Email or Password");

            string accesstoken = _jwtService.CreateToken(login);
            string refreshToken = await _jwtService.GenerateAndSaveRefreshTokenAsync(login, _dataContext);

            AuthDTO authDTO = _mapper.Map<AuthDTO>(login);
            authDTO.AccessToken = accesstoken;
            authDTO.RefreshToken = refreshToken;

            return authDTO;
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

            // Proceed with user creation...

            /*            var trimmedEmail = userRequest.Email.Trim();
                        var trimmedUsername = userRequest.UserName.Trim();
                        var trimmedPassword = userRequest.Password.Trim();*/

            userRequest.Email = userRequest.Email?.Trim() ?? string.Empty;
            userRequest.UserName = userRequest.UserName?.Trim() ?? string.Empty;
            userRequest.Password = userRequest.Password?.Trim() ?? string.Empty;

            var oldUser = await _userRepository.GetUserByEmailOrUsernameAsync(userRequest.Email.ToLower(), userRequest.UserName.ToLower());

            if (oldUser != null)
                throw new BadRequestException("Email or Username already used");

            //var newUser = new User();
            var register = _mapper.Map<User>(userRequest);
            var hashPassword = new PasswordHasher<User>()
                .HashPassword(register, userRequest.Password);

            /*            newUser.Email = trimmedEmail;
                        newUser.UserName = trimmedUsername;*/
            register.Password = hashPassword;

            var userRole =  await _roleRepository.GetRoleAsync("User");

            if (userRole == null)
                throw new NotFoundException("Default row not found");

            register.Roles.Add(userRole);

            var newUser = await _authRepository.CreateAsync(register);

            return _mapper.Map<UserDTO>(newUser);
        }
    }
}
