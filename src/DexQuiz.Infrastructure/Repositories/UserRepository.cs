using DexQuiz.Core.Entities;
using DexQuiz.Core.Interfaces.Repositories;
using DexQuiz.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexQuiz.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DexQuizContext context) : base(context)
        {
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
        }
    }
}
