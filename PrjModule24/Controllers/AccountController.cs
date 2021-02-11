using System;
using System.Threading.Tasks;
using JWTAuthenticationWithSwagger.Authentication;
using Microsoft.AspNetCore.Http;
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
            
            return account.Match<IActionResult>(
                (_) => StatusCode(StatusCodes.Status200OK, new Response { Message = "Account opened successfully", Status = "Success" }), 
                () => StatusCode(StatusCodes.Status500InternalServerError, new Response {Message = "Couldn't open account", Status = "Error"}));
        }

        [HttpPost]
        [Route("Close")]
        public async Task<IActionResult> CloseAccount(string id)
        {
            var guid = Guid.Parse(id);
            var account = await _db.UpdateAccountStateAsync(guid, false);

            return account.Match<IActionResult>(
                (_) => StatusCode(StatusCodes.Status200OK, new Response { Message = "Account closed successfully", Status = "Success" }),
                () => StatusCode(StatusCodes.Status500InternalServerError, new Response { Message = "Couldn't close account", Status = "Error" }));
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
                return StatusCode(StatusCodes.Status405MethodNotAllowed, new Response { Message = "Couldn't make deposit to account", Status = "Error" });
            }

            var account = await _db.UpdateAccountMoneyAsync(guid, amount);

            return account.Match<IActionResult>(
                (_) => StatusCode(StatusCodes.Status200OK, new Response { Message = "Deposit made successfully", Status = "Success" }),
                () => StatusCode(StatusCodes.Status500InternalServerError, new Response { Message = "Couldn't made deposit", Status = "Error" }));
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
                return StatusCode(StatusCodes.Status405MethodNotAllowed, new Response { Message = "Couldn't make withdrawal from account", Status = "Error" });
            }

            var account = await _db.UpdateAccountMoneyAsync(guid, -amount);

            return account.Match<IActionResult>(
                (_) => StatusCode(StatusCodes.Status200OK, new Response { Message = "Withdrawal made successfully", Status = "Success" }),
                () => StatusCode(StatusCodes.Status500InternalServerError, new Response { Message = "Couldn't made deposit", Status = "Error" }));
        }

        [HttpGet]
        [Route("GetBalance")]
        public async  Task<IActionResult> GetAccountBalance(string id)
        {
            var guid = Guid.Parse(id);
            var account = await _db.GetAccountAsync(guid);

            return account.Match<IActionResult>(
                (acc) => StatusCode(StatusCodes.Status200OK, new Response { Message = $"Your account balance is {acc.Money}", Status = "Success" }),
                () => StatusCode(StatusCodes.Status500InternalServerError, new Response { Message = "Couldn't made deposit", Status = "Error" }));
        }
    }
}