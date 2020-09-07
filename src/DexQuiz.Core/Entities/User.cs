using DexQuiz.Core.Enums;

namespace DexQuiz.Core.Entities
{
    public class User : AuditableEntity
    {
        public string Name { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        private string _password;
        public string Password
        {
            get { return _password; }

            set
            {
                _password = HashPassword(value);
            }
        }
        public UserType UserType { get; set; }
        public bool AllowContact { get; set; }
        public string Linkedin { get; set; }

        private string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    }
}
