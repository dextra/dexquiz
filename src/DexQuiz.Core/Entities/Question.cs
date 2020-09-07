using DexQuiz.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DexQuiz.Core.Entities
{
    public class Question : AuditableEntity
    {
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public QuestionLevel QuestionLevel { get; set; }
        public int TrackId { get; set; }
        public virtual IList<Answer> Answers { get; set; }
    }
}
