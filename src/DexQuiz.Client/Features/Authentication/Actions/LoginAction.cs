using BlazorState;
using DexQuiz.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Client.Features.Authentication
{
    public partial class AuthenticationState
    {
        public class LoginAction : IAction
        {
            public LoginModel Data { get; private set; }

            public LoginAction(LoginModel data)
            {
                Data = data;
            }
        }
    }
}
