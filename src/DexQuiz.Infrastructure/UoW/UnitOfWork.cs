using DexQuiz.Core.Interfaces.Repositories;
using DexQuiz.Core.Interfaces.UoW;
using DexQuiz.Infrastructure.Persistence;
using DexQuiz.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexQuiz.Infrastructure.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DexQuizContext _context;

        public UnitOfWork(DexQuizContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Rollback()
        {
            _context
                  .ChangeTracker
                  .Entries()
                  .ToList()
                  .ForEach(x => x.Reload());
        }
    }
}
