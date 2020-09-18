using DexQuiz.Core.Entities;
using DexQuiz.Core.Exceptions;
using DexQuiz.Core.Interfaces.Repositories;
using DexQuiz.Core.Interfaces.Services;
using DexQuiz.Core.Interfaces.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Core.Services
{
    public class RankingService : IRankingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITrackRankingRepository _trackRankingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionService _questionService;
        private readonly ITrackService _trackService;

        private const int DefaultNumberOfRankingsFetched = 3;

        public RankingService(IUnitOfWork unitOfWork,
            ITrackRankingRepository trackRankingRepository,
            IUserRepository userRepository,
            IQuestionRepository questionRepository,
            IAnswerRepository answerRepository,
            IQuestionService questionService,
            ITrackService trackService)
        {
            _unitOfWork = unitOfWork;
            _trackRankingRepository = trackRankingRepository;
            _userRepository = userRepository;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _questionService = questionService;
            _trackService = trackService;
        }

        public async Task<IEnumerable<TrackWithRanking>> GetAllPublicTracksWithRankingsAsync(int? top = null, DateTime? date = null)
        {
            var availableTracks = await _trackService.GetAllAvailableTracksAsync();
            var taskList = new List<Task<TrackWithRanking>>();

            Parallel.ForEach(availableTracks, (track) =>
            {
                taskList.Add(Task.Run(async () =>
                {
                    var resultado = await GetPublicTrackRankingsAsync(track.Id, top, date);

                    return new TrackWithRanking(track, resultado);
                }));
            });

            return await Task.WhenAll(taskList);
        }

        public async Task<IEnumerable<TrackWithRanking>> GetAllTracksWithRankingsForAdminAsync(int? top = null, DateTime? date = null)
        {
            var availableTracks = await _trackService.GetAllAvailableTracksAsync();
            var taskList = new List<Task<TrackWithRanking>>();

            Parallel.ForEach(availableTracks, (track) =>
            {
                taskList.Add(Task.Run(async () =>
                {
                    var resultado = await GetTrackRankingsForAdminAsync(track.Id, top, date);

                    return new TrackWithRanking(track, resultado);
                }));
            });

            return await Task.WhenAll(taskList);
        }

        public async Task<IEnumerable<TrackWithRanking>> GetAllTracksWithRankingsForUserAsync(int userId, int? top = null, DateTime? date = null)
        {
            var availableTracks = await _trackService.GetAllAvailableTracksAsync();
            var taskList = new List<Task<TrackWithRanking>>();

            Parallel.ForEach(availableTracks, (track) =>
            {
                taskList.Add(Task.Run(async () =>
                {
                    var resultado = await GetTrackRankingsForUserAsync(track.Id, userId, top, date);

                    return new TrackWithRanking(track, resultado);
                }));
            });

            return await Task.WhenAll(taskList);
        }

        public async Task<IEnumerable<TrackRanking>> GetPublicTrackRankingsAsync(int trackId, int? top = null, DateTime? date = null) =>
            (await GetOrderedTrackRankingsAsync(trackId, date))
            .Take(top ?? DefaultNumberOfRankingsFetched)
            .Select(MapTrackRankingWithoutUserInfo);

        public async Task<IEnumerable<TrackRanking>> GetTrackRankingsForAdminAsync(int trackId, int? top = null, DateTime? date = null) =>
            (await GetOrderedTrackRankingsAsync(trackId, date))
            .Take(top ?? DefaultNumberOfRankingsFetched)
            .Select(async tr => await MapTrackRankingWithUserInfo(tr))
            .Select(tr => tr.Result);

        public async Task<IEnumerable<TrackRanking>> GetTrackRankingsForUserAsync(int trackId, int userId, int? top = null, DateTime? date = null)
        {
            var trackRankings = await GetOrderedTrackRankingsAsync(trackId, date);
            var topRankings = trackRankings.Take(top ?? DefaultNumberOfRankingsFetched);
            var userTrackRanking = trackRankings.FirstOrDefault(tr => tr.UserId == userId);

            bool isUserInRanking = userTrackRanking != null;
            bool isUserInTopRanking = topRankings.Any(tr => tr.UserId == userId);
            var rankingsForUser = (isUserInRanking && !isUserInTopRanking) ?
                                  topRankings.Append(userTrackRanking) : topRankings;

            return rankingsForUser
                   .Select(r => r.UserId == userId ? MapTrackRankingWithUserInfo(r).GetAwaiter().GetResult() : MapTrackRankingWithoutUserInfo(r));
        }

        public async Task InitializeRankingIfNotCreatedAsync(int userId, int trackId)
        {
            bool userHasRankingInTrack = (await _trackRankingRepository
                                                    .FindAsync(tr => tr.TrackId == trackId && tr.UserId == userId))
                                                    .Any();
            if (!userHasRankingInTrack)
            {
                var trackRanking = new TrackRanking()
                {
                    TrackId = trackId,
                    UserId = userId,
                    Points = 0,
                    StartedAtUtc = DateTime.UtcNow,
                    CompletedTime = null
                };
                await _trackRankingRepository.AddAsync(trackRanking);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<ProcessResult> UpdateRankingAfterUserAnswerAsync(AnsweredQuestion answeredQuestion)
        {
            var question = await _questionRepository.FindAsync(answeredQuestion.QuestionId);
            var answer = await _answerRepository.FindAsync(answeredQuestion.AnswerId);

            if (!await _questionService.DoesAnswerBelongToQuestionAsync(answeredQuestion.AnswerId, answeredQuestion.QuestionId))
            {
                return new ProcessResult { Message = "A resposta não é uma das respostas possíveis para a questão.", Result = false };
            }

            bool didUserCompleteTrack = await _questionService.HasUserFinishedTrackAsync(answeredQuestion.UserId, question.TrackId);

            if (answer.IsAnswerCorrect || didUserCompleteTrack)
            {
                var trackRanking = (await _trackRankingRepository
                                          .FindAsync(tr => tr.TrackId == question.TrackId && tr.UserId == answeredQuestion.UserId))
                                          .FirstOrDefault();
                if (answer.IsAnswerCorrect)
                {
                    trackRanking.Points += (int)question.QuestionLevel;
                }
                if (didUserCompleteTrack)
                {
                    trackRanking.CompletedTime = DateTime.UtcNow - trackRanking.StartedAtUtc;
                }
                _trackRankingRepository.Update(trackRanking);
                await _unitOfWork.CommitAsync();
                return new ProcessResult { Result = true };
            }
            else
            {
                return new ProcessResult { Result = false };
            }
        }

        private async Task<IEnumerable<TrackRanking>> GetOrderedTrackRankingsAsync(int trackId, DateTime? date)
        {
            IEnumerable<TrackRanking> trackRankings = null;
            if (date.HasValue)
                trackRankings = await _trackRankingRepository.FindAsync(r => r.TrackId == trackId && r.CompletedTime != null && r.StartedAtUtc.Date == date.Value.Date);
            else
                trackRankings = await _trackRankingRepository.FindAsync(r => r.TrackId == trackId && r.CompletedTime != null);

            return trackRankings.OrderByDescending(r => r.Points)
                                .ThenBy(r => r.CompletedTime)
                                .Select(MapTrackRankingInsertingPosition);
        }

        private async Task<TrackRanking> MapTrackRankingWithUserInfo(TrackRanking trackRanking) =>
            new TrackRanking()
            {
                Position = trackRanking.Position,
                Id = trackRanking.Id,
                TrackId = trackRanking.TrackId,
                UserId = trackRanking.UserId,
                Username = (await _userRepository.FindAsync(trackRanking.UserId))?.Name,
                Points = trackRanking.Points,
                CompletedTime = trackRanking.CompletedTime
            };

        private TrackRanking MapTrackRankingWithoutUserInfo(TrackRanking trackRanking) =>
            new TrackRanking()
            {
                Position = trackRanking.Position,
                Id = trackRanking.Id,
                TrackId = trackRanking.TrackId,
                UserId = 0,
                Username = null,
                Points = trackRanking.Points,
                CompletedTime = trackRanking.CompletedTime
            };

        private TrackRanking MapTrackRankingInsertingPosition(TrackRanking trackRanking, int index) =>
            new TrackRanking()
            {
                Position = index + 1,
                Id = trackRanking.Id,
                TrackId = trackRanking.TrackId,
                UserId = trackRanking.UserId,
                Points = trackRanking.Points,
                CompletedTime = trackRanking.CompletedTime
            };
    }
}
