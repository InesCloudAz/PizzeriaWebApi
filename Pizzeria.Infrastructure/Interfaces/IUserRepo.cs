using Pizzeria.Domain.Entities;
using static Pizzeria.Domain.DTO.UserDTO;

namespace Pizzeria.Infrastructure.Interfaces
{
    public interface IUserRepo
    {
        public void AddUser(CreateUserDTO user);
        public void UpdateUser(CreateUserDTO user);
        public ApplicationUser GetUserById(int id);
    }
}
