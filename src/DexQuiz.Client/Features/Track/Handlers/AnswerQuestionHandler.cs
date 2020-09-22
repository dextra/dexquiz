using Blazored.Toast.Services;
using BlazorState;
using DexQuiz.Client.Models;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DexQuiz.Client.Features.Track
{
    public partial class TrackState
    {
        public class AnswerQuestionHandler : ActionHandler<AnswerQuestionAction>
        {
            private readonly HttpClient _httpClient;
            private readonly NavigationManager _navigationManager;
            private readonly IToastService _toastService;
            private readonly ILogger<AnswerQuestionHandler> _logger;

            public AnswerQuestionHandler(IStore store,
                HttpClient httpClient,
                NavigationManager navigationManager,
                IToastService toastService,
                ILogger<AnswerQuestionHandler> logger) : base(store)
            {
                _httpClient = httpClient;
                _navigationManager = navigationManager;
                _toastService = toastService;
                _logger = logger;
            }
            TrackState State => Store.GetState<TrackState>();

            public override async Task<Unit> Handle(AnswerQuestionAction action, CancellationToken cancellationToken)
            {
                try
                {
                    State.StartLoading();
                    await PostQuestionAnswer(action.Answer, cancellationToken);
                    
                    if (State.RemainingQuestions == State.TotalQuestions)
                    {
                        State.Succeed();
                        _navigationManager.NavigateTo($"/track-result/{action.TrackId}");
                        return await Unit.Task;
                    }
                    
                    State.Question = await GetNextQuestion(action.TrackId, cancellationToken);
                    State.RemainingQuestions = await GetRemainingQuestions(action.TrackId, cancellationToken);
                    State.Succeed();
                }
                catch (UnauthorizedAccessException uae)
                {
                    State.Fail(uae.Message);
                    _navigationManager.NavigateTo("/login");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    State.Fail(ex.Message);
                    _toastService.ShowError(ex.Message, "Login");
                }
                return await Unit.Task;
            }

            private async Task PostQuestionAnswer(QuestionAnswerModel answer, CancellationToken cancellationToken)
            {
                var response = await _httpClient.PostAsJsonAsync<QuestionAnswerModel>($"questions/answer", answer, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized
                    || response.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new UnauthorizedAccessException(response.ReasonPhrase);
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

            private async Task<int> GetRemainingQuestions(int trackId, CancellationToken cancellationToken)
            {
                var response = await _httpClient.GetAsync($"questions/track/{trackId}/progress");

                if (response.IsSuccessStatusCode)
                {
                    var progress = await response.Content.ReadFromJsonAsync<TrackProgressModel>();
                    return State.TotalQuestions - progress.QuestionNumber;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized
                    || response.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new UnauthorizedAccessException(response.ReasonPhrase);
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
            private async Task<QuestionModel> GetNextQuestion(int trackId, CancellationToken cancellationToken)
            {
                var response = await _httpClient.GetAsync($"questions/track/{trackId}", cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<QuestionModel>();
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    _navigationManager.NavigateTo($"/track-result/{trackId}");
                    return null;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized
                    || response.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new UnauthorizedAccessException(response.ReasonPhrase);
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
