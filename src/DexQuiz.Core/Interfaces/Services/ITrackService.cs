using DexQuiz.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DexQuiz.Core.Interfaces.Services
{
    public interface ITrackService
    {
        Task<IEnumerable<Track>> GetAllAvailableTracksAsync();
        Task<Track> GetTrackByIdAsync(int id);
        Task AddTrackAsync(Track track);
        Task UpdateTrackAsync(Track track);
        Task DeleteTrackAsync(int id);
    }
}
