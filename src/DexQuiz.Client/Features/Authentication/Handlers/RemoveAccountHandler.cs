using Blazored.Toast.Services;
using BlazorState;
using MediatR;
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
        public class RemoveAccountHandler : ActionHandler<RemoveAccountAction>
        {
            private readonly HttpClient _httpClient;
            private readonly IToastService _toastService;
            private readonly ILogger<RemoveAccountHandler> _logger;

            public RemoveAccountHandler(IStore store,
                    HttpClient httpClient,
                    IToastService toastService,
                    ILogger<RemoveAccountHandler> logger) : base(store)
            {
                _httpClient = httpClient;
                _toastService = toastService;
                _logger = logger;
            }

            AuthState State => Store.GetState<AuthState>();

            public override async Task<Unit> Handle(RemoveAccountAction aAction, CancellationToken aCancellationToken)
            {
                try
                {
                    State.StartLoading();
                    await RemoveAccount();
                    State.Succeed();
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

            private async Task RemoveAccount()
            {
                var response = await _httpClient.DeleteAsync("User/RequestRemoval");
                if (response.IsSuccessStatusCode)
                {
                    return;
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
