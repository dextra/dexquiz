using Blazored.Toast.Services;
using BlazorState;
using DexQuiz.Client.Models;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DexQuiz.Client.Features.Track
{
    public partial class TrackState
    {
        public class LoadTrackHandler : ActionHandler<LoadTrackAction>
        {
            private readonly HttpClient _httpClient;
            private readonly NavigationManager _navigationManager;
            private readonly IToastService _toastService;
            private readonly ILogger<LoadTrackHandler> _logger;

            public LoadTrackHandler(IStore store,
                HttpClient httpClient,
                NavigationManager navigationManager,
                IToastService toastService,
                ILogger<LoadTrackHandler> logger) : base(store)
            {
                _httpClient = httpClient;
                _navigationManager = navigationManager;
                _toastService = toastService;
                _logger = logger;
            }

            TrackState State => Store.GetState<TrackState>();

            public override async Task<Unit> Handle(LoadTrackAction action, CancellationToken cancellationToken)
            {
                try
                {
                    State.StartLoading();
                    State.Track = await GetTrack(action.TrackId, cancellationToken);
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

            private async Task<TrackModel> GetTrack(int trackId, CancellationToken cancellationToken)
            {
                var response = await _httpClient.GetAsync($"tracks/{trackId}", cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<TrackModel>();
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
