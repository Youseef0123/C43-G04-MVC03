using Microsoft.AspNetCore.Identity;

namespace Demo.DataAccess.model.IdentityModel
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
    }
}
