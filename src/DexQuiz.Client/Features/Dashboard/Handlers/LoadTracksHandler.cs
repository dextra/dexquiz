using BlazorState;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DexQuiz.Client.Features.Dashboard
{
    public partial class DashboardState
    {
        public class LoadTracksHandler : ActionHandler<LoadTracksAction>
        {
            public LoadTracksHandler(IStore aStore) : base(aStore)
            {
            }

            public override Task<Unit> Handle(LoadTracksAction aAction, CancellationToken aCancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
