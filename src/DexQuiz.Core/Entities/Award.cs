using DexQuiz.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DexQuiz.Core.Entities
{
    public class Award : AuditableEntity
    {
        public AwardType Type { get; set; }
        public int Position { get; set; }
        public string Description { get; set; }
    }
}
