using DexQuiz.Core.Entities;
using DexQuiz.Core.Interfaces.Repositories;
using DexQuiz.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace DexQuiz.Infrastructure.Repositories
{
    public class AnsweredQuestionRepository : GenericRepository<AnsweredQuestion>, IAnsweredQuestionRepository
    {
        public AnsweredQuestionRepository(DexQuizContext context) : base(context)
        {
        }
    }
}
