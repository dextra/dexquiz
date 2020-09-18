using DexQuiz.Core.Interfaces.Services;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using DexQuiz.Core.Entities;
using DexQuiz.Core.Enums;
using System.IdentityModel.Tokens.Jwt;
using Moq;
using Microsoft.Extensions.Configuration;
using DexQuiz.Core.Services;
using DexQuiz.Core.Interfaces.Repositories;

namespace DexQuiz.Tests.Core.Services
{
    public class AuthenticationServiceTest : Configuration
    {
        private IAuthenticationService _authenticationService;
        private IUserService _userService;
        private IUserRepository _userRepository;
        [SetUp]
        public void Setup()
        {
            CreateNewDatabaseInMemory();

            Mock<IConfiguration> configuration = new Mock<IConfiguration>();

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.SetupGet(x => x[It.Is<string>(s => s == "JwtData:JwtExpirationHours")]).Returns("123");
            mockConfiguration.SetupGet(x => x[It.Is<string>(s => s == "JwtData:JwtSecret")]).Returns("my dummy jwt secret");
            mockConfiguration.SetupGet(x => x[It.Is<string>(s => s == "JwtData:Issuer")]).Returns("dummy");

            _userService = ServiceProvider.GetService<IUserService>();
            _userRepository = ServiceProvider.GetService<IUserRepository>();

            _authenticationService = new AuthenticationService(mockConfiguration.Object, _userRepository);
        }


        [TestCase("XPTO", "19999999999", "test@test.com", "test123", UserType.Default)]
        [TestCase("XPTO", "19999999998", "test1@test12.com", "test123", UserType.Administrator)]
        public void Should_Return_Valid_Authentication(string userName, string userCellphone, string userEmail, string userPassword, UserType type)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var userModel = new User
            {
                Name = userName,
                CellPhone = userCellphone,
                Email = userEmail,
                Password = userPassword,
                UserType = type
            };

            var result = _userService.AddUser(userModel).Result;
            var user = _userService.FindUserById(1).Result;
            var token = _authenticationService.GenerateAuthenticationAsync(userEmail).Result;

            Assert.IsTrue(result.Result);
            Assert.IsNotNull(user);
            Assert.IsNotNull(token);
            Assert.IsTrue(tokenHandler.CanReadToken(token));
        }

        [TestCase("testCredentials@test.com", "test123", "test@t.com", "test")]
        public void Should_Compare_Credentials_Correctly(string userEmail, string userPassword, string wrongPassword, string wrongEmail)
        {
            var userModel = new User
            {
                Name = "XPTO",
                CellPhone = "19955999999",
                Email = userEmail,
                Password = userPassword,
                UserType = UserType.Default
            };

            var userResult = _userService.AddUser(userModel).Result;
            var user = _userService.FindUserById(1).Result;

            Assert.IsTrue(userResult.Result);
            Assert.IsNotNull(user);
            Assert.IsTrue(_authenticationService.ValidateCredentialsAsync(userEmail, userPassword).Result);
            Assert.IsFalse(_authenticationService.ValidateCredentialsAsync(userEmail, wrongPassword).Result);
            Assert.IsFalse(_authenticationService.ValidateCredentialsAsync(wrongEmail, userPassword).Result);
        }

    }
}
