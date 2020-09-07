using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Server.Models.Validations
{
    public class AnsweredQuestionModelValidation : AbstractValidator<AnsweredQuestionModel>
    {
        public AnsweredQuestionModelValidation()
        {
            RuleFor(x => x.QuestionId)
              .NotNull().WithMessage("QuestionId is required")
              .GreaterThan(0);

            RuleFor(x => x.AnswerId)
              .NotNull().WithMessage("AnswerId is required")
              .GreaterThan(0);
        }
    }
}
