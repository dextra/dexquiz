using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Client.Models
{
    public class TrackWithRankingsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public bool Available { get; set; }
        public AwardModel[] Awards { get; set; }
        public TrackRankingModel[] Rankings { get; set; }
    }
}
