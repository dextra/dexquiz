using DexQuiz.Core.Entities;
using DexQuiz.Core.Interfaces.Repositories;
using DexQuiz.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace DexQuiz.Infrastructure.Repositories
{
    public class AnswerRepository : GenericRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(DexQuizContext context) : base(context)
        {
        }
    }
}
