using Microsoft.AspNetCore.Identity;

namespace BusinessPdf.ApiService.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        // Many-to-many relationship
        public ICollection<ApplicationUserTenant> ApplicationUserTenants { get; set; }
    }
}
