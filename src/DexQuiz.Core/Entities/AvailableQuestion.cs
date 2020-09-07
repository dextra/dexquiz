using System;
using System.Collections.Generic;
using System.Text;

namespace DexQuiz.Core.Entities
{
    public class AvailableQuestion : BaseEntity
    {
        public int TrackId { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
    }
}
