using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PrjModule24.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Users")]
        public IActionResult Get()
        {
            return Ok(UserStab.UsersDb.ToArray());
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