using BlazorState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Client.Features.Dashboard
{
    public partial class DashboardState
    {
        public class LoadTrackAction : IAction
        {
            public int TrackId { get; private set; }

            public LoadTrackAction(int trackId)
            {
                TrackId = trackId;
            }
        }
    }
}
