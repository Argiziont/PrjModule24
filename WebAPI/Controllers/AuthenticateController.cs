using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JWTAuthenticationWithSwagger.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Models;
using WebAPI.Models.Authenticate_Models;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IEfFileFolderContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticateController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
            IConfiguration configuration, IEfFileFolderContext dbContext)
        {
            _userManager = userManager;
            _configuration = configuration;
            _dbContext = dbContext;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpPost("User/Login")]
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

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        [AllowAnonymous]
        [HttpPost("User/Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response {Status = "Error", Message = "User already exists!"});


            var user = new ApplicationUser
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var addResult = await _userManager.CreateAsync(user, model.Password);

            if (!addResult.Succeeded )
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                        {Status = "Error", Message = "User creation failed! Please check user details and try again."});

            await _dbContext.AddAccountAsync(new UserBankingAccount
            {
                ApplicationUser = user,
                Money = 0,
                State = false
            });

            await _dbContext.AddUserProfileAsync(new UserProfile
            {
                Age = 15,
                Name = "NameTEMPLATE",
                ApplicationUser = user
            });
            return Ok(new Response {Status = "Success", Message = "User created successfully!"});
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