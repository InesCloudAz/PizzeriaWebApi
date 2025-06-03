using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Core.Interfaces;
using System.Security.Claims;
using static Pizzeria.Domain.DTO.UserDTO;

namespace Pizzeria.API.Controllers
{

    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("api/login")]
        public async Task<IActionResult> Login(LoginUserDTO user)
        {

            var token = await _accountService.Login(user);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            return Ok(new { token });

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/register")]

        public async Task<IActionResult> Register(CreateUserDTO user)
        {
            
            var result = await _accountService.Register(user, "RegularUser");

            if (!result)
                return BadRequest();

            return Ok("User created");
        }


            [HttpPut]
        [Authorize]
        [Route("api/update-user")]
        public async Task<IActionResult> Update(UpdateUserDTO user)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID not found");

            var result = await _accountService.Update(user, userId);

            if (result)
            {
                return Ok("Updated");
            }

            else
            {
                return BadRequest();
            }
        }

        [HttpGet("api/get-all-users")]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID not found");

            var result = await _accountService.GetUser(userId);

            if (result == null)
                return NotFound("Invalid username or password");

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("api/regular-to-premium-user")]
        public async Task<IActionResult> RegularToPremuimUser(string userId)
        {

            await _accountService.UpdateUserRole(userId, "PremiumUser");

            return Ok("User is premium now");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("api/premium-to-regular-user")]
        public async Task<IActionResult> PremiumToRegularUser(string userId)
        {

            await _accountService.UpdateUserRole(userId, "RegularUser");

            return Ok("User is regular now");
        }

    }
}
