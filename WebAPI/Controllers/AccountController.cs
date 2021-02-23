using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.Authenticate_Models;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> OpenAccountWithId(string id)
        {
            var account = await _db.UpdateAccountStateAsync(id, true);

            return account.Match<IActionResult>(
                _ => Ok(),
                () => BadRequest(new ApiResponse { Message = "Couldn't open account", Status = "Error" }));
        }

        [Authorize(Roles = "Admin,Moderator,User")]
        [HttpPost]
        [Route("Open")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> OpenAccount()
        {
            var userId = User.GetLoggedInUserId<string>();

            var account = await _db.UpdateAccountStateAsync(userId, true);

            return account.Match<IActionResult>(
                _ => Ok(),
                () => BadRequest(new ApiResponse { Message = "Couldn't open account", Status = "Error" }));

        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost]
        [Route("{id}/Close")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CloseAccountWithId(string id)
        {
            var account = await _db.UpdateAccountStateAsync(id, false);

            return account.Match<IActionResult>(
                _ => Ok(),
                () => BadRequest(new ApiResponse { Message = "Couldn't close account", Status = "Error" }));
        }

        [Authorize(Roles = "Admin,Moderator,User")]
        [HttpPost]
        [Route("Close")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CloseAccount()
        {
            var userId = User.GetLoggedInUserId<string>();

            var account = await _db.UpdateAccountStateAsync(userId, false);

            return account.Match<IActionResult>(
                _ => Ok(),
                () => BadRequest(new ApiResponse { Message = "Couldn't close account", Status = "Error" }));
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost]
        [Route("{id}/Deposit={amount}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DepositMoneyToAccountWithId(string id, decimal amount)
        {
            var active = false;
            _db.GetAccountAsync(id).Result.Match(acc => active = acc.State, null);

            if (!active)
                return BadRequest();

            var account = await _db.UpdateAccountMoneyAsync(id, amount);

            return account.Match<IActionResult>(
                _ => Ok(),
                () => BadRequest(new ApiResponse {Message = "Couldn't made deposit", Status = "Error"}));
        }

        [Authorize(Roles = "Admin,Moderator,User")]
        [HttpPost]
        [Route("Deposit={amount}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DepositMoneyToAccount(decimal amount)
        {
            var userId = User.GetLoggedInUserId<string>();

            var active = false;
            _db.GetAccountAsync(userId).Result.Match(acc => active = acc.State, null);

            if (!active)
                return BadRequest();

            var account = await _db.UpdateAccountMoneyAsync(userId, amount);

            return account.Match<IActionResult>(
                _ => Ok(),
                () => BadRequest(new ApiResponse {Message = "Couldn't made deposit", Status = "Error"}));
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost]
        [Route("{id}/Withdrawal={amount}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> WithdrawalMoneyFromAccountWithId(string id, decimal amount)
        {
            var active = false;
            _db.GetAccountAsync(id).Result.Match(acc => active = acc.State, null);

            if (!active)
                return BadRequest();

            var account = await _db.UpdateAccountMoneyAsync(id, -amount);


            return account.Match<IActionResult>(
                _ => Ok(),
                () => BadRequest(new ApiResponse { Message = "Couldn't withdrawal money", Status = "Error" }));
        }

        [Authorize(Roles = "Admin,Moderator,User")]
        [HttpPost]
        [Route("Withdrawal={amount}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> WithdrawalMoneyFromAccount(decimal amount)
        {
            var userId = User.GetLoggedInUserId<string>();

            var active = false;
            _db.GetAccountAsync(userId).Result.Match(acc => active = acc.State, null);

            if (!active)
                return BadRequest();

            var account = await _db.UpdateAccountMoneyAsync(userId, -amount);

                return account.Match<IActionResult>(
                _ => Ok(),
                () => BadRequest(new ApiResponse { Message = "Couldn't withdrawal money", Status = "Error" }));
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpGet]
        [Route("{id}/GetBalance")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAccountBalanceWithId(string id)
        {
            var account = await _db.GetAccountAsync(id);

            return account.Match<IActionResult>(
                acc => Ok(new ApiResponse {Message = acc.Money.ToString(CultureInfo.InvariantCulture), Status = "Success"}),
                () => BadRequest(new ApiResponse {Message = "Couldn't get balance", Status = "Error"}));
        }

        [Authorize(Roles = "Admin,Moderator,User")]
        [HttpGet]
        [Route("GetBalance")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAccountBalance()
        {
            var userId = User.GetLoggedInUserId<string>();

            var account = await _db.GetAccountAsync(userId);

            return account.Match<IActionResult>(
                acc => Ok(new ApiResponse { Message = acc.Money.ToString(CultureInfo.InvariantCulture), Status = "Success" }),
                () => BadRequest(new ApiResponse { Message = "Couldn't get balance", Status = "Error" }));
        }
    }
}