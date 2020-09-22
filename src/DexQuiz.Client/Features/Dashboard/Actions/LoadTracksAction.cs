using BlazorState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Client.Features.Dashboard
{
    public partial class DashboardState
    {
        public class LoadTracksAction : IAction
        {
            public LoadTracksAction(int topRanking, DateTime date)
            {
                TopRanking = topRanking;
                Date = date;
            }

            public int TopRanking { get; private set; }
            public DateTime Date { get; private set; }
        }
    }
}
