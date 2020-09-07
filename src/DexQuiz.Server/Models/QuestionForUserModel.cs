using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Server.Models
{
    public class QuestionForUserModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public IList<AnswerForUserModel> Answers { get; set; }
        public int TrackId { get; set; }
    }
}
