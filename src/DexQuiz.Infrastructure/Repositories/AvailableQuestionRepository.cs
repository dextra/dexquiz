using DexQuiz.Core.Entities;
using DexQuiz.Core.Interfaces.Repositories;
using DexQuiz.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace DexQuiz.Infrastructure.Repositories
{
    public class AvailableQuestionRepository : GenericRepository<AvailableQuestion>, IAvailableQuestionRepository
    {
        public AvailableQuestionRepository(DexQuizContext context) : base(context)
        {
        }
    }
}
