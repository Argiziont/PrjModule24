using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrjModule24.Services.Interfaces;

namespace PrjModule24.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IEfFileFolderContext _db;

        public UserController(IEfFileFolderContext context,ILogger<UserController> logger)
        {
            _logger = logger;
            _db = context;
        }

        [HttpGet]
        [Route("Users")]
        public IActionResult Get()
        {
            return Ok(_db.GetUsers());
        }

        [HttpGet]
        [Route("User/{id}")]
        public IActionResult Get(string id)
        {
            var user = UserStab.UsersDb.FirstOrDefault(u => u.Id == id);
            return user == null ? NotFound() : Ok(user);
        }
    }
}