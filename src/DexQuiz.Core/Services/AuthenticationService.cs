using DexQuiz.Core.Enums;
using DexQuiz.Core.Interfaces.Repositories;
using DexQuiz.Core.Interfaces.Services;
using DexQuiz.Core.Interfaces.UoW;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DexQuiz.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;

        }

        public async Task<string> GenerateAuthenticationAsync(string userEmail)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(userEmail);
                var tokenHandler = new JwtSecurityTokenHandler();
                var secretKey = Encoding.UTF8.GetBytes(_configuration["JwtData:JwtSecret"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _configuration["JwtData:Issuer"],
                    Audience = _configuration["JwtData:Issuer"],
                    Subject = GenerateClaims(user.Id, user.UserType),
                    Expires = DateTime.Now.AddHours(Convert.ToInt32(_configuration["JwtData:JwtExpirationHours"])),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> ValidateCredentialsAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return true;
            }

            return false;
        }

        private ClaimsIdentity GenerateClaims(int userId, UserType userTyper)
        {
            return new ClaimsIdentity(new Claim[]
                  {
                    new Claim("userId", userId.ToString()),
                    new Claim(ClaimTypes.Role, userTyper.ToString())
                  });
        }
    }
}
