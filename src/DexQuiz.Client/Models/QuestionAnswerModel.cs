using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Client.Models
{
    public class QuestionAnswerModel
    {
        public QuestionAnswerModel(int questionId, int answerId, int trackId)
        {
            QuestionId = questionId;
            AnswerId = answerId;
            TrackId = trackId;
        }

        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int TrackId { get; set; }
    }
}
