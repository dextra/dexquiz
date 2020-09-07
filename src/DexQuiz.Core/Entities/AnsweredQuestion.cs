using System;
using System.Collections.Generic;
using System.Text;

namespace DexQuiz.Core.Entities
{
    public class AnsweredQuestion : BaseEntity
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int TrackId { get; set; }
    }
}
