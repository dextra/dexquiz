using BlazorState;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DexQuiz.Client.Features.Authentication
{
    public partial class AuthenticationState
    {
        public class RecoverPasswordHandler : ActionHandler<RecoverPasswordAction>
        {
            public RecoverPasswordHandler(IStore aStore) : base(aStore)
            {
            }

            public override Task<Unit> Handle(RecoverPasswordAction aAction, CancellationToken aCancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
