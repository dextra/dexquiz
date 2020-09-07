using System;
using System.Collections.Generic;
using System.Text;

namespace DexQuiz.Core.Entities
{
    public class Track : AuditableEntity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public bool Available { get; set; }

        public virtual IList<Award> Awards { get; set; }
    }
}
