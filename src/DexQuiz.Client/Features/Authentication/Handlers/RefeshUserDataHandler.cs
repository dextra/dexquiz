using Blazored.Toast.Services;
using BlazorState;
using DexQuiz.Client.Models;
using MediatR;
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
    public partial class AuthenticationState
    {
        public class RefeshUserDataHandler : ActionHandler<RefreshUserDataAction>
        {
            private readonly HttpClient _httpClient;
            private readonly IToastService _toastService;
            private readonly ILogger _logger;

            public RefeshUserDataHandler(IStore store,
                                HttpClient httpClient,
                                IToastService toastService,
                                ILogger logger) : base(store)
            {
                _httpClient = httpClient;
                _toastService = toastService;
                _logger = logger;
            }

            AuthenticationState State => Store.GetState<AuthenticationState>();

            public override async Task<Unit> Handle(RefreshUserDataAction action, CancellationToken cancellationToken)
            {
                try
                {
                    State.StartLoading();
                    State.User = await GetUserData(cancellationToken);
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

            private async Task<UserModel> GetUserData(CancellationToken cancellationToken = default)
            {
                return await _httpClient.GetFromJsonAsync<UserModel>("User", cancellationToken);
            }
        }
    }
}
