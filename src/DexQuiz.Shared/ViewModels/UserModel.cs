using System;
using System.Collections.Generic;
using System.Text;

namespace DexQuiz.Shared.Models
{
    public sealed class UserModel
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
