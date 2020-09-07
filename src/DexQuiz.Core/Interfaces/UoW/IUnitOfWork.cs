using DexQuiz.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DexQuiz.Core.Interfaces.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync();
        void Rollback();
    }
}
