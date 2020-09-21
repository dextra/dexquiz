using Blazored.Toast.Services;
using BlazorState;
using DexQuiz.Client.Converters;
using DexQuiz.Client.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
            private readonly IToastService _toastService;
            private readonly ILogger<LoadTracksHandler> _logger;

            public LoadTracksHandler(IStore store,
                    HttpClient httpClient,
                    IToastService toastService,
                    ILogger<LoadTracksHandler> logger) : base(store)
            {
                _httpClient = httpClient;
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
                return await _httpClient.GetFromJsonAsync<TrackWithRankingsModel[]>(url, cancellationToken);
            }
        }
    }
}
