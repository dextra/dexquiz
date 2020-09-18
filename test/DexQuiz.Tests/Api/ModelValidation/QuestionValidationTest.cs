using DexQuiz.Core.Enums;
using DexQuiz.Infrastructure.Persistence.Configuration;
using DexQuiz.Server.Models;
using DexQuiz.Server.Models.Validations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DexQuiz.Tests.Api.ModelValidation
{
    public class QuestionValidationTest : Configuration
    {
        [TestCase(null, 4, QuestionLevel.Easy, 1, 1, false, TestName = "Text null")]
        [TestCase("Invalid Text", 4, QuestionLevel.Easy, 1, 1, false, TestName = "Text with less than 20 characteres")]
        [TestCase("Valid text with more than 20 char", 4, QuestionLevel.Easy, 1, 1, true, TestName = "Valid Test")]
        [TestCase("Valid text with more than 20 char", 0, QuestionLevel.Easy, 1, 1, false, TestName = "Zero answers")]
        [TestCase("Valid text with more than 20 char", 1, QuestionLevel.Easy, 1, 1, false, TestName = "Less than 4 answers")] 
        [TestCase("Valid text with more than 20 char", 5, QuestionLevel.Easy, 1, 1, false, TestName = "More than 4 answers")]
        [TestCase("Valid text with more than 20 char", 4, QuestionLevel.Easy, 1, 0, false, TestName = "No one answer was correct")]
        [TestCase("Valid text with more than 20 char", 4, QuestionLevel.Easy, 1, 2, false, TestName = "More than one correct Answer")]
        [TestCase("Valid text with more than 20 char", 4, null, 1, 1, false, TestName = "Question Level null")]
        [TestCase("Valid text with more than 20 char", 4, 99999, 1, 1, false, TestName = "Invalid Question Level")]
        [TestCase("Valid text with more than 20 char", 4, QuestionLevel.Easy, 0, 1, false, TestName = "Invalid TrackId")]

        public void QuestionModelValidationTest(string text, int answerQuantity, QuestionLevel questionLevel, int trackId, int correctAnswerCount, bool expectedResult)
        {
            var questionModel = new QuestionModel
            {
                Text = text,
                Answers = SetupAnswers(answerQuantity, correctAnswerCount),
                QuestionLevel = questionLevel,
                TrackId = trackId
            };

            var validation = new QuestionModelValidation();
            var result = validation.Validate(questionModel);
            Assert.IsTrue(result.IsValid == expectedResult);
        }

        private List<AnswerModel> SetupAnswers(int quantity, int correctAnswerCount)
        {
            List<AnswerModel> answers = new List<AnswerModel>();
            int correctAnswerAllocated = 0;
            bool correctAnswer = true;

            for (int i = 0; i < quantity; i++)
            {
                if (correctAnswerAllocated == correctAnswerCount)
                {
                    correctAnswer = false;
                }

                answers.Add(new AnswerModel() { IsAnswerCorrect = correctAnswer });
                correctAnswerAllocated++;
            }

            return answers;
        }
    }
}
