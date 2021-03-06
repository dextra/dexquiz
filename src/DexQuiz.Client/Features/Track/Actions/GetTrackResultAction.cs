﻿using BlazorState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Client.Features.Track
{
    public partial class TrackState
    {
        public class GetTrackResultAction : IAction
        {
            public int TrackId { get; set; }

            public GetTrackResultAction(int trackId)
            {
                TrackId = trackId;
            }
        }
    }
}
