using BlazorState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Client.Features.Track
{
    public partial class TrackState
    {
        public class GetNextQuestionAction : IAction
        {
            public int TrackId { get; private set; }

            public GetNextQuestionAction(int trackId)
            {
                TrackId = trackId;
            }
        }
    }
}
