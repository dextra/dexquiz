namespace DexQuiz.Server.Models
{
    public class AnswerModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsAnswerCorrect { get; set; }
    }
}
