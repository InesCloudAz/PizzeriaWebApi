using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pizzeria.Core.Interfaces;
using Pizzeria.Domain.DTO;
using Pizzeria.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pizzeria.Core.Services
{
    internal class AccountService : IAccountService
    {

        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountService(SignInManager<ApplicationUser> signInManager,
                UserManager<ApplicationUser> userManager,
                RoleManager<IdentityRole> roleManager,
                IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }


        public async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var authClaims = new List<Claim>
            {

                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT-Secret-Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["IssuerAudiens"],
                audience: _configuration["IssuerAudiens"],
                expires: DateTime.UtcNow.AddHours(10),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<UserDTO.GetUserDTO> GetUser(string userId)
        {
           var authUser = await _userManager.FindByIdAsync(userId);

            if (authUser == null)
                return null;

            return new UserDTO.GetUserDTO
            {
                UserName = authUser.UserName,
                Email = authUser.Email,
                Phone = authUser.PhoneNumber
            };
        }

        public async Task<string> Login(UserDTO.LoginUserDTO user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, false, false);
            if (!result.Succeeded)
                return null;

            var pzUser = await _userManager.FindByNameAsync(user.UserName);
            if (pzUser == null)
                return null;

            return await GenerateJwtToken(pzUser);
        }

        public async Task<bool> Register(UserDTO.CreateUserDTO user, string role)
        {
            var newUser = new ApplicationUser()
            {
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.Phone,
            };

            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception("User creation failed: " + errors);
            }

            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            var roleResult = await _userManager.AddToRoleAsync(newUser, role);
            if (!roleResult.Succeeded)
            {
                var roleErrors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                throw new Exception("Adding user to role failed: " + roleErrors);
            }

            return true;
        }


        //public async Task<bool> Register(UserDTO.CreateUserDTO user, string role)
        //{
        //    var newUser = new ApplicationUser()
        //    {
        //        Email = user.Email,
        //        UserName = user.UserName,
        //        PhoneNumber = user.Phone,
        //    };

        //    var result = await _userManager.CreateAsync(newUser, user.Password);

        //    if (result.Succeeded)
        //    {
        //        // DEBUG: Skriv ut felen
        //        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        //        throw new Exception("User creation failed: " + errors);
        //    }


        //    {
        //        if (!await _roleManager.RoleExistsAsync(role))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole(role));
        //        }
        //        await _userManager.AddToRoleAsync(newUser, role);
        //    }

        //    return result.Succeeded;

        //}

        public async Task<bool> Update(UserDTO.UpdateUserDTO user, string userId)
        {

            var existingUser = await _userManager.FindByIdAsync(userId);
            if (existingUser == null)
                return false;

            existingUser.Email = user.Email;
            existingUser.PhoneNumber = user.Phone;
            existingUser.UserName = user.UserName;

            var result = await _userManager.UpdateAsync(existingUser);

            return result.Succeeded;

        }

        public async Task<bool> UpdateUserRole(string userId, string newRole)
        {

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            var existingRoles = await _userManager.GetRolesAsync(user);

            if (existingRoles.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, existingRoles);
                if (!removeResult.Succeeded)
                    return false;
            }

            if (!await _roleManager.RoleExistsAsync(newRole))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(newRole));
                if (!result.Succeeded)
                    return false;
            }

            var addResult = await _userManager.AddToRoleAsync(user, newRole);
            return addResult.Succeeded;
        }
    }
}
