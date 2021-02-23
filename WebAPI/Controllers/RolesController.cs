using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Authenticate_Models;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest( new ApiResponse
                    {Status = "Error", Message = "Role create failed! Please check user details and try again."});

            var result = await _roleManager.CreateAsync(new IdentityRole(name));
            if (result.Succeeded)
                return Ok();

            foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);

            return BadRequest(new ApiResponse
                {Status = "Error", Message = "Role create failed! Please check user details and try again." });
        }

        [HttpPost(nameof(DeleteWithId))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteWithId(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return BadRequest(new ApiResponse
                        {Status = "Error", Message = "Role delete failed! Please check user details and try again."});

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
                return BadRequest(new ApiResponse
                        {Status = "Error", Message = "Role delete failed! Please check user details and try again."});

            return Ok();
        }
        [ProducesResponseType(typeof(IdentityRole[]),StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        [HttpGet(nameof(GetRoles))]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles.ToArrayAsync();

            return Ok(roles);
        }
    }
}