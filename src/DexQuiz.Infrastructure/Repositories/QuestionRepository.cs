using DexQuiz.Core.Entities;
using DexQuiz.Core.Interfaces.Repositories;
using DexQuiz.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace DexQuiz.Infrastructure.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(DexQuizContext context) : base(context)
        {
        }
    }
}
