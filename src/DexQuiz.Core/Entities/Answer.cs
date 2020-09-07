using System;
using System.Collections.Generic;
using System.Text;

namespace DexQuiz.Core.Entities
{
    public class Answer : AuditableEntity
    {
        public string Text { get; set; }
        public bool IsAnswerCorrect { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}
