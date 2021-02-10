using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrjModule24.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace PrjModule24.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IEfFileFolderContext _db;

        public UserController(IEfFileFolderContext context, ILogger<UserController> logger)
        {
            _logger = logger;
            _db = context;
        }

        [HttpGet]
        [Route("Users")]
        public async Task<IActionResult> Get()
        {
            var users = await _db.GetUsersAsync();
            
            if (users.Count > 0)
                return Ok(users);

            return NotFound();
        }

        [HttpGet]
        [Route("User/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var guid = Guid.Parse(id);
            var user = await _db.GetUserAsync(guid);
            return user.Match<IActionResult>(Ok, NotFound());
            
        }
    }
}