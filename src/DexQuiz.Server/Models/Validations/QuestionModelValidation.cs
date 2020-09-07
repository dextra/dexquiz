using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Server.Models.Validations
{
    public class QuestionModelValidation : AbstractValidator<QuestionModel>
    {
        public QuestionModelValidation()
        {
            RuleFor(x => x.Text)
             .NotEmpty().WithMessage("Text is required")
             .MinimumLength(20);

            RuleFor(x => x.Answers)
             .NotEmpty().WithMessage("Answers is required")
             .Must(x => x.Count == 4).WithMessage("Answers count should be 4")
             .Must(x => x.Any(x => x.IsAnswerCorrect == true))
             .Must(x => x.Where(x => x.IsAnswerCorrect == true).Count() == 1).WithMessage("Just one answer could be right");

            RuleFor(x => x.QuestionLevel).NotNull().WithMessage("QuestionLevel is required")
              .IsInEnum().WithMessage("QuestionLevel should be a valid enum");

            RuleFor(x => x.TrackId)
              .NotNull().WithMessage("TrackId is required")
              .GreaterThan(0);

        }
    }
}
