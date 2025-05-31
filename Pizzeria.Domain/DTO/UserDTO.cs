namespace Pizzeria.Domain.DTO
{
    public class UserDTO
    {
        public class CreateUserDTO
        {

            public string UserName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
        }

        public class UpdateUserDTO
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
        }

        public class LoginUserDTO
        {

            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class GetUserDTO
        {

            public string UserName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
        }
    }
}

