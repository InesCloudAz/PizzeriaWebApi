using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Core.Interfaces;
using Pizzeria.Domain.Entities;
using Pizzeria.Infrastructure.Identities;
using System.Security.Claims;
using static Pizzeria.Domain.DTO.UserDTO;

namespace Pizzeria.API.Controllers
{

    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationUserContext _context;

        public AccountController(IAccountService accountService, UserManager<ApplicationUser> userManager, ApplicationUserContext context)
        {
            _accountService = accountService;
            _userManager = userManager;
            _context = context;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {

            var users = _userManager.Users.ToList();
            var userList = new List<object>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var userOrders = await _context.Orders
                    .Where(o => o.User.Id == user.Id)
                    .ToListAsync();

                var orderCount = userOrders.Count;
                var lastOrderDate = userOrders
                    .OrderByDescending(o => o.CreatedAt) 
                    .FirstOrDefault()?.CreatedAt;

                userList.Add(new
                {
                    user.Id,
                    user.UserName,
                    user.Email,
                    BonusPoints = user.BonusPoints,
                    Roles = roles,
                    OrderCount = orderCount,
                    LastOrderDate = lastOrderDate
                });
            }

            return Ok(userList);

            //var users = _userManager.Users.ToList();

            //var userList = new List<object>();

            //foreach (var user in users)
            //{
            //    var roles = await _userManager.GetRolesAsync(user);

            //    userList.Add(new
            //    {
            //        user.Id,
            //        user.UserName,
            //        user.Email,
            //        user.BonusPoints,
            //        Roles = roles
            //    });
            //}

            //return Ok(userList);


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

        [HttpGet("api/user-info")]
        [Authorize(Roles = "RegularUser,PremiumUser")]
        public async Task<IActionResult> GetUserInfo()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound("User not found");

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new
            {
                user.Id,
                user.UserName,
                user.Email,
                user.BonusPoints,
                Roles = roles
            });
        }


    }
}
