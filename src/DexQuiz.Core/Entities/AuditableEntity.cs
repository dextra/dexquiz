using System;
using System.Collections.Generic;
using System.Text;

namespace DexQuiz.Core.Entities
{
    public abstract class AuditableEntity : BaseEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }
}
