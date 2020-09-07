using DexQuiz.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DexQuiz.Core.Interfaces.Services
{
    public interface IRankingService
    {
        Task<IEnumerable<TrackRanking>> GetPublicTrackRankingsAsync(int trackId, int? top = null);
        Task<IEnumerable<TrackRanking>> GetTrackRankingsForAdminAsync(int trackId, int? top = null);
        Task<IEnumerable<TrackRanking>> GetTrackRankingsForUserAsync(int trackId, int userId, int? top = null);
        Task InitializeRankingIfNotCreatedAsync(int userId, int trackId);
        Task UpdateRankingAfterUserAnswerAsync(AnsweredQuestion answeredQuestion);
    }
}
