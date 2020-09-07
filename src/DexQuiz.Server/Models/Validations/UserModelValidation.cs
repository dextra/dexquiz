using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Server.Models.Validations
{
    public class UserModelValidation : AbstractValidator<UserModel>
    {
        public UserModelValidation()
        {
            RuleFor(x => x.Name)
             .NotEmpty().WithMessage("O campo Nome é obrigatório")
             .Length(3, 100).WithMessage("Esse campo deve possuir no minimo 3 caracteres e no máximo 100");

            RuleFor(x => x.CellPhone)
             .NotEmpty().WithMessage("O campo Celular é obrigatório")
             .Length(10, 11);

            RuleFor(x => x.Email)
             .NotEmpty().WithMessage("O campo Email é obrigatório")
             .Length(10, 50).WithMessage("Esse campo deve possuir no minimo 10 caracteres e no máximo 50"); ;

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("O campo Senha é obrigatório")
            .Length(7, 50);

            RuleFor(x => x.UserType)
            .IsInEnum()
            .WithMessage("O tipo de usuário deve ser válido");
        }
    }
}
