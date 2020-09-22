using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Client.Models
{
    public class ProfileModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CellPhone { get; set; }
        public string Linkedin { get; set; }
        public bool AllowContact { get; set; }
        public int UserType { get; set; }
    }
}
