using System;
using System.Collections.Generic;
using System.Text;

namespace DexQuiz.Core.Entities
{
    public class TrackRanking : BaseEntity
    {
        public int TrackId { get; set; }
        public int UserId { get; set; }
        public int Points { get; set; }
        public DateTime StartedAtUtc { get; set; }
        public TimeSpan? CompletedTime { get; set; }

        public int Position { get; set; }
        public string Username { get; set; }

        public bool IsCompleted() => this.CompletedTime.HasValue;
    }
}
