using DexQuiz.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Server.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CellPhone { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public UserType UserType { get; set; }

        public bool AllowContact { get; set; }

        public string Linkedin { get; set; }
    }
}
