using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Client.Models
{
    public class AwardModel
    {
        public AwardType Type { get; set; }

        public int Position { get; set; }

        public string Description { get; set; }

        public enum AwardType
        {
            Global = 1,
            Track = 2,
        }
    }
}
