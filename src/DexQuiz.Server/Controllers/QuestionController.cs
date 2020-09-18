using AutoMapper;
using DexQuiz.Core.Entities;
using DexQuiz.Core.Enums;
using DexQuiz.Core.Exceptions;
using DexQuiz.Core.Interfaces.Services;
using DexQuiz.Server.Models;
using DexQuiz.Server.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DexQuiz.Server.Controllers
{
    [Route("api/questions")]
    [ApiController]
    [Authorize]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IRankingService _rankingService;
        private readonly IMapper _mapper;

        public QuestionController(IQuestionService questionService, IRankingService rankingService, IMapper mapper)
        {
            _questionService = questionService;
            _rankingService = rankingService;
            _mapper = mapper;
        }

        #region Admin control

        /// <summary>
        /// Get question by id
        /// </summary>
        /// <param name="id"> Primary key</param>
        /// <response code="200">Returns a build</response>
        [HttpGet("{id}", Name = "GetQuestion")]
        [DexquizAuthorize(RoleEnum = UserType.Administrator)]
        public async Task<QuestionModel> GetAsync(int id)
        {
            return _mapper.Map<QuestionModel>(await _questionService.FindQuestionByIdAsync(id));
        }

        /// <summary>
        /// Gets all questions for a track
        /// </summary>
        /// <param name="trackId">Track id</param>
        /// <response code="200">Returns all questions of the track</response>
        [HttpGet("track/{trackId}/all")]
        [DexquizAuthorize(RoleEnum = UserType.Administrator)]
        public async Task<IActionResult> GetTrackQuestionsAsync(int trackId)
        {
            var questions = _mapper.Map<QuestionModel[]>(await _questionService.GetTrackQuestionsAsync(trackId));
            return Ok(questions);
        }

        /// <summary>
        /// Add a new question into the database
        /// </summary>
        /// <param name="question"> Question information</param>
        /// <response code="200">Returns the new question</response>
        /// <response code="400">Missing some information</response>

        [HttpPost]
        [DexquizAuthorize(RoleEnum = UserType.Administrator)]
        public async Task<IActionResult> PostAsync([FromBody] QuestionModel question)
        {
            try
            {
                var questionEntity = _mapper.Map<Question>(question);
                var result = await _questionService.AddQuestionAsync(questionEntity);

                if (result.Result)
                    return CreatedAtRoute("GetQuestion",
                                          new { id = questionEntity.Id },
                                          _mapper.Map<QuestionModel>(questionEntity));
                else
                    return BadRequest(result);
            }
            catch (Exception e)
            {
                return BadRequest(new ReturnData { Message = e.Message, Result = false });
            }
        }

        /// <summary>
        /// Updates a question
        /// </summary>
        /// <param name="id">Question id</param>
        /// <param name="question">Question with updated information</param>
        /// <response code="204">If the question was successfully updated</response>
        /// <response code="400">If the question id in the URI and in the question model are different</response>
        [HttpPut("{id}")]
        [DexquizAuthorize(RoleEnum = UserType.Administrator)]
        public async Task<IActionResult> UpdateQuestionAsync(int id, [FromBody] QuestionModel question)
        {
            var questionEntity = _mapper.Map<Question>(question);
            try
            {
                if (questionEntity.Id != id)
                {
                    return BadRequest(new ReturnData { Message = "O id da questão na URI deve ser igual ao id no objeto.", Result = false });
                }
                else
                {
                    await _questionService.UpdateQuestionAsync(questionEntity);
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return BadRequest(new ReturnData { Message = e.Message, Result = false });
            }
        }

        /// <summary>
        /// Deletes a question
        /// </summary>
        /// <param name="id">Question id</param>
        /// <response code="204">If the question was successfully deleted</response>
        [HttpDelete("{id}")]
        [DexquizAuthorize(RoleEnum = UserType.Administrator)]
        public async Task<IActionResult> DeleteQuestionAsync(int id)
        {
            try
            {
                var result = await _questionService.DeleteQuestionAsync(id);
                if (result.Result)
                {
                    return NoContent();
                }                
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new ReturnData { Message = e.Message, Result = false });
            }
        }

        #endregion

        #region User actions

        /// <summary>
        /// Picks a question for a user.
        /// The question will be randomly chosen from the available track questions.
        /// 12 questions per track are available for an user, 4 questions for each question level (easy, medium and hard).
        /// </summary>
        /// <param name="trackId">Track id</param>
        /// <response code="200">Returns a question picked for the user</response>
        /// <response code="400">If the user already completed the track</response>
        [HttpGet("track/{trackId}")]
        public async Task<IActionResult> PickQuestionForUserAsync(int trackId)
        {
            int userId = (int)this.GetLoggedUserId();
            try
            {
                await _questionService.InitializeQuestionsForUserIfNotCreatedAsync(userId, trackId);
                await _rankingService.InitializeRankingIfNotCreatedAsync(userId, trackId);
                return Ok(_mapper.Map<QuestionForUserModel>(await _questionService.PickQuestionForUserAsync(userId, trackId)));
            }
            catch (Exception e)
            {
                return BadRequest(new ReturnData { Message = e.Message, Result = false });
            }
        }

        /// <summary>
        /// Picks the current question number to the user.
        /// 12 questions per track are available for an user, 4 questions for each question level (easy, medium and hard).
        /// </summary>
        /// <param name="trackId">Track id</param>
        /// <response code="200">Returns the number of the current question for the user</response>
        [HttpGet("track/{trackId}/progress")]
        public async Task<IActionResult> PickCurrentProgressAsync(int trackId)
        {
            int userId = (int)this.GetLoggedUserId();
            try
            {
                await _questionService.InitializeQuestionsForUserIfNotCreatedAsync(userId, trackId);
                await _rankingService.InitializeRankingIfNotCreatedAsync(userId, trackId);
                return Ok(new CurrentQuestionForUserModel() { QuestionNumber = await _questionService.PickProgressForUserAsync(userId, trackId) });
            }
            catch (Exception e)
            {
                return BadRequest(new ReturnData { Message = e.Message, Result = false });
            }
        }

        /// <summary>
        /// Saves an user answer for a question.
        /// </summary>
        /// <param name="answeredQuestion">The answered question</param>
        /// <response code="204">If the user's answer for a question was successfully saved</response>
        /// <response code="400">If the user already completed the track; if the user already answered the question; or if the answer does not belong to the question</response>
        [HttpPost("answer")]
        public async Task<IActionResult> AnswerQuestionAsync([FromBody] AnsweredQuestionModel answeredQuestion)
        {
            int userId = (int)this.GetLoggedUserId();
            var answeredQuestionEntity = _mapper.Map<AnsweredQuestion>(answeredQuestion);
            answeredQuestionEntity.UserId = userId;

            try
            {
                var result = await _questionService.SaveAnsweredQuestionAsync(answeredQuestionEntity);
                if (result.Result)
                {
                    await _rankingService.UpdateRankingAfterUserAnswerAsync(answeredQuestionEntity);
                    return NoContent();
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception e)
            {
                return BadRequest(new ReturnData { Message = e.Message, Result = false });
            }
        }

        #endregion
    }
}
