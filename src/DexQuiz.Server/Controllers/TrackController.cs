using AutoMapper;
using DexQuiz.Core.Entities;
using DexQuiz.Core.Enums;
using DexQuiz.Core.Exceptions;
using DexQuiz.Core.Interfaces.Services;
using DexQuiz.Server.Models;
using DexQuiz.Server.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Server.Controllers
{
    [Route("api/tracks")]
    [ApiController]
    [Authorize]
    public class TrackController : ControllerBase
    {
        private readonly ITrackService _trackService;
        private readonly IMapper _mapper;
        private readonly ILogger<TrackController> _logger;

        public TrackController(ITrackService trackService,
                               IMapper mapper,
                               ILogger<TrackController> logger)
        {
            _trackService = trackService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Gets all available tracks
        /// </summary>
        /// <response code="200">Returns all tracks</response>
        [HttpGet]
        public async Task<IActionResult> GetAllAvailableTracksAsync()
        {
            var tracks = await _trackService.GetAllAvailableTracksAsync();
            return Ok(tracks);
        }

        /// <summary>
        /// Gets track by id
        /// </summary>
        /// <param name="id">Track id</param>
        /// <response code="200">Returns the track of corresponding id</response>
        [HttpGet("{id}", Name = "GetTrack")]
        public async Task<IActionResult> GetTrackAsync(int id)
        {
            var track = await _trackService.GetTrackByIdAsync(id);
            return Ok(track);
        }

        /// <summary>
        /// Creates a track
        /// </summary>
        /// <param name="trackModel">Track model</param>
        /// <response code="201">Returns the created track and its URI</response>
        /// <response code="400">If the track model is invalid</response>
        [HttpPost]
        [DexquizAuthorize(RoleEnum = UserType.Administrator)]
        public async Task<IActionResult> AddTrackAsync([FromBody] TrackModel trackModel)
        {
            try
            {
                var trackEntity = _mapper.Map<Track>(trackModel);
                await _trackService.AddTrackAsync(trackEntity);
                return CreatedAtRoute("GetTrack", new { id = trackEntity.Id }, trackEntity);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Updates a track
        /// </summary>
        /// <param name="id">Track id</param>
        /// <param name="trackModel">Track model</param>
        /// <response code="204">If the track was successfully updated</response>
        /// <response code="400">If the track model is invalid</response>
        [HttpPatch("{id}")]
        [DexquizAuthorize(RoleEnum = UserType.Administrator)]
        public async Task<IActionResult> UpdateTrackAsync(int id, [FromBody] TrackModel trackModel)
        {
            try
            {
                var trackEntity = _mapper.Map<Track>(trackModel);
                if (id != trackEntity.Id)
                    return BadRequest(new ProcessResult { Message = "Track id from URI and from model should be the same.", Result = false });
                else
                {
                    await _trackService.UpdateTrackAsync(trackEntity);
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return BadRequest(new ProcessResult { Message = e.Message, Result = false });
            }
        }

        /// <summary>
        /// Deletes a track
        /// </summary>
        /// <param name="id">Track id</param>
        /// <response code="204">If the track was successfully deleted</response>
        /// <response code="400">If there was an error while deleting the track</response>
        [HttpDelete("{id}")]
        [DexquizAuthorize(RoleEnum = UserType.Administrator)]

        public async Task<IActionResult> DeleteTrackAsync(int id)
        {
            try
            {
                await _trackService.DeleteTrackAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new ProcessResult { Message = e.Message, Result = false });
            }
        }
    }
}
