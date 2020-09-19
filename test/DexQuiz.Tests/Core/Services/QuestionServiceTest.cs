using DexQuiz.Core.Entities;
using DexQuiz.Core.Enums;
using DexQuiz.Core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DexQuiz.Tests.Core.Services
{
    public class QuestionServiceTest : Configuration
    {
        private IQuestionService _questionService;
        private ITrackService _trackService;

        [SetUp]
        public void Setup()
        {
            CreateNewDatabaseInMemory();
            _questionService = ServiceProvider.GetService<IQuestionService>();
            _trackService = ServiceProvider.GetService<ITrackService>();

            //Add Track to foreign key
            Track track = new Track
            {
                Name = "Teste Track",
                Available = true
            };
            _trackService.AddTrackAsync(track);

            //var config = new MapperConfiguration(opts =>
            //{
            //    opts.AddProfile(new UserMapping());
            //});
            //var mapper = config.CreateMapper();
        }

        [TestCase("XPTO", "www.url.com", QuestionLevel.Easy, 1, 4, 1, true)]
        [TestCase("XPTO", "www.url.com", QuestionLevel.Easy, 1, 4, 3, false)]
        [TestCase("XPTO", "www.url.com", QuestionLevel.Easy, 1, 0, 0, false)]

        public void AddQuestionTest(string text, string imageUrl, QuestionLevel questionLevel, int trackId, int answerQuantity, int correctAnswerCount, bool expectedResult)
        {
            var questionModel = new Question
            {
                Text = text,
                ImageUrl = imageUrl,
                QuestionLevel = questionLevel,
                TrackId = trackId,
                Answers = SetupAnswers(answerQuantity, correctAnswerCount)
            };

            var result = _questionService.AddQuestionAsync(questionModel).Result;
            
            Assert.IsTrue(result.Result == expectedResult);
        }

        [TestCase("XPTO", "www.url.com", QuestionLevel.Easy, 1, 4, 1, true)]
        [TestCase("XPTO", "www.url.com", QuestionLevel.Easy, 1, 4, 3, false)]

        public void DeleteQuestionTest(string text, string imageUrl, QuestionLevel questionLevel, int trackId, int answerQuantity, int correctAnswerCount, bool expectedResult)
        {
            var questionModel = new Question
            {
                Text = text,
                ImageUrl = imageUrl,
                QuestionLevel = questionLevel,
                TrackId = trackId,
                Answers = SetupAnswers(answerQuantity, correctAnswerCount)
            };

            var result = _questionService.AddQuestionAsync(questionModel).Result;
            var resultDelete = _questionService.DeleteQuestionAsync(1).Result;

            Assert.IsTrue(result.Result == expectedResult);
            Assert.IsTrue(resultDelete.Result == expectedResult);
        }

        [TestCase("XPTO", "www.url.com", QuestionLevel.Easy, 1, 4, 1, 1, 1, true)]
        [TestCase("XPTO", "www.url.com", QuestionLevel.Easy, 1, 4, 1, 1, 5, true)]
        [TestCase("XPTO", "www.url.com", QuestionLevel.Easy, 1, 4, 1, 5, 1, false)]
        public void DoesAnswerBelongToQuestionTest(string text, string imageUrl, QuestionLevel questionLevel, int trackId, int answerQuantity, int correctAnswerCount, int answerId, int questionId, bool expectedResult)
        {
            var questionModel = new Question
            {
                Text = text,
                ImageUrl = imageUrl,
                QuestionLevel = questionLevel,
                TrackId = trackId,
                Answers = SetupAnswers(answerQuantity, correctAnswerCount)
            };

            var result = _questionService.AddQuestionAsync(questionModel).Result;


            var resultMethod = _questionService.DoesAnswerBelongToQuestionAsync(answerId, questionId).Result;

            Assert.IsTrue(result.Result == expectedResult);
            Assert.IsTrue(resultMethod == expectedResult);
        }

        private List<Answer> SetupAnswers(int quantity, int correctAnswerCount)
        {
            List<Answer> answers = new List<Answer>();
            int correctAnswerAllocated = 0;
            bool correctAnswer = true;

            for (int i = 0; i < quantity; i++)
            {
                if (correctAnswerAllocated == correctAnswerCount)
                {
                    correctAnswer = false;
                }

                answers.Add(new Answer() { IsAnswerCorrect = correctAnswer, Text = "Question" });
                correctAnswerAllocated++;
            }

            return answers;
        }
    }
}
