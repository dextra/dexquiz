using System.Collections.Generic;

namespace DexQuiz.Server.Models
{
    public class TrackModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public bool Available { get; set; }

        public IList<AwardModel> Awards { get; set; }


    }
}
