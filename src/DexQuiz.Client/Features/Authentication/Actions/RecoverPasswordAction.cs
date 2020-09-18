using BlazorState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Client.Features.Authentication
{
    public partial class AuthenticationState
    {
        public class RecoverPasswordAction : IAction
        {
            public string Email { get; private set; }

            public RecoverPasswordAction(string email)
            {
                Email = email;
            }
        }
    }
}
