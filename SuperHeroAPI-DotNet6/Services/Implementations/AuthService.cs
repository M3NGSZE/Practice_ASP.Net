
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IConfiguration _configuration;

        public AuthService(
            IValidator<UserRequest> userValidator, 
            IValidator<AuthRequest> authValidator, 
            IUserRepository userRepository, 
            IMapper mapper, 
            IRoleRepository roleRepository, 
            IAuthRepository authRepository, 
            IConfiguration configuration
            )
        {
            _userValidator = userValidator;
            _authValidator = authValidator;
            _userRepository = userRepository;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _authRepository = authRepository;
            _configuration = configuration;
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

            var login = await _userRepository.GetUserByEmailOrUsernameAsync(authRequest.Email.ToLower(), "");

            Console.WriteLine("object user login" + login);

            if (login == null)
                throw new NotFoundException("Email not found");

            if (new PasswordHasher<User>().VerifyHashedPassword(login, login.Password, authRequest.Password) == PasswordVerificationResult.Failed)
                throw new BadRequestException("Invalid Email or Password");

            string token = CreateToken(login);

            AuthDTO authDTO = _mapper.Map<AuthDTO>(login);
            authDTO.Token = token;

            return authDTO;
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
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
