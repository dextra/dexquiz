using BlazorState;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DexQuiz.Client.Features.Track
{
    public partial class TrackState
    {
        public class GetNextQuestionHandler : ActionHandler<GetNextQuestionAction>
        {
            public GetNextQuestionHandler(IStore aStore) : base(aStore)
            {
            }

            public override Task<Unit> Handle(GetNextQuestionAction aAction, CancellationToken aCancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
