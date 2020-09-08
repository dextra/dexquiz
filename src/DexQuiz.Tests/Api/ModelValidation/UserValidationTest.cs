using DexQuiz.Core.Enums;
using DexQuiz.Server.Models;
using DexQuiz.Server.Models.Validations;

using NUnit.Framework;

namespace DexQuiz.Tests.Api.ModelValidation
{
    public class UserValidationTest : Configuration
    {
        [TestCase("XPTO", "19999999999", "test@test.com", "test123", UserType.Default, true)]
        [TestCase("XPTO", "19999999999", "test@test.com", "test123", UserType.Administrator, true)]
        [TestCase(null, "19999999999", "test@test.com", "test123", UserType.Default, false)]
        [TestCase("XPTO", null, "test@test.com", "test123", UserType.Default, false)]
        [TestCase("XPTO", null, "test@test.com", "test123", UserType.Default, false)]
        [TestCase("XPTO", "19999999999", "test@test.com", null, UserType.Default, false)]
        [TestCase("XPTO", "19999999999", "test@test.com", "test123", null, false)]

        public void UserModelValidationTest(string userName, string userCellphone, string userEmail, string userPassword, UserType type, bool expectedResult)
        {
            var userModel = new UserModel
            {
                Name = userName,
                CellPhone = userCellphone,
                Email = userEmail,
                Password = userPassword,
                UserType = type
            };

            var validation = new UserModelValidation();
            var result = validation.Validate(userModel);
            Assert.IsTrue(result.IsValid == expectedResult);
        }
    }
}
