using AutoMapper;
using DexQuiz.Core.Entities;
using DexQuiz.Core.Interfaces.Services;
using DexQuiz.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get an user by id
        /// </summary>
        /// <param name="id"> Primary key</param>
        /// <response code="200">Returns a build</response>
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<UserModel> GetAsync (int id)
        {
            return _mapper.Map<UserModel>(await _userService.FindUserById(id));
        }

        /// <summary>
        /// Add a new user into the database
        /// </summary>
        /// <param name="newUser"> User information</param>
        /// <response code="200">Returns the new user</response>
        /// <response code="400">Missing some information</response>

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PostAsync([FromBody] UserModel newUser)
        {
            try
            {
                var userEntity = _mapper.Map<User>(newUser);
                var result = await _userService.AddUser(userEntity);

                if (result)
                    return CreatedAtRoute("GetUser", new { id = userEntity.Id }, userEntity);
                else
                    return BadRequest("Something wrong has happened");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
