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

namespace DexQuiz.Client.Features.Authentication
{
    public partial class AuthState
    {
        public class RefeshUserDataHandler : ActionHandler<RefreshUserDataAction>
        {
            private readonly HttpClient _httpClient;
            private readonly NavigationManager _navigationManager;
            private readonly IToastService _toastService;
            private readonly ILogger<RefeshUserDataHandler> _logger;

            public RefeshUserDataHandler(IStore store,
                                HttpClient httpClient,
                                NavigationManager navigationManager,
                                IToastService toastService,
                                ILogger<RefeshUserDataHandler> logger) : base(store)
            {
                _httpClient = httpClient;
                _navigationManager = navigationManager;
                _toastService = toastService;
                _logger = logger;
            }

            AuthState State => Store.GetState<AuthState>();

            public override async Task<Unit> Handle(RefreshUserDataAction action, CancellationToken cancellationToken)
            {
                try
                {
                    State.StartLoading();
                    State.User = await GetUserData(cancellationToken);
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

            private async Task<ProfileModel> GetUserData(CancellationToken cancellationToken = default)
            {
                var response = await _httpClient.GetAsync("User", cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ProfileModel>();
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
