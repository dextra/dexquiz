using DexQuiz.Core.Enums;

namespace DexQuiz.Server.Models
{
    public class AwardModel
    {
        public AwardType Type { get; set; }

        public int Position { get; set; }

        public string Description { get; set; }
    }
}
