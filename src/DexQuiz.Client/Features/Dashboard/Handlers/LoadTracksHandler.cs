using Blazored.Toast.Services;
using BlazorState;
using DexQuiz.Client.Converters;
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
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DexQuiz.Client.Features.Dashboard
{
    public partial class DashboardState
    {
        public class LoadTracksHandler : ActionHandler<LoadTracksAction>
        {
            private readonly HttpClient _httpClient;
            private readonly NavigationManager _navigationManager;
            private readonly IToastService _toastService;
            private readonly ILogger<LoadTracksHandler> _logger;

            public LoadTracksHandler(IStore store,
                    HttpClient httpClient,
                    NavigationManager navigationManager,
                    IToastService toastService,
                    ILogger<LoadTracksHandler> logger) : base(store)
            {
                _httpClient = httpClient;
                _navigationManager = navigationManager;
                _toastService = toastService;
                _logger = logger;
            }

            DashboardState State => Store.GetState<DashboardState>();

            public override async Task<Unit> Handle(LoadTracksAction action, CancellationToken cancellationToken)
            {
                try
                {
                    State.StartLoading();
                    State.Tracks = await GetTracks(action.Date, action.TopRanking, cancellationToken);
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

            private async Task<TrackWithRankingsModel[]> GetTracks(DateTime date, int topRanking, CancellationToken cancellationToken)
            {
                var url = $"ranking/trackwithranking?top={topRanking}&date={date:yyyy-MM-dd}";
                var response = await _httpClient.GetAsync(url, cancellationToken: cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<TrackWithRankingsModel[]>();
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
