using DexQuiz.Core.Entities;
using DexQuiz.Core.Interfaces.Repositories;
using DexQuiz.Core.Interfaces.Services;
using DexQuiz.Core.Interfaces.UoW;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DexQuiz.Core.Services
{
    public class TrackService : ITrackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITrackRepository _trackRepository;

        public TrackService(IUnitOfWork unitOfWork, ITrackRepository trackRepository)
        {
            _unitOfWork = unitOfWork;
            _trackRepository = trackRepository;
        }

        public async Task AddTrackAsync(Track track)
        {
            await _trackRepository.AddAsync(track);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteTrackAsync(int id)
        {
            var track = await GetTrackByIdAsync(id);
            _trackRepository.Remove(track);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Track>> GetAllAvailableTracksAsync() =>
            await _trackRepository.FindAsync(x => x.Available);

        public async Task<Track> GetTrackByIdAsync(int id) =>
            await _trackRepository.FindAsync(id);

        public async Task UpdateTrackAsync(Track track)
        {
            _trackRepository.Update(track);
            await _unitOfWork.CommitAsync();
        }
    }
}
