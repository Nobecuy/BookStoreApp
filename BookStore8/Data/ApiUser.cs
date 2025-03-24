using Microsoft.AspNetCore.Identity;

namespace BookStore8.Data
{
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
