using System.Linq;
using System.Threading.Tasks;
using JWTAuthenticationWithSwagger.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[Controller]")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost(nameof(Create))]
        public async Task<IActionResult> Create(string name)
        {
            if (string.IsNullOrEmpty(name))
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                    {Status = "Error", Message = "Role create failed! Please check user details and try again."});

            var result = await _roleManager.CreateAsync(new IdentityRole(name));
            if (result.Succeeded)
                return StatusCode(StatusCodes.Status200OK,
                    new Response {Message = "Role created successfully", Status = "Success"});

            foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);

            return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {Status = "Error", Message = "Role create failed! Please check user details and try again." });
        }

        [HttpPost(nameof(DeleteWithId))]
        public async Task<IActionResult> DeleteWithId(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                        {Status = "Error", Message = "Role delete failed! Please check user details and try again."});

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                        {Status = "Error", Message = "Role delete failed! Please check user details and try again."});

            return StatusCode(StatusCodes.Status200OK,
                new Response {Message = "Role deleted successfully", Status = "Success"});
        }

        [HttpGet(nameof(GetRoles))]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles.ToArrayAsync();

            return Ok(roles);
        }
    }
}