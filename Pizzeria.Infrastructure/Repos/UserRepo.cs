using Pizzeria.Domain.DTO;
using Pizzeria.Domain.Entities;
using Pizzeria.Infrastructure.Interfaces;

namespace Pizzeria.Infrastructure.Repos
{
    public class UserRepo : IUserRepo
    {
        public void AddUser(UserDTO.CreateUserDTO user)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(UserDTO.CreateUserDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
