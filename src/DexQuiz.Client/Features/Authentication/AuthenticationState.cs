using BlazorState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Client.Features.Authentication
{
    public partial class AuthenticationState : BaseState<AuthenticationState>
    {
        public string Token { get; private set; }
        public  MyProperty { get; set; }
        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
