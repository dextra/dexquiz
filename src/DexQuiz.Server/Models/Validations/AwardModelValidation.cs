using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Server.Models.Validations
{
    public class AwardModelValidation : AbstractValidator<AwardModel>
    {
        public AwardModelValidation()
        {
            RuleFor(x => x.Type)
              .NotNull()
              .WithMessage("Award type is required")
              .IsInEnum()
              .WithMessage("Award type should be a valid enum");

            RuleFor(x => x.Position)
              .NotNull()
              .WithMessage("Position is required")
              .GreaterThan(0);

            RuleFor(x => x.Description)
             .NotEmpty()
             .WithMessage("Description is required, 10 characters minimum")
             .MinimumLength(10);
        }
    }
}
