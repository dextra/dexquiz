using AutoMapper;
using DexQuiz.Core.Entities;
using DexQuiz.Core.Interfaces.Services;
using DexQuiz.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DexQuiz.Server.Controllers
{
    [Route("api/ranking")]
    [ApiController]
    [Authorize]
    public class RankingController : ControllerBase
    {
        private readonly IRankingService _rankingService;
        private readonly IMapper _mapper;
        private readonly ILogger<RankingController> _logger;

        public RankingController(IRankingService rankingService,
                                 IMapper mapper,
                                 ILogger<RankingController> logger)
        {
            _rankingService = rankingService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Shows the ranking in ascending order for a given track.
        /// If is a public access, will have the top positions without users' ids.
        /// If the user is an admin, will have the top positions with users ids.
        /// If the user is a regular user, only the user's position will have the user id.
        /// For a regular user, the ranking will return the top positions, if the user is in it,
        /// or show the top positions plus the user's position.
        /// If the user is not in the ranking yet, the top positions will be returned.
        /// Only when an user finishes the track questions, his/her ranking will be displayed.
        /// The ranking ordering criteria is by most points, then by lowest completed time.
        /// </summary>
        /// <param name="trackId">Track id</param>
        /// <param name="top">Number of ranking positions to be fetched</param>
        /// <response code="200">Returns the ranking</response>
        [HttpGet("track/{trackId}")]
        public async Task<IActionResult> GetTrackRankings(int trackId,
                                              [FromQuery(Name = "top")] int? top = null)
        {
            // TODO: Usar cache para rankings públicos?
            bool isUserAdmin = this.IsLoggedUserAdmin();
            int? userId = this.GetLoggedUserId();

            var trackRankings = (isUserAdmin, userId) switch
            {
                (_, null) => await _rankingService.GetPublicTrackRankingsAsync(trackId, top),
                (true, _) => await _rankingService.GetTrackRankingsForAdminAsync(trackId, top),
                (false, _) => await _rankingService.GetTrackRankingsForUserAsync(trackId, (int)userId, top)
            };

            return Ok(_mapper.Map<TrackRankingModel[]>(trackRankings));
        }

        /// <summary>
        /// Show all available tracks with your respective ranking.
        /// </summary>
        /// <param name="top">Number of ranking positions to be fetched</param>
        /// <response code="200">Returns the track with ranking</response>
        [HttpGet("trackwithranking")]
        [ProducesResponseType(typeof(IEnumerable<TrackWithRankingModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAvailablesTrackWithRankings
        (
            [FromQuery(Name = "top")] int? top = null
        )
        {
            bool isUserAdmin = this.IsLoggedUserAdmin();
            int? userId = this.GetLoggedUserId();

            var trackRankings = (isUserAdmin, userId) switch
            {
                (_, null) => await _rankingService.GetAllPublicTracksWithRankingsAsync(top),
                (true, _) => await _rankingService.GetAllTracksWithRankingsForAdminAsync(top),
                (false, _) => await _rankingService.GetAllTracksWithRankingsForUserAsync((int)userId, top)
            };

            return Ok(_mapper.Map<IEnumerable<TrackWithRankingModel>>(trackRankings));
        }
    }
}