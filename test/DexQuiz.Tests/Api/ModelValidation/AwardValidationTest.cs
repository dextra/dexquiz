using DexQuiz.Core.Enums;
using DexQuiz.Server.Models;
using DexQuiz.Server.Models.Validations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DexQuiz.Tests.Api.ModelValidation
{
    public class AwardValidationTest : Configuration
    {
        [TestCase(null, 1, AwardType.Global, false, TestName = "Description null")]
        [TestCase("XPTO", 1, AwardType.Global, false, TestName = "Description less than 10 characteres")]
        [TestCase("Valid Description", 1, AwardType.Global, true, TestName = "Valid description")]
        [TestCase("Valid Description", 0, AwardType.Global, false, TestName = "Position 0")]
        [TestCase("Valid Description", 1, AwardType.Global, true, TestName = "Valid Position")]
        [TestCase("Valid Description", 1, null, false, TestName = "Type null")]
        [TestCase("Valid Description", 1, 999, false, TestName = "Type invalid")]
        [TestCase("Valid Description", 1, AwardType.Global, true, TestName = "Valid Type")]
        [TestCase(null, 0, null, false, TestName = "All invalid")]

        public void AwardModelValidationTest(string description, int position, AwardType type, bool expectedResult)
        {
            var awardModel = new AwardModel
            {
                Description = description,
                Position = position,
                Type = type
            };

            var validation = new AwardModelValidation();
            var result = validation.Validate(awardModel);
            Assert.IsTrue(result.IsValid == expectedResult);
        }
    }
}
