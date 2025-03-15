using Microsoft.AspNetCore.Identity;

namespace BusinessPdf.ApiService.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string JobTitle { get; set; }
        public ICollection<ApplicationUserTenant> ApplicationUserTenants { get; set; }
    }
}
