using AutoFixture;
using DexQuiz.Core.Entities;
using DexQuiz.Core.Interfaces.Repositories;
using DexQuiz.Core.Interfaces.UoW;
using DexQuiz.Core.Services;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DexQuiz.Tests.Core.Services
{
    [TestFixture]
    public sealed class TrackServiceTest
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<ITrackRepository> _trackRepository;

        private Fixture _fixture;
        private TrackService _trackService;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();

            _unitOfWork = new Mock<IUnitOfWork>();
            _trackRepository = new Mock<ITrackRepository>();

            _trackService = new TrackService
            (
                _unitOfWork.Object,
                _trackRepository.Object
            );
        }

        [Test]
        public async Task Should_Add_Track()
        {
            var track = _fixture.Create<Track>();

            await _trackService.AddTrackAsync(track);

            _trackRepository.Verify(x => x.AddAsync(It.IsAny<Track>()), Times.Once);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        }

        [Test]
        public async Task Should_Delete_Track()
        {
            var track = _fixture.Create<Track>();

            _trackRepository
                .Setup(x => x.FindAsync(It.IsAny<int>()))
                .ReturnsAsync(track);

            _trackRepository.Setup(x => x.Remove(It.IsAny<Track>()));

            await _trackService.DeleteTrackAsync(track.Id);

            _trackRepository.Verify(x => x.FindAsync(It.IsAny<int>()), Times.Once);
            _trackRepository.Verify(x => x.Remove(It.IsAny<Track>()), Times.Once);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        }

        [Test]
        public async Task Should_Find_Track()
        {
            var id = _fixture.Create<int>();
            var track = _fixture.Create<Track>();

            _trackRepository
                .Setup(x => x.FindAsync(It.IsAny<int>()))
                .ReturnsAsync(track);

            var result = await _trackService.GetTrackByIdAsync(id);

            _trackRepository.Verify(x => x.FindAsync(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task Should_Get_All_Available_Tracks()
        {
            var track = _fixture.CreateMany<Track>(5);

            _trackRepository
                .Setup(x => x.FindAsync(x => x.Available))
                .ReturnsAsync(track);

            var result = await _trackService.GetAllAvailableTracksAsync();

            _trackRepository.Verify(x => x.FindAsync(x => x.Available), Times.Once);
        }

        [Test]
        public async Task Should_Get_Track_By_Id()
        {
            var track = _fixture.Create<Track>();

            _trackRepository
                .Setup(x => x.FindAsync(It.IsAny<int>()))
                .ReturnsAsync(track);

            var result = await _trackService.GetTrackByIdAsync(track.Id);

            _trackRepository.Verify(x => x.FindAsync(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task Should_Update_Track()
        {
            var track = _fixture.Create<Track>();

            _trackRepository.Setup(x => x.Update(It.IsAny<Track>()));

            await _trackService.UpdateTrackAsync(track);

            _trackRepository.Verify(x => x.Update(It.IsAny<Track>()), Times.Once);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        }
    }
}
