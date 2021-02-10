using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrjModule24.Services.Interfaces;

namespace PrjModule24.Controllers
{
    [ApiController]
    [Route("User/{id}")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IEfFileFolderContext _db;
        
        public AccountController(IEfFileFolderContext db, ILogger<AccountController> logger)
        {
            _logger = logger;
            _db = db;
        }

        [HttpPost]
        [Route("Open")]
        public async Task<IActionResult> OpenAccount(string id)
        {
            var guid = Guid.Parse(id);
            var account = await _db.UpdateAccountStateAsync(guid, true);
            
            return account.Match<IActionResult>(Ok, NotFound());
        }

        [HttpPost]
        [Route("Close")]
        public async Task<IActionResult> CloseAccount(string id)
        {
            var guid = Guid.Parse(id);
            var account = await _db.UpdateAccountStateAsync(guid, false);

            return account.Match<IActionResult>(Ok, NotFound());
        }

        [HttpPost]
        [Route("Deposit={amount}")]
        public async Task<IActionResult> DepositMoneyToAccount(string id, decimal amount)
        {
            var guid = Guid.Parse(id);
            var active=false;
            _db.GetAccountAsync(guid).Result.Match((acc) => active = acc.State,null);
            
            if (!active)
            {
                return StatusCode(405);
            }

            var account = await _db.UpdateAccountMoneyAsync(guid, amount);
            
            return account.Match<IActionResult>(Ok, NotFound());
        }

        [HttpPost]
        [Route("Withdrawal={amount}")]
        public async Task<IActionResult> WithdrawalMoneyFromAccount(string id, decimal amount)
        {
            var guid = Guid.Parse(id);
            var active = false;
            _db.GetAccountAsync(guid).Result.Match((acc) => active = acc.State, null);

            if (!active)
            {
                return StatusCode(405);
            }

            var account = await _db.UpdateAccountMoneyAsync(guid, -amount);

            return account.Match<IActionResult>(Ok, NotFound());
        }

        [HttpGet]
        [Route("GetBalance")]
        public async  Task<IActionResult> GetAccountBalance(string id)
        {
            var guid = Guid.Parse(id);
            var account = await _db.GetAccountAsync(guid);

            return account.Match<IActionResult>((acc)=>Ok(acc.Money), NotFound());
        }
    }
}