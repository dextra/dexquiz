using DexQuiz.Client.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Client.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(50, ErrorMessage = "Nome deve possuir entre 3 e 50 caracteres", MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Informe um email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(7, ErrorMessage = "Informe uma senha com ao menos 7 dígitos")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório")]
        public double? CellPhone { get; set; }

        [StringLength(100, ErrorMessage = "Perfil do Linkedin deve ter até 100 caracteres")]
        public string Linkedin { get; set; }

        public bool AllowContact { get; set; }
        
        public UserType UserType { get; set; }
    }
}
