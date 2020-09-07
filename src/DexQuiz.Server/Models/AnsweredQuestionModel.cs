using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Server.Models
{
    public class AnsweredQuestionModel
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
    }
}
