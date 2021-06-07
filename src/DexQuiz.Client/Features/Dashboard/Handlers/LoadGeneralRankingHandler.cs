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
        public class LoadGeneralRankingHandler : ActionHandler<LoadGeneralRankingAction>
        {
            private readonly HttpClient _httpClient;
            private readonly NavigationManager _navigationManager;
            private readonly IToastService _toastService;
            private readonly ILogger<LoadGeneralRankingHandler> _logger;

            public LoadGeneralRankingHandler(IStore store,
                    HttpClient httpClient,
                    NavigationManager navigationManager,
                    IToastService toastService,
                    ILogger<LoadGeneralRankingHandler> logger) : base(store)
            {
                _httpClient = httpClient;
                _navigationManager = navigationManager;
                _toastService = toastService;
                _logger = logger;
            }

            DashboardState State => Store.GetState<DashboardState>();

            public override async Task<Unit> Handle(LoadGeneralRankingAction action, CancellationToken cancellationToken)
            {
                try
                {
                    State.StartLoading();
                    State.Rankings = await GetRankings(action.TopRanking, cancellationToken);
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
                    _toastService.ShowError(ex.Message, "Dashboard");
                }
                return await Unit.Task;
            }

            private async Task<GeneralUserRanking[]> GetRankings(int topRanking, CancellationToken cancellationToken)
            {
                var url = $"ranking/general?top={topRanking}";
                var response = await _httpClient.GetAsync(url, cancellationToken: cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<GeneralUserRanking[]>();
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
