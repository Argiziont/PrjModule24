using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityOAuth2.Models;
using IdentityOAuth2.Models.Authenticate_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IdentityOAuth2.Controllers
{
    [Authorize]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthenticateController(UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("User/Login")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password)) return Unauthorized();
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.NameIdentifier, user.Id),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                _configuration["JWT:ValidIssuer"],
                _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new UserResponse
            {
                Token= new JwtSecurityTokenHandler().WriteToken(token),
                ExpirationDate = token.ValidTo
            });
        }

        [AllowAnonymous]
        [HttpPost("User/Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse {Status = "Error", Message = "User already exists!"});


            var user = new ApplicationUser
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var addResult = await _userManager.CreateAsync(user, model.Password);

            if (!addResult.Succeeded)
            {
                var errors= addResult.Errors.Aggregate("", (current, error) => current + ("Code: "+error.Code+"\nDescription: " + error.Description + "\n"));
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse
                        { Status = "Error", Message = errors });
            }

            var addRoleAsyncResult= await _userManager.AddToRoleAsync(user, "User");

            if (!addRoleAsyncResult.Succeeded)
            {
                var errors = addRoleAsyncResult.Errors.Aggregate("", (current, error) => current + ("Code: " + error.Code + "\nDescription: " + error.Description + "\n"));
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse
                    { Status = "Error", Message = errors });
            }

            return Ok(new ApiResponse {Status = "Success", Message = "User created successfully!"});
        }

        [AllowAnonymous]
        [HttpGet("User/TryLogin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        public IActionResult TryLogin()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return Ok();
            }
            return Unauthorized();
        }
    }
}

//{
// "Username": "SuperUser",
// "Email": "user@example.com",
// "Password": "SuperUser123$"
//}

//{
//    "Username": "SuperUser",
//    "Password": "SuperUser123$"
//}

//{
//    "Username": "Moderator",
//    "Password": "ModeratorUser123$$"
//}

//{
//    "Username": "DefaultUser",
//    "Password": "UserUser123$$"
//}