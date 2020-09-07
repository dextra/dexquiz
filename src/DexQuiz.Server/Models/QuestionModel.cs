using DexQuiz.Core.Enums;
using System.Collections.Generic;

namespace DexQuiz.Server.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string ImageUrl { get; set; }

        public QuestionLevel QuestionLevel { get; set; }

        public IList<AnswerModel> Answers { get; set; }

        public int TrackId { get; set; }
    }
}
