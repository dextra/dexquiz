using System.Collections.Generic;

namespace DexQuiz.Core.Entities
{
    public class TrackWithRanking : Track
    {
        public IEnumerable<TrackRanking> Rankings { get; set; }

        public TrackWithRanking(Track track, IEnumerable<TrackRanking> ranking)
        {
            Id = track.Id;
            Name = track.Name;
            ImageUrl = track.ImageUrl;
            Available = track.Available;
            Awards = track.Awards;
            Rankings = ranking;
        }
    }
}