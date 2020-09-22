using Blazored.Toast.Services;
using BlazorState;
using DexQuiz.Client.Converters;
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
using System.Text.Json;
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
                    _navigationManager.NavigateTo("/");

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    State.Fail(ex.Message);
                    _toastService.ShowError(ex.Message, "Cadastro");
                }

                return await Unit.Task;
            }

            private async Task SignUp(UserModel userData, CancellationToken cancellationToken = default)
            {
                userData.UserType = Models.Enums.UserType.Default;
                var response = await _httpClient.PostAsJsonAsync<UserModel>("User", userData, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    await Login(new LoginModel(userData.Email, userData.Password));
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var content = await response.Content.ReadFromJsonAsync<ResultModel>(cancellationToken: cancellationToken);
                    if (content != null && content.Message != null)
                    {
                        throw new Exception(content.Message);
                    }
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

            private async Task<ProfileModel> GetUserData(CancellationToken cancellationToken = default)
            {
                return await _httpClient.GetFromJsonAsync<ProfileModel>("User", cancellationToken);
            }

            private async Task Login(LoginModel loginData, CancellationToken cancellationToken = default)
            {
                var response = await _httpClient.PostAsJsonAsync<LoginModel>("Authentication", loginData, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadFromJsonAsync<AuthenticationTokenModel>(cancellationToken: cancellationToken);
                    await ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginData.Email, content.Token);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var content = await response.Content.ReadFromJsonAsync<ResultModel>(cancellationToken: cancellationToken);
                    if (content != null && content.Message != null)
                    {
                        throw new Exception(content.Message);
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
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
