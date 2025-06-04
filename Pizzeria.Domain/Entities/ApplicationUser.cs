using Microsoft.AspNetCore.Identity;

namespace Pizzeria.Domain.Entities

{
    public class ApplicationUser : IdentityUser
    {

        public List<Order> Orders { get; set; }
        public int BonusPoints { get; set; }
    }
}

