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
    public class TrackRepository : GenericRepository<Track>, ITrackRepository
    {
        public TrackRepository(DexQuizContext context) : base(context)
        {
        }

        public async Task<bool> IsTrackDoneByUserAsync(int userId, int trackId)
        {
            var available = _context.AvailableQuestions
                .Where(x => x.TrackId == trackId && x.UserId == userId);
            var answered = _context.AnsweredQuestions
                .Where(x => x.TrackId == trackId && x.UserId == userId);

            return await available.CountAsync() == await answered.CountAsync();
        }
    }
}
