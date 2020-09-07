using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Server.Models.Validations
{
    public class TrackModelValidation : AbstractValidator<TrackModel>
    {
        public TrackModelValidation()
        {
            RuleFor(x => x.Name)
             .NotEmpty()
             .WithMessage("Name is required, 2 characters minimum")
             .MinimumLength(2);

            RuleFor(x => x.ImageUrl)
             .NotEmpty()
             .WithMessage("ImageUrl is required, 3 characters minimum")
             .MinimumLength(3);

            RuleForEach(x => x.Awards)
             .SetValidator(new AwardModelValidation());
        }
    }
}
