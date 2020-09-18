using DexQuiz.Core.Entities;
using DexQuiz.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DexQuiz.Core.Interfaces.Services
{
    public interface IQuestionService
    {
        Task<ReturnData> AddQuestionAsync(Question question);
        Task<Question> FindQuestionByIdAsync(int id);
        Task<IEnumerable<Question>> GetTrackQuestionsAsync(int trackId);
        Task<ReturnData> UpdateQuestionAsync(Question question);
        Task<ReturnData> DeleteQuestionAsync(int questionId);
        Task InitializeQuestionsForUserIfNotCreatedAsync(int userId, int trackId);
        Task<Question> PickQuestionForUserAsync(int userId, int trackId);
        Task<int> PickProgressForUserAsync(int userId, int trackId);
        Task<bool> HasQuestionBeenAnsweredByUserAsync(int userId, int questionId);
        Task<bool> DoesAnswerBelongToQuestionAsync(int answerId, int questionId);
        Task<ReturnData> SaveAnsweredQuestionAsync(AnsweredQuestion answeredQuestion);
        Task<bool> HasUserFinishedTrackAsync(int userId, int trackId);
        Task<bool> HasUserFinishedTrackAsync(AnsweredQuestion answeredQuestion);
    }
}
