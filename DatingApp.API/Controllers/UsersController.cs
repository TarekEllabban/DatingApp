using DatingApp.API.IServices;
using DatingApp.API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await this._userService.GetUser(id);
            return Ok(user);
        }
        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetUserByEmail(string username)
        {
            var user = await this._userService.GetUser(username);
            return Ok(user);
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }
        [HttpPut("{username}")]
        public async Task<IActionResult> UpdateUser(string username, UserForUpdateViewModel  userForUpdate)
        {
            if(username != User.FindFirst(ClaimTypes.Email).Value)
            {
                throw new UnauthorizedAccessException();
            }
            bool isUpdated = await _userService.UpdateUser(username, userForUpdate);
            return Ok(isUpdated);
        }
    }
}
