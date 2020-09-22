using Blazored.Toast.Services;
using BlazorState;
using DexQuiz.Client.Providers;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DexQuiz.Client.Features.Authentication
{
    public partial class AuthState
    {
        public class LogoutHandler : ActionHandler<LogoutAction>
        {
            private readonly AuthenticationStateProvider _authenticationStateProvider;
            private readonly NavigationManager _navigationManager;
            private readonly IToastService _toastService;
            private readonly ILogger<LogoutHandler> _logger;

            public LogoutHandler(IStore store,
                    AuthenticationStateProvider authenticationStateProvider,
                    NavigationManager navigationManager,
                    IToastService toastService,
                    ILogger<LogoutHandler> logger) : base(store)
            {
                _authenticationStateProvider = authenticationStateProvider;
                _navigationManager = navigationManager;
                _toastService = toastService;
                _logger = logger;
            }

            AuthState State => Store.GetState<AuthState>();

            public override async Task<Unit> Handle(LogoutAction action, CancellationToken cancellationToken)
            {
                try
                {
                    State.StartLoading();
                    State.User = null;
                    await ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
                    State.Succeed();
                    _navigationManager.NavigateTo("/login");
                }
                catch (UnauthorizedAccessException uae)
                {
                    State.Fail(uae.Message);
                    _toastService.ShowError(uae.Message, "Login");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    State.Fail(ex.Message);
                    _toastService.ShowError(ex.Message, "Login");
                }
                return await Unit.Task;
            }
        }
    }
}
