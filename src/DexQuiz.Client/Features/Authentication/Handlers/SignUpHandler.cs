using Blazored.Toast.Services;
using BlazorState;
using DexQuiz.Client.Models;
using DexQuiz.Client.Providers;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DexQuiz.Client.Features.Authentication
{
    public partial class AuthState
    {
        public class SignUpHandler : ActionHandler<SignUpAction>
        {
            private readonly HttpClient _httpClient;
            private readonly AuthenticationStateProvider _authenticationStateProvider;
            private readonly NavigationManager _navigationManager;
            private readonly IToastService _toastService;
            private readonly ILogger<SignUpHandler> _logger;

            public SignUpHandler(IStore store,
                                HttpClient httpClient,
                                AuthenticationStateProvider authenticationStateProvider,
                                NavigationManager navigationManager,
                                IToastService toastService,
                                ILogger<SignUpHandler> logger) : base(store)
            {
                _httpClient = httpClient;
                _authenticationStateProvider = authenticationStateProvider;
                _navigationManager = navigationManager;
                _toastService = toastService;
                _logger = logger;
            }

            AuthState State => Store.GetState<AuthState>();

            public override async Task<Unit> Handle(SignUpAction action, CancellationToken cancellationToken)
            {
                try
                {
                    State.StartLoading();
                    await SignUp(action.Data, cancellationToken);
                    State.User = await GetUserData(cancellationToken);
                    State.Succeed();
                    _navigationManager.NavigateTo("dashboard");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    State.Fail(ex.Message);
                    _toastService.ShowError(ex.Message, "Login");
                }

                return await Unit.Task;
            }

            private async Task SignUp(UserModel userData, CancellationToken cancellationToken = default)
            {
                var response = await _httpClient.PostAsJsonAsync<UserModel>("User", userData, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadFromJsonAsync<AuthenticationTokenModel>(cancellationToken: cancellationToken);
                    await ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(userData.Email, content.Token);
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

            private async Task<UserModel> GetUserData(CancellationToken cancellationToken = default)
            {
                return await _httpClient.GetFromJsonAsync<UserModel>("User", cancellationToken);
            }
        }
    }
}
