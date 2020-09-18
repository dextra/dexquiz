﻿using DexQuiz.Core.Entities;
using DexQuiz.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DexQuiz.Core.Interfaces.Services
{
    public interface IRankingService
    {
        Task<IEnumerable<TrackRanking>> GetPublicTrackRankingsAsync(int trackId, int? top = null, DateTime? date = null);
        Task<IEnumerable<TrackRanking>> GetTrackRankingsForAdminAsync(int trackId, int? top = null, DateTime? date = null);
        Task<IEnumerable<TrackRanking>> GetTrackRankingsForUserAsync(int trackId, int userId, int? top = null, DateTime? date = null);
        Task InitializeRankingIfNotCreatedAsync(int userId, int trackId);
        Task<ReturnData> UpdateRankingAfterUserAnswerAsync(AnsweredQuestion answeredQuestion);
    }
}
