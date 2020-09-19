using DexQuiz.Server.Models;
using DexQuiz.Server.Models.Validations;
using NUnit.Framework;

namespace DexQuiz.Tests.Api.ModelValidation
{
    public class AnsweredQuestionValidationTest : Configuration
    {
        [TestCase(1, 1, true, TestName = "All Valid")]
        [TestCase(1, 0, false, TestName = "QuestionId Invalid")]
        [TestCase(0, 1, false, TestName = "AnswerId Invalid")]
        [TestCase(0, 0, false, TestName = "All invalid")]
        public void AnsweredQuestionModelValidationTest(int answerId, int questionId, bool expectedResult)
        {
            var answeredQuestion = new AnsweredQuestionModel
            {
                AnswerId = answerId,
                QuestionId = questionId
            };

            var validation = new AnsweredQuestionModelValidation();
            var result = validation.Validate(answeredQuestion);
            Assert.IsTrue(result.IsValid == expectedResult);
        }
    }
}
