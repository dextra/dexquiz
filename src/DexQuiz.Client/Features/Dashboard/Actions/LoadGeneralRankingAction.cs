using BlazorState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Client.Features.Dashboard
{
    public partial class DashboardState
    {
        public class LoadGeneralRankingAction : IAction
        {
            public LoadGeneralRankingAction(int topRanking)
            {
                TopRanking = topRanking;
            }

            public int TopRanking { get; private set; }
        }
    }
}
