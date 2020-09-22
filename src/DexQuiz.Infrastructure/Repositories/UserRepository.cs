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

        public async Task RemoveAccountData(int userId)
        {
            _context.TrackRankings.RemoveRange(_context.TrackRankings.Where(tr => tr.UserId == userId));
            _context.AnsweredQuestions.RemoveRange(_context.AnsweredQuestions.Where(aq => aq.UserId == userId));
            _context.AvailableQuestions.RemoveRange(_context.AvailableQuestions.Where(aq => aq.UserId == userId));
            _context.Users.Remove(await _context.Users.FindAsync(userId));
        }
    }
}
