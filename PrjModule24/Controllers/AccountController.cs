using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PrjModule24.Controllers
{
    [ApiController]
    [Route("User/{id}")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("Open")]
        public IActionResult OpenAccount(string id)
        {
            var user = UserStab.UsersDb.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            user.Account.State = true;

            return Ok(user.Account);
        }

        [HttpPost]
        [Route("Deposit={amount}")]
        public IActionResult DepositMoneyToAccount(string id, int amount)
        {
            var user = UserStab.UsersDb.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            user.Account.Money += amount;

            return Ok(user.Account);
        }

        [HttpPost]
        [Route("Withdrawal={amount}")]
        public IActionResult WithdrawalMoneyFromAccount(string id, int amount)
        {
            var user = UserStab.UsersDb.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            user.Account.Money -= amount;

            return Ok(user.Account);
        }

        [HttpGet]
        [Route("GetBalance")]
        public IActionResult GetAccountBalance(string id)
        {
            var user = UserStab.UsersDb.FirstOrDefault(u => u.Id == id);
            return user == null ? NotFound() : Ok(user.Account.Money);
        }
    }
}