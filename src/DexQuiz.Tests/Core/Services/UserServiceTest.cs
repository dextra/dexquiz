using AutoMapper;
using DexQuiz.Core.Entities;
using DexQuiz.Core.Enums;
using DexQuiz.Core.Interfaces.Services;
using DexQuiz.Server.Controllers;
using DexQuiz.Server.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using NUnit.Framework;

using System;
using System.Linq;
using System.Security.Claims;

namespace DexQuiz.Tests.Core.Services
{
    public class UserServiceTest : Configuration
    {
        private IUserService _userService;
        private UserController _userController;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            CreateNewDatabaseInMemory();
            _userService = ServiceProvider.GetService<IUserService>();
            _mapper = ServiceProvider.GetService<IMapper>();

            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile(new UserMapping());
            });
            var mapper = config.CreateMapper();

            _userController = new UserController(_userService, mapper);
        }

        [TestCase("XPTO", "19999999999", "test@test.com", "test123", UserType.Default, true)]
        [TestCase("XPTO", "19999999998", "test1@test12.com", "test123", UserType.Administrator, true)]
        public void AddUserTest(string userName, string userCellphone, string userEmail, string userPassword, UserType type, bool expectedResult)
        {
            var userModel = new User
            {
                Name = userName,
                CellPhone = userCellphone,
                Email = userEmail,
                Password = userPassword,
                UserType = type
            };

            var result = _userService.AddUser(userModel).Result;
            Assert.IsTrue(result == expectedResult);
        }

        [TestCase("XPTO", "1932353645", "testdumb@test.com", "test123", UserType.Default, true)]
        public void IsUserEmailValidTest(string userName, string userCellphone, string userEmail, string userPassword, UserType type, bool expectedResult)
        {
            var userModel = new User
            {
                Name = userName,
                CellPhone = "1911555551",
                Email = userEmail,
                Password = userPassword,
                UserType = type
            };

            var result = _userService.AddUser(userModel).Result;

            Assert.IsTrue(result == expectedResult);
            Assert.ThrowsAsync(typeof(Exception), () => _userService.AddUser(userModel));
        }

        [TestCase("XPTO", "1912121212", "test@test.com", "test123", UserType.Default, true)]
        public void IsUserCellphoneValidTest(string userName, string userCellphone, string userEmail, string userPassword, UserType type, bool expectedResult)
        {

            var userModel = new User
            {
                Name = userName,
                CellPhone = userCellphone,
                Email = "test@test.com.br",
                Password = userPassword,
                UserType = type
            };

            var result = _userService.AddUser(userModel).Result;

            Assert.IsTrue(result == expectedResult);
            Assert.ThrowsAsync(typeof(Exception), () => _userService.AddUser(userModel));
        }

        [TestCase("TesteLoggedUser", "19123456789", "loggedSuccess@logged.com", "test123", UserType.Default, true)]
        public void GetLoggedUserTest(string userName, string userCellphone, string userEmail, string userPassword, UserType type, bool expectedResult)
        {
            var userModel = new User
            {
                Name = userName,
                CellPhone = userCellphone,
                Email = userEmail,
                Password = userPassword,
                UserType = type
            };

            var resultAdd = _userService.AddUser(userModel).Result;

            ClaimsPrincipal user = new ClaimsPrincipal();
            ClaimsIdentity claims = new ClaimsIdentity();
            claims.AddClaim(new Claim("userId", "1"));
            user.AddIdentity(claims);

            _userController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user
                }
            };

            var result = _userController.GetLoggedUserAsync().Result != null;
            Assert.IsTrue(result == expectedResult);
        }
    }
}
