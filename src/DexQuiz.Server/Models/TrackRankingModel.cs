using System;

namespace DexQuiz.Server.Models
{
    public class TrackRankingModel
    {
        public int TrackId { get; set; }
        public int Position { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int Points { get; set; }
        public TimeSpan? CompletedTime { get; set; }
    }
}
