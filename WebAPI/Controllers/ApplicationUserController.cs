﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    [Authorize]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IEfFileFolderContext _dbContext;

        public ApplicationUserController(IEfFileFolderContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Users")]
        public async Task<IActionResult> Get()
        {
            var users = await _dbContext.GetUsers();

            if (users.Count > 0)
                return Ok(users);

            return NotFound();
        }

        [HttpGet]
        [Route("User/{id}")]
        public async Task<IActionResult> GetWithId(string id)
        {
            var user = await _dbContext.GetUser(id);

            return user.Match<IActionResult>(Ok, NotFound());
        }
    }
}