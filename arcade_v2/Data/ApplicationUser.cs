using arcade_v2.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace arcade_v2.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Score> Scores { get; } = [];

    }

}
