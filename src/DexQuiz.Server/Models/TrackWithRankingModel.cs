using System.Collections.Generic;

namespace DexQuiz.Server.Models
{
    public class TrackWithRankingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public bool Available { get; set; }

        public IEnumerable<AwardModel> Awards { get; set; }
        public IEnumerable<TrackRankingModel> Ranking { get; set; }
    }
}