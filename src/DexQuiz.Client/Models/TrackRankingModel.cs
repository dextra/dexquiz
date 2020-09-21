using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Client.Models
{
    public class TrackRankingModel
    {
        public int TrackId { get; set; }
        public int Position { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int Points { get; set; }
        public string CompletedTime { get; set; }

        public TimeSpan CompletedTimeSpan() => !string.IsNullOrWhiteSpace(CompletedTime) ? TimeSpan.Parse(CompletedTime) : new TimeSpan(0, 0, 0);
    }
}
