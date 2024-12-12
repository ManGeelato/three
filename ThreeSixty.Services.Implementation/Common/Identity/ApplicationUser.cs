using Microsoft.AspNetCore.Identity;

namespace ThreeSixty.Services.Implementation.Common.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string LastName { get; set; }
    }
}
