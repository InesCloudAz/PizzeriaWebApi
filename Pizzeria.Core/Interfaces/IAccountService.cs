using static Pizzeria.Domain.DTO.UserDTO;

namespace Pizzeria.Core.Interfaces
{
    public interface IAccountService
    {
        Task<bool> Register(CreateUserDTO user, string role);
        Task<string> Login(LoginUserDTO user);
        Task<bool> Update(UpdateUserDTO user, string userId);
        Task<GetUserDTO> GetUser(string userId);
        Task<bool> UpdateUserRole(string userId, string newRole);
    }
}
