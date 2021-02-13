using System.Threading.Tasks;
using JWTAuthenticationWithSwagger.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services.Extensions;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("User")]
    public class AccountController : ControllerBase
    {
        private readonly IEfFileFolderContext _db;

        public AccountController(IEfFileFolderContext db)
        {
            _db = db;
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost]
        [Route("{id}/Open")]
        public async Task<IActionResult> OpenAccount(string id)
        {
            var account = await _db.UpdateAccountStateAsync(id, true);

            return account.Match<IActionResult>(
                _ => StatusCode(StatusCodes.Status200OK,
                    new Response {Message = "Account opened successfully", Status = "Success"}),
                () => StatusCode(StatusCodes.Status500InternalServerError,
                    new Response {Message = "Couldn't open account", Status = "Error"}));
        }

        [Authorize(Roles = "Admin,Moderator,User")]
        [HttpPost]
        [Route("Open")]
        public async Task<IActionResult> OpenAccount()
        {
            var userId = User.GetLoggedInUserId<string>();

            var account = await _db.UpdateAccountStateAsync(userId, true);

            return account.Match<IActionResult>(
                _ => StatusCode(StatusCodes.Status200OK,
                    new Response {Message = "Account opened successfully", Status = "Success"}),
                () => StatusCode(StatusCodes.Status500InternalServerError,
                    new Response {Message = "Couldn't open account", Status = "Error"}));
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost]
        [Route("{id}/Close")]
        public async Task<IActionResult> CloseAccount(string id)
        {
            var account = await _db.UpdateAccountStateAsync(id, false);

            return account.Match<IActionResult>(
                _ => StatusCode(StatusCodes.Status200OK,
                    new Response {Message = "Account closed successfully", Status = "Success"}),
                () => StatusCode(StatusCodes.Status500InternalServerError,
                    new Response {Message = "Couldn't close account", Status = "Error"}));
        }

        [Authorize(Roles = "Admin,Moderator,User")]
        [HttpPost]
        [Route("Close")]
        public async Task<IActionResult> CloseAccount()
        {
            var userId = User.GetLoggedInUserId<string>();

            var account = await _db.UpdateAccountStateAsync(userId, false);

            return account.Match<IActionResult>(
                _ => StatusCode(StatusCodes.Status200OK,
                    new Response {Message = "Account closed successfully", Status = "Success"}),
                () => StatusCode(StatusCodes.Status500InternalServerError,
                    new Response {Message = "Couldn't close account", Status = "Error"}));
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost]
        [Route("{id}/Deposit={amount}")]
        public async Task<IActionResult> DepositMoneyToAccount(string id, decimal amount)
        {
            var active = false;
            _db.GetAccountAsync(id).Result.Match(acc => active = acc.State, null);

            if (!active)
                return StatusCode(StatusCodes.Status405MethodNotAllowed,
                    new Response {Message = "Couldn't make deposit to account", Status = "Error"});

            var account = await _db.UpdateAccountMoneyAsync(id, amount);

            return account.Match<IActionResult>(
                _ => StatusCode(StatusCodes.Status200OK,
                    new Response {Message = "Deposit made successfully", Status = "Success"}),
                () => StatusCode(StatusCodes.Status500InternalServerError,
                    new Response {Message = "Couldn't made deposit", Status = "Error"}));
        }

        [Authorize(Roles = "Admin,Moderator,User")]
        [HttpPost]
        [Route("Deposit={amount}")]
        public async Task<IActionResult> DepositMoneyToAccount(decimal amount)
        {
            var userId = User.GetLoggedInUserId<string>();

            var active = false;
            _db.GetAccountAsync(userId).Result.Match(acc => active = acc.State, null);

            if (!active)
                return StatusCode(StatusCodes.Status405MethodNotAllowed,
                    new Response {Message = "Couldn't make deposit to account", Status = "Error"});

            var account = await _db.UpdateAccountMoneyAsync(userId, amount);

            return account.Match<IActionResult>(
                _ => StatusCode(StatusCodes.Status200OK,
                    new Response {Message = "Deposit made successfully", Status = "Success"}),
                () => StatusCode(StatusCodes.Status500InternalServerError,
                    new Response {Message = "Couldn't made deposit", Status = "Error"}));
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost]
        [Route("{id}/Withdrawal={amount}")]
        public async Task<IActionResult> WithdrawalMoneyFromAccount(string id, decimal amount)
        {
            var active = false;
            _db.GetAccountAsync(id).Result.Match(acc => active = acc.State, null);

            if (!active)
                return StatusCode(StatusCodes.Status405MethodNotAllowed,
                    new Response {Message = "Couldn't make withdrawal from account", Status = "Error"});

            var account = await _db.UpdateAccountMoneyAsync(id, -amount);

            return account.Match<IActionResult>(
                _ => StatusCode(StatusCodes.Status200OK,
                    new Response {Message = "Withdrawal made successfully", Status = "Success"}),
                () => StatusCode(StatusCodes.Status500InternalServerError,
                    new Response {Message = "Couldn't made deposit", Status = "Error"}));
        }

        [Authorize(Roles = "Admin,Moderator,User")]
        [HttpPost]
        [Route("Withdrawal={amount}")]
        public async Task<IActionResult> WithdrawalMoneyFromAccount(decimal amount)
        {
            var userId = User.GetLoggedInUserId<string>();

            var active = false;
            _db.GetAccountAsync(userId).Result.Match(acc => active = acc.State, null);

            if (!active)
                return StatusCode(StatusCodes.Status405MethodNotAllowed,
                    new Response {Message = "Couldn't make withdrawal from account", Status = "Error"});

            var account = await _db.UpdateAccountMoneyAsync(userId, -amount);

            return account.Match<IActionResult>(
                _ => StatusCode(StatusCodes.Status200OK,
                    new Response {Message = "Withdrawal made successfully", Status = "Success"}),
                () => StatusCode(StatusCodes.Status500InternalServerError,
                    new Response {Message = "Couldn't made deposit", Status = "Error"}));
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpGet]
        [Route("{id}/GetBalance")]
        public async Task<IActionResult> GetAccountBalance(string id)
        {
            var account = await _db.GetAccountAsync(id);

            return account.Match<IActionResult>(
                acc => StatusCode(StatusCodes.Status200OK,
                    new Response {Message = $"Your account balance is {acc.Money}", Status = "Success"}),
                () => StatusCode(StatusCodes.Status500InternalServerError,
                    new Response {Message = "Couldn't made deposit", Status = "Error"}));
        }

        [Authorize(Roles = "Admin,Moderator,User")]
        [HttpGet]
        [Route("GetBalance")]
        public async Task<IActionResult> GetAccountBalance()
        {
            var userId = User.GetLoggedInUserId<string>();

            var account = await _db.GetAccountAsync(userId);

            return account.Match<IActionResult>(
                acc => StatusCode(StatusCodes.Status200OK,
                    new Response {Message = $"Your account balance is {acc.Money}", Status = "Success"}),
                () => StatusCode(StatusCodes.Status500InternalServerError,
                    new Response {Message = "Couldn't made deposit", Status = "Error"}));
        }
    }
}