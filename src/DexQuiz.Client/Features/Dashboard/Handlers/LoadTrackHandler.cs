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
        public class LoadTrackHandler : ActionHandler<LoadTrackAction>
        {
            public LoadTrackHandler(IStore store) : base(store)
            {
            }

            public override Task<Unit> Handle(LoadTrackAction action, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
